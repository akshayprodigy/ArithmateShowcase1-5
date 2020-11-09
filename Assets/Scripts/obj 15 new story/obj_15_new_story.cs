using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class obj_15_new_story : MonoBehaviour
{
    string jsonFileName = "obj_15_new_story.json";
    public GameObject Dialouge_text, Dialouge_panel, chefConversationPanel, chef_ConversationText, notepad_page, notepad_text, TableAssets, pizza_box_top, pizza_box_bottom, pizza_box, compare_plates, two_or_more, number_line_panel, number_line_1, number_line_2, number_line_3, Obj15_RO_1, Obj15_RO_3, abstraction_panel, Exit_Panel, customer1, customer2, customer3, order_1, order_2, order_3, reinforcement_RO1_Panel;
    public Button dialougue_ok_button, notepad_button, notepad_ok_button, activity_submit, Submit_WrongTray, ok_btn_Reinforcement_RO1;
    public GameObject textAfterOrder_1, textAfterOrder_2, textAfterOrder2_1, textAfterOrder_3, textAfterOrder_5, Plotted_NumberLines;
    public GameObject cookiePlate1_1, cookiePlate1_2, cookiePlate2_1, cookiePlate2_2, cookiePlate2_3, cookiePlate2_4, cookiePlate3_1, cookiePlate3_2,
        cookiePlate3_3, cookiePlate3_4, cookiePlate3_5, cookiePlate3_6, cookiePlate3_7, cookiePlate3_8, loadingAudio;
    public Vector3 pos_cookiePlate1_1, pos_cookiePlate1_2, pos_cookiePlate2_1, pos_cookiePlate2_2, pos_cookiePlate2_3, pos_cookiePlate2_4,
        pos_cookiePlate3_1, pos_cookiePlate3_2, pos_cookiePlate3_3, pos_cookiePlate3_4, pos_cookiePlate3_5, pos_cookiePlate3_6, pos_cookiePlate3_7,
        pos_cookiePlate3_8;

    public int cookie_count, hint_count;
    public drag_drop_obj_15[] draggable_slice;
    public Obj15_RO_Manager Obj15_RO_Manager;
    public string current_slice = "";
    public Button exit_button;
    public GameObject LoadingAudio;
    public AudioSource startAudio;

    private void OnEnable()
    {
        timeline_new.OnEventCalled += EventToHandle;
    }

    private void OnDisable()
    {
        timeline_new.OnEventCalled -= EventToHandle;
    }

    void Start()
    {

        Initialization();
        Invoke("audio_invoke", 2.0f);
    }

    void Initialization()
    {
        startAudio = GetComponent<AudioSource>();
        startAudio.Play();

        loadingAudio = GameObject.Find("LoadAudio");
        LoadingAudio = GameObject.Find("LoadAudio");

        if (UtilityArtifacts.backTraversal)
        {
            Text textLoadingText = LoadingAudio.transform.GetChild(0).GetComponent<Text>();
            textLoadingText.text = "Let us understand this better";
        }
        TableAssets = GameObject.Find("TableAssets");
        CookiesInitially_Find();
        Dialouge_text = GameObject.Find("Dialougue text");
        Dialouge_panel = GameObject.Find("Dialougue Panel");
        dialougue_ok_button = GameObject.Find("dialougue_ok").GetComponent<Button>();
        chefConversationPanel = GameObject.Find("Chef conversation");
        chef_ConversationText = GameObject.Find("chef ConversationText");
        notepad_page = GameObject.Find("notepad_page");
        notepad_text = GameObject.Find("notepad_text");
        notepad_ok_button = GameObject.Find("notepad_ok").GetComponent<Button>();
        notepad_button = GameObject.Find("notepad_button").GetComponent<Button>();
        pizza_box_bottom = GameObject.Find("box_bottom_pos");
        pizza_box_top = GameObject.Find("box_top_pos");
        activity_submit = GameObject.Find("Activity_submit").GetComponent<Button>();
        Submit_WrongTray = GameObject.Find("Submit_WrongTray").GetComponent<Button>();
        pizza_box = GameObject.Find("PIZZA BOX");
        compare_plates = GameObject.Find("compare plates");
        two_or_more = GameObject.Find("two or more");
        number_line_panel = GameObject.Find("NumberLine");
        number_line_1 = GameObject.Find("Number_line_1");
        number_line_2 = GameObject.Find("Number_line_2");
        number_line_3 = GameObject.Find("Number_line_3");
        Obj15_RO_1 = GameObject.Find("Obj15_RO_1");
        Obj15_RO_3 = GameObject.Find("Obj15_RO_3");
        abstraction_panel = GameObject.Find("abstraction_panel");
        Exit_Panel = GameObject.Find("Exit Panel");
        exit_button = GameObject.Find("Exit_button").GetComponent<Button>();
        customer1 = GameObject.Find("customer1");
        customer2 = GameObject.Find("customer2");
        customer3 = GameObject.Find("customer3");
        order_1 = GameObject.Find("Order_1");
        order_2 = GameObject.Find("Order_2");
        order_3 = GameObject.Find("Order_3");
        reinforcement_RO1_Panel = GameObject.Find("Canvas/Obj15_RO_1/Reinforncement_RO1").gameObject;
        ok_btn_Reinforcement_RO1 = GameObject.Find("Canvas/Obj15_RO_1/Reinforncement_RO1/Ok_Btn").GetComponent<Button>();
        textAfterOrder_1 = GameObject.Find("Canvas/Text_AfterOrder_1").gameObject;
        textAfterOrder_2 = GameObject.Find("Canvas/Text_AfterOrder_1/FractionEqual").gameObject;
        textAfterOrder2_1 = GameObject.Find("Canvas/Text_AfterOrder_2_1").gameObject;
        textAfterOrder_3 = GameObject.Find("Canvas/Text_AfterOrder_3").gameObject;
        textAfterOrder_5 = GameObject.Find("Canvas/Text_AfterOrder_5").gameObject;
        Plotted_NumberLines = GameObject.Find("Canvas/3_Plotted_NumberLines").gameObject;


        current_slice = "";
        cookie_count = 0;
        hint_count = 0;

        var d = Obj15_RO_3.GetComponentsInChildren<AnimateColors>();
        foreach (AnimateColors s in d)
        {
            s.gameObject.GetComponent<Image>().enabled = false;
            s.enabled = false;
        }


        draggable_slice = FindObjectsOfType<drag_drop_obj_15>();
        foreach (drag_drop_obj_15 f in draggable_slice)
        {
            f.enabled = false;
        }
        Obj15_RO_Manager = FindObjectOfType<Obj15_RO_Manager>();
        Obj15_RO_Manager.Initiliaze();

        dialougue_ok_button.onClick.AddListener(Dialougue_ok_functionality);
        notepad_button.onClick.AddListener(() => enable_notepad());
        notepad_ok_button.onClick.AddListener(() => disable_notepad());
        activity_submit.onClick.AddListener(() => validate_submit_for_cookies());
        Submit_WrongTray.onClick.AddListener(() => GameObject.FindObjectOfType<drag_drop_obj_15>().select_wrong_tray());
        exit_button.onClick.AddListener(quit_app);
        ok_btn_Reinforcement_RO1.onClick.AddListener(Hide_Reinforcement_RO1);

        notepad_button.gameObject.SetActive(false);
        Dialouge_panel.SetActive(false);
        chefConversationPanel.SetActive(false);
        activity_submit.gameObject.SetActive(false);
        Submit_WrongTray.gameObject.SetActive(false);
        compare_plates.SetActive(false);
        two_or_more.SetActive(false);
        number_line_panel.SetActive(false);
        Obj15_RO_1.SetActive(false);
        Obj15_RO_3.SetActive(false);
        abstraction_panel.SetActive(false);
        Exit_Panel.SetActive(false);
        customer1.SetActive(false);
        customer2.SetActive(false);
        customer3.SetActive(false);
        order_1.SetActive(false);
        order_2.SetActive(false);
        order_3.SetActive(false);
        reinforcement_RO1_Panel.SetActive(false);
        textAfterOrder_1.SetActive(false);
        textAfterOrder_2.SetActive(false);
        textAfterOrder2_1.SetActive(false);
        textAfterOrder_3.SetActive(false);
        textAfterOrder_5.SetActive(false);
        Plotted_NumberLines.SetActive(false);
        TableAssets.SetActive(false);

    }

    public void CookiesInitially_Find()
    {
        cookiePlate1_1 = GameObject.Find("TableAssets/plate 1b2/1b2_1").gameObject;
        cookiePlate1_2 = GameObject.Find("TableAssets/plate 1b2/1b2_2").gameObject;
        cookiePlate2_1 = GameObject.Find("TableAssets/plate 2b4/2b4_1").gameObject;
        cookiePlate2_2 = GameObject.Find("TableAssets/plate 2b4/2b4_2").gameObject;
        cookiePlate2_3 = GameObject.Find("TableAssets/plate 2b4/2b4_3").gameObject;
        cookiePlate2_4 = GameObject.Find("TableAssets/plate 2b4/2b4_4").gameObject;
        cookiePlate3_1 = GameObject.Find("TableAssets/plate 4b8/4b8_1").gameObject;
        cookiePlate3_2 = GameObject.Find("TableAssets/plate 4b8/4b8_2").gameObject;
        cookiePlate3_3 = GameObject.Find("TableAssets/plate 4b8/4b8_3").gameObject;
        cookiePlate3_4 = GameObject.Find("TableAssets/plate 4b8/4b8_4").gameObject;
        cookiePlate3_5 = GameObject.Find("TableAssets/plate 4b8/4b8_5").gameObject;
        cookiePlate3_6 = GameObject.Find("TableAssets/plate 4b8/4b8_6").gameObject;
        cookiePlate3_7 = GameObject.Find("TableAssets/plate 4b8/4b8_7").gameObject;
        cookiePlate3_8 = GameObject.Find("TableAssets/plate 4b8/4b8_8").gameObject;

        pos_cookiePlate1_1 = cookiePlate1_1.transform.localPosition;
        pos_cookiePlate1_2 = cookiePlate1_2.transform.localPosition;
        pos_cookiePlate2_1 = cookiePlate2_1.transform.localPosition;
        pos_cookiePlate2_2 = cookiePlate2_2.transform.localPosition;
        pos_cookiePlate2_3 = cookiePlate2_3.transform.localPosition;
        pos_cookiePlate2_4 = cookiePlate2_4.transform.localPosition;
        pos_cookiePlate3_1 = cookiePlate3_1.transform.localPosition;
        pos_cookiePlate3_2 = cookiePlate3_2.transform.localPosition;
        pos_cookiePlate3_3 = cookiePlate3_3.transform.localPosition;
        pos_cookiePlate3_4 = cookiePlate3_4.transform.localPosition;
        pos_cookiePlate3_5 = cookiePlate3_5.transform.localPosition;
        pos_cookiePlate3_6 = cookiePlate3_6.transform.localPosition;
        pos_cookiePlate3_7 = cookiePlate3_7.transform.localPosition;
        pos_cookiePlate3_8 = cookiePlate3_8.transform.localPosition;


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
        if (startAudio.isPlaying)
            startAudio.Stop();
        HideLoadingAudio();
        switch (EventName)
        {
            //case "obj_15_looks_like":
            //    enable_panel(chefConversationPanel);
            //    set_conversation_msg("Looks like you are done with all the pizza orders for the day. While you were busy taking care of the orders, I made some desserts");

            //    break;

            case "obj_15_we_have_cookies":
                enable_panel(chefConversationPanel);
                set_conversation_msg("We have cookie slabs ready to be ordered. Here is a list of orders.");
                break;
            case "obj_15_click_on_notepad":
                notepad_button.gameObject.SetActive(true);
                notepad_button.GetComponentInChildren<AnimateColors>().enabled = true;
                set_conversation_msg("Click on notepad to see the next order");
                break;

            case "obj_15_the_first_three":
                notepad_button.GetComponentInChildren<AnimateColors>().enabled = false;
                notepad_button.GetComponentInChildren<AnimateColors>().gameObject.GetComponent<Image>().color = Color.white;
                break;

            //case "obj_15_here_are_the":
            //    customer1.SetActive(true);
            //    customer2.SetActive(true);
            //    customer3.SetActive(true);
            //    enable_panel(chefConversationPanel);

            //    //set_conversation_msg("Here are the trays which will help you pack the orders");
            //    TableAssets.SetActive(true);
            //    break;

            case "obj_15_there_are_three":
                enable_panel(chefConversationPanel);
                set_conversation_msg("There are three cookie slabs. They have been cut into different number of parts. You need to select the right portion of cookie slab from the tray and place the order in box");
                TableAssets.SetActive(true);
                customer1.SetActive(true);
                customer2.SetActive(false);
                customer3.SetActive(false);

                order_1.SetActive(true);
                order_2.SetActive(false);
                order_3.SetActive(false);
                break;

            case "obj_15_pack_one_half":
                TableAssets.SetActive(true);
                customer1.SetActive(true);
                customer2.SetActive(false);
                customer3.SetActive(false);
                UtilityArtifacts.loadStartingpointforcomingback = 6;
                order_1.SetActive(true);
                order_2.SetActive(false);
                order_3.SetActive(false);
                enable_panel(chefConversationPanel);
                activity_submit.gameObject.SetActive(true);

                cookie_count = 0;
                hint_count = 0;
                foreach (drag_drop_obj_15 f in draggable_slice)
                {
                    f.enabled = true;
                }
                set_conversation_msg("Pack \\frac{1}{2} cookie slab ");
                current_slice = "1b2";
                break;


            case "obj_15_pack_two_fourth":
                TableAssets.SetActive(true);
                enable_panel(chefConversationPanel);
                customer1.SetActive(false);
                customer2.SetActive(true);
                customer3.SetActive(false);
                UtilityArtifacts.loadStartingpointforcomingback = 7;
                order_1.SetActive(false);
                order_2.SetActive(true);
                order_3.SetActive(false);
                cookie_count = 0;
                hint_count = 0;
                set_conversation_msg("Pack \\frac{2}{4} cookie slab ");
                current_slice = "2b4";
                break;

            case "obj_15_pack_four_eight":
                enable_panel(chefConversationPanel);
                TableAssets.SetActive(true);
                customer1.SetActive(false);
                customer2.SetActive(false);
                customer3.SetActive(true);
                UtilityArtifacts.loadStartingpointforcomingback = 8;
                order_1.SetActive(false);
                order_2.SetActive(false);
                order_3.SetActive(true);
                cookie_count = 0;
                hint_count = 0;
                set_conversation_msg("Pack \\frac{4}{8} cookie slab ");
                current_slice = "4b8";
                break;

            case "obj_15_have_a_look_at":

                customer3.SetActive(false);
                order_3.SetActive(false);
                activity_submit.gameObject.SetActive(false);
                enable_panel(chefConversationPanel);
                set_conversation_msg("Have a look at all the orders packed with cookie slabs.");
                current_slice = "";
                TableAssets.SetActive(false);
                compare_plates.SetActive(true);
                break;

            case "obj_15_if_you_observe":
                enable_panel(chefConversationPanel);
                set_conversation_msg("If you observe closely, you will realize that the same quantity and size of cookie slabs for all the 3 orders have been packed. ");
                break;

            case "obj_15_only_the":
                enable_panel(chefConversationPanel);
                set_conversation_msg("Only the way they are cut is different..");
                break;

            case "obj_15_if_all_the_cookies":
                //enable_panel(chefConversationPanel);
                //compare_plates.GetComponent<Animator>().Play("stacking");
                //set_conversation_msg("If all the cookie slabs are same in size and quantity.");
                disable_panel(chefConversationPanel, 0.5f);
                textAfterOrder_1.SetActive(true);
                break;
            case "obj_15_can_say_that_fractions":
                //enable_panel(chefConversationPanel);
                //set_conversation_msg("Can we say that \\frac{1}{2} = \\frac{2}{4} = \\frac{4}{8} ?");
                textAfterOrder_1.SetActive(true);
                textAfterOrder_2.SetActive(true);
                disable_panel(chefConversationPanel, 0.5f);
                break;

            case "obj_15_since_the_frac":
                enable_panel(chefConversationPanel);
                set_conversation_msg("Observe that the number of parts in each slab is the only difference and  the fractions denoting them have different numerators and different denominators.");
                //disable_panel(chefConversationPanel, 0.5f);
                textAfterOrder_1.SetActive(false);
                textAfterOrder_2.SetActive(false);
                textAfterOrder2_1.SetActive(true);
                break;

            case "obj_15_hence_these_frac":
                enable_panel(chefConversationPanel);
                set_conversation_msg("Fractions that have different Numerators and Denominators but are equal in value and size are called Equivalent Fractions.");
                //disable_panel(chefConversationPanel,0.5f);
                textAfterOrder2_1.SetActive(false);
                textAfterOrder_3.SetActive(true);
                break;

            case "obj_15_two_or_more":
                enable_panel(chefConversationPanel);
                //disable_panel(chefConversationPanel,0.5f);
                compare_plates.SetActive(false);
                two_or_more.SetActive(true);
                set_conversation_msg("Two or more fractions are said to be Equivalent, if the amount or size considered is the same but the number of parts are different");
                textAfterOrder_3.SetActive(false);
                two_or_more.SetActive(false);
                compare_plates.SetActive(false);
                textAfterOrder_5.SetActive(true);

                break;
            case "obj_15_now_you_learned":
                disable_panel(chefConversationPanel, 0.5f);
                //enable_panel(chefConversationPanel);
                //set_conversation_msg("Now that you just learnt what Equivalent Fractions are.");
                two_or_more.SetActive(false);
                textAfterOrder_5.SetActive(true);

                break;

            case "obj_15_select_the_circle":
                textAfterOrder_5.SetActive(false);
                disable_panel(chefConversationPanel, 0.5f);
                Obj15_RO_1.SetActive(true);
                Obj15_RO_Manager.EnableSubmitButtonRO1_Obj15();

                break;


            case "obj_15_lets_understand":
                enable_panel(chefConversationPanel);
                Obj15_RO_1.SetActive(false);
                set_conversation_msg("Let's understand more about Equivalent Fractions by plotting them on a number line. We'll plot the fractions  \\frac{1}{2}, \\frac{2}{4} and \\frac{4}{8}  on different number lines");
                number_line_panel.SetActive(true);
                number_line_1.SetActive(false);
                number_line_2.SetActive(false);
                number_line_3.SetActive(false);
                break;

            case "obj_15_plot_half":
                enable_panel(chefConversationPanel);
                number_line_1.SetActive(true);
                number_line_1.GetComponent<NumberLineManager>().createDivision(2);
                number_line_1.GetComponent<NumberLineManager>().setText();
                set_conversation_msg("Plot the fraction" + " \\frac{1}{2}");
                break;

            case "obj_15_plot_two":
                enable_panel(chefConversationPanel);
                number_line_2.SetActive(true);
                number_line_2.GetComponent<NumberLineManager>().createDivision(4);
                number_line_2.GetComponent<NumberLineManager>().setText();
                set_conversation_msg("Plot the fraction" + " \\frac{2}{4}");
                break;

            case "obj_15_plot_four":
                enable_panel(chefConversationPanel);
                number_line_3.SetActive(true);
                number_line_3.GetComponent<NumberLineManager>().createDivision(8);
                number_line_3.GetComponent<NumberLineManager>().setText();
                set_conversation_msg("Plot the fraction" + " \\frac{4}{8}");
                break;

            case "obj_15_if_you_observe_you_will":
                enable_panel(chefConversationPanel);
                set_conversation_msg("If you observe, you will notice that all the points that have been plotted align at the same point on all the number lines.");
                Plotted_NumberLines.SetActive(true);
                break;

            case "obj_15_notice":
                enable_panel(chefConversationPanel);
                set_conversation_msg("Notice how the fractions are different yet are all plotted at the same distance from 0.");
                Plotted_NumberLines.GetComponent<Animator>().SetBool("Play", true);
                break;

            case "obj_15_hence_any":
                enable_panel(chefConversationPanel);
                set_conversation_msg("Hence, any set of fractions that align at the same point across multiple number lines are all equal in value and are Equivalent Fractions. ");
                //reverse play previous yellow line animation
                //Plotted_NumberLines.GetComponent<Animator>().SetBool("IsReverse", true);
                //Plotted_NumberLines.GetComponent<Animator>().SetBool("Play", false);
                Plotted_NumberLines.GetComponent<Animator>().SetBool("IsHighlight", true);

                break;
            case "obj_15_looking_at":
                disable_panel(chefConversationPanel, 0.5f);
                number_line_panel.SetActive(false);
                Obj15_RO_3.SetActive(true);
                Obj15_RO_Manager.EnableSubmitButtonRO3_Obj15();
                //Plotted_NumberLines.GetComponent<Animator>().SetBool("IsReverse", false);
                //Plotted_NumberLines.GetComponent<Animator>().SetBool("IsHighlightFraction", false);
                Plotted_NumberLines.SetActive(false);

                //set_conversation_msg("Looking at the number line, we can say that Equivalent Fractions are fractions");

                break;

            case "obj_15_abstarction":
                Obj15_RO_3.SetActive(false);
                abstraction_panel.SetActive(true);
                abstraction_panel.GetComponentInChildren<Text>().text = "We can say that a set of fractions are Equivalent if they represent the same value or quantity out of a whole. Identifying if 2 fractions are Equivalent or not is same as comparing 2 fractions to check if they are equal in value";
                // enable_panel(chefConversationPanel);
                // set_conversation_msg("We can say that a set of fractions are Equivalent if they represent the same value or quantity out of a whole. Identifying if 2 fractions are Equivalent or not is same as comparing 2 fractions to check if they are equal in value.");
                Invoke("enable_exit_panel", 20);
                break;


        }
    }

    void enable_exit_panel()
    {
        abstraction_panel.SetActive(false);
        //Exit_Panel.SetActive(true);
        gameObject.GetComponent<GameManager>().OnGameOver();
    }

    public void Show_Reinforcement_RO1()
    {
        reinforcement_RO1_Panel.SetActive(true);
    }
    public void Hide_Reinforcement_RO1()
    {
        FindObjectOfType<timeline_new>().lipsync_player.GetComponent<AudioSource>().Stop();
        reinforcement_RO1_Panel.SetActive(false);
        FindObjectOfType<timeline_new>().load_next();
        Obj15_RO_Manager.enableFade();
    }

    public void enable_panel(GameObject object_to_enable)
    {
        object_to_enable.SetActive(true);
        Animator animator_of_object;
        animator_of_object = object_to_enable.GetComponent<Animator>();

        animator_of_object.Play("enable", 0);
    }
    public void disable_panel(GameObject object_to_enable, float time)
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
    public void set_conversation_msg(string message)
    {
        if (chef_ConversationText != null)
        {
            chef_ConversationText.GetComponent<TEXDraw>().text = message;
        }
    }

    void enable_notepad()
    {
        notepad_button.GetComponentInChildren<AnimateColors>().enabled = false;
        notepad_button.GetComponentInChildren<AnimateColors>().gameObject.GetComponent<Image>().color = Color.white;
        disable_panel(chefConversationPanel, 1.0f);
        notepad_text.GetComponent<TEXDraw>().text = "\\frac{1}{2} of a cookie slab\n\n\\frac{2}{4} of a cookie slab\n\n\\frac{4}{8} of a cookie slab ";
        enable_panel(notepad_page);
        notepad_button.interactable = false;
        notepad_ok_button.interactable = true;
        FindObjectOfType<timeline_new>().load_next();

    }

    void disable_notepad()
    {
        disable_panel(notepad_page, 0.5f);
        notepad_button.GetComponentInChildren<AnimateColors>().enabled = false;
        notepad_button.GetComponentInChildren<AnimateColors>().gameObject.GetComponent<Image>().color = Color.white;
        notepad_ok_button.interactable = false;
        FindObjectOfType<timeline_new>().load_next();


    }

    public void validate_submit_for_cookies()
    {
        Debug.Log("validating");
        if (current_slice == "1b2")
        {
            if (cookie_count == 1)
            {
                pizza_box.GetComponent<Animator>().Play("packing");
                Invoke("pack_load_next", 1.6f);
                playCorrect();
            }
            else
            {
                playError();
                if (hint_count == 0)
                {
                    hint_count++;
                    FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_15_1b2_hint1.wav");

                    //set_dialougue("The customer wants 1/2 of a cookie slab. Ensure that the fraction matches the cookie slab packed. ");
                    OnHintAfterSubmittingQuestion();
                    set_conversation_msg("The customer wants 1/2 of a cookie slab. Ensure that the fraction matches the cookie slab packed.");
                    foreach (drag_drop_obj_15 d in draggable_slice)
                    {
                        if (d.name.Contains("1b2"))
                        {
                            d.transform.position = d.pos;
                        }
                    }
                }
                else
                {
                    hint_count++;
                    FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_15_1b2_hint2.wav");
                    OnHintAfterSubmittingQuestion();
                    set_conversation_msg("The amount of cookie slab to be packed should be according to the fraction given. Check again.");
                    //set_dialougue("The amount of cookie slab to be packed should be according to the fraction given. Check again.");
                    foreach (drag_drop_obj_15 d in draggable_slice)
                    {
                        if (d.name.Contains("1b2"))
                        {
                            d.transform.position = d.pos;
                        }
                    }
                }
                //else if(hint_count > 1)
                //{
                //    activity_submit.gameObject.SetActive(false);
                //    hint_count++;
                //    Debug.Log("move obj4_lo1");
                //    UtilityArtifacts.loading_pos = "Obj4_Lo1_from_obj15";
                //UtilityArtifacts.coming_back_from = "to_Obj15_quest1";
                //    UtilityArtifacts.backTraversal = true;
                //    UtilityArtifacts.comingbackafterTraversal = false;
                //    UtilityArtifacts.loadStartingpointforcomingback = 4;
                //    // load traversescene 4
                //    //SceneManager.LoadScene("Obj4AreaModule");
                //    OnTraversal(155, 129);
                //}
            }
            // cookie_count = 0;
        }
        else if (current_slice == "2b4")
        {
            if (cookie_count == 2)
            {
                pizza_box.GetComponent<Animator>().Play("packing");
                playCorrect();
                Invoke("pack_load_next", 1.6f);
            }
            else
            {
                playError();
                if (hint_count == 0)
                {
                    hint_count++;
                    FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_15_2b4_hint1.wav");
                    OnHintAfterSubmittingQuestion();
                    set_conversation_msg("The customer wants 2/4 of a cookie slab. Ensure that the fraction matches the cookie slab packed. ");
                    //set_dialougue("The customer wants 2/4 of a cookie slab. Ensure that the fraction matches the cookie slab packed. ");
                    foreach (drag_drop_obj_15 d in draggable_slice)
                    {
                        if (d.name.Contains("2b4"))
                        {
                            d.transform.position = d.pos;
                        }
                    }
                }
                else if (hint_count == 1)
                {
                    hint_count++;
                    FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_15_2b4_hint2.wav");
                    OnHintAfterSubmittingQuestion();
                    set_conversation_msg("The amount of cookie slab to be packed should be according to the fraction given. Check again. ");
                    //set_dialougue("The amount of cookie slab to be packed should be according to the fraction given. Check again.");
                    foreach (drag_drop_obj_15 d in draggable_slice)
                    {
                        if (d.name.Contains("2b4"))
                        {
                            d.transform.position = d.pos;
                        }
                    }
                }
                else if (hint_count > 1)
                {
                    activity_submit.gameObject.SetActive(false);
                    hint_count++;
                    Debug.Log("move obj4_lo1");
                    UtilityArtifacts.loading_pos = "Obj4_Lo1";
                    UtilityArtifacts.coming_back_from = "to_Obj15_quest2";
                   
                    UtilityArtifacts.backTraversal = true;
                    UtilityArtifacts.comingbackafterTraversal = false;
                    UtilityArtifacts.loadStartingpointforcomingback = 5;
                    // load traversescene 4
                    //SceneManager.LoadScene("Obj4AreaModule");
                    OnTraversal(155,129);
                }
            }
            // cookie_count = 0;
        }
        else if (current_slice == "4b8")
        {
            if (cookie_count == 4)
            {
                pizza_box.GetComponent<Animator>().Play("packing");
                Invoke("pack_load_next", 1.6f);
                playCorrect();
            }
            else
            {
                playError();
                if (hint_count == 0)
                {
                    hint_count++;
                    FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_15_4b8_hint1.wav");
                    OnHintAfterSubmittingQuestion();
                    set_conversation_msg("The customer wants 4/8 of a cookie slab. Ensure that the fraction matches the cookie slab packed. ");
                    //set_dialougue("The customer wants 4/8 of a cookie slab. Ensure that the fraction matches the cookie slab packed. ");
                    foreach (drag_drop_obj_15 d in draggable_slice)
                    {
                        if (d.name.Contains("4b8"))
                        {
                            d.transform.position = d.pos;
                        }
                    }

                }
                else if (hint_count == 1)
                {
                    hint_count++;
                    FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_15_4b8_hint2.wav");
                    OnHintAfterSubmittingQuestion();
                    set_conversation_msg("The amount of cookie slab to be packed should be according to the fraction given. Check again. ");
                    //set_dialougue("The amount of cookie slab to be packed should be according to the fraction given. Check again.");
                    foreach (drag_drop_obj_15 d in draggable_slice)
                    {
                        if (d.name.Contains("4b8"))
                        {
                            d.transform.position = d.pos;
                        }
                    }

                }
                else if (hint_count > 1)
                {
                    activity_submit.gameObject.SetActive(false);
                    hint_count++;
                    Debug.Log("move obj4_lo1");
                    UtilityArtifacts.loading_pos = "Obj4_Lo1";
                    UtilityArtifacts.coming_back_from = "to_Obj15_quest3";
                    UtilityArtifacts.backTraversal = true;
                    UtilityArtifacts.comingbackafterTraversal = false;
                    UtilityArtifacts.loadStartingpointforcomingback = 6;
                    // load traversescene 4
                    //SceneManager.LoadScene("Obj4AreaModule");
                    OnTraversal(155, 129);
                }
                //  cookie_count = 0;
            }
        }
        else
        {
            hint_count++;
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

    public void ResetCookiesPos()
    {
        cookiePlate1_1.transform.localPosition = pos_cookiePlate1_1;
        cookiePlate1_2.transform.localPosition = pos_cookiePlate1_2;
        cookiePlate2_1.transform.localPosition = pos_cookiePlate2_1;
        cookiePlate2_2.transform.localPosition = pos_cookiePlate2_2;
        cookiePlate2_3.transform.localPosition = pos_cookiePlate2_3;
        cookiePlate2_4.transform.localPosition = pos_cookiePlate2_4;
        cookiePlate3_1.transform.localPosition = pos_cookiePlate3_1;
        cookiePlate3_2.transform.localPosition = pos_cookiePlate3_2;
        cookiePlate3_3.transform.localPosition = pos_cookiePlate3_3;
        cookiePlate3_4.transform.localPosition = pos_cookiePlate3_4;
        cookiePlate3_5.transform.localPosition = pos_cookiePlate3_5;
        cookiePlate3_6.transform.localPosition = pos_cookiePlate3_6;
        cookiePlate3_7.transform.localPosition = pos_cookiePlate3_7;
        cookiePlate3_8.transform.localPosition = pos_cookiePlate3_8;
    }

    public void set_dialougue(string message)
    {
        Dialouge_panel.SetActive(true);
        if (Dialouge_text != null)
        {
            Dialouge_text.GetComponent<TEXDraw>().text = message;
        }
    }

    public void Dialougue_ok_functionality()
    {
        FindObjectOfType<timeline_new>().lipsync_player.GetComponent<AudioSource>().Stop();
        Dialouge_panel.SetActive(false);

        //reset the cookies
        //GameObject.FindObjectOfType<drag_drop_obj_15>().ResetCookies();

    }

    public void pack_load_next()
    {
        foreach (drag_drop_obj_15 s in draggable_slice)
        {
            if (s.name.Contains(current_slice))
            {
                if (s.transform.position != s.pos)
                {
                    s.gameObject.SetActive(false);


                }
                //s.enabled = false;
            }
        }
        FindObjectOfType<timeline_new>().load_next();
        pizza_box.GetComponent<Animator>().Play("empty");
    }

    public void highlight_ro3_numbers()
    {
        var d = Obj15_RO_3.GetComponentsInChildren<AnimateColors>();
        foreach (AnimateColors s in d)
        {
            s.gameObject.GetComponent<Image>().enabled = true;
            s.enabled = true;
        }

        Invoke("disable_highlight_ro3_numbers", 6f);
    }
    public void disable_highlight_ro3_numbers()
    {
        var d = Obj15_RO_3.GetComponentsInChildren<AnimateColors>();
        foreach (AnimateColors s in d)
        {
            s.gameObject.GetComponent<Image>().enabled = false;
            s.enabled = false;
        }

        FindObjectOfType<timeline_new>().load_next();

    }

    public void OnHintAfterSubmittingQuestion()
    {
        enable_panel(chefConversationPanel);
        activity_submit.gameObject.SetActive(false);
        Submit_WrongTray.gameObject.SetActive(false);
        foreach (drag_drop_obj_15 f in draggable_slice)
        {
            f.enabled = false;
        }
        Invoke("HideHintAndShowSubmitBtn", GameObject.FindObjectOfType<timeline_new>().lapa.length_of_audio + 2.0f);
    }

    public void OnHintWrongTray()
    {
        enable_panel(chefConversationPanel);
        activity_submit.gameObject.SetActive(false);
        Submit_WrongTray.gameObject.SetActive(false);
        foreach (drag_drop_obj_15 f in draggable_slice)
        {
            f.enabled = false;
        }
        Invoke("HideHintAndShowSubmitBtn", 9.7f);
    }

    void HideHintAndShowSubmitBtn()
    {

        ResetCookiesPos();

        activity_submit.gameObject.SetActive(true);
        foreach (drag_drop_obj_15 f in draggable_slice)
        {
            f.enabled = true;
        }
        chefConversationPanel.SetActive(false);
        activity_submit.onClick.RemoveAllListeners();
        activity_submit.onClick.AddListener(() => validate_submit_for_cookies());

    }

    public void ShowWrongTraySubmitButton()
    {
        activity_submit.gameObject.SetActive(false);
        Submit_WrongTray.gameObject.SetActive(true);
    }


}
