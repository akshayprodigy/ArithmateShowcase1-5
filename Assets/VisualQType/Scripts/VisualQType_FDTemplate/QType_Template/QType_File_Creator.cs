using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class storyboard_QType_data
{
    public int questionId;
    public string question;
    public string SubQuestion;
    public string events;
    public string audio;
    public string rightAns_Num;
    public string rightAns_Den;
    public List<string> rightAns;
    public string artifacts;
    public int numeratorShadeCount;
    public int denominatorShadeCount;
    public string questionSprite;
    public List<string> questionSprite_list;
    public string shadePartImg;
}

[System.Serializable]
public class jsonflags_QType
{
    public List<storyboard_QType_data> list;

}

public class QType_File_Creator : MonoBehaviour
{
    public static QType_File_Creator Instance;
    public string gameDataFileName, path, dataAsJson;
    public jsonflags_QType stry_data = new jsonflags_QType();
    public static List<int> numerators = new List<int>(), denominators = new List<int>(), mixedWhole = new List<int>();

    public TestAndroidPlugin tts;
    bool isAvatar = true;
    string voice = "en-in-x-ahp#male_1-local";

    private void Awake()
    {
        Instance = this;
        tts = FindObjectOfType<TestAndroidPlugin>();
    }


    private void Start()
    {
        print("thisObject " + this.gameObject.name);

        switch (this.gameObject.name)
        {
            case "VisualQType_AreaModel_Temp":
                {
                    StartCoroutine(story_board_json_audio("AreaModel.json")); //QType_AreaModel_Json
                    break;
                }

            case "VisualQTYpe_GroupObjects":
                {
                    StartCoroutine(story_board_json_audio("GroupObj.json"));//GroupObj  QType_GroupObj_Json
                    break;
                }

            case "VisualQType_FDTemplate":
                {
                    StartCoroutine(story_board_json_audio("Fract_Div.json"));//  QType_FractionAsDiv_Json
                    break;
                }

            case "QType_Template":
                {
                    StartCoroutine(story_board_json_audio("Fract_Propose.json"));//   QType_FractPurpose_Json
                    break;
                }
        }

        
        //LoadStoryBoardJson(3.0f); //call coroutine 3 sec
    }

    IEnumerator LoadStoryBoardJson(float time)
    {
        yield return new WaitForSeconds(time);
        StartCoroutine(story_board_json_audio("QType_AreaModel_Json.json")); 

    }

    IEnumerator story_board_json_audio(string file_name)
    {
        gameDataFileName = file_name;

        path = Path.Combine(Application.streamingAssetsPath, gameDataFileName);
        if (Application.platform == RuntimePlatform.Android)
        {
            // Android
            string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, gameDataFileName);
            Debug.Log("path Android  = " + filePath);
            // Android only use WWW to read file
            WWW reader = new WWW(filePath);
            while (!reader.isDone)
            {
                yield return null;
            }

            // Read the json from the file into a string
            dataAsJson = reader.text;
            Debug.Log("dataAsJson: " + dataAsJson);
            // Pass the json to JsonUtility, and tell it to create a GameData object from it
            //stry_data = JsonUtility.FromJson<jsonflags_QType>(dataAsJson);
            //Debug.Log("stry_data: " + stry_data);


        }
        else
        {
            Debug.Log(path);

            if (File.Exists(path))
            {
                // Read the json from the file into a string
                dataAsJson = File.ReadAllText(path);
                Debug.Log(path);

                Debug.Log(dataAsJson);
                // stry_data = JsonUtility.FromJson<jsonflags_QType>(dataAsJson);

            }
        }

        stry_data = JsonUtility.FromJson<jsonflags_QType>(dataAsJson);

        if (Application.platform == RuntimePlatform.Android)
        {

            int sequence = 0;

            for (int i = 0; i < stry_data.list.Count; i++)
            {
                Debug.Log(stry_data.list[i].question);
                Debug.Log(stry_data.list[i].audio);

                tts.setSpeedPitch(0.85f, 1.0f, isAvatar, true, voice);
                tts.TextToAudio(stry_data.list[i].question, "", stry_data.list[i].audio);

            }


        }

        else
        {
            for (int i = 0; i < stry_data.list.Count; i++)
            {
                Debug.Log(stry_data.list[i].question);
                Debug.Log(stry_data.list[i].audio);

            }
        }
    }

    //void story_board_json_audio(string file_name)
    //{
    //    //Debug.Log("inside storyboard json " + file_name);

    //    gameDataFileName = file_name;

    //    path = Path.Combine(Application.streamingAssetsPath, gameDataFileName);

    //    Debug.Log("path data = " + path);
    //    if (Application.platform == RuntimePlatform.Android)
    //    {
    //        // Android
    //        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, gameDataFileName);
    //        Debug.Log("path Android  = " + filePath);
    //        // Android only use WWW to read file
    //        WWW reader = new WWW(filePath);
    //        while (!reader.isDone) {

    //        }
    //        // Read the json from the file into a string
    //        reader.dow
    //        dataAsJson = reader.text;
    //        Debug.Log("dataAsJson: " + dataAsJson);
    //        // Pass the json to JsonUtility, and tell it to create a GameData object from it
    //        //stry_data = JsonUtility.FromJson<jsonflags_QType>(dataAsJson);
    //        //Debug.Log("stry_data: " + stry_data);


    //    }
    //    else
    //    {
    //        Debug.Log(path);

    //        if (File.Exists(path))
    //        {
    //            // Read the json from the file into a string
    //            dataAsJson = File.ReadAllText(path);
    //            Debug.Log(path);

    //            Debug.Log(dataAsJson);
    //           // stry_data = JsonUtility.FromJson<jsonflags_QType>(dataAsJson);
                
    //        }
    //    }
        
    //    stry_data = JsonUtility.FromJson<jsonflags_QType>(dataAsJson);
        
    //    if (Application.platform == RuntimePlatform.Android)
    //    {
            
    //        int sequence = 0;

    //        for(int i = 0; i < stry_data.list.Count; i++)
    //        {
    //            Debug.Log(stry_data.list[i].question);
    //            Debug.Log(stry_data.list[i].audio);

    //            tts.setSpeedPitch(0.85f, 1.0f, isAvatar, true, voice);
    //            tts.TextToAudio(stry_data.list[i].question, "", stry_data.list[i].audio);

    //        }

            
    //    }

    //    else
    //    {
    //        for (int i = 0; i < stry_data.list.Count; i++)
    //        {
    //            Debug.Log(stry_data.list[i].question);
    //            Debug.Log(stry_data.list[i].audio);

    //        }
    //    }
        

    //}

    void LoadStrryBoard(string jsonFileName)
    {
        StartCoroutine(story_board_json_audio(jsonFileName));

    }

    private static string GetAndroidExternalFilesDir()
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
