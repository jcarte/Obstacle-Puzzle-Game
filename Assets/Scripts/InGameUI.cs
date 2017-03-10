using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour {

    Button mainMenuButton;
    Button retryButton;

    Text movesText;
    Text levelNameText;


    public void Awake()
    {
        GameObject topPanel = transform.FindChild("InGameMenuBarPanel").gameObject;
        mainMenuButton = topPanel.transform.FindChild("MainMenuButton").GetComponent<Button>();
        movesText = topPanel.transform.FindChild("MovesText").GetComponent<Text>();
        retryButton = topPanel.transform.FindChild("ResetLevelButton").GetComponent<Button>();

        GameObject botPanel = transform.FindChild("InGameMenuBarPanelLower").gameObject;
        levelNameText = botPanel.transform.FindChild("LevelNameText").GetComponent<Text>();


        mainMenuButton.onClick.AddListener(delegate () { GameManager.Instance.LoadMainMenu(); });
        retryButton.onClick.AddListener(delegate () { GameManager.Instance.RetryLevel(); });
    }

    public void Init()
    {
        //GameObject topPanel = transform.FindChild("InGameMenuBarPanel").gameObject;
        //mainMenuButton = topPanel.transform.FindChild("MainMenuButton").GetComponent<Button>();
        //movesText = topPanel.transform.FindChild("MovesText").GetComponent<Text>();
        //retryButton = topPanel.transform.FindChild("ResetLevelButton").GetComponent<Button>();

        //GameObject botPanel = transform.FindChild("InGameMenuBarPanelLower").gameObject;
        //levelNameText = botPanel.transform.FindChild("LevelNameText").GetComponent<Text>();


        //mainMenuButton.onClick.AddListener(delegate () { GameManager.Instance.LoadMainMenu(); });
        //retryButton.onClick.AddListener(delegate () { GameManager.Instance.RetryLevel(); });
    }

    public void SetMoveText(int moveCount)
    {
        movesText.text = moveCount + " Moves";
    }

    public void SetLevelNameText(string levelName)
    {
        levelNameText.text = levelName;
    }

    //// Use this for initialization
    //void Start () {

    //}

    //// Update is called once per frame
    //void Update () {

    //}
}
