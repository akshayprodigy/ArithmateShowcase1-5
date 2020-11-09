using System.Collections;
using System.Collections.Generic;

namespace JasonTest
{


    public class Summary
    {
        public string Right_Answers { get; set; }
        public string Wrong_Answers { get; set; }
        public string Un_Answers { get; set; }
    }

    public class GoToNextQuestion
    {
        public bool Continue { get; set; }
        public string Comment { get; set; }
    }

    public class Explanation
    {
        public string flag { get; set; }
        public IList<object> list { get; set; }
    }

    public class Activity
    {
        public string flag { get; set; }
        public IList<object> list { get; set; }
    }

    public class Canvas
    {
        public string flag { get; set; }
        public IList<object> question { get; set; }
    }

    public class TimelineJson
    {
        public Explanation Explanation { get; set; }
        public Activity Activity { get; set; }
        public Canvas Canvas { get; set; }
    }

    public class Timeline
    {
        public string topic_id { get; set; }
        public string grade_id { get; set; }
        public TimelineJson timeline_json { get; set; }
    }

    public class QuestionFormat
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public string N { get; set; }
        public string D { get; set; }
    }

    public class BBFormat
    {
        public string BB_Function { get; set; }
        public string BB_Name { get; set; }
        public string Input { get; set; }
        public string Output { get; set; }
        public string var_arr { get; set; }
    }
    /*
    public class Display
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public IList<string> Actual_value { get; set; }
        public BBFormat BB_Format { get; set; }
    }

    public class Num
    {
        public IList<string> Operators { get; set; }
        public IList<IList<string>> Variables { get; set; }
        public string isChecked { get; set; }
    }

    public class Denum
    {
        public IList<string> Operators { get; set; }
        public IList<IList<string>> Variables { get; set; }
        public string isChecked { get; set; }
    }

    public class Display
    {
        public Num Num { get; set; }
        public Denum Denum { get; set; }
    }

    public class DisplayResult
    {
        public int Part { get; set; }
        public Display Display { get; set; }
        public string isChecked { get; set; }
    }

    public class Step
    {
        public string step_name { get; set; }
        public IList<Display> Display { get; set; }
        public IList<object> Step_Result { get; set; }
        public IList<DisplayResult> Display_Result { get; set; }
        public string Result { get; set; }
    }

    public class VarMainArr
    {
        public string var_arr { get; set; }
        public string BB_Name { get; set; }
        public string BB_Function { get; set; }
        public string Input { get; set; }
        public string Output { get; set; }
        public string Actual_value { get; set; }
    }

    public class Solution
    {
        public string Start { get; set; }
        public string Solution_Name { get; set; }
        public IList<Step> Steps { get; set; }
        public string End { get; set; }
        public IList<VarMainArr> var_main_arr { get; set; }
    }
    */
    public class Attribute
    {
        public int NUM { get; set; }
        public int NUMOP { get; set; }
        public int MF { get; set; }
        public int MFOP { get; set; }
        public int F { get; set; }
        public int FOP { get; set; }
        public int OP { get; set; }
        public int TOTOP { get; set; }
        public int TOTATT { get; set; }
    }

    public class Step
    {
        public Attribute Attribute { get; set; }
        public IList<object> Topic { get; set; }
        public IList<object> Links { get; set; }
        public IList<object> Error_msg { get; set; }
    }

    public class Solution
    {
        public IList<Step> Steps { get; set; }
    }

    public class AttributeJson
    {
        public IList<Solution> Solutions { get; set; }
    }

    public class TlcdQtypeSysGenJson
    {
        public int QType { get; set; }
        public string Qtype_Name { get; set; }
        public IList<QuestionFormat> Question_Format { get; set; }
        public IList<object> Initiation { get; set; }
        public IList<Solution> Solutions { get; set; }
        public AttributeJson Attribute_Json { get; set; }
    }

    public class NOQWithStepName
    {
        public int Count { get; set; }
    }

    public class GENRAL
    {
        public int OPEN_NOQ { get; set; }
        public string OPEN_NOTE { get; set; }
    }

    public class SPECIFIC
    {
        public int S_NOQ { get; set; }
        public string S_NOTE { get; set; }
    }

    public class NOQ
    {
        public int Total_NOQ { get; set; }
        public IList<NOQWithStepName> NOQ_With_Step_Name { get; set; }
        public GENRAL GENRAL { get; set; }
        public IList<SPECIFIC> SPECIFIC { get; set; }
    }

    public class Range
    {
        public string V1N { get; set; }
        public string V1D { get; set; }
        public string V2N { get; set; }
        public string V2D { get; set; }
    }

    public class QuestionRange
    {
        public IList<Range> Range { get; set; }
    }

    public class PriCondition
    {
        public string first { get; set; }
    }

    public class D
    {
        public string fifth { get; set; }
        public string sixth { get; set; }
    }

    public class SecCondition
    {
        public D D { get; set; }
    }

    public class Condition
    {
        public PriCondition Pri_Condition { get; set; }
        public SecCondition Sec_Condition { get; set; }
    }

    public class FirstTime
    {
        public string error_msg { get; set; }
    }

    public class SecondTime
    {
        public string error_msg { get; set; }
    }

    public class StepsPrompt
    {
        public IList<FirstTime> First_Time { get; set; }
        public IList<SecondTime> Second_Time { get; set; }
    }

    public class StepsPreRequiste
    {
        public string first { get; set; }
        public string second { get; set; }
    }

    public class TlcdDiffLevelJson
    {
        public string QType_ID { get; set; }
        public string Grade { get; set; }
        public string Type { get; set; }
        public NOQ NOQ { get; set; }
        public IList<QuestionRange> Question_Range { get; set; }
        public IList<Condition> Condition { get; set; }
        public IList<StepsPrompt> Steps_Prompts { get; set; }
        public IList<StepsPreRequiste> Steps_PreRequiste { get; set; }
    }

    public class Question
    {
        public string start_point { get; set; }
        public string session_tlcd_id { get; set; }
        public string tlcd_qlm_id { get; set; }
        public string tlcd_stud_id { get; set; }
        public string tlcd_qtype_id { get; set; }
        public string tlcd_diff_level { get; set; }
        public TlcdQtypeSysGenJson tlcd_qtype_sys_gen_json { get; set; }
        public int tlcd_status { get; set; }
        public string tlcd_by_which_solution { get; set; }
        public TlcdDiffLevelJson tlcd_diff_level_json { get; set; }
        public string tlcd_created_date { get; set; }
        public string session_tlcdpr_id { get; set; }
        public string session_parent_pre_requiste_child_id { get; set; }
    }
    [System.Serializable]
    public class Datum
    {
        public Summary Summary { get; set; }
        public GoToNextQuestion go_to_next_question { get; set; }
        public Timeline Timeline { get; set; }
        public bool? Success { get; set; }
        public Question Question { get; set; }
    }
    [System.Serializable]
    public class McqResult
    {
        public bool success { get; set; }
        public List<Datum> data { get; set; }
    }
}
