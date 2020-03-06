using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    Rigidbody rb;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Vector3 startPosition = new Vector3();
        startPosition.Set(player.transform.position.x, player.transform.position.y + 1.5f, -10f);
        transform.position = startPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float xTranslate = 0f;
        float yTranslate = 0f;


        // was falling but just hit the gound
        if(rb.isKinematic == false && player.GetComponent<CharacterMovement>().isNormalForceInDirection(1) == true)
        {
            rb.isKinematic = true;
            //transform.position.Set(transform.position.x, player.transform.position.y, transform.position.z);
            yTranslate += player.transform.position.y - transform.position.y + 1.5f;
            //justLandedFromFar = true;
            // resetting the camera after a far landing needs to be handled the next fram
            // otherwise the position of the player is based off of the physics engine of 
            // where it would be if there was no collider to land on
        }

        // move camera right
        if ((Input.GetKey("d") || Input.GetKey("right")) && player.GetComponent<CharacterMovement>().isNormalForceInDirection(4) == false) // move camera right
        {
            if (player.transform.position.x + 3.0f >= transform.position.x)
                xTranslate += 0.2f;
            else if(player.transform.position.x - 3.0f >= transform.position.x)
                xTranslate += 0.1f;
        }
        // move camera left
        else if ((Input.GetKey("a") || Input.GetKey("left")) && player.GetComponent<CharacterMovement>().isNormalForceInDirection(2) == false) // move camera left
        {
            if (player.transform.position.x - 3.0f <= transform.position.x)
                xTranslate += -0.2f;
            else if(player.transform.position.x + 3.0f <= transform.position.x)
                xTranslate += -0.1f;
        }



        // falling
        if (player.transform.position.y <= transform.position.y - 3)
        {
            //yTranslate = player.transform.position.y + 3.0f;
            rb.isKinematic = false;
            //rb.velocity = player.getVelocity();
            rb.velocity = player.GetComponent<CharacterMovement>().getVelocity() + new Vector3(0, -1, 0);
        }

        //moving up or falling far
        if ((player.transform.position.y >= transform.position.y + 3))
        {
            if (rb.isKinematic == true)
                yTranslate += 0.1f;
            else
                rb.velocity = player.GetComponent<CharacterMovement>().getVelocity();
        }


        transform.Translate(xTranslate, yTranslate, 0f);
    }
}
