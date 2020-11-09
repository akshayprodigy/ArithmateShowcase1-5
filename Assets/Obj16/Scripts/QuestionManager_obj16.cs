using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class QuestionManager_obj16 : MonoBehaviour
{

    public bool isNum, isDenum, isWhole;
    public Button Submit_button_for_input_panel;
    public string FuncrionName, ans;
    public int numberOfAttempt = 0;
    //28/07/2020
    public string loadScene;

    public delegate void LogMessage(string cases);
    public static event LogMessage onLogMessage;
    // For objective2
    public int noOfAppplesinTray1, noOfAppplesinTray2, noOfAppplesinTray3;
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

    // Start is called before the first frame update
    public void EnableSubmitButton()
    {
        GameObject.Find("SubmitButton").transform.GetChild(0).gameObject.SetActive(true);
        Submit_button_for_input_panel = GameObject.Find("SubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>();
    }
    public void DisableSubmitButton()
    {
        GameObject.Find("SubmitButton").transform.GetChild(0).gameObject.SetActive(false);
    }
    public void EnableForObj16Quest1()
    {
        Debug.Log("next question");
        GameObject.Find("NumberPanel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("FractionInputPanel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("FractionInputPanel").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(ClickOnNumerator);
        ClickOnNumerator();
        GameObject.Find("FractionInputPanel").transform.GetChild(1).gameObject.SetActive(true);
        GameObject.Find("FractionInputPanel").transform.GetChild(1).gameObject.GetComponent<Button>().onClick.AddListener(ClickOnDenominator);
        GameObject.Find("FractionInputPanel").transform.GetChild(2).gameObject.SetActive(true);
        GameObject.Find("SubmitButton").transform.GetChild(0).gameObject.SetActive(true);
        Submit_button_for_input_panel = GameObject.Find("SubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>();
        Submit_button_for_input_panel.onClick.AddListener(DoneForObj16Quest1);
        GetallNumberButtons();


    }
    public void DoneForObj16Quest1()
    {

        DisableSubmitButton();
        FindObjectOfType<Obj16_SceneManager>().Fold1_first_fold_hint.SetActive(false);

        FindObjectOfType<Obj16_SceneManager>().Unfold1ButtonArrow.SetActive(false);
        if (GameObject.Find("FractionInputPanel").transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text == "1" && GameObject.Find("FractionInputPanel").transform.GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text == "2")
        {

            Submit_button_for_input_panel.gameObject.SetActive(false);



            FindObjectOfType<conversationManager>().playCorrect();
            FindObjectOfType<timeline_new>().playAudioOnRelearn("obj16_input_fold1_that_is.wav");
            FindObjectOfType<conversationManager>().EnableConversation("That is correct");
            FindObjectOfType<Obj16_SceneManager>().right_prompt.SetActive(true);
            Invoke("obj16_q1_load_next", 4);
            if (onLogMessage != null)
            {
                //Debug.Log("onLogMessage ");
                onLogMessage("User knows ‘Representation of Fractions’");
            }
        }
        else
        {
            numberOfAttempt++;
            if (onLogMessage != null)
            {
                // Debug.Log("onLogMessage User dont knows");
                onLogMessage("User does not  know ‘Representation of Fractions’ ");
            }
            if (numberOfAttempt == 1)
            {
                FindObjectOfType<conversationManager>().playError();
                FindObjectOfType<timeline_new>().playAudioOnRelearn("obj16_input_fold1_this_is_quite_two_parts.wav");
                FindObjectOfType<conversationManager>().EnableConversation(" This is quite simple. There are two parts here and one of it is shaded.How will you represent part of the paper that is shaded ? ");
                Invoke("try_again_forq1", 9);
            }
            else
            {
                numberOfAttempt = 0;
                Debug.Log("move obj4_lo1");
                UtilityArtifacts.loading_pos = "Obj4_Lo1";
                UtilityArtifacts.coming_back_from = "quest1";
                UtilityArtifacts.backTraversal = true;
                UtilityArtifacts.comingbackafterTraversal = false;
                UtilityArtifacts.loadStartingpointforcomingback = 5;
                UtilityArtifacts.loadStartingpoint = 4;
                UtilityArtifacts.loadEndingpoint = 11;
                // load traversescene 4
                //OnTraversal(155, 129);

                //SceneManager.LoadScene("Obj4AreaModule");
                Invoke("TraverseToObjective4", 4);
                //loadScene = "Obj4AreaModule";
                //FindObjectOfType<Obj16_SceneManager>().Traverse_Connector.SetActive(true);
                //GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_16_Traverse_connector.wav");

            }

        }

    }


    void TraverseToObjective4()
    {
        if (onLogMessage != null)
        {
            //Debug.Log("onLogMessage Loading Objective 4");
            onLogMessage("Traversing the user to ‘Representation of Fractions’ objective");
        }
        SceneManager.LoadScene("Obj4AreaModule");
    }

    void try_again_forq1()
    {
        ClickOnDenominator();
        ClickOnNumerator();
        GameObject.Find("SubmitButton").transform.GetChild(0).gameObject.SetActive(true);
        Submit_button_for_input_panel.onClick.RemoveAllListeners();
        Submit_button_for_input_panel = GameObject.Find("SubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>();
        Submit_button_for_input_panel.onClick.AddListener(DoneForObj16Quest1);
        FindObjectOfType<conversationManager>().DisableConversation();
    }
    void try_again_forq2()
    {
        ClickOnDenominator();
        ClickOnNumerator();
        GameObject.Find("SubmitButton").transform.GetChild(0).gameObject.SetActive(true);
        Submit_button_for_input_panel.onClick.RemoveAllListeners();
        Submit_button_for_input_panel = GameObject.Find("SubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>();
        Submit_button_for_input_panel.onClick.AddListener(DoneForObj16Quest2);
        FindObjectOfType<conversationManager>().DisableConversation();
    }
    void try_again_forq3()
    {
        ClickOnDenominator();
        ClickOnNumerator();
        GameObject.Find("SubmitButton").transform.GetChild(0).gameObject.SetActive(true);
        Submit_button_for_input_panel.onClick.RemoveAllListeners();
        Submit_button_for_input_panel = GameObject.Find("SubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>();
        Submit_button_for_input_panel.onClick.AddListener(DoneForObj16Quest3);
        FindObjectOfType<conversationManager>().DisableConversation();
    }
    public void DisableForObj16Quest()
    {
        GameObject.Find("NumberPanel").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("FractionInputPanel").transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = "";
        GameObject.Find("FractionInputPanel").transform.GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text = "";
        GameObject.Find("FractionInputPanel").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("FractionInputPanel").transform.GetChild(1).gameObject.SetActive(false);
        GameObject.Find("FractionInputPanel").transform.GetChild(2).gameObject.SetActive(false);
        isNum = false;
        isDenum = false;
        isDenum = false;

    }



    public void EnableForObj16Quest2()
    {
        numberOfAttempt = 0;
        if (UtilityArtifacts.coming_back_from != "quest2")
        {
            Submit_button_for_input_panel.onClick.RemoveAllListeners();

        }
        GameObject.Find("NumberPanel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("FractionInputPanel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("FractionInputPanel").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(ClickOnNumerator);
        ClickOnNumerator();
        GameObject.Find("FractionInputPanel").transform.GetChild(1).gameObject.SetActive(true);
        GameObject.Find("FractionInputPanel").transform.GetChild(1).gameObject.GetComponent<Button>().onClick.AddListener(ClickOnDenominator);
        GameObject.Find("FractionInputPanel").transform.GetChild(2).gameObject.SetActive(true);
        GameObject.Find("SubmitButton").transform.GetChild(0).gameObject.SetActive(true);
        Submit_button_for_input_panel = GameObject.Find("SubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>();
        Submit_button_for_input_panel.onClick.AddListener(DoneForObj16Quest2);

        if (UtilityArtifacts.coming_back_from == "quest2")
        {
            GetallNumberButtons();
        }

    }
    public void DoneForObj16Quest2()
    {

        DisableSubmitButton();
        if (GameObject.Find("FractionInputPanel").transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text == "2" && GameObject.Find("FractionInputPanel").transform.GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text == "4")
        {
            DisableSubmitButton();
            GameObject.Find("FractionInputPanel").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
            GameObject.Find("NumberPanel").transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("FractionInputPanel").transform.GetChild(0).GetComponent<Button>().enabled = false;
            Submit_button_for_input_panel.gameObject.SetActive(false);



            FindObjectOfType<conversationManager>().playCorrect();
            FindObjectOfType<timeline_new>().playAudioOnRelearn("obj16_input_fold1_that_is.wav");
            FindObjectOfType<conversationManager>().EnableConversation("That is correct");
            FindObjectOfType<Obj16_SceneManager>().right_prompt.SetActive(true);
            Invoke("obj16_q2_load_next", 4);
            if (onLogMessage != null)
            {
                onLogMessage("User knows ‘Representation of Fractions’");
            }
        }
        else
        {
            numberOfAttempt++;
            if (onLogMessage != null)
            {
                onLogMessage("User does not  know ‘Representation of Fractions’ ");
            }
            if (numberOfAttempt == 1)
            {
                FindObjectOfType<conversationManager>().playError();
                FindObjectOfType<timeline_new>().playAudioOnRelearn("obj16_input_fold1_this_is_quite_four_parts.wav");
                FindObjectOfType<conversationManager>().EnableConversation(" This is quite simple. There are four parts here and two of it is shaded. How will you represent part of the paper that is shaded ? ");
                Invoke("try_again_forq2", 9);
            }
            else
            {
                numberOfAttempt = 0;
                Debug.Log("move obj4_lo1");
                UtilityArtifacts.loading_pos = "Obj4_Lo1";
                UtilityArtifacts.coming_back_from = "quest2";

                UtilityArtifacts.backTraversal = true;
                UtilityArtifacts.comingbackafterTraversal = false;
                UtilityArtifacts.loadStartingpointforcomingback = 11;
                UtilityArtifacts.loadStartingpoint = 4;
                UtilityArtifacts.loadEndingpoint = 11;
                // load traversescene 4
                //OnTraversal(155, 129);

                //SceneManager.LoadScene("Obj4AreaModule");
                //loadScene = "Obj4AreaModule";
                //FindObjectOfType<Obj16_SceneManager>().Traverse_Connector.SetActive(true);
                //GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_16_Traverse_connector.wav");
                //if (onLogMessage != null)
                //{
                //    onLogMessage("Traversing the user to ‘Representation of Fractions’ objective");
                //}
                Invoke("TraverseToObjective4", 4);
            }


        }

    }

    public void EnableForObj16Quest3()
    {
        numberOfAttempt = 0;
        if (UtilityArtifacts.coming_back_from != "quest3")
        {
            Submit_button_for_input_panel.onClick.RemoveAllListeners();
        }

        GameObject.Find("NumberPanel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("FractionInputPanel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("FractionInputPanel").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(ClickOnNumerator);
        ClickOnNumerator();
        GameObject.Find("FractionInputPanel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("FractionInputPanel").transform.GetChild(0).gameObject.GetComponent<Button>().enabled = true;
        GameObject.Find("FractionInputPanel").transform.GetChild(1).gameObject.SetActive(true);
        GameObject.Find("FractionInputPanel").transform.GetChild(1).gameObject.GetComponent<Button>().onClick.AddListener(ClickOnDenominator);
        GameObject.Find("FractionInputPanel").transform.GetChild(2).gameObject.SetActive(true);
        GameObject.Find("SubmitButton").transform.GetChild(0).gameObject.SetActive(true);
        Submit_button_for_input_panel = GameObject.Find("SubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>();
        Submit_button_for_input_panel.onClick.AddListener(DoneForObj16Quest3);
        FindObjectOfType<Obj16_SceneManager>().Fold_1.SetActive(false);
        FindObjectOfType<Obj16_SceneManager>().Fold_2.SetActive(false);
        if (UtilityArtifacts.coming_back_from == "quest3")
        {
            GetallNumberButtons();
        }
    }
    public void DoneForObj16Quest3()
    {

        DisableSubmitButton();
        if (GameObject.Find("FractionInputPanel").transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text == "3" && GameObject.Find("FractionInputPanel").transform.GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text == "6")
        {
            DisableSubmitButton();
            GameObject.Find("FractionInputPanel").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
            GameObject.Find("NumberPanel").transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("FractionInputPanel").transform.GetChild(0).GetComponent<Button>().enabled = false;
            Submit_button_for_input_panel.gameObject.SetActive(false);



            FindObjectOfType<conversationManager>().playCorrect();
            FindObjectOfType<timeline_new>().playAudioOnRelearn("obj16_input_fold1_that_is.wav");
            FindObjectOfType<conversationManager>().EnableConversation("That is correct");
            FindObjectOfType<Obj16_SceneManager>().right_prompt.SetActive(true);
            Invoke("obj16_q3_load_next", 4);
            if (onLogMessage != null)
            {
                onLogMessage("User knows ‘Representation of Fractions’");
            }
        }
        else
        {
            numberOfAttempt++;
            if (onLogMessage != null)
            {
                onLogMessage("User does not  know ‘Representation of Fractions’ ");
            }
            if (numberOfAttempt == 1)
            {
                FindObjectOfType<conversationManager>().playError();
                FindObjectOfType<timeline_new>().playAudioOnRelearn("obj16_input_fold1_this_is_quite_six_parts.wav");
                FindObjectOfType<conversationManager>().EnableConversation("This is quite simple. There are six parts here and three of them are shaded. How will you represent part of the paper that is shaded? ");
                Invoke("try_again_forq3", 9);
            }
            else
            {
                numberOfAttempt = 0;
                Debug.Log("move obj4_lo1");
                UtilityArtifacts.loading_pos = "Obj4_Lo1";
                UtilityArtifacts.coming_back_from = "quest3";
                UtilityArtifacts.backTraversal = true;
                UtilityArtifacts.comingbackafterTraversal = false;
                UtilityArtifacts.loadStartingpointforcomingback = 17;
                UtilityArtifacts.loadStartingpoint = 4;
                UtilityArtifacts.loadEndingpoint = 11;
                // load traversescene 4
                //OnTraversal(155, 129);

                //SceneManager.LoadScene("Obj4AreaModule");
                //loadScene = "Obj4AreaModule";
                //FindObjectOfType<Obj16_SceneManager>().Traverse_Connector.SetActive(true);
                //GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_16_Traverse_connector.wav");
                //if (onLogMessage != null)
                //{
                //    onLogMessage("Traversing the user to ‘Representation of Fractions’ objective");
                //}
                Invoke("TraverseToObjective4", 4);
            }

        }

    }

    public void EnableForObj16Quest4()
    {
        foreach (GameObject b in GameObject.FindGameObjectsWithTag("Numbers"))
        {
            b.GetComponent<Button>().onClick.AddListener(CheckAnswerForQuest4);
        }
        if (onLogMessage != null)
        {
            onLogMessage("Only option A is correct  <br>" +
"If the user selects option B, he/she has a misconception that ‘Like Fractions are same as Equivalent Fractions’ <br>" +
"If the user selects option C, he / she has not understood the ‘Concept of Equivalent Fractions’ <br>" +
"If the user selects option D, he / she has not understood the ‘Concept of Equivalent Fractions’ ");// need to change formultiple question

        }
    }
    public void CheckAnswerForQuest4()
    {
        var currentEventSystem = EventSystem.current;
        var currentSelectedGameObject = currentEventSystem.currentSelectedGameObject;

        ans = currentSelectedGameObject.name;
        if (ans != null)
        {
            if (ans == "1")
            {
                Debug.Log("Correct answer");
                Quest4Correct();
                if (onLogMessage != null)
                {
                    onLogMessage("User knows ‘Equivalent Fractions’");
                }
            }
            else
            {
                numberOfAttempt++;
                if (ans == "2")
                {
                    if (onLogMessage != null)
                    {
                        onLogMessage(" User has a misconception that ‘Like Fractions are same as Equivalent Fractions’");
                    }
                }
                else
                {
                    if (onLogMessage != null)
                    {
                        onLogMessage("User has not understood the ‘Concept of Equivalent Fractions’");
                    }
                }

                if (numberOfAttempt == 1)
                {
                    DisplayHintForQuest4();
                    Debug.Log("Wrong answer");
                }
                else
                {
                    numberOfAttempt = 0;
                    Debug.Log("move obj15_lo1");
                    UtilityArtifacts.loading_pos = "Obj15_Lo1";
                    UtilityArtifacts.coming_back_from = "quest4";
                    UtilityArtifacts.backTraversal = true;
                    UtilityArtifacts.comingbackafterTraversal = false;
                    UtilityArtifacts.loadStartingpointforcomingback = 21;
                    UtilityArtifacts.loadStartingpoint = 7;
                    UtilityArtifacts.loadEndingpoint = 17;
                    // load traversescene 15
                    //OnTraversal(162,132);


                    //loadScene = "obj_15_new_story";
                    //FindObjectOfType<Obj16_SceneManager>().Traverse_Connector.SetActive(true);
                    //GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_16_Traverse_connector.wav");
                    Invoke("LoadObjective15", 4);
                }

            }
        }
        FindObjectOfType<Obj16_SceneManager>().options.SetActive(false);
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
    void LoadObjective15()
    {
        if (onLogMessage != null)
        {
            onLogMessage("Traversing the user to ‘Recognising Equivalent Fractions’ objective");
        }
        SceneManager.LoadScene("obj_15_new_story");
    }
    void Quest4Correct()
    {
        FindObjectOfType<conversationManager>().EnableConversation("You are right");
        FindObjectOfType<timeline_new>().playAudioOnRelearn("obj16_correct.wav");
        Invoke("load_next_Quest4", 3.0f);
    }
    void DisplayHintForQuest4()
    {
        FindObjectOfType<conversationManager>().EnableConversation("If you look at the papers you will realize that the shaded area in all the papers are same. Only the number of parts are different. What are such fractions called? Try again");
        FindObjectOfType<timeline_new>().playAudioOnRelearn("obj16_hint_quest4.wav");
        Invoke("enableQuest4Option", 9.0f);
    }
    void enableQuest4Option()
    {
        FindObjectOfType<conversationManager>().DisableConversation();
        FindObjectOfType<Obj16_SceneManager>().options.SetActive(true);
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
            GameObject.Find("FractionInputPanel").transform.GetChild(3).GetChild(0).gameObject.GetComponent<Text>().text = GameObject.Find("FractionInputPanel").transform.GetChild(3).GetChild(0).gameObject.GetComponent<Text>().text + currentSelectedGameObject.name;

        }

    }

    void load_next()
    {
        DisableForObj16Quest();
        FindObjectOfType<conversationManager>().DisableConversation();
        FindObjectOfType<timeline_new>().load_next();
    }
    void load_next_Quest4()
    {
        FindObjectOfType<conversationManager>().DisableQuestion();
        FindObjectOfType<conversationManager>().DisableConversation();
        FindObjectOfType<timeline_new>().load_next();
    }

    void obj16_q1_load_next()
    {
        DisableForObj16Quest();
        FindObjectOfType<Obj16_SceneManager>().move_fold1_to_corner();
        FindObjectOfType<conversationManager>().DisableQuestion();
    }

    void obj16_q2_load_next()
    {
        DisableForObj16Quest();
        DisableSubmitButton();
        FindObjectOfType<Obj16_SceneManager>().Fold_1.SetActive(true);
        FindObjectOfType<Obj16_SceneManager>().move_fold2_to_corner();
        FindObjectOfType<conversationManager>().DisableQuestion();
    }
    void obj16_q3_load_next()
    {
        DisableForObj16Quest();
        DisableSubmitButton();
        //FindObjectOfType<Obj16_SceneManager>().move_fold2_to_corner();
        FindObjectOfType<conversationManager>().DisableQuestion();
        FindObjectOfType<timeline_new>().load_next();
    }

}
