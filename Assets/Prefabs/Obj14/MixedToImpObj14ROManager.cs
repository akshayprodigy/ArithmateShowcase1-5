using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MixedToImpObj14ROManager : MonoBehaviour
{
    public string ans;
    public List<string> ansList = new List<string>();
    void OnEnable()
    {
        //enableRO();
        Initiliaze();
    }
    public void Initiliaze()
    {
        enableRO();
        EnableSubmitButtonRO1();
        GetallNumberButtons();

    }
    void enableRO()
    {
        GameObject.Find("RO Panel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        GameObject.Find("Dialouge").transform.GetChild(0).gameObject.SetActive(false);
        

    }
    public void DisableSubmitButton()
    {

        GameObject.Find("SubmitButton").transform.GetChild(0).gameObject.SetActive(false);
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


    }

    public void EnableSubmitButtonRO1()
    {
        GameObject.Find("SubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(Submit);
        //GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(enableRO2);
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
        if (ans == "2")
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            enableRO2();
            //GameObject.FindObjectOfType<timeline_new>().load_next();
        }
        else
        {
            
                ReInforse_RO1_1();
           
            

        }

    }

    public void ReInforse_RO1_1()
    {
        GameObject.Find("SubmitButton").SetActive(false);
        //GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_14_RO1_Reinfo.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("When we convert mixed to improper fraction , we multiply the denominator and the whole number of the mixed fraction to make the whole number have the same number of partitions as the fraction has.");
    }
    
    void enableRO2()
    {
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        GameObject.Find("Dialouge").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        EnableSubmitButtonRO2();
    }

    public void EnableSubmitButtonRO2()
    {
        GetallNumberButtons();
        GameObject.Find("SubmitButton").transform.GetChild(0).GetComponent<Button>().onClick.RemoveListener(Submit);
        GameObject.Find("SubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(SubmitObj14RO2); ;
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(enableRO3);
    }
    public void SubmitObj14RO2()
    {
        if (ans != null)
        {
            ansList.Add(ans);
            check_RO2_ans();


        }
    }
    void check_RO2_ans()
    {
        if (ans == "2")
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            //GameObject.FindObjectOfType<timeline_new>().load_next();
            enableRO3();
        }

        else
        {
            GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
            ReInforse_RO2();
        }

    }
    public void ReInforse_RO2()
    {
        GameObject.Find("SubmitButton").SetActive(false);
        //GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_14_RO2_Reinfo.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("The denominator in the given mixed fraction and the resulting improper fraction does not change because the denominator indicates the number of parts and conversion of mixed to improper fraction does not change the number of parts. This only changes the way a fraction is expressed.");
    }
    void enableRO3()
    {
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        GameObject.Find("Dialouge").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        EnableSubmitButtonRO3();
    }


    public void EnableSubmitButtonRO3()
    {
        GetallNumberButtons();
        GameObject.Find("SubmitButton").transform.GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("SubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(SubmitObj14RO3); ;
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(enableRO4);
    }
    public void SubmitObj14RO3()
    {
        if (ans != null)
        {
            ansList.Add(ans);
            check_RO3_ans();


        }
    }
    void check_RO3_ans()
    {
        if (ans == "1")
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            //GameObject.FindObjectOfType<timeline_new>().load_next();
            enableRO4();
        }

        else
        {
            GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
            ReInforse_RO3();
        }

    }
    public void ReInforse_RO3()
    {
        GameObject.Find("SubmitButton").SetActive(false);
        //GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_14_RO3_Reinfo.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("When we convert mixed to improper fraction, we multiply the denominator and the whole number of the mixed fraction to make the whole number have the same number of partitions as the fraction has and add the result to the numerator of the mixed fraction.");
    }
    void enableRO4()
    {
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
        GameObject.Find("Dialouge").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(3).gameObject.SetActive(true);
        EnableSubmitButtonRO4();
    }



    public void EnableSubmitButtonRO4()
    {
        GetallNumberButtons();
        GameObject.Find("SubmitButton").transform.GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("SubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(SubmitObj14RO4); ;
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(enableRO5);
    }
    public void SubmitObj14RO4()
    {
        if (ans != null)
        {
            ansList.Add(ans);
            check_RO4_ans();


        }
    }
    void check_RO4_ans()
    {
        if (ans == "2")
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            //GameObject.FindObjectOfType<timeline_new>().load_next();
            enableRO5();
        }

        else
        {
            GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
            ReInforse_RO4();
        }

    }
    public void ReInforse_RO4()
    {
        GameObject.Find("SubmitButton").SetActive(false);
        //GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_14_RO4_Reinfo.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("To add the result of multiplication of the whole number and denominator to the numerator of the mixed fractions means to add the number of parts in the whole number part to the number of parts in fraction part");
    }
    void enableRO5()
    {
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
        GameObject.Find("Dialouge").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(4).gameObject.SetActive(true);
        EnableSubmitButtonRO5();
    }



    public void EnableSubmitButtonRO5()
    {
        GetallNumberButtons();
        GameObject.Find("SubmitButton").transform.GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("SubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(SubmitObj14RO5); ;
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(enableRO6);
    }
    public void SubmitObj14RO5()
    {
        if (ans != null)
        {
            ansList.Add(ans);
            check_RO5_ans();


        }
    }
    void check_RO5_ans()
    {
        if (ans == "1")
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            //GameObject.FindObjectOfType<timeline_new>().load_next();
            enableRO6();
        }

        else
        {
            GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
            ReInforse_RO5();
        }

    }
    public void ReInforse_RO5()
    {
        //GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_14_RO4_Reinfo.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("First we, multiply the denominator of the Mixed fraction with its whole number and then add the result to the numerator. This will give you the numerator of the Improper Fraction.Keep the denominator for the improper fraction same as the Mixed Fraction.The correct conversion of mixed to improper fraction of   2\\frac{1}{3} is \\frac{[(3x2)+1]}{7}");
    }
    void enableRO6()
    {
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
        GameObject.Find("Dialouge").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
        Invoke("animStop", 2.5f);
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

        GameObject.Find("ExitPanel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("ExitPanel").transform.GetChild(0).GetChild(0).GetComponent<Button>().onClick.AddListener(ExitNow);
    }
    void ExitNow()
    {
        Application.Quit();
    }
}
