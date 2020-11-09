using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAndroidPlugin : MonoBehaviour
{

    // Use this for initialization
    string[] audioText = { "hi, How are you?", "Testing <phoneme alphabet=\"xsampa\" ph=\"&#34;{k.t@`\"/>.", "Whats Up", "What, are you. up to?", "How do you do!", " < speak xml:lang =\"en-US\"> <phoneme alphabet=\"xsampa\" ph=\"d_ZIn\"/>.</speak>" };
    string[] audioFileName = { "hi.wav", "Testing.wav", "Whats.wav", "up to.wav", "How do.wav", "ssml.wav" };
    string folderName;

    public float speed, pitch;
    public bool isAvater,isEvertTime;
    string voice;
    
    void Start()
    {
      //  Debug.Log("Unity Start");
        

    }

    public void ButtonClick()
    {
      //  Debug.Log("Unity Button Clicked");
        /*
        Debug.Log("Unity Button Clicked"+ folderName);
        AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");

        // Retrieve the UnityPlayerActivity object ( a.k.a. the current context )
        AndroidJavaObject unityActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");
        /*
        var ajc = new AndroidJavaClass("com.example.texttospeechlibText.ToSpeechLib");
        ajc.CallStatic("DoLog");
        */
        /*
        AndroidJavaObject toSpeech = new AndroidJavaObject("com.example.texttospeechlib.TextToSpeechLib");
        
        object[] setParameter = new object[2];
        setParameter[0] = 0.8f;
        setParameter[1] = 1.0f;
        toSpeech.Call("setSppedAndPitch", setParameter);
        for (int i = 0; i < audioText.Length; i++)
        {
            object[] parameters = new object[4];
            parameters[0] = unityActivity;
            parameters[1] = audioText[i];
            parameters[2] = audioFileName[i];
            parameters[3] = folderName;
            toSpeech.Call("textToAudio", parameters);
        }*/
        //  setSpeedPitch(0.8f, 1.0f);
        for (int i = 0; i < audioText.Length; i++)
        {
            TextToAudio(audioText[i], "Audio/", audioFileName[i]);
        }

    }

    public void setSpeedPitch(float _speed, float _pitch, bool _isAvater,bool _isevertTime, string voice)
    {
        speed = _speed;
        pitch = _pitch;
        isAvater = _isAvater;
        isEvertTime = _isevertTime;
        //voice = _voice;
    }

    public void TextToAudio(string text, string _folderName, string fileName)
    {
        folderName = Load_audio_n_Play_storyBoard.GetAndroidExternalFilesDir() + Load_audio_n_Play_storyBoard.folderName + _folderName;
        Debug.LogError("folderName TextToAudio: " + folderName);
        AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");

        // Retrieve the UnityPlayerActivity object ( a.k.a. the current context )
        AndroidJavaObject unityActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");
        /*
        var ajc = new AndroidJavaClass("com.example.texttospeechlibText.ToSpeechLib");
        ajc.CallStatic("DoLog");
        */
        AndroidJavaObject toSpeech = new AndroidJavaObject("com.example.texttospeechlib.TextToSpeechLib");

        object[] setParameter = new object[4];
        setParameter[0] = speed;
        setParameter[1] = pitch;
        setParameter[2] = isAvater;
        setParameter[3] = true;
        // setting parameters speed, pitch
        toSpeech.Call("setSppedAndPitch", setParameter);

        object[] parameters = new object[4];
        parameters[0] = unityActivity;
        parameters[1] = text;
        parameters[2] = fileName;
        parameters[3] = folderName;
        toSpeech.Call("textToAudio", parameters);


    }

 
}
