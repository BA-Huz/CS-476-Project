using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioControl : MonoBehaviour
{
    void Start()
    {
        GetComponent<AudioSource>().mute = !PlayerSettings.playMusic;
    }

    
}
