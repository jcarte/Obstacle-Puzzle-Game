using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;
using System.Linq;

public class GameManager : MonoBehaviour {


    public BoardManager boardScript;

    public static GameManager Instance = null;

    [HideInInspector]
    public bool PlayerCanMove;

    public int MoveCount { get; private set; }

    public TilePiece[,] Board;

    public List<MovablePiece> MovablePieces;
    public MovablePiece SelectedPiece;

    

    // Use this for initialization
    void Start () {
        SceneManager.sceneLoaded += NewSceneLoaded;
    }

    private void NewSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        //TODO complete
        InitGame();
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        
        InitGame();
    }

    //TODO check game won


    private void InitGame()
    {
        //TODO get board file
        //byte[,] bF = new byte[4, 3];

        //bF[0, 0] = 0;
        //bF[0, 1] = 1;
        //bF[0, 2] = 2;
        //bF[1, 0] = 3;
        //bF[1, 1] = 0;
        //bF[1, 2] = 100;
        //bF[2, 0] = 100;
        //bF[2, 1] = 200;
        //bF[2, 2] = 200;
        //bF[3, 0] = 0;
        //bF[3, 1] = 1;
        //bF[3, 2] = 2;


        MoveCount = 0;

    //TODO add matrix of tiles and matrix of frogs, seperate
        byte[,] bF = new byte[8, 4];
        byte[,] mF = new byte[8, 4];

        bF[0, 0] = 1;
        bF[0, 1] = 1;
        bF[0, 2] = 0;
        bF[0, 3] = 1;
        bF[1, 0] = 1;
        bF[1, 1] = 0;
        bF[1, 2] = 0;
        bF[1, 3] = 2;
        bF[2, 0] = 1;
        bF[2, 1] = 3;
        bF[2, 2] = 101;
        bF[2, 3] = 1;
        bF[3, 0] = 1;
        bF[3, 1] = 4;
        bF[3, 2] = 1;
        bF[3, 3] = 1;
        bF[4, 0] = 1;
        bF[4, 1] = 5;
        bF[4, 2] = 1;
        bF[4, 3] = 1;
        bF[5, 0] = 6;
        bF[5, 1] = 1;
        bF[5, 2] = 7;
        bF[5, 3] = 1;
        bF[6, 0] = 6;
        bF[6, 1] = 1;
        bF[6, 2] = 7;
        bF[6, 3] = 1;
        bF[7, 0] = 6;
        bF[7, 1] = 1;
        bF[7, 2] = 7;
        bF[7, 3] = 1;



        mF[0, 0] = 0;
        mF[0, 1] = 1;
        mF[0, 2] = 0;
        mF[0, 3] = 0;
        mF[1, 0] = 1;
        mF[1, 1] = 0;
        mF[1, 2] = 0;
        mF[1, 3] = 0;
        mF[2, 0] = 0;
        mF[2, 1] = 0;
        mF[2, 2] = 0;
        mF[2, 3] = 0;
        mF[3, 0] = 0;
        mF[3, 1] = 0;
        mF[3, 2] = 0;
        mF[3, 3] = 0;
        mF[4, 0] = 0;
        mF[4, 1] = 0;
        mF[4, 2] = 0;
        mF[4, 3] = 0;
        mF[5, 0] = 0;
        mF[5, 1] = 0;
        mF[5, 2] = 0;
        mF[5, 3] = 0;
        mF[6, 0] = 0;
        mF[6, 1] = 0;
        mF[6, 2] = 0;
        mF[6, 3] = 0;
        mF[7, 0] = 0;
        mF[7, 1] = 0;
        mF[7, 2] = 0;
        mF[7, 3] = 1;

        MovablePieces = new List<MovablePiece>();

        Board = new TilePiece[bF.GetUpperBound(0)+1, bF.GetUpperBound(1)+1];
        boardScript = GetComponent<BoardManager>();
        boardScript.SetupBoard(bF,mF);

        float width = bF.GetUpperBound(1);
        float height = bF.GetUpperBound(0);

        Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        cam.transform.position = new Vector3(width * 0.5f, height * -0.5f, -10f);
        cam.orthographicSize = Math.Max((width +1)/ 2, (height +1) / 2);

        PlayerCanMove = true;
    }

    public void AddMovablePiece(MovablePiece mp)
    {
        MovablePieces.Add(mp);
        mp.MovementCompleted += (s, e) => OnPlayerMoveComplete();
        mp.Clicked += (s, e) => MovablePieceSelected(s as MovablePiece);

        //TODO delete
        //SelectedPiece = mp;
    }

    public void RemoveMovablePiece(MovablePiece mp)
    {
        mp.MovementCompleted -= (s, e) => OnPlayerMoveComplete();

        if (SelectedPiece == mp)
            SelectedPiece = null;
        MovablePieces.Remove(mp);
    }

    
    public void MovablePieceSelected(MovablePiece mp)
    {
        if(mp!=null && MovablePieces.Contains(mp))
            SelectedPiece = mp;
    }

    public void AddTilePiece(TilePiece tp)
    {
        //Debug.Log(tp.transform.position.x + ", " + tp.transform.position.y);
        int c = (int)Math.Round(tp.transform.position.x,0);
        int r = (int)Math.Round(-tp.transform.position.y, 0);
        Board[r, c] = tp;
    }


    public void ChangeSelectedPiece(MovablePiece mp)
    {
        SelectedPiece = MovablePieces.FirstOrDefault(m => m == mp);
    }


    //TODO needed?
    public enum Direction
    {
        Up, Down, Left, Right
    }

    //TODO package r/c together in struct?
    private TilePiece GetTilePiece(int row, int col)
    {
        //is inside board
        if(row >= 0 && row <= Board.GetUpperBound(0) && col >= 0 && col <= Board.GetUpperBound(1))
            return Board[row, col];
        else
            return null;
    }


    public void MoveCurrentPiece(Direction dir)
    {
        if (SelectedPiece == null)//no piece selected, can't process move
            return;

        int dRow = 0;
        int dCol = 0;

        if (dir == Direction.Up)
            dRow = -1;
        else if (dir == Direction.Down)
            dRow = 1;
        else if (dir == Direction.Left)
            dCol = -1;
        else if (dir == Direction.Right)
            dCol = 1;

        TilePiece t = GetTilePiece(SelectedPiece.Row + dRow, SelectedPiece.Column + dCol);

        if (t == null)
            return;//not valid move

        

        if (t.IsRedirector)
        {
            Debug.Log("IsRedirect");
            int rowDest = t.Row - (int)Math.Round(t.RedirectDirection.y,0);
            int colDest = t.Column + (int)Math.Round(t.RedirectDirection.x,0);

            
            TilePiece destT = GetTilePiece(rowDest, colDest);
            Debug.Log((destT != null) + "  " + (destT.CanBeLandedOn) + "  " + (!destT.HasMovableOnIt) + "  FROM " + t.Row + " " + t.Column + "     TO " + rowDest + " " + colDest);
            if (destT !=null && destT.CanBeLandedOn && !destT.HasMovableOnIt)
            {
                //MoveToPiece(t);//TODO fix broken because of coroutine
                MoveToPiece(destT);
            }
        }
        else if(t.CanBeLandedOn && !t.HasMovableOnIt)
        {
            Debug.Log("Single Move");
            MoveToPiece(t);
        }
        else if(t.CanBeJumpedOver)//Check Jumping
        {
            Debug.Log("Attempt Jump");
            TilePiece j = GetTilePiece(SelectedPiece.Row + (2*dRow), SelectedPiece.Column + (2*dCol));
            if (j != null && j.CanBeLandedOn && !j.HasMovableOnIt)
                JumpToPiece(t,j);
        }

        Debug.Log("Not valid");
    }


    private void MoveToPiece(TilePiece p)
    {
        PlayerCanMove = false;

        SelectedPiece.Move(p);

        if (p.KillsPieceOnLand)
        {
            MovablePieces.Remove(SelectedPiece);
            SelectedPiece.gameObject.SetActive(false);
            SelectedPiece = null;
        }

        if (p.IsDestroyedOnMoveOn)
        {
            p.gameObject.SetActive(false);
            Board[p.Row, p.Column] = null;
            boardScript.AddTile(boardScript.EmptyPiece, p.Row, p.Column);
        }

    }

    private void JumpToPiece(TilePiece overPiece, TilePiece destPiece)
    {
        PlayerCanMove = false;

        SelectedPiece.Jump(destPiece);

        if (overPiece.KillsPieceOnJumpOver || destPiece.KillsPieceOnLand)
        {
            MovablePieces.Remove(SelectedPiece);
            SelectedPiece.gameObject.SetActive(false);
            SelectedPiece = null;
        }

        if (overPiece.IsDestroyedOnJumpOver)
        {
            overPiece.gameObject.SetActive(false);
            Board[overPiece.Row, overPiece.Column] = null;
            boardScript.AddTile(boardScript.EmptyPiece, overPiece.Row, overPiece.Column);
        }

        if (destPiece.IsDestroyedOnMoveOn)
        {
            destPiece.gameObject.SetActive(false);
            Board[destPiece.Row, destPiece.Column] = null;
            boardScript.AddTile(boardScript.EmptyPiece, destPiece.Row, destPiece.Column);
        }
    }

    //public void MoveCurrentPiece(Direction dir)
    //{
    //    if (SelectedPiece == null)//no piece selected, can't process move
    //        return;

    //    PlayerCanMove = false;//stop accepting new moves while evaluate last

    //    int moveRows = 0;//num rows/cols to move/jump
    //    int moveCols = 0;

    //    int jumpRows = 0;
    //    int jumpCols = 0;

    //    switch (dir)//fill in move based on direction
    //    {
    //        case Direction.Up:
    //            moveRows = -1;
    //            jumpRows = -2;
    //            break;
    //        case Direction.Down:
    //            moveRows = 1;
    //            jumpRows = 2;
    //            break;
    //        case Direction.Left:
    //            moveCols = -1;
    //            jumpCols = -2;
    //            break;
    //        case Direction.Right:
    //            moveCols = 1;
    //            jumpCols = 2;
    //            break;
    //        default:
    //            return;
    //    }

    //    //get position of piece after moving/jumping in given direction
    //    int endMoveRow = SelectedPiece.Row + moveRows;
    //    int endMoveCol = SelectedPiece.Column + moveCols;
    //    int endJumpRow = SelectedPiece.Row + jumpRows;
    //    int endJumpCol = SelectedPiece.Column + jumpCols;

    //    //would moving/jumping in given direction move the piece outside of the board?
    //    bool canMove = endMoveRow >= 0 && endMoveRow <= Board.GetUpperBound(0) && endMoveCol >= 0 && endMoveCol <= Board.GetUpperBound(1);
    //    bool canJump = endJumpRow >= 0 && endJumpRow <= Board.GetUpperBound(0) && endJumpCol >= 0 && endJumpCol <= Board.GetUpperBound(1);

    //    bool isMovingPieceOnMove = MovablePieces.Any(m => m.Column == endMoveCol && m.Row == endMoveRow);
    //    bool isMovingPieceOnJump = MovablePieces.Any(m => m.Column == endJumpCol && m.Row == endJumpRow);

    //    //piece to be landed on after move/jump
    //    TilePiece movePiece = canMove ? Board[endMoveRow, endMoveCol] : null;
    //    TilePiece jumpPiece = canJump ? Board[endJumpRow, endJumpCol] : null;

    //    if (canMove && !isMovingPieceOnMove && movePiece.CanBeLandedOn)//do a move if not out of bounds, no moving place already there
    //    {
    //        SelectedPiece.Move(moveRows, moveCols);

    //        if (movePiece.KillsPieceOnLand)
    //        {
    //            MovablePieces.Remove(SelectedPiece);
    //            SelectedPiece.gameObject.SetActive(false);
    //            SelectedPiece = null;
    //        }

    //        if(movePiece.IsDestroyedOnMoveOn)
    //        {
    //            movePiece.gameObject.SetActive(false);
    //            Board[endMoveRow, endMoveCol] = null;
    //            boardScript.AddTile(boardScript.EmptyPiece, endMoveRow, endMoveCol);
    //        }
    //    }
    //    else if(canJump && (movePiece.CanBeJumpedOver || isMovingPieceOnMove) && !isMovingPieceOnJump && jumpPiece.CanBeLandedOn)//do a jump
    //    {
    //        SelectedPiece.Jump(jumpRows, jumpCols);

    //        if (movePiece.KillsPieceOnJumpOver || jumpPiece.KillsPieceOnLand)
    //        {
    //            MovablePieces.Remove(SelectedPiece);
    //            SelectedPiece.gameObject.SetActive(false);
    //            SelectedPiece = null;
    //        }

    //        if (movePiece.IsDestroyedOnJumpOver)
    //        {
    //            movePiece.gameObject.SetActive(false);
    //            Board[endMoveRow, endMoveCol] = null;
    //            boardScript.AddTile(boardScript.EmptyPiece, endMoveRow, endMoveCol);
    //        }

    //        if (jumpPiece.IsDestroyedOnMoveOn)
    //        {
    //            jumpPiece.gameObject.SetActive(false);
    //            Board[endJumpRow, endJumpCol] = null;
    //            boardScript.AddTile(boardScript.EmptyPiece, endJumpRow, endJumpCol);
    //        }
    //    }
    //    else//no move happened
    //    {
    //        PlayerCanMove = true;
    //        return;
    //    }

    //    //OnPlayerMoveComplete();
    //}

    public void OnPlayerMoveComplete()
    {
        Debug.Log("End of Turn");
        
        MoveCount++;

        int destinationCount = 0;//TODO create stats breakdown by colour
        int filledDestinations = 0;
        int movingPieceCount = MovablePieces.Count;
         
        for (int r = 0; r <= Board.GetUpperBound(0); r++)
        {
            for (int c = 0; c <= Board.GetUpperBound(1); c++)
            {
                var t = Board[r, c];
                if(t.IsDestination)
                {
                    destinationCount++;
                    MovablePiece mp = MovablePieces.FirstOrDefault(m => m.Column == c && m.Row == r);
                    if (mp != null && mp.PieceColour == t.PieceColour)
                        filledDestinations++;
                }
            }
        }

        //Check if won
        if (destinationCount == filledDestinations)
        {
            Debug.Log("GAME WON! " + MoveCount);//TODO bring up UI
            return;
        }
        

        if (destinationCount > movingPieceCount)
        {
            Debug.Log("GAME LOST " + MoveCount);//TODO bring up UI, could determine this for each colour
            return;
        }

        PlayerCanMove = true;

    }


    // Update is called once per frame
    //   void Update () {

    //}
}



/* TODO
 * ==========
 * -Handle end of move check better, currently if kill player end of turn doesn't get evaluated (bit messy)
 * -Game manager becoming too bloated, sort of concerns between GM and boardmanager, what do they do?
 * -Build test boards to check
 * -Load from file (figure out level loading)
 */

//New Structure....
/*
 * Redirect
 *      Rotate Tile
 * 
 * Change input, each tile has properties e.g. direct direction, has moving player on it, colour of tile
 * MovingPiece inherits from TilePiece?
 */
