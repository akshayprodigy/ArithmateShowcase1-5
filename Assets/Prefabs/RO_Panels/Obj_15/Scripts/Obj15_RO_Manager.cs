using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Obj15_RO_Manager : MonoBehaviour
{
    public delegate void LogMessage(string cases);
    public static event LogMessage onLogMessage;
    public string ans;
    public List<string> ansList = new List<string>();
    public GameObject temp;
    public void Initiliaze()
    {
        //  EnableSubmitButtonRO1_Obj15();
        //EnableSubmitButtonRO3_Obj15();


        GetallNumberButtons();
        //GetOkButton();


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
        convesationDisable();

    }

    void convesationDisable()
    {
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
        GameObject.FindObjectOfType<conversationManager>().DisableDialouge();
        FindObjectOfType<timeline_new>().lipsync_player.GetComponent<AudioSource>().Stop();
        enableFade();
    }
    public void enableFade()
    {
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
        if (UtilityArtifacts.loading_pos == "Obj15_Lo1")
        {
            //UtilityArtifacts.loading_pos = "";
            //
            UtilityArtifacts.loading_pos = "";
            UtilityArtifacts.backTraversal = false;
            UtilityArtifacts.comingbackafterTraversal = true;
            UtilityArtifacts.loadStartingpoint = 0;
            UtilityArtifacts.loadEndingpoint = 0;
            // load traversescene 16
            //OnPreRequisitOver();
            SceneManager.LoadScene("obj 16 folding activity 1");
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
        reset_option();
        GameObject enabledPanel = GameObject.FindGameObjectWithTag("RO Panel");
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadeout");
        enabledPanel.transform.parent.gameObject.SetActive(false);
        enabledPanel.SetActive(false);
        GameObject.FindObjectOfType<timeline_new>().load_next();

    }

    public void EnableSubmitButtonRO1_Obj15()
    {
        Debug.Log("Enable for ro1");
        if (UtilityArtifacts.loading_pos != "Obj15_Lo1")
        {
            GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        }

        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(Submit_RO_1);

        if (onLogMessage != null)
        {
            onLogMessage("Only option C is correct.  <br>" +
                "If the user selects A,B,D or E option , he/ she has not  understood ‘Recognising Equivalent Fractions’ ");
        }
    }
    public void Submit_RO_1()
    {
        if (ans == "")
        {
            Debug.Log("please select answer");
            GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
        }
        else
        { 
            Debug.Log("done");
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
       
            deselect_option();
            DisableSubmitButton();
            if (ans == "3")
            {
                GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
                //GameObject.FindObjectOfType<Obj4Manager>().UnHighLightAllBlocks();
                temp.GetComponent<Image>().color = Color.green;
                FindObjectOfType<timeline_new>().lipsync_player.GetComponent<AudioSource>().Stop();
                enableFade();
                //FindObjectOfType<timeline_new>().load_next();
                if (onLogMessage != null)
                {
                    onLogMessage("User understood Recognising Equivalent Fractions");
                }

            }
            else
            {
                if (ans == "1")
                {
                    {
                        GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj15_let_see_why_common.wav");
                        temp.GetComponent<Image>().color = Color.red;
                        Invoke("ReInforse_RO_1", 5);
                        //ReInforse_RO_1();
                    }

                }
                else if (ans == "2")
                {
                    {
                        GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj15_let_see_why_common.wav");
                        temp.GetComponent<Image>().color = Color.red;
                        Invoke("ReInforse_RO_2", 5);
                        // ReInforse_RO_2();
                    }
                }

                else if (ans == "4")
                {
                    {
                        GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj15_let_see_why_common.wav");
                        temp.GetComponent<Image>().color = Color.red;
                        Invoke("ReInforse_RO_4", 5);
                        // ReInforse_RO_4();
                    }
                }

                else if (ans == "5")
                {
                    {
                        GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj15_let_see_why_common.wav");
                        temp.GetComponent<Image>().color = Color.red;
                        Invoke("ReInforse_RO_5", 5);
                        //  ReInforse_RO_5();
                    }
                }
                if (onLogMessage != null)
                {
                    onLogMessage("User has not  understood ‘Recognising Equivalent Fractions");
                }
            }
       
    }
    void ReInforse_RO_1()
    {
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_15_ro_1_wrong.wav");
        //GameObject.FindObjectOfType<obj_15_new_story>().set_dialougue("Fractions are Equivalent if the quantity represented is same but the number of parts are different. All the circles here have different number of parts. This one is not equivalent to any of the other circles.");
        GameObject.FindObjectOfType<obj_15_new_story>().Show_Reinforcement_RO1();
        //show visualization
        reset_option();
       

        //GameObject.Find("Dialouge").transform.GetChild(0).GetChild(2).GetComponent<Image>().sprite = Load_audio_n_Play_storyBoard from assets;
    }
    void ReInforse_RO_2()
    {
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_15_ro_1_wrong.wav");
        //GameObject.FindObjectOfType<obj_15_new_story>().set_dialougue("Fractions are Equivalent if the quantity represented is same but the number of parts are different. All the circles here have different number of parts. 4 of them have the same size as the circle shaded, except for 1. This one is not equivalent to any of the other circles.");
        GameObject.FindObjectOfType<obj_15_new_story>().Show_Reinforcement_RO1();
        //GameObject.Find("Dialouge").transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        reset_option();
        //GameObject.Find("Dialouge").transform.GetChild(0).GetChild(2).GetComponent<Image>().sprite = Load_audio_n_Play_storyBoard from assets;
    }
    void ReInforse_RO_4()
    {
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_15_ro_1_wrong.wav");
        //GameObject.FindObjectOfType<obj_15_new_story>().set_dialougue("Fractions are Equivalent if the quantity represented is same but the number of parts are different. All the circles here have different number of parts. 4 of them have the same size as the circle shaded, except for 1. This one is not equivalent to any of the other circles.");
        GameObject.FindObjectOfType<obj_15_new_story>().Show_Reinforcement_RO1();
        reset_option();
        //  GameObject.Find("Dialouge").transform.GetChild(0).GetChild(2).gameObject.SetActive(true);

        //GameObject.Find("Dialouge").transform.GetChild(0).GetChild(2).GetComponent<Image>().sprite = Load_audio_n_Play_storyBoard from assets;
    }
    void ReInforse_RO_5()
    {
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_15_ro_1_wrong.wav");
        //GameObject.FindObjectOfType<obj_15_new_story>().set_dialougue("Fractions are Equivalent if the quantity represented is same but the number of parts are different. All the circles here have different number of parts. 4 of them have the same size as the circle shaded, except for 1. This one is not equivalent to any of the other circles.");
        GameObject.FindObjectOfType<obj_15_new_story>().Show_Reinforcement_RO1();
        reset_option();
        // GameObject.Find("Dialouge").transform.GetChild(0).GetChild(2).gameObject.SetActive(true);

        //GameObject.Find("Dialouge").transform.GetChild(0).GetChild(2).GetComponent<Image>().sprite = Load_audio_n_Play_storyBoard from assets;
    }

    public void EnableSubmitButtonRO3_Obj15()
    {
        Debug.Log("Enable for ro1");
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(Submit_RO_3);
    }
    public void Submit_RO_3()
    {
        //if (ans != null)
        //{
        //    ansList.Add(ans);
        //    check_RO3_ans();
        //}

        // GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
        //  enableFade();
        if (ans == "")
        {
            Debug.Log("please select answer");
            GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
        }
        else
        { 
            FindObjectOfType<obj_15_new_story>().number_line_panel.SetActive(false);

            if (ans == "1")
            {
                GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
                //GameObject.FindObjectOfType<Obj4Manager>().UnHighLightAllBlocks();
                // enableFade();
                FindObjectOfType<timeline_new>().load_next();


            }
            else
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                FindObjectOfType<obj_15_new_story>().highlight_ro3_numbers();
                GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_15_RO_3_option_b.wav");
                reset_option();
            }
        }

    }
    void deselect_option()
    {
        foreach (GameObject b in GameObject.FindGameObjectsWithTag("Numbers"))
        {
            b.transform.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        }

    }

}