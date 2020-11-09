using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class FractionAsDivision
{
    //public string SubQuestion;
    //public string rightAns_Num, rightAns_Den;
    //public Sprite questionSprite;
}


public class FractionAsDivision_QType : MonoBehaviour
{

    Text subQuestion;
    Image questionSprite_Img;
    InputField inputField_Num, inputField_Den;
    GameObject hintPopup_Pnl, loading, objOver;
    Button hintOkayBtn, submitBtn, deleteBtn;
    Button btn0, btn1, btn2, btn3, btn4, btn5, btn6, btn7, btn8, btn9;
    Text hintMsg_Text;
    int questionNum;
    bool isNumerator, isDenominator;
    string spritePath;

    //public FractionAsDivision[] _fractionAsDivision;


    private void Awake()
    {
        Init();
    }

    void Init()
    {
        loading = transform.GetChildFromName<Transform>("Loading").gameObject;
        objOver = transform.GetChildFromName<Transform>("Hint_GameOver_Panel").gameObject;
        spritePath = "QTypeSprites/FractionAsDivision/";

        subQuestion = GameObject.Find("VisualQType_FDTemplate/TemplateQType/Image_Root/SubQuestionText").GetComponent<Text>();
        questionSprite_Img = GameObject.Find("VisualQType_FDTemplate/TemplateQType/Image_Root/QuestionArea_Panel/Question_Image").GetComponent<Image>();
        hintPopup_Pnl = GameObject.Find("VisualQType_FDTemplate/TemplateQType/Image_Root/Hint_Popup_Panel");
        hintMsg_Text = GameObject.Find("VisualQType_FDTemplate/TemplateQType/Image_Root/Hint_Popup_Panel/Dialougue Panel/Text_Hint").GetComponent<Text>();


        hintOkayBtn = GameObject.Find("VisualQType_FDTemplate/TemplateQType/Image_Root/Hint_Popup_Panel/Dialougue Panel/Button").GetComponent<Button>();
        hintOkayBtn.onClick.AddListener(() => CloseHintPopup());

        submitBtn = GameObject.Find("VisualQType_FDTemplate/TemplateQType/Image_Root/NextBtn").GetComponent<Button>();
        submitBtn.onClick.AddListener(() => OnSubmit());

        deleteBtn = GameObject.Find("VisualQType_FDTemplate/TemplateQType/Image_Root/QuestionArea_Panel/ClearInputField").GetComponent<Button>();
        deleteBtn.onClick.AddListener(() => OnDeleteBtn());

        btn0 = GameObject.Find("VisualQType_FDTemplate/TemplateQType/Image_Root/NumberPad_Pnl/0").GetComponent<Button>();
        btn1 = GameObject.Find("VisualQType_FDTemplate/TemplateQType/Image_Root/NumberPad_Pnl/1").GetComponent<Button>();
        btn2 = GameObject.Find("VisualQType_FDTemplate/TemplateQType/Image_Root/NumberPad_Pnl/2").GetComponent<Button>();
        btn3 = GameObject.Find("VisualQType_FDTemplate/TemplateQType/Image_Root/NumberPad_Pnl/3").GetComponent<Button>();
        btn4 = GameObject.Find("VisualQType_FDTemplate/TemplateQType/Image_Root/NumberPad_Pnl/4").GetComponent<Button>();
        btn5 = GameObject.Find("VisualQType_FDTemplate/TemplateQType/Image_Root/NumberPad_Pnl/5").GetComponent<Button>();
        btn6 = GameObject.Find("VisualQType_FDTemplate/TemplateQType/Image_Root/NumberPad_Pnl/6").GetComponent<Button>();
        btn7 = GameObject.Find("VisualQType_FDTemplate/TemplateQType/Image_Root/NumberPad_Pnl/7").GetComponent<Button>();
        btn8 = GameObject.Find("VisualQType_FDTemplate/TemplateQType/Image_Root/NumberPad_Pnl/8").GetComponent<Button>();
        btn9 = GameObject.Find("VisualQType_FDTemplate/TemplateQType/Image_Root/NumberPad_Pnl/9").GetComponent<Button>();

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


        inputField_Num = GameObject.Find("VisualQType_FDTemplate/TemplateQType/Image_Root/QuestionArea_Panel/InputField_Numerator").GetComponent<InputField>();
        EventTrigger.Entry entryNum = new EventTrigger.Entry();
        entryNum.callback.AddListener((eventData) => { OnNumeratorField((PointerEventData)eventData); });
        inputField_Num.gameObject.GetComponent<EventTrigger>().triggers.Add(entryNum);

        inputField_Den = GameObject.Find("VisualQType_FDTemplate/TemplateQType/Image_Root/QuestionArea_Panel/InputField_Denominator").GetComponent<InputField>();
        EventTrigger.Entry entryDen = new EventTrigger.Entry();
        entryDen.callback.AddListener((eventData) => { OnDenominatorField((PointerEventData)eventData); });
        inputField_Den.gameObject.GetComponent<EventTrigger>().triggers.Add(entryDen);
    }

    private void Start()
    {
        Invoke("Initialization", 4f);
    }

    void Initialization()
    {
        hintPopup_Pnl.SetActive(false);
        questionNum = 0;
        questionSprite_Img.sprite = Resources.Load<Sprite>(spritePath + QType_File_Creator.Instance.stry_data.list[questionNum].questionSprite);//load from resources
        subQuestion.text = QType_File_Creator.Instance.stry_data.list[questionNum].SubQuestion;
        objOver.SetActive(false);
        loading.SetActive(false);
    }

    void OnSubmit()
    {
        if (questionNum < 4)
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
        //questionNum.text = _fractionAsDivision[questionNum].question;
        questionSprite_Img.sprite = Resources.Load<Sprite>(spritePath + QType_File_Creator.Instance.stry_data.list[questionNum].questionSprite);//load from resource 
        subQuestion.text = QType_File_Creator.Instance.stry_data.list[questionNum].SubQuestion;
        inputField_Num.text = "";
        inputField_Den.text = "";
    }

    void ShowHintPopup()
    {
        hintPopup_Pnl.SetActive(true);
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
            inputField_Num.text = number.ToString();
        }
        else if (isDenominator)
        {
            inputField_Den.text = number.ToString();

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

    void OnDeleteBtn()
    {
        if (isNumerator)
        {
            inputField_Num.text = "";
        }
        else if (isDenominator)
        {
            inputField_Den.text = "";

        }
    }
}
