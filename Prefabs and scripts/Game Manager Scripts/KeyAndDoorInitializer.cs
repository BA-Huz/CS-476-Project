using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyAndDoorInitializer : MonoBehaviour
{
    public GameObject key; // the game object of a key
    public Vector3[] keyLocations; // array of vector3s for the locations to spawn in keys
    GameObject[] keys; // for use  when deleting old keys


    public GameObject door; // the game object of a door
    public Vector3[] doorLocations; // array of vector3s for the locations to spawn in doors
    GameObject[] doors; // for use when closing doors

    void Awake()
    {
        keys = new GameObject[0];
        InitializeKeys();

        doors = new GameObject[0];
        InitializeDoors();
    }

    public void InitializeKeys()
    {
        transform.GetChild(0).GetComponent<KeyAndDoorManager>().LooseAllKeys();

        // ensures any still existing keys are destroyed
        if (keys.Length > 0)
        {
            foreach (GameObject k in keys)
            {
                Destroy(k);
            }
        }

        // spawn in the keys at the correct locations
        Quaternion roation = new Quaternion(0, 0, 0, 1);
        keys = new GameObject[keyLocations.Length];
        int i = 0;
        foreach(Vector3 location in keyLocations)
        {
            keys[i] = Instantiate(key, location, roation);
            Physics2D.IgnoreCollision(transform.GetChild(0).GetComponent<EdgeCollider2D>(), keys[i++].GetComponent<BoxCollider2D>());
        }
    }

    public void InitializeDoors()
    {
        // ensures all doors are closed
        if (doors.Length > 0)
        {
            foreach (GameObject d in doors)
            {
                d.GetComponent<BoxCollider2D>().enabled = true;
                d.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            }
        }

        // spawn in the doors at the correct locations
        Quaternion roation = new Quaternion(0, 0, 0, 1);
        doors = new GameObject[doorLocations.Length];
        int i = 0;
        foreach(Vector3 location in doorLocations)
        {
            doors[i++] = Instantiate(door, location, roation);
        }
    }
}
