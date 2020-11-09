using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class obj_12_improper_and_mixed : MonoBehaviour
{
    string jsonFileName = "obj_12_improper_and_mixed.json";
    public GameObject Dialouge_text, Dialouge_panel, chefConversationPanel, chef_ConversationText, notepad_page, notepad_text, Exit_Panel, customer1, customer2, customer3, plate_2b6, plate_full, one_two_by_six, yes_no_true_false_panel, yes_no_quest_text, one_two_by_six_veg_pizza_LO_1,PizzaBox_one,PizzaBox_two,one_two_one,one_two_two_six, full_pizza_four_parts,one_one_by_four,five_by_four, both11by4_5by4, LO3_5by4, LO3_5by4_animation, LO3_5by4_fixed, lo3_pizza_inbox, lo3_pizza_inbox_1, number_line,line_1,line_2,line_3,line_4,engagement_number_line,line_1_engagement,line_2_engagement, Obj12_RO_1, Obj12_RO_1_2, Obj12_RO_2, Obj12_RO_3, Obj12_RO_4_1, Obj12_RO_5_1, Obj12_RO_6_1, Obj12_RO_6_2, Obj12_RO_6_3, dialougue_image, dialougue_ro5_1, dialougue_ro5_2, ro_3_animation, five_b_four_impr, arrow_mixed, dialougue_ro6_1_abcd, four_five_proper,numberline3b4,dialougue_ro6_2,dialougue_ro6_3;

    public GameObject whole_numbr, propr_fract, mixed_fraction_explain,improper_fraction_explain;
    public Button dialougue_ok_button, notepad_button, notepad_ok_button, exit_button, submit_2b6,yes_button,no_button, submit_lo3, engagementsubmit;

    public bool full_pizza, pizza_2b6;

    public int pizza_2b6_count,hint_count,lo3_pizza_count, lo3_pizza_count2, lo3_hint, pointer_count;
    public string first_dragged_id,dialougue_decision_case;

    public Obj12_RO_Manager Obj12_RO_Manager;
    public GameObject LoadingAudio;
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
        pizza_2b6_count = 0;
        hint_count = 0;
        lo3_pizza_count = 0;
        lo3_pizza_count2 = 0;
        full_pizza = false;
        pizza_2b6 = false;
        pointer_count = 0;
        Initialization();
        Invoke("audio_invoke", 2.0f);
        first_dragged_id ="";

}
void Initialization()
    {
        Dialouge_text = GameObject.Find("Dialougue text");
        Dialouge_panel = GameObject.Find("Dialougue Panel");
        dialougue_image = GameObject.Find("dialougue_image");
        dialougue_ok_button = GameObject.Find("dialougue_ok").GetComponent<Button>();
        chefConversationPanel = GameObject.Find("Chef conversation");
        chef_ConversationText = GameObject.Find("ConversationText");
        notepad_page = GameObject.Find("notepad_page");
        notepad_text = GameObject.Find("notepad_text");
        notepad_ok_button = GameObject.Find("notepad_ok").GetComponent<Button>();
        notepad_button = GameObject.Find("notepad_button").GetComponent<Button>();
        Exit_Panel = GameObject.Find("Exit Panel");
        exit_button = GameObject.Find("Exit_button").GetComponent<Button>();
        customer1 = GameObject.Find("customer");
        customer2 = GameObject.Find("customer2");
        customer3 = GameObject.Find("customer3");
        plate_full = GameObject.Find("Plate_full");
        plate_2b6 = GameObject.Find("Plate 6 piece");
        one_two_by_six = GameObject.Find("one two by six veg pizza");
        yes_no_true_false_panel = GameObject.Find("yes_no_true_false_panel");
        yes_no_quest_text = GameObject.Find("yes_no_quest_text");
        one_two_by_six_veg_pizza_LO_1 = GameObject.Find("one two by six veg pizza LO_1");
        PizzaBox_one = GameObject.Find("PizzaBox_one");
        PizzaBox_two = GameObject.Find("PizzaBox_two");
        one_two_one = GameObject.Find("one_two_one");
        one_two_two_six = GameObject.Find("one_two_two_six");
        full_pizza_four_parts = GameObject.Find("full_pizza_four_parts");
        one_one_by_four = GameObject.Find("one_one_by_four_mixed_fraction");
        five_by_four = GameObject.Find("five_by_four_improper_fraction");
        both11by4_5by4 = GameObject.Find("both11by4_5by4");
        LO3_5by4_animation = GameObject.Find("LO3_5by4_animation");
        LO3_5by4 = GameObject.Find("LO3_5by4");
        LO3_5by4_fixed = GameObject.Find("LO3_5by4_fixed");
        lo3_pizza_inbox = GameObject.Find("lo3_pizza in box");
        lo3_pizza_inbox_1 = GameObject.Find("lo3_pizza in box_1");
        number_line = GameObject.Find("NumberLine");
        line_1 = GameObject.Find("Line_1");
        line_2 = GameObject.Find("Line_2");
        line_3 = GameObject.Find("Line_3");
        line_4 = GameObject.Find("Line_4");
        whole_numbr = GameObject.Find("whole_numbr");
        propr_fract = GameObject.Find("propr_fract");
        engagement_number_line = GameObject.Find("Engagement_NumberLine");
        line_1_engagement = GameObject.Find("Line_1_engagement");
        line_2_engagement = GameObject.Find("Line_2_engagement");
        Obj12_RO_1 = GameObject.Find("Obj12_RO_1");
        Obj12_RO_1_2 = GameObject.Find("Obj12_RO_1_2");
        Obj12_RO_2 = GameObject.Find("Obj12_RO_2");
        Obj12_RO_3 = GameObject.Find("Obj12_RO_3");
        Obj12_RO_4_1 = GameObject.Find("Obj12_RO_4_1");
        Obj12_RO_5_1 = GameObject.Find("Obj12_RO_5_1");
        Obj12_RO_6_1 = GameObject.Find("Obj12_RO_6_1");
        Obj12_RO_6_2 = GameObject.Find("Obj12_RO_6_2");
        Obj12_RO_6_3 = GameObject.Find("Obj12_RO_6_3");
        dialougue_ro5_1 = GameObject.Find("dialougue_ro5_1");
        dialougue_ro5_2 = GameObject.Find("dialougue_ro5_2");
        ro_3_animation = GameObject.Find("ro_3_animation");
        numberline3b4 = GameObject.Find("numberline3b4");

        yes_button = GameObject.Find("yes_button").GetComponent<Button>();
        no_button = GameObject.Find("no_button").GetComponent<Button>();
        submit_lo3 = GameObject.Find("submit_lo3").GetComponent<Button>();
        submit_2b6 = GameObject.Find("submit_2b6").GetComponent<Button>();
        engagementsubmit = GameObject.Find("engagement submit").GetComponentInChildren<Button>();
        five_b_four_impr = GameObject.Find("five_b_four_impr");
        arrow_mixed = GameObject.Find("arrow_mixed");
        four_five_proper = GameObject.Find("four_five_proper");
        mixed_fraction_explain = GameObject.Find("mixed_fraction_explain");
        improper_fraction_explain= GameObject.Find("improper_fraction_explain"); ;
        dialougue_ro6_2 = GameObject.Find("dialougue_ro6_2");
        dialougue_ro6_3 = GameObject.Find("dialougue_ro6_3");
        dialougue_ro6_1_abcd = GameObject.Find("dialougue_ro6_1abcd");
        LoadingAudio = GameObject.Find("LoadAudio");
        if (UtilityArtifacts.backTraversal)
        {
            Text textLoadingText = LoadingAudio.transform.GetChild(2).GetComponent<Text>();
            textLoadingText.text = "Let us understand this better";
        }
        Obj12_RO_Manager = FindObjectOfType<Obj12_RO_Manager>();
        Obj12_RO_Manager.Initiliaze();



        dialougue_ok_button.onClick.AddListener(Dialougue_ok_functionality);
        notepad_button.onClick.AddListener(() => enable_notepad());
        notepad_ok_button.onClick.AddListener(() => disable_notepad());
        submit_2b6.onClick.AddListener(() => drag_pizza_validation());
        yes_button.onClick.AddListener(() => validate_yes_true());
        no_button.onClick.AddListener(()=>validate_no_false());
        submit_lo3.onClick.AddListener(() => validate_lo3_submit());
        engagementsubmit.onClick.AddListener(load_next);
        exit_button.onClick.AddListener(quit_app);


        var f = lo3_pizza_inbox_1.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer s in f)
        {
            s.gameObject.SetActive(false);
        }
        var d = lo3_pizza_inbox.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer s in d)
        {
            s.gameObject.SetActive(false);
        }
        notepad_button.gameObject.SetActive(false);
        dialougue_image.SetActive(false);
        Dialouge_panel.SetActive(false);
        chefConversationPanel.SetActive(false);
        one_two_by_six.SetActive(false);
        submit_2b6.gameObject.SetActive(false);
      
        PizzaBox_one.SetActive(false);
        PizzaBox_two.SetActive(false);
        one_two_one.SetActive(false);
        one_two_two_six.SetActive(false);
        one_two_by_six_veg_pizza_LO_1.SetActive(false);
        full_pizza_four_parts.SetActive(false);
        one_one_by_four.SetActive(false);
        five_by_four.SetActive(false);
        both11by4_5by4.SetActive(false);
        yes_no_true_false_panel.SetActive(false);
        submit_lo3.gameObject.SetActive(false);
        LO3_5by4_animation.SetActive(false);
        LO3_5by4.SetActive(false);
        LO3_5by4_fixed.SetActive(false);
        line_1.SetActive(false);
        line_2.SetActive(false);
        line_3.SetActive(false);
        line_4.SetActive(false);
        number_line.SetActive(false);
        engagementsubmit.gameObject.SetActive(false);
        engagement_number_line.SetActive(false);
        Obj12_RO_1.SetActive(false);
        Obj12_RO_2.SetActive(false);
        Obj12_RO_3.SetActive(false);
        Obj12_RO_1_2.SetActive(false);
        Obj12_RO_4_1.SetActive(false);
        Obj12_RO_5_1.SetActive(false);
        Obj12_RO_6_1.SetActive(false);
        Obj12_RO_6_2.SetActive(false);
        Obj12_RO_6_3.SetActive(false);
        Exit_Panel.SetActive(false);
        dialougue_ro5_1.SetActive(false);
        dialougue_ro5_2.SetActive(false);
        dialougue_ro6_2.SetActive(false);
        dialougue_ro6_3.SetActive(false);
        dialougue_ro6_1_abcd.SetActive(false);
        ro_3_animation.SetActive(false);
          customer1.SetActive(false);
        //  customer2.SetActive(false);
        //   customer3.SetActive(false);
        five_b_four_impr.SetActive(false);
        four_five_proper.SetActive(false);
        arrow_mixed.SetActive(false);
        whole_numbr.SetActive(false);
        propr_fract.SetActive(false);
        numberline3b4.SetActive(false);
        mixed_fraction_explain.SetActive(false);
        improper_fraction_explain.SetActive(false);
        lo3_hint = 0;
    }
    void load_next()
    {
        yes_no_true_false_panel.SetActive(false);
        FindObjectOfType<timeline_new>().load_next();
    }
    void load_next1()
    {
        yes_no_true_false_panel.SetActive(false);
        UtilityArtifacts.loading_pos = "Obj11_Lo4_from_obj12";
        UtilityArtifacts.coming_back_from = "to_Obj12_quest1";
        UtilityArtifacts.backTraversal = false;
        UtilityArtifacts.comingbackafterTraversal = true;
        UtilityArtifacts.loadStartingpointforcomingback = 29;
        UtilityArtifacts.loadStartingpoint = 0;
        UtilityArtifacts.loadEndingpoint = 0;
        //load traversescene 11
        //SceneManager.LoadScene("Obj11");
        OnTraversal(148, 131);
    }
    void load_next2()
    {
        yes_no_true_false_panel.SetActive(false);
        UtilityArtifacts.loading_pos = "Obj4_Lo1_from_obj12";
        UtilityArtifacts.coming_back_from = "to_Obj12_quest2";
        UtilityArtifacts.backTraversal = false;
        UtilityArtifacts.comingbackafterTraversal = true;
        UtilityArtifacts.loadStartingpointforcomingback = 20;
        UtilityArtifacts.loadStartingpoint = 0;
        UtilityArtifacts.loadEndingpoint = 0;
        //load traversescene 4
        //SceneManager.LoadScene("Obj4AreaModule");
        OnTraversal(155, 129);
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
    void enable_notepad()
    {

        disable_panel(chefConversationPanel, 1.0f);
        notepad_button.GetComponentInChildren<AnimateColors>().enabled = false;
        notepad_button.GetComponentInChildren<AnimateColors>().gameObject.GetComponent<Image>().color = Color.white;
        notepad_text.GetComponent<TEXDraw>().text = "One whole and \\frac{2}{6} of veg pizza ";
        enable_panel(notepad_page);
        notepad_button.interactable = false;
        notepad_ok_button.interactable = true;
        FindObjectOfType<timeline_new>().load_next();

    }

    void disable_notepad()
    {
        disable_panel(notepad_page,  0.5f);
        notepad_button.GetComponentInChildren<AnimateColors>().enabled = false;
        notepad_button.GetComponentInChildren<AnimateColors>().gameObject.GetComponent<Image>().color = Color.white;
        notepad_ok_button.interactable = false;
        FindObjectOfType<timeline_new>().load_next();


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
            case "obj_12_lets_check":
                dialougue_decision_case = "";
                enable_panel(chefConversationPanel);
                chef_ConversationText.GetComponent<TEXDraw>().text = "Let's check the next order";
                notepad_button.gameObject.SetActive(true);
                notepad_button.GetComponentInChildren<AnimateColors>().enabled = true;              
                break;

            case  "obj_12_we_need_to":

                break;

            case "obj_12_go_ahead":
                //  enable_panel(chefConversationPanel);
                customer1.SetActive(true);
                one_two_by_six.gameObject.SetActive(true);
                FindObjectOfType<conversationManager>().EnableQuestion("Pack a whole pizza and \\frac{2}{6} of a pizza in separate boxes.");
               // chef_ConversationText.GetComponent<TEXDraw>().text = "Go ahead and pack the pizzas. Pack a whole pizza and \\frac{2}{6} of a pizza in separate boxes.";
                break;

            case "obj_12_we_know_that":
                customer1.SetActive(false);
                //  disable_panel(chefConversationPanel,0.5F);
                FindObjectOfType<conversationManager>().DisableQuestion();
                dialougue_decision_case = "LO1_yes_no";
                yes_no_true_false_panel.SetActive(true);
                set_yes_no_true_false("We know that fractions are used to represent quantities which are less than a whole.\nBut can we also use fractions to represent quantities which are more than a whole ? ",true);
                break;

            case "obj_12_for_example":
                dialougue_decision_case = "";
                yes_no_true_false_panel.SetActive(false);
                one_two_by_six_veg_pizza_LO_1.SetActive(true);
                PizzaBox_one.SetActive(true);
                PizzaBox_two.SetActive(true);
                enable_panel(chefConversationPanel);
                chef_ConversationText.GetComponent<TEXDraw>().text = "For example, packing an order for a customer who wants one full veg pizza and a part of another veg pizza.";
                break;

            case "obj_12_so_what":
                one_two_by_six_veg_pizza_LO_1.SetActive(true);
                PizzaBox_one.SetActive(true);
                PizzaBox_two.SetActive(true);
                enable_panel(chefConversationPanel);
                chef_ConversationText.GetComponent<TEXDraw>().text = "So, what quantity of pizza did this customer order exactly?";
                
                break;
            case "obj_12_the_correct":
              
                chef_ConversationText.GetComponent<TEXDraw>().text = "The correct numerical ways to express the quantity is by saying that this customer ordered 1 \\frac{2}{6} of veg pizza. ";

                break;
            case "obj_12_this_type":
               
                chef_ConversationText.GetComponent<TEXDraw>().text = "This type of a fraction is called a mixed fraction.1 \\frac{2}{6} is a mixed fraction ";

                break;
            case "obj_12_the_customer":
                enable_panel(chefConversationPanel);
                chef_ConversationText.GetComponent<TEXDraw>().text = "The customer ordered 1 \\frac{2}{6} of veg pizza.This fraction here, 1 \\frac{2}{6} is called a mixed fraction.";
                one_two_one.SetActive(true);
                one_two_two_six.SetActive(true);
                arrow_mixed.SetActive(true);
                break;

            case "obj_12_the_customer_ordered":
                enable_panel(chefConversationPanel);
                chef_ConversationText.GetComponent<TEXDraw>().text = "The customer ordered one whole pizza and a part of another pizza. So, we represent the total quantity ordered as a mixed fraction.";
                one_two_one.SetActive(true);
                one_two_two_six.SetActive(true);
                break;

            case "obj_12_mixed_fraction":
                enable_panel(chefConversationPanel);
                chef_ConversationText.GetComponent<TEXDraw>().text = "Mixed fraction is a combination of a whole number and a proper fraction.";
                var a=PizzaBox_one.GetComponentsInChildren<AnimateColors>();
                var a1=PizzaBox_two.GetComponentsInChildren<AnimateColors>();
                foreach (AnimateColors dw in a)
                {
                    dw.enabled = true;

                }
                foreach (AnimateColors dw in a1)
                {
                    dw.enabled = true;

                }
                one_two_one.SetActive(true);
                one_two_two_six.SetActive(true);
                propr_fract.SetActive(false);
                whole_numbr.SetActive(true);
                propr_fract.SetActive(true);
                arrow_mixed.SetActive(false);
                break;


            case "obj_12_which_define_mix":
                one_two_by_six_veg_pizza_LO_1.SetActive(false);
                arrow_mixed.SetActive(false);
                PizzaBox_one.SetActive(false);
                PizzaBox_two.SetActive(false);
                one_two_one.SetActive(false);
                one_two_two_six.SetActive(false);
                whole_numbr.SetActive(false);
                propr_fract.SetActive(false);
                arrow_mixed.SetActive(false);
                disable_panel(chefConversationPanel,0.5f);
                Obj12_RO_1.gameObject.SetActive(true);
                Obj12_RO_Manager.Initiliaze();
                Obj12_RO_Manager.EnableSubmitButtonRO1_Obj12();
              
                dialougue_decision_case = "RO_1_1";
                ////forwarding ro
                //FindObjectOfType<timeline_new>().load_next();
                break;


            case "obj_12_which_is_mix":
                Obj12_RO_1.gameObject.SetActive(false);
                dialougue_decision_case = "RO_1_1";
                Obj12_RO_1_2.gameObject.SetActive(true);
                Obj12_RO_Manager.EnableSubmitButtonRO1_2Obj12();
                ////forwarding ro
                //FindObjectOfType<timeline_new>().load_next();
                break;


            case "obj_12_here_is_another":
                Obj12_RO_1_2.gameObject.SetActive(false);
                enable_panel(chefConversationPanel);
                chef_ConversationText.GetComponent<TEXDraw>().text = "Here is another order. This order is for  \\frac{5}{4} veg pizzas.";
                break;

            case "obj_12_we_learnt_proper":
                enable_panel(chefConversationPanel);
                four_five_proper.SetActive(true);
                chef_ConversationText.GetComponent<TEXDraw>().text = " We learnt that fractions whose numerator is lesser than the denominator are called proper fractions";
                break;

            case "obj_12_but_what_about":
                enable_panel(chefConversationPanel);
                four_five_proper.SetActive(false);
                five_b_four_impr.SetActive(true);
                chef_ConversationText.GetComponent<TEXDraw>().text = " But what about this fraction \\frac{5}{4}? Its numerator is greater than the denominator. Such fractions are called Improper fractions.";
                break;


            case "obj_12_which_is_improper":
                five_b_four_impr.SetActive(false);
                disable_panel(chefConversationPanel, 0.5f);
                Obj12_RO_2.SetActive(true);
                dialougue_decision_case = "RO_2_1";
                Obj12_RO_Manager.EnableSubmitButtonRO2_Obj12();
                ////forwarding ro
                //FindObjectOfType<timeline_new>().load_next();
                break;

            case "obj_12_now_you_learnt":
                enable_panel(chefConversationPanel);
                chef_ConversationText.GetComponent<TEXDraw>().text = " Now, you have learnt what an improper fraction looks like. Lets learn more about Improper fractions.";
                break;

            case "obj_12_we_learnt_frac_made":
                enable_panel(chefConversationPanel);
                chef_ConversationText.GetComponent<TEXDraw>().text = " We learnt that a fraction is made up of one or many unit fractions.";
                break;

            case "obj_12_is_five_one_four":
               disable_panel(chefConversationPanel,0.5f);
                yes_no_true_false_panel.SetActive(true);
                set_yes_no_true_false("Is the following statement true?\nFive \\frac{1}{4} will result in \\frac{5}{4} ",true);
                dialougue_decision_case = "LO3_yes_no";
                break;

            case "obj_12_is_five_one_four_where_Each":
                dialougue_decision_case = "";
                enable_panel(chefConversationPanel);
                yes_no_true_false_panel.SetActive(false);
                LO3_5by4_animation.SetActive(true);
                disable_drag();
                chef_ConversationText.GetComponent<TEXDraw>().text = "Here, \\frac{5}{4} indicates 5 slices of pizzas, where each slice is of the size \\frac{1}{4}";
                break;

            case "obj_12_combine_these":
                LO3_5by4_animation.SetActive(false);
                disable_panel(chefConversationPanel, 0.5f);
                FindObjectOfType<conversationManager>().EnableQuestion("Combine these slices to form a full pizza.");
                enable_drag();
               // LO3_5by4.transform.GetChild(3).gameObject.SetActive(false);
                LO3_5by4.SetActive(true);
                lo3_pizza_inbox.SetActive(true);
                lo3_pizza_inbox_1.SetActive(true);
                submit_lo3.gameObject.SetActive(true);
                break;

            case "obj_12_as_you_can_see":
                LO3_5by4.SetActive(false);
                LO3_5by4_fixed.SetActive(true);
                FindObjectOfType<conversationManager>().DisableQuestion();
                enable_panel(chefConversationPanel);
                yes_no_true_false_panel.SetActive(false);
                chef_ConversationText.GetComponent<TEXDraw>().text = "As you can see, \\frac{5}{4} means that there is one full pizza cut into 4 parts and one part remaining from another pizza which is also of the same size. Hence, the value of an improper fraction is more than 1.";
                break;

            case "obj_12_which_of_the_foll_is_imp":
                disable_panel(chefConversationPanel,0.5f);
                LO3_5by4_fixed.SetActive(false);
                Obj12_RO_3.SetActive(true);
                dialougue_decision_case = "RO_3_1";
                Obj12_RO_Manager.EnableSubmitButtonRO3_Obj12();
                ////forwarding ro
                //FindObjectOfType<timeline_new>().load_next();
                break;

            case "obj_12_4b4is_the_rep":
                Obj12_RO_3.SetActive(false);
                full_pizza_four_parts.SetActive(true);
                yes_no_true_false_panel.SetActive(true);
                dialougue_decision_case = "LO4_true_false";
                set_yes_no_true_false("\\frac{4}{4} is the representation of the amount of pizza shown here", false);
                
                break;

            case "obj_12_such_fract_that_have":
                full_pizza_four_parts.transform.GetChild(0).gameObject.SetActive(true);
                yes_no_true_false_panel.SetActive(false);
                enable_panel(chefConversationPanel);
                chef_ConversationText.GetComponent<TEXDraw>().text = "Such fractions that have the same numerator and denominator are also called Improper fractions.";
                break;
                    
            case "obj_12_what_type_6b6":
                full_pizza_four_parts.transform.GetChild(0).gameObject.SetActive(false);
                full_pizza_four_parts.SetActive(false);
                disable_panel(chefConversationPanel, 0.5f);
                Obj12_RO_4_1.SetActive(true);
                Obj12_RO_Manager.EnableSubmitButtonRO4_1Obj12();
                dialougue_decision_case ="RO_4_1";
                ////forwarding ro
                //FindObjectOfType<timeline_new>().load_next();
                break;


            case "obj_12_lets_now_summarized":
                dialougue_decision_case = "";
                Obj12_RO_4_1.SetActive(false);
                enable_panel(chefConversationPanel);
                chef_ConversationText.GetComponent<TEXDraw>().text = "Let's now summarise what are mixed fractions and improper fractions. ";
                break;

            case "obj_12_mixed_fraction_can_be":
                mixed_fraction_explain.SetActive(true);
                enable_panel(chefConversationPanel);
                chef_ConversationText.GetComponent<TEXDraw>().text = "Mixed fraction can be expressed as a combination of a whole number and a proper fraction";
                break;

           case "obj_12_improper_fraction_can_be":
                improper_fraction_explain.SetActive(true);
                enable_panel(chefConversationPanel);
                chef_ConversationText.GetComponent<TEXDraw>().text = "Improper fraction can be expressed as \\frac{numerator}{denominator} where numerator is greater than denominator.";
                break;

            case "obj_12_mixed_frac_tells_you":
                mixed_fraction_explain.SetActive(false);
                improper_fraction_explain.SetActive(false);
                one_one_by_four.SetActive(true);
                enable_panel(chefConversationPanel);
                chef_ConversationText.GetComponent<TEXDraw>().text = "Mixed fraction precisely tells you about how many wholes and how many part of a whole are there in the fraction.";
                break;

            case "obj_12_improper_frac_tells_you":
                enable_panel(chefConversationPanel);
                five_by_four.SetActive(true);
                chef_ConversationText.GetComponent<TEXDraw>().text = "Whereas,  improper fraction tells you about what is the total number of parts we are considering and how many parts are there in a whole. ";
                break;

            case "obj_12_which_of_the_following":
                
                one_one_by_four.SetActive(false);
                five_by_four.SetActive(false);
                disable_panel(chefConversationPanel, 0.5f);
                Obj12_RO_5_1.SetActive(true);
                Obj12_RO_Manager.EnableSubmitButtonRO5_1Obj12();
                dialougue_decision_case = "RO_5_1";
                ////forwarding ro
                //FindObjectOfType<timeline_new>().load_next();
                break;

            case "obj_12_these_pizzas_can":
                Obj12_RO_5_1.SetActive(false);
                enable_panel(chefConversationPanel);
                chef_ConversationText.GetComponent<TEXDraw>().text = " These pizzas can both be expressed as an improper fraction as well as a mixed fraction.";
                both11by4_5by4.SetActive(true);
                break;

            case "obj_12_both_improper_fraction":
                enable_panel(chefConversationPanel);
                chef_ConversationText.GetComponent<TEXDraw>().text = "  Both improper fractions and mixed fractions are used to measure values more than one. The only difference between them is the way they are written. ";
                break;

            case "obj_12_Which_following_statement":
                disable_panel(chefConversationPanel,0.5f);
                both11by4_5by4.SetActive(false);
                Obj12_RO_6_1.SetActive(true);
                Obj12_RO_Manager.EnableSubmitButtonRO6_1Obj12();
                dialougue_decision_case = "RO_6_1";

                break;




            case "obj_12_one_one_by_four_same":
                Obj12_RO_6_1.SetActive(false);
                enable_panel(chefConversationPanel);
                chef_ConversationText.GetComponent<TEXDraw>().text = " 1 \\frac{1}{4} Pizza is same as \\frac{5}{4} Pizza ";
                break;


            case "obj_12_mixed_and_improper_can_be":
                enable_panel(chefConversationPanel);
                both11by4_5by4.SetActive(false);
                number_line.SetActive(true);
                chef_ConversationText.GetComponent<TEXDraw>().text = "Mixed fractions and Improper fractions can be represented on a number line too. Let's see how \n Lets take a fraction 1 \\frac{3}{4} ";
                numberline3b4.SetActive(true);
                break;

        

            case "obj_12_since_one_is_less":
                line_1.SetActive(true);
                enable_panel(chefConversationPanel);
                chef_ConversationText.GetComponent<TEXDraw>().text = "Since 1 is less than 2, 1 \\frac{3}{4} is less than 2. Therefore, we will draw a number line from 0 to 2. ";
                break;

            case "obj_12_since_the_fraction_is":
                line_2.SetActive(true);
                line_2.transform.GetChild(0).gameObject.SetActive(true);
                enable_panel(chefConversationPanel);
                chef_ConversationText.GetComponent<TEXDraw>().text = "Since the fraction part of this fraction is \\frac{3}{4}, we will divide each whole number part into 4 parts as the denominator is 4 ";
                break;

            case "obj_12_we_then_label":
                line_4.SetActive(true);
                line_4.transform.GetChild(7).GetChild(1).gameObject.SetActive(true);
                line_2.transform.GetChild(0).gameObject.SetActive(false);
                enable_panel(chefConversationPanel);
                chef_ConversationText.GetComponent<TEXDraw>().text = "We then label each point with a fraction and identify the correct fraction ";
                numberline3b4.SetActive(true);
               
                break;

            case "obj_12_the_same_number":
                numberline3b4.SetActive(false);
                line_4.SetActive(false);
                line_3.SetActive(true);
                enable_panel(chefConversationPanel);
                chef_ConversationText.GetComponent<TEXDraw>().text = "The same number line can be used to represent improper fraction";
                break;

            case "obj_12_if_we_simply_count":
                line_3.GetComponent<Animator>().Play("number line last");
                enable_panel(chefConversationPanel);
                chef_ConversationText.GetComponent<TEXDraw>().text = "If we replace the point next to \\frac{4}{4} as \\frac{5}{4} instead of 1 \\frac{1}{4}.And 1\\frac{2}{4} can be considered as \\frac{6}{4} and so on, we can express improper fractions too on a number line ";
                break;

            case "obj_12_move_the_pointer":
                line_3.SetActive(false);
                numberline3b4.SetActive(false);
                number_line.SetActive(false);
                engagement_number_line.SetActive(true);
                enable_panel(chefConversationPanel);
                chef_ConversationText.GetComponent<TEXDraw>().text = "Move the pointer on any one of the number line and observe how a mixed fraction and an improper fraction share the same point on the number lines.";
                break;
            case "obj_12_which_correct_11b3":
                number_line.SetActive(false);
                engagement_number_line.SetActive(false);
                disable_panel(chefConversationPanel,0.5f);
                Obj12_RO_6_2.SetActive(true);
                Obj12_RO_Manager.EnableSubmitButtonRO6_2Obj12();
                dialougue_decision_case = "RO_6_1";
                break;

            case "obj_12_which_correct_8b7":
                Obj12_RO_6_2.SetActive(false);
                Obj12_RO_6_3.SetActive(true);
                Obj12_RO_Manager.EnableSubmitButtonRO6_3Obj12();
                dialougue_decision_case = "RO_6_3";
                break;


        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            FindObjectOfType<timeline_new>().load_next();
        }
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
        CancelInvoke();
        FindObjectOfType<Obj12_RO_Manager>().CancelInvoke();

        FindObjectOfType<timeline_new>().lipsync_player.GetComponent<AudioSource>().Stop();
        if (dialougue_image.activeSelf)
        {
            dialougue_image.SetActive(false);
        }
        Dialouge_panel.SetActive(false);
        if (dialougue_decision_case.Equals("LO1_yes_no"))
        {
            yes_no_true_false_panel.SetActive(false);
            dialougue_decision_case = "";
            FindObjectOfType<timeline_new>().load_next();
        }
        else if (dialougue_decision_case.Equals("LO4_true_false")|| dialougue_decision_case.Equals("LO3_yes_no"))
        {
            yes_no_true_false_panel.SetActive(false);
            dialougue_decision_case = "";
         
            FindObjectOfType<timeline_new>().load_next();
        }
        else if (dialougue_decision_case.Equals("RO_1_1") || dialougue_decision_case.Equals("RO_2_1")||dialougue_decision_case.Equals("RO_3_1")|| dialougue_decision_case.Equals("RO_4_1") || dialougue_decision_case.Equals("RO_4_1")||dialougue_decision_case.Equals("RO_5_1") ||dialougue_decision_case.Equals("RO_6_1"))                                         
        {
            ro_3_animation.SetActive(false);
            Obj12_RO_1.SetActive(false);
            Obj12_RO_1_2.SetActive(false);
            Obj12_RO_2.SetActive(false);
            Obj12_RO_3.SetActive(false);
            Obj12_RO_4_1.SetActive(false);
            Obj12_RO_5_1.SetActive(false);
            Obj12_RO_6_1.SetActive(false);
            Obj12_RO_6_2.SetActive(false);
            Obj12_RO_6_3.SetActive(false);
            dialougue_ro5_1.SetActive(false);
            dialougue_ro5_2.SetActive(false);
            dialougue_ro6_2.SetActive(false);
            dialougue_ro6_3.SetActive(false);
            dialougue_ro6_1_abcd.SetActive(false);
            dialougue_decision_case = "";
            FindObjectOfType<timeline_new>().load_next();
        }
        else if ( dialougue_decision_case.Equals("RO_6_3"))
        {
            Obj12_RO_1.SetActive(false);
            Obj12_RO_1_2.SetActive(false);
            Obj12_RO_2.SetActive(false);
            Obj12_RO_3.SetActive(false);
            Obj12_RO_4_1.SetActive(false);
            Obj12_RO_5_1.SetActive(false);
            Obj12_RO_6_1.SetActive(false);
            Obj12_RO_6_2.SetActive(false);
            Obj12_RO_6_3.SetActive(false);
            dialougue_ro6_2.SetActive(false);
            dialougue_ro6_3.SetActive(false);
            dialougue_ro6_1_abcd.SetActive(false);
            dialougue_decision_case = "";
            GameObject.FindObjectOfType<GameManager>().OnGameOver();
            //Exit_Panel.SetActive(true);
        }
        dialougue_decision_case = "";
    }


    public void drag_pizza_validation()
    {
        submit_2b6.gameObject.SetActive(false);
        if (full_pizza && pizza_2b6)
        {
            if (pizza_2b6_count == 2)
            {
                Debug.Log("done");
                playCorrect();
                StartCoroutine(completed_activity());
            }
            else
            {
                playError();
                if (hint_count == 0)
                {

                    StartCoroutine(hint_0());
                }
                else if (hint_count == 1)
                {
                    StartCoroutine(hint_1());
                }
                else if (hint_count == 2)
                {
                    StartCoroutine(hint_2());
                }
            }
        }
        else
        {
            playError();
            if (hint_count == 0)
            {
                StartCoroutine(hint_0());
            }
            else if (hint_count == 1)
            {
                StartCoroutine(hint_1());
            }
            else if (hint_count == 2)
            {
                StartCoroutine(hint_2());
            }
        }
    }


    void reset_pizza_2b6()
    {
        pizza_2b6_count = 0;
        for (int i = 0; i < plate_2b6.transform.childCount; i++)
        {
           
            plate_2b6.transform.GetChild(i).position = plate_2b6.transform.GetChild(i).GetComponent<drag_drop_obj_12>().pos;
            plate_2b6.transform.GetChild(i).GetComponent<drag_drop_obj_12>().enabled = true;
            plate_2b6.transform.GetChild(i).GetComponent<Collider2D>().enabled = true;
            plate_2b6.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = true;
        }
        var d = GameObject.FindGameObjectsWithTag("Pizza_Box");
        foreach (GameObject s in d)
        {
            s.GetComponent<Collider2D>().enabled=true;
        }

        submit_2b6.gameObject.SetActive(true);
    }
    void reset_full_pizza()
    {
        plate_full.transform.GetChild(0).gameObject.SetActive(true);
      
        plate_full.transform.GetChild(0).position = plate_full.transform.GetChild(0).GetComponent<drag_drop_obj_12>().pos;
        plate_full.transform.GetChild(0).GetComponent<drag_drop_obj_12>().enabled = true;
        plate_full.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        plate_full.transform.GetChild(0).GetComponent<Collider2D>().enabled = true;
        var d = GameObject.FindGameObjectsWithTag("Pizza_Box");
        foreach (GameObject s in d)
        {
            s.GetComponent<Collider2D>().enabled = true;
        }

        submit_2b6.gameObject.SetActive(true);
    }


    IEnumerator completed_activity()
    {
        disable_drag();
        yield return new WaitForSeconds(2);
        var d = GameObject.FindGameObjectsWithTag("Pizza_Box");
        foreach (GameObject s in d)
        {
            s.GetComponent<Animator>().enabled = true;
        }
        yield return new WaitForSeconds(0.9f);
        for (int i = 0; i < plate_2b6.transform.childCount; i++)
        {if(plate_full.transform.GetChild(0).position!= plate_full.transform.GetChild(0).GetComponent<drag_drop_obj_12>().pos)
            plate_full.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;

            if (plate_2b6.transform.GetChild(i).position != plate_2b6.transform.GetChild(i).GetComponent<drag_drop_obj_12>().pos)
                plate_2b6.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = false;
        }
        yield return new WaitForSeconds(1f);

        one_two_by_six.gameObject.SetActive(false);
        FindObjectOfType<timeline_new>().load_next();
    }

    IEnumerator hint_0()
    {
        disable_drag();
        hint_count++;
        one_two_by_six.transform.GetChild(2).GetComponent<Collider2D>().enabled = false;
        one_two_by_six.transform.GetChild(3).GetComponent<Collider2D>().enabled = false;
        enable_panel(chefConversationPanel);
        chef_ConversationText.GetComponent<TEXDraw>().text = "The customer wants \\frac{2}{6} of a pizza. Ensure that the fraction and the pizza packed match. ";
        FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_12_scentence_3_hint1.wav");
       
        yield return new WaitForSeconds(0.6f);

        if (!Application.isEditor)
            yield return new WaitForSeconds(FindObjectOfType<timeline_new>().length_of_audio);
        else
            yield return new WaitForSeconds(4);

        disable_panel(chefConversationPanel,0.5f);
        FindObjectOfType<conversationManager>().EnableQuestion("Go ahead and pack the pizzas. Pack a whole pizza and \\frac{2}{6} of a pizza in separate boxes.");
        one_two_by_six.transform.GetChild(2).GetComponent<Collider2D>().enabled = true;
        one_two_by_six.transform.GetChild(3).GetComponent<Collider2D>().enabled = true;
        reset_pizza_2b6();
        reset_full_pizza();
        enable_drag();
    }

    IEnumerator hint_1()
    {
        disable_drag();
        hint_count++;
        one_two_by_six.transform.GetChild(2).GetComponent<Collider2D>().enabled = false;
        one_two_by_six.transform.GetChild(3).GetComponent<Collider2D>().enabled = false;
        enable_panel(chefConversationPanel);
        chef_ConversationText.GetComponent<TEXDraw>().text = "The number of pizza slices to be packed should be according to the fraction given. Check again. ";
        FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_12_scentence_3_hint2.wav");

        yield return new WaitForSeconds(0.6f);

        if (!Application.isEditor)
            yield return new WaitForSeconds(FindObjectOfType<timeline_new>().length_of_audio);
        else
            yield return new WaitForSeconds(4);

        disable_panel(chefConversationPanel, 0.5f);
        FindObjectOfType<conversationManager>().EnableQuestion("Go ahead and pack the pizzas. Pack a whole pizza and \\frac{2}{6} of a pizza in separate boxes.");
        one_two_by_six.transform.GetChild(2).GetComponent<Collider2D>().enabled = true;
        one_two_by_six.transform.GetChild(3).GetComponent<Collider2D>().enabled = true;
        reset_pizza_2b6();
        reset_full_pizza();
        enable_drag();
    }

    IEnumerator hint_2()
    {
        disable_drag();
        hint_count = 0;

        one_two_by_six.transform.GetChild(2).GetComponent<Collider2D>().enabled = false;
        one_two_by_six.transform.GetChild(3).GetComponent<Collider2D>().enabled = false;
        enable_panel(chefConversationPanel);
        //chef_ConversationText.GetComponent<TEXDraw>().text = " Traverse to LO 1 of Objective 4";
       
        yield return new WaitForSeconds(0.6f);

        if (!Application.isEditor)
            yield return new WaitForSeconds(4);
        else
            yield return new WaitForSeconds(4);

        disable_panel(chefConversationPanel, 0.5f);
        FindObjectOfType<conversationManager>().EnableQuestion("Go ahead and pack the pizzas. Pack a whole pizza and \\frac{2}{6} of a pizza in separate boxes.");
        one_two_by_six.transform.GetChild(2).GetComponent<Collider2D>().enabled = true;
        one_two_by_six.transform.GetChild(3).GetComponent<Collider2D>().enabled = true;
        reset_pizza_2b6();
        reset_full_pizza();
        enable_drag();
    }



    void enable_drag()
    {
        first_dragged_id = "";
        var s = FindObjectsOfType<drag_drop_obj_12>();
        foreach (drag_drop_obj_12 tst in s)
        {
            tst.enabled = true;
        }
    }
    void disable_drag()
    {
        first_dragged_id = "";
        var s = FindObjectsOfType<drag_drop_obj_12>();
        foreach (drag_drop_obj_12 tst in s)
        {
            tst.enabled = false;
        }
    }





    void set_yes_no_true_false(string question,bool yes_no)
    {
        yes_no_true_false_panel.SetActive(true);
        yes_button.gameObject.SetActive(true);
        no_button.gameObject.SetActive(true);
        yes_no_quest_text.GetComponent<TEXDraw>().text = question;
        if (yes_no)
        {
            yes_button.GetComponentInChildren<Text>().text = "YES";
            no_button.GetComponentInChildren<Text>().text = "NO";
        }
        else
        {
            if (dialougue_decision_case.Equals("LO4_true_false"))
            {
                yes_button.GetComponent<RectTransform>().localPosition = new Vector3(-413, -363, 0);
                yes_button.GetComponent<RectTransform>().sizeDelta = new Vector2(800, 150);

                no_button.GetComponent<RectTransform>().localPosition = new Vector3(413, -363, 0);
                no_button.GetComponent<RectTransform>().sizeDelta = new Vector2(800, 150);

                yes_button.GetComponentInChildren<Text>().text = "True, because there are 4 slices";
                no_button.GetComponentInChildren<Text>().text = "False, because there is only 1 pizza";
            }
        else
            {
                yes_button.GetComponent<RectTransform>().localPosition = new Vector3(-213, -363, 0);
                yes_button.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 100);

                no_button.GetComponent<RectTransform>().localPosition = new Vector3(213, -363, 0);
                no_button.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 100);
                yes_button.GetComponentInChildren<Text>().text = "TRUE";
                no_button.GetComponentInChildren<Text>().text = "FALSE";
            }
           
        }
    }
    void validate_yes_true()
    {
        switch (dialougue_decision_case)
        {
            case "LO1_yes_no":
                yes_button.gameObject.SetActive(false);
                no_button.gameObject.SetActive(false);
                enable_panel(chefConversationPanel);
                set_conversation_msg("You are right.We can represent objects more than a whole as fractions too. Lets see how." );
               // set_dialougue("You are right. We can represent objects more than a whole as fractions too. Lets see how. ");
                FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_12_lo_1_yes.wav");
                Invoke("load_next", 6.5f);

            break;
            case "LO3_yes_no":

                FindObjectOfType<timeline_new>().load_next();

                break;

            case "LO4_true_false":
                yes_button.gameObject.SetActive(false);
                no_button.gameObject.SetActive(false);

                FindObjectOfType<timeline_new>().load_next();
                yes_no_true_false_panel.SetActive(false);
                break;
        }
    }

    void validate_no_false()
    {
        switch (dialougue_decision_case)
        {
            case "LO1_yes_no":
                yes_button.gameObject.SetActive(false);
                no_button.gameObject.SetActive(false);
                enable_panel(chefConversationPanel);
                set_conversation_msg("We can represent objects more than a whole as fractions too. Lets see how. ") ;
//                set_dialougue("We can represent objects more than a whole as fractions too. Lets see how. ");
                FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_12_lo_1_no.wav");
                Invoke("load_next", 6f);
                break;

            case "LO3_yes_no":

                yes_button.gameObject.SetActive(false);
                no_button.gameObject.SetActive(false);
                enable_panel(chefConversationPanel);
                set_conversation_msg("A group of unit fractions added will result in another fraction ");
//                set_dialougue("A group of unit fractions added will result in another fraction + Traverse to Obj 11 ");
                FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_12_lo_3_no.wav");
                Invoke("load_next1", 5f);
                break;


            case "LO4_true_false":
                yes_button.gameObject.SetActive(false);
                no_button.gameObject.SetActive(false);
                enable_panel(chefConversationPanel);
                set_conversation_msg("Fractions with same numerator and denominator is equal 1. Here, all 4 parts in the pizza are present. Hence, we can represent it as both \\frac{4}{4} and 1");
                // set_dialougue("Fractions with same numerator and denominator is equal 1. Here, all 4 parts in the pizza are present. Hence, we can represent it as both \\frac{4}{4} and 1 ");
                FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_12_lo_4_false.wav");
                Invoke("load_next2", 12f);
                break;

        }
    }

    void validate_lo3_submit()
    {

        if (lo3_pizza_count == 4||lo3_pizza_count2==4)
        {
            submit_lo3.gameObject.SetActive(false);
            var e = LO3_5by4.GetComponentsInChildren<drag_drop_obj_12>();
            foreach (drag_drop_obj_12 d in e)
            {
                d.enabled = false;
                d.GetComponent<Collider2D>().enabled = false;
            }
            FindObjectOfType<timeline_new>().load_next();
        }
        else
        {
            if (lo3_hint == 0)
            {
                StartCoroutine(lo3_hint1());
            }
        else
            {
                StartCoroutine(lo3_hint2());
            }
           
            


            lo3_pizza_count = 0;
            lo3_pizza_count2 = 0;
        }
    }

    public IEnumerator lo3_hint1()
    {

        submit_lo3.gameObject.SetActive(false);
        enable_panel(chefConversationPanel);
        chef_ConversationText.GetComponent<TEXDraw>().text = "Try to place the slices in such a way that it forms a full pizza.";
        FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_12_lo_3_drag_slice.wav");

        disable_drag();
        if (!Application.isEditor)
           yield return new WaitForSeconds (FindObjectOfType<timeline_new>().length_of_audio);
        else
            yield return new WaitForSeconds(4);

       
        lo3_pizza_inbox.transform.GetChild(0).gameObject.SetActive(false);
        lo3_pizza_inbox.transform.GetChild(1).gameObject.SetActive(false);
        lo3_pizza_inbox.transform.GetChild(2).gameObject.SetActive(false);
        lo3_pizza_inbox.transform.GetChild(3).gameObject.SetActive(false);
        lo3_pizza_inbox_1.transform.GetChild(0).gameObject.SetActive(false);
        lo3_pizza_inbox_1.transform.GetChild(1).gameObject.SetActive(false);
        lo3_pizza_inbox_1.transform.GetChild(2).gameObject.SetActive(false);
        lo3_pizza_inbox_1.transform.GetChild(3).gameObject.SetActive(false);
        var e = LO3_5by4.GetComponentsInChildren<drag_drop_obj_12>();
        foreach (drag_drop_obj_12 d in e)
        {
            d.transform.position = d.pos;
            d.enabled = true;
            d.GetComponent<Collider2D>().enabled = true;
            d.GetComponent<SpriteRenderer>().enabled = true;
            pizza_2b6_count = 0;

        }
        enable_drag();
        lo3_hint++;
        submit_lo3.gameObject.SetActive(true);
        disable_panel(chefConversationPanel, 0.5f);
    }


    public IEnumerator lo3_hint2()
    {
        submit_lo3.gameObject.SetActive(false);
        disable_drag();
        lo3_pizza_inbox.transform.GetChild(0).gameObject.SetActive(true);
        lo3_pizza_inbox.transform.GetChild(1).gameObject.SetActive(true);
        lo3_pizza_inbox.transform.GetChild(2).gameObject.SetActive(true);
        lo3_pizza_inbox.transform.GetChild(3).gameObject.SetActive(true);
        lo3_pizza_inbox_1.transform.GetChild(0).gameObject.SetActive(true);
        lo3_pizza_inbox_1.transform.GetChild(1).gameObject.SetActive(false);
        lo3_pizza_inbox_1.transform.GetChild(2).gameObject.SetActive(false);
        lo3_pizza_inbox_1.transform.GetChild(3).gameObject.SetActive(false);
        var e = LO3_5by4.GetComponentsInChildren<drag_drop_obj_12>();
        GameObject dty = null;
        foreach (drag_drop_obj_12 d in e)
        {
            d.transform.position = d.pos;
            // d.enabled = false;
            d.GetComponent<Collider2D>().enabled = false;
            d.GetComponent<SpriteRenderer>().enabled = false;
            pizza_2b6_count = 0;
            //dty = d.gameObject;
        }
        //LO3_5by4.GetComponentInChildren<drag_drop_obj_12>().GetComponent<SpriteRenderer>().enabled = true;
        enable_panel(chefConversationPanel);


        chef_ConversationText.GetComponent<TEXDraw>().text = "Now we have a pizza that is full and cut into 4 slices and one slice of \\frac{1}{4}. ";
        FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_12_lo_3_drag_slice_2.wav");
        if (!Application.isEditor)
            yield return new WaitForSeconds(FindObjectOfType<timeline_new>().length_of_audio);
        else
            yield return new WaitForSeconds(4);
        submit_lo3.gameObject.SetActive(false);
        yield return new WaitForSeconds(2);
        load_next();
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
