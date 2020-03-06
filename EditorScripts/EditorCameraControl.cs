using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorCameraControl : MonoBehaviour
{

    public float Speed;
    public float XMax;
    public float XMin;
    public float YMax;
    public float YMin;

    void start() {
        if(Speed == null) Speed = 2.0f;


    }


    
    void Update(){
        float xMove = Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
        float yMove = Input.GetAxis("Vertical") * Speed * Time.deltaTime;

        transform.Translate(new Vector3(xMove, yMove, 0.0f));

        if (transform.position.x > XMax){
            boundCamera(0);
        }
        else if (transform.position.x < XMin){
            boundCamera(1);
        }
        if (transform.position.y > YMax){
            boundCamera(2);
        }
        else if (transform.position.y < YMin){
            boundCamera(3);            
        }


    }

    void boundCamera(int mode){
        var pos = transform.position;
        switch(mode){
            case 0:
                pos.x = XMax;
                break;
            case 1:
                pos.x = XMin;
                break;
            case 2:
                pos.y = YMax;
                break;
            case 3:
                pos.y = YMin;
                break;
            default:
                Debug.Log("Invalid boundCamera() mode.");
                break;
        }
        transform.position = pos;
    }

}
