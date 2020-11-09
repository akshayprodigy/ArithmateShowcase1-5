using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class object_to_move : MonoBehaviour
{
   
    public Vector3 desired_scale, desired_pos_center;

    public float transition_speed;
    public bool move;
    private void Start()
    {
       
        desired_pos_center = Camera.main.transform.position;
        desired_pos_center = new Vector3(desired_pos_center.x, desired_pos_center.y, 0);
        desired_scale = new Vector3(0.6f, 0.6f, 0.6f);


    }

    
   
    public void LateUpdate()
    {

        //if (move)
        //{
        //    transform.position = Vector3.Lerp(transform.position, desired_pos, Time.deltaTime * transition_speed);
        //    transform.localScale = Vector3.Lerp(transform.localScale, desired_scale, Time.deltaTime * transition_speed);
        //    if (Mathf.Abs(transform.position.x - desired_pos.x)<0.05f)
        //    {
        //        move = false;

        //    }
        //}

        if (move)
        {
            float step = transition_speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, desired_pos_center, step);
            transform.localScale = Vector3.Lerp(transform.localScale, desired_scale, Time.deltaTime * transition_speed);
            // Check if the position of the two transform are approximately equal.
            if (Vector3.Distance(transform.position, desired_pos_center) < 0.001f)
            {
                Debug.Log("reached");
                move = false;
                
                GetComponent<object_to_move>().enabled = false;
            }
        }
        }
}
