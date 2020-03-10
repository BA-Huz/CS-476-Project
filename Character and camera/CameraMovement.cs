using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Rigidbody rb;
    Rigidbody2D parentRB;

    public float jumpYCoordinate;

    public bool falling = false;
    public bool jumping = false;
    public bool correctCamera = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        parentRB = transform.parent.GetComponent<Rigidbody2D>();
        jumpYCoordinate = transform.parent.position.y;
        transform.position.Set(transform.parent.position.x, transform.parent.position.y + 2f, -10);

    }

    // Update is called once per frame
    void Update()
    {
        float xTranslate = 0;
        float yTranslate = 0;
 
        // move right
        if ((Input.GetKey("d") || Input.GetKey("right")) && (! transform.parent.GetComponent<CharacterMovement>().isNormalForceInDirection(4) && transform.parent.position.x > transform.position.x - 3f))
            xTranslate += 0.1f;
        // move left
        else if ((Input.GetKey("a") || Input.GetKey("left")) && (! transform.parent.GetComponent<CharacterMovement>().isNormalForceInDirection(2) && transform.parent.position.x < transform.position.x + 3f))
            xTranslate += -0.1f;

        // jump
        if ((Input.GetKeyDown("w") || Input.GetKey("up")) && (transform.parent.GetComponent<CharacterMovement>().isNormalForceInDirection(1) && ! jumping))
        {
            jumpYCoordinate = transform.parent.position.y;
            jumping = true;
        }
        // reached jump peak
        else if (transform.parent.GetComponent<Rigidbody2D>().velocity.y <= 0f && jumping)
        {
            jumping = false;
            falling = true;
        }
        // fall off platform
        else if (! transform.parent.GetComponent<CharacterMovement>().isNormalForceInDirection(1) && (! jumping && ! falling))
        {
            jumpYCoordinate = transform.parent.position.y;
            falling = true;
            jumping = false;
        }
        // falling far
        else if ( !transform.parent.GetComponent<CharacterMovement>().isNormalForceInDirection(1) && transform.parent.position.y < jumpYCoordinate - 1f)
        {
            jumping = false;
            falling = true;
            if (transform.position.y > transform.parent.position.y - 3)
                yTranslate -= 0.05f;
        }
        // landed after fall far and needs to correct camera
        else if ((transform.parent.GetComponent<CharacterMovement>().isNormalForceInDirection(1) && transform.position.y < transform.parent.position.y + 2f) || correctCamera )
        {
            correctCamera = true;
            if(transform.position.y > transform.parent.position.y + 1.7f) // move up to correct location
            {
                yTranslate += transform.parent.position.y + 2f - transform.position.y;
                correctCamera = false;
            }
            else // move up 0.3 closer to correct location
                yTranslate += 0.3f;
        }

        if (transform.parent.GetComponent<CharacterMovement>().isNormalForceInDirection(1))
            falling = false;


        transform.Translate(xTranslate, yTranslate, 0f);

    }
}
