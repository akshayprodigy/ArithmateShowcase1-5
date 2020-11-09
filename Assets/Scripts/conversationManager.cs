using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class conversationManager : MonoBehaviour
{
    public string conversationText;

    // Start is called before the first frame update

    public void GetHintOkButton()
    {

        GameObject.Find("Hint").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(DisableHint);
    }
    public void EnableQuestion(string conversationText)
    {
        GameObject ques = GameObject.Find("Question").transform.GetChild(0).gameObject;
        ques.SetActive(true);
        ques.GetComponentInChildren<TEXDraw>().text = conversationText;
        //GameObject.Find("Question").transform.GetChildFromName<TEXDraw>("QuestionText").text = conversationText; //GetComponentInChildren<TEXDraw>().text = conversationText;
        //GameObject.Find("QuestionText").GetComponent<TEXDraw>().text = conversationText;
        //DisableConversation();
    }
    public void DisableQuestion()
    {
        GameObject ques = GameObject.Find("Question").transform.GetChild(0).gameObject;
       
        ques.GetComponentInChildren<TEXDraw>().text = "";
        ques.SetActive(false);
        // GameObject.Find("QuestionText").GetComponent<TEXDraw>().text = "";


    }

    public void EnableQuestion1(string conversationText)
    {
        GameObject.Find("Question").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("QuestionText").GetComponent<TEXDraw>().text = conversationText;
        //DisableConversation();
    }
    public void DisableQuestion1()
    {
        GameObject.Find("Question").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("QuestionText").GetComponent<TEXDraw>().text = "";
        GameObject.Find("Question").transform.GetChild(0).gameObject.SetActive(false);


    }

    public void EnableConversation(string conversationText)
    {
        GameObject.Find("Chef conversation").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("ConversationText").GetComponent<TEXDraw>().text = conversationText;
        //DisableConversation();
    }
    public void DisableConversation()
    {
        GameObject.Find("Chef conversation").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("ConversationText").GetComponent<TEXDraw>().text = "";
        GameObject.Find("Chef conversation").transform.GetChild(0).gameObject.SetActive(false);
       

    }
    public void EnableROQuestion(string conversationText)
    {
        
        GameObject.Find("ROQuestion").GetComponent<TEXDraw>().text = conversationText;
        //DisableConversation();
    }
    public void DisableROQuestion()
    {
        GameObject.Find("ROQuestion").GetComponent<TEXDraw>().text = "";
        
    }
    public void EnableDialouge(string conversationText)
    {
        GameObject.Find("Dialouge").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("DialougeText").GetComponent<TEXDraw>().text = conversationText;
        //DisableConversation();
    }
    public void DisableDialouge()
    {
        
        GameObject.Find("Dialouge").transform.GetChild(0).gameObject.SetActive(false);
        

    }
    public void EnableHint(string hintText)
    {
        GameObject.Find("Hint").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("HintText").GetComponent<TEXDraw>().text = hintText;
        //DisableConversation();
    }
    public void DisableHint()
    {

        GameObject.Find("Hint").transform.GetChild(0).gameObject.SetActive(false);


    }
    public void DisableAll()
    {
        GameObject.Find("Chef conversation").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("Question").transform.GetChild(0).gameObject.SetActive(false);
        DisableDialouge();
    }

    public void playError()
    {
        GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
    }
    public void playCorrect()
    {
        GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
    }
}
