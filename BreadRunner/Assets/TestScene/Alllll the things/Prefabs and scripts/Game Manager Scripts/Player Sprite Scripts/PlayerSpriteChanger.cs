using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteChanger : MonoBehaviour
{
    public Sprite stand; // the standing sprite
    public Sprite[] walk; // array of sprites for walking
    int walkSize; // the size of walk[]

    public Sprite[] fall; // array of sprites for falling and damage
    int fallSize; // the size of fall[]

    int counter;
    int spriteManager;

    public bool standOrFall = false; // stand is false, fall is true
    bool disableAnimations; // this boolean is turned true upon dying or receiving damage so that nomal sprite animations do not take place in Update()

    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = stand;
        spriteManager = 0;
        walkSize = walk.Length;
        fallSize = fall.Length;
        counter = 0;
        disableAnimations = false;
    }

    // Update is called once per frame
    void Update()
    {
        // trying to walk in both directions so do nothing
        if((Input.GetKey("d") || Input.GetKey("right")) && (Input.GetKey("a") || Input.GetKey("left")))
        {    /*   do nothing   */    }  
        else if (Input.GetKey("a") || Input.GetKey("left"))
            spriteRenderer.flipX = true;
        else if (Input.GetKey("d") || Input.GetKey("right"))
            spriteRenderer.flipX = false;

        if ( ! disableAnimations )
        {
            // dashing
            if (GetComponent<CharacterMovement>().isDashing)
            {
                spriteRenderer.color = Color.yellow;
                if(counter++ >= 3) // every 3rd frame switch sprite
                {
                    if (spriteManager >= fallSize)
                        spriteManager = 0;
                    spriteRenderer.sprite = fall[spriteManager++];
                    counter = 0;
                }
            }
            // standing or walking
            else if (standOrFall == false)
            {
                spriteRenderer.color = Color.white; // ensure normal character colour after damage, death and dash
                // moving left or moving right but not left and right at the same time
                if (((Input.GetKey("a") || Input.GetKey("left")) || (Input.GetKey("d") || Input.GetKey("right"))) && ! ((Input.GetKey("a") || Input.GetKey("left")) && (Input.GetKey("d") || Input.GetKey("right"))))
                {
                    if(counter-- == 0) // every 4th frame switch sprite
                    {
                        if (spriteManager >= walkSize)
                            spriteManager = 0;
                        spriteRenderer.sprite = walk[spriteManager++];
                        counter = 4;
                    }
                }
                else
                {
                    counter = 0;
                    spriteManager = 0;
                    spriteRenderer.sprite = stand;
                }
        }
            // falling
            else
            {
                spriteRenderer.color = Color.white; // ensure normal character colour after damage, death and dash
                if(counter++ == 4) // every 4th frame switch sprite
                {
                    if (spriteManager >= fallSize)
                        spriteManager = 0;
                    spriteRenderer.sprite = fall[spriteManager++];
                    counter = 0;
                }
           
            }
        }
    }

    public void ReceiveDamageAnimation(bool isDead)
    {
      
        // play death animation
        if (isDead)
        {
            StartCoroutine(DeathAnimation());
        }
        // play damage animation
        else
        {
            StartCoroutine(DamageAnimation());
        }
    }

    IEnumerator DamageAnimation()
    {
        disableAnimations = true;

        spriteRenderer.sprite = fall[0]; // make use of the fall sprites to demonstrate panic
        spriteRenderer.color = Color.red;// set the colour of the character to a red shade
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.sprite = fall[1];
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.sprite = fall[0];
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.sprite = fall[1];
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.sprite = fall[0];
        yield return new WaitForSeconds(0.1f);

        disableAnimations = false;
    }

    IEnumerator DeathAnimation()
    {
        disableAnimations = true;

        spriteRenderer.sprite = fall[0]; // make use of the fall sprites to demonstrate panic
        spriteRenderer.color = Color.gray;// set the colour of the character to a red shade
        //transform.Rotate(0f, 0f, 90f, Space.Self);
        //transform.GetChild(0).transform.Rotate(0f, 0f, -90f, Space.Self);
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.sprite = fall[1];
        yield return new WaitForSeconds(0.1f);
        
        // this scripts focus is on sprite changes for animation purposes
        // so ill leave it to the health manager to handle death
        GetComponent<HealthManager>().PlayerDeath();
        disableAnimations = false;
    }
}
