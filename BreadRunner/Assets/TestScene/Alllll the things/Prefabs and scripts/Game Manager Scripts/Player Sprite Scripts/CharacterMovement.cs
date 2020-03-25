using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
   // bool lastDirection; // true means left, false means right
    Rigidbody2D rbBox;
    PlayerSpriteChanger sendStandFallDetails; // stand is false, fall is true
    public bool isDashing;
    bool dashAvailible;


    void Awake()
    {
        rbBox = GetComponent<Rigidbody2D>();
        sendStandFallDetails = GetComponent<PlayerSpriteChanger>(); // 0 is stand, 1 is move left, 2 is move right, 3 is falling
        isDashing = false;
        dashAvailible = true;
    }

    // Update is called once per frame
    void Update()
    {
        bool slope = false;
        // trying to walk in both directions so do neither
        if((Input.GetKey("d") || Input.GetKey("right")) && (Input.GetKey("a") || Input.GetKey("left")))
        {    /*   do nothing   */    }     
        // walk to the right
        else if ((Input.GetKey("d") || Input.GetKey("right")) && ! isNormalForceInDirection(4, out slope) )
        {
            if (! slope)
                transform.Translate(0.1f, 0f, 0f);
            else
                transform.Translate(0.1f, 0.10001f, 0f);
            //rbBox.AddForce(new Vector2(10f, 0f), ForceMode2D.Force);
            // dash
            if ((Input.GetKey("space") && dashAvailible) && GetComponent<PowerUpManager>().GetPowerUp() == 1)
            {
                StartCoroutine(Dash(true)); // true is for right
            }
                
        }
        // walk to the left
        else if ((Input.GetKey("a") || Input.GetKey("left")) && ! isNormalForceInDirection(2, out slope))
        {
            if (! slope)
                transform.Translate(-0.1f, 0f, 0f);
            else
                transform.Translate(-0.1f, 0.10001f, 0f);
            // dash
            if ((Input.GetKey("space") && dashAvailible) && GetComponent<PowerUpManager>().GetPowerUp() == 1)
            {
                StartCoroutine(Dash(false)); // false is for left
            }
        }

        // jump
        if ((Input.GetKeyDown("w") || Input.GetKeyDown("up")) && (isNormalForceInDirection(1, out slope) || slope))// can jump if on flat ground or a slope
        {
            rbBox.AddForce(new Vector2(0, 7f), ForceMode2D.Impulse);
        }

        if (isNormalForceInDirection(1, out slope) || slope)
            sendStandFallDetails.standOrFall = false;
        else
            sendStandFallDetails.standOrFall = true;
        
    }

    public bool isNormalForceInDirection(int direction, out bool slope) // 1 is up, 2 is right, 3 is down, 4 is left
    {
        slope = false;
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
                else if(contactPoints[i].normal.y >= 0.6)
                {
                    slope = true;
                    slopeCounterActer(0f, contactPoints[i].normal.y);
                }
            }
        }
        else if (direction == 2)
        {
            for(int i = 0; i < numOfContacts; i++)
            {
                if (contactPoints[i].normal.x >= 1)
                {
                    isDashing = false; // so we dont bounce off walls with a double impulse
                    return true;
                }
                else if (contactPoints[i].normal.x >= 0.6)
                {
                    isDashing = false; // so we dont bounce off walls with a double impulse
                    slope = true;
                    slopeCounterActer(contactPoints[i].normal.x, 0f);
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
                    isDashing = false; // so we dont bounce off walls with a double impulse
                    return true;
                }
                else if (contactPoints[i].normal.x <= -0.6)
                {
                    isDashing = false; // so we dont bounce off walls with a double impulse
                    slope = true;
                    slopeCounterActer(contactPoints[i].normal.x, 0f);
                }
            }
        }
        return false;
    }

    // ensures the player does not slide down slopes or loose momentum

    void slopeCounterActer(float xNormal, float yNormal)
    {
        // set velocities to 0 to counteract what the physics engine wants to to
        rbBox.velocity = new Vector3 (0f, rbBox.velocity.y, 0f);
    }
    

   IEnumerator Dash(bool right)
   {
       isDashing = true;
       dashAvailible = false;
       if(right)
       {
            rbBox.AddForce(new Vector2(15, 1), ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.2f);
            if (isDashing)
                rbBox.AddForce(new Vector2(-15, 0), ForceMode2D.Impulse);
       }
       else
       {
            rbBox.AddForce(new Vector2(-15, 1), ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.2f);
            if (isDashing)
                rbBox.AddForce(new Vector2(15, 0), ForceMode2D.Impulse);
       }
       isDashing = false;

       yield return new WaitForSeconds(1f); // so you cant spam dash
       dashAvailible = true;
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
                    isDashing = false; // so we dont bounce off walls with a double impulse
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
                    isDashing = false; // so we dont bounce off walls with a double impulse
                    return true;
                }
            }
        }
        return false;
    }


    public Vector3 getVelocity()
    {
        return rbBox.velocity;
    }
}
