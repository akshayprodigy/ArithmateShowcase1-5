using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MixedObj13ROManager : MonoBehaviour
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
       
    }
   
    
    public void DisableSubmitButton()
    {

        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.SetActive(false);
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

    public void EnableSubmitButtonRO1()
    {
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(Submit);
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(ok);
    }
    public void Submit()
    {
        if (ans != null)
        {
            ansList.Add(ans);
            check_RO1_ans();


        }

    }
    void check_RO1_ans()
    {
        deselect_option();
        if (ans == "6")
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();

            GameObject.FindObjectOfType<timeline_new>().load_next();
        }
        else
        {
            if (ans == "1" || ans == "3")
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj13_let_see_why_common.wav");
                temp.GetComponent<Image>().color = Color.red;
                Invoke("ReInforse_RO1_1", 5);
                //ReInforse_RO1_1();
            }
            else if (ans == "2")
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj13_let_see_why_common.wav");
                temp.GetComponent<Image>().color = Color.red;
                Invoke("ReInforse_RO1_2", 5);
                //ReInforse_RO1_2();
            }
            else if (ans == "4")
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj13_let_see_why_common.wav");
                temp.GetComponent<Image>().color = Color.red;
                Invoke("ReInforse_RO1_3", 5);
                //ReInforse_RO1_3();
            }
            else if (ans == "5" || ans == "7")
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj13_let_see_why_common.wav");
                temp.GetComponent<Image>().color = Color.red;
                Invoke("ReInforse_RO1_4", 5);
                //ReInforse_RO1_4();
            }

        }

    }

    public void ReInforse_RO1_1()
    {
        GameObject.Find("ROSubmitButton").SetActive(false);
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_13_RO1_Reinfo_1.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("It is easy to tell quantity in terms of mixed fraction than improper fractions as mixed fractions are easy to understand");
    }
    public void ReInforse_RO1_2()
    {
        GameObject.Find("ROSubmitButton").SetActive(false);
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_13_RO1_Reinfo_2.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("Any mixed fraction can be used in daily life to tell quantity. So 3 \\frac{1}{4} kgs of sugar too, can be used during shopping for groceries.");
    }
    public void ReInforse_RO1_3()
    {
        GameObject.Find("ROSubmitButton").SetActive(false);
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_13_RO1_Reinfo_3.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("Any mixed fraction can be used in daily life to tell quantity. So 1 \\frac{1}{2} litres of milk too can be used during shopping for groceries. ");
    }
    public void ReInforse_RO1_4()
    {
        GameObject.Find("ROSubmitButton").SetActive(false);
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_13_RO1_Reinfo_4.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("It is easy to tell quantity in terms of mixed fractions than improper fractions as mixed fractions are easy to understand");
    }


    public void EnableSubmitButtonRO2()
    {
        GetallNumberButtons();
        GameObject.Find("ROSubmitButton").transform.GetChild(0).GetComponent<Button>().onClick.RemoveListener(Submit);
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(SubmitObj13RO2); ;
    }
    public void SubmitObj13RO2()
    {
        if (ans != null)
        {
            ansList.Add(ans);
            check_RO2_ans();


        }
    }
    void check_RO2_ans()
    {
        deselect_option();
        if (ans == "1")
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            GameObject.FindObjectOfType<timeline_new>().load_next();
        }

        else
        {
            GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
            GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj13_let_see_why_common.wav");
            temp.GetComponent<Image>().color = Color.red;
            Invoke("ReInforse_RO2", 5);
            //ReInforse_RO2();
        }

    }
    public void ReInforse_RO2()
    {
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_13_RO2_Reinfo.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("You have to combine the four \\frac{1}{4}s to say that there is one whole in 5/4 where as just by looking at 1 \\frac{1}{4}, you can say that there is one whole in 1 \\frac{1}{4}.");
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(ok);
    }


    public void EnableSubmitButtonRO3()
    {
        GetallNumberButtons();
        GameObject.Find("ROSubmitButton").transform.GetChild(0).GetComponent<Button>().onClick.RemoveListener(EnableSubmitButtonRO2);
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(SubmitObj13RO3); ;
    }
    public void SubmitObj13RO3()
    {
        if (ans != null)
        {
            ansList.Add(ans);
            check_RO3_ans();


        }
    }
    void check_RO3_ans()
    {
        deselect_option();
        if (ans == "2")
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            GameObject.FindObjectOfType<timeline_new>().load_next();
        }

        else
        {
            if (ans == "4")
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj13_let_see_why_common.wav");
                temp.GetComponent<Image>().color = Color.red;
                Invoke("ReInforse_RO3_1", 5);
                //ReInforse_RO3_1();
            }
            else
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj13_let_see_why_common.wav");
                temp.GetComponent<Image>().color = Color.red;
                Invoke("ReInforse_RO3", 5);
                //ReInforse_RO3();
            }
        }

    }
    public void ReInforse_RO3()
    {
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_13_RO3_Reinfo.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("To convert improper to mixed fractions, we divide the numerator by the denominator of the fraction. Multiplying the numerator and the denominator will not yield a mixed fraction, but a whole number. ");
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(ok);
    }
    public void ReInforse_RO3_1()
    {
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_13_RO3_Reinfo_1.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("divide the numerator by the denominator of the fraction and not the other way around. Multiplying the numerator and the denominator will not yield a mixed fraction, but a whole number.  ");
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(ok);
    }


    public void EnableSubmitButtonRO4()
    {
        GetallNumberButtons();
        GameObject.Find("ROSubmitButton").transform.GetChild(0).GetComponent<Button>().onClick.RemoveListener(EnableSubmitButtonRO3);
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(SubmitObj13RO4); ;
    }
    public void SubmitObj13RO4()
    {
        if (ans != null)
        {
            ansList.Add(ans);
            check_RO4_ans();


        }
    }
    void check_RO4_ans()
    {
        deselect_option();
        if (ans == "1")
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
            Invoke("animStop", 2.5f);
        }

        else
        {
            GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
            GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj13_let_see_why_common.wav");
            temp.GetComponent<Image>().color = Color.red;
            Invoke("ReInforse_RO4", 5);
            //ReInforse_RO4();
        }

    }
    public void ReInforse_RO4()
    {
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_13_RO4_Reinfo.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("The numerator has to be divided by the denominator of the improper fraction to arrive at the correct mixed fraction. ");
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(3).gameObject.SetActive(true);
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(ok1);
    }





    void next_line()
    {
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("fractions are used to count objects which are less than a whole.");
    }
    
   
    void ok()
    {
        CancelInvoke();
        convesationDisable();
    }
    void ok1()
    {
        CancelInvoke();
        lastConvesationDisable();
    }
    void convesationDisable()
    {
        GameObject.FindObjectOfType<conversationManager>().DisableDialouge();
        GameObject.FindObjectOfType<timeline_new>().load_next();
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
    void deselect_option()
    {
        foreach (GameObject b in GameObject.FindGameObjectsWithTag("Numbers"))
        {
            b.transform.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        }

    }
}
