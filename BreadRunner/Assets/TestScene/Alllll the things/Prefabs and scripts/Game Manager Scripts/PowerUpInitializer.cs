using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpInitializer : MonoBehaviour
{
    public GameObject dashPowerUp; // the game object of a dash power up
    public List<Vector3Int> dashPowerUpLocations; // array of vector3s for the locations to spawn in dash power ups
    GameObject[] dashPowerUps; // for use  when deleting old dashs
    
    void Start()
    {
        dashPowerUps = new GameObject[0];
        //InitializePowerUps();
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
        dashPowerUps = new GameObject[dashPowerUpLocations.Count];
        int i = 0;
        foreach(Vector3 location in dashPowerUpLocations)
        {
            Vector3 spawn = new Vector3(location.x,location.y + 0.5f, location.z);
            dashPowerUps[i++] = Instantiate(dashPowerUp, spawn, roation);
        }
    }
}
