using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stop_fold : MonoBehaviour
{
    public float delay;
    // Start is called before the first frame update
    void OnEnable()
    {
        Invoke("off", delay);
    }

    // Update is called once per frame
    void off()
    {
        //this.GetComponent<PageFlipper>().enabled = false;
    }
}
