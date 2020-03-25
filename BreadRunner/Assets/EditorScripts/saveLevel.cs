using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
public class saveLevel : MonoBehaviour
{
    public Tilemap [] tilemaps;
    public GameObject placer;
    private Button attachedButton;
    SpritePlacer placerScript;
    ServerController serverScript;
    public Text text;

    private string delim = "x";


    void Start(){
        placerScript = placer.GetComponent<SpritePlacer>();
        serverScript = GetComponent<ServerController>();
        attachedButton = GetComponent<Button>();
        attachedButton.onClick.AddListener(() => SaveTileMaps(tilemaps));
    }

    public void SaveTileMaps(Tilemap [] tilemaps){
        string saveString = placerScript.MinLevelBounds.ToString() + delim;
        saveString += placerScript.MaxLevelBounds.ToString() + delim;
        saveString += placerScript.StartLocation.ToString() + delim;
        saveString += placerScript.EndLocation.ToString() + delim;
        foreach (Tilemap x in tilemaps){
            saveString += convertTilemapToString(x);
        }
        StartCoroutine(serverScript.PostLevel(text.text, saveString));
        
    }


    public string convertTilemapToString (Tilemap tilemap){
        Vector3Int currentPos = placerScript.MinLevelBounds;
        Sprite currentSprite;
        Sprite previousSprite = tilemap.GetSprite(currentPos);
        string saveString = (previousSprite == null) ? "n" : previousSprite.name.Replace("SpriteMap_",string.Empty);
        saveString += delim;
        int previousSpriteCount = 0;

        //For each tile in used tile bounds
        for (; currentPos.y <= placerScript.MaxLevelBounds.y; currentPos.y++){
            for (currentPos.x = placerScript.MinLevelBounds.x; currentPos.x <= placerScript.MaxLevelBounds.x; currentPos.x++){
                //Get sprite at currentPosition
                currentSprite = tilemap.GetSprite(currentPos);
                //Compare if current tile is the same as the previous tile
                if (currentSprite == previousSprite){
                    previousSpriteCount++;  //Increment count of duplicates
                } 
                //On change of tile 
                else {
                    saveString +=  previousSpriteCount.ToString() + delim;
                    
                    saveString += (currentSprite == null) ? "n" : currentSprite.name.Replace("SpriteMap_",string.Empty);
                    saveString += delim;
                    previousSprite = currentSprite;
                    previousSpriteCount = 1;
                }
            }
        }
        saveString += previousSpriteCount.ToString() + delim;
        
        //Debug.Log(saveString);
        return saveString;

    }
    
}
