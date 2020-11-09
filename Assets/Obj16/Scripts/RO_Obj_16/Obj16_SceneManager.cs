using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Obj16_SceneManager : MonoBehaviour
{
    //string jsonFileName = "Obj16_json.json";
    public delegate void LogMessage(string cases);
    public static event LogMessage onLogMessage;

    public GameObject Traverse_Connector, TraverseOk;
    public QuestionManager_obj16 questionManager_Obj16;
    public string ans;

    GameObject paper, paper1, paperAnim, paperAnim1, paperAnim2, paperAnim3;

    GameObject mainPanel_forAnim, arrowRepresentation, fraction_Loop_Animation, hintPopUpPanel, CurvedArrow, loadingAudio;
    GameObject b2, b3, b4, b5;
    GameObject highlight_1, highlight_2, highlight_3, highlight_4, numBerPad, highlight_5, highlight_6, highlight_7, highlight_8, highlight_Doubled,
        Highlight_Num, Highlight_Den;
    TEXDraw textDraw_1, textDraw_2, textDraw_3, textDraw_4, textDraw_5, textDraw_6, textDraw_7, textDraw_8, textDraw_9, textDraw_10, foldedThriceText;
    Button btn0, btn1, btn2, btn3, btn4, btn5, btn6, btn7, btn8, btn9;
    Text hintText, infoText, startInfoText;
    Button numSubmit, deleteNumBtn, deleteDenBtn;
    int numAns, demAns;
    InputField inputField_Num, inputField_Den;
    bool isNumerator, isDenominator;
    int numeratorValue, denominatorValue;
    GameObject conversationPnl, questionPnl;
    TEXDraw conversationText, questionText;
    int question = 0;
    GameObject object_to_hide;
    public delegate void EventCalled(string EventName);
    public static event EventCalled OnEventCalled;
    string jsonFileName = "Obj6Fraction_By_Multiplication.json";
    public string condition;
    public bool fold1, fold2, fold3, istimer_on,tut;
    public GameObject Fold_1, Fold_1_unfold_button, Fold1_first_fold_hint, fold1_book, fold1_pos, fold_from_here_arrow, fold1_dotted_line;
    public GameObject Folded1, Folded2, Folded3;

    public GameObject fold2_from_here_arrow, left_to_right_curved, fold2_pos, Fold_2, fold2_unfold_button, Fold2_first_fold_hint, Fold2_second_fold_hint, fold2_book1, fold2_book2, fold2_left_to_right_curved1, fold2_left_to_right_curved2, fold2_unfold_page, fold1_from_here_arrow, fold2_dotted_line, right_prompt;
    public GameObject Fold_3, fold3_pos, fold3_book1, fold3_book2, fold3_book3, fold3_unfold_button, fold3_left_to_right_curved1, fold3_left_to_right_curved2, fold3_left_to_right_curved3, Fold3_first_fold_hint, Fold3_second_fold_hint, Fold3_third_fold_hint, fold3_unfold_page, Fold_3_unfold_button, fold3_from_here_arrow, fold4_from_here_arrow, fold3_dotted_line;
    public GameObject Unfold1ButtonArrow, Unfold2ButtonArrow, Unfold3ButtonArrow;
    public int fold2_count, fold3_count;
    public float timer_for_fold, timer_length;

    public GameObject allPapers, NoOfFolds, fractText, NoOfPartsIncrease_text, options, textWithTwoArrows, numberInsidethePaper, doubleText, twoTimesText, seperateAditionText, multipicationtext, lastMultiplicationText, ShadedArrow;
    public GameObject fold3_top, fold3_bottom;
    public GameObject conv, lineInMiddle;
    public GameObject temp;
    public GameObject temp_tut;
    void FindObjects()
    {

        //mainPanel_forAnim = GameObject.Find("Canvas/Main_Panel").transform.GetChild(0).gameObject;
        //textDraw_1 = GameObject.Find("Canvas/Main_Panel").transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<TEXDraw>();
        //textDraw_2 = GameObject.Find("Canvas/Main_Panel").transform.GetChild(0).GetChild(0).GetChild(2).GetComponent<TEXDraw>();
        //textDraw_3 = GameObject.Find("Canvas/Main_Panel").transform.GetChild(0).GetChild(0).GetChild(3).GetComponent<TEXDraw>();
        //textDraw_4 = GameObject.Find("Canvas/Main_Panel").transform.GetChild(0).GetChild(0).GetChild(4).GetComponent<TEXDraw>();
        //textDraw_5 = GameObject.Find("Canvas/Main_Panel").transform.GetChild(0).GetChild(0).GetChild(5).GetComponent<TEXDraw>();
        //textDraw_6 = GameObject.Find("Canvas/Main_Panel").transform.GetChild(0).GetChild(0).GetChild(10).GetComponent<TEXDraw>();
        //textDraw_7 = GameObject.Find("Canvas/Main_Panel").transform.GetChild(0).GetChild(0).GetChild(11).GetComponent<TEXDraw>();
        //textDraw_8 = GameObject.Find("Canvas/Main_Panel").transform.GetChild(0).GetChild(0).GetChild(12).GetComponent<TEXDraw>();
        //arrowRepresentation = GameObject.Find("Canvas/Main_Panel/GameObject/Objects/Arrow_Presentation").transform.GetChild(0).gameObject;
        //fraction_Loop_Animation = GameObject.Find("Canvas/Main_Panel/GameObject/Objects/Frcation_Loop_Animation").transform.GetChild(0).gameObject;

        //highlight_1 = GameObject.Find("Canvas/Main_Panel").transform.GetChild(0).GetChild(0).GetChild(6).gameObject;
        //highlight_2 = GameObject.Find("Canvas/Main_Panel").transform.GetChild(0).GetChild(0).GetChild(7).gameObject;
        //highlight_3 = GameObject.Find("Canvas/Main_Panel").transform.GetChild(0).GetChild(0).GetChild(8).gameObject;
        //highlight_4 = GameObject.Find("Canvas/Main_Panel").transform.GetChild(0).GetChild(0).GetChild(9).gameObject;

        mainPanel_forAnim = transform.GetChildFromName<Transform>("mainPanel_forAnim").gameObject;
        fraction_Loop_Animation = transform.GetChildFromName<Transform>("Frcation_Loop").gameObject;
        numBerPad = transform.GetChildFromName<Transform>("numpad").gameObject;
        textDraw_1 = transform.GetChildFromName<TEXDraw>("TEXDraw_1");
        textDraw_2 = transform.GetChildFromName<TEXDraw>("TEXDraw_2");
        textDraw_3 = transform.GetChildFromName<TEXDraw>("TEXDraw_3");
        textDraw_4 = transform.GetChildFromName<TEXDraw>("TEXDraw_4");
        textDraw_5 = transform.GetChildFromName<TEXDraw>("TEXDraw_5");
        textDraw_6 = transform.GetChildFromName<TEXDraw>("TEXDraw_6");
        textDraw_7 = transform.GetChildFromName<TEXDraw>("TEXDraw_7");
        textDraw_8 = transform.GetChildFromName<TEXDraw>("TEXDraw_8");
        textDraw_9 = transform.GetChildFromName<TEXDraw>("TEXDraw_9");
        textDraw_10 = transform.GetChildFromName<TEXDraw>("TEXDraw_10");
        foldedThriceText = transform.GetChildFromName<TEXDraw>("FoldedThriceText");
        loadingAudio = transform.GetChildFromName<Transform>("LoadAudio").gameObject;
        fold3_top = GameObject.Find("fold3_top");
        fold3_bottom = GameObject.Find("fold3_bottom");

        paper = transform.GetChildFromName<Transform>("Paper").gameObject;
        paper1 = transform.GetChildFromName<Transform>("Paper1").gameObject;
        paper.SetActive(false);
        paper1.SetActive(false);
        right_prompt = GameObject.Find("right_prompt");
        right_prompt.SetActive(false);
        arrowRepresentation = transform.GetChildFromName<Transform>("ArrowImg_1").gameObject;
        CurvedArrow = transform.GetChildFromName<Transform>("CurvedArrow").gameObject;
        highlight_1 = transform.GetChildFromName<Transform>("Highlight_1").transform.GetChild(0).gameObject;
        highlight_2 = transform.GetChildFromName<Transform>("Highlight_2").transform.GetChild(0).gameObject;
        highlight_3 = transform.GetChildFromName<Transform>("Highlight_3").transform.GetChild(0).gameObject;
        highlight_4 = transform.GetChildFromName<Transform>("Highlight_4").transform.GetChild(0).gameObject;
        highlight_Doubled = transform.GetChildFromName<Transform>("Highlight_Double").transform.GetChild(0).gameObject;
        Highlight_Num = transform.GetChildFromName<Transform>("Highlight_Num").transform.GetChild(0).gameObject;
        Highlight_Den = transform.GetChildFromName<Transform>("Highlight_Den").transform.GetChild(0).gameObject;

        highlight_5 = transform.GetChildFromName<Transform>("Highlight_5").transform.GetChild(0).gameObject;
        highlight_6 = transform.GetChildFromName<Transform>("Highlight_6").transform.GetChild(0).gameObject;
        highlight_7 = transform.GetChildFromName<Transform>("Highlight_7").transform.GetChild(0).gameObject;
        highlight_8 = transform.GetChildFromName<Transform>("Highlight_8").transform.GetChild(0).gameObject;

        conversationPnl = transform.GetChildFromName<Transform>("Conversation").transform.GetChild(0).gameObject;
        conversationText = transform.GetChildFromName<Transform>("ConversationText").GetComponent<TEXDraw>();

        questionPnl = transform.GetChildFromName<Transform>("Question").transform.GetChild(0).gameObject;
        questionText = transform.GetChildFromName<Transform>("QuestionText").GetComponent<TEXDraw>();

        hintPopUpPanel = transform.GetChildFromName<Transform>("Hint_Popup_Panel").gameObject;
        hintText = transform.GetChildFromName<Text>("Text_Hint");
        infoText = transform.GetChildFromName<Text>("infoText");
        startInfoText = transform.GetChildFromName<Text>("startInfoText");
        b2 = transform.GetChildFromName<Transform>("2b2").gameObject;
        b3 = transform.GetChildFromName<Transform>("3b3").gameObject;
        b4 = transform.GetChildFromName<Transform>("4b4").gameObject;
        b5 = transform.GetChildFromName<Transform>("5b5").gameObject;
        paperAnim1 = transform.GetChildFromName<Transform>("PaperAnim1").gameObject;
        paperAnim2 = transform.GetChildFromName<Transform>("PaperAnim2").gameObject;
        paperAnim3 = transform.GetChildFromName<Transform>("PaperAnim3").gameObject;
        paperAnim1.SetActive(false);
        paperAnim2.SetActive(false);
        paperAnim3.SetActive(false);

        b2.SetActive(false);
        b3.SetActive(false);
        b4.SetActive(false);
        b5.SetActive(false);
        // numberpad
        btn0 = transform.GetChildFromName<Button>("0");
        btn1 = transform.GetChildFromName<Button>("1");
        btn2 = transform.GetChildFromName<Button>("2");
        btn3 = transform.GetChildFromName<Button>("3");
        btn4 = transform.GetChildFromName<Button>("4");
        btn5 = transform.GetChildFromName<Button>("5");
        btn6 = transform.GetChildFromName<Button>("6");
        btn7 = transform.GetChildFromName<Button>("7");
        btn8 = transform.GetChildFromName<Button>("8");
        btn9 = transform.GetChildFromName<Button>("9");
        numSubmit = transform.GetChildFromName<Button>("numSubmit");
        deleteNumBtn = transform.GetChildFromName<Button>("DeleteNumBtn");
        deleteDenBtn = transform.GetChildFromName<Button>("DeleteDenBtn");

        btn0.onClick.RemoveAllListeners();
        btn1.onClick.RemoveAllListeners();
        btn2.onClick.RemoveAllListeners();
        btn3.onClick.RemoveAllListeners();
        btn4.onClick.RemoveAllListeners();
        btn5.onClick.RemoveAllListeners();
        btn6.onClick.RemoveAllListeners();
        btn7.onClick.RemoveAllListeners();
        btn8.onClick.RemoveAllListeners();
        btn9.onClick.RemoveAllListeners();

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

        numSubmit.onClick.AddListener(() => OnNumberSubmit());
        inputField_Num = transform.GetChildFromName<InputField>("InputField_Num");// GameObject.Find("VisualQType_FDTemplate/TemplateQType/Image_Root/QuestionArea_Panel/InputField_Numerator").GetComponent<InputField>();
        EventTrigger.Entry entryNum = new EventTrigger.Entry();
        entryNum.callback.AddListener((eventData) => { OnNumeratorField((PointerEventData)eventData); });
        inputField_Num.gameObject.GetComponent<EventTrigger>().triggers.Add(entryNum);

        inputField_Den = transform.GetChildFromName<InputField>("InputField_Dem");// GameObject.Find("VisualQType_FDTemplate/TemplateQType/Image_Root/QuestionArea_Panel/InputField_Numerator").GetComponent<InputField>();
        EventTrigger.Entry entryDen = new EventTrigger.Entry();
        entryDen.callback.AddListener((eventData) => { OnDenominatorField((PointerEventData)eventData); });
        inputField_Den.gameObject.GetComponent<EventTrigger>().triggers.Add(entryDen);

        deleteNumBtn.onClick.AddListener(() => OnDeleteNumField());
        deleteDenBtn.onClick.AddListener(() => OnDeleteDenField());


        isDenominator = false;
        isNumerator = false;
        HideHint();
        arrowRepresentation.SetActive(false);
        CurvedArrow.SetActive(false);
        fraction_Loop_Animation.SetActive(false);
        numBerPad.SetActive(false);

        textDraw_2.transform.gameObject.SetActive(false);
        textDraw_3.transform.gameObject.SetActive(false);
        textDraw_4.transform.gameObject.SetActive(false);
        textDraw_5.transform.gameObject.SetActive(false);
        textDraw_6.transform.gameObject.SetActive(false);
        textDraw_7.transform.gameObject.SetActive(false);
        textDraw_8.transform.gameObject.SetActive(false);
        textDraw_9.transform.gameObject.SetActive(false);
        textDraw_10.transform.gameObject.SetActive(false);
        foldedThriceText.transform.gameObject.SetActive(false);

        highlight_1.transform.gameObject.SetActive(false);
        highlight_2.transform.gameObject.SetActive(false);
        highlight_3.transform.gameObject.SetActive(false);
        highlight_4.transform.gameObject.SetActive(false);

        highlight_5.transform.gameObject.SetActive(false);
        highlight_6.transform.gameObject.SetActive(false);
        highlight_7.transform.gameObject.SetActive(false);
        highlight_8.transform.gameObject.SetActive(false);


        fold3_unfold_button = GameObject.Find("Fold_3_unfold_button");
        Unfold3ButtonArrow = GameObject.Find("Unfold3ButtonArrow");
        timer_for_fold = timer_length;
        Fold_1 = GameObject.Find("Fold 1");
        temp_tut = GameObject.Find("temp_tut");
        Fold_1_unfold_button = GameObject.Find("Fold_1_unfold_button");
        Unfold1ButtonArrow = GameObject.Find("Unfold1ButtonArrow");
        Fold_1_unfold_button.GetComponent<Button>().onClick.AddListener(click_on_unfold);
        fold1_book = GameObject.Find("fold1_book");
        fold1_dotted_line = GameObject.Find("Dotted Line Fold1");
        Fold1_first_fold_hint = GameObject.Find("Fold1_first_fold_hint");
        fold_from_here_arrow = GameObject.Find("fold_from_here_arrow");
        fold1_from_here_arrow = GameObject.Find("fold1_from_here_arrow");
        fold2_from_here_arrow = GameObject.Find("fold2_from_here_arrow");
        fold3_from_here_arrow = GameObject.Find("fold3_from_here_arrow");
        fold4_from_here_arrow = GameObject.Find("fold4_from_here_arrow");
        left_to_right_curved = GameObject.Find("left_to_right_curved");
        fold1_pos = GameObject.Find("fold1_pos");
        fold2_pos = GameObject.Find("fold2_pos");
        fold3_pos = GameObject.Find("fold3_pos");
        Folded1 = GameObject.Find("Folded1");
        Folded2 = GameObject.Find("Folded2");
        Folded3 = GameObject.Find("Folded3");

        fold2_dotted_line = GameObject.Find("Dotted Line Fold2");
        Fold_2 = GameObject.Find("Fold 2");
        fold2_book1 = GameObject.Find("fold2_book1");
        fold2_book2 = GameObject.Find("fold2_book2");

        fold2_unfold_button = GameObject.Find("Fold_2_unfold_button");
        fold2_left_to_right_curved1 = GameObject.Find("fold2_left_to_right_curved1");
        fold2_left_to_right_curved2 = GameObject.Find("fold2_left_to_right_curved2");
        fold3_left_to_right_curved3 = GameObject.Find("fold3_left_to_right_curved3");
        Fold2_first_fold_hint = GameObject.Find("Fold2_first_fold_hint");
        Fold2_second_fold_hint = GameObject.Find("Fold2_second_fold_hint");
        Fold3_third_fold_hint = GameObject.Find("Fold3_third_fold_hint");
        fold2_unfold_page = GameObject.Find("fold2_unfold_page");

        Fold_3 = GameObject.Find("Fold 3");
        fold3_dotted_line = GameObject.Find("Dotted Line Fold3");
        fold3_book1 = GameObject.Find("fold3_book1");
        fold3_book2 = GameObject.Find("fold3_book2");
        fold3_book3 = GameObject.Find("fold3_book3");

        fold3_left_to_right_curved1 = GameObject.Find("fold3_left_to_right_curved1");
        fold3_left_to_right_curved2 = GameObject.Find("fold3_left_to_right_curved2");
        Fold3_first_fold_hint = GameObject.Find("Fold3_first_fold_hint");
        Fold3_second_fold_hint = GameObject.Find("Fold3_second_fold_hint");
        fold3_unfold_page = GameObject.Find("fold3_unfold_page");




        allPapers = GameObject.Find("allPapers");
        NoOfPartsIncrease_text = GameObject.Find("NoOfPartsIncrease_text");
        options = GameObject.Find("options");
        textWithTwoArrows = GameObject.Find("textWithTwoArrows");
        numberInsidethePaper = GameObject.Find("numberInsidethePaper");
        doubleText = GameObject.Find("doubleText");
        twoTimesText = GameObject.Find("twoTimesText");
        seperateAditionText = GameObject.Find("seperateAditionText");
        multipicationtext = GameObject.Find("multipicationtext");
        lastMultiplicationText = GameObject.Find("lastMultiplicationText");
        NoOfFolds = GameObject.Find("NumberOffoldsText");
        fractText = GameObject.Find("FractionText");
        ShadedArrow = GameObject.Find("ShadedArrow");


        allPapers.SetActive(false);
        NoOfPartsIncrease_text.SetActive(false);
        options.SetActive(false);
        textWithTwoArrows.SetActive(false);
        numberInsidethePaper.SetActive(false);
        doubleText.SetActive(false);
        twoTimesText.SetActive(false);
        seperateAditionText.SetActive(false);
        multipicationtext.SetActive(false);
        lastMultiplicationText.SetActive(false);
        NoOfFolds.SetActive(false);
        fractText.SetActive(false);
        ShadedArrow.SetActive(false);

        Fold_3_unfold_button.GetComponent<Button>().onClick.AddListener(fold3_unfold_functionality);


        Fold_1_unfold_button.GetComponent<Button>().onClick.AddListener(fold2_unfold_functionality);
        Unfold2ButtonArrow = GameObject.Find("Unfold2ButtonArrow");
        left_to_right_curved.SetActive(false);
        fold_from_here_arrow.SetActive(false);
        fold1_from_here_arrow.SetActive(false);
        fold2_from_here_arrow.SetActive(false);
        fold3_from_here_arrow.SetActive(false);
        fold4_from_here_arrow.SetActive(false);
        Fold1_first_fold_hint.SetActive(false);
        Fold_1_unfold_button.SetActive(false);
        Unfold1ButtonArrow.SetActive(false);
        fold1_book.SetActive(false);
        temp_tut.SetActive(false);
        Fold_1.SetActive(false);
        fold1_dotted_line.SetActive(false);



        fold2_unfold_button.SetActive(false);
        Unfold2ButtonArrow.SetActive(false);
        fold2_left_to_right_curved1.SetActive(false);
        fold2_left_to_right_curved2.SetActive(false);
        fold2_unfold_page.SetActive(false);
        fold2_left_to_right_curved1.SetActive(false);
        fold2_left_to_right_curved2.SetActive(false);
        Fold2_first_fold_hint.SetActive(false);
        Fold2_second_fold_hint.SetActive(false);
        fold2_book2.SetActive(false);
        fold2_book1.SetActive(false);
        Fold_2.SetActive(false);
        fold2_dotted_line.SetActive(false);
        fold3_book2.SetActive(false);
        fold3_book3.SetActive(false);

        fold2_count = 0;
        fold3_count = 0;

        Fold_3.SetActive(false);

        Fold_3.SetActive(false);
        fold3_book1.SetActive(false);
        fold3_book2.SetActive(false);
        fold3_book3.SetActive(false);
        fold3_unfold_button.SetActive(false);
        fold3_left_to_right_curved1.SetActive(false);
        fold3_left_to_right_curved2.SetActive(false);
        fold3_left_to_right_curved3.SetActive(false);
        Fold3_first_fold_hint.SetActive(false);
        Fold3_second_fold_hint.SetActive(false);
        Fold3_third_fold_hint.SetActive(false);
        fold3_unfold_page.SetActive(false);
        fold3_dotted_line.SetActive(false);
        Folded1.SetActive(false);
        Folded2.SetActive(false);
        Folded3.SetActive(false);
        Unfold3ButtonArrow.SetActive(false);
        conv = GameObject.Find("Conversation");
        lineInMiddle = GameObject.Find("VerticleMiddleLine");
        conv.SetActive(false);
        lineInMiddle.SetActive(false);

        //28/07/2020

        Traverse_Connector = GameObject.Find("Traverse_Connector");
        TraverseOk = GameObject.Find("TraverseOk");
        Traverse_Connector.SetActive(false);
        questionManager_Obj16 = GameObject.FindObjectOfType<QuestionManager_obj16>();
        TraverseOk.GetComponent<Button>().onClick.AddListener(() => ClickonTraverseOk());

        if (string.Equals(UtilityArtifacts.coming_back_from, ""))
        {
            infoText.gameObject.SetActive(false);
            startInfoText.gameObject.SetActive(true);
        }
        else
        {
            if (onLogMessage != null)
                onLogMessage("Traversing the user back to ‘Forming Equivalent Fractions by Multiplication’ objective");
            infoText.gameObject.SetActive(true);
            startInfoText.gameObject.SetActive(false);
        }
    }

    public void ClickonTraverseOk()
    {
        SceneManager.LoadScene(questionManager_Obj16.loadScene);
    }

    public void EnableConversation(string Convtext)
    {
        conversationPnl.SetActive(true);
        conversationText.text = Convtext;
    }

    public void DisableConversation()
    {
        conversationPnl.SetActive(false);
        conversationText.text = " ";

    }

    public void EnableQuestion(string questText)
    {
        questionPnl.SetActive(true);
        questionText.text = questText;
    }

    public void DisableQuestion()
    {
        questionPnl.SetActive(false);
        questionText.text = " ";
    }

    private void OnEnable()
    {
        BookPro.onBookFlipCasecall += BookFlipCase_call;
        timeline_new.OnEventCalled += EventToHandle;
    }

    private void OnDisable()
    {
        BookPro.onBookFlipCasecall -= BookFlipCase_call;
        timeline_new.OnEventCalled -= EventToHandle;
    }

    private void Awake()
    {
        Initialised();

    }

    void Start()
    {
        FindObjects();
        DisableQuestion();
    }

    void Initialised()
    {
        Invoke("audio_invoke", 2.0f);

    }

    void ShowHint(string msg, float _time)
    {
        hintText.text = msg;
        hintPopUpPanel.SetActive(true);
        Invoke("HideHint", _time);
    }

    void HideHint()
    {
        hintPopUpPanel.SetActive(false);
    }

    int incorrectCount = 0;
    void OnNumberSubmit()
    {
        if (numeratorValue == numAns && denominatorValue == demAns)
        {
            // show thumns up and load next
            if (onLogMessage != null)
            {
                onLogMessage("User knows ‘Multiplication’ ");
            }
            ShowHint("Well Done", 2);
            GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
            Invoke("MoveToNectTimeline", 2);
        }
        else
        {
            //show hint and next
            if (onLogMessage != null)
            {
                onLogMessage("User has some confusion in ‘Multiplication’");
            }
            incorrectCount++;
            if (incorrectCount > 1)
            {
                // show answer and continue
                ShowHint(" Numerator is " + numAns + " and Denominator is " + demAns, 2);
                Invoke("MoveToNectTimeline", 2);
            }
            else
            {
                if (question == 1)
                {
                    //show hint and reset
                    ShowHint("You have to first multiply the numerator  with " + numAns + " and write the answer in the numerator box.", 5);
                    Highlight_Num.SetActive(true);
                    GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                    GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj16_hint_2b2_Num.wav");

                    resetNum();
                    Invoke("HintForDenominator_Q1", 5f);
                    //Then multiply the denominator with"+ numAns + "  and write in the denominator box"
                }
                else if (question == 2)
                {
                    //show hint and reset
                    ShowHint("You have to first multiply the numerator  with " + numAns + " and write the answer in the numerator box.", 5);
                    Highlight_Num.SetActive(true);
                    GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
                    GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj16_hint_3b3_Num.wav");

                    resetNum();
                    Invoke("HintForDenominator_Q2", 5f);

                }


            }
        }
    }

    void HintForDenominator_Q1()
    {
        ShowHint("Then multiply the denominator with " + numAns + "  and write in the denominator box.", 5);
        Highlight_Den.SetActive(true);
        GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj16_hint_2b2_Den.wav");
        Highlight_Den.SetActive(true);
    }

    void HintForDenominator_Q2()
    {
        ShowHint("Then multiply the denominator with 3 and write in the denominator box", 5);
        Highlight_Den.SetActive(true);
        GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
        GameObject.FindObjectOfType<timeline_new>().playAudioOnRelearn("obj16_hint_3b3_Den.wav");
        Highlight_Den.SetActive(true);
    }

    void resetNum()
    {
        isNumerator = false;
        isDenominator = false;
        inputField_Num.text = "";
        numeratorValue = 0;
        inputField_Den.text = "";
        denominatorValue = 0;
    }

    void OnDeleteNumField()
    {
        isNumerator = false;
        inputField_Num.text = "";
        numeratorValue = 0;
    }

    void OnDeleteDenField()
    {
        isDenominator = false;
        inputField_Den.text = "";
        denominatorValue = 0;
    }

    void MoveToNectTimeline()
    {
        timeline_new.Instance.load_next();
    }

    void OnNumberButton(int number)
    {
        if (isNumerator)
        {
            Debug.Log("OnNumberButton: " + number);
            numeratorValue = numeratorValue * 10 + number;
            inputField_Num.text = numeratorValue.ToString();
        }
        else if (isDenominator)
        {
            Debug.Log("OnNumberButton: " + number);
            denominatorValue = denominatorValue * 10 + number;
            inputField_Den.text = denominatorValue.ToString();
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

    void reset_option()
    {
        foreach (GameObject b in GameObject.FindGameObjectsWithTag("Numbers"))
        {
            b.transform.GetChild(2).gameObject.SetActive(false);
        }
        ans = "";
    }

    void audio_invoke()
    {
        Debug.Log(EVENT.SetUpTimeLine);
        EventManager.setNameOfJasonFile(jsonFileName);
        EventManager.Broadcast(EVENT.SetUpTimeLine);
    }

    void EventToHandle(string EventName)
    {
        infoText.gameObject.SetActive(false);
        switch (EventName)
        {
            case "Obj16_Have_a_look":
                loadingAudio.SetActive(false);
                
                Fold_1.SetActive(true);
               
                fold1_book.SetActive(true);
                disable_paper_fold();
                FindObjectOfType<conversationManager>().EnableConversation("Have a look at this piece of paper ");
                break;

            case "Obj16_Go_ahead":
                //fold1_book.SetActive(false);
                Fold1_first_fold_hint.SetActive(true);
                FindObjectOfType<conversationManager>().EnableConversation(" Go ahead and fold the paper as per the instructions.");
                //Fold1_first_fold_hint.GetComponent<AutoFlip>().StartFlipping();

                break;

             
            case "Obj16_Fold_this_paper":
                FindObjectOfType<conversationManager>().EnableConversation("Fold this paper from bottom corner to perfectly fit within the dotted lines");
                Fold1_first_fold_hint.SetActive(false);
                fold1_dotted_line.SetActive(true);
                temp_tut.SetActive(true);
                
                fold1_book.SetActive(false);
                Invoke("fold_the_paper", 2.5f);
                
                //fold1_book.SetActive(true);
                Invoke("fold_now", 7.0f);
                
                break;

            case "Obj16_unfold1":
                FindObjectOfType<conversationManager>().EnableConversation("Now let's unfold it ");
                fold1 = false;
                istimer_on = false;
                break;

            case "Obj16_click_unfold1":
                FindObjectOfType<conversationManager>().EnableConversation("Click on the unfold button.");
                Fold_1_unfold_button.SetActive(true);
                Unfold1ButtonArrow.SetActive(true);
                break;

            case "Obj16_Quest_Fold1":
                loadingAudio.SetActive(false);
                Fold_1.SetActive(true);
                fold1_book.SetActive(false);
                Folded1.SetActive(true);
                FindObjectOfType<conversationManager>().DisableConversation();
                FindObjectOfType<conversationManager>().EnableQuestion("Can you represent the coloured part of the paper in terms of fractions?");
                FindObjectOfType<QuestionManager_obj16>().EnableForObj16Quest1();

                break;

            case "Obj16_Second_paper_appear":
                //move_fold1_to_corner();
                FindObjectOfType<conversationManager>().DisableQuestion();
                FindObjectOfType<conversationManager>().EnableConversation("Let's take one more paper with the same shaded region ");
                GameObject.Find("Fold 1").transform.GetChild(11).gameObject.SetActive(false);
                 //highlight_marker
                 Fold_2.SetActive(true);
                fold2_book1.SetActive(true);
                disable_paper_fold();
                break;

            case "Obj16_lets_fold_twice":
                FindObjectOfType<conversationManager>().EnableConversation("Let's  fold this paper twice. ");

                break;

            case "Obj16_Make_exactly_two":
                FindObjectOfType<conversationManager>().EnableConversation("Make exactly two folds such that the paper fits in the highlighted area");
                fold2_dotted_line.SetActive(true);
                fold2 = true;
                istimer_on = true;
                enable_paper_fold();
                break;

            case "Obj16_unfold2":
                FindObjectOfType<conversationManager>().EnableConversation("Let's unfold now to see what happens");
                fold2 = false;
                istimer_on = false;
                break;

            case "Obj16_click_unfold2":
                FindObjectOfType<conversationManager>().EnableConversation("Click on the unfold button");

                fold2_unfold_button.SetActive(true);
                Unfold2ButtonArrow.SetActive(true);
                break;


            case "Obj16_Quest_Fold2":
                loadingAudio.SetActive(false);
                Fold_2.SetActive(true);
                fold2_book1.SetActive(false);
                Folded2.SetActive(true);
                FindObjectOfType<conversationManager>().DisableConversation();
                Fold_1.SetActive(false);
                FindObjectOfType<conversationManager>().EnableQuestion("Can you represent the coloured part of the paper in terms of fractions?");
                FindObjectOfType<QuestionManager_obj16>().EnableForObj16Quest2();
                break;

            case "Obj16_Third_paper_appear":
                //move_fold2_to_corner();
                GameObject.Find("Fold 2").transform.GetChild(18).gameObject.SetActive(false);
                GameObject.Find("Fold 2").transform.GetChild(19).gameObject.SetActive(false);
                GameObject.Find("Fold 1").transform.GetChild(11).gameObject.SetActive(false);
                Fold_1.SetActive(true);
                Fold_3.SetActive(true);
                fold3_book1.SetActive(true);
                FindObjectOfType<conversationManager>().DisableQuestion();
                FindObjectOfType<conversationManager>().EnableConversation("Let's take one more paper with the same shaded area");
                break;

            case "Obj16_lets_fold_thrice":
                FindObjectOfType<conversationManager>().EnableConversation("This time we will make 3 folds");
                break;

            case "Obj16_Make_exactly_three":
                FindObjectOfType<conversationManager>().EnableConversation("Make exactly three folds such that the paper fits in the highlighted area ");
                fold3_dotted_line.SetActive(true);
                fold3 = true;
                istimer_on = true;
                enable_paper_fold();
                break;

            case "Obj16_unfold3":
                FindObjectOfType<conversationManager>().EnableConversation("Now let's unfold it");
                fold3 = false;
                istimer_on = false;
                break;

            case "Obj16_click_unfold3":
                FindObjectOfType<conversationManager>().EnableConversation("Click on the unfold button");

                fold3_unfold_button.SetActive(true);
                Unfold3ButtonArrow.SetActive(true);
                break;

            case "Obj16_Quest_Fold3":
                loadingAudio.SetActive(false);
                Fold_3.SetActive(true);
                fold3_book1.SetActive(false);
                Folded3.SetActive(true);
                FindObjectOfType<conversationManager>().DisableConversation();

                FindObjectOfType<conversationManager>().EnableQuestion("Now represent the shaded area in terms of fractions");
                FindObjectOfType<QuestionManager_obj16>().EnableForObj16Quest3();
                break;
            case "Obj16_No_matter":

                FindObjectOfType<conversationManager>().EnableConversation("No matter how many times we folded the paper, the size and shape of the shaded region remained the same.");
                Fold_1.SetActive(false);
                Fold_2.SetActive(false);
                Fold_3.SetActive(false);
                allPapers.SetActive(true);
                NoOfFolds.SetActive(true);

                break;

            case "Obj16_Only_the_number":
                FindObjectOfType<conversationManager>().EnableConversation("Only the number of parts in the pieces of paper increased after each fold.");
                NoOfPartsIncrease_text.SetActive(true);


                break;
            case "Obj16_These_are_the":
                FindObjectOfType<conversationManager>().DisableConversation();
                NoOfPartsIncrease_text.SetActive(false);
                fractText.SetActive(true);
                FindObjectOfType<conversationManager>().EnableQuestion("These are the fractions representing the shaded area for all the 3 papers.");
                ShadedArrow.SetActive(true);
                if (onLogMessage != null)
                    onLogMessage("Let us allow the student to reflect on what they have learned till now");

                break;
            case "Obj16_Quest2":
                loadingAudio.SetActive(false);
                allPapers.SetActive(true);
                NoOfFolds.SetActive(true);
                ShadedArrow.SetActive(true);
                fractText.SetActive(true);
                FindObjectOfType<conversationManager>().EnableQuestion("What are these fractions called?");
                options.SetActive(true);
                FindObjectOfType<QuestionManager_obj16>().EnableForObj16Quest4();

                break;
            case "Obj16_We_started":
                if (onLogMessage != null)
                {
                    onLogMessage("Abstraction Session Starts");
                }
                ShadedArrow.SetActive(false);
                fractText.SetActive(false);
                NoOfFolds.SetActive(false);
                allPapers.SetActive(false);
                options.SetActive(false);
                FindObjectOfType<conversationManager>().EnableConversation("We started with \\frac{1}{2} and ended up with \\frac{3}{6}.");
                textWithTwoArrows.SetActive(true);

                break;
            case "Obj16_lets_learn":
                FindObjectOfType<conversationManager>().EnableConversation("Let's learn how equivalent fractions are related to paper folding.");
                break;

            case "Obj16_lets_look_at":
                textWithTwoArrows.SetActive(false);
                FindObjectOfType<conversationManager>().EnableConversation("Let's look at these papers one more time.");
                allPapers.SetActive(true);
                break;
            case "Obj16_The":
                FindObjectOfType<conversationManager>().EnableConversation("The number of parts in these papers are 2, 4 and 6 respectively.");
                numberInsidethePaper.SetActive(true);
                break;
            case "Obj16_If_you_observe_carefully":
                FindObjectOfType<conversationManager>().EnableConversation("If you observe carefully, you will realize that the number of parts have doubled and tripled with each fold.");
                doubleText.SetActive(true);
                break;
            case "Obj16_As_you_know_doubled":
                twoTimesText.SetActive(true);
                FindObjectOfType<conversationManager>().EnableConversation("As you know, doubled means adding two times and tripled means adding three times.");
                break;
            case "Obj16_repeated_addition":
                twoTimesText.SetActive(false);
                FindObjectOfType<conversationManager>().EnableConversation("This is repeated addition.");
                seperateAditionText.SetActive(true);
                break;
            case "Obj16_Repeated_addition_is":
                seperateAditionText.SetActive(false);
                FindObjectOfType<conversationManager>().EnableConversation("Repeated addition is the same as multiplication.");
                multipicationtext.SetActive(true);
                break;
            case "Obj16_Mathematically":
                FindObjectOfType<conversationManager>().EnableConversation("Mathematically, the action of the paper being folded can be considered as Multiplication.");
                multipicationtext.SetActive(false);
                doubleText.SetActive(false);
                numberInsidethePaper.SetActive(false);
                lastMultiplicationText.SetActive(true);
                foreach (GameObject g in GameObject.FindGameObjectsWithTag("book"))
                    g.GetComponent<AutoFlip>().enabled = true;
                break;
            case "Obj16_represents_1b2":
                FindObjectOfType<conversationManager>().DisableConversation();
                lastMultiplicationText.SetActive(false);
                allPapers.SetActive(false);
                conv.SetActive(true);
                lineInMiddle.SetActive(true);
                EnableConversation("Let's start with the paper that represents " + " \\frac{1}{2} .");
                paper.SetActive(true);
                textDraw_1.transform.DOScale(1f, 0.4f);
                textDraw_1.text = "\\frac{1}{2}";
                break;

            case "Obj16_fold_this_paper_twice":
                EnableConversation("If we fold this paper twice ");
                GameObject.FindGameObjectWithTag("book").GetComponent<AutoFlip>().StartFlipping();
                //show folding paper without animation
                textDraw_1.text = "\\frac{1}{2}";
                break;

            case "Obj16_double_the_numberPart":
                EnableConversation("we double the number of parts.");
                PaperFold_Unfold_Animation();//show paper animation folding/unfolding on right and write number on parts after unfolding 

                textDraw_6.transform.gameObject.SetActive(true);
                textDraw_4.transform.gameObject.SetActive(true);
                textDraw_5.transform.gameObject.SetActive(true);
                textDraw_4.transform.DOScale(1f, 0.4f);
                textDraw_5.transform.DOScale(1f, 0.4f);
                textDraw_6.transform.DOScale(1f, 0.4f);
                break;

            case "Obj16_expressed_As_multiplication":
                EnableConversation("Folding can be expressed as multiplication. ");
                mainPanel_forAnim.SetActive(true);
                textDraw_1.text = "\\frac{1}{2}  X  ";

                break;

            case "Obj16_change_value_fraction":
                EnableConversation("We are trying to find an Equivalent Fraction. We need to multiply the fraction here with something that does not change the value of the fraction. ");

                textDraw_1.transform.DOScale(0f, 0.4f);
                textDraw_1.transform.gameObject.SetActive(false);

                textDraw_2.transform.gameObject.SetActive(true);
                textDraw_2.transform.DOScale(1f, 0.4f);
                break;

            case "Obj16_since_we_have_doubled_parts":
                EnableConversation("Since we have doubled the number of parts, we can multiply " + "\\frac{1}{2}" + " with a fraction whose numerator and the denominator is 2. ");

                textDraw_2.transform.DOScale(0f, 0.4f);
                textDraw_2.transform.gameObject.SetActive(false);

                textDraw_1.transform.gameObject.SetActive(true);
                textDraw_6.transform.gameObject.SetActive(true);

                textDraw_1.transform.DOScale(1f, 0.4f);
                textDraw_6.transform.DOScale(1f, 0.4f);

                highlight_Doubled.SetActive(true);
                Invoke("waitfornext", 1.5f);
                Invoke("load_next", 3.0f);

                break;

            case "Obj16_num_den_is_2":
                EnableConversation("Since we have doubled the number of parts,  we can multiply " + "\\frac{1}{2}" + " with a fraction whose numerator and the denominator is 2. ");

                highlight_2.SetActive(true);

                Invoke("EnableFrcation_2b2", 3.2f);
                break;


            case "Obj16_Quest_GoAhead_multiply_2b2":
                //Question1
                EnableQuestion("Go ahead and multiply the Numerator and Denominator of the fraction by  " + "\\frac{2}{2}");
                highlight_1.SetActive(false);
                highlight_2.SetActive(false);
                resetNum();
                numBerPad.SetActive(true);

                question = 1;

                numAns = 2;
                demAns = 4;
                break;

            case "Obj16_the_value_2b2_1":
                DisableQuestion();
                EnableConversation("The value of " + "\\frac{2}{2}" + " is 1 ");
                numBerPad.SetActive(false);
                textDraw_7.transform.DOScale(0f, 0.4f);
                textDraw_7.transform.gameObject.SetActive(false);
                textDraw_9.transform.gameObject.SetActive(true);
                highlight_3.SetActive(true);
                //highlight_4.SetActive(true);
                arrowRepresentation.SetActive(true);
                textDraw_9.transform.DOScale(1f, 0.4f);
                arrowRepresentation.transform.DOScale(1f, 0.4f);

                break;
            case "Obj16_the_value_same":
                DisableQuestion();
                EnableConversation("And hence the value of the fraction " + "\\frac{1}{2} and \\frac{2}{4}" + " is the same. ");
                CurvedArrow.SetActive(true);
                CurvedArrow.transform.DOScale(1f, 0.4f);
                break;

            case "Obj16_fraction_first_time":
                EnableConversation("\\frac{2}{4}" + " is the same fraction we got when we unfolded the paper the first time. ");
                textDraw_8.transform.gameObject.SetActive(true);
                textDraw_8.transform.DOScale(1f, 0.4f);

                highlight_3.SetActive(false);
                highlight_4.SetActive(false);
                highlight_5.transform.gameObject.SetActive(true);
                //highlight_6.transform.gameObject.SetActive(true);
                highlight_7.transform.gameObject.SetActive(true);
                //highlight_8.transform.gameObject.SetActive(true);
                //arrowRepresentation.SetActive(false);
                break;

            case "Obj16_the_paper_presenting":
                EnableConversation("The paper representing " + "\\frac{1}{2}" + " as shaded area when folded thrice is same as  " + "\\frac{1}{2} X \\frac{3}{3} ");
                PaperThriceAnimation(); //show trice paper animation
                paper.SetActive(false);
                paper1.SetActive(true);
                GameObject.FindGameObjectWithTag("book1").GetComponent<AutoFlip>().StartFlipping();

                arrowRepresentation.transform.DOScale(0f, 0.4f);
                CurvedArrow.transform.DOScale(0f, 0.4f);
                textDraw_8.transform.DOScale(0f, 0.4f);
                textDraw_9.transform.DOScale(0f, 0.4f);
                textDraw_4.transform.DOScale(0f, 0.4f);
                textDraw_5.transform.DOScale(0f, 0.4f);
                arrowRepresentation.SetActive(false);
                CurvedArrow.SetActive(false);
                highlight_5.transform.gameObject.SetActive(false);
                highlight_6.transform.gameObject.SetActive(false);
                highlight_7.transform.gameObject.SetActive(false);
                highlight_8.transform.gameObject.SetActive(false);
                textDraw_8.transform.gameObject.SetActive(false);
                textDraw_9.transform.gameObject.SetActive(false);
                textDraw_4.transform.gameObject.SetActive(false);
                textDraw_5.transform.gameObject.SetActive(false);
                textDraw_10.transform.gameObject.SetActive(true);
                textDraw_10.transform.DOScale(1f, 0.4f);
                textDraw_10.text = " \\frac{1}{2} X \\frac{3}{3} ";
                foldedThriceText.transform.gameObject.SetActive(true);
                foldedThriceText.transform.DOScale(1f, 0.4f);
                StartCoroutine("EnableFraction_3b3");
                //Invoke("EnableFraction_3b3", 7.1f);
                break;
            case "Obj16_s":

                EnableQuestion("");

                break;
            case "Obj16_Quest_GoAhead_multiply_3b3":
                //Question2

                DisableConversation();
                EnableQuestion("Go ahead and multiply the Numerator and Denominator of the fraction by " + "\\frac{3}{3}");
                resetNum();
                numBerPad.SetActive(true);

                question = 2;


                numAns = 3;
                demAns = 6;
                break;

            case "Obj16_we_can_say_EF":
                DisableQuestion();
                EnableConversation("We can say that any fraction multiplied with another fraction whose value is 1 does not change the value of the fraction but will result in an equivalent fraction.");
                fraction_Loop_Animation.SetActive(true);
                fraction_Loop_Animation.transform.DOScale(1f, 0.4f);

                foldedThriceText.transform.DOScale(0f, 0.4f);
                numBerPad.transform.DOScale(0f, 0.4f);
                textDraw_10.transform.DOScale(0f, 0.4f);

                foldedThriceText.transform.gameObject.SetActive(false);
                numBerPad.SetActive(false);
                textDraw_10.transform.gameObject.SetActive(false);
                paper1.SetActive(false);
                loopThroughFracValues();
                break;

            case "Obj16_this_is_how_find_EF":
                EnableConversation("This is how we find an equivalent fraction by multiplication. ");

                break;

            case "Obj16_RO1_WillResult_EF":

                enableFade1();
                if (onLogMessage != null)
                    onLogMessage("Let us allow the student to reflect on what they have learned till now");

                break;

            case "Obj16_RO2_multiplying":
                enableFade2();
                break;

            case "Obj16_RO3_multiplying_a_Fraction":
                enableFade3();
                break;

            case "Obj16_RO4_which_true":
                enableFade4();
                break;
        }
    }
    void waitfornext()
    {
        textDraw_3.transform.gameObject.SetActive(true);
        textDraw_3.transform.DOScale(1f, 0.4f);
    }
    void EnableFrcation_2b2()
    {
        highlight_2.SetActive(false);
        DisableConversation();
        textDraw_1.transform.DOScale(0f, 0.2f);
        textDraw_3.transform.DOScale(0f, 0.2f);
        textDraw_6.transform.DOScale(0f, 0.2f);
        textDraw_1.transform.gameObject.SetActive(false);
        textDraw_3.transform.gameObject.SetActive(false);
        textDraw_4.transform.gameObject.SetActive(false);
        textDraw_5.transform.gameObject.SetActive(false);
        textDraw_6.transform.gameObject.SetActive(false);

        textDraw_7.transform.gameObject.SetActive(true); //fraction should come one by one with sound effect
        textDraw_7.transform.DOScale(1f, 0.4f);
        GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
        Invoke("loadNect", 6.2f);
    }
    void fold_now()
    {
        temp_tut.SetActive(false);
        fold1_book.SetActive(true);
        reset_fold();
        enable_paper_fold();
        tut = false;
        fold1 = true;
        istimer_on = true;
    }
    public void load_next()
    {
        FindObjectOfType<timeline_new>().load_next();
    }
    IEnumerator EnableFraction_3b3()
    {
        // 1/2 x 3/3 = (1x3)/(2x3)
        yield return new WaitForSeconds(7.1f);
        //textDraw_10.text = " \\frac{1}{2} X \\frac{3}{3} = \\frac{1 X 3}{2 X 3} = ";

        textDraw_10.text = " \\frac{1}{2} X \\frac{3}{3} = \\frac{1 X 3}";
        GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(1.0f);
        textDraw_10.text = " \\frac{1}{2} X \\frac{3}{3} = \\frac{1 X 3}{2 X 3} = ";
        GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(1.0f);
        Invoke("loadNect", 0.2f);
    }

    void loadNect()
    {
        GameObject.FindObjectOfType<timeline_new>().load_next();
    }

    int farctionShow = 0;
    void loopThroughFracValues()
    {
        switch (farctionShow % 3)
        {
            case 0:
                b2.SetActive(true);
                b3.SetActive(false);
                b4.SetActive(false);
                b5.SetActive(false);
                paperAnim1.SetActive(true);
                paperAnim2.SetActive(false);
                paperAnim3.SetActive(false);
                Invoke("loopThroughFracValues", 1.5f);
                break;
            case 1:
                b2.SetActive(false);
                b3.SetActive(true);
                b4.SetActive(false);
                b5.SetActive(false);
                paperAnim1.SetActive(false);
                paperAnim2.SetActive(true);
                paperAnim3.SetActive(false);
                Invoke("loopThroughFracValues", 1.5f);
                break;
            case 2:
                b2.SetActive(false);
                b3.SetActive(false);
                b4.SetActive(true);
                b5.SetActive(false);
                paperAnim1.SetActive(false);
                paperAnim2.SetActive(false);
                paperAnim3.SetActive(true);
                Invoke("loopThroughFracValues", 1.5f);
                break;

            default:
                b2.SetActive(false);
                b3.SetActive(false);
                b4.SetActive(false);
                b5.SetActive(false);
                paperAnim1.SetActive(false);
                paperAnim2.SetActive(false);
                paperAnim3.SetActive(false);
                break;
        }
        farctionShow++;

    }

    void enableFade1()
    {
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
        Invoke("nextObjective1", 3.0f);

    }
    void nextObjective1()
    {
        EnableRoPanel1();
        //Invoke("nextObjectiveVo1", 3.0f);
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadeout");
    }
    void EnableRoPanel1()
    {
        GameObject.Find("RO_Panel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("RO_Panel").transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        GameObject.Find("Background").transform.GetComponent<Image>().enabled = true;
        GameObject.FindObjectOfType<Obj16_RO_Manager>().Initiliaze();

    }

    void enableFade2()
    {
        //GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
        nextObjective2();
        //Invoke("nextObjective2", 3.0f);
    }
    void nextObjective2()
    {
        EnableRoPanel2();
        //GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadeout");
        //Invoke("nextObjectiveVo1", 3.0f);

    }
    void EnableRoPanel2()
    {
        GameObject.Find("RO_Panel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("RO_Panel").transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        GameObject.Find("RO_Panel").transform.GetChild(0).GetChild(1).gameObject.SetActive(true);

        GameObject.FindObjectOfType<Obj16_RO_Manager>().Initiliaze_RO2();
    }

    void enableFade3()
    {
        //GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
        nextObjective3();
        //Invoke("nextObjective3", 3.0f);
    }
    void nextObjective3()
    {
        EnableRoPanel3();
        //Invoke("nextObjectiveVo1", 3.0f);
        //GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadeout");

    }
    void EnableRoPanel3()
    {
        GameObject.Find("RO_Panel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("RO_Panel").transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        GameObject.Find("RO_Panel").transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        GameObject.Find("RO_Panel").transform.GetChild(0).GetChild(2).gameObject.SetActive(true);

        GameObject.FindObjectOfType<Obj16_RO_Manager>().Initiliaze_RO3();
    }

    void enableFade4()
    {
        //GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
        nextObjective4();
        //Invoke("nextObjective4", 3.0f);
    }
    void nextObjective4()
    {
        EnableRoPanel4();
        //Invoke("nextObjectiveVo1", 3.0f);
        //GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadeout");

    }
    void EnableRoPanel4()
    {
        GameObject.Find("RO_Panel").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("RO_Panel").transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        GameObject.Find("RO_Panel").transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        GameObject.Find("RO_Panel").transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
        GameObject.Find("RO_Panel").transform.GetChild(0).GetChild(3).gameObject.SetActive(true);

        GameObject.FindObjectOfType<Obj16_RO_Manager>().Initiliaze_RO4();
    }
    void enableBook3()
    {
        fold3_book3.GetComponent<BookPro>().interactable = true;
    }
    void nextObjectiveVo1()
    {

        GameObject.FindObjectOfType<timeline_new>().load_next();

    }

    void PaperFold_Unfold_Animation()
    {
        //show paper animation folding/unfolding on right and write number on parts after unfolding 
    }

    void PaperThriceAnimation()
    {
        //show thrice paper animation folding/unfolding 
    }






    void BookFlipCase_call(string cases)
    {
        StartCoroutine(Fold_manager(cases));
    }
    IEnumerator Fold_manager(string cases)
    {
        Vector3 current_scale = new Vector3(1, 1, 1);
        if (fold1)
        {
            stop_timer();
            disable_paper_fold();
            switch (cases)
            {

                case "right_manner":
                    Debug.Log("right manner");
                    yield return new WaitForSeconds(1);
                    FindObjectOfType<conversationManager>().playCorrect();
                    fold1 = false;
                    FindObjectOfType<timeline_new>().load_next();
                    break;

                case "wrong_manner":
                    Debug.Log("wrong manner");
                    yield return new WaitForSeconds(1);
                    left_to_right_curved.SetActive(true);
                    current_scale = new Vector3(-1, 1, 1);
                    left_to_right_curved.transform.localScale = Vector3.zero;
                    left_to_right_curved.transform.DOScale(current_scale, 1).SetEase(Ease.OutBounce);
                    object_to_hide = left_to_right_curved;

                    FindObjectOfType<conversationManager>().EnableConversation("Make folds so that the paper fits within the dotted lines. Try again");
                    FindObjectOfType<timeline_new>().playAudioOnRelearn("obj16_hint_fold_you_have_to_make.wav");
                    yield return new WaitForSeconds(0.6f);
                    yield return new WaitForSeconds(7);
                    left_to_right_curved.transform.DOScale(Vector3.zero, 1).OnComplete(hide_object);

                    reset_fold();

                    break;

                case "wrong_end":
                    yield return new WaitForSeconds(1);
                    Debug.Log("wrong end");
                    fold_from_here_arrow.SetActive(true);
                    // current_scale = fold_from_here_arrow.transform.localScale;
                    fold_from_here_arrow.transform.localScale = Vector3.zero;
                    fold_from_here_arrow.transform.DOScale(current_scale, 1).SetEase(Ease.OutBounce);
                    object_to_hide = fold_from_here_arrow;
                    FindObjectOfType<conversationManager>().EnableConversation("If you fold from this end you will not be able to fold the paper in the highlighted area. Try another end");
                    FindObjectOfType<timeline_new>().playAudioOnRelearn("obj16_hint_fold_if_you_fold.wav");
                    yield return new WaitForSeconds(0.6f);
                    yield return new WaitForSeconds(7);
                    fold_from_here_arrow.transform.DOScale(Vector3.zero, 1).OnComplete(hide_object);

                    reset_fold();
                    break;

                case "show_timer_popup":

                    yield return new WaitForSeconds(1);

                    FindObjectOfType<conversationManager>().EnableConversation("Go ahead and fold the paper to perfectly fit within the dotted lines. ");
                    FindObjectOfType<timeline_new>().playAudioOnRelearn("obj16_hint_fold1_go_ahead.wav");
                    //fold1_book.SetActive(false);
                    Fold1_first_fold_hint.SetActive(true);
                    left_to_right_curved.SetActive(true);
                    current_scale = new Vector3(-1, 1, 1);
                    left_to_right_curved.transform.localScale = Vector3.zero;
                    left_to_right_curved.transform.DOScale(current_scale, 1).SetEase(Ease.OutBounce);
                    object_to_hide = left_to_right_curved;
                    yield return new WaitForSeconds(0.4f);
                    //Fold1_first_fold_hint.GetComponent<AutoFlip>().StartFlipping();
                    yield return new WaitForSeconds(0.6f);
                    yield return new WaitForSeconds(7);
                    left_to_right_curved.transform.DOScale(Vector3.zero, 1).OnComplete(hide_object);
                    Fold1_first_fold_hint.SetActive(false);
                    reset_fold();

                    //fold1_book.SetActive(true);
                    reset_fold();
                    break;


            }
        }
        else if (fold2)
        {
            stop_timer();
            disable_paper_fold();
            switch (cases)
            {
                case "right_manner":
                    Debug.Log("right manner");
                    yield return new WaitForSeconds(1);

                    if (fold2_count == 0)
                    {
                        fold2_book1.SetActive(false);
                        fold2_book2.SetActive(true);
                    }
                    else
                    {
                        fold2 = false;
                        //fold2_book2.SetActive(false);
                        //fold2_unfold_page.SetActive(true);
                        FindObjectOfType<conversationManager>().playCorrect();
                        FindObjectOfType<timeline_new>().load_next();
                    }
                    fold2_count++;
                    start_timer();



                    break;

                case "wrong_manner":
                    Debug.Log("wrong manner");
                    yield return new WaitForSeconds(1);
                    if (fold2_count == 0)
                    {
                        fold2_left_to_right_curved1.SetActive(true);
                        current_scale = new Vector3(-1, 1, 1);
                        fold2_left_to_right_curved1.transform.localScale = Vector3.zero;
                        fold2_left_to_right_curved1.transform.DOScale(current_scale, 1).SetEase(Ease.OutBounce);
                        object_to_hide = fold2_left_to_right_curved1;

                        FindObjectOfType<conversationManager>().EnableConversation("Make folds so that the paper fits within the dotted lines. Try again");
                        FindObjectOfType<timeline_new>().playAudioOnRelearn("obj16_hint_fold_you_have_to_make.wav");
                        yield return new WaitForSeconds(0.6f);
                        yield return new WaitForSeconds(7);
                        fold2_left_to_right_curved1.transform.DOScale(Vector3.zero, 1).OnComplete(hide_object);
                    }
                    else
                    {
                        FindObjectOfType<conversationManager>().EnableConversation("Make folds so that the paper fits within the dotted lines. Try again");
                        FindObjectOfType<timeline_new>().playAudioOnRelearn("obj16_hint_fold_you_have_to_make.wav");
                        Fold2_second_fold_hint.SetActive(true);
                        fold2_left_to_right_curved2.SetActive(true);
                        current_scale = new Vector3(-1, 1, 1);
                        fold2_left_to_right_curved2.transform.localScale = Vector3.zero;
                        fold2_left_to_right_curved2.transform.DOScale(current_scale, 1).SetEase(Ease.OutBounce);
                        object_to_hide = fold2_left_to_right_curved2;
                        yield return new WaitForSeconds(0.4f);
                        //Fold2_second_fold_hint.GetComponent<AutoFlip>().StartFlipping();
                        yield return new WaitForSeconds(0.6f);
                        yield return new WaitForSeconds(7);
                        fold2_left_to_right_curved2.transform.DOScale(Vector3.zero, 1).OnComplete(hide_object);
                        reset_fold();
                        Fold2_second_fold_hint.SetActive(false);
                        fold2_book2.SetActive(true);
                        reset_fold();
                    }
                    reset_fold();

                    break;

                case "wrong_end":
                    if (fold2_count == 0)
                    {
                        yield return new WaitForSeconds(1);
                        Debug.Log("wrong end");
                        fold1_from_here_arrow.SetActive(true);
                        // current_scale = fold_from_here_arrow.transform.localScale;
                        fold1_from_here_arrow.transform.localScale = Vector3.zero;
                        fold1_from_here_arrow.transform.DOScale(current_scale, 3).SetEase(Ease.OutBounce);
                        object_to_hide = fold1_from_here_arrow;
                        FindObjectOfType<conversationManager>().EnableConversation("If you fold from this end you will not be able to fold the paper in the highlighted area. Try another end");
                        FindObjectOfType<timeline_new>().playAudioOnRelearn("obj16_hint_fold_if_you_fold.wav");
                        yield return new WaitForSeconds(0.6f);
                        yield return new WaitForSeconds(7);
                        fold1_from_here_arrow.transform.DOScale(Vector3.zero, 3).OnComplete(hide_object);

                        reset_fold();
                    }
                    else
                    {
                        yield return new WaitForSeconds(1);
                        Debug.Log("wrong end");
                        fold2_from_here_arrow.SetActive(true);
                        // current_scale = fold_from_here_arrow.transform.localScale;
                        fold2_from_here_arrow.transform.localScale = Vector3.zero;
                        fold2_from_here_arrow.transform.DOScale(current_scale, 3).SetEase(Ease.OutBounce);
                        object_to_hide = fold2_from_here_arrow;
                        FindObjectOfType<conversationManager>().EnableConversation("If you fold from this end you will not be able to fold the paper in the highlighted area. Try another end");
                        FindObjectOfType<timeline_new>().playAudioOnRelearn("obj16_hint_fold_if_you_fold.wav");
                        yield return new WaitForSeconds(0.6f);
                        yield return new WaitForSeconds(7);
                        fold2_from_here_arrow.transform.DOScale(Vector3.zero, 3).OnComplete(hide_object);


                        reset_fold();
                    }

                    break;

                case "show_timer_popup":

                    yield return new WaitForSeconds(1);
                    if (fold2_count == 0)
                    {
                        FindObjectOfType<conversationManager>().EnableConversation("Go ahead and fold the paper two times such that the paper fits  within the dotted lines.");
                        FindObjectOfType<timeline_new>().playAudioOnRelearn("obj16_hint_fold2_go_ahead_not_fold_once.wav");
                        //fold2_book1.SetActive(false);
                        Fold2_first_fold_hint.SetActive(true);
                        fold2_left_to_right_curved1.SetActive(true);
                        current_scale = new Vector3(-1, 1, 1);
                        fold2_left_to_right_curved1.transform.localScale = Vector3.zero;
                        fold2_left_to_right_curved1.transform.DOScale(current_scale, 1).SetEase(Ease.OutBounce);
                        object_to_hide = fold2_left_to_right_curved1;
                        yield return new WaitForSeconds(0.4f);
                        //Fold2_first_fold_hint.GetComponent<AutoFlip>().StartFlipping();
                        yield return new WaitForSeconds(0.6f);
                        yield return new WaitForSeconds(7);
                        fold2_left_to_right_curved1.transform.DOScale(Vector3.zero, 1).OnComplete(hide_object);
                        reset_fold();
                        Fold2_first_fold_hint.SetActive(false);
                        fold2_book1.SetActive(true);
                        reset_fold();
                    }
                    else
                    {
                        FindObjectOfType<conversationManager>().EnableConversation("You have folded the paper once. Now go ahead and make the second fold. Ensure the paper fits within the dotted lines.");
                        FindObjectOfType<timeline_new>().playAudioOnRelearn("obj16_hint_fold2_go_ahead_not_fold_second.wav");
                        //fold2_book2.SetActive(false);
                        Fold2_second_fold_hint.SetActive(true);
                        fold2_left_to_right_curved2.SetActive(true);
                        current_scale = new Vector3(-1, 1, 1);
                        fold2_left_to_right_curved2.transform.localScale = Vector3.zero;
                        fold2_left_to_right_curved2.transform.DOScale(current_scale, 1).SetEase(Ease.OutBounce);
                        object_to_hide = fold2_left_to_right_curved2;
                        yield return new WaitForSeconds(0.4f);
                        //Fold2_second_fold_hint.GetComponent<AutoFlip>().StartFlipping();
                        yield return new WaitForSeconds(0.6f);
                        yield return new WaitForSeconds(7);
                        fold2_left_to_right_curved2.transform.DOScale(Vector3.zero, 1).OnComplete(hide_object);
                        reset_fold();
                        Fold2_second_fold_hint.SetActive(false);
                        fold2_book2.SetActive(true);
                        reset_fold();

                    }
                    break;
            }

        }
        else if (fold3)
        {
            stop_timer();
            disable_paper_fold();
            switch (cases)
            {
                case "right_manner":
                    Debug.Log("right manner");
                    yield return new WaitForSeconds(1);

                    if (fold3_count == 0)
                    {
                        GameObject.FindGameObjectWithTag("dotted line").SetActive(false);
                        fold3_book1.SetActive(false);
                        fold3_book2.SetActive(true);
                        fold3_book3.SetActive(true);
                        fold3_book2.GetComponent<BookPro>().interactable = true;
                        fold3_book3.GetComponent<BookPro>().interactable = true;
                        Invoke("enableBook3", 1.0f);
                    }

                    else if (fold3_count > 1)
                    {
                        fold3 = false;
                        //fold2_book2.SetActive(false);
                        //fold2_unfold_page.SetActive(true);
                        FindObjectOfType<conversationManager>().playCorrect();
                        FindObjectOfType<timeline_new>().load_next();
                    }
                    fold3_count++;
                    start_timer();


                    break;

                case "wrong_manner":
                    Debug.Log("wrong manner");
                    yield return new WaitForSeconds(1);
                    if (fold3_count == 0)
                    {
                        fold3_left_to_right_curved1.SetActive(true);
                        current_scale = new Vector3(-1, 1, 1);
                        fold3_left_to_right_curved1.transform.localScale = Vector3.zero;
                        fold3_left_to_right_curved1.transform.DOScale(current_scale, 1).SetEase(Ease.OutBounce);
                        object_to_hide = fold3_left_to_right_curved1;

                        FindObjectOfType<conversationManager>().EnableConversation("Make folds so that the paper fits within the dotted lines. Try again");
                        FindObjectOfType<timeline_new>().playAudioOnRelearn("obj16_hint_fold_you_have_to_make.wav");
                        yield return new WaitForSeconds(0.6f);
                        yield return new WaitForSeconds(7);
                        fold3_left_to_right_curved1.transform.DOScale(Vector3.zero, 1).OnComplete(hide_object);
                    }
                    else if (fold3_count == 1)
                    {
                        FindObjectOfType<conversationManager>().EnableConversation("Make folds so that the paper fits within the dotted lines. Try again");
                        FindObjectOfType<timeline_new>().playAudioOnRelearn("obj16_hint_fold_you_have_to_make.wav");
                        //fold2_book2.SetActive(false);
                        GameObject d = temp;
                        if (temp.name == "fold3_book3")
                        {
                            Fold3_second_fold_hint.SetActive(true);
                            fold3_left_to_right_curved2.SetActive(true);
                            current_scale = new Vector3(-1, 1, 1);
                            fold3_left_to_right_curved2.transform.localScale = Vector3.zero;
                            fold3_left_to_right_curved2.transform.DOScale(current_scale, 1).SetEase(Ease.OutBounce);
                            object_to_hide = fold3_left_to_right_curved2;
                            yield return new WaitForSeconds(0.4f);
                            //Fold2_second_fold_hint.GetComponent<AutoFlip>().StartFlipping();
                            yield return new WaitForSeconds(0.6f);
                            yield return new WaitForSeconds(7);
                            fold3_left_to_right_curved2.transform.DOScale(Vector3.zero, 1).OnComplete(hide_object);
                            Fold3_second_fold_hint.SetActive(false);
                            fold3_book3.SetActive(true);
                        }
                        else
                        {

                            Fold3_third_fold_hint.SetActive(true);
                            fold3_left_to_right_curved3.SetActive(true);
                            current_scale = new Vector3(-1, 1, 1);
                            fold3_left_to_right_curved3.transform.localScale = Vector3.zero;
                            fold3_left_to_right_curved3.transform.DOScale(current_scale, 1).SetEase(Ease.OutBounce);
                            object_to_hide = fold3_left_to_right_curved3;
                            yield return new WaitForSeconds(0.4f);
                            //Fold2_second_fold_hint.GetComponent<AutoFlip>().StartFlipping();
                            yield return new WaitForSeconds(0.6f);
                            yield return new WaitForSeconds(7);
                            fold3_left_to_right_curved3.transform.DOScale(Vector3.zero, 1).OnComplete(hide_object);
                            Fold3_third_fold_hint.SetActive(false);
                            fold3_book2.SetActive(true);
                        }
                        reset_fold();

                    }
                    else if (fold3_count == 2)
                    {
                        FindObjectOfType<conversationManager>().EnableConversation("Make folds so that the paper fits within the dotted lines. Try again");
                        FindObjectOfType<timeline_new>().playAudioOnRelearn("obj16_hint_fold_you_have_to_make.wav");
                        //fold2_book2.SetActive(false);

                        GameObject d = GameObject.Find("fold3_book3");
                        if (d != null)
                        {
                            Fold3_second_fold_hint.SetActive(true);
                            fold3_left_to_right_curved2.SetActive(true);
                            current_scale = new Vector3(-1, 1, 1);
                            fold3_left_to_right_curved2.transform.localScale = Vector3.zero;
                            fold3_left_to_right_curved2.transform.DOScale(current_scale, 1).SetEase(Ease.OutBounce);
                            object_to_hide = fold3_left_to_right_curved2;
                            yield return new WaitForSeconds(0.4f);
                            //Fold2_second_fold_hint.GetComponent<AutoFlip>().StartFlipping();
                            yield return new WaitForSeconds(0.6f);
                            yield return new WaitForSeconds(7);
                            fold3_left_to_right_curved2.transform.DOScale(Vector3.zero, 1).OnComplete(hide_object);
                            Fold3_second_fold_hint.SetActive(false);
                            fold3_book3.SetActive(true);
                        }
                        else
                        {

                            Fold3_third_fold_hint.SetActive(true);
                            fold3_left_to_right_curved3.SetActive(true);
                            current_scale = new Vector3(-1, 1, 1);
                            fold3_left_to_right_curved3.transform.localScale = Vector3.zero;
                            fold3_left_to_right_curved3.transform.DOScale(current_scale, 1).SetEase(Ease.OutBounce);
                            object_to_hide = fold3_left_to_right_curved3;
                            yield return new WaitForSeconds(0.4f);
                            //Fold2_second_fold_hint.GetComponent<AutoFlip>().StartFlipping();
                            yield return new WaitForSeconds(0.6f);
                            yield return new WaitForSeconds(7);
                            fold3_left_to_right_curved3.transform.DOScale(Vector3.zero, 1).OnComplete(hide_object);
                            Fold3_third_fold_hint.SetActive(false);
                            fold3_book2.SetActive(true);
                        }
                    }
                    else
                    {
                        FindObjectOfType<conversationManager>().EnableConversation("That does not seem right. Try again one more time. ");
                        FindObjectOfType<timeline_new>().playAudioOnRelearn("obj16_hint_fold1_try_again_one_more.wav");
                        yield return new WaitForSeconds(0.6f);
                        yield return new WaitForSeconds(7);
                    }
                    reset_fold();

                    break;

                case "wrong_end":
                    if (fold3_count == 0)
                    {
                        yield return new WaitForSeconds(1);
                        Debug.Log("wrong end");
                        fold3_from_here_arrow.SetActive(true);
                        // current_scale = fold_from_here_arrow.transform.localScale;
                        fold3_from_here_arrow.transform.localScale = Vector3.zero;
                        fold3_from_here_arrow.transform.DOScale(current_scale, 3).SetEase(Ease.OutBounce);
                        object_to_hide = fold3_from_here_arrow;
                        FindObjectOfType<conversationManager>().EnableConversation("If you fold from this end you will not be able to fold the paper in the highlighted area. Try another end");
                        FindObjectOfType<timeline_new>().playAudioOnRelearn("obj16_hint_fold_if_you_fold.wav");
                        yield return new WaitForSeconds(0.6f);
                        yield return new WaitForSeconds(7);
                        fold3_from_here_arrow.transform.DOScale(Vector3.zero, 3).OnComplete(hide_object);

                        reset_fold();
                    }
                    else
                    {
                        yield return new WaitForSeconds(1);
                        Debug.Log("wrong end");
                        fold4_from_here_arrow.SetActive(true);
                        // current_scale = fold_from_here_arrow.transform.localScale;
                        fold4_from_here_arrow.transform.localScale = Vector3.zero;
                        fold4_from_here_arrow.transform.DOScale(current_scale, 3).SetEase(Ease.OutBounce);
                        object_to_hide = fold2_from_here_arrow;
                        FindObjectOfType<conversationManager>().EnableConversation("If you fold from this end you will not be able to fold the paper in the highlighted area. Try another end");
                        FindObjectOfType<timeline_new>().playAudioOnRelearn("obj16_hint_fold_if_you_fold.wav");
                        yield return new WaitForSeconds(0.6f);
                        yield return new WaitForSeconds(7);
                        fold4_from_here_arrow.transform.DOScale(Vector3.zero, 3).OnComplete(hide_object);

                        reset_fold();
                    }

                    break;

                case "show_timer_popup":

                    yield return new WaitForSeconds(1);
                    if (fold3_count == 0)
                    {
                        FindObjectOfType<conversationManager>().EnableConversation("Go ahead and fold the paper two times such that the paper fits  within the dotted lines.");
                        FindObjectOfType<timeline_new>().playAudioOnRelearn("obj16_hint_fold2_go_ahead_not_fold_once.wav");
                        //fold2_book1.SetActive(false);
                        Fold3_first_fold_hint.SetActive(true);
                        fold3_left_to_right_curved1.SetActive(true);
                        current_scale = new Vector3(-1, 1, 1);
                        fold3_left_to_right_curved1.transform.localScale = Vector3.zero;
                        fold3_left_to_right_curved1.transform.DOScale(current_scale, 1).SetEase(Ease.OutBounce);
                        object_to_hide = fold3_left_to_right_curved1;
                        yield return new WaitForSeconds(0.4f);
                        //Fold2_first_fold_hint.GetComponent<AutoFlip>().StartFlipping();
                        yield return new WaitForSeconds(0.6f);
                        yield return new WaitForSeconds(7);
                        fold3_left_to_right_curved1.transform.DOScale(Vector3.zero, 1).OnComplete(hide_object);
                        reset_fold();
                        Fold3_first_fold_hint.SetActive(false);
                        fold3_book1.SetActive(true);
                        reset_fold();
                    }
                    else if (fold3_count == 1)
                    {
                        FindObjectOfType<conversationManager>().EnableConversation("You have folded the paper once. Now go ahead and make the second fold. Ensure the paper fits within the dotted lines.");
                        FindObjectOfType<timeline_new>().playAudioOnRelearn("obj16_hint_fold2_go_ahead_not_fold_second.wav");
                        //fold2_book2.SetActive(false);
                        GameObject d = GameObject.Find("fold3_book3");
                        if (d != null)
                        {
                            Fold3_second_fold_hint.SetActive(true);
                            fold3_left_to_right_curved2.SetActive(true);
                            current_scale = new Vector3(-1, 1, 1);
                            fold3_left_to_right_curved2.transform.localScale = Vector3.zero;
                            fold3_left_to_right_curved2.transform.DOScale(current_scale, 1).SetEase(Ease.OutBounce);
                            object_to_hide = fold3_left_to_right_curved2;
                            yield return new WaitForSeconds(0.4f);
                            //Fold2_second_fold_hint.GetComponent<AutoFlip>().StartFlipping();
                            yield return new WaitForSeconds(0.6f);
                            yield return new WaitForSeconds(7);
                            fold3_left_to_right_curved2.transform.DOScale(Vector3.zero, 1).OnComplete(hide_object);
                            Fold3_second_fold_hint.SetActive(false);
                            fold3_book3.SetActive(true);
                        }
                        else
                        {

                            Fold3_third_fold_hint.SetActive(true);
                            fold3_left_to_right_curved3.SetActive(true);
                            current_scale = new Vector3(-1, 1, 1);
                            fold3_left_to_right_curved3.transform.localScale = Vector3.zero;
                            fold3_left_to_right_curved3.transform.DOScale(current_scale, 1).SetEase(Ease.OutBounce);
                            object_to_hide = fold3_left_to_right_curved3;
                            yield return new WaitForSeconds(0.4f);
                            //Fold2_second_fold_hint.GetComponent<AutoFlip>().StartFlipping();
                            yield return new WaitForSeconds(0.6f);
                            yield return new WaitForSeconds(7);
                            fold3_left_to_right_curved3.transform.DOScale(Vector3.zero, 1).OnComplete(hide_object);
                            Fold3_third_fold_hint.SetActive(false);
                            fold3_book2.SetActive(true);
                        }
                        reset_fold();

                    }
                    else if (fold3_count == 2)
                    {
                        FindObjectOfType<conversationManager>().EnableConversation("You have folded the paper once. Now go ahead and make the second fold. Ensure the paper fits within the dotted lines.");
                        FindObjectOfType<timeline_new>().playAudioOnRelearn("obj16_hint_fold2_go_ahead_not_fold_second.wav");
                        //fold2_book2.SetActive(false);

                        GameObject d = GameObject.Find("fold3_book3");
                        if (d != null)
                        {
                            Fold3_second_fold_hint.SetActive(true);
                            fold3_left_to_right_curved2.SetActive(true);
                            current_scale = new Vector3(-1, 1, 1);
                            fold3_left_to_right_curved2.transform.localScale = Vector3.zero;
                            fold3_left_to_right_curved2.transform.DOScale(current_scale, 1).SetEase(Ease.OutBounce);
                            object_to_hide = fold3_left_to_right_curved2;
                            yield return new WaitForSeconds(0.4f);
                            //Fold2_second_fold_hint.GetComponent<AutoFlip>().StartFlipping();
                            yield return new WaitForSeconds(0.6f);
                            yield return new WaitForSeconds(7);
                            fold3_left_to_right_curved2.transform.DOScale(Vector3.zero, 1).OnComplete(hide_object);
                            Fold3_second_fold_hint.SetActive(false);
                            fold3_book3.SetActive(true);
                        }
                        else
                        {

                            Fold3_third_fold_hint.SetActive(true);
                            fold3_left_to_right_curved3.SetActive(true);
                            current_scale = new Vector3(-1, 1, 1);
                            fold3_left_to_right_curved3.transform.localScale = Vector3.zero;
                            fold3_left_to_right_curved3.transform.DOScale(current_scale, 1).SetEase(Ease.OutBounce);
                            object_to_hide = fold3_left_to_right_curved3;
                            yield return new WaitForSeconds(0.4f);
                            //Fold2_second_fold_hint.GetComponent<AutoFlip>().StartFlipping();
                            yield return new WaitForSeconds(0.6f);
                            yield return new WaitForSeconds(7);
                            fold3_left_to_right_curved3.transform.DOScale(Vector3.zero, 1).OnComplete(hide_object);
                            Fold3_third_fold_hint.SetActive(false);
                            fold3_book2.SetActive(true);
                        }
                        reset_fold();


                        reset_fold();

                    }
                    break;
            }
        }
        yield return null;
    }

    void fold_the_paper()
    {
        temp_tut.GetComponent<AutoFlip>().enabled = true;
        tut = true;
    }
    public void fold2_unfold_functionality()
    {
        fold2_unfold_page.GetComponent<AutoFlip>().enabled = true;
        fold1_dotted_line.SetActive(false);
        fold2_dotted_line.SetActive(false);
    }
    public void fold3_unfold_functionality()
    {
        GameObject.FindGameObjectWithTag("book1").SetActive(false);
        fold3_dotted_line.SetActive(false);
        fold3_unfold_page.SetActive(true);
        fold3_unfold_page.GetComponent<AutoFlip>().enabled = true;
    }
    public void UnHighlightBack()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("marker"))
            g.transform.GetChild(0).gameObject.SetActive(false);
    }
    public void reset_fold()
    {
        if (fold1)
        {
            start_timer();
            Fold1_first_fold_hint.SetActive(false);
            Fold2_first_fold_hint.SetActive(false);
            CancelInvoke();
            FindObjectOfType<conversationManager>().DisableHint();
            FindObjectOfType<BookPro>().currentPaper = 1;
            FindObjectOfType<BookPro>().UpdatePages();

            enable_paper_fold();
        }
        if (fold2)
        {
            if (fold2_count == 0)
            {
                start_timer();

                Fold2_first_fold_hint.SetActive(false);
                CancelInvoke();
                FindObjectOfType<conversationManager>().DisableHint();
                fold2_book1.GetComponent<BookPro>().currentPaper = 1;
                fold2_book1.GetComponent<BookPro>().UpdatePages();

                enable_paper_fold();
            }
            else
            {
                start_timer();

                Fold2_second_fold_hint.SetActive(false);
                CancelInvoke();
                FindObjectOfType<conversationManager>().DisableHint();
                fold2_book2.GetComponent<BookPro>().currentPaper = 1;
                fold2_book2.GetComponent<BookPro>().UpdatePages();

                enable_paper_fold();
            }
        }
        if (fold3)
        {
            if (fold3_count == 0)
            {
                start_timer();

                Fold3_first_fold_hint.SetActive(false);
                CancelInvoke();
                FindObjectOfType<conversationManager>().DisableHint();
                fold3_book1.GetComponent<BookPro>().currentPaper = 1;
                fold3_book1.GetComponent<BookPro>().UpdatePages();

                enable_paper_fold();
            }
            else if (fold3_count == 1)
            {

                start_timer();

                Fold3_second_fold_hint.SetActive(false);
                CancelInvoke();
                FindObjectOfType<conversationManager>().DisableHint();
                foreach (GameObject g in GameObject.FindGameObjectsWithTag("book"))
                {

                    g.GetComponent<BookPro>().currentPaper = 1;
                    g.GetComponent<BookPro>().UpdatePages();
                }


                enable_paper_fold();
            }
            else
            {

                start_timer();

                Fold3_second_fold_hint.SetActive(false);
                CancelInvoke();
                FindObjectOfType<conversationManager>().DisableHint();
                foreach (GameObject g in GameObject.FindGameObjectsWithTag("book1"))
                {

                    g.GetComponent<BookPro>().currentPaper = 1;
                    g.GetComponent<BookPro>().UpdatePages();
                }

                enable_paper_fold();
            }
        }
        //UnHighlightBack();
    }

    void hide_object()
    {
        object_to_hide.SetActive(false);
    }

    void disable_paper_fold()
    {
        var d = FindObjectsOfType<BookPro>();
        foreach (BookPro s in d)
        {

            s.interactable = false;
        }
    }

    void enable_paper_fold()
    {
        var d = FindObjectsOfType<BookPro>();
        foreach (BookPro s in d)
        {
            s.interactable = true;
        }
    }

    private void Update()
    {

        if ((fold1 || fold2 || fold3) && istimer_on)
        {
            timer_for_fold = timer_for_fold - Time.deltaTime;
            if (timer_for_fold < 0)
            {

                StartCoroutine(Fold_manager("show_timer_popup"));
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FindObjectOfType<timeline_new>().load_next();
        }
    }


    public void stop_timer()
    {
        istimer_on = false;
        timer_for_fold = timer_length;
    }
    public void start_timer()
    {
        istimer_on = true;
        timer_for_fold = timer_length;
    }



    public void move_fold1_to_corner()
    {
        Fold_1.transform.DOMove(fold1_pos.transform.position, 2).SetEase(Ease.OutBounce);
        Fold_1.transform.DOScale(new Vector3(0.3f, 0.3f, 0.3f), 2).SetEase(Ease.OutBounce).OnComplete(load_next);
    }

    public void move_fold2_to_corner()
    {
        Fold_2.transform.DOMove(fold2_pos.transform.position, 2).SetEase(Ease.OutBounce);
        Fold_2.transform.DOScale(new Vector3(0.3f, 0.3f, 0.3f), 2).SetEase(Ease.OutBounce).OnComplete(load_next);
    }

    void click_on_unfold()
    {

    }

}
