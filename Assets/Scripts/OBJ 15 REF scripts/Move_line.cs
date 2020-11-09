using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_line : MonoBehaviour
{
    float speed = 2;
    public bool moving;
    objectiveManager_15_REF objectiveManager_15_REF;
    bool hr, vr;
    // Start is called before the first frame update
    void Start()
    {
        hr = false;
        vr = false;

        objectiveManager_15_REF = FindObjectOfType<objectiveManager_15_REF>();
        speed = 0.08f;

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && moving)
        {
             if (hr == true)
            {
                GameObject.FindObjectOfType<objectiveManager_15_REF>().horizontal_line = GameObject.FindObjectOfType<objectiveManager_15_REF>().horizontal_line - 1;
            }
            if (vr == true)
            {
                GameObject.FindObjectOfType<objectiveManager_15_REF>().vertical_line = GameObject.FindObjectOfType<objectiveManager_15_REF>().vertical_line - 1;
            }

            hr = vr = false;
        }
            if (Input.GetMouseButton(0)&&moving)
        {

            if (Input.GetAxis("Mouse X") > 0.1f)
            {
                transform.rotation=Quaternion.Euler(new Vector3(0, 0, 90));
                transform.position=new Vector3(transform.position.x + speed,
                            1.14f, 0);
                hr = false;
                vr = true;
            }
            else if (Input.GetAxis("Mouse X") < -0.1f)
            {
                transform.rotation=Quaternion.Euler(new Vector3(0, 0, 90));
                transform.position=new Vector3(transform.position.x - speed,
                            1.14f, 0);
                hr = false;
                vr = true;
            }
            if (Input.GetAxis("Mouse Y") > 0.1f)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                transform.position=new Vector3(0,
                          transform.position.y + speed, 0);
                hr = true;
                vr = false;
            }
            else if (Input.GetAxis("Mouse Y") < -0.1f)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                transform.position=new Vector3(0,
                          transform.position.y - speed, 0);
                hr = true;
                vr = false;
            }



            if (transform.position.y > 3.7f)
            {
                transform.position = new Vector3(transform.position.x, 3.7f, 0);
            }
            else if (transform.position.y < -2.14f)
            {
                transform.position = new Vector3(transform.position.x, -2.14f, 0);
            }

            if (transform.position.x > 3f)
            {
                transform.position = new Vector3(3f, transform.position.y, 0);
            }
            else if (transform.position.x < -3f)
            {
                transform.position = new Vector3(-3f, transform.position.y, 0);
            }
        }
        if (Input.GetMouseButtonUp(0)&&moving ==true)
        {
           
            moving = false;
            if (hr == true)
            {
                GameObject.FindObjectOfType<objectiveManager_15_REF>().horizontal_line = GameObject.FindObjectOfType<objectiveManager_15_REF>().horizontal_line + 1;
            }
            if (vr == true)
            {
                GameObject.FindObjectOfType<objectiveManager_15_REF>().vertical_line = GameObject.FindObjectOfType<objectiveManager_15_REF>().vertical_line + 1;
            }

        }
    }
  
}
