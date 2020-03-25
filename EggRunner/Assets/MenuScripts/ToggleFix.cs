using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleFix : MonoBehaviour
{
    public bool isMusic;

    //Ensure that the toggles match the actual state of the sound when loaded.
    //The inversion of the PlayerSetting undoes the toggles OnToggle function which triggers in the first line unnessecarily
    //This is a workaround as Unity has no way to change a toggles state without actually triggering it.
    void Start()
    {
        if(isMusic) {
            GetComponent<Toggle>().isOn = PlayerSettings.playMusic;
        }
        else {
            GetComponent<Toggle>().isOn = PlayerSettings.playSoundEffects;
        }
    }

   
}
