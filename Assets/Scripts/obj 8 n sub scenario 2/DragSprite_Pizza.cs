using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

/// <summary>
/// /Place this script on any 2d sprite you want to drag and fix collider on object you want to drag
/// </summary>
public class DragSprite_Pizza : MonoBehaviour
{

    private bool isDragging;
    private bool isDraggingDone,reached,activity_done,wrong_slice;

    Vector2 pos;
  
   // GameObject ErrorMsg;
   // Text errorMsgText;
    public obj_8_subscenario2 Obj_8_Subscenario2;
    
    private void Awake()
    {
       
     //   ErrorMsg = GameObject.Find("Canvas/ResponseMsgPanel");
     //   errorMsgText = GameObject.Find("Canvas/ResponseMsgPanel/Image/Text").GetComponent<Text>();
    }

    private void Start()
    {
        Init();
    }

    void Init()
    {
        Obj_8_Subscenario2 = FindObjectOfType<obj_8_subscenario2>();
        pos = this.transform.position;
        wrong_slice = false;
        isDraggingDone = false;
      //  ErrorMsg.SetActive(false);
      

    }

    public void OnMouseDown()
    {
        if (isActiveAndEnabled)
        {

            isDragging = true;
            if (Obj_8_Subscenario2.chefCOnversationPanel.activeSelf)
            {
                Obj_8_Subscenario2.disable_panel(Obj_8_Subscenario2.chefCOnversationPanel, 0.5f);
            }
        }

    }

    public void OnMouseUp()
    {
        if (isActiveAndEnabled)
        {
            isDragging = false;
            if (!activity_done)
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
        if(col.tag == "Pizza_Box" && this.tag == "Equal_Pizza_Slice")
        {
            Debug.Log("Drag_Done");
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Obj_8_Subscenario2.pizza_in_the_box.SetActive(true);
            col.GetComponent<Animator>().enabled = true;
            isDragging = false;
              reached = true;
            activity_done = true;

            StartCoroutine(StopDragging());

        }

        else
        {
            isDragging = false;
            reached = false;
            activity_done = true;
            this.gameObject.transform.position = Obj_8_Subscenario2.pizza_in_the_box.transform.position;
            wrong_slice = true;
            StartCoroutine( ShowErrorMsg("A fraction is a representation of a part of an object that is equally divided. The quantity ordered and the quantity delivered is different. The customer is unhappy! Let's see why."));
           
        }
    }

    IEnumerator StopDragging()
    {
     //   yield return StartCoroutine(ShowErrorMsg("well done"));
        yield return new WaitForSeconds(0.7f);
        Obj_8_Subscenario2.pizza_in_the_box.SetActive(false);

        var d = FindObjectsOfType<DragSprite_Pizza>();
        foreach (DragSprite_Pizza pizzaSlice in d)
        {
            pizzaSlice.enabled = false;
        }
        Obj_8_Subscenario2.top_layer_objects.SetActive(false);
        Obj_8_Subscenario2.pizza_box.SetActive(false);
        Obj_8_Subscenario2.pizza_in_the_box.SetActive(false);
        Obj_8_Subscenario2.customer.SetActive(false);
        Obj_8_Subscenario2.playCorrect();
        FindObjectOfType<timeline_new>().load_next();
    }

    IEnumerator ShowErrorMsg(string msg)
    {
        yield return new WaitForSeconds(1.0f);
        Obj_8_Subscenario2.errorMsg.SetActive(true);
        Obj_8_Subscenario2.errorMsgText.GetComponent<Text>().text = msg;
        if (reached == false)
        {
            Obj_8_Subscenario2.playError();
            FindObjectOfType<timeline_new>().count = 13;
            FindObjectOfType<timeline_new>().playAudioOnRelearn("a_fraction_is_devided.wav");
            yield return new WaitForSeconds(14.0f);
            //Obj_8_Subscenario2.errorMsgText.GetComponent<Text>().text = "";
          //  Obj_8_Subscenario2.errorMsg.SetActive(false);
           
            Obj_8_Subscenario2.top_layer_objects.SetActive(false);
            this.gameObject.transform.position = pos;
            Obj_8_Subscenario2.pizza_box.SetActive(false);
            Obj_8_Subscenario2.pizza_in_the_box.SetActive(false);
            Obj_8_Subscenario2.customer.SetActive(false);
          
          //  
         //   FindObjectOfType<timeline_new>().load_next();

        }
        else
        {
            // FindObjectOfType<timeline_new>().playAudioOnRelearn("well_done.wav");
            //  yield return new WaitForSeconds(4.0f);
            Obj_8_Subscenario2.top_layer_objects.SetActive(false);
            Obj_8_Subscenario2.pizza_box.SetActive(false);
              Obj_8_Subscenario2.pizza_in_the_box.SetActive(false);
               Obj_8_Subscenario2.customer.SetActive(false);
            Obj_8_Subscenario2.playCorrect();
         //   FindObjectOfType<timeline_new>().load_next();
        }
    }

   
}