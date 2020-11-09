using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Obj11ROManager : MonoBehaviour
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
        DisableSubmitButton();
        if (ans == "1")
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            if (UtilityArtifacts.loading_pos == "Obj11_Lo1_from_obj12")
            {

                UtilityArtifacts.loading_pos = "";
                UtilityArtifacts.backTraversal = false;
                UtilityArtifacts.comingbackafterTraversal = true;
                UtilityArtifacts.loadStartingpoint = 0;
                UtilityArtifacts.loadEndingpoint = 0;

                // load traversescene 13
                //SceneManager.LoadScene("obj_12_improper_and_mixed");
                OnPreRequisitOver();

            }
            else
            {
                GameObject.FindObjectOfType<timeline_new>().load_next();
            }

        }
        else
        {


            GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
            GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj11_let_see_why_common.wav");
            temp.GetComponent<Image>().color = Color.red;
            Invoke("ReInforse_RO1", 5);
            //ReInforse_RO1();

        }

    }
    public void ReInforse_RO1()
    {
        GameObject.Find("ROSubmitButton").SetActive(false);
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj11_RO1_Quest1_Hint1.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("Proper fractions represents objects that are less than a whole and have value less than 1. As you can see, this pizza is full and hence we cannot use proper fractions to express its quantity.");
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(ok);
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
        DisableSubmitButton();
        if (ans == "3")
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            convesationDisable();
        }

        else
        {
            GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
            GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj11_let_see_why_common.wav");
            temp.GetComponent<Image>().color = Color.red;
            Invoke("ReInforse_RO2", 5);
            //ReInforse_RO2();
        }

    }
    public void ReInforse_RO2()
    {
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj11_RO1_Ques2_Hint1.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("Fractions whose value is less than 1 always have numerator less than their denominator. Such fractions are called Proper fractions.");
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
        DisableSubmitButton();
        if (ans == "2")
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            convesationDisable();
        }

        else
        {
            
           
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
            GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj11_let_see_why_common.wav");
            temp.GetComponent<Image>().color = Color.red;
            Invoke("ReInforse_RO3", 5);
            //ReInforse_RO3();
           
        }

    }
    public void ReInforse_RO3()
    {
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj11_RO2_Quest1_Hint1.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("Fractions whose numerator is 1 are called unit fractions. ");
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(ok);
    }
    


    public void EnableSubmitButtonRO4()
    {
        GetallNumberButtons();
        GameObject.Find("ROSubmitButton").transform.GetChild(0).GetComponent<Button>().onClick.RemoveListener(EnableSubmitButtonRO3);
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(SubmitObj13RO4);
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
        DisableSubmitButton();
        if (ans == "1")
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            convesationDisable();
        }

        else
        {
            GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
            GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj11_let_see_why_common.wav");
            temp.GetComponent<Image>().color = Color.red;
            Invoke("ReInforse_RO4", 5);
            //ReInforse_RO4();
        }

    }
    public void ReInforse_RO4()
    {
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj11_RO3_Quest1_Hint1.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("Unit fraction of any object represents the size of each part of the object.");
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(6).gameObject.SetActive(true);
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(ok);
    }


    public void EnableSubmitButtonRO5()
    {
        GetallNumberButtons();
        GameObject.Find("ROSubmitButton").transform.GetChild(0).GetComponent<Button>().onClick.RemoveListener(EnableSubmitButtonRO4);
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(SubmitObj13RO5);
    }
    public void SubmitObj13RO5()
    {
        if (ans != null)
        {
            ansList.Add(ans);
            check_RO5_ans();


        }
    }
    void check_RO5_ans()
    {
        deselect_option();
        DisableSubmitButton();
        if (ans == "1")
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            convesationDisable();
        }

        else
        {
            GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
            GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj11_let_see_why_common.wav");
            temp.GetComponent<Image>().color = Color.red;
            Invoke("ReInforse_RO5", 5);
            //ReInforse_RO5();
        }

    }
    public void ReInforse_RO5()
    {
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj11_RO4_Quest1_Hint1.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("The unit fraction determines the size of each part in an object or a fraction. As you can see, this object has 6 parts and hence each part is of the size \\frac{1}{6}. Hence the size of each part in the fraction \\frac{4}{6} is \\frac{1}{6}.");
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(5).gameObject.SetActive(true);
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(ok);
    }



    public void EnableSubmitButtonRO6()
    {
        GetallNumberButtons();
        GameObject.Find("ROSubmitButton").transform.GetChild(0).GetComponent<Button>().onClick.RemoveListener(EnableSubmitButtonRO5);
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(SubmitObj13RO6);
    }
    public void SubmitObj13RO6()
    {
        if (ans != null)
        {
            ansList.Add(ans);
            check_RO6_ans();


        }
    }
    void check_RO6_ans()
    {
        deselect_option();
        DisableSubmitButton();
        if (ans == "1")
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
            GameObject.FindObjectOfType<timeline_new>().stopAudio();
            Invoke("animStop", 2.5f);
        }

        else
        {
            GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
            GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj11_let_see_why_common.wav");
            temp.GetComponent<Image>().color = Color.red;
            Invoke("ReInforse_RO6", 5);
            //ReInforse_RO6();
        }

    }
    public void ReInforse_RO6()
    {
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj11_RO6_Quest1_Hint1.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("A group of unit fractions added will result in another fraction. Example, When you bring a group of 3 quarters together, we will get \\frac{3}{4}");
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
        if (UtilityArtifacts.loading_pos == "Obj11_Lo1_from_obj12")
        {

            UtilityArtifacts.loading_pos = "";
            UtilityArtifacts.backTraversal = false;
            UtilityArtifacts.comingbackafterTraversal = true;
            UtilityArtifacts.loadStartingpoint = 0;
            UtilityArtifacts.loadEndingpoint = 0;

            // load traversescene 13
            //SceneManager.LoadScene("obj_12_improper_and_mixed");
            OnPreRequisitOver();

        }
        else
        {
            convesationDisable();
        }

    }
    void ok1()
    {
        CancelInvoke();
        lastConvesationDisable();
    }
    void convesationDisable()
    {
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(5).gameObject.SetActive(false);
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(6).gameObject.SetActive(false);
        GameObject.FindObjectOfType<conversationManager>().DisableDialouge();
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        //if (UtilityArtifacts.loading_pos == "Obj11_Lo1_from_obj12")
        //{
        //    UtilityArtifacts.loading_pos = "";
        //    UtilityArtifacts.loading_pos = "Obj11_Lo1_from_obj12";
        //    UtilityArtifacts.coming_back_from = "to_Obj12_quest2";
        //    UtilityArtifacts.backTraversal = false;
        //    UtilityArtifacts.comingbackafterTraversal = true;
        //    UtilityArtifacts.loadStartingpointforcomingback = 24;
        //    UtilityArtifacts.loadStartingpoint = 0;
        //    UtilityArtifacts.loadEndingpoint = 0;
        //    // load traversescene 12
        //    SceneManager.LoadScene("obj_12_improper_and_mixed");
        //}
        //else
        //{
            GameObject.FindObjectOfType<timeline_new>().load_next();
        //}
        
    }
    void lastConvesationDisable()
    {
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(5).gameObject.SetActive(false);
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(6).gameObject.SetActive(false);
        GameObject.FindObjectOfType<conversationManager>().DisableDialouge();
        GameObject.FindObjectOfType<timeline_new>().stopAudio();
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
    void OnPreRequisitOver()
    {
        UtilityArtifacts.loadStartingpoint = 0;
        UtilityArtifacts.loadEndingpoint = 0;
        UtilityArtifacts.loading_pos = "";
        UtilityArtifacts.backTraversal = false;
        UtilityArtifacts.comingbackafterTraversal = true;
        NewIncomingMsg mg = UtilityArtifacts.NJMsg;
        mg.current_activity_status = 2;
        mg.pre_req_id = 0;
        mg.pre_req_status = 1;
        mg.pre_req_reqData.error_obj_id = 0;
        UtilityArtifacts.UnityMsg = UtilityArtifacts.outGoingMessage(mg, "false");
        SceneManager.LoadScene(UtilityArtifacts.NJLoadScene);
    }
    void animStop()
    {
        if (UtilityArtifacts.loading_pos == "Obj11_Lo4_from_obj12")
        {
            UtilityArtifacts.loading_pos = "Obj11_Lo4_from_obj12";
            UtilityArtifacts.coming_back_from = "to_Obj12_quest1";
            UtilityArtifacts.backTraversal = false;
            UtilityArtifacts.comingbackafterTraversal = true;
            UtilityArtifacts.loadStartingpointforcomingback = 16;
            UtilityArtifacts.loadStartingpoint = 0;
            UtilityArtifacts.loadEndingpoint = 0;
            // load traversescene 12
            //SceneManager.LoadScene("obj_12_improper_and_mixed");
            OnPreRequisitOver();
        }
        else
        {
            GameObject.FindObjectOfType<GameManager>().OnGameOver();
        }

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
