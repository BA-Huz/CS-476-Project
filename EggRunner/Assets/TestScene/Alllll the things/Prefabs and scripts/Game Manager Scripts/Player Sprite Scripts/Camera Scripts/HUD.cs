using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{

    public Sprite[] PowerUps; // the order is important, should be in the order of Dash,
    int score; // the amount of points the player has
    

    public void AddPointsToScore(int points) // adds int points to the score and updates all the score displays
    {
        score += points;
        transform.GetChild(0).GetChild(0).GetChild(4).GetComponent<Text>().text = score.ToString();
        transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<Text>().text = score.ToString();
        transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Text>().text = score.ToString();
    }

    // returns the score
    public int GetScore()
    {
        return score;
    }

    // depending on the value of int hearts, it updates the health images in the game HUD to properly display health
    public void ChangeHealthDisplay(int hearts)
    {
        if (hearts >=3)
        {
            transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().enabled = true;
            transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Image>().enabled = true;
            transform.GetChild(0).GetChild(0).GetChild(2).GetComponent<Image>().enabled = true;
        }
        else if (hearts == 2)
        {
            transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().enabled = true;
            transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Image>().enabled = true;
            transform.GetChild(0).GetChild(0).GetChild(2).GetComponent<Image>().enabled = false;
        }
        else if (hearts == 1)
        {
            transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().enabled = true;
            transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Image>().enabled = false;
            transform.GetChild(0).GetChild(0).GetChild(2).GetComponent<Image>().enabled = false;
        }
        else if (hearts <= 0)
        {
            transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().enabled = false;
            transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Image>().enabled = false;
            transform.GetChild(0).GetChild(0).GetChild(2).GetComponent<Image>().enabled = false;
        }
    }


    // turns on the game HUD and turns off other HUDs/ screens
    public void GameHUD()
    {
        score = 0;
        transform.GetChild(0).GetChild(0).GetChild(4).GetComponent<Text>().text = score.ToString();
        transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<Text>().text = score.ToString();
        transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
        DisplayPowerUp(0);
    }

    // displays the game over screen and tursn off other HUDs/ screens
    public void GameOverScreen()
    {
        transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
    }

    // displays the win game screen and turns off other HUDs/ screens
    public void WinGameScreen()
    {
        transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        transform.GetChild(0).GetChild(2).gameObject.SetActive(true);       
    }

    // Displays the appropriate power up, tjogh at this state there exists only dash
    public void DisplayPowerUp(int powerUpType)
    {
        if(powerUpType == 0)
        {
            // no power up
            transform.GetChild(0).GetChild(0).GetChild(3).gameObject.GetComponent<Image>().sprite = null;
            transform.GetChild(0).GetChild(0).GetChild(3).gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);

        }
        else // attach the appropriate power up, they must be in the same order as power up managers PowerUP enum
        {
            transform.GetChild(0).GetChild(0).GetChild(3).gameObject.GetComponent<Image>().sprite = PowerUps[powerUpType - 1];
            transform.GetChild(0).GetChild(0).GetChild(3).gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        }
    }
}
