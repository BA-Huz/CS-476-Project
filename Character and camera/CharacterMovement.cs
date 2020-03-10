using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
   // bool lastDirection; // true means left, false means right
    Rigidbody2D rbBox;



    void Awake()
    {
        rbBox = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rbBox.AddForce(new Vector2(0, 0), ForceMode2D.Force);
        if ((Input.GetKey("d") || Input.GetKey("right")) && ! isNormalForceInDirection(4) )
        {
            transform.Translate(0.1f, 0f, 0f);
            //rbBox.AddForce(new Vector2(0.1f, 0), ForceMode2D.Impulse);
            //rbBox.velocity = new Vector2(5f, rbBox.velocity.y);
        }
        else if ((Input.GetKey("a") || Input.GetKey("left")) && ! isNormalForceInDirection(2))
        {
            transform.Translate(-0.1f, 0f, 0f);
            //rbBox.AddForce(new Vector2(-0.1f, 0), ForceMode2D.Impulse);
            //rbBox.velocity = new Vector2(-5f,  rbBox.velocity.y);
        }
        //else
      // {
            //rbBox.velocity = new Vector2(0f, rbBox.velocity.y);
        //}

        if ((Input.GetKeyDown("w") || Input.GetKeyDown("up")) && isNormalForceInDirection(1))// && rb.GetContacts.y == -1)//isOnGround == true)
        {
            rbBox.AddForce(new Vector2(0, 8.5f), ForceMode2D.Impulse);
        }
        
    }

    public bool isNormalForceInDirection(int direction) // 1 is up, 2 is right, 3 is down, 4 is left
    {
        ContactPoint2D[] contactPoints = new ContactPoint2D[20]; // this 20 is an intentional over estimate 
        int numOfContacts = rbBox.GetContacts(contactPoints);

        if (direction == 1)
        {
            for(int i = 0; i < numOfContacts; i++)
            {
                if (contactPoints[i].normal.y >= 1) // if on any contact points the y axis is experienceing an upwards normal force, ie it is on top of an object
                {
                    return true;
                }
            }
        }
        else if (direction == 2)
        {
            for(int i = 0; i < numOfContacts; i++)
            {
                if (contactPoints[i].normal.x >= 1)
                {
                    return true;
                }
            }
        }
        else if (direction == 3)
        {
            for(int i = 0; i < numOfContacts; i++)
            {
                if (contactPoints[i].normal.y <= -1)
                {
                    return true;
                }
            }
        }
        else if (direction == 4)
        {
            for(int i = 0; i < numOfContacts; i++)
            {
                if (contactPoints[i].normal.x <= -1)
                {
                    return true;
                }
            }
        }
        return false;
    }

    void OnCollisionStay2D(Collision2D collision) // detects collisons on side as being on ground
    {
        //isOnGround = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        //isOnGround = false;
    }

    public Vector3 getVelocity()
    {
        return rbBox.velocity;
    }
}
