using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VisualQTypeNumberLineManager : MonoBehaviour
{
    // Start is called before the first frame update
    Button btn0, btn1, btn2, btn3, btn4, btn5, btn6, btn7, btn8, btn9;
    Button hintOkayBtn, submitValueBtn, deleteBtn, submitAnswerButton,closeHintBt,restartBt,quitBt,forceQuitBt, forceQuitYesBt, forceQuitNoBt;
    InputField inputField_Num;
    NumberLineManager line;
    bool isNumerator;
    int divisionValue = 0;
    bool lineCreated = false;
    public GameObject hintPopup_Pnl,gameOverPopUp, forceQuitPopUp;
    Text hintMsg_Text,Question;


    private void OnEnable()
    {
        VisualQTypeManager.OnLoadNextQuestion += SetQuestionMsg;
        VisualQTypeManager.OnGameOver += ShowGameOver;
        NumberLineManager.OnShowHint += ShowHitWithMsg;
    }

    private void OnDisable()
    {
        VisualQTypeManager.OnLoadNextQuestion -= SetQuestionMsg;
        VisualQTypeManager.OnGameOver -= ShowGameOver;
        NumberLineManager.OnShowHint -= ShowHitWithMsg;
    }

    void Start()
    {
        Initialized();
    }

    private void Initialized()
    {
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

        submitValueBtn = transform.GetChildFromName<Button>("ValueSubmit"); //GameObject.Find("VisualQType_FDTemplate/TemplateQType/Image_Root/NextBtn").GetComponent<Button>();
        submitValueBtn.onClick.AddListener(() => OnDenominatorSubmit());
        submitAnswerButton = transform.GetChildFromName<Button>("NextBtn"); //GameObject.Find("VisualQType_FDTemplate/TemplateQType/Image_Root/NextBtn").GetComponent<Button>();
        submitAnswerButton.onClick.AddListener(() => OnAnswerSubmit());

        deleteBtn = transform.GetChildFromName<Button>("ClearInputField"); //GameObject.Find("VisualQType_FDTemplate/TemplateQType/Image_Root/QuestionArea_Panel/ClearInputField").GetComponent<Button>();
        deleteBtn.onClick.AddListener(() => OnDeleteBtn());

        closeHintBt = transform.GetChildFromName<Button>("ButtonCloseHint"); //GameObject.Find("VisualQType_FDTemplate/TemplateQType/Image_Root/QuestionArea_Panel/ClearInputField").GetComponent<Button>();
        closeHintBt.onClick.AddListener(() => CloseHintPopup());

        restartBt = transform.GetChildFromName<Button>("ButtonRestart"); //GameObject.Find("VisualQType_FDTemplate/TemplateQType/Image_Root/QuestionArea_Panel/ClearInputField").GetComponent<Button>();
        restartBt.onClick.AddListener(() => OnReStart());
        quitBt = transform.GetChildFromName<Button>("ButtonQuit"); //GameObject.Find("VisualQType_FDTemplate/TemplateQType/Image_Root/QuestionArea_Panel/ClearInputField").GetComponent<Button>();
        quitBt.onClick.AddListener(() => OnQuit());
        forceQuitBt = transform.GetChildFromName<Button>("Quit");
        forceQuitBt.onClick.AddListener(() => OnForceQuit());
        forceQuitYesBt = transform.GetChildFromName<Button>("ButtonYes");
        forceQuitYesBt.onClick.AddListener(() => GoToLoadingPage());
        forceQuitNoBt = transform.GetChildFromName<Button>("ButtonNo");
        forceQuitNoBt.onClick.AddListener(() => HideForceQuitPanel());
        line = transform.GetChildFromName<NumberLineManager>("Line");
        hintPopup_Pnl = transform.GetChildFromName<Transform>("Hint_Popup_Panel").gameObject;
        gameOverPopUp = transform.GetChildFromName<Transform>("Hint_GameOver_Panel").gameObject;
        forceQuitPopUp = transform.GetChildFromName<Transform>("Hint_QuitGame").gameObject;
        hintMsg_Text = transform.GetChildFromName<Text>("Text_Hint");
        Question = transform.GetChildFromName<Text>("FractionQuestionText");
        hintPopup_Pnl.SetActive(false);
        gameOverPopUp.SetActive(false);
        forceQuitPopUp.SetActive(false);
    }

    void OnNumberButton(int number)
    {
        if (isNumerator)
        {
            Debug.Log("OnNumberButton: " + number);
            divisionValue = divisionValue * 10 + number;
            inputField_Num.text = divisionValue.ToString();
        }
        
    }

    void OnNumeratorField(PointerEventData eventData)
    {
        isNumerator = true;
        //isDenominator = false;
    }

    void OnDeleteBtn()
    {
       
            inputField_Num.text = "";
            divisionValue = 0;
        
        
    }

    void OnAnswerSubmit()
    {
        if (VisualQTypeManager.instance.canGotoNextQuestion)
        {
            VisualQTypeManager.instance.NextQuestion();
            OnDeleteBtn();
        }
        else
        {
            ShowHitWithMsg("Please answer before continueing.");
        }
    }

    void OnDenominatorSubmit()
    {
        isNumerator = false;
       
        if(divisionValue >= 0)
        {
            if (VisualQTypeManager.instance.checkdenominator(divisionValue))
            {
                line.createDivision(divisionValue);
                lineCreated = true;
            }
            else
            {
                ShowHitWithMsg("The number of parts in a number line should be equal to the denominator ");
            }
            
        }
           
        //divide numberline
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

    void SetQuestionMsg(string msg)
    {
        Question.text = msg;
        initilizeNumberline();
    }

    void initilizeNumberline()
    {
        if (lineCreated)
        {
            line.resetLine();
        }
    }

    public void ShowHintPopup()
    {
        hintPopup_Pnl.SetActive(true);
    }

    public void CloseHintPopup()
    {
        hintPopup_Pnl.SetActive(false);

    }

    public void ShowGameOver()
    {
        gameOverPopUp.SetActive(true);
    }

    public void OnReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ShowForceQuitPanel()
    {
        forceQuitPopUp.SetActive(true);
    }

    public void HideForceQuitPanel()
    {
        forceQuitPopUp.SetActive(false);
    }

    public void GoToLoadingPage()
    {
        SceneManager.LoadScene(0);
    }
    public void OnForceQuit()
    {
        ShowForceQuitPanel();
    }
    public void OnQuit()
    {
        Application.Quit();
    }
}
