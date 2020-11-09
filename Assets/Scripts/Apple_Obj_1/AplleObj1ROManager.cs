using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class AplleObj1ROManager : MonoBehaviour
{
    public string ans;
    public List<string> ansList = new List<string>();
    public GameObject temp;

    void OnEnable()
    {
       
    }
   public void Initiliaze()
    {
        UtilityArtifacts.scene_to_load_after_canvas = "";
        EnableSubmitButtonRO1();
            GetallNumberButtons();
    }
    public void EnableSubmitButtonRO1()
    {
        GameObject.Find("SubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(Submit);
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(ok);
    }
    public void DisableSubmitButton()
    {
        
        GameObject.Find("SubmitButton").transform.GetChild(0).gameObject.SetActive(false);
    }
    void RemoveallNumberButtons()
    {
        foreach (GameObject b in GameObject.FindGameObjectsWithTag("Numbers"))
        {
            b.GetComponent<Button>().onClick.RemoveAllListeners();
        }
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
        temp.GetComponent<Image>().color = new Color32(64, 161, 251, 255);

    }
   public void Submit()
    {
        if(ans!= null)
        {
            ansList.Add(ans);
            check_Lo1_ans();
            DisableSubmitButton();

        }
        
    }
    public void EnableSubmitButtonRO2()
    {
        GetallNumberButtons();
        GameObject.Find("SubmitButton").transform.GetChild(0).GetComponent<Button>().onClick.RemoveListener(Submit);
        GameObject.Find("SubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(SubmitObj1Q1); ;
    }
    public void EnableSubmitButtonRO3()
    {
        DisableAllTicks();
        RemoveallNumberButtons();
        GetallNumberButtons();
        GameObject.Find("SubmitButton").transform.GetChild(0).GetComponent<Button>().onClick.RemoveListener(SubmitObj1Q1);
        
        GameObject.Find("SubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(SubmitObj1Q2); ;
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(ok1);
    }
    public void SubmitObj1Q1()
   {
        if (ans != null)
        {
            ansList.Add(ans);
            check_Lo2_ans1();
            DisableSubmitButton();

        }
    }
    public void SubmitObj1Q2()
    {
        if (ans != null)
        {
            ansList.Add(ans);
            check_Lo2_ans2();
            DisableSubmitButton();
        }
    }
    void check_Lo1_ans()
    {
        deselect_option();
        if (ans == "5")
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            temp.GetComponent<Image>().color = Color.green;
            GameObject.FindObjectOfType<timeline_new>().load_next();
        }
        else
        {
            GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
            GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj1_let_see_why_common.wav");
            Invoke("ReInforse_LO1", 5);
            temp.GetComponent<Image>().color = Color.red;
        }
            
    }
    void check_Lo2_ans1()
    {
        deselect_option();
        if (ans == "3")
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            temp.GetComponent<Image>().color = Color.green;
            GameObject.FindObjectOfType<timeline_new>().load_next();
        }
            
        else
        {
            GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
            GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj1_let_see_why_common.wav");
            Invoke("ReInforse_LO2_ans1", 5);
            temp.GetComponent<Image>().color = Color.red;
            
        }
            
    }
    void check_Lo2_ans2()
    {
        deselect_option();
        if (ans == "3")
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            temp.GetComponent<Image>().color = Color.green;
            GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
            Invoke("animStop", 2.5f);
        }
           
        else
        {
            GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
            GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj1_let_see_why_common.wav");
            Invoke("ReInforse_LO2_ans2", 5);
            temp.GetComponent<Image>().color = Color.red;
           
        }
            
    }
    
   public void ReInforse_LO1()
    {
        GameObject.Find("ROType1").transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj1_Lo1_Reinfo1.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("Whole numbers are used to count objects which are whole and \nfractions are used to count objects which are less than a whole");
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(3).gameObject.SetActive(true);
        DisableSubmitButton();
        //Invoke("next_line", 5.0f);
        //GameObject.Find("ROType1").transform.GetChild(2).GetChild(0).gameObject.SetActive(true);

    }
    void next_line()
    {
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("fractions are used to count objects which are less than a whole.");
    }
   public void ReInforse_LO2_ans1()
    {
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj1_Lo2_Reinfo1.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("Fractions are written as 2 numbers separated by a line. For example");
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(7).gameObject.SetActive(true);
        DisableSubmitButton();


    }
  public void ReInforse_LO2_ans2()
    {
       
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj1_Lo2_Reinfo2.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("The line separating the numbers in a fraction is called the Vinculum.");
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(6).gameObject.SetActive(true);
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
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(7).gameObject.SetActive(false);
        GameObject.FindObjectOfType<conversationManager>().DisableDialouge();
        GameObject.FindObjectOfType<timeline_new>().load_next();
    }
    void lastConvesationDisable()
    {
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(6).gameObject.SetActive(false);
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
        //gameObject.GetComponent<GameManager>().OnGameOver();
        ExitNow();
        //GameObject.Find("ExitPanel").transform.GetChild(0).gameObject.SetActive(true);
        //GameObject.Find("ExitPanel").transform.GetChild(0).GetChild(0).GetComponent<Button>().onClick.AddListener(ExitNow);
    }
    void ExitNow()
    {
        //Application.Quit();
        GameObject.FindObjectOfType<Obj1Manager>().QType_Template.SetActive(true);


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
