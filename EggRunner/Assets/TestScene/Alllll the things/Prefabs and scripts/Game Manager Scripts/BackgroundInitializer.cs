using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundInitializer : MonoBehaviour
{
    void Start()
    {
        transform.GetChild(1).GetChild(0).GetComponent<SkyMover>().player = transform.GetChild(0).gameObject;
        transform.GetChild(1).GetChild(1).GetComponent<MountainMover>().player = transform.GetChild(0).gameObject;
        transform.GetChild(1).GetChild(1).GetComponent<MountainMover>().order = 2;
        transform.GetChild(1).GetChild(2).GetComponent<MountainMover>().player = transform.GetChild(0).gameObject;
        transform.GetChild(1).GetChild(2).GetComponent<MountainMover>().order = 1;
        transform.GetChild(1).GetChild(3).GetComponent<HillMover>().player = transform.GetChild(0).gameObject;
        transform.GetChild(1).GetChild(3).GetComponent<HillMover>().order = 2;
        transform.GetChild(1).GetChild(4).GetComponent<HillMover>().player = transform.GetChild(0).gameObject;
        transform.GetChild(1).GetChild(4).GetComponent<HillMover>().order = 1;
    }
}
