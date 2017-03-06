using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Diagnostics;


//TODO embed into board canvas


/// <summary>
/// Factory class which constructs boards
/// </summary>
public class BoardRenderer : MonoBehaviour {

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


    public Board CurrentBoard;//TODO assign






    //public Action MoveCompleted;

    private GameObject boardCanvas;

    private EndOfGameMenu endUI;
    private InGameUI gameUI;

    private Camera cam;

    private GameObject canvas;



    public void Start()
    {

        //GameObject canvas = GameObject.Find("Canvas").gameObject;
        //GameObject inGame = canvas.transform.FindChild("InGame").gameObject;
        //boardCanvas = inGame.transform.FindChild("BoardPanel").gameObject;
        ////boardCanvas = GameObject.Find("Canvas").transform.FindChild("InGame").transform.FindChild("BoardPanel").gameObject;
        //endUI = GameObject.Find("Canvas").transform.FindChild("EndOfGame").GetComponent<EndOfGameMenu>();//get level complete panel object
        //cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }


    public void Init()
    {
        canvas = GameObject.Find("Canvas").gameObject;
        endUI = canvas.transform.FindChild("EndOfGame").GetComponent<EndOfGameMenu>();//get level complete panel object
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        gameUI = canvas.transform.FindChild("InGame").GetComponent<InGameUI>();

        endUI.Init();
        gameUI.Init();
    }

    //TODO make static / move to factory class
    public void SetupBoard(Level lvl)
    {

        
        GameObject inGame = canvas.transform.FindChild("InGame").gameObject;
        boardCanvas = inGame.transform.FindChild("BoardPanel").gameObject;
        //boardCanvas = GameObject.Find("Canvas").transform.FindChild("InGame").transform.FindChild("BoardPanel").gameObject;



        
        endUI.Hide();
        
        gameUI.SetLevelNameText(lvl.Name);
        gameUI.SetMoveText(0);

        //Clear board
        foreach (Transform child in boardCanvas.transform)
        {
            Destroy(child.gameObject);
        }

        


        //Camera Zooming and positioning - Nicolas Magic
        float screenWidth = 1080;
        float screenHeight = 1920 - (150 * 2);

        float spriteSize = 280;

        float boardWidth = spriteSize * lvl.ColumnCount;
        float boardHeight = spriteSize * lvl.RowCount;

        float widthRatio = screenWidth / boardWidth;
        float heightRatio = screenHeight / boardHeight;

        cam.transform.position = new Vector3((lvl.ColumnCount - 1) * 0.5f, (lvl.RowCount - 1) * -0.5f, -10f);

        if (widthRatio >= heightRatio) //shows that we need to deal with height only to determine the layout and vv 
        {
            float RowsSeen = lvl.RowCount + (500f / spriteSize);
            cam.orthographicSize = RowsSeen / 2f;
        }
        else
        {
            float RowsSeen = lvl.ColumnCount / cam.aspect;
            cam.orthographicSize = RowsSeen / 2f;
        }





        #region OldCameraZoom

        //float width = NumberOfColumns;
        //float height = NumberOfRows;

        //cam.transform.position = new Vector3((width - 1) * 0.5f, (height - 1) * -0.5f, -10f);

        //if (width >= height)//square or fat and short
        //{
        //    cam.orthographicSize = Math.Max(width, height) / (2 * cam.aspect);
        //}
        //else//tall - need to take account of UI at top at bottom (150px each, 300 total)
        //{
        //    cam.orthographicSize = Math.Max(width, height) * 0.5f * (1f + (300f / cam.pixelHeight));
        //}
        //cam.orthographicSize = Math.Max((width + 1) / 2, (height + 1) / 2) * 2;
        //cam.orthographicSize = (Math.Max(width, height)) / (2 * (width >= height ? cam.aspect : 1));

        #endregion


        /*RULES
         * RowsSeen = 2 * cam.orthographicSize
         * ColsSeen = RowsSeen * cam.aspect
         */






        Inputs = ((GameObject)Instantiate(InputListener, new Vector3(0f, 0f, 0f), Quaternion.identity,boardCanvas.transform)).GetComponent<InputListener>();
        

        for (int r = 0; r < lvl.RowCount; r++)
        {
            for (int c = 0; c < lvl.ColumnCount; c++)
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
                TilePiece tp = ((GameObject)Instantiate(go, new Vector3(c, -r, 0f), Quaternion.Euler(0,0, rotation),boardCanvas.transform)).GetComponent<TilePiece>();
                //tp.transform.parent = boardCanvas.transform;

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

                    MovablePiece mp = ((GameObject)Instantiate(mgo, new Vector3(c, -r, 0f), Quaternion.identity,boardCanvas.transform)).GetComponent<MovablePiece>();
                    //mp.transform.parent = boardCanvas.transform;
                    mp.Move(tp);
                    mp.Clicked +=(s,e) => CurrentBoard.ChangeSelectedPiece(s as MovablePiece);
                }

                CurrentBoard.AddTile(tp);

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
        if (CurrentBoard.AttemptToMoveSelected(rows, cols))
        {
            CurrentBoard.EndTurn();//TODO remove this, should be handled internally
        }
        else//move failed
        {
            Inputs.StartListening();
        }
    }



    private void OnFinished(Board.GameResult result)
    {
        endUI.Show(CurrentBoard.MoveCount, result);//TODO move not right shouldn't be handling ui inside the board object
        //if (GameFinished != null)//TODO don't think this is used
        //{
        //    GameFinished.Invoke(this, result);
        //}
    }

    private void OnTurnEnd()//TODO hookup
    {
        gameUI.SetMoveText(CurrentBoard.MoveCount);
    }


    
}
