using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class TileButtonMaker : MonoBehaviour
{
    public GameObject prefabButton;
    public GameObject placerObject;

    public Sprite[] tiles;
   
    // Use this for initialization
    void Start () {
        GameObject newButton;
        SpritePlacer script = placerObject.GetComponent<SpritePlacer>();

        foreach (var tile in tiles){
            Debug.Log(tile.name);
            newButton = (GameObject)Instantiate(prefabButton, transform);
            newButton.GetComponent<Image>().sprite = tile;


            UnityEngine.Events.UnityAction action1 = () => { script.SetSelectedTileBase(tile.name); };
            newButton.GetComponent<Button>().onClick.AddListener(action1);//find the button and set
        }       
    }   
}
