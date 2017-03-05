using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Reflection;

public class GameManager : MonoBehaviour {


    private BoardManager boardScript;//TODO split BoardManager to Board

    public static GameManager Instance = null;

    private List<Level> levels;

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }


    private Level pendingLevel = null;

    private void LoadPendingLevel()
    {
        if (pendingLevel != null)
        {
            boardScript.Init();
            StartLevel(pendingLevel);
            pendingLevel = null;
        }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        boardScript = GetComponent<BoardManager>();
        
        DontDestroyOnLoad(gameObject);

        DontDestroyOnLoad(GameObject.Find("BG Music").gameObject);

        levels = LevelManager.GetAllLevels();

        SceneManager.sceneLoaded += (s, e) => LoadPendingLevel();

        //TODO remove
        //StartLevel(1);
        //StartLevel(LevelManager.GetDemoLevel());
        //StartLevel(LevelManager.GenerateBlankLevel(5, 10));
    }


    public void RetryLevel()
    {
        StartLevel(boardScript.lvl);
    }


    public bool HasNextLevel()
    {
        if (boardScript != null && boardScript.lvl != null && boardScript.lvl.LevelID != 0)
        {
            return levels.Any(l => l.LevelID == boardScript.lvl.LevelID + 1);
        }
        else
            return false;
        
    }

    public void StartNextLevel()
    {
        StartLevel(boardScript.lvl.LevelID + 1);
    }

    public void StartLevel(int levelID)
    {
        Level lvl = levels.FirstOrDefault(l => l.LevelID == levelID);
        StartLevel(lvl);
    }

    

    public void StartLevel(Level lvl)
    {
        if (lvl == null)
            throw new ArgumentException("Level can't be null");
        if (SceneManager.GetActiveScene().name != "GameBoard")
        {
            //SceneManager.sceneLoaded += (s, e) => StartLevel(lvl);
            pendingLevel = lvl;
            SceneManager.LoadScene("GameBoard");

        }
        else
        {
            //SceneManager.sceneLoaded -= (s, e) => StartLevel(lvl);
            boardScript.SetupBoard(lvl);
        }
        
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



