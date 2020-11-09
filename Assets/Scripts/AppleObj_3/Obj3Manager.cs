using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Obj3Manager : MonoBehaviour
{
    string jsonFileName = "Obj3NumDenum_json.json";
    public GameObject RO_Panel, RO_qtype3, RO_qtype3_text, RO_qtype3_op_a, RO_qtype3_op_b, RO_qtype3_op_c, RO_qtype3_op_d, RO_qtype3_op_e, RO_qtype3_op_f, Dialouge_text, Dialouge_panel, RO_qtype_3number_pnael, ROqtype_3_fractionInput_panel, apple_panel, TRAYS_POSITION, FRACTIONS_POSITION, dialougue_image, textual_fraction_panel, textual_num_text, textual_line, textual_denum_text, text_numer, text_denom, single_slots_apple, LO4_Panel, exit_panel, drag_drop_panel;
    public Sprite ro1_q1img_b, ro1_q1img_c, ro1_q2img_b, ro1_q2img_c;
    public string ans, current_ques, num, deno;
    public Image imga, imgb, imgc, imgd, imge, imgf, ro_question_image;
    public Button submit_button, exit_button, dialougue_ok_button;
    public Sprite apple_complete, filled_sprite;
    public Transform center_pos;
    public string condition;
    public bool isNum, isDenum, num_dragged, denum_dragged;
    public GameObject temp;
    public GameObject number_of_apples, total_number_of_slots, amount_of_case_that_is;
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
        Initialised();

    }
    //GameObject.FindObjectOfType<timeline_new>().load_next();
    // Update is called once per frame

    void HideLoadingAudio()
    {
        LoadingAudio.SetActive(false);
    }
    void Initialised()
    {
        LoadingAudio = GameObject.Find("LoadAudio");
        UtilityArtifacts.scene_to_load_after_canvas = "obj3";
        //if (UtilityArtifacts.backTraversal)
        //{
        //    Text textLoadingText = LoadingAudio.transform.GetChild(1).GetComponent<Text>();
        //    textLoadingText.text = "Let us understand this better";
        //}
        exit_panel = GameObject.Find("Exit Panel");
        exit_button = GameObject.Find("Exit_button").GetComponent<Button>();
        Dialouge_text = GameObject.Find("Dialougue text");
        Dialouge_panel = GameObject.Find("Dialougue Panel");
        dialougue_image = GameObject.Find("Dialougue Image");
        submit_button = GameObject.Find("Submit FOR Button").GetComponentInChildren<Button>();
        dialougue_ok_button = GameObject.Find("dialougue_ok_button").GetComponent<Button>();
        GetallNumberButtons();


        RO_Panel = GameObject.Find("RO Panel");
        RO_qtype3 = GameObject.Find("ROType3");
        RO_qtype3_text = GameObject.Find("ROQuestion");
        RO_qtype3_op_a = GameObject.Find("RO op 1");
        RO_qtype3_op_b = GameObject.Find("RO op 2");
        RO_qtype3_op_c = GameObject.Find("RO op 3");
        RO_qtype3_op_d = GameObject.Find("RO op 4");
        RO_qtype3_op_e = GameObject.Find("RO op 5");
        RO_qtype3_op_f = GameObject.Find("RO op 6");
        ro_question_image = GameObject.Find("ro_question_image").GetComponent<Image>();
        ROqtype_3_fractionInput_panel = GameObject.Find("FractionInputPanel_RO");
        RO_qtype_3number_pnael = GameObject.Find("NumberPanel_RO");
        apple_panel = GameObject.Find("Apple Panel");
        TRAYS_POSITION = GameObject.Find("TARYS POSTION");
        FRACTIONS_POSITION = GameObject.Find("FRACTIONS POSITION");

        textual_fraction_panel = GameObject.Find("Fraction_panel_textual");
        textual_num_text = GameObject.Find("numerator_text");
        textual_denum_text = GameObject.Find("denominator_text");
        text_numer = GameObject.Find("text_numer");
        text_denom = GameObject.Find("text_denom");
        textual_line = GameObject.Find("line textual");
        single_slots_apple = GameObject.Find("single_slots_apple");
        LO4_Panel = GameObject.Find("LO_4_panel");
        drag_drop_panel = GameObject.Find("drag drop panel");
        number_of_apples = GameObject.Find("number_of_apples");
        total_number_of_slots = GameObject.Find("total_number_of_slots");
        amount_of_case_that_is = GameObject.Find("amount_of_case_that_is");

        exit_button.onClick.AddListener(quit_app);
        var a = RO_qtype3_op_a.GetComponentsInChildren<Image>();
        foreach (Image d in a)
        {
            if (d.gameObject.name == "Image")
            {
                imga = d.GetComponent<Image>();
            }
        }
        a = RO_qtype3_op_b.GetComponentsInChildren<Image>();
        foreach (Image d in a)
        {
            if (d.gameObject.name == "Image")
            {
                imgb = d.GetComponent<Image>();
            }
        }
        a = RO_qtype3_op_c.GetComponentsInChildren<Image>();
        foreach (Image d in a)
        {
            if (d.gameObject.name == "Image")
            {
                imgc = d.GetComponent<Image>();
            }
        }
        a = RO_qtype3_op_d.GetComponentsInChildren<Image>();
        foreach (Image d in a)
        {
            if (d.gameObject.name == "Image")
            {
                imgd = d.GetComponent<Image>();
            }
        }
        a = RO_qtype3_op_e.GetComponentsInChildren<Image>();
        foreach (Image d in a)
        {
            if (d.gameObject.name == "Image")
            {
                imge = d.GetComponent<Image>();
            }
        }
        a = RO_qtype3_op_f.GetComponentsInChildren<Image>();
        foreach (Image d in a)
        {
            if (d.gameObject.name == "Image")
            {
                imgf = d.GetComponent<Image>();
            }
        }




        // ROqtype_3_fractionInput_panel.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(ClickOnNumerator);
        // ROqtype_3_fractionInput_panel.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(ClickOnDenominator);
        dialougue_ok_button.onClick.AddListener(dialougue_ok_functionality);

        RO_Panel.SetActive(false);
        RO_qtype3.SetActive(false);
        Dialouge_panel.SetActive(false);
        submit_button.gameObject.SetActive(false);
        textual_fraction_panel.SetActive(false);
        single_slots_apple.SetActive(false);
        LO4_Panel.SetActive(false);
        exit_panel.SetActive(false);
        text_numer.SetActive(false);
        text_denom.SetActive(false);
        drag_drop_panel.SetActive(false);
        number_of_apples.SetActive(false);
        total_number_of_slots.SetActive(false);
        amount_of_case_that_is.SetActive(false);
        Invoke("audio_invoke", 2.0f);
        this.GetComponent<Obj3AppleGenerator>().Initialize();




    }
    void quit_app()
    {
        //Application.Quit();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    void audio_invoke()
    {
        Debug.Log(EVENT.SetUpTimeLine);
        EventManager.setNameOfJasonFile(jsonFileName);
        EventManager.Broadcast(EVENT.SetUpTimeLine);
    }

    void EventToHandle(string EventName)
    {
        HideLoadingAudio();
        switch (EventName)
        {
            case "Obj3_great_you_have":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Great! Now that you have separated the apples that are whole and the ones that are not whole");
                break;

            case "Obj3_thank_you_so_much":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation(" Thank you so much for helping me out. You are right on time. We will be opening the stall shortly. Today we have  pizzas, apple juice and cookie slab on the menu. ");
                break;

            case "Obj3_as_i_bake":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("As I bake the pizzas and make apple juice, why don't you place the apples in this case?  Remember, these cases are made for apples that are whole only.");
                break;

            case "Obj3_quest1":
                GameObject.FindObjectOfType<conversationManager>().EnableQuestion("Can you count the total number of slots in the case and write it in the box ?");
                Invoke("StartQuestion1", 0f);
                break;


            case "Obj3_quest2":
                GameObject.FindObjectOfType<conversationManager>().EnableQuestion("Can you now count all the apples in the case and write it in this box ?");
                Invoke("StartQuestion2", 0f);
                break;


            case "Obj3_LO1_lets_consider":
                GameObject.FindObjectOfType<conversationManager>().DisableQuestion();
                FindObjectOfType<QuestionManager>().DoneButton.gameObject.SetActive(false);

                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Let's consider this case as a whole. As you can see the case has 12 slots out of which 6 slots are filled with apples.");


                break;

            case "Obj3_LO1_now_measure":
                FindObjectOfType<QuestionManager>().DoneButton.gameObject.SetActive(false);
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Now let's measure the part of the case that has apples.");

                break;
            case "Obj3_LO1_as_we_know":
                FindObjectOfType<QuestionManager>().DoneButton.gameObject.SetActive(false);
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("As we know, fraction is a part of a whole.");

                break;
            case "Obj3_LO1_since_the_case":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Since the case is partly filled, we use fractions to measure the amount of case that is filled with apples.");

                break;

            case "Obj3_LO1_number_of_slots":
                amount_of_case_that_is.SetActive(true);
                FindObjectOfType<QuestionManager>().DisableForObj3Quest2();
                number_of_apples.SetActive(false);
                total_number_of_slots.SetActive(false);
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Number of slots that are filled with apples, is the Numerator ");
                apple_panel.transform.position = TRAYS_POSITION.transform.position;
                //  GameObject.Find("FractionInputPanel").transform.position = FRACTIONS_POSITION.transform.position;
                for (int i = 0; i < 6; i++)
                {
                    if (apple_panel.transform.GetChild(0).GetChild(i).childCount == 2)
                    {
                        Destroy(apple_panel.transform.GetChild(0).GetChild(i).GetChild(1).gameObject);
                        Destroy(apple_panel.transform.GetChild(0).GetChild(i).GetChild(0).gameObject);
                    }
                    else if (apple_panel.transform.GetChild(0).GetChild(i).childCount == 1)
                    {
                        Destroy(apple_panel.transform.GetChild(0).GetChild(i).GetChild(0).gameObject);
                    }
                    highlight__obj(apple_panel.transform.GetChild(0).GetChild(i).gameObject);
                }
                textual_fraction_panel.SetActive(true);
                textual_line.SetActive(false);
                textual_num_text.GetComponent<Text>().text = "";
                textual_denum_text.GetComponent<Text>().text = "";
                GameObject.Find("FractionInputPanel").transform.position = textual_fraction_panel.transform.position;
                GameObject.Find("FractionInputPanel").transform.GetChild(0).gameObject.SetActive(true);
                highlight__obj(GameObject.Find("FractionInputPanel").transform.GetChild(0).gameObject);
                GameObject.Find("FractionInputPanel").transform.GetChild(2).gameObject.SetActive(true);
                text_numer.SetActive(true);


                break;

            case "Obj3_LO1_total_no_slots":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("The total number of slots in the case will be the denominator of the fraction.");
                for (int i = 0; i < 6; i++)
                {
                    disable_all_highlights(apple_panel.transform.GetChild(0).GetChild(i).gameObject);
                }
                disable_all_highlights(GameObject.Find("FractionInputPanel").transform.GetChild(0).gameObject);
                highlight__obj(apple_panel.transform.GetChild(0).gameObject);
                highlight__obj(GameObject.Find("FractionInputPanel").transform.GetChild(1).gameObject);
                GameObject.Find("FractionInputPanel").transform.GetChild(1).gameObject.SetActive(true);
                textual_denum_text.GetComponent<Text>().text = "";
                text_denom.SetActive(true);
                break;


            case "Obj3_LO1_therefore_we_can":
                amount_of_case_that_is.SetActive(false);
                apple_panel.SetActive(false);
                disable_all_highlights(apple_panel.transform.GetChild(0).gameObject);
                disable_all_highlights(GameObject.Find("FractionInputPanel").transform.GetChild(1).gameObject);

                GameObject.Find("FractionInputPanel").transform.GetChild(0).gameObject.SetActive(false);
                GameObject.Find("FractionInputPanel").transform.GetChild(1).gameObject.SetActive(false);
                GameObject.Find("FractionInputPanel").transform.GetChild(2).gameObject.SetActive(false);
                textual_fraction_panel.transform.position = GameObject.Find("center").transform.position;
                textual_num_text.GetComponent<Text>().text = "The number of parts considered";
                textual_line.SetActive(true);
                textual_denum_text.GetComponent<Text>().text = "";
                text_numer.SetActive(true);
                text_denom.SetActive(false);

                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Therefore we can say that Numerator is the number of parts considered.");
                //disable_all_highlights(apple_panel.transform.GetChild(0).gameObject);
                //disable_all_highlights(GameObject.Find("FractionInputPanel").transform.GetChild(1).gameObject);
                //for (int i = 0; i < 6; i++)
                //{

                //    highlight__obj(apple_panel.transform.GetChild(0).GetChild(i).gameObject);
                //}
                //highlight__obj(GameObject.Find("FractionInputPanel").transform.GetChild(0).gameObject);
                break;


            case "Obj3_LO1_and_the_denominator":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Denominator is the total number of equal parts in a whole.");
                textual_num_text.GetComponent<Text>().text = "The number of parts considered";
                textual_denum_text.GetComponent<Text>().text = "The total number of equal parts in a whole";
                text_numer.SetActive(true);
                text_denom.SetActive(true);
                //for (int i = 0; i < 6; i++)
                //{
                //    disable_all_highlights(apple_panel.transform.GetChild(0).GetChild(i).gameObject);
                //}
                //disable_all_highlights(GameObject.Find("FractionInputPanel").transform.GetChild(0).gameObject);

                //highlight__obj(apple_panel.transform.GetChild(0).gameObject);
                //highlight__obj(GameObject.Find("FractionInputPanel").transform.GetChild(1).gameObject);

                break;

            case "Obj3_RO1_can_you_tell":

                text_numer.SetActive(false);
                text_denom.SetActive(false);
                textual_fraction_panel.SetActive(false);
                //   disable_all_highlights(apple_panel.transform.GetChild(0).gameObject);
                //   disable_all_highlights(GameObject.Find("FractionInputPanel").transform.GetChild(1).gameObject);

                GameObject.FindObjectOfType<conversationManager>().DisableConversation();
                RO_Panel.SetActive(true);
                RO_qtype3.SetActive(true);
                setup_ro_question_options_text("Can you tell, which of the following represents a numerator here?", 4, "            Number of parts taken from the whole", "            Number of apples placed in the case", "            Number written above the line", "            All of the above", "", "", null, ro1_q1img_b, ro1_q1img_c, null, null, null, null);
                ans = "";
                submit_button.gameObject.SetActive(true);
                current_ques = "Obj3_RO1_q1";
                submit_button.onClick.AddListener(check_answers);

                break;

            case "Obj3_RO1_which_of_the_following":

                RO_Panel.SetActive(true);
                RO_qtype3.SetActive(true);
                RO_qtype3_op_a.transform.parent.GetComponent<GridLayoutGroup>().cellSize = new Vector2(1100f, 150f);
                setup_ro_question_options_text("Which of the following represents a denominator?", 4, "            Number of equal parts the whole is divided into", "            Number of slots in the case", "            Number written below the line", "            All of the above", "", "", null, ro1_q2img_b, ro1_q2img_c, null, null, null, null);
                ans = "";
                submit_button.gameObject.SetActive(true);
                current_ques = "Obj3_RO1_q2";


                break;

            case "Obj3_LO2_remeber_when":
                apple_panel.SetActive(false);
                //  GameObject.Find("FractionInputPanel").transform.GetChild(0).gameObject.SetActive(true);
                //  GameObject.Find("FractionInputPanel").transform.GetChild(1).gameObject.SetActive(true);
                //   GameObject.Find("FractionInputPanel").transform.GetChild(2).gameObject.SetActive(true);


                //  GameObject.Find("FractionInputPanel").transform.position = GameObject.Find("center").transform.position;
                RO_Panel.SetActive(false);
                RO_qtype3.SetActive(false);
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Remember,  When we write a fraction ");

                break;
            case "Obj3_LO2_the_numerator_is":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("The numerator is the number written above the line ");

                GameObject.Find("FractionInputPanel").transform.GetChild(0).gameObject.SetActive(true);
                GameObject.Find("FractionInputPanel").transform.GetChild(2).gameObject.SetActive(true);
                GameObject.Find("FractionInputPanel").transform.GetChild(1).gameObject.SetActive(true);

                GameObject.Find("FractionInputPanel").transform.position = GameObject.Find("center").transform.position;

                textual_fraction_panel.SetActive(true);
                textual_line.SetActive(false);
                textual_line.GetComponent<RectTransform>().sizeDelta = new Vector2(400f, 14f);
                //  textual_fraction_panel.transform.position = GameObject.Find("FRACTIONS POSITION").transform.position;
                textual_num_text.GetComponent<Text>().text = "";
                textual_denum_text.GetComponent<Text>().text = "";
                text_numer.SetActive(true);
                text_denom.SetActive(true);
                break;
            case "Obj3_LO2_the_denominator_is":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("The denominator is the number written below the line");
                //  disable_all_highlights(GameObject.Find("FractionInputPanel").transform.GetChild(0).gameObject);
                textual_num_text.GetComponent<Text>().text = "";
                textual_denum_text.GetComponent<Text>().text = "";
                GameObject.Find("FractionInputPanel").transform.GetChild(0).gameObject.SetActive(true);
                GameObject.Find("FractionInputPanel").transform.GetChild(2).gameObject.SetActive(true);
                GameObject.Find("FractionInputPanel").transform.GetChild(1).gameObject.SetActive(true);


                //highlight__obj(GameObject.Find("FractionInputPanel").transform.GetChild(1).gameObject);

                break;
            case "Obj3_RO2_can_you_label":
                textual_fraction_panel.SetActive(false);
                GameObject.FindObjectOfType<conversationManager>().DisableConversation();
                // disable_all_highlights(GameObject.Find("FractionInputPanel").transform.GetChild(1).gameObject);
                reset_options();
                RO_Panel.SetActive(true);
                RO_qtype3.SetActive(true);
                // RO_qtype_3number_pnael.SetActive(true);
                drag_drop_panel.SetActive(true);
                ROqtype_3_fractionInput_panel.SetActive(true);
                RO_qtype3_op_a.transform.parent.GetComponent<GridLayoutGroup>().cellSize = new Vector2(1100f, 150f);
                setup_ro_question_options_text("Label the fraction as Numerator and Denominator", 0, "", "", "", "", "", "", null, null, null, null, null, null, null);
                ans = "";
                ROqtype_3_fractionInput_panel.SetActive(true);
                ROqtype_3_fractionInput_panel.transform.GetChild(0).gameObject.GetComponentInChildren<Text>().text = "1";
                ROqtype_3_fractionInput_panel.transform.GetChild(1).gameObject.GetComponentInChildren<Text>().text = "2";
                // RO_qtype_3number_pnael.SetActive(true);
                drag_drop_panel.SetActive(true);
                submit_button.gameObject.SetActive(true);
                current_ques = "Obj3_RO2_q1";
                GameObject.Find("FractionInputPanel").transform.GetChild(0).gameObject.SetActive(false);
                GameObject.Find("FractionInputPanel").transform.GetChild(1).gameObject.SetActive(false);
                GameObject.Find("FractionInputPanel").transform.GetChild(2).gameObject.SetActive(false);
                break;

            case "Obj3_LO3_fraction_represent":
                drag_drop_panel.SetActive(false);
                RO_Panel.SetActive(false);
                RO_qtype3.SetActive(false);
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Fractions represent a part of a whole and we learnt that denominator represent the number of equal parts in a whole.");
                single_slots_apple.SetActive(true);

                GameObject.Find("FractionInputPanel").transform.GetChild(0).gameObject.SetActive(true);
                GameObject.Find("FractionInputPanel").transform.GetChild(1).gameObject.SetActive(true);
                GameObject.Find("FractionInputPanel").transform.GetChild(2).gameObject.SetActive(true);

                GameObject.Find("FractionInputPanel").transform.position = FRACTIONS_POSITION.transform.position;
                GameObject.Find("FractionInputPanel").transform.GetChild(0).gameObject.GetComponentInChildren<Text>().text = "";
                GameObject.Find("FractionInputPanel").transform.GetChild(1).gameObject.GetComponentInChildren<Text>().text = "";


                break;
            case "Obj3_LO3_if_the_whole":

                GameObject.FindObjectOfType<conversationManager>().EnableConversation("If the whole is not divided into parts, then we consider the whole as one part and the denominator will simply be 1");
                highlight__obj(GameObject.Find("FractionInputPanel").transform.GetChild(1).gameObject);
                highlight__obj(single_slots_apple.transform.GetChild(0).gameObject);
                highlight__obj(single_slots_apple.transform.GetChild(1).gameObject);
                highlight__obj(single_slots_apple.transform.GetChild(2).gameObject);
                GameObject.Find("FractionInputPanel").transform.GetChild(0).gameObject.GetComponentInChildren<Text>().text = "3";
                GameObject.Find("FractionInputPanel").transform.GetChild(1).gameObject.GetComponentInChildren<Text>().text = "1";
                break;

            case "Obj3_RO3_whole_having":
                GameObject.FindObjectOfType<conversationManager>().DisableConversation();

                RO_Panel.SetActive(true);
                RO_qtype3.SetActive(true);
                disable_all_highlights(single_slots_apple.transform.GetChild(0).gameObject);
                disable_all_highlights(single_slots_apple.transform.GetChild(1).gameObject);
                disable_all_highlights(single_slots_apple.transform.GetChild(2).gameObject);
                single_slots_apple.SetActive(false);
                disable_all_highlights(GameObject.Find("FractionInputPanel").transform.GetChild(1).gameObject);
                GameObject.Find("FractionInputPanel").transform.GetChild(0).gameObject.SetActive(false);
                GameObject.Find("FractionInputPanel").transform.GetChild(1).gameObject.SetActive(false);
                GameObject.Find("FractionInputPanel").transform.GetChild(2).gameObject.SetActive(false);
                RO_qtype3_op_a.transform.parent.GetComponent<GridLayoutGroup>().cellSize = new Vector2(1100f, 100f);
                setup_ro_question_options_text("A whole having no partitions will have a denominator that is", 3, "1, as the whole can be considered as one part", "0, as the whole has no parts", "Can have both 1 and 0 as it will have same value ", "", "", "", null, null, null, null, null, null, filled_sprite);
                ans = "";
                submit_button.gameObject.SetActive(true);
                current_ques = "Obj3_RO3_q1";

                break;

            case "Obj3_LO4_fractions_cannot":
                RO_Panel.SetActive(false);
                RO_qtype3.SetActive(false);
                enable_lo4();
                LO4_Panel.GetComponent<RectTransform>().sizeDelta = new Vector2(1322f, 200f);
                LO4_Panel.GetComponentInChildren<Text>().text = "Fractions cannot have 0 as their denominator.";
                // GameObject.FindObjectOfType<conversationManager>().EnableConversation("Fractions cannot have 0 as their denominator.");
                //  GameObject.Find("FractionInputPanel").transform.GetChild(0).gameObject.GetComponentInChildren<Text>().text = "1";
                //  GameObject.Find("FractionInputPanel").transform.GetChild(1).gameObject.GetComponentInChildren<Text>().text = "0";
                //  GameObject.Find("FractionInputPanel").transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
                break;

            case "Obj3_LO4_do_you_know":
                // GameObject.FindObjectOfType<conversationManager>().EnableConversation("Do you know why?");
                LO4_Panel.GetComponentInChildren<Text>().text = "Do you know why?";
                break;

            case "Obj3_LO4_what_part_can_you":
                // GameObject.FindObjectOfType<conversationManager>().EnableConversation("What part can you take from a whole that does not exist ? ");
                LO4_Panel.GetComponentInChildren<Text>().text = "What part can you take from a whole that does not exist ?";
                break;

            case "Obj3_LO4_having_zero":
                //   GameObject.FindObjectOfType<conversationManager>().EnableConversation("Having 0 parts means there is nothing to take a part or a portion from and fractions are taking a part from a whole");
                LO4_Panel.GetComponent<RectTransform>().sizeDelta = new Vector2(1322f, 500f);

                LO4_Panel.GetComponentInChildren<Text>().text = "Having 0 parts means there is nothing to take a part or a portion from \n\n";

                break;
            case "Obj3_LO4_fractions_are":
                //   GameObject.FindObjectOfType<conversationManager>().EnableConversation("Having 0 parts means there is nothing to take a part or a portion from and fractions are taking a part from a whole");


                LO4_Panel.GetComponentInChildren<Text>().text = "Having 0 parts means there is nothing to take a part or a portion from and fractions are taking a part from a whole.\n\n";

                break;

            case "Obj3_LO4_therefore_fractions":
                // GameObject.FindObjectOfType<conversationManager>().EnableConversation("Therefore, fractions having 0 as denominator does not exist");
                LO4_Panel.GetComponentInChildren<Text>().text = "Having 0 parts means there is nothing to take a part or a portion from and fractions are taking a part from a whole.\nTherefore, fractions having 0 as denominator does not exist.";
                break;


            case "Obj3_RO4_identify_the":
                disable_lo4();
                RO_Panel.SetActive(true);
                RO_qtype3.SetActive(true);
                RO_qtype3_op_a.transform.parent.GetComponent<GridLayoutGroup>().cellSize = new Vector2(1100f, 100f);
                setup_ro_question_options_text("Identify the option which does not make a fraction?", 5, "\\frac{0}{1} as fraction cannot have numerator 0", "\\frac{1}{2} as we cannot have 1 as numerator.", "\\frac{0}{2} as fraction cannot have numerator 0", "\\frac{2}{0} as fraction cannot have 0 parts", "Both A and C", "", null, null, null, null, null, null, apple_complete);
                ans = "";

                submit_button.gameObject.SetActive(true);
                current_ques = "Obj3_RO4_q1";
                condition = "last";
                break;



        }

    }
    void animStop()
    {
        //GameObject.Find("Fade").GetComponent<Animator>().enabled = false;
        ExitNow();
    }
    void ExitNow()
    {
        //SceneManager.LoadScene(10);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadeout");
       
    }
    void StartQuestion2()
    {
        // GameObject.FindObjectOfType<conversationManager>().DisableConversation();
        GameObject.FindObjectOfType<QuestionManager>().EnableForObj3Quest1();
    }
    void StartQuestion1()
    {
        //  GameObject.FindObjectOfType<conversationManager>().DisableConversation();
        GameObject.FindObjectOfType<QuestionManager>().EnableForObj3Quest2();
    }


    void enable_lo4()
    {
        LO4_Panel.SetActive(true);
    }

    void disable_lo4()
    {
        LO4_Panel.SetActive(false);
    }



    void setup_ro_question_options_text(string question, int option_limit, string op_a, string op_b, string op_c, string op_d, string op_e, string op_f, Sprite img_a, Sprite img_b, Sprite img_c, Sprite img_d, Sprite img_e, Sprite img_f, Sprite question_image)
    {
        submit_button.gameObject.SetActive(true);
        RO_qtype3_text.GetComponent<TEXDraw>().text = question;
        switch (option_limit)
        {
            case 0:
                reset_options();
                break;

            case 2:
                reset_options();
                RO_qtype3_op_a.SetActive(true);
                RO_qtype3_op_b.SetActive(true);
                RO_qtype3_op_a.GetComponentInChildren<TEXDraw>().text = op_a;
                RO_qtype3_op_b.GetComponentInChildren<TEXDraw>().text = op_b;
                if (img_a != null)
                {
                    imga.color = Color.white;
                    imga.sprite = img_a;

                }
                if (img_b != null)
                {
                    imgb.color = Color.white;
                    imgb.sprite = img_b;

                }

                if (question_image != null)
                {
                    ro_question_image.color = Color.white;
                    ro_question_image.sprite = question_image;
                }
                break;


            case 3:
                reset_options();
                RO_qtype3_op_a.SetActive(true);
                RO_qtype3_op_b.SetActive(true);
                RO_qtype3_op_c.SetActive(true);
                RO_qtype3_op_a.GetComponentInChildren<TEXDraw>().text = op_a;
                RO_qtype3_op_b.GetComponentInChildren<TEXDraw>().text = op_b;
                RO_qtype3_op_c.GetComponentInChildren<TEXDraw>().text = op_c;
                if (img_a != null)
                {
                    imga.color = Color.white;
                    imga.sprite = img_a;

                }
                if (img_b != null)
                {
                    imgb.color = Color.white;
                    imgb.sprite = img_b;

                }
                if (img_c != null)
                {
                    imgc.color = Color.white;
                    imgc.sprite = img_c;


                }
                if (question_image != null)
                {
                    ro_question_image.color = Color.white;
                    ro_question_image.sprite = question_image;
                }
                break;
            case 4:
                reset_options();
                RO_qtype3_op_a.SetActive(true);
                RO_qtype3_op_b.SetActive(true);
                RO_qtype3_op_c.SetActive(true);
                RO_qtype3_op_d.SetActive(true);
                RO_qtype3_op_a.GetComponentInChildren<TEXDraw>().text = op_a;
                RO_qtype3_op_b.GetComponentInChildren<TEXDraw>().text = op_b;
                RO_qtype3_op_c.GetComponentInChildren<TEXDraw>().text = op_c;
                RO_qtype3_op_d.GetComponentInChildren<TEXDraw>().text = op_d;
                if (img_a != null)
                {
                    imga.color = Color.white;
                    imga.sprite = img_a;

                }
                if (img_b != null)
                {
                    imgb.color = Color.white;
                    imgb.sprite = img_b;

                }
                if (img_c != null)
                {
                    imgc.color = Color.white;
                    imgc.sprite = img_c;

                }
                if (img_d != null)
                {
                    imgd.color = Color.white;
                    imgd.sprite = img_d;

                }
                if (question_image != null)
                {
                    ro_question_image.color = Color.white;
                    ro_question_image.sprite = question_image;
                }

                break;
            case 5:
                reset_options();
                RO_qtype3_op_a.SetActive(true);
                RO_qtype3_op_b.SetActive(true);
                RO_qtype3_op_c.SetActive(true);
                RO_qtype3_op_d.SetActive(true);
                RO_qtype3_op_e.SetActive(true);
                RO_qtype3_op_a.GetComponentInChildren<TEXDraw>().text = op_a;
                RO_qtype3_op_b.GetComponentInChildren<TEXDraw>().text = op_b;
                RO_qtype3_op_c.GetComponentInChildren<TEXDraw>().text = op_c;
                RO_qtype3_op_d.GetComponentInChildren<TEXDraw>().text = op_d;
                RO_qtype3_op_e.GetComponentInChildren<TEXDraw>().text = op_e;
                if (img_a != null)
                {
                    imga.color = Color.white;
                    imga.sprite = img_a;

                }
                if (img_b != null)
                {
                    imgb.color = Color.white;
                    imgb.sprite = img_b;

                }
                if (img_c != null)
                {
                    imgc.color = Color.white;
                    imgc.sprite = img_c;

                }
                if (img_d != null)
                {
                    imgd.color = Color.white;
                    imgd.sprite = img_d;

                }
                if (img_e != null)
                {
                    imge.color = Color.white;
                    imge.sprite = img_e;

                }
                if (question_image != null)
                {
                    ro_question_image.color = Color.white;
                    ro_question_image.sprite = question_image;
                }


                break;

            case 6:
                reset_options();
                RO_qtype3_op_a.SetActive(true);
                RO_qtype3_op_b.SetActive(true);
                RO_qtype3_op_c.SetActive(true);
                RO_qtype3_op_d.SetActive(true);
                RO_qtype3_op_e.SetActive(true);
                RO_qtype3_op_f.SetActive(true);
                RO_qtype3_op_a.GetComponentInChildren<TEXDraw>().text = op_a;
                RO_qtype3_op_b.GetComponentInChildren<TEXDraw>().text = op_b;
                RO_qtype3_op_c.GetComponentInChildren<TEXDraw>().text = op_c;
                RO_qtype3_op_d.GetComponentInChildren<TEXDraw>().text = op_d;
                RO_qtype3_op_e.GetComponentInChildren<TEXDraw>().text = op_e;
                RO_qtype3_op_f.GetComponentInChildren<TEXDraw>().text = op_f;

                if (img_a != null)
                {
                    imga.color = Color.white;
                    imga.sprite = img_a;

                }
                if (img_b != null)
                {
                    imgb.color = Color.white;
                    imgb.sprite = img_b;

                }
                if (img_c != null)
                {
                    imgc.color = Color.white;
                    imgc.sprite = img_c;

                }
                if (img_d != null)
                {
                    imgd.color = Color.white;
                    imgd.sprite = img_d;

                }
                if (img_e != null)
                {
                    imge.color = Color.white;
                    imge.sprite = img_e;

                }
                if (img_f != null)
                {
                    imgf.color = Color.white;
                    imgf.sprite = img_f;

                }
                if (question_image != null)
                {
                    ro_question_image.color = Color.white;
                    ro_question_image.sprite = question_image;
                }

                break;


        }



    }

    public void reset_options()
    {
        RO_qtype3_op_a.SetActive(true);
        RO_qtype3_op_b.SetActive(true);
        RO_qtype3_op_c.SetActive(true);
        RO_qtype3_op_d.SetActive(true);
        RO_qtype3_op_e.SetActive(true);
        RO_qtype3_op_f.SetActive(true);
        imga.color = Color.clear;
        imgb.color = Color.clear;
        imgc.color = Color.clear;
        imgd.color = Color.clear;
        imge.color = Color.clear;
        imgf.color = Color.clear;
        ro_question_image.color = Color.clear;

        RO_qtype3_op_a.GetComponent<Image>().color = Color.white;
        RO_qtype3_op_b.GetComponent<Image>().color = Color.white;
        RO_qtype3_op_c.GetComponent<Image>().color = Color.white;
        RO_qtype3_op_d.GetComponent<Image>().color = Color.white;
        RO_qtype3_op_e.GetComponent<Image>().color = Color.white;
        RO_qtype3_op_f.GetComponent<Image>().color = Color.white;

        RO_qtype3_op_a.GetComponentInChildren<TEXDraw>().text = "";
        RO_qtype3_op_b.GetComponentInChildren<TEXDraw>().text = "";
        RO_qtype3_op_c.GetComponentInChildren<TEXDraw>().text = "";
        RO_qtype3_op_d.GetComponentInChildren<TEXDraw>().text = "";
        RO_qtype3_op_e.GetComponentInChildren<TEXDraw>().text = "";
        RO_qtype3_op_f.GetComponentInChildren<TEXDraw>().text = "";
        RO_qtype3_op_a.transform.GetChild(2).gameObject.SetActive(false);
        RO_qtype3_op_b.transform.GetChild(2).gameObject.SetActive(false);
        RO_qtype3_op_c.transform.GetChild(2).gameObject.SetActive(false);
        RO_qtype3_op_d.transform.GetChild(2).gameObject.SetActive(false);
        RO_qtype3_op_e.transform.GetChild(2).gameObject.SetActive(false);
        RO_qtype3_op_f.transform.GetChild(2).gameObject.SetActive(false);
        RO_qtype3_op_a.SetActive(false);
        RO_qtype3_op_b.SetActive(false);
        RO_qtype3_op_c.SetActive(false);
        RO_qtype3_op_d.SetActive(false);
        RO_qtype3_op_e.SetActive(false);
        RO_qtype3_op_f.SetActive(false);
        ROqtype_3_fractionInput_panel.SetActive(false);
        RO_qtype_3number_pnael.SetActive(false);
        drag_drop_panel.SetActive(false);



    }

    public void Reset_options_afterselect()
    {
        RO_qtype3_op_a.transform.GetChild(2).gameObject.SetActive(false);
        RO_qtype3_op_b.transform.GetChild(2).gameObject.SetActive(false);
        RO_qtype3_op_c.transform.GetChild(2).gameObject.SetActive(false);
        RO_qtype3_op_d.transform.GetChild(2).gameObject.SetActive(false);
        RO_qtype3_op_e.transform.GetChild(2).gameObject.SetActive(false);
        RO_qtype3_op_f.transform.GetChild(2).gameObject.SetActive(false);
        temp.GetComponent<Image>().color = Color.white;
        ans = "";
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
        if (current_ques.Equals("Obj3_RO2_q1"))
        {
            if (isNum == true && isDenum == false)
            {
                num = num + currentSelectedGameObject.name;
                GameObject.Find("FractionInputPanel_RO").transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = num;
            }
            if (isNum == false && isDenum == true)
            {
                deno = deno + currentSelectedGameObject.name;
                GameObject.Find("FractionInputPanel_RO").transform.GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text = deno;
            }
        }
        else
        {
            Debug.Log("numerator =" + currentSelectedGameObject.name);
            ans = currentSelectedGameObject.name;
            foreach (GameObject b in GameObject.FindGameObjectsWithTag("Numbers"))
            {
                b.transform.GetChild(2).gameObject.SetActive(false);
            }
            currentSelectedGameObject.transform.GetChild(2).gameObject.SetActive(true);
            temp = currentSelectedGameObject.transform.GetChild(2).gameObject;
        }

    }

    public void ClickOnNumerator()
    {
        isNum = true;
        isDenum = false;
        GameObject.Find("FractionInputPanel_RO").transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = "";
        num = "";
    }
    public void ClickOnDenominator()
    {
        isNum = false;
        isDenum = true;
        GameObject.Find("FractionInputPanel_RO").transform.GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text = "";
        deno = "";
    }






    public void enable_panel(GameObject object_to_enable)
    {
        //  Animator animator_of_object = object_to_enable.GetComponent<Animator>();
        object_to_enable.SetActive(true);
        //   animator_of_object.Play("enable", 0);
    }

    public void disable_panel(GameObject object_to_disable, float time)
    {
        Animator animator_of_object = object_to_disable.GetComponent<Animator>();
        Coroutine a = StartCoroutine(disable_after(object_to_disable, animator_of_object, time));
    }

    IEnumerator disable_after(GameObject object_to_enable, Animator animator_of_object, float time)
    {
        // animator_of_object.Play("disable", 0);
        yield return new WaitForSeconds(time);
        object_to_enable.SetActive(false);

    }

    public IEnumerator disable_afte_timer(GameObject object_to_disable, float time)
    {
        yield return new WaitForSeconds(time);
        //  Animator animator_of_object = object_to_disable.GetComponent<Animator>();
        //  animator_of_object.Play("disable", 0);
        // yield return new WaitForSeconds(1.0f);
        object_to_disable.SetActive(false);

    }

    void reset_drag_me()
    {
        var a = FindObjectsOfType<DragMe>();
        foreach (DragMe d in a)
        {
            Color currentDataColor = d.gameObject.GetComponent<Image>().color;
            currentDataColor.a = 0;
            d.gameObject.GetComponent<Image>().color = currentDataColor;
            d.gameObject.GetComponent<Image>().color = Color.white;

        }
        var a1 = FindObjectsOfType<obj_3drop>();
        foreach (obj_3drop d in a1)
        {

            d.gameObject.GetComponent<Image>().overrideSprite = d.dead;

        }
        ROqtype_3_fractionInput_panel.transform.GetChild(0).gameObject.GetComponentInChildren<Text>().text = "1";
        ROqtype_3_fractionInput_panel.transform.GetChild(1).gameObject.GetComponentInChildren<Text>().text = "2";
    }
    public void check_answers()
    {
        Debug.Log("in answers");
        if (ans != null || ans != "")
        {
            condition = "";
            Debug.Log("in answers123");
            submit_button.gameObject.SetActive(false);
            if (current_ques == "Obj3_RO1_q1")
            {
                Debug.Log("in answers1234");
                if (ans.Equals("RO op 1") || ans.Equals("RO op 2"))
                {
                    FindObjectOfType<conversationManager>().playError();

                    FindObjectOfType<timeline_new>().playAudioOnRelearn("obj3_let_see_why_common.wav");
                    StartCoroutine(enable_dialougue_panel("ro1_answer_a_b.wav"));
                    Dialouge_text.GetComponent<Text>().text = "The number of apples placed in the case is the number of parts considered. Hence both number of apples in a case and the number of parts considered can both be called as the numerator";

                    //   enable_panel(Dialouge_panel);
                    //StartCoroutine(enable_disable_dialougue());
                    //  Reset_options_afterselect();

                }
                else if (ans.Equals("RO op 3"))
                {
                    FindObjectOfType<conversationManager>().playError();
                    // FindObjectOfType<timeline_new>().playAudioOnRelearn("ro1_answer_c.wav");
                    FindObjectOfType<timeline_new>().playAudioOnRelearn("obj3_let_see_why_common.wav");
                    StartCoroutine(enable_dialougue_panel("ro1_answer_c.wav"));
                    Dialouge_text.GetComponent<Text>().text = "The top number is the numerator but the numerator is also the number of parts considered from an object or a group of objects";
                    //  enable_panel(Dialouge_panel);
                    // StartCoroutine(enable_disable_dialougue());

                    // Reset_options_afterselect();
                }
                else if (ans.Equals("RO op 4"))
                {
                    // RO_Panel.SetActive(false);
                    // RO_qtype3.SetActive(false);

                    FindObjectOfType<timeline_new>().load_next();
                    FindObjectOfType<conversationManager>().playCorrect();
                }
                else
                {
                    enter_somethinprompt();
                }

            }

            if (current_ques == "Obj3_RO1_q2")
            {
                if (ans.Equals("RO op 1") || ans.Equals("RO op 2"))
                {
                    FindObjectOfType<conversationManager>().playError();
                    // FindObjectOfType<timeline_new>().playAudioOnRelearn("ro2_answer_a_b.wav");
                    FindObjectOfType<timeline_new>().playAudioOnRelearn("obj3_let_see_why_common.wav");
                    StartCoroutine(enable_dialougue_panel("ro2_answer_a_b.wav"));
                    Dialouge_text.GetComponent<Text>().text = "The total number of slots in the case is the number of parts the whole is divided into. Hence both number of slots in a case and number of parts the whole is divided into can be called as the denominator";
                    //  enable_panel(Dialouge_panel);


                    // Reset_options_afterselect();
                }
                else if (ans.Equals("RO op 3"))
                {
                    FindObjectOfType<conversationManager>().playError();
                    //   FindObjectOfType<timeline_new>().playAudioOnRelearn("ro2_answer_c.wav");
                    FindObjectOfType<timeline_new>().playAudioOnRelearn("obj3_let_see_why_common.wav");
                    StartCoroutine(enable_dialougue_panel("ro2_answer_c.wav"));
                    Dialouge_text.GetComponent<Text>().text = " The bottom number is the denominator and denominator is also the total number of equal parts in a whole or an object in a group of objects";
                    //  enable_panel(Dialouge_panel);
                    // StartCoroutine(enable_disable_dialougue());
                    //  Reset_options_afterselect();
                }
                else if (ans.Equals("RO op 4"))
                {
                    FindObjectOfType<conversationManager>().playCorrect();
                    RO_Panel.SetActive(false);
                    RO_qtype3.SetActive(false);

                    FindObjectOfType<timeline_new>().load_next();

                }
                else
                {
                    enter_somethinprompt();
                }

            }


            if (current_ques == "Obj3_RO2_q1")
            {

                if (num_dragged && denum_dragged)
                {
                    // reset_options();
                    reset_drag_me();
                    RO_Panel.SetActive(false);
                    RO_qtype3.SetActive(false);
                    FindObjectOfType<conversationManager>().playCorrect();
                    FindObjectOfType<timeline_new>().load_next();

                }
                else
                {
                    reset_drag_me();
                    FindObjectOfType<conversationManager>().playError();
                    //  FindObjectOfType<timeline_new>().playAudioOnRelearn("ro2_answer_wrong.wav");
                    FindObjectOfType<timeline_new>().playAudioOnRelearn("obj3_let_see_why_common.wav");
                    StartCoroutine(enable_dialougue_panel("ro2_answer_wrong.wav"));
                    Dialouge_text.GetComponent<Text>().text = "The top number is the numerator and the bottom number is the denominator";
                    //   enable_panel(Dialouge_panel);
                    num = "1";
                    deno = "2";
                    //   GameObject.Find("FractionInputPanel_RO").transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = num;
                    //   GameObject.Find("FractionInputPanel_RO").transform.GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text = deno;

                    // StartCoroutine(enable_disable_dialougue());
                    //  Reset_options_afterselect();
                }


            }

            if (current_ques == "Obj3_RO3_q1")
            {
                if (ans.Equals("RO op 3") || ans.Equals("RO op 2"))
                {
                    FindObjectOfType<conversationManager>().playError();
                    //   FindObjectOfType<timeline_new>().playAudioOnRelearn("ro3_answer_wrong.wav");
                    FindObjectOfType<timeline_new>().playAudioOnRelearn("obj3_let_see_why_common.wav");
                    StartCoroutine(enable_dialougue_panel("ro3_answer_wrong.wav"));
                    dialougue_image.GetComponent<Image>().sprite = filled_sprite;
                    Dialouge_text.GetComponent<Text>().text = "If a whole is not divided into parts, then we consider the whole as one part and the denominator will simply be 1.";
                    //  enable_panel(Dialouge_panel);
                    // StartCoroutine(enable_disable_dialougue());
                    //  Reset_options_afterselect();

                }
                else if (ans.Equals("RO op 1"))
                {
                    dialougue_image.GetComponent<Image>().sprite = null;
                    RO_Panel.SetActive(false);
                    RO_qtype3.SetActive(false);
                    FindObjectOfType<conversationManager>().playCorrect();
                    FindObjectOfType<timeline_new>().load_next();


                }
                else
                {
                    enter_somethinprompt();
                }

            }
            if (current_ques == "Obj3_RO4_q1")
            {
                if (ans.Equals("RO op 1") || ans.Equals("RO op 3") || ans.Equals("RO op 2") || ans.Equals("RO op 5"))
                {
                    FindObjectOfType<conversationManager>().playError();
                    //   FindObjectOfType<timeline_new>().playAudioOnRelearn("ro4_answer_wrong.wav");
                    FindObjectOfType<timeline_new>().playAudioOnRelearn("obj3_let_see_why_common.wav");
                    StartCoroutine(enable_dialougue_panel("ro4_answer_wrong.wav"));
                    dialougue_image.GetComponent<Image>().sprite = apple_complete;
                    Dialouge_text.GetComponent<Text>().text = "Have a look at this apple. This is one whole apple. So the denominator here will be one. If the denominator is zero, there will be no apple. That is the reason why we can never have 0 as the denominator.";
                    // enable_panel(Dialouge_panel);
                    // StartCoroutine(enable_disable_dialougue());


                    //  Reset_options_afterselect();


                }
                else if (ans.Equals("RO op 4"))
                {
                    RO_Panel.SetActive(false);
                    RO_qtype3.SetActive(false);
                    FindObjectOfType<conversationManager>().playCorrect();
                    //GameObject.Find("Fade").GetComponent<Animator>().enabled = false;
                    //GameObject.Find("Fade").GetComponent<Animator>().enabled = true;
                    GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
                    Invoke("animStop", 2.5f);
                    //exit_panel.SetActive(true);
                   
                    //FindObjectOfType<GameManager>().OnGameOver();
                }
                else
                {
                    enter_somethinprompt();
                }

            }
            submit_button.gameObject.SetActive(false);
        }
        else
        {
            enter_somethinprompt();

        }
    }

    public IEnumerator enable_dialougue_panel(string audio_filename)
    {
        temp.GetComponent<Image>().color = Color.red;
        yield return new WaitForSeconds(0.6f);
        yield return new WaitForSeconds(FindObjectOfType<timeline_new>().length_of_audio + 2);
        enable_panel(Dialouge_panel);
        if (current_ques == "Obj3_RO2_q1")
        {
            num = "1";
            deno = "2";
            GameObject.Find("FractionInputPanel_RO").transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = num;
            GameObject.Find("FractionInputPanel_RO").transform.GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text = deno;
        }

        Reset_options_afterselect();
        FindObjectOfType<timeline_new>().playAudioOnRelearn(audio_filename);

    }


    void enter_somethinprompt()
    {
        Dialouge_text.GetComponent<Text>().text = "Please select some value then Click on Submit button";
        enable_panel(Dialouge_panel);
        StartCoroutine(disable_afte_timer(Dialouge_panel, 4));
        Reset_options_afterselect();
        Invoke("ENABLE_SUBMIT", 4);
    }
    void ENABLE_SUBMIT()
    {
        submit_button.gameObject.SetActive(true);
    }



    void highlight__obj(GameObject object_to)
    {
        object_to.GetComponent<AnimateColors>().enabled = true;
    }


    void disable_all_highlights(GameObject object_to)
    {
        object_to.GetComponent<AnimateColors>().enabled = false;

        if (object_to.GetComponent<SpriteRenderer>())
        {
            object_to.GetComponent<SpriteRenderer>().color = Color.white;
        }
        if (object_to.GetComponent<Image>())
        {
            object_to.GetComponent<Image>().color = Color.white;
        }
    }


    void dialougue_ok_functionality()
    {
        StopAllCoroutines();
        Dialouge_panel.SetActive(false);
        FindObjectOfType<timeline_new>().lipsync_player.GetComponent<AudioSource>().Stop();

        if (condition == "num_denum")
        {
            FindObjectOfType<QuestionManager>().DoneButton.gameObject.SetActive(true);
        }
        else if (current_ques == "Obj3_RO4_q1")
        {
            RO_Panel.SetActive(false);
            RO_qtype3.SetActive(false);

            //GameObject.Find("Fade").GetComponent<Animator>().enabled = false;
            //GameObject.Find("Fade").GetComponent<Animator>().enabled = true;
            GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
            Invoke("animStop", 2.5f);
            //FindObjectOfType<GameManager>().OnGameOver();
            //exit_panel.SetActive(true);
        }
        else
        {
            reset_options();
            Reset_options_afterselect();

            // submit_button.gameObject.SetActive(true);
            FindObjectOfType<timeline_new>().load_next();
        }
    }

    IEnumerator enable_disable_dialougue()

    {
        Dialouge_panel.SetActive(true);
        if (Application.isEditor)
        {
            yield return new WaitForSeconds(5);
            //    submit_button.gameObject.SetActive(true);
            Dialouge_panel.gameObject.SetActive(false);
        }
        else
        {
            yield return new WaitForSeconds(0.6f);
            yield return new WaitForSeconds(FindObjectOfType<timeline_new>().length_of_audio);
            Dialouge_panel.SetActive(false);
            //    submit_button.gameObject.SetActive(true);
        }

        Dialouge_panel.SetActive(false);
    }
}