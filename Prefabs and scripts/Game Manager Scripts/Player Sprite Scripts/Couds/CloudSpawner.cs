using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public GameObject cloud;
    GameObject[] clouds; // the currently instantiated clouds
    bool[] exists; // whether or not the corosponding cloud exists in clouds[]
    int chanceOfSpawn; // the inverse is the actual chance

    void Awake()
    {
        clouds = new GameObject[10];
        exists = new bool[10];

        for (int i = 0; i < 10; i++)
            exists[i] = false;
        chanceOfSpawn = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0, 10*chanceOfSpawn) == 0) // random chance of spawning a new cloud
        {
            int i = 0;
            while( i < 10 && exists[i] == true) // find the first index in clouds[] that does not have a cloud
                i++;

            if (i != 10) // Instantiate a cloud
            {
                exists[i] = true;
                float xPos = Random.Range(0, 2);
                if (xPos == 0)
                    xPos = Random.Range(transform.parent.position.x - 30, transform.parent.position.x - 60);
                else
                    xPos = Random.Range(transform.parent.position.x + 30, transform.parent.position.x + 70);
                clouds[i] = Instantiate(cloud, new Vector3(xPos, Random.Range(-.5f, 10f), -6f), new Quaternion(0, 0, 0, 1));
                clouds[i].GetComponent<cloud>().player = transform.parent.gameObject;
                clouds[i].GetComponent<cloud>().distanceInBackground = Random.Range(0.8f, 2.2f);
                chanceOfSpawn += 4;
            }
        }

        for (int i = 0; i < 10; i++)
        {
            // if a cloud gets too far from the player destroy it to make room for new clouds
            if (exists[i] && (clouds[i].transform.position.x < transform.parent.position.x - 100f || clouds[i].transform.position.x > transform.parent.position.x + 100f))
            {
                Destroy(clouds[i]);
                exists[i] = false;
                chanceOfSpawn -= 4;
            }
        }
    }
}
