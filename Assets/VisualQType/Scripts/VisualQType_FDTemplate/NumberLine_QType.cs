using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

[System.Serializable]
public class NumberLine
{
    public int questionId;
    public string question;
    public Sprite showLineSprite;
    public GameObject lineEvent;
}

public class NumberLine_QType : MonoBehaviour
{
    public Text Question;
    public Image questionSprite_Img;
    public int questionNum;
    public GameObject hintPopup_Pnl;
    public bool isNext;

    public NumberLine[] _numberLine;


    // Start is called before the first frame update
    void Start()
    {
        questionNum = 0;
        Question.text = _numberLine[questionNum].question;
        questionSprite_Img.sprite = _numberLine[questionNum].showLineSprite;

        for (int i = 0; i< _numberLine.Length; i++)
        {
            _numberLine[i].lineEvent.SetActive(false);

        }
        _numberLine[questionNum].lineEvent.SetActive(true);

    }

    public void OnSubmitBtn()
    {
        if (questionNum < 4)
        {

            if (isNext)
            {
                questionNum++;

                for (int i = 0; i < _numberLine.Length; i++)
                {
                    _numberLine[i].lineEvent.SetActive(false);

                }

                Question.text = _numberLine[questionNum].question;
                questionSprite_Img.sprite = _numberLine[questionNum].showLineSprite;
                _numberLine[questionNum].lineEvent.SetActive(true);
                isNext = false;

            }
            else
            {
                ShowHintPopup();
            }
            
        }
        else
        {
            this.gameObject.SetActive(false);
        }

    }

    public void OnLineEvent(int id)
    {
        if(id == 1)
        {
            isNext = true;
        }
        else if(id == 0)
        {
            isNext = false;

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
}
