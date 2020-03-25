using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Misc : MonoBehaviour
{

    ServerController serverScript;
    public Text text;
    // place code to submit score here
    public void SubmitScore()
    {
        StartCoroutine(wrap());
        
    }

    IEnumerator wrap(){
        int score = transform.GetChild(0).GetChild(0).GetComponent<HUD>().GetScore();

        serverScript = GetComponent<ServerController>();

        yield return StartCoroutine(serverScript.PostScore(text.text, PlayerSettings.levelID, score)); 
        LoadMainMenu();
    }


    // load the menu
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("menu");
    }
}
