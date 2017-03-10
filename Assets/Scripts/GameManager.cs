using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Reflection;

public class GameManager : MonoBehaviour {


    public BoardRenderer BoardRenderer;//TODO split BoardManager to Board

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
            //BoardRenderer.Init();
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

        

    }



    private void Start()
    {
        //BoardRenderer = GetComponent<BoardRenderer>();


        //TODO restore
        //DontDestroyOnLoad(GameObject.Find("BG Music").gameObject);

        levels = LevelManager.GetAllLevels();


        if (SceneManager.GetActiveScene().name == "GameBoard")
        {
            //SceneManager.sceneLoaded += (s, e) => LoadPendingLevel();
            LoadPendingLevel();//Should be called every scene load anyway    

            BoardRenderer = GameObject.Find("Canvas").GetComponent<BoardRenderer>();
        }




        DontDestroyOnLoad(gameObject);


        Instance = this;



        //TODO remove
        StartLevel(1);
        //StartLevel(LevelManager.GetDemoLevel());
        //StartLevel(LevelManager.GenerateBlankLevel(5, 10));
    }

    public void RetryLevel()
    {
        StartLevel(BoardRenderer.CurrentBoard.lvl);
    }


    public bool HasNextLevel()
    {
        if (BoardRenderer != null && BoardRenderer.CurrentBoard.lvl != null && BoardRenderer.CurrentBoard.lvl.LevelID != 0)//TODO change structure, call is too long
        {
            return levels.Any(l => l.LevelID == BoardRenderer.CurrentBoard.lvl.LevelID + 1);
        }
        else
            return false;
        
    }

    public void StartNextLevel()
    {
        StartLevel(BoardRenderer.CurrentBoard.lvl.LevelID + 1);
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
            pendingLevel = lvl;
            SceneManager.LoadScene("GameBoard");
        }
        else
        {
            BoardRenderer.SetupBoard(lvl);
        }
        
    }



}





