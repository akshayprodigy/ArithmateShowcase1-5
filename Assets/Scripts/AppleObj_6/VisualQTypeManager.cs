using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualQTypeManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static VisualQTypeManager instance;

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




    // event delegate
    public delegate void LoadNextQuestion(string question);
    public static event LoadNextQuestion OnLoadNextQuestion;
    public delegate void GameOver();
    public static event GameOver OnGameOver;

    int QuestionNumber = 0,numerator =0,denominator =0;
    public bool canGotoNextQuestion = false;
    void Start()
    {
        Invoke("NextQuestion", 0.5f);
        //NextQuestion();
    }

    public void NextQuestion()
    {
        string question = "";
        canGotoNextQuestion = false;
        switch (QuestionNumber)
        {
            case 0:
                question = "4/5";
                numerator = 4;
                denominator = 5;
                break;
            case 1:
                question = "7/10";
                numerator = 7;
                denominator = 10;
                break;
            case 2:
                question = "2/7";
                numerator = 2;
                denominator = 7;
                break;
            case 3:
                question = "3/5";
                numerator = 3;
                denominator = 5;
                break;
            case 4:
                question = "5/6";
                numerator = 5;
                denominator = 6;
                break;
            case 5:
                if (OnGameOver != null)
                    OnGameOver();
                break;
        }
        if (OnLoadNextQuestion != null)
            OnLoadNextQuestion(question);
        QuestionNumber++;
    }

    public bool checkNumerator(int msg)
    {
        if (msg == numerator)
        {

            canGotoNextQuestion = true;
            return true;
        }
        else
        {
            canGotoNextQuestion = false;
            return false;
        }
    } 

    public bool checkdenominator(int msg)
    {
        if (msg == denominator)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
