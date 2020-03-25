using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TryAgainButton : MonoBehaviour
{
    public GameObject player;
    public void OnButtonClick()
    {
        player.GetComponent<PlayerInitializer>().InitializePlayer();
    }
}
