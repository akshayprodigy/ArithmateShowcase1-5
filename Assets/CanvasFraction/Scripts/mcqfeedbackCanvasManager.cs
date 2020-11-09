using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mcqfeedbackCanvasManager : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject segmentTitle, segmentQuestioms,title;
    Text segmentTitleText, segmentQuesTitleText, segmentQuestionText;
    InputField commentInputText, rateInputTex;
    Toggle a, b, c, d, e;
    Button nextButton, msgOk;
    Transform messageBox;
    ToggleGroup toggleGroup;
    int nextSequence = 0;
    public float Fade_Time = 2f;
    public float Fade_Max = 1f;
    float _time;
    public bool FadeIn_ing = false;
    public bool FadeOut_ing = false;
    Image Black_screen;
    Results results;

    public delegate void LogMessage(string cases);
    public static event LogMessage onLogMessage;

    private void Start()
    {
        Initilize();
    }

    void Initilize()
    {
        title = GameObject.Find("Title");
        segmentTitle = GameObject.Find("ShowSegement");
        segmentQuestioms = GameObject.Find("ShowSegementQuestion");
        segmentTitleText = transform.GetChildFromName<Text>("SegmentText");
        segmentQuesTitleText = transform.GetChildFromName<Text>("SegmentQuestionText");
        segmentQuestionText = transform.GetChildFromName<Text>("QuestionText");
        commentInputText = transform.GetChildFromName<InputField>("Comment");
        rateInputTex = transform.GetChildFromName<InputField>("Rate");
        toggleGroup = transform.GetChildFromName<ToggleGroup>("ToggelGroup");
        messageBox = transform.GetChildFromName<Transform>("Dialog");
        msgOk = transform.GetChildFromName<Button>("Button");
        a = transform.GetChildFromName<Toggle>("a");
        b = transform.GetChildFromName<Toggle>("b");
        c = transform.GetChildFromName<Toggle>("c");
        d = transform.GetChildFromName<Toggle>("d");
        e = transform.GetChildFromName<Toggle>("e");

        nextButton = transform.GetChildFromName<Button>("Next");
        nextButton.onClick.AddListener(delegate { onSaveAndNext(); });
        Black_screen = transform.GetChildFromName<Image>("BackBg");
        Black_screen.gameObject.SetActive(false);
        msgOk.onClick.AddListener(delegate { CloseDialog(); });
        CloseDialog();
        HideSegmentQuestioms();
        HideSegment();
        Invoke("FadeIn", 3);

        // restul

        results = new Results();
    }
    void showDialogMsg()
    {
        messageBox.gameObject.SetActive(true);
    }

    void CloseDialog()
    {
        rateInputTex.text = "";
        messageBox.gameObject.SetActive(false);
        
    }
    void HideSegmentQuestioms()
    {
        segmentQuestioms.SetActive(false);
    }

    void ShowSegmentQuestioms()
    {
        segmentQuestioms.SetActive(true);
    }

    void HideSegment()
    {
        segmentTitle.SetActive(false);
    }

    void ShowSegment(string msg)
    {
        segmentTitle.SetActive(true);
        segmentTitleText.text = msg;
    }

    void HideTitle()
    {
        title.SetActive(false);
    }

    void ShowTitle()
    {
        title.SetActive(true);
    }

    void Hideoptions()
    {
        a.gameObject.SetActive(false);
        b.gameObject.SetActive(false);
        c.gameObject.SetActive(false);
        d.gameObject.SetActive(false);
        e.gameObject.SetActive(false);
    }
    void UnHideoptions()
    {
        a.gameObject.SetActive(true);
        b.gameObject.SetActive(true);
        c.gameObject.SetActive(true);
        d.gameObject.SetActive(true);
        e.gameObject.SetActive(true);
    }
    void hideRatingBox()
    {
        rateInputTex.gameObject.SetActive(false);
    }
    void unHideRatingBox()
    {
        rateInputTex.gameObject.SetActive(true);
    }
    void onSaveAndNext()
    {
        //string usrType = PlayerPrefs.GetString(UtilityArtifacts.UserType);
        //if (usrType == "Other" || usrType == "Parent")
        //{
        //    if (int.Parse(segmentQuestionText.text) < 11 && int.Parse(segmentQuestionText.text) > 0)
        //    {
                
        //        SavaQuestionanies();
        //        FadeIn();
        //    }
        //    else
        //    {
        //        showDialogMsg();
        //    }
        //}
        //else
        //{
            SavaQuestionanies();
            FadeIn();
        //}
    }

    void SavaQuestionanies()
    {
        QuestionAnswer questionAnswer = new QuestionAnswer();
        questionAnswer.Question = segmentQuestionText.text;
        questionAnswer.setType = segmentQuesTitleText.text;
        questionAnswer.comment = commentInputText.text;
        if (UtilityArtifacts.UserType == "Other" || UtilityArtifacts.UserType == "Teacher")
        {
            
                questionAnswer.rate = rateInputTex.text;
           
        }
        else
        {
            

                if (a.isOn)
                    questionAnswer.option = a.GetComponentInChildren<Text>().text;
                else if (b.isOn)
                    questionAnswer.option = b.GetComponentInChildren<Text>().text;
                else if (c.isOn)
                    questionAnswer.option = c.GetComponentInChildren<Text>().text;
                else if (d.isOn)
                    questionAnswer.option = d.GetComponentInChildren<Text>().text;
                else if (e.isOn)
                    questionAnswer.option = e.GetComponentInChildren<Text>().text;
            
            
        }
       

        results.questionSets.Add(questionAnswer);
    }

    void onNextButtonClick()
    {
       
            nextSequence++;
            string usrType = PlayerPrefs.GetString(UtilityArtifacts.UserType);
            Debug.Log("nextSequence " + nextSequence);
            Debug.Log("user type " + UtilityArtifacts.UserType);

            if (usrType == "Other" || usrType == "Parent")
            {
           
                switch (nextSequence)
                {
                    case 1:
                        HideTitle();
                        ShowSegment("Concrete Experience ");
                        Invoke("FadeIn", 8);
                        break;
                    case 2:
                        HideSegment();
                        ShowSegmentQuestioms();
                        segmentQuesTitleText.text = "Concrete Experience";
                        segmentQuestionText.text = "1.	On a scale of 1-10, how would you rate your overall experience of using the app? ";
                        Hideoptions();
                        unHideRatingBox();
                        commentInputText.text = "";
                        rateInputTex.text = "";

                        break;
                    case 3:
                        segmentQuesTitleText.text = "Concrete Experience";
                        segmentQuestionText.text = "2.  On a scale of 1-10, do you think this app will help students understand maths concepts better?";
                        Hideoptions();
                        unHideRatingBox();
                        rateInputTex.text = "";
                        commentInputText.text = "";
                        toggleGroup.SetAllTogglesOff();
                        break;
                    case 4:
                        segmentQuesTitleText.text = "Concrete Experience";
                        segmentQuestionText.text = "3.	On a scale of 1-10, were you able to understand the concept taught?";
                        Hideoptions();
                        unHideRatingBox();
                        rateInputTex.text = "";
                        commentInputText.text = "";
                        toggleGroup.SetAllTogglesOff();
                        break;
                    case 5:

                        segmentQuesTitleText.text = "Concrete Experience";
                        segmentQuestionText.text = "4.	Any additional inputs";
                        a.gameObject.SetActive(false);
                        b.gameObject.SetActive(false);
                        c.gameObject.SetActive(false);
                        d.gameObject.SetActive(false);
                        e.gameObject.SetActive(false);
                        hideRatingBox();
                        commentInputText.text = "";
                        toggleGroup.SetAllTogglesOff();
                        break;

                    case 6:
                        HideSegmentQuestioms();
                        ShowSegment("Canvas");
                        Invoke("FadeIn", 8);
                        break;
                    case 7:
                        HideSegment();
                        ShowSegmentQuestioms();
                        segmentQuesTitleText.text = "Canvas";
                        segmentQuestionText.text = "1.	On a scale of 1-10, how would you rate the use and feel of the Problem Solving Canvas? ";
                        Hideoptions();
                        unHideRatingBox();
                        rateInputTex.text = "";
                        commentInputText.text = "";
                        toggleGroup.SetAllTogglesOff();
                        break;
                    case 8:
                        HideSegment();
                        ShowSegmentQuestioms();
                        UnHideoptions();
                        segmentQuesTitleText.text = "Canvas";
                        segmentQuestionText.text = "2.	Did the Canvas Tutorial make sense? ";
                        a.GetComponentInChildren<Text>().text = "Yes";
                        b.GetComponentInChildren<Text>().text = "No";
                        rateInputTex.gameObject.SetActive(false);
                        c.gameObject.SetActive(false);
                        d.gameObject.SetActive(false);
                        e.gameObject.SetActive(false);
                        hideRatingBox();
                        commentInputText.text = "";
                        toggleGroup.SetAllTogglesOff();
                        break;


                    case 9:
                        HideSegment();
                        ShowSegmentQuestioms();
                        hideRatingBox();
                        Hideoptions();
                        segmentQuesTitleText.text = "Canvas";
                        segmentQuestionText.text = "3.	Any additional inputs";
                        a.gameObject.SetActive(false);
                        b.gameObject.SetActive(false);
                        c.gameObject.SetActive(false);
                        d.gameObject.SetActive(false);
                        e.gameObject.SetActive(false);
                        commentInputText.text = "";
                        toggleGroup.SetAllTogglesOff();
                        break;
                    case 10:
                        HideSegmentQuestioms();
                        ShowSegment("Active Experimentation");
                        Invoke("FadeIn", 8);
                        break;
                    case 11:
                        HideSegment();
                        ShowSegmentQuestioms();
                        hideRatingBox();
                        UnHideoptions();
                        segmentQuesTitleText.text = "Active Experimentation";
                        segmentQuestionText.text = "1.	How were the 4 activities?";
                        a.gameObject.SetActive(true);
                        b.gameObject.SetActive(true);
                        c.gameObject.SetActive(true);
                        d.gameObject.SetActive(true);
                        e.gameObject.SetActive(true);
                        a.GetComponentInChildren<Text>().text = "Excellent";
                        b.GetComponentInChildren<Text>().text = "Good";
                        c.GetComponentInChildren<Text>().text = "Average";
                        d.GetComponentInChildren<Text>().text = "Bad ";
                        e.GetComponentInChildren<Text>().text = "Poor";
                        e.gameObject.SetActive(false);
                        commentInputText.text = "";
                        toggleGroup.SetAllTogglesOff();
                        break;
                    case 12:
                        segmentQuesTitleText.text = "Active Experimentation";
                        segmentQuestionText.text = "2.	Were you able to perform the activities with ease?";
                        a.GetComponentInChildren<Text>().text = "Yes";
                        b.GetComponentInChildren<Text>().text = "No";
                        c.gameObject.SetActive(false);
                        d.gameObject.SetActive(false);
                        e.gameObject.SetActive(false);
                        commentInputText.text = "";
                        toggleGroup.SetAllTogglesOff();
                        break;
                    case 13:
                        segmentQuesTitleText.text = "Active Experimentation";
                        segmentQuestionText.text = "3.	Did the Active Experimentation module correlate well with the Learning Experience?";
                        a.GetComponentInChildren<Text>().text = "Yes";
                        b.GetComponentInChildren<Text>().text = "No";
                        c.gameObject.SetActive(false);
                        d.gameObject.SetActive(false);
                        e.gameObject.SetActive(false);
                        commentInputText.text = "";
                        toggleGroup.SetAllTogglesOff();
                        break;
                    case 14:
                        HideSegment();
                        ShowSegmentQuestioms();
                        segmentQuesTitleText.text = "Active Experimentation";
                        segmentQuestionText.text = "4.	Any additional inputs";
                        a.gameObject.SetActive(false);
                        b.gameObject.SetActive(false);
                        c.gameObject.SetActive(false);
                        d.gameObject.SetActive(false);
                        e.gameObject.SetActive(false);
                        commentInputText.text = "";
                        toggleGroup.SetAllTogglesOff();
                        break;
                    case 15:
                        HideSegmentQuestioms();
                        Feedback feed = new Feedback();
                        feed.Name = PlayerPrefs.GetString(UtilityArtifacts.UserName, "");
                        feed.PhoneNumber = PlayerPrefs.GetString(UtilityArtifacts.UserPhoneNumber, "");
                        feed.grade = PlayerPrefs.GetString(UtilityArtifacts.UserGrade, "");
                        feed.userType = PlayerPrefs.GetString(UtilityArtifacts.UserType, "");
                        ShowSegment("Thank you");
                        Invoke("LoadThankYou", 3);
                        feed.results = results;
                        string serMsg = JsonUtility.ToJson(feed);
                        Debug.Log("serMsg " + serMsg);
                        if (onLogMessage != null)
                            onLogMessage(serMsg);


                        break;
                }
            
            }
            else
            {
                hideRatingBox();
                switch (nextSequence)
                {
                    case 1:
                        HideTitle();
                        ShowSegment("Concrete Experience ");
                        Invoke("FadeIn", 8);
                        break;
                    case 2:
                        HideSegment();
                        ShowSegmentQuestioms();
                        segmentQuesTitleText.text = "Concrete Experience";
                        segmentQuestionText.text = "1.	How was the experience of paper folding? ";
                        a.GetComponentInChildren<Text>().text = "Excellent";
                        b.GetComponentInChildren<Text>().text = "Good";
                        c.GetComponentInChildren<Text>().text = "Average";
                        d.GetComponentInChildren<Text>().text = "Bad ";
                        e.GetComponentInChildren<Text>().text = "Poor";
                        e.gameObject.SetActive(false);
                        commentInputText.text = "";
                        toggleGroup.SetAllTogglesOff();
                        break;
                    case 3:
                        segmentQuesTitleText.text = "Concrete Experience";
                        segmentQuestionText.text = "2.	Could you understand the process of Forming Equivalent Fractions by Multiplication?";
                        a.GetComponentInChildren<Text>().text = "Yes";
                        b.GetComponentInChildren<Text>().text = "No";
                        c.gameObject.SetActive(false);
                        d.gameObject.SetActive(false);
                        e.gameObject.SetActive(false);
                        commentInputText.text = "";
                        toggleGroup.SetAllTogglesOff();
                        break;
                    case 4:
                        segmentQuesTitleText.text = "Concrete Experience";
                        segmentQuestionText.text = "3.	Do you think the multiple choice questions at the end of each session covered what was taught?";
                        a.GetComponentInChildren<Text>().text = "Yes";
                        b.GetComponentInChildren<Text>().text = "No";
                        c.gameObject.SetActive(false);
                        d.gameObject.SetActive(false);
                        e.gameObject.SetActive(false);
                        commentInputText.text = "";
                        toggleGroup.SetAllTogglesOff();
                        break;
                    case 5:
                        segmentQuesTitleText.text = "Concrete Experience";
                        segmentQuestionText.text = "4.	Were the questions sufficient?";
                        a.GetComponentInChildren<Text>().text = "Yes";
                        b.GetComponentInChildren<Text>().text = "No";
                        c.gameObject.SetActive(false);
                        d.gameObject.SetActive(false);
                        e.gameObject.SetActive(false);
                        commentInputText.text = "";
                        toggleGroup.SetAllTogglesOff();
                        break;
                case 6:
                        

                    HideSegment();
                    ShowSegmentQuestioms();
                    segmentQuesTitleText.text = "Concrete Experience";
                    segmentQuestionText.text = "5.	How well were you able to understand the concept taught ?";// explanation ?
                    c.gameObject.SetActive(true);
                    d.gameObject.SetActive(true);
                    e.gameObject.SetActive(true);
                    a.GetComponentInChildren<Text>().text = "Excellent";
                    b.GetComponentInChildren<Text>().text = "Good";
                    c.GetComponentInChildren<Text>().text = "Average";
                    d.GetComponentInChildren<Text>().text = "Bad ";
                    e.GetComponentInChildren<Text>().text = "Poor";
                    e.gameObject.SetActive(false);
                    commentInputText.text = "";
                    toggleGroup.SetAllTogglesOff();
                    break;

                case 7:
                    HideSegment();
                    ShowSegmentQuestioms();
                    segmentQuesTitleText.text = "Concrete Experience";
                    segmentQuestionText.text = "6.	How did you find the voice over? ";// explanation ? Were you able to understand the concept well?
                    c.gameObject.SetActive(true);
                    d.gameObject.SetActive(true);
                    e.gameObject.SetActive(true);
                    a.GetComponentInChildren<Text>().text = "Excellent";
                    b.GetComponentInChildren<Text>().text = "Good";
                    c.GetComponentInChildren<Text>().text = "Average";
                    d.GetComponentInChildren<Text>().text = "Bad ";
                    e.GetComponentInChildren<Text>().text = "Poor";
                    e.gameObject.SetActive(false);
                    commentInputText.text = "";
                    toggleGroup.SetAllTogglesOff();
                    break;
                case 8:
                    segmentQuesTitleText.text = "Concrete Experience";
                    segmentQuestionText.text = "7.	Were you able to navigate through the app easily?";
                    a.GetComponentInChildren<Text>().text = "Yes";
                    b.GetComponentInChildren<Text>().text = "No";
                    c.gameObject.SetActive(false);
                    d.gameObject.SetActive(false);
                    e.gameObject.SetActive(false);
                    commentInputText.text = "";
                    toggleGroup.SetAllTogglesOff();
                    break;
                case 9:
                    segmentQuesTitleText.text = "Concrete Experience";
                    segmentQuestionText.text = "8.	Did you like the elements (shapes, images etc.) used in the app?";
                    a.GetComponentInChildren<Text>().text = "Yes";
                    b.GetComponentInChildren<Text>().text = "No";
                    c.gameObject.SetActive(false);
                    d.gameObject.SetActive(false);
                    e.gameObject.SetActive(false);
                    commentInputText.text = "";
                    toggleGroup.SetAllTogglesOff();
                    break;
                case 10:
                    segmentQuesTitleText.text = "Concrete Experience";
                    segmentQuestionText.text = "9.	Did you like the colours used in the app?";
                    a.GetComponentInChildren<Text>().text = "Yes";
                    b.GetComponentInChildren<Text>().text = "No";
                    c.gameObject.SetActive(false);
                    d.gameObject.SetActive(false);
                    e.gameObject.SetActive(false);
                    commentInputText.text = "";
                    toggleGroup.SetAllTogglesOff();
                    break;
                case 11:
                    segmentQuesTitleText.text = "Concrete Experience";
                    segmentQuestionText.text = "10.	Did you enjoy using the app?";
                    a.GetComponentInChildren<Text>().text = "Yes";
                    b.GetComponentInChildren<Text>().text = "No";
                    c.gameObject.SetActive(false);
                    d.gameObject.SetActive(false);
                    e.gameObject.SetActive(false);
                    commentInputText.text = "";
                    toggleGroup.SetAllTogglesOff();
                    break;
                case 12:
                        HideSegment();
                        ShowSegmentQuestioms();
                        segmentQuesTitleText.text = "Concrete Experience";
                        segmentQuestionText.text = "11.	Any additional inputs";
                        a.gameObject.SetActive(false);
                        b.gameObject.SetActive(false);
                        c.gameObject.SetActive(false);
                        d.gameObject.SetActive(false);
                        e.gameObject.SetActive(false);
                        commentInputText.text = "";
                        toggleGroup.SetAllTogglesOff();
                        break;
                    case 13:
                        HideSegmentQuestioms();
                        ShowSegment("Canvas");
                        Invoke("FadeIn", 8);
                        break;
                    case 14:
                        HideSegment();
                        ShowSegmentQuestioms();
                        segmentQuesTitleText.text = "Canvas";
                        segmentQuestionText.text = "1.	How was your experience of using the Problem Solving Canvas? ";
                        a.gameObject.SetActive(true);
                        b.gameObject.SetActive(true);
                        c.gameObject.SetActive(true);
                        d.gameObject.SetActive(true);
                        e.gameObject.SetActive(true);
                        a.GetComponentInChildren<Text>().text = "Excellent";
                        b.GetComponentInChildren<Text>().text = "Good";
                        c.GetComponentInChildren<Text>().text = "Average";
                        d.GetComponentInChildren<Text>().text = "Bad ";
                        e.GetComponentInChildren<Text>().text = "Poor";
                        e.gameObject.SetActive(false);
                        commentInputText.text = "";
                        toggleGroup.SetAllTogglesOff();
                        break;
                    case 15:
                        segmentQuesTitleText.text = "Canvas";
                        segmentQuestionText.text = "2.	Did the Canvas Tutorial help?";
                        a.GetComponentInChildren<Text>().text = "Yes";
                        b.GetComponentInChildren<Text>().text = "No";
                        c.gameObject.SetActive(false);
                        d.gameObject.SetActive(false);
                        e.gameObject.SetActive(false);
                        commentInputText.text = "";
                        toggleGroup.SetAllTogglesOff();
                        break;
                    case 16:
                        segmentQuesTitleText.text = "Canvas";
                        segmentQuestionText.text = "3.	Could you attempt any questions?";
                        a.GetComponentInChildren<Text>().text = "Yes";
                        b.GetComponentInChildren<Text>().text = "No";
                        c.gameObject.SetActive(false);
                        d.gameObject.SetActive(false);
                        e.gameObject.SetActive(false);
                        commentInputText.text = "";
                        toggleGroup.SetAllTogglesOff();
                        break;
                    case 17:
                        segmentQuesTitleText.text = "Canvas";
                        segmentQuestionText.text = "4.	Any difficulty in solvingthe problems?";
                        a.GetComponentInChildren<Text>().text = "Yes";
                        b.GetComponentInChildren<Text>().text = "No";
                        c.gameObject.SetActive(false);
                        d.gameObject.SetActive(false);
                        e.gameObject.SetActive(false);
                        commentInputText.text = "";
                        toggleGroup.SetAllTogglesOff();
                        break;
                    case 18:
                        HideSegment();
                        ShowSegmentQuestioms();
                        segmentQuesTitleText.text = "Canvas";
                        segmentQuestionText.text = "5.	Any additional inputs";
                        a.gameObject.SetActive(false);
                        b.gameObject.SetActive(false);
                        c.gameObject.SetActive(false);
                        d.gameObject.SetActive(false);
                        e.gameObject.SetActive(false);
                        commentInputText.text = "";
                        toggleGroup.SetAllTogglesOff();
                        break;
                    case 19:
                        HideSegmentQuestioms();
                        ShowSegment("Active Experimentation");
                        Invoke("FadeIn", 8);
                        break;
                    case 20:
                        HideSegment();
                        ShowSegmentQuestioms();
                        segmentQuesTitleText.text = "Active Experimentation";
                        segmentQuestionText.text = "1.	How were the 4 activities?";
                        a.gameObject.SetActive(true);
                        b.gameObject.SetActive(true);
                        c.gameObject.SetActive(true);
                        d.gameObject.SetActive(true);
                        e.gameObject.SetActive(true);
                        a.GetComponentInChildren<Text>().text = "Excellent";
                        b.GetComponentInChildren<Text>().text = "Good";
                        c.GetComponentInChildren<Text>().text = "Average";
                        d.GetComponentInChildren<Text>().text = "Bad ";
                        e.GetComponentInChildren<Text>().text = "Poor";
                        e.gameObject.SetActive(false);
                        commentInputText.text = "";
                        toggleGroup.SetAllTogglesOff();
                        break;
                    case 21:
                        segmentQuesTitleText.text = "Active Experimentation";
                        segmentQuestionText.text = "2.	Were you able to perform the activities with ease?";
                        a.GetComponentInChildren<Text>().text = "Yes";
                        b.GetComponentInChildren<Text>().text = "No";
                        c.gameObject.SetActive(false);
                        d.gameObject.SetActive(false);
                        e.gameObject.SetActive(false);
                        commentInputText.text = "";
                        toggleGroup.SetAllTogglesOff();
                        break;
                    case 22:
                        segmentQuesTitleText.text = "Active Experimentation";
                        segmentQuestionText.text = "3.	Did the Active Experimentation module correlate well with the Learning Experience?";
                        a.GetComponentInChildren<Text>().text = "Yes";
                        b.GetComponentInChildren<Text>().text = "No";
                        c.gameObject.SetActive(false);
                        d.gameObject.SetActive(false);
                        e.gameObject.SetActive(false);
                        commentInputText.text = "";
                        toggleGroup.SetAllTogglesOff();
                        break;
                    case 23:
                        HideSegment();
                        ShowSegmentQuestioms();
                        segmentQuesTitleText.text = "Active Experimentation";
                        segmentQuestionText.text = "4.	Any additional inputs";
                        a.gameObject.SetActive(false);
                        b.gameObject.SetActive(false);
                        c.gameObject.SetActive(false);
                        d.gameObject.SetActive(false);
                        e.gameObject.SetActive(false);
                        commentInputText.text = "";
                        toggleGroup.SetAllTogglesOff();
                        break;
                    case 24:
                        HideSegmentQuestioms();
                        Feedback feed = new Feedback();
                        feed.Name = PlayerPrefs.GetString(UtilityArtifacts.UserName, "");
                        feed.PhoneNumber = PlayerPrefs.GetString(UtilityArtifacts.UserPhoneNumber, "");
                        feed.grade = PlayerPrefs.GetString(UtilityArtifacts.UserGrade, "");
                        feed.userType = PlayerPrefs.GetString(UtilityArtifacts.UserType, "");
                        feed.results = results;
                        string serMsg = JsonUtility.ToJson(feed);
                        Debug.Log("serMsg " + serMsg);
                        if (onLogMessage != null)
                            onLogMessage(serMsg);
                        ShowSegment("Thank you");
                        Invoke("LoadThankYou", 3);

                        break;
                }
            }
        
    }


    void LoadThankYou()
    {
        SceneManager.LoadScene(10);
    }

    // Update is called once per frame
    void Update()
    {
        if (FadeIn_ing)
        {
            _time += Time.deltaTime;
            Black_screen.color = Color.Lerp(new Color(0, 0, 0, 0), new Color(0, 0, 0, Fade_Max), _time / Fade_Time);
        }

        if (FadeOut_ing)
        {
            _time += Time.deltaTime;         
            Black_screen.color = Color.Lerp(new Color(0, 0, 0, Fade_Max), new Color(0, 0, 0, 0), _time / Fade_Time);
        }

        if (_time >= Fade_Time)
        {
            _time = 0;
            if (FadeIn_ing)
            {
                FadeIn_ing = false;
                FadeOut_ing = false;
                onNextButtonClick();
                FadeOut();

            }else if (FadeOut_ing)
            {
                FadeIn_ing = false;
                FadeOut_ing = false;
                Black_screen.gameObject.SetActive(false);
            }
            
        }
    }

    public void FadeIn()
    {
        Black_screen.gameObject.SetActive(true);
        FadeIn_ing = true;
    }

    public void FadeOut()
    {
        FadeOut_ing = true;
    }

}
[Serializable]
class Feedback
{
    public string Name;
    public string PhoneNumber;
    public string email;
    public string grade;
    public string userType;
    public Results results = new Results();
}
[Serializable]
class Results
{
    public List<QuestionAnswer> questionSets;
    public Results()
    {
        questionSets = new List<QuestionAnswer>();
    }
}

[Serializable]
class QuestionAnswer
{
    public string setType;
    public string Question;
    public string option;
    public string comment;
    public string rate;
}

