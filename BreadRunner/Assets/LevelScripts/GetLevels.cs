using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetLevels : MonoBehaviour
{
    public GameObject UIPrefab;
    public GameObject contentPanel;

    private int offset = 0;
    private int levelsToFetch = 10;
    private int lastFetchedAmmount = 0;
    ServerController serverScript;


    void Start(){
        //Initialize the level table at start
        serverScript = GetComponent<ServerController>();
        StartCoroutine(CreateLevelEntry());
    }

    //Fetch the next set of levels by increasing the offset
    public void nextLevels(){
        if(lastFetchedAmmount == levelsToFetch){
            offset += levelsToFetch;
            StartCoroutine(CreateLevelEntry()); 
        }
    }
    
    //Fetch the previous set of levels by decreasing the offset
    public void previousLevels(){
        offset -= levelsToFetch;
        if (offset < 0) offset = 0;
        StartCoroutine(CreateLevelEntry()); 
    }

    IEnumerator CreateLevelEntry(){
        //Remove any old level entries
        foreach (Transform child in contentPanel.transform){
            Destroy(child.gameObject);
        }
        
        //Fetch levels
        yield return StartCoroutine(serverScript.GetLevels(levelsToFetch, offset));

        string [] serverData = serverScript.levelString.Split(','); //{"1","Bob","Joe","15","2","Timmy","Bob","27"};//
        int i;
        for (i = 0; i < serverData.Length; i += 4){
            GameObject newEntry = Instantiate(UIPrefab, contentPanel.transform);
           
            Text[] text = newEntry.GetComponentsInChildren<Text>();
            text[0].text = serverData[i];
            text[1].text = serverData[i+1];
            text[2].text = serverData[i+2];
            text[3].text = serverData[i+3];
            yield return StartCoroutine(GetHighScore(int.Parse(text[0].text), text));            
        }
        lastFetchedAmmount = i/4;
    }

    IEnumerator GetHighScore(int levelID, Text[] text){
        yield return StartCoroutine(serverScript.GetScore(levelID));

        string [] serverData = serverScript.levelString.Split(',');
        if (serverData.Length == 2){
            text[2].text = serverData[0];
            text[3].text = serverData[1];
        }
    }
}
