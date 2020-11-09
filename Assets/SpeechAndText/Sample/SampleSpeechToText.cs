﻿using UnityEngine;
using UnityEngine.UI;
using TextSpeech;
using UnityEngine.Android;

public class SampleSpeechToText : MonoBehaviour
{
    //public GameObject loading;
    //public InputField inputLocale;
    //public InputField inputText;
    public float pitch;
    public float rate;

    //GameObject dialog = null;

    //public Text txtLocale;
    //public Text txtPitch;
    //public Text txtRate;
    void Start()
    {
        Setting("en-US");
        //loading.SetActive(false);
        SpeechToText.instance.onResultCallback = OnResultSpeech;



        //if (Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
        //{
        //    // The user authorized use of the microphone.
        //}
        //else
        //{
        //    // We do not have permission to use the microphone.
        //    // Ask for permission or proceed without the functionality enabled.
        //    Permission.RequestUserPermission(Permission.ExternalStorageWrite);
        //}

        if (Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            // The user authorized use of the microphone.
        }
        else
        {
            // We do not have permission to use the microphone.
            // Ask for permission or proceed without the functionality enabled.
            Permission.RequestUserPermission(Permission.Microphone);
        }

    }

    public void StartRecording()
    {
        Debug.Log("StartRecording: ");
#if UNITY_EDITOR
        Debug.Log("StartRecording:___UnityEditor ");

#else
        SpeechToText.instance.StartRecording("Speak any");
#endif
    }

    public void StopRecording()
    {
#if UNITY_EDITOR
        OnResultSpeech("Not support in editor.");
        Debug.Log("StopRecording:___UnityEditor ");

#else
        SpeechToText.instance.StopRecording();
#endif
#if UNITY_IOS
        //loading.SetActive(true);
#endif
    }
    void OnResultSpeech(string _data)
    {
        //inputText.text = _data;
#if UNITY_IOS
        //loading.SetActive(false);
#endif
    }
    public void OnClickSpeak()
    {
        //TextToSpeech.instance.StartSpeak(inputText.text);
    }
    public void  OnClickStopSpeak()
    {
        //TextToSpeech.instance.StopSpeak();
    }
    public void Setting(string code)
    {
        //TextToSpeech.instance.Setting(code, pitch, rate);
        SpeechToText.instance.Setting(code);
        //txtLocale.text = "Locale: " + code;
        //txtPitch.text = "Pitch: " + pitch;
        //txtRate.text = "Rate: " + rate;
    }
    public void OnClickApply()
    {
        //Setting(inputLocale.text);
    }

}
