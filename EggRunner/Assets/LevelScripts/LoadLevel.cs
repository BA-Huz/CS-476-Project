using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LoadLevel : MonoBehaviour
{
    //An array of tilemaps to be populated with tiles
    //Must match the import order or strange behaviour will occur
    public Tilemap [] tilemaps;
    //A string containing all level data that is going to be imported
    public string levelString;
    public GameObject gameManager;
    List<Vector3Int> greenSlimePositions = new List<Vector3Int>();
    List<Vector3Int> purpleSlimePositions = new List<Vector3Int>();
    List<Vector3Int> doorPositions = new List<Vector3Int>();
    List<Vector3Int> keyPositions = new List<Vector3Int>();
    List<Vector3Int> eggPositions = new List<Vector3Int>();
    List<Vector3Int> breadPositions = new List<Vector3Int>();
    ServerController serverScript;
 
    private string delim = "x";
    IEnumerator Start()
    {
        //Load the selected levels data
        serverScript = GetComponent<ServerController>();
        yield return StartCoroutine(serverScript.GetLevel(PlayerSettings.levelID));
        
        levelString = serverScript.levelString;

        int tilemapIndex = 0;   //Index of the current tilemap
        string[] levelArray = levelString.Split(delim[0]);   //Array of all imported level data from string.
        //The bottom left and top right tile locations. all tiles outside this range will be null
        Vector3Int MinLevelBounds = StringToVector3Int(levelArray[0]);  
        Vector3Int MaxLevelBounds = StringToVector3Int(levelArray[1]);
        
        gameManager.GetComponent<PlayerInitializer>().minLevelBounds = MinLevelBounds;

        //Get Start Location
        gameManager.GetComponent<PlayerInitializer>().startLocation = StringToVector3Int(levelArray[2]);
        //Get Coop Location
        gameManager.GetComponent<CoopInitilizer>().coopLocation = StringToVector3Int(levelArray[3]);
        //Working index in tilemap
        Vector3Int CurrentPos = MinLevelBounds;
        //The tile that is to be placed
        TileBase selectedTileBase;
        int LevelWidth = MaxLevelBounds.x - MinLevelBounds.x + 1;
        
        

        for (int i = 4; i < levelArray.Length-1; i = i + 2) {
            //Get next tile to place, and ammount that need to be placed.
            string CurrentTile = levelArray[i];
            int CurrentTileCount = int.Parse(levelArray[i+1]);
            if (CurrentTile == "n"){
                //Increment x Pos
                CurrentPos.x += CurrentTileCount % LevelWidth;
                //If x overflows bounds increment y by 1, and pull back x
                if (CurrentPos.x > MaxLevelBounds.x) {
                    CurrentPos.x -= LevelWidth;
                    CurrentPos.y++;
                }

                //Increment y Pos
                CurrentPos.y += CurrentTileCount / LevelWidth;
                //Go to next layer if y overflows
                if (CurrentPos.y > MaxLevelBounds.y) {
                    CurrentPos = MinLevelBounds;
                    tilemapIndex++;
                }
            }
            else {
                //Load the tile
                selectedTileBase = Resources.Load<TileBase>("Tiles/"+"SpriteMap_" +CurrentTile);
                for (int j = 0; j < CurrentTileCount; j++){
                    if (tilemapIndex == 1){  //Collectibles layer is special
                        if (selectedTileBase.name == "SpriteMap_40"){   //Green Slime
                            greenSlimePositions.Add(CurrentPos);
                        } 
                        else if (selectedTileBase.name == "SpriteMap_59"){  //Purple Slime
                            purpleSlimePositions.Add(CurrentPos);
                        }
                        else if (selectedTileBase.name == "SpriteMap_32"){  //key
                            keyPositions.Add(CurrentPos);
                        } 
                        else if (selectedTileBase.name == "SpriteMap_33"){  //Door
                            doorPositions.Add(CurrentPos);
                        }
                        else if (selectedTileBase.name == "SpriteMap_41"){  //bread
                            breadPositions.Add(CurrentPos);
                        }
                        else if (selectedTileBase.name == "SpriteMap_42"){  //egg
                            eggPositions.Add(CurrentPos);
                        }
                        else {  //terrain
                            tilemaps[tilemapIndex].SetTile(CurrentPos,selectedTileBase);
                        }
                    }
                    else {
                        tilemaps[tilemapIndex].SetTile(CurrentPos,selectedTileBase);
                    }
                    //Properly increment currentPos from bottom left to top right, and increase working tilemap index when grid is exhausted.
                    if (CurrentPos.x < MaxLevelBounds.x) CurrentPos.x++;
                    else {
                        CurrentPos.x = MinLevelBounds.x;
                        CurrentPos.y++;
                        if (CurrentPos.y > MaxLevelBounds.y){
                            CurrentPos.y = MinLevelBounds.y;
                            tilemapIndex++;
                        }
                    }
                }
            }
        }
        //Set Gamemanager variables
        gameManager.GetComponent<PowerUpInitializer>().dashPowerUpLocations = breadPositions;
        gameManager.GetComponent<EnemyInitializer>().greenSlimeLocations = greenSlimePositions;
        gameManager.GetComponent<EnemyInitializer>().purpleSlimeLocations = purpleSlimePositions;
        gameManager.GetComponent<EggInitializer>().eggLocations = eggPositions;
        gameManager.GetComponent<KeyAndDoorInitializer>().keyLocations = keyPositions;
        gameManager.GetComponent<KeyAndDoorInitializer>().doorLocations = doorPositions;
        //Call GameManager Initializer

        gameManager.GetComponent<PowerUpInitializer>().InitializePowerUps();
        gameManager.GetComponent<EnemyInitializer>().InitializeEnemies();
        gameManager.GetComponent<EggInitializer>().InitializeEggs();
        gameManager.GetComponent<KeyAndDoorInitializer>().InitializeKeys();
        gameManager.GetComponent<KeyAndDoorInitializer>().InitializeDoors();
        gameManager.GetComponent<PlayerInitializer>().InitializePlayer();
        gameManager.GetComponent<CoopInitilizer>().InitializeCoop();
    }


    //Simple function to convert a string representation of a Vector3Int into an actual Vector3Int
    public static Vector3Int StringToVector3Int(string stringVector){
        //Remove parentheses
        stringVector = stringVector.Substring(1, stringVector.Length-2);
         
         string[] stringArray = stringVector.Split(',');
 
         //Create Vector3Int
         Vector3Int result = new Vector3Int(
             int.Parse(stringArray[0]),
             int.Parse(stringArray[1]),
             int.Parse(stringArray[2]));
 
         return result;
    }

    
}
