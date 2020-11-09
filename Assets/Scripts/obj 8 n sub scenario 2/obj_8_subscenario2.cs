using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TexDrawLib;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.SceneManagement;

public class obj_8_subscenario2 : MonoBehaviour
{
  //  public static obj_8_subscenario2 Instance;
    UnityEvent u_Event = new UnityEvent();


    // Start is called before the first frame update
    string jsonFileName = "Obj_8_Pizza.json";
    public GameObject LoadingAudio;
    public TEXDraw converstationMsgText;
    public GameObject chefCOnversationPanel,top_layer_objects,pizza_in_the_box, mushroom_veg_noveg,four_mushroom,four_veg,four_nonveg, notepad_page, notepad_text,customer,pizza_box;
    public GameObject LO1, unequal_explaination, unequal_pizza, not_equal_1b4, you_compare_each, not_fractions,the_parts_are_not,equal_explaination, Pizza_EqualCut_explaination,equal_sign_explain,equal_highlight,unequal_highlight,RO_panel,ROQuestion,RO_op1,RO_op2,RO_op3,RO_op4, RO_Submit_button;
    public Button notepad_button, notepad_ok_button,dialougue_ok,last_dialougue_ok;

    public DragSprite_Pizza[] draggable_object;
    string ans;
    public Sprite op1_sprite, op2_sprite, op3_sprite, op4_sprite;
    GameObject obj3_ro_prompt;
    public GameObject errorMsg, errorMsgText, exit_panel,temp;
    public Button exit_button;


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
        ans = "";
        GetallNumberButtons();
        exit_panel = GameObject.Find("Exit Panel");
        exit_button = GameObject.Find("Exit_button").GetComponent<Button>();
        LoadingAudio = GameObject.Find("LoadAudio");

        if (UtilityArtifacts.backTraversal)
        {
            Text textLoadingText = LoadingAudio.transform.GetChild(2).GetComponent<Text>();
            textLoadingText.text = "Let us understand this better";
        }

        chefCOnversationPanel = GameObject.Find("Chef conversation").gameObject;
        chefCOnversationPanel.SetActive(true);
        converstationMsgText = GameObject.Find("ConversationText").GetComponent<TEXDraw>();
        top_layer_objects = GameObject.Find("Top_Layer_Objects");
        pizza_in_the_box= GameObject.Find("PizzaSliceInTheBox");
        mushroom_veg_noveg = GameObject.Find("MUSHROOM_VEG_NONVEG");
        four_veg = GameObject.Find("four veg pizza");
        four_mushroom = GameObject.Find("four mushroom pizza");
        four_nonveg = GameObject.Find("four non veg pizza");
        customer = GameObject.Find("customer");
        notepad_page = GameObject.Find("notepad_page");
        notepad_text = GameObject.Find("notepad_text");
        notepad_ok_button = GameObject.Find("notepad_ok").GetComponent<Button>();
        notepad_button = GameObject.Find("notepad_button").GetComponent<Button>();
        pizza_box = GameObject.Find("PizzaBox");

        LO1 = GameObject.Find("LO 1");
        unequal_explaination = GameObject.Find("unequal_explaination");
        unequal_pizza = GameObject.Find("unequal_pizza");
        not_equal_1b4 = GameObject.Find("not_equal_1b4");
        you_compare_each = GameObject.Find("you compare each");
        not_fractions = GameObject.Find("not_fractions");
        the_parts_are_not = GameObject.Find("the_parts_not_equal");
        Pizza_EqualCut_explaination = GameObject.Find("Pizza_EqualCut_explaination");
        equal_explaination = GameObject.Find("Equal_explaination");
        equal_sign_explain = GameObject.Find("equal_sign_equal_exp");
        equal_highlight = GameObject.Find("PizzaEqualhighlight");
        unequal_highlight = GameObject.Find("PizzaUnequalHighlight");

        RO_panel = GameObject.Find("RO Panel");
        ROQuestion = GameObject.Find("ROQuestion");
        RO_op1 = GameObject.Find("RO op 1");
        RO_op2 = GameObject.Find("RO op 2");
        RO_op3 = GameObject.Find("RO op 3");
        RO_op4 = GameObject.Find("RO op 4");
        RO_Submit_button = GameObject.Find("RO_Submit_button");


        errorMsg = GameObject.Find("ResponseMsgPanel");
        errorMsgText = GameObject.Find("response_msg_text");


        dialougue_ok = GameObject.Find("dialougue_ok").GetComponent<Button>();
        last_dialougue_ok = GameObject.Find("last_dialougue_ok").GetComponent<Button>();

        dialougue_ok.onClick.AddListener(() =>dialougue_ok_definition());
        last_dialougue_ok.onClick.AddListener(()=>last_dialougue_ok_definition());



        notepad_button.onClick.AddListener(() => enable_notepad());
        notepad_ok_button.onClick.AddListener(() => disable_notepad());
        RO_Submit_button.GetComponentInChildren<Button>().onClick.AddListener(()=>validate_ro_answer());
        exit_button.onClick.AddListener(quit_app);


        draggable_object = FindObjectsOfType<DragSprite_Pizza>();
        foreach (DragSprite_Pizza pizzaSlice in draggable_object)
        {
            pizzaSlice.enabled = false;
        }


        pizza_in_the_box.SetActive(false);
        chefCOnversationPanel.SetActive(false);
        mushroom_veg_noveg.SetActive(false);
        four_veg.SetActive(false);
        four_nonveg.SetActive(false);
        four_mushroom.SetActive(false);
        notepad_button.gameObject.SetActive(false);
        customer.SetActive(false);
        top_layer_objects.SetActive(false);

        unequal_pizza.SetActive(false);
        not_fractions.SetActive(false);
        not_equal_1b4.SetActive(false);
        you_compare_each.SetActive(false);
        the_parts_are_not.SetActive(false);
        unequal_explaination.SetActive(false);
        equal_highlight.SetActive(false);
        unequal_highlight.SetActive(false);
        Pizza_EqualCut_explaination.SetActive(false);
        equal_sign_explain.SetActive(false);
        equal_explaination.SetActive(false);

        LO1.SetActive(false);

        RO_panel.SetActive(false);
        errorMsg.SetActive(false);

        obj3_ro_prompt = GameObject.Find("obj3_ro_prompt");
        obj3_ro_prompt.SetActive(false);

        exit_panel.SetActive(false);



    }

    void quit_app()
    {
        Application.Quit();
    }
    void audio_invoke()
    {
        Debug.Log(EVENT.SetUpTimeLine);
        EventManager.setNameOfJasonFile(jsonFileName);
        EventManager.Broadcast(EVENT.SetUpTimeLine);
    }



    void HideLoadingAudio()
    {
        LoadingAudio.SetActive(false);
    }
    void EventToHandle(string EventName)
    {
        HideLoadingAudio();
        switch (EventName)
        {

            case "sub_scen_2_Once_the_user":
                break;

            case "sub_scen_2_stall_sells":

                break;

            case "sub_scen_2_MUSHROOM_VEG_NONVEG":
                enable_panel(mushroom_veg_noveg);
                break;

            case "sub_scen_2_while_you_had":
                mushroom_veg_noveg.SetActive(false);
                converstationMsgText.text = "While you had gone some customers were there at the stall. These customers have placed the orders already and will pick later.";
                enable_panel(chefCOnversationPanel);
                break;

         

            case "sub_scen_2_following_are":
                converstationMsgText.text = " Following are the pizza's that are ready ";
                break;

            case "sub_scen_2_four_mushroom":
                converstationMsgText.text = "4 Mushroom Pizza ";
                enable_panel(four_mushroom);
                break;
            case "sub_scen_2_four_veg":
                converstationMsgText.text = "4 Veg Pizza ";
                disable_panel(four_mushroom, 0.5f);
                enable_panel(four_veg);
                break;
            case "sub_scen_2_four_non_veg":
                converstationMsgText.text = "4 Nonveg Pizza ";
                disable_panel(four_veg,0.5f);
                enable_panel(four_nonveg);

                break;




            case "sub_scen_2_Few_customers_were":
                converstationMsgText.text = "Few customers were at the shop a while back as I was setting up the stall and you were collecting apples.";
                enable_panel(chefCOnversationPanel);
                break;
            case "sub_scen_2_you_has_been":
                converstationMsgText.text = "you has been assigned a task to pack these orders and keep it ready so the customers can come and pick it up";
                break;



            case "sub_scen_2_Since_the_pizzas":
                converstationMsgText.text = "Since the pizzas were still in oven, these customers placed their orders and left";
                break;


            case "sub_scen_2_They_will_be_back":
                converstationMsgText.text = "They will be back shortly to pick up their orders.";
                break;

            case "sub_scen_2_LOOKS_LIKE":
                GameObject.Find("ovensound").GetComponent<AudioSource>().Play();
                converstationMsgText.text = "    Looks like the pizzas are ready. Lets keep these pizzas ready for pick up";
                disable_panel(four_nonveg, 0.5f);

                break;
            case "sub_scen_2_this_is_list":
                enable_panel(chefCOnversationPanel);
                converstationMsgText.text = "Here is an order.  Let's start packing.";
                 break;


            case "sub_scen_2_You has to read":
           
                converstationMsgText.text = "You need to read the order and select the right pizza and pack it in these boxes";
                break;

            case "Pack_VegPizza_1_4th":
                enable_panel(chefCOnversationPanel);
                converstationMsgText.text = "Click on the notepad to see the order";
                notepad_button.gameObject.SetActive(true);
                notepad_button.gameObject.GetComponentInChildren<AnimateColors>().enabled = true;
                notepad_ok_button.interactable = false;
                break;

            case "The_first_order_says":
              //  converstationMsgText.text = "The first order says: 1/4 of a veg pizza";
                break;

            case "obj_8_We_have_two_veg":
                Invoke("setup_table_for_activity", 0.5f);
                customer.SetActive(true);
                enable_panel(chefCOnversationPanel);
                converstationMsgText.text = "We have two veg pizzas.Click on the pizza from which you can pack \\frac{1}{4} of the pizza.";
                break;

            case "obj_8_select_the_right":
                enable_panel(chefCOnversationPanel);
                setup_table_for_activity();
                converstationMsgText.text = "Select the right pizza to pack \\frac{1}{4} of a pizza in the box. Click on the pizza and drag it to the box.";
                foreach (DragSprite_Pizza pizzaSlice in draggable_object)
                {
                    pizzaSlice.enabled = true;
                }
                break;

            case "obj_8_We_have_to_pack":
                enable_panel(chefCOnversationPanel);
                converstationMsgText.text = "We had to pack one-fourth of the pizza. This pizza is divided into equal slices.";
                equal_highlight.SetActive(true);
                unequal_highlight.SetActive(true);
                var d = equal_highlight.GetComponentsInChildren<AnimateColors>();
                foreach (AnimateColors a in d)
                {
                    a.enabled = true;
                }
                add_extra_time(2.0F);
                break;

            case "obj_8_while_the_other":
                converstationMsgText.text = "While the other pizza has slices which are not equal in size.";
                var d1 = equal_highlight.GetComponentsInChildren<AnimateColors>();
                foreach (AnimateColors a in d1)
                {
                    a.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                    a.enabled = false;
                    
                }
              
               
                var d2 = unequal_highlight.GetComponentsInChildren<AnimateColors>();
                foreach (AnimateColors a in d2)
                {
                    a.enabled = true;
                }
                add_extra_time(2.0F);
                break;

            case "obj_8_lo1_lets_take_same":
                converstationMsgText.text = "Let's take the same example and understand it better.";
                enable_panel(chefCOnversationPanel);
                var d3 = unequal_highlight.GetComponentsInChildren<AnimateColors>();
                foreach (AnimateColors a in d3)
                {
                    a.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                    a.enabled = false;
                }

                unequal_highlight.SetActive(false);
                equal_highlight.SetActive(false);
                break;

            case "obj_8_lo1_what_is1b4":
               
                enable_panel(chefCOnversationPanel);
                converstationMsgText.text = "What is \\frac{1}{4} ?";
                break;

            case "obj_8_lo1_taking_the_part":
                
                equal_highlight.SetActive(true);
                equal_highlight.transform.position = Vector3.zero;
                converstationMsgText.text = "Taking one part from an object that has 4 parts where each part is equal in size. All parts are \\frac{1}{4} in this object";
                add_extra_time(2.0F);
                Invoke("lift_one",2);
                Invoke("lift_all", 7);


                break;


            case "obj_8_lo1_say_you_pack":
                unlift_all();
                equal_highlight.SetActive(false);
                
                converstationMsgText.text = "Say, you pack 1 slice from 4 slices";
                LO1.SetActive(true);
                unequal_explaination.SetActive(true);
                unequal_pizza.SetActive(true);

                break;

            case "obj_8_lo1_it_cannot_mean":
                converstationMsgText.text = "It cannot mean that you packed \\frac{1}{4} of the pizza";
                not_equal_1b4.SetActive(true);
                add_extra_time(2.0F);

                break;

            case "obj_8_lo1_if_you_compare":
                converstationMsgText.text = "If you compare each part, all are different in size and shape ";
                unequal_pizza.SetActive(false);
                not_equal_1b4.SetActive(false);
                you_compare_each.SetActive(true);
                add_extra_time(2.0F);   
                break;

            case "obj_8_lo1_so_the_slices_in_this":
                converstationMsgText.text = "So, the slices in this pizza cannot be expressed as a fraction";
                add_extra_time(2.0F);
                you_compare_each.GetComponent<Animator>().Play("back to plate");
                // not_fractions.SetActive(true);
                break;

            case "obj_8_bcause_the_parts":
                converstationMsgText.text = "The parts are not equal in size and shape";
                //the_parts_are_not.SetActive(true);
                add_extra_time(2.0F);
                you_compare_each.GetComponent<Animator>().Play("to center");
                //  not_fractions.SetActive(false);


                break;

            case "obj_8_lo1_to_express":
                converstationMsgText.text = "To express any part of an object, in terms of fraction";
                you_compare_each.SetActive(false);
                not_fractions.SetActive(false);
                the_parts_are_not.SetActive(false);
                unequal_pizza.SetActive(false);
                equal_explaination.SetActive(true);
                  Pizza_EqualCut_explaination.SetActive(true);
               // Pizza_EqualCut_explaination.SetActive(false);

                break;

            case "obj_8_lo1_we_must_keep":
                converstationMsgText.text = "We must keep in mind that the whole is divided into equal parts";
                Pizza_EqualCut_explaination.SetActive(false);
                equal_sign_explain.SetActive(true);
                add_extra_time(2.0F);

                break;

            case "obj_8_lo2_since_fraction":
                converstationMsgText.text = "Since fractions are like division, all parts in an object should be equal to be represented as a fraction.";

                equal_sign_explain.transform.GetChild(0).GetComponent<Animator>().Play("lift  new");
                
                    add_extra_time(2.0F);
                break;
            case "obj_8_Ro2_which_of_the":
                equal_sign_explain.SetActive(false);
                Pizza_EqualCut_explaination.SetActive(false);
                equal_explaination.SetActive(false);
                //  converstationMsgText.text = "Since fractions are like division, all parts in an object should be equal to be represented as a fraction.";
                disable_panel(chefCOnversationPanel,0.5f);

                RO_panel.SetActive(true);
                ROQuestion.GetComponent<TEXDraw>().text = "Which of the following can be represented as a fraction?";
                RO_op1.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = op1_sprite;
                RO_op2.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = op2_sprite;
                RO_op3.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = op3_sprite;
                RO_op4.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = op4_sprite;

                break;

        }

    }



    
    void add_extra_time(float TIME)
    {
        PlayableDirector playableDirector;
        playableDirector = FindObjectOfType<timeline_new>().director;
        playableDirector.Pause();
        var timelineAsset = playableDirector.playableAsset as TimelineAsset;
        foreach (var track in timelineAsset.GetOutputTracks())
        {
            var animTrack = track as AnimationTrack;
            if (animTrack != null)
            {
                foreach (var clips in animTrack.GetClips())
                {
                    clips.start = clips.start + TIME;
                }
            }
        }
        playableDirector.RebuildGraph();
        playableDirector.Evaluate();
        playableDirector.Play();
    }

    public void lift_one()
    {
        equal_highlight.transform.GetChild(0).GetChild(0).transform.localScale = new Vector3(1,1,1);
    }

    void lift_all()
    {
        for (int i = 0; i < equal_highlight.transform.GetChild(0).childCount; i++)
        {
            equal_highlight.transform.GetChild(0).GetChild(i).transform.localScale = new Vector3(1, 1, 1);
            equal_highlight.transform.GetChild(0).GetChild(i).transform.GetChild(0).gameObject.SetActive(true);
        }

     
    }

    public void unlift_all()
    {
        for (int i = 0; i < equal_highlight.transform.GetChild(0).childCount; i++)
        {
            equal_highlight.transform.GetChild(0).GetChild(i).transform.localScale = new Vector3(.7f, .7f, .7f);
            equal_highlight.transform.GetChild(0).GetChild(i).transform.GetChild(0).gameObject.SetActive(false);
        }
       
    }
    public void enable_panel(GameObject object_to_enable)
    {
        Animator animator_of_object = object_to_enable.GetComponent<Animator>();
        object_to_enable.SetActive(true);
        animator_of_object.Play("enable", 0);
    }

    public void disable_panel(GameObject object_to_enable,  float time)
    {
        Animator animator_of_object = object_to_enable.GetComponent<Animator>();
        Coroutine a = StartCoroutine(disable_after(object_to_enable, animator_of_object, time));
    }

    IEnumerator disable_after(GameObject object_to_enable, Animator animator_of_object, float time)
    {
        animator_of_object.Play("disable", 0);
        yield return new WaitForSeconds(time);
        object_to_enable.SetActive(false);

    }


    void enable_notepad()
    {
        
        notepad_button.gameObject.GetComponentInChildren<AnimateColors>().enabled = false;
        notepad_button.gameObject.GetComponentInChildren<AnimateColors>().gameObject.GetComponent<Image>().color = Color.white;
        FindObjectOfType<timeline_new>().load_next();
        disable_panel(chefCOnversationPanel, 1.0f);
        notepad_text.GetComponent<TEXDraw>().text = "\\frac{1}{4}  of a veg pizza";
        enable_panel(notepad_page);
        notepad_button.interactable = false;
        notepad_ok_button.interactable = true;
       

    }
    void setup_table_for_activity()
    {
        top_layer_objects.SetActive(true);
    }

    void disable_notepad()
    {
        notepad_ok_button.interactable = false;
       
        disable_panel(notepad_page, 1f);
        FindObjectOfType<timeline_new>().load_next();
       // converstationMsgText.text = "select the right pizza slice and pack it";
      //  FindObjectOfType<timeline_new>().playAudioOnRelearn("select the right pizza.wav");
       // enable_panel(chefCOnversationPanel);
        


    }

    void OnPreRequisitOver()
    {
        NewIncomingMsg mg = UtilityArtifacts.NJMsg;
        mg.current_activity_status = 2;
        mg.pre_req_id = 0;
        mg.pre_req_status = 1;//1
        mg.pre_req_reqData.error_obj_id = 0;
        UtilityArtifacts.UnityMsg = UtilityArtifacts.outGoingMessage(mg, "false");
        SceneManager.LoadScene(UtilityArtifacts.NJLoadScene);
    }

    void validate_ro_answer()
    {
        RO_Submit_button.SetActive(false);
        if (ans.Equals("RO op 1"))
        {
           playCorrect();
            RO_panel.SetActive(false);
            GameObject.Find("Fade").GetComponent<Animator>().enabled = false;
            GameObject.Find("Fade").GetComponent<Animator>().enabled = true;
            Invoke("animStop", 2.5f);
            //exit_panel.SetActive(true);
            if (UtilityArtifacts.loading_pos == "Obj8_Lo1_from_obj10")
            {
                UtilityArtifacts.loading_pos = "";
                UtilityArtifacts.coming_back_from = "to_Obj10_quest1";
                UtilityArtifacts.backTraversal = false;
                UtilityArtifacts.comingbackafterTraversal = true;
                // load traversescene 10
                OnPreRequisitOver();
            }
            else
            {
                GameObject.FindObjectOfType<GameManager>().OnGameOver();
            }
           
           

        }
        else if (ans.Equals("RO op 2") || ans.Equals("RO op 3") || ans.Equals("RO op 4"))
        {
            RO_Submit_button.SetActive(false);
            playError();
          
            StartCoroutine(start_RO_diagnose());
                
          

        }
        else
        {
            RO_Submit_button.SetActive(false);
            playError();
            errorMsg.SetActive(true);
            errorMsgText.GetComponent<Text>().text = "Please select answer and try again";
          //  Invoke("hideerror_msg", 5f);
          //  Invoke("reset_option", 5f);
        }
    }

    void animStop()
    {
        GameObject.Find("Fade").GetComponent<Animator>().enabled = false;
    }
    void hideerror_msg()
    {
        obj3_ro_prompt.SetActive(false);
        errorMsg.SetActive(false);
    }

    void show_exit()
    {
        RO_panel.SetActive(false);
        exit_panel.SetActive(true);
        GameObject.Find("Fade").GetComponent<Animator>().enabled = false;
        GameObject.Find("Fade").GetComponent<Animator>().enabled = true;
        Invoke("animStop", 2.5f);
    }

    void reset_option()
    {
        foreach (GameObject b in GameObject.FindGameObjectsWithTag("Numbers"))
        {
            b.transform.GetChild(2).gameObject.SetActive(false);
        }
        ans = "";
        RO_Submit_button.SetActive(true);
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

        ans = currentSelectedGameObject.name;
        foreach (GameObject b in GameObject.FindGameObjectsWithTag("Numbers"))
        {
            b.transform.GetChild(2).gameObject.SetActive(false);
        }
        currentSelectedGameObject.transform.GetChild(2).gameObject.SetActive(true);

        temp = currentSelectedGameObject;
    }

    public void playError()
    {
        GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
    }
    public void playCorrect()
    {
        GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
    }


    public IEnumerator start_RO_diagnose()
    {
        temp.GetComponent<Image>().color = Color.red;
        FindObjectOfType<timeline_new>().playAudioOnRelearn("obj8_let_see_why_common.wav");
        yield return new WaitForSeconds(0.6f);
        yield return new WaitForSeconds(FindObjectOfType<timeline_new>().length_of_audio+2);

        obj3_ro_prompt.SetActive(true);
        temp.GetComponent<Image>().color = Color.white;
        FindObjectOfType<timeline_new>().playAudioOnRelearn("RO2_division_is_splitting.wav");
        yield return new WaitForSeconds(0.6f);
        yield return new WaitForSeconds(FindObjectOfType<timeline_new>().length_of_audio);

        FindObjectOfType<timeline_new>().playAudioOnRelearn("RO2_since_fractions_are.wav");
        yield return new WaitForSeconds(0.6f);
        yield return new WaitForSeconds(FindObjectOfType<timeline_new>().length_of_audio);

        FindObjectOfType<timeline_new>().playAudioOnRelearn("RO2_therefore_the_circle.wav");
        yield return new WaitForSeconds(0.6f);
        yield return new WaitForSeconds(FindObjectOfType<timeline_new>().length_of_audio);

        FindObjectOfType<timeline_new>().playAudioOnRelearn("RO2_theparts.wav");
        yield return new WaitForSeconds(0.6f);
        yield return new WaitForSeconds(FindObjectOfType<timeline_new>().length_of_audio);

      //  hideerror_msg();
      //  reset_option();
     //   show_exit();
        yield return new WaitForSeconds(0f);

    }



    void dialougue_ok_definition()
    {
        if (errorMsg.activeSelf)
        {
            errorMsgText.GetComponent<Text>().text = "";
            errorMsg.SetActive(false);

            top_layer_objects.SetActive(false);
            
            pizza_box.SetActive(false);
            pizza_in_the_box.SetActive(false);
            customer.SetActive(false);
            StopAllCoroutines();
            //FindObjectOfType<timeline_new>().count = 13;
            FindObjectOfType<timeline_new>().load_next();
            
        }
    }

    void last_dialougue_ok_definition()
    {
        StopAllCoroutines();
        hideerror_msg();
        reset_option();
        if (UtilityArtifacts.loading_pos == "Obj8_Lo1_from_obj10")
        {
            //UtilityArtifacts.loading_pos = "";
            UtilityArtifacts.coming_back_from = "to_Obj10_quest2";
            UtilityArtifacts.backTraversal = false;
            UtilityArtifacts.comingbackafterTraversal = true;
            UtilityArtifacts.loadStartingpoint = 0;
            UtilityArtifacts.loadEndingpoint = 0;
            UtilityArtifacts.loadStartingpointforcomingback = 5;
            // load traversescene 10
            //SceneManager.LoadScene("Obj10");
            OnPreRequisitOver();
        }
        else
        {
            show_exit();
            FindObjectOfType<timeline_new>().lipsync_player.GetComponent<AudioSource>().Stop();
        }
       


    }
}
