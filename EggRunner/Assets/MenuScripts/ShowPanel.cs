using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShowPanel : MonoBehaviour
{

    private Button attachedButton;
    public GameObject panel;
    void Start(){
        attachedButton = GetComponent<Button>();
        attachedButton.onClick.AddListener(() => TogglePanelVisibility(panel));
    }
    public void TogglePanelVisibility(GameObject panel){
        var canvasGroup  = panel.GetComponent<CanvasGroup>();
         if (canvasGroup.alpha == 0){
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;

         }else{
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
         }
    }
}
