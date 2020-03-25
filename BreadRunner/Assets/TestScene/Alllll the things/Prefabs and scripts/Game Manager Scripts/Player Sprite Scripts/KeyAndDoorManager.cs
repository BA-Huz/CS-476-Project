using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyAndDoorManager : MonoBehaviour
{
    public int numOfKeys;
    public AudioSource keyCollect;
    public AudioSource doorUnlock;
    void Awake()
    {
        numOfKeys = 0;
        UpdateKeyDisplay();
    }

    void Update()
    {
        if (numOfKeys > 0)
        {
            // search for nearby doors
            RaycastHit2D[] results = new RaycastHit2D[100];
            ContactFilter2D cf = new ContactFilter2D();
            cf.NoFilter();
            int numOfHits = Physics2D.CircleCast(new Vector2(transform.position.x, transform.position.y), 1f, new Vector2(0, 0), cf, results, 1f);
    
            // parse through the circle cast results for a door and opens the door if found and reduce numOfKeys by 1
            bool escapeFor = false;
            for(int i = 0; i < numOfHits && ! escapeFor; i++)
            {
                if(results[i].transform.gameObject.tag == "Door")
                {
                    if(PlayerSettings.playSoundEffects){
                        doorUnlock.Play();
                    }
                    
                    numOfKeys--;
                    UpdateKeyDisplay();
                    results[i].transform.GetComponent<BoxCollider2D>().enabled = false;
                    results[i].transform.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.35f);
                    escapeFor = true;
                }
            }
            
        }
    }

    // obtain a key
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Key")
        {
            if(PlayerSettings.playSoundEffects){
                keyCollect.Play();
            }
            Destroy(collider.gameObject);
            numOfKeys++;
            UpdateKeyDisplay();
        }
    }

    // updates the key dispay appropriatly for number of keys player now has
    void UpdateKeyDisplay()
    {
        if (numOfKeys == 0)
        {
            transform.GetChild(0).GetChild(0).GetChild(0).GetChild(5).GetComponent<Image>().enabled = false;
            transform.GetChild(0).GetChild(0).GetChild(0).GetChild(6).GetComponent<Text>().text = "";
        }
        else if (numOfKeys == 1)
        {
            transform.GetChild(0).GetChild(0).GetChild(0).GetChild(5).GetComponent<Image>().enabled = true;
            transform.GetChild(0).GetChild(0).GetChild(0).GetChild(6).GetComponent<Text>().text = "";
        }
        else
        {
            transform.GetChild(0).GetChild(0).GetChild(0).GetChild(5).GetComponent<Image>().enabled = true;
            transform.GetChild(0).GetChild(0).GetChild(0).GetChild(6).GetComponent<Text>().text = "x " + numOfKeys.ToString();
        }
    }

    // loose all keys
    public void LooseAllKeys()
    {
        numOfKeys = 0;
        UpdateKeyDisplay();
    }
}
