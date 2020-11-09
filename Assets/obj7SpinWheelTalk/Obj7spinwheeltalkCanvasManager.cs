using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Obj7spinwheeltalkCanvasManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject hintPopup_Pnl, hintMsg_Text;
    Button hintOkayBtn, submitAnswerButton;
    Button btnPause, btnPlay, btnQuit, btnQuitOk, btQuitCancel, btnExit, btn_OK_TraverseToObj7;
    Transform loading, microphone;
    Transform quitPannel;
    Transform spinWheel, wheelPointer, exitPannel, traverseToObj7_Panel;
    Button btSpin;
    bool arrowOutline;
    FortuneWheelManager wheelManager;
    obj7SpinWheelTalkSceneManager manager;
    timeline_new timeline_New;
    string spinValue;
    void Start()
    {
        Initilize();
    }

    private void OnEnable()
    {
        FortuneWheelManager.OnSpinComplete += OnSpinComplete;
    }

    private void OnDisable()
    {
        FortuneWheelManager.OnSpinComplete -= OnSpinComplete;
    }
    void Initilize()
    {
        wheelManager = GameObject.FindObjectOfType<FortuneWheelManager>();
        manager = GameObject.FindObjectOfType<obj7SpinWheelTalkSceneManager>();
        timeline_New = GameObject.FindObjectOfType<timeline_new>();
        exitPannel = transform.GetChildFromName<Transform>("ExitPanel");
        traverseToObj7_Panel = transform.GetChildFromName<Transform>("TraverseToObj7_Panel");
        loading = transform.GetChildFromName<Transform>("Loading");
        microphone = transform.GetChildFromName<Transform>("Microphone"); 
        spinWheel = transform.GetChildFromName<Transform>("Fortune Wheel Window");
        wheelPointer = transform.GetChildFromName<Transform>("Arrow");
        btnPause = transform.GetChildFromName<Button>("ButtonPause");
        btnPlay = transform.GetChildFromName<Button>("ButtonPlay");
        btnQuit = transform.GetChildFromName<Button>("ButtonQuit");
        btnQuitOk = transform.GetChildFromName<Button>("ButtonYes");
        btnExit = transform.GetChildFromName<Button>("Exit_button");
        btn_OK_TraverseToObj7 = transform.GetChildFromName<Button>("OK_Obj7_Button");
        btQuitCancel = transform.GetChildFromName<Button>("ButtonNo");
        quitPannel = transform.GetChildFromName<Transform>("ExitPanelDialog");
        btSpin = transform.GetChildFromName<Button>("Turn Gacha Button");

        submitAnswerButton = transform.GetChildFromName<Button>("SubmitButton");
        submitAnswerButton.onClick.AddListener(() => OnAnswerSubmit());
        btnPause.onClick.AddListener(() => PauseClicked());
        btnPlay.onClick.AddListener(() => ResumeClicked());
        btnQuit.onClick.AddListener(() => ShowQuitPannel());
        btnQuitOk.onClick.AddListener(() => QuitApplication());
        btnExit.onClick.AddListener(() => QuitApplication());
        btn_OK_TraverseToObj7.onClick.AddListener(() => Hide_TraverseToObj7_Panel());
        btQuitCancel.onClick.AddListener(() => QuitApplicationNo());
        btSpin.onClick.AddListener(() => wheelManager.TurnWheel());
        arrowOutline = false;
        HideExitPanel();
        HideSpinWheel();
        hideMicrophone();
        hideSubmitButton();
        hideExitPannel();
        Hide_TraverseToObj7_Panel();
        
    }
   
    public void showExitpannel()
    {
        //exitPannel.gameObject.SetActive(true);
        GameObject.FindObjectOfType<GameManager>().OnGameOver();
    }

    public void hideExitPannel()
    {
        exitPannel.gameObject.SetActive(false);
    }

    public void showSubmitButton()
    {
        submitAnswerButton.gameObject.SetActive(true);
    }

    public void hideSubmitButton()
    {
        submitAnswerButton.gameObject.SetActive(false);
    }

    public void Show_TraverseToObj7_Panel()
    {
        traverseToObj7_Panel.gameObject.SetActive(true);
    }

    public void Hide_TraverseToObj7_Panel()
    {
        traverseToObj7_Panel.gameObject.SetActive(false);
    }

    public void OnTraverseToObj_7Scene()
    {
        SceneManager.LoadScene("Obj7SpinWheel");

    }

    void nextSpinReady()
    {
        incorrectSubmitCounter = 0;
        hideMicrophone();
        hideSubmitButton();
        //wheelManager.EnableSpinButton();
        manager.EnableSpinButton();
    }
    int incorrectSubmitCounter = 0;
    int totalIncorrect = 0;
    public void OnAnswerSubmit()
    {
        // compare the answer
        if (manager.compareSpokenwithSpinvalue(spinValue))
        {
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            manager.ShowMsginChefPanel("THAT’s Great. Try reading another fraction");
            Invoke("nextSpinReady", 3);
        }
        else
        {
            GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();


            incorrectSubmitCounter++;
            if (incorrectSubmitCounter < 4)
            {
                hideSubmitButton();
                manager.ShowMsginChefPanel("Try Again");

                if (incorrectSubmitCounter == 2)
                {
                    manager.ShowMsginChefPanel("Why don’t you try again?");
                }
                else if (incorrectSubmitCounter == 3)
                {
                    manager.ShowMsginChefPanel("Try Again. You have one more chance to read out the fraction correctly");
                }
            }
            else
            {
                totalIncorrect++;
                if (totalIncorrect < 5)
                {
                    manager.ShowMsginChefPanel("Let’s learn how to read fractions one more time");

                    //Invoke("nextSpinReady", 3);

                    //Stores the last fraction to the traversal (Use this data when returning from Obj7 after the explaination and should always be 0 from start (Obj7 start))
                    PlayerPrefs.SetFloat("LastFraction_Value", FortuneWheelManager.Instance._finalAngle);
                    UtilityArtifacts.backTraversal = true;
                    UtilityArtifacts.comingbackafterTraversal = false;
                    //Call obj 7 to explain (Traverse to Obj7)
                    Invoke("LoadScene_Obj7", 2);
                }
                else
                {
                    showExitpannel();
                }
            }
        }
    }

    void LoadScene_Obj7()
    {
        //Show the traversal panel to obj7
        Show_TraverseToObj7_Panel();
    }

    public void ShowSpinWheel()
    {
        spinWheel.gameObject.SetActive(true);
    }

    public void OnSpinComplete(string value)
    {
        spinValue = value;
        manager.ShowMsginChefPanel("Go ahead and read the fraction");
        showMicrophone();
        //test
        //Invoke("LoadScene_Obj7", 2);

    }

    public void HideSpinWheel()
    {
        spinWheel.gameObject.SetActive(false);
    }


    public void showMicrophone()
    {
        microphone.gameObject.SetActive(true);
    }

    public void hideMicrophone()
    {
        microphone.gameObject.SetActive(false);
    }

    public void HighlightArrow()
    {
        arrowOutline = true;
        StartCoroutine(ArrowOutline(wheelPointer.GetComponent<Outline>(), 0.01f, 1f, 2));
    }

    public void StopArrowHighlight()
    {
        arrowOutline = false;
        StopCoroutine("ArrowOutline");
    }

    public void HighlightSpinButton()
    {
        arrowOutline = true;
        StartCoroutine(ArrowOutline(btSpin.GetComponent<Outline>(), 0.01f, 1f, 2));
    }

    public void StopSpinButtonHighlight()
    {
        arrowOutline = false;
        StopCoroutine("ArrowOutline");
    }

    public IEnumerator ArrowOutline(Outline renderer, float minAlpha, float maxAlpha, float interval)
    {
        Color colorNow = renderer.effectColor;
        Color minColor = new Color(renderer.effectColor.r, renderer.effectColor.g, renderer.effectColor.b, minAlpha);
        Color maxColor = new Color(renderer.effectColor.r, renderer.effectColor.g, renderer.effectColor.b, maxAlpha);

        float currentInterval = 0;
        while (arrowOutline)
        {
            float tColor = currentInterval / interval;
            renderer.effectColor = Color.Lerp(minColor, maxColor, tColor);

            currentInterval += Time.deltaTime;
            if (currentInterval >= interval)
            {
                Color temp = minColor;
                minColor = maxColor;
                maxColor = temp;
                currentInterval = currentInterval - interval;
            }
            //duration -= Time.deltaTime;
            yield return null;
        }

        renderer.effectColor = colorNow;
    }

    void PauseClicked()
    {
        Time.timeScale = 0;
        timeline_New.pauseAudio();
        btnPause.gameObject.SetActive(false);
        btnPlay.gameObject.SetActive(true);
    }

    void ResumeClicked()
    {
        Time.timeScale = 1;
        timeline_New.resumeAudio();
        btnPause.gameObject.SetActive(true);
        btnPlay.gameObject.SetActive(false);
    }
    void HideExitPanel()
    {
        Time.timeScale = 1;
        timeline_New.resumeAudio();
        quitPannel.gameObject.SetActive(false);
    }

    void ShowQuitPannel()
    {
        Time.timeScale = 0;
        timeline_New.pauseAudio();
        quitPannel.gameObject.SetActive(true);
    }

    void QuitApplication()
    {
        Application.Quit(); //Test
        //SceneManager.LoadScene("Obj7SpinWheellVoice");

    }

    void QuitApplicationNo()
    {
        HideExitPanel();
    }

    void InitializeDialoguePanl()
    {
        hintPopup_Pnl = GameObject.Find("Canvas/Dialouge").transform.GetChild(0).gameObject;
        hintMsg_Text = hintPopup_Pnl.transform.GetChild(1).gameObject;
        hintOkayBtn = hintPopup_Pnl.transform.GetChild(4).gameObject.GetComponent<Button>();

        hintOkayBtn.onClick.RemoveAllListeners();
        hintOkayBtn.onClick.AddListener(Hint_ok_functionality);
    }

    public void set_dialougue(string message)
    {
        hintPopup_Pnl.SetActive(true);
        if (hintMsg_Text != null)
        {
            hintMsg_Text.GetComponent<TEXDraw>().text = message;
        }
    }

    public void Hint_ok_functionality()
    {
        FindObjectOfType<timeline_new>().lipsync_player.GetComponent<AudioSource>().Stop();
        hintPopup_Pnl.SetActive(false);

        //if (OnCanvasJobDone != null)
        //    OnCanvasJobDone(UtilityArtifacts.Obj14_ChoosefractionFailed);
    }

    public void HideLoading()
    {
        loading.gameObject.SetActive(false);
    }


    // Update is called once per frame
}
