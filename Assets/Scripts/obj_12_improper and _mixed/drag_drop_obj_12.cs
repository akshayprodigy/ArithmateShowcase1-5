using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drag_drop_obj_12 : MonoBehaviour
{
    private bool isDragging, reached;
    private bool isDraggingDone;
    public Vector3 pos;
    public obj_12_improper_and_mixed obj_12_improper_and_mixed;
    private void Start()
    {
        obj_12_improper_and_mixed = FindObjectOfType<obj_12_improper_and_mixed>();
    }
    private void OnEnable()
    {
        pos = this.transform.position;

    }
    public void OnMouseDown()
    {
        isDragging = true;

        if (this.enabled)

        {
            GetComponent<SpriteRenderer>().sortingOrder = 6;

            if (obj_12_improper_and_mixed.chefConversationPanel.activeSelf)
            {
                obj_12_improper_and_mixed.disable_panel(obj_12_improper_and_mixed.chefConversationPanel, 0.5f);
            }
        }
    }

    public void OnMouseUp()
    {
        isDragging = false;
        if (this.enabled && !reached)
        {
            GetComponent<SpriteRenderer>().sortingOrder = 3;
            this.transform.position = pos;
        }
    }
    void Update()
    {
        if (isDragging)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.Translate(mousePosition);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.name == "PizzaBox" && this.name.Equals("full veg"))
        { if (FindObjectOfType<obj_12_improper_and_mixed>().first_dragged_id == "" || FindObjectOfType<obj_12_improper_and_mixed>().first_dragged_id != col.gameObject.GetInstanceID().ToString())
            {
                GetComponent<SpriteRenderer>().sortingOrder = 3;
                reached = true;
                isDragging = false;
                this.transform.position = col.gameObject.transform.GetChild(0).transform.position;
                col.gameObject.GetComponent<Collider2D>().enabled = false;
                this.GetComponent<Collider2D>().enabled = false;
                FindObjectOfType<obj_12_improper_and_mixed>().full_pizza = true;

                FindObjectOfType<obj_12_improper_and_mixed>().submit_2b6.gameObject.SetActive(true);
                this.enabled = false;
            }
            else
            {

            }

        }
        else if (col.gameObject.name == "PizzaBox" && this.name.Contains("2b6"))
        {
            FindObjectOfType<obj_12_improper_and_mixed>().first_dragged_id = col.gameObject.GetInstanceID().ToString();
            GetComponent<SpriteRenderer>().sortingOrder = 3;
            FindObjectOfType<obj_12_improper_and_mixed>().pizza_2b6 = true;
            reached = true;
            isDragging = false;
            this.transform.position = col.gameObject.transform.GetChild(0).transform.position;
            FindObjectOfType<obj_12_improper_and_mixed>().pizza_2b6_count++;
            FindObjectOfType<obj_12_improper_and_mixed>().submit_2b6.gameObject.SetActive(true);
            this.GetComponent<Collider2D>().enabled = false;
            this.enabled = false;
        }
        else if ((col.gameObject.name == "PizzaBox" && this.name== "lo3_pizza"))
        {
            FindObjectOfType<obj_12_improper_and_mixed>().lo3_pizza_count++;
            if (FindObjectOfType<obj_12_improper_and_mixed>().lo3_pizza_count <= 4)
            {
                
                FindObjectOfType<obj_12_improper_and_mixed>().lo3_pizza_inbox.transform.GetChild(FindObjectOfType<obj_12_improper_and_mixed>().lo3_pizza_count - 1).gameObject.SetActive(true);
                this.GetComponent<SpriteRenderer>().enabled = false;
                this.GetComponent<Collider2D>().enabled = false;
                this.enabled = false;
            }
            else if (FindObjectOfType<obj_12_improper_and_mixed>().lo3_pizza_count > 4)
            {
                FindObjectOfType<obj_12_improper_and_mixed>().lo3_pizza_count--;
                this.transform.position = pos;
            }

        }

       


    }

    

  
    


}
