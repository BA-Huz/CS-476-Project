using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Misc : MonoBehaviour
{

    // place code to submit score here
    public void SubmitScore()
    {
        int score = transform.GetChild(0).GetChild(0).GetComponent<HUD>().GetScore();
    }

    // load the menu
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("menu");
    }
}
