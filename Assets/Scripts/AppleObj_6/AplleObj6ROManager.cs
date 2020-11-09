using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AplleObj6ROManager : MonoBehaviour
{
    public string ans;
    public List<string> ansList = new List<string>();
    public GameObject temp;
    void OnEnable()
    {

    }
    public void Initiliaze()
    {
        EnableSubmitButtonRO1();
        GetallNumberButtons();
        GetOkButton();
        


    }
    public void GetallNumberButtons()
    {
        foreach (GameObject b in GameObject.FindGameObjectsWithTag("Numbers"))
        {
            b.GetComponent<Button>().onClick.AddListener(InputNumbers);
        }
    }
    public void InputNumbers()
    {
        var currentEventSystem = EventSystem.current;
        var currentSelectedGameObject = currentEventSystem.currentSelectedGameObject;

        Debug.Log("numerator =" + currentSelectedGameObject.name);
        ans = currentSelectedGameObject.name;
        foreach (GameObject b in GameObject.FindGameObjectsWithTag("Numbers"))
        {
            b.transform.GetChild(2).gameObject.SetActive(false);
        }
        currentSelectedGameObject.transform.GetChild(2).gameObject.SetActive(true);
        temp = currentSelectedGameObject.transform.GetChild(2).gameObject;

    }
    public void DisableSubmitButton()
    {

        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.SetActive(false);
    }
    public void GetOkButton()
    {
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(Done);
    }
    


    public void Done()
    {
        GameObject.FindObjectOfType<timeline_new>().stopAudio();
        //GameObject.FindObjectOfType<Obj4Manager>().UnHighLightAllBlocks();
        CancelInvoke();
        convesationDisable();

    }
    public void EnableSubmitButtonRO1()
    {
        Debug.Log("Enable for ro1");
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(Submit);
    }
    public void Submit()
    {
        if (ans != null)
        {
            ansList.Add(ans);
           
            check_Lo2_ans1();

        }

    }
    void check_Lo2_ans1()
    {
        deselect_option();
        DisableSubmitButton();
        if (ans == "3")
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            enableFade();

        }

        else
        {
            GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
            GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj6_let_see_why_common.wav");
            temp.GetComponent<Image>().color = Color.red;
            Invoke("ReInforse_LO1", 5);
            //ReInforse_LO1();
        }

    }   
    public void ReInforse_LO1()
    {
        Debug.Log("Enable for reinfo1");

        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj6_Quest1_RO_Hint1.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("To denote a fraction on a number line, the number line has to be divided into parts. The number of parts in a number line is determined by the denominator. If a fraction is \\frac{2}{15}, to plot it on a number line, the number line has to be divided into 15 equal parts.");
        Invoke("ReInforse_LO1_1", 12.0f);

    }
    public void ReInforse_LO1_1()
    {
        Debug.Log("Enable for reinfo1_1");

        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj6_Quest1_RO_Hint1_1.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("To denote a fraction on a number line, the number line has to be divided into parts. The number of parts in a number line is determined by the denominator. If a fraction is \\frac{2}{15}, to plot it on a number line, the number line has to be divided into 15 equal parts.");
        


    }
    
    


    public void EnableSubmitButtonRO2()
    {
        Debug.Log("Enable for ro2");
        GetallNumberButtons();
        GameObject.Find("ROSubmitButton").transform.GetChild(0).GetComponent<Button>().onClick.RemoveListener(Submit);
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(SubmitObj4Q2); ;
    }
    public void SubmitObj4Q2()
    {
        if (ans != null)
        {
            ansList.Add(ans);
            check_Lo1_ans();


        }
    }
    void check_Lo1_ans()
    {
        deselect_option();
        DisableSubmitButton();
        if (ans == "5")
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();

            enableFade();


        }
        else
        {
            if (ans == "1" || ans == "2")
            {
                {
                    GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                    GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj6_let_see_why_common.wav");
                    temp.GetComponent<Image>().color = Color.red;
                    Invoke("ReInforse_LO2", 5);
                    //ReInforse_LO2();
                }
            }
            else
            {
                {
                    GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                    GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj6_let_see_why_common.wav");
                    temp.GetComponent<Image>().color = Color.red;
                    Invoke("ReInforse_LO2_1", 5);
                    //ReInforse_LO2_1();
                }
            }
        }

    }
    public void ReInforse_LO2()
    {
        Debug.Log("Enable for reinfo1");
        //GameObject.Find("Dialouge").transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        FindObjectOfType<Obj6Manager>().ro2_line.SetActive(true);
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj6_Quest2_RO_Hint1.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("\n\nThe number line has to be drawn between 0 and 1 as value of an object less than a whole is less than 1.");


    }
    public void ReInforse_LO2_1()
    {
        Debug.Log("Enable for reinfo1");
        //GameObject.Find("Dialouge").transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        FindObjectOfType<Obj6Manager>().ro2_line.SetActive(true);
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj6_Quest2_RO_Hint2.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("\n\n\n\nThe number line has to be drawn between 0 and 1 as value of an object less than a whole is less than 1.\n 0 to 1 is same as 0 to \\frac{2}{2} as \n\\frac{2}{2} = 1");


    }


    



    public void EnableSubmitButtonRO3()
    {
        Debug.Log("Enable for ro2");
        GetallNumberButtons();
        GameObject.Find("ROSubmitButton").transform.GetChild(0).GetComponent<Button>().onClick.RemoveListener(SubmitObj4Q2);
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(SubmitObj4Q3); ;
    }
    public void SubmitObj4Q3()
    {
        if (ans != null)
        {
            ansList.Add(ans);
            check_Lo2_ans2();


        }
    }
    void check_Lo2_ans2()
    {
        deselect_option();
        DisableSubmitButton();
        if (ans == "1")
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
            Invoke("animStop", 2.5f);
        }

        else
        {
            GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
            GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj6_let_see_why_common.wav");
            temp.GetComponent<Image>().color = Color.red;
            Invoke("ReInforse_LO2_ans2", 5);
            //ReInforse_LO2_ans2();
        }

    }
    public void ReInforse_LO2_ans2()
    {
        Debug.Log("Enable for reinfo2");
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj6_Quest3_RO_Hint1.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("All parts in a number line should be equal and the number of parts should be equal to the denominator.");
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(lastConvesationDisable);
    }


    









    void convesationDisable()
    {
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(5).gameObject.SetActive(false);
        GameObject.FindObjectOfType<conversationManager>().DisableDialouge();
        FindObjectOfType<Obj6Manager>().ro2_line.SetActive(false);
        enableFade();
    }
    void lastConvesationDisable()
    {
        GameObject.FindObjectOfType<conversationManager>().DisableDialouge();
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
        Invoke("animStop", 2.5f);
    }
    public void DisableAllTicks()
    {
        foreach (GameObject b in GameObject.FindGameObjectsWithTag("Numbers"))
        {
            b.transform.GetChild(2).gameObject.SetActive(false);
        }
    }
    void animStop()
    {
        GameObject.FindObjectOfType<GameManager>().OnGameOver();
        //GameObject.Find("ExitPanel").transform.GetChild(0).gameObject.SetActive(true);
        //GameObject.Find("ExitPanel").transform.GetChild(0).GetChild(0).GetComponent<Button>().onClick.AddListener(ExitNow);
    }
    void ExitNow()
    {
        Application.Quit();
    }
    void enableFade()
    {
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
        FindObjectOfType<Obj6Manager>().ro2_line.SetActive(false);
        Invoke("nextObjective1", 3.0f);
    }
    void nextObjective1()
    {
        GameObject enabledPanel = GameObject.FindGameObjectWithTag("RO Panel");
        enabledPanel.SetActive(false);
        enabledPanel.transform.parent.gameObject.SetActive(false);
        
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadeout");
        GameObject.FindObjectOfType<timeline_new>().load_next();

    }
    void deselect_option()
    {
        foreach (GameObject b in GameObject.FindGameObjectsWithTag("Numbers"))
        {
            b.transform.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        }

    }

}
