using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginSceneManager : Singleton<LoginSceneManager> {

    // Use this for initialization
    public delegate void DemoLogin();
    public static event DemoLogin OnDemoLogin;

    public delegate void LogMessage(string cases);
    public static event LogMessage onLogMessage;
    public GameObject loadingBar;
    public GameObject FadeImage;
    public GameObject menu;
    public float fadeSpeed = 1f;
    public bool isEquivalent;
    Image fadeImg;
    bool fadetoBlack;
    //13-02-2020
    int loadScene = 1;

    private void Awake()
    {
        Time.timeScale = 1;
    }

    private void OnEnable()
    {
        CallRESTServices.OnLoginSuccssfull += loadNextScene;
    }

    private void OnDisable()
    {
        CallRESTServices.OnLoginSuccssfull -= loadNextScene;
    }
    void Start () {
        
        loadingBar.SetActive(false);
        fadetoBlack = false;
        fadeImg = FadeImage.GetComponent<Image>();
        FadeImage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (fadetoBlack)
        {

            fadeImg.color = Color.Lerp(fadeImg.color, Color.black, fadeSpeed * Time.deltaTime);
            if (fadeImg.color.a >= 0.99f)
            {
                fadetoBlack = false;
                StartCoroutine(LoadYourAsyncScene());
                //  test

            }
        }

      //  Debug.Log(Time.timeScale);
    }

    public void Login(int  sceneNumber)
    {
        Debug.Log("sceneNumber: " + sceneNumber);
        loadScene = 10;// sceneNumber;
        UtilityArtifacts.logInScene = sceneNumber;
        if (sceneNumber == 6)
        {
            UtilityREST.without_objective = 154;
            UtilityREST.subtopic_id = 128;
            UtilityREST.qType_id = "267,299,355";
            PlayerPrefs.SetInt("First", 2);
        }
        else
        if (sceneNumber == 5)
        {
            UtilityREST.without_objective = 153;
            UtilityREST.subtopic_id = 128;
            UtilityREST.qType_id = "268";
            PlayerPrefs.SetInt("First", 2);
        }
        else
            UtilityREST.without_objective = 154;



        //02-06-2020 canvas will not be loaded load main menu
        //loadScene = UtilityArtifacts.CanvasSceneNumber;
        //loadScene = 7;
        UtilityArtifacts.coming_back_from = "";
        //loadScene = 4;
        if (OnDemoLogin != null)
            OnDemoLogin();

        //if (onLogMessage != null)
        //{
        //    UtilityREST.msgHead = "start";
        //    onLogMessage("User starts the Learning Session with ‘Concrete Experience’");
        //}
        UtilityArtifacts.isobj16 = true;
        loadingBar.SetActive(true);
        UtilityArtifacts.backTraversal = false;
        UtilityArtifacts.comingbackafterTraversal = true;
        UtilityArtifacts.loadStartingpointforcomingback = 0;
        UtilityArtifacts.loadStartingpoint = 0;
        UtilityArtifacts.loadEndingpoint = 0;
        // testing canvas
        ///Invoke("LoadWithoutLogin", 4f);
    }

    void LoadWithoutLogin()
    {
        loadNextScene(true);
    }

    public void Reset()
    {
        PlayerPrefs.SetString(UtilityREST.token_name, "");
    }

    public void loadNextScene(bool status)
    {
        if (status)
        {
            Debug.Log("fade  true");
            fadetoBlack = true;
            FadeImage.SetActive(true);
            fadeImg.color = Color.clear;
            UtilityArtifacts.backTraversal = false;
            UtilityArtifacts.comingbackafterTraversal = true;
            UtilityArtifacts.loadStartingpointforcomingback = 0;
            UtilityArtifacts.loadStartingpoint = 0;
            UtilityArtifacts.loadEndingpoint = 0;
            Debug.Log("fade done");
        }
        else
        {
            // show Dialog Loading Issue
            Debug.Log("fade  fallse");
        }

    }

    IEnumerator LoadYourAsyncScene()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.
        // for obj2 7
        UtilityArtifacts.currentScene = loadScene;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(loadScene);

        // Wait until the asynchronous scene fully loads
        UtilityArtifacts.current_json = UtilityArtifacts.json_story_board;
        while (!asyncLoad.isDone)
        {

            yield return null;
        }

    }


}
