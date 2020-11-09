using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class QuestionManager : MonoBehaviour
{

    public bool isNum, isDenum, isWhole, isLO2_Num, isLO3_Den;
    public Button DoneButton;
    public string FuncrionName, ans;
    public int numberOfAttempt = 0;
    public bool isTray1, isTray2, isTray3;
    public GameObject CountingFlashcard, countingOkButton, beforeFlashCard;

    // For objective2
    public int noOfAppplesinTray1, noOfAppplesinTray2, noOfAppplesinTray3;

    void Start()
    {
        initialize();
    }
    void initialize()
    {
        CountingFlashcard = GameObject.Find("CountingFlashCard");
        countingOkButton = GameObject.Find("countingOkButton");
        CountingFlashcard.SetActive(false);
        countingOkButton.GetComponent<Button>().onClick.AddListener(() => OnFlashSubmit());
        beforeFlashCard = GameObject.Find("LoadFlashCard");
        HideFlashCard();
    }
    void ShowBeforeFashCard()
    {
        beforeFlashCard.SetActive(true);
        Invoke("setUpFlashCard", 5);
    }

    void setUpFlashCard()
    {
        CountingFlashcard.SetActive(true);
        HideFlashCard();
    }

    void HideFlashCard()
    {
        beforeFlashCard.SetActive(false);
    }
    void OnFlashSubmit()
    {
        CountingFlashcard.SetActive(false);
        //GameObject.FindObjectOfType<timeline_new>().load_next();
    }
    // Start is called before the first frame update
    public void EnableSubmitButton()
    {
        GameObject.Find("SubmitButton").transform.GetChild(0).gameObject.SetActive(true);
        DoneButton = GameObject.Find("SubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>();
    }
    public void DisableSubmitButton()
    {
        GameObject.Find("SubmitButton").transform.GetChild(0).gameObject.SetActive(false);
    }
    public void EnableForObj3Quest1()
    {
        numberOfAttempt = 0;
        FindObjectOfType<Obj3Manager>().condition = "num_denum";
        GameObject.Find("NumberPanel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("FractionInputPanel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("FractionInputPanel").transform.GetChild(2).gameObject.SetActive(true);
        // GameObject.Find("FractionInputPanel").transform.GetChild(0).position = GameObject.Find("FractionInputPanel").transform.GetChild(2).position;
        GameObject.Find("FractionInputPanel").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(ClickOnNumerator);
        ClickOnNumerator();
    }
    public void DoneForObj3Quest1()
    {

        DisableSubmitButton();
        if (GameObject.Find("FractionInputPanel").transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text == "6")
        {
            GameObject.Find("FractionInputPanel").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
            GameObject.Find("FractionInputPanel").transform.GetChild(0).position = GameObject.Find("FractionInputPanel").transform.GetChild(3).position;
            GameObject.Find("FractionInputPanel").transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("NumberPanel").transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("FractionInputPanel").transform.GetChild(0).GetComponent<Button>().enabled = false;
            DoneButton.gameObject.SetActive(false);
            FindObjectOfType<Obj3Manager>().condition = "";
            GameObject.FindObjectOfType<timeline_new>().load_next();
            DisableForObj3Quest2();
            FindObjectOfType<conversationManager>().playCorrect();
        }
        else
        {
            if (numberOfAttempt < 3)
            {
                numberOfAttempt++;
                FindObjectOfType<conversationManager>().playError();
                FindObjectOfType<timeline_new>().playAudioOnRelearn("exp_answer_a.wav");
                FindObjectOfType<Obj3Manager>().Dialouge_text.GetComponent<Text>().text = " You can do a better job in counting than this. Give it another try.";
                FindObjectOfType<Obj3Manager>().enable_panel(FindObjectOfType<Obj3Manager>().Dialouge_panel);
                //   FindObjectOfType<Obj3Manager>().StartCoroutine(FindObjectOfType<Obj3Manager>().disable_afte_timer(FindObjectOfType<Obj3Manager>().Dialouge_panel, 8f));
                //    Invoke("EnableSubmitButton", 8);
                GameObject.Find("FractionInputPanel").transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = "";
            }
            else
            {
                numberOfAttempt = 0;
                Debug.Log("counting flash card for obj3 quest1");
                //CountingFlashcard.SetActive(true);
                ShowBeforeFashCard();
                //GameObject.Find("FractionInputPanel").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                //GameObject.Find("FractionInputPanel").transform.GetChild(0).position = GameObject.Find("FractionInputPanel").transform.GetChild(3).position;
                //GameObject.Find("FractionInputPanel").transform.GetChild(0).gameObject.SetActive(false);
                //GameObject.Find("NumberPanel").transform.GetChild(0).gameObject.SetActive(false);
                //GameObject.Find("Dialougue Panel").transform.GetChild(0).gameObject.SetActive(false);
                //GameObject.Find("FractionInputPanel").transform.GetChild(0).GetComponent<Button>().enabled = false;
                //DoneButton.gameObject.SetActive(false);
                //FindObjectOfType<Obj3Manager>().condition = "";

                //DisableForObj3Quest2();
                //UtilityArtifacts.loading_pos = "Obj4_Lo1";
                //UtilityArtifacts.coming_back_from = "quest1";
                //SceneManager.LoadScene("Obj4AreaModule");
            }
        }

    }

    public void EnableForObj3Quest2()
    {
        numberOfAttempt = 0;
        FindObjectOfType<Obj3Manager>().condition = "num_denum";
        GameObject.Find("FractionInputPanel").transform.GetChild(1).gameObject.SetActive(true);
        //GameObject.Find("FractionInputPanel").transform.GetChild(1).position = GameObject.Find("FractionInputPanel").transform.GetChild(2).position;

        GameObject.Find("FractionInputPanel").transform.GetChild(1).gameObject.GetComponent<Button>().onClick.AddListener(ClickOnDenominator);
        GameObject.Find("NumberPanel").transform.GetChild(0).gameObject.SetActive(true);
        GetallNumberButtons();
        ClickOnDenominator();
    }
    public void DisableForObj3Quest2()
    {
        GameObject.Find("FractionInputPanel").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("FractionInputPanel").transform.GetChild(1).gameObject.SetActive(false);
        GameObject.Find("FractionInputPanel").transform.GetChild(2).gameObject.SetActive(false);
    }
    public void DoneForObj3Quest2()
    {

        DisableSubmitButton();

        if (GameObject.Find("FractionInputPanel").transform.GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text == "12")
        {
            GameObject.Find("FractionInputPanel").transform.GetChild(1).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
            GameObject.Find("FractionInputPanel").transform.GetChild(1).position = GameObject.Find("FractionInputPanel").transform.GetChild(4).position;
            //  GameObject.Find("NumberPanel").transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("FractionInputPanel").transform.GetChild(1).gameObject.GetComponent<Button>().enabled = false;
            FindObjectOfType<conversationManager>().playCorrect();
            GameObject.FindObjectOfType<timeline_new>().load_next();
        }
        else
        {
            if (numberOfAttempt < 3)
            {
                FindObjectOfType<conversationManager>().playError();
                FindObjectOfType<timeline_new>().playAudioOnRelearn("exp_answer_a.wav");
                FindObjectOfType<Obj3Manager>().Dialouge_text.GetComponent<Text>().text = " You can do a better job in counting than this. Give it another try.";
                FindObjectOfType<Obj3Manager>().enable_panel(FindObjectOfType<Obj3Manager>().Dialouge_panel);
                GameObject.Find("FractionInputPanel").transform.GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text = "";
                numberOfAttempt++;
            }
            else
            {
                numberOfAttempt = 0;
                Debug.Log("counting flash card for obj3 quest1");
                //CountingFlashcard.SetActive(true);
                ShowBeforeFashCard();
                //GameObject.Find("Dialougue Panel").transform.GetChild(0).gameObject.SetActive(false);
                //GameObject.Find("FractionInputPanel").transform.GetChild(1).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                //GameObject.Find("FractionInputPanel").transform.GetChild(1).position = GameObject.Find("FractionInputPanel").transform.GetChild(4).position;
                ////  GameObject.Find("NumberPanel").transform.GetChild(0).gameObject.SetActive(false);
                //GameObject.Find("FractionInputPanel").transform.GetChild(1).gameObject.GetComponent<Button>().enabled = false;
                //UtilityArtifacts.loading_pos = "Obj4_Lo1";
                //UtilityArtifacts.coming_back_from = "quest1";
                //SceneManager.LoadScene("Obj4AreaModule");
            }

        }

    }

    public void ClickOnNumerator()
    {
        isNum = true;
        isDenum = false;
        isWhole = false;
        GameObject.Find("FractionInputPanel").transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = "";
    }
    public void ClickOnDenominator()
    {
        isNum = false;
        isDenum = true;
        isWhole = false;
        GameObject.Find("FractionInputPanel").transform.GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text = "";
    }
    public void ClickOnWhole()
    {
        isNum = false;
        isDenum = false;
        isWhole = true;
        GameObject.Find("FractionInputPanel").transform.GetChild(3).GetChild(0).gameObject.GetComponent<Text>().text = "";
    }
    public void ClickOn_LO2_Numerator()
    {
        isNum = false;
        isDenum = false;
        isWhole = false;
        isLO2_Num = true;
        GameObject.Find("FractionInputPanel").transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = "";
    }
    public void ClickOn_LO3_Numerator()
    {
        isNum = false;
        isDenum = false;
        isWhole = false;
        isLO2_Num = false;
        isLO3_Den = true;

        GameObject.Find("FractionInputPanel").transform.GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text = "";
    }
    // Objective 13
    public void EnableForObj13Quest()
    {
        GameObject.Find("NumberPanel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("FractionInputPanel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("FractionInputPanel").transform.GetChild(1).gameObject.SetActive(true);
        GameObject.Find("FractionInputPanel").transform.GetChild(2).gameObject.SetActive(true);
        GameObject.Find("FractionInputPanel").transform.GetChild(3).gameObject.SetActive(true);
        GameObject.Find("FractionInputPanel").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(ClickOnNumerator);
        GameObject.Find("FractionInputPanel").transform.GetChild(1).gameObject.GetComponent<Button>().onClick.AddListener(ClickOnDenominator);
        GameObject.Find("FractionInputPanel").transform.GetChild(3).gameObject.GetComponent<Button>().onClick.AddListener(ClickOnWhole);


        EnableSubmitButton();

        //ClickOnWhole();
        EmptyNumDenumBox();
        GetallNumberButtonsObj13();
    }
    public void GetallNumberButtonsObj13()
    {
        foreach (GameObject b in GameObject.FindGameObjectsWithTag("Numbers"))
        {
            b.GetComponent<Button>().onClick.AddListener(InputNumbersObj13);
        }
    }
    public void InputNumbersObj13()
    {
        if (GameObject.Find("Chef conversation").transform.GetChild(0).gameObject.activeSelf)
        {
            GameObject.FindObjectOfType<conversationManager>().DisableConversation();
        }
        var currentEventSystem = EventSystem.current;
        var currentSelectedGameObject = currentEventSystem.currentSelectedGameObject;

        DoneButton.onClick.AddListener(DoneForObj13Quest1);
        if (isNum)
        {
            Debug.Log("numerator =" + currentSelectedGameObject.name);
            GameObject.Find("FractionInputPanel").transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = GameObject.Find("FractionInputPanel").transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text + currentSelectedGameObject.name;


        }

        if (isDenum)
        {
            Debug.Log("denominator =" + currentSelectedGameObject.name);
            GameObject.Find("FractionInputPanel").transform.GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text = GameObject.Find("FractionInputPanel").transform.GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text + currentSelectedGameObject.name;


        }
        if (isWhole)
        {
            Debug.Log("whole =" + currentSelectedGameObject.name);
            GameObject.Find("FractionInputPanel").transform.GetChild(3).GetChild(0).gameObject.GetComponent<Text>().text = GameObject.Find("FractionInputPanel").transform.GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text + currentSelectedGameObject.name;

        }


    }
    void DoneForObj13Quest1()
    {
        Debug.Log("Quest");
        checkObj13Ans1();
    }
    void checkObj13Ans1()
    {

        if (GameObject.Find("FractionInputPanel").transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text == "1" && GameObject.Find("FractionInputPanel").transform.GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text == "2" && GameObject.Find("FractionInputPanel").transform.GetChild(3).GetChild(0).gameObject.GetComponent<Text>().text == "3")
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();

            GameObject.Find("FractionInputPanel").transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("FractionInputPanel").transform.GetChild(1).gameObject.SetActive(false);
            GameObject.Find("NumberPanel").transform.GetChild(0).gameObject.SetActive(false);
            GameObject.FindObjectOfType<timeline_new>().load_next();
            DoneButton.onClick.RemoveListener(DoneForObj13Quest1);
            DisableSubmitButton();
        }
        else
        {
            if (numberOfAttempt < 1)
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                ReInforse_O13Q1H1();
                numberOfAttempt++;
                DisableSubmitButton();

            }
            else if (numberOfAttempt == 1)
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                ReInforse_O13Q1H2();
                numberOfAttempt++;
                DisableSubmitButton();

            }
            else
            {

                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();

                //ReInforse_O13Q1H2();
                numberOfAttempt = 0;
                UtilityArtifacts.loading_pos = "Obj12_Lo1_from_obj13";
                UtilityArtifacts.coming_back_from = "to_Obj13_quest1";
                UtilityArtifacts.backTraversal = true;
                UtilityArtifacts.comingbackafterTraversal = false;
                UtilityArtifacts.loadStartingpointforcomingback = 5;
                UtilityArtifacts.loadStartingpoint = 3;
                UtilityArtifacts.loadEndingpoint = 12;
                DisableSubmitButton();
                //SceneManager.LoadScene("obj_12_improper_and_mixed");
                OnTraversal(149, 131);
                //load scene 12

            }

        }
    }
    void ReInforse_O13Q1H1()
    {

        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_13_wrong.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("Let me remind you of mixed fractions – They are made of whole number and a proper fraction");
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(okQuest13);
    }
    void ReInforse_O13Q1H2()
    {

        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_13_hint1.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("Check how many whole pizzas are there and write it in this box and the remaining pizza can be represented in terms of fraction in these boxes ");
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(okQuest13_last);
    }
    void okQuest13()
    {
        CancelInvoke();
        EnableSubmitButton();
        EmptyNumDenumBox();
        ClickOnWhole();
        GameObject.FindObjectOfType<conversationManager>().DisableDialouge();
    }
    void okQuest13_last()
    {
        CancelInvoke();

        GameObject.FindObjectOfType<conversationManager>().DisableDialouge();
        GameObject.FindObjectOfType<timeline_new>().load_next();
    }

    // for Objective 11

    public void checkObj11Ans1()
    {
        if (GameObject.FindObjectOfType<Obj11Manager>().eightPiecePizza == 2 &&
            GameObject.FindObjectOfType<Obj11Manager>().SixPiecePizza == 0 &&
            GameObject.FindObjectOfType<Obj11Manager>().threePiecePizza == 0)
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            GameObject.Find("SubmitButton").transform.GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
            GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(ok);

            DisableSubmitButton();
            ok();
        }
        else if (GameObject.FindObjectOfType<Obj11Manager>().eightPiecePizza != 2 &&
                  GameObject.FindObjectOfType<Obj11Manager>().SixPiecePizza == 0 &&
                  GameObject.FindObjectOfType<Obj11Manager>().threePiecePizza == 0)
        {
            if (numberOfAttempt == 0)
            {
                numberOfAttempt = numberOfAttempt + 1;
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();

                GameObject.FindObjectOfType<conversationManager>().EnableDialouge("The customer wants \\frac{2}{8} of a pizza. Ensure that the fraction and the pizza packed match.");
                GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(ok1);
                GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj11_Quest1_Hint1.wav");
            }
            else if (numberOfAttempt == 1)
            {
                numberOfAttempt = numberOfAttempt + 1;
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();

                GameObject.FindObjectOfType<conversationManager>().EnableDialouge("The number of pizza slices to be packed should be according to the fraction given. Check again.");
                GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(ok1);
                GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj11_Quest1_Hint1_1.wav");
            }
            else
            {
                numberOfAttempt = 0;
                UtilityArtifacts.loading_pos = "Obj4_Lo1_from_obj11";
                UtilityArtifacts.coming_back_from = "to_Obj11_quest1";
                UtilityArtifacts.backTraversal = true;
                UtilityArtifacts.comingbackafterTraversal = false;
                UtilityArtifacts.loadStartingpointforcomingback = 6;
                UtilityArtifacts.loadEndingpoint = 11;
                // load traversescene 4
                //SceneManager.LoadScene("Obj4AreaModule");
                OnTraversal(155, 129);
            }

            DisableSubmitButton();
        }
        else if (GameObject.FindObjectOfType<Obj11Manager>().eightPiecePizza == 0 &&
                 (GameObject.FindObjectOfType<Obj11Manager>().SixPiecePizza == 2 ||
                 GameObject.FindObjectOfType<Obj11Manager>().threePiecePizza == 2))
        {
            if (numberOfAttempt == 0)
            {
                numberOfAttempt = numberOfAttempt + 1;
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();

                GameObject.FindObjectOfType<conversationManager>().EnableDialouge("Check if you are taking 2 slices of pizza from the correct pizza.");
                GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(ok1);
                GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj11_Quest1_Hint2.wav");
            }
            else if (numberOfAttempt == 1)
            {
                numberOfAttempt++;
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();

                GameObject.FindObjectOfType<conversationManager>().EnableDialouge("The customer wants \\frac{2}{8} of a pizza. Ensure that the denominator of the fraction and the total number of slices in the pizza selected match.");
                GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(ok1);
                GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj11_Quest1_Hint2_1.wav");
            }
            else
            {
                numberOfAttempt = 0;
                UtilityArtifacts.loading_pos = "Obj4_Lo1_from_obj11";
                UtilityArtifacts.coming_back_from = "to_Obj11_quest1";
                UtilityArtifacts.backTraversal = true;
                UtilityArtifacts.comingbackafterTraversal = false;
                UtilityArtifacts.loadStartingpointforcomingback = 6;
                // load traversescene 4
                //SceneManager.LoadScene("Obj4AreaModule");
                OnTraversal(155, 129);
            }
            DisableSubmitButton();
        }
        else if (GameObject.FindObjectOfType<Obj11Manager>().eightPiecePizza == 0 &&
                 (((GameObject.FindObjectOfType<Obj11Manager>().SixPiecePizza < 2 || GameObject.FindObjectOfType<Obj11Manager>().SixPiecePizza > 2) ||
                 (GameObject.FindObjectOfType<Obj11Manager>().threePiecePizza < 2 || GameObject.FindObjectOfType<Obj11Manager>().threePiecePizza > 2))))
        {
            if (numberOfAttempt == 0)
            {
                numberOfAttempt = numberOfAttempt + 1;
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();

                GameObject.FindObjectOfType<conversationManager>().EnableDialouge("The customer wants \\frac{2}{8} of a pizza. Ensure that the fraction and the amount of pizza packed match");
                GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(ok1);
                GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj11_Quest1_Hint3.wav");
            }
            else if (numberOfAttempt == 1)
            {
                numberOfAttempt = numberOfAttempt + 1;
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();

                GameObject.FindObjectOfType<conversationManager>().EnableDialouge("The number of pizza slices to be packed should be according to the fraction given. Check again");
                GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(ok1);
                GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj11_Quest1_Hint3_1.wav");
            }
            else
            {
                numberOfAttempt = 0;
                UtilityArtifacts.loading_pos = "Obj4_Lo1_from_obj11";
                UtilityArtifacts.coming_back_from = "to_Obj11_quest1";
                UtilityArtifacts.backTraversal = true;
                UtilityArtifacts.comingbackafterTraversal = false;
                UtilityArtifacts.loadStartingpointforcomingback = 6;
                UtilityArtifacts.loadStartingpoint = 4;
                UtilityArtifacts.loadEndingpoint = 11;
                // load traversescene 4
                //SceneManager.LoadScene("Obj4AreaModule");
                OnTraversal(155, 129);
            }
            DisableSubmitButton();
        }

    }

    public void checkObj11Ans2()
    {
        if (GameObject.FindObjectOfType<Obj11Manager>().eightPiecePizza == 0 &&
            GameObject.FindObjectOfType<Obj11Manager>().SixPiecePizza == 4 &&
            GameObject.FindObjectOfType<Obj11Manager>().threePiecePizza == 0)
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            //GameObject.Find("SubmitButton").transform.GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
            GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(ok);
            DisableSubmitButton();
            ok();
        }
        else if ((GameObject.FindObjectOfType<Obj11Manager>().SixPiecePizza < 4 || GameObject.FindObjectOfType<Obj11Manager>().SixPiecePizza < 4) &&
                  GameObject.FindObjectOfType<Obj11Manager>().eightPiecePizza == 0 &&
                  GameObject.FindObjectOfType<Obj11Manager>().threePiecePizza == 0)
        {
            if (numberOfAttempt == 0)
            {
                numberOfAttempt = numberOfAttempt + 1;
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();

                GameObject.FindObjectOfType<conversationManager>().EnableDialouge("The customer wants \\frac{4}{6} of a pizza. Ensure that the fraction and the pizza packed match.");
                GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(ok1);
                GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj11_Quest2_Hint1.wav");
            }
            else if (numberOfAttempt == 1)
            {
                numberOfAttempt = numberOfAttempt + 1;
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();

                GameObject.FindObjectOfType<conversationManager>().EnableDialouge("The number of pizza slices to be packed should be according to the fraction given. Check again.");
                GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(ok1);
                GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj11_Quest2_Hint1_1.wav");
            }
            else
            {
                numberOfAttempt = 0;
                UtilityArtifacts.loading_pos = "Obj4_Lo1_from_obj11";
                UtilityArtifacts.coming_back_from = "to_Obj11_quest2";
                UtilityArtifacts.backTraversal = true;
                UtilityArtifacts.comingbackafterTraversal = false;
                UtilityArtifacts.loadStartingpointforcomingback = 7;
                UtilityArtifacts.loadStartingpoint = 4;
                UtilityArtifacts.loadEndingpoint = 11;
                // load traversescene 4
                //SceneManager.LoadScene("Obj4AreaModule");
                OnTraversal(155, 129);
            }
            DisableSubmitButton();
        }
        else if (GameObject.FindObjectOfType<Obj11Manager>().SixPiecePizza == 0 &&
                 (GameObject.FindObjectOfType<Obj11Manager>().eightPiecePizza == 2 ||
                 GameObject.FindObjectOfType<Obj11Manager>().threePiecePizza == 2))
        {
            if (numberOfAttempt == 0)
            {
                numberOfAttempt = numberOfAttempt + 1;
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();

                GameObject.FindObjectOfType<conversationManager>().EnableDialouge("Check if you are taking 4 slices of pizza from the correct pizza.");
                GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(ok1);
                GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj11_Quest2_Hint2.wav");
            }
            else if (numberOfAttempt == 1)
            {
                numberOfAttempt = numberOfAttempt + 1;
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();

                GameObject.FindObjectOfType<conversationManager>().EnableDialouge("The customer wants \\frac{4}{6} of a pizza. Ensure that the denominator of the fraction and the total number of slices in the pizza selected match.");
                GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(ok1);
                GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj11_Quest2_Hint2_1.wav");
            }
            else
            {
                numberOfAttempt = 0;
                UtilityArtifacts.loading_pos = "Obj4_Lo1_from_obj11";
                UtilityArtifacts.coming_back_from = "to_Obj11_quest2";
                UtilityArtifacts.backTraversal = true;
                UtilityArtifacts.comingbackafterTraversal = false;
                UtilityArtifacts.loadStartingpointforcomingback = 7;
                UtilityArtifacts.loadStartingpoint = 4;
                UtilityArtifacts.loadEndingpoint = 11;
                // load traversescene 4
                //SceneManager.LoadScene("Obj4AreaModule");
                OnTraversal(155, 129);
            }
            DisableSubmitButton();
        }
        else if (GameObject.FindObjectOfType<Obj11Manager>().SixPiecePizza == 0 &&
                 (((GameObject.FindObjectOfType<Obj11Manager>().eightPiecePizza < 2 || GameObject.FindObjectOfType<Obj11Manager>().eightPiecePizza > 2) ||
                 (GameObject.FindObjectOfType<Obj11Manager>().threePiecePizza < 2 || GameObject.FindObjectOfType<Obj11Manager>().threePiecePizza > 2))))
        {
            if (numberOfAttempt == 0)
            {
                numberOfAttempt = numberOfAttempt + 1;
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();

                GameObject.FindObjectOfType<conversationManager>().EnableDialouge("The customer wants \\frac{4}{6} of a pizza. Ensure that the fraction and the amount of pizza packed match");
                GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(ok1);
                GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj11_Quest2_Hint3.wav");
            }
            else if (numberOfAttempt == 1)
            {
                numberOfAttempt = numberOfAttempt + 1;
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();

                GameObject.FindObjectOfType<conversationManager>().EnableDialouge("The number of pizza slices to be packed should be according to the fraction given. Check again");
                GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(ok1);
                GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj11_Quest2_Hint3_1.wav");
            }
            else
            {
                numberOfAttempt = 0;
                UtilityArtifacts.loading_pos = "Obj4_Lo1_from_obj11";
                UtilityArtifacts.coming_back_from = "to_Obj11_quest2";
                UtilityArtifacts.backTraversal = true;
                UtilityArtifacts.comingbackafterTraversal = false;
                UtilityArtifacts.loadStartingpointforcomingback = 7;
                UtilityArtifacts.loadStartingpoint = 4;
                UtilityArtifacts.loadEndingpoint = 11;
                // load traversescene 4
                //SceneManager.LoadScene("Obj4AreaModule");
                OnTraversal(155, 129);
            }
            DisableSubmitButton();
        }

    }

    public void checkObj11Ans3()
    {
        if (GameObject.FindObjectOfType<Obj11Manager>().eightPiecePizza == 0 &&
            GameObject.FindObjectOfType<Obj11Manager>().SixPiecePizza == 0 &&
            GameObject.FindObjectOfType<Obj11Manager>().threePiecePizza == 1)
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            GameObject.Find("SubmitButton").transform.GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
            GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(ok);
            DisableSubmitButton();
            ok();
        }
        else if (GameObject.FindObjectOfType<Obj11Manager>().threePiecePizza != 1 &&
                  GameObject.FindObjectOfType<Obj11Manager>().eightPiecePizza == 0 &&
                  GameObject.FindObjectOfType<Obj11Manager>().SixPiecePizza == 0)
        {
            if (numberOfAttempt == 0)
            {
                numberOfAttempt = numberOfAttempt + 1;
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();

                GameObject.FindObjectOfType<conversationManager>().EnableDialouge("The customer wants \\frac{1}{3} of a pizza. Ensure that the fraction and the pizza packed match.");

                GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(ok1);
                GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj11_Quest3_Hint1.wav");
            }
            else if (numberOfAttempt == 1)
            {
                numberOfAttempt = numberOfAttempt + 1;
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();

                GameObject.FindObjectOfType<conversationManager>().EnableDialouge("The number of pizza slices to be packed should be according to the fraction given. Check again.");
                GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(ok1);
                GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj11_Quest3_Hint1_1.wav");
            }
            else
            {
                numberOfAttempt = 0;
                UtilityArtifacts.loading_pos = "Obj4_Lo1_from_obj11";
                UtilityArtifacts.coming_back_from = "to_Obj11_quest3";
                UtilityArtifacts.backTraversal = true;
                UtilityArtifacts.comingbackafterTraversal = false;
                UtilityArtifacts.loadStartingpointforcomingback = 8;
                UtilityArtifacts.loadStartingpoint = 4;
                UtilityArtifacts.loadEndingpoint = 11;
                // load traversescene 4
                //SceneManager.LoadScene("Obj4AreaModule");
                OnTraversal(155, 129);
            }
            DisableSubmitButton();
        }
        else if (GameObject.FindObjectOfType<Obj11Manager>().threePiecePizza == 0 &&
                 (GameObject.FindObjectOfType<Obj11Manager>().eightPiecePizza == 1 ||
                 GameObject.FindObjectOfType<Obj11Manager>().SixPiecePizza == 1))
        {
            if (numberOfAttempt == 0)
            {
                numberOfAttempt = numberOfAttempt + 1;
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();

                GameObject.FindObjectOfType<conversationManager>().EnableDialouge("Check if you are taking 1 slices of pizza from the correct pizza.");
                GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(ok1);
                GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj11_Quest3_Hint2.wav");
            }
            else if (numberOfAttempt == 1)
            {
                numberOfAttempt = numberOfAttempt + 1;
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();

                GameObject.FindObjectOfType<conversationManager>().EnableDialouge("The customer wants \\frac{1}{3} of a pizza. Ensure that the denominator of the fraction and the total number of slices in the pizza selected match.");
                GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(ok1);
                GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj11_Quest3_Hint2_1.wav");
            }
            else
            {
                numberOfAttempt = 0;
                UtilityArtifacts.loading_pos = "Obj4_Lo1_from_obj11";
                UtilityArtifacts.coming_back_from = "to_Obj11_quest3";
                UtilityArtifacts.backTraversal = true;
                UtilityArtifacts.comingbackafterTraversal = false;
                UtilityArtifacts.loadStartingpointforcomingback = 8;
                UtilityArtifacts.loadStartingpoint = 4;
                UtilityArtifacts.loadEndingpoint = 11;
                // load traversescene 4
                //SceneManager.LoadScene("Obj4AreaModule");
                OnTraversal(155, 129);
            }
            DisableSubmitButton();
        }
        else if (GameObject.FindObjectOfType<Obj11Manager>().threePiecePizza == 0 &&
                 ((GameObject.FindObjectOfType<Obj11Manager>().eightPiecePizza > 1) ||
                 (GameObject.FindObjectOfType<Obj11Manager>().SixPiecePizza > 1)))
        {
            if (numberOfAttempt == 0)
            {
                numberOfAttempt = numberOfAttempt + 1;
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();

                GameObject.FindObjectOfType<conversationManager>().EnableDialouge("The customer wants \\frac{1}{3} of a pizza. Ensure that the fraction and the amount of pizza packed match");
                GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(ok1);
                GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj11_Quest3_Hint3.wav");
            }
            else if (numberOfAttempt == 1)
            {
                numberOfAttempt = numberOfAttempt + 1;
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();

                GameObject.FindObjectOfType<conversationManager>().EnableDialouge("The number of pizza slices to be packed should be according to the fraction given. Check again");
                GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(ok1);
                GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj11_Quest3_Hint3_1.wav");
            }
            else
            {
                numberOfAttempt = 0;
                UtilityArtifacts.loading_pos = "Obj4_Lo1_from_obj11";
                UtilityArtifacts.coming_back_from = "to_Obj11_quest3";
                UtilityArtifacts.backTraversal = true;
                UtilityArtifacts.comingbackafterTraversal = false;
                UtilityArtifacts.loadStartingpointforcomingback = 8;
                UtilityArtifacts.loadStartingpoint = 4;
                UtilityArtifacts.loadEndingpoint = 11;
                // load traversescene 4
                //SceneManager.LoadScene("Obj4AreaModule");
                OnTraversal(155, 129);
            }
            DisableSubmitButton();
        }

    }

    void disableDraggedPizza()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("tree"))
            g.SetActive(false);
    }
    void ok()
    {
        CancelInvoke();

        //GameObject.Find("PIZZA BOX").GetComponent<Animator>().enabled = true;
        Invoke("closePizzaBox", 3.0f);
        GameObject.FindObjectOfType<Obj11Manager>().eightPiecePizza = 0;
        GameObject.FindObjectOfType<Obj11Manager>().SixPiecePizza = 0;
        GameObject.FindObjectOfType<Obj11Manager>().threePiecePizza = 0;

        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("RO Panel").transform.GetChild(0).gameObject.SetActive(false);
        Debug.Log("correct");

    }
    void ok1()
    {
        CancelInvoke();
        GameObject.FindObjectOfType<Obj11Drag>().resetAllPizza();
        GameObject.FindObjectOfType<Obj11Manager>().eightPiecePizza = 0;
        GameObject.FindObjectOfType<Obj11Manager>().SixPiecePizza = 0;
        GameObject.FindObjectOfType<Obj11Manager>().threePiecePizza = 0;
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("RO Panel").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.FindObjectOfType<conversationManager>().DisableDialouge();
    }

    void closePizzaBox()
    {
        disableDraggedPizza();
        GameObject.Find("PIZZA BOX").SetActive(false);
        GameObject.FindObjectOfType<timeline_new>().load_next();

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
        if (GameObject.Find("Chef conversation").transform.GetChild(0).gameObject.activeSelf)
        {
            GameObject.FindObjectOfType<conversationManager>().DisableConversation();
        }
        var currentEventSystem = EventSystem.current;
        var currentSelectedGameObject = currentEventSystem.currentSelectedGameObject;
        if (isNum)
        {
            Debug.Log("numerator =" + currentSelectedGameObject.name);
            GameObject.Find("FractionInputPanel").transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = GameObject.Find("FractionInputPanel").transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text + currentSelectedGameObject.name;
            EnableSubmitButton();
            DoneButton.onClick.RemoveAllListeners();
            DoneButton.onClick.AddListener(DoneForObj3Quest1);
        }

        if (isDenum)
        {
            Debug.Log("denominator =" + currentSelectedGameObject.name);
            GameObject.Find("FractionInputPanel").transform.GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text = GameObject.Find("FractionInputPanel").transform.GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text + currentSelectedGameObject.name;
            EnableSubmitButton();
            DoneButton.onClick.RemoveAllListeners();
            DoneButton.onClick.AddListener(DoneForObj3Quest2);
        }


    }
    public void GetallNumberButtonsObj4()
    {
        foreach (GameObject b in GameObject.FindGameObjectsWithTag("Numbers"))
        {
            b.GetComponent<Button>().onClick.AddListener(InputNumbersForObj4);
        }
    }
    public void InputNumbersForObj4()
    {
        var currentEventSystem = EventSystem.current;
        var currentSelectedGameObject = currentEventSystem.currentSelectedGameObject;

        if (isNum)
        {
            Debug.Log("numerator =" + currentSelectedGameObject.name);
            GameObject.Find("FractionInputPanel").transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = GameObject.Find("FractionInputPanel").transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text + currentSelectedGameObject.name;

        }

        if (isDenum)
        {
            Debug.Log("denominator =" + currentSelectedGameObject.name);
            GameObject.Find("FractionInputPanel").transform.GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text = GameObject.Find("FractionInputPanel").transform.GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text + currentSelectedGameObject.name;

        }

    }
    public void EmptyNumDenumBox()
    {
        GameObject.Find("FractionInputPanel").transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = "";
        GameObject.Find("FractionInputPanel").transform.GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text = "";
        GameObject.Find("FractionInputPanel").transform.GetChild(3).GetChild(0).gameObject.GetComponent<Text>().text = "";
    }


    // Question for objective 2
    public void GetallNumberButtonsObj2()
    {
        foreach (GameObject b in GameObject.FindGameObjectsWithTag("Numbers"))
        {
            b.GetComponent<Button>().onClick.AddListener(InputNumbersForObj2);
        }
    }
    public void InputNumbersForObj2()
    {
        var currentEventSystem = EventSystem.current;
        var currentSelectedGameObject = currentEventSystem.currentSelectedGameObject;

        if (isNum)
        {
            Debug.Log("numerator =" + currentSelectedGameObject.name);
            GameObject.Find("FractionInputPanel").transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = GameObject.Find("FractionInputPanel").transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text + currentSelectedGameObject.name;

        }

    }
    public void EnableForObj2Quest1()
    {

        GameObject.FindObjectOfType<GameManager>().isObj4On = false;
        EnableSubmitButton();
        DoneButton.onClick.AddListener(DoneForObj2Quest1);
        //ClickOnNumerator();
    }

    public void EnableForObj2Quest2()
    {
        GameObject.Find("NumberPanel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("FractionInputPanel").transform.GetChild(0).gameObject.SetActive(true);
        //GameObject.Find("FractionInputPanel").transform.GetChild(1).gameObject.SetActive(true);

        DoneButton.onClick.AddListener(DoneForOb2Quest2);
        GameObject.Find("FractionInputPanel").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(ClickOnNumerator);
        EnableSubmitButton();
        GetallNumberButtonsObj2();
        ClickOnNumerator();
        EmptyNumDenumBox();
    }


    public void DoneForObj2Quest1()
    {

        Debug.Log("Quest");
        checkObj2Ans1();

    }
    public void DoneForOb2Quest2()
    {
        checkObj2Ans2();


        DisableSubmitButton();
    }
    void checkObj2Ans2()
    {

        if (GameObject.Find("FractionInputPanel").transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text == "4")
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();

            GameObject.Find("FractionInputPanel").transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("FractionInputPanel").transform.GetChild(1).gameObject.SetActive(false);
            GameObject.Find("NumberPanel").transform.GetChild(0).gameObject.SetActive(false);
            GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj2_q2_ans1.wav");


            DoneButton.onClick.RemoveListener(DoneForOb2Quest2);
            DisableSubmitButton();
            Invoke("next_obj1", 2.0f);
        }
        else
        {
            if (numberOfAttempt < 1)
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj2_q2_H1.wav");
                GameObject.FindObjectOfType<conversationManager>().EnableDialouge("Are you sure?");

                numberOfAttempt++;
                DisableSubmitButton();

            }
            else
            {

                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj2_q2_H2.wav");
                GameObject.FindObjectOfType<conversationManager>().EnableDialouge("Count the number of apples in each tray");
                numberOfAttempt++;
                DisableSubmitButton();

            }

        }
    }
    public void calculateApplesInEachTray()
    {
        noOfAppplesinTray1 = 0; noOfAppplesinTray2 = 0; noOfAppplesinTray3 = 0;
        foreach (GameObject t1a1 in GameObject.FindGameObjectsWithTag("Red"))
        {
            noOfAppplesinTray1 = noOfAppplesinTray1 + 1;
        }

        foreach (GameObject t2a2 in GameObject.FindGameObjectsWithTag("Green"))
            noOfAppplesinTray2 = noOfAppplesinTray2 + 1;
        foreach (GameObject t3a3 in GameObject.FindGameObjectsWithTag("Yellow"))
            noOfAppplesinTray3 = noOfAppplesinTray3 + 1;
    }

    void checkObj2Ans1()
    {
        calculateApplesInEachTray();
        if (noOfAppplesinTray1 == 4 && noOfAppplesinTray2 == 4 && noOfAppplesinTray3 == 4)
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();

            GameObject.Find("FractionInputPanel").transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("FractionInputPanel").transform.GetChild(1).gameObject.SetActive(false);
            GameObject.Find("NumberPanel").transform.GetChild(0).gameObject.SetActive(false);
            GameObject.FindObjectOfType<timeline_new>().load_next();
            DoneButton.onClick.RemoveListener(DoneForObj2Quest1);
            DisableSubmitButton();
        }
        else
        {
            if (numberOfAttempt < 1)
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                ReInforse_O2Q1H1();
                numberOfAttempt++;
                DisableSubmitButton();

            }
            else
            {

                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                ReInforse_O2Q1H2();
                numberOfAttempt++;
                DisableSubmitButton();

            }

        }
    }
    void ReInforse_O2Q1H1()
    {




        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj2_Hint1.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("The goal is to split the 12 apples such that all the trays have the same number of apples.");

    }
    public void ok_button_set()
    {
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(okQuest11);
    }
    void ReInforse_O2Q1H2()
    {

        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj2_Hint2.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("If you have placed more apples in one tray than the others, move the extra apples to the other trays. Ensure that the number of apples on each tray is the same");

    }
    void okQuest11()
    {

        CancelInvoke();
        convesationDisableFirst1();
        EnableSubmitButton();
    }
    void convesationDisable1()
    {
        GameObject.FindObjectOfType<conversationManager>().DisableDialouge();
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        GameObject.Find("RO Panel").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj4_lets_go_ahead.wav");
        Invoke("next_obj", 2.0f);
    }
    void next_obj1()
    {
        GameObject.FindObjectOfType<timeline_new>().load_next();
    }
    void convesationDisableFirst1()
    {
        GameObject.FindObjectOfType<conversationManager>().DisableDialouge();

    }


    // Question for Objective 4

    public void EnableForObj4Quest1()
    {

        GameObject.FindObjectOfType<GameManager>().isObj4On = false;
        EnableSubmitButton();
        DoneButton.onClick.AddListener(DoneForObj4Quest1);
        //ClickOnNumerator();
    }
    public void EnableForObj4Quest2()
    {
        GameObject.Find("NumberPanel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("FractionInputPanel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("FractionInputPanel").transform.GetChild(1).gameObject.SetActive(true);
        GameObject.Find("FractionInputPanel").transform.GetChild(2).gameObject.SetActive(true);

        EnableSubmitButton();

        ClickOnNumerator();
        EmptyNumDenumBox();
    }
    public void EnableForObj4Quest3()
    {
        GameObject.Find("NumberPanel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("FractionInputPanel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("FractionInputPanel").transform.GetChild(1).gameObject.SetActive(true);
        GameObject.Find("FractionInputPanel").transform.GetChild(2).gameObject.SetActive(true);

        EnableSubmitButton();

        ClickOnNumerator();
        DoneButton.onClick.RemoveListener(DoneForObj4Quest1);
        DoneButton.onClick.AddListener(DoneForObj4Quest3);
        EmptyNumDenumBox();
    }

    public void DoneForObj4Quest1()
    {

        Debug.Log("Quest");
        checkObj4Ans1();

    }
    public void DoneForObj4Quest3()
    {
        GameObject.Find("NumberPanel").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("FractionInputPanel").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("FractionInputPanel").transform.GetChild(1).gameObject.SetActive(false);
        GameObject.Find("FractionInputPanel").transform.GetChild(2).gameObject.SetActive(false);
        GameObject.Find("Fade").GetComponent<Animator>().enabled = true;
        DisableSubmitButton();
    }

    void checkObj4Ans1()
    {
        if (Obj4Manager.SelectedPart == 5 && Obj4Manager.other_part == 0)
        {
            Debug.Log("Quest1");
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            GameObject.FindObjectOfType<timeline_new>().load_next();
            DoneButton.onClick.RemoveListener(DoneForObj4Quest1);
            GameObject.Find("Ro Panel").transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            GameObject.Find("Ro Panel").transform.GetChild(0).gameObject.SetActive(false);
            DisableSubmitButton();
        }
        else
        {
            Debug.Log("Quest2");
            if (numberOfAttempt < 1)
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                ReInforse_Q1H1();
                numberOfAttempt++;
                //DoneButton.onClick.RemoveListener(DoneForObj4Quest1);
                DisableSubmitButton();
                Invoke("EnableSubmitButton", 10.01f);
            }
            else
            {
                //DoneButton.onClick.RemoveListener(DoneForObj4Quest1);
                DisableSubmitButton();
                GameObject.FindObjectOfType<Obj4Manager>().UnHighLightAllBlocks();
                GameObject.FindObjectOfType<Obj4Manager>().HighlightBenches();
                Invoke("convesationDisable", 4.0f);
                
            }

        }
    }
    void ReInforse_Q1H1()
    {

        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(okQuest1);

        Obj4Manager.SelectedPart = 0;
        Obj4Manager.other_part = 0;
        GameObject.FindObjectOfType<Obj4Manager>().UnHighLightAllBlocks();
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj4_Quest1_RO_Hint1.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("Why don't you try one more time? You might have missed some parts or selected some extra parts.");
        Invoke("convesationDisableFirst", 10.0f);
    }
    void okQuest1()
    {
        GameObject.FindObjectOfType<timeline_new>().stopAudio();
        GameObject.FindObjectOfType<Obj4Manager>().UnHighLightAllBlocks();
        CancelInvoke();
        Obj4Manager.SelectedPart = 0;
        Obj4Manager.other_part = 0;
        GameObject.FindObjectOfType<Obj4Manager>().UnHighLightAllBlocks();
        convesationDisableFirst();
        EnableSubmitButton();
    }
    void convesationDisable()
    {
        GameObject.FindObjectOfType<conversationManager>().DisableDialouge();
        GameObject.Find("RO Panel").transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        GameObject.Find("RO Panel").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj4_lets_go_ahead.wav");
        Invoke("next_obj", 2.0f);
    }
    void next_obj()
    {
        GameObject.FindObjectOfType<timeline_new>().load_next();
    }
    void convesationDisableFirst()
    {
        GameObject.FindObjectOfType<conversationManager>().DisableDialouge();

    }
    //Questions for Objective 5

    public void CalculateAppleObj5()
    {

        //foreach (Transform child in GameObject.Find("Tray1Apple").transform)
        //{
        //    if (child.tag == "Red")
        //    {
        //        noOfAppplesinTray1 = noOfAppplesinTray1 + 1;
        //    }

        //}
        //foreach (Transform child in GameObject.Find("Tray2Apple").transform)
        //{
        //    if (child.tag == "Green")
        //    {
        //        noOfAppplesinTray2 = noOfAppplesinTray2 + 1;
        //    }

        //}
        //foreach (Transform child in GameObject.Find("Tray3Apple").transform)
        //{
        //    if (child.tag == "Yellow")
        //    {
        //        noOfAppplesinTray3 = noOfAppplesinTray3 + 1;
        //    }

        //}
        noOfAppplesinTray1 = 0; noOfAppplesinTray2 = 0; noOfAppplesinTray3 = 0;
        isTray1 = false; isTray2 = false; isTray3 = false;

        if ((GameObject.Find("Tray2Apple").transform.GetChild(0).tag != "Obj2t2a2") || (GameObject.Find("Tray2Apple").transform.GetChild(1).tag != "Obj2t2a2"))
        {
            if (GameObject.Find("Tray2Apple").transform.GetChild(0).tag == GameObject.Find("Tray2Apple").transform.GetChild(1).tag)
            {

                isTray2 = true;
            }
        }
        if ((GameObject.Find("Tray1Apple").transform.GetChild(0).tag != "Obj2t1a1") || (GameObject.Find("Tray1Apple").transform.GetChild(1).tag != "Obj2t1a1"))
        {
            if (GameObject.Find("Tray1Apple").transform.GetChild(0).tag == GameObject.Find("Tray1Apple").transform.GetChild(1).tag)
            {
                isTray1 = true;
            }
        }
        if ((GameObject.Find("Tray3Apple").transform.GetChild(0).tag != "Obj2t3a3") || (GameObject.Find("Tray3Apple").transform.GetChild(1).tag != "Obj2t3a3"))
        {
            if (GameObject.Find("Tray3Apple").transform.GetChild(0).tag == GameObject.Find("Tray3Apple").transform.GetChild(1).tag)
            {
                isTray3 = true;
            }
        }

    }

    void instentiateResetApple()
    {
        GameObject.FindObjectOfType<Obj5Manager>().InitiatedApplel = Instantiate(GameObject.FindObjectOfType<Obj5Manager>().TrayToReset) as GameObject;
        GameObject.FindObjectOfType<Obj5Manager>().InitiatedApplel.transform.parent = GameObject.Find("Full").transform;
        GameObject.FindObjectOfType<Obj5Manager>().InitiatedApplel.transform.localPosition = new Vector3(0, 0, 0);
        GameObject.FindObjectOfType<Obj5Manager>().InitiatedApplel.transform.localScale = new Vector3(1, 1, 1);
        GameObject.FindObjectOfType<Obj5Manager>().InitiatedApplel.name = "Full1";
    }
    public void ResetApples_DragNDrop()
    {

        foreach (Transform child in GameObject.Find("Tray1Apple").transform)
        {
            child.tag = "Obj2t1a1";
            Color c = child.GetComponent<Image>().color;
            c.a = 0;
            child.GetComponent<Image>().color = c;
            child.GetComponent<Obj5Drag>().enabled = false;
        }
        foreach (Transform child in GameObject.Find("Tray2Apple").transform)
        {
            child.tag = "Obj2t2a2";
            Color c = child.GetComponent<Image>().color;
            c.a = 0;
            child.GetComponent<Image>().color = c;
            child.GetComponent<Obj5Drag>().enabled = false;

        }
        foreach (Transform child in GameObject.Find("Tray3Apple").transform)
        {
            child.tag = "Obj2t3a3";
            Color c = child.GetComponent<Image>().color;
            c.a = 0;
            child.GetComponent<Image>().color = c;
            child.GetComponent<Obj5Drag>().enabled = false;

        }

        Destroy(GameObject.FindObjectOfType<Obj5Manager>().InitiatedApplel);
        instentiateResetApple();

        isTray1 = false; isTray2 = false; isTray3 = false;
    }



    public void GetallNumberButtonsObj5()
    {
        foreach (GameObject b in GameObject.FindGameObjectsWithTag("Numbers"))
        {
            b.GetComponent<Button>().onClick.AddListener(InputNumbersForObj5);
        }
    }

    public void GetOkButton()
    {
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(HintDisable);
    }

    public void InputNumbersForObj5()
    {
        var currentEventSystem = EventSystem.current;
        var currentSelectedGameObject = currentEventSystem.currentSelectedGameObject;
        if (isNum)
        {
            Debug.Log("numerator =" + currentSelectedGameObject.name);
            GameObject.Find("FractionInputPanel").transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = GameObject.Find("FractionInputPanel").transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text + currentSelectedGameObject.name;
            EnableSubmitButton();
            DoneButton.onClick.RemoveAllListeners();
            DoneButton.onClick.AddListener(DoneForObj5Quest1);
        }

        if (isDenum)
        {
            Debug.Log("denominator =" + currentSelectedGameObject.name);
            GameObject.Find("FractionInputPanel").transform.GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text = GameObject.Find("FractionInputPanel").transform.GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text + currentSelectedGameObject.name;
            EnableSubmitButton();
            DoneButton.onClick.RemoveAllListeners();
            DoneButton.onClick.AddListener(DoneForObj5Quest2);
        }

        if (isLO2_Num)
        {
            Debug.Log("numerator =" + currentSelectedGameObject.name);
            GameObject.Find("FractionInputPanel").transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = GameObject.Find("FractionInputPanel").transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text + currentSelectedGameObject.name;
            EnableSubmitButton();
            DoneButton.onClick.RemoveAllListeners();
            DoneButton.onClick.AddListener(DoneForObj5_LO2_Question2);
        }

        if (isLO3_Den)
        {
            Debug.Log("numerator =" + currentSelectedGameObject.name);
            GameObject.Find("FractionInputPanel").transform.GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text = GameObject.Find("FractionInputPanel").transform.GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text + currentSelectedGameObject.name;
            EnableSubmitButton();
            DoneButton.onClick.RemoveAllListeners();
            DoneButton.onClick.AddListener(DoneForObj5_LO2_Question3);
        }

    }

    public void EnableForObj5Quest1()
    {
        numberOfAttempt = 0;
        GameObject.Find("NumberPanel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("FractionInputPanel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("FractionInputPanel").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(ClickOnNumerator);
        //GameObject.Find("Question_1_Text").transform.GetChild(0).gameObject.SetActive(true);
        GetallNumberButtonsObj5();
        ClickOnNumerator();

    }

    public void EnableForObj5Quest2()
    {
        numberOfAttempt = 0;
        var currentEventSystem = EventSystem.current;
        var currentSelectedGameObject = currentEventSystem.currentSelectedGameObject;

        GameObject.Find("FractionInputPanel").transform.GetChild(1).gameObject.SetActive(true);
        GameObject.Find("FractionInputPanel").transform.GetChild(2).gameObject.SetActive(true); //Question_LO_1_Text

        GameObject.Find("FractionInputPanel").transform.GetChild(1).gameObject.GetComponent<Button>().onClick.AddListener(ClickOnDenominator);
        ClickOnDenominator();
        GameObject.Find("NumberPanel").transform.GetChild(0).gameObject.SetActive(true);
        //GameObject.Find("Question_2_Text").transform.GetChild(0).gameObject.SetActive(true);
        numberOfAttempt = 0;


    }
    public void EnableForObj5Quest3()
    {

    }

    public void Obj5_LO2_Question2()
    {
        numberOfAttempt = 0;
        var currentEventSystem = EventSystem.current;
        var currentSelectedGameObject = currentEventSystem.currentSelectedGameObject;

        GameObject.Find("SubmitButton").transform.GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Question_LO_1_Text").transform.GetChild(0).gameObject.SetActive(true);

        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).GetComponent<Button>().onClick.AddListener(convesationDisableFirst);


        GameObject.Find("FractionInputPanel").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(ClickOn_LO2_Numerator);
        ClickOn_LO2_Numerator();


        GameObject.Find("NumberPanel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("FractionInputPanel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("FractionInputPanel").transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        GameObject.Find("FractionInputPanel").transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = "";
        GameObject.Find("FractionInputPanel").transform.GetChild(1).gameObject.SetActive(false);
        GameObject.Find("FractionInputPanel").transform.GetChild(2).gameObject.SetActive(false);
        GameObject.Find("FractionInputPanel").transform.GetChild(0).GetComponent<Button>().enabled = true;

    }

    public void DoneForObj5_LO2_Question2()
    {
        DisableSubmitButton();
        if (GameObject.Find("FractionInputPanel").transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text == "1")
        {
            FindObjectOfType<conversationManager>().playCorrect();
            //GameObject.Find("FractionInputPanel").transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = "";
            GameObject.FindObjectOfType<timeline_new>().load_next();
            GameObject.Find("FractionInputPanel").transform.GetChild(0).GetComponent<Button>().enabled = false;
            DisableSubmitButton();
        }
        else
        {
            if (numberOfAttempt < 3)
            {
                FindObjectOfType<conversationManager>().playError();
                FindObjectOfType<timeline_new>().playAudioOnRelearn("count_the_case_red_apples.wav");
                //GameObject.FindObjectOfType<conversationManager>().EnableDialouge("You have to count the case which has red apples. Try one more time, I am sure you will get it.");
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("You have to count the case which has red apples. Try one more time, I am sure you will get it.");
                FindObjectOfType<timeline_new>().playAudioOnRelearn("count_the_case_red_apples.wav");
                numberOfAttempt++;
                DisableSubmitButton();
                Invoke("DisableConvo_ForQuestionHints", GameObject.FindObjectOfType<timeline_new>().lapa.length_of_audio + 1.5f);
            }
            else
            {
                numberOfAttempt = 0;
                //CountingFlashcard.SetActive(true);
                ShowBeforeFashCard();
                GameObject.Find("FractionInputPanel").transform.GetChild(0).GetComponent<Button>().enabled = false;
                DisableSubmitButton();
            }
            //GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).GetComponent<Button>().onClick.RemoveAllListeners();
            //GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).GetComponent<Button>().onClick.AddListener(HintDisable);
        }

    }

    public void DisableNumbers_InputField()
    {
        GameObject.Find("NumberPanel").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("FractionInputPanel").transform.gameObject.SetActive(false);
        GameObject.Find("Question_LO_1_Text").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("Question_LO_2_Text").transform.GetChild(0).gameObject.SetActive(false);


    }

    public void Obj5_LO2_Question3()
    {
        numberOfAttempt = 0;
        var currentEventSystem = EventSystem.current;
        var currentSelectedGameObject = currentEventSystem.currentSelectedGameObject;
        GameObject.Find("Question_LO_2_Text").transform.GetChild(0).gameObject.SetActive(true);

        GameObject.Find("SubmitButton").transform.GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();

        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).GetComponent<Button>().onClick.AddListener(convesationDisableFirst);


        GameObject.Find("FractionInputPanel").transform.GetChild(1).gameObject.GetComponent<Button>().onClick.AddListener(ClickOn_LO3_Numerator);
        ClickOn_LO3_Numerator();


        GameObject.Find("NumberPanel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("FractionInputPanel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("FractionInputPanel").transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        GameObject.Find("FractionInputPanel").transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
        GameObject.Find("FractionInputPanel").transform.GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text = "";
        GameObject.Find("FractionInputPanel").transform.GetChild(1).gameObject.SetActive(true);
        GameObject.Find("FractionInputPanel").transform.GetChild(2).gameObject.SetActive(true);
        GameObject.Find("FractionInputPanel").transform.GetChild(1).GetComponent<Button>().enabled = true;
        GameObject.Find("FractionInputPanel").transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = "1";


    }

    public void DoneForObj5_LO2_Question3()
    {
        DisableSubmitButton();
        if (GameObject.Find("FractionInputPanel").transform.GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text == "3")
        {
            FindObjectOfType<conversationManager>().playCorrect();
            GameObject.Find("FractionInputPanel").transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = "1";
            GameObject.FindObjectOfType<timeline_new>().load_next();
            GameObject.Find("FractionInputPanel").transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("NumberPanel").transform.GetChild(0).gameObject.SetActive(false);

            GameObject.Find("FractionInputPanel").transform.GetChild(2).gameObject.SetActive(false);
            GameObject.Find("FractionInputPanel").transform.GetChild(1).GetComponent<Button>().enabled = false;

            DisableSubmitButton();
        }
        else
        {
            if (numberOfAttempt < 3)
            {
                FindObjectOfType<conversationManager>().playError();
                FindObjectOfType<timeline_new>().playAudioOnRelearn("count_total_cases.wav");
                //GameObject.FindObjectOfType<conversationManager>().EnableDialouge("You have to count the total cases we have. Try one more time, I am sure you will get it.");
                //GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).GetComponent<Button>().onClick.RemoveAllListeners();
                //GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).GetComponent<Button>().onClick.AddListener(HintDisable);

                GameObject.FindObjectOfType<conversationManager>().EnableConversation("You have to count the total cases we have. Try one more time, I am sure you will get it.");
                FindObjectOfType<timeline_new>().playAudioOnRelearn("count_total_cases.wav");
                DisableSubmitButton();
                numberOfAttempt++;
                Invoke("DisableConvo_ForQuestionHints", GameObject.FindObjectOfType<timeline_new>().lapa.length_of_audio + 1.5f);
            }
            else
            {
                numberOfAttempt = 0;
                //CountingFlashcard.SetActive(true);
                ShowBeforeFashCard();
                GameObject.Find("FractionInputPanel").transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = "1";

                GameObject.Find("FractionInputPanel").transform.GetChild(0).gameObject.SetActive(false);
                GameObject.Find("NumberPanel").transform.GetChild(0).gameObject.SetActive(false);

                GameObject.Find("FractionInputPanel").transform.GetChild(2).gameObject.SetActive(false);
                GameObject.Find("FractionInputPanel").transform.GetChild(1).GetComponent<Button>().enabled = false;

                DisableSubmitButton();
            }
        }

    }

    public void DoneForObj5Quest1()
    {
        if (GameObject.Find("FractionInputPanel").transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text == "2")
        {

            FindObjectOfType<conversationManager>().playCorrect();
            GameObject.Find("NumberPanel").transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("FractionInputPanel").transform.GetChild(0).GetComponent<Button>().enabled = false;
            GameObject.Find("FractionInputPanel").transform.GetChild(1).GetComponent<Button>().enabled = true;
            DisableSubmitButton();
            GameObject.FindObjectOfType<timeline_new>().load_next();
           
        }
        else
        {
            if (numberOfAttempt < 3)
            {
                FindObjectOfType<conversationManager>().playError();
                FindObjectOfType<timeline_new>().playAudioOnRelearn("You_can_do_better.wav");
                //GameObject.FindObjectOfType<conversationManager>().EnableDialouge(" You can do a better job in counting than this. Give it another try.");
                GameObject.FindObjectOfType<conversationManager>().EnableConversation(" You can do a better job in counting than this. Give it another try.");
                DisableSubmitButton();
                numberOfAttempt++;
                Invoke("DisableConvo_ForQuestionHints", GameObject.FindObjectOfType<timeline_new>().lapa.length_of_audio + 1.5f);
            }
            else
            {
                //CountingFlashcard.SetActive(true);
                ShowBeforeFashCard();
                //GameObject.Find("NumberPanel").transform.GetChild(0).gameObject.SetActive(false);
                //GameObject.Find("FractionInputPanel").transform.GetChild(0).GetComponent<Button>().enabled = false;
                //GameObject.Find("FractionInputPanel").transform.GetChild(1).GetComponent<Button>().enabled = true;

                //DoneButton.gameObject.SetActive(false);
            }
        }

    }

    void DisableConvo_ForQuestionHints()
    {
        GameObject.FindObjectOfType<conversationManager>().DisableConversation();
        EnableSubmitButton();

    }

    void HintDisable()
    {
        GameObject.FindObjectOfType<conversationManager>().DisableDialouge();
        EnableSubmitButton();

        //GameObject.FindObjectOfType<timeline_new>().load_next();

    }

    public void DoneForObj5Quest2()
    {
        //DisableForObj3Quest2();
        //DoneButton.onClick.RemoveListener(DoneForObj5Quest2);
        if (GameObject.Find("FractionInputPanel").transform.GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text == "8")
        {
            FindObjectOfType<conversationManager>().playCorrect();
            GameObject.Find("NumberPanel").transform.GetChild(0).gameObject.SetActive(false);


            //GameObject.Find("FractionInputPanel").transform.GetChild(0).gameObject.SetActive(false);
            //GameObject.Find("FractionInputPanel").transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            //GameObject.Find("FractionInputPanel").transform.GetChild(1).gameObject.SetActive(false);
            //GameObject.Find("FractionInputPanel").transform.GetChild(2).gameObject.SetActive(false);

            //GameObject.Find("FractionInputPanel").transform.GetChild(0).GetComponent<Button>().enabled = false;
            //GameObject.Find("FractionInputPanel").gameObject.SetActive(false);
            GameObject.Find("FractionInputPanel").transform.GetChild(1).GetComponent<Button>().enabled = false;
            numberOfAttempt++;
            
            DisableSubmitButton();
            GameObject.FindObjectOfType<timeline_new>().load_next();
        }
        else
        {
            if (numberOfAttempt < 3)
            {
                FindObjectOfType<conversationManager>().playError();
                FindObjectOfType<timeline_new>().playAudioOnRelearn("You_can_do_better.wav");
                //GameObject.FindObjectOfType<conversationManager>().EnableDialouge(" You can do a better job in counting than this. Give it another try.");
                GameObject.FindObjectOfType<conversationManager>().EnableConversation(" You can do a better job in counting than this. Give it another try.");
                GameObject.Find("NumberPanel").transform.GetChild(0).gameObject.SetActive(true);
                DisableSubmitButton();
                numberOfAttempt++;
                Invoke("DisableHintQuest2", 10.0f);
                Invoke("DisableConvo_ForQuestionHints", GameObject.FindObjectOfType<timeline_new>().lapa.length_of_audio + 1.5f);
            }
            else
            {
                //CountingFlashcard.SetActive(true);
                ShowBeforeFashCard();
                //GameObject.Find("NumberPanel").transform.GetChild(0).gameObject.SetActive(false);
                //GameObject.Find("FractionInputPanel").transform.GetChild(0).GetComponent<Button>().enabled = false;
                //GameObject.Find("FractionInputPanel").transform.GetChild(1).GetComponent<Button>().enabled = true;

                //DoneButton.gameObject.SetActive(false);
            }
        }

    }
    void DisableHintQuest2()
    {
        GameObject.FindObjectOfType<conversationManager>().DisableDialouge();
    }

    //ro questions for objective1

    //Questions for Objective 6
    public void GetallNumberButtonsObj6()
    {
        foreach (GameObject b in GameObject.FindGameObjectsWithTag("Numbers"))
        {
            b.GetComponent<Button>().onClick.AddListener(InputNumbersForObj6);
        }
    }
    public void InputNumbersForObj6()
    {
        var currentEventSystem = EventSystem.current;
        var currentSelectedGameObject = currentEventSystem.currentSelectedGameObject;
        if (isNum)
        {
            Debug.Log("numerator =" + currentSelectedGameObject.name);
            GameObject.Find("FractionInputPanel").transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = GameObject.Find("FractionInputPanel").transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text + currentSelectedGameObject.name;

        }

        if (isDenum)
        {
            Debug.Log("denominator =" + currentSelectedGameObject.name);
            GameObject.Find("FractionInputPanel").transform.GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text = GameObject.Find("FractionInputPanel").transform.GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text + currentSelectedGameObject.name;

        }


    }
    public void EnableForObj6Quest1()
    {
        numberOfAttempt = 0;
        GameObject.Find("NumberPanel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("FractionInputPanel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("FractionInputPanel").transform.GetChild(1).gameObject.SetActive(true);
        GameObject.Find("FractionInputPanel").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(ClickOnNumerator);
        GameObject.Find("FractionInputPanel").transform.GetChild(1).gameObject.GetComponent<Button>().onClick.AddListener(ClickOnDenominator);
        GetallNumberButtonsObj6();
        EnableSubmitButton();
        DoneButton.onClick.AddListener(DoneForObj6Quest1);
        ClickOnNumerator();


    }
    public void DoneForObj6Quest1()
    {
        checkAnswerObj6();


    }

    void checkAnswerObj6()
    {
        if (GameObject.Find("FractionInputPanel").transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text != null && GameObject.Find("FractionInputPanel").transform.GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text != null)
        {
            if (GameObject.Find("FractionInputPanel").transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text == "3" && GameObject.Find("FractionInputPanel").transform.GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text == "5")
            {
                GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
                enableFade();
            }
            else
            {
                if (numberOfAttempt < 2)
                {
                    GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                    ReInforse_Q6H1();
                    numberOfAttempt++;
                    DisableSubmitButton();

                }
                else
                {

                    DisableSubmitButton();
                    DoneButton.onClick.RemoveListener(DoneForObj6Quest1);
                    Debug.Log("move obj5_lo1");
                    UtilityArtifacts.loading_pos = "Obj5_Lo1_from_obj6";
                    UtilityArtifacts.coming_back_from = "to_Obj6_quest1";
                    UtilityArtifacts.backTraversal = true;
                    UtilityArtifacts.comingbackafterTraversal = false;
                    UtilityArtifacts.loadStartingpoint = 6;
                    UtilityArtifacts.loadEndingpoint = 10;
                    UtilityArtifacts.loadStartingpointforcomingback = 21;
                    UtilityArtifacts.loadStartingpoint = 5;
                    UtilityArtifacts.loadEndingpoint = 11;
                    // load traversescene 5
                    //SceneManager.LoadScene("Obj5AppleGroup");
                    OnTraversal(156, 129);
                }
            }
        }
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

    void ReInforse_Q6H1()
    {

        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(okQuest2);


        //GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj6_Hint1.wav");
        GameObject.FindObjectOfType<conversationManager>().EnableDialouge("\\frac{Number of Red apples}{total number of Apples}");

    }
    void okQuest2()
    {

        CancelInvoke();
        GameObject.Find("FractionInputPanel").transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = null;
        GameObject.Find("FractionInputPanel").transform.GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text = null;

        convesationDisableFirst();
        EnableSubmitButton();
    }
    void okQuest2_1()
    {

        CancelInvoke();
        enableFade();

    }
    void enableFade()
    {
        GameObject.FindObjectOfType<conversationManager>().DisableDialouge();
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
        Invoke("nextObjective", 3.0f);
    }
    void nextObjective()
    {
        Invoke("nextObjectiveVo", 3.0f);

        GameObject.Find("FractionInputPanel").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("FractionInputPanel").transform.GetChild(1).gameObject.SetActive(false);
        GameObject.Find("NumberPanel").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("Canvas").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("NumberLine").transform.GetChild(0).gameObject.SetActive(true);
        DisableSubmitButton();
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadeout");
    }
    void nextObjectiveVo()
    {
        GameObject.FindObjectOfType<timeline_new>().load_next();
    }
}
