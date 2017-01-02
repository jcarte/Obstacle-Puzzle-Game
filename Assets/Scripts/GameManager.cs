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




    //TODO add matrix of tiles and matrix of frogs, seperate
    byte[,] bF = new byte[5, 4];

        bF[0, 0] = 1;
        bF[0, 1] = 1;
        bF[0, 2] = 1;
        bF[0, 3] = 1;
        bF[1, 0] = 1;
        bF[1, 1] = 0;
        bF[1, 2] = 1;
        bF[1, 3] = 1;
        bF[2, 0] = 1;
        bF[2, 1] = 3;
        bF[2, 2] = 101;
        bF[2, 3] = 1;
        bF[3, 0] = 1;
        bF[3, 1] = 1;
        bF[3, 2] = 1;
        bF[3, 3] = 1;
        bF[4, 0] = 1;
        bF[4, 1] = 1;
        bF[4, 2] = 1;
        bF[4, 3] = 1;


        byte[,] mF = new byte[5, 4];

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



        MovablePieces = new List<MovablePiece>();

        Board = new TilePiece[bF.GetUpperBound(0)+1, bF.GetUpperBound(1)+1];
        boardScript = GetComponent<BoardManager>();
        boardScript.SetupBoard(bF,mF);

        PlayerCanMove = true;
    }

    public void AddMovablePiece(MovablePiece mp)
    {
        MovablePieces.Add(mp);
        //TODO delete
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


    public enum Direction
    {
        Up, Down, Left, Right
    }


    public void MoveCurrentPiece(Direction dir)
    {
        if (SelectedPiece == null)//no piece selected, can't process move
            return;
        
        PlayerCanMove = false;//stop accepting new moves while evaluate last

        int moveRows = 0;//num rows/cols to move/jump
        int moveCols = 0;

        int jumpRows = 0;
        int jumpCols = 0;

        switch (dir)//fill in move based on direction
        {
            case Direction.Up:
                moveRows = -1;
                jumpRows = -2;
                break;
            case Direction.Down:
                moveRows = 1;
                jumpRows = 2;
                break;
            case Direction.Left:
                moveCols = -1;
                jumpCols = -2;
                break;
            case Direction.Right:
                moveCols = 1;
                jumpCols = 2;
                break;
            default:
                return;
        }

        //get position of piece after moving/jumping in given direction
        int endMoveRow = SelectedPiece.Row + moveRows;
        int endMoveCol = SelectedPiece.Column + moveCols;
        int endJumpRow = SelectedPiece.Row + jumpRows;
        int endJumpCol = SelectedPiece.Column + jumpCols;

        //would moving/jumping in given direction move the piece outside of the board?
        bool canMove = endMoveRow >= 0 && endMoveRow <= Board.GetUpperBound(0) && endMoveCol >= 0 && endMoveCol <= Board.GetUpperBound(1);
        bool canJump = endJumpRow >= 0 && endJumpRow <= Board.GetUpperBound(0) && endJumpCol >= 0 && endJumpCol <= Board.GetUpperBound(1);

        //piece to be landed on after move/jump
        TilePiece movePiece = canMove ? Board[endMoveRow, endMoveCol] : null;
        TilePiece jumpPiece = canJump ? Board[endJumpRow, endJumpCol] : null;

        if (canMove && movePiece.CanBeLandedOn)//do a move
        {
            SelectedPiece.Move(moveRows, moveCols);

            if (movePiece.KillsPieceOnLand)
            {
                SelectedPiece.gameObject.SetActive(false);
                SelectedPiece = null;
            }
        }
        else if(canJump && movePiece.CanBeJumpedOver && jumpPiece.CanBeLandedOn)//do a jump
        {
            SelectedPiece.Jump(jumpRows, jumpCols);

            if (movePiece.KillsPieceOnJumpOver || jumpPiece.KillsPieceOnLand)
            {
                SelectedPiece.gameObject.SetActive(false);
                SelectedPiece = null;
            }
        }
        else//no move happened
        {
            PlayerCanMove = true;
        }

        
    }





    //TODO clean up
    //public void MoveCurrentPiece(int cols, int rows)
    //{
    //    PlayerCanMove = false;

    //    int rM = SelectedPiece.Row + rows;
    //    int cM = SelectedPiece.Column + cols;
    //    int rJ = SelectedPiece.Row + (2 * rows);
    //    int cJ = SelectedPiece.Column + (2 * cols);

    //    //Debug.Log(rM + "," + cM + " | " + rJ + "," + cJ);

    //    //Out of bounds
    //    if (rM < 0 || rM > Board.GetUpperBound(0) || cM < 0 || cM > Board.GetUpperBound(1))
    //    {
    //        PlayerCanMove = true;
    //        return;
    //    }

    //    TilePiece tp = Board[rM, cM];

    //    if (tp.CanBeLandedOn)
    //    {
    //        //Debug.Log("Moving, can be landed on");
    //        SelectedPiece.Move(rows, cols);
    //        if (tp.KillsPieceOnLand)
    //            SelectedPiece.gameObject.SetActive(false);
    //        //PlayerCanMove = true;
    //        return;
    //    }


    //    //Out of bounds
    //    if (rJ < 0 || rJ > Board.GetUpperBound(0) || cJ < 0 || cJ > Board.GetUpperBound(1)) 
    //    {
    //        PlayerCanMove = true;
    //        return;
    //    }


    //    if (tp.CanBeJumpedOver)
    //    {
    //        //Debug.Log("Jumping, can be jumped over");
    //        SelectedPiece.Jump(rows * 2, cols * 2);
    //        if (tp.KillsPieceOnJumpOver)
    //            SelectedPiece.gameObject.SetActive(false);
    //        //PlayerCanMove = true;
    //        return;
    //    }

    //    PlayerCanMove = true;
    //}


    // Update is called once per frame
    //   void Update () {

    //}
}



/* TODO
 * ==========
 * -Find better way of seeing if Coroutine is finished, check rogue
 * -Clean up GameManager.MoveCurrentPiece, currently if move into obstacle crashes (possible getting stuck on player can't move)
 * -Add moveable pieces matrix aswell as tile matrix
 * -Add win conditions
 * -Check all tile logic (figure out redirect), build test boards to check
 * -Add game stats
 * -Load from file (figure out level loading)
 * -Source Control
 */
