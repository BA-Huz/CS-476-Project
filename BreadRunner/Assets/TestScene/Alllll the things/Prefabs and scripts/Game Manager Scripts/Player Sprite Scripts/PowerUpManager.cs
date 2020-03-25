using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public AudioSource eat;
    enum PowerUp{None, Dash}
    PowerUp currentPowerUp;
    // Start is called before the first frame update
    void Start()
    {
        currentPowerUp = PowerUp.None;
    }


    public void SetPowerUp(int i)
    {
        currentPowerUp = (PowerUp) i;
    }

    // when colliding with a dash power up
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Dash Power Up" && currentPowerUp != PowerUp.Dash)
        {
            if(PlayerSettings.playSoundEffects){
                eat.Play();
            }
            Destroy(collider.gameObject);
            // set current power up and display it within the HUD
            currentPowerUp = PowerUp.Dash;
            transform.GetChild(0).gameObject.GetComponent<HUD>().DisplayPowerUp((int)currentPowerUp);

        }
    }

    public int GetPowerUp()
    {
        return (int) currentPowerUp;
    }
}
