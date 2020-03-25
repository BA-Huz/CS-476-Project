using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EggManager : MonoBehaviour
{
    public Canvas floatingPoints;
    public AudioSource SoundEffect;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Egg") // trigger all the events that happen upon collecting an egg
        {
            if(PlayerSettings.playSoundEffects){
                SoundEffect.Play();
            }
            Destroy(collider.gameObject);
            //create a temporary floating point identifier
            Canvas points = Instantiate(floatingPoints, new Vector3(transform.position.x, transform.position.y, 10), new Quaternion(0, 0, 0, 1));
            points.transform.GetChild(0).GetComponent<Text>().text = "+1";
            points.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, 0.3f, 0f);
            transform.GetChild(0).GetComponent<HUD>().AddPointsToScore(1);
            Destroy(points.GetComponent<CanvasScaler>(), 2f);
            Destroy(points.GetComponent<GraphicRaycaster>(), 2f);
            Destroy(points, 2f);
        }
    }
}
