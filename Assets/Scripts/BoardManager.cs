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



    public int NumberOfRows { get; private set; }
    public int NumberOfColumns { get; private set; }

    private Level lvl;

    private TilePiece[,] board;

    public void SetupBoard(Level lv)
    {
        lvl = lv;

        NumberOfRows = lvl.RowCount;
        NumberOfColumns = lvl.ColumnCount;
        board = new TilePiece[NumberOfRows, NumberOfColumns];


        for (int r = 0; r < NumberOfRows; r++)
        {
            for (int c = 0; c < NumberOfColumns; c++)
            {
                Level.Cell ce = lvl.Array[r].Cells[c];

                GameObject go = null;

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
                        break;
                }
                
                //AddTile(go, r, c);
                TilePiece tp = ((GameObject)Instantiate(go, new Vector3(c, -r, 0f), Quaternion.identity)).GetComponent<TilePiece>();

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
                    tp.MovingPiece = mp;
                }

                board[r, c] = tp;

            }
        }
    }


    public event EventHandler GameWon;
    public event EventHandler GameLost;

    [HideInInspector]
    public int MoveCount = 0;

    private MovablePiece selectedPiece;

    private void EndTurn()
    {
        List<TilePiece> boardList = board.Cast<TilePiece>().ToList();

        //check if the game has won, if all destinations have been completed
        if(!boardList.Any(t => t.IsDestination && !t.IsCompleted))
        {
            if (GameWon != null)
                GameWon.Invoke(this, null);
            return;
        }

        int movePieceCount = boardList.Count(p => p.HasMovingPiece);
        int destinationCount = boardList.Count(p => p.IsDestination);

        //check if game lost, if there are less moving pieces than destinations
        if (movePieceCount < destinationCount)
        {
            if (GameLost != null)
                GameLost.Invoke(this, null);
            return;
        }



    }



    public void MoveSelectedPiece(int rowOffset, int colOffset)
    {
        if (selectedPiece == null)
            return;

        int dRow = selectedPiece.Row + rowOffset;
        int dCol = selectedPiece.Column + colOffset;

        //check if valid move
        TilePiece destP = GetTile(dRow, dCol);
        if (destP == null)
            return;

        if(destP.IsLandable)
        {

        }
        else if(destP.IsRedirector)
        {

        }
        else if(destP.CanBeJumpedOver)
        {
            
        }
    }


    private TilePiece GetTile(int r, int c)
    {
        if (r < 0 || r >= NumberOfRows || c < 0 || c >= NumberOfColumns)
            return null;

        return board[r, c];
    }
}
