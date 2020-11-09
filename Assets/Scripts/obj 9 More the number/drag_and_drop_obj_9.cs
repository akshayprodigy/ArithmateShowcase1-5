using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class drag_and_drop_obj_9 : MonoBehaviour
{
    private bool isDragging,reached;
    private bool isDraggingDone;
    Vector2 pos;
    public Transform pizzapos_inbox;
    public OBJ_9_more_the_numbers OBJ_9_More_The_Numbers;
    // Start is called before the first frame update
   
    string condition = "";
    void Start()
    {
        
        OBJ_9_More_The_Numbers = FindObjectOfType<OBJ_9_more_the_numbers>();
       
        pizzapos_inbox = GameObject.Find("pizza in box pos").GetComponent<Transform>();
    }

    private void OnEnable()
    {
        pos = this.transform.position;
        if (FindObjectOfType<OBJ_9_more_the_numbers>().firstclicked == "null")
        {
            if (gameObject.tag == "smallest_slice")
            {
                gameObject.tag ="wrong_slice";
            }
        }
    }
    public void OnMouseDown()
    {

        isDragging = true;

    }

    public void OnMouseUp()
    {
        isDragging = false;
        if(this.enabled)
        if ((!reached && OBJ_9_More_The_Numbers.uncut_one == true)|| (!reached && OBJ_9_More_The_Numbers.uncut_two == true))
        {
            this.transform.position = pos;
        }
    }
    void Update()
    {
        if ((isDragging&&OBJ_9_More_The_Numbers.uncut_one==true)|| (isDragging && OBJ_9_More_The_Numbers.uncut_two == true))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.Translate(mousePosition);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        OBJ_9_More_The_Numbers.dialougue_ok_button.onClick.RemoveAllListeners();
       Debug.Log(col.name);
        if (col.gameObject.name == "PIZZA BOX" && this.tag == "biggest_slice" && OBJ_9_More_The_Numbers.biggest_slice == false)
        {
            Debug.Log("Drag_Done");
           
            int s = 0;
            s =int.Parse(FindObjectOfType<OBJ_9_more_the_numbers>().pizza_pieces_text.text);
            if (s == 2)
            {
                OBJ_9_More_The_Numbers.playCorrect();
                this.transform.position = pizzapos_inbox.position;
                this.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                reached = true;
                isDragging = false;
                col.GetComponent<Animator>().Play("packing");
                OBJ_9_More_The_Numbers.biggest_slice = true;
                OBJ_9_More_The_Numbers.dialougue_ok_button.onClick.AddListener(dialougue_ok_button_function);
                StartCoroutine(big_pizza_corutine());
                var f = FindObjectsOfType<drag_and_drop_obj_9>();
                foreach (drag_and_drop_obj_9 d in f)
                {
                    d.gameObject.GetComponent<Collider2D>().enabled = false;
                }

                OBJ_9_More_The_Numbers.customer1.GetComponent<change_colour_smoothly>().enabled = true;


                FindObjectOfType<OBJ_9_more_the_numbers>().firstclicked = "small";
            }
            else
            {
                OBJ_9_More_The_Numbers.playError();
               reached = true;
                this.transform.position = pizzapos_inbox.position;
                isDragging = false;
              
                OBJ_9_More_The_Numbers.dialougue_ok_button.onClick.AddListener(dialougue_ok_button_function);
                StartCoroutine(wrong_big_pizza_corutine());

            }


           

        }
        else if (col.gameObject.name == "PIZZA BOX" && this.tag == "smallest_slice" && OBJ_9_More_The_Numbers.smallest_slice == false)
        {
            Debug.Log("Drag_Done");
            
            int s = 0;
            s = int.Parse(FindObjectOfType<OBJ_9_more_the_numbers>().pizza_pieces_text.text);
            if (s == 10)
            {
                OBJ_9_More_The_Numbers.playCorrect();
                this.transform.position = pizzapos_inbox.position;
                reached = true;
                isDragging = false;
                col.GetComponent<Animator>().Play("packing");
                var f = FindObjectsOfType<drag_and_drop_obj_9>();
                foreach (drag_and_drop_obj_9 d in f)
                {
                    d.gameObject.GetComponent<Collider2D>().enabled = false;
                }
                this.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
               
                OBJ_9_More_The_Numbers.smallest_slice = true;
                OBJ_9_More_The_Numbers.dialougue_ok_button.onClick.AddListener(dialougue_ok_button_function);
                StartCoroutine(small_pizza_corutine());
               
            }
            else
            {
                OBJ_9_More_The_Numbers.playError();
               reached = true;
                this.transform.position = pizzapos_inbox.position;
              
                isDragging = false;
              
                OBJ_9_More_The_Numbers.dialougue_ok_button.onClick.AddListener(dialougue_ok_button_function);
                StartCoroutine(wrong_small_pizza_corutine());

            }

        }
        else if (col.gameObject.name == "PIZZA BOX" && this.tag == "wrong_slice")
        {
            reached = true;
            this.transform.position = pizzapos_inbox.position;
            
            isDragging = false;
            // FindObjectOfType<OBJ_9_more_the_numbers>().show_prompt("sorry..! you have to chose proper pizza slice as per order please try again");
            if (OBJ_9_More_The_Numbers.biggest_slice == false)
            {
                OBJ_9_More_The_Numbers.playError();
              
                OBJ_9_More_The_Numbers.dialougue_ok_button.onClick.AddListener(dialougue_ok_button_function);
                StartCoroutine(wrong_slice_coroutine_big_pizza());
            }
            else
            {
                OBJ_9_More_The_Numbers.playError();
               
                OBJ_9_More_The_Numbers.dialougue_ok_button.onClick.AddListener(dialougue_ok_button_function);
                StartCoroutine(wrong_slice_coroutine_small_pizza());
            }

        }
        else if (col.gameObject.name == "PIZZA BOX" && this.tag == "biggest_slice" && OBJ_9_More_The_Numbers.biggest_slice == true)
        {
            condition = "already";
            reached = false;
            this.transform.position = pos;
            isDragging = false;
            FindObjectOfType<OBJ_9_more_the_numbers>().show_prompt("You have already cut biggest slice now please cut smallest slice");
            OBJ_9_More_The_Numbers.dialougue_ok_button.onClick.AddListener(dialougue_ok_button_function);

        }
        else if (col.gameObject.name == "PIZZA BOX" && this.tag == "smallest_slice" && OBJ_9_More_The_Numbers.smallest_slice == true)
        {
            condition = "already";
            reached = false;
            this.transform.position = pos;
            isDragging = false;
            FindObjectOfType<OBJ_9_more_the_numbers>().show_prompt("You have already cut smallest slice now please cut biggest slice");
            OBJ_9_More_The_Numbers.dialougue_ok_button.onClick.AddListener(dialougue_ok_button_function);

        }




    }

    


    IEnumerator big_pizza_corutine()
    {
        condition = "big_pizza";
        yield return new WaitForSeconds(0.9f);
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(1f);


        FindObjectOfType<timeline_new>().playAudioOnRelearn("exp_q_1_ans_cutting_pizzas_into.wav");
        show_prompt("That's right. Cutting the pizzas into half would give the biggest slice of the mushroom pizza");

       
        if(!Application.isEditor)
        yield return new WaitForSeconds(FindObjectOfType<timeline_new>().length_of_audio);
        else
        yield return new WaitForSeconds(4);
      //  FindObjectOfType<OBJ_9_more_the_numbers>().Reset_pos_of_pizza_n_box();
      //  FindObjectOfType<OBJ_9_more_the_numbers>().Dialouge_panel.SetActive(false);
      //  FindObjectOfType<timeline_new>().load_next();
        OBJ_9_More_The_Numbers.customer1.SetActive(false);
     //   this.gameObject.SetActive(false);

        var d = FindObjectsOfType<drag_and_drop_obj_9>();
        foreach (drag_and_drop_obj_9 h in d)
        {
            h.enabled = false;
        }
    }

    IEnumerator wrong_big_pizza_corutine()
    {
        condition = "big_pizza_wrong";
        yield return new WaitForSeconds(1.6f);
        this.transform.position = pos;
        show_prompt("You are on the right track. Each slice is in the pizza which is cut into 3 slices is bigger than each slice in the four-sliced pizza. But you can get a bigger slice than this.\nTry one more time");
        FindObjectOfType<timeline_new>().playAudioOnRelearn("exp_q_1_ans_you_are_on.wav");
        yield return new WaitForSeconds(0.6f);
      
        if (!Application.isEditor)
            yield return new WaitForSeconds(FindObjectOfType<timeline_new>().length_of_audio);
        else
            yield return new WaitForSeconds(4);
      
     //   FindObjectOfType<OBJ_9_more_the_numbers>().Dialouge_panel.SetActive(false);
        reached = false;
    }


    IEnumerator wrong_small_pizza_corutine()
    {
        condition = "small_pizza_wrong";
        yield return new WaitForSeconds(1.6f);

        show_prompt("The slices in nine sliced pizza has a slice smaller than the eight-sliced pizza. But I'm sure you can get a slice smaller than this. ");
        FindObjectOfType<timeline_new>().playAudioOnRelearn("exp_q_2_ans_the_slices_in.wav");
        yield return new WaitForSeconds(0.6f);
        this.transform.position = pos;
        if (!Application.isEditor)
            yield return new WaitForSeconds(FindObjectOfType<timeline_new>().length_of_audio);
        else
            yield return new WaitForSeconds(4);

      //  FindObjectOfType<OBJ_9_more_the_numbers>().Dialouge_panel.SetActive(false);
        reached = false;
    }


    IEnumerator small_pizza_corutine()
    {
        condition = "small_pizza";
        yield return new WaitForSeconds(0.9f);
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(1f);
       

        show_prompt("That's right. Cutting the pizza into ten equal parts would give you the smallest slice of the pizza.");
        FindObjectOfType<timeline_new>().playAudioOnRelearn("exp_q_2_ans_that_right_Cutting_the.wav");

        if (!Application.isEditor)
            yield return new WaitForSeconds(FindObjectOfType<timeline_new>().length_of_audio);
        else
            yield return new WaitForSeconds(4);
    //    FindObjectOfType<OBJ_9_more_the_numbers>().Reset_pos_of_pizza_n_box();
       // FindObjectOfType<OBJ_9_more_the_numbers>().Dialouge_panel.SetActive(false);
     //   FindObjectOfType<timeline_new>().load_next();
        OBJ_9_More_The_Numbers.customer2.SetActive(false);
    //    this.gameObject.SetActive(false);
        var d = FindObjectsOfType<drag_and_drop_obj_9>();
        foreach (drag_and_drop_obj_9 h in d)
        {
            h.enabled = false;
        }

    }


    public void show_prompt(string prompt)
    {
        FindObjectOfType<OBJ_9_more_the_numbers>().Dialouge_text.GetComponent<TEXDraw>().text = prompt;
        FindObjectOfType<OBJ_9_more_the_numbers>().Dialouge_panel.SetActive(true);
       
       

    }


    IEnumerator wrong_slice_coroutine_big_pizza()
    {
        condition = "wrong_slice_big";
        yield return new WaitForSeconds(1.6f);
        show_prompt("We need to make the pizza have slices which are bigger than \\frac{1}{4}. This pizza has slices which are smaller than \\frac{1}{4}. \nTry again.");
        FindObjectOfType<timeline_new>().playAudioOnRelearn("exp_q_1_ans_we_need_to_make_big.wav");
        yield return new WaitForSeconds(0.6f);
        this.transform.position = pos;
        if (!Application.isEditor)
            yield return new WaitForSeconds(FindObjectOfType<timeline_new>().length_of_audio);
        else
            yield return new WaitForSeconds(4);

       // FindObjectOfType<OBJ_9_more_the_numbers>().Dialouge_panel.SetActive(false);
        reached = false;
    }


    IEnumerator wrong_slice_coroutine_small_pizza()
    {
        condition = "wrong_slice_small";
        yield return new WaitForSeconds(1.6f);
        show_prompt("We need to make the pizza have slices which are smaller than \\frac{1}{8}. This pizza has slices which are bigger than \\frac{1}{8}. \nTry again.");
        FindObjectOfType<timeline_new>().playAudioOnRelearn("exp_q_1_ans_we_need_to_make_small.wav");
        yield return new WaitForSeconds(0.6f);
        this.transform.position = pos;
        if (!Application.isEditor)
            yield return new WaitForSeconds(FindObjectOfType<timeline_new>().length_of_audio);
        else
            yield return new WaitForSeconds(4);

       // FindObjectOfType<OBJ_9_more_the_numbers>().Dialouge_panel.SetActive(false);
        reached = false;
    }


    public void dialougue_ok_button_function()
    {
        StopAllCoroutines();
        FindObjectOfType<timeline_new>().lipsync_player.GetComponent<AudioSource>().Stop();
        switch (condition)
        {
            case "big_pizza":
                this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                FindObjectOfType<OBJ_9_more_the_numbers>().Reset_pos_of_pizza_n_box();
                FindObjectOfType<OBJ_9_more_the_numbers>().Dialouge_panel.SetActive(false);
                FindObjectOfType<timeline_new>().load_next();
                var d = FindObjectsOfType<drag_and_drop_obj_9>();
                foreach (drag_and_drop_obj_9 h in d)
                {
                    h.enabled = false;
                }
                this.gameObject.SetActive(false);
                break;

            case "big_pizza_wrong":
                reached = false;
                this.transform.position = pos;
                FindObjectOfType<OBJ_9_more_the_numbers>().Dialouge_panel.SetActive(false);
                break;

            case "small_pizza_wrong":
                reached = false;
                this.transform.position = pos;
                FindObjectOfType<OBJ_9_more_the_numbers>().Dialouge_panel.SetActive(false);
                break;

            case "small_pizza":
                this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                FindObjectOfType<OBJ_9_more_the_numbers>().Reset_pos_of_pizza_n_box();
                FindObjectOfType<OBJ_9_more_the_numbers>().Dialouge_panel.SetActive(false);
                FindObjectOfType<timeline_new>().load_next();
                var da = FindObjectsOfType<drag_and_drop_obj_9>();
                foreach (drag_and_drop_obj_9 h in da)
                {
                    h.enabled = false;
                }
                this.gameObject.SetActive(false);

                break;

            case "wrong_slice_big":
                reached = false;
                this.transform.position = pos;
                FindObjectOfType<OBJ_9_more_the_numbers>().Dialouge_panel.SetActive(false);
                break;

            case "wrong_slice_small":
                reached = false;
                this.transform.position = pos;
                FindObjectOfType<OBJ_9_more_the_numbers>().Dialouge_panel.SetActive(false);
                break;

            case "already":
                this.transform.position = pos;
                FindObjectOfType<OBJ_9_more_the_numbers>().Dialouge_panel.SetActive(false);
                break;
        }
        OBJ_9_More_The_Numbers.dialougue_ok_button.onClick.RemoveAllListeners();
    }
}
