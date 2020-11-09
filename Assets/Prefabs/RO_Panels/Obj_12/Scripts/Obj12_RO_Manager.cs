using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Obj12_RO_Manager : MonoBehaviour
{
    public string ans;
    public List<string> ansList = new List<string>();
    public GameObject temp;
    public obj_12_improper_and_mixed Obj_12_Improper_And_Mixed;

    private void Start()
    {
        Obj_12_Improper_And_Mixed = FindObjectOfType<obj_12_improper_and_mixed>();
    }

    public void Initiliaze()
    {
        EnableSubmitButtonRO1_Obj12();


        GetallNumberButtons();
        //  GetOkButton();


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
        enableFade();
    }
    void enableFade()
    {
        if (UtilityArtifacts.loading_pos == "Obj12_Lo1_from_obj13")
        {
            UtilityArtifacts.loading_pos = "";
            UtilityArtifacts.backTraversal = false;
            UtilityArtifacts.comingbackafterTraversal = true;
            UtilityArtifacts.loadStartingpoint = 0;
            UtilityArtifacts.loadEndingpoint = 0;
           
            // load traversescene 13
            //SceneManager.LoadScene("obj_13_conversion_improper_to_mixed");
            OnPreRequisitOver();
        }
       else if (UtilityArtifacts.loading_pos == "Obj12_Lo1_from_obj14")
        {
            UtilityArtifacts.loading_pos = "";
            UtilityArtifacts.backTraversal = false;
            UtilityArtifacts.comingbackafterTraversal = true;
            UtilityArtifacts.loadStartingpoint = 0;
            UtilityArtifacts.loadEndingpoint = 0;

            // load traversescene 14
            //SceneManager.LoadScene("Obj14");
            OnPreRequisitOver();
        }
        else if (UtilityArtifacts.loading_pos == "Obj12_Lo2_from_obj14")
        {
            UtilityArtifacts.loading_pos = "";
            UtilityArtifacts.backTraversal = false;
            UtilityArtifacts.comingbackafterTraversal = true;
            UtilityArtifacts.loadStartingpoint = 0;
            UtilityArtifacts.loadEndingpoint = 0;
            
            // load traversescene 14
            //SceneManager.LoadScene("Obj14");
            OnPreRequisitOver();
        }
        else
        {
            GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
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
        // GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadeout");
        enabledPanel.transform.parent.gameObject.SetActive(false);
        enabledPanel.SetActive(false);
        GameObject.FindObjectOfType<timeline_new>().load_next();

    }



    public void EnableSubmitButtonRO1_Obj12()
    {
       
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
        deselect_option();
        DisableSubmitButton();
        if (ans == "2")
        {
            if (UtilityArtifacts.loading_pos == "Obj12_Lo1_from_obj13")
            {
                UtilityArtifacts.loading_pos = "";
                UtilityArtifacts.backTraversal = false;
                UtilityArtifacts.comingbackafterTraversal = true;
                UtilityArtifacts.loadStartingpoint = 0;
                UtilityArtifacts.loadEndingpoint = 0;

                // load traversescene 13
                //SceneManager.LoadScene("obj_13_conversion_improper_to_mixed");
                OnPreRequisitOver();
            }
            else if (UtilityArtifacts.loading_pos == "Obj12_Lo1_from_obj14")
            {
                UtilityArtifacts.loading_pos = "";
                UtilityArtifacts.backTraversal = false;
                UtilityArtifacts.comingbackafterTraversal = true;
                UtilityArtifacts.loadStartingpoint = 0;
                UtilityArtifacts.loadEndingpoint = 0;

                // load traversescene 14
                //SceneManager.LoadScene("Obj14");
                OnPreRequisitOver();
            }
            else
            {
                GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
                //GameObject.FindObjectOfType<Obj4Manager>().UnHighLightAllBlocks();
                //  enableFade();
                Obj_12_Improper_And_Mixed.Obj12_RO_1.gameObject.SetActive(false);
                FindObjectOfType<timeline_new>().load_next();

            }
           
            //enableFade();



        }
        else if (ans == "1" || ans == "3")
        {
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj12_let_see_why_common.wav");
                temp.GetComponent<Image>().color = Color.red;
                Invoke("ReInforse_RO_1_1_wrong", 5);
                //ReInforse_RO_1_1_wrong();
            }
        }

        ans = "";
    }
    void ReInforse_RO_1_1_wrong()
    {
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_12_ro_1_1.wav");
        Obj_12_Improper_And_Mixed.set_dialougue(" Mixed fraction is a combination of a whole number and a proper fraction.");
    }


    public void EnableSubmitButtonRO1_2Obj12()
    {

        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(Submit_RO_1_2);
    }
    public void Submit_RO_1_2()
    {
        if (ans != null)
        {
            ansList.Add(ans);
            check_RO1_2_ans();

        }

    }
    void check_RO1_2_ans()
    {
        deselect_option();
        DisableSubmitButton();
        if (ans == "1")
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            //GameObject.FindObjectOfType<Obj4Manager>().UnHighLightAllBlocks();
            //  enableFade();
            Obj_12_Improper_And_Mixed.Obj12_RO_1_2.gameObject.SetActive(false);
            FindObjectOfType<timeline_new>().load_next();




        }
        else if (ans == "2" || ans == "3")
        {
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj12_let_see_why_common.wav");
                temp.GetComponent<Image>().color = Color.red;
                Invoke("ReInforse_RO_1_2_wrong", 5);
                //ReInforse_RO_1_2_wrong();
            }
        }

        ans = "";
    }
    void ReInforse_RO_1_2_wrong()
    {
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_12_ro_1_2.wav");
        Obj_12_Improper_And_Mixed.set_dialougue("A mixed fraction is a combination of a whole number and a proper fraction. Here is a combination of 1 and \\frac{1}{2} representing a combination of 1 full pizza and half of a pizza. Therefore, 1 \\frac{1}{2} is a mixed fraction.");
    }







    public void EnableSubmitButtonRO2_Obj12()
    {
       
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
        deselect_option();
        DisableSubmitButton();
        if (ans == "3")
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            //GameObject.FindObjectOfType<Obj4Manager>().UnHighLightAllBlocks();
         
            Obj_12_Improper_And_Mixed.Obj12_RO_2.gameObject.SetActive(false);
            FindObjectOfType<timeline_new>().load_next();

        }
        else if (ans == "1"|| ans == "2"|| ans == "4")
        {
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj12_let_see_why_common.wav");
                temp.GetComponent<Image>().color = Color.red;
                Invoke("ReInforse_RO2_1_wrong", 5);
                //ReInforse_RO2_1_wrong();
            }
        }
        ans = "";

    }
    void ReInforse_RO2_1_wrong()
    {
        Obj_12_Improper_And_Mixed.dialougue_image.SetActive(false);
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_12_ro_2.wav");
        Obj_12_Improper_And_Mixed.set_dialougue("Fractions whose numerator is bigger than the denominator are called Improper fractions.\n \\size[2]\\frac{3}{2} \\size[0]\n The numerator 3 is bigger than the denominator 2.\n Hence, \\frac{3}{2} is an improper fraction. ");
    }
    



    public void EnableSubmitButtonRO3_Obj12()
    {
      
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
        deselect_option();
        if (ans == "2")
        {
            if (UtilityArtifacts.loading_pos == "Obj12_Lo2_from_obj14")
            {
                UtilityArtifacts.loading_pos = "";
                UtilityArtifacts.backTraversal = false;
                UtilityArtifacts.comingbackafterTraversal = true;
                UtilityArtifacts.loadStartingpoint = 0;
                UtilityArtifacts.loadEndingpoint = 0;

                // load traversescene 14
                //SceneManager.LoadScene("Obj14");
                OnPreRequisitOver();
            }
            else
            {
                GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
                Obj_12_Improper_And_Mixed.Obj12_RO_2.gameObject.SetActive(false);
                FindObjectOfType<timeline_new>().load_next();

            }
           


        }
        else if (ans == "1"|| ans == "3")
        {
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj12_let_see_why_common.wav");
                temp.GetComponent<Image>().color = Color.red;
                Invoke("ReInforse_RO3_1", 5);
                //ReInforse_RO3_1();
            }
        }
        ans = "";
        DisableSubmitButton();
    }
    void ReInforse_RO3_1()
    {
        Obj_12_Improper_And_Mixed.dialougue_image.SetActive(false);
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_12_ro_3.wav");
        Obj_12_Improper_And_Mixed.set_dialougue("Improper fractions are used to represent objects whose value is more than 1 and this pizza is clearly less than 1 and hence cannot be expressed as a proper fraction. As you can see, \\frac{7}{4} of pizza forms more than one whole pizza and hence \\frac{7}{4} is an improper fraction.");
        enble_ro3_animation();
    }

    void enble_ro3_animation()
    {
        Obj_12_Improper_And_Mixed.ro_3_animation.SetActive(true);
    }


    public void EnableSubmitButtonRO4_1Obj12()
    {

        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(Submit_RO_4_1);
    }
    public void Submit_RO_4_1()
    {
        if (ans != null)
        {
            ansList.Add(ans);
            check_RO_4_1_ans();

        }

    }
    void check_RO_4_1_ans()
    {
        deselect_option();
        DisableSubmitButton();
        if (ans == "2")
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            //GameObject.FindObjectOfType<Obj4Manager>().UnHighLightAllBlocks();
            //  enableFade();
            Obj_12_Improper_And_Mixed.Obj12_RO_4_1.gameObject.SetActive(false);
            FindObjectOfType<timeline_new>().load_next();




        }
        else if (ans == "1" || ans == "3" || ans == "4")
        {
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj12_let_see_why_common.wav");
                temp.GetComponent<Image>().color = Color.red;
                Invoke("ReInforse_RO_4_1_wrong", 5);
                //ReInforse_RO_4_1_wrong();
            }
        }

        ans = "";
    }
    void ReInforse_RO_4_1_wrong()
    {
        Obj_12_Improper_And_Mixed.dialougue_image.SetActive(false);
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_12_ro_4.wav");
        Obj_12_Improper_And_Mixed.set_dialougue("We learnt that a fraction that have the same numerator and denominator carries a value of 1. Such fractions are also called Improper fractions");
    }


    public void EnableSubmitButtonRO5_1Obj12()
    {

        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(Submit_RO_5_1);
    }
    public void Submit_RO_5_1()
    {
        if (ans != null)
        {
            ansList.Add(ans);
            check_RO_5_1_ans();

        }

    }
    void check_RO_5_1_ans()
    {
        deselect_option();
        DisableSubmitButton();
        if (ans == "3")
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            //GameObject.FindObjectOfType<Obj4Manager>().UnHighLightAllBlocks();
            //  enableFade();
            Obj_12_Improper_And_Mixed.Obj12_RO_5_1.gameObject.SetActive(false);
            FindObjectOfType<timeline_new>().load_next();




        }
        else if (ans == "1" )
        {
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj12_let_see_why_common.wav");
                temp.GetComponent<Image>().color = Color.red;
                Invoke("ReInforse_RO_5_1_a_wrong", 5);
                //ReInforse_RO_5_1_a_wrong();
            }
        }
        else if (ans == "2")
        {
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj12_let_see_why_common.wav");
                temp.GetComponent<Image>().color = Color.red;
                Invoke("ReInforse_RO_5_1_b_wrong", 5);
                //ReInforse_RO_5_1_b_wrong();
            }
        }

        ans = "";
    }
    void ReInforse_RO_5_1_a_wrong()
    {
        Obj_12_Improper_And_Mixed.dialougue_image.SetActive(false);
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_12_ro_5_a.wav");
        Obj_12_Improper_And_Mixed.set_dialougue("Mixed Fractions and Improper Fractions are used to represent objects that are greater than 1. Here we have a pizza less than a whole.");
        Obj_12_Improper_And_Mixed.dialougue_ro5_1.SetActive(true);
    }
    void ReInforse_RO_5_1_b_wrong()
    {
        Obj_12_Improper_And_Mixed.dialougue_image.SetActive(false);
        Obj_12_Improper_And_Mixed.dialougue_ro5_2.SetActive(true);
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_12_ro_5_b.wav");
        Obj_12_Improper_And_Mixed.set_dialougue("We can represent this as \\frac{3}{3} which is an improper fraction but we cannot represent this pizza as a mixed fraction. Mixed fractions are used to represent objects more than 1");
    }







    public void EnableSubmitButtonRO6_1Obj12()
    {

        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(Submit_RO_6_1);
    }
    public void Submit_RO_6_1()
    {
        if (ans != null)
        {
            ansList.Add(ans);
            check_RO_6_1_ans();

        }

    }
    void check_RO_6_1_ans()
    {
        deselect_option();
        DisableSubmitButton();
        if (ans == "5")
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            //GameObject.FindObjectOfType<Obj4Manager>().UnHighLightAllBlocks();
            //  enableFade();
            Obj_12_Improper_And_Mixed.Obj12_RO_6_1.gameObject.SetActive(false);
            FindObjectOfType<timeline_new>().load_next();




        }
        else if (ans == "1"||ans=="4")
        {
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj12_let_see_why_common.wav");
                temp.GetComponent<Image>().color = Color.red;
                Invoke("ReInforse_RO_6_1_ad_wrong", 5);
                //ReInforse_RO_6_1_ad_wrong();
            }
        }
        else if (ans == "2"||ans=="3")
        {
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj12_let_see_why_common.wav");
                temp.GetComponent<Image>().color = Color.red;
                Invoke("ReInforse_RO_6_1_bc_wrong", 5);
                //ReInforse_RO_6_1_bc_wrong();
            }
        }
        else if (ans == "6")
        {
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj12_let_see_why_common.wav");
                temp.GetComponent<Image>().color = Color.red;
                Invoke("ReInforse_RO_6_1_f_wrong", 5);
                //ReInforse_RO_6_1_f_wrong();
            }
        }

        ans = "";
    }
    void ReInforse_RO_6_1_ad_wrong()
    {
        Obj_12_Improper_And_Mixed.dialougue_ro6_1_abcd.SetActive(true);
        Obj_12_Improper_And_Mixed.dialougue_image.SetActive(true);
        Obj_12_Improper_And_Mixed.dialougue_image.GetComponent<Image>().sprite = GameObject.Find("RO_figure").GetComponent<Image>().sprite;
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_12_ro_6_1_ad.wav");
        Obj_12_Improper_And_Mixed.set_dialougue("As you can see, the value of the figure is more than 1 and hence the figure can be represented both as an improper fraction and as a mixed fraction.");
    }
    void ReInforse_RO_6_1_bc_wrong()
    {
        Obj_12_Improper_And_Mixed.dialougue_ro6_1_abcd.SetActive(true);
        Obj_12_Improper_And_Mixed.dialougue_image.SetActive(true);
        Obj_12_Improper_And_Mixed.dialougue_image.GetComponent<Image>().sprite = GameObject.Find("RO_figure").GetComponent<Image>().sprite;
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_12_ro_6_1_bc.wav");
        Obj_12_Improper_And_Mixed.set_dialougue(" As you can see, the value of the figure is more than 1 and hence the figure can be represented both as an improper fraction and as a mixed fractions. ");
    }
    void ReInforse_RO_6_1_f_wrong()
    {
        Obj_12_Improper_And_Mixed.dialougue_image.SetActive(false);

        Debug.Log("move obj11_lo1");
        UtilityArtifacts.loading_pos = "Obj11_Lo1_from_obj12";
        UtilityArtifacts.coming_back_from = "to_Obj12_ro_last";
        UtilityArtifacts.backTraversal = true;
        UtilityArtifacts.comingbackafterTraversal = false;
        UtilityArtifacts.loadStartingpointforcomingback = 33;
        UtilityArtifacts.loadStartingpoint = 8;
        UtilityArtifacts.loadEndingpoint = 15;
        // load traversescene 11
        //SceneManager.LoadScene("Obj11");
        OnTraversal(148, 131);


        //GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_12_ro_6_1_f.wav");
        //Obj_12_Improper_And_Mixed.set_dialougue("Let's quickly revise what are proper fractions");
    }



    public void EnableSubmitButtonRO6_2Obj12()
    {

        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(Submit_RO_6_2);
    }
    public void Submit_RO_6_2()
    {
        if (ans != null)
        {
            ansList.Add(ans);
            check_RO_6_2_ans();

        }

    }
    void check_RO_6_2_ans()
    {
        deselect_option();
        DisableSubmitButton();
        if (ans == "1")
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            //GameObject.FindObjectOfType<Obj4Manager>().UnHighLightAllBlocks();
            //  enableFade();
            Obj_12_Improper_And_Mixed.Obj12_RO_6_2.gameObject.SetActive(false);
            FindObjectOfType<timeline_new>().load_next();




        }
        else if (ans == "2" || ans == "3"|| ans == "4")
        {
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj12_let_see_why_common.wav");
                temp.GetComponent<Image>().color = Color.red;
                Invoke("ReInforse_RO_6_2_wrong", 5);
                //ReInforse_RO_6_2_wrong();
            }
        }
       

        ans = "";
    }
    void ReInforse_RO_6_2_wrong()
    {
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_12_ro_6_2.wav");
        // Obj_12_Improper_And_Mixed.dialougue_image.SetActive(true);
        Obj_12_Improper_And_Mixed.dialougue_ro6_2.SetActive(true);
        Obj_12_Improper_And_Mixed.set_dialougue("The correct representation of 1 \\frac{1}{3} is here.The number line should be divided in such a way that the parts between the whole numbers should be divided as per the number in the denominator.  ");
    }
   






    public void EnableSubmitButtonRO6_3Obj12()
    {

        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(Submit_RO_6_3);
    }
    public void Submit_RO_6_3()
    {
        if (ans != null)
        {
            ansList.Add(ans);
            check_RO_6_3_ans();

        }

    }
    void check_RO_6_3_ans()
    {
        deselect_option();
        DisableSubmitButton();
        if (ans == "1")
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play(); 
            Obj_12_Improper_And_Mixed.Obj12_RO_6_3.gameObject.SetActive(false);
            GameObject.FindObjectOfType<GameManager>().OnGameOver();
            //Obj_12_Improper_And_Mixed.Exit_Panel.SetActive(true);



        }
        else if (ans == "2" || ans == "3" || ans == "4")
        {
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj12_let_see_why_common.wav");
                temp.GetComponent<Image>().color = Color.red;
                Invoke("ReInforse_RO_6_3_wrong", 5);
                //ReInforse_RO_6_3_wrong();
            }
        }

        ans = "";
    }
    void ReInforse_RO_6_3_wrong()
    {
      
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_12_ro_6_3.wav");
        // Obj_12_Improper_And_Mixed.dialougue_image.SetActive(true);
        Obj_12_Improper_And_Mixed.dialougue_ro6_3.SetActive(true);
        Obj_12_Improper_And_Mixed.set_dialougue(" The correct location of \\frac{7}{5} on the number line is here. The 2 points after \\frac{5}{5} ");

    }
    void OnTraversal(int objId, int subTopicId)
    {
        NewIncomingMsg mg = UtilityArtifacts.NJMsg;
        mg.current_activity_status = 1;
        mg.pre_req_id = subTopicId;//
        mg.pre_req_status = 1;
        mg.pre_req_reqData.error_obj_id = objId;
        UtilityArtifacts.UnityMsg = UtilityArtifacts.outGoingMessage(mg, "false");
        SceneManager.LoadScene(UtilityArtifacts.NJLoadScene);
    }
    void deselect_option()
    {
        foreach (GameObject b in GameObject.FindGameObjectsWithTag("Numbers"))
        {
            b.transform.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        }

    }
}
