
using System.Collections.Generic;

//[System.Serializable]
//public class IncomingMsgFronReact
//{
//    // Start is called before the first frame update
//    public string current_activity;
//    public int current_activity_status;
//    public int pre_req_id;
//    public string pre_req_reqData;
//    public int pre_req_status;
//    public string user_data;
//    public string asset_bundle_address;
//    public int from_id;
//    public int obj_id;
//    public string objective_name;
//    public int subtopic_id;
//    public string topic_id;
//    public int user_id;
//}
[System.Serializable]
public class SampleMsg
{
    public string objective_name;
    public string asset_bundle_address;
}

//[System.Serializable]
//public class OutGoingMsgFronReact
//{
//    // Start is called before the first frame update
//    public string current_activity;
//    public int current_activity_status;
//    public string current_activity_close_status;
//    public int pre_req_id;
//    public string pre_req_reqData;
//    public int pre_req_status;
//    public string user_data;
//    public string asset_bundle_address;
//    public int from_id;
//    public int obj_id;
//    public string objective_name;
//    public int subtopic_id;
//    public string topic_id;
//    public int user_id;
//}

[System.Serializable]
public class user_data
{

}


// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
[System.Serializable]
public class Summary
{
    public string Right_Answers;
    public string Wrong_Answers;
    public string Un_Answers;
}
[System.Serializable]
public class GoToNextQuestion
{
    public bool Continue;
    public string Comment;
}
[System.Serializable]
public class Timeline
{
    public string topic_id;
    public string topic_name;
    public string grade_id;
    public string timeline_json;
}
[System.Serializable]
public class Question
{
    public string start_point;
    public string session_tlcd_id;
    public object tlcd_qlm_id;
    public object tlcd_stud_id;
    public object tlcd_qtype_id;
    public object tlcd_diff_level;
    public object tlcd_qtype_sys_gen_json;
    public object tlcd_status;
    public object tlcd_by_which_solution;
    public object tlcd_diff_level_json;
    public string tlcd_created_date;
    public string session_tlcdpr_id;
    public string session_parent_pre_requiste_child_id;
    public string noq_with_step_name_flag;
    public string noq_with_step_name_comment;
}
[System.Serializable]
public class McqResultData
{
    public Summary Summary;
    public GoToNextQuestion go_to_next_question;
    public Timeline Timeline;
    public bool Success;
    public Question Question;
}


[System.Serializable]
public class PreReqReqData 
{
    public int error_obj_id;
}

[System.Serializable]
public class UserData
{
    public int user_id;
    public int topic_id;
    public int subtopic_id;
    public int obj_id;
    public string objective_name;
    public string asset_bundle_address;
    public int from_id;
    public List<McqResultData> mcq_result_data;
}
[System.Serializable]
public class NewIncomingMsg
{
    public string current_activity;
    public int current_activity_status;//1
    public int pre_req_id;//id of obj
    public int pre_req_status;//1
    public PreReqReqData pre_req_reqData;//data that i need
    public UserData user_data;
}

[System.Serializable]
public class NewOutGoingMsg: NewIncomingMsg
{
    public string current_activity_close_status;//false
}

