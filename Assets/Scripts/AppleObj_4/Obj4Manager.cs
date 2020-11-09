using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class Obj4Manager : MonoBehaviour
{
    string jsonFileName = "Obj4AreaModule_json.json";
   
    public static int SelectedPart,other_part;
    public bool graden_activity;
    public GameObject ro_3_reinforcement,rectangle_animated, question_1, part_of_the_farm, thus_the_value_of,loadingAudio, VisualQType_AreaModel_Temp;

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
        Initialised();
        
    }

    void Initialised()
    {
        startAudio = GetComponent<AudioSource>();
        startAudio.Play();

        Invoke("audio_invoke", 2.0f);
        UnHighLightAllBlocks();
        loadingAudio = GameObject.Find("LoadAudio");
        if (UtilityArtifacts.backTraversal || UtilityArtifacts.comingbackafterTraversal)
        {
            Text textLoadingText = loadingAudio.transform.GetChild(2).GetComponent<Text>();
            //textLoadingText.text = "Let us understand this better";
        }
        ro_3_reinforcement = GameObject.Find("ro_3_reinforcement");
        ro_3_reinforcement.SetActive(false);
        rectangle_animated = GameObject.Find("rectangle_animated");
        rectangle_animated.SetActive(false);
        question_1 = GameObject.Find("question_1");
        question_1.SetActive(false);
        part_of_the_farm = GameObject.Find("part of the farm");
        part_of_the_farm.SetActive(false);
        thus_the_value_of = GameObject.Find("thus the value of");
        thus_the_value_of.SetActive(false);
        VisualQType_AreaModel_Temp = GameObject.Find("VisualQType_AreaModel_Temp").transform.GetChild(0).gameObject;
        //VisualQType_AreaModel_Temp.SetActive(false);
    }
    void audio_invoke()
    {
        Debug.Log(EVENT.SetUpTimeLine);
        EventManager.setNameOfJasonFile(jsonFileName);
        EventManager.Broadcast(EVENT.SetUpTimeLine);
    }
    
    void EventToHandle(string EventName)
    {
        if (startAudio.isPlaying)
            startAudio.Stop();
        loadingAudio.SetActive(false);
        switch (EventName)
        {
            case "Obj4_Here_is":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Here is a map of the apple farm. You can keep it. It will be helpful, incase if you need to go back to the farm.");
                break;
            case "Obj4_Les_us_have_a_look":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Let's have a look at this map and try to see if we can learn more about fractions using the map");
                Invoke("HighLightAllBlocks", 2.0f);
                break;
            case "Obj4_quest1":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Select the sections of the apple farm that have benches in them.");
                Invoke("StartQuestion1", 5.0f);
                break;
            case "Obj4_We_learnt_that":
               
                question_1.SetActive(false);
                graden_activity = true;
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("We learnt that,");
                HighlightBenches();
                break;
            case "Obj4_A_part_of":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("A part of a whole can be represented as fractions.");
                break;
            case "Obj4_In_the_map":
                HighlightBenches();
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Have a look at this map. The sections of the farm that has benches are selected.");
                break;
            case "Obj4_Lets_see_using":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Let's see using fractions, how we represent the part of the farm that has benches.");
                HighlightBenches();
                break;
            case "Obj4_As":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("As you can see, the apple farm has 8 equal sections. The sections that have benches are highlighted.");
                break;
            case "Obj4_If_you_simply":
                GameObject.Find("LO Panel").transform.GetChild(0).gameObject.SetActive(true);
                GameObject.FindObjectOfType<conversationManager>().DisableConversation();
                part_of_the_farm.SetActive(true);
                GameObject.Find("SubmitButton").transform.GetChild(0).gameObject.SetActive(false);
                //GameObject.FindObjectOfType<conversationManager>().EnableConversation("If you simply consider the number of sections that have benches and the total number of sections in the farm,you have the numerator and the denominator");
                break;
            case "Obj4_Writing":
                //GameObject.FindObjectOfType<conversationManager>().EnableConversation("Writing the numerator over the denominator will give you the fraction that represents the section of farm have benches.");
                Invoke("disableDialog", 10.0f);
                Invoke("enableFade1", 10.0f);
                break;
            case "Obj4_quest2":
                part_of_the_farm.SetActive(false);
                Debug.Log("Hello");
                break;
            case "Obj4_You_can_represent":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("You can also represent the part of the farm that does not have benches and also the part that has trees");
                HighLightextra_part();
                Invoke("UnHighLightAllBlocks", 5);
                break;
            case "Obj4_You_can":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("You can represent any part of the farm as fractions");
                UnHighLightAllBlocks();
                break;
            case "Obj4_Parts_with_bench":
                HighlightBenches();
                GameObject.FindObjectOfType<conversationManager>().EnableConversation(" Part of the farm with benches");
                part_of_the_farm.SetActive(true);
                break;
            case "Obj4_Parts_with_trees":
                part_of_the_farm.SetActive(false);
                UnHighLightAllBlocks();
                HighLightAllBlocks();
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Part of the farm that have trees");
                break;
            //case "Obj4_Parts_with_Brown_colour":
            //    UnHighLightAllBlocks();
            //    HighLightBrow();
            //    GameObject.FindObjectOfType<conversationManager>().EnableConversation("Part of the farm that has the colour brown");
            //    break;
            case "Obj4_If_you_consider":
                UnHighLightAllBlocks();
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("If you consider the farm as a rectangle instead and say it has few parts which are coloured, you can represent the coloured part of the rectangle as fractions too.");
                Enable_rectangle();
                break;
            case "Obj4_Similarly_instead":
                EnableCircle();
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Similarly, instead of a rectangle, if there is a circle and a part of it is coloured, you can represent the coloured part of the circle as fractions.");
                break;
            case "Obj4_You_can_represent_a_shape":
                EnableOtherShape();
                Invoke("enableFade2", 7.0f);
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("You can represent a part of any shape as fractions, as long as an object has equal parts.");
                break;
            case "Obj4_quest3":
                Debug.Log("Quest 3");
                DisableOtherShape();
                break;
            case "Obj4_quest4":
                Debug.Log("Quest 4");
                EnableRoPanel2();

                break;
            case "Obj5_Here_is_the_map":
                ro_3_reinforcement.SetActive(false);
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Here is the map of the apple farm.");
                break;

            case "Obj5_lets_consider":
               //ro_3_reinforcement.SetActive(false);
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Lets consider what fraction of the farm have apple trees.");
                break;
            case "Obj5_As":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("As you can see, all 8 sections of the farm have apple trees.");
                break;
            case "Obj5_So_we":
                GameObject.FindObjectOfType<conversationManager>().DisableConversation();
                GameObject.Find("LO Panel").transform.GetChild(2).gameObject.SetActive(true);
                Invoke("enable_lo1_numdenum",4);
               

                //highlight farm


                break;
            case "Obj5_or_we":
               // GameObject.FindObjectOfType<conversationManager>().EnableConversation("or we can say that this 1 whole farm have apple trees");
                GameObject.Find("LO Panel").transform.GetChild(3).gameObject.SetActive(true);
                Invoke("HighLightAllBlocks", 3);
                Invoke("UnHighLightAllBlocks", 4);
                Invoke("HighLightAllBlocks", 5);
                Invoke("UnHighLightAllBlocks", 6);
                Invoke("HighLightAllBlocks", 7);
                Invoke("UnHighLightAllBlocks", 8);
               

                break;
            case "Obj5_Thus":
                //  GameObject.FindObjectOfType<conversationManager>().EnableConversation("Thus, the value of any fraction is 1 whose numerator and denominator is the same.");
                GameObject.FindObjectOfType<conversationManager>().DisableConversation();
                GameObject.Find("LO Panel").transform.GetChild(1).gameObject.SetActive(false);
                GameObject.Find("LO Panel").transform.GetChild(2).gameObject.SetActive(false);
                GameObject.Find("LO Panel").transform.GetChild(3).gameObject.SetActive(false);
                thus_the_value_of.SetActive(true);
                Invoke("enableFade3", 7.0f);
                break;
            case "Obj5_Quest5":
                thus_the_value_of.SetActive(false);
                Debug.Log("Quest3");
                break;
        }
        //8604531475
    }

    void enable_lo1_numdenum()
    {
        
        GameObject.Find("LO Panel").transform.GetChild(1).gameObject.SetActive(true);
    }

   public  void HighLightAllBlocks()
    {
       foreach(GameObject g in GameObject.FindGameObjectsWithTag("Map Part"))
        {
            g.GetComponent<SpriteRenderer>().color = Color.green;
        }
        Invoke("UnHighLightAllBlocks", 4.0f);
    }
    public void UnHighLightAllBlocks()
    {
        GameObject.Find("highlights").transform.GetChild(6).GetComponent<SpriteRenderer>().color = Color.white;
        GameObject.Find("highlights").transform.GetChild(2).GetComponent<SpriteRenderer>().color = Color.white;
        GameObject.Find("highlights").transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.white;
        GameObject.Find("highlights").transform.GetChild(3).GetComponent<SpriteRenderer>().color = Color.white;
        GameObject.Find("highlights").transform.GetChild(5).GetComponent<SpriteRenderer>().color = Color.white;
        GameObject.Find("highlights").transform.GetChild(4).GetComponent<SpriteRenderer>().color = Color.white;
        GameObject.Find("highlights").transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
        GameObject.Find("highlights").transform.GetChild(7).GetComponent<SpriteRenderer>().color = Color.white;
    }
   public void HighlightBenches()
    {
        GameObject.Find("highlights").transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.green; 
        GameObject.Find("highlights").transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.green; 
        GameObject.Find("highlights").transform.GetChild(3).GetComponent<SpriteRenderer>().color = Color.green; 
        GameObject.Find("highlights").transform.GetChild(5).GetComponent<SpriteRenderer>().color = Color.green; 
        GameObject.Find("highlights").transform.GetChild(7).GetComponent<SpriteRenderer>().color = Color.green; 
    }
    public void HighLightBrow()
    {
        GameObject.Find("highlights").transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.green; 
        GameObject.Find("highlights").transform.GetChild(7).GetComponent<SpriteRenderer>().color = Color.green;
    }
    void HighLightextra_part()
    {
        GameObject.Find("highlights").transform.GetChild(2).GetComponent<SpriteRenderer>().color = Color.green;
        GameObject.Find("highlights").transform.GetChild(4).GetComponent<SpriteRenderer>().color = Color.green;
        GameObject.Find("highlights").transform.GetChild(6).GetComponent<SpriteRenderer>().color = Color.green;
       
    }
        void StartQuestion1()
    {
        GameObject.FindObjectOfType<conversationManager>().DisableConversation();
        GameObject.FindObjectOfType<QuestionManager>().EnableForObj4Quest1();
        GameObject.FindObjectOfType<GameManager>().isObj4On = true;
        question_1.SetActive(true);
    }
    void enableFade1()
    {
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
        Invoke("nextObjective1", 3.0f);
    }
    void nextObjective1()
    {
        EnableRoPanel();
        //nextObjectiveVo2();
        Invoke("nextObjectiveVo1", 3.0f);
        GameObject.FindObjectOfType<conversationManager>().EnableConversation("");
        GameObject.FindObjectOfType<conversationManager>().DisableConversation();
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadeout");

    }
    void nextObjectiveVo1()
    {

        GameObject.FindObjectOfType<timeline_new>().load_next();

    }

    void EnableRoPanel()
    {
        GameObject.Find("RO Panel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        GameObject.FindObjectOfType<AplleObj4ROManager>().Initiliaze();
    }
    void DisableRoPanel()
    {
        GameObject.Find("RO Panel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
    }
    void disableDialog()
    {
        part_of_the_farm.SetActive(false);
        GameObject.Find("LO Panel").transform.GetChild(0).gameObject.SetActive(false);
    }
    void EnableCircle()
    {
        GameObject.Find("Garden Map").transform.GetChild(0).gameObject.SetActive(false);
        rectangle_animated.SetActive(false);
        GameObject.Find("Garden Map").transform.GetChild(1).gameObject.SetActive(true);
    }
    void Enable_rectangle()
    {
        rectangle_animated.SetActive(true);
        Invoke("disable_garden", 1);
    }
    void disable_garden()
    {
        GameObject.Find("Garden Map").transform.GetChild(0).gameObject.SetActive(false);
    }


    void EnableOtherShape()
    {
        GameObject.Find("Garden Map").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("Garden Map").transform.GetChild(1).gameObject.SetActive(false);
        GameObject.Find("Garden Map").transform.GetChild(2).gameObject.SetActive(true);
    }
    public void DisableOtherShape()
    {
        GameObject.Find("Garden Map").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("Garden Map").transform.GetChild(1).gameObject.SetActive(false);
        GameObject.Find("Garden Map").transform.GetChild(2).gameObject.SetActive(false);
    }
    void enableFade2()
    {
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
        Invoke("nextObjective2", 3.0f);
    }
    void nextObjective2()
    {
        EnableRoPanel1();
        GameObject.FindObjectOfType<conversationManager>().DisableConversation();
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadeout");

    }
    void nextObjectiveVo2()
    {

        EnableRoPanel1();
       

    }
    void EnableRoPanel1()
    {
        GameObject.Find("RO Panel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        GameObject.FindObjectOfType<AplleObj4ROManager>().EnableSubmitButtonRO2();
       
    }
    void EnableRoPanel2()
    {
        GameObject.Find("RO Panel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        GameObject.FindObjectOfType<AplleObj4ROManager>().EnableSubmitButtonRO3();

    }
    void enableFade3()
    {
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
        Invoke("nextObjective3", 3.0f);
    }
    void nextObjective3()
    {
       // Invoke("nextObjectiveVo3", 3.0f);
        GameObject.Find("LO Panel").transform.GetChild(1).gameObject.SetActive(false);
        GameObject.Find("LO Panel").transform.GetChild(2).gameObject.SetActive(false);
        GameObject.Find("LO Panel").transform.GetChild(3).gameObject.SetActive(false);
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadeout");
        GameObject.FindObjectOfType<conversationManager>().DisableConversation();
        EnableRoPanel3();
        GameObject.FindObjectOfType<timeline_new>().load_next();

    }
    
    void EnableRoPanel3()
    {
        GameObject.Find("RO Panel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(3).gameObject.SetActive(true);
        GameObject.FindObjectOfType<AplleObj4ROManager>().EnableSubmitButtonRO4();

    }
    void DisableRoPanel1()
    {
        GameObject.Find("RO Panel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
    }
    void StartQuestion3()
    {
        GameObject.FindObjectOfType<conversationManager>().DisableConversation();
        GameObject.FindObjectOfType<QuestionManager>().EnableForObj4Quest3();
        
    }

}
