using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;
using System.Linq;

public class GameManager : MonoBehaviour {


    private BoardManager boardScript;//TODO split BoardManager to Board

    public static GameManager Instance = null;


    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        boardScript = GetComponent<BoardManager>();
        DontDestroyOnLoad(gameObject);

        StartLevel(GetDemoLevel());//TODO remove

        //InitGame();
    }


    public void RetryLevel()
    {
        StartLevel(boardScript.lvl);
    }

    public void StartLevel(string name)
    {
        throw new NotImplementedException();//TODO get file from name, convert to Level put into StartLevel

        //    //string path = "Levels\\Level1.DAT";
        //    //LevelManager.SaveLevel(v1, path);

        //    //Level lvl = LevelManager.RestoreLevel(path);
        //    Level lvl = v1;

    }

    public void StartLevel(Level lvl)
    {
        if (lvl == null)
            throw new ArgumentException("Level can't be null");
        if (SceneManager.GetActiveScene().name != "GameBoard")
            SceneManager.LoadScene("GameBoard");
        boardScript.SetupBoard(lvl);
    }


    //TODO save progress / retrieve progress here



    // Use this for initialization
    //void Start () {
    //    //SceneManager.sceneLoaded += NewSceneLoaded;

    //}

    //private void NewSceneLoaded(Scene arg0, LoadSceneMode arg1)
    //{
    //    //TODO complete
    //    InitGame();
    //}





    //private void InitGame()
    //{
    //    Debug.ClearDeveloperConsole();











    //    boardScript = GetComponent<BoardManager>();
    //    boardScript.SetupBoard(lvl);

    //    boardScript.GameFinished += BoardFinished;
    //    //boardScript.GameWon += (s, e) => Debug.Log(string.Format("Game Won in {0} moves", ((BoardManager)s).MoveCount));
    //    //boardScript.GameLost += (s, e) => Debug.Log(string.Format("Game Lost in {0} moves", ((BoardManager)s).MoveCount));




    //}



    //private void BoardFinished(BoardManager board, BoardManager.GameResult result)
    //{


    //    endUI.Show(board.MoveCount, result);


    //}



    // Update is called once per frame
    //   void Update () {

    //}

    #region StaticLevels
    private static Level GetDemoLevel()
    {
        //Demo Level
        Level v1 = Level.Create(3, 5);
        v1.BronzeTarget = 11;
        v1.SilverTarget = 9;
        v1.GoldTarget = 7;

        v1.ChangeTile(0, 0, Level.TileType.Destination, Level.ColourType.Blue);
        v1.AddMovable(0, 0, Level.ColourType.Red);
        v1.ChangeTile(0, 1, Level.TileType.Landing);
        v1.ChangeTile(0, 2, Level.TileType.Landing);
        v1.ChangeTile(0, 3, Level.TileType.Landing);
        v1.ChangeTile(1, 1, Level.TileType.Landing);
        v1.ChangeTile(1, 3, Level.TileType.Landing);
        v1.ChangeTile(0, 4, Level.TileType.Destination, Level.ColourType.Red);
        v1.AddMovable(0, 4, Level.ColourType.Blue);
        v1.ChangeTile(1, 2, Level.TileType.Enemy);


        v1.ChangeTile(2, 0, Level.TileType.Landing);
        v1.ChangeTile(2, 4, Level.TileType.Landing);

        return v1;
    }

    private static Level GetTestLevel()
    {
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
        v1.ChangeTile(2, 2, Level.TileType.Redirect, 0, 1);
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
        v1.ChangeTile(6, 1, Level.TileType.Redirect, 0, 1);
        v1.ChangeTile(6, 2, Level.TileType.Landing);

        //7
        v1.ChangeTile(7, 0, Level.TileType.Landing);
        v1.AddMovable(7, 0, Level.ColourType.Red);
        v1.ChangeTile(7, 1, Level.TileType.Enemy);

        //8
        v1.ChangeTile(8, 0, Level.TileType.Landing);
        v1.AddMovable(8, 0, Level.ColourType.Yellow);
        v1.ChangeTile(8, 1, Level.TileType.Redirect, 0, 1);
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
        v1.ChangeTile(16, 2, Level.TileType.Redirect, 0, 1);

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

        return v1;
    }
    #endregion
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
 * -Save User Progress
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



/*
 * Completed Screen
 * -Fire finished event on lose
 * -Level/board has medals 
 * 
 */



