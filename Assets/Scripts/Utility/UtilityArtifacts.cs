
using System.Collections.Generic;

public class UtilityArtifacts
{
    public static string Tag_Rule_Board = "Rule_Board";
    public static string Tag_Apples = "Apples";
    public static string Tag_SceneManager = "SceneManager";
    public static string Tag_Dispenser = "Dispenser";
    public static string Tag_TrayController = "TrayController";
    public static string Tag_BigTray = "BigTray";
    public static string Tag_Applecase = "Applecase";
    public const string Fracton_proper = "proper";
    public const string Fracton_improper = "improper";
    public const string Fraction_mixed = "mixed";
    public const string Fraction_unit = "unit";
    public const string json_story_board = "storyboard";
    public const string json_addition = "mixed_fraction";
    public const string json_addition_of_mixed_fraction = "addition";
    public static string current_json = "";
    public const string addition = "Addition";
    public const string multiplicationId = "103";
    public const string divisionId = "104";
    public const string json_proper_storyboard = "proper_storyboard_json";
    public const string json_improper_storyboard = "improper_storyboard_json";
    public const string UserName = "UserName";
    public const string UserPhoneNumber = "PhoneNumber";
    public const string UserGrade = "UserGrade";
    public const string UserType = "UserType";
    public const string UserPassword = "UserPassword";
    public const string UserCity = "UserCity";
    public const string json_EOF_storyboard = "EOF_storyboard_json";
    public const string json_Equivalent_Concrete_Exp_storyboard = "Equivalent_Concrete_Exp";
    public const string json_eof_dignostic_testing = "storyboard_EOF_DIG";
    public const string json_eof_anstract_conceptualisation = "storyboard_EOF_AC";
    public const string json_eof_active_experiment = "storyboard_EOF_ACT";
    public const string json_eof_visual_q_type = "storyboard_EOF_visual_q_type";
    public const string objective2_visual_qtype = "objective2_visual_qtype";
    public const string test_json = "test_json";
    public const string json_interval_prefix = "interval_";
    public const string json_Equivalent_Fraction_Objective2_ActiveExperiment = "storyboard_eof_obj2_active_experimentation";
    public static string scene_to_load_after_canvas = "";
    public const string json_Equivalent_Fraction_Objective2_AbstractCon = "storyboard_eof_obj2_abstract_conceptualisation";

    public const string json_Equivalent_Fraction_Objective2 = "Equivalent_Fraction_Objective_2_Concrete_Exp";
    public const string what_fraction_looks_like = "what_fraction_looks_like";
    public const string Activity_representation_of_fraction = "Activity_representation_of_fraction";
    public const string ROF_Numerator_denominator = "ROF_Numerator_denominator";
    public const string json_SentenceConnector_storyboard = "SentenceConnector_storyboard_json";
    public static string Tag_source = "source";
    public static string Tag_source1 = "source1";
    public static string Tag_destination = "destination";
    public static List<int> numerators = new List<int>(), denominators = new List<int>(), mixedWhole = new List<int>();

    public static bool scafoldCanvas = false;
    public static int currentScene = 0;
    public static int LastScene = 0;
    public static int SkipScene = 1;
    public static bool isScafold = false;
    public static int logInScene = 0;
    public static int loadStartingpoint;
    public static int loadStartingpointforcomingback;
    public static int loadEndingpoint;
    public static bool backTraversal;
    public static bool comingbackafterTraversal;
    public const string Obj14_ShowingBoard = "Obj14_ShowingBoard";
    public const string Obj14_ShowingCountText = "Obj14_ShowingCountText";
    public const string Obj14_ShowingText = "Obj14_ShowingText";
    public const string Obj14_InputString = "Obj14_InputString";
    public const string Obj14_Choosefraction = "Obj14_Choosefraction";
    public const string Obj14_ChoosefractionFailed = "Obj14_ChoosefractionFailed";
    public static bool Obj14C_isChoiceCorrect = false;
    public static int obj14InputValue = 0;
    public const string Obj14_ShowingCountText_After = "Obj14_ShowingCountText_After";
    public const int CanvasSceneNumber = 2;
    public const int FlashCardSceneNumber = 3;
    public static bool GameQuit = false;
    // json messga from NJ
    public static NewIncomingMsg NJMsg ;
    public static NewOutGoingMsg UnityMsg;

    public static int NJLoadScene = 1;

    //public static OutGoingMsgFronReact outGoingMessage(NewIncomingMsg njMsg,string objStatus)
    //{
    //    OutGoingMsgFronReact outMsg = new OutGoingMsgFronReact();
    //    outMsg.asset_bundle_address = njMsg.user_data.asset_bundle_address;
    //    outMsg.current_activity = njMsg.current_activity;
    //    outMsg.current_activity_status = njMsg.current_activity_status;
    //    outMsg.pre_req_id = njMsg.pre_req_id;
    //    outMsg.pre_req_reqData = njMsg.pre_req_reqData;
    //    outMsg.pre_req_status = njMsg.pre_req_status;
    //    outMsg.user_data = njMsg.user_data;
    //    outMsg.from_id = njMsg.from_id;
    //    outMsg.obj_id = njMsg.obj_id;
    //    outMsg.objective_name = njMsg.objective_name;
    //    outMsg.subtopic_id = njMsg.subtopic_id;
    //    outMsg.topic_id = njMsg.topic_id;
    //    outMsg.user_id = njMsg.user_id;
    //    outMsg.current_activity_close_status = objStatus;
    //    return outMsg;
    //}

    public static NewOutGoingMsg outGoingMessage(NewIncomingMsg njMsg, string objStatus)
    {
        NewOutGoingMsg outMsg = new NewOutGoingMsg();
        outMsg.current_activity = njMsg.current_activity;
        outMsg.current_activity_status = njMsg.current_activity_status;
        outMsg.pre_req_id = njMsg.pre_req_id;
        outMsg.pre_req_reqData = njMsg.pre_req_reqData;
        outMsg.pre_req_status = njMsg.pre_req_status;
        outMsg.user_data = njMsg.user_data;
        outMsg.current_activity_close_status = objStatus;

        return outMsg;
    }


    public static bool isobj16 = false;
    //public static int NumeratorObj16 = 0;
    //public static int DemoninatorObj16 = 0;
    public static List<ImproperFraction> ansfractionList = new List<ImproperFraction>();

    // for obj 16,4
    public static string loading_pos = "";
    public static string coming_back_from = "";


    // for canvas
    public static bool isMultipication = false;
}

public class ImproperFraction
{
    public List<int> num = new List<int>();
    public List<int> dem = new List<int>();
    public List<string> operators = new List<string>();

}
