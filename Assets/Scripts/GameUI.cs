using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {

    //in game
    Button mainMenuButton;
    Button retryButton;

    Text movesText;
    Text levelNameText;


    //end of game
    Text movesTxt;
    Text resultTxt;

    Button nextLevelButton;
    Button mainMenuButtonEOG;
    Button tryAgainButton;

    GameObject lastPage;

    Button lastPageMainMenuButton;


    // Use this for initialization
    void Awake () 
    {
        //in game
        GameObject topPanel = transform.FindChild("InGameMenuBarPanel").gameObject;
        mainMenuButton = topPanel.transform.FindChild("MainMenuButton").GetComponent<Button>();
        movesText = topPanel.transform.FindChild("MovesText").GetComponent<Text>();
        retryButton = topPanel.transform.FindChild("ResetLevelButton").GetComponent<Button>();

        GameObject botPanel = transform.FindChild("InGameMenuBarPanelLower").gameObject;
        levelNameText = botPanel.transform.FindChild("LevelNameText").GetComponent<Text>();
        
        mainMenuButton.onClick.AddListener(delegate () { GameManager.Instance.LoadMainMenu(); });
        retryButton.onClick.AddListener(delegate () { GameManager.Instance.RetryLevel(); });


        //end of game
        movesTxt = transform.FindChild("ResultMoves").GetComponent<Text>();
        resultTxt = transform.FindChild("ResultCup").GetComponent<Text>();

        tryAgainButton = transform.FindChild("TryAgainButton").GetComponent<Button>();

        GameObject panel = transform.FindChild("EndOfGameMenuBarPanel").gameObject;

        nextLevelButton = panel.transform.FindChild("NextLevelButton").GetComponent<Button>();
        mainMenuButtonEOG = panel.transform.FindChild("MainMenuButton").GetComponent<Button>();

        lastPage = transform.FindChild("LastPage").gameObject;

        lastPageMainMenuButton = lastPage.transform.FindChild("MainMenuButton").GetComponent<Button>();

        nextLevelButton.onClick.AddListener(delegate () { GameManager.Instance.StartNextLevel(); });
        mainMenuButtonEOG.onClick.AddListener(delegate () { GameManager.Instance.LoadMainMenu(); });
        tryAgainButton.onClick.AddListener(delegate () { GameManager.Instance.RetryLevel(); });
        lastPageMainMenuButton.onClick.AddListener(delegate () { GameManager.Instance.LoadMainMenu(); });

    }

    public void SetMoveText(int moveCount)
    {
        movesText.text = moveCount + " Moves";
    }

    public void SetLevelNameText(string levelName)
    {
        levelNameText.text = levelName;
    }

    public void HideGameEndMenu()
    {
        gameObject.SetActive(false);
    }

    public void ShowGameEndMenu(int moveCount, Board.GameResult result)
    {
        movesTxt.text = "Finished in " + moveCount + " moves!";

        if (result == Board.GameResult.Loss)
            resultTxt.text = "You Failed";
        else
            resultTxt.text = "You earned " + result.ToString();

        gameObject.SetActive(true);

        if (result != Board.GameResult.Loss)
        {
            if (GameManager.Instance.HasNextLevel())
            {
                nextLevelButton.gameObject.SetActive(true);
            }
            else
            {
                //Hide();
                lastPage.gameObject.SetActive(true);
            }
        }
        else
        {
            nextLevelButton.gameObject.SetActive(false);
        }

    }


    //   // Update is called once per frame
    //   void Update () {

    //}
}
