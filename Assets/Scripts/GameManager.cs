using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // for obj1
    public float AppleFallingSpeed;
    public bool IsGameOn, isGamePause;
    public bool isFirstTime = true;
    public bool isObj1On, isObj3On, isObj4On, isObj5On, isObj11On, isObj10On, isObj5_AppleGroup;
    Transform DropOffMenu;
    Button GameOver, GameQuit,DropCancel;

    public delegate void LogMessage(string cases);
    public static event LogMessage onLogMessage;

    public void Start()
    {
        getUiButtonsandAssignFunctions();
    }

    public void Initialization()

    {
        Invoke("FadePanelOff", 3.0f);
    }
    void FadePanelOff()
    {

        //GameObject.Find("Canvas").transform.GetChild(3).GetComponent<Animator>().enabled = false;
        //GameObject.Find("Canvas").transform.GetChild(3).gameObject.SetActive(false);
        StartGameInstruction();

    }
    public void StartGameInstruction()
    {
        GameObject.Find("Canvas").transform.GetChild(1).gameObject.SetActive(true);
        GameObject.Find("Guide").GetComponent<conversationManager>().enabled = true;
    }
    public void StopGameInstruction()
    {
        GameObject.FindObjectOfType<conversationManager>().DisableConversation();
        GameObject.Find("Guide").GetComponent<conversationManager>().enabled = false;
        GameObject.Find("GardenCamera").GetComponent<switch_cam>().enabled = true;
        StartGame();

    }
    void StartGame()
    {
        IsGameOn = true;
        Invoke("StartTime", 4.2f);
    }
    void StartTime()
    {
        GameObject.Find("Canvas").transform.GetChild(2).gameObject.SetActive(true);
        GameObject.FindObjectOfType<conversationManager>().EnableQuestion1("Shake the trees to make the apples fall. Tap on the apples on the ground to collect them.");
    }
    void StopGame()
    {
        IsGameOn = false;

    }

    void getUiButtonsandAssignFunctions()
    {
        GameObject.Find("GameUIButtonPanel").transform.GetChild(0).GetChild(0).GetComponent<Button>().onClick.AddListener(ShowDropOffMenu);
        GameObject.Find("GameUIPanel").transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Button>().onClick.AddListener(ExitYes);
        GameObject.Find("GameUIPanel").transform.GetChild(0).GetChild(0).GetChild(2).GetComponent<Button>().onClick.AddListener(ExitNo);
        GameObject.Find("GameUIButtonPanel").transform.GetChild(0).GetChild(2).GetComponent<Button>().onClick.AddListener(Play);
        GameObject.Find("GameUIButtonPanel").transform.GetChild(0).GetChild(3).GetComponent<Button>().onClick.AddListener(Pause);
        GameObject.Find("GameUIPanel").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("GameUIButtonPanel").transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
        GameObject.Find("GameUIButtonPanel").transform.GetChild(0).GetChild(1).gameObject.SetActive(false);

        DropOffMenu = GameObject.Find("DropOffMenu").transform;
        GameOver = GameObject.Find("BtObjOver").GetComponent<Button>();
        GameOver.onClick.AddListener(ApplicationOver);
        GameQuit = GameObject.Find("ButtonQuit").GetComponent<Button>(); 
        GameQuit.onClick.AddListener(ApplicationQuit);
        DropCancel =  GameObject.Find("BtCancel").GetComponent<Button>();
        DropCancel.onClick.AddListener(CancelDropOff);
        HideDropOffMenu();
    }

    void CancelDropOff()
    {
        if (DropOffMenu.gameObject.active)
        {
            GameObject.FindObjectOfType<timeline_new>().resume_app();
            HideDropOffMenu();
        }
    }

    void ApplicationOver()
    {
        if (DropOffMenu.gameObject.active)
        {
            GameObject.FindObjectOfType<timeline_new>().SkipApp();
           
            HideDropOffMenu();
        }
        UtilityArtifacts.GameQuit = false;
        // print outputmessgae
        if(SceneManager.GetActiveScene().name == "Introdunction&Game")
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //OnGameOver();
    }

    public void OnGameOver()
    {
        if (UtilityArtifacts.backTraversal)
        {
            //user has skiped inside traversal
            UtilityArtifacts.backTraversal = false;
            UtilityArtifacts.comingbackafterTraversal = true;
            UtilityArtifacts.loadStartingpoint = 0;
            UtilityArtifacts.loadEndingpoint = 0;

        }else if (UtilityArtifacts.comingbackafterTraversal)
        {
            if(string.Equals(UtilityArtifacts.loading_pos, "Obj4_Lo1_from_obj12"))
            {
                UtilityArtifacts.loading_pos = "";
                UtilityArtifacts.coming_back_from = "to_Obj12_quest2";
                UtilityArtifacts.backTraversal = false;
                UtilityArtifacts.comingbackafterTraversal = true;
                UtilityArtifacts.loadStartingpointforcomingback = 21;
                UtilityArtifacts.loadStartingpoint = 0;
                UtilityArtifacts.loadEndingpoint = 0;
            }
            else if (string.Equals(UtilityArtifacts.loading_pos, "Obj11_Lo4_from_obj12"))
            {
                UtilityArtifacts.loading_pos = "Obj11_Lo4_from_obj12";
                UtilityArtifacts.coming_back_from = "to_Obj12_quest1";
                UtilityArtifacts.backTraversal = false;
                UtilityArtifacts.comingbackafterTraversal = true;
                UtilityArtifacts.loadStartingpointforcomingback = 16;
                UtilityArtifacts.loadStartingpoint = 0;
                UtilityArtifacts.loadEndingpoint = 0;
            }
            else
            {
                UtilityArtifacts.loading_pos = "";
                UtilityArtifacts.coming_back_from = "";
                UtilityArtifacts.backTraversal = false;
                UtilityArtifacts.comingbackafterTraversal = false;
                UtilityArtifacts.loadStartingpointforcomingback = 0;
                UtilityArtifacts.loadStartingpoint = 0;
                UtilityArtifacts.loadEndingpoint = 0;
            }
        }
        else
        {
            UtilityArtifacts.loading_pos = "";
            UtilityArtifacts.coming_back_from = "";
            UtilityArtifacts.backTraversal = false;
            UtilityArtifacts.comingbackafterTraversal = false;
            UtilityArtifacts.loadStartingpointforcomingback = 0;
            UtilityArtifacts.loadStartingpoint = 0;
            UtilityArtifacts.loadEndingpoint = 0;
        }
       


        UtilityArtifacts.GameQuit = false;
        //NewIncomingMsg mg = UtilityArtifacts.NJMsg;
        //mg.current_activity_status = 2;
        //UtilityArtifacts.UnityMsg = UtilityArtifacts.outGoingMessage(mg, "false");
        GameObject.FindObjectOfType<timeline_new>().SkipApp();
        SceneManager.LoadScene(1);//UtilityArtifacts.NJLoadScene
    }

    void ApplicationQuit()
    {
        if (DropOffMenu.gameObject.active)
        {
            //GameObject.FindObjectOfType<timeline_new>().SkipApp();

            HideDropOffMenu();
        }
        UtilityArtifacts.GameQuit = true;
        //UtilityArtifacts.UnityMsg = UtilityArtifacts.outGoingMessage(UtilityArtifacts.NJMsg, "true");
        //SceneManager.LoadScene(1);
        Application.Quit();
    }

    void HideDropOffMenu()
    {
        DropOffMenu.gameObject.SetActive(false);
    }

    void ShowDropOffMenu()
    {
        Debug.Log("ShowDropOffMenu");
        DropOffMenu.gameObject.SetActive(true);
        GameObject.FindObjectOfType<timeline_new>().pause_app();
    }


    void Play()
    {
        if (isGamePause)
        {
            GameObject.FindObjectOfType<timeline_new>().resume_app();
            isGamePause = false;
            GameObject.Find("GameUIButtonPanel").transform.GetChild(0).GetChild(3).gameObject.SetActive(true);
            GameObject.Find("GameUIButtonPanel").transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
            GameObject.Find("GameUIButtonPanel").transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
        }

    }
    void Pause()
    {
        if (!isGamePause)
        {

            isGamePause = true;
            GameObject.Find("GameUIButtonPanel").transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
            GameObject.Find("GameUIButtonPanel").transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
            GameObject.Find("GameUIButtonPanel").transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
            GameObject.FindObjectOfType<timeline_new>().pause_app();
        }
    }
    void Exit()
    {
        GameObject.Find("GameUIPanel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.FindObjectOfType<timeline_new>().pause_app();
    }
    void ExitYes()
    {

        Time.timeScale = 1.0f;
        //Application.Quit();
        //new quit for showcase
        if (onLogMessage != null)
        {
            UtilityREST.msgHead = "end";
            onLogMessage("Application Quit ");
        }
        // SceneManager.LoadScene(UtilityArtifacts.NJLoadScene);
        OnGameOver();
    }
    void ExitNo()
    {
        GameObject.FindObjectOfType<timeline_new>().resume_app();
        GameObject.Find("GameUIPanel").transform.GetChild(0).gameObject.SetActive(false);

    }
}