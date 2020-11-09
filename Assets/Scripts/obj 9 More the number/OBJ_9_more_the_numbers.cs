using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class OBJ_9_more_the_numbers : MonoBehaviour
{
    string jsonFileName = "obj_9_more_the_number.json";

    public Button increase, decrease;
    public Text pizza_pieces_text;
    public GameObject uncut_pizza_one,uncut_pizza_two,cut_pizza_one,cut_pizza_two, increase_decrease_panel,Dialouge_panel, Dialouge_text, Dialouge_panel_ro2, Dialouge_text_ro2, pizza_box,grayed_background,object_to_move, desired_pos_pizza,notepad_page,notepad_text, desired_pos_box,chefConversationPanel,chef_ConversationText,customer1,customer2,pizzas_on_table,lesser_parts,more_parts,lo2_explain, Exit_Panel, oneb4,oneb8, ro_1_dialougue,bigger,smaller,  whole_increases,whole_decreases, reference_1b4_pizza, reference_1b8_pizza;
    public GameObject[] uncut_one_array, uncut_two_array;
    public bool uncut_one, uncut_two,move_center,smallest_slice,biggest_slice;
    public int pointer,pizza_pieces;
    public Vector3 initial_pos_pizza,initial_pos_box,cut_one_inital,cut_two_initial;
    public GameObject RO_panel,ro_1_ans_panel,ro_2_ans_panel;
    public TEXDraw RO_question;
    public Button notepad_button,notepad_ok_button,ro_submit_pizza,ro1_op_1,ro1_op_2,dialougue_ok_button, dialougue_ok_button_ro2, ro2_op_1, ro2_op_2,ro2_op_3, ro2_op_4;
    public float transition_speed;
    public string ans,condition;
    public Button exit_button;
    public Sprite five_pizza,seven_pizza;
    public string firstclicked,big_slice;
    public GameObject tmp1, tmp2,temp;
    public GameObject LoadingAudio;
    // Start is called before the first frame update
    void Start()
    {

        Initialization();
        Invoke("audio_invoke", 2.0f);
    }

    private void OnEnable()
    {
        timeline_new.OnEventCalled += EventToHandle;
    }



    private void OnDisable()
    {
        timeline_new.OnEventCalled -= EventToHandle;
    }
    void Initialization()

    {
        GetallNumberButtons();
        firstclicked = "null";
        Dialouge_text = GameObject.Find("Dialougue text");
        Dialouge_panel = GameObject.Find("Dialougue Panel");
        dialougue_ok_button = GameObject.Find("dialougue_ok_button").GetComponent<Button>();

        Dialouge_text_ro2 = GameObject.Find("Dialougue text ro2");
        Dialouge_panel_ro2 = GameObject.Find("Dialougue Panel RO2");
        dialougue_ok_button_ro2 = GameObject.Find("dialougue_ok_button ro2").GetComponent<Button>();

        cut_pizza_one = GameObject.Find("cut_pizza_one");
        cut_pizza_two = GameObject.Find("cut_pizza_two");
        uncut_pizza_one = GameObject.Find("uncut pizza one");
        uncut_pizza_two = GameObject.Find("uncut pizza two");
        pizzas_on_table = GameObject.Find("pizzas on table");
        uncut_one_array = new GameObject[uncut_pizza_one.transform.childCount];
        uncut_two_array = new GameObject[uncut_pizza_two.transform.childCount];
        increase = GameObject.Find("increase_button").GetComponent<Button>();
        decrease = GameObject.Find("decrease_button").GetComponent<Button>();
        increase_decrease_panel = GameObject.Find("increase_decrease buttons");
        pizza_pieces_text = GameObject.Find("pizza_pieces_text").GetComponent<Text>();
        pizza_box = GameObject.Find("PIZZA BOX");
        grayed_background = GameObject.Find("grayed_out_background");
        desired_pos_pizza = GameObject.Find("desired_pos_pizza");
        desired_pos_box = GameObject.Find("desire_pos_box");
        chefConversationPanel = GameObject.Find("Chef conversation");
        chef_ConversationText = GameObject.Find("chef ConversationText");
        notepad_page = GameObject.Find("notepad_page");
        notepad_text = GameObject.Find("notepad_text");
        notepad_ok_button = GameObject.Find("notepad_ok").GetComponent<Button>();
        notepad_button = GameObject.Find("notepad_button").GetComponent<Button>();
        customer1 = GameObject.Find("customer 1");
        customer2 = GameObject.Find("customer 2");
        lesser_parts = GameObject.Find("lesser_parts");
        more_parts = GameObject.Find("more_parts");
        oneb4 = GameObject.Find("oneby_4");
        oneb8 = GameObject.Find("oneby_8");



        RO_panel = GameObject.Find("RO Panel");
        RO_question = GameObject.Find("ROQuestion").GetComponent<TEXDraw>();
        ro_submit_pizza = GameObject.Find("RO_Submit_button").GetComponent<Button>();
        ro_1_ans_panel = GameObject.Find("RO1OPT");
        ro_2_ans_panel = GameObject.Find("RO2OPT");
        ro1_op_1 = GameObject.Find("RO op 1").GetComponent<Button>();
        ro1_op_2 = GameObject.Find("RO op 2").GetComponent<Button>();
        ro2_op_1 = GameObject.Find("RO2 op 1").GetComponent<Button>();
        ro2_op_2 = GameObject.Find("RO2 op 2").GetComponent<Button>();
        ro2_op_3 = GameObject.Find("RO2 op 3").GetComponent<Button>();
        ro2_op_4 = GameObject.Find("RO2 op 4").GetComponent<Button>();
        ro_1_dialougue = GameObject.Find("ro_1_dialougue");

        lo2_explain = GameObject.Find("lo2");

        Exit_Panel = GameObject.Find("Exit Panel");
       
        exit_button = GameObject.Find("Exit_button").GetComponent<Button>();
        bigger = GameObject.Find("bigger_slice_text");
        smaller = GameObject.Find("smaller_slice_text");

        whole_increases=GameObject.Find("whole_increases");
        whole_decreases=GameObject.Find("whole_decreases");
        reference_1b4_pizza = GameObject.Find("reference 1b4 pizza");
        reference_1b8_pizza = GameObject.Find("reference 1b8 pizza");

        LoadingAudio = GameObject.Find("LoadAudio");
        //if (UtilityArtifacts.backTraversal)
        //{
        //    Text textLoadingText = LoadingAudio.transform.GetChild(1).GetComponent<Text>();
        //    textLoadingText.text = "Let us understand this better";
        //}
        for (int i = 0; i < uncut_one_array.Length; i++)
        {
            uncut_one_array[i] = uncut_pizza_one.transform.GetChild(i).gameObject;
        }
        for (int i = 0; i < uncut_two_array.Length; i++)
        {
            uncut_two_array[i] = uncut_pizza_two.transform.GetChild(i).gameObject;
        }
        pointer = 0;

        pizza_pieces_text.text = "0";

        increase.onClick.AddListener(() => increase_pizza_slices());
        decrease.onClick.AddListener(() => decrease_pizza_slice());
        notepad_button.onClick.AddListener(()=>enable_notepad());
        notepad_ok_button.onClick.AddListener(() => disable_notepad());
        ro_submit_pizza.onClick.AddListener(()=>validate_answer());
        exit_button.onClick.AddListener(quit_app);


        initial_pos_box = pizza_box.transform.position;

        for (int i = 0; i < uncut_two_array.Length; i++)
        {
            uncut_two_array[i].SetActive(false);
        }
        for (int i = 0; i < uncut_one_array.Length; i++)
        {
            uncut_one_array[i].SetActive(false);
        }


        cut_one_inital = cut_pizza_one.transform.position;
        cut_two_initial = cut_pizza_two.transform.position;

        increase_decrease_panel.SetActive(false);
        Dialouge_panel_ro2.SetActive(false);
        Dialouge_panel.SetActive(false);
        grayed_background.SetActive(false);
        chefConversationPanel.SetActive(true);
        move_center = false;

        notepad_ok_button.interactable = false;
        smallest_slice = false;
        biggest_slice = false;
        transition_speed=4;
        uncut_pizza_one.GetComponent<Collider2D>().enabled = false;
        uncut_pizza_two.GetComponent<Collider2D>().enabled = false;

        chefConversationPanel.SetActive(false);
        pizzas_on_table.SetActive(false);
        pizza_box.SetActive(false);
        customer1.SetActive(false);
        customer2.SetActive(false);
        notepad_button.gameObject.SetActive(false);
        lesser_parts.SetActive(false);
        more_parts.SetActive(false);
        ro_1_ans_panel.SetActive(false);
        ro_2_ans_panel.SetActive(false);
        RO_panel.SetActive(false);
        lo2_explain.SetActive(false);
        Exit_Panel.SetActive(false);
        oneb4.SetActive(false);
        oneb8.SetActive(false);
        ro_1_dialougue.SetActive(false);
        bigger.SetActive(false);
        smaller.SetActive(false);
        whole_increases.SetActive(false);
        whole_decreases.SetActive(false);
        reference_1b4_pizza.SetActive(false);
        reference_1b8_pizza.SetActive(false);
        //  click_on_notepad();
    }



    public void GetallNumberButtons()
    {
        foreach (GameObject b in GameObject.FindGameObjectsWithTag("Numbers"))
        {
            b.GetComponent<Button>().onClick.AddListener(InputNumbers);
        }
    }
    public void InputNumbers()
    {
        var currentEventSystem = EventSystem.current;
        var currentSelectedGameObject = currentEventSystem.currentSelectedGameObject;
         Debug.Log("numerator =" + currentSelectedGameObject.name);
            ans = currentSelectedGameObject.name;
            foreach (GameObject b in GameObject.FindGameObjectsWithTag("Numbers"))
            {
                b.transform.GetChild(2).gameObject.SetActive(false);
            }
            currentSelectedGameObject.transform.GetChild(2).gameObject.SetActive(true);
        temp = currentSelectedGameObject;
     }


        void audio_invoke()
        {
            Debug.Log(EVENT.SetUpTimeLine);
            EventManager.setNameOfJasonFile(jsonFileName);
            EventManager.Broadcast(EVENT.SetUpTimeLine);
        }

    void increase_pizza_slices()
    {
        //if (chefConversationPanel.activeSelf)
        //disable_panel(chefConversationPanel, chefConversationPanel.GetComponent<Animator>(), 0.5f);
        
        increase.interactable = true;
        decrease.interactable = true;
        if (uncut_one)
        {
            uncut_pizza_one.GetComponent<SpriteRenderer>().enabled = false;
            for (int i = 0; i < uncut_one_array.Length; i++)
            {
                uncut_one_array[i].SetActive(false);
            }

            if (pointer >= uncut_one_array.Length-1)
            {
               

                pointer = uncut_one_array.Length - 1;
                uncut_one_array[pointer].SetActive(true);
                increase.interactable = false;

            }
            else
            {

                uncut_one_array[pointer].SetActive(true);
            }
            pizza_pieces_text.text = uncut_one_array[pointer].transform.childCount.ToString();
        }
        if (uncut_two)
        {
            uncut_pizza_two.GetComponent<SpriteRenderer>().enabled = false;
            for (int i = 0; i < uncut_two_array.Length; i++)
            {
                uncut_two_array[i].SetActive(false);
            }
            if (pointer >= uncut_two_array.Length-1)
            {
               
                pointer = uncut_two_array.Length - 1;
                uncut_two_array[pointer].SetActive(true);
                increase.interactable = false;

            }
            else
            {
                uncut_two_array[pointer].SetActive(true);
            }
            pizza_pieces_text.text = uncut_two_array[pointer].transform.childCount.ToString();
        }

        pointer = pointer + 1;

    }




    void decrease_pizza_slice()
    {
        //if (chefConversationPanel.activeSelf)
        //    disable_panel(chefConversationPanel, chefConversationPanel.GetComponent<Animator>(), 0.5f);

        pointer = pointer - 1;
        increase.interactable = true;
        decrease.interactable = true;
        if (uncut_one)
        {
            uncut_pizza_one.GetComponent<SpriteRenderer>().enabled = false;
            for (int i = 0; i < uncut_one_array.Length; i++)
            {
                uncut_one_array[i].SetActive(false);
            }
            if (pointer <= 0)
            {

                pointer =0;
                uncut_one_array[pointer].SetActive(true);
                decrease.interactable = false;

            }
            else
            {
                uncut_one_array[pointer].SetActive(true);
            }
            pizza_pieces_text.text = uncut_one_array[pointer].transform.childCount.ToString();
        }


        if (uncut_two)
        {
            uncut_pizza_two.GetComponent<SpriteRenderer>().enabled = false;
            for (int i = 0; i < uncut_two_array.Length; i++)
            {
                uncut_two_array[i].SetActive(false);
            }
            if( pointer <= 0)
            {

                pointer = 0;
                uncut_two_array[pointer].SetActive(true);
                decrease.interactable = false;

            }
            else
            {
                uncut_two_array[pointer].SetActive(true);
            }
            pizza_pieces_text.text = uncut_two_array[pointer].transform.childCount.ToString();
        }
    }


    public GameObject getclicked_obj_2d()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10;

        Vector3 screenPos = Camera.main.ScreenToWorldPoint(mousePos);

        RaycastHit2D hit = Physics2D.Raycast(screenPos, Vector2.zero);

        if (hit)
        {
            return hit.collider.gameObject;
        }
        else
        {
            return null;
        }

    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FindObjectOfType<timeline_new>().load_next();
        }
        if (!IsPointerOverUIElement())
        {
            Debug.Log("not over ui");

            if (Input.GetMouseButtonDown(0))
            {
                GameObject hit_obj = getclicked_obj_2d();
                if (hit_obj != null)
                {
                    Debug.Log(hit_obj.name);
                    if (hit_obj.name.Equals("uncut pizza one"))
                    {
                        pointer = 0;
                        grayed_background.SetActive(true);
                        move_center = true;
                        hit_obj.GetComponent<Collider2D>().enabled = false;
                        uncut_pizza_two.GetComponent<Collider2D>().enabled = false;
                        initial_pos_pizza = uncut_pizza_one.transform.parent.position;
                        object_to_move = uncut_pizza_one.transform.parent.gameObject;
                        uncut_one = true;
                        uncut_two = false;
                        // increase_decrease_panel.SetActive(true);
                        //  increase.interactable = false;
                        //  decrease.interactable = false;
                        Invoke("inrease_enable", 1.3f);
                        uncut_pizza_one.transform.parent.GetComponent<SortingGroup>().sortingOrder = 5;
                        uncut_pizza_two.transform.parent.GetComponent<SortingGroup>().sortingOrder = 2;
                        uncut_pizza_one.transform.parent.GetComponent<AnimateColors>().enabled=false;
                        uncut_pizza_two.transform.parent.GetComponent<AnimateColors>().enabled = false;
                        uncut_pizza_one.transform.parent.GetComponent<SpriteRenderer>().color = Color.white;
                        uncut_pizza_two.transform.parent.GetComponent<SpriteRenderer>().color = Color.white;
                     
                        pizza_pieces_text.text = "0";
                        if (firstclicked == "null")
                        {
                            increase.interactable = false;
                            decrease.interactable = false;
                            FindObjectOfType<timeline_new>().load_next();
                            big_slice = "uncut pizza one";
                        }
                        
                        

                    }
                    else if (hit_obj.name.Equals("uncut pizza two"))
                    {
                        //chefConversationPanel.SetActive(false);
                        pointer = 0;
                        grayed_background.SetActive(true);
                        move_center = true;
                        hit_obj.GetComponent<Collider2D>().enabled = false;
                        uncut_pizza_one.GetComponent<Collider2D>().enabled = false;
                        initial_pos_pizza = uncut_pizza_two.transform.parent.position;
                        object_to_move = uncut_pizza_two.transform.parent.gameObject;
                        uncut_two = true;
                        uncut_one = false;
                       // increase_decrease_panel.SetActive(true);
                        uncut_pizza_two.transform.parent.GetComponent<SortingGroup>().sortingOrder = 5;
                        uncut_pizza_one.transform.parent.GetComponent<SortingGroup>().sortingOrder = 2;
                        uncut_pizza_one.transform.parent.GetComponent<AnimateColors>().enabled = false;
                        uncut_pizza_two.transform.parent.GetComponent<AnimateColors>().enabled = false;
                        uncut_pizza_one.transform.parent.GetComponent<SpriteRenderer>().color = Color.white;
                        uncut_pizza_two.transform.parent.GetComponent<SpriteRenderer>().color = Color.white;
                        Invoke("inrease_enable", 1.3f);
                        if (firstclicked == "null")
                        {
                            increase.interactable = false;
                            decrease.interactable = false;
                            FindObjectOfType<timeline_new>().load_next();
                            big_slice = "uncut pizza two";
                            // GameObject.Find("uncut pizza one").GetComponent<Collider>().enabled = false;
                        }
                        pizza_pieces_text.text = "0";
                       

                    }

                }

            }
        }
    }

    

    public static bool IsPointerOverUIElement()
    {
        return IsPointerOverUIElement(GetEventSystemRaycastResults());
    }
    ///Returns 'true' if we touched or hovering on Unity UI element.
    public static bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];
            if (curRaysastResult.gameObject.layer == LayerMask.NameToLayer("UI"))
                return true;
        }
        return false;
    }
    ///Gets all event systen raycast results of current mouse or touch position.
    static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        return raysastResults;
    }





    public void enable_panel(GameObject object_to_enable)
    {
        object_to_enable.SetActive(true);
        Animator animator_of_object;
        animator_of_object = object_to_enable.GetComponent<Animator>();
       
        animator_of_object.Play("enable", 0);
    }

    public void disable_panel(GameObject object_to_enable, Animator animator_of_object, float time)
    {
        Coroutine a = StartCoroutine(disable_after(object_to_enable, animator_of_object, time));
    }

    IEnumerator disable_after(GameObject object_to_enable, Animator animator_of_object, float time)
    {
        animator_of_object.Play("disable", 0);
        yield return new WaitForSeconds(time);
        object_to_enable.SetActive(false);

    }



    public void set_dialougue(string message)
    {
        if (Dialouge_text != null)
        {
            Dialouge_text.GetComponent<TEXDraw>().text = message;
        }
    }


    public void set_conversation_msg(string message)
    {
        if (chef_ConversationText != null)
        {
            chef_ConversationText.GetComponent<TEXDraw>().text = message;
        }
    }

    public void show_prompt(string prompt)
    {
        Dialouge_panel.SetActive(true);
       Dialouge_text.GetComponent<TEXDraw>().text = prompt;
     //  enable_panel(Dialouge_panel);
        StartCoroutine(hide_prompt());
    }
    IEnumerator hide_prompt()
    {
        yield return new WaitForSeconds(4.0f);
        //   disable_panel(Dialouge_panel,Dialouge_panel.GetComponent<Animator>(), 0.5f);
        Dialouge_panel.SetActive(false);

    }


    public void LateUpdate()
    {

      
        if (move_center&&object_to_move!=null)
        {
            Debug.Log("started" );
            float step = transition_speed * Time.deltaTime; // calculate distance to move
            pizza_box.SetActive(true);
            object_to_move.transform.position = Vector3.MoveTowards(object_to_move.transform.position, desired_pos_pizza.transform.position, step);
            pizza_box.transform.position = Vector3.MoveTowards(pizza_box.transform.position, desired_pos_box.transform.position, step);
         
            if (/*(Vector3.Distance(object_to_move.transform.position, desired_pos_box.transform.position) < 2f )*/( Vector3.Distance(pizza_box.transform.position, desired_pos_box.transform.position) < 0.1f))
            {
                Debug.Log("reached");
                move_center = false;
               // object_to_move = null;
               


            }
        }
    }
    void inrease_enable()
    {
        increase_decrease_panel.SetActive(true);
    }


    public void Reset_pos_of_pizza_n_box() 
    {
        
        object_to_move.transform.position = initial_pos_pizza;
        pizza_box.transform.position = initial_pos_box;
        pizza_box.GetComponent<Animator>().Play("empty");
       
        if (object_to_move.gameObject == uncut_pizza_one.transform.parent.gameObject)
        {
            if (smallest_slice == false || biggest_slice == false)
            {
                uncut_pizza_two.GetComponent<Collider2D>().enabled = true;
                uncut_pizza_one.GetComponent<Collider2D>().enabled = false;
                uncut_pizza_one.transform.parent.GetComponent<AnimateColors>().enabled = false;
                uncut_pizza_two.transform.parent.GetComponent<AnimateColors>().enabled = true;
            

          //      chef_ConversationText.GetComponent<Text>().text = "now click on another pizza to cut";
               // enable_panel(chefConversationPanel);
            }
            else
            {
                uncut_pizza_two.GetComponent<Collider2D>().enabled = false;
                uncut_pizza_one.GetComponent<Collider2D>().enabled = false;
                uncut_pizza_one.transform.parent.GetComponent<AnimateColors>().enabled = false;
                uncut_pizza_two.transform.parent.GetComponent<AnimateColors>().enabled = false;
            }
        }
        if (object_to_move.gameObject == uncut_pizza_two.transform.parent.gameObject)
        {
            if (smallest_slice == false || biggest_slice == false)
            {
                uncut_pizza_two.GetComponent<Collider2D>().enabled = false;
                uncut_pizza_one.GetComponent<Collider2D>().enabled = true;

                uncut_pizza_one.transform.parent.GetComponent<AnimateColors>().enabled = true;
                uncut_pizza_two.transform.parent.GetComponent<AnimateColors>().enabled = false;

             //   chef_ConversationText.GetComponent<Text>().text = "now click on another pizza to cut";
              //  enable_panel(chefConversationPanel);
            }
            else
            {
                uncut_pizza_two.GetComponent<Collider2D>().enabled = false;
                uncut_pizza_one.GetComponent<Collider2D>().enabled = false;
                uncut_pizza_one.transform.parent.GetComponent<AnimateColors>().enabled = false;
                uncut_pizza_two.transform.parent.GetComponent<AnimateColors>().enabled = false;
            }
        }

        object_to_move = null;
        grayed_background.SetActive(false);
        increase_decrease_panel.SetActive(false);
       // uncut_one = false;
      //  uncut_two = false;
    }




    void click_on_notepad()
    {
        set_conversation_msg("Click on Notepad for next Order");
        enable_panel(chefConversationPanel);
    }

    void enable_notepad()
    {

        disable_panel(chefConversationPanel, chefConversationPanel.GetComponent<Animator>(), 1.0f);
        notepad_text.GetComponent<TEXDraw>().text = "1 Biggest slice of\nmushroom pizza\n\n1 Smallest slice of\nmushroom pizza";
        enable_panel(notepad_page);
        notepad_button.interactable = false;
        notepad_ok_button.interactable = true;
        FindObjectOfType<timeline_new>().load_next();

    }

    void disable_notepad()
    {
        disable_panel(notepad_page, notepad_page.GetComponent<Animator>(),0.5f);
        notepad_button.GetComponentInChildren<AnimateColors>().enabled = false;
        notepad_button.GetComponentInChildren<AnimateColors>().gameObject.GetComponent<Image>().color = Color.white;
        notepad_ok_button.interactable = false;
        FindObjectOfType<timeline_new>().load_next();

       
    }






    //****************------CASES------************************//

    void HideLoadingAudio()
    {
        LoadingAudio.SetActive(false);
    }
    void EventToHandle(string EventName)
    {
        HideLoadingAudio();
        switch (EventName)
        {

            case "obj_9_great_we_are_done":
                Debug.Log("hi");
                set_conversation_msg("Great! We are done with the first order of pizzas");
                enable_panel(chefConversationPanel);
                break;
            case "obj_9_lets_move_onto_next":
                set_conversation_msg("Let's move onto the next. And let's see if we can learn something more about fractions while we fulfill the order");
                break;
            case "obj_9_click_on_the":
                set_conversation_msg("Click on the notepad to see the next order");
                notepad_button.gameObject.SetActive(true);
                notepad_button.GetComponentInChildren<AnimateColors>().enabled = true;
                break;
            case "obj_9_the_next":
                notepad_button.GetComponentInChildren<AnimateColors>().enabled = false;
                notepad_button.GetComponentInChildren<AnimateColors>().gameObject.GetComponent<Image>().color = Color.white;
                set_conversation_msg("The next order says - One biggest slice  of Mushroom pizza and smallest size of Mushroom pizza");
                break;
            case "obj_9_here_we_have":
                enable_panel(chefConversationPanel);
                set_conversation_msg("Here we have four mushroom pizzas");
              //  pizza_box.SetActive(true);
                pizzas_on_table.SetActive(true);
                customer1.SetActive(true);
                customer2.SetActive(true);
                uncut_pizza_one.GetComponent<Collider2D>().enabled = false;
                uncut_pizza_two.GetComponent<Collider2D>().enabled = false;
                uncut_pizza_one.transform.parent.GetComponent<AnimateColors>().enabled = false;
                uncut_pizza_two.transform.parent.GetComponent<AnimateColors>().enabled = false;

                break;
            case "obj_9_the_first_pizza_is":
                oneb4.SetActive(true);
                set_conversation_msg("The first pizza is cut into 4 slices so the size of each slice is one-fourth of the whole pizza");
                cut_pizza_one.GetComponent<AnimateColors>().enabled = true;
                break;
            case "obj_9_the_second_pizza_is":
                oneb8.SetActive(true);
                set_conversation_msg("The second pizza is cut into 8 slices, so the each slice of the second pizza is one-eighth of the whole pizza");
                cut_pizza_one.GetComponent<AnimateColors>().enabled = false;
                cut_pizza_one.GetComponent<SpriteRenderer>().color = Color.white;
                cut_pizza_two.GetComponent<AnimateColors>().enabled = true;
                break;

           case "obj_9_if_we_compare":
               
                set_conversation_msg("If we compare the slices of these two pizzas,");
                cut_pizza_one.GetComponent<AnimateColors>().enabled = true;
                cut_pizza_two.GetComponent<AnimateColors>().enabled = true;
                cut_pizza_one.transform.position = desired_pos_pizza.transform.position;
                cut_pizza_two.transform.position = desired_pos_box.transform.position;
                grayed_background.SetActive(true);
                cut_pizza_one.GetComponent<SortingGroup>().sortingOrder = 5;
                cut_pizza_two.GetComponent<SortingGroup>().sortingOrder = 5;

                break;

            case "obj_9_each_is_bigger":
                set_conversation_msg(" we can say that each slice of this pizza is bigger than each slice of this pizza");
                cut_pizza_one.GetComponent<AnimateColors>().enabled = false;
                cut_pizza_two.GetComponent<AnimateColors>().enabled = false;
                cut_pizza_one.GetComponent<SpriteRenderer>().color = Color.white;
                cut_pizza_two.GetComponent<SpriteRenderer>().color = Color.white;
                cut_pizza_one.transform.GetChild(0).transform.localScale = new Vector3(1,1,1);

                break;
            case "obj_9_each_of_this":
                set_conversation_msg(" we can say that each slice of this pizza is bigger than each slice of this pizza");

                cut_pizza_two.transform.GetChild(0).transform.localScale = new Vector3(1, 1, 1);

                break;

         

            case "obj_9_wecan_give_1_slice":
                cut_pizza_one.transform.GetChild(0).transform.localScale = new Vector3(1, 1, 1);
                cut_pizza_two.transform.GetChild(0).transform.localScale = new Vector3(0.6251563f, 0.6251563f, 0.6251563f);
                uncut_pizza_one.transform.parent.GetComponent<AnimateColors>().enabled = false;
                uncut_pizza_two.transform.parent.GetComponent<AnimateColors>().enabled = false;
                uncut_pizza_one.transform.parent.GetComponent<SpriteRenderer>().color = Color.white;
                uncut_pizza_two.transform.parent.GetComponent<SpriteRenderer>().color = Color.white;

               
                set_conversation_msg("We can give 1 slice from this pizza which is \\frac{1}{4} ");
                cut_pizza_one.GetComponent<AnimateColors>().enabled = true;
               


                break;

            case "obj_9_wecan_give_1_slice_18th":
                cut_pizza_one.transform.GetChild(0).transform.localScale = new Vector3(0.6251563f, 0.6251563f, 0.6251563f);
                cut_pizza_two.transform.GetChild(0).transform.localScale = new Vector3(1, 1, 1);
                set_conversation_msg("1 slice from this pizza which is \\frac{1}{8}");
                cut_pizza_two.GetComponent<AnimateColors>().enabled = true;
                cut_pizza_one.GetComponent<AnimateColors>().enabled = false;
                cut_pizza_one.GetComponent<SpriteRenderer>().color = Color.white;



                break;


            case "obj_9_wecan_consider":
                cut_pizza_two.GetComponent<AnimateColors>().enabled = false;
                cut_pizza_two.GetComponent<SpriteRenderer>().color = Color.white; cut_pizza_one.transform.GetChild(0).transform.localScale = new Vector3(0.6251563f, 0.6251563f, 0.6251563f);
                cut_pizza_one.transform.GetChild(0).transform.localScale = new Vector3(0.6251563f, 0.6251563f, 0.6251563f);
                cut_pizza_one.transform.GetChild(0).transform.localScale = new Vector3(1, 1, 1);
                set_conversation_msg("We can consider \\frac{1}{4} as the biggest slice and \\frac{1}{8} as the smallest slice");
                bigger.SetActive(true);
                

                break;

            case "obj_9_wecan_consider18th":
                
                cut_pizza_two.transform.GetChild(0).transform.localScale = new Vector3(1, 1, 1);

                set_conversation_msg("We can consider \\frac{1}{4} as the biggest slice and \\frac{1}{8} as the smallest slice");
                smaller.SetActive(true);
                break;


            case "obj_9_its_possible":
                cut_pizza_one.transform.GetChild(0).transform.localScale = new Vector3(0.6251563f, 0.6251563f, 0.6251563f);
                cut_pizza_two.transform.GetChild(0).transform.localScale = new Vector3(0.6251563f, 0.6251563f, 0.6251563f);
                set_conversation_msg("It is possible that there can be bigger slice than \\frac{1}{4} and a smaller slice than \\frac{1}{8}");
                break;



            case "obj_9_apart_from_these":
                bigger.SetActive(false);
                smaller.SetActive(false);
                cut_pizza_one.transform.position = cut_one_inital;
                cut_pizza_two.transform.position = cut_two_initial;
                cut_pizza_one.GetComponent<SortingGroup>().sortingOrder = 2;
                cut_pizza_two.GetComponent<SortingGroup>().sortingOrder = 2;
                grayed_background.SetActive(false);
                oneb4.SetActive(false);
                oneb8.SetActive(false);
                set_conversation_msg("Apart from these two pizzas there are two more pizzas that have not been cut");

                uncut_pizza_one.transform.parent.GetComponent<AnimateColors>().enabled = true;
                uncut_pizza_two.transform.parent.GetComponent<AnimateColors>().enabled = true;
                break;

            case "obj_9_lets_try":
               
                set_conversation_msg("Let's try to do that with these 2 pizzas");
                uncut_pizza_one.transform.parent.GetComponent<AnimateColors>().enabled = true;
                uncut_pizza_two.transform.parent.GetComponent<AnimateColors>().enabled = true;
                break;

          

            case "obj_9_select_the_pizza":
              
                set_conversation_msg("Select the pizza in order to serve the biggest slice mushroom pizza");
                if (!uncut_one)
                {
                    uncut_pizza_one.transform.parent.GetComponent<AnimateColors>().enabled = true;
                    uncut_pizza_one.GetComponent<Collider2D>().enabled = true;

                }
                else
                {
                    uncut_one = false;
                }
                if (!uncut_two)
                {
                    uncut_pizza_two.transform.parent.GetComponent<AnimateColors>().enabled = true;
                    uncut_pizza_two.GetComponent<Collider2D>().enabled = true;
                }
                else
                {
                    uncut_two = false;
                }
                    break;

            case "obj_9_you_can_cut":
                uncut_pizza_one.transform.parent.GetComponent<AnimateColors>().enabled = false;
                uncut_pizza_two.transform.parent.GetComponent<AnimateColors>().enabled = false;
                uncut_pizza_one.transform.parent.GetComponent<SpriteRenderer>().color = Color.white;
                uncut_pizza_two.transform.parent.GetComponent<SpriteRenderer>().color = Color.white;
                set_conversation_msg(" You can cut the pizzas by selecting the number of slices that you want. Say if you select 5, the pizza will be divided into five equal parts");
                break;

            case "obj_9_you_can_increase":
                set_conversation_msg("You can increase the number of slices with the UP arrow and decrease the number of slices with the DOWN arrow");
                break;

            case "obj_9_cut_this_pizza":
                enable_panel(chefConversationPanel);
               
                set_conversation_msg(" Cut this pizza so that you get the biggest slice of the pizza");
                increase.interactable = true;
                decrease.interactable = true;
                break;

            case "obj_9_now_cut_this_pizza":
                pizza_box.SetActive(false);
                enable_panel(chefConversationPanel);
                increase.interactable = true;
                decrease.interactable = true;
                if (!uncut_one)
                {
                    uncut_pizza_one.transform.parent.GetComponent<AnimateColors>().enabled = true;
                    uncut_pizza_one.GetComponent<Collider2D>().enabled = true;

                }
                else
                {
                    uncut_one = false;
                }
                if (!uncut_two)
                {
                    uncut_pizza_two.transform.parent.GetComponent<AnimateColors>().enabled = true;
                    uncut_pizza_two.GetComponent<Collider2D>().enabled = true;
                }
                else
                {
                    uncut_two = false;
                }
                set_conversation_msg("Now cut this pizza so that you get the smallest slice of the pizza");
                break;

            case "obj_9_lo1_1_when_pizza":
                enable_panel(chefConversationPanel);
                pizza_box.SetActive(false);
                customer2.SetActive(false);
                oneb4.SetActive(true);
                oneb8.SetActive(true);
                blink_pizzas();
                set_conversation_msg("When the pizza was divided into two slices, we got the biggest slice of the pizza");
                uncut_pizza_one.transform.parent.GetComponent<AnimateColors>().enabled = false;
                uncut_pizza_one.transform.parent.GetComponent<SpriteRenderer>().color = Color.white;
                uncut_pizza_two.transform.parent.GetComponent<AnimateColors>().enabled = false;
                uncut_pizza_two.transform.parent.GetComponent<SpriteRenderer>().color = Color.white;
                if (big_slice == "uncut pizza one")
                {
                    uncut_pizza_one.transform.parent.GetComponent<AnimateColors>().enabled = true;
                    uncut_pizza_one.transform.parent.GetComponentInChildren<TEXDraw3D>().text = "Each Slice \n \\frac{1}{10}";
                    uncut_pizza_two.transform.parent.GetComponentInChildren<TEXDraw3D>().text = "Each Slice \n \\frac{1}{2}";
                    uncut_pizza_one.transform.parent.GetChild(2).GetComponent<SpriteRenderer>().enabled = true;
                    uncut_pizza_two.transform.parent.GetChild(2).GetComponent<SpriteRenderer>().enabled = true;

                }


                if (big_slice == "uncut pizza two")
                {
                    uncut_pizza_two.transform.parent.GetComponent<AnimateColors>().enabled = true;
                    uncut_pizza_two.transform.parent.GetComponentInChildren<TEXDraw3D>().text = "Each Slice \n \\frac{1}{10}";
                    uncut_pizza_one.transform.parent.GetComponentInChildren<TEXDraw3D>().text = "Each Slice \n \\frac{1}{2}";
                    uncut_pizza_one.transform.parent.GetChild(2).GetComponent<SpriteRenderer>().enabled = true;
                    uncut_pizza_two.transform.parent.GetChild(2).GetComponent<SpriteRenderer>().enabled = true;
                }
               
                
                break;

            case "obj_9_lo1_1_when_pizza_in_ten":
                set_conversation_msg("When the pizza was divided into ten slices, we got the smallest slice of the pizza");
                uncut_pizza_one.transform.parent.GetComponent<AnimateColors>().enabled = false;
                uncut_pizza_one.transform.parent.GetComponent<SpriteRenderer>().color = Color.white;
                uncut_pizza_two.transform.parent.GetComponent<AnimateColors>().enabled = false;
                uncut_pizza_two.transform.parent.GetComponent<SpriteRenderer>().color = Color.white;
                if (big_slice == "uncut pizza one")
                    uncut_pizza_two.transform.parent.GetComponent<AnimateColors>().enabled = true;


                if (big_slice == "uncut pizza two")
                    uncut_pizza_one.transform.parent.GetComponent<AnimateColors>().enabled = true;
                
                break;

            case "obj_9_lo1_so_changing":
                set_conversation_msg("So changing the number of slices of the pizza changes the size of each slice.");
                uncut_pizza_one.transform.parent.GetComponent<AnimateColors>().enabled = false;
                uncut_pizza_one.transform.parent.GetComponent<SpriteRenderer>().color = Color.white;
                uncut_pizza_two.transform.parent.GetComponent<AnimateColors>().enabled = false;
                uncut_pizza_two.transform.parent.GetComponent<SpriteRenderer>().color = Color.white;
                oneb4.SetActive(false);
                oneb8.SetActive(false);
                uncut_pizza_one.transform.parent.GetChild(2).GetComponent<SpriteRenderer>().enabled = false;
                uncut_pizza_two.transform.parent.GetChild(2).GetComponent<SpriteRenderer>().enabled = false;
                uncut_pizza_one.transform.parent.GetComponentInChildren<TEXDraw3D>().text = "";
                uncut_pizza_two.transform.parent.GetComponentInChildren<TEXDraw3D>().text = "";
                unblink_pizzas();
                break;

            case "obj_9_lo1_so_wecan":
                chefConversationPanel.SetActive(false);
              //  set_conversation_msg("So we can say that");
                pizzas_on_table.SetActive(false);
                pizza_box.SetActive(false);
                break;

            case "obj_9_lo1_lesser_the_number":
                chefConversationPanel.SetActive(false);
               // set_conversation_msg("Lesser the number of parts in an object, the size of each part is bigger.");
                lesser_parts.SetActive(true);

                break;

            case "obj_9_lo1_and_more_the_number":
                //set_conversation_msg("More the number of parts in an object, the size of each part is smaller");
                chefConversationPanel.SetActive(false);
                lesser_parts.SetActive(false);
                more_parts.SetActive(true);
                break;

         

            case "obj_9_ro1_here_are":

                lesser_parts.SetActive(false);
                more_parts.SetActive(false);
                chefConversationPanel.SetActive(false);
                RO_panel.SetActive(true);
                ro_1_ans_panel.SetActive(true);
                ro_2_ans_panel.SetActive(false);

                RO_question.text="Here are 2 pizzas. The first pizza is shared by five friends and the second pizza is shared by seven friends.\n";
                ro_submit_pizza.gameObject.SetActive(false);
                ro1_op_1.transform.GetChild(1).transform.gameObject.GetComponent<Image>().sprite = five_pizza;
                ro1_op_2.transform.GetChild(1).transform.gameObject.GetComponent<Image>().sprite = seven_pizza;


                break;
            case "obj_9_ro1_which_of_the":
                RO_question.text = "Here are 2 pizzas. The first pizza is shared by five friends and the second pizza is shared by seven friends.\nWhich of the group of friends would get a smaller size of the pizza?";

                ro_submit_pizza.gameObject.SetActive(true);
                break;

            case "obj_9_lo2_so_as_the":
                 enable_panel(chefConversationPanel);
                 set_conversation_msg("As the number of parts in a whole increases the size of each part reduces ");
                whole_decreases.SetActive(true);
               
                break;
            case "obj_9_lo2_and_as_the":
               
                set_conversation_msg("The number of parts decreases the size of each part increases.");
                whole_increases.SetActive(true);
                whole_decreases.SetActive(false);
                break;
            case "obj_9_lo2_fraction_with":
                whole_increases.SetActive(false);


                set_conversation_msg("Fractions with larger denominators represent objects, where each part is smaller in size. in comparison to a fraction whose denominator is a smaller number");
                lo2_explain.SetActive(true);
                break;


            case "obj_9_Ro2_the_size":

                disable_panel(chefConversationPanel,chefConversationPanel.GetComponent<Animator>(), 0.5f);
                lo2_explain.SetActive(false);
                RO_panel.SetActive(true);
                ro_1_ans_panel.SetActive(false);
                ro_2_ans_panel.SetActive(true);
                RO_question.text = "The size of each part is bigger in which fraction?";
                ro_submit_pizza.gameObject.SetActive(true);
                ro2_op_1.transform.GetChild(1).transform.gameObject.SetActive(false);
                ro2_op_2.transform.GetChild(1).transform.gameObject.SetActive(false);
                ro2_op_3.transform.GetChild(1).transform.gameObject.SetActive(false);
                ro2_op_4.transform.GetChild(1).transform.gameObject.SetActive(false);
                ro2_op_1.transform.GetChild(3).transform.gameObject.GetComponent<TEXDraw>().text="\\frac{2}{3}";
                ro2_op_2.transform.GetChild(3).transform.gameObject.GetComponent<TEXDraw>().text = "\\frac{3}{10}";
                ro2_op_3.transform.GetChild(3).transform.gameObject.GetComponent<TEXDraw>().text = "\\frac{4}{9}";
                ro2_op_4.transform.GetChild(3).transform.gameObject.GetComponent<TEXDraw>().text = "\\frac{1}{4}";
                break;

        }
    }

    public void blink_pizzas()
    {
        
                tmp1.GetComponent<SpriteRenderer>().enabled = true;
                tmp1.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.2f);
                tmp1.transform.localPosition = Vector3.zero;
                tmp1.transform.localScale = new Vector3(1, 1, 1);


                tmp2.GetComponent<SpriteRenderer>().enabled = true;
                tmp2.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.2f);
                tmp2.transform.localPosition = Vector3.zero;
                tmp2.transform.localScale = new Vector3(1, 1, 1);

    }
    public void unblink_pizzas()
    {
        
               tmp1.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
               tmp1.GetComponent<SpriteRenderer>().enabled = false;
     
              
                tmp2.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                tmp2.GetComponent<SpriteRenderer>().enabled = false;
          
    }



    void validate_answer()
    {
        ro_submit_pizza.gameObject.SetActive(false);
        if (ro_1_ans_panel.activeSelf)
        {
            if (ans.Equals("RO op 2"))
            {
                playCorrect();
                RO_panel.SetActive(false);
                FindObjectOfType<timeline_new>().load_next();
            }
            else if (ans.Equals("RO op 1"))
            {
                playError();
                StartCoroutine(select_wrong_answer_ro1("The number of parts in an object determines the size of each part of the object. More number of parts means that each part is smaller in comparison to an object which has less number of parts. The size of the slices received by group of 5 is bigger than the size of the slices received by group of 7."));
            }
            else
            {
                playError();
                show_prompt("Please select any answer");
                reset_options();
                ro_submit_pizza.gameObject.SetActive(true);

            }
        }
        else if (ro_2_ans_panel.activeSelf)
        {
            if (ans.Equals("RO2 op 1"))
            {
                playCorrect();
                RO_panel.SetActive(false);
                GameObject.FindObjectOfType<GameManager>().OnGameOver();
                //Exit_Panel.SetActive(true);

            }
            else if (ans.Equals("RO2 op 2")|| ans.Equals("RO2 op 3")|| ans.Equals("RO2 op 4"))
            {
                playError();
                StartCoroutine(select_wrong_answer_ro2("The size of each part of a fraction depends on the number of parts. If the number of parts are more, the size of each part will be smaller. Whereas, the size of each part will be bigger if the number of parts in the objects are lesser. "));
            }
            else
            {
                playError();
                show_prompt("Please select any answer");
                reset_options();
                ro_submit_pizza.gameObject.SetActive(true);

            }
        }
        ans = "";
    }

    void reset_options()
    {
        ro1_op_1.transform.GetChild(2).gameObject.SetActive(false);
        ro1_op_2.transform.GetChild(2).gameObject.SetActive(false);
        ro2_op_1.transform.GetChild(2).gameObject.SetActive(false);
        ro2_op_2.transform.GetChild(2).gameObject.SetActive(false);
        ro2_op_3.transform.GetChild(2).gameObject.SetActive(false);
        ro2_op_4.transform.GetChild(2).gameObject.SetActive(false);
        temp.GetComponent<Image>().color = Color.white;
        ans = "";
        
    }

    IEnumerator select_wrong_answer_ro1(string prompt)
    {
        dialougue_ok_button.onClick.RemoveAllListeners();
        dialougue_ok_button.onClick.AddListener(dialougue_ok_button_function);
        condition = "RO_1";
        Dialouge_text.GetComponent<TEXDraw>().text = prompt;
        temp.GetComponent<Image>().color = Color.red;
        FindObjectOfType<timeline_new>().playAudioOnRelearn("obj9_let_see_why_common.wav");
        yield return new WaitForSeconds(0.6f);
        yield return new WaitForSeconds(FindObjectOfType<timeline_new>().length_of_audio+2);
        

        Dialouge_panel.SetActive(true);
        ro_1_dialougue.SetActive(true);
        FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_9_ro_wrong.wav");
        yield return new WaitForSeconds(0.6f);

        if (!Application.isEditor)
            yield return new WaitForSeconds(FindObjectOfType<timeline_new>().length_of_audio);
        else
            yield return new WaitForSeconds(4);


     //  Dialouge_panel.SetActive(false);
        reset_options();
     //   ro_submit_pizza.gameObject.SetActive(true);
     //   dialougue_ok_button.onClick.RemoveAllListeners();

    }

    IEnumerator select_wrong_answer_ro2(string prompt)
    {
        dialougue_ok_button.onClick.RemoveAllListeners();
        dialougue_ok_button_ro2.onClick.AddListener(dialougue_ok_button_function);
        condition = "RO_2";
        Dialouge_text_ro2.GetComponent<TEXDraw>().text = "";
        temp.GetComponent<Image>().color = Color.red;
        FindObjectOfType<timeline_new>().playAudioOnRelearn("obj9_let_see_why_common.wav");
        yield return new WaitForSeconds(0.6f);
        yield return new WaitForSeconds(FindObjectOfType<timeline_new>().length_of_audio + 2);
        Dialouge_panel_ro2.SetActive(true);
        
           

        FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_9_ro2_wrong1.wav");
        yield return new WaitForSeconds(0.6f);

        if (!Application.isEditor)
            yield return new WaitForSeconds(FindObjectOfType<timeline_new>().length_of_audio);
        else
            yield return new WaitForSeconds(4);

        Dialouge_panel_ro2.GetComponentInChildren<AnimateColors>().enabled = true;
        Dialouge_text_ro2.GetComponent<TEXDraw>().text = " As you can see, \\frac{1}{3} is bigger in size than the others. This is because there are lesser parts in this object ";
        FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_9_ro2_wrong_as_you.wav");
        yield return new WaitForSeconds(0.6f);

        if (!Application.isEditor)
            yield return new WaitForSeconds(FindObjectOfType<timeline_new>().length_of_audio);
        else
            yield return new WaitForSeconds(4);


       
        

       


      // Dialouge_panel_ro2.SetActive(false);
        reset_options();
      //  ro_submit_pizza.gameObject.SetActive(true);
     //   dialougue_ok_button_ro2.onClick.RemoveAllListeners();
    }

    public void dialougue_ok_button_function()
    {
        StopAllCoroutines();
        FindObjectOfType<timeline_new>().lipsync_player.GetComponent<AudioSource>().Stop();
        switch (condition)
        {
            case "RO_1":
                Dialouge_panel.SetActive(false);
                ro_1_dialougue.SetActive(false);
                reset_options();
                ro_submit_pizza.gameObject.SetActive(true);
                FindObjectOfType<timeline_new>().load_next();
                RO_panel.SetActive(false);

                break;
            case "RO_2":
                Dialouge_panel_ro2.GetComponentInChildren<AnimateColors>().enabled = false;
                Dialouge_panel_ro2.GetComponentInChildren<AnimateColors>().gameObject.GetComponent<Image>().color = Color.white;
                Dialouge_panel_ro2.SetActive(false);
                reset_options();
                ro_submit_pizza.gameObject.SetActive(true);
                RO_panel.SetActive(false);
                GameObject.FindObjectOfType<GameManager>().OnGameOver();
                //Exit_Panel.SetActive(true);
                //RO_panel.SetActive(false);
                break;
        }
        dialougue_ok_button.onClick.RemoveAllListeners();
        


    }
    void quit_app()
    {
        Application.Quit();
    }
    public void playError()
    {
        GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
    }
    public void playCorrect()
    {
        GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           