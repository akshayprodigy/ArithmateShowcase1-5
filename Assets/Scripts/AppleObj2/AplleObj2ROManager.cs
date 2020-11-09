using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class AplleObj2ROManager : MonoBehaviour
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
    public void EnableSubmitButtonRO1()
    {
        Debug.Log("Enable for ro1");
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(Submit);
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
    public void Submit()
    {
        if (ans != null)
        {
            ansList.Add(ans);
            check_Lo1_ans();


        }

    }
    public void EnableSubmitButtonRO2()
    {
        Debug.Log("Enable for ro2");
        GetallNumberButtons();
        GameObject.Find("ROSubmitButton").transform.GetChild(0).GetComponent<Button>().onClick.RemoveListener(Submit);
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(SubmitObj1Q1); ;
    }
    
    public void SubmitObj1Q1()
    {
        if (ans != null)
        {
            ansList.Add(ans);
            check_Lo2_ans1();


        }
    }
    
    void check_Lo1_ans()
    {
        deselect_option();
        if (ans == "4")
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            temp.GetComponent<Image>().color = Color.green;
            GameObject.FindObjectOfType<timeline_new>().load_next();
        }
        else
        {
            GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
            GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj2_let_see_why_common.wav");
            temp.GetComponent<Image>().color = Color.red;
            Invoke("ReInforse_LO1", 5);
           
           
        }

    }
    void check_Lo2_ans1()
    {
        if (ans == "1")
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            temp.GetComponent<Image>().color = Color.green;
            GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
            Invoke("animStop", 2.5f);
        }

        else
        {
            GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
            ReInforse_LO2_ans1();
        }

    }
   

    public void ReInforse_LO1()
    {
        Debug.Log("Enable for reinfo1");
        //GameObject.Find("ROType1").transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj2_RO_quest1_reonfo1.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("Division is splitting a given amount equally. Similarly, fractions is splitting a single object into equal parts. Hence fractions and the operation division is similar to each other.");
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();

        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(ok);//GameObject.Find("ROType1").transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
        DisableSubmitButton();
    }
    public void ReInforse_LO2_ans1()
    {
        Debug.Log("Enable for reinfo2");
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj2_RO_quest1_reonfo1.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("Division is splitting a given amount equally. Similarly, fractions is splitting a single object into equal parts");

        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(ok1);
        DisableSubmitButton();
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
        GameObject.FindObjectOfType<timeline_new>().stopAudio();
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
        //GameObject.Find("GameManager").GetComponent<GameManager>().OnGameOver();
        ExitNow();
        //GameObject.Find("ExitPanel").transform.GetChild(0).gameObject.SetActive(true);
        //GameObject.Find("ExitPanel").transform.GetChild(0).GetChild(0).GetComponent<Button>().onClick.AddListener(ExitNow);
    }
    void ExitNow()
    {
        //Application.Quit();
        GameObject.FindObjectOfType<Obj2Manager>().VisualQType_FDTemplate.SetActive(true);


        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadeout");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    void enableFade()
    {
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
        Invoke("ExitNow", 3.0f);
    }
    void deselect_option()
    {
        foreach (GameObject b in GameObject.FindGameObjectsWithTag("Numbers"))
        {
            b.transform.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        }

    }
}
