using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class drag_drop_obj_15 : MonoBehaviour
{
    private bool isDragging, reached;
    private bool isDraggingDone;
    public int numberOfAttempt = 0;
    public Vector3 pos;
    public obj_15_new_story Obj_15_New_Story;
    private void Start()
    {
        Obj_15_New_Story = FindObjectOfType<obj_15_new_story>();
    }
    private void OnEnable()
    {
        pos = this.transform.position;
       
    }
    public void OnMouseDown()
    {
        isDragging = true;
        if (this.enabled)
            if (Obj_15_New_Story.chefConversationPanel.activeSelf)
            {
                Obj_15_New_Story.disable_panel(Obj_15_New_Story.chefConversationPanel, 0.5f);
            }
    }

    public void OnMouseUp()
    {
        isDragging = false;
        if (this.enabled&&!reached)
            this.transform.position = pos;
           
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

        if (col.gameObject.name == "PIZZA BOX" && this.tag == "top")
        {
            reached = true;
            isDragging = false;


            if (Obj_15_New_Story.current_slice == "1b2"&&this.name.Contains("1b2"))
            {
                
                    this.transform.position = Obj_15_New_Story.pizza_box_top.transform.position;
                increase_cookies();
            }
            else if (Obj_15_New_Story.current_slice == "2b4" && this.name.Contains("2b4"))
            {
                
                    this.transform.position = Obj_15_New_Story.pizza_box_top.transform.position;
                increase_cookies();
            }
            else if (Obj_15_New_Story.current_slice == "4b8" && this.name.Contains("4b8"))
            {
                   this.transform.position = Obj_15_New_Story.pizza_box_top.transform.position;
                increase_cookies();
            }
            else
            {
                this.transform.position = Obj_15_New_Story.pizza_box_bottom.transform.position;

                
                GameObject.FindObjectOfType<obj_15_new_story>().ShowWrongTraySubmitButton();
                //Invoke("select_wrong_tray", 1.0f);
            }
        }
        if (col.gameObject.name == "PIZZA BOX" && this.tag == "bottom" )
        {
            reached = true;
            isDragging = false;


            if (Obj_15_New_Story.current_slice == "1b2" && this.name.Contains("1b2"))
            {
                this.transform.position = Obj_15_New_Story.pizza_box_bottom.transform.position;
                increase_cookies();
            }
            else if (Obj_15_New_Story.current_slice == "2b4" && this.name.Contains("2b4"))
            {
                increase_cookies();
                this.transform.position = Obj_15_New_Story.pizza_box_bottom.transform.position;
            }
            else if (Obj_15_New_Story.current_slice == "4b8" && this.name.Contains("4b8"))
            {
                increase_cookies();
                this.transform.position = Obj_15_New_Story.pizza_box_bottom.transform.position;
            }
            else
            {
                this.transform.position = Obj_15_New_Story.pizza_box_bottom.transform.position;

                GameObject.FindObjectOfType<obj_15_new_story>().ShowWrongTraySubmitButton();
                //Invoke("select_wrong_tray",1.0f);
            }
        }

       
    }

    private void OnTriggerExit2D(Collider2D col)
    {

        if (col.gameObject.name == "PIZZA BOX" && this.tag == "top")
        {
            reached = false;
            if (Obj_15_New_Story.current_slice == "1b2" && this.name.Contains("1b2"))
            {
                decrease_cookies();
                isDragging = false;
                this.transform.position = pos;
            }
            else if (Obj_15_New_Story.current_slice == "2b4" && this.name.Contains("2b4"))
            {
                decrease_cookies();
                isDragging = false;
                this.transform.position = pos;
            }
            else if (Obj_15_New_Story.current_slice == "4b8" && this.name.Contains("4b8"))
            {
                decrease_cookies();
                isDragging = false;
                this.transform.position = pos;
            }
            else
            {
                isDragging = false;
                this.transform.position = pos;
              
            }
        }
        if (col.gameObject.name == "PIZZA BOX" && this.tag == "bottom")
        {
            reached = false;
            if (Obj_15_New_Story.current_slice == "1b2" && this.name.Contains("1b2"))
            {
                decrease_cookies();
                isDragging = false;
                this.transform.position = pos;
            }
            else if (Obj_15_New_Story.current_slice == "2b4" && this.name.Contains("2b4"))
            {
                decrease_cookies();
                isDragging = false;
                this.transform.position =pos;
            }
            else if (Obj_15_New_Story.current_slice == "4b8" && this.name.Contains("4b8"))
            {
                decrease_cookies();
                isDragging = false;
                this.transform.position = pos;
            }
            else
            {
                isDragging = false;
                this.transform.position = pos;
            }
        }


    }

    public void ResetCookies()
    {
        this.transform.position = pos;
    }

    public void select_wrong_tray()
    {
        if (numberOfAttempt < 1)
        {
            FindObjectOfType<obj_15_new_story>().OnHintWrongTray();
            FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_15_1b2_wrong_ans.wav");
            FindObjectOfType<obj_15_new_story>().set_conversation_msg("The tray from which you have picked up the cookie slab isn't right. Pick from the tray that matches the number of parts in the cookie slab and the denominator.");
            numberOfAttempt++;
        }
        else
        {
            numberOfAttempt = 0;
            UtilityArtifacts.loading_pos = "Obj4_Lo1_from_obj15";
            UtilityArtifacts.coming_back_from = "to_Obj15_quest1";
            UtilityArtifacts.backTraversal = true;
            UtilityArtifacts.comingbackafterTraversal = false;
            UtilityArtifacts.loadStartingpointforcomingback = 4;
            UtilityArtifacts.loadStartingpoint = 4;
            UtilityArtifacts.loadEndingpoint = 11;
            // load traversescene 4
            //SceneManager.LoadScene("Obj4AreaModule");
            OnTraversal(155, 129);
        }

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


    public void set_dialougue(string message)
    {
        Obj_15_New_Story.Dialouge_panel.SetActive(true);
        if (Obj_15_New_Story.Dialouge_text != null)
        {
            Obj_15_New_Story.Dialouge_text.GetComponent<TEXDraw>().text = message;
        }
    }


    public void increase_cookies()
    {
        Obj_15_New_Story.cookie_count = Obj_15_New_Story.cookie_count + 1;
    }

    public void decrease_cookies()
    {
        Obj_15_New_Story.cookie_count = Obj_15_New_Story.cookie_count - 1;
    }
}
