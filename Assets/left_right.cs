using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class left_right : MonoBehaviour
{
    public Transform target;
    public float dirNum;

    private Sprite mySprite;
    public string current_location;

    

   
    private void Start()
    {
        


        target = gameObject.transform.parent;
        Vector3 heading = target.position - transform.position;
        dirNum = AngleDir(transform.forward, heading, transform.up, transform.right);
    }

   

    float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up,Vector3 right)
    {
        Vector3 perp = Vector3.Cross(fwd, targetDir);
        float dir = Vector3.Dot(perp, up);
        float dir1 = Vector3.Dot(perp, right);
        //float dir1 = Vector3.Dot(perp, transform.right);


        if (dir1 > 0f && dir > 0)
        {
            Debug.Log("top left");
            current_location = "TOP_LEFT";
            return 1f;
        }
        else if (dir1 < 0f && dir < 0f)
        {
            Debug.Log("bottom right");
            current_location = "BOTTOM_RIGHT";
            return -1f;
        }
        else if (dir1 > 0f && dir < 0)
        {
            Debug.Log("top right");
            current_location = "TOP_RIGHT";
            return 2f;
        }
        else if (dir1 < 0f && dir > 0)
        {
            Debug.Log("bottom left");
            current_location = "BOTTOM_LEFT";
            return -2f;
        }
        else
        {
            current_location = "CENTER";
            Debug.Log("center");
            return 0f;
        }
    }
}
