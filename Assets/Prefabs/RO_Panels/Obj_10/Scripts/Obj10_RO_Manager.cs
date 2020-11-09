using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Obj10_RO_Manager : MonoBehaviour
{
    public string ans;
    public List<string> ansList = new List<string>();
    public int numberOfAttempt = 0;
    public GameObject temp;
    public void Initiliaze()
    {
        EnableSubmitButtonRO1_Obj10();


        GetallNumberButtons();
        GetOkButton();
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(ok);

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
        convesationDisable();

    }


    void enableFade()
    {
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
        Invoke("nextObjective1", 3.0f);
    }
    void nextObjective1()
    {

        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadeout");
        GameObject.Find("RO Panel").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.FindObjectOfType<timeline_new>().load_next();

    }


    public void EnableSubmitButtonRO1_Obj10()
    {
        Debug.Log("Enable for ro1");
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(Submit_RO_1);
    }
    public void Submit_RO_1()
    {
        if (ans != null)
        {
            ansList.Add(ans);
            check_RO1_ans();

        }

    }
    void check_RO1_ans()
    {
        temp = GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject;
        if (ans == "1")
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            //GameObject.FindObjectOfType<Obj4Manager>().UnHighLightAllBlocks();
            ReInforse_RO_right();


        }
        else
        {
            if(numberOfAttempt<1)
            {
                numberOfAttempt++;
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                ReInforse_RO1();
            }
            else
            {
                numberOfAttempt = 0;
                UtilityArtifacts.loading_pos = "Obj8_Lo1_from_obj10";
                UtilityArtifacts.coming_back_from = "to_Obj10_quest1";
                UtilityArtifacts.backTraversal = false;
                UtilityArtifacts.comingbackafterTraversal = true;
                UtilityArtifacts.loadStartingpoint = 0;
                UtilityArtifacts.loadEndingpoint = 0;
                UtilityArtifacts.loadStartingpointforcomingback = 5;
                // load traversescene 8
                //SceneManager.LoadScene("OBJ_8_N_subscenario_2");
                OnTraversal(159, 130);
            }

        }
    }
    void ReInforse_RO1()
    {
        FindObjectOfType<Obj10manager>().unequal_pizza_reinforcement.SetActive(true);
        GameObject.FindObjectOfType<conversationManager>().EnableHint("This isn't right. Let's understand why we can't put the pizza in any of the sections.");
        FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj10_ro1.wav");
       
        DisableSubmitButton();
        Invoke("disablehint_1", 7.0f);
        //Invoke("enableFade", 7);
    }
    void ReInforse_RO_right()
    {
       
        GameObject.FindObjectOfType<conversationManager>().EnableHint("You are right . Let's understand this in detail. ");
        DisableSubmitButton();
        FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj10_ro1_right.wav");
        
        Invoke("disablehint_1", 5.0f);
        Invoke("enableFade",5);
    }
    void disablehint()
    {
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.FindObjectOfType<conversationManager>().DisableHint();
    }
    void disablehint_1()
    {
        FindObjectOfType<Obj10manager>().unequal_pizza_reinforcement.SetActive(false);
        //GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.FindObjectOfType<conversationManager>().DisableHint();
        temp.SetActive(true);
    }






    public void EnableSubmitButtonRO2_Obj10()
    {
        GetallNumberButtons();
        Debug.Log("Enable for ro1");
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(Submit_RO_2);
    }
    public void Submit_RO_2()
    {
        if (ans != null)
        {
            ansList.Add(ans);
            check_RO2_ans();

        }

    }
    void check_RO2_ans()
    {
        temp = GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject;
        if (ans == "1")
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            //GameObject.FindObjectOfType<Obj4Manager>().UnHighLightAllBlocks();
            GameObject.FindObjectOfType<timeline_new>().load_next();


        }
        else if (ans == "2")
        {
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                ReInforse_RO2_1();
            }
        }
        else
        {
            if(numberOfAttempt<1)
            {
                numberOfAttempt++;
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                ReInforse_RO2_2();
            }
            else
            {
                numberOfAttempt = 0;
                UtilityArtifacts.loading_pos = "Obj4_Lo1_from_obj10";
                UtilityArtifacts.coming_back_from = "to_Obj10_RO1";
                UtilityArtifacts.backTraversal = true;
                UtilityArtifacts.comingbackafterTraversal = false;
                UtilityArtifacts.loadStartingpoint = 4;
                UtilityArtifacts.loadEndingpoint = 11;
                UtilityArtifacts.loadStartingpointforcomingback = 14;
                // load traversescene 4
                //SceneManager.LoadScene("Obj4AreaModule");
                OnTraversal(155, 129);
            }

        }


    }
    void ReInforse_RO2_2()
    {
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj10_RO1_Hint2.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("You have divided the circle equally. As we can see the denominator part is right. It indicates 5 parts of the circle. But the fraction is wrong.");
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(6).gameObject.SetActive(true);
        DisableSubmitButton();
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(ok2);
        //GameObject.Find("Dialouge").transform.GetChild(0).GetChild(2).GetComponent<Image>().sprite = Load_audio_n_Play_storyBoard from assets;
    }
    void ReInforse_RO2_1()
    {
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj10_RO1_Hint1.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("To represent a part of an unequally cut object, we divide the object to make all parts equal and then count the number of parts coloured and the total number of parts to represent the part as a fraction. All parts should be \\frac{1}{4} as per your answer. Clearly, all parts are not equal in size or shape.");
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        DisableSubmitButton();
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(ok2);
        Invoke("next_Vo", 9f);
        //GameObject.Find("Dialouge").transform.GetChild(0).GetChild(2).GetComponent<Image>().sprite = Load_audio_n_Play_storyBoard from assets;
    }



    public void EnableSubmitButtonRO3_Obj10()
    {
        GetallNumberButtons();
        Debug.Log("Enable for ro1");
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(Submit_RO_3);
    }
    public void Submit_RO_3()
    {
        if (ans != null)
        {
            ansList.Add(ans);
            check_RO3_ans();

        }

    }
    void check_RO3_ans()
    {
        if (ans == "3")
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            //GameObject.FindObjectOfType<Obj4Manager>().UnHighLightAllBlocks();
            //  GameObject.FindObjectOfType<timeline_new>().load_next();
            lastConvesationDisable();

        }
        else
        {

            GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
            ReInforse_RO3();

        }


    }
    void ReInforse_RO3()
    {
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj10_RO5_Hint1.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("Both the coloured part of the circle is \\frac{1}{8} as the coloured portion is equal in size and shape. If you make the unequally portioned circle to have equal parts, you can see that the coloured part is also \\frac{1}{8}. Therefore, unequally partitioned objects can be represented as fractions by making the size of all parts equal.");
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(5).gameObject.SetActive(true);
        DisableSubmitButton();
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(ok1);
        //GameObject.Find("Dialouge").transform.GetChild(0).GetChild(2).GetComponent<Image>().sprite = Load_audio_n_Play_storyBoard from assets;
    }


    void next_Vo()
    {
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj10_RO1_Hint1_1.wav");
        Invoke("next_Vo1", 8f);
    }
    void next_Vo1()
    {
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj10_RO1_Hint1_2.wav");
    }


    void enableNextQuest()
    {
        CancelInvoke();
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(5).gameObject.SetActive(false);
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(6).gameObject.SetActive(false);
        GameObject.Find("Dialouge").transform.GetChild(0).gameObject.SetActive(false);




    }

    void ok()
    {
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(5).gameObject.SetActive(false);
        CancelInvoke();
        convesationDisable();
    }
    void ok2()
    {
        GameObject.Find("Dialouge").transform.GetChild(0).gameObject.SetActive(false);
        CancelInvoke();
        temp.SetActive(true);
    }
    void ok1()
    {
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(5).gameObject.SetActive(false);
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
        GameObject.FindObjectOfType<timeline_new>().load_next();
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
    void animStop()
    {
        UtilityArtifacts.loading_pos = "";
        UtilityArtifacts.coming_back_from = "";
        GameObject.FindObjectOfType<GameManager>().OnGameOver();
       
        //GameObject.Find("ExitPanel").transform.GetChild(0).gameObject.SetActive(true);
        //GameObject.Find("ExitPanel").transform.GetChild(0).GetChild(0).GetComponent<Button>().onClick.AddListener(ExitNow);
    }
    void ExitNow()
    {
        Application.Quit();
    }
    void OnTraversal(int objId, int subTopicId)
    {
        NewIncomingMsg mg = UtilityArtifacts.NJMsg;
        mg.current_activity_status = 1;
        mg.pre_req_id = subTopicId;// objId;
        mg.pre_req_status = 1;
        mg.pre_req_reqData.error_obj_id = objId;
        UtilityArtifacts.UnityMsg = UtilityArtifacts.outGoingMessage(mg, "false");
        SceneManager.LoadScene(UtilityArtifacts.NJLoadScene);
    }
}
