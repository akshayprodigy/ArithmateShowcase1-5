using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Obj1Manager : MonoBehaviour
{
    string jsonFileName = "obj1WholeAppleShorting_json.json";
    public Color highlightColor = Color.green;
    public GameObject LoadingAudio, QType_Template;

    private void OnEnable()
    {
        timeline_new.OnEventCalled += EventToHandle;
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
            hintEnable();
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
        Invoke("audio_invoke", 2.0f);
        //this.GetComponent<Obj1AppleGenerator>().Initialize();
        AppleManager.total();
        LoadingAudio = GameObject.Find("LoadAudio");
        QType_Template = GameObject.Find("QType_Template");
        QType_Template.SetActive(false);
    }
    void audio_invoke()
    {
        Debug.Log(EVENT.SetUpTimeLine);
        EventManager.setNameOfJasonFile(jsonFileName);
        EventManager.Broadcast(EVENT.SetUpTimeLine);
    }

    void HideLoadingAudio()
    {
        QType_Template.SetActive(false);
        LoadingAudio.SetActive(false);
    }

    void EventToHandle(string EventName)
    {
        HideLoadingAudio();
        switch (EventName)
        {
            case "Obj1_GoodJob":
                GameObject.Find("Canvas").transform.GetChild(3).gameObject.SetActive(true);
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Good Job.\nI can see that you have collected quite a lot of apples. I am sure that all of them are not whole.");
                break;
            case "Obj1_You_Now":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("You now need to separate these apples.");
                GameObject.Find("Canvas").transform.GetChild(1).gameObject.SetActive(true);
                //Invoke("HighLightWholeTrey", 6.0f);
                break;
            case "Obj1_And_The":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("The one's that are whole have to be placed in one tray and the one’s that are not whole have to be placed in the other tray.");
                HighLightWholeTrey();
                Invoke("HighLightFracTrey", 3.5f);
                break;
            case "Obj1_So_Go":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("So go ahead and separate these apples.");
                Invoke("StartGame", 4.5f);
                break;
            case "Obj1_Lets_say":
                GameObject.Find("Treys").transform.GetChild(0).gameObject.SetActive(false);
                GameObject.Find("Treys").transform.GetChild(1).gameObject.SetActive(false);
                GameObject.Find("Treys").transform.GetChild(3).gameObject.SetActive(true);
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Let's say we have 10 apples that are whole and 10 that are not whole.");
                break;
            case "Obj1_Both_of":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Both these types of apples cannot be equal in size. \nSo, let's separate them and count them.");
                break;
               
            case "Obj1_We_Count":
                GameObject.Find("Treys").transform.GetChild(3).gameObject.SetActive(false);
                GameObject.Find("SecondTrey").transform.GetChild(0).gameObject.SetActive(true);
                GameObject.Find("SecondTrey").transform.GetChild(1).gameObject.SetActive(true);
                
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("We count the whole apples by using whole numbers.}");
                Invoke("HighLightWholeApple", 1.0f);
                break;
            case "Obj1_But_How":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("But how do we count the one’s that are not whole?");
                Invoke("HighLightFracApple", 0.5f);
                break;
            case "Obj1_We_use":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("We use a type of numbers called Fractions to count objects which are not whole.");
                GameObject.Find("SecondTrey").transform.GetChild(0).gameObject.SetActive(false);
                GameObject.Find("SecondTrey").transform.GetChild(1).gameObject.SetActive(false);
                GameObject.Find("SecondTrey").transform.GetChild(2).gameObject.SetActive(true);
                break;
            case "Obj1_To_count":
              
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Therefore, to count the apples that are not whole, we can use fractions.");
                break;
            case "Obj1_We_can_say":
                GameObject.Find("SecondFrac").transform.GetChild(13).gameObject.SetActive(true);
                GameObject.Find("SecondFrac").transform.GetChild(0).GetComponent<Text>().text= "Part of a whole";
                //Invoke("HighLightFracApple", 1.0f);
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Fractions are used to represent any object that are less than a whole or part of a whole.}");
                Invoke("enableFade",10.0f);
                break;
            case "Now_that_you":
                
                //GameObject.FindObjectOfType<conversationManager>().EnableROQuestion("Now that you know how do we count objects that are whole and objects that are not whole, can you identify which of the statements are true");
                
               
                break;
            case "Obj1_Do_you_know":
                DisableRoPanel();
                EnableFractions();
                EnableLine();
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Do you know what does a fraction look like?");
                break;
            case "Obj1_A_fraction":
                
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Fractions are 2 numbers separated by a line, written one below the other.");
                //GameObject.Find("Mix").transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Whole numbers";
                //GameObject.Find("Mix").transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "Part of a whole";
                break;
           
            case "Obj1_The_line_that":
                GameObject.Find("RO Panel").transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<Outline>().enabled = true;
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("The line that separates the two numbers is called the \\bg[magenta]Vinculum}");
                Invoke("enableFade1", 4.0f);
                break;
            case "Obj1_Now_that_you":
                
                break;
            case "Obj1_What_is_the":
                EnableRoPanel2();
                GameObject.FindObjectOfType<AplleObj1ROManager>().EnableSubmitButtonRO3();
                GameObject.FindObjectOfType<conversationManager>().EnableROQuestion("What is the line separating the numbers in a fraction called?");
                break;
        }

    }
    void StartGame()
    {
        GameObject.FindObjectOfType<conversationManager>().DisableConversation();
        GameObject.FindObjectOfType<GameManager>().isObj1On = true;
        GameObject.Find("AppleSlot").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("Basket").SetActive(false);
        //GameObject.Find("Basket").transform.GetChild(0).gameObject.SetActive(true);
        this.GetComponent<Obj1AppleGenerator>().Initialize();

    }
    void EnableRoPanel()
    {
        GameObject.Find("RO Panel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
    }
    void DisableRoPanel()
    {
        GameObject.Find("RO Panel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
    }
    void EnableRoPanel1()
    {
        GameObject.Find("RO Panel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(0).gameObject.SetActive(false);

        GameObject.Find("ROType2ValueSet").transform.GetChild(0).GetChild(3).GetComponent<TEXDraw>().text="1";
        GameObject.Find("ROType2ValueSet").transform.GetChild(2).GetChild(3).GetComponent<TEXDraw>().text = "1-2";
        GameObject.Find("ROType2ValueSet").transform.GetChild(4).GetChild(3).GetComponent<TEXDraw>().text = "\\frac{1}{2}";

    }
    void EnableRoPanel2()
    {
        

        GameObject.Find("ROType2ValueSet").transform.GetChild(0).GetChild(3).GetComponent<TEXDraw>().text = "Line";
        GameObject.Find("ROType2ValueSet").transform.GetChild(2).GetChild(3).GetComponent<TEXDraw>().text = "Seperator";
        GameObject.Find("ROType2ValueSet").transform.GetChild(4).GetChild(3).GetComponent<TEXDraw>().text = "Vinculum";
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(1).GetChild(3).GetChild(0).gameObject.SetActive(true);
    }
    void EnableFractions()
    {
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
    }
    void EnableLine()
    {
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(3).gameObject.SetActive(true);
    }
    void DisableFrac()
    {
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
    }
    void HighLightWholeTrey()
    {
        GameObject.Find("Full").GetComponent<Image>().color = Color.yellow;
        Invoke("UnHighLightWholeTrey", 3.5f);
    }
    void UnHighLightWholeTrey()
    {
        GameObject.Find("Full").GetComponent<Image>().color = Color.white;
        
    }
    void UnHighLightSecondWholeTrey()
    {
        GameObject.Find("SecondFull").GetComponent<Image>().color = Color.white;

    }
    void HighLightFracTrey()
    {
        GameObject.Find("Frac").GetComponent<Image>().color = Color.yellow;
        Invoke("UnHighLightFracTrey", 5.0f);
    }
    void UnHighLightFracTrey()
    {
        GameObject.Find("Frac").GetComponent<Image>().color = Color.white;

    }
    void UnHighLightSecondFracTrey()
    {
        GameObject.Find("SecondFrac").GetComponent<Image>().color = Color.white;

    }

    void HighLightWholeApple()
    {
        

        GameObject.Find("SecondFull").GetComponent<Image>().color = Color.green;
        GameObject.Find("SecondFull").transform.GetChild(13).gameObject.SetActive(true);
        Invoke("UnHighLightSecondWholeTrey", 5.0f);
    }
    void UnHighLightWholeApple()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("FullApple"))
            g.GetComponent<Outline>().enabled = false;

    }
   
    void HighLightFracApple()
    {
        Debug.Log("HighLightFrac");
        
        GameObject.Find("SecondFrac").GetComponent<Image>().color = Color.cyan;
        GameObject.Find("SecondFrac").transform.GetChild(13).gameObject.SetActive(true);
        Invoke("UnHighLightSecondFracTrey", 9.0f);
    }
    void UnHighLightFracApple()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("FracApple"))
            g.GetComponent<Outline>().enabled = false;

    }
    void enableFade()
    {
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
        Invoke("nextObjective", 3.0f);
    }
    void nextObjective()
    {
        GameObject.FindObjectOfType<conversationManager>().DisableConversation();
        EnableRoPanel();
        GameObject.FindObjectOfType<conversationManager>().EnableROQuestion("Now that you know how do we count objects that are whole and objects that are not whole, can you identify the statement that are true");
        GameObject.FindObjectOfType<AplleObj1ROManager>().Initiliaze();
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadeout");
        Invoke("nextObjectiveVo", 3.0f);

    }
    void nextObjectiveVo()
    {
       
        GameObject.FindObjectOfType<timeline_new>().load_next();
       
    }

    public void hintEnable()
    {
        Debug.Log("Enable Hint");
        GameObject.FindObjectOfType<conversationManager>().EnableHint("Good Job");
        Invoke("hintDisable", 4.0f);
    }
    public void hintDisable()
    {
        GameObject.FindObjectOfType<conversationManager>().DisableHint();
        GameObject.FindObjectOfType<timeline_new>().load_next();
    }

    void enableFade1()
    {
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
        Invoke("nextObjective1", 3.0f);
    }
    void nextObjective1()
    {

        DisableFrac();
        GameObject.FindObjectOfType<conversationManager>().DisableConversation();
        EnableRoPanel1();
        GameObject.FindObjectOfType<AplleObj1ROManager>().EnableSubmitButtonRO2();
        GameObject.FindObjectOfType<conversationManager>().EnableROQuestion("Now that you know what a fraction looks like, can you tell which of the following is a fraction?");

        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadeout");

        Invoke("nextObjectiveVo1", 3.0f);

    }
    void nextObjectiveVo1()
    {

        GameObject.FindObjectOfType<timeline_new>().load_next();

    }
}
