using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;



[System.Serializable]
public class interv_array
{
    public List<interv_data> interventions = new List<interv_data>();
    public List<storyboard_data> STORY = new List<storyboard_data>();
}
[System.Serializable]
public class interv_data
{

    public string response_id;
    public string conversation_type;
    public string response_for_user;
    public string audio_file_name;
    public string function_call;
    public string to_conversation_builder;
    //updated sagar 11-7-19
    public string context;
    public string context_based_flag;
}
[System.Serializable]
public class storyboard_data
{
    public string sentence;
    public string events;
    public string audio;
    public string actual_answer;
    public List<string> Right_answer;
    public List<string> Wrong_answer;
    public string artifacts;
    public string Timeout_answer;
    public string Facts;
    public string Value_Count;
    public string next_sequence;
    public string Skills;
    public string camera_location;
    public string speaker;
    //updated sagar 11-7-19
    public string context;


}

[System.Serializable]
public class jsonflags
{
    public string flag;
    public List<storyboard_data> list;

}
[System.Serializable]
public class timeline_json_subdata
{
    public jsonflags Introduction;
    public jsonflags Explanation;
    public jsonflags Activity;
}

[System.Serializable]
public class timeline_data
{
    public timeline_json_subdata timeline_json;
}
//get autio file text and name from json
public class audio_files_data
{
    public string file_name;
    public string file_text;
};

public class audi_files_creator : MonoBehaviour
{

    // replace @ with denominator 
    // replace & with numerator
    // # for mixed whole number

    public audio_files_data audio_Files_Data = new audio_files_data();
    public Queue<audio_files_data> Introduction_queue = new Queue<audio_files_data>();
    public Queue<audio_files_data> Explaination_queue = new Queue<audio_files_data>();
    public Queue<audio_files_data> activity_queue = new Queue<audio_files_data>();
    public Queue<audio_files_data> response_queue = new Queue<audio_files_data>();

    public List<interv_array> interv_info = new List<interv_array>();
    public TestAndroidPlugin tts;
   public  interv_array alldata = new interv_array();

    //public timeline_data stry_data = new timeline_data();

    public timeline_json_subdata stry_data = new timeline_json_subdata();
    public jsonflags jsngflag = new jsonflags();
    public storyboard_data Storyboard_Data = new storyboard_data();
    public bool audio_ready, isEvertTime;
    public static string audio_file_creation_scene_name = "EOF1";

    //    data3 addit = new data3();
    public string gameDataFileName, path, dataAsJson;
    public int numberOfCasesToShow = 3;
    public int listCount = 10;
    public static List<int> numerators = new List<int>(), denominators = new List<int>(), mixedWhole = new List<int>();
    bool isAvatar = true;
    string voice = "en-in-x-ahp#male_1-local";

    //public create_runtime_timeline _create_runtime_timeline;
    public delegate void TimeLineRun();
    public static event TimeLineRun OnTimeLineRun;

    //data quew

    public List<audio_files_data> ResponceAudioListQueue, TimeAudioListQueue;
    // Use this for initialization
    void initiliseAllQueue()
    {
        ResponceAudioListQueue = new List<audio_files_data>();
        TimeAudioListQueue = new List<audio_files_data>();
        TimeAudioListQueue.Clear();
        ResponceAudioListQueue.Clear();
    }


    public void enQueueAudio(bool responce,audio_files_data data)
    {
        if (responce)
        {
            ResponceAudioListQueue.Add(data);
        }
        else
        {
            TimeAudioListQueue.Add(data);
        }
    }

    public audio_files_data deQueueAudio(bool responce)
    {
        audio_files_data data;
        if (responce)
        {
            data = ResponceAudioListQueue[0];
            ResponceAudioListQueue.RemoveAt(0);
        }
        else
        {
            data = TimeAudioListQueue[0];
            TimeAudioListQueue.RemoveAt(0);
        }
        return data;
    }


    private void Awake()
    {
        tts = FindObjectOfType<TestAndroidPlugin>();
        setCollectedAppleForTest();
    }

    void Start()
    { 
        Debug.Log("call to storyboard json");
        EventManager.AddHandler(EVENT.SetUpTimeLine, InitilizeAudio);
       // InitilizeAudioFileCreator("test_json.json");
    }

    void InitilizeAudio()
    {
        InitilizeAudioFileCreator(EventManager.getNameOfJasonFile());
    }

    void InitilizeAudioFileCreator(string jsonName)
    {
        initiliseAllQueue();
        load_interv_json(jsonName);
        audio_ready = true;
        //UtilityArtifacts.current_json = jsonName;
        if (Application.isEditor)
        {
            StartCoroutine(LoadTimeLineForEditor());
        }
        else
        {
            CreateAudio();
        }
    }

    void setCollectedAppleForTest()
    {
        //AppleManager.CollectedFullRedApple = 3;
        //AppleManager.CollectedFullGreenApple = 1;
        //AppleManager.CollectedFullYellowApple = 1;
        //AppleManager.totalWholeApple();
    }
    IEnumerator LoadTimeLineForEditor()
    {
        yield return new WaitForSeconds(1);
        if (OnTimeLineRun != null)
            OnTimeLineRun();
    }

    void LoadStrryBoard(string jsonFileName)
    {
        //Debug.LogError("current_json: " + UtilityArtifacts.current_json);
        story_board_json_audio(jsonFileName);
        //switch (UtilityArtifacts.current_json)
        //{
        //    case UtilityArtifacts.json_story_board:
        //        story_board_json_audio("storyboard.json");

        //        break;
        //    case UtilityArtifacts.json_proper_storyboard:

        //        story_board_json_audio("storyboard_proper.json");

        //        break;
        //    case UtilityArtifacts.json_improper_storyboard:
        //        story_board_json_audio("storyboard_improper.json");
        //        break;

        //    case UtilityArtifacts.json_EOF_storyboard:
        //        story_board_json_audio("storyboard_EOF.json");

        //        break;
        //    case UtilityArtifacts.json_addition_of_mixed_fraction:
        //        story_board_json_audio("mixed_fraction.json");
        //        break;
        //    case UtilityArtifacts.json_addition:
        //        story_board_json_audio("addition.json");
        //        break;
        //    case UtilityArtifacts.json_SentenceConnector_storyboard:
        //        story_board_json_audio("storyboard_EOF.json");
        //        break;
        //    case UtilityArtifacts.what_fraction_looks_like:
        //        story_board_json_audio("what_fraction_looks_like.json");
        //        break;
        //    case UtilityArtifacts.Activity_representation_of_fraction:
        //        story_board_json_audio("Activity_representation_of_fraction.json");
        //        break;
        //    case UtilityArtifacts.ROF_Numerator_denominator:
        //        story_board_json_audio("ROF_Numerator_denominator.json");
        //        break;
        //    case UtilityArtifacts.json_Equivalent_Concrete_Exp_storyboard:
        //        story_board_json_audio("Equivalent_Concrete_Exp.json");
        //        break;


        //        //eof josns
        //    case UtilityArtifacts.json_eof_dignostic_testing:
        //        story_board_json_audio("storyboard_EOF_DIG.json");

        //        break;
        //    case UtilityArtifacts.json_eof_anstract_conceptualisation:
        //        story_board_json_audio("storyboard_EOF_AC.json");
        //        break;
        //    case UtilityArtifacts.json_eof_active_experiment:
        //        story_board_json_audio("storyboard_EOF_ACT.json");
        //        break;
        //    case UtilityArtifacts.json_eof_visual_q_type:
        //        story_board_json_audio("json_eof_visual_q_type.json");
        //        break;
        //    case UtilityArtifacts.json_Equivalent_Fraction_Objective2:
        //        story_board_json_audio("Equivalent_Fraction_Objective_2_Concrete_Exp.json");
        //        break;
        //    case UtilityArtifacts.json_Equivalent_Fraction_Objective2_AbstractCon:
        //        story_board_json_audio("storyboard_eof_obj2_abstract_conceptualisation.json");
        //        break;
        //    case UtilityArtifacts.json_Equivalent_Fraction_Objective2_ActiveExperiment:
        //        story_board_json_audio("storyboard_eof_obj2_active_experimentation.json");
        //        break;
        //    case UtilityArtifacts.objective2_visual_qtype:
        //        story_board_json_audio("objective2_visual_qtype.json");
        //        break;


        //    case UtilityArtifacts.test_json:
        //        story_board_json_audio("test_json.json");
        //        break;

        //}

    }


    void load_interv_json(string jsonFileName)
    {
        //test
        CreateFractionSequence();

        // gameDataFileName = "structured_json newly updated_EOF.json";
        //gameDataFileName = "structured_json newly updated_EOF.json";
        // for extra prompts
        gameDataFileName = UtilityArtifacts.json_interval_prefix + jsonFileName;
        Debug.Log("gameDataFileName: " + gameDataFileName);
        //if (SceneManager.GetActiveScene().buildIndex == 3)
        //{
        //    //akshays scene concrete ex and reflective obs
        //    gameDataFileName = "structured_json newly updated_EOF.json";
        //}
        //else if (SceneManager.GetActiveScene().buildIndex == 6)//6
        //{
        //    gameDataFileName = "structured_json visual qtype.json";
        //}
        //else if (SceneManager.GetActiveScene().buildIndex == 7)
        //{
        //    gameDataFileName = "ExtraModuleEqObjective2.json";
        //}
        //else if (SceneManager.GetActiveScene().buildIndex == 8|| SceneManager.GetActiveScene().buildIndex == 9)
        //{
        //    Debug.LogError("current_json: " + UtilityArtifacts.current_json);
        //    gameDataFileName = "structure_json_eof__obj2_active_experiment1A.json";
        //}
        //else if (SceneManager.GetActiveScene().buildIndex == 10)
        //{
        //    gameDataFileName = "visual_qtype_obj2_structured.json";
        //}
        //else
        //{

        //    gameDataFileName = "structured_json_mahesh.json";

        //}
        path = Path.Combine(Application.streamingAssetsPath, gameDataFileName);

        //Debug.Log("path data = " + path);
        if (Application.platform == RuntimePlatform.Android)
        {
            // Android
            string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, gameDataFileName);

            // Android only use WWW to read file
            WWW reader = new WWW(filePath);
            while (!reader.isDone) { }
            // Read the json from the file into a string
            string dataAsJson = reader.text;
            Debug.Log(dataAsJson);
            // Pass the json to JsonUtility, and tell it to create a GameData object from it
            alldata = JsonUtility.FromJson<interv_array>(dataAsJson);

            Debug.Log("android: " + alldata);
            Debug.Log("all data: " + alldata);
            Debug.LogError("value1: " + alldata.interventions.Count);

        }
        else
        {

            if (File.Exists(path))
            {
                // Read the json from the file into a string
                dataAsJson = File.ReadAllText(path);

                Debug.Log(path);
                Debug.Log(dataAsJson);
                alldata = JsonUtility.FromJson<interv_array>(dataAsJson);
               // interv_info = alldata;
                //dep = JsonUtility.FromJson<data1>(dataAsJson);


                //alldata.Department.RemoveAt(alldata.Department.Count-1);

                Debug.Log("all data: " + alldata);
             //   Debug.Log("value1: " + alldata.interventions.Count);



            }
        }
        if (Application.isEditor)
        {

        }
        else
        {
            //if (SceneManager.GetActiveScene().name.Equals(audio_file_creation_scene_name))
            //{
            if (SceneManager.GetActiveScene().name.Equals("DemoScene_proper"))
            {
                CreateFractionSequence();
            }

            for (int i = 0; i < alldata.interventions.Count; i++)
            {
                string audiofilename = alldata.interventions[i].audio_file_name;
                string fileName = GetAndroidExternalFilesDir() + "files/Sound/TTS/" + audiofilename;//Header;
                Debug.LogError("fileName:load_interv_json " + fileName);
                if (!System.IO.File.Exists(fileName))
                {
                    //Debug.Log("File does NOT exist   file path = " + fileName);
                    //isAvatar = true;
                    //tts.setSpeedPitch(0.85f, 1.0f, isAvatar);
                    //tts.TextToAudio(alldata.interventions[i].response_for_user, "", audiofilename);
                    string audiotext = alldata.interventions[i].response_for_user;
                    Debug.Log("File does NOT exist   file path = " + fileName);
                    Debug.Log("Activityaudiotext: " + audiotext);


                    // audio file listing
                    audio_files_data data = new audio_files_data();
                    data.file_name = audiofilename;
                    data.file_text = audiotext;
                    enQueueAudio(true, data);
                    //sagar Updated :-11-7-19
                    //isAvatar = true;
                    //isEvertTime = false;
                    //tts.setSpeedPitch(0.85f, 1.0f, isAvatar, isEvertTime, voice);
                    //tts.TextToAudio(audiotext, "", audiofilename);
                }
                else
                {
                    Debug.Log("already present");
                }
            }
            //}
        }
        LoadStrryBoard(jsonFileName);
    }


    // set up
    public void CreateFractionSequence()
    {
        int num;
        for (int i = 0; i < numberOfCasesToShow; i++)
        {
            switch (i)
            {
                case 0:
                    num = 1;
                    setupProperfraction(num);
                    break;
                case 1:
                    num = Random.Range(2, listCount - 2);
                    setupProperfraction(num);
                    break;
                case 2:
                    num = Random.Range(4, listCount + 2);
                    setupMixedfraction(num);
                    break;
                default:
                    int n = Random.Range(0, 2);
                    if (n > 0)
                    {
                        // set up proper fraction
                        num = Random.Range(1, listCount - 2);
                        setupProperfraction(num);
                    }
                    else
                    {
                        num = Random.Range(4, listCount + 2);
                        setupMixedfraction(num);
                    }
                    break;
            }

        }
        UtilityArtifacts.numerators = numerators;
        UtilityArtifacts.denominators = denominators;
        UtilityArtifacts.mixedWhole = mixedWhole;
    }

    void setupProperfraction(int num)
    {
        int dem = Random.Range(2, 10);
        numerators.Add(num);
        denominators.Add(dem);
        mixedWhole.Add(0);
    }

    void setupMixedfraction(int num)
    {
        int dem = Random.Range(2, num);
        while (num % dem == 0 || num / dem > 2)
            dem = Random.Range(2, num);
        numerators.Add(num);
        denominators.Add(dem);
        mixedWhole.Add(num / dem);
    }




    void story_board_json_audio(string file_name)
    {
        //Debug.Log("inside storyboard json " + file_name);

        gameDataFileName = file_name;

        path = Path.Combine(Application.streamingAssetsPath, gameDataFileName);

        Debug.Log("path data = " + path);
        if (Application.platform == RuntimePlatform.Android)
        {
            // Android
            string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, gameDataFileName);

            // Android only use WWW to read file
            WWW reader = new WWW(filePath);
            while (!reader.isDone) { }
            // Read the json from the file into a string
            dataAsJson = reader.text;
            Debug.Log("dataAsJson: " + dataAsJson);
            // Pass the json to JsonUtility, and tell it to create a GameData object from it
            stry_data = JsonUtility.FromJson<timeline_json_subdata>(dataAsJson);
            //Debug.Log(stry_data.Activity.list[0].artifacts);


        }
        else
        {
            if (File.Exists(path))
            {
                // Read the json from the file into a string
                dataAsJson = File.ReadAllText(path);

                //Debug.Log(dataAsJson);
                stry_data = JsonUtility.FromJson<timeline_json_subdata>(dataAsJson);

                //dep = JsonUtility.FromJson<data1>(dataAsJson);


                //alldata.Department.RemoveAt(alldata.Department.Count-1);




            }
        }

        //  dataAsJson = UtilityREST.timeline_json;
        //Debug.Log("soryboard data " + dataAsJson);
        stry_data = JsonUtility.FromJson<timeline_json_subdata>(dataAsJson);
        //Debug.Log("Count of timelne data" + stry_data.Introduction.list.Count);


        //Debug.Log("Count of timelne data" + stry_data.Explanation.list.Count);
        //Debug.Log("Count of timelne data" + stry_data.Activity.list.Count);

        //Debug.Log("intro" + stry_data.Introduction.flag);
        //Debug.Log("explain" + stry_data.Explanation.flag);
        //Debug.Log("activity" + stry_data.Activity.flag);
        //if (SceneManager.GetActiveScene().name.Equals(audio_file_creation_scene_name))
        //{
        if (Application.platform == RuntimePlatform.Android)
        {
            //Debug.Log("Inside audio_file_creation_scene_name: " + audio_file_creation_scene_name + "SceneManager.GetActiveScene().name: " + SceneManager.GetActiveScene().name);

            if (stry_data.Introduction.flag.Equals("true"))
            {
                for (int i = 0; i < stry_data.Introduction.list.Count; i++)
                {
                    string audiofilename = stry_data.Introduction.list[i].audio;
                    string fileName = GetAndroidExternalFilesDir() + "files/Sound/TTS/" + audiofilename;//Header;
                    string audiotext = stry_data.Introduction.list[i].sentence;
                    Debug.LogError("fileName: story_board_json_audio " + fileName);
                    //Debug.Log("fileName Introduction: " + fileName);
                    if (!System.IO.File.Exists(fileName))
                    {
                        //Debug.Log("File does NOT exist Introduction   file path = " + fileName);
                        ////sagar Updated :-11-7-19
                        //if (stry_data.Introduction.list[i].speaker.Equals("avatar"))
                        //{
                        //    isAvatar = true;
                        //}
                        //else
                        //{
                        //    isAvatar = false;
                        //}
                        //tts.setSpeedPitch(0.85f, 1.0f, isAvatar);
                        //tts.TextToAudio(stry_data.Introduction.list[i].sentence, "", audiofilename);
                        //isAvatar = !isAvatar;

                        //Debug.Log("File does NOT exist   file path = " + fileName);
                        //Debug.Log("Activityaudiotext: " + audiotext);

                        // audio file listing
                        audio_files_data data = new audio_files_data();
                        data.file_name = audiofilename;
                        data.file_text = audiotext;
                        enQueueAudio(false, data);
                        //sagar Updated :-11-7-19 not needed
                        //if (stry_data.Introduction.list[i].speaker.Equals("avatar"))
                        //{
                        //    isAvatar = true;
                        //}
                        //else
                        //{
                        //    isAvatar = false;
                        //}
                        //if (stry_data.Introduction.list[i].Value_Count.Equals("0"))
                        //{
                        //    isEvertTime = false;
                        //}
                        //else
                        //{
                        //    isEvertTime = true;
                        //}
                        //tts.setSpeedPitch(0.85f, 1.0f, isAvatar, isEvertTime, voice);
                        //tts.TextToAudio(audiotext, "", audiofilename);


                    }
                    else
                    {
                        Debug.Log("already present");
                    }
                }
            }

            if (stry_data.Explanation.flag.Equals("true"))
            {
                Debug.Log("Explanation: " + stry_data.Explanation);
                for (int i = 0; i < stry_data.Explanation.list.Count; i++)
                {
                    string audiofilename = stry_data.Explanation.list[i].audio;
                    string fileName = GetAndroidExternalFilesDir() + "files/Sound/TTS/" + audiofilename;//Header;
                    string audiotext = stry_data.Explanation.list[i].sentence;
                    Debug.LogError("fileName: Explanation" + fileName);
                    if (!System.IO.File.Exists(fileName))
                    {
                        //Debug.Log("File does NOT exist   file path = " + fileName);

                        ////sagar Updated :-11-7-19
                        //if (stry_data.Explanation.list[i].speaker.Equals("avatar"))
                        //{
                        //    isAvatar = true;
                        //}
                        //else
                        //{
                        //    isAvatar = false;
                        //}
                        //tts.setSpeedPitch(0.85f, 1.0f, isAvatar);
                        //tts.TextToAudio(stry_data.Explanation.list[i].sentence, "", audiofilename);
                        //isAvatar = !isAvatar;
                        // audio file listing
                        audio_files_data data = new audio_files_data();
                        data.file_name = audiofilename;
                        data.file_text = audiotext;
                        enQueueAudio(false, data);
                        //Debug.Log("File does NOT exist   file path = " + fileName);
                        //Debug.Log("Activityaudiotext: " + audiotext);
                        ////sagar Updated :-11-7-19
                        //if (stry_data.Explanation.list[i].speaker.Equals("avatar"))
                        //{
                        //    isAvatar = true;
                        //}
                        //else
                        //{
                        //    isAvatar = false;
                        //}
                        //if (stry_data.Explanation.list[i].Value_Count.Equals("0"))
                        //{
                        //    isEvertTime = false;
                        //}
                        //else
                        //{
                        //    isEvertTime = true;
                        //}
                        //tts.setSpeedPitch(0.85f, 1.0f, isAvatar, isEvertTime, voice);
                        //tts.TextToAudio(audiotext, "", audiofilename);
                    }
                    else
                    {
                        Debug.Log("already present");
                    }
                }

            }

            // replace @ with denominator 
            // replace & with numerator
            // # for mixed whole number
            int sequence = 0;
            if (stry_data.Activity.flag.Equals("true"))
            {
                Debug.Log("Activity: " + stry_data.Activity);
                for (int i = 0; i < stry_data.Activity.list.Count; i++)
                {
                    Debug.Log("Activity.list[i]: " + stry_data.Activity.list[i]);
                    string audiofilename = stry_data.Activity.list[i].audio;
                    string fileName = GetAndroidExternalFilesDir() + "files/Sound/TTS/" + audiofilename;//Header;
                    Debug.LogError("fileName: Explanation" + fileName);
                    string audiotext = stry_data.Activity.list[i].sentence;
                    int varCount = int.Parse(stry_data.Activity.list[i].Value_Count);
                    bool next = bool.Parse(stry_data.Activity.list[i].next_sequence);
                    Debug.Log("audiotext: " + audiotext);
                    if (varCount > 0)
                    {
                        if (next)
                            sequence++;

                        audiotext = audiotext.Replace("@", AppleManager.totalAppleCollected.ToString());
                        audiotext = audiotext.Replace("&", AppleManager.CollectedFullRedApple.ToString());
                        audiotext = audiotext.Replace("#", mixedWhole[sequence].ToString());
                    }
                    //Debug.Log("fileNameActivity: " + fileName);
                    if (!System.IO.File.Exists(fileName))
                    {
                        //Debug.Log("File does NOT exist   file path = " + fileName);
                        //Debug.Log("Activityaudiotext: " + audiotext);
                        ////sagar Updated :-11-7-19
                        //if (stry_data.Activity.list[i].speaker.Equals("avatar"))
                        //{
                        //    isAvatar = true;
                        //}
                        //else
                        //{
                        //    isAvatar = false;
                        //}
                        //tts.setSpeedPitch(0.85f, 1.0f, isAvatar);
                        //tts.TextToAudio(audiotext, "", audiofilename);

                        // audio file listing
                        Debug.Log("file Doesnot Exist");
                        audio_files_data data = new audio_files_data();
                        data.file_name = audiofilename;
                        data.file_text = audiotext;
                        enQueueAudio(false, data);
                        //Debug.Log("File does NOT exist   file path = " + fileName);
                        //Debug.Log("Activityaudiotext: " + audiotext);
                        ////sagar Updated :-11-7-19
                        //if (stry_data.Activity.list[i].speaker.Equals("avatar"))
                        //{
                        //    isAvatar = true;
                        //}
                        //else
                        //{
                        //    isAvatar = false;
                        //}
                        //if (stry_data.Activity.list[i].Value_Count.Equals("0"))
                        //{
                        //    isEvertTime = false;
                        //}
                        //else
                        //{
                        //    isEvertTime = true;
                        //}
                        //tts.setSpeedPitch(0.85f, 1.0f, isAvatar, isEvertTime, voice);
                        //tts.TextToAudio(audiotext, "", audiofilename);
                    }
                    else
                    {
                        Debug.LogError("already present");
                    }
                }
            }
        }
        //}
        //else
        //{
        //    Debug.Log("nothing is loded");
        //}

    }

    public string getaudio_name(string art_fact_name)
    {
        string abc = "";
        gameDataFileName = "addition of fractions.json";

        path = Path.Combine(Application.streamingAssetsPath, gameDataFileName);

        Debug.Log("path data = " + path);
        if (Application.platform == RuntimePlatform.Android)
        {
            // Android
            string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, gameDataFileName);

            // Android only use WWW to read file
            WWW reader = new WWW(filePath);
            while (!reader.isDone) { }
            // Read the json from the file into a string
            string dataAsJson = reader.text;
            Debug.Log("dataAsJson: " + dataAsJson);
            // Pass the json to JsonUtility, and tell it to create a GameData object from it
            stry_data = JsonUtility.FromJson<timeline_json_subdata>(dataAsJson);
            Debug.Log(stry_data.Activity.list[0].artifacts);


        }
        else
        {
            if (File.Exists(path))
            {
                // Read the json from the file into a string
                dataAsJson = File.ReadAllText(path);

                Debug.Log(dataAsJson);
                stry_data = JsonUtility.FromJson<timeline_json_subdata>(dataAsJson);

                //dep = JsonUtility.FromJson<data1>(dataAsJson);


                //alldata.Department.RemoveAt(alldata.Department.Count-1);

            }
        }


        for (int i = 0; i < stry_data.Introduction.list.Count; i++)
        {
            if (stry_data.Introduction.list[i].artifacts.Equals(art_fact_name))
            {
                abc = stry_data.Introduction.list[i].audio;
                return stry_data.Introduction.list[i].audio;
                break;
            }
        }
        for (int i = 0; i < stry_data.Explanation.list.Count; i++)
        {
            if (stry_data.Explanation.list[i].artifacts.Equals(art_fact_name))
            {
                abc = stry_data.Explanation.list[i].audio;
                return stry_data.Explanation.list[i].audio;
                break;
            }
        }
        for (int i = 0; i < stry_data.Activity.list.Count; i++)
        {
            if (stry_data.Activity.list[i].artifacts.Equals(art_fact_name))
            {
                abc = stry_data.Activity.list[i].audio;
                return stry_data.Activity.list[i].audio;
                break;
            }
        }


        return abc;
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

    public void loadNextAudio(string msg)
    {
        Debug.LogError("Unity loadNextAudio: " + msg+ "TimeAudioListQueue: "+ TimeAudioListQueue.Count);
        if(TimeAudioListQueue.Count > 0)
        {
            //Debug.LogError("inside TimeAudioListQueue");
            CreateAudio();
        }
        else
        {
            Debug.LogError("inside else of timeAudio");
            //_create_runtime_timeline.startPlayingTimeLine();
            if (OnTimeLineRun != null)
                OnTimeLineRun();
        }
    }

    public void CreateAudio()
    {
        audio_files_data data;
        if(ResponceAudioListQueue.Count > 0)
        {
            Debug.LogError("responce");
            data = deQueueAudio(true);
        }
        else
        {
            //if(_create_runtime_timeline.AudioCount > 0)
            //{
            //    _create_runtime_timeline.AudioCount--;

            //}
            Debug.LogError("timeline");
            data = deQueueAudio(false);
        }
        Debug.LogError("data:"+ data.file_name);
        tts.setSpeedPitch(0.85f, 1.0f, isAvatar, isEvertTime, voice);
        tts.TextToAudio(data.file_text, "", data.file_name);
    }

}

