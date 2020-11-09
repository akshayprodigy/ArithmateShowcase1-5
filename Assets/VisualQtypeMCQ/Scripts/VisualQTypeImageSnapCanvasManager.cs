using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VisualQTypeImageSnapCanvasManager : MonoBehaviour
{
    // Start is called before the first frame update
    Button btn0, btn1, btn2, btn3, btn4, btn5, btn6, btn7, btn8, btn9;
    InputField inputField_Num, inputField_Den, inputField_Whole;
    Text hintMsg_Text, Question,headingText;
    Image QuestionImage;
    public GameObject hintPopup_Pnl, gameOverPopUp;
    Button hintOkayBtn, submitAnswerButton, closeHintBt, restartBt, quitBt, deleteBtn;
    bool isNumerator, isDenominator, isWhole;
    int numeratorValue, denominatorValue, wholeValue;
    visualQtypeSelectionButton[] selectionbuttonlist;
    SetQtypeSelectionButton[] qtypeSelectionButton;
    int wrongAnswerCount = 0;
    QTypeSnapImageQuestion currentQuesion;
    Transform snapArea;
    private void OnEnable()
    {
        VisualQTypeSnapImageManager.OnLoadNextQuestion += loadNextQuestion;
        VisualQTypeSnapImageManager.OnGameOver += ShowGameOver;
        visualQtypesegmentImage.OnSnapPoint += OnSnapPointEnter;
    }

    private void OnDisable()
    {
        VisualQTypeSnapImageManager.OnLoadNextQuestion += loadNextQuestion;
        VisualQTypeSnapImageManager.OnGameOver -= ShowGameOver;
        visualQtypesegmentImage.OnSnapPoint -= OnSnapPointEnter;
    }

    void OnSnapPointEnter(bool value)
    {
        if (value)
        {
            ShowInputData();
        }
        else
        {
            ShowHitWithMsg("Check line position");
        }
    }

    void Start()
    {
        innitlize();
    }

    void innitlize()
    {

        // numberpad
        btn0 = transform.GetChildFromName<Button>("0");
        btn1 = transform.GetChildFromName<Button>("1");
        btn2 = transform.GetChildFromName<Button>("2");
        btn3 = transform.GetChildFromName<Button>("3");
        btn4 = transform.GetChildFromName<Button>("4");
        btn5 = transform.GetChildFromName<Button>("5");
        btn6 = transform.GetChildFromName<Button>("6");
        btn7 = transform.GetChildFromName<Button>("7");
        btn8 = transform.GetChildFromName<Button>("8");
        btn9 = transform.GetChildFromName<Button>("9");

        btn0.onClick.AddListener(() => OnNumberButton(0));
        btn1.onClick.AddListener(() => OnNumberButton(1));
        btn2.onClick.AddListener(() => OnNumberButton(2));
        btn3.onClick.AddListener(() => OnNumberButton(3));
        btn4.onClick.AddListener(() => OnNumberButton(4));
        btn5.onClick.AddListener(() => OnNumberButton(5));
        btn6.onClick.AddListener(() => OnNumberButton(6));
        btn7.onClick.AddListener(() => OnNumberButton(7));
        btn8.onClick.AddListener(() => OnNumberButton(8));
        btn9.onClick.AddListener(() => OnNumberButton(9));

        inputField_Num = transform.GetChildFromName<InputField>("InputField_Numerator");// GameObject.Find("VisualQType_FDTemplate/TemplateQType/Image_Root/QuestionArea_Panel/InputField_Numerator").GetComponent<InputField>();
        EventTrigger.Entry entryNum = new EventTrigger.Entry();
        entryNum.callback.AddListener((eventData) => { OnNumeratorField((PointerEventData)eventData); });
        inputField_Num.gameObject.GetComponent<EventTrigger>().triggers.Add(entryNum);

        inputField_Den = transform.GetChildFromName<InputField>("InputField_Denominator");// GameObject.Find("VisualQType_FDTemplate/TemplateQType/Image_Root/QuestionArea_Panel/InputField_Numerator").GetComponent<InputField>();
        EventTrigger.Entry entryDen = new EventTrigger.Entry();
        entryDen.callback.AddListener((eventData) => { OnDenominatorField((PointerEventData)eventData); });
        inputField_Den.gameObject.GetComponent<EventTrigger>().triggers.Add(entryDen);

        inputField_Whole = transform.GetChildFromName<InputField>("InputField_Whole");// GameObject.Find("VisualQType_FDTemplate/TemplateQType/Image_Root/QuestionArea_Panel/InputField_Numerator").GetComponent<InputField>();
        EventTrigger.Entry entryWhole = new EventTrigger.Entry();
        entryWhole.callback.AddListener((eventData) => { OnWholeField((PointerEventData)eventData); });
        inputField_Whole.gameObject.GetComponent<EventTrigger>().triggers.Add(entryWhole);

        deleteBtn = transform.GetChildFromName<Button>("ClearInputField"); //GameObject.Find("VisualQType_FDTemplate/TemplateQType/Image_Root/QuestionArea_Panel/ClearInputField").GetComponent<Button>();
        deleteBtn.onClick.AddListener(() => OnDeleteBtn());

        closeHintBt = transform.GetChildFromName<Button>("ButtonCloseHint"); //GameObject.Find("VisualQType_FDTemplate/TemplateQType/Image_Root/QuestionArea_Panel/ClearInputField").GetComponent<Button>();
        closeHintBt.onClick.AddListener(() => CloseHintPopup());

        restartBt = transform.GetChildFromName<Button>("ButtonRestart"); //GameObject.Find("VisualQType_FDTemplate/TemplateQType/Image_Root/QuestionArea_Panel/ClearInputField").GetComponent<Button>();
        restartBt.onClick.AddListener(() => OnReStart());
        quitBt = transform.GetChildFromName<Button>("ButtonQuit"); //GameObject.Find("VisualQType_FDTemplate/TemplateQType/Image_Root/QuestionArea_Panel/ClearInputField").GetComponent<Button>();
        quitBt.onClick.AddListener(() => OnQuit());
        submitAnswerButton = transform.GetChildFromName<Button>("NextBtn"); //GameObject.Find("VisualQType_FDTemplate/TemplateQType/Image_Root/NextBtn").GetComponent<Button>();
        submitAnswerButton.onClick.AddListener(() => OnAnswerSubmit());
        hintPopup_Pnl = transform.GetChildFromName<Transform>("Hint_Popup_Panel").gameObject;
        gameOverPopUp = transform.GetChildFromName<Transform>("Hint_GameOver_Panel").gameObject;
        hintMsg_Text = transform.GetChildFromName<Text>("Text_Hint");
        Question = transform.GetChildFromName<Text>("QuestionText");
        QuestionImage = transform.GetChildFromName<Image>("QuestionImage");
        headingText = transform.GetChildFromName<Text>("QtypeText");
        snapArea = transform.GetChildFromName<Transform>("SnapArea");
        selectionbuttonlist = new visualQtypeSelectionButton[4];
        selectionbuttonlist = transform.GetComponentsInChildren<visualQtypeSelectionButton>();
        ResetinputTypeValues();
        hideSelectionButton();
        hideInputData();
        hintPopup_Pnl.SetActive(false);
        gameOverPopUp.SetActive(false);
        Question.gameObject.SetActive(false);
        QuestionImage.gameObject.SetActive(false);
    }

    void hideSelectionButton()
    {
        foreach (visualQtypeSelectionButton bt in selectionbuttonlist)
        {
            bt.gameObject.SetActive(false);
        }
    }

    void ShowInputData()
    {
        btn0.gameObject.SetActive(true);
        btn1.gameObject.SetActive(true);
        btn2.gameObject.SetActive(true);
        btn3.gameObject.SetActive(true);
        btn4.gameObject.SetActive(true);
        btn5.gameObject.SetActive(true);
        btn6.gameObject.SetActive(true);
        btn7.gameObject.SetActive(true);
        btn8.gameObject.SetActive(true);
        btn9.gameObject.SetActive(true);
        deleteBtn.gameObject.SetActive(true);
        inputField_Num.gameObject.SetActive(true);
        inputField_Den.gameObject.SetActive(true);
        inputField_Whole.gameObject.SetActive(true);
    }

    void hideInputData()
    {
        btn0.gameObject.SetActive(false);
        btn1.gameObject.SetActive(false);
        btn2.gameObject.SetActive(false);
        btn3.gameObject.SetActive(false);
        btn4.gameObject.SetActive(false);
        btn5.gameObject.SetActive(false);
        btn6.gameObject.SetActive(false);
        btn7.gameObject.SetActive(false);
        btn8.gameObject.SetActive(false);
        btn9.gameObject.SetActive(false);
        deleteBtn.gameObject.SetActive(false);
        inputField_Num.gameObject.SetActive(false);
        inputField_Den.gameObject.SetActive(false);
        inputField_Whole.gameObject.SetActive(false);
    }

    void hideQuestionText()
    {
        Question.gameObject.SetActive(false);
    }

    void hideQuestionImage()
    {
        QuestionImage.gameObject.SetActive(false);
    }

    void OnNumberButton(int number)
    {
        if (isNumerator)
        {
            Debug.Log("OnNumberButton: " + number);
            numeratorValue = numeratorValue * 10 + number;
            inputField_Num.text = numeratorValue.ToString();
        }
        else if(isDenominator){
            Debug.Log("OnNumberButton: " + number);
            denominatorValue = denominatorValue * 10 + number;
            inputField_Den.text = denominatorValue.ToString();
        }else if (isWhole)
        {
            Debug.Log("OnNumberButton: " + number);
            wholeValue = wholeValue * 10 + number;
            inputField_Whole.text = wholeValue.ToString();
        }

    }

    void OnNumeratorField(PointerEventData eventData)
    {
        isNumerator = true;
        isDenominator = false;
        isWhole = false;
    }

    void OnDenominatorField(PointerEventData eventData)
    {
        isNumerator = false;
        isDenominator = true;
        isWhole = false;
    }

    void OnWholeField(PointerEventData eventData)
    {
        isNumerator = false;
        isDenominator = false;
        isWhole = true;
    }

    void OnDeleteBtn()
    {
        if (isNumerator)
        {
            inputField_Num.text = "";
            numeratorValue = 0;
        }else if (isDenominator)
        {
            inputField_Den.text = "";
            denominatorValue = 0;
        }else if (isWhole)
        {
            inputField_Whole.text = "";
            wholeValue = 0;
        }
        
        //divisionValue = 0;


    }

    void ResetinputTypeValues()
    {
        inputField_Num.text = "";
        numeratorValue = 0;
        inputField_Den.text = "";
        denominatorValue = 0;
        inputField_Whole.text = "";
        wholeValue = 0;
        isNumerator = false;
        isDenominator = false;
        isWhole = false;
    }

    void ResetSnapArea()
    {
        if (snapArea.childCount > 0)
        {
            foreach (Transform child in snapArea)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }

    void LoadSnapImage(string name)
    {
        Transform Obj = Instantiate(Resources.Load<Transform>(name));
        Obj.parent = snapArea;
        Obj.GetComponent<RectTransform>().localPosition = Vector3.zero;
    }

    void loadNextQuestion(QTypeSnapImageQuestion question)
    {
        //Debug.Log("loadNextQuestion");
        // need to work on this
        ResetSnapArea();
        hideInputData();
        hideSelectionButton();
        hideQuestionText();
        hideQuestionImage();
        currentQuesion = question;
        SetHeadingMsg(question.heading);
        if (question.textTypeQuestion)
            SetQuestionMsg(question.question);
        LoadSnapImage(question.SnapImageObjectName);
        //else
        //    SetQuestionImage(question.imageName);
        if (question.inputTypeAnswer)
        {
            
        }
        //else
        //setUpSelectionButton(question.qption.ToArray());
    }

   public void setUpSelectionButton(SetQtypeSelectionButton[] setQtypeSelectionButton)
   {
        qtypeSelectionButton = setQtypeSelectionButton;
        for (int i = 0; i < setQtypeSelectionButton.Length; i++)
        {
            selectionbuttonlist[i].gameObject.SetActive(true);
            if(setQtypeSelectionButton[i].answerOption!=null)
                selectionbuttonlist[i].setText(setQtypeSelectionButton[i].answerOption);
            if(setQtypeSelectionButton[i].imageLocation!=null)
                selectionbuttonlist[i].setImage(setQtypeSelectionButton[i].imageLocation);
        }
   }

    void OnAnswerSubmit()
    {
        if (currentQuesion.inputTypeAnswer)
        {
            if (currentQuesion.inputValues.Length > 2)
            {
                if(wholeValue == currentQuesion.inputValues[0]&&numeratorValue == currentQuesion.inputValues[1]&& denominatorValue == currentQuesion.inputValues[2])
                {
                    VisualQTypeMcqNewManager.instance.NextQuestion();
                    wrongAnswerCount = 0;
                }
                else
                {
                    ShowHitWithMsg("Check the values of the fraction");
                }
            }
            else
            {
                if ( numeratorValue == currentQuesion.inputValues[0] && denominatorValue == currentQuesion.inputValues[1])
                {
                    VisualQTypeSnapImageManager.instance.NextQuestion();
                    wrongAnswerCount = 0;
                }
                else
                {
                    ShowHitWithMsg("Check the values of the fraction");
                }
            }
            ResetinputTypeValues();
        }
        else
        {
            int counter = 0;
            int selectionCounter = 0;
            int answerCounter = 0;
            for (int i = 0; i < qtypeSelectionButton.Length; i++)
            {
                if (selectionbuttonlist[i].isSelected)
                {
                    selectionCounter++;
                }
                if (qtypeSelectionButton[i].answer)
                {
                    answerCounter++;
                }
                if (selectionbuttonlist[i].isSelected == qtypeSelectionButton[i].answer)
                {
                    counter++;
                }
            }
            if (counter == qtypeSelectionButton.Length)
            {
                // get next question
                VisualQTypeMcqNewManager.instance.NextQuestion();
                wrongAnswerCount = 0;
            }
            else
            {
                if (selectionCounter > 0)
                {
                    wrongAnswerCount++;
                    if (wrongAnswerCount > 1)
                    {
                        ShowHitWithMsg("Need To load LOD1");
                    }
                    else
                    {
                        if (selectionCounter < answerCounter)
                            ShowHitWithMsg("Check if you selected all figures that can be represented as a fraction");
                        else
                            ShowHitWithMsg("To represent any part of an object as a fraction, we need to ensure all parts are equal. Check if you have selected a figure in which the parts are not equal. ");

                    }
                }
                else
                {
                    ShowHitWithMsg("Please answer before continueing.");
                }


            }
            resetSelectionButton();
        }
        

        //if (VisualQTypeManager.instance.canGotoNextQuestion)
        //{
        //    VisualQTypeManager.instance.NextQuestion();

        //}
        //else
        //{
        //    ShowHitWithMsg("Please answer before continueing.");
        //}
    }

    void resetSelectionButton()
    {
        foreach(visualQtypeSelectionButton vssb in selectionbuttonlist)
        {
            vssb.ResetButton();
        }
    }

    void ShowHitWithMsg(string msg)
    {
        SetHintText(msg);
        ShowHintPopup();
    }

    void SetHintText(string msg)
    {
        hintMsg_Text.text = msg;
    }

    
    public void ShowHintPopup()
    {
        hintPopup_Pnl.SetActive(true);
    }

    public void CloseHintPopup()
    {
        hintPopup_Pnl.SetActive(false);

    }

    void SetHeadingMsg(string msg)
    {
        //Question.gameObject.SetActive(true);
        headingText.text = msg;

    }

    void SetQuestionImage(string imaneName)
    {
        QuestionImage.gameObject.SetActive(true);
        QuestionImage.sprite = Resources.Load<Sprite>(imaneName);
        QuestionImage.type = Image.Type.Sliced;
    }

    void SetQuestionMsg(string msg)
    {
        Question.gameObject.SetActive(true);
        Question.text = msg;

    }


    public void ShowGameOver()
    {
        gameOverPopUp.SetActive(true);
    }

    public void OnReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
