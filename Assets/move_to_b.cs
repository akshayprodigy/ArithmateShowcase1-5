using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_to_b : MonoBehaviour
{
    public Vector3  desired_pos,pos;

    public float transition_speed;
    public bool move;
    private void Start()
    {

      


    }

    private void OnEnable()
    {
        pos = transform.localPosition;
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
            float step = transition_speed * Time.deltaTime*2; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, desired_pos, step);

            // Check if the position of the two transform are approximately equal.
            if (this.name.Contains("plate"))
            {
                FindObjectOfType<Obj13Manager>().disable_pizza_click();
  //              FindObjectOfType<Obj13Manager>().activate_pizza_plate();
            }
            if (Vector3.Distance(transform.position, desired_pos) <= 0.001f)
            {
                Debug.Log("reached");
                move = false;

                if (this.name.Contains("plate"))
                {
                    FindObjectOfType<Obj13Manager>().disable_pizza_click();
                    FindObjectOfType<Obj13Manager>().activate_pizza_plate();
                }
                if (FindObjectOfType<Obj13Manager>() != null&& !this.name.Contains("plate"))
                {
                    FindObjectOfType<Obj13Manager>().enable_pizza_click();
                    if (FindObjectOfType<Obj13Manager>().slice_counter == 2)
                    {
                        FindObjectOfType<Obj13Manager>().disable_pizza_click();
                        FindObjectOfType<Obj13Manager>().reached_two_slices();
                    }
                    else if (FindObjectOfType<Obj13Manager>().slice_counter == 1 && FindObjectOfType<Obj13Manager>().plate_counter == 3)
                    {
                        FindObjectOfType<Obj13Manager>().reached_two_slices();
                    }
                }
                GetComponent<move_to_b>().enabled = false;
            }
        }
    }
}
