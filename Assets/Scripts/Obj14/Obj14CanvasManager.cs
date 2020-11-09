using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Obj14CanvasManager : MonoBehaviour
{
    // Start is called before the first frame update
    Image imgWhole1, imgWhole2, imgWhole3, imgFraction1;
    Image[] boders;
    TEXDraw[] FractionNumbers, CountNumbers;
    Transform FractionHolder;
    TEXDraw initWhole, initFraction;
    Transform divideLines,initBlock,divideTextBlock, divideTextBlockCount,ChooseFractionBlock,Mixedfractionpanel,FractionAnswer,ImproperfractionBlock;
    int imageNumber = 0;
    bool flashOutline = false;
    Button btn0, btn1, btn2, btn3, btn4, btn5, btn6, btn7, btn8, btn9;
    InputField inputField_Num;
    public GameObject hintPopup_Pnl, hintMsg_Text;
    public Button hintOkayBtn, submitAnswerButton;
    Button btnProperfraction, btnImProperfraction, btnMixedfraction, btnUnitfraction;
    bool isNumerator;
    int numeratorValue;
    Transform loading;
    // easyMethord
    public Transform easyMethordPanel, easyMixedFractionNumber, arrowMultiply, arrawAdd, easyMustiplyStep1, easyMultiplayStep2, easyAddStep1, easyAddStep2, easyFinalStep1, easyFinalStep2, easyFinalStep3, easyAddImage, easyMulImage;
    public GameObject mixedImproper_Pnl, RO_Panel;

    public delegate void CanvasJobDone(string Job);
    public static event CanvasJobDone OnCanvasJobDone;
    public GameObject RO_Pnl_1, RO_Pnl_2, RO_Pnl_3, RO_Pnl_4, RO_Pnl_5, RO_Pnl_6;

    void Start()
    {
        Initilize();
        Sub_ROPanels();
    }

    void Sub_ROPanels()
    {
        RO_Pnl_1 = GameObject.Find("Canvas/RO Panel/Panel/Obj14_RO_1").gameObject;
        RO_Pnl_2 = GameObject.Find("Canvas/RO Panel/Panel/ROType1").gameObject;
        RO_Pnl_3 = GameObject.Find("Canvas/RO Panel/Panel/ROType1 (1)").gameObject;
        RO_Pnl_4 = GameObject.Find("Canvas/RO Panel/Panel/ROType1 (2)").gameObject;
        RO_Pnl_5= GameObject.Find("Canvas/RO Panel/Panel/ROType1 (3)").gameObject;
        RO_Pnl_6 = GameObject.Find("Canvas/RO Panel/Panel/ROType1 (4)").gameObject;

    }

    


    
    void Initilize()
    {
        loading = transform.GetChildFromName<Transform>("Loading");
        divideLines = transform.GetChildFromName<Transform>("DivideLines");
        boders = divideLines.GetComponentsInChildren<Image>();
        initWhole = transform.GetChildFromName<TEXDraw>("wholeNumber");
        initFraction = transform.GetChildFromName<TEXDraw>("FractionPart");
        initBlock = transform.GetChildFromName<Transform>("InitNumber");
        divideTextBlock = transform.GetChildFromName<Transform>("Parts_Div");
        FractionNumbers = divideTextBlock.GetComponentsInChildren<TEXDraw>();
        divideTextBlockCount = transform.GetChildFromName<Transform>("NumberCount");
        CountNumbers = divideTextBlockCount.GetComponentsInChildren<TEXDraw>();
        imgWhole1 = transform.GetChildFromName<Image>("ImageWhole1");
        imgWhole2 = transform.GetChildFromName<Image>("ImageWhole2");
        imgWhole3 = transform.GetChildFromName<Image>("ImageWhole3");
        imgFraction1 = transform.GetChildFromName<Image>("ImageFraction1");
        ChooseFractionBlock = transform.GetChildFromName<Transform>("ChooseFraction");
        Mixedfractionpanel = transform.GetChildFromName<Transform>("MixedfractionBoderPanel");
        FractionAnswer = transform.GetChildFromName<Transform>("AnswerFrac");
        ImproperfractionBlock = transform.GetChildFromName<Transform>("BoderPanel");
        mixedImproper_Pnl = GameObject.Find("Canvas/MixedImproperFraction_Panel");
        RO_Panel = GameObject.Find("Canvas/RO Panel");
        HideInitBlock();
        HideAllBoders();
        HideAllFractText();
        HideAllCountText();
        HideFractionAnswer();
        //numberpad
        // numberpad
        btn0 = transform.GetChildFromName<Button>("00");
        btn1 = transform.GetChildFromName<Button>("01");
        btn2 = transform.GetChildFromName<Button>("02");
        btn3 = transform.GetChildFromName<Button>("03");
        btn4 = transform.GetChildFromName<Button>("04");
        btn5 = transform.GetChildFromName<Button>("05");
        btn6 = transform.GetChildFromName<Button>("06");
        btn7 = transform.GetChildFromName<Button>("07");
        btn8 = transform.GetChildFromName<Button>("08");
        btn9 = transform.GetChildFromName<Button>("09");
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

        inputField_Num = transform.GetChildFromName<InputField>("InputField_Numerator");
        EventTrigger.Entry entryNum = new EventTrigger.Entry();
        entryNum.callback.AddListener((eventData) => { OnNumeratorField((PointerEventData)eventData); });
        inputField_Num.gameObject.GetComponent<EventTrigger>().triggers.Add(entryNum);

        submitAnswerButton = transform.GetChildFromName<Button>("SubmitButton"); 
        submitAnswerButton.onClick.AddListener(() => OnAnswerSubmit());
        btnProperfraction = transform.GetChildFromName<Button>("ProperfractionButton");
        btnProperfraction.onClick.AddListener(() => ChooseFractionInCorrect());
        btnImProperfraction = transform.GetChildFromName<Button>("ImproperfractionButton");
        btnImProperfraction.onClick.AddListener(() => ChooseFractionCorrect());
        btnMixedfraction = transform.GetChildFromName<Button>("MixedfractionButton");
        btnMixedfraction.onClick.AddListener(() => ChooseFractionInCorrect());
        btnUnitfraction = transform.GetChildFromName<Button>("UnitfractionButton");
        btnUnitfraction.onClick.AddListener(() => ChooseFractionInCorrect());
        HideNumberInputPanel();
        HideFractionChoosingPanel();
        HideMixedFractionpanel();
        HideImproperFractionBlock();
        InitEasy();
        InitializeDialoguePanl();


    }

    void InitializeDialoguePanl()
    {
        hintPopup_Pnl = GameObject.Find("Canvas/Dialouge").transform.GetChild(0).gameObject;
        hintMsg_Text = hintPopup_Pnl.transform.GetChild(1).gameObject;
        hintOkayBtn = hintPopup_Pnl.transform.GetChild(4).gameObject.GetComponent<Button>();

        hintOkayBtn.onClick.RemoveAllListeners();
        hintOkayBtn.onClick.AddListener(Hint_ok_functionality);
    }

    public void set_dialougue(string message)
    {
        hintPopup_Pnl.SetActive(true);
        if (hintMsg_Text != null)
        {
            hintMsg_Text.GetComponent<TEXDraw>().text = message;
        }
    }

    public void Hint_ok_functionality()
    {
        FindObjectOfType<timeline_new>().lipsync_player.GetComponent<AudioSource>().Stop();
        hintPopup_Pnl.SetActive(false);

        if (OnCanvasJobDone != null)
            OnCanvasJobDone(UtilityArtifacts.Obj14_ChoosefractionFailed);
    }

    public void HideLoading()
    {
        loading.gameObject.SetActive(false);
    }

    void InitEasy()
    {
        //easyMethordPanel  = transform.GetChildFromName<Transform>("EasyProcess");
        //easyMixedFractionNumber = transform.GetChildFromName<Transform>("EasyMixedNumber");
        //arrowMultiply = transform.GetChildFromName<Transform>("MultiplyArrow");
        //arrawAdd = transform.GetChildFromName<Transform>("AddArrow");
        //easyMustiplyStep1 = transform.GetChildFromName<Transform>("EasyMixedNumberToImproper");
        //easyMultiplayStep2 = transform.GetChildFromName<Transform>("EasyMixedNumberToImproper2");
        //easyAddStep1 = transform.GetChildFromName<Transform>("EasyMixedNumberToImproperADD");
        ////easyAddStep2 = transform.GetChildFromName<Transform>("DivideLines");
        //easyFinalStep1 = transform.GetChildFromName<Transform>("EasyMixedNumberToImproperFinal1");
        //easyFinalStep2 = transform.GetChildFromName<Transform>("EasyMixedNumberToImproperFinal2");
        //easyFinalStep3 = transform.GetChildFromName<Transform>("EasyMixedNumberToImproperFinal3");
        //easyAddImage = transform.GetChildFromName<Transform>("DenMulImage");
        //easyMulImage = transform.GetChildFromName<Transform>("WholeMulTiplayAndAdd"); //WholeMulTiplayAndAdd
        //HideEasyMethordPanel();
        //HideEasyMixedFractionNumber();
        //HideArrowMultiply();
        //HideArrawAdd();
        //HideEasyMustiplyStep1();
        //HideEasyMultiplayStep2();
        //HideEasyAddStep1();
        //HideEasyFinalStep1();
        //HideEasyFinalStep2();
        //HideEasyFinalStep3();
        //HideEasyAddImage();
        //HideEasyMulImage();
    }

    public void HideEasyMethordPanel()
    {
        easyMethordPanel.transform.gameObject.SetActive(false);
    }

    public void ShowEasyMethordPanel()
    {
        easyMethordPanel.transform.gameObject.SetActive(true);
    }

    public void HideEasyMixedFractionNumber()
    {
        easyMixedFractionNumber.transform.gameObject.SetActive(false);
    }

    public void ShowEasyMixedFractionNumber()
    {
        easyMixedFractionNumber.transform.gameObject.SetActive(true);
    }
    public void HideArrowMultiply()
    {
        arrowMultiply.transform.gameObject.SetActive(false);
    }

    public void ShowArrowMultiply()
    {
        arrowMultiply.transform.gameObject.SetActive(true);
    }
    public void HideArrawAdd()
    {
        arrawAdd.transform.gameObject.SetActive(false);
    }

    public void ShowArrawAdd()
    {
        arrawAdd.transform.gameObject.SetActive(true);
    }
    public void HideEasyMustiplyStep1()
    {
        easyMustiplyStep1.transform.gameObject.SetActive(false);
    }

    public void ShowEasyMustiplyStep1()
    {
        easyMustiplyStep1.transform.gameObject.SetActive(true);
    }
    public void HideEasyMultiplayStep2()
    {
        easyMultiplayStep2.transform.gameObject.SetActive(false);
    }

    public void ShowEasyMultiplayStep2()
    {
        easyMultiplayStep2.transform.gameObject.SetActive(true);
    }
    public void HideEasyAddStep1()
    {
        easyAddStep1.transform.gameObject.SetActive(false);
    }

    public void ShowEasyAddStep1()
    {
        easyAddStep1.transform.gameObject.SetActive(true);
    }
    public void HideEasyFinalStep1()
    {
        easyFinalStep1.transform.gameObject.SetActive(false);
    }

    public void ShowEasyFinalStep1()
    {
        easyFinalStep1.transform.gameObject.SetActive(true);
    }
    public void HideEasyFinalStep2()
    {
        easyFinalStep2.transform.gameObject.SetActive(false);
    }

    public void ShowEasyFinalStep2()
    {
        easyFinalStep2.transform.gameObject.SetActive(true);
    }
    public void HideEasyFinalStep3()
    {
        easyFinalStep3.transform.gameObject.SetActive(false);
    }

    public void ShowEasyFinalStep3()
    {
        easyFinalStep3.transform.gameObject.SetActive(true);
    }
    public void HideEasyAddImage()
    {
        easyAddImage.transform.gameObject.SetActive(false);
    }

    public void ShowEasyAddImage()
    {
        easyAddImage.transform.gameObject.SetActive(true);
    }

    public void HideEasyMulImage()
    {
        easyMulImage.transform.gameObject.SetActive(false);
    }

    public void ShowEasyMulImage()
    {
        easyMulImage.transform.gameObject.SetActive(true);
    }

    public void HideImproperFractionBlock()
    {
        ImproperfractionBlock.transform.gameObject.SetActive(false);
    }

    public void ShowImproperFractionBlock()
    {
        ImproperfractionBlock.transform.gameObject.SetActive(true);
    }

    public void HideAllObjectsPanel()
    {
        HideImproperFractionBlock();
        HideFractionAnswer();
        HideInitBlock();
        HideAllBoders();
        HideAllFractText();
        HideAllCountText();
        HideFractionAnswer();
        HideNumberInputPanel();
        HideFractionChoosingPanel();
        HideMixedFractionpanel();
    }

    public void HideFractionAnswer()
    {
        FractionAnswer.gameObject.SetActive(false);
    }

    public void ShowFractionAnswer()
    {
        FractionAnswer.gameObject.SetActive(true);
    }

    public void HideMixedFractionpanel()
    {
        Mixedfractionpanel.gameObject.SetActive(false);
    }

    public void ShowMixedFractionPanel()
    {
        Mixedfractionpanel.gameObject.SetActive(true);
    }

    void ChooseFractionCorrect()
    {
        HideFractionChoosingPanel();
        UtilityArtifacts.Obj14C_isChoiceCorrect = true;
        if (OnCanvasJobDone != null)
        {
            OnCanvasJobDone(UtilityArtifacts.Obj14_Choosefraction);
        }
    }

    void ChooseFractionInCorrect()
    {
        HideFractionChoosingPanel();
        UtilityArtifacts.Obj14C_isChoiceCorrect = false;
        if (OnCanvasJobDone != null)
        {
            OnCanvasJobDone(UtilityArtifacts.Obj14_Choosefraction);
        }
    }

    void OnNumberButton(int number)
    {
        if (isNumerator)
        {
            Debug.Log("OnNumberButton: " + number);
            numeratorValue = numeratorValue * 10 + number;
            inputField_Num.text = numeratorValue.ToString();
        }

    }

    void OnAnswerSubmit()
    {

        HideNumberInputPanel();
        UtilityArtifacts.obj14InputValue = numeratorValue;
        if (OnCanvasJobDone != null)
        {
            OnCanvasJobDone(UtilityArtifacts.Obj14_InputString);
            Debug.Log("Done for quest");
        }
        
    }

    void OnNumeratorField(PointerEventData eventData)
    {
        isNumerator = true;
        numeratorValue = 0;
        inputField_Num.text = "";
    }

    public void DisableRoPanel()
    {
        Debug.Log("OFF RO Panel");
        RO_Panel.SetActive(false);
    }
    public void HideNumberInputPanel()
    {
        btn0.gameObject.SetActive(false);
        btn1.gameObject.SetActive(false);
        btn2.gameObject.SetActive(false);
        btn3.gameObject.SetActive(false);
        btn4.gameObject.SetActive(false);
        btn5.gameObject.SetActive(false);
        btn6.gameObject.SetActive(false);
        btn7.gameObject.SetActive(false);
        btn8.gameObject.SetActive(false);
        btn9.gameObject.SetActive(false);
        inputField_Num.gameObject.SetActive(false);
        submitAnswerButton.gameObject.SetActive(false);
    }

    public void ShowNumberInputPanel()
    {
        btn0.gameObject.SetActive(true);
        btn1.gameObject.SetActive(true);
        btn2.gameObject.SetActive(true);
        btn3.gameObject.SetActive(true);
        btn4.gameObject.SetActive(true);
        btn5.gameObject.SetActive(true);
        btn6.gameObject.SetActive(true);
        btn7.gameObject.SetActive(true);
        btn8.gameObject.SetActive(true);
        btn9.gameObject.SetActive(true);
        inputField_Num.gameObject.SetActive(true);
        submitAnswerButton.gameObject.SetActive(true);
        ResetNumberInputPanel();
    }

    void ResetNumberInputPanel()
    {
        isNumerator = true;
        numeratorValue = 0;
        inputField_Num.text = "";
    }

    public void HighlightWholeNumber()
    {
        flashOutline = true;
        StartCoroutine(FlashOutline(initWhole.GetComponent<Outline>(), 0.01f, 1f,2));
        //StartCoroutine(FlashOutline(imgWhole1.GetComponent<Outline>(), 0.01f, 1f, 2));
        //StartCoroutine(FlashOutline(imgWhole2.GetComponent<Outline>(), 0.01f, 1f, 2));
        //StartCoroutine(FlashOutline(imgWhole3.GetComponent<Outline>(), 0.01f, 1f, 2));
    }

    public void StopWholeHighlightStartFractionHighlight()
    {
        StopFlashing();
        Invoke("HighlightFractionNumber", 0.2f);
    }

    public void StopFlashing()
    {
        flashOutline = false;
        StopCoroutine("FlashOutline");
    }

    public void HighlightFractionNumber()
    {
        flashOutline = true;
        //StartCoroutine(FlashOutline(initFraction.GetComponent<Outline>(), 0.01f, 1f, 2));
        StartCoroutine(FlashOutline(imgFraction1.GetComponent<Outline>(), 0.01f, 1f, 2));
    }

    public void HideInitBlock()
    {
        initBlock.gameObject.SetActive(false);

    }

    public void ShowInitBlock()
    {
        initBlock.gameObject.SetActive(true);

    }

    public void HideAllBoders()
    {
        foreach(Image i in boders)
        {
            i.gameObject.SetActive(false);
        }
    }

    public void HideAllFractText()
    {
        foreach (TEXDraw i in FractionNumbers)
        {
            i.gameObject.SetActive(false);
        }
    }

    public void HideAllCountText()
    {
        foreach (TEXDraw i in CountNumbers)
        {
            i.gameObject.SetActive(false);
        }
    }

    public void StartShowingFractionText()
    {
        StartCoroutine(ShowNextFractionText(0));
    }

    public void ShowFractionChoosingPanel()
    {
        ChooseFractionBlock.gameObject.SetActive(true);
    }

    public void HideFractionChoosingPanel()
    {
        ChooseFractionBlock.gameObject.SetActive(false);
    }

    IEnumerator ShowNextFractionText(int textNumber)
    {
        yield return new WaitForSeconds(0.2f);
        FractionNumbers[textNumber++].gameObject.SetActive(true);
        if (textNumber < FractionNumbers.Length)
            StartCoroutine(ShowNextFractionText(textNumber));
        else
        {
            if (OnCanvasJobDone != null)
            {
                OnCanvasJobDone(UtilityArtifacts.Obj14_ShowingText);
            }
        }
    }

    public void StartShowingCountText()
    {
        StartCoroutine(ShowNextCountText(0));
    }

    IEnumerator ShowNextCountText(int textNumber)
    {
        yield return new WaitForSeconds(0.2f);
        CountNumbers[textNumber++].gameObject.SetActive(true);
        if (textNumber < CountNumbers.Length)
            StartCoroutine(ShowNextCountText(textNumber));
        else
        {
            if (OnCanvasJobDone != null)
            {
                OnCanvasJobDone(UtilityArtifacts.Obj14_ShowingCountText);
            }
        }
    }

    public void StartShowingCountText_After()
    {
        StartCoroutine(ShowNextCountText_After(0));
    }

    IEnumerator ShowNextCountText_After(int textNumber)
    {
        yield return new WaitForSeconds(0.2f);
        CountNumbers[textNumber++].gameObject.SetActive(true);
        if (textNumber < CountNumbers.Length)
            StartCoroutine(ShowNextCountText_After(textNumber));
        else
        {
            if (OnCanvasJobDone != null)
            {
                OnCanvasJobDone(UtilityArtifacts.Obj14_ShowingCountText_After);
            }
        }
    }

    public void TestSeventeenCondition()
    {
        OnCanvasJobDone(UtilityArtifacts.Obj14_ShowingCountText);

    }

    public void StartShowingBorder()
    {
        imageNumber = 0;
        StartCoroutine(ShowNextImage());
    }

    IEnumerator ShowNextImage()
    {
        yield return new WaitForSeconds(0.2f);
        boders[imageNumber++].gameObject.SetActive(true);
        if(imageNumber < boders.Length)
            StartCoroutine(ShowNextImage());
        else
        {
            if (OnCanvasJobDone != null)
            {
                OnCanvasJobDone(UtilityArtifacts.Obj14_ShowingBoard);
            }
        }
    }

    public IEnumerator FlashSprite(SpriteRenderer renderer, float minAlpha, float maxAlpha, float interval, float duration)
    {
        Color colorNow = renderer.color;
        Color minColor = new Color(renderer.color.r, renderer.color.g, renderer.color.b, minAlpha);
        Color maxColor = new Color(renderer.color.r, renderer.color.g, renderer.color.b, maxAlpha);

        float currentInterval = 0;
        while (duration > 0)
        {
            float tColor = currentInterval / interval;
            renderer.color = Color.Lerp(minColor, maxColor, tColor);

            currentInterval += Time.deltaTime;
            if (currentInterval >= interval)
            {
                Color temp = minColor;
                minColor = maxColor;
                maxColor = temp;
                currentInterval = currentInterval - interval;
            }
            duration -= Time.deltaTime;
            yield return null;
        }

        renderer.color = colorNow;
    }


    public IEnumerator FlashOutline(Outline renderer, float minAlpha, float maxAlpha, float interval)
    {
        Color colorNow = renderer.effectColor;
        Color minColor = new Color(renderer.effectColor.r, renderer.effectColor.g, renderer.effectColor.b, minAlpha);
        Color maxColor = new Color(renderer.effectColor.r, renderer.effectColor.g, renderer.effectColor.b, maxAlpha);

        float currentInterval = 0;
        while (flashOutline)
        {
            float tColor = currentInterval / interval;
            renderer.effectColor = Color.Lerp(minColor, maxColor, tColor);

            currentInterval += Time.deltaTime;
            if (currentInterval >= interval)
            {
                Color temp = minColor;
                minColor = maxColor;
                maxColor = temp;
                currentInterval = currentInterval - interval;
            }
            //duration -= Time.deltaTime;
            yield return null;
        }

        renderer.effectColor = colorNow;
    }

    public void ShowMixedImproperPnl()
    {
        mixedImproper_Pnl.SetActive(true);
    }
    public void HideMixedImproperPnl()
    {
        mixedImproper_Pnl.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
