using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpInitializer : MonoBehaviour
{
    public GameObject dashPowerUp; // the game object of a dash power up
    public Vector3[] dashPowerUpLocations; // array of vector3s for the locations to spawn in dash power ups
    GameObject[] dashPowerUps; // for use  when deleting old dashs
    
    void Awake()
    {
        dashPowerUps = new GameObject[0];
        InitializePowerUps();
    }

    public void InitializePowerUps()
    {
        // ensures any still existing power ups are destroyed
        if (dashPowerUps.Length > 0)
        {
            foreach (GameObject dash in dashPowerUps)
            {
                Destroy(dash);
            }
        }

        // spawn in the dash power ups at the correct locations
        Quaternion roation = new Quaternion(0, 0, 0, 1);
        dashPowerUps = new GameObject[dashPowerUpLocations.Length];
        int i = 0;
        foreach(Vector3 location in dashPowerUpLocations)
        {
            dashPowerUps[i++] = Instantiate(dashPowerUp, location, roation);
        }
    }
}
