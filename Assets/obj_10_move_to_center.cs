using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obj_10_move_to_center : MonoBehaviour
{
    public Vector3 desired_pos, pos,scale;

    public float transition_speed=4;
    public bool move;
    private void OnEnable()
    {
        pos = transform.position;
        scale = transform.localScale;
    }
    public void reset_piece()
    {
        transform.position = pos;
        transform.localScale = scale;
    }
    public void LateUpdate()
    {

        

        if (move)
        {
            float step = transition_speed * Time.deltaTime * 2; // calculate distance to move
            transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
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
            }
        }
    }
}
