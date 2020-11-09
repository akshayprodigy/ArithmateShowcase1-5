using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Obj11Drag : MonoBehaviour
{
    private bool isDragging, reached;
    private bool isDraggingDone;
    Vector2 pos;
    public Transform pizzapos_inbox;
   

    string condition = "";
    void Start()
    {

        pizzapos_inbox = GameObject.Find("pizza in box pos").GetComponent<Transform>();
    }

    private void OnEnable()
    {
        pos = this.transform.position;
        
    }
    public void OnMouseDown()
    {

        isDragging = true;
        this.GetComponent<SpriteRenderer>().sortingOrder = 6;

    }

    public void OnMouseUp()
    {
        isDragging = false;
        //this.transform.position = pos;
        
        if (this.enabled)
            if (!reached)
            {
                this.transform.position = pos;
                this.GetComponent<SpriteRenderer>().sortingOrder = 3;
                
            }
    }
    void Update()
    {
        if(GameObject.FindObjectOfType<GameManager>().isObj11On)
        if (isDragging )
        {
                if(this.gameObject.tag== "biggest_slice")
                {
                    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                    transform.Translate(mousePosition);

                }
           
        }
        if (GameObject.FindObjectOfType<Obj11Manager>().eightPiecePizza < 0)
            GameObject.FindObjectOfType<Obj11Manager>().eightPiecePizza = 0;
        if (GameObject.FindObjectOfType<Obj11Manager>().SixPiecePizza < 0)
            GameObject.FindObjectOfType<Obj11Manager>().SixPiecePizza = 0;
        if (GameObject.FindObjectOfType<Obj11Manager>().threePiecePizza < 0)
            GameObject.FindObjectOfType<Obj11Manager>().threePiecePizza = 0;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.name);
        if (col.gameObject.name == "PIZZA BOX" && this.tag == "biggest_slice")
        {
            Debug.Log("Drag_Done");
            this.transform.position = pizzapos_inbox.position;
            this.tag = "tree";
            reached = true;
            isDragging = false;
            GameObject.Find("SubmitButton").transform.GetChild(0).gameObject.SetActive(true);
            GameObject.Find("Chef conversation").transform.GetChild(0).gameObject.SetActive(false);
        }
        if (col.gameObject.name == "PIZZA BOX" && this.name == "3 piece")
        {
            GameObject.FindObjectOfType<Obj11Manager>().threePiecePizza = GameObject.FindObjectOfType<Obj11Manager>().threePiecePizza+1;
        }
        if (col.gameObject.name == "PIZZA BOX" && this.name == "6 piece")
        {
            GameObject.FindObjectOfType<Obj11Manager>().SixPiecePizza = GameObject.FindObjectOfType<Obj11Manager>().SixPiecePizza + 1;
        }
        if (col.gameObject.name == "PIZZA BOX" && this.name == "8 piece")
        {
            GameObject.FindObjectOfType<Obj11Manager>().eightPiecePizza = GameObject.FindObjectOfType<Obj11Manager>().eightPiecePizza + 1;
        }

    }
    private void OnTriggerExit2D(Collider2D col)
    {
       
        if (col.gameObject.name == "PIZZA BOX" && this.name == "3 piece")
        {
            GameObject.FindObjectOfType<Obj11Manager>().threePiecePizza = GameObject.FindObjectOfType<Obj11Manager>().threePiecePizza - 1;
        }
        if (col.gameObject.name == "PIZZA BOX" && this.name == "6 piece")
        {
            GameObject.FindObjectOfType<Obj11Manager>().SixPiecePizza = GameObject.FindObjectOfType<Obj11Manager>().SixPiecePizza - 1;
        }
        if (col.gameObject.name == "PIZZA BOX" && this.name == "8 piece")
        {
            GameObject.FindObjectOfType<Obj11Manager>().eightPiecePizza = GameObject.FindObjectOfType<Obj11Manager>().eightPiecePizza - 1;
        }

    }

    public void resetAllPizza()
    {
        foreach (GameObject g1 in GameObject.FindGameObjectsWithTag("tree"))
            g1.tag= "biggest_slice";
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("biggest_slice"))
            g.transform.localPosition = new Vector3(0f,0.11f,0f) ;
        GameObject.FindObjectOfType<Obj11Manager>().eightPiecePizza = 0;
        GameObject.FindObjectOfType<Obj11Manager>().SixPiecePizza = 0;
        GameObject.FindObjectOfType<Obj11Manager>().threePiecePizza = 0;
        Debug.Log("Done");
    }

}