using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class Obj7Manager : MonoBehaviour
{
    string jsonFileName = "Obj7ReadingFraction.json";

    public static int SelectedPart;
    public static string totalApple, redApple;
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
        Debug.Log("selected =" + SelectedPart);
    }
    void Initialised()
    {
        totalApple = AppleManager.totalAppleCollected.ToString();
        redApple = AppleManager.CollectedFullRedApple.ToString();
        GameObject.FindObjectOfType<conversationManager>().GetHintOkButton();
        LoadingAudio = GameObject.Find("LoadAudio");
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
            case "Obj7_Before_we":
                enableHeading();
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Before we understand how to read fractions or write fractions in words, Let us first understand the concept of Cardinal Numbers and Ordinal Numbers.");
                break;
            case "Obj7_Cardinal_Numbers":
                enableFirstLine();
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Cardinal Numbers are numbers that denote quantity and are used for counting, such as one, two, three, four, and so on");
                break;
            case "Obj7_Numbers_that":
                enableSecondLine();
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Numbers that define position or order, such as ‘first’, ‘second’, or ‘third’ are called Ordinal Numbers");
                break;
            case "Obj7_Now_that":
                enabletrey();
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Now that we know what are Cardinal Numbers and what are Ordinal Numbers, let us learn how to read fractions.");
                break;
            case "Obj7_As_you":
                //enableFirstfrac();
                GameObject.Find("NumberLine").transform.GetChild(0).GetChild(9).gameObject.SetActive(true);
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("As you can see 3 out of 5 apples are red.");
                break;
            case "Obj7_To_read":
                enableHint();
                //GameObject.FindObjectOfType<conversationManager>().EnableConversation("To read or write this fraction in words, we use Cardinal Numbers to read the numerator and Ordinal numbers to read the denominator");
                break;
            case "Obj7_This_fraction":
                disableHint();
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("This fraction can be read as three-fifths");
                break;
            case "Obj7_Let":
                GameObject.Find("NumberLine").transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
                GameObject.Find("NumberLine").transform.GetChild(0).GetChild(10).gameObject.SetActive(false);
                GameObject.Find("NumberLine").transform.GetChild(0).GetChild(11).gameObject.SetActive(false);
                enableOtherExample();
                GameObject.FindObjectOfType<conversationManager>().DisableConversation();
                break;
            case "Obj7_The_fraction":
                onScreenOneThird();
                //GameObject.FindObjectOfType<conversationManager>().EnableConversation("The fraction one by three is read as one-third");
                break;
            case "Obj7_Where":
                onScreenOneHighlight();
                //GameObject.FindObjectOfType<conversationManager>().EnableConversation("Where one is read as cardinal number");
                break;
            case "Obj7_And_3":
                onScreenThirdHighlight();
                //GameObject.FindObjectOfType<conversationManager>().EnableConversation("And 3 is read as ordinal number");
                break;
            case "Obj7_We_consider":
                singular();
                //GameObject.FindObjectOfType<conversationManager>().EnableConversation("We consider the denominator as singular when the numerator is one");
                break;
            case "Obj7_and_plural":
                plural();
                //GameObject.FindObjectOfType<conversationManager>().EnableConversation("and plural when the numerator is not one");
                break;
            case "Obj7_Lo1":
                GameObject.Find("NumberLine").transform.GetChild(0).GetChild(7).gameObject.SetActive(false);
                GameObject.Find("NumberLine").transform.GetChild(0).GetChild(8).gameObject.SetActive(false);
                GameObject.Find("NumberLine").transform.GetChild(0).GetChild(3).GetComponent<TEXDraw>().text= "\\frac{1}{4} is one – fourth ";
                GameObject.Find("NumberLine").transform.GetChild(0).GetChild(4).GetComponent<TEXDraw>().text = "\\frac{2}{4} is two – fourths ";
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("The numerator is read as cardinal numbers and the denominator is read as ordinal numbers");
                Invoke("enableFade",5.0f);
                Invoke("enableROQuest1", 8.0f);
                break;
            case "Obj7_quest1":
               
                break;
            case "Obj7_Lo2":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("The denominator is read as ordinal numbers and denominators are read as plurals for numerators more than 1.");
                Invoke("enableFade", 6.0f);
                Invoke("enableROQuest2", 9.0f);
                break;
            case "Obj7_quest2":
                enableROQuest2();
                break;
            case "Obj7_quest3":
                enableROQuest3();
                break;
            case "Obj7_Lo3":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("1/2 is called half and 1/4 is read as one - quarter or one fourths 4/4 is read as four - fourths or four - quarters or one");
                Invoke("enableFade", 6.0f);
                Invoke("enableROQuest4", 9.0f);
                break;
            case "Obj7_quest4":
                enableROQuest4();
                break;
            case "Obj7_You":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("You have just learnt how to read fractions");
                break;
            case "Obj7_Here_is_a_wheel":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Here’s a wheel on which we have different fractions. And here is a pointer.");
                break;
            case "Obj7_Tap":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Tap on the spin button to make wheel spin.");
                break;
            case "Obj7_Once":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Once the wheel stops, read out that fraction by clicking on the mike button");
                break;



        }

    }
    void enableHeading()
    {
        GameObject.Find("LineOnScreen").transform.GetChild(2).gameObject.SetActive(true);
    }
    void enableFirstLine()
    {
        GameObject.Find("LineOnScreen").transform.GetChild(2).gameObject.SetActive(false);
        GameObject.Find("LineOnScreen").transform.GetChild(0).gameObject.SetActive(true);
    }
    void enableSecondLine()
    {
        GameObject.Find("LineOnScreen").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("LineOnScreen").transform.GetChild(1).gameObject.SetActive(true);
    }
    void disableAllLine()
    {
        GameObject.Find("LineOnScreen").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("LineOnScreen").transform.GetChild(1).gameObject.SetActive(false);
    }
    void enabletrey()
    {
        disableAllLine();
        GameObject.Find("NumberLine").transform.GetChild(0).GetComponent<Image>().enabled = false;
        GameObject.Find("Canvas").transform.GetChild(0).gameObject.SetActive(true);
    }
    void enableFirstfrac()
    {
        GameObject.Find("NumberLine").transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
    }
    void enableHint()
    {
        GameObject.Find("NumberLine").transform.GetChild(0).GetChild(9).gameObject.SetActive(false);
        GameObject.FindObjectOfType<conversationManager>().DisableConversation();
        GameObject.Find("LO Panel").transform.GetChild(0).gameObject.SetActive(true);
        Invoke("lo_num", 4.2f);
        Invoke("lo_denum", 6.5f);
    }
    void lo_num()
    {
        GameObject.Find("LO Panel").transform.GetChild(0).GetChild(0).GetComponent<TEXDraw>().text = "\\frac{\\clr[4]3}{5}";
    }
    void lo_denum()
    {
        GameObject.Find("LO Panel").transform.GetChild(0).GetChild(0).GetComponent<TEXDraw>().text = "\\frac{3}{\\clr[4]5}";
    }
    void disableHint()
    {
        GameObject.Find("LO Panel").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("NumberLine").transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        GameObject.Find("NumberLine").transform.GetChild(0).GetChild(10).gameObject.SetActive(true);
        GameObject.Find("NumberLine").transform.GetChild(0).GetChild(11).gameObject.SetActive(true);
    }
   
    void enableOtherExample()
    {
        GameObject.Find("Canvas").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("NumberLine").transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        GameObject.Find("NumberLine").transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
    }
    void onScreenOneThird()
    {
        GameObject.Find("NumberLine").transform.GetChild(0).GetChild(2).GetComponent<TEXDraw>().text = "\\frac{1}{3} = ONE-THIRD";
    }
    void onScreenOneHighlight()
    {
        GameObject.Find("NumberLine").transform.GetChild(0).GetChild(2).GetComponent<TEXDraw>().text = "\\frac{1}{3}";
        GameObject.Find("NumberLine").transform.GetChild(0).GetChild(5).gameObject.SetActive(true);
    }
    void onScreenThirdHighlight()
    {
        GameObject.Find("NumberLine").transform.GetChild(0).GetChild(2).GetComponent<TEXDraw>().text = "\\frac{1}{3}";
        //GameObject.Find("NumberLine").transform.GetChild(0).GetChild(5).gameObject.SetActive(false);
        GameObject.Find("NumberLine").transform.GetChild(0).GetChild(6).gameObject.SetActive(true);
    }

    void singular()
    {
        GameObject.Find("NumberLine").transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
        GameObject.Find("NumberLine").transform.GetChild(0).GetChild(6).gameObject.SetActive(false);
        GameObject.Find("NumberLine").transform.GetChild(0).GetChild(5).gameObject.SetActive(false);
        GameObject.Find("NumberLine").transform.GetChild(0).GetChild(3).gameObject.SetActive(true);
        GameObject.Find("NumberLine").transform.GetChild(0).GetChild(7).gameObject.SetActive(true);
    }
    void plural()
    {
        GameObject.Find("NumberLine").transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
        GameObject.Find("NumberLine").transform.GetChild(0).GetChild(4).gameObject.SetActive(true);
        GameObject.Find("NumberLine").transform.GetChild(0).GetChild(8).gameObject.SetActive(true);
    }
    void enableFade()
    {
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
        Invoke("nextObjective", 3.0f);
    }
    void nextObjective()
    {
        Invoke("nextObjectiveVo", 3.0f);
        
       
        GameObject.FindObjectOfType<conversationManager>().DisableConversation();
        
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
        GameObject.FindObjectOfType<AplleObj7ROManager>().Initiliaze();
    }
    void enableROQuest2()
    {
        GameObject.Find("RO Panel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        GameObject.FindObjectOfType<AplleObj7ROManager>().EnableSubmitButtonRO2();
    }
    void enableROQuest3()
    {
        GameObject.Find("RO Panel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        GameObject.FindObjectOfType<AplleObj7ROManager>().EnableSubmitButtonRO3();
    }
    void enableROQuest4()
    {
        GameObject.Find("RO Panel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(3).gameObject.SetActive(true);
        GameObject.FindObjectOfType<AplleObj7ROManager>().EnableSubmitButtonRO4();
    }

}
