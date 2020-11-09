using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class Obj10Drag : MonoBehaviour
{
    private bool isDragging, reached;
    private bool isDraggingDone;
    public int numberOfAttempt = 0;
    Vector2 pos;
    public Transform pizzapos_inbox;


    string condition = "";
    void Start()
    {

       
    }

    private void OnEnable()
    {
        pos = this.transform.position;

    }
    public void OnMouseDown()
    {

        isDragging = true;
        this.GetComponent<SpriteRenderer>().sortingOrder = 3;
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
        if (!GameObject.FindObjectOfType<GameManager>().isGamePause)
        {
            if (GameObject.FindObjectOfType<GameManager>().isObj10On)
            {

                if (isDragging)
                {
                    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                    transform.Translate(mousePosition);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.name);
        if (col.gameObject.tag == "Pizza_Box" && this.tag == "biggest_slice")
        {
            Debug.Log("Drag_Done");
            pizzapos_inbox = col.transform.GetChild(2).GetComponent<Transform>();
            this.transform.position = pizzapos_inbox.position;
            this.tag = "tree";
            reached = true;
            isDragging = false;
           // GameObject.Find("SubmitButton").transform.GetChild(0).gameObject.SetActive(true);
           // GameObject.Find("Chef conversation").transform.GetChild(0).gameObject.SetActive(false);
           // GameObject.FindObjectOfType<GameManager>().isObj10On = false;
        }
        if (col.gameObject.name == "5" && this.name == "5 piece")
        {
            GameObject.FindObjectOfType<GameManager>().isObj10On = false;
            GameObject.FindObjectOfType<Obj10manager>().correctPizzaDragged = true;
            col.gameObject.name = "Placed in 5";
            StartCoroutine(show_right_prompt());
        }
        else if (col.gameObject.name == "4" && this.name == "4 piece")
        {
            GameObject.FindObjectOfType<GameManager>().isObj10On = false;
            GameObject.FindObjectOfType<Obj10manager>().correctPizzaDragged = true;
            col.gameObject.name = "Placed in 4";
            StartCoroutine(show_right_prompt());
        }
        else if (col.gameObject.name == "3" && this.name == "3 piece")
        {
            GameObject.FindObjectOfType<GameManager>().isObj10On = false;
            GameObject.FindObjectOfType<Obj10manager>().correctPizzaDragged = true;
            col.gameObject.name = "Placed in 3";
            StartCoroutine(show_right_prompt());
        }
        else if (col.gameObject.name == "2" && this.name == "2 piece")
        {
            GameObject.FindObjectOfType<GameManager>().isObj10On = false;
            GameObject.FindObjectOfType<Obj10manager>().correctPizzaDragged = true;
            col.gameObject.name = "Placed in 2";
            StartCoroutine(show_right_prompt());
        }
        else if (col.gameObject.name == "unequal" && this.name == "3 piece unqual")
        {
            GameObject.FindObjectOfType<GameManager>().isObj10On = false;
            //  GameObject.FindObjectOfType<Obj10manager>().correctPizzaDragged = true;
            col.gameObject.name = "Placed in 3 unequal";
            StartCoroutine(show_right_prompt_for_unequal());
        }
        else if (col.gameObject.name == "unequal" && this.name != "3 piece unqual")
        {
            GameObject.FindObjectOfType<GameManager>().isObj10On = false;
            //  GameObject.FindObjectOfType<Obj10manager>().correctPizzaDragged = true;
            // col.gameObject.name = "Placed in 3 unequal";
            if(numberOfAttempt<1)
            {
                numberOfAttempt++;
                StartCoroutine(show_onli_hint_1_prompt());
            }
            else
            {
                numberOfAttempt = 0;
                UtilityArtifacts.loading_pos = "Obj8_Lo1_from_obj10";
                UtilityArtifacts.backTraversal = false;
                UtilityArtifacts.comingbackafterTraversal = true;
                UtilityArtifacts.loadStartingpoint = 0;
                UtilityArtifacts.loadEndingpoint = 0;
                UtilityArtifacts.loadStartingpointforcomingback = 5;
                
                // load traversescene 8
                //SceneManager.LoadScene("OBJ_8_N_subscenario_2");
                OnTraversal(159, 130);
            }
            Debug.Log("wrong");
            
        }
        else
        {
            GameObject.FindObjectOfType<GameManager>().isObj10On = false;
            GameObject.FindObjectOfType<Obj10manager>().correctPizzaDragged = false;
            if (col.gameObject.name == "4")
                col.gameObject.name = "Placed in 4";
            else if (col.gameObject.name == "3")
                col.gameObject.name = "Placed in 3";
            else if (col.gameObject.name == "2")
                col.gameObject.name = "Placed in 2";
            else if (col.gameObject.name == "5")
                col.gameObject.name = "Placed in 5";
            StartCoroutine(show_wrong_prompt());


        }

    }

    IEnumerator show_right_prompt()
    {
        yield return new WaitForSeconds(2);
        FindObjectOfType<Obj10manager>().checkAnswer1();
    }
    IEnumerator show_wrong_prompt()
    {
        yield return new WaitForSeconds(2);
        if (FindObjectOfType<Obj10manager>().current_pizza == 5)
        {
            FindObjectOfType<Obj10manager>().checkAnswer2();
        }
        else
        {
            FindObjectOfType<Obj10manager>().checkAnswer1();
        }
    }
    IEnumerator show_onli_hint_1_prompt()
    {
        yield return new WaitForSeconds(2);
        GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
        FindObjectOfType<Obj10manager>().Obj10_ReinfoHint1();
}
    IEnumerator show_right_prompt_for_unequal()
    {
        yield return new WaitForSeconds(2);
        FindObjectOfType<Obj10manager>().pizzaCannotBePlaced1();
      //  FindObjectOfType<Obj10manager>().show();
    }
    public void resetAllPizza()
    {
        foreach (GameObject g1 in GameObject.FindGameObjectsWithTag("tree"))
            g1.tag = "biggest_slice";
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("biggest_slice"))
            g.transform.localPosition = new Vector3(0f, 0.11f, 0f);
        GameObject.FindObjectOfType<Obj10manager>().correctPizzaDragged = false;
        GameObject.FindObjectOfType<GameManager>().isObj10On = true;
        GameObject.Find("PizzaBoxes").transform.GetChild(0).GetChild(0).name="4";
        GameObject.Find("PizzaBoxes").transform.GetChild(0).GetChild(1).name = "5";
        GameObject.Find("PizzaBoxes").transform.GetChild(0).GetChild(2).name = "2";
        GameObject.Find("PizzaBoxes").transform.GetChild(0).GetChild(3).name = "3";

        Debug.Log("Done");
    }
    void OnTraversal(int objId, int subTopicId)
    {
        NewIncomingMsg mg = UtilityArtifacts.NJMsg;
        mg.current_activity_status = 1;
        mg.pre_req_id = subTopicId;// objId;
        mg.pre_req_status = 1;
        mg.pre_req_reqData.error_obj_id = objId;
        UtilityArtifacts.UnityMsg = UtilityArtifacts.outGoingMessage(mg, "false");
        SceneManager.LoadScene(UtilityArtifacts.NJLoadScene);
    }
}