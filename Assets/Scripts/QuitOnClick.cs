using UnityEngine;
using System.Collections;

public class QuitOnClick : MonoBehaviour {//TODO rename / merge into other script as now not useful

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void GoToWebpage()
    {
        Application.OpenURL("https://www.facebook.com/jicolastudios/");
    }
}
