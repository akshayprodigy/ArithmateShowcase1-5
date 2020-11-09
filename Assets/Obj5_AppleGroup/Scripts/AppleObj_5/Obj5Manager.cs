using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class Obj5Manager : MonoBehaviour
{
    string jsonFileName = "Obj5GroupOfAppes_json.json";
    public string ans;
    public static int correctDraggedApples;
    public GameObject LoadingAudio, VisualQTYpe_GroupObjects;
    Obj5AppleGenerator appleGenerator;
    public Image apple1, apple2, apple3, apple4, apple5, apple6, apple7, apple8;
    public GameObject TrayToReset, InitiatedApplel;


    private void OnEnable()
    {
        timeline_new.OnEventCalled += EventToHandle;
    }

    private void OnDisable()
    {
        timeline_new.OnEventCalled -= EventToHandle;
    }

    private void Awake()
    {
        Initialised();

    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
            nextObjectiveVo1();
    }
    void Start()
    {
        GameObject.FindObjectOfType<conversationManager>().DisableDialouge();

    }
    
    void Initialised()
    {
        LoadingAudio = GameObject.Find("LoadAudio");
        if (UtilityArtifacts.backTraversal)
        {
            Text textLoadingText = LoadingAudio.transform.GetChild(2).GetComponent<Text>();
            //textLoadingText.text = "Let us understand this better";
        }
        Invoke("audio_invoke", 2.0f);
        InitiatedApplel = GameObject.Find("Full1");
        VisualQTYpe_GroupObjects = GameObject.Find("VisualQTYpe_GroupObjects");
        VisualQTYpe_GroupObjects.SetActive(false);
        //this.GetComponent<Obj5AppleGenerator>().Initialize();
        GameObject.FindObjectOfType<QuestionManager>().GetOkButton();

    }

    void reset_option()
    {
        foreach (GameObject b in GameObject.FindGameObjectsWithTag("Numbers"))
        {
            b.transform.GetChild(2).gameObject.SetActive(false);
        }
        ans = "";
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

            case "Obj5_Start1":
                GameObject.FindObjectOfType<conversationManager>().DisableConversation();
                break;
            case "Obj5_Start2":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("As you can see, there are red apples, green apples and yellow apples in the case");
                break;
            case "Obj5_Que_1":
                GameObject.FindObjectOfType<conversationManager>().EnableQuestion1("Can you count and enter the number of red apples in the case?");
                GameObject.FindObjectOfType<conversationManager>().DisableConversation();
                Invoke("StartQuestion1", 3.5f);
                break;
            case "Obj5_Que_2":
                GameObject.FindObjectOfType<conversationManager>().EnableQuestion1("Can you now count and enter the total number of apples in the case? ");
                GameObject.FindObjectOfType<conversationManager>().DisableConversation();
                Invoke("StartQuestion2", 3.5f);
                break; 
            case "Obj5_LO_1":
                GameObject.FindObjectOfType<conversationManager>().DisableQuestion1();
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("What you just did, is represent a part of a group in terms of fractions");
                GameObject.Find("Question_1_Text").transform.GetChild(0).gameObject.SetActive(true);
                break;
            case "Obj5_LO_2":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Representing objects in a group as fractions is same as representing a part of an object divided into many parts. ");
                ShowFractions();
                GameObject.Find("Question_1_Text").transform.GetChild(0).gameObject.SetActive(false);
                GameObject.Find("Question_2_Text").transform.GetChild(0).gameObject.SetActive(false);
                break;
            case "Obj5_LO_1_representation_1":
                //GameObject.FindObjectOfType<conversationManager>().EnableConversation("To represent an object or objects in a group using fractions, show the number of objects considered from the group");
                GameObject.FindObjectOfType<conversationManager>().DisableConversation();
                HideFractions();
                GameObject.Find("Highlights").SetActive(false);
                Show_LO1_Representation_1();
                break;
            case "Obj5_LO_1_representation_2":
                //GameObject.FindObjectOfType<conversationManager>().EnableConversation("over the total number of objects in the group.");
                GameObject.FindObjectOfType<conversationManager>().DisableConversation();
                //show animation
                GameObject.Find("Canvas (1)/LO1_Representation_Pnl (1)").transform.GetChild(0).GetComponent<Animator>().SetBool("Play", true);
                GameObject.Find("Canvas (1)/LO1_Representation_Pnl (1)").transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
                GameObject.Find("Canvas (1)").transform.GetChild(0).GetChild(4).GetChild(1).GetChild(1).gameObject.SetActive(true);

                break;
            case "Obj5_RO_1":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Now that you know how to represent a certain part of the group. Can you tell,");
                Hide_LO1_Representation_1();
                Invoke("enableFade1", 5.0f);
                break;
            case "Obj5_Pass_6_Apples":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("From the apples that you have collected, can you pass 6 apples, two of each colour. ");
                break;
            case "Obj5_Use_The_Case":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("You can use this case to give me the apples ");
                //Invoke("ActiveQuestion2Setup", 6.5f);
                ActiveQuestion2Setup();
                break;
            case "Obj5_DraGdrop_2_apples":
                GameObject.FindObjectOfType<conversationManager>().EnableQuestion1("Drag and drop 2 apples of the same colour in each of the cases.");
                GameObject.FindObjectOfType<conversationManager>().DisableConversation();
                GameObject.Find("GameManager").GetComponent<GameManager>().isObj5_AppleGroup = true;
                GameObject.FindObjectOfType<Obj5_RO_Manager>().EnableSubmitButton_DropApple();
                break;
            case "Obj5_case_Red_apples":
                GameObject.FindObjectOfType<conversationManager>().EnableQuestion1(" Can you enter how many cases have red apples ? ");
                GameObject.FindObjectOfType<conversationManager>().DisableConversation();
                GameObject.Find("GameManager").GetComponent<GameManager>().isObj5_AppleGroup = false;

                //Invoke("LO2_Quest2", 3.5f);
                LO2_Quest2();
                break;
            case "Obj5_case_totalNo_apples":
                GameObject.FindObjectOfType<conversationManager>().EnableQuestion1("Can you count and enter the total number of cases we have ? ");
                GameObject.FindObjectOfType<conversationManager>().DisableConversation();
                //Invoke("LO2_Quest3", 3.5f);
                LO2_Quest3();
                break;
            case "Obj5_LO2_As_you_can_see":
                GameObject.FindObjectOfType<conversationManager>().DisableQuestion1();
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("As you can see, there are three groups of apples, one group of green apples, one  group of yellow apples and one group of red apples.");
                GameObject.FindObjectOfType<QuestionManager>().DisableSubmitButton();
                GameObject.FindObjectOfType<QuestionManager>().DisableNumbers_InputField();
                break;
            case "Obj5_LO2_what_Just_you_did":
                GameObject.FindObjectOfType<conversationManager>().DisableQuestion1();
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("What you just did, is you represented the group which has red apples from the given set of groups of apples.");
                GameObject.Find("Canvas (1)/LO2_Representation_Pnl").transform.GetChild(0).gameObject.SetActive(true);
                break;
            case "Obj5_RO2_can_you_tell_2_3":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Now that you know how to represent a group within a set of groups , Can you tell ");
                GameObject.Find("Canvas (1)/LO2_Representation_Pnl").transform.GetChild(0).gameObject.SetActive(false);
                Invoke("enableFade2", 5.0f);
                break;
            case "Obj5_RO2_fraction_2_3":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Which of the following options do you think represents the fraction 2/3?");
                break;
            case "Obj5_LO1_Fact":
                GameObject.Find("Summerise_Pnl").transform.GetChild(0).gameObject.SetActive(true);
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("To summarize, we can say that as we use fractions to represent");
                break;
            case "Obj5_LO1_Fact_A_part_whole":
                GameObject.Find("Summerise_Pnl").transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("a part of a whole,");
                break;
            case "Obj5_LO1_Fact_A_part_group":
                GameObject.Find("Summerise_Pnl").transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
                GameObject.FindObjectOfType<conversationManager>().EnableConversation(" a part of a group of objects");
                break;
            case "Obj5_LO1_Fact_A_part_group_among":
                GameObject.Find("Summerise_Pnl").transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
                GameObject.FindObjectOfType<conversationManager>().EnableConversation(" a group or groups among other group of objects");
                Invoke("enableFade3", 7.5f);
                break;
            case "Obj5_RO1_Fact":
                break;
            case "Obj5_RO1_Fact_fraction_of_shapes":
                EnableRoPanel3();
                break;
            case "Obj5_groups_have_red_apples":
                GameObject.FindObjectOfType<conversationManager>().DisableConversation();
                GameObject.FindObjectOfType<conversationManager>().DisableQuestion1();
                EnableRoPanel4();
                break;
        }
    }

    void enableFade1()
    {
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
        Invoke("nextObjective1", 3.0f);
        


    }
    void nextObjective1()
    {
        AppleManager.CollectedFullRedApple = 2;
        AppleManager.CollectedFullGreenApple = 2;
        AppleManager.CollectedFullYellowApple = 2;
        EnableRoPanel();
        //nextObjectiveVo2();
        Invoke("nextObjectiveVo1", 3.0f);
        GameObject.FindObjectOfType<conversationManager>().EnableConversation("");
        GameObject.FindObjectOfType<conversationManager>().DisableConversation();
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadeout");
        //GameObject.Find("Canvas (1)").transform.GetChild(0).GetChild(0).GetChild(6).gameObject.SetActive(false);
        //GameObject.Find("Canvas (1)").transform.GetChild(0).GetChild(0).GetChild(7).gameObject.SetActive(false);

        //Color c = GameObject.Find("Canvas (1)").transform.GetChild(0).GetChild(0).GetChild(0).GetChild(6).GetComponent<Image>().color;
        //c.a = 0;
        //GameObject.Find("Canvas (1)").transform.GetChild(0).GetChild(0).GetChild(0).GetChild(6).GetComponent<Image>().color = c;
        //GameObject.Find("Canvas (1)").transform.GetChild(0).GetChild(0).GetChild(0).GetChild(7).GetComponent<Image>().color = c;

    }
    void EnableRoPanel()
    {
        GameObject.Find("RO Panel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        GameObject.FindObjectOfType<Obj5_RO_Manager>().Initiliaze();
    

    }

    void enableFade2()
    {
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
        Invoke("nextObjective2", 3.0f);
    }
    void nextObjective2()
    {
        EnableRoPanel1();
        Invoke("nextObjectiveVo1", 3.0f);
        GameObject.FindObjectOfType<conversationManager>().EnableConversation("");
        GameObject.FindObjectOfType<conversationManager>().DisableConversation();
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadeout");

    }
    void EnableRoPanel1()
    {
        GameObject.Find("RO Panel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(1).gameObject.SetActive(true);

        GameObject.FindObjectOfType<Obj5_RO_Manager>().EnableSubmitButtonObj5_RO2();
        Debug.Log("RO2 Called+++++++++++++++++++++");
    }

    void enableFade3()
    {
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");

        Invoke("nextObjective3", 3.0f);
    }
    void nextObjective3()
    {
        EnableRoPanel2();
        Invoke("nextObjectiveVo1", 3.0f);
        GameObject.FindObjectOfType<conversationManager>().EnableConversation("");
        GameObject.FindObjectOfType<conversationManager>().DisableConversation();
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadeout");
        GameObject.Find("Summerise_Pnl").transform.GetChild(0).gameObject.SetActive(false);

    }
    void EnableRoPanel2()
    {
        GameObject.Find("RO Panel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(2).gameObject.SetActive(true);

        GameObject.FindObjectOfType<Obj5_RO_Manager>().Initiliaze_ROFact1();
    }

    void enableFade4()
    {
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
        Invoke("nextObjective4", 3.0f);
    }
    void nextObjective4()
    {
        EnableRoPanel3();
        Invoke("nextObjectiveVo1", 3.0f);
        GameObject.FindObjectOfType<conversationManager>().EnableConversation("");
        GameObject.FindObjectOfType<conversationManager>().DisableConversation();
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadeout");

    }
    void EnableRoPanel3()
    {
        GameObject.Find("RO Panel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(3).gameObject.SetActive(true);

        GameObject.FindObjectOfType<Obj5_RO_Manager>().Initiliaze_ROFact2();
    }

    void enableFade5()
    {
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
        Invoke("nextObjective5", 3.0f);
    }
    void nextObjective5()
    {
        EnableRoPanel4();
        Invoke("nextObjectiveVo1", 3.0f);
        GameObject.FindObjectOfType<conversationManager>().EnableConversation("");
        GameObject.FindObjectOfType<conversationManager>().DisableConversation();
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadeout");

    }
    void EnableRoPanel4()
    {
        GameObject.Find("RO Panel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(4).gameObject.SetActive(true);

        GameObject.FindObjectOfType<Obj5_RO_Manager>().Initiliaze_ROFact3();
    }

    void nextObjectiveVo1()
    {

        GameObject.FindObjectOfType<timeline_new>().load_next();

    }

    void StartQuestion1()
    {
        GameObject.FindObjectOfType<conversationManager>().DisableConversation();
        GameObject.FindObjectOfType<QuestionManager>().EnableForObj5Quest1();
    }
    void StartQuestion2()
    {
        GameObject.FindObjectOfType<conversationManager>().DisableConversation();
        GameObject.FindObjectOfType<QuestionManager>().EnableForObj5Quest2();
    }
    void StartQuestion3()
    {
        GameObject.FindObjectOfType<conversationManager>().DisableConversation();
        GameObject.FindObjectOfType<QuestionManager>().EnableForObj5Quest3();
    }

    void LO2_Quest2()
    {
        GameObject.FindObjectOfType<conversationManager>().DisableConversation();
        GameObject.FindObjectOfType<QuestionManager>().Obj5_LO2_Question2();

        //GameObject.Find("Canvas (1)").transform.GetChild(0).gameObject.SetActive(false);
        //GameObject.Find("Canvas (1)").transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        //GameObject.Find("Canvas (1)").transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
        //GameObject.Find("Canvas (1)").transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
    }
    void LO2_Quest3()
    {
        GameObject.FindObjectOfType<conversationManager>().DisableConversation();
        GameObject.FindObjectOfType<QuestionManager>().Obj5_LO2_Question3();
        GameObject.Find("Canvas (1)").transform.GetChild(0).GetComponent<Animator>().SetBool("Play", true); 
    }
    
    void ActiveQuestion2Setup()
    {
        GameObject.Find("Canvas (1)").transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        GameObject.Find("Canvas (1)").transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        GameObject.Find("Canvas (1)").transform.GetChild(0).GetChild(3).gameObject.SetActive(true);
        //GameObject.Find("GameManager").GetComponent<GameManager>().isObj5On = true;
    }

    public void enable_panel(GameObject object_to_enable)
    {
        //  Animator animator_of_object = object_to_enable.GetComponent<Animator>();
        object_to_enable.SetActive(true);
        //   animator_of_object.Play("enable", 0);
    }

    void ShowFractions()
    {
        GameObject.Find("FractionInputPanel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("FractionInputPanel").transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        GameObject.Find("FractionInputPanel").transform.GetChild(1).gameObject.SetActive(true);
        GameObject.Find("FractionInputPanel").transform.GetChild(2).gameObject.SetActive(true);
        GameObject.Find("Canvas (1)/LO1_Representation_Pnl").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("FractionInputPanel").transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = "2";
        GameObject.Find("FractionInputPanel").transform.GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text = "8";
        GameObject.Find("Question_1_Text").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("Question_2_Text").transform.GetChild(0).gameObject.SetActive(false);
        //GameObject.FindObjectOfType<QuestionManager>().DisableNumbers_InputField();

    }
    void HideFractions()
    {
        GameObject.Find("FractionInputPanel").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("FractionInputPanel").transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        GameObject.Find("FractionInputPanel").transform.GetChild(1).gameObject.SetActive(false);
        GameObject.Find("FractionInputPanel").transform.GetChild(2).gameObject.SetActive(false);
        GameObject.Find("Canvas (1)/LO1_Representation_Pnl").transform.GetChild(0).gameObject.SetActive(false);

    }

    void Show_LO1_Representation_1()
    {
        GameObject.Find("Canvas (1)/LO1_Representation_Pnl (1)").transform.GetChild(0).gameObject.SetActive(true);

        GameObject.Find("Canvas (1)").transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        GameObject.Find("Canvas (1)").transform.GetChild(0).GetChild(4).GetChild(0).gameObject.SetActive(true);

        GameObject.Find("Canvas (1)").transform.GetChild(0).GetChild(4).GetChild(1).GetChild(0).gameObject.SetActive(true);


    }
    void Hide_LO1_Representation_1()
    {
        GameObject.Find("Canvas (1)/LO1_Representation_Pnl (1)").transform.GetChild(0).gameObject.SetActive(false);

        GameObject.Find("Canvas (1)").transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        GameObject.Find("Canvas (1)").transform.GetChild(0).GetChild(4).GetChild(0).gameObject.SetActive(false);
    }
}