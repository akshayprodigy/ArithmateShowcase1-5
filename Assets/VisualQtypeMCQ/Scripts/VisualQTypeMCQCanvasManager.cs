using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VisualQTypeMCQCanvasManager : MonoBehaviour
{
    // Start is called before the first frame update
    Text hintMsg_Text, Question;
    public GameObject hintPopup_Pnl, gameOverPopUp;
    Button hintOkayBtn, submitAnswerButton, closeHintBt, restartBt, quitBt;

    visualQtypeSelectionButton[] selectionbuttonlist;
    SetQtypeSelectionButton[] qtypeSelectionButton;
    int wrongAnswerCount = 0;
    private void OnEnable()
    {
        VisualQTypeMcqManager.OnLoadNextQuestion += loadNextQuestion;
        VisualQTypeMcqManager.OnGameOver += ShowGameOver;
    }

    private void OnDisable()
    {
        VisualQTypeMcqManager.OnLoadNextQuestion += loadNextQuestion;
        VisualQTypeMcqManager.OnGameOver -= ShowGameOver;
    }

    void Start()
    {
        innitlize();
    }

    void innitlize()
    {

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
        selectionbuttonlist = new visualQtypeSelectionButton[4];
        selectionbuttonlist = transform.GetComponentsInChildren<visualQtypeSelectionButton>();
        hideSelectionButton();
        hintPopup_Pnl.SetActive(false);
        gameOverPopUp.SetActive(false);
    }
    void hideSelectionButton()
    {
        foreach (visualQtypeSelectionButton bt in selectionbuttonlist)
        {
            bt.gameObject.SetActive(false);
        }
    }
    void loadNextQuestion(QTypeMCQQuestion question)
    {
        Debug.Log("loadNextQuestion");
        SetQuestionMsg(question.question);
        setUpSelectionButton(question.qption.ToArray());
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
        int counter=0;
        int selectionCounter = 0;
        int answerCounter = 0;
        for (int i= 0;i< qtypeSelectionButton.Length; i++)
        {
            if (selectionbuttonlist[i].isSelected)
            {
                selectionCounter++;
            }
            if (qtypeSelectionButton[i].answer)
            {
                answerCounter++;
            }
           if( selectionbuttonlist[i].isSelected == qtypeSelectionButton[i].answer)
            {
                counter++;
            }
        }
        if(counter == qtypeSelectionButton.Length)
        {
            // get next question
            VisualQTypeMcqManager.instance.NextQuestion();
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

    void SetQuestionMsg(string msg)
    {
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
