using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndOfGameMenu : MonoBehaviour {

    Text movesTxt;
    Text resultTxt;
    
	// Use this for initialization
	void Start () 
    {
        movesTxt = transform.Find("ResultMoves").GetComponent<Text>();
        resultTxt = transform.Find("ResultCup").GetComponent<Text>();
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
    }

    

    // Update is called once per frame
    void Update () {
	
	}
}
