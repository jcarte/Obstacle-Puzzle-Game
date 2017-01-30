using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;
using System.Linq;

public class GameManager : MonoBehaviour {


    public BoardManager boardScript;

    public static GameManager Instance = null;

    //[HideInInspector]
    //public bool PlayerCanMove;

    //public int MoveCount { get; private set; }

    //public TilePiece[,] Board;

    //public List<MovablePiece> MovablePieces;
    //public MovablePiece SelectedPiece;

    

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
        Debug.ClearDeveloperConsole();

        //Level v1 = Level.Create(4, 5);

        //for (int r = 0; r < v1.RowCount; r++)
        //{
        //    for (int c = 0; c < v1.ColumnCount; c++)
        //    {
        //        v1.ChangeTile(r, c, Level.TileType.Landing);

        //    }
        //}

        //v1.AddMovable(1, 1, Level.ColourType.Blue);
        //v1.AddMovable(2, 2, Level.ColourType.Red);
        //v1.ChangeTile(0, 2, Level.TileType.Disappearing);
        //v1.ChangeTile(3, 3, Level.TileType.Destination, Level.ColourType.Blue);


        //v1.ChangeTile(0, 0, Level.TileType.Disappearing);
        //v1.ChangeTile(0, 1, Level.TileType.Enemy);
        //v1.ChangeTile(0, 2, Level.TileType.FakeDisappearing);
        //v1.ChangeTile(0, 3, Level.TileType.NonLanding);
        //v1.ChangeTile(0, 4, Level.TileType.Obstacle);
        //v1.ChangeTile(1, 0, Level.TileType.Redirect, -1, 0);
        //v1.ChangeTile(1, 1, Level.TileType.Landing);
        //v1.ChangeTile(1, 2, Level.TileType.Landing);
        //v1.ChangeTile(1, 3, Level.TileType.Landing);
        //v1.ChangeTile(2, 3, Level.TileType.Destination, Level.ColourType.Red);
        //v1.AddMovable(1, 1, Level.ColourType.Red);




        //Level v1 = Level.Create(6, 6);
        //v1.ChangeTile(0, 0, Level.TileType.Landing);
        //v1.ChangeTile(0, 1, Level.TileType.Landing);
        //v1.ChangeTile(0, 2, Level.TileType.Landing);
        //v1.ChangeTile(0, 3, Level.TileType.Enemy);
        //v1.ChangeTile(0, 4, Level.TileType.Landing);
        //v1.ChangeTile(0, 5, Level.TileType.Landing);

        //v1.ChangeTile(1, 0, Level.TileType.Landing);
        //v1.ChangeTile(1, 1, Level.TileType.Redirect, 0, 1);
        ////v1.ChangeTile(1, 2, Level.TileType.Redirect, 0, 1);
        //v1.ChangeTile(1, 2, Level.TileType.Empty);
        //v1.ChangeTile(1, 3, Level.TileType.Empty);
        ////v1.ChangeTile(1, 3, Level.TileType.Redirect, 0, 1);
        ////v1.ChangeTile(1, 4, Level.TileType.Redirect, 0, 1);
        //v1.ChangeTile(1, 5, Level.TileType.Disappearing);

        //v1.ChangeTile(2, 0, Level.TileType.Landing);
        //v1.ChangeTile(2, 1, Level.TileType.Landing);
        //v1.ChangeTile(2, 2, Level.TileType.Redirect, 0, 1);
        //v1.ChangeTile(2, 3, Level.TileType.Disappearing);
        //v1.ChangeTile(2, 4, Level.TileType.Landing);
        //v1.ChangeTile(2, 5, Level.TileType.Landing);

        //v1.AddMovable(1, 0, Level.ColourType.Yellow);







        //TEST LEVEL
        Level v1 = Level.Create(20, 5);
        //0
        v1.ChangeTile(0, 0, Level.TileType.Landing);
        v1.AddMovable(0, 0, Level.ColourType.Blue);
        v1.ChangeTile(0, 2, Level.TileType.Landing);
        v1.ChangeTile(0, 4, Level.TileType.Obstacle);

        //1
        v1.ChangeTile(1, 0, Level.TileType.Landing);
        v1.AddMovable(1, 0, Level.ColourType.Yellow);
        v1.ChangeTile(1, 1, Level.TileType.Landing);
        v1.AddMovable(1, 1, Level.ColourType.Red);
        v1.ChangeTile(1, 2, Level.TileType.Landing);

        //2
        v1.ChangeTile(2, 0, Level.TileType.Landing);
        v1.AddMovable(2, 0, Level.ColourType.Green);
        v1.ChangeTile(2, 1, Level.TileType.FakeDisappearing);
        v1.ChangeTile(2, 2, Level.TileType.Redirect,0,1);
        v1.ChangeTile(2, 3, Level.TileType.Redirect, 0, 1);
        v1.ChangeTile(2, 4, Level.TileType.Obstacle);

        //3
        v1.ChangeTile(3, 0, Level.TileType.Landing);
        v1.AddMovable(3, 0, Level.ColourType.Red);
        v1.ChangeTile(3, 1, Level.TileType.NonLanding);
        v1.ChangeTile(3, 2, Level.TileType.Landing);
        v1.ChangeTile(3, 3, Level.TileType.Enemy);
        v1.ChangeTile(3, 4, Level.TileType.Landing);

        //4
        v1.ChangeTile(4, 0, Level.TileType.Landing);
        v1.AddMovable(4, 0, Level.ColourType.Blue);
        v1.ChangeTile(4, 1, Level.TileType.Disappearing);

        //5
        v1.ChangeTile(5, 0, Level.TileType.Landing);
        v1.AddMovable(5, 0, Level.ColourType.Blue);
        v1.ChangeTile(5, 1, Level.TileType.Obstacle);

        //6
        v1.ChangeTile(6, 0, Level.TileType.Landing);
        v1.AddMovable(6, 0, Level.ColourType.Green);
        v1.ChangeTile(6, 1, Level.TileType.Redirect,0,1);
        v1.ChangeTile(6, 2, Level.TileType.Landing);

        //7
        v1.ChangeTile(7, 0, Level.TileType.Landing);
        v1.AddMovable(7, 0, Level.ColourType.Red);
        v1.ChangeTile(7, 1, Level.TileType.Enemy);

        //8
        v1.ChangeTile(8, 0, Level.TileType.Landing);
        v1.AddMovable(8, 0, Level.ColourType.Yellow);
        v1.ChangeTile(8, 1, Level.TileType.Redirect,0,1);
        v1.ChangeTile(8, 2, Level.TileType.Redirect, 0, 1);
        v1.ChangeTile(8, 3, Level.TileType.Disappearing);

        //9
        v1.ChangeTile(9, 0, Level.TileType.Landing);
        v1.AddMovable(9, 0, Level.ColourType.Blue);
        v1.ChangeTile(9, 1, Level.TileType.Redirect, 0, 1);
        v1.ChangeTile(9, 2, Level.TileType.Redirect, 0, 1);
        v1.ChangeTile(9, 4, Level.TileType.NonLanding);

        //10
        v1.ChangeTile(10, 0, Level.TileType.Landing);
        v1.AddMovable(10, 0, Level.ColourType.Green);
        v1.ChangeTile(10, 1, Level.TileType.Redirect, 0, 1);
        v1.ChangeTile(10, 2, Level.TileType.Redirect, 0, 1);
        v1.ChangeTile(10, 4, Level.TileType.Disappearing);

        //11
        v1.ChangeTile(11, 0, Level.TileType.Landing);
        v1.AddMovable(11, 0, Level.ColourType.Red);
        v1.ChangeTile(11, 1, Level.TileType.Redirect, 0, 1);
        v1.ChangeTile(11, 2, Level.TileType.Landing);
        v1.AddMovable(11, 2, Level.ColourType.Green);
        v1.ChangeTile(11, 3, Level.TileType.Redirect, 0, 1);
        v1.ChangeTile(11, 4, Level.TileType.Enemy);

        //12
        v1.ChangeTile(12, 0, Level.TileType.Landing);
        v1.AddMovable(12, 0, Level.ColourType.Yellow);
        v1.ChangeTile(12, 1, Level.TileType.Redirect, 0, 1);
        v1.ChangeTile(12, 2, Level.TileType.Landing);
        v1.AddMovable(12, 2, Level.ColourType.Blue);
        v1.ChangeTile(12, 3, Level.TileType.Redirect, 0, 1);
        v1.ChangeTile(12, 4, Level.TileType.FakeDisappearing);

        //13
        v1.ChangeTile(13, 0, Level.TileType.Landing);
        v1.AddMovable(13, 0, Level.ColourType.Blue);
        v1.ChangeTile(13, 2, Level.TileType.NonLanding);

        //14
        v1.ChangeTile(14, 0, Level.TileType.Landing);
        v1.AddMovable(14, 0, Level.ColourType.Green);
        v1.ChangeTile(14, 1, Level.TileType.Landing);
        v1.AddMovable(14, 1, Level.ColourType.Red);
        v1.ChangeTile(14, 2, Level.TileType.Disappearing);

        //15
        v1.ChangeTile(15, 0, Level.TileType.Landing);
        v1.AddMovable(15, 0, Level.ColourType.Yellow);
        v1.ChangeTile(15, 1, Level.TileType.Enemy);
        v1.ChangeTile(15, 2, Level.TileType.Disappearing);

        //16
        v1.ChangeTile(16, 0, Level.TileType.Landing);
        v1.AddMovable(16, 0, Level.ColourType.Blue);
        v1.ChangeTile(16, 1, Level.TileType.Enemy);
        v1.ChangeTile(16, 2, Level.TileType.Redirect,0,1);

        //17
        v1.ChangeTile(17, 0, Level.TileType.Landing);
        v1.AddMovable(17, 0, Level.ColourType.Green);

        //18
        v1.ChangeTile(18, 0, Level.TileType.Landing);
        v1.AddMovable(18, 0, Level.ColourType.Red);
        v1.ChangeTile(18, 1, Level.TileType.Redirect, 0, 1);
        v1.ChangeTile(18, 3, Level.TileType.Redirect, 0, 1);
        v1.ChangeTile(18, 4, Level.TileType.Redirect, 1, 0);
        v1.ChangeTile(19, 4, Level.TileType.Redirect, 0, -1);
        v1.ChangeTile(19, 3, Level.TileType.NonLanding);
        v1.ChangeTile(19, 2, Level.TileType.Redirect, 0, -1);
        v1.ChangeTile(19, 1, Level.TileType.Redirect, 0, -1);
        v1.ChangeTile(19, 0, Level.TileType.Redirect, -1, 0);


        //string path = "Levels\\Level1.DAT";
        //LevelManager.SaveLevel(v1, path);

        //Level lvl = LevelManager.RestoreLevel(path);
        Level lvl = v1;

        

        float width = lvl.ColumnCount;
        float height = lvl.RowCount;

        Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        cam.transform.position = new Vector3(width * 0.5f, height * -0.5f, -10f);
        cam.orthographicSize = Math.Max((width + 1) / 2, (height + 1) / 2);

        boardScript = GetComponent<BoardManager>();
        boardScript.SetupBoard(lvl);

        boardScript.GameWon += (s, e) => Debug.Log(string.Format("Game Won in {0} moves", ((BoardManager)s).MoveCount));
        boardScript.GameLost += (s, e) => Debug.Log(string.Format("Game Lost in {0} moves", ((BoardManager)s).MoveCount));


    }

    //private void InitGame()
    //{
    //    MoveCount = 0;
    //    MovablePieces = new List<MovablePiece>();

    //    Level v1 = Level.Create(4, 5);

    //    for (int r = 0; r < v1.RowCount; r++)
    //    {
    //        for (int c = 0; c < v1.ColumnCount; c++)
    //        {
    //            v1.ChangeTile(r, c, Level.TileType.Landing);
                
    //        }
    //    }

    //    v1.AddMovable(1, 1, Level.ColourType.Blue);
    //    v1.AddMovable(2, 2, Level.ColourType.Red);
    //    v1.ChangeTile(0, 2,Level.TileType.Disappearing);
    //    v1.ChangeTile(3, 3,Level.TileType.Destination,Level.ColourType.Blue);


    //    //v1.ChangeTile(0, 0, Level.TileType.Disappearing);
    //    //v1.ChangeTile(0, 1, Level.TileType.Enemy);
    //    //v1.ChangeTile(0, 2, Level.TileType.FakeDisappearing);
    //    //v1.ChangeTile(0, 3, Level.TileType.NonLanding);
    //    //v1.ChangeTile(0, 4, Level.TileType.Obstacle);
    //    //v1.ChangeTile(1, 0, Level.TileType.Redirect, 2, 2);
    //    //v1.ChangeTile(1, 1, Level.TileType.Landing);
    //    //v1.ChangeTile(1, 2, Level.TileType.Landing);
    //    //v1.ChangeTile(1, 3, Level.TileType.Landing);
    //    //v1.ChangeTile(2, 3, Level.TileType.Destination, Level.ColourType.Red);
    //    //v1.AddMovable(1, 1, Level.ColourType.Red);


    //    //string path = "Levels\\Level1.DAT";
    //    //LevelManager.SaveLevel(v1, path);

    //    //Level lvl = LevelManager.RestoreLevel(path);
    //    Level lvl = v1;

    //    Board = new TilePiece[lvl.RowCount, lvl.ColumnCount];
    //    boardScript = GetComponent<BoardManager>();
    //    boardScript.SetupBoard(lvl);

    //    float width = lvl.ColumnCount;
    //    float height = lvl.RowCount;

    //    Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    //    cam.transform.position = new Vector3(width * 0.5f, height * -0.5f, -10f);
    //    cam.orthographicSize = Math.Max((width +1)/ 2, (height +1) / 2);

    //    PlayerCanMove = true;
    //}

    //public void AddMovablePiece(MovablePiece mp)
    //{
    //    MovablePieces.Add(mp);
    //    mp.MovementCompleted += (s, e) => OnPlayerMoveComplete();
    //    mp.Clicked += (s, e) => MovablePieceSelected(s as MovablePiece);

    //    //TODO delete
    //    //SelectedPiece = mp;
    //}

    //public void RemoveMovablePiece(MovablePiece mp)
    //{
    //    mp.MovementCompleted -= (s, e) => OnPlayerMoveComplete();

    //    if (SelectedPiece == mp)
    //        SelectedPiece = null;
    //    MovablePieces.Remove(mp);
    //    mp.gameObject.SetActive(false);
    //    //OnPlayerMoveComplete();
    //}

    
    //public void MovablePieceSelected(MovablePiece mp)
    //{
    //    if(mp!=null && MovablePieces.Contains(mp))
    //    {
    //        Debug.Log("New selected Movable Piece");
    //        SelectedPiece = mp;
    //    }
            
    //}

    //public void AddTilePiece(TilePiece tp)
    //{
    //    //Debug.Log(tp.transform.position.x + ", " + tp.transform.position.y);
    //    int c = (int)Math.Round(tp.transform.position.x,0);
    //    int r = (int)Math.Round(-tp.transform.position.y, 0);
    //    Board[r, c] = tp;
    //}


    //public void ChangeSelectedPiece(MovablePiece mp)
    //{
    //    SelectedPiece = MovablePieces.FirstOrDefault(m => m == mp);
    //}


    //TODO needed?
    //public enum Direction
    //{
    //    Up, Down, Left, Right
    //}

    ////TODO package r/c together in struct?
    //private TilePiece GetTilePiece(int row, int col)
    //{
    //    //is inside board
    //    if(row >= 0 && row <= Board.GetUpperBound(0) && col >= 0 && col <= Board.GetUpperBound(1))
    //        return Board[row, col];
    //    else
    //        return null;
    //}


    //public void MoveCurrentPiece(Direction dir)
    //{
    //    if (SelectedPiece == null)//no piece selected, can't process move
    //        return;

    //    int dRow = 0;
    //    int dCol = 0;

    //    if (dir == Direction.Up)
    //        dRow = -1;
    //    else if (dir == Direction.Down)
    //        dRow = 1;
    //    else if (dir == Direction.Left)
    //        dCol = -1;
    //    else if (dir == Direction.Right)
    //        dCol = 1;

    //    TilePiece t = GetTilePiece(SelectedPiece.Row + dRow, SelectedPiece.Column + dCol);

    //    if (t == null)
    //        return;//not valid move

        

    //    if (t.IsRedirector)
    //    {
    //        Debug.Log("IsRedirect");
    //        int rowDest = t.Row - (int)Math.Round(t.RedirectDirection.y,0);
    //        int colDest = t.Column + (int)Math.Round(t.RedirectDirection.x,0);

            
    //        TilePiece destT = GetTilePiece(rowDest, colDest);
    //        Debug.Log((destT != null) + "  " + (destT.CanBeLandedOn) + "  " + (!destT.HasMovableOnIt) + "  FROM " + t.Row + " " + t.Column + "     TO " + rowDest + " " + colDest);
    //        if (destT !=null && destT.CanBeLandedOn && !destT.HasMovableOnIt)
    //        {
    //            //MoveToPiece(t);//TODO fix broken because of coroutine
    //            MoveToPiece(destT);
    //        }
    //    }
    //    else if(t.CanBeLandedOn && !t.HasMovableOnIt)
    //    {
    //        Debug.Log("Single Move");
    //        MoveToPiece(t);
    //    }
    //    else if(t.CanBeJumpedOver)//Check Jumping
    //    {
    //        Debug.Log("Attempt Jump");
    //        TilePiece j = GetTilePiece(SelectedPiece.Row + (2*dRow), SelectedPiece.Column + (2*dCol));
    //        if (j != null && j.CanBeLandedOn && !j.HasMovableOnIt)
    //            JumpToPiece(t,j);
    //    }

    //    Debug.Log("Not valid");
    //}


    //private void MoveToPiece(TilePiece p)
    //{
    //    PlayerCanMove = false;

    //    SelectedPiece.Move(p);

    //    if (p.KillsPieceOnLand)
    //    {
    //        RemoveMovablePiece(SelectedPiece);
    //        OnPlayerMoveComplete();
    //    }

    //    if (p.IsDestroyedOnMoveOn)
    //    {
    //        p.gameObject.SetActive(false);
    //        Board[p.Row, p.Column] = null;
    //        boardScript.AddTile(boardScript.EmptyPiece, p.Row, p.Column);
    //    }

    //}

    //private void JumpToPiece(TilePiece overPiece, TilePiece destPiece)
    //{
    //    PlayerCanMove = false;

    //    SelectedPiece.Move(destPiece);

    //    if (overPiece.KillsPieceOnJumpOver || destPiece.KillsPieceOnLand)
    //    {
    //        RemoveMovablePiece(SelectedPiece);
    //        OnPlayerMoveComplete();
    //    }

    //    if (overPiece.IsDestroyedOnJumpOver)
    //    {
    //        overPiece.gameObject.SetActive(false);
    //        Board[overPiece.Row, overPiece.Column] = null;
    //        boardScript.AddTile(boardScript.EmptyPiece, overPiece.Row, overPiece.Column);
    //    }

    //    if (destPiece.IsDestroyedOnMoveOn)
    //    {
    //        destPiece.gameObject.SetActive(false);
    //        Board[destPiece.Row, destPiece.Column] = null;
    //        boardScript.AddTile(boardScript.EmptyPiece, destPiece.Row, destPiece.Column);
    //    }
    //}
    

    //public void OnPlayerMoveComplete()
    //{
    //    Debug.Log("End of Turn");
        
    //    MoveCount++;

    //    int destinationCount = 0;//TODO create stats breakdown by colour
    //    int filledDestinations = 0;
    //    int movingPieceCount = MovablePieces.Count;
         
    //    for (int r = 0; r <= Board.GetUpperBound(0); r++)
    //    {
    //        for (int c = 0; c <= Board.GetUpperBound(1); c++)
    //        {
    //            var t = Board[r, c];
    //            if(t.IsDestination)
    //            {
    //                destinationCount++;
    //                MovablePiece mp = MovablePieces.FirstOrDefault(m => m.Column == c && m.Row == r);
    //                if (mp != null && mp.PieceColour == t.PieceColour)
    //                    filledDestinations++;
    //            }
    //        }
    //    }

    //    //Check if won
    //    if (destinationCount == filledDestinations)
    //    {
    //        Debug.Log("GAME WON! " + MoveCount);//TODO bring up UI
    //        return;
    //    }
        

    //    if (destinationCount > movingPieceCount)
    //    {
    //        Debug.Log("GAME LOST " + MoveCount);//TODO bring up UI, could determine this for each colour
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
 * -Load level correctly from file
 * -Add game over UI screen and stats
 * -Add main menu UI
 * -Add new images
 * -Add sounds
 * -Comment/Clean
 * -Create level files x 10
 *
 * -Add medal win conditions in level object, evaluate at game manager not board
 * -Multiple fountains leading to a disappearing log, log goes way before the frog is killed
 */

//New Structure....
/*
    
    Game Manager - will handle everything outside of the current board configuration
        - Choosing level, get level config object, setup new board, listen to game finished events
        - Gather and save game results
        - Bring up UI menus
        - Starts new level



 * 
 */
