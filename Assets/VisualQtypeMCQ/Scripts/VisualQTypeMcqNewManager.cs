using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualQTypeMcqNewManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static VisualQTypeMcqNewManager instance;

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
    [SerializeField]
    public bool isTypesOfFraction = false;
    public void NextQuestion()
    {
        //string question = "";
        QTypeMCQQuestion question = new QTypeMCQQuestion();
        if (isTypesOfFraction)
        {
            switch (QuestionNumber)
            {
                case 0:
                    question.heading = "Express the shaded part of the image given as a proper fraction";
                    question.textTypeQuestion = false;
                    question.imageName = "circle7";
                    question.inputTypeAnswer = true;
                    int[] value = new int[] { 3, 8 };
                    question.inputValues = value;
                    break;
                case 1:
                    question.heading = "Express the shaded part of the image given as a proper fraction";
                    question.textTypeQuestion = false;
                    question.imageName = "box9";
                    question.inputTypeAnswer = true;
                    int[] value1 = new int[] { 2, 7 };
                    question.inputValues = value1;
                    break;
                case 2:
                    if (OnGameOver != null)
                        OnGameOver();
                    break;
            }
           
        }
        else
        {
            List<SetQtypeSelectionButton> qTypeAns = new List<SetQtypeSelectionButton>();
            SetQtypeSelectionButton selectionButton1 = new SetQtypeSelectionButton();
            SetQtypeSelectionButton selectionButton2 = new SetQtypeSelectionButton();
            SetQtypeSelectionButton selectionButton3 = new SetQtypeSelectionButton();
            SetQtypeSelectionButton selectionButton4 = new SetQtypeSelectionButton();
            //Debug.Log("QuestionNumber: " + QuestionNumber);
            switch (QuestionNumber)
            {
                case 0:
                    question.heading = "Identify the type of fraction from the given options";
                    question.textTypeQuestion = true;
                    question.question = "	5/6?";


                    selectionButton1.answerOption = "Mixed Fraction";
                    selectionButton1.answer = false;
                    qTypeAns.Add(selectionButton1);

                    selectionButton2.answerOption = "Proper  Fraction";
                    selectionButton2.answer = true;
                    qTypeAns.Add(selectionButton2);

                    selectionButton3.answerOption = "ImProper  Fraction";
                    selectionButton3.answer = false;
                    qTypeAns.Add(selectionButton3);

                    selectionButton4.answerOption = "Unit  Fraction";
                    selectionButton4.answer = false;
                    qTypeAns.Add(selectionButton4);

                    question.qption = qTypeAns;
                    break;
                case 1:
                    question.heading = "Identify the type of fraction from the given options";
                    question.textTypeQuestion = true;
                    question.question = "	3 1/3?";


                    selectionButton1.answerOption = "Mixed Fraction";
                    selectionButton1.answer = true;
                    qTypeAns.Add(selectionButton1);

                    selectionButton2.answerOption = "Proper  Fraction";
                    selectionButton2.answer = false;
                    qTypeAns.Add(selectionButton2);

                    selectionButton3.answerOption = "ImProper  Fraction";
                    selectionButton3.answer = false;
                    qTypeAns.Add(selectionButton3);

                    selectionButton4.answerOption = "Unit  Fraction";
                    selectionButton4.answer = false;
                    qTypeAns.Add(selectionButton4);

                    question.qption = qTypeAns;
                    break;
                case 20:
                    question.heading = "Identify the type of fraction from the given options";
                    question.textTypeQuestion = true;
                    question.question = "	9/4?";


                    selectionButton1.answerOption = "Mixed Fraction";
                    selectionButton1.answer = false;
                    qTypeAns.Add(selectionButton1);

                    selectionButton2.answerOption = "Proper  Fraction";
                    selectionButton2.answer = false;
                    qTypeAns.Add(selectionButton2);

                    selectionButton3.answerOption = "ImProper  Fraction";
                    selectionButton3.answer = true;
                    qTypeAns.Add(selectionButton3);

                    selectionButton4.answerOption = "Unit  Fraction";
                    selectionButton4.answer = false;
                    qTypeAns.Add(selectionButton4);

                    question.qption = qTypeAns;
                    break;
                case 3:
                    question.heading = "Identify the type of fraction from the given options";
                    question.textTypeQuestion = true;
                    question.question = "	5/5?";


                    selectionButton1.answerOption = "Mixed Fraction";
                    selectionButton1.answer = false;
                    qTypeAns.Add(selectionButton1);

                    selectionButton2.answerOption = "Proper  Fraction";
                    selectionButton2.answer = false;
                    qTypeAns.Add(selectionButton2);

                    selectionButton3.answerOption = "ImProper  Fraction";
                    selectionButton3.answer = true;
                    qTypeAns.Add(selectionButton3);

                    selectionButton4.answerOption = "Unit  Fraction";
                    selectionButton4.answer = false;
                    qTypeAns.Add(selectionButton4);

                    question.qption = qTypeAns;
                    break;
                case 4:
                    question.heading = "Identify the type of fraction from the given options";
                    question.textTypeQuestion = true;
                    question.question = "	1 2/4?";


                    selectionButton1.answerOption = "Mixed Fraction";
                    selectionButton1.answer = true ;
                    qTypeAns.Add(selectionButton1);

                    selectionButton2.answerOption = "Proper  Fraction";
                    selectionButton2.answer = false;
                    qTypeAns.Add(selectionButton2);

                    selectionButton3.answerOption = "ImProper  Fraction";
                    selectionButton3.answer = false;
                    qTypeAns.Add(selectionButton3);

                    selectionButton4.answerOption = "Unit  Fraction";
                    selectionButton4.answer = false;
                    qTypeAns.Add(selectionButton4);

                    question.qption = qTypeAns;
                    break;
                case 5:
                    question.heading = "Identify the type of fraction from the given options";
                    question.textTypeQuestion = true;
                    question.question = "	1/10?";


                    selectionButton1.answerOption = "Mixed Fraction";
                    selectionButton1.answer = false;
                    qTypeAns.Add(selectionButton1);

                    selectionButton2.answerOption = "Proper  Fraction";
                    selectionButton2.answer = true;
                    qTypeAns.Add(selectionButton2);

                    selectionButton3.answerOption = "ImProper  Fraction";
                    selectionButton3.answer = false;
                    qTypeAns.Add(selectionButton3);

                    selectionButton4.answerOption = "Unit  Fraction";
                    selectionButton4.answer = false;
                    qTypeAns.Add(selectionButton4);

                    question.qption = qTypeAns;
                    break;
                case 6:
                    question.heading = "Identify the fraction which matches with the fraction in the question";
                    question.textTypeQuestion = true;
                    question.question = "	Which of the following fraction is a proper fraction?";


                    selectionButton1.answerOption = "2/5";
                    selectionButton1.answer = true;
                    qTypeAns.Add(selectionButton1);

                    selectionButton2.answerOption = "7/5";
                    selectionButton2.answer = false;
                    qTypeAns.Add(selectionButton2);

                    selectionButton3.answerOption = "3 1/3";
                    selectionButton3.answer = false;
                    qTypeAns.Add(selectionButton3);

                    selectionButton4.answerOption = "9/8";
                    selectionButton4.answer = false;
                    qTypeAns.Add(selectionButton4);

                    question.qption = qTypeAns;
                    break;
                case 7:
                    question.heading = "Identify the fraction which matches with the fraction in the question";
                    question.textTypeQuestion = true;
                    question.question = "	Which of the following fraction is an improper fraction?";


                    selectionButton1.answerOption = "7/8";
                    selectionButton1.answer = false;
                    qTypeAns.Add(selectionButton1);

                    selectionButton2.answerOption = "7/4";
                    selectionButton2.answer = true;
                    qTypeAns.Add(selectionButton2);

                    selectionButton3.answerOption = "2 1/7";
                    selectionButton3.answer = false;
                    qTypeAns.Add(selectionButton3);

                    selectionButton4.answerOption = "9/0";
                    selectionButton4.answer = false;
                    qTypeAns.Add(selectionButton4);

                    question.qption = qTypeAns;
                    break;
                case 8:
                    question.heading = "Identify the fraction which matches with the fraction in the question";
                    question.textTypeQuestion = true;
                    question.question = "Which of the following fraction is a mixed fraction?";


                    selectionButton1.answerOption = "2 2/9";
                    selectionButton1.answer = true;
                    qTypeAns.Add(selectionButton1);

                    selectionButton2.answerOption = "7/5";
                    selectionButton2.answer = false;
                    qTypeAns.Add(selectionButton2);

                    selectionButton3.answerOption = " 3/3";
                    selectionButton3.answer = false;
                    qTypeAns.Add(selectionButton3);

                    selectionButton4.answerOption = "17/8";
                    selectionButton4.answer = false;
                    qTypeAns.Add(selectionButton4);

                    question.qption = qTypeAns;
                    break;
                case 9:
                    question.heading = "Identify the fraction which matches with the fraction in the question";
                    question.textTypeQuestion = true;
                    question.question = "	Which of the following fraction is a unit  fraction?";


                    selectionButton1.answerOption = "1 2/5";
                    selectionButton1.answer = false;
                    qTypeAns.Add(selectionButton1);

                    selectionButton2.answerOption = "7/5";
                    selectionButton2.answer = false;
                    qTypeAns.Add(selectionButton2);

                    selectionButton3.answerOption = "1/3";
                    selectionButton3.answer = true;
                    qTypeAns.Add(selectionButton3);

                    selectionButton4.answerOption = "9/8";
                    selectionButton4.answer = false;
                    qTypeAns.Add(selectionButton4);

                    question.qption = qTypeAns;
                    break;
                case 10:
                    question.heading = "Identify the fraction which matches with the fraction in the question";
                    question.textTypeQuestion = true;
                    question.question = "	Which of the following fraction is a improper  fraction?";


                    selectionButton1.answerOption = "2/5";
                    selectionButton1.answer = false;
                    qTypeAns.Add(selectionButton1);

                    selectionButton2.answerOption = "1/5";
                    selectionButton2.answer = false;
                    qTypeAns.Add(selectionButton2);

                    selectionButton3.answerOption = "3 1/3";
                    selectionButton3.answer = false;
                    qTypeAns.Add(selectionButton3);

                    selectionButton4.answerOption = "9/8";
                    selectionButton4.answer = true;
                    qTypeAns.Add(selectionButton4);

                    question.qption = qTypeAns;
                    break;
                case 11:
                    question.heading = "Identify the type of fractions for the shaded part of the given figures";
                    question.textTypeQuestion = true;
                    question.question = "	Which of the following fraction is a mixed  fraction?";


                    selectionButton1.answerOption = "2/5";
                    selectionButton1.answer = false;
                    qTypeAns.Add(selectionButton1);

                    selectionButton2.answerOption = "7/5";
                    selectionButton2.answer = false;
                    qTypeAns.Add(selectionButton2);

                    selectionButton3.answerOption = "3 1/3";
                    selectionButton3.answer = true;
                    qTypeAns.Add(selectionButton3);

                    selectionButton4.answerOption = "9/8";
                    selectionButton4.answer = false;
                    qTypeAns.Add(selectionButton4);

                    question.qption = qTypeAns;
                    break;
                case 12:
                    question.heading = "Identify the fraction which matches with the fraction in the question";
                    question.textTypeQuestion = false;
                    //question.question = "	Which of the following fraction is a mixed  fraction?";
                    question.imageName = "box7";

                    selectionButton1.answerOption = "Proper Fraction";
                    selectionButton1.answer = false;
                    qTypeAns.Add(selectionButton1);

                    selectionButton2.answerOption = "Mixed Fraction";
                    selectionButton2.answer = true;
                    qTypeAns.Add(selectionButton2);

                    question.qption = qTypeAns;
                    break;
                case 13:
                    question.heading = "Identify the fraction which matches with the fraction in the question";
                    question.textTypeQuestion = false;
                    //question.question = "	Which of the following fraction is a mixed  fraction?";
                    question.imageName = "box7";

                    selectionButton1.answerOption = "Proper Fraction";
                    selectionButton1.answer = true;
                    qTypeAns.Add(selectionButton1);

                    selectionButton2.answerOption = "Mixed Fraction";
                    selectionButton2.answer = false;
                    qTypeAns.Add(selectionButton2);

                    question.qption = qTypeAns;
                    break;
                case 14:
                    question.heading = "Identify the fraction which matches with the fraction in the question";
                    question.textTypeQuestion = false;
                    //question.question = "	Which of the following fraction is a mixed  fraction?";
                    question.imageName = "box7";

                    selectionButton1.answerOption = "Proper Fraction";
                    selectionButton1.answer = false;
                    qTypeAns.Add(selectionButton1);

                    selectionButton2.answerOption = "Mixed Fraction";
                    selectionButton2.answer = true;
                    qTypeAns.Add(selectionButton2);

                    question.qption = qTypeAns;
                    break;
                case 15:
                    question.heading = "Identify the fraction which matches with the fraction in the question";
                    question.textTypeQuestion = false;
                    //question.question = "	Which of the following fraction is a mixed  fraction?";
                    question.imageName = "box7";

                    selectionButton1.answerOption = "Proper Fraction";
                    selectionButton1.answer = true;
                    qTypeAns.Add(selectionButton1);

                    selectionButton2.answerOption = "Mixed Fraction";
                    selectionButton2.answer = false;
                    qTypeAns.Add(selectionButton2);

                    question.qption = qTypeAns;
                    break;
                case 16:
                    question.heading = "Identify the type of fraction for the point marked on the number line ";
                    question.textTypeQuestion = false;
                    //question.question = "	Which of the following fraction is a mixed  fraction?";
                    question.imageName = "box7";

                    selectionButton1.answerOption = "Proper Fraction";
                    selectionButton1.answer = false;
                    qTypeAns.Add(selectionButton1);

                    selectionButton2.answerOption = "Mixed Fraction";
                    selectionButton2.answer = true;
                    qTypeAns.Add(selectionButton2);

                    question.qption = qTypeAns;
                    break;
                case 17:
                    question.heading = "Identify the type of fraction for the point marked on the number line ";
                    question.textTypeQuestion = false;
                    //question.question = "	Which of the following fraction is a mixed  fraction?";
                    question.imageName = "box7";

                    selectionButton1.answerOption = "Proper Fraction";
                    selectionButton1.answer = true;
                    qTypeAns.Add(selectionButton1);

                    selectionButton2.answerOption = "Mixed Fraction";
                    selectionButton2.answer = false;
                    qTypeAns.Add(selectionButton2);

                    question.qption = qTypeAns;
                    break;
                case 18:
                    question.heading = "Identify the type of fraction for the point marked on the number line ";
                    question.textTypeQuestion = false;
                    //question.question = "	Which of the following fraction is a mixed  fraction?";
                    question.imageName = "box7";

                    selectionButton1.answerOption = "Proper Fraction";
                    selectionButton1.answer = false;
                    qTypeAns.Add(selectionButton1);

                    selectionButton2.answerOption = "Mixed Fraction";
                    selectionButton2.answer = true;
                    qTypeAns.Add(selectionButton2);

                    question.qption = qTypeAns;
                    break;
                case 19:
                    question.heading = "Identify the type of fraction for the point marked on the number line ";
                    question.textTypeQuestion = false;
                    //question.question = "	Which of the following fraction is a mixed  fraction?";
                    question.imageName = "box7";

                    selectionButton1.answerOption = "Proper Fraction";
                    selectionButton1.answer = true;
                    qTypeAns.Add(selectionButton1);

                    selectionButton2.answerOption = "Mixed Fraction";
                    selectionButton2.answer = false;
                    qTypeAns.Add(selectionButton2);

                    question.qption = qTypeAns;
                    break;
                case 2:
                    question.heading = "Express the shaded parts of the following figures as mixed fractions";
                    question.textTypeQuestion = false;
                    question.imageName = "circles1";
                    question.inputTypeAnswer = true;
                    int[] value = new int[] { 2,1,4 };
                    question.inputValues = value;
                    break;
                case 21:
                    question.heading = "Express the shaded parts of the following figures as mixed fractions";
                    question.textTypeQuestion = false;
                    question.imageName = "circles1";
                    question.inputTypeAnswer = true;
                    int[] value1 = new int[] { 1, 2, 3 };
                    question.inputValues = value1;
                    break;
                case 22:
                    question.heading = "Express the shaded parts of the following figures as mixed fractions";
                    question.textTypeQuestion = false;
                    question.imageName = "circles1";
                    question.inputTypeAnswer = true;
                    int[] value2 = new int[] { 1, 1, 3 };
                    question.inputValues = value2;
                    break;
                case 23:
                    question.heading = "Express the point marked on the number line as a fraction";
                    question.textTypeQuestion = false;
                    question.imageName = "circles1";
                    question.inputTypeAnswer = true;
                    int[] value3 = new int[] { 3, 5, 6 };
                    question.inputValues = value3;
                    break;
                case 24:
                    question.heading = "Express the point marked on the number line as a fraction";
                    question.textTypeQuestion = false;
                    question.imageName = "circles1";
                    question.inputTypeAnswer = true;
                    int[] value4 = new int[] { 3, 1, 2 };
                    question.inputValues = value4;
                    break;
                case 25:
                    question.heading = "Express the point marked on the number line as a fraction";
                    question.textTypeQuestion = false;
                    question.imageName = "circles1";
                    question.inputTypeAnswer = true;
                    int[] value5 = new int[] { 9, 3, 4 };
                    question.inputValues = value5;
                    break;
                case 26:
                    if (OnGameOver != null)
                        OnGameOver();
                    break;
            }
        }
        
        if (OnLoadNextQuestion != null)
            OnLoadNextQuestion(question);
        QuestionNumber++;
    }
}
