using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelSelectButton : MonoBehaviour {

    private void Start()
    {
        if(name.StartsWith("LevelBtn (") && name.EndsWith(")"))
        {
            string n = name.Replace("LevelBtn (", "").Replace(")", "");
            int LevelNumber = int.Parse(n);

            GetComponent<Button>().onClick.AddListener(delegate () { GameManager.Instance.StartLevel(LevelNumber); });
        }
    }
}
