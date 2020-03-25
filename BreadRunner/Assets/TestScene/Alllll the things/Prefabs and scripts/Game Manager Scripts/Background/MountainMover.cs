using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainMover : MonoBehaviour
{
    public GameObject player; // the player for use of start location and parralax effect
    public int order; // one must be 1, the other any other number, used to properly leap frog mountains

    void Awake()
    {
        if (order == 1)
            transform.SetPositionAndRotation(new Vector3(player.transform.position.x - 32f, player.transform.position.y + 1.5f, -8), new Quaternion(0,0,0,1));
        else
            transform.SetPositionAndRotation(new Vector3(player.transform.position.x, player.transform.position.y + 1.5f, -8), new Quaternion(0,0,0,1));
    }

    // Update is called once per frame
    void Update()
    {
        float xPos = 0;
        float yPos;


        if (player.transform.position.y > 100)// above where we can allow vertical parralax else top edge of skybox is in camera view
            yPos = transform.position.y - 1;
        else if (player.transform.position.y < -85)// below where we can allow vertical parralax else we see bottom edge of the hills
            yPos = player.transform.position.y + ((-85 * 0.95f -0.5f) - -85);
        else
            yPos = (player.transform.position.y * 0.95f - 0.5f); // within the range of vertical parralax


        // this if and 2 whiles leap frog the mountain images so that one or both are always witin camera view
        if (order == 1)
            xPos = player.transform.position.x * 0.7f - 32;
        else
            xPos = player.transform.position.x * 0.7f;

        while (player.transform.position.x < xPos - 32f)
        {
            xPos -= 64f;
        }

        while (player.transform.position.x > xPos + 32f)
        {
            xPos += 64f;
        }


        transform.SetPositionAndRotation(new Vector3(xPos, yPos + 1, -8), new Quaternion(0,0,0,1));
    }
}
