using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Obj2Manager : MonoBehaviour
{
    string jsonFileName = "Obj2FractionAsDivision.json";
    public Color highlightColor = Color.green;
    public GameObject[] a;
    public GameObject LoadingAudio, VisualQType_FDTemplate;

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
    void Initialised()
    {
        UtilityArtifacts.scene_to_load_after_canvas = "obj2";
        Invoke("audio_invoke", 2.0f);
        //this.GetComponent<Obj1AppleGenerator>().Initialize();
        GameObject.FindObjectOfType<QuestionManager>().ok_button_set();
        LoadingAudio = GameObject.Find("LoadAudio");
        VisualQType_FDTemplate = GameObject.Find("VisualQType_FDTemplate");
        VisualQType_FDTemplate.SetActive(false);
    }

    void HideLoadingAudio()
    {
        Debug.Log("HideLoadingAudio");
        LoadingAudio.SetActive(false);
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
            
            case "Obj2_Here_are_some":
                Debug.Log("Obj2_Here_are_some");
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Here are some apples");
                break;
            case "Obj2_lets_split":
                GameObject.FindObjectOfType<conversationManager>().EnableQuestion1("Can you split these apples equally among these three trays? \nDrag the apples to move them.");
                moveTrays();
                break;
            case "Obj2_As_you_can":
                GameObject.FindObjectOfType<conversationManager>().DisableQuestion1();
                GameObject.Find("Trays").transform.GetChild(0).gameObject.SetActive(false);
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("We successfully split 12 apples among 3 trays. \nAs you can see, all the trays have the same number of apples");
                //GameObject.FindObjectOfType<conversationManager>().DisableConversation();
                HighlightAllTrays();
                break;
            case "Obj2_Can_you":
                Debug.Log("Obj2_Can_you");
                GameObject.FindObjectOfType<conversationManager>().EnableQuestion1("Can you tell how many apples are there in each tray?");
                GameObject.FindObjectOfType<conversationManager>().DisableConversation();
                GameObject.Find("Trays").transform.GetChild(1).GetComponent<Animator>().enabled=true;
                GameObject.Find("Trays").transform.GetChild(2).GetComponent<Animator>().enabled = true;
                GameObject.Find("Trays").transform.GetChild(3).GetComponent<Animator>().enabled = true;
                Invoke("enableQuest2", 1.5f);
                
                break;

            case "Obj2_Yes_you":
                GameObject.FindObjectOfType<conversationManager>().DisableQuestion1();
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Yes, you are right, there are four apples in each tray");
                HighLightApples();
                Debug.Log("Obj2_Yes_you");
                break;
            case "Obj2_What_we":
                UnHighLightApples();
                Debug.Log("Obj2_What_we");
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("What we just did is, division");
                EnableExplaination();
                break;
            case "Obj2_We_divided":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("We divided 12 by 3 and we got 4 as the result");
                Debug.Log("Obj2_We_divided");
                break;
            case "Obj2_When_we_divide":
                DisableExplaination();
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("When we divide a number by another number, what we do is split a given amount equally into groups and find out what part of the amount is in each group");
                GameObject.Find("Trays").transform.GetChild(15).gameObject.SetActive(true);
                break;
            case "Obj2_what_we_do_is":
                Invoke("enableFade", 7.0f);
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("When we divide a number by another number, what we do is split a given amount equally into groups and find out what part of the amount is in each group");
                
                break;
            case "Obj2_Instead":
                Debug.Log("Obj2_Instead");
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Instead of 12 apples, let’s take only one apple now.");
                break;
            case "Obj2_And_if":
                Debug.Log("Obj2_Instead");
                GameObject.FindObjectOfType<conversationManager>().DisableConversation();
                GameObject.Find("Characters").transform.GetChild(0).gameObject.SetActive(true);
                break;
            case "Obj2_What_part":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("What part of the apple will each friend receive? ");
                break;
            case "Obj2_lets_divide":
                StartCoroutine(DevideSingleApple());
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Let’s divide the whole apple into four equal parts.");
                break;
            case "Obj2_When":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("When we split one whole apple into four equal parts, \nwe can say that each friend received one – fourth of the apple");
                break;
            case "Obj2_we_can_say":
                EnableText();
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Each friend received \\frac{1}{4} of the apple.");
                break;
            case "Obj2_Each_part":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("1 \\div 4 = \\frac{1}{4}");
                GameObject.Find("Trays").transform.GetChild(4).gameObject.SetActive(false);
                GameObject.Find("LastApple").transform.GetChild(0).gameObject.SetActive(true);
                GameObject.Find("LastApple").transform.GetChild(0).GetChild(3).gameObject.SetActive(true);
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Each part of the apple is a fraction as each part is part of a whole");
                break;
            case "Obj2_We_can_say_that_fractions":
                //DisableExplaination1();
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Fractions is a way of representing a part of an object that is split into equal parts.");
                GameObject.Find("LastApple").transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
                GameObject.Find("LastApple").transform.GetChild(0).GetChild(4).gameObject.SetActive(true); 
                Invoke("enableFade1", 10.0f);
                
                Invoke("DisableSingleApple", 11.5f);
                break;
            case "Obj2_lets_compare":
                Debug.Log("Obj2_we_devided");
                break;
            case "Obj2_we_initially":
                //compare1();
                GameObject.Find("Trays").transform.GetChild(8).gameObject.SetActive(true);
                break;
            case "Obj2_we_devided":
                GameObject.Find("Trays").transform.GetChild(8).gameObject.SetActive(false);
                EnableObj1();
                Debug.Log("Obj2_we_devided");
                break;
            case "Obj2_we_got":
                Debug.Log("Obj2_we_devided");
                break;
            case "Obj2_When_we_had":
                //compare2();
                GameObject.Find("Trays").transform.GetChild(9).gameObject.SetActive(true);
                Invoke("EnableObj2", 2.2f);
                break;
            case "Obj2_What_can":
                Debug.Log("Obj2_we_devided");
                //StartCoroutine("StartDialog");
                break;
            case "Obj2_Division_is":
                
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Division is splitting a given amount equally. Similarly, fractions is a way of representing an object that is split into equal parts");
                break;
            case "Obj2_Therefore":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Therefore, division and fraction is about finding the measure of objects that are equally divided");
                break;
            case "Obj2_There_is_one":
                enableDivision();
                EnableExplaination2();
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("There’s one more interesting similarity between fractions and division.");
                break;
            case "Obj2_Have_a_look":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Have a look at the division sign. There is a dot above the line and a dot below the line.");
                break;
            case "Obj2_If_we_replace":
                StartCoroutine(EnableExplaination3());
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("If we replace the dots by numbers, what we get is a fraction.");
                break;
            case "Obj2_RO_quest1":
                Debug.Log("Start RO 1");
                //GameObject.FindObjectOfType<conversationManager>().EnableConversation("Fractions and which operation are similar to each other?");
                break;
            case "Obj2_RO_quest2":
                EnableRoPanel1();
                //GameObject.FindObjectOfType<conversationManager>().EnableConversation("Fractions are similar to division because they are both about");
                break;
        }
    }
    void enableQuest2()
    {
        GameObject.FindObjectOfType<QuestionManager>().EnableForObj2Quest2();
    }
    void compare1()
    {
        GameObject.Find("Trays").transform.GetChild(8).gameObject.SetActive(true);
        GameObject.Find("Trays").transform.GetChild(5).gameObject.SetActive(false);
        Invoke("compare1_1", 3.0f);
    }
    void compare1_1()
    {
        GameObject.Find("Trays").transform.GetChild(8).gameObject.SetActive(false);
        EnableObj1();
        Invoke("compare1", 5.5f);
    }
    void compare2()
    {
        GameObject.Find("Trays").transform.GetChild(6).gameObject.SetActive(false);
        GameObject.Find("Trays").transform.GetChild(9).gameObject.SetActive(true);
        //Invoke("EnableObj2", 2.2f);

        
    }
    void enableDivision()
    {
        CancelInvoke();
        GameObject.Find("Trays").transform.GetChild(8).gameObject.SetActive(false);
        GameObject.Find("Trays").transform.GetChild(5).gameObject.SetActive(false);
        GameObject.Find("Trays").transform.GetChild(6).gameObject.SetActive(false);
        GameObject.Find("Trays").transform.GetChild(9).gameObject.SetActive(false);
    }

    void moveTrays()
    {
        GameObject.FindObjectOfType<conversationManager>().DisableConversation();
        GameObject.Find("Trays").transform.GetChild(0).GetComponent<Animator>().enabled = true;
        Invoke("EnableThreeTrays", 1.5f);
    }
    void EnableThreeTrays()
    {
      
        GameObject.Find("Trays").transform.GetChild(1).gameObject.SetActive(true) ;
        GameObject.Find("Trays").transform.GetChild(2).gameObject.SetActive(true);
        GameObject.Find("Trays").transform.GetChild(3).gameObject.SetActive(true);
        Invoke("startQuest", 2.0f);
        //StartCoroutine(AppleMove());
    }
    void startQuest()
    {
       
        GameObject.Find("GameManager").GetComponent<GameManager>().isObj1On = true;
        GameObject.FindObjectOfType<QuestionManager>().EnableForObj2Quest1();
    }
    void DisableThreeTrays()
    {
        GameObject.Find("Trays").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("Trays").transform.GetChild(1).gameObject.SetActive(false);
        GameObject.Find("Trays").transform.GetChild(2).gameObject.SetActive(false);
        GameObject.Find("Trays").transform.GetChild(3).gameObject.SetActive(false);
        GameObject.Find("GameManager").GetComponent<GameManager>().isObj1On = false;
        Invoke("EnableSingleApple",0.5f);
    }
    IEnumerator AppleMove()
    {
        //foreach(GameObject apple in GameObject.FindGameObjectsWithTag("Moving Apple"))
        foreach (GameObject apple in a)
        {
            yield return new WaitForSeconds(1.0f);
            apple.GetComponent<Animator>().enabled = true;

        }
        yield return new WaitForSeconds(8.0f);
        GameObject.FindObjectOfType<timeline_new>().load_next();
    }
    
    void HighlightAllTrays()
    {
        GameObject.Find("Tray1").GetComponent<Image>().color = Color.cyan;
        GameObject.Find("Tray2").GetComponent<Image>().color = Color.cyan;
        GameObject.Find("Tray3").GetComponent<Image>().color = Color.cyan;
        Invoke("UnHighlightAllTrays", 5.0f);
    }
    void UnHighlightAllTrays()
    {
        GameObject.Find("Tray1").GetComponent<Image>().color = Color.white;
        GameObject.Find("Tray2").GetComponent<Image>().color = Color.white;
        GameObject.Find("Tray3").GetComponent<Image>().color = Color.white;
    }
    void HighLightApples()
    {
        //GameObject.Find("Tray1Apple").transform.GetChild(0).GetComponent<Image>().color = Color.yellow;
        //GameObject.Find("Tray1Apple").transform.GetChild(1).GetComponent<Image>().color = Color.yellow;
        //GameObject.Find("Tray1Apple").transform.GetChild(2).GetComponent<Image>().color = Color.yellow;
        //GameObject.Find("Tray1Apple").transform.GetChild(3).GetComponent<Image>().color = Color.yellow;
    foreach(GameObject g in GameObject.FindGameObjectsWithTag("Red"))
        {
            g.GetComponent<Image>().color = Color.yellow;
        }
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Green"))
        {
            g.GetComponent<Image>().color = Color.yellow;
        }
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Yellow"))
        {
            g.GetComponent<Image>().color = Color.yellow;
        }

    }
    void UnHighLightApples()
    {
        //GameObject.Find("Tray1Apple").transform.GetChild(0).GetComponent<Image>().color = Color.white;
        //GameObject.Find("Tray1Apple").transform.GetChild(1).GetComponent<Image>().color = Color.white;
        //GameObject.Find("Tray1Apple").transform.GetChild(2).GetComponent<Image>().color = Color.white;
        //GameObject.Find("Tray1Apple").transform.GetChild(3).GetComponent<Image>().color = Color.white;
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Red"))
        {
            g.GetComponent<Image>().color = Color.white;
        }
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Green"))
        {
            g.GetComponent<Image>().color = Color.white;
        }
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Yellow"))
        {
            g.GetComponent<Image>().color = Color.white;
        }
    }

    void EnableExplaination()
    {
        //GameObject.Find("RO Panel").transform.GetChild(0).gameObject.SetActive(true);
        //GameObject.Find("RO Panel").transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        GameObject.Find("Trays").transform.GetChild(7).gameObject.SetActive(true);

    }
    void DisableExplaination()
    {
        //GameObject.Find("RO Panel").transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
        //GameObject.Find("RO Panel").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("Trays").transform.GetChild(7).gameObject.SetActive(false);
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
        GameObject.Find("Trays").transform.GetChild(15).gameObject.SetActive(false);
        DisableThreeTrays();
        EnableSingleApple();
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadeout");
    }
    void nextObjectiveVo()
    {
        //GameObject.Find("Fade").GetComponent<Animator>().enabled = false;
        GameObject.FindObjectOfType<timeline_new>().load_next();
        
    }
    void EnableSingleApple()
    {
        GameObject.Find("Trays").transform.GetChild(4).gameObject.SetActive(true);
    }
    void DisableSingleApple()
    {
        GameObject.Find("Trays").transform.GetChild(4).gameObject.SetActive(false);
        GameObject.Find("LastApple").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("Characters").gameObject.SetActive(false);
    }
    IEnumerator DevideSingleApple()
    {
        GameObject.Find("Trays").transform.GetChild(4).GetChild(0).GetComponent<Animator>().enabled=true;
        yield return new WaitForSeconds(2.0f);
        GameObject.Find("Trays").transform.GetChild(4).GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        GameObject.Find("Trays").transform.GetChild(4).GetChild(2).gameObject.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        GameObject.Find("Trays").transform.GetChild(4).GetChild(3).gameObject.SetActive(true);
        //yield return new WaitForSeconds(1.0f);
        //GameObject.Find("Trays").transform.GetChild(4).GetChild(0).gameObject.SetActive(false);
        //GameObject.Find("Trays").transform.GetChild(4).GetChild(4).gameObject.SetActive(true);

    }
    void EnableText()
    {
        GameObject.Find("Trays").transform.GetChild(4).GetChild(4).gameObject.SetActive(true);
    }
    void EnableExplaination1()
    {
        GameObject.Find("RO Panel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(3).gameObject.SetActive(true);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(4).gameObject.SetActive(true);

    }
    void DisableExplaination1()
    {
        GameObject.Find("RO Panel").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(4).gameObject.SetActive(false);

    }

    void enableFade1()
    {
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
        Invoke("nextObjective1", 3.0f);
    }
    void nextObjective1()
    {
        Invoke("nextObjectiveVo1", 3.0f);
        GameObject.FindObjectOfType<conversationManager>().DisableConversation();
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadeout");

    }
    void nextObjectiveVo1()
    {
        //GameObject.Find("Fade").GetComponent<Animator>().enabled = false;
        GameObject.FindObjectOfType<timeline_new>().load_next();
        
    }
    void EnableObj1()
    {
        GameObject.Find("Trays").transform.GetChild(5).gameObject.SetActive(true);
    }
    void EnableObj2()
    {
        GameObject.Find("Trays").transform.GetChild(9).gameObject.SetActive(false);
        GameObject.Find("Trays").transform.GetChild(6).gameObject.SetActive(true);
        //Invoke("compare2", 3.2f);
    }

    IEnumerator StartDialog()
    {
        yield return new WaitForSeconds(5.0f);
        GameObject.FindObjectOfType<conversationManager>().EnableConversation("Division is splitting a given amount equally. Similarly, fractions is a way of representing an object that is split into equal parts");
        yield return new WaitForSeconds(8.0f);
        GameObject.FindObjectOfType<conversationManager>().EnableConversation("Therefore, division and fraction is about finding the measure of objects that are equally divided");
        yield return new WaitForSeconds(8.0f);
        GameObject.FindObjectOfType<conversationManager>().DisableConversation();
        GameObject.FindObjectOfType<timeline_new>().load_next();
    }
    void EnableExplaination2()
    {
        for(int i =0;i<10;i++)
        {
            GameObject.Find("Trays").transform.GetChild(i).gameObject.SetActive(false);
        }
        GameObject.Find("Trays").transform.GetChild(10).gameObject.SetActive(true);


    }
    IEnumerator EnableExplaination3()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject.Find("Trays").transform.GetChild(i).gameObject.SetActive(false);
        }
        GameObject.Find("Trays").transform.GetChild(10).GetComponent<TEXDraw>().text = "\\div";
        GameObject.Find("Trays").transform.GetChild(10).gameObject.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        GameObject.Find("Trays").transform.GetChild(10).GetComponent<TEXDraw>().text = "\\frac{\\bg[magenta].}{\\bg[magenta].}}";
        yield return new WaitForSeconds(2.0f);
        GameObject.Find("Trays").transform.GetChild(10).GetComponent<TEXDraw>().text = "\\frac{3}{.}";
        yield return new WaitForSeconds(2.0f);
        GameObject.Find("Trays").transform.GetChild(10).GetComponent<TEXDraw>().text = "\\frac{3}{12}";
        Invoke("enableFade1", 5.0f);
        //Invoke("DisableExplaination3", 15.0f);
        
        Invoke("EnableRoPanel", 8.0f);
    }
    void DisableExplaination3()
    {
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(4).gameObject.SetActive(false);
        
        EnableRoPanel();
    }

    void EnableRoPanel()
    {
        GameObject.Find("RO Panel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        GameObject.FindObjectOfType<AplleObj2ROManager>().Initiliaze();
    }
    void DisableRoPanel()
    {
        GameObject.Find("RO Panel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
    }
    void EnableRoPanel1()
    {
        DisableRoPanel();
        GameObject.Find("RO Panel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        GameObject.FindObjectOfType<AplleObj2ROManager>().EnableSubmitButtonRO2();
    }
    void DisableRoPanel1()
    {
        GameObject.Find("RO Panel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
    }
}
