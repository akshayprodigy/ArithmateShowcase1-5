using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ApplePickManager : MonoBehaviour
{
    string jsonFileName = "introduction_and_apple_pickicg_game_json.json";
    public string obj_after_invoke;
    GameObject LoadingAudio;
    private void OnEnable()
    {
        timeline_new.OnEventCalled += EventToHandle;
    }

    private void OnDisable()
    {
        timeline_new.OnEventCalled -= EventToHandle;
    }
    void Start()
    {
        Initialised();

    }
    //GameObject.FindObjectOfType<timeline_new>().load_next();
    // Update is called once per frame
    void Initialised()
    {
        
        Invoke("audio_invoke", 3.0f);
        GameObject.Find("Chef conversation").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("conversationManager").GetComponent<conversationManager>().DisableDialouge();
        LoadingAudio = GameObject.Find("LoadAudio");

    }

    void HideLoadingAudio()
    {
        LoadingAudio.SetActive(false);
    }

    void audio_invoke()
    {
        Debug.Log(EVENT.SetUpTimeLine);
        EventManager.setNameOfJasonFile(jsonFileName);
        EventManager.Broadcast(EVENT.SetUpTimeLine);
    }
    void startConversation()
    {
        GameObject.Find("Guide").GetComponent<IsometricPlayerMovementControllerFair>().destination = "Park_Gate";
        GameObject.Find("Guide").GetComponent<IsometricPlayerMovementControllerFair>().enabled = true;
        GameObject.Find("WitchRender").GetComponent<Animator>().enabled = true;
        functionToNextandReplayButtons();
        //StartCoroutine(sentanceChange());
    }
    public void setDirectionToPrakinfair()
    {
        GameObject.Find("Guide").GetComponent<IsometricPlayerMovementControllerFair>().destination = "Park_Gate_1";
        GameObject.Find("Guide").GetComponent<IsometricPlayerMovementControllerFair>().enabled = true;
        GameObject.Find("WitchRender").GetComponent<Animator>().enabled = true;
    }
    public void setDirection2ToPrakinfair()
    {
        GameObject.Find("Guide").GetComponent<IsometricPlayerMovementControllerFair>().destination = "Park_Gate_2";
        GameObject.Find("Guide").GetComponent<IsometricPlayerMovementControllerFair>().reached = true;
    }
    public IEnumerator sentanceChange()
    {
        yield return new WaitForSeconds(1.4f);
        obj_after_invoke = "nextObjective1";
        enableFade1();
        
        
    }
    void enableFade1()
    {
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
        Invoke(obj_after_invoke, 3.0f);
    }
    void nextObjective1()
    {
        GameObject.Find("Game").transform.GetChild(1).gameObject.SetActive(true);
        GameObject.Find("Game").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.FindObjectOfType<GameManager>().Initialization();
        Invoke("nextObjectiveVo1", 3.0f);
        GameObject.FindObjectOfType<conversationManager>().EnableConversation("");
        GameObject.FindObjectOfType<conversationManager>().DisableConversation();
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadeout");

    }
    void switchToWalk()
    {
        GameObject.Find("Introduction&Game").transform.GetChild(1).gameObject.SetActive(true);
        GameObject.Find("Introduction&Game").transform.GetChild(0).gameObject.SetActive(false);
        
        Invoke("nextObjectiveVo1", 3.0f);
        GameObject.FindObjectOfType<conversationManager>().EnableConversation("");
        GameObject.FindObjectOfType<conversationManager>().DisableConversation();
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadeout");

    }
    void stopPlayer()
    {
        GameObject.Find("walking_player").GetComponent<Animator>().SetTrigger("idle");
    }
    void nextObjectiveVo1()
    {

        GameObject.FindObjectOfType<timeline_new>().load_next();

    }


    void EventToHandle(string EventName)
    {
        HideLoadingAudio();
        switch (EventName)
        {
            case "Fair_Its_Early":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Here we are at the Carnival.");
                startConversation();
                break;
            case "Fair_Mr_x":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Mr. Chef has a stall here. Let’s go to the stall and help him.");
                //obj_after_invoke = "switchToWalk";
               
                break;
            case "Fair_oh_here":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Looks like he is calling us. Let's hear what he has to say.");
                //Invoke("enableFade1",3.0f);
                break;
            case "Fair_Mr_asked":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Mr. Chef has asked us to get few apples to make apple juice");
                
                setDirectionToPrakinfair();
                break;
            case "Fair_So_we_have":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Let’s head to the apple orchard and collect apples and then go back to the stall.");
                setDirection2ToPrakinfair();
                StartCoroutine(sentanceChange());
                break;
            case "Fair_lets_get":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Let’s head to the apple orchard and collect apples and then go back to the stall.");
               
                
                break;
            case "Game_As_you":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("As you can see, there are a lot of apples hanging from the trees.");
                break;
            case "Game_you_have":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("You have to shake the trees so that the apples fall down. Tap on the tree to shake them. Once they are on the ground, pick up the apples by tapping on them.");
                break;
            case "Game_the_apple":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Some apples might break after falling down, make sure you pick all of these as well");
                break;
            case "Game_we_are":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("We have to get back to the stall in thirty seconds");
                break;
            case "Game_so_go":
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("Let’s get started and collect as many apples as possible.");
                GameObject.Find("GameManager").GetComponent<GameManager>().StopGameInstruction();
                Invoke("tut1", 2.2f);
                break;

               
               
        }
    }

    void functionToNextandReplayButtons()
    {
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Button>().onClick.AddListener(replay);
        GameObject.Find("Dialouge").transform.GetChild(0).GetChild(0).GetChild(2).GetComponent<Button>().onClick.AddListener(next);
    }
    void next()
    {
        //GameObject.Find("Canvas").transform.GetChild(3).gameObject.SetActive(true);
        //GameObject.Find("Canvas").transform.GetChild(3).GetComponent<Animator>().enabled = false;
        //GameObject.Find("Canvas").transform.GetChild(3).GetComponent<Animator>().enabled = true;
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
        GameObject.FindObjectOfType<conversationManager>().DisableDialouge();
        GameObject.Find("Canvas").transform.GetChild(2).gameObject.SetActive(false);
        GameObject.Find("Canvas").transform.GetChild(2).GetComponent<TimeManager>().timeStart = 30;
        
        Invoke("animStop", 2.5f);


        //AppleManager.setGameData();


        GameObject.Find("GameManager").GetComponent<SceneLoader>().LoadScene("Obj1AppleSorting");
    }
   public void replay()
    {
        Debug.Log("replay");
        removeGeneratedApple();
        enableApplesOnTree();
       
        AppleManager.resetData();
        GameObject.FindObjectOfType<conversationManager>().DisableDialouge();
        GameObject.Find("Canvas").transform.GetChild(2).gameObject.SetActive(true);
        GameObject.FindObjectOfType<TimeManager>().Initialization();
        GameObject.FindObjectOfType<conversationManager>().EnableQuestion1("Shake the trees to make the apples fall. Tap on the apples on the ground to collect them.");
        GameObject.Find("Guide").GetComponent<IsometricPlayerMovementControllerGarden>().ClickedObject = null;
        GameObject.Find("WitchRender").GetComponent<Animator>().enabled = true;
        GameObject.Find("GameManager").GetComponent<GameManager>().IsGameOn = true;

        foreach (GameObject g in GameObject.FindGameObjectsWithTag("tree"))
        {
            //g.GetComponent<Animator>().enabled = false;
            g.GetComponent<PolygonCollider2D>().enabled = true;
        }
           
    }
    void removeGeneratedApple()
    {
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("Fallen Apple"))
        {
            Destroy(g);
        }
    }
    void enableApplesOnTree()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("tree"))
        {
            g.transform.GetChild(0).gameObject.SetActive(true);
            g.transform.GetChild(1).gameObject.SetActive(true);
            g.transform.GetChild(2).gameObject.SetActive(true);
            g.transform.GetChild(3).gameObject.SetActive(true);
            g.transform.GetChild(4).gameObject.SetActive(true);
        }
    }
    void animStop()
    {
        GameObject.Find("Fade").GetComponent<Animator>().enabled = false;
    }

    void tut1()
    {
        //GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj1_tut1.wav");
        
        GameObject.Find("BasketToCollect").transform.GetChild(0).gameObject.SetActive(true);
    }
}
