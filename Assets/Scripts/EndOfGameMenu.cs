using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndOfGameMenu : MonoBehaviour {

    Text movesTxt;
    Text resultTxt;

    GameObject nextLevelButton;
    

	// Use this for initialization
	public void Init () 
    {
        movesTxt = transform.FindChild("ResultMoves").GetComponent<Text>();
        resultTxt = transform.FindChild("ResultCup").GetComponent<Text>();
        nextLevelButton = transform.FindChild("EndOfGameMenuBarPanel").transform.FindChild("NextLevelButton").gameObject;
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

        nextLevelButton.SetActive(GameManager.Instance.HasNextLevel() && result != BoardManager.GameResult.Loss);
    }

    

    // Update is called once per frame
    void Update () {
	
	}
}
