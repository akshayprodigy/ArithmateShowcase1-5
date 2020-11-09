using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualQTypeMcqManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static VisualQTypeMcqManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public delegate void LoadNextQuestion(QTypeMCQQuestion question);
    public static event LoadNextQuestion OnLoadNextQuestion;
    public delegate void GameOver();
    public static event GameOver OnGameOver;

    int QuestionNumber = 0, numerator = 0, denominator = 0;


    void Start()
    {
        Invoke("NextQuestion", 0.5f);
    }

    // Update is called once per frame
    public void NextQuestion()
    {
        //string question = "";
        QTypeMCQQuestion question = new QTypeMCQQuestion();
        List<SetQtypeSelectionButton> qTypeAns = new List<SetQtypeSelectionButton>();
        SetQtypeSelectionButton selectionButton1 = new SetQtypeSelectionButton();
        SetQtypeSelectionButton selectionButton2 = new SetQtypeSelectionButton();
        SetQtypeSelectionButton selectionButton3 = new SetQtypeSelectionButton();
        SetQtypeSelectionButton selectionButton4 = new SetQtypeSelectionButton();
        Debug.Log("QuestionNumber: " + QuestionNumber);
        switch (QuestionNumber)
        {
            case 0:
                question.question = "which of the following figure represents ¼?";
                question.inputTypeAnswer = false;
                question.textTypeQuestion = true;
                //List<SetQtypeSelectionButton> qTypeAns = new List<SetQtypeSelectionButton>();
                //SetQtypeSelectionButton selectionButton1 = new SetQtypeSelectionButton();

                selectionButton1.imageLocation = "box1";
                selectionButton1.answer = true;
                qTypeAns.Add(selectionButton1);
               
                selectionButton2.imageLocation = "box2";
                selectionButton2.answer = false;
                qTypeAns.Add(selectionButton2);
                
                selectionButton3.imageLocation = "box3";
                selectionButton3.answer = true;
                qTypeAns.Add(selectionButton3);
                
                selectionButton4.imageLocation = "box4";
                selectionButton4.answer = false;
                qTypeAns.Add(selectionButton4);
                question.qption = qTypeAns;
                break;
            case 1:
                question.question = "which of the following figure represents 1/3";

                selectionButton1.imageLocation = "circles1";
                selectionButton1.answer = false;
                qTypeAns.Add(selectionButton1);

                selectionButton2.imageLocation = "circles2";
                selectionButton2.answer = true;
                qTypeAns.Add(selectionButton2);

                selectionButton3.imageLocation = "circles3";
                selectionButton3.answer = false;
                qTypeAns.Add(selectionButton3);

                selectionButton4.imageLocation = "circles4";
                selectionButton4.answer = false;
                qTypeAns.Add(selectionButton4);
                question.qption = qTypeAns;
                break;
            //case 2:
            //    question.question = "which of the following figure represents ¼?";
            //    //List<SetQtypeSelectionButton> qTypeAns = new List<SetQtypeSelectionButton>();
            //    //SetQtypeSelectionButton selectionButton1 = new SetQtypeSelectionButton();
            //    selectionButton1.imageLocation = "box1";
            //    selectionButton1.answer = true;
            //    qTypeAns.Add(selectionButton1);

            //    selectionButton2.imageLocation = "box2";
            //    selectionButton2.answer = false;
            //    qTypeAns.Add(selectionButton2);

            //    selectionButton3.imageLocation = "box3";
            //    selectionButton3.answer = true;
            //    qTypeAns.Add(selectionButton3);

            //    selectionButton4.imageLocation = "box4";
            //    selectionButton4.answer = false;
            //    qTypeAns.Add(selectionButton4);
            //    question.qption = qTypeAns;
            //    break;
            case 2:
                question.question = "The shaded part of which object can be represented as a fraction?";
                //List<SetQtypeSelectionButton> qTypeAns = new List<SetQtypeSelectionButton>();
                //SetQtypeSelectionButton selectionButton1 = new SetQtypeSelectionButton();
                selectionButton1.imageLocation = "box6";
                selectionButton1.answer = true;
                qTypeAns.Add(selectionButton1);

                selectionButton2.imageLocation = "pentagon-3";
                selectionButton2.answer = false;
                qTypeAns.Add(selectionButton2);

                selectionButton3.imageLocation = "retancle2";
                selectionButton3.answer = true;
                qTypeAns.Add(selectionButton3);

                selectionButton4.imageLocation = "triangle1";
                selectionButton4.answer = false;
                qTypeAns.Add(selectionButton4);
                question.qption = qTypeAns;
                break;
            case 3:
                question.question = "The shaded part of which object can be represented as a fraction?";
                //List<SetQtypeSelectionButton> qTypeAns = new List<SetQtypeSelectionButton>();
                //SetQtypeSelectionButton selectionButton1 = new SetQtypeSelectionButton();
                selectionButton1.imageLocation = "triangle10";
                selectionButton1.answer = false;
                qTypeAns.Add(selectionButton1);

                selectionButton2.imageLocation = "ciecle9";
                selectionButton2.answer = true;
                qTypeAns.Add(selectionButton2);

                selectionButton3.imageLocation = "circle8";
                selectionButton3.answer = true;
                qTypeAns.Add(selectionButton3);

                selectionButton4.imageLocation = "box7";
                selectionButton4.answer = false;
                qTypeAns.Add(selectionButton4);
                question.qption = qTypeAns;
                break;
            case 4:
                if (OnGameOver != null)
                    OnGameOver();
                break;
        }
        if (OnLoadNextQuestion != null)
            OnLoadNextQuestion(question);
        QuestionNumber++;
    }
}
