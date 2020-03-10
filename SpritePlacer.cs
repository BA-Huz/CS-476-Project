using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class SpritePlacer : MonoBehaviour
{
    public TileBase selectedTileBase;
    public Tilemap Ground;
    public Tilemap UnderGround; 
    public Tilemap Decoration;

    //0 == Underground, 1 == Ground, 2 == Decoration
    public int DrawingLayer;


    void Start(){
        DrawingLayer = 1;
    }
    
    void Update() { 
        //Right click to delete tiles
        if (Input.GetMouseButton(1)) {
            if (!EventSystem.current.IsPointerOverGameObject()){ 
                Vector3Int selectedTile = selectTile();
                Sprite newSprite = Ground.GetSprite(selectedTile);
                UnderGround.SetTile(selectedTile, null);                
                Ground.SetTile(selectedTile, null);  
                Decoration.SetTile(selectedTile, null);
            }
        } 
        //Left click to place selected tile
        else if(Input.GetMouseButton(0)) {
            if (!EventSystem.current.IsPointerOverGameObject()){ 
                Vector3Int selectedTile = selectTile();
                placeTerrain(selectedTile);
            }
        }
        //Middle click to set a tile in the tile map as your selectedTileBase
        else if(Input.GetMouseButton(2)) {
            if (!EventSystem.current.IsPointerOverGameObject()){
                Vector3Int selectedTile = selectTile(); 
                Sprite newSprite = Ground.GetSprite(selectedTile);
                if (newSprite == null){
                    newSprite = UnderGround.GetSprite(selectedTile);
                }
                if(newSprite != null){
                    SetSelectedTileBase(newSprite.name);
                }
            }
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
    //If an underground terrain is used, places it on the Underground Tilemap
    void placeTerrain(Vector3Int selectedTile){
        if (selectedTileBase!= null){
            if (selectedTileBase.name == "SpriteMap_4" || selectedTileBase.name == "SpriteMap_17" || selectedTileBase.name == "SpriteMap_30"){
                UnderGround.SetTile(selectedTile, selectedTileBase);
            } else {
                Ground.SetTile(selectedTile, selectedTileBase);
            }
        }
    }


}