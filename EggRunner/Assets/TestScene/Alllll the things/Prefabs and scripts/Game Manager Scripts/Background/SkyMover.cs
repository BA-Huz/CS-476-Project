using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyMover : MonoBehaviour
{
    public GameObject player; // the player for use of start location and parralax effect

    void Awake()
    {
        transform.SetPositionAndRotation(new Vector3(player.transform.position.x, player.transform.position.y, -8), new Quaternion(0,0,0,1));
    }
    void Update()
    {
        float yPos;

        if (player.transform.position.y > 100) // above where we can allow vertical parralax else top edge of skybox is in camera view
            yPos = player.transform.position.y - 0.8f;
        else if (player.transform.position.y < -85) // below where we can allow vertical parralax else we see bottom edge of the hills
            yPos = player.transform.position.y + ((-85 * 0.99f) - -85);
        else // within the range of vertical parralax
            yPos = (player.transform.position.y * 0.99f);
        
            

        transform.SetPositionAndRotation(new Vector3(player.transform.position.x, yPos, -8), new Quaternion(0,0,0,1));
    }
}
