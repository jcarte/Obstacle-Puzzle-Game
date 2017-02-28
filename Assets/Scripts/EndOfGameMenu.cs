using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndOfGameMenu : MonoBehaviour {

    Text movesTxt;
    Text resultTxt;

    Button nextLevelButton;
    Button mainMenuButton;
    Button tryAgainButton;

    GameObject lastPage;

    Button lastPageMainMenuButton;

    // Use this for initialization
    public void Init () 
    {
        movesTxt = transform.FindChild("ResultMoves").GetComponent<Text>();
        resultTxt = transform.FindChild("ResultCup").GetComponent<Text>();

        tryAgainButton = transform.FindChild("TryAgainButton").GetComponent<Button>();

        GameObject panel = transform.FindChild("EndOfGameMenuBarPanel").gameObject;

        nextLevelButton = panel.transform.FindChild("NextLevelButton").GetComponent<Button>();
        mainMenuButton = panel.transform.FindChild("MainMenuButton").GetComponent<Button>();

        lastPage = transform.FindChild("LastPage").gameObject;

        lastPageMainMenuButton = lastPage.transform.FindChild("MainMenuButton").GetComponent<Button>();

        nextLevelButton.onClick.AddListener(delegate () { GameManager.Instance.StartNextLevel(); });
        mainMenuButton.onClick.AddListener(delegate () { GameManager.Instance.LoadMainMenu(); });
        tryAgainButton.onClick.AddListener(delegate () { GameManager.Instance.RetryLevel(); });
        lastPageMainMenuButton.onClick.AddListener(delegate () { GameManager.Instance.LoadMainMenu(); });
    }
	

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show(int moveCount, BoardManager.GameResult result)
    {
        movesTxt.text = "Finished in " + moveCount + " moves!";

        if (result == BoardManager.GameResult.Loss)
            resultTxt.text = "You Failed";
        else
            resultTxt.text = "You earned " + result.ToString();

        gameObject.SetActive(true);

        if(result != BoardManager.GameResult.Loss)
        {
            if(GameManager.Instance.HasNextLevel())
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

    

    // Update is called once per frame
    void Update () {
	
	}
}
