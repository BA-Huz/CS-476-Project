using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleSlimeBehaviour : MonoBehaviour
{
    public GameObject player; // the player that the slime will interact with
    public float activateRange = 30f; // the range the above player must be from the slime for the slime to activate
    Rigidbody2D rb;
    public bool direction; // right is true, left is false
    bool directionDecided = false;

    public bool chillOut;
    // Start is called before the first frame update
    void Start()
    {
        chillOut = false;
        rb = GetComponent<Rigidbody2D>();
        // so the player and slimes can trigger collision events but still pass through eachother
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), player.GetComponent<CapsuleCollider2D>());
        //Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), player.GetComponent<EdgeCollider2D>());
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange())
        {
            // determine which way the slime should begin moving, it should be towards the player
            if ( ! directionDecided)
            {
                if (player.transform.position.x < transform.position.x)
                {
                    direction = false;
                }
                else
                {
                    direction = true;
                }
                directionDecided = true;
            }

            if (isNormalForceInDirection(4))
                direction = false;
            else if (isNormalForceInDirection(2))
                direction = true;

            if (direction == false) // travel left
            {
                if( ! chillOut && isNormalForceInDirection(1))
                {
                    StartCoroutine(ChillOut());
                    rb.AddForce(new Vector2(-2.5f, 5f), ForceMode2D.Impulse);
                }
            }
            else // travel right
            {
                if( ! chillOut && isNormalForceInDirection(1))
                {
                    StartCoroutine(ChillOut());
                    rb.AddForce(new Vector2(2.5f, 5f), ForceMode2D.Impulse);
                }
            }

            GetComponent<SlimeSpriteChanger>().direction = direction;
        }
    }

    // determines if fthe player is in range of the slime
    bool inRange()
    {
        float a = player.transform.position.x - transform.position.x;
        float b = player.transform.position.y - transform.position.y;
        if (Mathf.Sqrt(a*a + b*b) <= activateRange)
            return true;
        else
            return false;

    }



    public bool isNormalForceInDirection(int direction) // 1 is up, 2 is right, 3 is down, 4 is left
    {
        ContactPoint2D[] contactPoints = new ContactPoint2D[20]; // this 20 is an intentional over estimate 
        int numOfContacts = rb.GetContacts(contactPoints);

        if (direction == 1)
        {
            for(int i = 0; i < numOfContacts; i++)
            {
                if (contactPoints[i].normal.y >= 0.7f) // if on any contact points the y axis is experienceing an upwards normal force, ie it is on top of an object
                {
                    return true;
                }
            }
        }
        else if (direction == 2)
        {
            for(int i = 0; i < numOfContacts; i++)
            {
                if (contactPoints[i].normal.x >= 0.7f)
                {
                    return true;
                }
            }
        }
        else if (direction == 3)
        {
            for(int i = 0; i < numOfContacts; i++)
            {
                if (contactPoints[i].normal.y <= -0.7f)
                {
                    return true;
                }
            }
        }
        else if (direction == 4)
        {
            for(int i = 0; i < numOfContacts; i++)
            {
                if (contactPoints[i].normal.x <= -0.7f)
                {
                    return true;
                }
            }
        }
        return false;
    }

    // disables slime hopping for 2 seconds
    IEnumerator ChillOut()
    {
        chillOut = true;
        yield return new WaitForSeconds(2f);
        chillOut = false;
    }
}

