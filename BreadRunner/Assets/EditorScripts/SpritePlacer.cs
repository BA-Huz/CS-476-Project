using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpritePlacer : MonoBehaviour
{
    public TileBase selectedTileBase;
    public Tilemap Ground;
    public Tilemap UnderGround; 
    public Tilemap Decoration;
    public Tilemap Bridge;
    public Tilemap Hazard;
    public Tilemap Collectible;
    public Tilemap Highlight;
    public TileBase[] UnderGroundTiles;
    public TileBase [] DecorationTiles;
    public TileBase [] HazardTiles;
    public TileBase [] BridgeTiles;
    public TileBase [] CollectibleTiles;
    public TileBase StartTile;
    public TileBase EndTile;
    public Vector3Int MinLevelBounds;
    public Vector3Int MaxLevelBounds;

    public Vector3Int previousSelectedTile;

    public Vector3Int StartLocation;
    public Vector3Int EndLocation;

    public Text StartText;
    public Text EndText;
    public Text CursorText;



    void Start(){
        MinLevelBounds = new Vector3Int(0,0,0);
        MaxLevelBounds = new Vector3Int(0,0,0);
        StartLocation = new Vector3Int(0,0,0);
        EndLocation = new Vector3Int(0,0,0);
    }

    void Update() { 
       
        Vector3Int selectedTile = selectTile();
        CursorText.text = "Cursor:\n" + selectedTile.x + ", " + selectedTile.y;        

        //Don't check for input if user has mouse over UI
        if (!EventSystem.current.IsPointerOverGameObject()){ 
            if (previousSelectedTile != selectedTile){
                Highlight.SetTile(previousSelectedTile, null);
                Highlight.SetTile(selectedTile, selectedTileBase);
                previousSelectedTile = selectedTile;
            }


            //Right click to delete tiles
            if (Input.GetMouseButton(1)) { 
                Highlight.SetTile(selectedTile, null);
                UnderGround.SetTile(selectedTile, null);                
                Ground.SetTile(selectedTile, null);  
                Decoration.SetTile(selectedTile, null);
                Hazard.SetTile(selectedTile,null);
                Bridge.SetTile(selectedTile,null);
                Collectible.SetTile(selectedTile,null);
                
            } 
            //Left click to place selected tile
            else if(Input.GetMouseButton(0)) {
                //Adjust level minimum/maximum bounds
                if(selectedTile.x < MinLevelBounds.x) MinLevelBounds.x = selectedTile.x;
                else if (selectedTile.x > MaxLevelBounds.x) MaxLevelBounds.x = selectedTile.x; 
                if(selectedTile.y < MinLevelBounds.y) MinLevelBounds.y = selectedTile.y;
                else if (selectedTile.y > MaxLevelBounds.y) MaxLevelBounds.y = selectedTile.y; 

                placeTerrain(selectedTile);              
            }
            //Middle click to set a tile in the tile map as your selectedTileBase
            else if(Input.GetMouseButton(2)) {               
                Sprite newSprite = Ground.GetSprite(selectedTile);
                if (newSprite == null){
                    newSprite = Bridge.GetSprite(selectedTile);
                    if (newSprite == null){
                        newSprite = UnderGround.GetSprite(selectedTile);
                        if (newSprite == null){
                            newSprite = Hazard.GetSprite(selectedTile);
                            if (newSprite == null){
                                newSprite = Collectible.GetSprite(selectedTile);
                                if (newSprite == null){
                                    newSprite = Decoration.GetSprite(selectedTile);
                                }
                            }
                        }
                    }
                }
                if(newSprite != null){
                    SetSelectedTileBase(newSprite.name);
                    Highlight.SetTile(previousSelectedTile,selectedTileBase);
                }
            }
        } else{
            //Don't highlight anything if user is hovering menu
            Highlight.SetTile(previousSelectedTile, null);
        }
    } 

    //Get the cell the mouse is currently over
    Vector3Int selectTile(){
        Vector3 cursorPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
        Vector3Int selectedTile = Ground.WorldToCell(cursorPoint);
        return selectedTile;
    }

    //Set SelectedTileBase to specified sprite. 
    public void SetSelectedTileBase (string tileName){
        selectedTileBase = Resources.Load<TileBase>("Tiles/"+tileName);
    }

    //Places the current tile in SelectedTileBase at the location of selected Tile
    //On the proper tilemap layer

    /* This layer matrix shows which layers can't overlap
                D   C   H   B   G   U
    Decoration  -   Y   Y   Y   Y   Y
    Collectible     -   Y   Y   N   Y  
    Hazard              -   Y   N   Y
    Bridge                  -   N   Y
    Ground                      -   Y
    Underground                     -
    */
    void placeTerrain(Vector3Int selectedTile){
        //Prevent null comparisons
        if (selectedTileBase!= null){
            
            //Underground Tiles
            if (Array.Exists(UnderGroundTiles, element => element == selectedTileBase)){
                UnderGround.SetTile(selectedTile, selectedTileBase);
            } 
            //Decoration Tiles
            else if (Array.Exists(DecorationTiles, element => element == selectedTileBase)){
                Decoration.SetTile(selectedTile, selectedTileBase);
            } 
            //Bridge Tiles
            else if (Array.Exists(BridgeTiles, element => element == selectedTileBase)){
                Bridge.SetTile(selectedTile, selectedTileBase);
                Hazard.SetTile(selectedTile, null);
                Ground.SetTile(selectedTile, null);
            } 
            //Hazard Tiles
            else if (Array.Exists(HazardTiles, element => element == selectedTileBase)){
                Hazard.SetTile(selectedTile, selectedTileBase);
                Ground.SetTile(selectedTile, null);
                Bridge.SetTile(selectedTile, null);
            }
            //Collectible Tiles
            else if(Array.Exists(CollectibleTiles, element => element == selectedTileBase)){
                Collectible.SetTile(selectedTile, selectedTileBase);
                Ground.SetTile(selectedTile, null);
            } 
            //Start Tile
            else if (selectedTileBase == StartTile){
                StartLocation = selectedTile;
                StartText.text = "Start:\n" + selectedTile.x + ", " + selectedTile.y;    

            }
            else if (selectedTileBase == EndTile){
                EndLocation = selectedTile;
                EndText.text = "Finish:\n" + selectedTile.x + ", " + selectedTile.y;    
            }
            //Ground Tiles
            else {
                Ground.SetTile(selectedTile, selectedTileBase);
                Bridge.SetTile(selectedTile, null);
                Hazard.SetTile(selectedTile, null);
                Collectible.SetTile(selectedTile, null);
            }
        }
    }


}