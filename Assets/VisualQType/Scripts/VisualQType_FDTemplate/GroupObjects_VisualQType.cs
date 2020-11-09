using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GroupObjects
{
    //public int questionNum;
    //public string rightAns_Num, rightAns_Den;
    //public string question;
    //public string subQuestion;
    //public Sprite[] questionSprite;
}
public class GroupObjects_VisualQType : MonoBehaviour
{

    Text Question;
    Text SubQuestion;

    Image[] questionSprite_Img;
    InputField inputField_Num, inputField_Den;
    GameObject hintPopup_Pnl, loading, objOver;
    Text hintMsg_Text;
    Button hintOkayBtn, submitBtn, deleteBtn_Num, deleteBtn_Den;
    Button btn0, btn1, btn2, btn3, btn4, btn5, btn6, btn7, btn8, btn9;
    GameObject questionImgArray;
    //Image questionImg1, questionImg2, questionImg3;

    int questionNum;
    bool isNumerator, isDenominator;
    string spritePath;

    //public GroupObjects[] _groupObjects;

    private void Awake()
    {
        Init();
    }


    private void Init()
    {
        spritePath = "QTypeSprites/GroupObjects/";
        loading = transform.GetChildFromName<Transform>("Loading").gameObject;
        objOver = transform.GetChildFromName<Transform>("Hint_GameOver_Panel").gameObject;
        Question = GameObject.Find("VisualQTYpe_GroupObjects/TemplateQType/Image_Root/QuestionText").GetComponent<Text>();
        SubQuestion = GameObject.Find("VisualQTYpe_GroupObjects/TemplateQType/Image_Root/QuestionArea_Panel/SubQuestionText").GetComponent<Text>();
        hintPopup_Pnl = GameObject.Find("VisualQTYpe_GroupObjects/TemplateQType/Image_Root/Hint_Popup_Panel");
        hintMsg_Text = GameObject.Find("VisualQTYpe_GroupObjects/TemplateQType/Image_Root/Hint_Popup_Panel/Dialougue Panel/Text_Hint").GetComponent<Text>();

        questionImgArray = GameObject.Find("VisualQTYpe_GroupObjects/TemplateQType/Image_Root/QuestionArea_Panel/Question_Sprite_Pnl");
        questionSprite_Img = new Image[3]; // = GameObject.FindGameObjectsWithTag("Moving Apple")
        for (int j = 0; j < questionSprite_Img.Length; j++)
        {
            questionSprite_Img[j] = questionImgArray.transform.GetChild(j).GetComponent<Image>();
        }

        hintOkayBtn = GameObject.Find("VisualQTYpe_GroupObjects/TemplateQType/Image_Root/Hint_Popup_Panel/Dialougue Panel/Button").GetComponent<Button>();
        hintOkayBtn.onClick.AddListener(() => CloseHintPopup());

        submitBtn = GameObject.Find("VisualQTYpe_GroupObjects/TemplateQType/Image_Root/NextBtn").GetComponent<Button>();
        submitBtn.onClick.AddListener(() => OnSubmit());

        deleteBtn_Num = GameObject.Find("VisualQTYpe_GroupObjects/TemplateQType/Image_Root/QuestionArea_Panel/ClearInputField_Num").GetComponent<Button>();
        deleteBtn_Num.onClick.AddListener(() => OnDeleteBtn_Num());


        deleteBtn_Den = GameObject.Find("VisualQTYpe_GroupObjects/TemplateQType/Image_Root/QuestionArea_Panel/ClearInputField_Den").GetComponent<Button>();
        deleteBtn_Den.onClick.AddListener(() => OnDeleteBtn_Den());

        inputField_Num = GameObject.Find("VisualQTYpe_GroupObjects/TemplateQType/Image_Root/QuestionArea_Panel/InputField_Numerator").GetComponent<InputField>();
        EventTrigger.Entry entryNum = new EventTrigger.Entry();
        entryNum.callback.AddListener((eventData) => { OnNumeratorField((PointerEventData)eventData); });
        inputField_Num.gameObject.GetComponent<EventTrigger>().triggers.Add(entryNum);

        inputField_Den = GameObject.Find("VisualQTYpe_GroupObjects/TemplateQType/Image_Root/QuestionArea_Panel/InputField_Denominator").GetComponent<InputField>();
        EventTrigger.Entry entryDen = new EventTrigger.Entry();
        entryDen.callback.AddListener((eventData) => { OnDenominatorField((PointerEventData)eventData); });
        inputField_Den.gameObject.GetComponent<EventTrigger>().triggers.Add(entryDen);

        btn0 = GameObject.Find("VisualQTYpe_GroupObjects/TemplateQType/Image_Root/NumberPad_Pnl/0").GetComponent<Button>();
        btn1 = GameObject.Find("VisualQTYpe_GroupObjects/TemplateQType/Image_Root/NumberPad_Pnl/1").GetComponent<Button>();
        btn2 = GameObject.Find("VisualQTYpe_GroupObjects/TemplateQType/Image_Root/NumberPad_Pnl/2").GetComponent<Button>();
        btn3 = GameObject.Find("VisualQTYpe_GroupObjects/TemplateQType/Image_Root/NumberPad_Pnl/3").GetComponent<Button>();
        btn4 = GameObject.Find("VisualQTYpe_GroupObjects/TemplateQType/Image_Root/NumberPad_Pnl/4").GetComponent<Button>();
        btn5 = GameObject.Find("VisualQTYpe_GroupObjects/TemplateQType/Image_Root/NumberPad_Pnl/5").GetComponent<Button>();
        btn6 = GameObject.Find("VisualQTYpe_GroupObjects/TemplateQType/Image_Root/NumberPad_Pnl/6").GetComponent<Button>();
        btn7 = GameObject.Find("VisualQTYpe_GroupObjects/TemplateQType/Image_Root/NumberPad_Pnl/7").GetComponent<Button>();
        btn8 = GameObject.Find("VisualQTYpe_GroupObjects/TemplateQType/Image_Root/NumberPad_Pnl/8").GetComponent<Button>();
        btn9 = GameObject.Find("VisualQTYpe_GroupObjects/TemplateQType/Image_Root/NumberPad_Pnl/9").GetComponent<Button>();

        btn0.onClick.AddListener(() => OnNumberButton(0));
        btn1.onClick.AddListener(() => OnNumberButton(1));
        btn2.onClick.AddListener(() => OnNumberButton(2));
        btn3.onClick.AddListener(() => OnNumberButton(3));
        btn4.onClick.AddListener(() => OnNumberButton(4));
        btn5.onClick.AddListener(() => OnNumberButton(5));
        btn6.onClick.AddListener(() => OnNumberButton(6));
        btn7.onClick.AddListener(() => OnNumberButton(7));
        btn8.onClick.AddListener(() => OnNumberButton(8));
        btn9.onClick.AddListener(() => OnNumberButton(9));
    }

    private void Start()
    {
        Invoke("Initialize_StartQType", 4);
    }

    // Start is called before the first frame update
    void Initialize_StartQType()
    {
        questionNum = 0;

        Question.text = QType_File_Creator.Instance.stry_data.list[questionNum].question;
        SubQuestion.text = QType_File_Creator.Instance.stry_data.list[questionNum].SubQuestion;
        hintPopup_Pnl.SetActive(false);

        for (int i = 0; i < QType_File_Creator.Instance.stry_data.list[questionNum].questionSprite_list.Count; i++)
        {
            questionSprite_Img[i].gameObject.SetActive(true);
            questionSprite_Img[i].sprite = Resources.Load<Sprite>(spritePath + QType_File_Creator.Instance.stry_data.list[questionNum].questionSprite_list[i]); //load from resources

        }
        objOver.SetActive(false);
        loading.SetActive(false);

    }

   

    void OnSubmit()
    {
        if (questionNum < 9)
        {
            if ((inputField_Num.text == QType_File_Creator.Instance.stry_data.list[questionNum].rightAns_Num) && (inputField_Den.text == QType_File_Creator.Instance.stry_data.list[questionNum].rightAns_Den))
            {
                questionNum++;

                NextQuestion();

                return;
            }

            else
            {
                ShowHintPopup();
            }
        }

        else
        {
            //objOver.SetActive(true);
            //this.gameObject.SetActive(false);
            GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
            Invoke("ExitNow", 3.0f);
            
        }
    }
    void ExitNow()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    void NextQuestion()
    {
        Question.text = QType_File_Creator.Instance.stry_data.list[questionNum].question;
        SubQuestion.text = QType_File_Creator.Instance.stry_data.list[questionNum].SubQuestion;

        for (int j = 0; j < questionSprite_Img.Length; j++)
        {
            questionSprite_Img[j].gameObject.SetActive(false);
        }

        for (int i = 0; i < QType_File_Creator.Instance.stry_data.list[questionNum].questionSprite_list.Count; i++)
        {
            questionSprite_Img[i].gameObject.SetActive(true);
            questionSprite_Img[i].sprite = Resources.Load<Sprite>(spritePath + QType_File_Creator.Instance.stry_data.list[questionNum].questionSprite_list[i]);
        }

        inputField_Num.text = "";
        inputField_Den.text = "";
        isNumerator = false;
        isDenominator = false;
    }

    void ShowHintPopup()
    {
        hintPopup_Pnl.SetActive(true);


        if (questionNum >= 5)
        {

            //Numerator wrong
            if ((inputField_Num.text != QType_File_Creator.Instance.stry_data.list[questionNum].rightAns_Num) && (inputField_Den.text == QType_File_Creator.Instance.stry_data.list[questionNum].rightAns_Den))
            {
                hintMsg_Text.text = "Count the number of groups to be considered from the given groups for the correct numerator. ";
            }

            //Denominator wrong
            if ((inputField_Den.text != QType_File_Creator.Instance.stry_data.list[questionNum].rightAns_Den) && (inputField_Num.text == QType_File_Creator.Instance.stry_data.list[questionNum].rightAns_Num))
            {
                hintMsg_Text.text = "The number of groups considered from the set of groups is the numerator and total number of groups is the denominator";
            }


            //Both wrong
            if ((inputField_Num.text != QType_File_Creator.Instance.stry_data.list[questionNum].rightAns_Num) && (inputField_Den.text != QType_File_Creator.Instance.stry_data.list[questionNum].rightAns_Den))
            {
                hintMsg_Text.text = "You need to represent the fraction of the groups considered not the fraction of the objects considered. The number of groups considered from the set of groups is numerator and total number of groups is denominator ";
            }
        }
        else if (questionNum < 5)
        {

            //Numerator wrong
            if ((inputField_Num.text != QType_File_Creator.Instance.stry_data.list[questionNum].rightAns_Num) && (inputField_Den.text == QType_File_Creator.Instance.stry_data.list[questionNum].rightAns_Den))
            {
                hintMsg_Text.text = "Count the number of considered objects from the group for the correct numerator";
            }

            //Denominator wrong
            if ((inputField_Den.text != QType_File_Creator.Instance.stry_data.list[questionNum].rightAns_Den) && (inputField_Num.text == QType_File_Creator.Instance.stry_data.list[questionNum].rightAns_Num))
            {
                hintMsg_Text.text = "Count the total number of objects  for the correct denominator";
            }

            //Both wrong
            if ((inputField_Num.text != QType_File_Creator.Instance.stry_data.list[questionNum].rightAns_Num) && (inputField_Den.text != QType_File_Creator.Instance.stry_data.list[questionNum].rightAns_Den))
            {
                hintMsg_Text.text = "The number of considered objects in the group written upon the total number of objects in the group will give the correct numerator and denominator for the fraction required. ";
            }
        }

    }

    void CloseHintPopup()
    {
        hintPopup_Pnl.SetActive(false);
        inputField_Num.text = "";
        inputField_Den.text = "";

    }

    void OnNumberButton(int number)
    {
        if (isNumerator)
        {
            inputField_Num.text += number.ToString();
        }
        else if (isDenominator)
        {
            inputField_Den.text += number.ToString();

        }
    }

    void OnNumeratorField(PointerEventData eventData)
    {
        isNumerator = true;
        isDenominator = false;
    }

    void OnDenominatorField(PointerEventData eventData)
    {
        isNumerator = false;
        isDenominator = true;


    }

    void OnDeleteBtn_Num()
    {
        inputField_Num.text = "";
    }

    void OnDeleteBtn_Den()
    {
        inputField_Den.text = "";
    }
}

