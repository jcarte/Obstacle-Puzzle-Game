using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class BoardManager : MonoBehaviour {

    //Templates
    //public GameObject[] MovablePieces;
    //public GameObject[] DestinationPieces;

    public GameObject MovablePiece_Red;
    public GameObject MovablePiece_Yellow;
    public GameObject MovablePiece_Blue;
    public GameObject MovablePiece_Green;

    public GameObject DestinationPiece_Red;
    public GameObject DestinationPiece_Yellow;
    public GameObject DestinationPiece_Blue;
    public GameObject DestinationPiece_Green;


    public GameObject EmptyPiece;
    public GameObject LandingPiece;
    public GameObject NonLandingPiece;
    public GameObject ObstaclePiece;
    public GameObject RedirectPiece;
    public GameObject EnemyPiece;
    public GameObject DisappearingPiece;
    public GameObject FakeDisappearingPiece;

    public GameObject InputListener;


    public InputListener Inputs { get; private set; }

    public int NumberOfRows { get; private set; }
    public int NumberOfColumns { get; private set; }

    private Level lvl;

    private TilePiece[,] board;

    //TODO make static / move to factory class
    public void SetupBoard(Level lv)
    {
        lvl = lv;

        NumberOfRows = lvl.RowCount;
        NumberOfColumns = lvl.ColumnCount;
        board = new TilePiece[NumberOfRows, NumberOfColumns];

        //Rubbish
        Inputs = ((GameObject)Instantiate(InputListener, new Vector3(0f, 0f, 0f), Quaternion.identity)).GetComponent<InputListener>();
        
        

        for (int r = 0; r < NumberOfRows; r++)
        {
            for (int c = 0; c < NumberOfColumns; c++)
            {
                Level.Cell ce = lvl.Array[r].Cells[c];

                GameObject go = null;
                int rotation = 0;

                switch (ce.Tile.Type)
                {
                    case Level.TileType.Empty:
                        go = EmptyPiece;
                        break;
                    case Level.TileType.Landing:
                        go = LandingPiece;
                        break;
                    case Level.TileType.NonLanding:
                        go = NonLandingPiece;
                        break;
                    case Level.TileType.Obstacle:
                        go = ObstaclePiece;
                        break;
                    case Level.TileType.Enemy:
                        go = EnemyPiece;
                        break;
                    case Level.TileType.Disappearing:
                        go = DisappearingPiece;
                        break;
                    case Level.TileType.FakeDisappearing:
                        go = FakeDisappearingPiece;
                        break;
                    case Level.TileType.Destination:
                        switch (ce.Tile.Colour)
                        {
                            case Level.ColourType.Red:
                                go = DestinationPiece_Red;
                                break;
                            case Level.ColourType.Yellow:
                                go = DestinationPiece_Yellow;
                                break;
                            case Level.ColourType.Blue:
                                go = DestinationPiece_Blue;
                                break;
                            case Level.ColourType.Green:
                                go = DestinationPiece_Green;
                                break;
                            default:
                                throw new System.Exception("Unknown Tile Colour Type");
                        }
                        break;
                    case Level.TileType.Redirect:
                        go = RedirectPiece;
                        if (ce.Tile.Redirect.RowOffset == 0 && ce.Tile.Redirect.ColumnOffset == 1)
                            rotation = 270;
                        else if (ce.Tile.Redirect.RowOffset == 1 && ce.Tile.Redirect.ColumnOffset == 0)
                            rotation = 180;
                        else if (ce.Tile.Redirect.RowOffset == 0 && ce.Tile.Redirect.ColumnOffset == -1)
                            rotation = 90;
                        break;
                }
                
                //AddTile(go, r, c);
                TilePiece tp = ((GameObject)Instantiate(go, new Vector3(c, -r, 0f), Quaternion.Euler(0,0, rotation))).GetComponent<TilePiece>();

                if(ce.Tile.Type == Level.TileType.Redirect)
                {
                    tp.RedirectColumnOffset = ce.Tile.Redirect.ColumnOffset;
                    tp.RedirectRowOffset = ce.Tile.Redirect.RowOffset;
                }


                if (ce.Movable != null)
                {
                    GameObject mgo;

                    switch (ce.Movable.Colour)
                    {
                        case Level.ColourType.Red:
                            mgo = MovablePiece_Red;
                            break;
                        case Level.ColourType.Yellow:
                            mgo = MovablePiece_Yellow;
                            break;
                        case Level.ColourType.Blue:
                            mgo = MovablePiece_Blue;
                            break;
                        case Level.ColourType.Green:
                            mgo = MovablePiece_Green;
                            break;
                        default:
                            throw new System.Exception("Unknown Tile Colour Type");
                    }

                    //AddTile(mgo, r, c);
                    MovablePiece mp = ((GameObject)Instantiate(mgo, new Vector3(c, -r, 0f), Quaternion.identity)).GetComponent<MovablePiece>();
                    mp.Move(tp);
                    mp.Clicked +=(s,e) => ChangeSelectedPiece(s as MovablePiece);
                }

                board[r, c] = tp;

            }
        }

        Inputs.MoveUp += (s, e) => ProcessMove(-1,0);
        Inputs.MoveDown += (s, e) => ProcessMove(1,0);
        Inputs.MoveRight += (s, e) => ProcessMove(0, 1);
        Inputs.MoveLeft += (s, e) => ProcessMove(0, -1);

        Inputs.StartListening();
    }


    private void ProcessMove(int rows, int cols)//TODO better name
    {

        Inputs.StopListening();
        if (AttemptToMoveSelected(rows, cols))
        {
            EndTurn();
        }
        else//move failed
        {
            Inputs.StartListening();
        }
    }

    private void ChangeSelectedPiece(MovablePiece m)
    {
        if(selectedPiece != null)
            selectedPiece.MovementCompleted -= (s, e) => Inputs.StartListening();

        selectedPiece = m;
        selectedPiece.MovementCompleted += (s,e) => Inputs.StartListening();
        
    }

    public event EventHandler GameWon;
    public event EventHandler GameLost;

    [HideInInspector]
    public int MoveCount = 0;

    private MovablePiece selectedPiece;

    private void EndTurn()
    {
        MoveCount++;

        List<TilePiece> boardList = board.Cast<TilePiece>().ToList();

        //check if the game has won, if all destinations have been completed
        if(!boardList.Any(t => t.IsDestination && !t.IsCompleted))
        {
            if (GameWon != null)
                GameWon.Invoke(this, null);
            return;
        }

        List<Color> cols = boardList.GroupBy(p => p.PieceColour).Select(pp => pp.Key).Where(c=> c != new Color(0,0,0,0)).ToList();

        foreach (Color c in cols)
        {
            
            int movePieceCount = boardList.Count(p => p.HasMovingPiece && p.MovingPiece.PieceColour == c);
            int destinationCount = boardList.Count(p => p.IsDestination && p.PieceColour == c);

            //check if game lost, if there are less moving pieces than destinations
            if (movePieceCount < destinationCount)
            {
                if (GameLost != null)
                    GameLost.Invoke(this, null);
                return;
            }
        }
    }


    private bool ResolveMovement(int rowOffset, int colOffset, MovablePiece m, out TilePiece finalP, out TilePiece jumpedP)
    {
        finalP = null;//setup
        jumpedP = null;

        if (m == null)
            return false;

        finalP = GetTile(m.Row + rowOffset, m.Column + colOffset);
        if (finalP == null)
            return false;//exit if heading out of board

        if (finalP.IsLandable)//normal move, no jump
        {
            m.Move(finalP);
            return true;
        }
        else if(finalP.CanBeJumpedOver)
        {
            TilePiece temp = GetTile(m.Row + (rowOffset * 2), m.Column + (colOffset * 2));
            if (temp != null && temp.IsLandable)
            {
                jumpedP = finalP;
                finalP = temp;
                m.Move(finalP);
                return true;
            }
        }
        return false;

    }

    public bool AttemptToMoveSelected(int rowOffset, int colOffset)
    {
        TilePiece jumpedOver;
        TilePiece landedOn;
        bool res;

        bool hasMovedOnce = false;

        do
        {
            jumpedOver = null;
            landedOn = null;
            res = ResolveMovement(rowOffset, colOffset, selectedPiece, out landedOn, out jumpedOver);
            if(res)
            {
                hasMovedOnce = true;
                rowOffset = landedOn.RedirectRowOffset;
                colOffset = landedOn.RedirectColumnOffset;
                
                //is selected moving destoyed?
                if (landedOn.KillsPieceOnLand || (jumpedOver != null && jumpedOver.KillsPieceOnJumpOver))
                {
                    selectedPiece.Kill();
                    selectedPiece = null;
                }

                //is jumped over piece destroyed?
                if (jumpedOver != null && jumpedOver.IsDestroyedOnJumpOver)//TODO move tile destruction inside tile? - reset all pub vars to empty, sprite to none, kill all pieces inside
                {
                    if (jumpedOver.MovingPiece != null)
                    {
                        jumpedOver.MovingPiece.Kill();
                    }

                    TilePiece newT = ((GameObject)Instantiate(EmptyPiece, new Vector3(jumpedOver.Column, -jumpedOver.Row, 0f), Quaternion.identity)).GetComponent<TilePiece>();
                    board[jumpedOver.Row, jumpedOver.Column] = newT;
                    jumpedOver.gameObject.SetActive(false);
                }
                
                else if (landedOn.IsDestroyedOnMoveOn)//is final landed piece destroyed?
                {
                    if (landedOn.MovingPiece != null)
                        landedOn.MovingPiece.Kill();

                    TilePiece newT = ((GameObject)Instantiate(EmptyPiece, new Vector3(landedOn.Column, -landedOn.Row, 0f), Quaternion.identity)).GetComponent<TilePiece>();
                    board[landedOn.Row, landedOn.Column] = newT;
                    landedOn.gameObject.SetActive(false);
                }

                

            }
        } while (res && landedOn != null && landedOn.IsRedirector && selectedPiece != null && !selectedPiece.IsKillPending);

        return hasMovedOnce;
    }


    //public bool AttemptToMoveSelected(int rowOffset, int colOffset)
    //{
    //    if (selectedPiece == null)
    //        return false;


    //    int dRow = selectedPiece.Row + rowOffset;
    //    int dCol = selectedPiece.Column + colOffset;

    //    TilePiece destP = GetTile(dRow, dCol);
    //    if (destP == null)
    //        return false;//exit if heading out of board


    //    TilePiece jumpedOver = null;

    //    //perform jump
    //    if(!destP.IsLandable && !destP.IsRedirector && destP.CanBeJumpedOver)//if can't land on then recalculate for jumping
    //    {
    //        jumpedOver = destP;
    //        dRow = selectedPiece.Row + (rowOffset * 2);
    //        dCol = selectedPiece.Column + (colOffset * 2);
    //        destP = GetTile(dRow, dCol);

    //        if (destP == null)
    //            return false;//jump would be out of board
    //    }

    //    if (destP.IsRedirector)
    //    {
    //        TilePiece destP2 = destP;//intermediatary step
    //        destP = GetTile(dRow + destP.RedirectRowOffset, dCol + destP.RedirectColumnOffset);

    //        selectedPiece.Move(destP2,destP);//step on fountain
    //        //selectedPiece.Move(destP);//move to where fountain is pointing

    //    }
    //    else if (destP.IsLandable)
    //    {
    //        selectedPiece.Move(destP);
    //    }
    //    else
    //    {
    //        return false;//no moves completed
    //    }


    //    //is final landed piece destroyed?
    //    if (destP.IsDestroyedOnMoveOn || (jumpedOver != null && jumpedOver.IsDestroyedOnJumpOver))
    //    {
    //        TilePiece newT = ((GameObject)Instantiate(EmptyPiece, new Vector3(destP.Column, -destP.Row, 0f), Quaternion.identity)).GetComponent<TilePiece>();
    //        board[destP.Row, destP.Column] = newT;
    //        destP.gameObject.SetActive(false);
    //    }

    //    //is selected moving destoyed?
    //    if (destP.KillsPieceOnLand || (jumpedOver != null && jumpedOver.KillsPieceOnJumpOver))
    //    {
    //        selectedPiece.Kill();
    //        selectedPiece = null;
    //    }

    //    //is jumped over piece destroyed?
    //    if (jumpedOver != null && jumpedOver.IsDestroyedOnJumpOver)
    //    {//TODO move tile destruction inside tile? - reset all pub vars to empty, sprite to none, kill all pieces inside
    //        if(jumpedOver.MovingPiece != null)
    //            jumpedOver.MovingPiece.Kill();

    //        TilePiece newT = ((GameObject)Instantiate(EmptyPiece, new Vector3(jumpedOver.Column, -jumpedOver.Row, 0f), Quaternion.identity)).GetComponent<TilePiece>();
    //        board[jumpedOver.Row, jumpedOver.Column] = newT;
    //        jumpedOver.gameObject.SetActive(false);
    //    }

    //    return true;
    //}



    //private void MoveSelectedToTile(TilePiece tp)
    //{


    //    TilePiece curTile = GetTile(selectedPiece.Row, selectedPiece.Column);
    //    selectedPiece.Move(tp);
    //    tp.MovingPiece = selectedPiece;
    //    curTile.MovingPiece = null;
    //}




    private TilePiece GetTile(int r, int c)
    {
        if (r < 0 || r >= NumberOfRows || c < 0 || c >= NumberOfColumns)
            return null;

        return board[r, c];
    }
}
