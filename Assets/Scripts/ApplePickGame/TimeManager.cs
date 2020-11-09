using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public float timeStart = 30;
    public bool clickOnApple, clickOnTree;
    void Start()
    {
        Initialization();
    }
    public void Initialization()
    {
        EnableTimer();
    }
     void EnableTimer()
    {
        timeStart = 30;
        this.gameObject.transform.GetChild(0).GetComponent<Text>().text = timeStart.ToString();
        //Invoke("checkClicked", 5.0f);
        clickOnApple = false; clickOnTree=false;
    }
    // Update is called once per frame
    void Update()
    {
       if( GameObject.Find("GameManager").GetComponent<GameManager>().IsGameOn);
        {
            timeStart -= Time.deltaTime;
            this.gameObject.transform.GetChild(0).GetComponent<Text>().text = Mathf.Round(timeStart).ToString();
            if (timeStart < 0f)
            {
                timeStart = 0f;
                GameObject.Find("GameManager").GetComponent<GameManager>().IsGameOn = false;
                
                //GameObject.Find("Guide").GetComponent<IsometricPlayerMovementControllerFair>().enabled = false;
                GameObject.Find("WitchRender").GetComponent<Animator>().enabled = false;

                //AppleManager.total();
                if (!clickOnApple)
                {
                    GameObject.FindObjectOfType<conversationManager>().EnableDialouge("You didn't pick up any apple.Let's retry");
                    GameObject.Find("Dialouge").transform.GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(true);
                    GameObject.Find("Dialouge").transform.GetChild(0).GetChild(0).GetChild(2).gameObject.SetActive(false);
                    clickOnApple = true;
                  
                }
                else
                {
                    GameObject.FindObjectOfType<conversationManager>().EnableDialouge("Well Done");
                    GameObject.Find("Dialouge").transform.GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(true);
                    GameObject.Find("Dialouge").transform.GetChild(0).GetChild(0).GetChild(2).gameObject.SetActive(true);
                }
                GameObject.FindObjectOfType<conversationManager>().DisableQuestion1();
                this.gameObject.SetActive(false);
                //this.GetComponent<TimeManager>().enabled = false;

            }
        }
        
       
    }
    void disable()
    {
        GameObject.FindObjectOfType<conversationManager>().DisableHint();
    }
   
    void checkClicked()
    {
        if (!clickOnApple)
        {
             if (!clickOnTree && !clickOnApple)
                GameObject.FindObjectOfType<conversationManager>().EnableHint("Tap on any tree to shake it");
            else if (clickOnTree && !clickOnApple)
                GameObject.FindObjectOfType<conversationManager>().EnableHint("Tap on the apples on the ground to collect them");
            Invoke("disablehint", 2.0f);
        }
    }
    void disablehint()
    {
        Invoke("checkClicked", 5.0f);
        GameObject.FindObjectOfType<conversationManager>().DisableHint();

    }
    void checkTap()
    {
            if (!clickOnTree && !clickOnApple)
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Tap on any tree to shake it");
            else if (clickOnTree && !clickOnApple)
            GameObject.FindObjectOfType<conversationManager>().EnableConversation("Tap on the apples on the ground to collect them");
           
    }
}

    

