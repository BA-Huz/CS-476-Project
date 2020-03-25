using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloud : MonoBehaviour
{
    public GameObject player; // the player who we use for location
    public float distanceInBackground; // how far back the clouds are, effects scale and parralax
    float wind; // will be used to move clouds
    float startX, startY; // store the starting positions
    void Awake()
    {
        float num;
        if (distanceInBackground < 1f)
            num = 1f;
        else
            num = 1f/distanceInBackground;
        
        // set the scale according to the distance in background so farther clouds look smaller
        transform.localScale = new Vector3(num, num, -6f);
        // set the order in layer so that clouds can be accuratly drawn behind eachother, because i tis an int there is some inaccuracy
        GetComponent<SpriteRenderer>().sortingOrder = (int)(2.5f*(1/distanceInBackground) - 10.5f);
        wind = 0;

        startX = transform.position.x;
        startY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        float xPos;
        float yPos;

        // so parralax effects map up with the parralax effects on the background
        // distanceIn Background = 1 means same parralax as hills, 1.6 for the mountains
        xPos = player.transform.position.x * (1f - 0.5f*(1f/distanceInBackground));
        yPos = player.transform.position.y * (0.0833f*distanceInBackground + 0.8155f);

        wind += 0.01f/distanceInBackground;

        transform.SetPositionAndRotation(new Vector3(startX + xPos - wind, startY + yPos, -6), new Quaternion(0,0,0,1));
    }
}
