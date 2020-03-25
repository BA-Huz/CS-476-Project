using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInitializer : MonoBehaviour
{
    public Vector3Int startLocation;
    public Vector3Int minLevelBounds;

    public void InitializePlayer()
    {
        // set up the camera UI canvas properly
        // enable game HUD elements
        transform.GetChild(0).GetChild(0).gameObject.GetComponent<HUD>().GameHUD();

        // reset health an power up
        transform.GetChild(0).gameObject.GetComponent<HealthManager>().ResetHealth();
        transform.GetChild(0).gameObject.GetComponent<HealthManager>().killBelowY = minLevelBounds.y - 10;
        transform.GetChild(0).gameObject.GetComponent<PowerUpManager>().SetPowerUp(0);

        transform.position = new Vector3(startLocation.x, startLocation.y, startLocation.z);
        transform.GetChild(0).position = new Vector3(0.5f + startLocation.x, 0.5f + startLocation.y, startLocation.z);
        // although we do not have any reason for a z other then 0, I will allow it to be
        // set diferently by startLocation as we may encorporate new features in the future
        transform.GetChild(0).gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0,0,0);

        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        transform.GetChild(0).GetComponent<CharacterMovement>().enabled = true;
        transform.GetChild(0).GetChild(0).GetComponent<CameraMovement>().enabled = true;

    }

    
}
