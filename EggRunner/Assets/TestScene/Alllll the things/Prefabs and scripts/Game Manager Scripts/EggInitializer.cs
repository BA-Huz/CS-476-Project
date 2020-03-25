using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggInitializer : MonoBehaviour
{
    public GameObject egg; // the game object of an egg
    public List<Vector3Int> eggLocations; // array of vector3s for the locations to spawn in eggs
    GameObject[] eggs; // for use  when deleting old eggs

    // Start is called before the first frame update
    void Start()
    {
        eggs = new GameObject[0];
        //InitializeEggs();
    }

    public void InitializeEggs()
    {
        // ensures any still existing eggs are destroyed
        if (eggs.Length > 0)
        {
            foreach (GameObject e in eggs)
            {
                Destroy(e);
            }
        }

        // spawn in the eggs at the correct locations
        Quaternion roation = new Quaternion(0, 0, 0, 1);
        eggs = new GameObject[eggLocations.Count];
        int i = 0;
        foreach(Vector3 location in eggLocations)
        {
            eggs[i] = Instantiate(egg, location, roation);
            Physics2D.IgnoreCollision(transform.GetChild(0).GetComponent<EdgeCollider2D>(), eggs[i++].GetComponent<BoxCollider2D>());
        }
    }
}
