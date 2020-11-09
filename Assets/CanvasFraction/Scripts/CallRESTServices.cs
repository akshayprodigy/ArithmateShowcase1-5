using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
//using JasonTest;
using SimpleJSON;
using System;

public class CallRESTServices : MonoBehaviour
{

    // Use this for initialization

    public delegate void QuestionDownloaded(JSONNode question);
    public static event QuestionDownloaded OnQuestionDownloaded;

    public delegate void LoginSuccssfull(bool status);
    public static event LoginSuccssfull OnLoginSuccssfull;
    string url;
    string testUrl = "";
    string server = "https://arithmate.com/api/v1";
    string mcqResult = "/mcq_result", login = "/login", forward = "/forward-backward", demo = "/demo",data_log = "/demo_logs", token_log = "/token_logs", registration_log = "/registration_logs";
    string email = "prodigy.akshay@gmail.com", password = "123456", token = "";
    int subject_id, topic_id, subtopic_id, grade_id, without_mcq, start_point, session_tlcd_id, tlcd_qlm_id, tlcd_stud_id, tlcd_qtype_id, tlcd_diff_level, tlcd_status, session_tlcdpr_id;
    bool noq_with_step_name_flag;
    string jsontest = "";
    string tlcd_by_which_solution, solution = "Solution-";
    int solution_No;
    public bool test = true;
    //bool timelineCanvas;
    bool nextQuestionSuccess;
    string comment;
    JSONNode questionData, tlcd_diff_level_json;
    string studJson, solJson;
    //public McqResult result;
    public delegate void ShowMessage(string msg);
    public static event ShowMessage OnShowMessage;

    private void OnEnable()
    {
        //CanvasManager.OnNextQuestion += DownloadQuestion;
        MainGridLayoutManager.OndownloadNextQuestion += DownloadQuestion;
        LoginSceneManager.OnDemoLogin += DemoLogIn;
        LoginManager.OnDemoRegistration += registartionDemo;
        //Diagnostic_Manager.OnDemoLogin += DemoLogIn;
        DialogControllerScript.OnCloseDialog += closeMessageLoadScene;
        Obj16_SceneManager.onLogMessage += SendDebugDataToServer;
        ObjectMove.onLogMessage += SendDebugDataToServer;
        QuestionManager_obj16.onLogMessage += SendDebugDataToServer;
        Obj16_RO_Manager.onLogMessage += SendDebugDataToServer;
        AplleObj4ROManager.onLogMessage += SendDebugDataToServer;
        Obj15_RO_Manager.onLogMessage += SendDebugDataToServer;
        //CanvasManager.onLogMessage += SendDebugDataToServer;
        LoginSceneManager.onLogMessage += SendDebugDataToServer;
        GameManager1.onLogMessage += SendDebugDataToServer;
        ObjectMove.onLogMessage += SendDebugDataToServer;
        MainGridLayoutManager.onLogMessage += SendDebugDataToServer;
        GameManager.onLogMessage += SendDebugDataToServer;
        FlashCardManager.onLogMessage += SendDebugDataToServer;
        mcqfeedbackCanvasManager.onLogMessage += SendFeedBackDataToServer;
    }

    private void OnDisable()
    {
        //CanvasManager.OnNextQuestion -= DownloadQuestion;
        MainGridLayoutManager.OndownloadNextQuestion -= DownloadQuestion;
        LoginSceneManager.OnDemoLogin -= DemoLogIn;
        //Diagnostic_Manager.OnDemoLogin -= DemoLogIn;
        LoginManager.OnDemoRegistration -= registartionDemo;
        DialogControllerScript.OnCloseDialog -= closeMessageLoadScene;
        Obj16_SceneManager.onLogMessage -= SendDebugDataToServer;
        ObjectMove.onLogMessage -= SendDebugDataToServer;
        QuestionManager_obj16.onLogMessage -= SendDebugDataToServer;
        Obj16_RO_Manager.onLogMessage -= SendDebugDataToServer;
        AplleObj4ROManager.onLogMessage -= SendDebugDataToServer;
        Obj15_RO_Manager.onLogMessage -= SendDebugDataToServer;
        //CanvasManager.onLogMessage -= SendDebugDataToServer;
        LoginSceneManager.onLogMessage -= SendDebugDataToServer;
        ObjectMove.onLogMessage -= SendDebugDataToServer;
        MainGridLayoutManager.onLogMessage -= SendDebugDataToServer;
        GameManager.onLogMessage -= SendDebugDataToServer;
        FlashCardManager.onLogMessage -= SendDebugDataToServer;
        mcqfeedbackCanvasManager.onLogMessage -= SendFeedBackDataToServer;
    }
    void Start()
    {
        if (test)
        {
            // StartCoroutine("Login");
            token = PlayerPrefs.GetString("token_id");//"045da4cc7b94b82f14f18d3f92431a84";
            subject_id = 9;
            topic_id = 6;
            subtopic_id = 107;
            grade_id = 4;
            //loadjsonfromResource();
            //StartCoroutine(loadMCQResult(subject_id, topic_id, subtopic_id, grade_id));
            // StartCoroutine(loadMCQResult(subject_id, topic_id, subtopic_id, grade_id));
            //string res = 

        }
        else
        {
            token = PlayerPrefs.GetString("token_id");
            //Debug.Log("token: " + token);
            subject_id = PlayerPrefs.GetInt("subject_id");
            topic_id = PlayerPrefs.GetInt("topic_id");
            subtopic_id = PlayerPrefs.GetInt("subtopic_id");
            grade_id = PlayerPrefs.GetInt("grade_id");
            string res = PlayerPrefs.GetString("responce");
            //Debug.Log("question" + res);
            JSONNode responce = JSON.Parse(res);
            //Debug.Log("responce: " + responce.ToString());
            int from_submit = PlayerPrefs.GetInt("from_submit");
            //Debug.Log("from_submit: " + from_submit);
            if (from_submit == 1)
                showQuestionfromJson(responce);
            else
                showQuestionfromJsonsubmit(responce);
            //StartCoroutine(loadMCQResult(subject_id, topic_id, subtopic_id, grade_id));
        }
    }

    void showQuestionfromJsonsubmit(JSONNode responce)
    {
        bool success = responce["success"].AsBool;
        //Debug.Log("success" + success);
        int i = 0;
        //Debug.Log("Responce from submit" + responce);
        foreach (JSONNode data in responce["data"])
        {
            switch (i)
            {/*
                case 0:
                    int Right_Answers = data["Summary"]["Right_Answers"].AsInt;
                    int Wrong_Answers = data["Summary"]["Wrong_Answers"].AsInt;
                    int Un_Answers = data["Summary"]["Un_Answers"].AsInt;
                    Debug.Log("Summary Right_Answers:" + Right_Answers + " Wrong_Answers: " + Wrong_Answers + " Un_Answers: " + Un_Answers);
                    break;*/
                case 0:
                    bool nextQuestionSuccess = data["go_to_next_question"]["Continue"].AsBool;
                    string comment = data["go_to_next_question"]["Comment"].Value;
                    //Debug.Log("nextQuestionSuccess: Continue " + nextQuestionSuccess + " comment: " + comment);
                    break;
                case 1:
                    //timelineCanvas = data["Timeline"]["Canvas"]["flag"].AsBool;
                    name = data["Timeline"]["topic_name"].Value;
                    UtilityREST.topic_name = name;
                    //Debug.Log("timelineCanvas: " + timelineCanvas + " name: " + name);
                    //Debug.Log("timelineCanvas: " + timelineCanvas);
                    break;
                case 3:
                    //Debug.Log("3data: " + data.ToString());
                    getTheQuestion(data);
                    break;
                case 2:
                    // get the question;
                    //Debug.Log("2data: " + data.ToString());
                    //getTheQuestion(data);

                    break;
                default:
                    break;
            }
            i++;


        }

        //if (timelineCanvas)
        {
            if (OnQuestionDownloaded != null)
                OnQuestionDownloaded(questionData);
        }
    }

    void getTheQuestion(JSONNode data)
    {
        Debug.Log("getTheQuestion data:" + data.ToString());
        //"noq_with_step_name_flag" : "true/false"
        start_point = data["Question"]["start_point"].AsInt;
        session_tlcd_id = data["Question"]["session_tlcd_id"].AsInt;
        session_tlcdpr_id = data["Question"]["session_tlcdpr_id"].AsInt;
        tlcd_qlm_id = data["Question"]["tlcd_qlm_id"].AsInt;
        noq_with_step_name_flag = data["Question"]["noq_with_step_name_flag"].AsBool;
        // Debug.Log("noq_with_step_name_flag: " + noq_with_step_name_flag);
        UtilityREST.print_step_name = true;//noq_with_step_name_flag;
        if (session_tlcdpr_id != 0)
        {
            //Debug.Log("its a pre requisite session_tlcdpr_id"+ session_tlcdpr_id);
            UtilityREST.isprerequisit = true;
        }
        else
        {
            //Debug.Log("its not a pre requisite session_tlcdpr_id" + session_tlcdpr_id);
            UtilityREST.isprerequisit = false;
        }
        tlcd_stud_id = data["Question"]["tlcd_stud_id"].AsInt;
        tlcd_qtype_id = data["Question"]["tlcd_qtype_id"].AsInt;
        tlcd_diff_level = data["Question"]["tlcd_diff_level"].AsInt;
        tlcd_status = data["Question"]["tlcd_status"].AsInt;
        tlcd_by_which_solution = data["Question"]["tlcd_by_which_solution"].Value;
        Debug.Log("get question tlcd_by_which_solution: " + tlcd_by_which_solution + "solution.Length: " + solution.Length);//is null for addition to fraction
        if (tlcd_by_which_solution == "")
        {
            solution_No = 1;
        }
        else
        {
            tlcd_by_which_solution = tlcd_by_which_solution.Substring(solution.Length);
            solution_No = int.Parse(tlcd_by_which_solution);
        }
        
        solution_No--;

        // Debug.Log("tlcd_by_which_solution: " + tlcd_by_which_solution+ " solution_No:"+ solution_No);
        questionData = data["Question"]["tlcd_qtype_sys_gen_json"];

        tlcd_diff_level_json = data["Question"]["tlcd_diff_level_json"];
        //Debug.LogError("nextQuestionSuccess: questionData " + questionData.ToString());
        UtilityREST.QuestionData = questionData.ToString();
        Debug.Log("UtilityREST.QuestionData : " + UtilityREST.QuestionData);
        //Debug.Log("tlcd_diff_level_json: " + tlcd_diff_level_json.ToString());
        solJson = questionData["Solutions"].ToString();
        //Debug.Log("nextQuestionSuccess: questionData " + questionData.ToString());
        foreach (JSONNode solndata in questionData["Solutions"])
        {
            //Debug.Log("solndata: " + solndata.ToString());
        }
        //Debug.Log("Question: session_tlcdpr_id " + session_tlcdpr_id + " session_tlcd_id: " + session_tlcd_id);
        UtilityREST.tlcd_id = session_tlcd_id.ToString();
        UtilityREST.solution_No = solution_No;
        UtilityREST.solJson = solJson;
        UtilityREST.tlcd_diff_level_json = tlcd_diff_level_json.ToString();
        UtilityREST.studJson = questionData.ToString();
        UtilityREST.session_tlcd_id = session_tlcd_id.ToString();
        UtilityREST.session_tlcdpr_id = session_tlcdpr_id.ToString();
        if (string.Equals(UtilityREST.session_tlcdpr_id, "0"))
            UtilityREST.session_tlcdpr_id = "";

        Debug.LogError("tlcd_id: " + UtilityREST.tlcd_id + " tlcd_qtype_id: "+ tlcd_qtype_id);
        Debug.Log("UtilityREST.solJson" +  UtilityREST.solJson);

        // testing for force qtype id 215,217

        if(tlcd_qtype_id==215|| tlcd_qtype_id == 217)
        {
            UtilityArtifacts.isobj16 = true;
        }
        else
        {
            UtilityArtifacts.isobj16 = false;
        }


        //Debug.LogError(" UtilityREST.session_tlcd_id: " + UtilityREST.session_tlcd_id + " UtilityREST.tlcd_id: "+ UtilityREST.tlcd_id+ " UtilityREST.session_tlcdpr_id: "+ UtilityREST.session_tlcdpr_id);
        // need to check if pre requiste or not


        //PlayerPrefs.SetString("session_tlcd_id", session_tlcd_id.ToString());
        //PlayerPrefs.SetString("tlcd_by_which_solution", tlcd_by_which_solution);
        //PlayerPrefs.SetString("solJson", solJson);
        //Debug.Log("session_tlcd_id: " + session_tlcd_id);
        //Debug.Log("tlcd_diff_level_json: " + tlcd_diff_level_json);
        //PlayerPrefs.SetString("tlcd_diff_level_json", tlcd_diff_level_json.ToString());
        //PlayerPrefs.Save();
        //tlcd_by_which_solution = "Solution" + tlcd_by_which_solution;
        /*
        studJson = questionData.ToString();
        Debug.Log("solJson: " + solJson.ToString());
        Debug.Log("studJson: " + studJson.ToString());
        Debug.Log("answer data from Question:");
        Debug.Log("token:" + token);
        Debug.Log("grade_id" + grade_id);
        Debug.Log("session_tlcd_id" + session_tlcd_id);
        //Debug.Log("status" + status);
        Debug.Log("studJson" + studJson);
        Debug.Log("tlcd_by_which_solution:" + tlcd_by_which_solution);
       // Debug.Log("prerequiste:" + prerequiste);
       // Debug.Log("prerequiste_status:" + prerequiste_status);*/

    }

    void showQuestionfromJson(JSONNode responce)
    {
        bool success = responce["success"].AsBool;
        //Debug.Log("success" + success);
        int i = 0;
        foreach (JSONNode data in responce["data"])
        {
            switch (i)
            {
                case 0:
                    int Right_Answers = data["Summary"]["Right_Answers"].AsInt;
                    int Wrong_Answers = data["Summary"]["Wrong_Answers"].AsInt;
                    int Un_Answers = data["Summary"]["Un_Answers"].AsInt;
                    //Debug.Log("Summary Right_Answers:" + Right_Answers + " Wrong_Answers: " + Wrong_Answers + " Un_Answers: " + Un_Answers);
                    break;
                case 1:
                    bool nextQuestionSuccess = data["go_to_next_question"]["Continue"].AsBool;
                    string comment = data["go_to_next_question"]["Comment"].Value;
                    //Debug.Log("nextQuestionSuccess: Continue " + nextQuestionSuccess + " comment: " + comment);
                    break;
                case 2:
                    //timelineCanvas = data["Timeline"]["Canvas"]["flag"].AsBool;
                    name = data["Timeline"]["topic_name"].Value;
                    UtilityREST.topic_name = name;
                    //Debug.Log("timelineCanvas: " + timelineCanvas + " name: " + name);
                    //Debug.Log("timelineCanvas: " + timelineCanvas);
                    break;
                case 3:
                    break;
                case 4:
                    // get the question;
                    getTheQuestion(data);
                    // tlcd_by_which_solution = "Solution" + tlcd_by_which_solution;
                    /*
                    start_point = data["Question"]["start_point"].AsInt;
                    session_tlcd_id = data["Question"]["session_tlcd_id"].AsInt;
                    tlcd_qlm_id = data["Question"]["tlcd_qlm_id"].AsInt;
                    tlcd_stud_id = data["Question"]["tlcd_stud_id"].AsInt;
                    tlcd_qtype_id = data["Question"]["tlcd_qtype_id"].AsInt;
                    tlcd_diff_level = data["Question"]["tlcd_diff_level"].AsInt;
                    tlcd_status = data["Question"]["tlcd_status"].AsInt;
                    tlcd_by_which_solution = data["Question"]["tlcd_by_which_solution"].Value;
                    tlcd_by_which_solution = tlcd_by_which_solution.Substring(solution.Length);
                    Debug.Log("tlcd_by_which_solution: " + tlcd_by_which_solution);
                    questionData = data["Question"]["tlcd_qtype_sys_gen_json"];
                    tlcd_diff_level_json = data["Question"]["tlcd_diff_level_json"];
                    Debug.Log("nextQuestionSuccess: questionData " + questionData.ToString());
                    solJson = questionData["Solutions"].ToString();
                    foreach (JSONNode solndata in questionData["Solutions"])
                    {
                        Debug.Log("solndata: " + solndata.ToString());
                    }
                    PlayerPrefs.SetString("tlcd_by_which_solution", tlcd_by_which_solution);
                    PlayerPrefs.SetString("solJson", solJson);
                    PlayerPrefs.Save();
                    studJson = questionData.ToString();
                    Debug.Log("studJson: " + studJson.ToString());
                    Debug.Log("solJson: " + solJson);
                    */


                    break;
                default:
                    break;
            }
            i++;

            //string msgSpeech = message["speech"].Value;
            // do something
        }
        /*
        start_point = data["start_point"].AsInt;
        session_tlcd_id = data["session_tlcd_id"].AsInt;
        tlcd_qlm_id = data["tlcd_qlm_id"].AsInt;
        tlcd_stud_id = data["tlcd_stud_id"].AsInt;
        tlcd_qtype_id = data["tlcd_qtype_id"].AsInt;
        tlcd_diff_level = data["tlcd_diff_level"].AsInt;
        tlcd_status = data["tlcd_status"].AsInt;
        tlcd_by_which_solution = data["tlcd_by_which_solution"].Value;
        questionData = data["tlcd_qtype_sys_gen_json"];
        tlcd_diff_level_json = data["tlcd_diff_level_json"];
        Debug.Log("nextQuestionSuccess: questionData " + questionData.ToString());
        solJson = questionData["Solutions"].Value;
        studJson = questionData.ToString();
        Debug.Log("studJson: " + studJson.ToString());*/
        //if (timelineCanvas)
        {
            if (OnQuestionDownloaded != null)
                OnQuestionDownloaded(questionData);

        }
    }

    void DownloadQuestion()
    {
        // Debug.Log("studJson: " + studJson.ToString());
        StartCoroutine(loadForwardResult());
    }

    void DemoLogIn()
    {
        //Debug.Log("login demo called");
        //if (LoginSceneManager.Instance.isEquivalent)
        //{
        //    subject_id = 9;
        //    topic_id = 6;
        //    subtopic_id = 15;
        //    grade_id = 5;
        //    without_mcq = 1;
        //    token = "045da4cc7b94b82f14f18d3f92431a84";

        //    StartCoroutine(loadMCQResult(subject_id, topic_id, subtopic_id, grade_id, without_mcq));
        //    //UtilityREST.QuestionData = questionData.ToString();
        //}
        //else
        //{
        StartCoroutine(LogInDemo());
        //}
    }

    void SendDebugDataToServer(string msg)
    {
        //Debug.Log("msg: " + msg);
        //DateTime dt = DateTime.Now;
        //string message = dt.ToLongDateString() + " " + dt.ToLongTimeString() + " msg: " + msg;
        if(this!=null)
        StartCoroutine(LoadDataToServer(msg));
    }

    void SendFeedBackDataToServer(string msg)
    {
        if (this != null)
            StartCoroutine(LoadTokenDataToServer(msg));
    }

    void loadjsonfromResource()
    {
        TextAsset targetFile = Resources.Load<TextAsset>("McqResult");
        //Debug.Log("file: " + targetFile.text);
        //result = JsonUtility.FromJson<McqResult>(targetFile.text);
        // Debug.Log("result: " + result.success);
    }

    void registartionDemo()
    {
        StartCoroutine(RegistrationDemo());
    }
    IEnumerator RegistrationDemo()
    {
        WWWForm form = new WWWForm();
        form.AddField("user_name", PlayerPrefs.GetString(UtilityArtifacts.UserName,""));//subtopic_id
        form.AddField("user_mobile_number", PlayerPrefs.GetString(UtilityArtifacts.UserPhoneNumber, ""));//qtype_id
        form.AddField("user_type", PlayerPrefs.GetString(UtilityArtifacts.UserType, ""));//subtopic_id
        form.AddField("user_grade", PlayerPrefs.GetString(UtilityArtifacts.UserGrade, ""));//qtype_id
        form.AddField("user_city", PlayerPrefs.GetString(UtilityArtifacts.UserCity, ""));//subtopic_id
        form.AddField("user_password", PlayerPrefs.GetString(UtilityArtifacts.UserPassword, ""));//qtype_id
        form.AddField("user_confirm_password", PlayerPrefs.GetString(UtilityArtifacts.UserPassword, ""));//subtopic_id
        url = server + registration_log;
        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.Send();
        if (www.error != null)
        {
            Debug.Log("Error is " + www.error);

        }
        else
        {
            JSONNode responce = JSON.Parse(www.downloadHandler.text);
            bool success = responce["success"].AsBool;
            LoginManager.Instance.UserLogin();

        }

    }
    IEnumerator LogInDemo()
    {
        WWWForm form = new WWWForm();
        if (UtilityREST.without_objective != 0)
        {
            //objective_id
            form.AddField("objective_id", UtilityREST.without_objective.ToString());
            form.AddField("subtopic_id", UtilityREST.subtopic_id.ToString());
            if (!string.Equals(UtilityREST.qType_id, ""))
            {
                Debug.Log("qType_id: " + UtilityREST.qType_id);
                form.AddField("qtype_id", UtilityREST.qType_id);
            }
                

        }
        //form.AddField("without_objective", UtilityREST.without_objective);
        url = server + demo;
        UnityWebRequest www = UnityWebRequest.Post(url, form);
        www.SetRequestHeader("AUTHORIZATION", token);

        bool status = false;
        yield return www.Send();
        if (www.error != null)
        {
            //Debug.Log("Error is " + www.error);

        }
        else
        {
          
            JSONNode responce = JSON.Parse(www.downloadHandler.text);
            Debug.Log("responce: " + responce.ToString());
            bool success = responce["success"].AsBool;
            PlayerPrefs.SetString("responce", responce.ToString());
            //Debug.Log(" LogInDemo success : " + success);
            PlayerPrefs.SetInt("from_submit", 0);
            string token = PlayerPrefs.GetString(UtilityREST.token_name);

            if (success)
            {
                //    if (string.Equals(token, ""))
                //    {
                UtilityREST.token = responce["token"].Value;
                PlayerPrefs.SetString(UtilityREST.token_name, token);
                PlayerPrefs.SetString("token_id", token);
                UtilityREST.user_id = responce["user_id"].Value;
                PlayerPrefs.SetInt("from_submit", 1);
                UtilityREST.msgHead = "start";
                if(UtilityArtifacts.logInScene == UtilityArtifacts.CanvasSceneNumber)
                {
                    SendDebugDataToServer("'Practice Canvas’ Session begins");
                }else if(UtilityArtifacts.logInScene == 7)
                {
                    SendDebugDataToServer("'Active Experimentation’ Session begins");
                }else
                    SendDebugDataToServer("User starts the Learning Session with ‘Concrete Experience’");
                //}
                status = true;
                //Debug.Log("success" + success + " responce: " + responce.ToString());
                int i = 0;
                foreach (JSONNode data in responce["data"])
                {
                    switch (i)
                    {
                        case 0:
                            int Right_Answers = data["Summary"]["Right_Answers"].AsInt;
                            int Wrong_Answers = data["Summary"]["Wrong_Answers"].AsInt;
                            int Un_Answers = data["Summary"]["Un_Answers"].AsInt;
                            //Debug.Log("Summary Right_Answers:" + Right_Answers + " Wrong_Answers: " + Wrong_Answers + " Un_Answers: " + Un_Answers);
                            break;
                        case 1:
                            bool nextQuestionSuccess = data["go_to_next_question"]["Continue"].AsBool;
                            string comment = data["go_to_next_question"]["Comment"].Value;
                            //Debug.Log("nextQuestionSuccess: Continue " + nextQuestionSuccess + " comment: " + comment);
                            break;
                        case 2:
                            //timelineCanvas = data["Timeline"]["Canvas"]["flag"].AsBool;
                            name = data["Timeline"]["topic_name"].Value;
                            UtilityREST.topic_name = name;
                            //Debug.Log("timelineCanvas: " + timelineCanvas + " name: " + name);
                            //Debug.Log("timelineCanvas: " + timelineCanvas);
                            break;
                        case 3:
                            break;
                        case 4:
                            // get the question;
                            getTheQuestion(data);

                            break;
                        default:
                            break;
                    }
                    i++;

                    //string msgSpeech = message["speech"].Value;
                    // do something
                }
            }
            PlayerPrefs.Save();
        }
        if (OnLoginSuccssfull != null)
            OnLoginSuccssfull(status);
    }

    IEnumerator Login()
    {
        WWWForm form = new WWWForm();
        //Debug.Log("email:" + email + " password:" + password);
        form.AddField("email", email);
        form.AddField("password", password);

        url = server + login;
        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.Send();

        if (www.error != null)
        {
            // Debug.Log("Error is " + www.error);
        }
        else
        {
            //Debug.Log("Responce " + www.downloadHandler.text);

            //loginDetail LoginDetail = JsonUtility.FromJson<loginDetail>(www.downloadHandler.text);
            ////Debug.Log("message " + LoginDetail.data.user_name.ToString());
            //if (LoginDetail.success == true)
            //{
            //    //Debug.Log("login successful");
            //    /*
            //    //reg_message.text = UserDetail.data.message.ToString();
            //    foreach (GameObject p1 in panels)
            //    {
            //        p1.SetActive(false);
            //    }
            //    panels[2].SetActive(true);
            //    //StartCoroutine(LoadSubject());
            //    dashboard_user_name.text = LoginDetail.data.user_name.ToString();
            //    ////Grade_Id = LoginDetail.data.user_standard;

            //    //StartCoroutine(loadTopic(Sub_Id,Grade_Id));
            //    reg_message.text = "";
            //    clear_fields();
            //    */
            //}
            //else
            //{

            //}
        }
    }

    IEnumerator loadMCQResult(int Sub_Id, int Topic_Id, int SubTopic_Id, int Grade_Id, int without_mcq)
    {

        WWWForm form = new WWWForm();
        form.AddField("subject_id", Sub_Id);
        form.AddField("topic_id", Topic_Id);
        form.AddField("subtopic_id", SubTopic_Id);
        form.AddField("grade_id", Grade_Id);
        form.AddField("without_mcq", without_mcq);
        url = server + mcqResult;

        UnityWebRequest www = UnityWebRequest.Post(url, form);
        Debug.Log("token load loadMCQResult : " + token);
        www.SetRequestHeader("AUTHORIZATION", token);
        //Debug.Log("token MCQ Result: " + token);
        bool status = false;
        yield return www.Send();

        if (www.error != null)
        {
            //Debug.Log("Error is " + www.error);

        }
        else
        {
            //Debug.Log("Responce " + www.downloadHandler.text);
            JSONNode responce = JSON.Parse(www.downloadHandler.text);
            bool success = responce["success"].AsBool;
            //Debug.Log("success" + success);
            status = true;
            int i = 0;
            foreach (JSONNode data in responce["data"])
            {
                switch (i)
                {
                    case 0:
                        int Right_Answers = data["Summary"]["Right_Answers"].AsInt;
                        int Wrong_Answers = data["Summary"]["Wrong_Answers"].AsInt;
                        int Un_Answers = data["Summary"]["Un_Answers"].AsInt;
                        Debug.Log("Summary Right_Answers:" + Right_Answers + " Wrong_Answers: " + Wrong_Answers + " Un_Answers: " + Un_Answers);
                        break;
                    case 1:
                        bool nextQuestionSuccess = data["go_to_next_question"]["Continue"].AsBool;
                        string comment = data["go_to_next_question"]["Comment"].Value;
                        Debug.Log("nextQuestionSuccess: Continue " + nextQuestionSuccess + " comment: " + comment);
                        break;
                    case 2:
                        //timelineCanvas = data["Timeline"]["Canvas"]["flag"].AsBool;
                        name = data["Timeline"]["topic_name"].Value;
                        UtilityREST.topic_name = name;
                        Debug.Log(" name: " + name);
                        //Debug.Log("timelineCanvas: " + timelineCanvas);
                        break;
                    case 3:
                        break;
                    case 4:
                        // get the question;
                        getTheQuestion(data);

                        break;
                    default:
                        break;
                }
                i++;

                //string msgSpeech = message["speech"].Value;
                // do something
            }
            //if (timelineCanvas)
            {
                if (OnQuestionDownloaded != null)
                    OnQuestionDownloaded(questionData);
            }

            if (LoginSceneManager.Instance.isEquivalent)
            {
                if (OnLoginSuccssfull != null)
                    OnLoginSuccssfull(status);
            }

        }
    }

    IEnumerator loadForwardResult()
    {
        Debug.LogError(" tlcd_id: " + UtilityREST.tlcd_id + " session_tlcd_id: " + UtilityREST.session_tlcd_id + " session_tlcdpr_id: " + UtilityREST.session_tlcdpr_id);
        Debug.LogError(" UtilityREST.status: " + UtilityREST.status + " UtilityREST.prerequiste: " + UtilityREST.prerequiste + " UtilityREST.prerequiste_status: " + UtilityREST.prerequiste_status);
        WWWForm form = new WWWForm();
        form.AddField("grade_id", grade_id);//4
        form.AddField("tlcd_id", UtilityREST.tlcd_id);//1452 
        form.AddField("status", UtilityREST.status);//1
        form.AddField("stud_json", UtilityREST.studJson);//
        form.AddField("relevant_solution", UtilityREST.solution_No);//0
        form.AddField("prerequiste", UtilityREST.prerequiste);//103
        form.AddField("prerequiste_status", UtilityREST.prerequiste_status);//1
        form.AddField("session_tlcd_id", UtilityREST.session_tlcd_id);//blank
        form.AddField("session_parent_pre_requiste_child_id", "");//blank
        form.AddField("session_tlcdpr_id", UtilityREST.session_tlcdpr_id);//blank  


        url = server + forward;
        Debug.Log("token load forward : " + UtilityREST.token);
        UnityWebRequest www = UnityWebRequest.Post(url, form);
        www.SetRequestHeader("AUTHORIZATION", UtilityREST.token);


        yield return www.Send();

        if (www.error != null)
        {
            Debug.Log("Error is " + www.error);

        }
        else
        {
            //Debug.Log("Responce " + www.downloadHandler.text);
            if (www.downloadHandler.text == null)
                Debug.Log("is null");
            JSONNode responce = JSON.Parse(www.downloadHandler.text);
            bool success = responce["success"].AsBool;
            PlayerPrefs.SetString("responce", responce.ToString());
            PlayerPrefs.SetInt("from_submit", 0);
            PlayerPrefs.Save();
            Debug.Log("success" + success + " responce: " + responce.ToString());
            string name = "";
            int topicId = 0;
            int i = 0;
            if (success)
            {
                foreach (JSONNode data in responce["data"])
                {
                    switch (i)
                    {
                        case 0:
                            nextQuestionSuccess = data["go_to_next_question"]["Continue"].AsBool;// sometime next Question Success is coming null
                            comment = data["go_to_next_question"]["Comment"].Value;
                            //Debug.Log("nextQuestionSuccess: Continue " + nextQuestionSuccess + " comment: " + comment);
                            break;
                        case 1:
                            //timelineCanvas = data["Timeline"]["timeline_json"]["Canvas"]["flag"].AsBool;
                            name = data["Timeline"]["topic_name"].Value;
                            //topicId = data["Timeline"]["topic_id"].AsInt;
                            UtilityREST.topic_name = name;
                            UtilityREST.timeline_json = data["Timeline"]["timeline_json"].ToString();
                            //if (!nextQuestionSuccess)
                            //{
                            //    Debug.LogError("data: " + data["Timeline"]["topic_id"].Value);
                            //}
                            //Debug.Log("timelineCanvas: " + timelineCanvas+ " name: "+ name);
                            break;
                        case 2:
                            break;
                        case 3:
                            // get the question;
                            if(UtilityREST.topic_name != "Not Found")
                                getTheQuestion(data);
                            break;
                        default:
                            break;
                    }
                    i++;
                }
            }
            Debug.Log("tlcd_qtype_id: " + tlcd_qtype_id);
            if (nextQuestionSuccess&&(tlcd_qtype_id == 215 || tlcd_qtype_id == 230))
            {
                if (OnQuestionDownloaded != null)
                    OnQuestionDownloaded(questionData);
            }
            else
            {
                // Debug.LogError(" data[]" + data["Timeline"].Value);
                //Debug.LogError("topicId: " + topicId);
                //Debug.LogError("name" + name);
                //if(string.Equals(name, UtilityArtifacts.addition))
                //{
                //    UtilityArtifacts.current_json = UtilityArtifacts.json_addition;
                //}
                //else
                //{
                //    UtilityArtifacts.current_json = UtilityArtifacts.json_addition_of_mixed_fraction;
                //}
                //if(string.Equals(name,""))
                //    UtilityArtifacts.scafoldCanvas = true;
                //else
                //    UtilityArtifacts.scafoldCanvas = false;

                string error = "";
                if (string.Equals(UtilityREST.prerequiste, UtilityArtifacts.multiplicationId) || string.Equals(UtilityREST.prerequiste, UtilityArtifacts.divisionId))
                {
                    error = " Let's learn " + name;

                    SendDebugDataToServer("Traversing the user to ‘Multiplication’ Flash Card");
                    //SendDebugDataToServer("Validating multiplication learning through problems");
                }
                else
                {
                    error = "Let's learn " + name;
                }

                //if (OnShowMessage != null)
                //    OnShowMessage(error);

                // new canvas 17/02/2020
                if (UtilityREST.topic_name != "Not Found")
                    StartCoroutine(LoadYourAsyncScene(UtilityArtifacts.FlashCardSceneNumber));
                else if (UtilityArtifacts.scene_to_load_after_canvas == "obj2")
                    StartCoroutine(LoadYourAsyncScene(5));//0
                else if (UtilityArtifacts.scene_to_load_after_canvas == "obj3")
                    StartCoroutine(LoadYourAsyncScene(6));//0
                else 
                    StartCoroutine(LoadYourAsyncScene(7));//0
                PlayerPrefs.SetString("introduction_title", name);
                PlayerPrefs.Save();
                //loadScene = true;
            }

        }

    }

    IEnumerator LoadDataToServer(string msg)
    {
        //Debug.Log("LoadDataToServer: user_id" + UtilityREST.tlcd_id + " msg: " + msg);
        WWWForm form = new WWWForm();
        form.AddField("user_id", UtilityREST.user_id);//4
        form.AddField("logs_data", msg);//1452 
        if(string.Equals( UtilityREST.msgHead, "start"))
        {
            form.AddField("logs_head", "start");
            UtilityREST.msgHead = "";
        }
        else if(string.Equals(UtilityREST.msgHead, "end"))
        {
            form.AddField("logs_head", "end");
            UtilityREST.msgHead = "";
        }
        else
            form.AddField("logs_head", "in-process");
        url = server + data_log;
        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.Send();

        if (www.error != null)
        {
            Debug.Log("LoadDataToServer Error is " + www.error);

        }
        else
        {
            Debug.Log("LoadDataToServer succrsfully" + UtilityREST.user_id);
        }
    }

    IEnumerator LoadTokenDataToServer(string msg)
    {
        WWWForm form = new WWWForm();
        form.AddField("user_id", UtilityREST.user_id);//4
        form.AddField("token_feedback_data", msg);//1452 
        url = server + token_log;
        Debug.Log("url = " + url);
        Debug.Log("user_id = " + UtilityREST.user_id);
        Debug.Log("token_feedback_data" + msg);
        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.Send();

        if (www.error != null)
        {
            Debug.Log("LoadTokenDataToServer Error is " + www.error);

        }
        else
        {
            Debug.Log("LoadTokenDataToServer succrsfully" + UtilityREST.user_id);
        }
    }

    bool loadScene = false;
    void closeMessageLoadScene()
    {
        if (loadScene)
        {
            StartCoroutine(LoadYourAsyncScene(2));
        }
        loadScene = false;

    }

    IEnumerator LoadYourAsyncScene(int sceneNumber)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.
        // 0 for test
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneNumber);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {

            yield return null;
        }


    }

}
/*
[System.Serializable]
public class showResult

{
    public bool success;
    public string data;
   // public List<ResultData>data;
}
public class ResultData

{
    public Summary Summary { get; set; }
    /*
    public GoToNextQuestion go_to_next_question { get; set; }
    public Timeline Timeline { get; set; }
    public bool? Success { get; set; }
    public Question Question { get; set; }
}
[System.Serializable]
public class Summary
{
    public string Right_Answers { get; set; }
    public string Wrong_Answers { get; set; }
    public string Un_Answers { get; set; }
}
[System.Serializable]
public class GoToNextQuestion
{
    public bool Continue { get; set; }
    public string Comment { get; set; }
}
/*
[System.Serializable]
public class TimelineJson
{
    public Explanation Explanation { get; set; }
    public Activity Activity { get; set; }
    public Canvas Canvas { get; set; }
}
*/

