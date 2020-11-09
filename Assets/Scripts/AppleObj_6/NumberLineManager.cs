using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class NumberLineManager : MonoBehaviour
{
    // Start is called before the first frame update

    public int count;
    public GameObject marker;
    Text markertext;
    bool needSample = false;
    public GameObject Sample;

    public delegate void ShowHint(string msg);
    public static event ShowHint OnShowHint;

    void Start()
    {
        //Initi();
        //createDivision(count);
    }

    public void Initi()
    {
        //marker = transform.Find("Marker").gameObject;
        //markertext = marker.transform.Find("Text").GetComponent<Text>();
        //markertext.text = 0+"" ;
        GameObject markerObj = Instantiate(marker, new Vector3(0, 0, 0), Quaternion.identity);
        markerObj.transform.SetParent(this.transform);
        Text markertextObj;
        markertextObj = markerObj.transform.Find("Text").GetComponent<Text>();
        markertextObj.text = 0 + "";
        markerObj.transform.localScale = new Vector3(1, 1, 1);
        RectTransform rt = markerObj.GetComponent<RectTransform>();
        //Debug.Log("eachBlockLength: " + eachBlockLength + " i: " + i + " = " + (i * eachBlockLength));
        rt.anchoredPosition = new Vector3(0, -5, 0);
        EventTrigger trigger = markerObj.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        MarkerSetup markerS = markerObj.GetComponent<MarkerSetup>();
        //entry.callback.AddListener((eventData) => { MarkerClicked(0,markerS); });
        
        entry.callback.AddListener((eventData) => { MarkerClickedObj5(0, markerS); });
        trigger.triggers.Add(entry);

    }

    void MarkerClicked(int value,MarkerSetup marker)
    {
        Debug.Log("MarkerClicked: " + value);
        if (VisualQTypeManager.instance.checkNumerator(value))
        {
            VisualQTypeManager.instance.canGotoNextQuestion = true;
            marker.OnCorrectAnswer();
        }
        else
        {
            VisualQTypeManager.instance.canGotoNextQuestion = false;
            if (OnShowHint != null)
                OnShowHint("Identify the fraction on the number line ");
        }
    }


    void MarkerClickedObj5(int value, MarkerSetup marker)
    {
        if (SceneManager.GetActiveScene().name.Equals("obj_15_new_story"))
        {
            objective_15_validation(value, marker);
        }
        else
        {
            Debug.Log("MarkerClicked: " + value);
            if (value == 3)
            {
                FindObjectOfType<timeline_new>().lipsync_player.GetComponent<AudioSource>().Stop();
                marker.OnCorrectAnswer();
                GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
                disable();
                Invoke("enableFade", 2.0f);
            }
            else
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                GameObject.FindObjectOfType<conversationManager>().EnableConversation("The fraction to be plotted is \\frac{3}{5}. Identify where \\frac{3}{5} lies on the number line and tap on the point precisely");
                //Invoke("disableHint", 4.0f);
                FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj6_Hint2.wav");
                Invoke("show_question_again", 9);
            }
        }
    }

    void show_question_again()
    {
        GameObject.FindObjectOfType<conversationManager>().DisableConversation();
    }

    void objective_15_validation(int value, MarkerSetup marker)
        {
        Debug.Log("MarkerClicked: " + value);
        if (this.name == "Number_line_1")
        {
            if (value == 1)
            {
                marker.OnCorrectAnswer();
                GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();

                Invoke("next", 2.0f);
                Invoke("disable", 2.0f);
            }
            else
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                GameObject.FindObjectOfType<obj_15_new_story>().set_dialougue("Identify where \\frac{1}{2} is on the number line and tap on the point precisely.");
                FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_15_plot_1b2_mistake.wav");
            }

        }
        else if (this.name == "Number_line_2")
        {
            if (value == 2)
            {
                marker.OnCorrectAnswer();
                GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();

                Invoke("next", 2.0f);
                Invoke("disable", 2.0f);
            }
            else
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                GameObject.FindObjectOfType<obj_15_new_story>().set_dialougue("Identify where \\frac{2}{4} is on the number line and tap on the point precisely.");
                FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_15_plot_2b4_mistake.wav");
            }
        }
        else if ( this.name == "Number_line_3")
        {
            if (value == 4)
            {
                marker.OnCorrectAnswer();
                GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();

                Invoke("next", 2.0f);
                Invoke("disable", 2.0f);
            }
            else
            {
                GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                GameObject.FindObjectOfType<obj_15_new_story>().set_dialougue("Identify where \\frac{4}{8} is on the number line and tap on the point precisely.");
                FindObjectOfType<timeline_new>().playAudioOnRelearn("obj_15_plot_4b8_mistake.wav");
            }
        }
       
    }
    void disableHint()
    {
        GameObject.FindObjectOfType<conversationManager>().DisableHint();
    }
    void enableFade()
    {

        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
        Invoke("nextObjective1", 3.0f);
    }
    void nextObjective1()
    {
        GameObject.FindObjectOfType<conversationManager>().DisableConversation();
        GameObject.FindObjectOfType<Obj6Manager>().enableROQuest1();
        GameObject.FindObjectOfType<Obj6Manager>().headings.text = "";
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadeout");
        Invoke("next", 3.0f);
       

    }
    void next()
    {
        GameObject.FindObjectOfType<timeline_new>().load_next();
    }

    private void disable()
    {
        this.enabled = false;
        var d = this.GetComponentsInChildren<MarkerSetup>();

        foreach (MarkerSetup a in d)
        {
            a.GetComponent<EventTrigger>().enabled = false;
            a.enabled = false;
        }
    }
    // Update is called once per frame

    public  void createDivision(int block)
    {
        Initi();
        count = block;
        float eachBlockLength = 1000 / block;
        for(int i = 1; i <= block; i++)
        {

            GameObject markerObj = Instantiate(marker, new Vector3(0, 0, 0), Quaternion.identity);
            markerObj.transform.SetParent(this.transform);
            Text markertextObj;
            markertextObj = markerObj.transform.Find("Text").GetComponent<Text>();
           
            if (i != block)
                markertextObj.text = i + "/" + count;
            else
                markertextObj.text = 1+"" ;
            markerObj.transform.localScale = new Vector3(1, 1, 1);
            RectTransform rt = markerObj.GetComponent<RectTransform>();
            //Debug.Log("eachBlockLength: " + eachBlockLength + " i: " + i + " = " + (i * eachBlockLength));
            rt.anchoredPosition = new Vector3(i * eachBlockLength, -5, 0);
            if (needSample)
            {
                GameObject SampleObj = Instantiate(Sample, new Vector3(0, 0, 0), Quaternion.identity);
                SampleObj.transform.SetParent(this.transform);
                SampleObj.transform.localScale = new Vector3(1, 1, 1);
                RectTransform rtObj = SampleObj.GetComponent<RectTransform>();
                //Debug.Log("eachBlockLength: " + eachBlockLength + " i: " + i + " = " + (i * eachBlockLength));
                rtObj.anchoredPosition = new Vector3(((i * eachBlockLength) - eachBlockLength / 2), 150, 0);
                rtObj.sizeDelta = new Vector2(eachBlockLength, eachBlockLength);
            }

            EventTrigger trigger = markerObj.GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            Debug.Log("i: " + i);
            int j = i;
            MarkerSetup markerS = markerObj.GetComponent<MarkerSetup>();
            //entry.callback.AddListener((eventData) => { MarkerClicked(j, markerS); });
            entry.callback.AddListener((eventData) => { MarkerClickedObj5(j, markerS); });
            
            trigger.triggers.Add(entry);
        }
    }

    public void setText()
    {
        foreach(GameObject numberlineText in GameObject.FindGameObjectsWithTag("marker"))
        {
            numberlineText.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    public void resetLine()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        Initi();
    }

   
}
