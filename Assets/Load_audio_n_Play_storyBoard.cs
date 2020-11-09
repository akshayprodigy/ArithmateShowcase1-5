using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load_audio_n_Play_storyBoard : MonoBehaviour
{
    // Use this for initialization
    public AudioSource aSource;
    string FILE_NAME = "http://192.168.0.103:8080/1.wav";
    string file = "";
    public static string folderName = "/Sound/";//TTS/
    string Header = folderName + "hi.wav";
    public static string _FOLDERNAME;
    public bool runtime_audio;
    //public Text audio_status;
    public bool audio_ready, relearn_audio = false;
    public float length_of_audio = 0;
    public bool speakflag;
    bool temp_ready = false;
    float counter_video = 0;

    //  public AudioClip ab;
    private void Awake()
    {
        aSource =GameObject.Find("Player").GetComponent<AudioSource>();

    }
    void Start()
    {
        if (!Application.isEditor)
            _FOLDERNAME = GetAndroidExternalFilesDir() + folderName;
     
    }

    public void PlaySound()
    {
        // StartCoroutine(LoadAndPlaySound(Header));
        // PlayAudioFile("Audio/", "hi.wav",ab);
    }

    public void load_sound_rePlay(string _folderName, string fileName, AudioClip ac)
    {
        audio_ready = false;
        string fullName = folderName + _folderName + fileName;
        StartCoroutine(LoadSoundReplay(fullName, ac));
    }

    public void load_sound(string _folderName, string fileName, AudioClip ac)
    {
        audio_ready = false;
        string fullName = folderName + _folderName + fileName;
        StartCoroutine(LoadSound(fullName, ac));

    }




    // call this funtion to load audio file from a specific folder
       public void PlayAudioFile(string _folderName, string fileName, AudioClip ac, string speaker)
    {
        string fullName = folderName + _folderName + fileName;
        Debug.Log("PlayAudioFile: " + fullName);
        Debug.Log("PlayAudioFile speaker: " + speaker);
        StartCoroutine(LoadAndPlaySound(fullName, ac, speaker));
    }


    // call this coroutine from   function  to load audio file from a specific folder
    IEnumerator LoadAndPlaySound(string header, AudioClip ds, string speaker)
    {
       
        if (runtime_audio == true)
        {
            runtime_audio = false;
            yield return new WaitForSeconds(1.8f);
        }
        else
        {
            runtime_audio = false;
        }
        if (!Application.isEditor)
        {
            string fileName = GetAndroidExternalFilesDir() + header;//Header;
            Debug.Log("Audio fileName for testing: " + fileName);
            if (!System.IO.File.Exists(fileName))
            {
                Debug.Log("File does NOT exist   file path = " + fileName);
                yield break;
            }
            Caching.ClearCache();

            if (aSource.clip != null)
            {
                aSource.clip.UnloadAudioData();
            }
            


            WWW request = new WWW("file:///" + fileName);
            Debug.Log("file:///" + fileName);

            while (!request.isDone)
            {
                Debug.Log("request progrss " + request.progress);
                yield return new WaitForEndOfFrame();
            }
            if (request.error == null)
            {

                AudioClip audioTrack = request.GetAudioClip();
                while (audioTrack.loadState == AudioDataLoadState.Loading)
                {
                    // Wait until loading completed
                    yield return new WaitForEndOfFrame();
                }
                if (audioTrack.loadState != AudioDataLoadState.Loaded)
                {
                    // Fail to load
                    Debug.Log("Failed to Load!");
                    yield break;
                }
                if (speaker.Equals("avatar"))
                {
                    speakflag = false;
                    aSource.clip = audioTrack;
                }
                Debug.Log("audio: " + audioTrack);
                length_of_audio = audioTrack.length;
                Debug.Log("length from load and play " + length_of_audio);
                audio_ready = true;
            }
            else
            {
                Debug.Log("error: " + request.error);
            }
        }



    }

    IEnumerator LoadSoundReplay(string header, AudioClip ds)
    {
        if (runtime_audio == true)
        {
            runtime_audio = false;
            yield return new WaitForSeconds(1.8f);
        }
        else
        {
            runtime_audio = false;
        }
        if (!Application.isEditor)
        {
            string fileName = GetAndroidExternalFilesDir() + header;//Header;
            Debug.Log("fileName: " + fileName);
            if (!System.IO.File.Exists(fileName))
            {
                Debug.Log("File does NOT exist   file path = " + fileName);
                //audio_status.text = "File does NOT exist   file path ";
                yield break;
            }
            Caching.ClearCache();

            if (aSource.clip != null)
            {
                aSource.clip.UnloadAudioData();
            }

            WWW request = new WWW("file:///" + fileName);//WWW.LoadFromCacheOrDownload(fileName, 0);
            Debug.Log("file:///" + fileName);

            while (!request.isDone)
            {
                Debug.Log("request progrss " + request.progress);
                //audio_status.text = audio_status.text + "request progrss " + request.progress;
                yield return new WaitForEndOfFrame();
            }
            if (request.error == null)
            {

                AudioClip audioTrack = request.GetAudioClip();

                while (audioTrack.loadState == AudioDataLoadState.Loading)
                {
                    // Wait until loading completed
                    yield return new WaitForEndOfFrame();
                }

                if (audioTrack.loadState != AudioDataLoadState.Loaded)
                {
                    // Fail to load
                    Debug.Log("Failed to Load!");
                    yield break;
                }

                aSource.clip = audioTrack;

                Debug.Log("audio: " + audioTrack);
                length_of_audio = audioTrack.length;
                audio_ready = true;

                aSource.Play();

                //relearn_audio flag alerts system if file playing is started or not
                relearn_audio = true;

            }
            else
            {
                Debug.Log("error: " + request.error);
            }

        }


    }

    IEnumerator LoadSound(string header, AudioClip ds)
    {
        if (runtime_audio == true)
        {
            runtime_audio = false;
            yield return new WaitForSeconds(1.8f);
        }
        else
        {
            runtime_audio = false;
        }
        if (!Application.isEditor)
        {
            string fileName = GetAndroidExternalFilesDir() + header;//Header;
            Debug.Log("fileName: " + fileName);
            if (!System.IO.File.Exists(fileName))
            {
                Debug.Log("File does NOT exist   file path = " + fileName);
                //audio_status.text = "File does NOT exist   file path ";
                yield break;
            }
            Caching.ClearCache();

            if (aSource.clip != null)
            {
                aSource.clip.UnloadAudioData();
            }

            WWW request = new WWW("file:///" + fileName);//WWW.LoadFromCacheOrDownload(fileName, 0);
            Debug.Log("file:///" + fileName);

            while (!request.isDone)
            {
                Debug.Log("request progrss " + request.progress);
                //audio_status.text = audio_status.text + "request progrss " + request.progress;
                yield return new WaitForEndOfFrame();
            }
            if (request.error == null)
            {

                AudioClip audioTrack = request.GetAudioClip();

                while (audioTrack.loadState == AudioDataLoadState.Loading)
                {
                    // Wait until loading completed
                    yield return new WaitForEndOfFrame();
                }

                if (audioTrack.loadState != AudioDataLoadState.Loaded)
                {
                    // Fail to load
                    Debug.Log("Failed to Load!");
                    //audio_status.text = audio_status.text + "failed to load";
                    yield break;
                }

                aSource.clip = audioTrack;

                Debug.Log("audio: " + audioTrack);
                //audio_status.text = audio_status.text + audioTrack.name;

                counter_video = aSource.clip.length;
                length_of_audio = audioTrack.length;
              
                Debug.Log("length from load and play " + length_of_audio);
                audio_ready = true;

                aSource.Play();
                temp_ready = true;

            }
            else
            {
                //audio_status.text = audio_status.text + "error:  " + request.error;
                Debug.Log("error: " + request.error);
            }

        }


    }


    
    

    public static string GetAndroidExternalFilesDir()
    {
        using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            using (AndroidJavaObject context = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                // Get all available external file directories (emulated and sdCards)
                AndroidJavaObject[] externalFilesDirectories = context.Call<AndroidJavaObject[]>("getExternalFilesDirs", null);
                AndroidJavaObject emulated = null;
                AndroidJavaObject sdCard = null;

                for (int i = 0; i < externalFilesDirectories.Length; i++)
                {
                    AndroidJavaObject directory = externalFilesDirectories[i];
                    using (AndroidJavaClass environment = new AndroidJavaClass("android.os.Environment"))
                    {
                        // Check which one is the emulated and which the sdCard.
                        bool isRemovable = environment.CallStatic<bool>("isExternalStorageRemovable", directory);
                        bool isEmulated = environment.CallStatic<bool>("isExternalStorageEmulated", directory);
                        if (isEmulated)
                            emulated = directory;
                        else if (isRemovable && isEmulated == false)
                            sdCard = directory;
                    }
                }
                // Return the sdCard if available
                if (sdCard != null)
                    return sdCard.Call<string>("getAbsolutePath");
                else
                    return emulated.Call<string>("getAbsolutePath");
            }
        }
    }
}
