using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Obj16_RO_Manager : MonoBehaviour
{
    public delegate void LogMessage(string cases);
    public static event LogMessage onLogMessage;
    public string ans;
    public List<string> ansList = new List<string>();
    GameObject reinforcementPanel_1, reinforcementPanel_2, reinforcementPanel_3, reinforcementPanel_4_1, reinforcementPanel_4_2;
    TEXDraw reinforcementText_RO_1;
    GameObject LoadCanvas;
    public GameObject temp;
    void OnEnable()
    {

    }
    private void Start()
    {
        ans = null;

        reinforcementPanel_1 = GameObject.Find("RO_Panel").transform.GetChild(0).GetChild(0).GetChild(3).GetChild(0).gameObject;
        reinforcementPanel_2 = GameObject.Find("RO_Panel").transform.GetChild(0).GetChild(1).GetChild(3).GetChild(0).gameObject;
        reinforcementPanel_3 = GameObject.Find("RO_Panel").transform.GetChild(0).GetChild(2).GetChild(3).GetChild(0).gameObject;
        reinforcementPanel_4_1 = GameObject.Find("RO_Panel").transform.GetChild(0).GetChild(3).GetChild(3).GetChild(0).gameObject;
        reinforcementPanel_4_2 = GameObject.Find("RO_Panel").transform.GetChild(0).GetChild(3).GetChild(4).GetChild(0).gameObject;
        reinforcementText_RO_1 = reinforcementPanel_1.transform.GetChild(0).GetChild(0).GetComponent<TEXDraw>();
        LoadCanvas = transform.GetChildFromName<Transform>("LoadCanvas").gameObject;
        LoadCanvas.SetActive(false);
    }
    public void Initiliaze()
    {
        EnableSubmitButtonRO1();
        GetallNumberButtons();

    }
    public void Initiliaze_RO2()
    {
        GetallNumberButtons();
        EnableSubmitButtonRO2();
    }
    public void Initiliaze_RO3()
    {
        GetallNumberButtons();
        EnableSubmitButtonRO3();

    }
    public void Initiliaze_RO4()
    {
        GetallNumberButtons();
        EnableSubmitButtonRO4();

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
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(Submit1);

        reinforcementPanel_1.transform.GetChild(2).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        reinforcementPanel_1.transform.GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(ok1);
        //first ro
        if (onLogMessage != null)
        {
            onLogMessage("Only option A is correct  <br>" + 
                "If the user selects option B, he/she has a misconception that ‘Addition’ will result in an Equivalent Fraction <br> " +
                "If the user selects option C, he/she has a misconception that ‘Subtraction’ will result in an Equivalent Fraction <br>" +
                "If the user selects option D, he/she has not understood the ‘Learning Objective’ ");
        }
    }
    public void Submit1()
    {
        if (ans != null)
        {
            ansList.Add(ans);
            check_RO1_ans();
        }
    }
    void check_RO1_ans()
    {
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.SetActive(false);
        deselect_option();
        if (ans == "1")
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            enableFade();
            reset_option();
            if (onLogMessage != null)
            {
                onLogMessage("user knows equivalent fraction");
            }
        }
        else if (ans == "2")
        {
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.SetActive(false);
                GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj16_let_see_why_common.wav");
                Invoke("ReInforse_RO1_2", 5);
                temp.GetComponent<Image>().color = Color.red;
              
            }
            if (onLogMessage != null)
            {
                onLogMessage("User has a misconception about adding will result in an equivalent fraction");
            }
        }
        else if (ans == "3")
        {
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.SetActive(false);
                GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj16_let_see_why_common.wav");
                Invoke("ReInforse_RO1_3", 5);
                temp.GetComponent<Image>().color = Color.red;
               
            }
            if (onLogMessage != null)
            {
                onLogMessage("User has a misconception that ‘Subtraction’ will result in an Equivalent Fraction");
            }
        }
        else if (ans == "4")
        {
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.SetActive(false);
                GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj16_let_see_why_common.wav");
                Invoke("ReInforse_RO1_4", 5);
                temp.GetComponent<Image>().color = Color.red;
              //  ReInforse_RO1_4();
            }
            if (onLogMessage != null)
            {
                onLogMessage("User has not understood the ‘Learning Objective’");
            }
        }
    }
    public void ReInforse_RO1_2()
    {
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj16_RO1_b.wav");
        reinforcementPanel_1.SetActive(true);
        reinforcementText_RO_1.text = "Adding \\frac{2}{2} to \\frac{1}{4} will result in a fraction that is bigger than \\frac{1}{4} and hence the result will not be equivalent to \\frac{1}{4}. You need to multiply \\frac{1}{4} with a fraction that has a value of 1 to get a fraction that is equal in value to \\frac{1}{4}. ";
    }

    public void ReInforse_RO1_3()
    {
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj16_RO1_c.wav");
        reinforcementPanel_1.SetActive(true);
        reinforcementText_RO_1.text = "Subtracting \\frac{2}{2} from \\frac{1}{4} will result in a fraction that is bigger than \\frac{1}{4} and hence the result will not be equivalent to \\frac{1}{4}. You need to multiply \\frac{1}{4} with a fraction that has a value of 1 to get a fraction that is equal in value to \\frac{1}{4}.";
    }

    public void ReInforse_RO1_4()
    {
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj16_RO1_d.wav");
        reinforcementPanel_1.SetActive(true);
        reinforcementText_RO_1.text = "You need to multiply \\frac{1}{4} with a fraction that has a value of 1 to get a fraction that is equal in value to \\frac{1}{4}";
    }

    public void EnableSubmitButtonRO2()
    {
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(Submit2);
        reinforcementPanel_2.transform.GetChild(2).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        reinforcementPanel_2.transform.GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(ok2);
        if (onLogMessage != null)
        {
            onLogMessage("For question 1, only option A is correct <br>" + "if the user selects option B or C, he/she has not understood  the ‘Learning Objective’ ");
        }
        }
    public void Submit2()
    {
        if (ans != null)
        {
            ansList.Add(ans);
            check_RO2_ans();
        }
    }
    void check_RO2_ans()
    {
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.SetActive(false);
        deselect_option();
        if (ans == "1")
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            enableFade();
            reset_option();
            if (onLogMessage != null)
            {
                onLogMessage("User understood learning Objective");
            }
        }
        else
        {
            GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
            GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj16_let_see_why_common.wav");
            Invoke("ReInforse_RO2_1", 5);
            temp.GetComponent<Image>().color = Color.red;
           // ReInforse_RO2_1();
            if (onLogMessage != null)
            {
                onLogMessage("User has not understood  the ‘Learning Objective’ ");
            }
        }
    }
    public void ReInforse_RO2_1()
    {
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj16_RO2_3_Wrong.wav");
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.SetActive(false);
        reinforcementPanel_2.SetActive(true);
    }

    public void EnableSubmitButtonRO3()
    {
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(Submit3);
        reinforcementPanel_3.transform.GetChild(2).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        reinforcementPanel_3.transform.GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(ok3);
        if (onLogMessage != null)
        {
            onLogMessage("For question 2, only option A is correct <br>" + "if the user selects option B or C, he/she has not understood  the ‘Learning Objective’   <br>" +
                            "Additional Diagnosis: <br>" +
                            "If Q1 is wrong and Q2 is right, user does not know that 1 is same as ‘any number over the same number’. < br>" +
                            "The user has not mastered ‘Fact Learning Objective’ in ‘Representation of Fractions - Objective 4’  ");
        }
    }
    public void Submit3()
    {
        if (ans != null)
        {
            ansList.Add(ans);
            check_RO3_ans();
        }
    }
    void check_RO3_ans()
    {
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.SetActive(false);
        deselect_option();
        if (ans == "1")
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            enableFade();
            reset_option();
            if (onLogMessage != null)
            {
                onLogMessage("User understood learning Objective");
            }
        }
        else
        {
            GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
            GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj16_let_see_why_common.wav");
            Invoke("ReInforse_RO3_1", 5);
            temp.GetComponent<Image>().color = Color.red;
            //ReInforse_RO3_1();
            if (onLogMessage != null)
            {
                onLogMessage("User has not understood  the ‘Learning Objective’ ");
            }
        }
    }
    public void ReInforse_RO3_1()
    {
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj16_RO2_3_Wrong.wav");
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.SetActive(false);
        reinforcementPanel_3.SetActive(true);
    }

    public void EnableSubmitButtonRO4()
    {
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(Submit4);

        reinforcementPanel_4_1.transform.GetChild(2).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        reinforcementPanel_4_2.transform.GetChild(2).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        reinforcementPanel_4_1.transform.GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(ok_last);
        reinforcementPanel_4_2.transform.GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(ok_last);
        if (onLogMessage != null)
        {
            onLogMessage("option A,User has a misconception that a fraction is n times greater than its Equivalent Fraction. The user has not understood that a fraction is an indication of a part and its numerator and denominator cannot be treated as separate numbers.   <br>" +
                 "If the user selects option B, User has understood the ‘Learning Objective’ <br> " +
                 "If the user selects option C, User has understood the ‘Learning Objective’ <br>" +
                 "If the user selects option D, Both options B & C are correct – User has understood the ‘Learning Objective’ <br>" +
                 "If the user selects option E, User has a misconception that a fraction is n times greater than its Equivalent Fraction. The user has not understood that a fraction is an indication of a part and its numerator and denominator cannot be treated as separate numbers. ");
        }
    }
    public void Submit4()
    {
        if (ans != null)
        {
            ansList.Add(ans);
            check_RO4_ans();
        }
    }
    void check_RO4_ans()
    {
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.SetActive(false);
        deselect_option();
        if (ans == "1" || ans == "5")
        {
            //GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            //enableFade();
            reset_option();
            GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj16_let_see_why_common.wav");
            Invoke("ReInforse_RO4_1", 5);
            temp.GetComponent<Image>().color = Color.red;
            //ReInforse_RO4_1();
            if (onLogMessage != null)
            {
                onLogMessage("User has a misconception that a fraction is n times greater than its Equivalent Fraction. The user has not understood that a fraction is an indication of a part and its numerator and denominator cannot be treated as separate numbers. ");
            }
        }
        else
        {
            //GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
           // GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj16_let_see_why_common.wav");
           // Invoke("ReInforse_RO4_2", 5);
            temp.GetComponent<Image>().color = Color.green;
            ReInforse_RO4_2();
            if (onLogMessage != null)
            {
                onLogMessage("User has understood the ‘Learning Objective’");
            }
        }

    }
    public void ReInforse_RO4_1()
    {
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj16_RO4_A_E.wav");
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.SetActive(false);
        reinforcementPanel_4_1.SetActive(true);
    }
    public void ReInforse_RO4_2()
    {
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj16_RO4_B_C_D.wav");
        GameObject.Find("ROSubmitButton").transform.GetChild(0).gameObject.SetActive(false);
        reinforcementPanel_4_2.SetActive(true);


    }

    void ok1()
    {
        CancelInvoke();
        convesationDisable_1();
        reset_option();

    }
    void ok2()
    {
        CancelInvoke();
        convesationDisable_2();
        reset_option();

    }
    void ok3()
    {
        CancelInvoke();
        convesationDisable_3();
        reset_option();

    }
    void ok_last()
    {
        CancelInvoke();
        lastConvesationDisable();
    }
    void convesationDisable_1()
    {
        FindObjectOfType<timeline_new>().lipsync_player.GetComponent<AudioSource>().Stop();
        reinforcementPanel_1.SetActive(false);
        enableFade();
    }
    void convesationDisable_2()
    {
        FindObjectOfType<timeline_new>().lipsync_player.GetComponent<AudioSource>().Stop();
        reinforcementPanel_2.SetActive(false);
        enableFade();
    }
    void convesationDisable_3()
    {
        FindObjectOfType<timeline_new>().lipsync_player.GetComponent<AudioSource>().Stop();
        reinforcementPanel_3.SetActive(false);
        enableFade();
    }
    void lastConvesationDisable()
    {
        FindObjectOfType<timeline_new>().lipsync_player.GetComponent<AudioSource>().Stop();
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
        Invoke("animStop", 2.5f);

        //
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
        //GameObject.Find("Fade").GetComponent<Animator>().enabled = false;
        //GameObject.Find("ExitPanel").transform.GetChild(0).gameObject.SetActive(true);
        //GameObject.Find("ExitPanel").transform.GetChild(0).GetChild(0).GetComponent<Button>().onClick.AddListener(ExitNow);
        // stop above and call 
        gameObject.GetComponent<GameManager>().OnGameOver();
        //LoadCanvas.SetActive(true);
        //Invoke("loadnextScene", 5f);
       
    }

    void loadnextScene()
    {
        if (onLogMessage != null)
        {
            onLogMessage("'Practice Canvas’ Session begins");
        }
        SceneManager.LoadScene(9);//UtilityArtifacts.CanvasSceneNumber
    }
    void ExitNow()
    {
        Application.Quit();
    }
    void enableFade()
    {
        //GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
        nextObjective1();
        //Invoke("nextObjective1", 3.0f);
    }
    void nextObjective1()
    {
        GameObject enabledPanel = GameObject.FindGameObjectWithTag("RO Panel");
        //GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadeout");
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
    void reset_option()
    {
        foreach (GameObject b in GameObject.FindGameObjectsWithTag("Numbers"))
        {
            b.transform.GetChild(2).gameObject.SetActive(false);
        }
        ans = "";
        ans = null;
    }

    void deselect_option()
    {
        foreach (GameObject b in GameObject.FindGameObjectsWithTag("Numbers"))
        {
            b.transform.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        }

    }

}
