using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class Obj10_Manager : MonoBehaviour
{
    string jsonFileName = ".json";
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

            case "":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("");
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
        EnableRoPanel();
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
        GameObject.FindObjectOfType<Obj10_RO_Manager>().Initiliaze();
    }
}
