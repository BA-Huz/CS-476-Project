using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class TileButtons : MonoBehaviour
{
    public GameObject prefabButton;
    public GameObject placerObject;
    public GameObject ContentWindow;

    public Sprite[] tiles;
   

    private Button attachedButton;
    void Start(){
        attachedButton = GetComponent<Button>();
        attachedButton.onClick.AddListener(() => makeButtons());
    }

    void makeButtons () {
        GameObject newButton;
        SpritePlacer script = placerObject.GetComponent<SpritePlacer>();

        //Remove all buttons currently in the content window
        foreach (Transform child in ContentWindow.transform){
            Destroy(child.gameObject);
        }

        //Add new buttons
        foreach (var tile in tiles){
            newButton = (GameObject)Instantiate(prefabButton, ContentWindow.transform);
            newButton.GetComponent<Image>().sprite = tile;


            UnityEngine.Events.UnityAction action1 = () => { script.SetSelectedTileBase(tile.name); };
            newButton.GetComponent<Button>().onClick.AddListener(action1);//find the button and set
        } 
    }



}