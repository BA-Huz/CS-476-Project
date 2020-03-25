using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSetting : MonoBehaviour
{
    public AudioSource mainMenuMusic;
    public void toggleSoundEffect(bool playEffects){
        PlayerSettings.playSoundEffects = playEffects;
    }

    public void toggleMusic(bool playSound){  
        PlayerSettings.playMusic = playSound;
        mainMenuMusic.mute = !PlayerSettings.playMusic;
    }
}
