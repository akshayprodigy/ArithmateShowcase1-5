using System.Collections;
using System.Collections.Generic;
using TextSpeech;
using UnityEngine;

public class obj7SpinWheelTalkSceneManager : MonoBehaviour
{
    string jsonFileName = "Obj7_SpinWheel_json.json";

    public static int SelectedPart;
    conversationManager conversationManager;
    FortuneWheelManager fortuneWheelManager;
    Obj7spinwheeltalkCanvasManager canvasManager;
    public GameObject LoadingAudio;
    bool SeconTimecall = false;


    private void OnEnable()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        timeline_new.OnEventCalled += EventToHandle;
        //Obj14CanvasManager.OnCanvasJobDone += CanvasJobDone;
    }

    private void OnDisable()
    {
        timeline_new.OnEventCalled -= EventToHandle;
    }
    void Start()
    {
        Initialised();
        // DisableRoPanel();

    }
    void Initialised()
    {
        Invoke("audio_invoke", 2.0f);
        conversationManager = GameObject.FindObjectOfType<conversationManager>();
        canvasManager = GameObject.FindObjectOfType<Obj7spinwheeltalkCanvasManager>();
        fortuneWheelManager = GameObject.FindObjectOfType<FortuneWheelManager>();
        Debug.Log(canvasManager.name);
        SpeechToText.instance.onResultCallback = onResultCallback;
        LoadingAudio = GameObject.Find("LoadAudio");
#if UNITY_ANDROID
        SpeechToText.instance.onResultCallback = onResultCallback;
        SpeechToText.instance.onReadyForSpeechCallback = onReadyForSpeechCallback;
        SpeechToText.instance.onEndOfSpeechCallback = onEndOfSpeechCallback;
        SpeechToText.instance.onRmsChangedCallback = onRmsChangedCallback;
        SpeechToText.instance.onBeginningOfSpeechCallback = onBeginningOfSpeechCallback;
        SpeechToText.instance.onErrorCallback = onErrorCallback;
        SpeechToText.instance.onPartialResultsCallback = onPartialResultsCallback;
#endif

    }


    void AddLog(string log)
    {
        //txtLog.text += "\n" + log;
        //txtNewLog.text = log;
        conversationManager.EnableConversation(log);
        Debug.Log(log);
    }

    string spokenValue;
    void onResultCallback(string _data)
    {
        Debug.Log("log onResultCallback: " + _data);
        spokenValue = _data;
        AddLog("Result: " + _data);
        canvasManager.showSubmitButton();
    }

    void onReadyForSpeechCallback(string _params)
    {
        //AddLog("Ready for the user to start speaking");
    }
    void onEndOfSpeechCallback()
    {
        //AddLog("User stops speaking");
    }
    void onRmsChangedCallback(float _value)
    {
       // float _size = _value * 10;
        //RmsBar.sizeDelta = new Vector2(_size, 5);
    }
    void onBeginningOfSpeechCallback()
    {
        //AddLog("User has started to speak");
    }
    void onErrorCallback(string _params)
    {
        //AddLog("Error: " + _params);
    }
    void onPartialResultsCallback(string _params)
    {
        spokenValue = _params;
        canvasManager.showSubmitButton();
        spokenValue = _params;
        AddLog("Result: " + _params);
    }

    void audio_invoke()
    {
        Debug.Log(EVENT.SetUpTimeLine);
        EventManager.setNameOfJasonFile(jsonFileName);
        EventManager.Broadcast(EVENT.SetUpTimeLine);
    }

    public void ShowMsginChefPanel(string msg)
    {
        conversationManager.EnableConversation(msg);
    }

    public bool compareSpokenwithSpinvalue(string val)
    {
        if (spokenValue.Contains(val.ToLower()))
            return true;
        else
            return false;
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
            case "Obj7_learntreadfractions":
                canvasManager.HideLoading();
                canvasManager.ShowSpinWheel();
                conversationManager.EnableConversation("You have just learnt how to read fractions");
                fortuneWheelManager.DisableSpinButton();
                break;
            case "Obj7_wheelpointer":
                canvasManager.HighlightArrow();
                conversationManager.EnableConversation("Here’s a wheel on which we have different fractions. And here is a pointer");
                break;
            case "Obj7_Tapspinbutton":
                canvasManager.StopArrowHighlight();
                canvasManager.HighlightSpinButton();
                conversationManager.EnableConversation("Tap on the spin button to make wheel spin");
                break;
            case "Obj7_readmikebutton":
                canvasManager.StopSpinButtonHighlight();
                canvasManager.showMicrophone();
                conversationManager.EnableConversation("Once the wheel stops, read out that fraction by clicking on the mic button");
                break;
            case "Obj7_aheadspinwheel":
                canvasManager.hideMicrophone();
                conversationManager.EnableConversation("You get only three chances before you get all the fractions right.So go ahead and spin the wheel.");
                Invoke("EnableSpinButton", 3);
                break;

        }
    }

    public void EnableSpinButton()
    {
        conversationManager.DisableConversation();
        fortuneWheelManager.EnableSpinButton();
    }
    // Update is called once per frame

}
