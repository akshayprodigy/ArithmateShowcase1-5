using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class Obj6Manager : MonoBehaviour
{
    string jsonFileName = "Obj6NumberLine.json";

    public static int SelectedPart;
    public static string totalApple, redApple;
    public GameObject Number_Line_text, extend_line, arrow, pointer, fract_highlight, four_parts, threebfive, each_part_highlight, fraction34,ro2_line;
    public GameObject LoadingAudio;
    public TEXDraw  headings;
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
        Debug.Log("selected =" + SelectedPart);
    }
    void Initialised()
    {
        LoadingAudio = GameObject.Find("LoadAudio");
        //if (UtilityArtifacts.backTraversal)
        //{
        //    Text textLoadingText = LoadingAudio.transform.GetChild(1).GetComponent<Text>();
        //    textLoadingText.text = "Let us understand this better";
        //}
        totalApple = AppleManager.totalAppleCollected.ToString();
        redApple= AppleManager.CollectedFullRedApple.ToString();
        GameObject.FindObjectOfType<conversationManager>().GetHintOkButton();
        Number_Line_text = GameObject.Find("Number_Line_text");
        Number_Line_text.SetActive(false);

        headings = GameObject.Find("Headings").GetComponent<TEXDraw>();
        headings.gameObject.SetActive(false);

        extend_line = GameObject.Find("lets_extend");
        extend_line.SetActive(false);

        arrow = GameObject.Find("arrow");
        arrow.SetActive(false);

        pointer = GameObject.Find("pointer");
        pointer.SetActive(false);

        fract_highlight = GameObject.Find("fract_highlight");
        four_parts = GameObject.Find("4 parts highlight");
        fract_highlight.SetActive(false);
        four_parts.SetActive(false);

        each_part_highlight = GameObject.Find("each part highlight");
        each_part_highlight.SetActive(false);

        threebfive = GameObject.Find("3b5");
        threebfive.SetActive(false);

        fraction34 = GameObject.Find("fraction_34");
        fraction34.SetActive(false);

        ro2_line = GameObject.Find("ro2_line");
        ro2_line.SetActive(false);
        Invoke("audio_invoke", 2.0f);
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
            case "Obj6_We_have":
                // GameObject.FindObjectOfType<conversationManager>().EnableConversation("We have already seen that fractions can be used to represent parts of a whole");
                headings.gameObject.SetActive(true);
                headings.text = "Fractions represent parts of a whole";
                Number_Line_text.SetActive(true);
                break;

            case "Obj6_Lets_extend":
                Number_Line_text.SetActive(false);
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Let’s extend this concept to think about fractions as numbers on a number line");
                headings.text = "";
                extend_line.SetActive(true);
               enableLine();

                break;
            case "Obj6_First":
                extend_line.SetActive(false);
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("First, lets draw a number line");
               
                break;
            case "Obj6_we_mark":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("We mark 0 on the left end of the number line");
                enableFirstPoint();
                break;
            case "Obj6_and_on":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("And on the right end, we mark 1");
                enableSecondPoint();
                break;
            case "Obj6_we_know":
                GameObject.FindObjectOfType<conversationManager>().DisableConversation();
                //GameObject.FindObjectOfType<conversationManager>().EnableConversation("We know that the value of a fraction is less than 1.");
                headings.text = "Value of fraction is less than 1";
                break;
            case "Obj6_so_fraction":
               
                                GameObject.FindObjectOfType<conversationManager>().EnableConversation("So, fractions whose value is less than 1 will be between 0 and 1 on the number line.");
                headings.text = "Value of fraction is less than 1";
                pointer.SetActive(true);
                break;
            case "Obj6_Here_is_a":
                pointer.SetActive(false);
                pointer.SetActive(true);
                headings.text = "";
              enableFraction();
                arrow.SetActive(true);
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Let us plot it on the number line.");
               
                break;
            case "Obj6_We_have_to_plot":
               
                pointer.SetActive(false);
                pointer.SetActive(true);
               
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("We have to plot \\frac{3}{4} between 0 and 1.");
                break;
            case "Obj6_lets_figure":
                pointer.SetActive(false);
                arrow.SetActive(false);
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Let’s figure out where the fraction will lie on the number line.");
                break;
            case "Obj6_The_first_thing":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("The first thing that you need to remember is – The number of parts on the number line is equal to the denominator of the fraction.");
                break;
            case "Obj6_Here_the_denominator_4":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Here, the denominator is 4 so the number line will have to be divided into 4 equal parts");
                highLightfractionDenum();
                enableFirstLineOnly();
                four_parts.SetActive(true);
                fract_highlight.SetActive(true);
                break;
            case "Obj6_As_you_can":
                four_parts.SetActive(false);
                fract_highlight.SetActive(false);
                each_part_highlight.SetActive(true);
                headings.text = "Each Part is equal";
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("As you can see, all the points are marked at an equal distance from each other.");
                
                break;
            case "Obj6_lets_go_ahead":
                headings.text = "";
                each_part_highlight.SetActive(false);
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Let’s go ahead and mark the number line with the given fraction");
                break;
            case "Obj6_The_first_point":
                firstPoint();
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("The first point from zero will be \\frac{1}{4}");
                break;
            case "Obj6_second_point":
                secondPoint();
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("The second point \\frac{2}{4}");
                break;
            case "Obj6_third_point":
                thirdPoint();
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Third will be \\frac{3}{4}");
                break;
            case "Obj6_fourth_point":
                fourthPoint();
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("And the last point \\frac{4}{4}, which is same as 1");
                break;
            case "Obj6_now_lets_place":
                Invoke("markerPoint", 4);
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Now lets place a marker on where \\frac{3}{4} is");
               
                break;
            case "Obj6_this_is_how":
               
                Invoke("enableFade", 6.0f);
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("This is how you plot a fraction on a number line.");
                break;
            case "Obj6_now_that":
                enabletray();
               
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Now that you know how to plot fractions on the number line, let us see how we can represent the apples that you have collected on the number line");
                break;
            case "Obj6_Enter_the_fraction":
                enabletray();
                enableLine();
                GameObject.Find("NumberLine").transform.GetChild(0).gameObject.SetActive(false);
                GameObject.FindObjectOfType<conversationManager>().DisableConversation();
                GameObject.FindObjectOfType<conversationManager>().EnableQuestion("Enter the fraction of red apples collected");
                Invoke("enableQuestion",0.0f);
                break;
            
            case "Obj6_Here_the_denominator":
                GameObject.FindObjectOfType<conversationManager>().DisableQuestion();
                threebfive.SetActive(true);
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Let’s represent \\frac{3}{5} on the number line. Here the denominator is 5 so the number line has to be divided in 5 parts");
                enableLinePoint();
                break;
            case "Obj6_final_plot_point":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Let us now plot all the points");
                plotPoints();
                break;
            case "Obj6_Let_us_identify":
                GameObject.FindObjectOfType<conversationManager>().DisableConversation();
               //GameObject.FindObjectOfType<conversationManager>().EnableConversation("Let us identify and plot \\frac{3}{5} on the number line. Tap on the numbers to plot the fraction");
               headings.text = "Tap on the numbers to plot the fraction";
                break;
            case "Obj6_quest1":
                threebfive.SetActive(false);
                headings.text = "";
                break;
            case "Obj6_quest2":
                enableROQuest2();
                
                break;
            case "Obj6_quest3":
                enableROQuest3();
                
                break;
            

        }
        
    }
    void enableLine()
    {
        GameObject.Find("NumberLine").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("NumberLine").transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
    }
    void enabletray()
    {
        GameObject.Find("Canvas").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.FindObjectOfType<Obj6AppleGenerator>().Initialize();
    }
   void enableFirstPoint()
    {
        GameObject.Find("Line").transform.GetChild(1).gameObject.SetActive(true);
    }
    void enableSecondPoint()
    {
        GameObject.Find("Line").transform.GetChild(2).gameObject.SetActive(true);
    }
    void disableAlltPoint()
    {
        GameObject.Find("Line").transform.GetChild(1).gameObject.SetActive(false);
        GameObject.Find("Line").transform.GetChild(2).gameObject.SetActive(false);
        GameObject.Find("Line").transform.GetChild(3).gameObject.SetActive(false);
        GameObject.Find("Line").transform.GetChild(4).gameObject.SetActive(false);
        GameObject.Find("Line").transform.GetChild(5).gameObject.SetActive(false);
        GameObject.Find("Line").transform.GetChild(6).gameObject.SetActive(false);
        GameObject.Find("Line").transform.GetChild(7).gameObject.SetActive(false);
    }
    void enableFirstLineOnly()
    {
        GameObject.Find("Line").transform.GetChild(3).gameObject.SetActive(true);
        GameObject.Find("Line").transform.GetChild(4).gameObject.SetActive(true);
        GameObject.Find("Line").transform.GetChild(5).gameObject.SetActive(true);
    }
    void enableFraction()
    {
        fraction34.gameObject.SetActive(true);
        fraction34.SetActive(true);
    }
    void highLightfractionDenum()
    {
       // fraction34.gameObject.GetComponent<TEXDraw>().text = "\\frac{3}{\\bg[cyan4]}";
        Invoke("unhighLightfractionDenum", 4.0f);
    }
    void unhighLightfractionDenum()
    {
        fraction34.gameObject.GetComponent<TEXDraw>().text = "\\frac{3}{4}";
    }
    void enableFade()
    {
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
        Invoke("nextObjective", 3.0f);
    }
    void nextObjective()
    {      
       Invoke("nextObjectiveVo", 3.0f);
       // GameObject.Find("NumberLine").transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        fraction34.SetActive(false);
        disableAlltPoint();
        GameObject.FindObjectOfType<conversationManager>().DisableConversation();
      //  GameObject.Find("NumberLine").transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        GameObject.Find("NumberLine").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadeout");
    }
    void nextObjectiveVo()
    {
        GameObject.FindObjectOfType<timeline_new>().load_next();
    }
    void firstPoint()
    {
        GameObject.Find("Line").transform.GetChild(3).GetChild(2).gameObject.SetActive(true);
    }
    void secondPoint()
    {
        GameObject.Find("Line").transform.GetChild(4).GetChild(2).gameObject.SetActive(true);
    }
    void thirdPoint()
    {
        GameObject.Find("Line").transform.GetChild(5).GetChild(2).gameObject.SetActive(true);
    }
    void fourthPoint()
    {
         GameObject.Find("Line").transform.GetChild(2).GetChild(2).gameObject.SetActive(true);
        //GameObject.Find("Line").transform.GetChild(6).gameObject.SetActive(true);
        Debug.Log("Hello");
    }
    void markerPoint()
    {
        GameObject.Find("Line").transform.GetChild(7).gameObject.SetActive(true);
    }
    void enableQuestion()
    {
        GameObject.FindObjectOfType<conversationManager>().DisableConversation();
       GameObject.FindObjectOfType<QuestionManager>().EnableForObj6Quest1();
    }
    void enableLinePoint()
    {
        GameObject.FindObjectOfType<NumberLineManager>().createDivision(5);
    }
    void plotPoints()
    {
       
        GameObject.FindObjectOfType<NumberLineManager>().setText();
    }
   public void enableROQuest1()
    {
        GameObject.Find("RO Panel").transform.GetChild(0).gameObject.SetActive(true);
       
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        GameObject.FindObjectOfType<AplleObj6ROManager>().Initiliaze();
    }
    void enableROQuest2()
    {
        GameObject.Find("RO Panel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        GameObject.FindObjectOfType<AplleObj6ROManager>().EnableSubmitButtonRO2();
    }
    void enableROQuest3()
    {
        GameObject.Find("RO Panel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        GameObject.FindObjectOfType<AplleObj6ROManager>().EnableSubmitButtonRO3();
    }
}
