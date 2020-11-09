using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AplleObj7ROManager : MonoBehaviour
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
        //GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
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
            check_Lo1_ans();


        }

    }
    void check_Lo1_ans()
    {
        deselect_option();
        if (ans == "1")
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            //GameObject.FindObjectOfType<Obj4Manager>().UnHighLightAllBlocks();
            enableFade();
        }
        else
        {
            GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
            GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj7_let_see_why_common.wav");
            temp.GetComponent<Image>().color = Color.red;
            Invoke("ReInforse_LO1", 5);
            //ReInforse_LO1();      
        }

    }
    public void ReInforse_LO1()
    {
        Debug.Log("Enable for reinfo1");
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj7_Quest1_RO_Hint1.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("The numerator has to be read as a cardinal number - One, Two, Three");
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(6).GetComponent<TEXDraw>().text = "\\frac{\\clr[4]2}{5}";
       
        DisableSubmitButton();
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
            check_Lo2_ans1();


        }
    }
    void check_Lo2_ans1()
    {
        deselect_option();
        DisableSubmitButton();
        if (ans == "2")
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            enableFade();

        }

        else
        {
            if (ans == "4")
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj7_let_see_why_common.wav");
                temp.GetComponent<Image>().color = Color.red;
                Invoke("ReInforse1_LO2_ans1", 5);
                //ReInforse1_LO2_ans1();
            }
            else
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj7_let_see_why_common.wav");
                temp.GetComponent<Image>().color = Color.red;
                Invoke("ReInforse_LO2_ans1", 5);
                //ReInforse_LO2_ans1();
            }
        }

    }
    public void ReInforse_LO2_ans1()
    {
        Debug.Log("Enable for reinfo2");
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj7_Quest2_RO_Hint1.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("The denominator is read as ordinal numbers such as second, third, fourth,.. ");
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(6).GetComponent<TEXDraw>().text = "\\frac{2}{\\clr[4]5}";

    }
    public void ReInforse1_LO2_ans1()
    {
        Debug.Log("Enable for reinfo2.1");
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj7_Quest2_RO_Hint2.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("We consider the denominator as singular when the numerator is one and plural when the numerator is not one. Here the denominator is read as fifths");
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(6).GetComponent<TEXDraw>().text = "\\frac{2}{\\clr[4]5}";
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
        if (ans == "2")
        {
            
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            enableFade();
        }

        else
        {
            if (ans == "3")
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj7_let_see_why_common.wav");
                temp.GetComponent<Image>().color = Color.red;
                Invoke("ReInforse_LO2_ans2_1", 5);
                //ReInforse_LO2_ans2_1();
            }
            else
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj7_let_see_why_common.wav");
                temp.GetComponent<Image>().color = Color.red;
                Invoke("ReInforse_LO2_ans2", 5);
                //ReInforse_LO2_ans2();
            }
        }

    }
    public void ReInforse_LO2_ans2()
    {
        Debug.Log("Enable for reinfo2");
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj7_Quest3_RO_Hint1.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("The numerator is read as cardinal numbers and the denominator is read as ordinal numbers. Here the given fraction is read as Two-Fifths.");
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(6).GetComponent<TEXDraw>().text = "\\frac{2}{5} = Two-Fifths.";
        //Invoke("convesationDisable", 15.0f);

    }
    public void ReInforse_LO2_ans2_1()
    {
        Debug.Log("Enable for reinfo2");
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj7_Quest3_RO_Hint2.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("We consider the denominator as singular when the numerator is one and plural when the numerator is not one. Here the given fraction is read as Two-fifths.");

        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(6).GetComponent<TEXDraw>().text = "\\frac{2}{5} = Two-Fifths.";
        //Invoke("convesationDisable", 15.0f);

    }




    public void EnableSubmitButtonRO4()
    {
        Debug.Log("Enable for ro4");
        GetallNumberButtons();
        GameObject.Find("ROSubmitButton").transform.GetChild(0).GetComponent<Button>().onClick.RemoveListener(SubmitObj4Q3);
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(SubmitObj4Q4); ;
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(lastConvesationDisable);
    }
    public void SubmitObj4Q4()
    {
        if (ans != null)
        {
            ansList.Add(ans);
            check_Lo2_ans4();


        }
    }
    void check_Lo2_ans4()
    {
        deselect_option();
        DisableSubmitButton();
        if (ans == "3")
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
            Invoke("animStop", 2.5f);
        }

        else
        {
            if (ans == "4")
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj7_let_see_why_common.wav");
                temp.GetComponent<Image>().color = Color.red;
                Invoke("ReInforse_LO2_ans4_1", 5);
                //ReInforse_LO2_ans4_1();
            }
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj7_let_see_why_common.wav");
                temp.GetComponent<Image>().color = Color.red;
                Invoke("ReInforse_LO2_ans4", 5);
                //ReInforse_LO2_ans4();
            }
        }

    }
    public void ReInforse_LO2_ans4()
    {
        Debug.Log("Enable for reinfo2");
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj7_Quest4_RO_Hint1.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("\\frac{1}{4} is read as one - quarter or one fourth");

        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(6).GetComponent<TEXDraw>().text = "";
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(lastConvesationDisable);
    }
    public void ReInforse_LO2_ans4_1()
    {
        Debug.Log("Enable for reinfo2");
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj7_Quest4_RO_Hint2.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("The numerator is read as cardinal numbers and the denominator is read as ordinal numbers");

        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(6).GetComponent<TEXDraw>().text = "\\frac{1}{4}";
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(lastConvesationDisable);
    }









    void convesationDisable()
    {
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(5).gameObject.SetActive(false);
        //GameObject.Find("Dialouge").transform.GetChild(0).GetChild(6).gameObject.SetActive(false);
        GameObject.FindObjectOfType<conversationManager>().DisableDialouge();
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
        //GameObject.Find("Fade").GetComponent<Animator>().enabled = false;
        //GameObject.Find("ExitPanel").transform.GetChild(0).gameObject.SetActive(true);
        //GameObject.Find("ExitPanel").transform.GetChild(0).GetChild(0).GetComponent<Button>().onClick.AddListener(ExitNow);
        gameObject.GetComponent<GameManager>().OnGameOver();

    }
    void ExitNow()
    {
        Application.Quit();
    }
    void enableFade()
    {
        GameObject.FindObjectOfType<timeline_new>().load_next();
        //GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
        //Invoke("nextObjective1", 3.0f);
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
