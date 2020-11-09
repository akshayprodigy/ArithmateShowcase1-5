using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using FantomLib;
using UnityEngine.Video;

//Speech Recognizer demo using controllers and localize
//音声認識でコントローラ（～Controller）とローカライズを利用したデモ
public class simple_speech_recognizer : MonoBehaviour
{


    public Text displayText;
    public bool result,call_to_speech;
    public float increment_timer=5.0f;
    public string recognized_text;
    public VideoPlayer vp;
    public SpeechRecognizerController src;
    //public create_timeline Create_Timeline;
 //   public video_player_manager video_manager;


    //Message when recognizer start.
    public LocalizeString recognizerStartMessage = new LocalizeString(SystemLanguage.English,
        new List<LocalizeString.Data>()
        {
            new LocalizeString.Data(SystemLanguage.English, "Starting Recognizer..."),    //default language
            new LocalizeString.Data(SystemLanguage.Japanese, "音声認識を起動してます…"),
        });

    //Message when recognizer ready.
    public LocalizeString recognizerReadyMessage = new LocalizeString(SystemLanguage.English,
        new List<LocalizeString.Data>()
        {
            new LocalizeString.Data(SystemLanguage.English, "Waiting voice..."),    //default language
            new LocalizeString.Data(SystemLanguage.Japanese, "音声を待機中…"),
        });

    //Message when recognizer begin.
    public LocalizeString recognizerBeginMessage = new LocalizeString(SystemLanguage.English,
        new List<LocalizeString.Data>()
        {
            new LocalizeString.Data(SystemLanguage.English, "Recognizing voice..."),    //default language
            new LocalizeString.Data(SystemLanguage.Japanese, "音声を取得しています…"),
        });

  
   


    //SpeechRecognizer
    public SpeechRecognizerController speechRecognizerControl;
 

   

  

  
    


    //==========================================================

    // Use this for initialization
    private void Start()
    {
       
    }

    public void call_method()
    {
        //if (video_manager.big_screen.clip != null)
        //{
        //    if (video_manager.big_screen.isPlaying)
        //        video_manager.big_screen.Pause();
        //}
        //if (video_manager.fullscreen_video_player.clip != null)
        //{
        //    if (video_manager.fullscreen_video_player.isPlaying)
        //        video_manager.fullscreen_video_player.Pause();
        //}

        //if (vp.isPlaying)
        //{
        //    vp.Pause();
        //}
        //Create_Timeline.timeline_pause();
        OnStartRecognizer();
        speechRecognizerControl.StartRecognizer();
        call_to_speech = true;
       
        

    }
    private void Update()
    {
        //if (call_to_speech == true)
        //{
        //    increment_timer = increment_timer - Time.deltaTime;
        //    if (increment_timer < 0)
        //    {
        //        speechRecognizerControl.StartRecognizer();
        //        call_to_speech = false;
        //    }
        //}
    }
    // Update is called once per frame
    //private void Update () {

    //}


    //==========================================================
    //Display text string

    //Display text string (and for reading)
    public void DisplayText(object message)
    {
        if (displayText != null)
            displayText.text = message.ToString();
    }

    public void DisplayText(object message, bool add)
    {
        if (displayText != null)
        {
            if (add)
                displayText.text += message;
            else
                displayText.text = message.ToString();
        }
    }

    public void DisplayTextLine(object message)
    {
        DisplayText(message + "\n", true);
    }

    public void DisplayText(object[] words)
    {
        if (displayText != null)
        {
            displayText.text = string.Join("\n", words.Select(e => e.ToString()).ToArray());
            
        }
    }

    public void DisplayPermission(string permission, bool granted)
    {
        if (displayText != null)
            displayText.text += permission.Replace("android.permission.", "") + " = " + granted + "\n";
    }

    //==========================================================
    //Function of text etc.

  

   

    //Toggle button (webSearchToggle) to switch WebSearch.
   

    //==========================================================
    //Example with Google dialog

    //Receive results from speech recognition with dialog.
    public void OnResultSpeechRecognizerDialog(string[] words)
    {
        DisplayText(words);
       
    }


    //==========================================================
    //Example without dialog (Callback handlers)

    //Callback handler for start Speech Recognizer
    public void OnStartRecognizer()
    {
        if (speechRecognizerControl != null)
        {
            if (speechRecognizerControl.IsSupportedRecognizer && speechRecognizerControl.IsPermissionGranted)
            {
                DisplayText(recognizerStartMessage);
              
            }
        }
    }

    //Callback handler for microphone standby state
    public void OnReady()
    {
        DisplayText(recognizerReadyMessage);
  
    }

    ///Callback handler for microphone begin speech recognization state
    public void OnBegin()
    {
        DisplayText(recognizerBeginMessage);
   
    }

    //Receive the result when speech recognition succeed.
    public void OnResult(string[] words)
    {
        
        DisplayText(words);
        
        recognized_text = words[0];
        result = true;
        //if (recognized_text == null || recognized_text=="")
        //{
        //    Create_Timeline.timeline_resume();

        //}
      
    }

    //Receive the error when speech recognition fail.
    public void OnError(string message)
    {
    
        DisplayText(message);
        //Create_Timeline.timeline_resume();
        //if (video_manager.big_screen.clip != null)
        //{
        //    video_manager.big_screen.Play();
        //}
        //if (video_manager.fullscreen_video_player.clip != null)
        //{
        //    video_manager.fullscreen_video_player.Play();
        //}



    }


    
  

 

    //Callback handler for locale change dropdown (OnValueChanged)
    public void OnLocaleValueChanged(Dropdown localeDropdown)
    {
        if (localeDropdown == null)
            return;

        string loc = localeDropdown.captionText.text;
        if (speechRecognizerControl != null)
            speechRecognizerControl.Locale = (loc == AndroidLocale.Default) ? "" : loc; //To make it the system default, put an empty character ("").
       
    }
}
