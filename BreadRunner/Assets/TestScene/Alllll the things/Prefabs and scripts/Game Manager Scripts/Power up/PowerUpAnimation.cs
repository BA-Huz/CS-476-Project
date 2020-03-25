using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpAnimation : MonoBehaviour
{
    public Sprite sprite1;
    public Sprite sprite2;
    SpriteRenderer spriteRenderer;

    int counter;
    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() // every 20 frames bob the powerup up or down
    {
        if (counter == 20)
            spriteRenderer.sprite = sprite2;
        else if (counter == 40)
        {
            spriteRenderer.sprite = sprite1;
            counter = -1;
        }
        counter++;

    }
}
