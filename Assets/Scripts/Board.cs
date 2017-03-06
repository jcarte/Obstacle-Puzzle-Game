using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System;
using System.Linq;
using System.Collections.Generic;

/// <summary>
/// All game logic around a board configuration
/// </summary>
public class Board : MonoBehaviour
{


    public int NumberOfRows { get; private set; }
    public int NumberOfColumns { get; private set; }

    [HideInInspector]
    public Level lvl;

    private TilePiece[,] board;

    public event Action<GameResult> GameFinished;

    public event Action TurnEnded;//TODO hook up

    public enum GameResult { Gold, Silver, Bronze, Loss }

    [HideInInspector]
    public Stopwatch Timer = new Stopwatch();//TODO start and stop


    [HideInInspector]
    public int MoveCount = 0;

    private MovablePiece selectedPiece;


    public void Init(Level lvl)
    {
        this.lvl = lvl;
        MoveCount = 0;
        NumberOfRows = lvl.RowCount;
        NumberOfColumns = lvl.ColumnCount;
        board = new TilePiece[NumberOfRows, NumberOfColumns];

    }

    public void AddTile(TilePiece tp)
    {
        board[tp.Row, tp.Column] = tp;
    }

    public void AddMoving(MovablePiece mp)
    {

    }

    public void ChangeSelectedPiece(MovablePiece m)//TODO stop inputs?
    {
        if (selectedPiece != null)
            selectedPiece.MovementCompleted -= (s, e) => Inputs.StartListening();

        selectedPiece = m;
        selectedPiece.MovementCompleted += (s, e) => Inputs.StartListening();

    }





    /// <summary>
    /// Increment move and check for win/loss
    /// </summary>
    public void EndTurn()//todo return to private, should be called internally
    {
        if (GameFinished == null)//can't continue if no one is listening
            throw new InvalidOperationException("Renderer not subscribed to GameFinish Event");
        
        MoveCount++;//increment move
        
        List<TilePiece> boardList = board.Cast<TilePiece>().ToList();

        //check if the game has won, if all destinations have been completed
        if (!boardList.Any(t => t.IsDestination && !t.IsCompleted))
        {
            
            if (MoveCount > lvl.BronzeTarget)
                GameFinished.Invoke(GameResult.Loss);
            else if (MoveCount > lvl.SilverTarget)
                GameFinished.Invoke(GameResult.Bronze);
            else if (MoveCount > lvl.GoldTarget)
                GameFinished.Invoke(GameResult.Silver);
            else
                GameFinished.Invoke(GameResult.Gold);

            return;
        }

        //get all colours currently in board
        List<Color> cols = boardList.GroupBy(p => p.PieceColour).Select(pp => pp.Key).Where(c => c != new Color(0, 0, 0, 0)).ToList();

        //check if any colour can't be completed (i.e. not enough pieces), if true then game is lost
        foreach (Color c in cols)
        {
            int movePieceCount = boardList.Count(p => p.HasMovingPiece && p.MovingPiece.PieceColour == c && !p.MovingPiece.IsKillPending);
            int destinationCount = boardList.Count(p => p.IsDestination && p.PieceColour == c);

            //check if game lost, if there are less moving pieces than destinations
            if (movePieceCount < destinationCount)
            {
                GameFinished.Invoke(GameResult.Loss);
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
        else if (finalP.CanBeJumpedOver)
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
            if (res)
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




    private TilePiece GetTile(int r, int c)
    {
        if (r < 0 || r >= NumberOfRows || c < 0 || c >= NumberOfColumns)
            return null;

        return board[r, c];
    }




    //// Use this for initialization
    //void Start () {

    //}

    //// Update is called once per frame
    //void Update () {

    //}
}
