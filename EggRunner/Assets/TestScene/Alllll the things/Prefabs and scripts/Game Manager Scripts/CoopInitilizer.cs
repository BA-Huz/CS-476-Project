using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoopInitilizer : MonoBehaviour
{
    public GameObject coop; // the game object of a coop

    public Vector3Int coopLocation; // the location to put a coop

    void Start()
    {
        //Instantiate(coop, coopLocation, new Quaternion(0f, 0f, 0f, 1f));
    }
    public void InitializeCoop(){
        Instantiate(coop, coopLocation, new Quaternion(0f, 0f, 0f, 1f));
    }
}
