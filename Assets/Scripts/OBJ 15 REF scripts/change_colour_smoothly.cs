using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class change_colour_smoothly : MonoBehaviour
{
    public SpriteRenderer image;
    public Color current_color, desired_color;
    public float transition_speed;

    void Start()
    {
        image = GetComponent<SpriteRenderer>();
        image.color = current_color;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        image.color = Color.Lerp(image.color, desired_color, Time.deltaTime * transition_speed);
    }
}
