using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class AplleObj4ROManager : MonoBehaviour
{
    //MixedToImpObj13ROManager
    public string ans;
    public List<string> ansList = new List<string>();
    public GameObject temp;
    public delegate void LogMessage(string cases);
    public static event LogMessage onLogMessage;

    void OnEnable()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FindObjectOfType<timeline_new>().load_next();
        }
    }

    public void Initiliaze()
    {
        UtilityArtifacts.scene_to_load_after_canvas = "";
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
        GameObject.FindObjectOfType<Obj4Manager>().UnHighLightAllBlocks();
        CancelInvoke();
        convesationDisable();
        FindObjectOfType<Obj4Manager>().ro_3_reinforcement.SetActive(false);

    }
    public void EnableSubmitButtonRO1()
    {
        Debug.Log("Enable for ro1");
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(Submit);

        if (onLogMessage != null)
        {
            onLogMessage("Only option B is correct.  <br>" +
                "If the user selects option A, he/ she has not  understood ‘Representation of Fractions’ completely or did not read the question properly <br> " +
                "If the user selects option C, he/ she has not  understood ‘Representation of Fractions’  <br>" +
                "If the user selects option D, he/ she has not  understood ‘Representation of Fractions’ or is confused with the concept/ placement of ‘Numerator & Denominator’");
        }
    }
    public void Submit()
    {
        if (ans == "")
        {
            Debug.Log("please select answer");
            GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
        }
        else
        {
            ansList.Add(ans);
            check_Lo1_ans();


        }

    }
    void check_Lo1_ans()
    {
        deselect_option();
        DisableSubmitButton();
        if (ans == "2")
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            GameObject.FindObjectOfType<Obj4Manager>().UnHighLightAllBlocks();
            temp.GetComponent<Image>().color = Color.green;
            FindObjectOfType<timeline_new>().lipsync_player.GetComponent<AudioSource>().Stop();
            enableFade();

            if (onLogMessage != null)
            {
                onLogMessage("User has   understood ‘Representation of Fractions’ ");
            }
        }
        else
        {
            if (ans == "1")
            {
                {
                    GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                    GameObject.FindObjectOfType<Obj4Manager>().UnHighLightAllBlocks();
                    GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj4_let_see_why_common.wav");
                    temp.GetComponent<Image>().color = Color.red;
                    Invoke("ReInforse_LO1", 5);
                    //ReInforse_LO1();
                }
                if (onLogMessage != null)
                {
                    onLogMessage("User has not  understood ‘Representation of Fractions’ completely or did not read the question properly");
                }
            }
            else
            {
                {
                    GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                    GameObject.FindObjectOfType<Obj4Manager>().UnHighLightAllBlocks();
                    GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj4_let_see_why_common.wav");
                    temp.GetComponent<Image>().color = Color.red;
                    Invoke("ReInforse_LO2", 5);
                    //ReInforse_LO2();
                }

                // for option c and d 
                if (ans == "3")
                {
                    if (onLogMessage != null)
                    {
                        onLogMessage("User has not  understood ‘Representation of Fractions’");
                    }
                }
                else
                {
                    if (onLogMessage != null)
                    {
                        onLogMessage("User has not  understood ‘Representation of Fractions’ or is confused with the concept/ placement of ‘Numerator & Denominator’");
                    }
                }

            }
        }

    }
    public void ReInforse_LO1()
    {
        Debug.Log("Enable for reinfo1");

        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj4_Quest2_RO_Hint1.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("Numerator of a fraction is the number of parts considered. In this case, the number of sections of the farm that have benches.\n As you can see, there are 5 sections of the farm that have benches and since there are 8 total sections in the farm.\n The part of the farm that has benches is \\frac{5}{8}\n\n\n");
        Invoke("ReInforse_LO1_1", 9.0f);

    }
    public void ReInforse_LO1_1()
    {
        Debug.Log("Enable for reinfo1_1");

        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj4_Quest2_RO_Hint1_1.wav");
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(5).gameObject.SetActive(true);
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("4_Quest2_RO_Hint1.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("Numerator of a fraction is the number of parts considered. In this case, the number of sections of the farm that have benches.\n As you can see, there are 5 sections of the farm that have benches and since there are 8 total sections in the farm.\n The part of the farm that has benches is \\frac{5}{8}\n\n\n");


    }
    public void ReInforse_LO2()
    {
        Debug.Log("Enable for reinfo1");
        //GameObject.Find("Dialouge").transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj4_Quest2_RO_Hint2.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("A fraction is written as \\frac{Numerator}{Denominator}. \n \\frac{Sections considered}{Total number of sections}   =   \\frac{Numerator}{Denominator} \nTherefore, the part of the farm that has benches is \\frac{5}{8}.");
        Invoke("ReInforse_LO2_1", 11.0f);

    }
    public void ReInforse_LO2_1()
    {
        Debug.Log("Enable for reinfo1_1");

        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj4_Quest2_RO_Hint2_1.wav");
        //GameObject.Find("Dialouge").transform.GetChild(0).GetChild(3).gameObject.SetActive(true);
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("A fraction is written as \\frac{Numerator}{Denominator}. \n \\frac{Sections considered}{Total number of sections}   =   \\frac{Numerator}{Denominator} \nTherefore, the part of the farm that has benches is \\frac{5}{8}.");


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
        if (ans == "")
        {
            Debug.Log("please select answer");
            GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
        }
        else
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
            temp.GetComponent<Image>().color = Color.green;
            enableFade();

        }

        else
        {
            GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
            GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj4_let_see_why_common.wav");
            temp.GetComponent<Image>().color = Color.red;
            Invoke("ReInforse_LO2_ans1", 5);

            //ReInforse_LO2_ans1();
        }

    }
    public void ReInforse_LO2_ans1()
    {
        Debug.Log("Enable for reinfo2");
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj4_Quest3_RO_Hint1.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("To represent a part of an object as a fraction, find the numerator and denominator. This means that the numerator is 3 and the denominator is 4.\n The fraction of the circle that is coloured is \\frac{3}{4}.");
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        Invoke("ReInforse1_LO2_ans1", 7.0f);

    }
    public void ReInforse1_LO2_ans1()
    {
        Debug.Log("Enable for reinfo2.1");
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj4_Quest3_RO_Hint2.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("To represent a part of an object as a fraction, find the numerator and denominator. This means that the numerator is 3 and the denominator is 4.\n The fraction of the circle that is coloured is \\frac{3}{4}.");

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
        if (ans == "")
        {
            Debug.Log("please select answer");
            GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
        }
        else
        {
            ansList.Add(ans);
            check_Lo2_ans2();


        }
    }
    void check_Lo2_ans2()
    {
        deselect_option();
        DisableSubmitButton();
        if (ans == "5")
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            temp.GetComponent<Image>().color = Color.green;
            enableFade();
        }

        else
        {
            GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
            GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj4_let_see_why_common.wav");
            temp.GetComponent<Image>().color = Color.red;
            Invoke("ReInforse_LO2_ans2", 5);
            //ReInforse_LO2_ans2();
        }

    }
    public void ReInforse_LO2_ans2()
    {
        FindObjectOfType<Obj4Manager>().ro_3_reinforcement.SetActive(true);
        Debug.Log("Enable for reinfo2");
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj4_Quest4_RO_Hint1.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("Only the total sections should be considered and not which parts are considered. Here, all shapes have 2 parts coloured out of 4 total parts. Hence all 4 shapes are \\frac{2}{4}. ");

        //Invoke("convesationDisable", 15.0f);

    }


    public void EnableSubmitButtonRO4()
    {
        Debug.Log("Enable for ro4");
        GetallNumberButtons();
        GameObject.Find("ROSubmitButton").transform.GetChild(0).GetComponent<Button>().onClick.RemoveListener(SubmitObj4Q3);
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(SubmitObj4Q4); ;
    }
    public void SubmitObj4Q4()
    {
        if (ans == "")
        {
            Debug.Log("please select answer");
            GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
        }
        else
        {
            ansList.Add(ans);
            check_Lo2_ans4();


        }
    }
    void check_Lo2_ans4()
    {
        deselect_option();
        DisableSubmitButton();
        if (ans == "4")
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            temp.GetComponent<Image>().color = Color.green;
            //if (UtilityArtifacts.loading_pos == "Obj4_Lo1_from_obj12")
            //{
            enableFade();
            //}
            //else
            //{
            GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
            Invoke("animStop", 2.5f);
            //}

        }

        else
        {
            GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
            GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj4_let_see_why_common.wav");
            temp.GetComponent<Image>().color = Color.red;
            Invoke("ReInforse_LO2_ans4", 5);
            //ReInforse_LO2_ans4();
        }

    }
    public void ReInforse_LO2_ans4()
    {
        Debug.Log("Enable for reinfo2");
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj4_Quest6_RO_Hint1.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("The value of a fractions is 1 whose numerator and denominator is the same.");
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(3).gameObject.SetActive(true);
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        //if (UtilityArtifacts.loading_pos == "Obj4_Lo1_from_obj12")
        //{
        //    GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(GetOkButton);
        //}
        //else
        //{
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(lastConvesationDisable);
        //}

    }





    void convesationDisable()
    {
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(5).gameObject.SetActive(false);
        GameObject.FindObjectOfType<conversationManager>().DisableDialouge();
        FindObjectOfType<timeline_new>().lipsync_player.GetComponent<AudioSource>().Stop();
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
        if (UtilityArtifacts.loading_pos == "Obj4_Lo1_from_obj12")
        {

            UtilityArtifacts.loading_pos = "";
            UtilityArtifacts.coming_back_from = "to_Obj12_quest2";
            UtilityArtifacts.backTraversal = false;
            UtilityArtifacts.comingbackafterTraversal = true;
            UtilityArtifacts.loadStartingpointforcomingback = 21;
            UtilityArtifacts.loadStartingpoint = 0;
            UtilityArtifacts.loadEndingpoint = 0;
            // load traversescene 12
            //SceneManager.LoadScene("obj_12_improper_and_mixed");
            OnPreRequisitOver();
        }
        else
        {
            //GameObject.FindObjectOfType<GameManager>().OnGameOver();
            ExitNow();
            //GameObject.Find("ExitPanel").transform.GetChild(0).gameObject.SetActive(true);
            //GameObject.Find("ExitPanel").transform.GetChild(0).GetChild(0).GetComponent<Button>().onClick.AddListener(ExitNow);

        }
    }
    void ExitNow()
    {
        //Application.Quit();
        GameObject.FindObjectOfType<Obj4Manager>().VisualQType_AreaModel_Temp.SetActive(true);


        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadeout");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    void enableFade1()
    {
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
        Invoke("ExitNow", 3.0f);
    }
    // for traversing back to calling objective
    void enableFade()
    {
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
        if (UtilityArtifacts.loading_pos == "Obj4_Lo1")
        {
            UtilityArtifacts.loading_pos = "";
            UtilityArtifacts.backTraversal = false;
            UtilityArtifacts.comingbackafterTraversal = true;
            UtilityArtifacts.loadStartingpoint = 0;
            UtilityArtifacts.loadEndingpoint = 0;

            // load traversescene 16
            SceneManager.LoadScene("obj 16 folding activity 1");
            //OnPreRequisitOver();
        }
        else if (UtilityArtifacts.loading_pos == "Obj4_Lo1_from_obj10")
        {
            UtilityArtifacts.loading_pos = "";
            UtilityArtifacts.backTraversal = false;
            UtilityArtifacts.comingbackafterTraversal = true;
            UtilityArtifacts.loadStartingpoint = 0;
            UtilityArtifacts.loadEndingpoint = 0;
            // load traversescene 10
            SceneManager.LoadScene("Obj10");
            //OnPreRequisitOver();
        }
        else if (UtilityArtifacts.loading_pos == "Obj4_Lo1_from_obj14")
        {
            UtilityArtifacts.loading_pos = "";
            UtilityArtifacts.backTraversal = false;
            UtilityArtifacts.comingbackafterTraversal = true;
            UtilityArtifacts.loadStartingpoint = 0;
            UtilityArtifacts.loadEndingpoint = 0;
            // load traversescene 14
            SceneManager.LoadScene("Obj14");
            //OnPreRequisitOver();
        }
        else if (UtilityArtifacts.loading_pos == "Obj4_Lo1_from_obj11")
        {
            UtilityArtifacts.loading_pos = "";
            UtilityArtifacts.backTraversal = false;
            UtilityArtifacts.comingbackafterTraversal = true;
            UtilityArtifacts.loadStartingpoint = 0;
            UtilityArtifacts.loadEndingpoint = 0;
            // load traversescene 11
            SceneManager.LoadScene("Obj11");
            //OnPreRequisitOver();
        }
        else if (UtilityArtifacts.loading_pos == "Obj4_Lo1_from_obj15")
        {
            UtilityArtifacts.loading_pos = "";
            UtilityArtifacts.backTraversal = false;
            UtilityArtifacts.comingbackafterTraversal = true;
            UtilityArtifacts.loadStartingpoint = 0;
            UtilityArtifacts.loadEndingpoint = 0;
            // load traversescene 15
            SceneManager.LoadScene("obj_15_new_story");
            //OnPreRequisitOver();
        }
        else
        {
            Invoke("nextObjective1", 3.0f);
        }



    }

    void OnPreRequisitOver()
    {
        UtilityArtifacts.loadStartingpoint = 0;
        UtilityArtifacts.loadEndingpoint = 0;
        NewIncomingMsg mg = UtilityArtifacts.NJMsg;
        mg.current_activity_status = 2;
        mg.pre_req_id = 0;
        mg.pre_req_status = 1;
        mg.pre_req_reqData.error_obj_id = 0;
        UtilityArtifacts.UnityMsg = UtilityArtifacts.outGoingMessage(mg, "false");
        SceneManager.LoadScene(UtilityArtifacts.NJLoadScene);
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
