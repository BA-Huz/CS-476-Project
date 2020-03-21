using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSpriteChanger : MonoBehaviour
{
    public Sprite[] slimeSprites;
    public int spriteNum;
    public int counter;
    public int spriteManager;
    SpriteRenderer spriteRenderer;
    public bool direction;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = slimeSprites[0];
        spriteNum = slimeSprites.Length;
        counter = 0;
        spriteManager = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (direction == true)
        {
            if(counter-- <= 0) // every 6th frame switch sprite traveling right
            {
                if (spriteManager < 0 || spriteManager >= spriteNum)
                    spriteManager = spriteNum - 1;
                spriteRenderer.sprite = slimeSprites[spriteManager--];
                counter = 6;
            }
        }
        else
        {
            if(counter-- <= 0) // every 6th frame switch sprite traveling left
            {
                if (spriteManager >= spriteNum || spriteManager < 0)
                    spriteManager = 0;
                spriteRenderer.sprite = slimeSprites[spriteManager];
                spriteManager += 1;
                counter = 6;
            }
        }
    }
}
