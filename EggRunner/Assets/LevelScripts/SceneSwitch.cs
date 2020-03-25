using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    private Button attachedButton;
    public string newScene;
    public Text text;
    void Start(){
        attachedButton = GetComponent<Button>();
        attachedButton.onClick.AddListener(() => SwitchScene(newScene));
    }


    void SwitchScene(string newScene){
        if (newScene == "Level"){
            PlayerSettings.levelID = int.Parse(text.text);
        }
        SceneManager.LoadScene(newScene);
    }
}
