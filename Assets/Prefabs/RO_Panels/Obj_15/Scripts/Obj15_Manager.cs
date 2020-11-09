using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;


public class Obj15_Manager : MonoBehaviour
{
    string jsonFileName = "obj_15_new_story.json";
    public string ans;
    public static int correctDraggedApples;

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

    void Start()
    {
        GameObject.FindObjectOfType<conversationManager>().DisableDialouge();

    }

    void Initialised()
    {
        Invoke("audio_invoke", 2.0f);

        //this.GetComponent<Obj5AppleGenerator>().Initialize();
        //GameObject.FindObjectOfType<QuestionManager>().GetOkButton();

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

    void EventToHandle(string EventName)
    {
        switch (EventName)
        {

            case "obj_15_1b2_hint1":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("The customer wants half a cookie slab. Ensure that the fraction matches the cookie slab packed. ");
                break;

            case "obj_15_1b2_hint2":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("The amount of cookie slab to be packed should be according to the fraction given. Check again.");
                break;

            case "obj_15_1b2_wrong_ans":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation(" The tray from which you have picked up the cookie slab isn't right. Have a look at the denominator of this fraction. Pick from the tray that matches the number of parts in the cookie slab and the denominator.");
                break;
            case "obj_15_2b4_hint1":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("The customer wants 2/4 of a cookie slab. Ensure that the fraction matches the cookie slab packed.");
                break;
            case "obj_15_2b4_hint2":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("The amount of cookie slab to be packed should be according to the fraction given. Check again.");
                break;
            case "obj_15_4b8_hint1":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("The customer wants 4/8 of a cookie slab. Ensure that the fraction matches the cookie slab packed. ");
                break;
            case "obj_15_4b8_hint2":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("The amount of cookie slab to be packed should be according to the fraction given. Check again.");
                break;
            case "obj_15_ro_1_wrong":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Fractions are Equivalent if the quantity represented is same but the number of parts are different. All the circles here have different number of parts. 4 of them have the same size as the circle shaded, except for 1. This one is not equivalent to any of the other circles.");
                break;
            case "obj_15_plot_1b2_mistake":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Identify where 1/2 is on the number line and tap on the point precisely.");
                break;
            case "obj_15_plot_2b4_mistake":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Identify where 2/4 is on the number line and tap on the point precisely.");
                break;
            case "obj_15_plot_4b8_mistake":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Identify where 4/8 is on the number line and tap on the point precisely.");
                break;
            case "obj_15_RO_3_option_b":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("If you observe the number lines, carefully, you can tell that equivalent fractions align at the same point on the number lines");
                break;
            case "obj_15_RO_3_option_c":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("If you observe the number lines, carefully, you can tell that equivalent fractions align at the same point on the number lines");
                break;
            case "obj_15_RO_3_option_d":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("If you observe the number lines, carefully, you can tell that equivalent fractions align at the same point on the number lines");
                break;

        }

    }

    void enableFade1()
    {
        //GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
        Invoke("nextObjective1", 1.0f);
    }
    void nextObjective1()
    {
      //  EnableRoPanel();
    //    Invoke("nextObjectiveVo1", 1.0f);
      //  GameObject.FindObjectOfType<conversationManager>().EnableConversation("");
      //  GameObject.FindObjectOfType<conversationManager>().DisableConversation();
    //    GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadeout");

    }
    void nextObjectiveVo1()
    {

        GameObject.FindObjectOfType<timeline_new>().load_next();

    }
    void EnableRoPanel()
    {
        GameObject.Find("RO Panel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        GameObject.FindObjectOfType<Obj15_RO_Manager>().Initiliaze();
    }
}
