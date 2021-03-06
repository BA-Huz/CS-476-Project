﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInitializer : MonoBehaviour
{
    public GameObject greenSlime; // the game object of green slime
    public Vector3[] greenSlimeLocations; // the array of vector 3s corosponding with the positions to spawn in green slimes
    GameObject[] greenSlimes; // for use  when deleting old green slimes
    public GameObject purpleSlime; // the game object of purple slime
    public Vector3[] purpleSlimeLocations; // the array of vector 3s corosponding with the positions to spawn in purple slimes
    GameObject[] purpleSlimes; // for use  when deleting old purple slimes
    
    void Awake()
    {
        greenSlimes = new GameObject[0];
        purpleSlimes = new GameObject[0];
        InitializeEnemies();
    }

    public void InitializeEnemies()
    {
        // this if and nested loop ensures their arent any currently existing green slimes
        if (greenSlimes.Length > 0)
        {
            foreach (GameObject slime in greenSlimes)
            {
                Destroy(slime);
            }
        }

        // spawn in each green slime
        Quaternion roation = new Quaternion(0, 0, 0, 1);
        greenSlimes = new GameObject[greenSlimeLocations.Length];
        int i = 0;
        foreach(Vector3 location in greenSlimeLocations)
        {
            location.Set(location.x, location.y, -0.1f);
            greenSlimes[i] = Instantiate(greenSlime, location, roation);
            greenSlimes[i++].GetComponent<GreenSlimeBehaviour>().player = transform.GetChild(0).gameObject;
        }

        // this if and nested loop ensures their arent any currently existing purple slimes
        if (purpleSlimes.Length > 0)
        {
            foreach (GameObject slime in purpleSlimes)
            {
                Destroy(slime);
            }
        }

        // spawn in each purple slime
        purpleSlimes = new GameObject[purpleSlimeLocations.Length];
        i = 0;
        foreach(Vector3 location in purpleSlimeLocations)
        {
            location.Set(location.x, location.y, -0.1f);
            purpleSlimes[i] = Instantiate(purpleSlime, location, roation);
            purpleSlimes[i++].GetComponent<PurpleSlimeBehaviour>().player = transform.GetChild(0).gameObject;
        }
    }
}
