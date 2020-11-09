using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class Obj13Manager : MonoBehaviour
{
    string jsonFileName = "obj_13_Improper_to_Mixed_Fraction.json";
    public GameObject have_a_look, asyou_can, which_an_improper, simplytap, pizza_combine_activity, pizza_after_combine_activity, we_checked_how_many, We_had_7b2_slices, we_combine_the_parts, we_can_tell_how_many, three_and_half, one_and_hour, simpler_way, once_again, show_the_devision, here_are_thre_labels_activity, mixed_fractions_is_always, so_write_quotient_3, this_is_same_as_half, the_remainder_1_is, show31b2, The_denominator_of_the_fraction, This_indicates_the_number_of, so_we_write_2, let_us_once_again_have_a_look_at, let_us_step_1_divide_the_numerator, let_us_step_2_the_quotient, ro4_reinfo;

    public GameObject whole_numbr, propr_fraction, chef_panel_okay, three_1b2, This_makes_mixed;
    public GameObject plate_1, plate_2, plate_3, plate_4, plate_to_active, three_labels_submit;
    public TEXDraw heading;
    public GameObject[] pizza_slices;
    public int plate_counter, slice_counter,labels_count;
    public bool quotient, remainder, divisor;
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
    void Update()
    {

    }
    void Initialised()
    {

        labels_count = 0;
        plate_counter = 0;
        slice_counter = 0;
        assign_pizza_action();
        have_a_look = GameObject.Find("Have look at this");
        asyou_can = GameObject.Find("As you can see, there are");
        which_an_improper = GameObject.Find("which an improper fractions");
        simplytap = GameObject.Find("Simply tap on 2 slices to make ");
        pizza_combine_activity = GameObject.Find("pizza_combine_activity");
        pizza_after_combine_activity = GameObject.Find("pizza_after_combine_activity");
        We_had_7b2_slices = GameObject.Find("We had 7/2 slices");
        we_checked_how_many = GameObject.Find("We checked how many whole");
        we_combine_the_parts = GameObject.Find("We combine the parts");
        we_can_tell_how_many = GameObject.Find("We can tell how many");
        three_and_half = GameObject.Find("3 and a half bottles");
        one_and_hour = GameObject.Find("one and hour to reach");
        simpler_way = GameObject.Find("simpler way");
        once_again = GameObject.Find("Once again");
        show_the_devision = GameObject.Find("show_the_devision");
        plate_1 = GameObject.Find("plate_1_activity");
        plate_2 = GameObject.Find("plate_2_activity");
        plate_3 = GameObject.Find("plate_3_activity");
        plate_4 = GameObject.Find("plate_4_activity");
        here_are_thre_labels_activity = GameObject.Find("Here are three Labels_activity");
        three_labels_submit = GameObject.Find("three Labels submit");
        mixed_fractions_is_always = GameObject.Find("mixed fraction is always");
        so_write_quotient_3 = GameObject.Find("So we write the quotient 3");
        this_is_same_as_half = GameObject.Find("This same as 7 half slices");
        the_remainder_1_is = GameObject.Find("The Remainder 1 is written");
        show31b2 = GameObject.Find("show31b2");
        The_denominator_of_the_fraction = GameObject.Find("The_denominator_of_the_fraction");
        This_indicates_the_number_of = GameObject.Find("This indicates the number of");
        so_we_write_2 = GameObject.Find("So we write 2");
        let_us_once_again_have_a_look_at = GameObject.Find("let_us_once_again_have_a_look_at");
        let_us_step_1_divide_the_numerator = GameObject.Find("let_us_step_1_divide_the_numerator");
        let_us_step_2_the_quotient = GameObject.Find("let_us_step_2_the_quotient");
        ro4_reinfo = GameObject.Find("ro4_reinfo");
        heading = GameObject.Find("Heading_text").GetComponent<TEXDraw>();
        whole_numbr = GameObject.Find("whole_numbr");
        propr_fraction = GameObject.Find("propr_fraction");
        chef_panel_okay = GameObject.Find("chef_panel_okay");
        three_1b2 = GameObject.Find("31b2");
        This_makes_mixed = GameObject.Find("This_makes_mixed");
        LoadingAudio = GameObject.Find("LoadAudio");
        if (UtilityArtifacts.backTraversal)
        {
            Text textLoadingText = LoadingAudio.transform.GetChild(2).GetComponent<Text>();
            textLoadingText.text = "Let us understand this better";
        }
        three_labels_submit.GetComponent<Button>().onClick.AddListener(validate_three_label_submit);
        chef_panel_okay.GetComponent<Button>().onClick.AddListener(load_next);







        have_a_look.SetActive(false);
        asyou_can.SetActive(false);
        which_an_improper.SetActive(false);
        simplytap.SetActive(false);
        pizza_combine_activity.SetActive(false);
        pizza_after_combine_activity.SetActive(false);
        We_had_7b2_slices.SetActive(false);
        we_checked_how_many.SetActive(false);
        we_combine_the_parts.SetActive(false);
        we_can_tell_how_many.SetActive(false);
        three_and_half.SetActive(false);
        one_and_hour.SetActive(false);
        simpler_way.SetActive(false);
        once_again.SetActive(false);
        show_the_devision.SetActive(false);
        plate_1.SetActive(false);
        plate_2.SetActive(false);
        plate_3.SetActive(false);
        plate_4.SetActive(false);
        here_are_thre_labels_activity.SetActive(false);
        mixed_fractions_is_always.SetActive(false);
        so_write_quotient_3.SetActive(false);
        this_is_same_as_half.SetActive(false);
        the_remainder_1_is.SetActive(false);
        show31b2.SetActive(false);
        The_denominator_of_the_fraction.SetActive(false);
        so_we_write_2.SetActive(false);
        This_indicates_the_number_of.SetActive(false);
        let_us_once_again_have_a_look_at.SetActive(false);
        let_us_step_1_divide_the_numerator.SetActive(false);
        let_us_step_2_the_quotient.SetActive(false);
        ro4_reinfo.SetActive(false);
        whole_numbr.SetActive(false);
        propr_fraction.SetActive(false);
        chef_panel_okay.SetActive(false);
        three_labels_submit.SetActive(false);
        three_1b2.SetActive(false);
        This_makes_mixed.SetActive(false);
        Invoke("audio_invoke", 2.0f);
    }

    void load_next()
    {
        chef_panel_okay.SetActive(false);
        chef_panel_okay.transform.GetChild(1).gameObject.SetActive(false);
        FindObjectOfType<timeline_new>().load_next();
    }

    void audio_invoke()
    {
        Debug.Log(EVENT.SetUpTimeLine);
        EventManager.setNameOfJasonFile(jsonFileName);
        EventManager.Broadcast(EVENT.SetUpTimeLine);
    }

    void assign_pizza_action()
    {
        Debug.Log("hi");
        pizza_slices = GameObject.FindGameObjectsWithTag("pizza_activity");
        foreach (GameObject m in pizza_slices)
        {
            Debug.Log(m.name);
            m.GetComponent<Button>().onClick.AddListener(() => select_pizza());
        }
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

            case "obj_13_Have_a_look":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Have a look at these pizza slices.");
                have_a_look.SetActive(true);
                heading.text = "";
                Invoke("nextObjectiveVo", 6.0f);
                break;
            case "obj_13_As_you_can":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("As you can see, there are 7 half slices  ");
                asyou_can.SetActive(true);
                heading.text = "";
                break;
            case "obj_13_which":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Which if represented as a fraction is \\frac{7}{2} and we know that \\frac{7}{2} is an improper fraction");
                which_an_improper.SetActive(true);
                heading.text = "";
                break;
            case "obj_13_Simply_tap":
                have_a_look.SetActive(false);
                asyou_can.SetActive(false);
                which_an_improper.SetActive(false);
                simplytap.SetActive(true);
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Simply tap on 2 slices to make the slices become a whole pizza until you cannot make any more whole pizzas.");

                heading.text = "";
                break;
            case "obj_13_Go_Ahead":
                simplytap.SetActive(false);
                pizza_combine_activity.SetActive(true);
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Go ahead and make the pizza into whole and see if we have any additional slices left");
                plate_1.SetActive(true);
                heading.text = "Make the pizza into whole";
                //write "make the pizza into whole " as heading on screen
                break;
            case "obj_13_quest1":
                GameObject.FindObjectOfType<conversationManager>().DisableConversation();
                pizza_combine_activity.SetActive(false);
                pizza_after_combine_activity.SetActive(true);
                //plate_1.SetActive(true);
                heading.text = "Represent these pizzas in terms of mixed fraction";
                // write "Represent these pizzas in terms of mixed fraction " as heading
                FindObjectOfType<QuestionManager>().EnableForObj13Quest();
                break;
            case "obj_13_Lets_look":
                pizza_combine_activity.SetActive(false);
                pizza_after_combine_activity.SetActive(false);
                GameObject.Find("FractionInputPanel").transform.GetChild(0).gameObject.SetActive(false);
                GameObject.Find("FractionInputPanel").transform.GetChild(1).gameObject.SetActive(false);
                GameObject.Find("FractionInputPanel").transform.GetChild(2).gameObject.SetActive(false);
                GameObject.Find("FractionInputPanel").transform.GetChild(3).gameObject.SetActive(false);
                GameObject.Find("NumberPanel").transform.GetChild(0).gameObject.SetActive(false);
                heading.text = "Let’s look at what we just did";
                // write "Let’s look at what we just did" as heading
                break;
            case "obj_13_We_had":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("We had \\frac{7}{2} slices  ");
                We_had_7b2_slices.SetActive(true);
                Invoke("nextObjectiveVo", 8.0f);
                break;
            case "obj_13_we_checked":
                We_had_7b2_slices.SetActive(false);
                we_checked_how_many.SetActive(true);
                Invoke("enable_3_1b2", 13);
               
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("We checked how many whole pizzas can be made from these many slices and what amount of pizza is left. ");
                Invoke("nextObjectiveVo", 17.0f);
                break;
            case "obj_13_This_is_exactly":
                three_1b2.SetActive(false);
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("This is exactly what happens when we convert an improper fraction into a mixed fraction. ");
                we_combine_the_parts.SetActive(true);
                break;
            case "obj_13_We_combine":
                three_1b2.SetActive(false);
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("We combine the parts to form as many wholes as possible. Then, represent the new combination of the wholes and the remaining part as a mixed fraction.");

                break;
            case "obj_13_We_can_tell":
                we_combine_the_parts.SetActive(false);
                we_checked_how_many.SetActive(false);
                we_can_tell_how_many.SetActive(true);
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("We can tell how many whole objects are there in the Fraction ");
                break;
            case "obj_13_and_how_many":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("and how many parts of a whole are there just by looking at the fraction");
                break;
            case "obj_13_This_makes_mixed":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("This makes mixed fractions easy to understand in comparison to improper fractions.");
                we_can_tell_how_many.GetComponent<Animator>().enabled = false;
                we_can_tell_how_many.SetActive(false);
                This_makes_mixed.SetActive(true);

                break;
            case "obj_13_That_is_why":
                This_makes_mixed.SetActive(false);
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("That is why we use mixed fractions in our daily lives.");
                heading.text = "Uses of Mixed Fraction in Daily Lives";


                break;
            case "obj_13_I_had_three":
                heading.text = "Uses of Mixed Fraction in Daily Lives";
                we_can_tell_how_many.GetComponent<Animator>().enabled = false;
                we_can_tell_how_many.SetActive(false);
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("I had 3 and a half bottles of water yesterday");
                three_and_half.SetActive(true);
                break;
            case "obj_13_or_i_will":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("or I will take 1 \\frac{1}{2} hours to reach home are some examples.");
                one_and_hour.SetActive(true);
                break;
            case "obj_13_But":
                GameObject.FindObjectOfType<conversationManager>().DisableConversation();
                //enable fade for RO
                Invoke("enableFade", 3.0f);
                Invoke("enableROQuest1", 6.0f);
                break;
            case "obj_13_RO1_quest1":
                heading.text = "";
                three_and_half.SetActive(false);
                one_and_hour.SetActive(false);
                break;
            case "obj_13_RO1_quest2":
                enableROQuest2();
                we_can_tell_how_many.SetActive(true);
                break;
            case "obj_13_as_much_as":
                GameObject.Find("RO Panel").transform.GetChild(0).gameObject.SetActive(false);
                GameObject.Find("RO Panel").transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
                GameObject.Find("RO Panel").transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
                we_can_tell_how_many.GetComponent<Animator>().enabled = true;
                we_can_tell_how_many.GetComponent<Animator>().Play("fade");
                heading.text = "";
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("As much as we like the pizza, we cannot have them all the time for our reference to convert fractions.");
                break;
            case "obj_13_so_lets_learn":
                we_can_tell_how_many.SetActive(false);
                simpler_way.SetActive(true);
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("So Let’s learn a simpler way to convert an Improper Fraction into a Mixed Fraction");
                heading.text = "Simpler way";
                //write simpler way as heading
                break;
            case "obj_13_Once_again":
                once_again.SetActive(true);
                simpler_way.SetActive(false);
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Once again, have a look at the Improper Fraction \\frac{7}{2} ");
                break;
            case "obj_13_We_learnt":
                simpler_way.SetActive(false);
                GameObject.FindObjectOfType<conversationManager>().DisableConversation();
                heading.text = "Fractions are like division";
                //write "Fractions are like division " as heading
                break;
            case "obj_13_So_lets_devide":
                simpler_way.SetActive(false);
                once_again.GetComponent<Animator>().Play("lets devide new clip");
                //write "Fractions are like division " as heading
                Invoke("enable_division_corutine", 10.0f);
                break;

            case "obj_13_We_all_have_learnt":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("We all have learnt division. Let’s see if we are able to remember it.");
                break;

            case "obj_13_here_are_three_labels":
                heading.text = "Drag and drop these labels at their right place";
                show_the_devision.SetActive(false);
                here_are_thre_labels_activity.SetActive(true);
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("");
               GameObject.FindObjectOfType<conversationManager>().DisableConversation();
                break;
            case "obj_13_Mixed_fraction_is_always":
                here_are_thre_labels_activity.SetActive(false);
                mixed_fractions_is_always.SetActive(true);
                chef_panel_okay.SetActive(true);
               
                heading.text = "A Mixed Fraction is always written as";
                // GameObject.FindObjectOfType<conversationManager>().EnableConversation(" Whole Number \\frac{Numerator}{Denominator}       which is same as       Quotient \\frac{Remainder}{Divisor}");
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("");
                GameObject.FindObjectOfType<conversationManager>().DisableConversation();
                break;

            case "obj_13_so_write_quotient_3":
                chef_panel_okay.SetActive(true);
                mixed_fractions_is_always.SetActive(false);
                so_write_quotient_3.SetActive(true);
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("");
                GameObject.FindObjectOfType<conversationManager>().DisableConversation();
                // GameObject.FindObjectOfType<conversationManager>().EnableConversation("So we write the quotient 3 as the whole number of the Mixed fraction, as it tells how many wholes can be formed in the given fraction. ");
               
                break;

            case "obj_13_this_is_same_as_7_b_2":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("This same as 7 half slices pizzas giving us 3 whole pizzas.");
                // so_write_quotient_3.SetActive(true);
                this_is_same_as_half.SetActive(true);
                Invoke("nextObjectiveVo", 17.0f);
                break;

            case "obj_13_The_Remainder_1_is_written":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("The remainder 1 is written as the numerator of the Mixed Fraction");
                so_write_quotient_3.SetActive(false);
                this_is_same_as_half.SetActive(false);
                the_remainder_1_is.SetActive(true);
                Invoke("show_1b3", 8);

                break;
            case "obj_13_The_denominator_of_the_fraction":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Denominator of the fraction is the divisor.");
                the_remainder_1_is.SetActive(false);
                show31b2.SetActive(false);
                The_denominator_of_the_fraction.SetActive(true);
                break;

            case "obj_13_This_indicates_the_number_of":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("This indicates the number of parts in the improper and the mixed fraction is the same.");
                This_indicates_the_number_of.SetActive(true);
                break;
            case "obj_13_so_the_denominator_of_mix":
                so_we_write_2.SetActive(true);
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("So, the denominator of the Mixed Fraction would be the same as the denominator of the Improper fraction. ");
                break;
            case "obj_13_So_we_write_2_as":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("So we write 2 as the denominator. ");
               
                break;

            case "obj_13_7b2_is_an_improper_fraction_that":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("\\frac{7}{2} is an improper fraction that has a value more than 1 and has a numerator greater than 1. When \\frac{7}{2} is converted to mixed fraction we get 3\\frac{1}{2}.");

                break;

            case "obj_13_When_we_say_3_1b2":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("When we say 3\\frac{1}{2}, it is 3 wholes + \\frac{1}{2} which is nothing but a combination of a whole number and a proper fraction. ");

                break;

            case "obj_13_let_us_once_again_have_a_look_at":
                so_we_write_2.SetActive(false);
                This_indicates_the_number_of.SetActive(false);
                The_denominator_of_the_fraction.SetActive(false);
                let_us_once_again_have_a_look_at.SetActive(true);
                heading.text = "Steps";
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("let us once again have a look at the steps that are required to convert an Improper Fraction to a mixed fraction.");
                break;
            case "obj_13_let_us_step_1":
                let_us_step_1_divide_the_numerator.SetActive(true);
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Step 1: Divide the numerator of the Improper fraction with its denominator.");
                Invoke("nextObjectiveVo", 10);
                break;
            case "obj_13_let_us_step_2":
                let_us_step_1_divide_the_numerator.SetActive(false);
                let_us_step_2_the_quotient.SetActive(true);
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Step 2: The quotient that you obtain from Step 1, becomes the whole number and the remainder becomes the numerator of the Mixed Fraction and the divisor becomes the denominator.");

                Invoke("enableFade", 15.0f);
                Invoke("enableROQuest3", 18.0f);
                break;

            case "obj_13_RO_3_we_can_convert":

                heading.text = "";


                break;

            case "obj_13_RO_4_which_of_the":
                enableROQuest4();
                break;


        }

    }


    void enable_3_1b2()
    {
        three_1b2.SetActive(true);
    }


    public void enable_three_labels_submit()
    {
        labels_count++;
        if (labels_count == 3)
        {
            three_labels_submit.SetActive(true);
        }
    }
    void validate_three_label_submit()
    {
        three_labels_submit.SetActive(false);
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(dialougue_ok_for_label_activity);
        if (!quotient && !divisor && !remainder)
        {
            GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
            StartCoroutine(Divisor_wrong_Quotient_wrong_Remainder_wrong());
            quot_wrng();
            div_wrng();
            remai_wrng();
        }
        else if (quotient && !divisor && !remainder)
        {
            GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
            StartCoroutine(Remainder_wrong_Divisor_wrong());
            div_wrng();
            remai_wrng();
        }
        else if (!quotient && divisor && !remainder)
        {
            GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
            StartCoroutine(Quotient_wrong_Remainder_wrong());
            quot_wrng();
            remai_wrng();
        }
        else if (!quotient && !divisor && remainder)
        {
            GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
            StartCoroutine(Divisor_wrong_Quotient_wrong());
            quot_wrng();
            div_wrng();
           
        }
        else if (!quotient && divisor && remainder)
        {
            GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
            StartCoroutine(quot_wrong_load_next());
            quot_wrng();
           
        }
        else if (quotient && !divisor && remainder)
        {
            GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
            StartCoroutine(div_wrong_load_next());
            div_wrng();

        }
        else if (quotient && divisor && !remainder)
        {
            GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
            StartCoroutine(remain_wronf_load_next());
            remai_wrng();

        }
        else if (quotient && divisor && remainder)
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            StartCoroutine(everything_is_right());
        }
    }

    IEnumerator Divisor_wrong_Quotient_wrong_Remainder_wrong()
    {
        yield return new WaitForSeconds(0f);
        yield return StartCoroutine(Divisor_wrong());
        yield return new WaitForSeconds(0.5f);
        //if (!Application.isEditor)
        //{
        //    yield return new WaitForSeconds(FindObjectOfType<timeline_new>().length_of_audio);
        //}
        //else
        //{
        //    yield return new WaitForSeconds(4);
        //}

        yield return StartCoroutine(Quotient_wrong());
        yield return new WaitForSeconds(0.5f);
        //if (!Application.isEditor)
        //{
        //    yield return new WaitForSeconds(FindObjectOfType<timeline_new>().length_of_audio);
        //}
        //else
        //{
        //    yield return new WaitForSeconds(4);
        //}

        yield return StartCoroutine(Remainder_wrong());
        yield return new WaitForSeconds(0.5f);
        //if (!Application.isEditor)
        //{
        //    yield return new WaitForSeconds(FindObjectOfType<timeline_new>().length_of_audio);
        //}
        //else
        //{
        //    yield return new WaitForSeconds(4);
        //}

        FindObjectOfType<timeline_new>().load_next();
    }

    IEnumerator Divisor_wrong_Quotient_wrong()
    {
        yield return new WaitForSeconds(0f);

        yield return StartCoroutine(Divisor_wrong());
        yield return new WaitForSeconds(0.5f);
       

        yield return StartCoroutine(Quotient_wrong());
        yield return new WaitForSeconds(0.5f);
        //if (!Application.isEditor)
        //{
        //    yield return new WaitForSeconds(FindObjectOfType<timeline_new>().length_of_audio);
        //}
        //else
        //{
        //    yield return new WaitForSeconds(4);
        //}
        FindObjectOfType<timeline_new>().load_next();

    }

    IEnumerator Quotient_wrong_Remainder_wrong()
    {
        yield return StartCoroutine(Quotient_wrong());
        yield return new WaitForSeconds(0.5f);
        //if (!Application.isEditor)
        //{
        //    yield return new WaitForSeconds(FindObjectOfType<timeline_new>().length_of_audio);
        //}
        //else
        //{
        //    yield return new WaitForSeconds(4);
        //}

        yield return StartCoroutine(Remainder_wrong());
        yield return new WaitForSeconds(0.5f);
        //if (!Application.isEditor)
        //{
        //    yield return new WaitForSeconds(FindObjectOfType<timeline_new>().length_of_audio);
        //}
        //else
        //{
        //    yield return new WaitForSeconds(4);
        //}
        FindObjectOfType<timeline_new>().load_next();
    }

    IEnumerator Remainder_wrong_Divisor_wrong()
    {
        yield return StartCoroutine(Divisor_wrong());
        yield return new WaitForSeconds(0.5f);
        //if (!Application.isEditor)
        //{
        //    yield return new WaitForSeconds(FindObjectOfType<timeline_new>().length_of_audio);
        //}
        //else
        //{
        //    yield return new WaitForSeconds(4);
        //}

        yield return StartCoroutine(Remainder_wrong());
        yield return new WaitForSeconds(0.5f);
        //if (!Application.isEditor)
        //{
        //    yield return new WaitForSeconds(FindObjectOfType<timeline_new>().length_of_audio);
        //}
        //else
        //{
        //    yield return new WaitForSeconds(4);
        //}
        FindObjectOfType<timeline_new>().load_next();
    }

    IEnumerator Divisor_wrong()
    {
        yield return new WaitForSeconds(0f);
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_13_divisor_wrong.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableConversation("The number by which we divide is called the divisor – Here we are dividing 7 by 2, hence the divisor is 2");
        var p = FindObjectsOfType<obj13_drop_me>();
        foreach (obj13_drop_me d in p)
        {
            if (d.gameObject.name.Equals("Divisor"))
            {
                d.set_image_to_actual();
            }
        }
        if (!Application.isEditor)
        {
            yield return new WaitForSeconds(FindObjectOfType<timeline_new>().length_of_audio);
        }
        else
        {
            yield return new WaitForSeconds(4);
        }
    }

    IEnumerator Quotient_wrong()
    {
        yield return new WaitForSeconds(0f);
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_13_quotient_wrong.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableConversation("The result obtained by division is called the quotient. Therefore, the quotient is 3");
        var p = FindObjectsOfType<obj13_drop_me>();
        foreach (obj13_drop_me d in p)
        {
            if (d.gameObject.name.Equals("Quotient"))
            {
                d.set_image_to_actual();
            }
        }
        if (!Application.isEditor)
        {
            yield return new WaitForSeconds(FindObjectOfType<timeline_new>().length_of_audio);
        }
        else
        {
            yield return new WaitForSeconds(4);
        }
    }

    IEnumerator Remainder_wrong()
    {
        yield return new WaitForSeconds(0f);
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_13_remainder_wrong.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableConversation("The left over number after division is called the  Remainder. Here, the remainder is 1");
        var p = FindObjectsOfType<obj13_drop_me>();
        foreach (obj13_drop_me d in p)
        {
            if (d.gameObject.name.Equals("Remainder"))
            {
                d.set_image_to_actual();
            }
        }
        if (!Application.isEditor)
        {
            yield return new WaitForSeconds(FindObjectOfType<timeline_new>().length_of_audio);
        }
        else
        {
            yield return new WaitForSeconds(4);
        }
    }


    void div_wrng()
    {
        var p = FindObjectsOfType<obj13_drop_me>();
        foreach (obj13_drop_me d in p)
        {
            if (d.gameObject.name.Equals("Divisor"))
            {
                d.GetComponent<Image>().color = Color.red;
            }
        }
    }

    void quot_wrng()
    {
        var p = FindObjectsOfType<obj13_drop_me>();
        foreach (obj13_drop_me d in p)
        {
            if (d.gameObject.name.Equals("Quotient"))
            {
                d.GetComponent<Image>().color = Color.red;
            }
        }
    }
    void remai_wrng()
    {
        var p = FindObjectsOfType<obj13_drop_me>();
        foreach (obj13_drop_me d in p)
        {
            if (d.gameObject.name.Equals("Remainder"))
            {
                d.GetComponent<Image>().color = Color.red;
            }
        }
    }






    IEnumerator div_wrong_load_next()
    {

        yield return new WaitForSeconds(0f);
        yield return StartCoroutine(Divisor_wrong());
        yield return new WaitForSeconds(0.5f);
        FindObjectOfType<timeline_new>().load_next();
    }

    IEnumerator quot_wrong_load_next()
    {

        yield return new WaitForSeconds(0f);
        yield return StartCoroutine(Quotient_wrong());
        yield return new WaitForSeconds(0.5f);
        FindObjectOfType<timeline_new>().load_next();
    }
    IEnumerator remain_wronf_load_next()
    {

        yield return new WaitForSeconds(0f);
        yield return StartCoroutine(Remainder_wrong());
        yield return new WaitForSeconds(0.5f);
        FindObjectOfType<timeline_new>().load_next();
    }


    IEnumerator everything_is_right()
    {

        yield return new WaitForSeconds(0f);
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_13_everything_right.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("");
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
    }


    void dialougue_ok_for_label_activity()
    {
        FindObjectOfType<timeline_new>().lipsync_player.GetComponent<AudioSource>().Stop();
        CancelInvoke();
        StopAllCoroutines();
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
        GameObject.FindObjectOfType<conversationManager>().DisableDialouge();
        GameObject.FindObjectOfType<timeline_new>().load_next();
    }



    void show_1b3()
    {
        show31b2.SetActive(true);
        Invoke("nextObjectiveVo", 10.0f);


    }

    void enableFade()
    {

        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
        Invoke("nextObjective", 3.0f);
    }
    void nextObjective()
    {
        Invoke("nextObjectiveVo", 3.0f);
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadeout");
    }
    void nextObjectiveVo()
    {

        GameObject.FindObjectOfType<timeline_new>().load_next();
    }

    public void enableROQuest1()
    {
        GameObject.Find("RO Panel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        GameObject.FindObjectOfType<MixedObj13ROManager>().Initiliaze();
    }
    void enableROQuest2()
    {
        GameObject.Find("RO Panel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        GameObject.FindObjectOfType<MixedObj13ROManager>().EnableSubmitButtonRO2();
    }
    void enableROQuest3()
    {
        let_us_once_again_have_a_look_at.SetActive(false);
        let_us_step_2_the_quotient.SetActive(false);
        GameObject.FindObjectOfType<conversationManager>().DisableConversation();
        GameObject.Find("RO Panel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        GameObject.FindObjectOfType<MixedObj13ROManager>().EnableSubmitButtonRO3();
    }
    void enableROQuest4()
    {
        GameObject.Find("RO Panel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(3).gameObject.SetActive(true);
        GameObject.FindObjectOfType<MixedObj13ROManager>().EnableSubmitButtonRO4();
    }

    void enable_division_corutine()
    {
        once_again.SetActive(false);
        show_the_devision.SetActive(true);
        StartCoroutine(enableDivision());
    }

    IEnumerator enableDivision()
    {
        yield return new WaitForSeconds(1.0f);
        GameObject.Find("CorrectSound").GetComponent<AudioSource>().volume = 0.5f;
        GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
        show_the_devision.GetComponent<TEXDraw>().text = "   \n2\\root[)]7\n";
        yield return new WaitForSeconds(1.0f);
        GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
        show_the_devision.GetComponent<TEXDraw>().text = "   3\n 2\\root[)]7\n";
        yield return new WaitForSeconds(1.0f);
        GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
        show_the_devision.GetComponent<TEXDraw>().text = "   3\n 2\\root[)]7\n- 6\n";
        yield return new WaitForSeconds(1.0f);
        GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
        show_the_devision.GetComponent<TEXDraw>().text = "   3\n 2\\root[)]7\n- 6\n  	\\border[03]1";
        yield return new WaitForSeconds(3.0f);
        GameObject.Find("CorrectSound").GetComponent<AudioSource>().volume = 1f;
        nextObjectiveVo();
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            FindObjectOfType<timeline_new>().load_next();
        }
    }

    public void select_pizza()
    {

        Debug.Log("hello");
        var currentEventSystem = EventSystem.current;
        var currentSelectedGameObject = currentEventSystem.currentSelectedGameObject;
        //  currentSelectedGameObject.GetComponent<Button>().enabled = false;
        currentSelectedGameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        disable_pizza_click();

        slice_counter++;
        currentSelectedGameObject.GetComponent<move_to_b>().enabled = true;
        if (slice_counter == 1)
        {

            currentSelectedGameObject.transform.localScale = new Vector3(-(currentSelectedGameObject.transform.localScale.x), currentSelectedGameObject.transform.localScale.y, currentSelectedGameObject.transform.localScale.z);
            if (plate_counter == 0)
            {
                currentSelectedGameObject.GetComponent<move_to_b>().desired_pos = plate_1.transform.GetChild(0).position;
                currentSelectedGameObject.transform.parent = plate_1.transform;
                currentSelectedGameObject.GetComponent<move_to_b>().transition_speed = 750;
                currentSelectedGameObject.GetComponent<move_to_b>().move = true;

            }
            if (plate_counter == 1)
            {
                currentSelectedGameObject.GetComponent<move_to_b>().desired_pos = plate_2.transform.GetChild(0).position;
                currentSelectedGameObject.transform.parent = plate_2.transform;
                currentSelectedGameObject.GetComponent<move_to_b>().transition_speed = 750;
                currentSelectedGameObject.GetComponent<move_to_b>().move = true;

            }
            if (plate_counter == 2)
            {
                currentSelectedGameObject.GetComponent<move_to_b>().desired_pos = plate_3.transform.GetChild(0).position;
                currentSelectedGameObject.transform.parent = plate_3.transform;
                currentSelectedGameObject.GetComponent<move_to_b>().transition_speed = 750;
                currentSelectedGameObject.GetComponent<move_to_b>().move = true;

            }
            if (plate_counter == 3)
            {
                currentSelectedGameObject.GetComponent<move_to_b>().desired_pos = plate_4.transform.GetChild(0).position;
                currentSelectedGameObject.transform.parent = plate_4.transform;
                currentSelectedGameObject.GetComponent<move_to_b>().transition_speed = 750;
                currentSelectedGameObject.GetComponent<move_to_b>().move = true;

            }
        }
        else if (slice_counter == 2)
        {
            //currentSelectedGameObject.transform.localScale = new Vector3(-(currentSelectedGameObject.transform.localScale.x), currentSelectedGameObject.transform.localScale.y, currentSelectedGameObject.transform.localScale.z);
            if (plate_counter == 0)
            {
                currentSelectedGameObject.GetComponent<move_to_b>().desired_pos = plate_1.transform.GetChild(1).position;
                currentSelectedGameObject.transform.parent = plate_1.transform;
                currentSelectedGameObject.GetComponent<move_to_b>().transition_speed = 750;
                currentSelectedGameObject.GetComponent<move_to_b>().move = true;
            }
            if (plate_counter == 1)
            {
                currentSelectedGameObject.GetComponent<move_to_b>().desired_pos = plate_2.transform.GetChild(1).position;
                currentSelectedGameObject.transform.parent = plate_2.transform;
                currentSelectedGameObject.GetComponent<move_to_b>().transition_speed = 750;
                currentSelectedGameObject.GetComponent<move_to_b>().move = true;

            }
            if (plate_counter == 2)
            {
                currentSelectedGameObject.GetComponent<move_to_b>().desired_pos = plate_3.transform.GetChild(1).position;
                currentSelectedGameObject.transform.parent = plate_3.transform;
                currentSelectedGameObject.GetComponent<move_to_b>().transition_speed = 750;
                currentSelectedGameObject.GetComponent<move_to_b>().move = true;

            }
            if (plate_counter == 3)
            {
                currentSelectedGameObject.GetComponent<move_to_b>().desired_pos = plate_4.transform.GetChild(1).position;
                currentSelectedGameObject.transform.parent = plate_4.transform;
                currentSelectedGameObject.GetComponent<move_to_b>().transition_speed = 750;
                currentSelectedGameObject.GetComponent<move_to_b>().move = true;

            }
        }



    }

    public void enable_pizza_click()
    {
        foreach (GameObject s in pizza_slices)
        {
            s.GetComponent<Button>().interactable = true;
        }
    }

    public void disable_pizza_click()
    {
        foreach (GameObject s in pizza_slices)
        {
            s.GetComponent<Button>().interactable = false;
        }
    }


    public void reached_two_slices()
    {
        slice_counter = 0;
        if (plate_counter == 0)
        {
            plate_1.GetComponent<move_to_b>().enabled = true;
            plate_1.GetComponent<move_to_b>().desired_pos = GameObject.Find("plate_1_pos").transform.position;
            plate_1.GetComponent<move_to_b>().move = true;
            plate_1.GetComponent<move_to_b>().transition_speed = 900f;
            plate_counter++;
            enable_pizza_click();
            plate_to_active = plate_2;
        }
        else if (plate_counter == 1)
        {
            plate_2.GetComponent<move_to_b>().enabled = true;
            plate_2.GetComponent<move_to_b>().desired_pos = GameObject.Find("plate_2_pos").transform.position;
            plate_2.GetComponent<move_to_b>().move = true;
            plate_2.GetComponent<move_to_b>().transition_speed = 900f;
            plate_counter++;
            enable_pizza_click();
            plate_to_active = plate_3;
        }
        else if (plate_counter == 2)
        {
            plate_3.GetComponent<move_to_b>().enabled = true;
            plate_3.GetComponent<move_to_b>().desired_pos = GameObject.Find("plate_3_pos").transform.position;
            plate_3.GetComponent<move_to_b>().move = true;
            plate_3.GetComponent<move_to_b>().transition_speed = 900f;
            plate_counter++;
            enable_pizza_click();
            plate_to_active = plate_4;
        }
        else if (plate_counter == 3)
        {
            plate_4.GetComponent<move_to_b>().enabled = true;
            plate_4.GetComponent<move_to_b>().desired_pos = GameObject.Find("plate_4_pos").transform.position;
            plate_4.GetComponent<move_to_b>().move = true;
            plate_4.GetComponent<move_to_b>().transition_speed = 900f;
            plate_counter++;
            pizza_combine_activity.GetComponent<Animator>().Play("move up");
            disable_pizza_click();
            Invoke("nextObjectiveVo", 5);
        }
    }



    public void activate_pizza_plate()
    {
        plate_to_active.SetActive(true);
        enable_pizza_click();
    }

}