using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndOfGameMenu : MonoBehaviour {

    Text movesTxt;
    Text resultTxt;

    Button nextLevelButton;
    Button mainMenuButton;
    Button tryAgainButton;

    // Use this for initialization
    public void Init () 
    {
        movesTxt = transform.FindChild("ResultMoves").GetComponent<Text>();
        resultTxt = transform.FindChild("ResultCup").GetComponent<Text>();

        tryAgainButton = transform.FindChild("TryAgainButton").GetComponent<Button>();

        GameObject panel = transform.FindChild("EndOfGameMenuBarPanel").gameObject;

        nextLevelButton = panel.transform.FindChild("NextLevelButton").GetComponent<Button>();
        mainMenuButton = panel.transform.FindChild("MainMenuButton").GetComponent<Button>();


        nextLevelButton.onClick.AddListener(delegate () { GameManager.Instance.StartNextLevel(); });
        mainMenuButton.onClick.AddListener(delegate () { GameManager.Instance.LoadMainMenu(); });
        tryAgainButton.onClick.AddListener(delegate () { GameManager.Instance.RetryLevel(); });

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

        nextLevelButton.gameObject.SetActive(GameManager.Instance.HasNextLevel() && result != BoardManager.GameResult.Loss);
    }

    

    // Update is called once per frame
    void Update () {
	
	}
}
