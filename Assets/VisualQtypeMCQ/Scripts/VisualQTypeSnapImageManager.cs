using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualQTypeSnapImageManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static VisualQTypeSnapImageManager instance;

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

    public delegate void LoadNextQuestion(QTypeSnapImageQuestion question);
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
        QTypeSnapImageQuestion question = new QTypeSnapImageQuestion();

            switch (QuestionNumber)
            {
                case 0:
                    question.heading = "Representing unequally partitioned objects as fractions";
                    question.question = "Make the given shape have equal parts and express the shaded part as a fraction";
                    question.textTypeQuestion = true;
                    question.SnapImageObjectName = "SnapImage";
                    question.inputTypeAnswer = true;
                    int[] value = new int[] { 3, 4 };
                    question.inputValues = value;
                    break;
                case 1:
                    question.heading = "Representing unequally partitioned objects as fractions";
                    question.question = "Make the given shape have equal parts and express the shaded part as a fraction";
                    question.textTypeQuestion = true;
                    question.SnapImageObjectName = "SnapBox";
                    question.inputTypeAnswer = true;
                    int[] value1 = new int[] { 1, 8 };
                    question.inputValues = value1;
                    break;
                case 2:
                    if (OnGameOver != null)
                        OnGameOver();
                    break;
            }
           
       
        
        if (OnLoadNextQuestion != null)
            OnLoadNextQuestion(question);
        QuestionNumber++;
    }
}
