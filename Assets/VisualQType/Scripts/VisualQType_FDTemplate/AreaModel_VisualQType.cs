using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class AreaModel
{
    //public int questionNum;
    //public string question;
    ////public string question_Mark;
    //public string SubQuestion;
    //public string rightAns_Num, rightAns_Den;
    //public int numeratorShadeCount, denominatorShadeCount;
    //public Sprite questionSprite;
    //public GameObject shadePartImg;

}

public class AreaModel_VisualQType : MonoBehaviour
{
    Text Question;
    Text subQuestion;
    Image questionSprite_Img;
    InputField inputField_Num, inputField_Den;
    GameObject hintPopup_Pnl, loading, objOver;
    TEXDraw hintMsg_Text;
    int questionNum;
    bool isNumerator, isDenominator;
    int shadeCount;
    GameObject inputFields_Hide;
    GameObject NumberPad;
    GameObject rectShade, hexaShade, triangleShade, pentaShade;
    string spritePath;

    Button hintOkayBtn, submitBtn, deleteBtn_Num, deleteBtn_Den;
    Button btn0, btn1, btn2, btn3, btn4, btn5, btn6, btn7, btn8, btn9;
    Button RS1, RS2, RS3, RS4, HS1, HS2, HS3, HS4, HS5, HS6, TS1, TS2, TS3, PS1, PS2, PS3, PS4, PS5;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        spritePath = "QTypeSprites/AreaModel/";
        loading = transform.GetChildFromName<Transform>("Loading").gameObject;
        objOver = transform.GetChildFromName<Transform>("Hint_GameOver_Panel").gameObject;
        Question = GameObject.Find("VisualQType_AreaModel_Temp/TemplateQType/Image_Root/QuestionText").GetComponent<Text>();
        subQuestion = GameObject.Find("VisualQType_AreaModel_Temp/TemplateQType/Image_Root/SubQuestionText").GetComponent<Text>();
        questionSprite_Img = GameObject.Find("VisualQType_AreaModel_Temp/TemplateQType/Image_Root/QuestionArea_Panel/Question_Image").GetComponent<Image>();
        inputFields_Hide = GameObject.Find("VisualQType_AreaModel_Temp/TemplateQType/Image_Root/QuestionArea_Panel/InputFields");
        NumberPad = GameObject.Find("VisualQType_AreaModel_Temp/TemplateQType/Image_Root/NumberPad_Pnl");

        rectShade = GameObject.Find("VisualQType_AreaModel_Temp/TemplateQType/Image_Root/QuestionArea_Panel/RectShade");
        hexaShade = GameObject.Find("VisualQType_AreaModel_Temp/TemplateQType/Image_Root/QuestionArea_Panel/HexaShade");
        triangleShade = GameObject.Find("VisualQType_AreaModel_Temp/TemplateQType/Image_Root/QuestionArea_Panel/TriangleShade");
        pentaShade = GameObject.Find("VisualQType_AreaModel_Temp/TemplateQType/Image_Root/QuestionArea_Panel/PentaShade");

        RS1 = GameObject.Find("VisualQType_AreaModel_Temp/TemplateQType/Image_Root/QuestionArea_Panel/RectShade/RS1").GetComponent<Button>();
        RS2 = GameObject.Find("VisualQType_AreaModel_Temp/TemplateQType/Image_Root/QuestionArea_Panel/RectShade/RS2").GetComponent<Button>();
        RS3 = GameObject.Find("VisualQType_AreaModel_Temp/TemplateQType/Image_Root/QuestionArea_Panel/RectShade/RS3").GetComponent<Button>();
        RS4 = GameObject.Find("VisualQType_AreaModel_Temp/TemplateQType/Image_Root/QuestionArea_Panel/RectShade/RS4").GetComponent<Button>();
        RS1.onClick.AddListener(() => OnShadePart(0));
        RS2.onClick.AddListener(() => OnShadePart(1));
        RS3.onClick.AddListener(() => OnShadePart(2));
        RS4.onClick.AddListener(() => OnShadePart(3));
        

        HS1 = GameObject.Find("VisualQType_AreaModel_Temp/TemplateQType/Image_Root/QuestionArea_Panel/HexaShade/HS1_1").GetComponent<Button>();
        HS2 = GameObject.Find("VisualQType_AreaModel_Temp/TemplateQType/Image_Root/QuestionArea_Panel/HexaShade/HS2_2").GetComponent<Button>();
        HS3 = GameObject.Find("VisualQType_AreaModel_Temp/TemplateQType/Image_Root/QuestionArea_Panel/HexaShade/HS3_3").GetComponent<Button>();
        HS4 = GameObject.Find("VisualQType_AreaModel_Temp/TemplateQType/Image_Root/QuestionArea_Panel/HexaShade/HS4_4").GetComponent<Button>();
        HS5 = GameObject.Find("VisualQType_AreaModel_Temp/TemplateQType/Image_Root/QuestionArea_Panel/HexaShade/HS5_5").GetComponent<Button>();
        HS6 = GameObject.Find("VisualQType_AreaModel_Temp/TemplateQType/Image_Root/QuestionArea_Panel/HexaShade/HS6_6").GetComponent<Button>();
        HS1.onClick.AddListener(() => OnShadePart(0));
        HS2.onClick.AddListener(() => OnShadePart(1));
        HS3.onClick.AddListener(() => OnShadePart(2));
        HS4.onClick.AddListener(() => OnShadePart(3));
        HS5.onClick.AddListener(() => OnShadePart(4));
        HS6.onClick.AddListener(() => OnShadePart(5));

        TS1 = GameObject.Find("VisualQType_AreaModel_Temp/TemplateQType/Image_Root/QuestionArea_Panel/TriangleShade/TS1_1").GetComponent<Button>();
        TS2 = GameObject.Find("VisualQType_AreaModel_Temp/TemplateQType/Image_Root/QuestionArea_Panel/TriangleShade/TS2_2").GetComponent<Button>();
        TS3 = GameObject.Find("VisualQType_AreaModel_Temp/TemplateQType/Image_Root/QuestionArea_Panel/TriangleShade/TS3_3").GetComponent<Button>();
        TS1.onClick.AddListener(() => OnShadePart(0));
        TS2.onClick.AddListener(() => OnShadePart(1));
        TS3.onClick.AddListener(() => OnShadePart(2));

        PS1 = GameObject.Find("VisualQType_AreaModel_Temp/TemplateQType/Image_Root/QuestionArea_Panel/PentaShade/PS1_1").GetComponent<Button>();
        PS2 = GameObject.Find("VisualQType_AreaModel_Temp/TemplateQType/Image_Root/QuestionArea_Panel/PentaShade/PS2_2").GetComponent<Button>();
        PS3 = GameObject.Find("VisualQType_AreaModel_Temp/TemplateQType/Image_Root/QuestionArea_Panel/PentaShade/PS3_3").GetComponent<Button>();
        PS4 = GameObject.Find("VisualQType_AreaModel_Temp/TemplateQType/Image_Root/QuestionArea_Panel/PentaShade/PS4_4").GetComponent<Button>();
        PS5 = GameObject.Find("VisualQType_AreaModel_Temp/TemplateQType/Image_Root/QuestionArea_Panel/PentaShade/PS5_5").GetComponent<Button>();
        PS1.onClick.AddListener(() => OnShadePart(0));
        PS2.onClick.AddListener(() => OnShadePart(1));
        PS3.onClick.AddListener(() => OnShadePart(2));
        PS4.onClick.AddListener(() => OnShadePart(3));
        PS5.onClick.AddListener(() => OnShadePart(4));


        hintPopup_Pnl = GameObject.Find("VisualQType_AreaModel_Temp/TemplateQType/Image_Root/Hint_Popup_Panel");
        hintMsg_Text = GameObject.Find("VisualQType_AreaModel_Temp/TemplateQType/Image_Root/Hint_Popup_Panel/Dialougue Panel/Text_Hint").GetComponent<TEXDraw>();


        hintOkayBtn = GameObject.Find("VisualQType_AreaModel_Temp/TemplateQType/Image_Root/Hint_Popup_Panel/Dialougue Panel/Button").GetComponent<Button>();
        hintOkayBtn.onClick.AddListener(() => CloseHintPopup());

        submitBtn = GameObject.Find("VisualQType_AreaModel_Temp/TemplateQType/Image_Root/NextBtn").GetComponent<Button>();
        submitBtn.onClick.AddListener(() => OnSubmit());

        deleteBtn_Num = GameObject.Find("VisualQType_AreaModel_Temp/TemplateQType/Image_Root/QuestionArea_Panel/InputFields/ClearInputField_Num").GetComponent<Button>();
        deleteBtn_Num.onClick.AddListener(() => OnDeleteBtn_Num());


        deleteBtn_Den = GameObject.Find("VisualQType_AreaModel_Temp/TemplateQType/Image_Root/QuestionArea_Panel/InputFields/ClearInputField_Den").GetComponent<Button>();
        deleteBtn_Den.onClick.AddListener(() => OnDeleteBtn_Den());

        inputField_Num = GameObject.Find("VisualQType_AreaModel_Temp/TemplateQType/Image_Root/QuestionArea_Panel/InputFields/InputField_Numerator").GetComponent<InputField>();
        EventTrigger.Entry entryNum = new EventTrigger.Entry();
        entryNum.callback.AddListener((eventData) => { OnNumeratorField((PointerEventData)eventData); });
        inputField_Num.gameObject.GetComponent<EventTrigger>().triggers.Add(entryNum);

        inputField_Den = GameObject.Find("VisualQType_AreaModel_Temp/TemplateQType/Image_Root/QuestionArea_Panel/InputFields/InputField_Denominator").GetComponent<InputField>();
        EventTrigger.Entry entryDen = new EventTrigger.Entry();
        entryDen.callback.AddListener((eventData) => { OnDenominatorField((PointerEventData)eventData); });
        inputField_Den.gameObject.GetComponent<EventTrigger>().triggers.Add(entryDen);

        btn0 = GameObject.Find("VisualQType_AreaModel_Temp/TemplateQType/Image_Root/NumberPad_Pnl/0").GetComponent<Button>();
        btn1 = GameObject.Find("VisualQType_AreaModel_Temp/TemplateQType/Image_Root/NumberPad_Pnl/1").GetComponent<Button>();
        btn2 = GameObject.Find("VisualQType_AreaModel_Temp/TemplateQType/Image_Root/NumberPad_Pnl/2").GetComponent<Button>();
        btn3 = GameObject.Find("VisualQType_AreaModel_Temp/TemplateQType/Image_Root/NumberPad_Pnl/3").GetComponent<Button>();
        btn4 = GameObject.Find("VisualQType_AreaModel_Temp/TemplateQType/Image_Root/NumberPad_Pnl/4").GetComponent<Button>();
        btn5 = GameObject.Find("VisualQType_AreaModel_Temp/TemplateQType/Image_Root/NumberPad_Pnl/5").GetComponent<Button>();
        btn6 = GameObject.Find("VisualQType_AreaModel_Temp/TemplateQType/Image_Root/NumberPad_Pnl/6").GetComponent<Button>();
        btn7 = GameObject.Find("VisualQType_AreaModel_Temp/TemplateQType/Image_Root/NumberPad_Pnl/7").GetComponent<Button>();
        btn8 = GameObject.Find("VisualQType_AreaModel_Temp/TemplateQType/Image_Root/NumberPad_Pnl/8").GetComponent<Button>();
        btn9 = GameObject.Find("VisualQType_AreaModel_Temp/TemplateQType/Image_Root/NumberPad_Pnl/9").GetComponent<Button>();

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
        subQuestion.text = QType_File_Creator.Instance.stry_data.list[questionNum].SubQuestion;
        hintPopup_Pnl.SetActive(false);
        questionSprite_Img.gameObject.SetActive(true);

        Debug.Log(spritePath + QType_File_Creator.Instance.stry_data.list[questionNum].questionSprite);
        questionSprite_Img.sprite = Resources.Load<Sprite>(spritePath + QType_File_Creator.Instance.stry_data.list[questionNum].questionSprite) ;
        GameObject.FindWithTag(QType_File_Creator.Instance.stry_data.list[questionNum].shadePartImg).SetActive(false);
        inputFields_Hide.SetActive(true);

        NumberPad.SetActive(true);
        rectShade.SetActive(false);
        hexaShade.SetActive(false);
        triangleShade.SetActive(false);
        pentaShade.SetActive(false);
        objOver.SetActive(false);
        loading.SetActive(false);
    }

    public void OnSubmit()
    {
        if (questionNum < 3)
        {
            if ((inputField_Num.text == (QType_File_Creator.Instance.stry_data.list[questionNum].rightAns_Num)) && (inputField_Den.text == QType_File_Creator.Instance.stry_data.list[questionNum].rightAns_Den))
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
            inputFields_Hide.SetActive(false);
            NumberPad.SetActive(false);

            OnSubmitAfterThreeQuestions();
        }
    }
    
    void NextQuestion()
    {
        questionSprite_Img.sprite = Resources.Load<Sprite>(spritePath + QType_File_Creator.Instance.stry_data.list[questionNum].questionSprite);
        Question.text = QType_File_Creator.Instance.stry_data.list[questionNum].question;
        subQuestion.text = QType_File_Creator.Instance.stry_data.list[questionNum].SubQuestion;

        inputField_Num.text = "";
        inputField_Den.text = "";

        if(questionNum == 3)
        {
            questionSprite_Img.gameObject.SetActive(false);
            GameObject.FindWithTag(QType_File_Creator.Instance.stry_data.list[questionNum-1].shadePartImg).SetActive(false);
            GameObject.FindWithTag(QType_File_Creator.Instance.stry_data.list[questionNum].shadePartImg).SetActive(true);
            inputFields_Hide.SetActive(false);
            NumberPad.SetActive(false);

        }
    }

    void OnSubmitAfterThreeQuestions()
    {

        if (questionNum <= 6)
        {
            if(questionNum >= 3)
            {
                if(shadeCount == QType_File_Creator.Instance.stry_data.list[questionNum].numeratorShadeCount)
                {
                    questionNum++;
                    if (questionNum == 7)
                    {
                        this.gameObject.SetActive(false);
                        return;
                    }

                    questionSprite_Img.gameObject.SetActive(false);
                    GameObject.FindWithTag(QType_File_Creator.Instance.stry_data.list[questionNum - 1].shadePartImg).SetActive(false);
                    GameObject.FindWithTag(QType_File_Creator.Instance.stry_data.list[questionNum].shadePartImg).SetActive(true);
                    Question.text = QType_File_Creator.Instance.stry_data.list[questionNum].question;
                    subQuestion.text = QType_File_Creator.Instance.stry_data.list[questionNum].SubQuestion;
                    shadeCount = 0;
                    
                }
                else
                {
                    ShowHintPopup();
                }
            }
            
        }
        else
        {
            GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
            Invoke("ExitNow", 3.0f);

        }
    }
    void ExitNow()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnShadePart(int id)
    {
        if(GameObject.FindWithTag(QType_File_Creator.Instance.stry_data.list[questionNum].shadePartImg).transform.GetChild(id).GetComponent<Image>().color == Color.white)
        {
            shadeCount++;
            GameObject.FindWithTag(QType_File_Creator.Instance.stry_data.list[questionNum].shadePartImg).transform.GetChild(id).GetComponent<Image>().color = Color.red;
        }

        else if (GameObject.FindWithTag(QType_File_Creator.Instance.stry_data.list[questionNum].shadePartImg).transform.GetChild(id).GetComponent<Image>().color == Color.red)
        {
            shadeCount--;
            GameObject.FindWithTag(QType_File_Creator.Instance.stry_data.list[questionNum].shadePartImg).transform.GetChild(id).GetComponent<Image>().color = Color.white;
        }
    }

    public void ShowHintPopup()
    {
        hintPopup_Pnl.SetActive(true);
        if (questionNum < 3)
        {

            hintMsg_Text.GetComponent<TEXDraw>().text = "To represent the correct fraction: " + " \\frac{Number of shaded part }{ Total number of parts}";

            ////Numerator wrong
            //if ((inputField_Num.text != QType_File_Creator.Instance.stry_data.list[questionNum].rightAns_Num) && (inputField_Den.text == QType_File_Creator.Instance.stry_data.list[questionNum].rightAns_Den))
            //{
            //    hintMsg_Text.text = "Count the number of objects that are shaded for the correct numerator";
            //}

            ////Denominator wrong
            //if ((inputField_Den.text != QType_File_Creator.Instance.stry_data.list[questionNum].rightAns_Den) && (inputField_Num.text == QType_File_Creator.Instance.stry_data.list[questionNum].rightAns_Num))
            //{
            //    hintMsg_Text.text = "Count the total number of parts the whole is divided into, irrespective of whether they are shaded or not, for the correct denominator";
            //}

            ////Both wrong
            //if ((inputField_Num.text != QType_File_Creator.Instance.stry_data.list[questionNum].rightAns_Num) && (inputField_Den.text != QType_File_Creator.Instance.stry_data.list[questionNum].rightAns_Den))
            //{
            //    hintMsg_Text.text = "The number of shaded parts written upon the total number of parts will give the correct fraction";
            //}

        }
        else if (questionNum >= 3)
        {
            if (shadeCount != QType_File_Creator.Instance.stry_data.list[questionNum].numeratorShadeCount)
            {
                hintMsg_Text.GetComponent<TEXDraw>().text = "Numerator represents the total number of shaded parts";
            }
        }
    }

    public void CloseHintPopup()
    {
        hintPopup_Pnl.SetActive(false);
        inputField_Num.text = "";
        inputField_Den.text = "";

    }

    public void OnNumberButton(int number)
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

    public void OnNumeratorField(PointerEventData eventData)
    {
        isNumerator = true;
        isDenominator = false;
    }

    public void OnDenominatorField(PointerEventData eventData)
    {
        isNumerator = false;
        isDenominator = true;


    }

    public void OnDeleteBtn_Num()
    {
        inputField_Num.text = "";
    }

    public void OnDeleteBtn_Den()
    {
        inputField_Den.text = "";
    }
}


