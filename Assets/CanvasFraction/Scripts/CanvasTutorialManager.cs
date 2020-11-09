using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasTutorialManager : MonoBehaviour
{
    // Start is called before the first frame update
    string jsonFileName = "Tutorial_CanvasFlow.json";
    string jsonQuestionFileName = "Tutorial_Canvas.json";
    public string gameDataFileName, path, dataAsJson;
    CanvasManager canvasManager;
    GameObject infoText;
    public const string onFractionButton = "FractionButton";
    public const string onImproperButton = "ImproperButton";
    public const string onTabButton = "TabButton";
    public const string on1Button = "1";
    public const string on2Button = "2";
    public const string on3Button = "3";
    public const string on4Button = "4";
    public const string on6Button = "6";
    public const string on8Button = "8";
    public const string onMulButton = "\times";
    public const string onEqualButton = "=";
    public const string onNotRequired = "";
    public const string onNextButton = "NextButton";
    public const string onSubmitButton = "SubmitButton";
    public const string onError = "OnError";
    public const string onSuccess = "OnSuccess";
    DialogControllerScript dialogController;

    string needsTobeClicked;

    GameObject fullScreenCover, handPointer, questionAreaStarting, questionAreaEnding, answerStartingPoint, answerEndingPoint, numberStartingPoint, numberEndingPoint, operatorStartingPoint, operatorEndingPoint;
    GameObject valueMultiply, value1, value2, value3, value4, value6, value8, valueNext, valueBackSpace, valueClearRow, valueSubmit, valueFraction, valueImproper, valueKeyBoard, valueTab,valueEqual,valueMul;
    private float _timePassed;
    bool startMoving = false;
    Vector3 startingPosition, endingPosition;
    GameObject gameOver;
    public AudioSource startAudio;
    private void OnEnable()
    {
        timeline_new.OnEventCalled += EventToHandle;
        CanvasManager.OntestOnTutorial += UserHasClicked;
    }

    private void OnDisable()
    {
        timeline_new.OnEventCalled -= EventToHandle;
        CanvasManager.OntestOnTutorial -= UserHasClicked;
    }

    void Start()
    {
        Initialised();
    }

    // Update is called once per frame



    void Initialised()
    {
        startAudio = GetComponent<AudioSource>();
        startAudio.Play();

        canvasManager = GameObject.FindObjectOfType<CanvasManager>();
        
        infoText = GameObject.Find("LoadAudio");
        fullScreenCover = GameObject.Find("FullScreen");
        handPointer = GameObject.Find("HandPointer");
        questionAreaStarting = GameObject.Find("QuestionStartPoint");
        questionAreaEnding = GameObject.Find("QuestionEndPoint");
        answerStartingPoint = GameObject.Find("AnswerStartPoint");
        answerEndingPoint = GameObject.Find("AnswerEndPoint");
        numberStartingPoint = GameObject.Find("NumberStartPoint");
        numberEndingPoint = GameObject.Find("NumberEndPoint");
        operatorStartingPoint = GameObject.Find("OperatorStartPoint");
        operatorEndingPoint = GameObject.Find("OperatorEndPoint");

        valueMultiply = GameObject.Find("ValueX");
        value1 = GameObject.Find("Value1");
        value2 = GameObject.Find("Value2");
        value3 = GameObject.Find("Value3");
        value4 = GameObject.Find("Value4");
        value6 = GameObject.Find("Value6");
        value8 = GameObject.Find("Value8");
        valueNext = GameObject.Find("ValueNextStep");
        valueBackSpace = GameObject.Find("ValueBackSpace");
        valueClearRow = GameObject.Find("ValueClear");
        valueSubmit = GameObject.Find("ValueSubmit");
        valueFraction = GameObject.Find("ValueFraction");
        valueImproper = GameObject.Find("ValueImproperFraction");
        valueKeyBoard = GameObject.Find("ValueKeyBoard");
        valueTab = GameObject.Find("ValueTab");
        valueEqual = GameObject.Find("valueEqual");
        valueMul = GameObject.Find("valueMul");
        gameOver = GameObject.Find("GameOver");
        gameOver.SetActive(false);
        HideHandlePointer();
        JsonFromStreamingAssets(jsonQuestionFileName);
        Invoke("audio_invoke", 2.0f);

    }

    void audio_invoke()
    {
        Debug.Log(EVENT.SetUpTimeLine);
        EventManager.setNameOfJasonFile(jsonFileName);
        EventManager.Broadcast(EVENT.SetUpTimeLine);
    }

    void HideHandlePointer()
    {
        handPointer.SetActive(false);
    }

    void ShowHandOnPoint(GameObject point)
    {
        if (!fullScreenCover.active)
        {
            ShowScreenCover();
        }
        handPointer.transform.position = point.transform.position;
        handPointer.SetActive(true);
    }

    void HideScreenCover()
    {
        fullScreenCover.SetActive(false);
    }

  
    void MovePointer(GameObject startingPoint,GameObject endingPoint)
    {
        startMoving = true;
        handPointer.transform.position = startingPoint.transform.position;
        handPointer.SetActive(true);
        startingPosition = startingPoint.transform.position;
        endingPosition = endingPoint.transform.position;

    }

    void stopMovingPointer()
    {
        startMoving = false;
        handPointer.SetActive(false);
    }

    private void Update()
    {
        if (startMoving)
        {
            _timePassed += Time.deltaTime;
            handPointer.transform.position = Vector3.Lerp(startingPosition, endingPosition,
                Mathf.PingPong(_timePassed, 1));
        }
    }

    void ShowScreenCover()
    {
        fullScreenCover.SetActive(true);
    }

    void hideScreenandPointer()
    {
        Debug.Log("hideScreenandPointer");
        HideHandlePointer();
        HideScreenCover();
    }

    void EventToHandle(string EventName)
    {
        if (startAudio.isPlaying)
            startAudio.Stop();
        infoText.SetActive(false);
        switch (EventName)
        {
            case "CanvasTutorialIntro":
                //Debug.Log("CanvasTutorialIntro");
                //Debug.Log("length_of_audio: " + timeline_new.Instance.getCurrentAudioLength());
                break;
            case "CanvasTutorialQuestionArea":
                MovePointer(questionAreaStarting, questionAreaEnding);
                break;
            case "CanvasTutorialAnswerArea":
                MovePointer(answerStartingPoint, answerEndingPoint);
                break;
            case "CanvasTutorialDigitPress":
                MovePointer(numberStartingPoint, numberEndingPoint);
                break;
            case "CanvasTutorialOperatorPress":
                MovePointer(operatorStartingPoint, operatorEndingPoint);
                break;
            case "CanvasTutorialExpandKeyBoard":
                stopMovingPointer();
                ShowHandOnPoint(valueKeyBoard);
                break;
            case "CanvasTutorialUseBackSpace":
                ShowHandOnPoint(valueBackSpace);
                break;
            case "CanvasTutorialClearRow":
                ShowHandOnPoint(valueClearRow);
                break;
            case "CanvasTutorialNextStep":
                ShowHandOnPoint(valueNext);
                break;
            case "CanvasTutorialOnSubmit":
                ShowHandOnPoint(valueSubmit);
                break;
            case "CanvasTutorialOnTab":
                ShowHandOnPoint(valueTab);
                break;
            case "CanvasTutorialStartProblem":
                HideHandlePointer();
                break;
            case "CanvasTutorialStep1FractionPress":
                ShowHandOnPoint(valueFraction);
                userneedsToClick(onFractionButton);
                canvasManager.buttonToPress = onFractionButton;
                Invoke("hideScreenandPointer", timeline_new.Instance.getCurrentAudioLength());
                break;
            case "CanvasTutorialStep1ImproperPress":
                ShowHandOnPoint(valueImproper);
                userneedsToClick(onImproperButton);
                canvasManager.buttonToPress = onImproperButton;
                Invoke("hideScreenandPointer", timeline_new.Instance.getCurrentAudioLength());
                break;
            case "CanvasTutorialEnter1":
                ShowHandOnPoint(value1);
                userneedsToClick(on1Button);
                canvasManager.buttonToPress = on1Button;
                Invoke("hideScreenandPointer", timeline_new.Instance.getCurrentAudioLength());
                break;
            case "CanvasTutorialStep1clickTab":
                ShowHandOnPoint(valueTab);
                userneedsToClick(onTabButton);
                canvasManager.buttonToPress = onTabButton;
                Invoke("hideScreenandPointer", timeline_new.Instance.getCurrentAudioLength());
                break;
            case "CanvasTutorialenter2":
                ShowHandOnPoint(value2);
                userneedsToClick(on2Button);
                canvasManager.buttonToPress = on2Button;
                Invoke("hideScreenandPointer", timeline_new.Instance.getCurrentAudioLength());
                break;
            case "CanvasTutorialStep1clickEqual":
                ShowHandOnPoint(valueEqual);
                userneedsToClick(onEqualButton);
                canvasManager.buttonToPress = onEqualButton;
                Invoke("hideScreenandPointer", timeline_new.Instance.getCurrentAudioLength());
                break;
            case "CanvasTutorialStep1clickMultiply":
                ShowHandOnPoint(valueMul);
                userneedsToClick(onMulButton);
                canvasManager.buttonToPress = onMulButton;
                Invoke("hideScreenandPointer", timeline_new.Instance.getCurrentAudioLength());
                break;
            case "CanvasTutorialenter4":
                ShowHandOnPoint(value4);
                userneedsToClick(on4Button);
                canvasManager.buttonToPress = on4Button;
                Invoke("hideScreenandPointer", timeline_new.Instance.getCurrentAudioLength());
                break;
            case "CanvasTutorialenterNextStep":
                ShowHandOnPoint(valueNext);
                userneedsToClick(onNextButton);
                canvasManager.buttonToPress = onNextButton;
                Invoke("hideScreenandPointer", timeline_new.Instance.getCurrentAudioLength());
                break;
            case "CanvasTutorialenterStep2":
                ShowScreenCover();
                break;
            case "CanvasTutorialenter3":
                ShowHandOnPoint(value3);
                userneedsToClick(on3Button);
                canvasManager.buttonToPress = on3Button;
                Invoke("hideScreenandPointer", timeline_new.Instance.getCurrentAudioLength());
                break;
            case "CanvasTutorialenter6":
                ShowHandOnPoint(value6);
                userneedsToClick(on6Button);
                canvasManager.buttonToPress = on6Button;
                Invoke("hideScreenandPointer", timeline_new.Instance.getCurrentAudioLength());
                break;
            case "CanvasTutorialenter8":
                ShowHandOnPoint(value8);
                userneedsToClick(on8Button);
                canvasManager.buttonToPress = on8Button;
                Invoke("hideScreenandPointer", timeline_new.Instance.getCurrentAudioLength());
                break;
            case "CanvasTutorialenterNextSubmit":
                ShowHandOnPoint(valueSubmit);
                userneedsToClick(onSubmitButton);
                canvasManager.buttonToPress = onSubmitButton;
                Invoke("hideScreenandPointer", timeline_new.Instance.getCurrentAudioLength());
                break;

        }
    }

    void userneedsToClick(string val)
    {
        needsTobeClicked = val;
    }

    void UserHasClicked(string userValue)
    {
        if (!string.Equals(userValue, onError))
        {
            if(string.Equals(needsTobeClicked, onSubmitButton))
            {
                OnTutorialOver();
            }else
            timeline_new.Instance.load_next();
        }
        else
        {
            timeline_new.Instance.playAudioOnRelearn("CanvasTutorialNotCorrectEntry");
            Invoke("AutoCompleteNextStepandProceed", timeline_new.Instance.getCurrentAudioLength());//timeline_new.Instance.getCurrentAudioLength()
            //AutoCompleteNextStepandProceed();
        }
    }

    void OnTutorialOver()
    {
        gameOver.SetActive(true);
        Invoke("LoadFractionCanvas", 3);
    }

    void LoadFractionCanvas()
    {
        SceneManager.LoadScene(UtilityArtifacts.CanvasSceneNumber);
    }

    void AutoCompleteNextStepandProceed()
    {
        if (canvasManager.dialog_on)
        {
            Debug.Log("dialog_on");
            dialogController = GameObject.FindObjectOfType<DialogControllerScript>();
            dialogController.readMsg();
        }

        Debug.Log("needsTobeClicked: " + needsTobeClicked);
        switch (needsTobeClicked)
        {
            case onFractionButton:
                canvasManager.ShowSighn();
                break;
            case onImproperButton:
                canvasManager.pressedFraction();
                break;
            case onTabButton:
                canvasManager.pressedTab();
                break;
            case on1Button:
                canvasManager.pressedNumber(1);
                break;
            case on2Button:
                canvasManager.pressedNumber(2);
                break;
            case on3Button:
                canvasManager.pressedNumber(3);
                break;
            case on4Button:
                canvasManager.pressedNumber(4);
                break;
            case on6Button:
                canvasManager.pressedNumber(6);
                break;
            case on8Button:
                Debug.Log("on8Button");
                canvasManager.pressedNumber(8);
                break;
            case onMulButton:
                canvasManager.pressedExpression("\\times");
                break;
            case onEqualButton:
                canvasManager.pressedExpression("=");
                break;
            case onNextButton:
                canvasManager.pressedNextstep();
                break;
        }
       // timeline_new.Instance.load_next();
    }


    void JsonFromStreamingAssets(string jsonFileName)
    {
        path = Path.Combine(Application.streamingAssetsPath, jsonFileName);
        if (Application.platform == RuntimePlatform.Android)
        {
            // Android
            string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, gameDataFileName);

            // Android only use WWW to read file
            WWW reader = new WWW(path);
            while (!reader.isDone) { }
            // Read the json from the file into a string
            string dataAsJson = reader.text;
            Debug.LogError(dataAsJson);
            // Pass the json to JsonUtility, and tell it to create a GameData object from it
            UtilityREST.TutorialQuestionData = dataAsJson;
        }
        else
        {

            if (File.Exists(path))
            {
                // Read the json from the file into a string
                dataAsJson = File.ReadAllText(path);
                UtilityREST.TutorialQuestionData = dataAsJson;
            }
        }
        Debug.LogError("TutorialQuestionData: " + UtilityREST.TutorialQuestionData);
        canvasManager.ShowQuestionFromTutorial(UtilityREST.TutorialQuestionData);
    }
}
