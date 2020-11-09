using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class obj14SceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    string jsonFileName = "Obj14_json.json";

    public static int SelectedPart;
    public int count =0, count1=0;
    conversationManager conversationManager;
    Obj14CanvasManager canvasManager;
    bool SeconTimecall = false;
    public GameObject LoadingAudio;
    private void OnEnable()
    {
        timeline_new.OnEventCalled += EventToHandle;
        Obj14CanvasManager.OnCanvasJobDone += CanvasJobDone;
    }

    private void OnDisable()
    {
        timeline_new.OnEventCalled -= EventToHandle;
    }
    void Start()
    {
        Initialised();
       // DisableRoPanel();

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            timeline_new.Instance.load_next();
        }
    }
    void Initialised()
    {
        Invoke("audio_invoke", 2.0f);
        conversationManager = GameObject.FindObjectOfType<conversationManager>();
        canvasManager = GameObject.FindObjectOfType<Obj14CanvasManager>();
        LoadingAudio = GameObject.Find("LoadAudio");
        //if (UtilityArtifacts.backTraversal)
        //{
        //    Text textLoadingText = LoadingAudio.transform.GetChild(1).GetComponent<Text>();
        //    textLoadingText.text = "Let us understand this better";
        //}
        Debug.Log(canvasManager.name);

    }
    void audio_invoke()
    {
        Debug.Log(EVENT.SetUpTimeLine);
        EventManager.setNameOfJasonFile(jsonFileName);
        EventManager.Broadcast(EVENT.SetUpTimeLine);
    }

    void HideLoadingAudio()
    {
        LoadingAudio.SetActive(false);
    }
    void EventToHandle(string EventName)
    {
        HideLoadingAudio();
        switch (EventName)
        {
            case "Obj14_MixedFrac_great":
                canvasManager.DisableRoPanel();
                canvasManager.HideLoading();
                canvasManager.ShowMixedImproperPnl();
                conversationManager.EnableConversation("Mixed fractions are great for everyday use as they are easy to understand.");
                GameObject.Find("Canvas/MixedImproperFraction_Panel/Head_Text").SetActive(true);
                break;
            case "Obj14_is_easier":
                conversationManager.EnableConversation("2" + " \\frac{1}{2}" + " loaves of bread is easier to understand than " + "\\frac{5}{2}" + " loaves of bread. ");
                GameObject.Find("Canvas/MixedImproperFraction_Panel/BreadLoafs_Container").SetActive(true);
                GameObject.Find("Canvas/MixedImproperFraction_Panel/TEXDraw_Frac").SetActive(true);
                GameObject.Find("Canvas/MixedImproperFraction_Panel/TEXDraw_Frac (1)").SetActive(true);
                break;
            case "Obj14_is_easier_because":
                conversationManager.EnableConversation("It is easier to understand because a mixed fraction clearly denotes the number of ");
                break;
            case "Obj14_Whole_Object":
                conversationManager.EnableConversation("whole objects and ");
                GameObject.Find("Canvas/MixedImproperFraction_Panel/Highlights/Highlight_WholeBread").SetActive(true);
                GameObject.Find("Canvas/MixedImproperFraction_Panel/Highlights/Highlight_WholeBread1").SetActive(true);
                GameObject.Find("Canvas/MixedImproperFraction_Panel/Highlights/Highlight_FracWholeBread").SetActive(true);
                GameObject.Find("Canvas/MixedImproperFraction_Panel/TEXDraw_Frac").SetActive(false);
                GameObject.Find("Canvas/MixedImproperFraction_Panel/TEXDraw_Frac (1)").SetActive(false);
                GameObject.Find("Canvas/MixedImproperFraction_Panel/TEXDraw_Frac (2)").SetActive(true);

                break;
            case "Obj14_part_of_the_same_Object":
                conversationManager.EnableConversation("a part of the same object. ");
                GameObject.Find("Canvas/MixedImproperFraction_Panel/Highlights/Highlight_HalfBread").SetActive(true);
                GameObject.Find("Canvas/MixedImproperFraction_Panel/Highlights/Highlight_FracHalfBread").SetActive(true);
                break;
            case "Obj14_MixedFrac_has_two_parts":
                conversationManager.EnableConversation("As you know a mixed fraction has two parts to it");
                GameObject.Find("Canvas/MixedImproperFraction_Panel/Head_Text").GetComponent<Text>().text = "Mixed and Improper Fractions";
                GameObject.Find("Canvas/MixedImproperFraction_Panel/BreadLoafs_Container").SetActive(false);

                break;
            case "Obj14_whole_number":
                conversationManager.EnableConversation("the whole number and");
                GameObject.Find("Canvas/MixedImproperFraction_Panel/Highlights/Highlight_LabelWholeBread").SetActive(true);
                break;
            case "Obj14_fraction_number":
                conversationManager.EnableConversation("the fraction which makes it slightly complex to do calculations. This is where improper fractions are very helpful.");
                GameObject.Find("Canvas/MixedImproperFraction_Panel/Highlights/Highlight_LabelHalfBread").SetActive(true);
                break;
            case "Obj14_this_is_true":
                conversationManager.EnableConversation("You will realize this is true when you try to add, subtract, multiply or divide mixed fractions.");
                break;
            case "Obj14_convert_improper_proper":
                conversationManager.EnableConversation("We convert Mixed fractions into Improper Fractions for performing operations involving fractions.");
                GameObject.Find("Canvas/MixedImproperFraction_Panel/TEXDraw_Frac (2)").SetActive(false);
                GameObject.Find("Canvas/MixedImproperFraction_Panel/Image_1").SetActive(true);
                break;
            case "Obj14_Let_us_learn":
                conversationManager.EnableConversation("Let us learn how we can convert a Mixed fraction or a mixed number into an Improper Fraction. ");
                GameObject.Find("Canvas/MixedImproperFraction_Panel/Image_1").SetActive(false);
                GameObject.Find("Canvas/MixedImproperFraction_Panel/ArrwPnl_Animation").SetActive(true);
                GameObject.Find("Canvas/MixedImproperFraction_Panel/ArrwPnl_Animation").GetComponent<Animator>().SetBool("Play", true);
                GameObject.Find("Canvas/MixedImproperFraction_Panel/Head_Text").GetComponent<Text>().text = "";

                break;
            case "Obj14_we_know_that":
                conversationManager.DisableConversation();
                //conversationManager.EnableConversation("We know that a mixed fraction is made of a whole number and a proper fraction. ");
                GameObject.Find("Canvas/MixedImproperFraction_Panel/Image_2").SetActive(true);
                break;
            case "Obj14_while_converting":
                //conversationManager.EnableConversation("While converting the mixed fraction to an improper fraction, we will change the whole number into a fraction and combine it with the proper fraction.");
                conversationManager.DisableConversation();
                GameObject.Find("Canvas/MixedImproperFraction_Panel/ArrwPnl_Animation").SetActive(false);
                GameObject.Find("Canvas/MixedImproperFraction_Panel/Image_1").SetActive(false);
                GameObject.Find("Canvas/MixedImproperFraction_Panel/Image_2").SetActive(false);
                GameObject.Find("Canvas/MixedImproperFraction_Panel/WhileConverting_Text").SetActive(true);
                break;
            case "Obj14_Convert_the_mixedFrac":
                //conversationManager.EnableConversation("Let’s convert the Mixed Fraction 3" + "\\frac{2}{5}" + " into an Improper Fraction.");
                GameObject.Find("Canvas/MixedImproperFraction_Panel/WhileConverting_Text").SetActive(false);
                GameObject.Find("Canvas/MixedImproperFraction_Panel/Covert_3_1_2_IF_Text").SetActive(true);
                conversationManager.DisableConversation();
                canvasManager.DisableRoPanel();
                canvasManager.HideLoading();
                //call Q1
                Invoke("nextObjective1", 5.6f);
                break;
            case "Obj14_RO_Identify_which_following":
                canvasManager.HideLoading();
                conversationManager.EnableConversation("Identify which of the following correctly represents 3 " + "\\frac{2}{5} ?");
                //canvasManager.HideMixedImproperPnl();
                break;
            case "wholebarsarecolored":
                canvasManager.RO_Panel.transform.GetChild(0).gameObject.SetActive(false);
                canvasManager.RO_Panel.SetActive(false);
                canvasManager.ShowImproperFractionBlock();
                conversationManager.EnableConversation("3 whole bars are colored to represent the whole number part of the fraction ");
                canvasManager.ShowInitBlock();
                //canvasManager.HighlightWholeNumber();
                //show highlights
                GameObject.Find("Canvas/BoderPanel/Highlight_ImageWhole1").SetActive(true);
                GameObject.Find("Canvas/BoderPanel/Highlight_ImageWhole2").SetActive(true);
                GameObject.Find("Canvas/BoderPanel/Highlight_ImageWhole3").SetActive(true);
                GameObject.Find("Canvas/BoderPanel/Highlight_FracWhole").SetActive(true);
                GameObject.Find("Canvas/BoderPanel/Highlight_ImageWhole4").SetActive(false);
                GameObject.Find("Canvas/BoderPanel/Highlight_FracHalf").SetActive(false);
                break;
            case "haveanotherbar":
                conversationManager.EnableConversation("we have another bar with 5 parts where 2 parts are colored to represent the fraction part of the mixed fraction");
                //canvasManager.StopWholeHighlightStartFractionHighlight();
                GameObject.Find("Canvas/BoderPanel/Highlight_ImageWhole1").SetActive(false);
                GameObject.Find("Canvas/BoderPanel/Highlight_ImageWhole2").SetActive(false);
                GameObject.Find("Canvas/BoderPanel/Highlight_ImageWhole3").SetActive(false);
                GameObject.Find("Canvas/BoderPanel/Highlight_FracWhole").SetActive(false);
                GameObject.Find("Canvas/BoderPanel/Highlight_ImageWhole4").SetActive(true);
                GameObject.Find("Canvas/BoderPanel/Highlight_FracHalf").SetActive(true);
                GameObject.Find("Canvas/BoderPanel/Parts_Div").SetActive(true);

                break;
            case "MixedfractionintoanImproperFraction":
                conversationManager.EnableConversation("To convert the Mixed fraction into an Improper Fraction, we first need to make the wholes to have the same number of partitions as the fraction part");
                GameObject.Find("Canvas/BoderPanel/Highlight_ImageWhole4").SetActive(false);
                GameObject.Find("Canvas/BoderPanel/Highlight_FracHalf").SetActive(false);
                canvasManager.StopFlashing();
                break;
            case "thefractionalpart":
                conversationManager.EnableConversation("Since the fractional part " + "\\frac{2}{5}" + " has the denominator 5, we have to divide each of the wholes into 5 equal parts.");
                canvasManager.ShowInitBlock();
                GameObject.Find("Canvas/BoderPanel/Highlight_5").SetActive(true);
                SeconTimecall = false;
                Invoke("StartShowingBorder", 5);
                break;
            case "Eachbarnow":
                conversationManager.EnableConversation("Each bar now has 5 equal parts in them");
                GameObject.Find("Canvas/BoderPanel/Highlight_5").SetActive(false);
                break;
            case "Eachpartin":
                conversationManager.EnableConversation("Each part in each bar represents " + "\\frac{1}{5}");
                SeconTimecall = false;
                Invoke("StartShowingFractionText", 3);
                break;
            case "Soeachbar":
                conversationManager.EnableConversation("So each bar is made up of five " + "\\frac{1}{5}" +" parts");
                //show animation highlights yellow which shows 1/5 parts
                GameObject.Find("Canvas/BoderPanel/Animation_Highlight_Parts").SetActive(true);
                break;
            case "Canyoucount":
                conversationManager.DisableConversation();
                canvasManager.DisableRoPanel();
                canvasManager.HideLoading();

                canvasManager.ShowImproperFractionBlock();
                //conversationManager.EnableConversation("Can you count and tell how many 1/5 parts are colored in total?");
                canvasManager.ShowNumberInputPanel();
                conversationManager.EnableQuestion("Can you count and tell how many " + "\\frac{1}{5}" + " parts are colored in total?");
                GameObject.Find("Canvas/BoderPanel/Animation_Highlight_Parts").SetActive(false);
                GameObject.Find("Canvas/BoderPanel/Parts_Div").SetActive(false);
                canvasManager.HideInitBlock();
                //canvasManager.ShowNumberInputPanel();
                break;
            case "coloredpartsof":
                conversationManager.EnableConversation("Since there are 17 colored parts of " + "\\frac{1}{5}" + ", we can represent this as " + "\\frac{17}{5}" + "");
                GameObject.Find("Canvas/ShowImproperFraction").transform.GetChild(0).gameObject.SetActive(true);

                canvasManager.HideFractionAnswer();

                //GameObject.Find("Canvas/BoderPanel/17_5_Block").SetActive(false);

                
                break;
            case "Whattypeofafraction":
                conversationManager.DisableConversation();
                GameObject.Find("Canvas/ShowImproperFraction").transform.GetChild(0).gameObject.SetActive(false);
                conversationManager.EnableQuestion("What type of a fraction is " + "\\frac{17}{5}" + "?");
                frationFailedTimes = 0;
                canvasManager.ShowFractionChoosingPanel();
                break;
            case "didisconvert":
                conversationManager.DisableConversation();
                //conversationManager.EnableConversation("What we just did is convert a mixed fraction to an improper fraction");
                GameObject.Find("Canvas/CorrectAnswerImage_ForSeventeen").transform.GetChild(0).gameObject.SetActive(false);
                canvasManager.HideImproperFractionBlock();
                canvasManager.HideFractionChoosingPanel();
                conversationManager.DisableQuestion1();
                //canvasManager.ShowFractionAnswer();
                //canvasManager.ShowMixedFractionPanel();
                canvasManager.HideFractionAnswer();
                canvasManager.HideMixedFractionpanel();
                GameObject.Find("Canvas/Image_whatYouJustDid").transform.GetChild(0).gameObject.SetActive(true);
                break;
            case "observethedenominators":
                conversationManager.EnableConversation("If you observe, the denominators in both the mixed and the improper fraction did not change");
                //GameObject.Find("Canvas/MixedfractionBoderPanel/Highlight_5 (1)").SetActive(true);
                //GameObject.Find("Canvas/MixedfractionBoderPanel/Highlight_5 (2)").SetActive(true);
                GameObject.Find("Canvas/Image_whatYouJustDid/Image/Highlight_Den1").gameObject.SetActive(true);
                GameObject.Find("Canvas/Image_whatYouJustDid/Image/Highlight_Den2").gameObject.SetActive(true);


                break;
            case "dividedthewholeparts":
                conversationManager.EnableConversation("This is because we divided the whole parts to have the same number of parts as the fraction part");
                GameObject.Find("Canvas/Image_whatYouJustDid").transform.GetChild(0).gameObject.SetActive(false);
                canvasManager.HideAllBoders();
                canvasManager.HideAllFractText();
                canvasManager.HideAllCountText();
                canvasManager.HideFractionAnswer();
                GameObject.Find("Canvas/ShowMixedFraction").transform.GetChild(0).gameObject.SetActive(true);

                //GameObject.Find("Canvas/MixedfractionBoderPanel/Highlight_5 (1)").SetActive(false);
                //GameObject.Find("Canvas/MixedfractionBoderPanel/Highlight_5 (2)").SetActive(false);

                //canvasManager.ShowFractionAnswer();
                canvasManager.ShowMixedFractionPanel();

                //Invoke("StartShowingBorder", 3.2f);
                Invoke("StartDivisions", 3.2f);

                break;
            case "convertaMixedfraction":
                conversationManager.EnableConversation("So now we know how to convert a Mixed fraction into an Improper Fraction");
                SeconTimecall = true;
                //Invoke("StartShowingFractionText", 2);
                GameObject.Find("Canvas/MixedfractionBoderPanel/DivideLines (1)").SetActive(false);
                GameObject.Find("Canvas/MixedfractionBoderPanel/MixedFrac").SetActive(true);
                GameObject.Find("Canvas/ShowMixedFraction").transform.GetChild(0).gameObject.SetActive(false);


                Invoke("ShowImproperFrac", 3.0f);

                break;
            case "Especiallyifthewholenumber":
                conversationManager.EnableConversation("However this process is long and time consuming");
                GameObject.Find("Canvas/MixedfractionBoderPanel/ImproperFrac").SetActive(false);

                break;
            case "partfthemixedfraction":
                conversationManager.EnableConversation("Especially if the whole number part of the mixed fraction is a large number.");
                GameObject.Find("Canvas/FiftySix_Block").transform.GetChild(0).gameObject.SetActive(true);
                canvasManager.HideAllObjectsPanel();
                conversationManager.DisableQuestion1();

                break;
            case "maginethemixedfraction":
                conversationManager.EnableConversation("Imagine the mixed fraction to be 56 " + "\\frac{2}{3}" + ". You would have to use 56 bars, which is a lot of effort and time");
                break;
            case "easierconvertaMixedfraction":
                conversationManager.DisableConversation();
                //conversationManager.EnableConversation("We will now learn an easier method to quickly convert a Mixed fraction into an Improper Fraction.");
                GameObject.Find("Canvas/Image_4").transform.GetChild(0).gameObject.SetActive(true);
                GameObject.Find("Canvas/FiftySix_Block").transform.GetChild(0).gameObject.SetActive(false);
                break;
            case "againconsiderthefraction":
                conversationManager.EnableConversation("Let’s once again consider the fraction 3 " + "\\frac{2}{5}" + ". We need to change this into an Improper Fraction. ");
                GameObject.Find("Canvas/Image_4").transform.GetChild(0).gameObject.SetActive(false);
                GameObject.Find("Canvas/Image_5").transform.GetChild(0).gameObject.SetActive(true);
                canvasManager.HideAllObjectsPanel();
                //canvasManager.HideEasyMethordPanel();
                //canvasManager.ShowEasyMixedFractionNumber();
                GameObject.Find("Canvas/FiftySix_Block").transform.GetChild(0).gameObject.SetActive(false);
                break;

            case "multiplydenominatorwhole_1":
                //conversationManager.EnableConversation("we multiply the denominator with the whole number.");
                conversationManager.DisableConversation();
                GameObject.Find("Canvas/Image_5").transform.GetChild(0).gameObject.SetActive(false);
                GameObject.Find("Canvas/Image_5").SetActive(false);
                canvasManager.ShowEasyMethordPanel();
                canvasManager.ShowArrowMultiply();
                canvasManager.ShowEasyMustiplyStep1();
                canvasManager.ShowEasyMultiplayStep2();
                canvasManager.ShowEasyMixedFractionNumber();
                GameObject.Find("Canvas/EasyProcess/EasyMixedNumberToImproper/AddArrow (1)").gameObject.SetActive(false);
                GameObject.Find("Canvas/EasyProcess/EasyMixedNumberToImproper/MultiplyArrow (1)").gameObject.SetActive(true);

                Debug.Log("333333333333333333333333333333333333333333333333333333");
                break;

            case "whydowemultiplythe":
                //conversationManager.EnableConversation("Do you know why do we multiply the denominator with the whole number");
                conversationManager.DisableConversation();
                GameObject.Find("Canvas/EasyProcess/Represent_1").transform.GetChild(0).gameObject.SetActive(true);
                canvasManager.HideArrowMultiply();
                canvasManager.HideEasyMustiplyStep1();
                canvasManager.HideEasyMultiplayStep2();
                canvasManager.HideEasyMixedFractionNumber();
                canvasManager.HideEasyMultiplayStep2();
                break;

            case "thewholenumbercan":
                //conversationManager.EnableConversation("This is so that the whole number can have the same number of parts as fractions.");
                //canvasManager.HideEasyMethordPanel();
                conversationManager.DisableConversation();
                //canvasManager.HideArrowMultiply();
                //canvasManager.HideEasyMustiplyStep1();
                //canvasManager.HideEasyMultiplayStep2();
                //canvasManager.HideEasyMixedFractionNumber();
                //canvasManager.HideEasyMultiplayStep2();
                GameObject.Find("Canvas/EasyProcess/Represent_1").transform.GetChild(0).gameObject.SetActive(false);
                GameObject.Find("Canvas/EasyProcess/Represent_2").transform.GetChild(0).gameObject.SetActive(true);
                GameObject.Find("Canvas/EasyProcess/Represent_2").transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                break;

            case "stepissameasdividing":
                conversationManager.EnableConversation("This step is same as dividing the wholes into equal parts as per the denominator. ");
                canvasManager.HideEasyMulImage();
                GameObject.Find("Canvas/EasyProcess/Represent_2").transform.GetChild(0).gameObject.SetActive(false);
                // Highlight
                GameObject.Find("Canvas/Highlight_ThisStep").transform.GetChild(0).gameObject.SetActive(true);
                GameObject.Find("Canvas/Highlight_ThisStep/Image").transform.GetChild(0).gameObject.SetActive(true);

                //canvasManager.HideArrowMultiply();
                //canvasManager.HideEasyMustiplyStep1();
                //canvasManager.HideEasyMultiplayStep2();
                canvasManager.ShowMixedFractionPanel();
                GameObject.Find("Canvas/MixedfractionBoderPanel/DivideLines (1)").gameObject.SetActive(false);
                GameObject.Find("Canvas/MixedfractionBoderPanel/DivideLines (1)/GameObject").gameObject.SetActive(false);

                Invoke("StartDivisions", 3.2f);
                break;

            case "wholesgetsdividedinto":
                conversationManager.EnableConversation("The wholes gets divided into 15 parts and when we multiply the denominator and the whole number, we get 15");
                canvasManager.HideMixedFractionpanel();
                canvasManager.ShowEasyAddImage();
                GameObject.Find("Canvas/EasyProcess/WholeMulTiplayAndAdd/BoderPanelEasyAdd/DivideLinesEasyAdd").transform.GetChild(1).gameObject.SetActive(false);
                GameObject.Find("Canvas/EasyProcess/WholeMulTiplayAndAdd/BoderPanelEasyAdd/DivideLinesEasyAdd").transform.GetChild(2).gameObject.SetActive(false);

                canvasManager.HideEasyMulImage();
                break;

            case "makestheresultingimproper":
                conversationManager.EnableConversation("This also makes the resulting improper fraction to have the same denominator as the mixed fraction");
                //Invoke("enableFade2", 3f);
                //Invoke("HideEasyPanel", 3f);
                canvasManager.HideEasyAddImage();
                canvasManager.ShowMixedFractionPanel();
                
                GameObject.Find("Canvas/MixedfractionBoderPanel/DivideLines (1)").SetActive(true);
                GameObject.Find("Canvas/MixedfractionBoderPanel/DivideLines (1)/GameObject").gameObject.SetActive(false);
                GameObject.Find("Canvas/MixedfractionBoderPanel/MixedFrac").SetActive(true);
                GameObject.Find("Canvas/Highlight_ThisStep/Image").transform.GetChild(0).gameObject.SetActive(false);

                Invoke("ShowImproperFrac_Highlight", 3.0f);
                break;
            case "RO1Question":
                conversationManager.DisableConversation();
                canvasManager.HideMixedFractionpanel();
                //Invoke("enableFade3", 3f);
                //canvasManager.HideEasyMethordPanel();
                break;
            case "RO1Question2":
                //conversationManager.DisableConversation();
                //canvasManager.HideEasyMethordPanel();
                //Invoke("enableFade4", 3f);
                break;
            case "resultofmultiplication": //
                canvasManager.ShowEasyMethordPanel();
                conversationManager.EnableConversation("Add the result of multiplication to the numerator of the mixed fraction ");
                canvasManager.ShowArrawAdd();
                canvasManager.ShowArrowMultiply();
                canvasManager.ShowEasyMustiplyStep1();
                canvasManager.ShowEasyMultiplayStep2();
                canvasManager.ShowEasyMixedFractionNumber();
                GameObject.Find("Canvas/EasyProcess/EasyMixedNumberToImproper/AddArrow (1)").gameObject.SetActive(true);
                GameObject.Find("Canvas/EasyProcess/EasyMixedNumberToImproper/MultiplyArrow (1)").gameObject.SetActive(false);
                Debug.Log("000000000000000000000000000000000000000000000000000");
                break;
            case "resultofmultiplication_1": //resultofmultiplication_1
                //canvasManager.ShowEasyMethordPanel();
                //conversationManager.EnableConversation("Add the result of multiplication to the numerator of the mixed fraction ");
                //canvasManager.ShowArrawAdd();
                //canvasManager.ShowArrowMultiply();
                //canvasManager.ShowEasyMustiplyStep1();
                //canvasManager.ShowEasyMultiplayStep2();
                //canvasManager.ShowEasyMixedFractionNumber();
                //GameObject.Find("Canvas/EasyProcess/EasyMixedNumberToImproper/AddArrow (1)").gameObject.SetActive(true);
                //GameObject.Find("Canvas/EasyProcess/EasyMixedNumberToImproper/MultiplyArrow (1)").gameObject.SetActive(false);
                Debug.Log("1111111111111111111111111111111111111111111111111111");
                break;
            case "addthenumberofparts":
                conversationManager.EnableConversation("We do this step to add the number of parts in the whole to the number of parts considered in the fraction");
                //canvasManager.ShowEasyAddStep1();

                canvasManager.HideArrawAdd();
                canvasManager.HideArrowMultiply();
                canvasManager.HideEasyMustiplyStep1();
                canvasManager.HideEasyMultiplayStep2();
                canvasManager.HideEasyMixedFractionNumber();

                canvasManager.HideEasyFinalStep2();
                canvasManager.HideEasyFinalStep3();
                GameObject.Find("Canvas/Image_6").transform.GetChild(0).gameObject.SetActive(true);


                break;
            case "havealookattheshapes":
                conversationManager.EnableConversation("Let’s have a look at the shapes to understand it better.");
                GameObject.Find("Canvas/EasyProcess/WholeMulTiplayAndAdd/BoderPanelEasyAdd/DivideLinesEasyAdd").transform.GetChild(0).gameObject.SetActive(false);
                GameObject.Find("Canvas/EasyProcess/WholeMulTiplayAndAdd/BoderPanelEasyAdd/DivideLinesEasyAdd").transform.GetChild(1).gameObject.SetActive(false);
                GameObject.Find("Canvas/EasyProcess/WholeMulTiplayAndAdd/BoderPanelEasyAdd/DivideLinesEasyAdd").transform.GetChild(2).gameObject.SetActive(false);
                GameObject.Find("Canvas/Image_6").transform.GetChild(0).gameObject.SetActive(false);
                GameObject.Find("Canvas/ShowImproperFraction").transform.GetChild(0).gameObject.SetActive(true);

                canvasManager.ShowEasyAddImage();
                break;
            case "numberofpartsinthewhole":
                conversationManager.EnableConversation("So, we add 15 which is number of parts in the whole to 2 which is nothing but number of parts considered in a fraction");
                // need to figure out a highlight
                break;
            case "keepthedenominatorssame":
                conversationManager.EnableConversation("This gives us 15 plus 2 in the numerator, which is 17. We keep the denominators same in both the mixed and the improper fraction.  Therefore the improper fraction is " + "\\frac{17}{5}.");
                Invoke("HideEasyPanel", 6f);
                Invoke("enableFade4", 6f);
                break;
            case "Ro2Question":
                GameObject.Find("Canvas/ShowImproperFraction").transform.GetChild(0).gameObject.SetActive(false);
                conversationManager.DisableConversation();
                //Invoke("enableFade6", 6f);
                break;
            case "Ro3Question":
                conversationManager.DisableConversation();
                break;
            case "requiredtoconvertaMixed":
                //conversationManager.EnableConversation("Let us once again have a look at the steps that are required to convert a Mixed fraction into an Improper fraction.");
                conversationManager.DisableConversation();
                canvasManager.HideEasyMethordPanel();
                canvasManager.HideEasyAddImage();
                canvasManager.HideMixedFractionpanel();

                GameObject.Find("Canvas/Show_LetUsOnce_Image").transform.GetChild(0).gameObject.SetActive(true);
                
                break;
            case "multiplydenominatorwhole_2":
                conversationManager.EnableConversation("Multiply the denominator of the Mixed fraction with its Whole number");
                GameObject.Find("Canvas/Show_LetUsOnce_Image").transform.GetChild(0).gameObject.SetActive(false);
                canvasManager.ShowEasyMethordPanel();
                canvasManager.ShowEasyFinalStep2();
                canvasManager.ShowArrowMultiply();
                //canvasManager.ShowEasyMixedFractionNumber();
                break;
            case "AddmultiplicationMixed":
                conversationManager.EnableConversation("Add the result of the multiplication to the numerator of the Mixed fraction. This will give you the numerator of the Improper Fraction.");
                canvasManager.ShowArrawAdd();
                canvasManager.ShowEasyFinalStep1();
                canvasManager.ShowEasyFinalStep3();
                break;
            case "denominatorMixedImproper":
                conversationManager.EnableConversation("The denominator of the Mixed Fraction and the Improper fraction would remain the same.");
                GameObject.Find("Canvas/EasyProcess/Show_Den_H_1").gameObject.SetActive(true);
                GameObject.Find("Canvas/EasyProcess/Show_Den_H_2").gameObject.SetActive(true);
                break;
            case "denominatorimproperMixed":
                conversationManager.EnableConversation("So you can simply keep the denominator for the improper fraction same as the Mixed Fraction.");
                // canvasManager.ShowEasyFinalStep1();
                GameObject.Find("Canvas/EasyProcess/Show_Den_H_1").gameObject.SetActive(false);
                GameObject.Find("Canvas/EasyProcess/Show_Den_H_2").gameObject.SetActive(false);
                Invoke("enableFade6", 6f);
                break;
            case "Ro4Question":
                conversationManager.DisableConversation();
                GameObject.FindObjectOfType<Obj14_ROManager>().EnableSubmitButtonRO5();
                break;
            case "test":
                conversationManager.DisableConversation();
                // conversationManager.EnableConversation("Since the fractional part  2/5 has the denominator 5, we have to divide each of the wholes into 5 equal parts.");
                conversationManager.EnableQuestion("Since the fractional part " + "\\frac{2}{5}" + " has the denominator 5, we have to divide each of the wholes into 5 equal parts.");
                break;
        }
    }
    void StartDivisions() {
        GameObject.Find("Canvas/MixedfractionBoderPanel/DivideLines (1)").gameObject.SetActive(true);
        GameObject.Find("Canvas/MixedfractionBoderPanel").transform.GetComponent<Animator>().SetBool("Play", true);
        Invoke("Load_Next_Proceed", 4.0f);
    }

    void HideEasyPanel()
    {
        canvasManager.HideEasyMethordPanel();
    }

    void ShowEasyPanel()
    {
        canvasManager.ShowEasyMethordPanel();
    }

    void StartShowingBorder()
    {
        canvasManager.StartShowingBorder();
        Invoke("Load_Next_Proceed", 2.0f);
    }

    void ShowImproperFrac()
    {
        GameObject.Find("Canvas/MixedfractionBoderPanel/DivideLines (1)").SetActive(true);
        GameObject.Find("Canvas/MixedfractionBoderPanel/MixedFrac").SetActive(false);
        GameObject.Find("Canvas/MixedfractionBoderPanel/ImproperFrac").SetActive(true);
        GameObject.Find("Canvas/MixedfractionBoderPanel/MixedFrac/Highlight").SetActive(false);
        GameObject.Find("Canvas/MixedfractionBoderPanel/ImproperFrac/Highlight").SetActive(false);
        GameObject.Find("Canvas/ShowMixedFraction").transform.GetChild(0).gameObject.SetActive(false);

        Invoke("Load_Next_Proceed", 4.0f);

    }

    void ShowImproperFrac_Highlight()
    {
        GameObject.Find("Canvas/MixedfractionBoderPanel/DivideLines (1)").SetActive(true);
        GameObject.Find("Canvas/MixedfractionBoderPanel/MixedFrac").SetActive(false);
        GameObject.Find("Canvas/MixedfractionBoderPanel/ImproperFrac").SetActive(true);
        GameObject.Find("Canvas/MixedfractionBoderPanel/MixedFrac/Highlight").SetActive(true);
        GameObject.Find("Canvas/MixedfractionBoderPanel/ImproperFrac/Highlight").SetActive(true);
        GameObject.Find("Canvas/ShowMixedFraction").transform.GetChild(0).gameObject.SetActive(false);


        //Invoke("Load_Next_Proceed", 2.0f);
        Invoke("enableFade2", 4.0f);
        
    }

    void StartShowingFractionText()
    {
        canvasManager.StartShowingFractionText();
    }

    void CanvasJobDone(string job)
    {
        switch (job)
        {
            case UtilityArtifacts.Obj14_ShowingBoard:
                timeline_new.Instance.load_next();
                break;
            case UtilityArtifacts.Obj14_ShowingText:
                if (SeconTimecall)
                {
                    canvasManager.StartShowingCountText();
                }
                else
                    timeline_new.Instance.load_next();
                break;
            case UtilityArtifacts.Obj14_InputString:
               if(UtilityArtifacts.obj14InputValue == 17)
                {
                    GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
                    conversationManager.EnableConversation("There are seventeen coloured parts of " + "\\frac{1}{5} ");
                    FindObjectOfType<timeline_new>().playAudioOnRelearn("obj14VOWrong");
                    //conversationManager.DisableQuestion();
                    //stop this audio and hide conversation panel
                    Invoke("AfterCorrectAnswer", FindObjectOfType<timeline_new>().lapa.length_of_audio + 5.2f);
                    //timeline_new.Instance.load_next();
                }
                else
                {
                    // Wrong result input
                    //conversationManager.EnableConversation("There are seventeen coloured parts of "+"\\frac{1}{5} ");
                    //FindObjectOfType<timeline_new>().playAudioOnRelearn("obj14VOWrong"); 
                    //canvasManager.HideAllCountText();
                    //canvasManager.StartShowingCountText();

                    if (UtilityArtifacts.obj14InputValue == 20)
                    {
                        if (count < 1)
                        {
                            conversationManager.EnableConversation("Count only the parts that are colored to get the correct answer. Try again");
                            FindObjectOfType<timeline_new>().playAudioOnRelearn("obj14Hint_20");

                            //stop this audio and hide conversation panel
                            Invoke("HideCOnvo_Hint", FindObjectOfType<timeline_new>().lapa.length_of_audio + 1.5f);
                            Invoke("CheckForTwenty", FindObjectOfType<timeline_new>().lapa.length_of_audio + 2.5f);

                            count++;
                            
                        }
                        else
                        {
                            conversationManager.EnableConversation("There are seventeen coloured parts of " + "\\frac{1}{5} ");
                            FindObjectOfType<timeline_new>().playAudioOnRelearn("obj14VOWrong");
                            //stop this audio and hide conversation panel
                            Invoke("HideCOnvo_Hint", FindObjectOfType<timeline_new>().lapa.length_of_audio + 5.2f);

                            //canvasManager.StartShowingCountText();
                            //canvasManager.HideAllCountText();

                            //hide inputfield no pad and submit button
                            //HideInputPnl_SubmitBtn_NumPad();
                            canvasManager.StartShowingCountText_After();
                            conversationManager.DisableQuestion1();

                        }
                    }
                    else
                    {
                        if (count1 < 1)
                        {
                            canvasManager.StartShowingCountText();
                            conversationManager.EnableConversation("Try Again");
                            FindObjectOfType<timeline_new>().playAudioOnRelearn("obj14Hint_TryAgain");
                            //stop this audio and hide conversation panel
                            Invoke("HideCOnvo_Hint", FindObjectOfType<timeline_new>().lapa.length_of_audio + 1.5f);

                            count1++;

                        }
                        else
                        {
                            conversationManager.EnableConversation("There are seventeen coloured parts of " + "\\frac{1}{5} ");
                            FindObjectOfType<timeline_new>().playAudioOnRelearn("obj14VOWrong");
                            //stop this audio and hide conversation panel
                            Invoke("HideCOnvo_Hint", FindObjectOfType<timeline_new>().lapa.length_of_audio + 1.5f);

                            canvasManager.HideAllCountText();
                            conversationManager.DisableQuestion1();
                            FindObjectOfType<timeline_new>().load_next();

                        }
                    }
                }
                break;
            case UtilityArtifacts.Obj14_ShowingCountText:
                if (SeconTimecall)
                {
                    canvasManager.ShowFractionAnswer();
                    timeline_new.Instance.load_next();
                }
                else
                {
                    conversationManager.DisableConversation();
                    canvasManager.ShowNumberInputPanel();
                }
                    
                break;
            case UtilityArtifacts.Obj14_Choosefraction:
                if (UtilityArtifacts.Obj14C_isChoiceCorrect)
                {
                    timeline_new.Instance.load_next();
                    conversationManager.DisableQuestion();
                    GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();

                }
                else
                //if (fractionfailed)
                {
                    if (frationFailedTimes >= 1)
                    {


                        moveForTraverse();

                        conversationManager.DisableQuestion();
                        canvasManager.submitAnswerButton.gameObject.SetActive(false);
                        //canvasManager.HideImproperFractionBlock();
                        //GameObject.Find("Canvas/CorrectAnswerImage_ForSeventeen").transform.GetChild(0).gameObject.SetActive(true);
                        //FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj14_Quest2_VO_Hint2");
                        //SHowChoosingOptions_TypeofFractions();
                        //conversationManager.EnableQuestion("What type of a fraction is "+ "\\frac{17}{5}?");
                        ////Invoke("Load_Next_Proceed", 9.0f);
                        // Invoke("moveForTraverse", 9.0f);

                    }
                    else
                    {
                        conversationManager.EnableConversation("The fraction " + " \\frac{17}{5} " + " represents objects greater than 1 and has a numerator greater than the denominator. What type of fraction best describes" + " \\frac{17}{5} ? Try again.");
                        FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj14_Quest2_VO_Hint1");
                        fractionfailed = true;
                        frationFailedTimes++;
                        ////test
                        Invoke("SHowChoosingOptions_TypeofFractions", 11.5f);
                        //Invoke("moveForTraverse", 11.5f); 
                    }

                }
                break;
            case UtilityArtifacts.Obj14_ChoosefractionFailed:
                if (fractionfailed)
                {
                    if(frationFailedTimes >= 1)
                    {
                        conversationManager.DisableQuestion();
                        canvasManager.submitAnswerButton.gameObject.SetActive(false);
                        canvasManager.HideImproperFractionBlock();
                        GameObject.Find("Canvas/CorrectAnswerImage_ForSeventeen").transform.GetChild(0).gameObject.SetActive(true);
                        FindObjectOfType<timeline_new>().playAudioOnRelearn("Obj14_Quest2_VO_Hint2");
                        //canvasManager.HideAllObjectsPanel();
                        timeline_new.Instance.load_next();

                    }
                    else
                        canvasManager.ShowFractionChoosingPanel();
                }

                break;

            case UtilityArtifacts.Obj14_ShowingCountText_After:
                canvasManager.HideAllCountText();
                FindObjectOfType<timeline_new>().load_next();

                break;

        }
    }

    void AfterCorrectAnswer()
    {

        conversationManager.DisableQuestion();
        timeline_new.Instance.load_next();

    }

    void CheckForTwenty()
    {
        canvasManager.TestSeventeenCondition();
    }

    void HideInputPnl_SubmitBtn_NumPad()
    {
        GameObject.Find("Canvas/NumberPad_Pnl").SetActive(false);
        GameObject.Find("Canvas/SubmitButtonBg").SetActive(false);
        GameObject.Find("Canvas/InputField_Numerator").gameObject.SetActive(false);


    }

    void HideCOnvo_Hint()
    {
        conversationManager.DisableConversation();
    }

    void SHowChoosingOptions_TypeofFractions()
    {
        conversationManager.DisableConversation();
        canvasManager.ShowFractionChoosingPanel();

    }
    void moveForTraverse()
    {
        //numberOfAttempt = 0;
        UtilityArtifacts.loading_pos = "Obj12_Lo2_from_obj14";
        UtilityArtifacts.coming_back_from = "to_Obj14_quest2";
        UtilityArtifacts.backTraversal = true;
        UtilityArtifacts.comingbackafterTraversal = false;
        UtilityArtifacts.loadStartingpointforcomingback = 24;
        UtilityArtifacts.loadStartingpoint = 12;
        UtilityArtifacts.loadEndingpoint = 23;
        // load traversescene 12
        //SceneManager.LoadScene("Obj8");
        OnTraversal(149, 131);
    }
    void OnTraversal(int objId, int subTopicId)
    {
        NewIncomingMsg mg = UtilityArtifacts.NJMsg;
        mg.current_activity_status = 1;
        mg.pre_req_id = subTopicId;// objId;
        mg.pre_req_status = 1;
        mg.pre_req_reqData.error_obj_id = objId;
        UtilityArtifacts.UnityMsg = UtilityArtifacts.outGoingMessage(mg, "false");
        SceneManager.LoadScene(UtilityArtifacts.NJLoadScene);
    }
    void Load_Next_Proceed()
    {
        timeline_new.Instance.load_next();

    }

    int frationFailedTimes = 0;
    bool fractionfailed = false;

    void enableFade1()
    {
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
        Invoke("nextObjective1", 3.0f);
    }
    void nextObjective1()
    {
        conversationManager.DisableConversation();
        canvasManager.HideMixedImproperPnl();
        EnableRoPanel_1();
        //GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadeout");
        Invoke("nextObjectiveVo1", 2.0f);

    }
    void nextObjectiveVo1()
    {
        GameObject.FindObjectOfType<timeline_new>().load_next();

    }
    void EnableRoPanel_1()
    {
        canvasManager.RO_Panel.SetActive(true);
        canvasManager.RO_Panel.transform.GetChild(0).gameObject.SetActive(true);

        canvasManager.RO_Pnl_1.SetActive(true);
        canvasManager.RO_Pnl_2.SetActive(false);
        canvasManager.RO_Pnl_3.SetActive(false);
        canvasManager.RO_Pnl_4.SetActive(false);
        canvasManager.RO_Pnl_5.SetActive(false);
        canvasManager.RO_Pnl_6.SetActive(false);

        GameObject.FindObjectOfType<Obj14_ROManager>().Initiliaze();
    }

    void enableFade2()
    {
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
        Invoke("nextObjective2", 3.0f);
    }
    void nextObjective2()
    {
        conversationManager.DisableConversation();
        EnableRoPanel_2();
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadeout");
        Invoke("nextObjectiveVo2", 3.0f);

    }
    void nextObjectiveVo2()
    {

        GameObject.FindObjectOfType<timeline_new>().load_next();

    }
    void EnableRoPanel_2()
    {
        canvasManager.RO_Panel.SetActive(true);
        canvasManager.RO_Panel.transform.GetChild(0).gameObject.SetActive(true);
        canvasManager.RO_Pnl_2.SetActive(true);

        canvasManager.RO_Pnl_1.SetActive(false);
        canvasManager.RO_Pnl_3.SetActive(false);
        canvasManager.RO_Pnl_4.SetActive(false);
        canvasManager.RO_Pnl_5.SetActive(false);
        canvasManager.RO_Pnl_6.SetActive(false);
        GameObject.FindObjectOfType<Obj14_ROManager>().EnableSubmitButtonRO1();

    }

    void enableFade3()
    {
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
        Invoke("nextObjective13", 3.0f);
    }
    void nextObjective3()
    {
        conversationManager.DisableConversation();
        EnableRoPanel_3();
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadeout");
        Invoke("nextObjectiveVo3", 3.0f);

    }
    void nextObjectiveVo3()
    {

        GameObject.FindObjectOfType<timeline_new>().load_next();

    }
    void EnableRoPanel_3()
    {
        canvasManager.RO_Panel.SetActive(true);
        canvasManager.RO_Panel.transform.GetChild(0).gameObject.SetActive(true);
        canvasManager.RO_Pnl_3.SetActive(true);

        canvasManager.RO_Pnl_1.SetActive(false);
        canvasManager.RO_Pnl_2.SetActive(false);
        canvasManager.RO_Pnl_4.SetActive(false);
        canvasManager.RO_Pnl_5.SetActive(false);
        canvasManager.RO_Pnl_6.SetActive(false);
        GameObject.FindObjectOfType<Obj14_ROManager>().EnableSubmitButtonRO2();



    }

    void enableFade4()
    {
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
        Invoke("nextObjective4", 3.0f);
    }
    void nextObjective4()
    {
        conversationManager.DisableConversation();
        EnableRoPanel_4();
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadeout");
        Invoke("nextObjectiveVo4", 3.0f);

    }
    void nextObjectiveVo4()
    {

        GameObject.FindObjectOfType<timeline_new>().load_next();

    }
    void EnableRoPanel_4()
    {
        canvasManager.RO_Panel.SetActive(true);
        canvasManager.RO_Panel.transform.GetChild(0).gameObject.SetActive(true);
        canvasManager.RO_Pnl_4.SetActive(true);

        canvasManager.RO_Pnl_1.SetActive(false);
        canvasManager.RO_Pnl_2.SetActive(false);
        canvasManager.RO_Pnl_3.SetActive(false);
        canvasManager.RO_Pnl_5.SetActive(false);
        canvasManager.RO_Pnl_6.SetActive(false);
        GameObject.FindObjectOfType<Obj14_ROManager>().EnableSubmitButtonRO3();

    }

    void enableFade5()
    {
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
        Invoke("nextObjective5", 3.0f);
    }
    void nextObjective5()
    {
        conversationManager.DisableConversation();
        EnableRoPanel_5();
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadeout");
        Invoke("nextObjectiveVo5", 3.0f);

    }
    void nextObjectiveVo5()
    {

        GameObject.FindObjectOfType<timeline_new>().load_next();

    }
    void EnableRoPanel_5()
    {
        canvasManager.RO_Panel.SetActive(true);
        canvasManager.RO_Panel.transform.GetChild(0).gameObject.SetActive(true);
        canvasManager.RO_Pnl_5.SetActive(true);
        canvasManager.RO_Pnl_1.SetActive(false);
        canvasManager.RO_Pnl_2.SetActive(false);
        canvasManager.RO_Pnl_3.SetActive(false);
        canvasManager.RO_Pnl_4.SetActive(false);
        canvasManager.RO_Pnl_6.SetActive(false);

        GameObject.FindObjectOfType<Obj14_ROManager>().EnableSubmitButtonRO4();

    }

    void enableFade6()
    {
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadein");
        Invoke("nextObjective6", 3.0f);
    }
    void nextObjective6()
    {
        conversationManager.DisableConversation();
        EnableRoPanel_6();
        GameObject.Find("Fade").GetComponent<Animator>().SetTrigger("fadeout");
        Invoke("nextObjectiveVo6", 3.0f);

    }
    void nextObjectiveVo6()
    {

        GameObject.FindObjectOfType<timeline_new>().load_next();

    }
    void EnableRoPanel_6()
    {
        canvasManager.RO_Panel.SetActive(true);
        canvasManager.RO_Panel.transform.GetChild(0).gameObject.SetActive(true);
        canvasManager.RO_Pnl_6.SetActive(true);

        canvasManager.RO_Pnl_1.SetActive(false);
        canvasManager.RO_Pnl_2.SetActive(false);
        canvasManager.RO_Pnl_3.SetActive(false);
        canvasManager.RO_Pnl_4.SetActive(false);
        canvasManager.RO_Pnl_5.SetActive(false);

        GameObject.FindObjectOfType<Obj14_ROManager>().EnableSubmitButtonRO5();


    }

}
