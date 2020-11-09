using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Obj14_ROManager : MonoBehaviour
{
    public string ans;
    public List<string> ansList = new List<string>();
    Obj14CanvasManager canvasManager;
    int question_1_Hint_Count;

    void OnEnable()
    {
    }
    
    private void Start()
    {
        //Initiliaze();
    }
    public void Initiliaze()
    {
        canvasManager = GameObject.FindObjectOfType<Obj14CanvasManager>();
        enableRO();
        GetallNumberButtons();
        EnableSubmitButtonRO1_Obj14();

    }
    void enableRO()
    {
        canvasManager.RO_Panel.SetActive(true);
        GameObject.Find("Dialouge").transform.GetChild(0).gameObject.SetActive(false);


    }
    public void GetallNumberButtons()
    {
        Debug.LogError("GetallNumberButtons: ");

        foreach (GameObject b in GameObject.FindGameObjectsWithTag("Numbers"))
        {
            b.GetComponent<Button>().onClick.AddListener(InputNumbers);
            Debug.LogError("GetallNumberButtons: " + b.name);
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
        _convesationDisable();

    }

    void _convesationDisable()
    {
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
        GameObject.FindObjectOfType<conversationManager>().DisableDialouge();
        enableFade();
    }
    void enableFade()
    {
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
        Invoke("nextObjective1", 3.0f);
    }
    void nextObjective1()
    {
        reset_option();
        GameObject enabledPanel = GameObject.FindGameObjectWithTag("RO Panel");
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadeout");
        //enabledPanel.transform.parent.gameObject.SetActive(false);
        enabledPanel.SetActive(false);
        GameObject.FindObjectOfType<timeline_new>().load_next();

    }

    public void EnableSubmitButtonRO1_Obj14()
    {
        Debug.Log("Enable for ro1");
        canvasManager.RO_Panel.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(Submit_RO_1);
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(ok);
    }
    public void Submit_RO_1()
    {
        if (ans != null)
        {
            ansList.Add(ans);
            check_RO1_ans();

        }

    }
    void reset_option()
    {
        foreach (GameObject b in GameObject.FindGameObjectsWithTag("Numbers"))
        {
            b.transform.GetChild(2).gameObject.SetActive(false);
        }
        ans = "";
    }
    void check_RO1_ans()
    {
        if (ans == "1")
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            //GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj14_GoodJob");

            reset_option();
            GameObject enabledPanel = GameObject.FindGameObjectWithTag("RO Panel");
            //enabledPanel.gameObject.SetActive(false);
            enabledPanel.SetActive(false);
            GameObject.FindObjectOfType<timeline_new>().load_next();

            //enableFade();
            //FindObjectOfType<timeline_new>().load_next();

        }
        else if (ans == "2")
        {
            {
                if (question_1_Hint_Count >= 1)
                {
                    traverse();
                    //reset_option();
                    //GameObject enabledPanel = GameObject.FindGameObjectWithTag("RO Panel");
                    ////enabledPanel.transform.parent.gameObject.SetActive(false);
                    //enabledPanel.SetActive(false);
                    //GameObject.FindObjectOfType<timeline_new>().load_next();
                }
                else
                {
                    GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                    ReInforse_RO_2();
                }
            }
        }
        else if (ans == "3")
        {
            {
                if (question_1_Hint_Count >= 1)
                {
                    traverse();
                    //reset_option();
                    //GameObject enabledPanel = GameObject.FindGameObjectWithTag("RO Panel");
                    ////enabledPanel.transform.parent.gameObject.SetActive(false);
                    //enabledPanel.SetActive(false);
                    //GameObject.FindObjectOfType<timeline_new>().load_next();
                }
                else
                {
                    GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                    ReInforse_RO_3();
                }
            }
        }
        else if (ans == "4")
        {
            {
                if (question_1_Hint_Count >= 1)
                {
                    traverse();
                    //reset_option();
                    //GameObject enabledPanel = GameObject.FindGameObjectWithTag("RO Panel");
                    ////enabledPanel.transform.parent.gameObject.SetActive(false);
                    //enabledPanel.SetActive(false);
                    //GameObject.FindObjectOfType<timeline_new>().load_next();
                }
                else
                {
                    GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                    ReInforse_RO_4();
                }
            }
        }
    }
    void ReInforse_RO_2()
    {
        question_1_Hint_Count++;
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj14_RO1_Hint1");

        GameObject.Find("Canvas/RO Panel/Panel/Obj14_RO_1/Chef conversation (1)/GameObject").SetActive(true);
        GameObject.Find("Canvas/RO Panel/Panel/Obj14_RO_1/Chef conversation (1)/GameObject/Panel/ConversationText").GetComponent<TEXDraw>().text =
            "This option you chose does not represent 3 " + "\\frac{2}{5}" + ". Ensure that the option has a representation of the correct whole number part and the fraction part. ";
        reset_option();
        Invoke("HideConversationPanel_ForQuestion1", GameObject.FindObjectOfType<timeline_new>().lapa.length_of_audio + 5f);

    }
    void ReInforse_RO_3()
    {
        question_1_Hint_Count++;
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj14_RO1_Hint1");
        //canvasManager.set_dialougue("This option you chose does not represent 3 " + "\\frac{2}{5}" + ". Ensure that the option has a representation of the correct whole number part and the fraction part. ");
        GameObject.Find("Canvas/RO Panel/Panel/Obj14_RO_1/Chef conversation (1)/GameObject").SetActive(true);
        GameObject.Find("Canvas/RO Panel/Panel/Obj14_RO_1/Chef conversation (1)/GameObject/Panel/ConversationText").GetComponent<TEXDraw>().text =
            "The fraction part of the mixed fraction is" + "\\frac{2}{5}" + ". This option you chose is not a representation of" + "\\frac{2}{5}" + ". Ensure that the option is correct representation of both the whole number part and the fraction part.  ";
        reset_option();
        Invoke("HideConversationPanel_ForQuestion1", GameObject.FindObjectOfType<timeline_new>().lapa.length_of_audio + 5f);

    }
    void ReInforse_RO_4()
    {
        question_1_Hint_Count++;
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj14_RO1_Hint2");
        //canvasManager.set_dialougue("The fraction part of the mixed fraction is " + "\\frac{2}{5}" + ". This option you chose does not a representation of " + "\\frac{2}{5}" + ". Ensure that the option is correct representation of both the whole number part and the fraction part. ");
        GameObject.Find("Canvas/RO Panel/Panel/Obj14_RO_1/Chef conversation (1)/GameObject").SetActive(true);
        GameObject.Find("Canvas/RO Panel/Panel/Obj14_RO_1/Chef conversation (1)/GameObject/Panel/ConversationText").GetComponent<TEXDraw>().text =
            "This option you chose does not represent 3 " + "\\frac{2}{5}" + ". Ensure that the option has a representation of the correct whole number part and the fraction part.  ";
        reset_option();
        Invoke("HideConversationPanel_ForQuestion1", GameObject.FindObjectOfType<timeline_new>().lapa.length_of_audio + 5f);

    }
    void enableRO1()
    {
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        GameObject.Find("Dialouge").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        EnableSubmitButtonRO1();
    }
    void HideConversationPanel_ForQuestion1()
    {
        GameObject.Find("Canvas/RO Panel/Panel/Obj14_RO_1/Chef conversation (1)/GameObject/Panel/ConversationText").GetComponent<TEXDraw>().text = "";
        GameObject.Find("Canvas/RO Panel/Panel/Obj14_RO_1/Chef conversation (1)/GameObject").SetActive(false);

    }

    //==========================================================
    //Mahesh

    public void EnableSubmitButtonRO1()
    {
        GetallNumberButtons();
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ROSubmitButton").transform.GetChild(0).GetComponent<Button>().onClick.AddListener(Submit);
        //GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(enableRO2);
    }
    public void Submit()
    {
        if (ans != null)
        {
            ansList.Add(ans);
            check_RO1_2ans();


        }

    }
    void check_RO1_2ans()
    {
        if (ans == "2")
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            enableRO2();
        }
        else
        {

            ReInforse_RO1_1();



        }
        //GameObject.Find("ROSubmitButton").SetActive(false);
    }

    public void ReInforse_RO1_1()
    {
        Debug.Log("In RO1");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("When we convert mixed to improper fraction , we multiply the denominator and the whole number of the mixed fraction. This makes the whole number to have the same number of partitions as that of the fraction.");
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_14_RO1_Reinfo.wav");

    }

    void enableRO2()
    {
        GameObject.Find("Dialouge").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("RO Panel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        EnableSubmitButtonRO2();
        GameObject.FindObjectOfType<timeline_new>().load_next();
    }

    public void EnableSubmitButtonRO2()
    {
        GetallNumberButtons();
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(SubmitObj14RO2); ;
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(ok);
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
            ok();
        }

        else
        {
            GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
            ReInforse_RO2();
        }
        //GameObject.Find("ROSubmitButton").SetActive(false);
    }
    public void ReInforse_RO2()
    {
        Debug.Log("In RO2");
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_14_RO2_Reinfo.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("The denominator in the given mixed fraction and the resulting improper fraction does not change because the denominator indicates the number of parts and conversion of mixed to improper fraction does not change the number of parts. This only changes the way a fraction is expressed.");
    }
    void enableRO3()
    {
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
        GameObject.Find("Dialouge").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(3).gameObject.SetActive(true);
        EnableSubmitButtonRO3();
    }
    
    public void EnableSubmitButtonRO3()
    {
        GetallNumberButtons();
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(SubmitObj14RO3); ;
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
        //GameObject.Find("ROSubmitButton").SetActive(false);
    }
    public void ReInforse_RO3()
    {
        Debug.Log("In RO3");
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_14_RO3_Reinfo.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("When we convert mixed to improper fraction, we multiply the denominator and the whole number of the mixed fraction. We do this to make the whole number have the same number of partitions as the fraction has and add the result to the numerator of the mixed fraction.");
    }
    void enableRO4()
    {
        GameObject.Find("Dialouge").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("RO Panel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(4).gameObject.SetActive(true);
        EnableSubmitButtonRO4();
        GameObject.FindObjectOfType<timeline_new>().load_next();
    }
    
    public void EnableSubmitButtonRO4()
    {
        GetallNumberButtons();
        //GameObject.Find("ROSubmitButton").transform.GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(SubmitObj14RO4); ;
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(ok);
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
            ok();
        }

        else
        {
            GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
            ReInforse_RO4();
        }
        //GameObject.Find("ROSubmitButton").SetActive(false);
    }
    public void ReInforse_RO4()
    {
        Debug.Log("In RO4");
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_14_RO4_Reinfo.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("To add the result of multiplication of the whole number and denominator to the numerator of the mixed fractions means to add the number of parts in the whole number part to the number of parts in fraction part");
    }
    void enableRO5()
    {
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(4).gameObject.SetActive(false);
        GameObject.Find("Dialouge").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(5).gameObject.SetActive(true);
        EnableSubmitButtonRO5();
    }
    
    public void EnableSubmitButtonRO5()
    {
        GetallNumberButtons();
        //GameObject.Find("ROSubmitButton").transform.GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(SubmitObj14RO5); ;
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(ok1);
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
        Debug.Log("Last_________________________ROOOOOOOOOOOOO");

        if (ans == "1")
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
            Invoke("animStop", 2.5f);
        }

        else
        {
            GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
            ReInforse_RO5();
        }
        //GameObject.Find("ROSubmitButton").SetActive(false);
    }
    public void ReInforse_RO5()
    {
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_14_RO4_Reinfo.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("First we, multiply the denominator of the Mixed fraction with its whole number and then add the result to the numerator. This will give you the numerator of the Improper Fraction.Keep the denominator for the improper fraction same as the Mixed Fraction.The correct conversion of mixed to improper fraction of 2\\frac{1}{3} is \\frac{[(3x2)+1]}{3}" + " = " + "\\frac{7}{3}");
        Debug.Log("In RO5");
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
    void traverse()
    {
        UtilityArtifacts.loading_pos = "Obj12_Lo1_from_obj14";
        UtilityArtifacts.coming_back_from = "to_Obj14_quest1";
        UtilityArtifacts.backTraversal = true;
        UtilityArtifacts.comingbackafterTraversal = false;
        UtilityArtifacts.loadStartingpointforcomingback = 14;
        UtilityArtifacts.loadStartingpoint = 3;
        UtilityArtifacts.loadEndingpoint = 12;

        SceneManager.LoadScene("obj_12_improper_and_mixed");
        OnTraversal(149, 131);
        //load scene 12
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
    void ok()
    {
        CancelInvoke();
        convesationDisable();
    }
    void ok1()
    {
        Debug.Log("==================================OK1");

        CancelInvoke();
        lastConvesationDisable();
    }
    //Mhesh
    void convesationDisable()
    {
        GameObject.FindObjectOfType<conversationManager>().DisableDialouge();
        canvasManager.RO_Panel.transform.GetChild(0).gameObject.SetActive(false);
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

}