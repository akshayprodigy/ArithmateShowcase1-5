using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Obj5_RO_Manager : MonoBehaviour
{
    public string ans;
    public List<string> ansList = new List<string>();
    public GameObject temp;
    QuestionManager questionManager;
    void OnEnable()
    {

    }
    public void Initiliaze()
    {
        EnableSubmitButtonRO1();
        //EnableSubmitButtonObj5_RO2();
        //EnableSubmitButtonObj5_ROFact1();
        //EnableSubmitButtonObj5_ROFact2();
        //EnableSubmitButtonObj5_ROFact3();

        GetallNumberButtons();
        GetOkButton();

    }

    public void Initiliaze_RO2()
    {
        GetallNumberButtons();
        GetOkButton();
        EnableSubmitButtonObj5_RO2();
    }
    public void Initiliaze_ROFact1()
    {
        GetallNumberButtons();
        GetOkButton();
        EnableSubmitButtonObj5_ROFact1();

    }
    public void Initiliaze_ROFact2()
    {
        EnableSubmitButtonObj5_ROFact2();
        GetallNumberButtons();
        GetOkButton();

    }
    public void Initiliaze_ROFact3()
    {
        GetallNumberButtons();
        GetOkButton();
        EnableSubmitButtonObj5_ROFact3();

    }


    public void GetallNumberButtons()
    {
        foreach (GameObject b in GameObject.FindGameObjectsWithTag("Numbers"))
        {
            b.GetComponent<Button>().onClick.AddListener(InputNumbers);
            Debug.Log("GetallNumberButtons__________________________");
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
            Debug.Log("InputNumbers__________________________");

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

    public void EnableSubmitButtonRO1()
    {
        Debug.Log("Enable for ro1");
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(Submit);

        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(ok);

    }
    public void Submit()
    {
        if (ans != null)
        {
            ansList.Add(ans);
            check_Lo1_ans();

        }

    }
    void check_Lo1_ans()
    {
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.SetActive(false);
        deselect_option();
        if (ans == "5")
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            //GameObject.FindObjectOfType<Obj4Manager>().UnHighLightAllBlocks();
            enableFade();
            reset_option();

        }
        else if (ans == "1")
        {
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj5_let_see_why_common.wav");
                temp.GetComponent<Image>().color = Color.red;
                Invoke("ReInforse_RO1_1", 5);
                //ReInforse_RO1_1();
            }
        }
        else if (ans == "2")
        {
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj5_let_see_why_common.wav");
                temp.GetComponent<Image>().color = Color.red;
                Invoke("ReInforse_RO1_2", 5);
                //ReInforse_RO1_2();
            }
        }
        else if (ans == "3")
        {
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj5_let_see_why_common.wav");
                temp.GetComponent<Image>().color = Color.red;
                Invoke("ReInforse_RO1_3", 5);
                //ReInforse_RO1_3();
            }
        }
        else if (ans == "4")
        {
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj5_let_see_why_common.wav");
                temp.GetComponent<Image>().color = Color.red;
                Invoke("ReInforse_RO1_4", 5);
                //ReInforse_RO1_4();
            }
        }
        else if (ans == "6")
        {
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj5_let_see_why_common.wav");
                temp.GetComponent<Image>().color = Color.red;
                Invoke("ReInforse_RO1_6", 5);
                //ReInforse_RO1_6();
            }
        }

    }
    public void ReInforse_RO1_1()
    {
        Debug.Log("Enable for reinfo1");
        //GameObject.Find("ROType1").transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Objects_in_Groups_too.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge(" Fractions can be used represent objects in groups too");

        //GameObject.Find("ROType1").transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
        //Invoke("convesationDisable", 10.0f);

    }
    public void ReInforse_RO1_2()
    {
        Debug.Log("Enable for reinfo1");
        //GameObject.Find("ROType1").transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Both_Objects_in_Groups.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("Fractions can be used to represent both objects in a group as well as a part of a whole object.");

        //GameObject.Find("ROType1").transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
        //Invoke("convesationDisable", 10.0f);

    }
    public void ReInforse_RO1_3()
    {
        Debug.Log("Enable for reinfo1");
        //GameObject.Find("ROType1").transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Both_Objects_in_Groups.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("Fractions can be used to represent both objects in a group as well as a part of a whole object.");

        //GameObject.Find("ROType1").transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
        //Invoke("convesationDisable", 10.0f);

    }
    public void ReInforse_RO1_4()
    {
        Debug.Log("Enable for reinfo1");
        //GameObject.Find("ROType1").transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("number_of_objects_considered.wav");
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(0).GetChild(3).GetChild(0).gameObject.SetActive(true);
        //GameObject.FindObjectOfType<conversationManager>().EnableDialouge("The number of objects considered/ chosen is the numerator and number of total objects in the group is denominator while representing an object in a group as fractions. " + "\\frac{Number of objects considered}{total number of objects in the group} = " + "\\frac{Numerator}{Denominator}  ");

        //GameObject.Find("ROType1").transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
        //Invoke("convesationDisable", 10.0f);

    }
    public void ReInforse_RO1_6()
    {
        Debug.Log("Enable for reinfo1");
        //GameObject.Find("ROType1").transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Both_Objects_in_Groups.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("Fractions can be used to represent both objects in a group as well as a part of whole object.");

        //GameObject.Find("ROType1").transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
        //Invoke("convesationDisable", 10.0f);

    }


    public void EnableSubmitButtonObj5_RO2()
    {
        GetallNumberButtons();
        Debug.Log("Enable for ro2_Obj5");
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(SubmitRO2);

        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(ok);
    }
    public void SubmitRO2()
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
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.SetActive(false);

        if (ans == "1" || ans == "2")
        {
            //GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            //enableFade();
            GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj5_let_see_why_common.wav");
            temp.GetComponent<Image>().color = Color.red;
            Invoke("ReInforse_RO2_1", 5);
            //ReInforse_RO2_1();

        }
        else if (ans == "3" || ans == "4")
        {
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj5_let_see_why_common.wav");
                temp.GetComponent<Image>().color = Color.red;
                Invoke("ReInforse_RO2_2", 5);
                //ReInforse_RO2_2();
            }
        }

    }
    public void ReInforse_RO2_1()
    {
        Debug.Log("Enable for reinfo2_1");
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj5_groups_as_a_fraction.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("You can represent an object in a set of objects just like you can represent a group in a set of groups as a fraction. ");

    }
    public void ReInforse_RO2_2()
    {
        Debug.Log("Enable for reinfo2_2");
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj5_group_among_other_groups.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("The number of groups considered/ chosen from a set of groups is numerator and total number of groups is denominator while representing a group among other groups.");

    }


    public void EnableSubmitButtonObj5_ROFact1()
    {
        Debug.Log("Enable for _ROFact1");
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(SubmitROFact_1);

        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(ok);

        //GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        //GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(EnableSubmitButtonObj5_ROFact2);
    }
    public void SubmitROFact_1()
    {
        if (ans != null)
        {
            ansList.Add(ans);
            check_ROFact_1_ans();

        }

    }
    void check_ROFact_1_ans()
    {
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.SetActive(false);
        deselect_option();
        if (ans == "4")
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            //enableFade();
            GameObject.FindObjectOfType<timeline_new>().load_next();
            reset_option();

        }
        else if (ans == "1" || ans == "2" || ans == "3")
        {
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj5_let_see_why_common.wav");
                temp.GetComponent<Image>().color = Color.red;
                Invoke("ReInforse_ROFact_1", 5);
                //ReInforse_ROFact_1();
            }
        }
    }
    public void ReInforse_ROFact_1()
    {
        Debug.Log("Enable for reinfo2_1");
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj5_Fractions_can_be_used.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("We can use fractions to represent a part of group of objects. Fractions can be used not only for part of a whole but also for a group of objects. ");

    }


    public void EnableSubmitButtonObj5_ROFact2()
    {
        Debug.Log("Enable for _ROFact2");
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(SubmitROFact_2);

        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(ok);

        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(3).GetChild(4).GetChild(0).GetChild(6).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(3).GetChild(4).GetChild(0).GetChild(6).gameObject.GetComponent<Button>().onClick.AddListener(ok);

        //GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        //GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(EnableSubmitButtonObj5_ROFact3);

    }
    public void SubmitROFact_2()
    {

        if (ans != null)
        {
            ansList.Add(ans);
            check_ROFact_2_ans();

        }

    }
    void check_ROFact_2_ans()
    {
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.SetActive(false);
        deselect_option();
        if (ans == "1")
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            GameObject.FindObjectOfType<timeline_new>().load_next();
            //enableFade();
            reset_option();

        }
        else if (ans == "4" || ans == "2" || ans == "3")
        {
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj5_let_see_why_common.wav");
                temp.GetComponent<Image>().color = Color.red;
                Invoke("ReInforse_FractionsCanBeUsed", 5);
                //ReInforse_FractionsCanBeUsed();
            }
        }
    }
    public void ReInforse_FractionsCanBeUsed()
    {
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj5_Fractions_can_be_used.wav");
        //GameObject.FindObjectOfType<conversationManager>().EnableDialouge("We can use fractions to represent a part of group of objects. Fractions can be used not only for part of a whole but also for a group of objects. Fractions can represent a part of a group of objects");
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(3).GetChild(4).GetChild(0).gameObject.SetActive(true);
    }


    public void EnableSubmitButtonObj5_ROFact3()
    {
        Debug.Log("Enable for _ROFact3");
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(SubmitROFact_3);

        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(ok1);

    }
    public void SubmitROFact_3()
    {
        if (ans != null)
        {
            ansList.Add(ans);
            check_ROFact_3_ans();

        }

    }
    void check_ROFact_3_ans()
    {
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.SetActive(false);
        if (ans == "2")
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            //enableFade();
            //ok1();
            GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
            Invoke("animStop", 2.5f);
        }
        else if (ans == "1" || ans == "4" || ans == "3")
        {
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj5_let_see_why_common.wav");
                temp.GetComponent<Image>().color = Color.red;
                Invoke("ReInforse_ROFact_3", 5);
                //ReInforse_ROFact_3();
            }
        }
    }
    public void ReInforse_ROFact_3()
    {
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj5_group_does_not_matter.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("We can use fractions to represent one group of objects among other groups of objects. The number of objects in the group does not matter as we need to find what fraction of the given groups have red apples");

    }


    public void EnableSubmitButton_DropApple()
    {
        Debug.Log("Enable for ro1");
        GameObject.Find("SubmitButton").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("SubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("SubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(Submit_DropApple);
        GameObject.FindObjectOfType<Obj5Drag>().dragOnSurfaces = true;

    }
    public void Submit_DropApple()
    {
        if (ans != null)
        {
            ansList.Add(ans);
            check_DropApple_ans();
            //GameObject.Find("SubmitButton").transform.GetChild(0).gameObject.SetActive(false);
            //GameObject.Find("Canvas (1)").transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        }

        //Test
        //GameObject.Find("SubmitButton").transform.GetChild(0).gameObject.SetActive(true);

    }
    public void check_DropApple_ans()
    {
        //deselect_option();
        //=============================

        //return;
        //=============================


        //if (Obj5Manager.correctDraggedApples == AppleManager.totalWholeAppleCollected)
        //if (AppleManager.totalWholeAppleCollected == 0)
        //{

        GameObject.FindObjectOfType<QuestionManager>().CalculateAppleObj5();
        if ((GameObject.FindObjectOfType<QuestionManager>().isTray1) && (GameObject.FindObjectOfType<QuestionManager>().isTray2) &&
            (GameObject.FindObjectOfType<QuestionManager>().isTray3))
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();

            //GameObject.Find("FractionInputPanel").transform.GetChild(0).gameObject.SetActive(false);
            //GameObject.Find("FractionInputPanel").transform.GetChild(1).gameObject.SetActive(false);
            //GameObject.Find("NumberPanel").transform.GetChild(0).gameObject.SetActive(false);

            //GameObject.FindObjectOfType<QuestionManager>().DoneButton.onClick.RemoveListener(GameObject.FindObjectOfType<QuestionManager>().DoneForObj2Quest1);
            //GameObject.FindObjectOfType<Obj4Manager>().UnHighLightAllBlocks();

            //enableFade_1();
            GameObject.Find("Canvas (1)").transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            GameObject.FindObjectOfType<timeline_new>().load_next();
            GameObject.Find("Canvas (1)").transform.GetChild(0).GetComponent<Animator>().SetBool("Play", true);
            GameObject.Find("Canvas (1)").transform.GetChild(0).GetChild(0).transform.gameObject.SetActive(false);
            GameObject.Find("SubmitButton").transform.GetChild(0).gameObject.SetActive(true);

        }

        //}
        else
        {
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                //ReInforse_RO_AppleDrop(); //new add reinforncement
                GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj5_let_see_why_common.wav");
                temp.GetComponent<Image>().color = Color.red;
                Invoke("ReInforse_RO_AppleDrop", 5);
                GameObject.Find("SubmitButton").transform.GetChild(0).gameObject.SetActive(false);


            }
        }
    }
    void ReInforse_RO_AppleDrop()
    {
        FindObjectOfType<timeline_new>().playAudioOnRelearn("unable_pack_apples_as_req.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableConversation(" You have to pack the apples of the same colour in each case. I am sure you can do a better job. Try one more time");
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).GetComponent<Button>().onClick.AddListener(DisableHinT_AppleDrop);
        GameObject.Find("SubmitButton").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("Canvas (1)").transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        Invoke("DisableConvo_ForAppleQuestionHints", GameObject.FindObjectOfType<timeline_new>().lapa.length_of_audio + 1.5f);


    }

    void DisableConvo_ForAppleQuestionHints()
    {
        GameObject.FindObjectOfType<conversationManager>().DisableConversation();
        GameObject.Find("SubmitButton").transform.GetChild(0).gameObject.SetActive(true);

    }
    void DisableHinT_AppleDrop()
    {
        GameObject.Find("Dialouge").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.FindObjectOfType<Obj5Drag>().dragOnSurfaces = true;
        GameObject.Find("SubmitButton").transform.GetChild(0).gameObject.SetActive(true);
        Debug.Log("Apple Drag_______________________");
        GameObject.FindObjectOfType<QuestionManager>().ResetApples_DragNDrop();



        //Test 
        //Check the apple case each should have 2 apples and same color instead of these below 2 lines
        //GameObject.FindObjectOfType<timeline_new>().load_next();
        //GameObject.FindObjectOfType<QuestionManager>().Obj5_LO2_Question2();
        //Test

        //GameObject.FindObjectOfType<QuestionManager>().calculateApplesInEachTray();
        //if (GameObject.FindObjectOfType<QuestionManager>().noOfAppplesinTray1 == 2 && questionManager.noOfAppplesinTray2 == 2 && questionManager.noOfAppplesinTray3 == 2)
        //{
        //    GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();

        //    GameObject.Find("FractionInputPanel").transform.GetChild(0).gameObject.SetActive(false);
        //    GameObject.Find("FractionInputPanel").transform.GetChild(1).gameObject.SetActive(false);
        //    GameObject.Find("NumberPanel").transform.GetChild(0).gameObject.SetActive(false);
        //    GameObject.FindObjectOfType<timeline_new>().load_next();
        //    GameObject.Find("SubmitButton").transform.GetChild(0).GetComponent<Button>().onClick.RemoveListener(GameObject.FindObjectOfType<QuestionManager>().Obj5_LO2_Question2);
        //    DisableSubmitButton();
        //    GameObject.Find("Canvas (1)").transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        //}
        //else
        //{
        //    GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
        //    GameObject.FindObjectOfType<conversationManager>().EnableDialouge("You have pack the apples of the same colour in each case. I am sure you can do a better job. Try one more time");
        //    GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        //    GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(DisableDialogue_AfterDragApple);
        //    GameObject.Find("Canvas (1)").transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        //}
    }
    void DisableDialogue_AfterDragApple()
    {
        GameObject.Find("Dialouge").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("SubmitButton").transform.GetChild(0).gameObject.SetActive(true);
    }

    //public void EnableSubmitButtonRO2()
    //{
    //    Debug.Log("Enable for ro2");
    //    GetallNumberButtons();
    //    GameObject.Find("ROSubmitButton").transform.GetChild(0).GetComponent<Button>().onClick.RemoveListener(Submit);
    //    GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(SubmitObj4Q2); ;
    //}
    //public void SubmitObj4Q2()
    //{
    //    if (ans != null)
    //    {
    //        ansList.Add(ans);
    //        check_Lo2_ans1();


    //    }
    //}
    //void check_Lo2_ans1()
    //{
    //    if (ans == "2")
    //    {
    //        GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
    //        enableFade();

    //    }

    //    else
    //    {
    //        GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
    //        ReInforse_LO2_ans1();
    //    }

    //}
    //public void ReInforse_LO2_ans1()
    //{
    //    Debug.Log("Enable for reinfo2");
    //    GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj4_Quest3_RO_Hint1.wav");
    //    GameObject.FindObjectOfType<conversationManager>().EnableDialouge("");
    //    GameObject.Find("Dialouge").transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
    //    Invoke("ReInforse1_LO2_ans1", 4.0f);

    //}
    //public void ReInforse1_LO2_ans1()
    //{
    //    Debug.Log("Enable for reinfo2.1");
    //    GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj4_Quest3_RO_Hint2.wav");
    //    GameObject.FindObjectOfType<conversationManager>().EnableDialouge("");

    //}
    void ok()
    {
        CancelInvoke();
        convesationDisable();
        reset_option();

    }
    void ok1()
    {
        CancelInvoke();
        lastConvesationDisable();
    }
    void convesationDisable()
    {
        FindObjectOfType<timeline_new>().lipsync_player.GetComponent<AudioSource>().Stop();
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(3).GetChild(4).GetChild(0).gameObject.SetActive(false);
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(0).GetChild(3).GetChild(0).gameObject.SetActive(false);

        GameObject.FindObjectOfType<conversationManager>().DisableDialouge();
        enableFade();
    }
    void lastConvesationDisable()
    {
        FindObjectOfType<timeline_new>().lipsync_player.GetComponent<AudioSource>().Stop();
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
        //GameObject.FindObjectOfType<GameManager>().OnGameOver();
        ExitNow();
        //GameObject.Find("ExitPanel").transform.GetChild(0).gameObject.SetActive(true);
        //GameObject.Find("ExitPanel").transform.GetChild(0).GetChild(0).GetComponent<Button>().onClick.AddListener(ExitNow);
    }
    void ExitNow()
    {
        //Application.Quit();
        GameObject.FindObjectOfType<Obj5Manager>().VisualQTYpe_GroupObjects.SetActive(true);


        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadeout");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    void enableFade1()
    {
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
        Invoke("ExitNow", 3.0f);
    }
    void enableFade()
    {

        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
        if (UtilityArtifacts.loading_pos == "Obj5_Lo1_from_obj6")
        {
            UtilityArtifacts.loading_pos = "";
            UtilityArtifacts.backTraversal = false;
            UtilityArtifacts.comingbackafterTraversal = true;
            UtilityArtifacts.loadStartingpoint = 0;
            UtilityArtifacts.loadEndingpoint = 0;
            // load traversescene 16
            //SceneManager.LoadScene("Obj6NumberLine");
            OnPreRequisitOver();
        }
        else
        {
            Invoke("nextObjective1", 3.0f);
        }

    }

    void OnPreRequisitOver()
    {
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
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadeout");
        //enabledPanel.transform.parent.gameObject.SetActive(false);
        enabledPanel.transform.GetChild(0).gameObject.SetActive(false);
        GameObject.FindObjectOfType<timeline_new>().load_next();

    }
    void enableFade_1()
    {
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
        Invoke("nextObjective2", 3.0f);
    }
    void nextObjective2()
    {

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
    //public void EnableSubmitButtonRO3()
    //{
    //    Debug.Log("Enable for ro2");
    //    GetallNumberButtons();
    //    GameObject.Find("ROSubmitButton").transform.GetChild(0).GetComponent<Button>().onClick.RemoveListener(SubmitObj4Q2);
    //    GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(SubmitObj4Q3); ;
    //}

    //public void SubmitObj4Q3()
    //{
    //    if (ans != null)
    //    {
    //        ansList.Add(ans);
    //        check_Lo2_ans2();
    //    }
    //}

    //void check_Lo2_ans2()
    //{
    //    if (ans == "5")
    //    {
    //        GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
    //        enableFade();
    //    }

    //    else
    //    {
    //        GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
    //        ReInforse_LO2_ans2();
    //    }

    //}
    //public void ReInforse_LO2_ans2()
    //{
    //    Debug.Log("Enable for reinfo2");
    //    GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj4_Quest4_RO_Hint1.wav");
    //    GameObject.FindObjectOfType<conversationManager>().EnableDialouge("Only the total number of parts should be considered and not which parts are considered.Here, all shapes have 2 parts coloured out of 4 total parts.Hence all 4 shapes are 2 / 4. ");
    //    //Invoke("convesationDisable", 15.0f);
    //}

    //public void EnableSubmitButtonRO4()
    //{
    //    Debug.Log("Enable for ro4");
    //    GetallNumberButtons();
    //    GameObject.Find("ROSubmitButton").transform.GetChild(0).GetComponent<Button>().onClick.RemoveListener(SubmitObj4Q3);
    //    GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(SubmitObj4Q4); ;
    //}
    //public void SubmitObj4Q4()
    //{
    //    if (ans != null)
    //    {
    //        ansList.Add(ans);
    //        check_Lo2_ans4();
    //    }
    //}
    //void check_Lo2_ans4()
    //{
    //    if (ans == "4")
    //    {
    //        GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
    //        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
    //        Invoke("animStop", 2.5f);
    //    }

    //    else
    //    {
    //        GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
    //        ReInforse_LO2_ans4();
    //    }

    //}
    //public void ReInforse_LO2_ans4()
    //{
    //    Debug.Log("Enable for reinfo2");
    //    GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj4_Quest6_RO_Hint1.wav");
    //    GameObject.FindObjectOfType<conversationManager>().EnableDialouge("Only the total number of parts should be considered and not which parts are considered.Here, all shapes have 2 parts coloured out of 4 total parts.Hence all 4 shapes are 2 / 4. ");

    //    Invoke("lastConvesationDisable", 10.0f);

    //}


    void reset_option()
    {
        foreach (GameObject b in GameObject.FindGameObjectsWithTag("Numbers"))
        {
            b.transform.GetChild(2).gameObject.SetActive(false);
        }
        ans = "";
    }


}
