using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Obj14_Manager : MonoBehaviour
{
    string jsonFileName = "Obj14_Mixed_ImproperFraction.json";
    GameObject Dialouge_text, Dialouge_panel;
    Button dialougue_ok_button;

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

    void Initialised()
    {
        InitializeDialoguePanl();

        Invoke("audio_invoke", 2.0f);

    }

    void InitializeDialoguePanl()
    {
        Dialouge_panel = GameObject.Find("Canvas").transform.GetChild(0).GetChild(5).GetChild(0).gameObject;
        Dialouge_text = Dialouge_panel.transform.GetChild(1).gameObject;
        dialougue_ok_button = Dialouge_panel.transform.GetChild(4).gameObject.GetComponent<Button>();

        dialougue_ok_button.onClick.RemoveAllListeners();
        dialougue_ok_button.onClick.AddListener(Dialougue_ok_functionality);

    }
    void audio_invoke()
    {
        Debug.Log(EVENT.SetUpTimeLine);
        EventManager.setNameOfJasonFile(jsonFileName);
        EventManager.Broadcast(EVENT.SetUpTimeLine);
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
    }

    void EventToHandle(string EventName)
    {
        switch (EventName)
        {
            case "Obj14_MixedFrac_great":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Mixed fractions are great for everyday use as they are easy to understand.");
                GameObject.Find("Canvas/Main_Panel/MixedImproperFraction_Panel/Head_Text").SetActive(true);
                break;
            case "Obj14_is_easier":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("2 " +" \\frac{1}{2}"+ " loaves of bread is easier to understand than " +"\\frac{5}{2}" +" loaves of bread. ");
                GameObject.Find("Canvas/Main_Panel/MixedImproperFraction_Panel/BreadLoafs_Container").SetActive(true); 
                GameObject.Find("Canvas/Main_Panel/MixedImproperFraction_Panel/TEXDraw_Frac").SetActive(true); 
                break;
            case "Obj14_is_easier_because":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("It is easier to understand because a mixed fraction clearly denotes the number of whole objects and a part of the same object. ");
                break;
            case "Obj14_Whole_Object":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("It is easier to understand because a mixed fraction clearly denotes the number of whole objects and a part of the same object. ");
                GameObject.Find("Canvas/Main_Panel/MixedImproperFraction_Panel/Highlights/Highlight_WholeBread").SetActive(true);
                GameObject.Find("Canvas/Main_Panel/MixedImproperFraction_Panel/Highlights/Highlight_FracWholeBread").SetActive(true);
                break;
            case "Obj14_part_of_the_same_Object":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("It is easier to understand because a mixed fraction clearly denotes the number of whole objects and a part of the same object. ");
                GameObject.Find("Canvas/Main_Panel/MixedImproperFraction_Panel/Highlights/Highlight_HalfBread").SetActive(true);
                GameObject.Find("Canvas/Main_Panel/MixedImproperFraction_Panel/Highlights/Highlight_FracHalfBread").SetActive(true);
                break;
            case "Obj14_MixedFrac_has_two_parts":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("As you know a mixed fraction has two parts to it - the whole number and the fraction which makes it slightly complex to do calculations. This is where improper fraction are very helpful.");
                break;
            case "Obj14_whole_number":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("As you know a mixed fraction has two parts to it - the whole number and the fraction which makes it slightly complex to do calculations. This is where improper fraction are very helpful.");
                GameObject.Find("Canvas/Main_Panel/MixedImproperFraction_Panel/Highlights/Highlight_LabelWholeBread").SetActive(true);
                break;
            case "Obj14_fraction_number":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("As you know a mixed fraction has two parts to it - the whole number and the fraction which makes it slightly complex to do calculations. This is where improper fraction are very helpful.");
                GameObject.Find("Canvas/Main_Panel/MixedImproperFraction_Panel/Highlights/Highlight_LabelHalfBread").SetActive(true);
                break;
            case "Obj14_this_is_true":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("You will realize this is true when you try to add, subtract, multiply or divide mixed fractions.");
                break;
            case "Obj14_convert_improper_proper":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("We convert Mixed fractions into Improper Fractions for performing operations involving fractions. ");
                break;
            case "Obj14_Let_us_learn":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Let us learn how we can convert a Mixed fraction or a mixed number into an Improper Fraction.");
                //play Arrow Animation
                GameObject.Find("Canvas/Main_Panel/MixedImproperFraction_Panel/ArrwPnl_Animation").SetActive(true);
                GameObject.Find("Canvas/Main_Panel/MixedImproperFraction_Panel/ArrwPnl_Animation").GetComponent<Animator>().SetBool("Play", true);
                break;
            case "Obj14_we_know_that":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("We know that a mixed fraction is made of a whole number and a proper fraction. ");
                break;
            case "Obj14_while_converting":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("While converting the mixed fraction to an improper fraction, we will change the whole number into a fraction and combine it with the proper fraction.");
                break;
            case "Obj14_Convert_the_mixedFrac":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Let’s convert the Mixed Fraction 3" +"\\frac{2}{5}" +" into an Improper Fraction.");
                //call RO1
                Invoke("enableFade", 2.5f);
                break;
            case "Obj14_RO_Identify_which_following":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Identify which of the following correctly represents 3" + "\\frac{2}{5}");
                break;


        }

    }
    //void StartGame()
    //{
    //    GameObject.FindObjectOfType<conversationManager>().DisableConversation();
    //    GameObject.FindObjectOfType<GameManager>().isObj1On = true;
    //    GameObject.Find("AppleSlot").transform.GetChild(0).gameObject.SetActive(true);
    //    this.GetComponent<Obj1AppleGenerator>().Initialize();

    //}
    
    //void EnableRoPanel1()
    //{
    //    GameObject.Find("RO Panel").transform.GetChild(0).gameObject.SetActive(true);
    //    GameObject.Find("RO Panel").transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
    //    GameObject.Find("RO Panel").transform.GetChild(0).GetChild(0).gameObject.SetActive(false);

    //    GameObject.Find("ROType2ValueSet").transform.GetChild(0).GetChild(3).GetComponent<TEXDraw>().text = "1";
    //    GameObject.Find("ROType2ValueSet").transform.GetChild(2).GetChild(3).GetComponent<TEXDraw>().text = "1-2";
    //    GameObject.Find("ROType2ValueSet").transform.GetChild(4).GetChild(3).GetComponent<TEXDraw>().text = "\\frac{1}{2}";

    //}
    //void EnableRoPanel2()
    //{


    //    GameObject.Find("ROType2ValueSet").transform.GetChild(0).GetChild(3).GetComponent<TEXDraw>().text = "Line";
    //    GameObject.Find("ROType2ValueSet").transform.GetChild(2).GetChild(3).GetComponent<TEXDraw>().text = "Seperator";
    //    GameObject.Find("ROType2ValueSet").transform.GetChild(4).GetChild(3).GetComponent<TEXDraw>().text = "Vinculum";

    //}
    //void EnableFractions()
    //{
    //    GameObject.Find("RO Panel").transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
    //}
    //void EnableLine()
    //{
    //    GameObject.Find("RO Panel").transform.GetChild(0).GetChild(3).gameObject.SetActive(true);
    //}
    //void DisableFrac()
    //{
    //    GameObject.Find("RO Panel").transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
    //    GameObject.Find("RO Panel").transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
    //}
    //void HighLightWholeTrey()
    //{
    //    GameObject.Find("Full").GetComponent<Image>().color = Color.yellow;
    //    Invoke("UnHighLightWholeTrey", 5.0f);
    //}
    //void UnHighLightWholeTrey()
    //{
    //    GameObject.Find("Full").GetComponent<Image>().color = Color.white;

    //}


    void enableFade()
    {
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
        Invoke("nextObjective", 3.0f);
    }
    void nextObjective()
    {
        GameObject.FindObjectOfType<conversationManager>().DisableConversation();
        EnableRoPanel();
        //GameObject.FindObjectOfType<conversationManager>().EnableROQuestion("Now that you know how do we count objects that are whole and objects that are not whole, can you identify the statement that are true");
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadeout");
        Invoke("nextObjectiveVo", 3.0f);

    }
    void nextObjectiveVo()
    {

        GameObject.FindObjectOfType<timeline_new>().load_next();

    }

    void EnableRoPanel()
    {
        GameObject.Find("RO Panel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        GameObject.FindObjectOfType<Obj14_ROManager>().Initiliaze();

    }
    void DisableRoPanel()
    {
        GameObject.Find("RO Panel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
    }

    public void hintEnable()
    {
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("Good Job");
        Invoke("hintDisable", 4.0f);
    }
    public void hintDisable()
    {
        GameObject.FindObjectOfType<conversationManager>().DisableDialouge();
        GameObject.FindObjectOfType<timeline_new>().load_next();
    }
}

