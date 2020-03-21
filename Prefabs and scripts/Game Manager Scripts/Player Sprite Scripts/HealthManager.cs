using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    int health = 3;
    bool isInvinsible = false;
    public Canvas floatingPoints;

    public void ResetHealth()
    {
        health = 3;
        transform.GetChild(0).GetComponent<HUD>().ChangeHealthDisplay(health);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // kill enemy
        if (collision.gameObject.tag == "Enemy" && GetComponent<CharacterMovement>().isDashing)
        {
            Destroy(collision.gameObject);
            Canvas points = Instantiate(floatingPoints, new Vector3(transform.position.x, transform.position.y, 10), new Quaternion(0, 0, 0, 1));
            points.transform.GetChild(0).GetComponent<Text>().text = "+15";
            points.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, 0.3f, 0f);
            transform.GetChild(0).GetComponent<HUD>().AddPointsToScore(15);
            Destroy(points.GetComponent<CanvasScaler>(), 2f);
            Destroy(points.GetComponent<GraphicRaycaster>(), 2f);
            Destroy(points, 2f);
        }
        // lower player health
        else if ((collision.gameObject.tag == "Enemy" || collision.gameObject.layer == 99) && ! isInvinsible)
        {
            StartCoroutine(Invincibility());
            health -= 1;
            transform.GetChild(0).GetComponent<HUD>().ChangeHealthDisplay(health);

            if (health <= 0)
                GetComponent<PlayerSpriteChanger>().ReceiveDamageAnimation(true); // send true because dead
            else
                GetComponent<PlayerSpriteChanger>().ReceiveDamageAnimation(false); // send false because not dead yet
        }
        // raise player health
        else if (collision.gameObject.tag == "Health" && health < 3)
        {
            //collision.gameObject.Destroy();
            health += 1;
            transform.GetChild(0).GetComponent<HUD>().ChangeHealthDisplay(health);
        }
    }

    // triggers invincibility for half a second then removes it
    IEnumerator Invincibility()
    {
        isInvinsible = true;
        yield return new WaitForSeconds(0.5f);
        isInvinsible = false;
    }

    public void PlayerDeath()
    {
        // deactivate sprite and all movement scripts
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CharacterMovement>().enabled = false;
        transform.GetChild(0).GetComponent<CameraMovement>().enabled = false;

        // trigger the game over screen
        transform.GetChild(0).GetComponent<HUD>().GameOverScreen();
    }

    public void PermanentInvincibility()
    {
        isInvinsible = true;
    }
}
