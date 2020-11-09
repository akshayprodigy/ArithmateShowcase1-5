using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using System;

public class MainGridLayoutManager : MonoBehaviour
{

    // Use this for initialization
    public delegate void printNextStepAction();
    public static event printNextStepAction OnprintNextStepAction;
    public delegate void printNextLineAction();
    public static event printNextLineAction OnprintNextLineAction;
    public delegate void printfractionAction();
    public static event printfractionAction OnprintfractionAction;
    public delegate void roughworkShowAction(int index);
    public static event roughworkShowAction OnroughworkShowAction;
    public delegate void ShowMessage(string msg);
    public static event ShowMessage OnShowMessage;
    public delegate void downloadNextQuestion();
    public static event downloadNextQuestion OndownloadNextQuestion;
    public delegate void ClearCanvas();
    public static event ClearCanvas OnClearCanvas;
    public delegate void ShowFullScreenMsg();
    public static event ShowFullScreenMsg OnShowFullScreenMsg;
    public delegate void LogMessage(string cases);
    public static event LogMessage onLogMessage;

    public GameObject Row, GeneralDigit, Fraction, FractiondigitUnclickable;

    public GameObject currentWorkingBlock, currentRow, instanciateOnThisBlock;//currentStep
    int currentNoofColumns, prevNoofColumns, rowNumber, currentMaxSpaceInRow, PrevRowMaxSpace, currentrowPrevLineSpace;
    int indexOfStep = 0, indexofWorkingArea = 1, cursorVarCount = 20, noOfRowsinStep, intTestNo;
    bool blinkCursor, selectedBypointer, nexttextback, nextValue, fromGeneral;
    int Functionmode = 0, textBoxTypeMode = 0, answerPressedCounter;
    const int modeBasic = 111, modeLCM = 112, modeHCF = 114, modeDivision = 113, modeAddition = 115, modeFraction = 121, modeMixedFraction = 122, defaultmode = 0, modeTextValue = 124;
    string cursorVar = "\\vborder[1000 black]", texValue = " ", downLine = "\\vborder[0100 black] ", defaultvalue = " ", UpLine = "\\vborder[0001 black] ";
    TEXDraw currentStepTexDraw, currentWorkingTexDraw;
    private bool isActive;
    int display = 1;
    int stepNo = 0, nextReleventstep = 0;
    int value = 0;
    int errorNo = 0;
    public List<int> values;
    public List<string> operators;
    int optcount = 0, valCount = 0;
    string errorMsg;
    int errorCountTraver = 0, errorCountNonTravers = 0;
    bool islastStep, valueEnteredinList, hasExpression;
    bool operatedClicked, operSighnAdded;
    bool singleStep, isanswer, valueSwapped;
    bool canAddFraction = true;
    bool previous_value_zero = false, digitentered = false, valuefromList = false, changeValueOnBackspace = true, canDelete = true;
    public List<TexDrawUtils> allTexDrawInaRow;
    int valueentered = 0;
    int valueIndex, placeValue;
    Attributes_step step_attribute;
    StudentSolution stuJasonSoln;
    StudentJson studentJson = new StudentJson();
    CanvasManager canvasManager;

    void OnEnable()
    {
        CanvasManager.OnNumberClickedAction += NumberClicked;
        CanvasManager.OnNextLineAction += GotoNextLine;
        CanvasManager.OnMixedFractionAction += AddMixedFraction;
        CanvasManager.OnFractionAction += AddFraction;
        CanvasManager.OnTextAction += AddTextValue;
        CanvasManager.OnExpClickedAction += ExpressionClicked;
        CanvasManager.OnNextStepAction += NextStep;
        CanvasManager.OnDeleteAction += Delete;
        CanvasManager.OnTabAction += GoToNextWorkingBloack;
        CanvasManager.OnLCMAction += LCM;
        CanvasManager.OnHCFAction += HCF;
        CanvasManager.OnAnswerAction += Answer;
        CanvasManager.OnDivideAction += Divide;
        CanvasManager.OnRoughworkAction += roughworkClicked;
        CanvasManager.OnReOpenRoughworkAction += RoughWorkReOpened;
        CanvasManager.OnAdditionAction += Addition;
        NewRoughManager.OnRoughWorkBackAction += backFromRoughWork;
        TexDrawUtils.OnTexDrawClickedAction += OnClickedOnATexDraw;
        CanvasManager.OnNextQuestion += CheckCurrentCanvas;
        CanvasManager.OnClearCurrentRow += CLearRow;
        CallRESTServices.OnQuestionDownloaded += showQuestion;
        CanvasManager.OnNextDontKnowQuestion += showforNextUnknownQuestion;
        CanvasManager.OnClearCanvas += onClearCanvasButton;
        CanvasManager.OnRoughworkOpenAction += roughWorkOpened;
    }

    private void OnDisable()
    {
        CanvasManager.OnNumberClickedAction -= NumberClicked;
        CanvasManager.OnNextLineAction -= GotoNextLine;
        CanvasManager.OnMixedFractionAction -= AddMixedFraction;
        CanvasManager.OnFractionAction -= AddFraction;
        CanvasManager.OnTextAction -= AddTextValue;
        CanvasManager.OnExpClickedAction -= ExpressionClicked;
        CanvasManager.OnNextStepAction -= NextStep;
        CanvasManager.OnDeleteAction -= Delete;
        CanvasManager.OnTabAction -= GoToNextWorkingBloack;
        CanvasManager.OnLCMAction -= LCM;
        CanvasManager.OnHCFAction -= HCF;
        CanvasManager.OnAnswerAction -= Answer;
        CanvasManager.OnDivideAction -= Divide;
        CanvasManager.OnAdditionAction -= Addition;
        CanvasManager.OnRoughworkAction -= roughworkClicked;
        CanvasManager.OnReOpenRoughworkAction -= RoughWorkReOpened;
        NewRoughManager.OnRoughWorkBackAction -= backFromRoughWork;
        TexDrawUtils.OnTexDrawClickedAction -= OnClickedOnATexDraw;
        CanvasManager.OnNextQuestion -= CheckCurrentCanvas;
        CanvasManager.OnClearCurrentRow -= CLearRow;
        CallRESTServices.OnQuestionDownloaded -= showQuestion;
        CanvasManager.OnNextDontKnowQuestion -= showforNextUnknownQuestion;
        CanvasManager.OnClearCanvas -= onClearCanvasButton;
        CanvasManager.OnRoughworkOpenAction -= roughWorkOpened;
    }

    void Start()
    {
        initiate();
        canvasManager = GameObject.FindObjectOfType<CanvasManager>();
    }

    void showQuestion(JSONNode question)
    {

        clearCanvas();
        //printStepNumber();
    }

    void onClearCanvasButton()
    {
        if (isActive)
        {
            if (valueentered > 0 || rowNumber > 1 || fractionCounter > 0)
            {
                clearCanvas();
            }
            else
            {
                if (OnShowMessage != null)
                    OnShowMessage("Nothing to clear");
            }
        }
    }

    void clearCanvas()
    {
        if (isActive)
        {
            if (OnClearCanvas != null)
                OnClearCanvas();
            foreach (Transform child in transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            //Debug.Log("clearCanvas");
            initiate();
        }

    }

    void initiate()
    {
        rowNumber = 1;
        currentNoofColumns = 0;
        prevNoofColumns = 1;
        noOfRowsinStep = 0;
        answerPressedCounter = 0;
        currentMaxSpaceInRow = 0;
        currentrowPrevLineSpace = 0;
        PrevRowMaxSpace = 0;
        intTestNo = 0;
        stepNo = 0;
        nextReleventstep = 0;
        display = 1;
        value = 0;
        errorNo = 0;
        errorCountTraver = 0;
        errorCountNonTravers = 0;
        blinkCursor = true;
        isActive = true;
        singleStep = false;
        isanswer = false;
        nexttextback = false;
        nextValue = true;
        valueIndex = 0;
        placeValue = 0;
        islastStep = false;
        changeValueOnBackspace = true;
        hasExpression = false;
        fromGeneral = true;
        //stuJasonSoln = new StudentSolution();
        valueEnteredinList = false;
        operatedClicked = false;
        operSighnAdded = false;
        valueentered = 0;
        digitentered = false;
        valuefromList = false;
        canAddFraction = true;
        previous_value_zero = false;
        currentRow = (GameObject)Instantiate(Row, transform);
        values = new List<int>();
        operators = new List<string>();
        allTexDrawInaRow = new List<TexDrawUtils>();
        Functionmode = defaultmode;
        valueSwapped = false;
        step_attribute = new Attributes_step();
        printStepNumber();
        getNewCurrentWorkingTex();
        checkPrequisit();
        StartBlinkingCursor();

    }

    void checkPrequisit()
    {
        //Debug.Log("checkPrequisit :" + UtilityREST.isprerequisit + " UtilityREST.topic_name: " + UtilityREST.topic_name);
        if (UtilityREST.isprerequisit)
        {
            switch (UtilityREST.topic_name)
            {
                case UtilityREST.topic_addition:
                    Addition();
                    singleStep = true;
                    break;
                case UtilityREST.topic_multiplication:
                    //Addition();
                    //Multiplication();
                    singleStep = true;
                    break;
                default:
                    singleStep = false;
                    break;
            }
        }
    }

    void printStepNumber()
    {
        if (!singleStep)
        {
            if (OnprintNextStepAction != null)
            {
                //Debug.Log("called Maingrid");
                OnprintNextStepAction();
            }


        }

        operatedClicked = false;
        operSighnAdded = false;
        stepNo++;
        initilizeStep();
        //Debug.Log("printStepNumber");
        /*
        currentStep = currentRow.transform.GetChild(indexOfStep).gameObject;
        currentStepTexDraw = currentStep.GetComponent<TEXDraw>();
        currentStepTexDraw.text = currentStepTexDraw.text.Insert(currentStepTexDraw.text.Length, rowNumber.ToString());
        */
    }

    void getNewCurrentWorkingTex()
    {
        //Debug.Log("getNewCurrentWorkingTex");
        GameObject temp = currentRow.transform.GetChild(indexofWorkingArea).gameObject;
        currentNoofColumns++;
        VariableGridLayoutGroup mVariableGridLayout = temp.GetComponent<VariableGridLayoutGroup>();
        // Debug.Log("currentNoofColumns: " + currentNoofColumns + " prevNoofColumns: " + prevNoofColumns);
        if (currentNoofColumns > prevNoofColumns)
        {
            mVariableGridLayout.constraintCount++;
            GameObject child;
            /* if (noOfRows < 3)
             {*/
            for (int i = 1; i <= noOfRowsinStep; i++)
            {
                child = (GameObject)Instantiate(GeneralDigit, transform);
                child.transform.parent = temp.transform;
                child.GetComponent<TEXDraw>().text = child.GetComponent<TEXDraw>().text.Insert(0, " ");
                intTestNo++;
                child.name = "Child" + rowNumber.ToString() + intTestNo.ToString();

                switch (Functionmode)
                {
                    case modeAddition:
                        //Debug.Log("Funtion Mode:");
                        if (i < 3)
                        {
                            child.GetComponent<TexDrawUtils>().multiDigitAllowed = true;
                            child.GetComponent<TexDrawUtils>().numberOfDigit = 0;
                        }
                        if (i < 4)
                            child.transform.SetSiblingIndex((i * currentNoofColumns) - 1);
                        else
                            child.transform.SetSiblingIndex((i * currentNoofColumns) - 2);
                        break;
                    case modeLCM:
                        //Debug.Log("LCM Mode:");
                        child.transform.SetSiblingIndex((i - 1) * currentNoofColumns);
                        break;
                    case modeHCF:
                        //Debug.Log("LCM Mode:");
                        child.transform.SetSiblingIndex((i - 1) * currentNoofColumns);
                        break;
                    default:
                        //Debug.Log("Default Mode:");
                        child.transform.SetSiblingIndex((i * currentNoofColumns - 1));
                        break;

                }
            }
            prevNoofColumns = currentNoofColumns;
        }
        GameObject prevTex;
        int prevSerial;
        int pos;
        switch (textBoxTypeMode)
        {
            case modeFraction:
                prevTex = currentWorkingBlock;
                //Debug.Log("fraction mode");
                currentWorkingBlock = (GameObject)Instantiate(Fraction, transform);
                currentWorkingBlock.GetComponent<TexPrefab>().type = UtilityREST.typeFraction;
                currentWorkingBlock.transform.parent = temp.transform;
                currentWorkingBlock.transform.SetSiblingIndex(((noOfRowsinStep) * mVariableGridLayout.constraintCount));
                prevTex.transform.SetSiblingIndex(((noOfRowsinStep) * mVariableGridLayout.constraintCount));
                currentWorkingBlock.name = "newTexFrac" + rowNumber.ToString() + intTestNo.ToString();
                GameObject ParentWorkingBlock = currentWorkingBlock;
                currentWorkingBlock = currentWorkingBlock.transform.GetChild(0).gameObject;
                currentWorkingBlock.GetComponent<TexDrawUtils>().multiDigitAllowed = true;
                currentWorkingTexDraw = currentWorkingBlock.GetComponent<TEXDraw>();
                prevSerial = prevTex.GetComponent<TexDrawUtils>().serialNumber;
                //allTexDrawInaRow.Add(currentWorkingBlock.GetComponent<TexDrawUtils>());
                allTexDrawInaRow.Insert(prevSerial, currentWorkingBlock.GetComponent<TexDrawUtils>());
                currentWorkingBlock.GetComponent<TexDrawUtils>().serialNumber = prevSerial;
                currentWorkingBlock.GetComponent<TexDrawUtils>().digitvalue.Add(0);
                prevTex.GetComponent<TexDrawUtils>().serialNumber = ++prevSerial;
                // Numerator woking
                //Debug.Log("prev tex value: " + prevTex.GetComponent<TexDrawUtils>().valueIndex[prevTex.GetComponent<TexDrawUtils>().valueIndex.Count - 1]);
                pos = prevTex.GetComponent<TexDrawUtils>().valueIndex[prevTex.GetComponent<TexDrawUtils>().valueIndex.Count - 1];
                value = 0;
                //Debug.Log("num pos: " + pos + " values: " + values.Count);
                values.Insert(pos, value);
                currentWorkingBlock.GetComponent<TexDrawUtils>().expValue.Add("op-oround");
                currentWorkingBlock.GetComponent<TexDrawUtils>().expValue.Add("op_num_denom");
                if (fractionBracketAdjusted)
                {
                    if (fractionCounterAfterBracketAdjust > 0)
                    {
                        string val = operators[operators.Count - 1];
                        operators.RemoveAt(operators.Count - 1);
                        int insertpos = operators.Count - 1;
                        operators.Insert(insertpos, val);
                        //Debug.Log("allTexDrawInaRow[allTexDrawInaRow.Count - 2].expIndex[allTexDrawInaRow[allTexDrawInaRow.Count - 2].expIndex.Count - 1]: " + allTexDrawInaRow[allTexDrawInaRow.Count - 3].expIndex[allTexDrawInaRow[allTexDrawInaRow.Count - 3].expIndex.Count - 1]);
                        allTexDrawInaRow[allTexDrawInaRow.Count - 3].expIndex[allTexDrawInaRow[allTexDrawInaRow.Count - 3].expIndex.Count - 1] = insertpos;
                        //Debug.Log("allTexDrawInaRow[allTexDrawInaRow.Count - 2].expIndex[allTexDrawInaRow[allTexDrawInaRow.Count - 2].expIndex.Count - 1]: " + allTexDrawInaRow[allTexDrawInaRow.Count - 3].expIndex[allTexDrawInaRow[allTexDrawInaRow.Count - 3].expIndex.Count - 1]);

                    }
                    operators.Insert(operators.Count - 1, UtilityREST.var_openingBrackets);
                    fractionCounterAfterBracketAdjust++;
                }

                else
                    operators.Add("op-oround");
                int operPos = operators.Count - 1;
                //operators.Add("op-cround");
                display++;
                //values.Add(value);
                currentWorkingBlock.GetComponent<TexDrawUtils>().texType = UtilityREST.F;
                currentWorkingBlock.GetComponent<TexDrawUtils>().texPosition = UtilityREST.Numerator;
                currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex.Add(pos);
                currentWorkingBlock.GetComponent<TexDrawUtils>().expIndex.Add(operPos);
                prevTex.GetComponent<TexDrawUtils>().valueIndex[prevTex.GetComponent<TexDrawUtils>().valueIndex.Count - 1] = ++pos;
                // denominator
                GameObject dem = ParentWorkingBlock.transform.GetChild(1).gameObject;
                pos = prevTex.GetComponent<TexDrawUtils>().valueIndex[prevTex.GetComponent<TexDrawUtils>().valueIndex.Count - 1];
                //Debug.Log("prev tex value dem: " + pos);
                prevSerial = prevTex.GetComponent<TexDrawUtils>().serialNumber;
                allTexDrawInaRow.Insert(prevSerial, dem.GetComponent<TexDrawUtils>());
                dem.GetComponent<TexDrawUtils>().texType = UtilityREST.F;
                dem.GetComponent<TexDrawUtils>().texPosition = UtilityREST.Denominator;
                dem.GetComponent<TexDrawUtils>().serialNumber = prevSerial;
                prevTex.GetComponent<TexDrawUtils>().serialNumber = ++prevSerial;
                //allTexDrawInaRow.Add(dem.GetComponent<TexDrawUtils>());
                //dem.GetComponent<TexDrawUtils>().serialNumber = allTexDrawInaRow.Count - 1;
                dem.GetComponent<TexDrawUtils>().expValue.Add("op-cround");
                if (fractionBracketAdjusted)
                    operators.Insert(operators.Count - 1, UtilityREST.var_closingBrackets);
                else
                    operators.Add("op-cround");
                int operPosDem = operators.Count - 1;
                display++;
                //Debug.Log("dem pos: " + pos);
                values.Insert(pos, value);
                dem.GetComponent<TexDrawUtils>().valueIndex.Add(pos);
                dem.GetComponent<TexDrawUtils>().digitvalue.Add(0);
                dem.GetComponent<TexDrawUtils>().expIndex.Add(operPosDem);
                prevTex.GetComponent<TexDrawUtils>().valueIndex[prevTex.GetComponent<TexDrawUtils>().valueIndex.Count - 1] = ++pos;
                intTestNo++;
                textBoxTypeMode = defaultmode;
                break;
            case modeMixedFraction:
                // Debug.Log("modeMixedFraction mode");
                //as two values are added
                currentNoofColumns++;
                if (currentNoofColumns > prevNoofColumns)
                {
                    mVariableGridLayout.constraintCount++;
                    GameObject child;
                    /* if (noOfRows < 3)
                     {*/
                    for (int i = 1; i <= noOfRowsinStep; i++)
                    {
                        child = (GameObject)Instantiate(GeneralDigit, transform);
                        child.transform.parent = temp.transform;
                        child.GetComponent<TEXDraw>().text = child.GetComponent<TEXDraw>().text.Insert(0, " ");
                        intTestNo++;
                        child.name = "Child" + rowNumber.ToString() + intTestNo.ToString();

                        switch (Functionmode)
                        {
                            case modeBasic:
                                //Debug.Log("Funtion Mode:");
                                if (i < 3)
                                {
                                    child.GetComponent<TexDrawUtils>().multiDigitAllowed = true;
                                    child.GetComponent<TexDrawUtils>().numberOfDigit = 0;
                                }
                                if (i < 4)
                                    child.transform.SetSiblingIndex((i * currentNoofColumns) - 1);
                                else
                                    child.transform.SetSiblingIndex((i * currentNoofColumns) - 2);
                                break;
                            case modeLCM:
                                //Debug.Log("LCM Mode:");
                                child.transform.SetSiblingIndex((i - 1) * currentNoofColumns);
                                break;
                            case modeHCF:
                                //Debug.Log("LCM Mode:");
                                child.transform.SetSiblingIndex((i - 1) * currentNoofColumns);
                                break;
                            default:
                                Debug.Log("Default Mode:");
                                child.transform.SetSiblingIndex((i * currentNoofColumns - 1));
                                break;

                        }
                    }
                }
                prevTex = currentWorkingBlock;
                currentWorkingBlock = (GameObject)Instantiate(GeneralDigit, transform);
                currentWorkingBlock.GetComponent<TexPrefab>().type = UtilityREST.typeMixedFraction;
                currentWorkingBlock.transform.parent = temp.transform;
                currentWorkingBlock.transform.SetSiblingIndex((noOfRowsinStep) * mVariableGridLayout.constraintCount);
                currentWorkingBlock.name = "newTexmix" + rowNumber.ToString() + intTestNo.ToString();
                currentWorkingTexDraw = currentWorkingBlock.GetComponent<TEXDraw>();
                currentWorkingTexDraw.text = currentWorkingTexDraw.text.Insert(0, " ");
                currentWorkingBlock = (GameObject)Instantiate(Fraction, transform);
                currentWorkingBlock.GetComponent<TexPrefab>().type = UtilityREST.typeMixedFraction;
                GameObject ParentWorkingBlockFrac = currentWorkingBlock;
                currentWorkingBlock.transform.parent = temp.transform;
                currentWorkingBlock.transform.SetSiblingIndex(((noOfRowsinStep) * mVariableGridLayout.constraintCount));
                prevTex.transform.SetSiblingIndex(((noOfRowsinStep) * mVariableGridLayout.constraintCount));
                currentWorkingBlock.name = "newTexMixFrac" + rowNumber.ToString() + intTestNo.ToString();
                currentWorkingBlock = currentWorkingTexDraw.gameObject;
                currentWorkingBlock.GetComponent<TexDrawUtils>().multiDigitAllowed = true;
                prevSerial = prevTex.GetComponent<TexDrawUtils>().serialNumber;

                //allTexDrawInaRow.Add(currentWorkingBlock.GetComponent<TexDrawUtils>());
                allTexDrawInaRow.Insert(prevSerial, currentWorkingBlock.GetComponent<TexDrawUtils>());
                currentWorkingBlock.GetComponent<TexDrawUtils>().serialNumber = prevSerial;
                prevTex.GetComponent<TexDrawUtils>().serialNumber = ++prevSerial;
                //currentWorkingBlock.GetComponent<TexDrawUtils>().serialNumber = allTexDrawInaRow.Count - 1;
                //value = 0;
                //values.Add(value);
                pos = prevTex.GetComponent<TexDrawUtils>().valueIndex[prevTex.GetComponent<TexDrawUtils>().valueIndex.Count - 1];
                value = 0;
                values.Insert(pos, value);
                currentWorkingBlock.GetComponent<TexDrawUtils>().digitvalue.Add(0);
                currentWorkingBlock.GetComponent<TexDrawUtils>().texType = UtilityREST.MF;
                currentWorkingBlock.GetComponent<TexDrawUtils>().texPosition = UtilityREST.General;
                currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex.Add(pos);
                //currentWorkingBlock.GetComponent<TexDrawUtils>().digitvalue.Add(0);
                prevTex.GetComponent<TexDrawUtils>().valueIndex[prevTex.GetComponent<TexDrawUtils>().valueIndex.Count - 1] = ++pos;
                //currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex.Add(values.Count - 1);
                // set up the fraction Numerator

                GameObject frac = ParentWorkingBlockFrac.transform.GetChild(0).gameObject;
                prevSerial = prevTex.GetComponent<TexDrawUtils>().serialNumber;
                //allTexDrawInaRow.Add(frac.GetComponent<TexDrawUtils>());
                allTexDrawInaRow.Insert(prevSerial, frac.GetComponent<TexDrawUtils>());
                frac.GetComponent<TexDrawUtils>().serialNumber = prevSerial;
                prevTex.GetComponent<TexDrawUtils>().serialNumber = ++prevSerial;
                //values.Add(value);
                pos = prevTex.GetComponent<TexDrawUtils>().valueIndex[prevTex.GetComponent<TexDrawUtils>().valueIndex.Count - 1];
                //value = 0;
                values.Insert(pos, value);
                frac.GetComponent<TexDrawUtils>().digitvalue.Add(0);
                frac.GetComponent<TexDrawUtils>().texType = UtilityREST.MF;
                frac.GetComponent<TexDrawUtils>().texPosition = UtilityREST.Numerator;
                frac.GetComponent<TexDrawUtils>().valueIndex.Add(pos);
                //frac.GetComponent<TexDrawUtils>().digitvalue.Add(0);
                prevTex.GetComponent<TexDrawUtils>().valueIndex[prevTex.GetComponent<TexDrawUtils>().valueIndex.Count - 1] = ++pos;
                //frac.GetComponent<TexDrawUtils>().valueIndex.Add(values.Count - 1);
                // denominator
                frac = ParentWorkingBlockFrac.transform.GetChild(1).gameObject;
                //allTexDrawInaRow.Add(frac.gameObject.GetComponent<TexDrawUtils>());
                allTexDrawInaRow.Insert(prevSerial, frac.GetComponent<TexDrawUtils>());
                frac.GetComponent<TexDrawUtils>().serialNumber = prevSerial;
                prevTex.GetComponent<TexDrawUtils>().serialNumber = ++prevSerial;
                //frac.GetComponent<TexDrawUtils>().serialNumber = allTexDrawInaRow.Count - 1;
                //values.Add(value);
                //frac.GetComponent<TexDrawUtils>().digitvalue.Add(0);
                values.Insert(pos, value);
                frac.GetComponent<TexDrawUtils>().texType = UtilityREST.MF;
                frac.GetComponent<TexDrawUtils>().texPosition = UtilityREST.Denominator;
                frac.GetComponent<TexDrawUtils>().valueIndex.Add(pos);
                frac.GetComponent<TexDrawUtils>().digitvalue.Add(0);
                prevTex.GetComponent<TexDrawUtils>().valueIndex[prevTex.GetComponent<TexDrawUtils>().valueIndex.Count - 1] = ++pos;
                //frac.GetComponent<TexDrawUtils>().valueIndex.Add(values.Count - 1);
                //prevTex.name = "newTex" + intTestNo.ToString();
                intTestNo++;
                textBoxTypeMode = defaultmode;
                break;
            case modeTextValue:
                prevTex = currentWorkingBlock;
                currentWorkingBlock = (GameObject)Instantiate(GeneralDigit, transform);
                currentWorkingBlock.GetComponent<TexPrefab>().type = UtilityREST.typeText;
                currentWorkingBlock.transform.parent = temp.transform;
                currentWorkingBlock.transform.SetSiblingIndex(((noOfRowsinStep) * mVariableGridLayout.constraintCount));
                prevTex.transform.SetSiblingIndex(((noOfRowsinStep) * mVariableGridLayout.constraintCount));
                currentWorkingBlock.name = "newTexText" + rowNumber.ToString() + intTestNo.ToString();
                ParentWorkingBlock = currentWorkingBlock;
                currentWorkingTexDraw = currentWorkingBlock.GetComponent<TEXDraw>();
                currentWorkingBlock.GetComponent<TexDrawUtils>().multiDigitAllowed = true;
                prevSerial = prevTex.GetComponent<TexDrawUtils>().serialNumber;
                currentWorkingBlock.GetComponent<TexDrawUtils>().texType = UtilityREST.Text;
                currentWorkingBlock.GetComponent<TexDrawUtils>().texPosition = UtilityREST.Text;
                allTexDrawInaRow.Insert(prevSerial, currentWorkingBlock.GetComponent<TexDrawUtils>());
                currentWorkingBlock.GetComponent<TexDrawUtils>().serialNumber = prevSerial;
                currentWorkingBlock.GetComponent<TexDrawUtils>().digitvalue.Add(0);
                prevTex.GetComponent<TexDrawUtils>().serialNumber = ++prevSerial;
                textBoxTypeMode = defaultmode;

                break;
            default:
                currentWorkingBlock = (GameObject)Instantiate(GeneralDigit, transform);
                currentWorkingBlock.GetComponent<TexPrefab>().type = UtilityREST.typeGeneral;
                currentWorkingBlock.transform.parent = temp.transform;
                if (nexttextback)
                {
                    currentWorkingBlock.transform.SetSiblingIndex(temp.transform.childCount - 2);
                    //allTexDrawInaRow.Insert(0,currentWorkingBlock.GetComponent<TexDrawUtils>());
                }
                else
                {
                    currentWorkingBlock.transform.SetSiblingIndex((noOfRowsinStep) * mVariableGridLayout.constraintCount);

                }

                currentWorkingBlock.name = "newTex" + rowNumber.ToString() + intTestNo.ToString();
                intTestNo++;
                allTexDrawInaRow.Add(currentWorkingBlock.GetComponent<TexDrawUtils>());
                currentWorkingTexDraw = currentWorkingBlock.GetComponent<TEXDraw>();
                currentWorkingTexDraw.text = currentWorkingTexDraw.text.Insert(0, texValue);
                // setup bloc

                currentWorkingBlock.GetComponent<TexDrawUtils>().serialNumber = allTexDrawInaRow.Count - 1;
                currentWorkingBlock.GetComponent<TexDrawUtils>().texType = UtilityREST.General;
                currentWorkingBlock.GetComponent<TexDrawUtils>().texPosition = UtilityREST.General;
                if (nextValue)
                {
                    //Debug.Log("next Value");

                    value = 0;
                    if (fromGeneral)
                    {
                        values.Add(value);
                    }

                    valueIndex = values.Count - 1;
                    nextValue = false;

                }
                //Debug.Log("value new tex valueIndex: " + valueIndex);
                currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex.Add(valueIndex);
                currentWorkingBlock.GetComponent<TexDrawUtils>().faceValue = 0;
                instanciateOnThisBlock = currentWorkingBlock;
                //Debug.Log("instanciateOnThisBlock.name: " + instanciateOnThisBlock.name);
                break;
        }

        // instanciateOnThisBlock = currentWorkingBlock;
    }

    void ChangePreviousplacevalue(int noOfValues)
    {
        //Debug.Log("noOfValues: " + noOfValues);
        if (noOfValues > 0)
        {
            int i = currentWorkingBlock.GetComponent<TexDrawUtils>().serialNumber - 1;
            for (int j = 0; j < noOfValues; j++)
            {
                TexDrawUtils temp = allTexDrawInaRow[i--];
                //Debug.Log("face Value: " + temp.faceValue + " placValue: " + temp.placeValue);
                temp.placeValue++;
            }
        }
    }

    void NumberClicked(int number)
    {
        if (isActive)
        {
            valueentered++;
            digitentered = true;
            canAddFraction = true;
            valueEnteredinList = false;
            //Debug.Log("value: " + value + "isanswer: "+ isanswer);
            if (isanswer)
            {
                // Debug.Log("value: " + value);
                if (selectedBypointer && !currentWorkingBlock.GetComponent<TexDrawUtils>().multiDigitAllowed && !(string.Compare(instanciateOnThisBlock.name, currentWorkingBlock.name) == 0))
                {
                    //re selected answer area ;
                    Debug.Log("place carry value: " + placeValue);
                    int temp = currentWorkingBlock.GetComponent<TexDrawUtils>().faceValue * (int)(Math.Pow(10, currentWorkingBlock.GetComponent<TexDrawUtils>().placeValue));
                    value -= currentWorkingBlock.GetComponent<TexDrawUtils>().faceValue * (int)(Math.Pow(10, currentWorkingBlock.GetComponent<TexDrawUtils>().placeValue));
                    int temp_number = number * (int)(Math.Pow(10, currentWorkingBlock.GetComponent<TexDrawUtils>().placeValue));
                    currentWorkingBlock.GetComponent<TexDrawUtils>().faceValue = number;
                    Debug.Log("carry" + temp_number);
                    value += temp_number;
                }
                else
                {
                    Debug.Log("place value: " + placeValue);
                    int pow = value.ToString().Length;
                    if (value > 0)
                    {
                        //(int)Math.Floor(Math.Log10(value) + 1);
                        if (previous_value_zero)
                        {
                            pow++;
                            previous_value_zero = false;
                        }

                        //Debug.Log("number of digits: " + pow);
                        Debug.Log("value:>0 " + value);
                        value = ((int)(Math.Pow(10, pow)) * number) + value;
                        Debug.Log("value: " + value);

                    }
                    else
                    {
                        Debug.Log("value: < 0" + value);
                        if (selectedBypointer && !currentWorkingBlock.GetComponent<TexDrawUtils>().multiDigitAllowed && (string.Compare(instanciateOnThisBlock.name, currentWorkingBlock.name) == 0))
                        {
                            if (previous_value_zero)
                            {


                                //Debug.Log("pow: " + pow);
                                previous_value_zero = false;
                                value = ((int)(Math.Pow(10, pow)) * number) + value;
                                Debug.Log("value: " + value);
                            }
                            else
                                value = number;
                        }
                        else
                        {
                            value = number;
                        }

                        //Debug.Log("value: < 0" + value);
                    }
                    if (number == 0)
                        previous_value_zero = true;
                    currentWorkingBlock.GetComponent<TexDrawUtils>().faceValue = number;
                    currentWorkingBlock.GetComponent<TexDrawUtils>().placeValue = placeValue;
                    if (!currentWorkingBlock.GetComponent<TexDrawUtils>().multiDigitAllowed)
                        placeValue++;
                }
            }
            else
            {
                if (selectedBypointer && !currentWorkingBlock.GetComponent<TexDrawUtils>().multiDigitAllowed && !(string.Compare(instanciateOnThisBlock.name, currentWorkingBlock.name) == 0))
                {
                    //Debug.Log("val: " + value);
                    //Debug.Log("faceValue: "+ currentWorkingBlock.GetComponent<TexDrawUtils>().faceValue+ " placeValue: "+ currentWorkingBlock.GetComponent<TexDrawUtils>().placeValue);
                    int temp = currentWorkingBlock.GetComponent<TexDrawUtils>().faceValue * (int)(Math.Pow(10, currentWorkingBlock.GetComponent<TexDrawUtils>().placeValue));
                    //Debug.Log("temp: " + temp);
                    value -= currentWorkingBlock.GetComponent<TexDrawUtils>().faceValue * (int)(Math.Pow(10, currentWorkingBlock.GetComponent<TexDrawUtils>().placeValue));
                    int temp_number = number * (int)(Math.Pow(10, currentWorkingBlock.GetComponent<TexDrawUtils>().placeValue));
                    currentWorkingBlock.GetComponent<TexDrawUtils>().faceValue = number;
                    value += temp_number;
                    //Debug.Log("val: " + value);
                }
                else
                {
                    value = value * 10 + number;
                    //Debug.Log("number value :" + value);
                    if (!currentWorkingBlock.GetComponent<TexDrawUtils>().multiDigitAllowed)
                        ChangePreviousplacevalue(placeValue++);
                }
                currentWorkingBlock.GetComponent<TexDrawUtils>().faceValue = number;
                currentWorkingBlock.GetComponent<TexDrawUtils>().placeValue = 0;


            }

            StopblinkingCursor();
            if (Functionmode == modeDivision)
            {
                if (noOfRowsinStep == 1)
                {
                    if (currentWorkingBlock.GetComponent<TexDrawUtils>().multiDigitAllowed && currentWorkingBlock.GetComponent<TexDrawUtils>().numberOfDigit > 0)
                    {
                    }
                    else
                        currentWorkingTexDraw.text = currentWorkingTexDraw.text.Remove(currentWorkingTexDraw.text.Length - 1);
                    currentWorkingBlock.GetComponent<TexDrawUtils>().numberOfDigit++;
                    currentWorkingBlock.GetComponent<TexDrawUtils>().lastValueNoOfDigits++;
                    currentWorkingTexDraw.text = currentWorkingTexDraw.text.Insert(currentWorkingTexDraw.text.Length, number.ToString());
                    if (currentWorkingBlock.GetComponent<TexDrawUtils>().multiDigitAllowed)
                    {
                        //currentWorkingTexDraw.text = currentWorkingTexDraw.text.Insert(currentWorkingTexDraw.text.Length, " ");
                        currentMaxSpaceInRow++;
                    }
                    else
                    {
                        // Debug.Log("instanciateOnThisBlock.name: " + instanciateOnThisBlock.name + " :currentWorkingBlock.name: " + currentWorkingBlock.name);

                        if (string.Compare(instanciateOnThisBlock.name, currentWorkingBlock.name) == 0)
                        {
                            getNewCurrentWorkingTex();
                            currentMaxSpaceInRow++;
                        }

                    }
                }
                else
                {
                    if (string.Compare(instanciateOnThisBlock.name, currentWorkingBlock.name) == 0)
                    {

                    }
                    else
                    {
                        currentWorkingTexDraw.text = currentWorkingTexDraw.text.Remove(currentWorkingTexDraw.text.Length - 1);
                        currentWorkingBlock.GetComponent<TexDrawUtils>().numberOfDigit++;
                        currentWorkingBlock.GetComponent<TexDrawUtils>().lastValueNoOfDigits++;
                        currentWorkingTexDraw.text = currentWorkingTexDraw.text.Insert(currentWorkingTexDraw.text.Length, number.ToString());
                        currentWorkingBlock = currentWorkingBlock.transform.parent.GetChild(currentWorkingBlock.transform.GetSiblingIndex() - 1).gameObject;
                        currentWorkingTexDraw = currentWorkingBlock.GetComponent<TEXDraw>();

                    }

                }

            }
            else
            {
                if (currentWorkingBlock.GetComponent<TexDrawUtils>().multiDigitAllowed && currentWorkingBlock.GetComponent<TexDrawUtils>().numberOfDigit > 0)
                {
                }
                else
                    currentWorkingTexDraw.text = currentWorkingTexDraw.text.Remove(currentWorkingTexDraw.text.Length - 1);
                currentWorkingBlock.GetComponent<TexDrawUtils>().numberOfDigit++;
                //used for counting and geting block;
                currentWorkingBlock.GetComponent<TexDrawUtils>().lastValueNoOfDigits++;
                currentWorkingTexDraw.text = currentWorkingTexDraw.text.Insert(currentWorkingTexDraw.text.Length, number.ToString());


                if (currentWorkingBlock.GetComponent<TexDrawUtils>().multiDigitAllowed)
                {
                    //currentWorkingTexDraw.text = currentWorkingTexDraw.text.Insert(currentWorkingTexDraw.text.Length, " ");
                    currentMaxSpaceInRow++;
                }
                else
                {
                    // Debug.Log("instanciateOnThisBlock.name: " + instanciateOnThisBlock.name + " :currentWorkingBlock.name: " + currentWorkingBlock.name);

                    if (string.Compare(instanciateOnThisBlock.name, currentWorkingBlock.name) == 0)
                    {
                        // Debug.Log("Val: " + value);
                        getNewCurrentWorkingTex();
                        currentMaxSpaceInRow++;
                    }



                }
            }
            //Debug.Log("Val: " + value);
            StartBlinkingCursor();
        }

    }

    void ExpressionClicked(string exp)
    {
        if (isActive)
        {
            StopblinkingCursor();
            valueentered++;
            canAddFraction = true;
            valueEnteredinList = false;
            switch (Functionmode)
            {
                case modeBasic:
                    //Debug.Log("Funtion Mode:");
                    break;
                case modeAddition:
                    StartBlinkingCursor();/*
                    Debug.Log("in here");
                    allTexDrawInaRow.RemoveAt(allTexDrawInaRow.Count - 1);
                    GotoNextLine();*/
                    if (string.Equals(exp, "+"))
                    {
                        allTexDrawInaRow.RemoveAt(allTexDrawInaRow.Count - 1);
                        GotoNextLine();
                    }
                    else if (string.Equals(exp, "="))
                    {
                        //removeAllvalueIndex();
                        //Answer();
                        //valueEnteredinList = false;

                    }
                    StopblinkingCursor();
                    break;
                case modeLCM:
                    //Debug.Log("Lcm Mode");
                    if (string.Compare(exp, ",") == 0)
                    {
                        if (currentWorkingBlock.GetComponent<TexDrawUtils>().multiDigitAllowed && currentWorkingBlock.GetComponent<TexDrawUtils>().numberOfDigit > 0)
                        {
                        }
                        else
                            currentWorkingTexDraw.text = currentWorkingTexDraw.text.Remove(currentWorkingTexDraw.text.Length - 1);
                        currentWorkingBlock.GetComponent<TexDrawUtils>().numberOfDigit++;
                        currentWorkingTexDraw.text = currentWorkingTexDraw.text.Insert(currentWorkingTexDraw.text.Length, exp);
                        if (currentWorkingBlock.GetComponent<TexDrawUtils>().multiDigitAllowed)
                        {
                            //currentWorkingTexDraw.text = currentWorkingTexDraw.text.Insert(currentWorkingTexDraw.text.Length, " ");
                        }
                        else
                        {
                            // Debug.Log("instanciateOnThisBlock.name: " + instanciateOnThisBlock.name + " :currentWorkingBlock.name: " + currentWorkingBlock.name);
                            if (string.Compare(instanciateOnThisBlock.name, currentWorkingBlock.name) == 0)
                                getNewCurrentWorkingTex();
                        }
                    }
                    break;
                case modeHCF:
                    //Debug.Log("HCF Mode");
                    /*
                    if (string.Compare(exp, ",") == 0)
                    {
                        if (currentWorkingBlock.GetComponent<TexDrawUtils>().multiDigitAllowed && currentWorkingBlock.GetComponent<TexDrawUtils>().numberOfDigit > 0)
                        {
                        }
                        else
                            currentWorkingTexDraw.text = currentWorkingTexDraw.text.Remove(currentWorkingTexDraw.text.Length - 1);
                        currentWorkingBlock.GetComponent<TexDrawUtils>().numberOfDigit++;
                        currentWorkingTexDraw.text = currentWorkingTexDraw.text.Insert(currentWorkingTexDraw.text.Length, exp);
                        if (currentWorkingBlock.GetComponent<TexDrawUtils>().multiDigitAllowed)
                        {
                            //currentWorkingTexDraw.text = currentWorkingTexDraw.text.Insert(currentWorkingTexDraw.text.Length, " ");
                        }
                        else
                        {
                            // Debug.Log("instanciateOnThisBlock.name: " + instanciateOnThisBlock.name + " :currentWorkingBlock.name: " + currentWorkingBlock.name);
                            if (string.Compare(instanciateOnThisBlock.name, currentWorkingBlock.name) == 0)
                                getNewCurrentWorkingTex();
                        }
                    }*/
                    break;
                default:
                    //Debug.Log("Default Mode:");

                    if (currentWorkingBlock.GetComponent<TexDrawUtils>().multiDigitAllowed && currentWorkingBlock.GetComponent<TexDrawUtils>().numberOfDigit > 0)
                    {
                    }
                    else
                        currentWorkingTexDraw.text = currentWorkingTexDraw.text.Remove(currentWorkingTexDraw.text.Length - 1);
                    currentWorkingBlock.GetComponent<TexDrawUtils>().numberOfDigit++;

                    currentWorkingTexDraw.text = currentWorkingTexDraw.text.Insert(currentWorkingTexDraw.text.Length, exp);

                    display += 2;
                    step_attribute.NUM++;
                    step_attribute.NUMOP++;
                    step_attribute.TOTOP++;

                    step_attribute.TOTATT++;
                    //Debug.Log("Default TOTATT : " + step_attribute.TOTATT);
                    if (step_attribute.F > 0)
                        step_attribute.F = 0;
                    if (digitentered)
                    {
                        /*
                        values.Add(value);
                        currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex.Add(values.Count - 1);*/
                        // if a digit isentered add value to the digitvalue and the exp to ecplist
                        //Debug.Log("count current " + currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex.Count);
                        if (currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex.Count == 0)
                        {
                            values.Add(value);
                            value = 0;
                            currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex.Add(values.Count - 1);
                        }
                        else
                        {
                            int index = currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex[currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex.Count - 1];
                            // Debug.Log("index current " + index);
                            if (currentWorkingBlock.GetComponent<TexDrawUtils>().multiDigitAllowed)
                            {
                                // as single digit has single value
                                currentWorkingBlock.GetComponent<TexDrawUtils>().digitvalue[currentWorkingBlock.GetComponent<TexDrawUtils>().digitvalue.Count - 1] = value;
                                //currentWorkingBlock.GetComponent<TexDrawUtils>().digitvalue.Add(0);
                            }


                            if (operSighnAdded && currentWorkingBlock.GetComponent<TexDrawUtils>().multiDigitAllowed)
                            {
                                values.Insert(index + 1, value);
                                currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex.Add(index + 1);
                                chagebelowtextUtil(currentWorkingBlock.GetComponent<TexDrawUtils>().serialNumber);

                            }
                            else
                                values[index] = value;
                            //values[index] = value;
                            value = 0;
                            valuefromList = false;
                            //currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex.Add(index);
                        }
                        digitentered = false;

                    }
                    if (currentWorkingBlock.GetComponent<TexDrawUtils>().multiDigitAllowed)
                    {
                        // as single digit has single value
                        //currentWorkingBlock.GetComponent<TexDrawUtils>().digitvalue[currentWorkingBlock.GetComponent<TexDrawUtils>().digitvalue.Count - 1] = value;
                        currentWorkingBlock.GetComponent<TexDrawUtils>().digitvalue.Add(0);
                    }
                    value = 0;
                    addExpToOpt(exp);
                    if (!singleStep)
                    {
                        operatedClicked = true;
                        operSighnAdded = true;
                    }
                    else
                    {
                        // it is from single step delete last tes util from all

                    }
                    if (currentWorkingBlock.GetComponent<TexDrawUtils>().multiDigitAllowed)
                    {
                        // currentWorkingTexDraw.text = currentWorkingTexDraw.text.Insert(currentWorkingTexDraw.text.Length, " ");
                    }
                    else
                    {
                        // Debug.Log("instanciateOnThisBlock.name: " + instanciateOnThisBlock.name + " :currentWorkingBlock.name: " + currentWorkingBlock.name);
                        if (string.Compare(instanciateOnThisBlock.name, currentWorkingBlock.name) == 0)
                        {
                            TexDrawUtils prevTex;
                            if (currentWorkingBlock.GetComponent<TexDrawUtils>().serialNumber > 0)
                            {
                                prevTex = allTexDrawInaRow[currentWorkingBlock.GetComponent<TexDrawUtils>().serialNumber - 1];
                                if (String.Compare(prevTex.texType, UtilityREST.General) == 0)
                                {
                                    fromGeneral = true;
                                }
                                else
                                {
                                    fromGeneral = false;
                                    //nextValue = false;
                                }
                            }
                            else
                            {
                                fromGeneral = false;
                            }

                            // check if the prev value is mixed fraction or fraction then no need to add value in list 

                            nextValue = true;
                            placeValue = 0;
                            getNewCurrentWorkingTex();
                            // Debug.Log("ExpressionClicked");

                        }

                    }


                    break;
            }
            StartBlinkingCursor();
        }

    }

    bool checkePreviousoperator(string exp)
    {
        bool value = false;
        int operaterIndex = operators.Count - 1;
        if (hasBracked)
            operaterIndex = operators.Count - 2;
        // case if implemented ib fraction
        //if (String.Equals(currentWorkingBlock.GetComponent<TexDrawUtils>().texType, UtilityREST.F))
        //{
        //    operaterIndex = operators.Count - 2;
        //    if (hasBracked)
        //        operaterIndex = operators.Count - 3;
        //}



        switch (exp)
        {
            case "+":
                if (String.Equals("op_addition", operators[operaterIndex]) || String.Equals("op_subtraction", operators[operaterIndex]))
                    value = true;
                else
                    value = false;
                break;
            case "-":
                if (String.Equals("op_subtraction", operators[operaterIndex]) || String.Equals("op_addition", operators[operaterIndex]))
                    value = true;
                else
                    value = false;
                break;
            case "\\times":
                if (String.Equals("op_multiply", operators[operaterIndex]) || String.Equals("op_divide", operators[operaterIndex]))
                    value = true;
                else
                    value = false;
                break;
            case "\\div":
                if (String.Equals("op_divide", operators[operaterIndex]) || String.Equals("op_multiply", operators[operaterIndex]))
                    value = true;
                else
                    value = false;
                break;
            case "=":
                // need to do someting for this

                break;
            case "\not[0-0]{=}":
                break;
            default:

                break;
        }
        return value;
    }

    int prevExpvalueCounter = 0;
    int OpeningBracketcounter = 0;
    bool openingBracketAdded = false;
    bool hasBracked = false;

    void adjustBracakets(string exp)
    {
        // add relevent brackets if necessary
        // Debug.Log("Adjust Bracket");
        int expIndex = 0;
        if (operators.Count > 0)
        {
            if (checkePreviousoperator(exp))
            {
                //Debug.Log("current operator = prevoperator");
                prevExpvalueCounter++;
            }
            else
            {
                hasBracked = false;
                //Debug.Log("current operator != prevoperator"+ operators[operators.Count - 1]);
                // check where the brackets will be added i.e q+(b*c)  or (c*g)+f
                if (String.Equals(operators[operators.Count - 1], UtilityREST.var_openingBrackets))
                {
                    //Debug.Log("operators[operators.Count - 1]" + operators[operators.Count - 1]);
                    return;
                }
                else
                {
                    //Debug.Log("current operator != prevoperator" + operators[operators.Count - 1]);
                    // check what is previous operator
                    switch (operators[operators.Count - 1])
                    {
                        case UtilityREST.var_addition:
                            //Debug.Log("+ previous");
                            if (String.Equals(exp, UtilityREST.sighn_multiplication) || String.Equals(exp, UtilityREST.sighn_division))
                            {
                                // Debug.Log("multiplication");
                                if (currentWorkingBlock.GetComponent<TexDrawUtils>().expIndex.Count < 1)
                                {
                                    //Debug.Log("adding opeing bracket in exp count <1");
                                    operators.Add(UtilityREST.var_openingBrackets);
                                    expIndex = operators.Count - 1;
                                    openingBracketAdded = true;
                                }
                                else
                                {
                                    //Debug.Log("adding opeing bracket in exp count > 1");
                                    int index = currentWorkingBlock.GetComponent<TexDrawUtils>().expIndex[currentWorkingBlock.GetComponent<TexDrawUtils>().expIndex.Count - 1];
                                    if (String.Compare(currentWorkingBlock.GetComponent<TexDrawUtils>().texType, UtilityREST.F) == 0 && String.Compare(currentWorkingBlock.GetComponent<TexDrawUtils>().texPosition, UtilityREST.Denominator) == 0)
                                        expIndex = index;
                                    else
                                        expIndex = index + 1;
                                    int operatorIndex = 0;
                                    if (String.Equals(currentWorkingBlock.GetComponent<TexDrawUtils>().texType, UtilityREST.F))
                                    {
                                        operatorIndex = currentWorkingBlock.GetComponent<TexDrawUtils>().expValue.Count - 1;
                                    }
                                    operators.Insert(expIndex, UtilityREST.var_openingBrackets);
                                    currentWorkingBlock.GetComponent<TexDrawUtils>().expValue.Insert(operatorIndex, UtilityREST.var_openingBrackets);
                                    OpeningBracketcounter++;
                                }
                                currentWorkingBlock.GetComponent<TexDrawUtils>().numberOfExp++;
                                currentWorkingBlock.GetComponent<TexDrawUtils>().expIndex.Add(expIndex);
                                hasBracked = true;
                            }
                            break;
                        case UtilityREST.var_subtraction:
                            if (String.Equals(exp, UtilityREST.sighn_multiplication) || String.Equals(exp, UtilityREST.sighn_division))
                            {
                                if (currentWorkingBlock.GetComponent<TexDrawUtils>().expIndex.Count < 1)
                                {
                                    operators.Add(UtilityREST.var_openingBrackets);
                                    expIndex = operators.Count - 1;
                                    openingBracketAdded = true;
                                }
                                else
                                {
                                    int index = currentWorkingBlock.GetComponent<TexDrawUtils>().expIndex[currentWorkingBlock.GetComponent<TexDrawUtils>().expIndex.Count - 1];
                                    if (String.Compare(currentWorkingBlock.GetComponent<TexDrawUtils>().texType, UtilityREST.F) == 0 && String.Compare(currentWorkingBlock.GetComponent<TexDrawUtils>().texPosition, UtilityREST.Denominator) == 0)
                                        expIndex = index;
                                    else
                                        expIndex = index + 1;
                                    int operatorIndex = 0;
                                    if (String.Equals(currentWorkingBlock.GetComponent<TexDrawUtils>().texType, UtilityREST.F))
                                    {
                                        operatorIndex = currentWorkingBlock.GetComponent<TexDrawUtils>().expValue.Count - 1;
                                    }
                                    operators.Insert(expIndex, UtilityREST.var_openingBrackets);
                                    currentWorkingBlock.GetComponent<TexDrawUtils>().expValue.Insert(operatorIndex, UtilityREST.var_openingBrackets);
                                    OpeningBracketcounter++;
                                }
                                currentWorkingBlock.GetComponent<TexDrawUtils>().numberOfExp++;
                                currentWorkingBlock.GetComponent<TexDrawUtils>().expIndex.Add(expIndex);
                                hasBracked = true;
                            }
                            break;
                        case UtilityREST.var_multiplication:
                            if (String.Equals(exp, UtilityREST.sighn_addition) || String.Equals(exp, UtilityREST.sighn_subtraction))
                            {
                                // go back n steps to add opening bracket
                                //Debug.Log("multiply " + prevExpvalueCounter);
                                int valuestogo = prevExpvalueCounter + 1;
                                int insetPosition = operators.Count - (valuestogo);
                                //Debug.Log("insetPosition: " + insetPosition + " operators.Count: "+ operators.Count+ " valuestogo: "+ valuestogo);
                                operators.Insert(insetPosition, UtilityREST.var_openingBrackets);
                                currentWorkingBlock.GetComponent<TexDrawUtils>().expIndex.Add(insetPosition);
                                operators.Add(UtilityREST.var_closingBrackets);
                                currentWorkingBlock.GetComponent<TexDrawUtils>().expIndex.Add(operators.Count - 1);
                            }
                            break;
                        case UtilityREST.var_division:
                            if (String.Equals(exp, UtilityREST.sighn_addition) || String.Equals(exp, UtilityREST.sighn_subtraction))
                            {
                                // go back n steps to add opening bracket
                                //Debug.Log("multiply " + prevExpvalueCounter);
                                int valuestogo = prevExpvalueCounter + 1;
                                int insetPosition = operators.Count - (valuestogo);
                                //Debug.Log("insetPosition: " + insetPosition + " operators.Count: " + operators.Count + " valuestogo: " + valuestogo);
                                operators.Insert(insetPosition, UtilityREST.var_openingBrackets);
                                currentWorkingBlock.GetComponent<TexDrawUtils>().expIndex.Add(insetPosition);
                                operators.Add(UtilityREST.var_closingBrackets);
                                currentWorkingBlock.GetComponent<TexDrawUtils>().expIndex.Add(operators.Count - 1);
                            }
                            break;
                        default:

                            break;
                    }
                }
            }
        }
        else
        {
            // Debug.Log("operators = 0 ");
        }
    }

    void addExpToOpt(string exp)
    {
        /*
        Debug.Log(exp + " exp");
        if (String.Compare(exp, "\\times") == 0)
            Debug.Log("comparision happening");*/
        int expIndex = 0;
        // Debug.Log("before adjust " + hasBracked);
        adjustBracakets(exp);
        if (currentWorkingBlock.GetComponent<TexDrawUtils>().expIndex.Count < 1)
        {
            //Debug.Log("expIndex.Count < 1" + "hasBracked:"+ hasBracked);
            if (hasBracked)
            {
                Debug.Log("entered here");
                expIndex = expIndex = operators.Count - 1;
                switch (exp)
                {
                    case "+":
                        operators.Insert(expIndex, "op_addition");
                        currentWorkingBlock.GetComponent<TexDrawUtils>().expValue.Add("op_addition");
                        break;
                    case "-":
                        operators.Insert(expIndex, "op_subtraction");
                        currentWorkingBlock.GetComponent<TexDrawUtils>().expValue.Add("op_subtraction");
                        break;
                    case "\\times":
                        operators.Insert(expIndex, "op_multiply");
                        currentWorkingBlock.GetComponent<TexDrawUtils>().expValue.Add("op_multiply");
                        break;
                    case "\\div":
                        operators.Insert(expIndex, "op_divide");
                        currentWorkingBlock.GetComponent<TexDrawUtils>().expValue.Add("op_divide");
                        break;
                    case "=":
                        operators.Insert(expIndex, "op_isequal");
                        currentWorkingBlock.GetComponent<TexDrawUtils>().expValue.Add("op_isequal");
                        break;
                    case "\\not[0-0]{=}":
                        operators.Insert(expIndex, "op_isnotequal");
                        currentWorkingBlock.GetComponent<TexDrawUtils>().expValue.Add("op_isnotequal");
                        break;
                    case "(":
                        operators.Insert(expIndex, UtilityREST.var_openingBrackets);
                        currentWorkingBlock.GetComponent<TexDrawUtils>().expValue.Add("op_isequal");
                        break;
                    case ")":
                        operators.Insert(expIndex, UtilityREST.var_closingBrackets);
                        currentWorkingBlock.GetComponent<TexDrawUtils>().expValue.Add("op_isequal");
                        break;
                    default:
                        operators.Insert(expIndex, "op_unknown");
                        currentWorkingBlock.GetComponent<TexDrawUtils>().expValue.Add("op_unknown");
                        break;
                }

            }
            else
            {
                switch (exp)
                {
                    case "+":
                        operators.Add("op_addition");
                        currentWorkingBlock.GetComponent<TexDrawUtils>().expValue.Add("op_addition");
                        break;
                    case "-":
                        operators.Add("op_subtraction");
                        currentWorkingBlock.GetComponent<TexDrawUtils>().expValue.Add("op_subtraction");
                        break;
                    case "\\times":
                        operators.Add("op_multiply");
                        currentWorkingBlock.GetComponent<TexDrawUtils>().expValue.Add("op_multiply");
                        break;
                    case "\\div":
                        operators.Add("op_divide");
                        currentWorkingBlock.GetComponent<TexDrawUtils>().expValue.Add("op_divide");
                        break;
                    case "=":
                        operators.Add("op_isequal");
                        currentWorkingBlock.GetComponent<TexDrawUtils>().expValue.Add("op_isequal");
                        break;
                    case "\\not[0-0]{=}":
                        operators.Add("op_isnotequal");
                        currentWorkingBlock.GetComponent<TexDrawUtils>().expValue.Add("op_isnotequal");
                        break;
                    case "(":
                        operators.Insert(expIndex, UtilityREST.var_openingBrackets);
                        currentWorkingBlock.GetComponent<TexDrawUtils>().expValue.Add("op_isequal");
                        break;
                    case ")":
                        operators.Insert(expIndex, UtilityREST.var_closingBrackets);
                        currentWorkingBlock.GetComponent<TexDrawUtils>().expValue.Add("op_isequal");
                        break;
                    default:
                        operators.Add("op_unknown");
                        currentWorkingBlock.GetComponent<TexDrawUtils>().expValue.Add("op_unknown");
                        break;
                }
                expIndex = operators.Count - 1;
            }


        }
        else
        {
            int index = currentWorkingBlock.GetComponent<TexDrawUtils>().expIndex[currentWorkingBlock.GetComponent<TexDrawUtils>().expIndex.Count - 1];
            if (String.Compare(currentWorkingBlock.GetComponent<TexDrawUtils>().texType, UtilityREST.F) == 0 && String.Compare(currentWorkingBlock.GetComponent<TexDrawUtils>().texPosition, UtilityREST.Denominator) == 0)
                expIndex = index;
            else
                expIndex = index + 1;

            int operatorIndex = 0;
            if (String.Equals(currentWorkingBlock.GetComponent<TexDrawUtils>().texType, UtilityREST.F))
            {
                operatorIndex = currentWorkingBlock.GetComponent<TexDrawUtils>().expValue.Count - 1;
            }
            //Debug.Log(" expIndex.Count > 1 expIndex: " + expIndex + index);
            switch (exp)
            {
                case "+":
                    operators.Insert(expIndex, "op_addition");
                    currentWorkingBlock.GetComponent<TexDrawUtils>().expValue.Insert(operatorIndex, "op_addition");
                    break;
                case "-":
                    operators.Insert(expIndex, "op_subtraction");
                    currentWorkingBlock.GetComponent<TexDrawUtils>().expValue.Insert(operatorIndex, "op_subtraction");
                    break;
                case "\\times":
                    operators.Insert(expIndex, "op_multiply");
                    currentWorkingBlock.GetComponent<TexDrawUtils>().expValue.Insert(operatorIndex, "op_multiply");
                    break;
                case "\\div":
                    operators.Insert(expIndex, "op_divide");
                    currentWorkingBlock.GetComponent<TexDrawUtils>().expValue.Insert(operatorIndex, "op_divide");
                    break;
                case "=":
                    operators.Insert(expIndex, "op_isequal");
                    currentWorkingBlock.GetComponent<TexDrawUtils>().expValue.Insert(operatorIndex, "op_isequal");
                    break;
                case "\\not[0-0]{=}":
                    operators.Insert(expIndex, "op_isnotequal");
                    currentWorkingBlock.GetComponent<TexDrawUtils>().expValue.Insert(operatorIndex, "op_isnotequal");
                    break;
                case "(":
                    operators.Insert(expIndex, UtilityREST.var_openingBrackets);
                    currentWorkingBlock.GetComponent<TexDrawUtils>().expValue.Insert(operatorIndex, "op_isequal");
                    break;
                case ")":
                    operators.Insert(expIndex, UtilityREST.var_closingBrackets);
                    currentWorkingBlock.GetComponent<TexDrawUtils>().expValue.Insert(operatorIndex, "op_isequal");
                    break;
                default:
                    operators.Insert(expIndex, "op_unknown");
                    currentWorkingBlock.GetComponent<TexDrawUtils>().expValue.Insert(operatorIndex, "op_unknown");
                    break;
            }
            changeOperatorvaluebelow(currentWorkingBlock.GetComponent<TexDrawUtils>().serialNumber);
        }

        currentWorkingBlock.GetComponent<TexDrawUtils>().numberOfExp++;
        currentWorkingBlock.GetComponent<TexDrawUtils>().expIndex.Add(expIndex);
        currentWorkingBlock.GetComponent<TexDrawUtils>().noOfDigitsLastValue.Add(currentWorkingBlock.GetComponent<TexDrawUtils>().lastValueNoOfDigits);
        currentWorkingBlock.GetComponent<TexDrawUtils>().lastValueNoOfDigits = 0;
        currentWorkingBlock.GetComponent<TexDrawUtils>().lastValueNoOfDigits++;
        if (openingBracketAdded)
        {
            // opening bracket added 
            int index = currentWorkingBlock.GetComponent<TexDrawUtils>().expIndex[currentWorkingBlock.GetComponent<TexDrawUtils>().expIndex.Count - 1];
            if (String.Compare(currentWorkingBlock.GetComponent<TexDrawUtils>().texType, UtilityREST.F) == 0 && String.Compare(currentWorkingBlock.GetComponent<TexDrawUtils>().texPosition, UtilityREST.Denominator) == 0)
                expIndex = index;
            else
                expIndex = index + 1;
            int operatorIndex = 0;
            if (String.Equals(currentWorkingBlock.GetComponent<TexDrawUtils>().texType, UtilityREST.F))
            {
                operatorIndex = currentWorkingBlock.GetComponent<TexDrawUtils>().expValue.Count - 1;
            }
            operators.Insert(expIndex, UtilityREST.var_closingBrackets);
            currentWorkingBlock.GetComponent<TexDrawUtils>().expValue.Insert(operatorIndex, UtilityREST.var_closingBrackets);
            OpeningBracketcounter--;
            currentWorkingBlock.GetComponent<TexDrawUtils>().openingBracketAdded = true;
            openingBracketAdded = false;
        }
    }

    void GotoNextLine()
    {
        if (isActive)
        {
            StopblinkingCursor();
            switch (Functionmode)
            {
                case modeLCM:
                    Functionmode = defaultmode;
                    if (prevNoofColumns > currentNoofColumns)
                    {
                        int nuberofturns = (prevNoofColumns - currentNoofColumns);
                        for (int i = 0; i < nuberofturns; i++)
                        {
                            //Debug.Log("i = " + i);
                            getNewCurrentWorkingTex();
                            //Debug.Log("i = " + i);
                        }
                    }
                    NextLine();
                    texValue = defaultvalue;
                    getNewCurrentWorkingTex();
                    currentWorkingBlock.GetComponent<TexDrawUtils>().multiDigitAllowed = true;
                    currentWorkingBlock.GetComponent<TexDrawUtils>().lcmFactor = true;
                    getNewCurrentWorkingTex();
                    Functionmode = modeLCM;
                    texValue = downLine;
                    currentWorkingTexDraw.text = currentWorkingTexDraw.text.Insert(0, "\\vborder[1100 black]");
                    break;
                case modeAddition:
                    string exp = "+";
                    values[valueIndex] = value;
                    NextLine();
                    getNewCurrentWorkingTex();
                    currentWorkingTexDraw.text = currentWorkingTexDraw.text.Remove(currentWorkingTexDraw.text.Length - 1);
                    currentWorkingTexDraw.text = currentWorkingTexDraw.text.Insert(currentWorkingTexDraw.text.Length, exp);
                    addExpToOpt(exp);
                    value = 0;
                    nextValue = true;
                    //Debug.Log("GotoNextLine");
                    placeValue = 0;
                    getNewCurrentWorkingTex();
                    texValue = defaultvalue;

                    display += 2;
                    step_attribute.NUM++;
                    step_attribute.NUMOP++;

                    step_attribute.TOTATT++;
                    Debug.Log("Opp TOTATT : " + step_attribute.TOTATT);
                    step_attribute.TOTOP++;
                    //Debug.Log("Value: " + value);

                    // will work later


                    break;
                case modeHCF:
                    Functionmode = defaultmode;
                    if (prevNoofColumns > currentNoofColumns)
                    {
                        int nuberofturns = (prevNoofColumns - currentNoofColumns);
                        for (int i = 0; i < nuberofturns; i++)
                        {
                            //Debug.Log("i = " + i);
                            getNewCurrentWorkingTex();
                            //Debug.Log("i = " + i);
                        }
                    }
                    NextLine();
                    texValue = defaultvalue;
                    getNewCurrentWorkingTex();
                    currentWorkingBlock.GetComponent<TexDrawUtils>().multiDigitAllowed = true;
                    currentWorkingBlock.GetComponent<TexDrawUtils>().lcmFactor = true;
                    getNewCurrentWorkingTex();
                    Functionmode = modeHCF;
                    texValue = downLine;
                    currentWorkingTexDraw.text = currentWorkingTexDraw.text.Insert(0, "\\vborder[1100 black]");
                    break;
                case modeDivision:

                    NextLine();
                    int steps = (prevNoofColumns - currentNoofColumns);
                    for (int i = 0; i < steps; i++)
                    {
                        if (noOfRowsinStep % 2 == 0)
                            texValue = downLine;
                        else
                            texValue = defaultvalue;
                        getNewCurrentWorkingTex();
                    }
                    GameObject temp = currentRow.transform.GetChild(indexofWorkingArea).gameObject;
                    currentWorkingBlock = temp.transform.GetChild(currentWorkingBlock.transform.GetSiblingIndex() + currentNoofColumns - 2).gameObject;
                    currentWorkingTexDraw = currentWorkingBlock.GetComponent<TEXDraw>();
                    break;
                default:
                    NextLine();
                    getNewCurrentWorkingTex();
                    break;

            }
            if (currentrowPrevLineSpace < currentMaxSpaceInRow)
                currentrowPrevLineSpace = currentMaxSpaceInRow;
            currentMaxSpaceInRow = 0;
            StartBlinkingCursor();
        }

    }

    void NextLine()
    {
        if (isActive)
        {
            if (OnprintNextLineAction != null)
                OnprintNextLineAction();
            currentWorkingBlock.GetComponent<TexDrawUtils>().notClickable = true;
            GameObject temp = currentRow.transform.GetChild(indexofWorkingArea).gameObject;
            if (prevNoofColumns > currentNoofColumns)
            {
                for (int i = 0; i < (prevNoofColumns - currentNoofColumns); i++)
                {
                    //GameObject temp = currentRow.transform.GetChild(indexofWorkingArea).gameObject;
                    currentWorkingBlock = (GameObject)Instantiate(GeneralDigit, transform);
                    currentWorkingBlock.transform.parent = temp.transform;
                    currentWorkingTexDraw = currentWorkingBlock.GetComponent<TEXDraw>();
                    currentWorkingTexDraw.text = currentWorkingTexDraw.text.Insert(0, " ");
                    //Debug.Log("transform.childCount: " + transform.childCount);
                    //nextTex = (GameObject)Instantiate(prefabTex, transform);
                    //Debug.Log("transform.childCount: " + transform.childCount);
                    currentWorkingBlock.transform.SetSiblingIndex(temp.transform.childCount - 1);
                    intTestNo++;
                    currentWorkingBlock.name = "newTex" + rowNumber.ToString() + intTestNo.ToString();
                }
            }
            if (prevNoofColumns < currentNoofColumns)
            {
                //Debug.Log("prevNoofColumns: " + prevNoofColumns + " currentNoofColumns: " + currentNoofColumns);
                VariableGridLayoutGroup mVariableGridLayout = temp.GetComponent<VariableGridLayoutGroup>();
                prevNoofColumns = currentNoofColumns;
                mVariableGridLayout.constraintCount = currentNoofColumns;
            }

            noOfRowsinStep++;
            currentNoofColumns = 0;

        }

        // noofvaluesafterselect = 0;
        //selectedBypointer = false;
    }

    bool compareAttribute(Attributes_step system, Attributes_step student)
    {
        if (system.TOTATT == student.TOTATT && system.TOTOP == student.TOTOP && system.OP == student.OP && system.FOP == student.FOP && system.F == student.F && system.MFOP == student.MFOP && system.MF == student.MF && system.NUMOP == student.NUMOP && system.NUM == student.NUM)
            return true;
        else
            return false;
    }

    int getReleventStepNo(int currentStudStepNo, JSONArray steps, JSONArray attsteps)
    {
        int relevantStepNo = currentStudStepNo;
        List<int> listOfReleventStep = new List<int>();
        //Debug.Log("releventStepNo ++: " + relevantStepNo + " currentStudStepNo: "+ currentStudStepNo + " display: " + display);
        while (relevantStepNo < steps.Count)
        {
            //Debug.Log("relevantStepNo: " + relevantStepNo + " steps.Count: "+ steps.Count);
            JSONNode stepsData = steps[relevantStepNo];
            //Debug.Log("stepsData: " + stepsData.ToString());
            JSONArray displayJson = (JSONArray)stepsData["Display"];
            // Debug.Log(displayJson.Count + " display: "+ display);

            if (display == displayJson.Count)
            {
                listOfReleventStep.Add(relevantStepNo++);
                //return relevantStepNo;
            }
            else
            {
                relevantStepNo++;
                /*
                if (relevantStepNo >= steps.Count)
                    relevantStepNo = currentStudStepNo;
               // break;*/
            }

        }
        //Debug.Log("count: " + listOfReleventStep.Count);
        if (listOfReleventStep.Count > 0)
        {
            if (listOfReleventStep.Count > 1)
            {
                bool attribute_matched = false;
                foreach (int i in listOfReleventStep)
                {
                    // compare attribute jason of system and student and send and send the mathing row eles send the first row
                    Attributes_step system_attribute = new Attributes_step(attsteps[i]["Attribute"]["NUM"].AsInt, attsteps[i]["Attribute"]["NUMOP"].AsInt, attsteps[i]["Attribute"]["MF"].AsInt, attsteps[i]["Attribute"]["MFOP"].AsInt, attsteps[i]["Attribute"]["F"].AsInt, attsteps[i]["Attribute"]["FOP"].AsInt, attsteps[i]["Attribute"]["OP"].AsInt, attsteps[i]["Attribute"]["TOTOP"].AsInt, attsteps[i]["Attribute"]["TOTATT"].AsInt);
                    //if(compareAttribute(system_attribute, step_attribute))
                    {
                        relevantStepNo = i;
                        attribute_matched = true;
                        break;
                    }
                    //Debug.Log("system attribute: " + attsteps[i]["Attribute"].ToString());
                    //Debug.Log("i: " + i);
                    //Debug.Log("student attribute: JSON " + step_attribute.NUM + " mf" + step_attribute.MF + " op "+step_attribute.NUMOP+"tot"+step_attribute.TOTATT);
                }
                if (!attribute_matched)
                    relevantStepNo = listOfReleventStep[0];
            }
            else
            {
                //Debug.Log("list count = 0");
                relevantStepNo = listOfReleventStep[0];
            }
        }
        else
        {
            //  Debug.Log("list count < 0");
            relevantStepNo = currentStudStepNo;
        }
        //Debug.Log("releventStepNo:++++ " + relevantStepNo+ " display: "+ display);
        return relevantStepNo;
    }

    int getReleventStepNoJson(int currentStudStepNo, JSONArray steps, JSONArray attsteps)
    {
        int relevantStepNo = currentStudStepNo;
        List<int> listOfReleventStep = new List<int>();
        while (relevantStepNo < steps.Count)
        {
            JSONNode stepsData = steps[relevantStepNo];
            JSONArray displayJson = (JSONArray)stepsData["Display"];
            //to store the relevent steps nos who matches with current step.
            // Debug.Log("stud json: " + stuJasonSoln.Attribute.NUM.ToString() + " soln json: " + attsteps[relevantStepNo]["Attribute"].ToString()+ " relevantStepNo: "+ relevantStepNo + displayJson.Count+" "+ stuJasonSoln.Display.Count);
            //Debug.Log("getReleventStepNoJson: stuJasonSoln" + stuJasonSoln.Display.Count + " Attribute.NUM: "+ stuJasonSoln.Attribute.TOTATT + " stuJasonSoln Attribute: " + stuJasonSoln.Attribute.TOTATT + " " + stuJasonSoln.Attribute.TEXT + " displayJson: " + attsteps[relevantStepNo]["Attribute"]["TOTATT"].AsInt + " displayJson: " + attsteps[relevantStepNo]["Attribute"]["TEXT"].AsInt + "displayJson.Count "+ displayJson.Count);
            //if ((stuJasonSoln.Display.Count- stuJasonSoln.Attribute.TEXT) == (displayJson.Count- attsteps[relevantStepNo]["Attribute"]["TEXT"].AsInt))
            //Debug.Log("stuJasonSoln: " + stuJasonSoln.Attribute.TOTATT + " stuJasonSoln.Attribute.TEXT: " + stuJasonSoln.Attribute.TEXT + "  displayJson.Count: " + displayJson.Count + "  :" + attsteps[relevantStepNo]["Attribute"]["TEXT"].AsInt+ "stuJasonSoln.Display.Count: "+ stuJasonSoln.Display.Count);
            //if (stuJasonSoln.Attribute.TOTATT - stuJasonSoln.Attribute.TEXT == displayJson.Count - attsteps[relevantStepNo]["Attribute"]["TEXT"].AsInt)
            //Debug.Log("Attribute: " + stuJasonSoln.Attribute.NUM + ":" + stuJasonSoln.Attribute.TOTOP + ":" + stuJasonSoln.Attribute.F + ":" + stuJasonSoln.Attribute.FOP);
            int NUM = attsteps[relevantStepNo]["Attribute"]["NUM"].AsInt;
            if (attsteps[relevantStepNo]["Attribute"]["TEXT"].AsInt > 0 && attsteps[relevantStepNo]["Attribute"]["F"].AsInt > 0)
            {
                NUM += attsteps[relevantStepNo]["Attribute"]["F"].AsInt * 2;
            }
            if (NUM == stuJasonSoln.Attribute.NUM && attsteps[relevantStepNo]["Attribute"]["TOTOP"].AsInt == stuJasonSoln.Attribute.TOTOP)
            {
               // Debug.Log("in here relevantStepNo: " + relevantStepNo);

                listOfReleventStep.Add(relevantStepNo++);
                //return relevantStepNo;
            }
            else
            {
                relevantStepNo++; //will implement Attribute json later.
                /*
                if (relevantStepNo >= steps.Count)
                    relevantStepNo = currentStudStepNo;
               break;*/
            }
            //Debug.Log("listOfReleventStep.Count " + listOfReleventStep.Count + " listOfReleventStep[0] " + listOfReleventStep[0]);

        }
        if (listOfReleventStep.Count > 1)
        {
            //Debug.Log("in here");
            // chch with whomethe attributes matches
            bool foundValue = false;
            for (int l = 0; l < listOfReleventStep.Count; l++)
            {
                // scope of improvement need to make checking more robust with attribute class.
                int ste = listOfReleventStep[l];
                // Debug.Log("attsteps+ " + attsteps[l]["Attribute"]["NUM"].AsInt + " stuJasonSoln: " + stuJasonSoln.Attribute.NUM + "  MF: " + attsteps[l]["Attribute"]["MF"].AsInt + "  stud: " + stuJasonSoln.Attribute.MF+" l: "+l);
                if (attsteps[ste]["Attribute"]["NUM"].AsInt == stuJasonSoln.Attribute.NUM && attsteps[ste]["Attribute"]["MF"].AsInt == stuJasonSoln.Attribute.MF)
                {
                    relevantStepNo = ste;
                    //Debug.Log("l: " + ste);
                    foundValue = true;
                    break;
                }
            }
            if (!foundValue)
            {
                relevantStepNo = currentStudStepNo;
            }
        }
        else
        {
            // relevent step is the first one;
            if (listOfReleventStep.Count > 0)
                relevantStepNo = listOfReleventStep[0];
            else
                relevantStepNo = currentStudStepNo;
        }
        //Debug.Log("relevent: " + relevantStepNo);
        return relevantStepNo;
    }

    bool errorinStep()
    {
        //if (!UtilityREST.isprerequisit)
        {
            if (values.Count == 0 && operators.Count == 0)
            {
                errorMsg = "Steps is empty";
                return true;
            }
            string solJson = UtilityREST.solJson;//PlayerPrefs.GetString("solJson");
                                                 //string tlcd_by_which_solution = PlayerPrefs.GetString("tlcd_by_which_solution");
            string tlcd_diff_level_json = UtilityREST.tlcd_diff_level_json;//PlayerPrefs.GetString("tlcd_diff_level_json");
            string question_data = UtilityREST.studJson;
            //Debug.Log("tlcd_diff_level_json: " + tlcd_diff_level_json);
            int solution_No = UtilityREST.solution_No;//int.Parse(tlcd_by_which_solution);
            //Debug.Log("display:" + display);
            //Debug.Log("tlcd_diff_level_json:" + tlcd_diff_level_json);
            //Debug.Log(" solution_No: " + solution_No);son
            JSONNode responce = JSON.Parse(solJson);
            //JSONNode error = JSON.Parse(tlcd_diff_level_json);
            //Debug.Log(" responce: " + responce.ToString());
            JSONArray resArray = (JSONArray)responce;
            // Debug.Log("error: " + error.ToString());
            JSONNode soln = resArray[solution_No];
            //Debug.Log("soln: " + soln.ToString());
            JSONArray steps = (JSONArray)soln["Steps"];
            //Debug.Log("steps: "+steps.ToString()  );
            //Debug.Log("stepsnumber: " + stepNo);
            JSONNode ques_data = JSON.Parse(question_data);
            JSONNode attbSoln = ques_data["Attribute_Json"]["Solutions"];
            //Debug.Log("attbSoln: " + attbSoln.ToString());
            JSONArray attbSolnArray = (JSONArray)attbSoln;
            JSONNode attsoln = attbSolnArray[solution_No];
            //Debug.Log("attsoln: " + attsoln.ToString());
            JSONArray attsteps = (JSONArray)attsoln["Steps"];
            int releventStepNo = getReleventStepNo(stepNo - 1, steps, attsteps);
            // Debug.Log("releventStepNo: " + releventStepNo);
            //Debug.Log("student attribute: JSON " + step_attribute.NUM + " mf " + step_attribute.MF+ " f " + step_attribute.F + " op " + step_attribute.NUMOP + " totatt " + step_attribute.TOTATT + " totopp " + step_attribute.TOTOP);
            //Debug.Log("system attribute: " + attsteps[releventStepNo]["Attribute"].ToString());
            valCount = 0;
            optcount = 0;
            //Debug.Log("releventStepNo: " + releventStepNo );
            if (releventStepNo == (steps.Count - 1))
            {
                islastStep = true;
            }
            else
            {
                islastStep = false;
            }
            //JSONNode Steps_Prompts = error["Steps_Prompts"].Value;
            //Debug.Log("Steps_Prompts: " + Steps_Prompts.ToString());
            //JSONArray error_Array = (JSONArray)Steps_Prompts;
            //JSONNode relevant_Solution_Error = error_Array[solution_No];
            JSONNode diff_level_json = JSON.Parse(tlcd_diff_level_json);
            JSONNode Steps_Prompts = diff_level_json["Steps_Prompts"];
            JSONArray Steps_Prompts_Array = (JSONArray)Steps_Prompts;
            //Debug.Log("Steps_Prompts: " + Steps_Prompts.ToString());
            //Debug.Log(" Steps_PreRequiste:" + Steps_PreRequiste.ToString());
            JSONNode error_in_soln = Steps_Prompts_Array[UtilityREST.solution_No];

            //Debug.Log("pre: " + error_in_soln.ToString());
            //Debug.Log("error No" + errorNo);
            string errorString = "";
            if (errorNo < 1)
            {
                errorString = "First_Time";
            }
            else
            {
                errorString = "Second_Time";
            }
            //JSONNode diff_level_json = JSON.Parse(tlcd_diff_level_json);
            JSONNode Steps_PreRequiste = diff_level_json["Steps_PreRequiste"];
            JSONArray step_Req = (JSONArray)Steps_PreRequiste;
            //Debug.Log("step_Req: " + step_Req.ToString());
            //Debug.Log(" Steps_PreRequiste:" + Steps_PreRequiste.ToString());
            JSONNode pre = step_Req[UtilityREST.solution_No];
            String prerequiste = pre[stepNo - 1].Value;
            //Debug.Log("prerequiste: " + prerequiste + " stepNo:" + stepNo);
            UtilityREST.prerequiste = prerequiste;
            //foreach (JSONNode dtn in steps)
            //{
            //    Debug.Log("step dtn:" + dtn.ToString());
            //}
            JSONNode stepsData = steps[releventStepNo];

            //Debug.Log("stepsData: " + stepsData.ToString());
            JSONArray displayJson = (JSONArray)stepsData["Display"];

            // check for each step starts here
            // check display count
            if (display != displayJson.Count)
            {

                JSONNode Steps_Error_Prompt = error_in_soln[errorString];
                JSONArray Steps_Error_Prompt_Array = (JSONArray)Steps_Error_Prompt;
                JSONNode releventError = Steps_Error_Prompt_Array[stepNo - 1];
                errorMsg = releventError["error_msg"].Value;
                //Debug.Log("display error: "+ display+ " Count: "+ displayJson.Count);
                return true;


            }
            else
            {
                //foreach (string opt in operators)
                //{
                //    Debug.Log("opt: " + opt);
                //}
                //foreach (int vale in values)
                //{
                //    Debug.Log("vale: " + vale);
                //}
                int stepcounter = 0;
                List<int> valuesUsed = new List<int>();
                int startingvalue = 0;
                int valueSwapCounter = 0;
                foreach (JSONNode solndata in stepsData["Display"])
                {
                    Debug.Log("Next Iteration");
                    stepcounter++;
                    if (String.Equals(solndata["Type"].Value, "operator"))
                    {
                        if (!String.Equals(solndata["Name"].Value, "op_num_denom"))
                        {
                            if (!String.Equals(solndata["Name"].Value, operators[optcount++]))
                            {
                                //Debug.Log("String.Equals(solndata[].Value: " + solndata["Name"].Value + " operators[optcount++] :" + operators[optcount - 1]);

                                JSONNode Steps_Error_Prompt = error_in_soln[errorString];
                                JSONArray Steps_Error_Prompt_Array = (JSONArray)Steps_Error_Prompt;
                                JSONNode releventError = Steps_Error_Prompt_Array[stepNo - 1];
                                errorMsg = releventError["error_msg"].Value;
                                Debug.Log("opertor error");
                                return true;

                            }
                        }
                    }
                    else if (String.Equals(solndata["Type"].Value, "variable"))
                    {
                        //Debug.Log("variable: ");
                        string actual_value = solndata["Actual_value"].Value;
                        //Debug.Log("actual_value: " + actual_value);
                        if (actual_value.Contains(","))
                        {
                            //Debug.Log("variable conatins : ,");
                            char sep = ',';
                            string[] value_array = actual_value.Split(sep);
                            //Debug.Log("value_array: " + value_array.Length);
                            if (value_array.Length > values.Count)
                            {
                                JSONNode Steps_Error_Prompt = error_in_soln[errorString];
                                JSONArray Steps_Error_Prompt_Array = (JSONArray)Steps_Error_Prompt;
                                JSONNode releventError = Steps_Error_Prompt_Array[stepNo - 1];
                                errorMsg = releventError["error_msg"].Value;
                                Debug.Log("value error");
                                return true;
                            }
                            foreach (string str in value_array)
                            {
                                int value_json = int.Parse(str);
                                int value_list = values[valCount++];
                                //Debug.Log("value_json: " + value_json + "value_list: " + value_list);
                                if (value_json != value_list)
                                {

                                    JSONNode Steps_Error_Prompt = error_in_soln[errorString];
                                    JSONArray Steps_Error_Prompt_Array = (JSONArray)Steps_Error_Prompt;
                                    JSONNode releventError = Steps_Error_Prompt_Array[stepNo - 1];
                                    errorMsg = releventError["error_msg"].Value;
                                    Debug.Log("value error");
                                    return true;

                                }
                            }
                        }
                        else
                        {
                            int value_json = int.Parse(actual_value);
                            int value_list;
                            if (valueSwapped)
                            {
                                value_list = values[startingvalue];
                                valCount++;
                                valueSwapCounter++;
                                if (valueSwapCounter > 1)
                                    valueSwapped = false;
                            }
                            else
                            {
                                value_list = values[valCount++];
                                startingvalue = valCount - 1;
                            }

                            Debug.Log("value_json: " + value_json + "value_list: " + value_list);
                            if (value_json != value_list)
                            {
                                //Debug.Log("valCount: " + valCount);
                                if (valCount >= values.Count)
                                {
                                    JSONNode Steps_Error_Prompt = error_in_soln[errorString];
                                    JSONArray Steps_Error_Prompt_Array = (JSONArray)Steps_Error_Prompt;
                                    JSONNode releventError = Steps_Error_Prompt_Array[stepNo - 1];
                                    errorMsg = releventError["error_msg"].Value;
                                    Debug.Log("value error");
                                    return true;
                                }
                                else
                                {
                                    Debug.Log("value_json: " + value_json + " values[usedvalue]: " + values[startingvalue + 1] + "startingvalue: " + startingvalue);
                                    if (value_json != values[startingvalue + 1])
                                    {
                                        // for one addition its ok need to modifie if multiple addition sigh is there.
                                        string sighn;
                                        int counter = 0;

                                        if (String.Equals(displayJson[stepcounter]["Type"].Value, "operator"))
                                        {
                                            Debug.Log(".Value: " + displayJson[stepcounter]["Name"].Value);
                                            if (String.Equals(displayJson[stepcounter]["Name"].Value, "op_addition") || String.Equals(displayJson[stepcounter]["Name"].Value, "op_multiplication"))
                                            {
                                                sighn = displayJson[stepcounter]["Name"].Value;
                                                for (int i = stepcounter; i < displayJson.Count; i++)
                                                {
                                                    if (String.Equals(displayJson[i]["Type"].Value, "operator"))
                                                    {
                                                        if (String.Equals(displayJson[i]["Name"].Value, sighn))
                                                        {
                                                            counter++;
                                                        }
                                                        else
                                                        {
                                                            break;
                                                        }
                                                    }
                                                }
                                                if (counter > 0)
                                                {
                                                    bool valueMatched = false;
                                                    for (int j = 1; j <= counter; j++)
                                                    {

                                                        int usedvalue = startingvalue + 1 + j;
                                                        Debug.Log("j: " + j + " usedvalue: " + usedvalue);
                                                        Debug.Log("value_json: " + value_json + " values[usedvalue]: " + values[usedvalue]);

                                                        if (value_json == values[usedvalue])
                                                        {
                                                            valueMatched = true;
                                                            if (valuesUsed.Contains(usedvalue))
                                                            {
                                                                Debug.Log("Value already present");
                                                                continue;
                                                            }
                                                            else
                                                            {

                                                                valuesUsed.Add(usedvalue);
                                                                Debug.Log("adding value" + valuesUsed.Count);
                                                                if (!valueSwapped)
                                                                {
                                                                    valueSwapped = true;
                                                                    //startingvalue = startingvalue;
                                                                }
                                                                break;

                                                            }

                                                        }
                                                    }
                                                    if (!valueMatched)
                                                    {
                                                        JSONNode Steps_Error_Prompt = error_in_soln[errorString];
                                                        JSONArray Steps_Error_Prompt_Array = (JSONArray)Steps_Error_Prompt;
                                                        JSONNode releventError = Steps_Error_Prompt_Array[stepNo - 1];
                                                        errorMsg = releventError["error_msg"].Value;
                                                        Debug.Log("value error");
                                                        return true;
                                                    }
                                                }
                                                else
                                                {

                                                    JSONNode Steps_Error_Prompt = error_in_soln[errorString];
                                                    JSONArray Steps_Error_Prompt_Array = (JSONArray)Steps_Error_Prompt;
                                                    JSONNode releventError = Steps_Error_Prompt_Array[stepNo - 1];
                                                    errorMsg = releventError["error_msg"].Value;
                                                    Debug.Log("value error");
                                                    return true;
                                                }
                                            }
                                            else
                                            {

                                                JSONNode Steps_Error_Prompt = error_in_soln[errorString];
                                                JSONArray Steps_Error_Prompt_Array = (JSONArray)Steps_Error_Prompt;
                                                JSONNode releventError = Steps_Error_Prompt_Array[stepNo - 1];
                                                errorMsg = releventError["error_msg"].Value;
                                                Debug.Log("value error");
                                                return true;
                                            }
                                        }
                                        else
                                        {

                                            JSONNode Steps_Error_Prompt = error_in_soln[errorString];
                                            JSONArray Steps_Error_Prompt_Array = (JSONArray)Steps_Error_Prompt;
                                            JSONNode releventError = Steps_Error_Prompt_Array[stepNo - 1];
                                            errorMsg = releventError["error_msg"].Value;
                                            Debug.Log("value error");
                                            return true;
                                        }

                                    }
                                    else
                                    {
                                        if (String.Equals(displayJson[stepcounter]["Type"].Value, "operator"))
                                        {
                                            if (String.Equals(displayJson[stepcounter]["Name"].Value, "op_addition") || String.Equals(displayJson[stepcounter]["Name"].Value, "op_multiplication"))
                                            {
                                                valueSwapped = true;
                                                valuesUsed.Add(startingvalue + 1);
                                            }
                                        }
                                    }
                                }

                            }
                            else
                            {
                                valueSwapped = false;
                            }
                        }

                        /*
                        foreach (JSONNode Actual_value in solndata["Actual_value"])
                        {
                            int value_json = int.Parse(Actual_value);
                            int value_list = values[valCount++];
                            Debug.Log("value_json: " + value_json + "value_list: " + value_list);
                            if (value_json != value_list)
                            {
                                errorMsg = "Value error";
                                return true;
                            }

                        }*/
                    }
                }
            }

            return false;

        }
        /*
                else
                {
                    Debug.Log("in pre requisite");
                    string solJson = UtilityREST.solJson;//PlayerPrefs.GetString("solJson");
                                                         //string tlcd_by_which_solution = PlayerPrefs.GetString("tlcd_by_which_solution");
                    string tlcd_diff_level_json = UtilityREST.tlcd_diff_level_json;//PlayerPrefs.GetString("tlcd_diff_level_json");
                    int solution_No = UtilityREST.solution_No;//int.Parse(tlcd_by_which_solution);
                    Debug.Log("display:" + display);
                    Debug.Log("solJson:" + solJson);
                    Debug.Log("tlcd_diff_level_json:" + tlcd_diff_level_json);
                    Debug.Log("solution_No:" + solution_No);

                    JSONNode responce = JSON.Parse(solJson);
                    JSONArray resArray = (JSONArray)responce;
                    Debug.Log(resArray.Count);

                    JSONNode soln = resArray[solution_No];
                    Debug.Log("soln: " + soln.ToString());
                    JSONArray steps = (JSONArray)soln["Steps"];
                    Debug.Log(steps.Count);

                    Debug.Log("stepsnumber: " + stepNo);
                    /*
                    int releventStepNo = getReleventStepNo(stepNo - 1, steps);
                    Debug.Log("releventStepNo: " + releventStepNo + " steps.Count: " + steps.Count);
                    if (releventStepNo == (steps.Count - 1))
                    {
                        islastStep = true;

                    }*//*

                    foreach (string opt in operators)
                    {
                        Debug.Log("opt: " + opt);
                    }
                    foreach (int vale in values)
                    {
                        Debug.Log("vale: " + vale);
            }
            return false;
        }*/
    }

    bool presentinAnswerList(ImproperFraction frac)
    {
        foreach(ImproperFraction f in UtilityArtifacts.ansfractionList)
        {
            //Debug.Log("f.num: " + f.num[0] + " frac.num: " + frac.num[0] + " f.dem: " + f.dem[0] + " frac.dem: " + frac.dem[0]);
            if (f.num[0] == frac.num[0] && f.dem[0] == frac.dem[0])
                return true;
        }
        return false;
    }

    bool fractionsAreMultiple(ImproperFraction frac1, ImproperFraction frac2)
    {
        if ((frac2.num[0] % frac1.num[0] == 0) && (frac2.dem[0] % frac1.dem[0] == 0))
            return true;
        else
            return false;

        //    if(frac2.num[0]% frac1.num[0] != 0)
        //    {
        //        return false;
        //    }
        //    int div = frac2.num[0] / frac1.num[0];
        //    Debug.Log("fractionsAreMultiple div: " + div);
        //    if(div == 0)
        //    {
        //        return false;
        //    }

        //    if (frac1.dem[0] * div == frac2.dem[0])
        //    {
        //        Debug.Log("fractionsAreMultiple is a factor " + frac1.dem[0] + " div: " + div + " frac2.dem[0]: " + frac2.dem[0]);
        //        return true;
        //    }
        //    else
        //        return false;
    }

    bool CheckIfMultiplicationOk(ImproperFraction frac2, ImproperFraction frac3)
    {
        if((frac2.num[0]* frac2.num[0] == frac3.num[0]) && (frac2.dem[0] * frac2.dem[0] == frac3.dem[0]))
        {
            //Debug.Log()
            return true;
        }
        return false;
    }

    bool MultiplyerValue(ImproperFraction frac1, ImproperFraction frac2, ImproperFraction frac3)
    {
        int div = frac3.num[0] / frac1.num[0];
        Debug.Log("div: " + div + " frac3.num[0]: " + frac3.num[0] + " frac1.num[0]: " + frac1.num[0]);
        if(frac2.num.Count == 2|| frac2.dem.Count == 2)
        {
            if(frac2.num.Contains(div) && frac2.dem.Contains(div) && frac2.num.Contains(frac1.num[0]) && frac2.dem.Contains(frac1.dem[0]))
            {
                foreach(string oper in frac2.operators)
                {
                    if (!string.Equals(oper, UtilityREST.var_multiplication))
                        return false;
                }
                return true;
            }
        }
      
        return false;
    }

    bool CheckIfSameFactors(ImproperFraction frac1,ImproperFraction frac2)
    {
        Debug.Log("frac2.num[0] / frac1.num[0] : " + frac2.num[0] / frac1.num[0] + "  frac2.dem[0] / frac1.dem[0]:  " + frac2.dem[0] / frac1.dem[0]);

        if (frac2.num[0] / frac1.num[0] == frac2.dem[0] / frac1.dem[0])
            return true;
        else
            return false;
    }


    string errorlog = "",sucessLog="";
    bool errorinstepobj16()
    {
        string tlcd_diff_level_json = UtilityREST.tlcd_diff_level_json;
        JSONNode diff_level_json = JSON.Parse(tlcd_diff_level_json);
        JSONNode Steps_PreRequiste = diff_level_json["Steps_PreRequiste"];
        JSONArray step_Req = (JSONArray)Steps_PreRequiste;
        JSONNode pre = step_Req[UtilityREST.solution_No];
        String prerequiste = pre[1].Value;
        UtilityREST.prerequiste = prerequiste;
        studentJson.studJson.Add(stuJasonSoln);
        string studjson = JsonUtility.ToJson(studentJson);
        //Debug.Log("NUM: " + stuJasonSoln.Attribute.NUM + " OP: " + stuJasonSoln.Attribute.OP + " F: " + stuJasonSoln.Attribute.F);

     

        List<ImproperFraction> fractionList = new List<ImproperFraction>();
        List<string> operatorList = new List<string>();
        bool isnum = false, isdem = false;
        ImproperFraction frac = new ImproperFraction();
        foreach (StepDisplay sd in stuJasonSoln.Display)
        {
            //Debug.Log("Type: " + sd.Type + "  Name: " + sd.Name + "  Actual_value: " + sd.Actual_value);
            if (sd.Type == UtilityREST.type_operator && sd.Name == UtilityREST.var_openingBrackets)
            {
                frac = new ImproperFraction();
                isnum = true;
            }
            else if (sd.Type == UtilityREST.type_operator && sd.Name == UtilityREST.var_denom)
            {
                isnum = false;
                isdem = true;
            }
            else if (sd.Type == UtilityREST.type_variable)
            {
                if (isnum)
                {
                    frac.num.Add(int.Parse(sd.Actual_value));
                }
                else if (isdem)
                {
                    frac.dem.Add(int.Parse(sd.Actual_value));
                }
            }
            else if (sd.Type == UtilityREST.type_operator && sd.Name == UtilityREST.var_closingBrackets)
            {
                fractionList.Add(frac);
                isdem = false;
            }
            else if (sd.Type == UtilityREST.type_operator)
            {
                if (isnum || isdem)
                {
                    frac.operators.Add(sd.Name);
                }
                else
                {
                    operatorList.Add(sd.Name);
                }
            }
        }

        //different contitions:
        foreach (string oper in operatorList)
        {
            //Debug.Log("oper"+ oper);
            if (!string.Equals(oper, UtilityREST.var_equal))
            {
                errorCountNonTravers++;
                //errorCountTraver = 0;
                errorlog = "User has used incorrect operator";
                if (errorCountNonTravers == 1)
                {
                    errorMsg = "Check if you using the correct operation to find the next equivalent fraction. ";
                }
                else if(errorCountNonTravers == 2)
                {
                    errorMsg = "Try Again";
                }
                else
                {
                    errorMsg = "To find the next equivalent fraction, you need to use multiplication operator and multiply the given fraction by another fraction whose numerator and denominator is the same. Try Again";
                }
               
                return true;
            }
        }


        if (stuJasonSoln.Attribute.F == 2 && stuJasonSoln.Attribute.NUM == 4 && stuJasonSoln.Attribute.OP == 0)
        {
            //frac = frac
            //calculation step without middle number
            // check if first fraction is present in the ansfractionlist
            if (presentinAnswerList(fractionList[0]))
            {
                if (!allElementUnique(fractionList))
                {
                    errorCountNonTravers++;
                    errorlog = "Equivalent fraction formation error";
                    //errorCountTraver = 0;
                    if (errorCountNonTravers == 1)
                    {
                        errorMsg = "Check if the fraction you are multiplying with is in the correct form";
                    }
                    else if (errorCountNonTravers == 2)
                    {
                        errorMsg = "Try Again";
                    }
                    else
                    {
                        errorMsg = "To find the next equivalent fraction, you need to multiply the given fraction by another fraction whose numerator and denominator is the same. Try Again";
                    }
                    return true;
                }
                if (!fractionsAreMultiple(fractionList[0], fractionList[1]))
                {

                    {
                        errorCountTraver++;
                        errorCountNonTravers = 0;
                        errorMsg = "Multiplication error";

                        if (fractionList[1].num[0] % fractionList[0].num[0] == 0)
                        {
                            errorlog = "Multiplication error: as "+fractionList[0].dem[0] + " and "+ fractionList[1].dem[0]+" are not multiples";
                        }
                        else
                        {
                            errorlog = "Multiplication error: as " + fractionList[0].num[0] + " and " + fractionList[1].num[0] + " are not multiples";
                        }
                    }
                  
                   
                    return true;
                }else if(!CheckIfSameFactors(fractionList[0], fractionList[1]))
                {
                    errorCountNonTravers++;
                    int numfact = (fractionList[1].num[0] / fractionList[0].num[0]);
                    int demfact = (fractionList[1].dem[0] / fractionList[0].dem[0]);

                    errorlog = "Formation error: The multiples are not same ("+ numfact+", "+demfact+")";
                    if (errorCountNonTravers == 1)
                    {
                        errorMsg = "Check if the fraction you are multiplying with is in the correct form";
                    }
                    else if (errorCountNonTravers == 2)
                    {
                        errorMsg = "Try Again";
                    }
                    else
                    {
                        errorMsg = "To find the next equivalent fraction, you need to multiply the given fraction by another fraction whose numerator and denominator is the same. Try Again";
                    }
                    return true;
                }
            }
            else
            {
                errorCountNonTravers++;
                //errorCountTraver = 0;
                errorlog = "Equivalent fraction formation error";
                if (errorCountNonTravers == 1)
                {
                    errorMsg = "Check if the fraction you are multiplying with is in the correct form";
                }
                else if (errorCountNonTravers == 2)
                {
                    errorMsg = "Try Again";
                }
                else
                {
                    errorMsg = "To find the next equivalent fraction, you need to multiply the given fraction by another fraction whose numerator and denominator is the same. Try Again";
                }
                return true;
            }

            //Debug.Log("frac count: " + fractionList.Count + " oper count " + operatorList.Count);
            sucessLog = "but, not written the complete step";
            UtilityArtifacts.ansfractionList.Add(fractionList[1]);
            islastStep = false;
            return false;
        }
        else if(stuJasonSoln.Attribute.F == 2 && stuJasonSoln.Attribute.NUM == 8 && stuJasonSoln.Attribute.OP == 0)
        {
            //calucation step with middle number
            if (presentinAnswerList(fractionList[0]))
            {
                if (!fractionsAreMultiple(fractionList[0], fractionList[2]))
                {//need to check form here

                    if (!CheckIfMultiplicationOk( fractionList[1], fractionList[2]))
                    {
                        errorCountTraver++;
                        //errorCountNonTravers = 0;
                        errorMsg = "Multiplication error";
                        if (fractionList[2].num[0] % fractionList[1].num[0] == 0)
                        {
                            errorlog = "Multiplication error: as " + fractionList[1].dem[0] + " and " + fractionList[2].dem[0] + " are not multiples";
                        }
                        else
                        {
                            errorlog = "Multiplication error: as " + fractionList[2].num[0] + " and " + fractionList[1].num[0] + " are not multiples";
                        }
                       
                        //errorCountTraver = 0;
                       
                    }
                    else
                    {
                        errorCountNonTravers++;
                        errorlog = "Equivalent fraction formation error";
                        if (errorCountNonTravers == 1)
                        {
                            errorMsg = "Check if the fraction you are multiplying with is in the correct form";
                        }
                        else if (errorCountNonTravers == 2)
                        {
                            errorMsg = "Try Again";
                        }
                        else
                        {
                            errorMsg = "To find the next equivalent fraction, you need to multiply the given fraction by another fraction whose numerator and denominator is the same. Try Again";
                        }
                    }
                        
                    return true;
                }
                else if(!MultiplyerValue(fractionList[0], fractionList[1], fractionList[2])){

                    foreach (string oper in fractionList[1].operators)
                    {
                        if (!string.Equals(oper, UtilityREST.var_multiplication))
                        {
                            errorCountNonTravers++;
                            //errorCountTraver = 0;
                            errorlog = "User has used incorrect operator";
                            if (errorCountNonTravers == 1)
                            {
                                errorMsg = "Check if you using the correct operation to find the next equivalent fraction. ";
                            }
                            else if (errorCountNonTravers == 2)
                            {
                                errorMsg = "Try Again";
                            }
                            else
                            {
                                errorMsg = "To find the next equivalent fraction, you need to use multiplication operator and multiply the given fraction by another fraction whose numerator and denominator is the same. Try Again";
                            }
                            return true;
                        }
                        
                    }

                    // its a formation error
                    errorCountNonTravers++;
                    //errorCountTraver = 0;
                    int numfact = (fractionList[2].num[0] / fractionList[0].num[0]);
                    int demfact = (fractionList[2].dem[0] / fractionList[0].dem[0]);

                    errorlog = "Formation error: The multiples are not same (" + numfact + ", " + demfact + ")";
                   // errorlog = "Equivalent fraction formation error";
                    if (errorCountNonTravers == 1)
                    {
                        errorMsg = "Check if the fraction you are multiplying with is in the correct form";
                    }
                    else if (errorCountNonTravers == 2)
                    {
                        errorMsg = "Try Again";
                    }
                    else
                    {
                        errorMsg = "To find the next equivalent fraction, you need to multiply the given fraction by another fraction whose numerator and denominator is the same. Try Again";
                    }

                    return true;
                }
            }
            else
            {
                errorlog = "Equivalent fraction formation error fist fraction is different";
                errorCountNonTravers++;
                //errorCountTraver = 0;
                if (errorCountNonTravers == 1)
                {
                    errorMsg = "Check if the fraction you are multiplying with is in the correct form";
                }
                else if (errorCountNonTravers == 2)
                {
                    errorMsg = "Try Again";
                }
                else
                {
                    errorMsg = "To find the next equivalent fraction, you need to multiply the given fraction by another fraction whose numerator and denominator is the same. Try Again";
                }
                return true;
            }
            UtilityArtifacts.ansfractionList.Add(fractionList[2]);
            Debug.Log("frac count: " + fractionList.Count + " oper count " + operatorList.Count);
            islastStep = false;
            sucessLog = "";
            return false;
        }
        else if(stuJasonSoln.Attribute.F == 4 && stuJasonSoln.Attribute.NUM == 8 && stuJasonSoln.Attribute.OP == 0)
        {
            // last step check
            if (presentinAnswerList(fractionList[0]))
            {
                if(UtilityArtifacts.ansfractionList.Count == 4)
                {
                    //check if the list matches
                    if (!FractListMatches(fractionList,UtilityArtifacts.ansfractionList))
                    {
                        errorCountNonTravers++;
                        errorCountTraver = 0;
                        errorlog = "Answer is differnt from steps";
                        if (errorCountNonTravers == 1)
                        {
                            errorMsg = "Check if the fraction you are multiplying with is in the correct form";
                        }
                        else if (errorCountNonTravers == 2)
                        {
                            errorMsg = "Try Again";
                        }
                        else
                        {
                            errorMsg = "To find the next equivalent fraction, you need to multiply the given fraction by another fraction whose numerator and denominator is the same. Try Again";
                        }
                        return true;
                    }
                }
                else
                {
                    if (!allElementUnique(fractionList))
                    {
                        errorCountNonTravers++;
                        //errorCountTraver = 0;
                        errorlog = "Equivalent fraction formation error";
                        if (errorCountNonTravers == 1)
                        {
                            errorMsg = "Check if the fraction you are multiplying with is in the correct form";
                        }
                        else if (errorCountNonTravers == 2)
                        {
                            errorMsg = "Try Again";
                        }
                        else
                        {
                            errorMsg = "To find the next equivalent fraction, you need to multiply the given fraction by another fraction whose numerator and denominator is the same. Try Again";
                        }
                        return true;
                    }
                    for(int i = 1; i < fractionList.Count; i++)
                    {
                        if (!fractionsAreMultiple(fractionList[0], fractionList[i]))
                        {
                            errorMsg = "Multiplication error";
                            errorCountTraver++;
                            if (fractionList[i].num[0] % fractionList[0].num[0] == 0)
                            {
                                errorlog = "Multiplication error: as " + fractionList[i].dem[0] + " and " + fractionList[0].dem[0] + " are not multiples";
                            }
                            else
                            {
                                errorlog = "Multiplication error: as " + fractionList[i].num[0] + " and " + fractionList[0].num[0] + " are not multiples";
                            }
                            ///errorCountNonTravers = 0;
                            return true;
                        }
                    }
                    sucessLog = "but, user has jumped step";
                }
            }
                Debug.Log("frac count: " + fractionList.Count + " oper count " + operatorList.Count);
            islastStep = true;
            return false;
        }


        //if no issue
        ////if(stuJasonSoln.Attribute.OP > 0)
        ////    errorMsg = "operator error";
        ////else
        ///
        errorCountNonTravers++;
        //errorCountTraver = 0;
        errorlog = "Equivalent fraction formation error";
        if (errorCountNonTravers == 1)
        {
            errorMsg = "Check if the fraction you are multiplying with is in the correct form";
        }
        else if (errorCountNonTravers == 2)
        {
            errorMsg = "Try Again";
        }
        else
        {
            errorMsg = "To find the next equivalent fraction, you need to multiply the given fraction by another fraction whose numerator and denominator is the same. Try Again";
        }//releventError["error_msg"].Value;
        //Debug.Log("display error: " + stuJasonSoln.Display.Count + " Count: " + displayJson.Count);
        return true;
    }

    bool allElementUnique(List<ImproperFraction> fracList)
    {
        for(int i = 0;i< fracList.Count; i++)
        {
            ImproperFraction frac = fracList[i];
            if(frac.num[0] ==0 || frac.dem[0] == 0)
                return false;
            for (int j = 0; j < fracList.Count; j++)
            {
                if (i != j)
                {
                    ImproperFraction frac1 = fracList[j];
                    if(frac1.num[0]== frac.num[0] && frac1.dem[0] == frac.dem[0])
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }

    bool FractListMatches(List<ImproperFraction> fracList1, List<ImproperFraction> fracList2)
    {
        List<int> checkedIndex = new List<int>();
        foreach(ImproperFraction frac in fracList1)
        {
            bool found = false;

            for(int i = 0; i < fracList2.Count; i++)
            {
                if (checkedIndex.Contains(i))
                    continue;
                ImproperFraction frac2 = fracList2[i];
                if(frac.num[0]==frac2.num[0]&& frac.dem[0] == frac2.dem[0])
                {
                    found = true;
                    checkedIndex.Add(i);
                }
            }
            if (!found)
            {
                return false;
            }
        }

        return true;
    }

    bool errorinstepJason()
    {
        //Debug.Log("isobj16: " + UtilityArtifacts.isobj16);
        if (canvasManager.isTutorial)
            return false;

        if (UtilityArtifacts.isobj16)
        {
            return errorinstepobj16();
        }
        //Debug.Log("Student soln Jason: "+stuJasonSoln.Display.Count);
        // get the jason from the server...
        string solJson = UtilityREST.solJson;
        string tlcd_diff_level_json = UtilityREST.tlcd_diff_level_json;
        string question_data = UtilityREST.studJson;
        //Debug.Log("question_data: " + question_data);
        int solution_No = UtilityREST.solution_No;
        JSONNode responce = JSON.Parse(solJson);
        JSONArray resArray = (JSONArray)responce;
        JSONNode soln = resArray[solution_No];
        JSONArray steps = (JSONArray)soln["Steps"];
        JSONNode ques_data = JSON.Parse(question_data);
        JSONNode attbSoln = ques_data["Attribute_Json"]["Solutions"];
        JSONArray attbSolnArray = (JSONArray)attbSoln;
        JSONNode attsoln = attbSolnArray[solution_No];
        JSONArray attsteps = (JSONArray)attsoln["Steps"];
        //Debug.Log("nextReleventstep: " + nextReleventstep);
        int releventStepNo = getReleventStepNoJson(nextReleventstep, steps, attsteps);
        //int releventStepNo = getReleventStepNoJson(stepNo - 1, steps, attsteps);
        if (releventStepNo == (steps.Count - 1))
        {
            islastStep = true;
        }
        else
        {
            islastStep = false;
            nextReleventstep = releventStepNo + 1;
        }
        JSONNode diff_level_json = JSON.Parse(tlcd_diff_level_json);
        JSONNode Steps_Prompts = diff_level_json["Steps_Prompts"];
        JSONArray Steps_Prompts_Array = (JSONArray)Steps_Prompts;
        JSONNode error_in_soln = Steps_Prompts_Array[UtilityREST.solution_No];
        string errorString = "";
        if (errorNo < 1)
        {
            errorString = "First_Time";
        }
        else
        {
            errorString = "Second_Time";
        }
        JSONNode Steps_PreRequiste = diff_level_json["Steps_PreRequiste"];
        JSONArray step_Req = (JSONArray)Steps_PreRequiste;
        JSONNode pre = step_Req[UtilityREST.solution_No];
        String prerequiste = pre[releventStepNo].Value;
        UtilityREST.prerequiste = prerequiste;
        // relevent soln jason from server
        JSONNode stepsData = steps[releventStepNo];
        JSONNode ATTSetNode = attsteps[releventStepNo];
        JSONArray displayJson = (JSONArray)stepsData["Display"];
        return checkErrorViaJson(errorString, displayJson, error_in_soln, ATTSetNode);
        // checking if the Count of display matches or not
        //if(stuJasonSoln.Display.Count!= displayJson.Count)
        //{
        //    JSONNode Steps_Error_Prompt = error_in_soln[errorString];
        //    JSONArray Steps_Error_Prompt_Array = (JSONArray)Steps_Error_Prompt;
        //    JSONNode releventError = Steps_Error_Prompt_Array[stepNo - 1];
        //    errorMsg = releventError["error_msg"].Value;
        //    Debug.Log("display error: "+ stuJasonSoln.Display.Count + " Count: "+ displayJson.Count);
        //    return true;
        //}
        //else
        //{
        //    int stepcounter = 0;
        //    List<int> PrevValue;
        //    foreach (JSONNode solndata in stepsData["Display"])
        //    {
        //        if (String.Equals(solndata["Type"].Value, "operator"))
        //        {
        //            if (!String.Equals(solndata["Name"].Value, stuJasonSoln.Display[stepcounter].Name))
        //            {
        //                JSONNode Steps_Error_Prompt = error_in_soln[errorString];
        //                JSONArray Steps_Error_Prompt_Array = (JSONArray)Steps_Error_Prompt;
        //                JSONNode releventError = Steps_Error_Prompt_Array[stepNo - 1];
        //                errorMsg = releventError["error_msg"].Value;
        //                Debug.Log("opertor error");
        //                return true;
        //            }
        //        }
        //        else if (String.Equals(solndata["Type"].Value, "variable"))
        //        {
        //            string actual_value = solndata["Actual_value"].Value;
        //            if (actual_value.Contains(","))
        //            {
        //                // it is a MF
        //                char sep = ',';
        //                string[] value_array = actual_value.Split(sep);
        //                if(!String.Equals(actual_value, stuJasonSoln.Display[stepcounter].Actual_value))
        //                {
        //                    JSONNode Steps_Error_Prompt = error_in_soln[errorString];
        //                    JSONArray Steps_Error_Prompt_Array = (JSONArray)Steps_Error_Prompt;
        //                    JSONNode releventError = Steps_Error_Prompt_Array[stepNo - 1];
        //                    errorMsg = releventError["error_msg"].Value;
        //                    Debug.Log("value error");
        //                    return true;

        //                }
        //            }
        //            else
        //            {
        //                int value_json = int.Parse(actual_value);
        //                // without swaping
        //                int value_list = int.Parse(stuJasonSoln.Display[stepcounter].Actual_value);
        //                if (value_json != value_list)
        //                {
        //                    // check weather the next block contains a operator like + or *
        //                    int currentCounter = stepcounter+1;
        //                    if(String.Equals(stuJasonSoln.Display[currentCounter].Type,UtilityREST.type_operator))
        //                    {
        //                        if(String.Equals(stuJasonSoln.Display[currentCounter].Name,UtilityREST.var_addition)|| String.Equals(stuJasonSoln.Display[currentCounter].Name, UtilityREST.var_multiplication))
        //                        {
        //                            String currentSigh = stuJasonSoln.Display[currentCounter].Name;

        //                        }
        //                    }
        //                    else
        //                    {
        //                        JSONNode Steps_Error_Prompt = error_in_soln[errorString];
        //                        JSONArray Steps_Error_Prompt_Array = (JSONArray)Steps_Error_Prompt;
        //                        JSONNode releventError = Steps_Error_Prompt_Array[stepNo - 1];
        //                        errorMsg = releventError["error_msg"].Value;
        //                        Debug.Log("value error");
        //                        return true;
        //                    }

        //                }
        //            }
        //        }
        //        stepcounter++;
        //    }
        //}
        //return false;
    }

    bool checkErrorViaJson(string errorString, JSONArray displayJson, JSONNode error_in_soln, JSONNode ATTSetNode)
    {
        // first check if server json and the stdn json count matches or not
        //Debug.Log(" stuJasonSoln.Attribute: " + stuJasonSoln.Attribute.NUM + "  " + stuJasonSoln.Attribute.TOTOP);
        //Debug.Log(" ATTSetNode.Attribute: " + ATTSetNode["Attribute"]["NUM"].AsInt + " ATTSetNode" + ATTSetNode["Attribute"]["TOTOP"].AsInt);
        studentJson.studJson.Add(stuJasonSoln);
        string studjson = JsonUtility.ToJson(studentJson);
        //Debug.LogError("displayJson: " + displayJson.ToString());
        //Debug.LogError("studentJson: " + JsonUtility.ToJson(stuJasonSoln).ToString());
        //Debug.Log("stuJasonSoln.Display: " + studjson + "studentJson: " + studentJson.ToString());
        //if (stuJasonSoln.Display.Count - stuJasonSoln.Attribute.TEXT != displayJson.Count - ATTSetNode["Attribute"]["TEXT"].AsInt)
        //if (stuJasonSoln.Attribute.TOTATT - stuJasonSoln.Attribute.TEXT != displayJson.Count - ATTSetNode["Attribute"]["TEXT"].AsInt)
        // check total num and total operator

        int NUM = ATTSetNode["Attribute"]["NUM"].AsInt;
        if (ATTSetNode["Attribute"]["TEXT"].AsInt > 0 && ATTSetNode["Attribute"]["F"].AsInt > 0)
        {
            NUM += ATTSetNode["Attribute"]["F"].AsInt * 2;
        }

        // for number as fraction 04-06-2020
        if(ATTSetNode["Attribute"]["NUM"].AsInt ==0 && ATTSetNode["Attribute"]["F"].AsInt > 0)
        {
            NUM += ATTSetNode["Attribute"]["F"].AsInt * 2;
        }

        Debug.Log("stuJasonSoln.Attribute.NUM : " + stuJasonSoln.Attribute.NUM + " :NUM: " + NUM + " stuJasonSoln.Attribute.TOTOP: " + stuJasonSoln.Attribute.TOTOP + " :: " + ATTSetNode["Attribute"]["TOTOP"].AsInt);
        //if (stuJasonSoln.Attribute.NUM != ATTSetNode["Attribute"]["NUM"].AsInt || stuJasonSoln.Attribute.TOTOP != ATTSetNode["Attribute"]["TOTOP"].AsInt)
        if (stuJasonSoln.Attribute.NUM != NUM || stuJasonSoln.Attribute.TOTOP != ATTSetNode["Attribute"]["TOTOP"].AsInt)
        {
            JSONNode Steps_Error_Prompt = error_in_soln[errorString];
            JSONArray Steps_Error_Prompt_Array = (JSONArray)Steps_Error_Prompt;
            JSONNode releventError = Steps_Error_Prompt_Array[nextReleventstep - 1];
            //JSONNode releventError = Steps_Error_Prompt_Array[stepNo - 1];
            errorMsg = releventError["error_msg"].Value;
            Debug.Log("display error: " + stuJasonSoln.Display.Count + " Count: " + displayJson.Count);
            return true;
        }
        else
        {
            // if the total count matches count the total number of operators and also individual operator count.
            int servrOperatorCount = 0;
            int studOperatorCount = 0;
            int studentTextCount = 0;
            int serverTextCount = 0;
            DifferetOperators servOperator = new DifferetOperators();
            DifferetOperators studOperator = new DifferetOperators();
            List<int> servervariablePosition = new List<int>();
            List<int> studvariablePosition = new List<int>();
            List<int> studTextvariablePosition = new List<int>();
            // counting server
            int i = 0;
            foreach (JSONNode solndata in displayJson)
            {
                //Debug.Log("solndata[].Value:" + solndata["Type"].Value+"Name"+ solndata["Name"].Value);
                if (String.Equals(solndata["Type"].Value, UtilityREST.type_operator))
                {
                    servrOperatorCount++;
                    checkoperatertype(solndata["Name"].Value, ref servOperator);
                    if (ATTSetNode["Attribute"]["TEXT"].AsInt > 0 && ATTSetNode["Attribute"]["F"].AsInt > 0)
                    {

                    }

                }
                else if (String.Equals(solndata["Type"].Value, UtilityREST.type_variable))
                {
                    if (String.Equals(solndata["Name"].Value, "enum_improper_fraction"))
                    {
                        //if (ATTSetNode["Attribute"]["TEXT"].AsInt > 0 && ATTSetNode["Attribute"]["F"].AsInt > 0)
                        {
                            servrOperatorCount += 3;
                            servOperator.closingBracket++;
                            servOperator.denominatorOperator++;
                            servOperator.openingBracket++;
                            servervariablePosition.Add(i);
                            //i++;
                            //servervariablePosition.Add(i);
                        }
                    }
                    else
                        servervariablePosition.Add(i);
                    if (String.Equals(solndata["Name"].Value, UtilityREST.type_server_text))
                    {
                        serverTextCount++;
                    }
                }
                i++;
            }
            i = 0;
            foreach (StepDisplay sd in stuJasonSoln.Display)
            {
                Debug.Log("stuJasonSoln: " + sd.Actual_value+ ", sd.Type: "+ sd.Type);
                if (String.Equals(sd.Type, UtilityREST.type_operator))
                {
                    studOperatorCount++;
                    checkoperatertype(sd.Name, ref studOperator);

                }
                else if (String.Equals(sd.Type, UtilityREST.type_variable))
                {

                    studvariablePosition.Add(i);
                }
                else if(String.Equals(sd.Type, UtilityREST.type_text))
                {
                    studentTextCount++;
                    studTextvariablePosition.Add(i);
                }
                i++;
            }
            Debug.Log("servrOperatorCount " + servrOperatorCount + " studOperatorCount: " + studOperatorCount + " servOperator: " + servOperator + " studOperator: "+ studOperator);
            if (servrOperatorCount == studOperatorCount && servOperator.Equals(studOperator))
            // Debug.Log("studOperatorCount " + stuJasonSoln.Attribute.OP + " servrOperatorCount: " + servrOperatorCount + " servOperator: " + servOperator + " studOperator: " + studOperator);
            //if(stuJasonSoln.Attribute.OP == ATTSetNode["Attribute"]["OP"].AsInt && servOperator.Equals(studOperator))
            {
                // chech for values now;
                int servCount = servervariablePosition.Count;
                int studCount = studvariablePosition.Count;
                if (serverTextCount > 0)
                {
                    studCount += studentTextCount;
                }
                Debug.Log("servCount: " + servCount + " studCount: " + studCount+ " studentTextCount: "+ studentTextCount+ "  serverTextCount: "+ serverTextCount);
                foreach (int p in servervariablePosition)
                {
                    string serverActualValue = displayJson[p]["Actual_value"].Value;
                    //Debug.Log("serverActualValue: " + serverActualValue + "pos: " + p);
                }

                foreach (int p in studvariablePosition)
                {
                    string studActualValue = stuJasonSoln.Display[p].Actual_value;
                    //Debug.Log("studActualValue: " + studActualValue + "pos: " + p);
                }
                foreach (int pos in servervariablePosition)
                {

                    if (pos >= 0)
                    {
                        // needto implement MF
                        string serverActualValue = displayJson[pos]["Actual_value"].Value;
                        //Debug.Log("serverActualValue: " + serverActualValue + "pos: " + pos);
                        if (serverActualValue.Contains(","))
                        {
                            if (String.Equals(displayJson[pos]["Name"].Value, "enum_improper_fraction"))
                            {
                                string[] number = serverActualValue.Split(',');
                                int num = int.Parse(number[0]);
                                int dem = int.Parse(number[1]);
                                int p = 0;
                                for (p = 0; p < studvariablePosition.Count; p++)
                                {
                                    int studPos = studvariablePosition[p];
                                    if (studPos >= 0)
                                    {
                                        string studActualValue = stuJasonSoln.Display[studPos].Actual_value;
                                        if (studActualValue.Contains(","))
                                        {
                                            continue;
                                        }
                                        else
                                        {
                                            int studAcVal = int.Parse(stuJasonSoln.Display[studPos].Actual_value);
                                            if (studPos < stuJasonSoln.Display.Count - 2 && num == studAcVal)
                                            {
                                                int next = int.Parse(stuJasonSoln.Display[studvariablePosition[p + 1]].Actual_value.Trim());
                                                if (dem == next)
                                                {
                                                    studvariablePosition[p] = -999;
                                                    studvariablePosition[p + 1] = -999;
                                                    servCount--;
                                                    studCount -= 2;
                                                }
                                            }


                                        }
                                    }
                                }

                            }
                            else
                            {
                                int p = 0;
                                for (p = 0; p < studvariablePosition.Count; p++)
                                {
                                    int studPos = studvariablePosition[p];
                                    if (studPos >= 0)
                                    {
                                        string studActualValue = stuJasonSoln.Display[studPos].Actual_value;
                                        if (studActualValue.Contains(","))
                                        {
                                            // check if thestrings are equal93.
                                            if (String.Equals(studActualValue, serverActualValue))
                                            {
                                                servCount--;
                                                studCount--;
                                                studvariablePosition[p] = -999;

                                            }
                                        }
                                    }
                                }
                            }
                            //if(p >= studvariablePosition.Count)
                            //{
                            //    JSONNode Steps_Error_Prompt = error_in_soln[errorString];
                            //    JSONArray Steps_Error_Prompt_Array = (JSONArray)Steps_Error_Prompt;
                            //    JSONNode releventError = Steps_Error_Prompt_Array[stepNo - 1];
                            //    errorMsg = releventError["error_msg"].Value;
                            //    Debug.Log("value error: ");
                            //    return true;
                            //}

                        }
                        else
                        {

                            if (!string.Equals(serverActualValue, ""))
                            {
                                //if (String.Equals(displayJson[pos]["Name"].Value, "enum_improper_fraction"))
                                if (String.Equals(displayJson[pos]["Name"].Value, "enum_string"))
                                {
                                    //string serTextValue
                                    Debug.LogError("server has String: "+ serverActualValue);
                                    // 
                                    Debug.Log("servCount: " + servCount);
                                    Debug.Log("studCount: " + studCount);
                                    for (int p = 0; p < studTextvariablePosition.Count; p++)
                                    {
                                        int studTextPos = studTextvariablePosition[p];
                                        string actualTextValue = stuJasonSoln.Display[studTextPos].Actual_value;
                                        if (actualTextValue.Contains(serverActualValue))
                                        {
                                            studTextvariablePosition[p] = -999;
                                            servCount--;
                                            studCount--;
                                            Debug.Log("found value");
                                        }
                                    }
                                        //studTextvariablePosition
                                }
                                else
                                {
                                    int serAcVal = int.Parse(serverActualValue);
                                    for (int p = 0; p < studvariablePosition.Count; p++)
                                    {
                                        int studPos = studvariablePosition[p];
                                        if (studPos >= 0)
                                        {

                                            // Debug.Log("stuJasonSoln.Display[studPos].Actual_value: " + stuJasonSoln.Display[studPos].Actual_value);
                                            if (stuJasonSoln.Display[studPos].Actual_value.Contains(","))
                                            {
                                                continue;
                                            }
                                            int studAcVal = int.Parse(stuJasonSoln.Display[studPos].Actual_value);
                                            if (studAcVal == serAcVal)
                                            {
                                                // value matches delete it from stud and serv position and chech weathen the left and right matches or not
                                                string servRight = "";
                                                if (pos < displayJson.Count - 1)
                                                    servRight = displayJson[pos + 1]["Name"].Value;
                                                string servLeft = "";
                                                if (pos > 0)
                                                    servLeft = displayJson[pos - 1]["Name"].Value;
                                                string studRight = "";
                                                if (studPos < stuJasonSoln.Display.Count - 1)
                                                    studRight = stuJasonSoln.Display[studPos + 1].Name;
                                                string studLeft = "";
                                                if (studPos > 0)
                                                    studLeft = stuJasonSoln.Display[studPos - 1].Name;
                                                // Debug.LogError("studAcVal: "+ studAcVal+ " serAcVal:"+ serAcVal + " servRight: " + servRight + " servLeft: " + servLeft + " studRight: " + studRight + " studLeft: " + studLeft);

                                                if ((String.Equals(servRight, UtilityREST.var_addition) || String.Equals(servRight, UtilityREST.var_subtraction)
                                                    || String.Equals(servRight, UtilityREST.var_multiplication) || String.Equals(servRight, UtilityREST.var_division)) &&
                                                    (String.Equals(servLeft, UtilityREST.var_addition) || String.Equals(servLeft, UtilityREST.var_subtraction)
                                                      || String.Equals(servLeft, UtilityREST.var_multiplication) || String.Equals(servLeft, UtilityREST.var_division))
                                                      && !String.Equals(servLeft, servRight))
                                                {
                                                    // 
                                                    if ((String.Equals(servRight, studRight) && String.Equals(servLeft, studLeft)) || (String.Equals(servRight, studLeft) && String.Equals(servLeft, studRight)))
                                                    {
                                                        //servervariablePosition[pos] = -999;
                                                        studvariablePosition[p] = -999;
                                                        servCount--;
                                                        studCount--;
                                                    }
                                                    else
                                                    {
                                                        continue;
                                                    }
                                                }
                                                else if ((String.Equals(servRight, UtilityREST.var_addition) || String.Equals(servRight, UtilityREST.var_subtraction)
                                                    || String.Equals(servRight, UtilityREST.var_multiplication) || String.Equals(servRight, UtilityREST.var_division)) ||
                                                    (String.Equals(servLeft, UtilityREST.var_addition) || String.Equals(servLeft, UtilityREST.var_subtraction)
                                                      || String.Equals(servLeft, UtilityREST.var_multiplication) || String.Equals(servLeft, UtilityREST.var_division)))
                                                {
                                                    if ((String.Equals(servRight, studRight) || String.Equals(servLeft, studLeft)) || (String.Equals(servRight, studLeft) || String.Equals(servLeft, studRight)))
                                                    {
                                                        //servervariablePosition[pos] = -999;
                                                        studvariablePosition[p] = -999;
                                                        servCount--;
                                                        studCount--;
                                                    }
                                                    else
                                                    {
                                                        continue;
                                                    }
                                                }
                                                else if ((String.Equals(servRight, UtilityREST.var_denom) && String.Equals(servLeft, UtilityREST.var_openingBracketsBlank)) || (String.Equals(servRight, UtilityREST.var_closingBracketsBlank) && String.Equals(servLeft, UtilityREST.var_denom)))
                                                {
                                                    //Debug.Log("value var_denom: " + studAcVal+ " studLeft:"+ studLeft+ " servLeft"+ servLeft+ " Equals"+ String.Equals(servLeft, studLeft));
                                                    if (String.Equals(studLeft, UtilityREST.var_openingBrackets))
                                                        studLeft = UtilityREST.var_openingBracketsBlank;
                                                    if (String.Equals(studRight, UtilityREST.var_closingBrackets))
                                                    {
                                                        studRight = UtilityREST.var_closingBracketsBlank;
                                                    }
                                                    if (String.Equals(servRight, studRight) && String.Equals(servLeft, studLeft))
                                                    {
                                                        // Debug.Log("value var_denom: " + studAcVal);
                                                        studvariablePosition[p] = -999;
                                                        servCount--;
                                                        studCount--;
                                                    }
                                                    else
                                                    {
                                                        continue;
                                                    }
                                                }
                                                else
                                                {
                                                    //servervariablePosition[pos] = -999;
                                                    //Debug.Log("value else: " + studAcVal);
                                                    studvariablePosition[p] = -999;
                                                    servCount--;
                                                    studCount--;

                                                }


                                                //if(String.Equals(servRight,UtilityREST.var_addition)|| String.Equals(servRight, UtilityREST.var_subtraction)
                                                //    ||String.Equals(servRight, UtilityREST.var_multiplication)|| String.Equals(servRight, UtilityREST.var_division))
                                                //{
                                                //    // check if student has it or not.
                                                //    if (String.Equals(servRight, studRight))
                                                //    {
                                                //        if ((String.Equals(servLeft, UtilityREST.var_addition) || String.Equals(servLeft, UtilityREST.var_subtraction)
                                                //    || String.Equals(servLeft, UtilityREST.var_multiplication) || String.Equals(servLeft, UtilityREST.var_division))&&!String.Equals(servLeft,servRight))
                                                //        {
                                                //            if (String.Equals(servLeft, studLeft))
                                                //            {
                                                //                servervariablePosition.Remove(pos);
                                                //                studvariablePosition.RemoveAt(studPos);
                                                //            }
                                                //            else
                                                //            {
                                                //                continue;
                                                //            }
                                                //        }
                                                //        else
                                                //        {
                                                //            servervariablePosition.Remove(pos);
                                                //            studvariablePosition.RemoveAt(studPos);
                                                //        }
                                                //    }else if(String.Equals(servRight, studLeft))
                                                //    {
                                                //        if ((String.Equals(servLeft, UtilityREST.var_addition) || String.Equals(servLeft, UtilityREST.var_subtraction)
                                                //   || String.Equals(servLeft, UtilityREST.var_multiplication) || String.Equals(servLeft, UtilityREST.var_division)) && !String.Equals(servLeft, servRight))
                                                //        {
                                                //            if (String.Equals(servLeft, studRight))
                                                //            {
                                                //                servervariablePosition.Remove(pos);
                                                //                studvariablePosition.RemoveAt(studPos);
                                                //            }
                                                //            else
                                                //            {
                                                //                continue;
                                                //            }
                                                //        }
                                                //    }
                                                //    else
                                                //    {
                                                //        continue;
                                                //    }
                                                //}
                                                //break;
                                            }

                                        }
                                    }
                                }
                                
                            }
                        }

                    }

                }

                //Debug.Log("ser " + servCount + " stud: " + studCount);
                if (servCount > 0 || studCount > 0)
                {
                    JSONNode Steps_Error_Prompt = error_in_soln[errorString];
                    JSONArray Steps_Error_Prompt_Array = (JSONArray)Steps_Error_Prompt;
                    JSONNode releventError = Steps_Error_Prompt_Array[stepNo - 1];
                    errorMsg = releventError["error_msg"].Value;
                    Debug.Log("value error: ");
                    return true;
                }
                else if(!logicOk( displayJson))
                {
                    // implementing operator logic here
                    JSONNode Steps_Error_Prompt = error_in_soln[errorString];
                    JSONArray Steps_Error_Prompt_Array = (JSONArray)Steps_Error_Prompt;
                    JSONNode releventError = Steps_Error_Prompt_Array[stepNo - 1];
                    errorMsg = releventError["error_msg"].Value;
                    Debug.Log("value error: ");
                    return true;
                }



                //if (servervariablePosition.Count > 0 || studvariablePosition.Count > 0)
                //{
                //    Debug.Log("value error");
                //}
            }
            else
            {
                // operator mismatch
                JSONNode Steps_Error_Prompt = error_in_soln[errorString];
                JSONArray Steps_Error_Prompt_Array = (JSONArray)Steps_Error_Prompt;
                JSONNode releventError = Steps_Error_Prompt_Array[stepNo - 1];
                errorMsg = releventError["error_msg"].Value;
                Debug.Log("operator error: ");
                return true;
            }

           // Debug.Log("servrOperatorCount: " + servrOperatorCount + " studOperatorCount: " + studOperatorCount + " servOperator: " + servOperator.plusSigh + " studOperator: " + studOperator.plusSigh);

        }
        return false;
    }

    // new 
    bool logicOk(JSONArray displayJson)
    {
        //Debug.LogError(" logicOk displayJson: " + displayJson.ToString());
        //Debug.LogError("studentJson: " + JsonUtility.ToJson(studentJson).ToString());
        //Debug.LogError("studentJsonsoln: " + JsonUtility.ToJson(stuJasonSoln).ToString());
        List<List<int>> onStudleft =  new List<List<int>>();
        List<List<int>> onStudentRight = new List<List<int>>();
        List<List<int>> onServerleft = new List<List<int>>();
        List<List<int>> onServerRight = new List<List<int>>();
        List<int> StudValueLeft = new List<int>();
        List<int> StudValueRight = new List<int>();
        List<int> SerValueLeft = new List<int>();
        List<int> SerValueRight = new List<int>();
        //get All numerator from student
        int i = 0;
        // server data
        //Debug.LogError("logicOk");
        foreach (JSONNode solndata in displayJson)
        {
            ////Debug.Log(solndata["Type"].Value + " name: " + solndata["Name"].Value+ " Actual_value : " + solndata["Actual_value"].Value);

            if (String.Equals(solndata["Type"].Value, UtilityREST.type_operator)&& String.Equals(solndata["Name"].Value, UtilityREST.var_denom))
            {
                //get all numerator
                List<int> val = new List<int>();
                for (int j = i; j >= 0; j--)
                {
                    JSONNode sol = displayJson[j];
                    //Debug.LogError("server j: "+j + " Type: " + sol["Type"].Value + "  Actual_value: " + sol["Actual_value"].Value +"Name: "+ sol["Name"].Value);
                    if (String.Equals(sol["Type"].Value, UtilityREST.type_operator) && String.Equals(sol["Name"].Value, UtilityREST.var_openingBracketsBlank))
                        break;
                    else
                    {
                       
                        if(String.Equals(sol["Type"].Value, UtilityREST.type_variable))
                        {
                            string serverActualValue = sol["Actual_value"].Value;
                            int valu = int.Parse(serverActualValue);
                            //Debug.LogError("serverActualValue added : " + serverActualValue);
                            val.Add(valu);
                        }
                       
                    }
                    
                }
                //Debug.LogError("server Num count: " + val.Count + "  onServerleft: "+ onServerleft.Count);

                onServerleft.Add(val);

                // get denominator
                int k = i;
                //val.Clear();
                //while (k< displayJson.Count)
                //{
                //    JSONNode sol = displayJson[k];
                //    if (String.Equals(sol["Type"].Value, UtilityREST.type_operator) && String.Equals(solndata["Name"].Value, UtilityREST.var_closingBracketsBlank))
                //        break;
                //    else
                //    {

                //        if (String.Equals(sol["Type"].Value, UtilityREST.type_variable))
                //        {
                //            string serverActualValue = sol["Actual_value"].Value;
                //            int valu = int.Parse(serverActualValue);
                //            val.Add(valu);
                //        }

                //    }
                //    k++;
                //    sol = displayJson[k];
                //}
                //onServerRight.Add(val);
                //Debug.LogError("server dem count: " + val.Count);
            }else if(String.Equals(solndata["Type"].Value, UtilityREST.type_variable) && String.Equals(solndata["Name"].Value, UtilityREST.enum_improper_fraction))
            {
                string serverActualValue = solndata["Actual_value"].Value;
                //Debug.LogError("Numerator: " + solndata["N"].Value);
                string[] number = serverActualValue.Split(',');
                int num = int.Parse(number[0]);
                int dem = int.Parse(number[1]);
                List<int> val = new List<int>();
                val.Add(num);
                onServerleft.Add(val);
            }

           if (String.Equals(solndata["Type"].Value, UtilityREST.type_operator) && String.Equals(solndata["Name"].Value, UtilityREST.var_comma))
            {
                // get all valus on right side , leftof comma 
               
                for (int k = i; k >= 0; k--)
                {
                    JSONNode sol = displayJson[k];
                    if (String.Equals(sol["Type"].Value, UtilityREST.type_variable))
                    {
                        string serverActualValue = sol["Actual_value"].Value;
                        int valu = int.Parse(serverActualValue);
                        Debug.Log("serverActualValue added : " + serverActualValue);
                        SerValueLeft.Add(valu);
                    }
                }
                for (int k = i; k < displayJson.Count; k++)
                {
                    JSONNode sol = displayJson[k];
                    if (String.Equals(sol["Type"].Value, UtilityREST.type_variable))
                    {
                        string serverActualValue = sol["Actual_value"].Value;
                        int valu = int.Parse(serverActualValue);
                        Debug.Log("serverActualValue added : " + serverActualValue);
                        SerValueRight.Add(valu);
                    }
                }

            }
            i++;
        }

        // get student data

        i = 0;
        foreach (StepDisplay sd in stuJasonSoln.Display)
        {
            if (String.Equals(sd.Type, UtilityREST.type_operator)&& String.Equals(sd.Name, UtilityREST.var_denom))
            {
                int opBracCount = 1;
                int j = i;
                List<int> value = new List<int>();
                while(opBracCount > 0)
                {
                    
                    StepDisplay psd = stuJasonSoln.Display[j];
                   // Debug.Log("student: " + "Type: " + psd.Name + "  Actual_value: " + psd.Actual_value);
                    if (String.Equals(psd.Type, UtilityREST.type_operator))
                    {
                        if (String.Equals(psd.Name, UtilityREST.var_closingBrackets))
                            opBracCount++;
                        else if (String.Equals(psd.Name, UtilityREST.var_openingBrackets))
                            opBracCount--;
                    }
                    else if (String.Equals(psd.Type, UtilityREST.type_variable))
                    {
                        //Debug.Log("student: value" + psd.Actual_value);
                        value.Add(int.Parse(psd.Actual_value));
                    }
                    j--;
                }
                //Debug.LogError("stud Num count: " + value.Count+ " onStudleft: "+ onStudleft.Count);

                onStudleft.Add(value);


                //int cloBracCount = 1;
                //int k = i;
                //value.Clear();
                //while(cloBracCount > 0)
                //{
                //    StepDisplay psd = stuJasonSoln.Display[k];
                //    if (String.Equals(psd.Type, UtilityREST.type_operator))
                //    {
                //        if (String.Equals(psd.Name, UtilityREST.var_closingBrackets))
                //            cloBracCount--;
                //        else if (String.Equals(psd.Name, UtilityREST.var_openingBrackets))
                //            cloBracCount++;
                //    }
                //    else if (String.Equals(psd.Type, UtilityREST.type_variable))
                //    {
                //        value.Add(int.Parse(psd.Actual_value));
                //    }
                //    k++;
                //}
                //onStudentRight.Add(value);
               // Debug.LogError("stud den count: " + value.Count);
            }
            if(String.Equals(sd.Type, UtilityREST.type_operator) && String.Equals(sd.Name, UtilityREST.var_comma))
            {
                for(int k = i; k >= 0; k--)
                {
                    StepDisplay psd = stuJasonSoln.Display[k];
                    if (String.Equals(psd.Type, UtilityREST.type_variable))
                    {
                        //Debug.Log("student: value" + psd.Actual_value);
                        StudValueLeft.Add(int.Parse(psd.Actual_value));
                    }
                }

                for (int k = i; k< stuJasonSoln.Display.Count; k++)
                {
                    StepDisplay psd = stuJasonSoln.Display[k];
                    if (String.Equals(psd.Type, UtilityREST.type_variable))
                    {
                        //Debug.Log("student: value" + psd.Actual_value);
                        StudValueRight.Add(int.Parse(psd.Actual_value));
                    }
                }
            }
            i++;
        }

        if(checkArrayValues(onServerleft, onStudleft))
        {
            //Debug.LogError("check indiviual value logic");
        }
        else
        {
            //Debug.LogError("checkArrayValues");
            return false;
        }

        if (checkAllTheArrays(SerValueLeft, SerValueRight, StudValueLeft, StudValueRight))
        {
            // values ok
        }
        else
        {
            //Debug.LogError("checkAllTheArrays");
            return false;
        }
           

        return true;
    }

    bool checkAllTheArrays(List<int>serA, List<int> serB, List<int> studA, List<int> studB)
    {
        if(serA.Count == studA.Count)
        {
            foreach(int val in serA)
            {
                int ind = studA.IndexOf(val);
                if (ind < 0)
                {
                    break;
                }
                else
                {
                    studA.RemoveAt(ind);
                }
            }
            
        }
        if(studA.Count > 0)
        {
            if(serA.Count == studB.Count)
            {
                foreach (int val in serA)
                {
                    int ind = studB.IndexOf(val);
                    if (ind < 0)
                    {
                        break;
                    }
                    else
                    {
                        studB.RemoveAt(ind);
                    }
                }
                if(studB.Count > 0)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        return true;
    }

    bool checkArrayValues(List<List<int>> serNum, List<List<int>> studNum)
    {
        List<int> serveCount = new List<int>();
        List<int> studentCount = new List<int>();
        //Debug.LogError("serveCount serNum: " + serNum.Count);
        foreach (List<int> vs in serNum)
        {
            //Debug.Log("server Num serNum: " + serNum.Count);
            //foreach (int val in vs)
            //{
            //    //Debug.Log("vs: " + vs.Count);
            //    Debug.Log("val: " + val);
            //}
            //Debug.LogError("serveCount: " + vs.Count);
            serveCount.Add(vs.Count);
        }

        foreach (List<int> vs in studNum)
        {
            //Debug.Log("student Num");
            //foreach (int val in vs)
            //{
            //    Debug.Log("val: " + val);
            //}
            //Debug.LogError("studentCount: " + vs.Count);
            studentCount.Add(vs.Count);
        }

        
        for(int k = 0;k< serveCount.Count;)
        //foreach(int i in serveCount)
        {
            int i = serveCount[k];
            int pos = studentCount.IndexOf(i);

            studentCount.RemoveAt(pos);
            serveCount[k] = -9999;
            k++;
        }

        if(countarrarCount(serveCount, studentCount))
        {
            //Debug.LogError("count matched");
            foreach(List<int> sdv in serNum)
            {
                int sdvcount = sdv.Count;
                if(sdvcount > 1)
                {
                    for (int i = 0; i < studNum.Count; i++)
                    {
                        if(studNum[i].Count == sdvcount)
                        {
                            if (checkListValues(studNum[i], sdv))
                            {
                                // the valus matched
                                studNum[i].Clear();
                            }
                        }
                    }
                }
                
            }

            foreach(List<int>a in studNum)
            {
                //Debug.Log(a.Count);
                if (a.Count > 1)
                    return false;
            }
        }
        else
        {
            return false;
        }
        return true;
    }

    bool checkListValues(List<int> a, List<int> b)
    {
        for (int k = 0; k < a.Count;)
        //foreach(int i in serveCount)
        {
            int i = a[k];
            int pos = b.IndexOf(i);
            Debug.LogError("checkListValues pos" + pos);
            if(pos >= 0)
            {
                b.RemoveAt(pos);
                a[k] = -9999;
                k++;
            }
            else
            {
                return false;
            }
                
            
        }
        if (!countarrarCount(a, b)){
            return false;
        }
        return true;
    }

    bool countarrarCount(List<int> a, List<int> b)
    {
        foreach(int i in a)
        {
            //Debug.LogError("i= " + i);
            if(i != -9999)
            {
                return false;
            }
        }
        if (b.Count > 0)
            return false;
        return true;
    }

    void checkoperatertype(string oper, ref DifferetOperators opert)
    {
        //= new DifferetOperators();
        switch (oper)
        {
            case UtilityREST.var_addition:
                opert.plusSigh++;
                break;
            case UtilityREST.var_subtraction:
                opert.subSigh++;
                break;
            case UtilityREST.var_multiplication:
                opert.multiplySigh++;
                break;
            case UtilityREST.var_division:
                opert.divideSigh++;
                break;
            case UtilityREST.var_closingBrackets:
                opert.closingBracket++;
                break;
            case UtilityREST.var_openingBrackets:
                opert.openingBracket++;
                break;
            case UtilityREST.var_equal:
                opert.equalSigh++;
                break;
            case UtilityREST.var_denom:
                opert.denominatorOperator++;
                break;
            case UtilityREST.var_closingBracketsBlank:
                opert.closingBracket++;
                break;
            case UtilityREST.var_openingBracketsBlank:
                opert.openingBracket++;
                break;
        }
        //return opert;
    }

    // getting student json
    void getStudentJson()
    {
        //Debug.Log("getStudentJson");
        stuJasonSoln = new StudentSolution();
        int value = 0;
        int count = 0;
        string mf_value = "";
        StepDisplay va;
        string prev_type = "";
        int braketingStepCounter = 0;// counter to add brackets to previous steps.
        bool bracketAdded = false;
        Attributes_step stepAtt = new Attributes_step();
        for (int i = 0; i < allTexDrawInaRow.Count - 1; i++)
        {
            TexDrawUtils tex = allTexDrawInaRow[i];
            // Debug.Log("tex count " + tex.faceValue+ "texType " + tex.texType);

            if (!tex.multiDigitAllowed)
            {
                if (String.Equals(tex.texType, UtilityREST.General))
                {
                    //Debug.Log("Gen prev_type: " + prev_type);
                    if (tex.expIndex.Count > 0)
                    {
                        // it contains a operator
                        //Debug.Log("General operator");
                        if (value > 0)
                        {
                            va = new StepDisplay();
                            va.Actual_value = value.ToString();
                            va.Type = UtilityREST.type_variable;
                            va.Name = UtilityREST.num;
                            stuJasonSoln.Display.Add(va);
                            stepAtt.NUM++;
                            braketingStepCounter++;
                        }

                        va = new StepDisplay();
                        va.Type = UtilityREST.type_operator;
                        va.Name = tex.expValue[tex.expValue.Count - 1];
                        stuJasonSoln.Display.Add(va);
                        if (va.Name.Equals("op_comma") || va.Name.Equals("op_isequal") || va.Name.Equals("op_unknown") || va.Name.Equals("op_isnotequal"))
                        {

                        }
                        else
                        {
                            stepAtt.NUMOP++;
                            stepAtt.TOTOP++;
                            //Debug.LogError(" op NUMOP: " + stepAtt.NUMOP + va.Name);
                        }

                        braketingStepCounter++;
                        value = 0;
                        //stuJasonSoln.Display.

                    }
                    else
                    {
                        // its  general digit collect values;
                       // Debug.Log("General values " + value);
                        if (String.Equals(prev_type, UtilityREST.F) || String.Equals(prev_type, UtilityREST.MF))
                        {
                            //Debug.Log("need to inplement brackets: " + braketingStepCounter);
                            if (bracketAdded)
                            {
                                va = new StepDisplay();
                                va.Type = UtilityREST.type_operator;
                                va.Name = UtilityREST.var_closingBrackets;
                                stuJasonSoln.Display.Insert(stuJasonSoln.Display.Count - 1, va);
                            }
                            else
                            {
                                va = new StepDisplay();
                                va.Type = UtilityREST.type_operator;
                                va.Name = UtilityREST.var_openingBrackets;
                                stuJasonSoln.Display.Insert(stuJasonSoln.Display.Count - braketingStepCounter, va);
                                va = new StepDisplay();
                                va.Type = UtilityREST.type_operator;
                                va.Name = UtilityREST.var_closingBrackets;
                                stuJasonSoln.Display.Insert(stuJasonSoln.Display.Count - 1, va);
                            }
                            va = new StepDisplay();
                            va.Type = UtilityREST.type_operator;
                            va.Name = UtilityREST.var_openingBrackets;
                            stuJasonSoln.Display.Add(va);
                            bracketAdded = true;

                            braketingStepCounter = 0;
                        }
                        else if (String.Equals(prev_type, UtilityREST.Text))
                        {
                            Debug.Log("prev " + prev_type);
                        }
                        value = value * 10 + tex.faceValue;
                        prev_type = UtilityREST.General;
                    }
                }
                else
                {
                    Debug.Log("!multiDigitAllowed Text prev_type: " + prev_type + "tex.texType: " + tex.texType + "value" + value);
                }

            }
            else
            {
                // tex is multitex
                // Debug.Log("texType " + tex.texType + " pos:" + tex.texPosition);
                if (value > 0)
                {
                    // if value is there before exp
                    va = new StepDisplay();

                }
                if (String.Equals(tex.texType, UtilityREST.F))
                {
                    if (String.Equals(prev_type, UtilityREST.General))
                    {
                        Debug.Log("Fraction adjust brackets counter" + braketingStepCounter);
                        if (bracketAdded)
                        {
                            va = new StepDisplay();
                            va.Type = UtilityREST.type_operator;
                            va.Name = UtilityREST.var_closingBrackets;
                            stuJasonSoln.Display.Insert(stuJasonSoln.Display.Count - 1, va);
                        }
                        else
                        {
                            va = new StepDisplay();
                            va.Type = UtilityREST.type_operator;
                            va.Name = UtilityREST.var_openingBrackets;
                            stuJasonSoln.Display.Insert(stuJasonSoln.Display.Count - braketingStepCounter, va);
                            va = new StepDisplay();
                            va.Type = UtilityREST.type_operator;
                            va.Name = UtilityREST.var_closingBrackets;
                            stuJasonSoln.Display.Insert(stuJasonSoln.Display.Count - 1, va);

                        }
                        va = new StepDisplay();
                        va.Type = UtilityREST.type_operator;
                        va.Name = UtilityREST.var_openingBrackets;
                        stuJasonSoln.Display.Add(va);
                        bracketAdded = true;
                        braketingStepCounter = 0;

                    }
                    prev_type = UtilityREST.F;
                    if ((String.Equals(tex.texPosition, UtilityREST.Numerator)))
                    {
                        if (tex.expValue.Count > 2)
                        {
                            stepAtt.NUMOP += tex.expValue.Count - 2;
                            //stepAtt.TOTOP += stepAtt.NUMOP;
                            //Debug.LogError(" op NUMOP: " + stepAtt.NUMOP);
                        }
                        else
                            stepAtt.F++;
                        for (int j = 0; j < tex.expValue.Count; j++)
                        {
                            va = new StepDisplay();
                            va.Type = UtilityREST.type_operator;
                            va.Name = tex.expValue[j];
                            stuJasonSoln.Display.Add(va);
                            braketingStepCounter++;
                            if (j < tex.expValue.Count - 1)
                            {
                                va = new StepDisplay();
                                va.Actual_value = tex.digitvalue[j].ToString();
                                va.Type = UtilityREST.type_variable;
                                va.Name = UtilityREST.num;
                                stuJasonSoln.Display.Add(va);
                                stepAtt.NUM++;
                                braketingStepCounter++;
                            }

                        }
                    }
                    else
                    {
                        //Debug.Log("tex.expValue.Count: " + tex.expValue.Count + "tex.texPosition: " + tex.texPosition);
                        if (tex.expValue.Count > 1)
                        {
                            stepAtt.NUMOP += tex.expValue.Count - 1;
                            //stepAtt.TOTOP += tex.expValue.Count - 1;
                            //Debug.LogError(" op NUMOP: " + stepAtt.NUMOP);
                        }
                        for (int j = 0; j < tex.expValue.Count; j++)
                        {
                            va = new StepDisplay();
                            va.Actual_value = tex.digitvalue[j].ToString();
                            va.Type = UtilityREST.type_variable;
                            va.Name = UtilityREST.num;
                            stuJasonSoln.Display.Add(va);
                            stepAtt.NUM++;
                            braketingStepCounter++;
                            va = new StepDisplay();
                            va.Type = UtilityREST.type_operator;
                            va.Name = tex.expValue[j];
                            stuJasonSoln.Display.Add(va);
                            braketingStepCounter++;


                        }
                    }

                }
                else if (String.Equals(tex.texType, UtilityREST.MF))
                {
                    // test
                    if (String.Equals(prev_type, UtilityREST.General))
                    {
                        Debug.Log("Fraction adjust brackets counter" + braketingStepCounter);
                        if (bracketAdded)
                        {
                            va = new StepDisplay();
                            va.Type = UtilityREST.type_operator;
                            va.Name = UtilityREST.var_closingBrackets;
                            stuJasonSoln.Display.Insert(stuJasonSoln.Display.Count - 1, va);
                        }
                        else
                        {
                            va = new StepDisplay();
                            va.Type = UtilityREST.type_operator;
                            va.Name = UtilityREST.var_openingBrackets;
                            stuJasonSoln.Display.Insert(stuJasonSoln.Display.Count - braketingStepCounter, va);
                            va = new StepDisplay();
                            va.Type = UtilityREST.type_operator;
                            va.Name = UtilityREST.var_closingBrackets;
                            stuJasonSoln.Display.Insert(stuJasonSoln.Display.Count - 1, va);

                        }
                        va = new StepDisplay();
                        va.Type = UtilityREST.type_operator;
                        va.Name = UtilityREST.var_openingBrackets;
                        stuJasonSoln.Display.Add(va);
                        bracketAdded = true;
                        braketingStepCounter = 0;

                    }// test
                    prev_type = UtilityREST.MF;
                    if ((String.Equals(tex.texPosition, UtilityREST.General)) || (String.Equals(tex.texPosition, UtilityREST.Numerator)))
                    {
                        mf_value = mf_value + tex.digitvalue[0].ToString() + ",";
                    }
                    else if ((String.Equals(tex.texPosition, UtilityREST.Denominator)))
                    {
                        mf_value = mf_value + tex.digitvalue[0].ToString();
                        va = new StepDisplay();
                        va.Type = UtilityREST.type_variable;
                        va.Name = UtilityREST.enumMF;
                        va.Actual_value = mf_value;
                        stuJasonSoln.Display.Add(va);
                        stepAtt.MF++;
                        braketingStepCounter++;
                    }
                }
                else
                {
                    Debug.Log("MultiDigitAllowed Text prev_type: " + prev_type + "Current : " + tex.texType + "values: " + value);

                    if (String.Equals(tex.texType, UtilityREST.Text))
                    {
                        if (value > 0)
                        {
                            va = new StepDisplay();
                            va.Actual_value = value.ToString();
                            va.Type = UtilityREST.type_variable;
                            va.Name = UtilityREST.num;
                            stuJasonSoln.Display.Add(va);
                            stepAtt.NUM++;
                            braketingStepCounter++;
                        }
                        va = new StepDisplay();
                        va.Type = UtilityREST.type_text;
                        va.Name = UtilityREST.type_text;
                        va.Actual_value = tex.texString;
                        stuJasonSoln.Display.Add(va);
                        stepAtt.TEXT++;
                        braketingStepCounter++;
                        value = 0;
                        Debug.Log("stuJasonSolnText: " + va.Actual_value);
                    }
                }
                value = 0;

            }
        }
        if (value > 0)
        {
            va = new StepDisplay();
            if (nexttextback)
            {
                //Debug.Log("value: " + value);
                va.Actual_value = ReverseString_Rec(value.ToString());
                //Debug.Log("value: " + va.Actual_value);
            }
            else
                va.Actual_value = value.ToString();

            va.Type = UtilityREST.type_variable;
            va.Name = UtilityREST.num;
            stuJasonSoln.Display.Add(va);
            stepAtt.NUM++;

        }
        if (bracketAdded)
        {
            va = new StepDisplay();
            va.Type = UtilityREST.type_operator;
            va.Name = UtilityREST.var_closingBrackets;
            stuJasonSoln.Display.Add(va);
        }

        // biggest change
        stepAtt.TOTATT = stepAtt.NUM;
        stepAtt.TOTOP = stepAtt.NUMOP;
        stuJasonSoln.Attribute = stepAtt;
        //string json = JsonUtility.ToJson(stuJasonSoln + " stuJasonSoln.Attribute: "+ stuJasonSoln.Attribute.TOTATT);
        //Debug.Log("Agjust  TOTATT : " + step_attribute.TOTATT+ " stepAtt.TOTAT: "+ stepAtt.TOTATT);
        //Debug.Log("json string: " + json);
    }

    public string ReverseString_Rec(string str)
    {
        if (str.Length <= 1) return str;
        else return ReverseString_Rec(str.Substring(1)) + str[0];
    }

    void NextStep()
    {
        if (isActive)
        {
            StopblinkingCursor();
            //Debug.Log("valueentered NextStep: " + valueentered);
            if (valueentered > 0)
            {
                // form studendjason


                //Debug.Log("display: " + display + " valueEnteredinList: " + valueEnteredinList + " operatedClicked: " + operatedClicked);
                if (!valueEnteredinList)
                {
                    /*if (operatedClicked)
                    {
                        operators.Insert(0, "op-oround");
                        operators.Add("op-cround");
                        display += 2;

                    }*/


                    //Debug.Log("val: " + value);
                    //instanciateOnThisBlock.GetComponent<TexDrawUtils>().notClickable = true;
                    if (currentWorkingBlock.GetComponent<TexDrawUtils>().multiDigitAllowed)
                    {
                        // as single digit has single value
                        currentWorkingBlock.GetComponent<TexDrawUtils>().digitvalue[currentWorkingBlock.GetComponent<TexDrawUtils>().digitvalue.Count - 1] = value;
                    }
                    if (currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex.Count == 0)
                    {
                        values.Add(value);
                        value = 0;
                        currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex.Add(values.Count - 1);
                    }
                    else
                    {
                        int index = currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex[currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex.Count - 1];
                        if (operSighnAdded && currentWorkingBlock.GetComponent<TexDrawUtils>().multiDigitAllowed)
                        {
                            //Debug.Log("Valuesadded at +1");
                            values.Insert(index + 1, value);
                            chagebelowtextUtil(currentWorkingBlock.GetComponent<TexDrawUtils>().serialNumber);
                        }
                        else
                        {
                            //Debug.Log("Valuesadded at index");
                            values[index] = value;
                        }

                        //value = 0;
                        currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex.Add(index);
                    }

                    //Debug.Log("TexDrawUtils value Index: " + currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex[currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex.Count - 1]);
                    valueEnteredinList = true;
                }
                //in development
                getStudentJson();
                //errorinstepJason();
                int lastreleventStep = nextReleventstep;
                if (!errorinstepJason())//if (!errorinStep()) 
                {

                    //ScrollView_Debugging.Instance.AddToList("\n" + "Correct steps and correct result " + "\n" + "\n" + "Knows to identify equivalent fractions" + "\n"
                      //+ "Proceed to next question" + "\n" + "..............." + "\n");

                    if (islastStep)
                    {
                        //Debug.Log("is last step");
                        string msg = "submit and load next question as this is the last step";

                        

                        if (OnShowMessage != null)
                            OnShowMessage(msg);
                        //CheckCurrentCanvas();
                        StartBlinkingCursor();
                    }
                    else
                    {
                        if (onLogMessage != null)
                            onLogMessage("User has done the step correctly " + sucessLog);
                        sucessLog = "";
                        studentJson.studJson.Add(stuJasonSoln);
                        errorNo = 0;
                        UtilityREST.status = "2";
                        if (UtilityREST.isprerequisit)
                        {
                            // assuming all is right as no check
                            UtilityREST.status = "2";
                            UtilityREST.prerequiste = "";
                            UtilityREST.prerequiste_status = "";
                            UtilityREST.tlcd_id = UtilityREST.session_tlcdpr_id;
                        }
                        else
                        {
                            UtilityREST.status = "2";
                            UtilityREST.prerequiste = "";
                            UtilityREST.prerequiste_status = "";
                            UtilityREST.session_tlcd_id = "";
                            UtilityREST.session_tlcdpr_id = "";
                            /*
                            PlayerPrefs.SetString("status", "2");
                            PlayerPrefs.SetString("prerequiste", "");
                            PlayerPrefs.SetString("prerequiste_status", "");
                            PlayerPrefs.SetString("session_tlcd_id", "");
                            PlayerPrefs.SetString("session_parent_pre_requiste_child_id", "");
                            PlayerPrefs.SetString("session_tlcdpr_id", "");
                            PlayerPrefs.Save();*/
                        }
                        if (singleStep)
                        {
                            StartBlinkingCursor();
                            printStepNumber();
                            removeAllvalueIndex();
                            Answer();
                            valueEnteredinList = false;
                            // 

                        }
                        else
                        {
                            // it is a multistep problem
                            Debug.Log("fractionCounter " + fractionCounter);
                            //if (fractionCounter < 1)
                            //{
                            //    StartBlinkingCursor();
                            //    AddFraction();
                            //    StopblinkingCursor();
                            //    Debug.Log("no fraction");
                            //}
                            textBoxTypeMode = defaultmode;
                            Functionmode = defaultmode;
                            if (currentMaxSpaceInRow < currentrowPrevLineSpace)
                                currentMaxSpaceInRow = currentrowPrevLineSpace;
                            //Debug.Log("currentMaxSpaceInRow: " + currentMaxSpaceInRow + " PrevRowMaxSpace: " + PrevRowMaxSpace);
                            if (rowNumber > 1)
                            {
                                if (PrevRowMaxSpace < currentMaxSpaceInRow)
                                {
                                    //Debug.Log("need to adjust previous row:");
                                    for (int r = 1; r < rowNumber; r++)
                                    {
                                        currentRow = transform.GetChild(r).gameObject;
                                        // Debug.Log("r= " + r + "currentRow: " + currentRow.name);
                                        GameObject temp = currentRow.transform.GetChild(indexofWorkingArea).gameObject;
                                        VariableGridLayoutGroup rVariableGridLayout = temp.GetComponent<VariableGridLayoutGroup>();
                                        GameObject child;
                                        //Debug.Log("currentMaxSpaceInRow: " + currentMaxSpaceInRow + " PrevRowMaxSpace: " + PrevRowMaxSpace + "Functionmode: " + Functionmode + "textBoxTypeMode: " + textBoxTypeMode);
                                        for (int j = 0; j < (currentMaxSpaceInRow - PrevRowMaxSpace); j++)
                                        {
                                            rVariableGridLayout.constraintCount++;

                                            //for (int i = 0; i <= noOfRowsinStep; i++)
                                            // {
                                            int i = 0;

                                            child = (GameObject)Instantiate(GeneralDigit, transform);
                                            child.transform.parent = temp.transform;
                                            child.GetComponent<TEXDraw>().text = child.GetComponent<TEXDraw>().text.Insert(0, " ");
                                            intTestNo++;
                                            child.name = "Child" + rowNumber.ToString() + intTestNo.ToString();

                                            switch (Functionmode)
                                            {
                                                case modeBasic:
                                                    //Debug.Log("Funtion Mode:");
                                                    if (i < 3)
                                                    {
                                                        child.GetComponent<TexDrawUtils>().multiDigitAllowed = true;
                                                        child.GetComponent<TexDrawUtils>().numberOfDigit = 0;
                                                    }
                                                    if (i < 4)
                                                        child.transform.SetSiblingIndex((i * currentNoofColumns) - 1);
                                                    else
                                                        child.transform.SetSiblingIndex((i * currentNoofColumns) - 2);
                                                    break;
                                                case modeLCM:
                                                    //Debug.Log("LCM Mode:");
                                                    child.transform.SetSiblingIndex((i - 1) * currentNoofColumns);
                                                    break;
                                                case modeHCF:
                                                    //Debug.Log("LCM Mode:");
                                                    child.transform.SetSiblingIndex((i - 1) * currentNoofColumns);
                                                    break;
                                                default:
                                                    //Debug.Log("Default Mode:");
                                                    child.transform.SetSiblingIndex((rVariableGridLayout.constraintCount - 1));
                                                    break;

                                            }
                                            //}
                                        }
                                        // PrevRowMaxSpace = currentMaxSpaceInRow;
                                    }
                                    PrevRowMaxSpace = currentMaxSpaceInRow;
                                }
                                else if (PrevRowMaxSpace > currentMaxSpaceInRow)
                                {
                                    GameObject temp = currentRow.transform.GetChild(indexofWorkingArea).gameObject;
                                    VariableGridLayoutGroup mVariableGridLayout = temp.GetComponent<VariableGridLayoutGroup>();
                                    //Debug.Log("    need to adjust current row: ");

                                    GameObject child;
                                    /* if (noOfRows < 3)
                                        {*/
                                    for (int j = 0; j < (PrevRowMaxSpace - currentMaxSpaceInRow); j++)
                                    {
                                        mVariableGridLayout.constraintCount++;

                                        //for (int i = 0; i <= noOfRowsinStep; i++)
                                        // {
                                        int i = 0;
                                        if (!singleStep)
                                            child = (GameObject)Instantiate(FractiondigitUnclickable, transform);
                                        else
                                            child = (GameObject)Instantiate(GeneralDigit, transform);
                                        child.transform.parent = temp.transform;
                                        //child.GetComponent<TEXDraw>().text = child.GetComponent<TEXDraw>().text.Insert(0, " ");
                                        intTestNo++;
                                        child.name = "Child" + rowNumber.ToString() + intTestNo.ToString();

                                        switch (Functionmode)
                                        {
                                            case modeBasic:
                                                //Debug.Log("Funtion Mode:");
                                                if (i < 3)
                                                {
                                                    child.GetComponent<TexDrawUtils>().multiDigitAllowed = true;
                                                    child.GetComponent<TexDrawUtils>().numberOfDigit = 0;
                                                }
                                                if (i < 4)
                                                    child.transform.SetSiblingIndex((i * currentNoofColumns) - 1);
                                                else
                                                    child.transform.SetSiblingIndex((i * currentNoofColumns) - 2);
                                                break;
                                            case modeLCM:
                                                //Debug.Log("LCM Mode:");
                                                child.transform.SetSiblingIndex((i - 1) * currentNoofColumns);
                                                break;
                                            case modeHCF:
                                                //Debug.Log("HCF Mode:");
                                                child.transform.SetSiblingIndex((i - 1) * currentNoofColumns);
                                                break;
                                            default:
                                                //Debug.Log("Default Mode:");
                                                child.transform.SetSiblingIndex((mVariableGridLayout.constraintCount - 1));
                                                break;

                                        }
                                        //}
                                    }
                                    currentMaxSpaceInRow = PrevRowMaxSpace;
                                }
                            }
                            else
                            {
                                PrevRowMaxSpace = currentMaxSpaceInRow;
                            }
                            // finalizing the rows 
                            currentRow.GetComponent<RowController>().noofrows = noOfRowsinStep;
                            //Debug.Log("noOfRowsinStep: " + noOfRowsinStep + " noofrows: " + currentRow.GetComponent<RowController>().noofrows);
                            currentMaxSpaceInRow = 0;
                            currentrowPrevLineSpace = 0;
                            rowNumber++;
                            currentNoofColumns = 0;
                            prevNoofColumns = 1;
                            noOfRowsinStep = 0;
                            answerPressedCounter = 0;
                            nexttextback = false;
                            texValue = defaultvalue;
                            intTestNo = 0;
                            makealltexUnclickable(currentRow);
                            currentRow = (GameObject)Instantiate(Row, transform);
                            currentRow.GetComponent<RowController>().hasRoughwork = false;
                            currentRow.GetComponent<RowController>().noofrows = 0;



                            /*
                            foreach (JSONNode solndata in responce)
                            {
                                Debug.Log("solndata: " + solndata.ToString());
                            }*/

                            printStepNumber();
                            getNewCurrentWorkingTex();
                            StartBlinkingCursor();
                        }
                    }
                }
                else
                {

                    if (islastStep)
                    {
                        //ScrollView_Debugging.Instance.AddToList("\n" + "Error in result " + "\n" + "\n" + "Does not know how to express result" + "\n"
                      //+ "Give check result prompt until user gets the answer" + "\n" + "..............." + "\n");
                    }

                    else
                    {
                        //ScrollView_Debugging.Instance.AddToList("\n" + "Error in cross product" + "\n" + "\n" + "Does not know multiplication" + "\n"
                //+ "Scaffold with a hint to check cross multiplication and a flashcard on multiplication. Then make user solve a multiplication problem. Then make user solve the original problem.  " + "\n" + "..............." + "\n");
                    }
                    nextReleventstep = lastreleventStep;
                    errorNo++;
                    //PlayerPrefs.SetString("status", "1");
                    if (onLogMessage != null)
                        onLogMessage(errorlog);
                    studentJson.studJson.Add(stuJasonSoln);
                    string tlcd_diff_level_json = UtilityREST.tlcd_diff_level_json;//PlayerPrefs.GetString("tlcd_diff_level_json");
                                                                                   //Debug.Log("Main tlcd_diff_level_json: " + tlcd_diff_level_json);
                    //if (UtilityArtifacts.isobj16)
                    //{
                    //    prerequiste = pre[1 + ""].Value;
                    //}
                    //else
                    //    prerequiste = pre[stepNo + ""].Value;

                    //UtilityREST.prerequiste = prerequiste;
                    UtilityREST.prerequiste_status = "1";
                    if (!UtilityREST.isprerequisit)
                        UtilityREST.session_tlcd_id = "";
                    /*
                    PlayerPrefs.SetString("prerequiste", prerequiste);

                    PlayerPrefs.SetString("prerequiste_status", "1");*/
                    //Debug.Log("error msg:" + errorMsg);
                    clearingrow();
                    /*
                    GameObject temp = currentRow.transform.GetChild(indexofWorkingArea).gameObject;
                    foreach (Transform child in temp.transform)
                    {
                        GameObject.Destroy(child.gameObject);
                    }

                    currentMaxSpaceInRow = 0;
                    currentrowPrevLineSpace = 0;
                    //rowNumber++;
                    currentNoofColumns = 0;
                    prevNoofColumns = 1;
                    noOfRowsinStep = 0;
                    answerPressedCounter = 0;
                    nexttextback = false;
                    texValue = defaultvalue;
                    intTestNo = 0;
                    initilizeStep();
                        //Debug.Log("next step");
                    currentRow.GetComponent<RowController>().hasRoughwork = false;
                    currentRow.GetComponent<RowController>().noofrows = 0;
                    //Debug.Log("PrevRowMaxSpace: " + PrevRowMaxSpace);
                    getNewCurrentWorkingTex();*/
                    //Functionmode = defaultmode;
                    //checkPrequisit();

                    StartBlinkingCursor();

                    //Debug.Log("prerequiste: " + UtilityREST.prerequiste);
                    Debug.LogError("prerequiste: " + UtilityREST.prerequiste );
                    if (String.Compare(UtilityREST.prerequiste, "") != 0 && errorNo > 2 && errorCountTraver > 2)
                    {
                        if(UtilityArtifacts.isobj16)
                            UtilityArtifacts.isobj16 = false;
                        isMultipication = true;
                        errorMsg = "We need to do a pre requisit";
                        //Debug.LogError("prerequiste: " + UtilityREST.prerequiste + "errorMsg: " + errorMsg);
                        UtilityREST.status = "1";
                        UtilityREST.isprerequisit = true;
                        //UtilityREST.studJson = studentJson.ToString();
                        //if (OnShowMessage != null)
                        //    OnShowMessage(errorMsg);
                        //Debug.Log(" error");
                        string studjson = JsonUtility.ToJson(studentJson);
                        //Debug.Log("studjson: " + studjson + "studentJson: " + studentJson.ToString());
                        UtilityREST.studJson = studjson;
                        if (OndownloadNextQuestion != null)
                            OndownloadNextQuestion();
                    }
                    else
                    {
                        Debug.Log("no prerequiste "+ errorNo);
                        if (Functionmode == modeAddition)
                        {
                            clearCanvas();
                        }
                        if (OnShowMessage != null)
                        {
                            OnShowMessage(errorMsg);

                      //      //ScrollView_Debugging.Instance.AddToList("\n" + "Error in cross products" + "\n" + "\n" + "Does not know multiplication" + "\n"
                      //+ "Scaffold with a hint to check cross product and a flashcard on multiplication. Then make user solve a multiplication problem. Then make user solve the original problem.  " + "\n" + "..............." + "\n");
                        }
                    }

                }
            }
            else
            {
                if (OnShowMessage != null)
                    OnShowMessage("please enter some values");

                if (Functionmode == modeAddition)
                {
                    clearCanvas();
                }
                else
                    clearingrow();

                StartBlinkingCursor();
            }
        }
    }

    // for basic operation delete all value index
    void removeAllvalueIndex()
    {
        foreach (TexDrawUtils tex in allTexDrawInaRow)
        {
            tex.valueIndex.Clear();
        }
    }

    void makealltexUnclickable(GameObject currentRow)
    {
        foreach (TexDrawUtils tex in allTexDrawInaRow)
        {
            tex.notClickable = true;
        }
    }

    // ON sUBMIT BUTTON clicked
    void CheckCurrentCanvas()
    {
        // need to check if all steps are done or not
        StopblinkingCursor();
        if (valueentered > 0)
        {
            if (!valueEnteredinList)
            {
                /* if (operatedClicked)
                 {
                     operators.Insert(0, "op-oround");
                     operators.Add("op-cround");
                     display += 2;

                 }/*
                 values.Add(value);
                 value = 0;
                 currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex.Add(values.Count - 1);*/
                if (currentWorkingBlock.GetComponent<TexDrawUtils>().multiDigitAllowed)
                {
                    // as single digit has single value
                    currentWorkingBlock.GetComponent<TexDrawUtils>().digitvalue[currentWorkingBlock.GetComponent<TexDrawUtils>().digitvalue.Count - 1] = value;
                }
                if (currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex.Count == 0)
                {
                    values.Add(value);
                    value = 0;
                    currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex.Add(values.Count - 1);
                }
                else
                {
                    int index = currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex[currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex.Count - 1];
                    if (operSighnAdded && currentWorkingBlock.GetComponent<TexDrawUtils>().multiDigitAllowed)
                    {
                        values.Insert(index + 1, value);
                        chagebelowtextUtil(currentWorkingBlock.GetComponent<TexDrawUtils>().serialNumber);
                    }
                    else
                    {
                        //Debug.Log("Valuesadded at index");
                        values[index] = value;
                    }

                    //value = 0;
                    currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex.Add(index);
                }
                valueEnteredinList = true;
            }
            getStudentJson();
            if (!errorinstepJason())//(!errorinStep())
            {
                if (canvasManager.isTutorial)
                {
                    canvasManager.OnTutorialOver();
                    return;
                }
                if (islastStep)
                {
                    if (onLogMessage != null)
                        onLogMessage("Next Question");

                    studentJson.studJson.Add(stuJasonSoln);
                    UtilityREST.status = "2";
                    if (UtilityREST.isprerequisit)
                    {
                        // assuming all is right as no check
                        UtilityREST.status = "2";
                        UtilityREST.prerequiste = "";
                        UtilityREST.prerequiste_status = "";
                        UtilityREST.tlcd_id = UtilityREST.session_tlcdpr_id;
                        
                    }
                    else
                    {
                        UtilityREST.status = "2";
                        UtilityREST.prerequiste = "";
                        UtilityREST.prerequiste_status = "";
                        UtilityREST.session_tlcd_id = "";
                        UtilityREST.session_tlcdpr_id = "";
                    }
                    Debug.LogError("sessionTlcd id" + UtilityREST.session_tlcd_id);
                    //Debug.Log("not error");
                    string studJson = JsonUtility.ToJson(studentJson);
                    //Debug.Log("studJson: " + studJson + studentJson.ToString());
                    UtilityREST.studJson = studJson;
                    string msg = "Well Done";
                    Debug.Log("Well Done isMultipication: " + UtilityArtifacts.isMultipication + "Multiplecount " + Multiplecount);

                    if (UtilityArtifacts.isMultipication)
                    {
                        Multiplecount++;
                    }
                    if(Multiplecount > 2)
                    {
                        msg = "Now that we are clear on this, let us go back to continue learning equivalent fraction";
                        UtilityArtifacts.isMultipication = false;
                        Multiplecount = 0;
                        if (OnShowFullScreenMsg != null)
                            OnShowFullScreenMsg();
                        if (onLogMessage != null)
                            onLogMessage("Traversing the user back to ‘Forming Equivalent Fractions by Multiplication’’Practice Canvas’");
                    }
                    Debug.Log("msg: " + msg);
                    if (OnShowMessage != null)
                        OnShowMessage(msg);
                    if (OndownloadNextQuestion != null)
                        OndownloadNextQuestion();
                }
                else
                {
                    string msg = "the Answer is not yet complete";
                    if (OnShowMessage != null)
                        OnShowMessage(msg);
                }
                StartBlinkingCursor();
            }
            else
            {
                errorNo++;
                //PlayerPrefs.SetString("status", "1");
                studentJson.studJson.Add(stuJasonSoln);
                string tlcd_diff_level_json = UtilityREST.tlcd_diff_level_json;//PlayerPrefs.GetString("tlcd_diff_level_json");
                                                                               //Debug.Log("Main tlcd_diff_level_json: " + tlcd_diff_level_json);
                JSONNode diff_level_json = JSON.Parse(tlcd_diff_level_json);
                JSONNode Steps_PreRequiste = diff_level_json["Steps_PreRequiste"];
                JSONArray step_Req = (JSONArray)Steps_PreRequiste;
                //Debug.Log("step_Req: " + step_Req.ToString());
                //Debug.Log(" Steps_PreRequiste:" + Steps_PreRequiste.ToString());
                JSONNode pre = step_Req[0];
                String prerequiste = "";
                if (UtilityArtifacts.isobj16)
                {
                    prerequiste = pre[1 + ""].Value;
                }
                else
                    prerequiste = pre[stepNo + ""].Value;
                //Debug.Log("prerequiste: " + prerequiste + " stepNo:" + stepNo);
                UtilityREST.status = "1";
                UtilityREST.prerequiste = prerequiste;
                UtilityREST.prerequiste_status = "1";
                if (!UtilityREST.isprerequisit)
                    UtilityREST.session_tlcd_id = "";
                /*
                PlayerPrefs.SetString("prerequiste", prerequiste);

                PlayerPrefs.SetString("prerequiste_status", "1");*/
                //Debug.Log("error msg:" + errorMsg);
                GameObject temp = currentRow.transform.GetChild(indexofWorkingArea).gameObject;
                foreach (Transform child in temp.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }

                currentMaxSpaceInRow = 0;
                currentrowPrevLineSpace = 0;
                //rowNumber++;
                currentNoofColumns = 0;
                prevNoofColumns = 1;
                noOfRowsinStep = 0;
                answerPressedCounter = 0;
                nexttextback = false;
                texValue = defaultvalue;
                intTestNo = 0;
                //Debug.Log("before initilizeStep current canvas");
                initilizeStep();
                currentRow.GetComponent<RowController>().hasRoughwork = false;
                currentRow.GetComponent<RowController>().noofrows = 0;
                //Debug.Log("PrevRowMaxSpace: " + PrevRowMaxSpace);.
                //Debug.Log("before getNewCurrentWorkingTex");
                getNewCurrentWorkingTex();

                if (String.Compare(prerequiste, "") != 0 && errorNo > 2 && errorCountTraver > 2)
                {
                    errorMsg = "We need to do a pre requisit";
                    UtilityREST.status = "1";
                    if (OnShowMessage != null)
                        OnShowMessage(errorMsg);
                    //Debug.Log(" error");
                    
                    string studjson = JsonUtility.ToJson(studentJson);
                    Debug.Log("studjson: " + studjson);
                    UtilityREST.studJson = studjson;
                    if (OndownloadNextQuestion != null)
                        OndownloadNextQuestion();
                    UtilityREST.isprerequisit = true;
                }
                else
                {
                    Debug.Log("no prerequiste errorNo"+ errorNo);
                    if (Functionmode == modeAddition)
                    {
                        clearCanvas();
                    }
                    if (OnShowMessage != null)
                        OnShowMessage(errorMsg);
                }
                StartBlinkingCursor();
                //checkPrequisit();
            }
        }
        else
        {
            if (OnShowMessage != null)
                OnShowMessage("please enter some values");

            if (Functionmode == modeAddition)
            {
                clearCanvas();
            }
            else
                clearingrow();

            StartBlinkingCursor();
        }
    }

    bool isMultipication = false;
    int Multiplecount = 0;

    void showforNextUnknownQuestion()
    {

        //PlayerPrefs.SetString("status", "1");
        string tlcd_diff_level_json = UtilityREST.tlcd_diff_level_json;//PlayerPrefs.GetString("tlcd_diff_level_json");
        //Debug.Log("Main tlcd_diff_level_json: " + tlcd_diff_level_json);
        JSONNode diff_level_json = JSON.Parse(tlcd_diff_level_json);
        JSONNode Steps_PreRequiste = diff_level_json["Steps_PreRequiste"];
        JSONArray step_Req = (JSONArray)Steps_PreRequiste;
        //Debug.Log("step_Req: " + step_Req.ToString());
        //Debug.Log(" Steps_PreRequiste:" + Steps_PreRequiste.ToString());
        JSONNode pre = step_Req[0];
        String prerequiste = "";
        int step = 0;
        while (String.Compare(prerequiste, "") == 0)
        {
            if (UtilityArtifacts.isobj16)
            {
                prerequiste = pre[1 + ""].Value;
            }
            else
                //prerequiste = pre[stepNo + ""].Value;
            prerequiste = pre[step + ""].Value;
            step++;
        }
        
        //Debug.Log("prerequiste: " + prerequiste + " stepNo:" + stepNo);
        UtilityREST.status = "1";
        UtilityREST.prerequiste = prerequiste;
        UtilityREST.prerequiste_status = "1";
        UtilityREST.session_tlcd_id = "";
        /*
        PlayerPrefs.SetString("prerequiste", prerequiste);

        PlayerPrefs.SetString("prerequiste_status", "1");*/
        errorMsg = "We need to do a pre requisit";
        //Debug.Log("error msg:" + errorMsg);

        if (OnShowMessage != null)
            OnShowMessage(errorMsg);
        //Debug.Log(" error");
        /*
        if (OndownloadNextQuestion != null)
            OndownloadNextQuestion();*/
    }

    void initilizeStep()
    {
        display = 1;
        optcount = 0;
        valCount = 0;
        operators.Clear();
        //operators.Add("op-oround");
        values.Clear();
        allTexDrawInaRow.Clear();
        valueEnteredinList = false;
        if (Functionmode != modeAddition)
            valueentered = 0;
        digitentered = false;
        valuefromList = false;
        canAddFraction = true;
        nextValue = true;
        fromGeneral = true;
        operSighnAdded = false;
        hasExpression = false;
        changeValueOnBackspace = true;
        step_attribute = new Attributes_step();
        //Debug.Log("initilize");
        valueIndex = 0;
        UtilityREST.status = "2";
        placeValue = 0;
        valueSwapped = false;
        islastStep = false;
        operatedClicked = false;
        openingBracketAdded = false;
        prevExpvalueCounter = 0;
        OpeningBracketcounter = 0;
        hasBracked = false;
        //stuJasonSoln = new StudentSolution();
        fractionBracketAdjusted = false;
        fractionCounterAfterBracketAdjust = 0;
        fractionCounter = 0;
        //Functionmode = defaultmode;


    }

    void Answer()
    {
        if (isActive)
        {
            StopblinkingCursor();

            switch (Functionmode)
            {
                case modeLCM:

                    answerPressedCounter++;
                    if (answerPressedCounter == 1)
                    {
                        Functionmode = defaultmode;
                        if (prevNoofColumns > currentNoofColumns)
                        {
                            int nuberofturns = (prevNoofColumns - currentNoofColumns);
                            for (int i = 0; i < nuberofturns; i++)
                            {
                                //Debug.Log("i = " + i);
                                getNewCurrentWorkingTex();
                                //Debug.Log("i = " + i);
                            }

                        }
                        NextLine();
                        for (int i = 0; i < prevNoofColumns; i++)
                        {
                            texValue = "";
                            getNewCurrentWorkingTex();
                        }
                        texValue = defaultvalue;

                    }
                    NextLine();
                    getNewCurrentWorkingTex();
                    currentWorkingTexDraw.text = currentWorkingTexDraw.text.Insert(0, "l");
                    currentMaxSpaceInRow++;
                    getNewCurrentWorkingTex();
                    currentWorkingTexDraw.text = currentWorkingTexDraw.text.Insert(0, "c");
                    currentMaxSpaceInRow++;
                    getNewCurrentWorkingTex();
                    currentWorkingTexDraw.text = currentWorkingTexDraw.text.Insert(0, "m");
                    currentMaxSpaceInRow++;
                    getNewCurrentWorkingTex();
                    currentWorkingTexDraw.text = currentWorkingTexDraw.text.Insert(0, "=");
                    currentMaxSpaceInRow++;
                    getNewCurrentWorkingTex();
                    Functionmode = modeLCM;
                    break;
                case modeHCF:
                    //Debug.Log("answer HCF");
                    answerPressedCounter++;
                    if (answerPressedCounter == 1)
                    {
                        Functionmode = defaultmode;
                        if (prevNoofColumns > currentNoofColumns)
                        {
                            int nuberofturns = (prevNoofColumns - currentNoofColumns);
                            for (int i = 0; i < nuberofturns; i++)
                            {
                                //Debug.Log("i = " + i);
                                getNewCurrentWorkingTex();
                                //Debug.Log("i = " + i);
                            }

                        }
                        NextLine();
                        for (int i = 0; i < prevNoofColumns; i++)
                        {
                            texValue = "";
                            getNewCurrentWorkingTex();
                        }
                        texValue = defaultvalue;

                    }
                    NextLine();
                    getNewCurrentWorkingTex();
                    currentWorkingTexDraw.text = currentWorkingTexDraw.text.Insert(0, "h");
                    currentMaxSpaceInRow++;
                    getNewCurrentWorkingTex();
                    currentWorkingTexDraw.text = currentWorkingTexDraw.text.Insert(0, "c");
                    currentMaxSpaceInRow++;
                    getNewCurrentWorkingTex();
                    currentWorkingTexDraw.text = currentWorkingTexDraw.text.Insert(0, "f");
                    currentMaxSpaceInRow++;
                    getNewCurrentWorkingTex();
                    currentWorkingTexDraw.text = currentWorkingTexDraw.text.Insert(0, "=");
                    currentMaxSpaceInRow++;
                    getNewCurrentWorkingTex();
                    Functionmode = modeHCF;
                    break;
                case modeAddition:
                    isanswer = true;
                    answerPressedCounter++;
                    if (answerPressedCounter == 1)
                    {
                        NextLine();
                        for (int i = 0; i < prevNoofColumns; i++)
                        {
                            texValue = "-";
                            getNewCurrentWorkingTex();
                            // making the dashed line not clickable
                            currentWorkingBlock.GetComponent<TexDrawUtils>().notClickable = true;
                        }
                        texValue = defaultvalue;
                        NextLine();
                        getNewCurrentWorkingTex();
                        currentWorkingTexDraw.text = currentWorkingTexDraw.text.Insert(0, "=");
                        currentMaxSpaceInRow++;
                        getNewCurrentWorkingTex();

                        nexttextback = true;
                        getNewCurrentWorkingTex();
                    }
                    break;
                default:
                    break;

            }
            StartBlinkingCursor();
        }

    }

    void AddMixedFraction()
    {
        if (isActive)
        {
            if (!(string.Compare(instanciateOnThisBlock.name, currentWorkingBlock.name) == 0))
            {
                currentWorkingBlock.GetComponent<TexDrawUtils>().digitvalue[currentWorkingBlock.GetComponent<TexDrawUtils>().digitvalue.Count - 1] = value;
                currentWorkingBlock = instanciateOnThisBlock;
            }

            //if (canAddFraction)
            {
                StopblinkingCursor();
                if (OnprintfractionAction != null)
                    OnprintfractionAction();
                textBoxTypeMode = modeMixedFraction;
                getNewCurrentWorkingTex();
                step_attribute.MF++;
                step_attribute.NUM = 0;
                step_attribute.TOTATT = 0;
                StartBlinkingCursor();
                canAddFraction = false;
                //valueentered =0;
                fractionCounter++;
                if (operSighnAdded)
                    operSighnAdded = false;
            }
            // display;
        }

    }

    int fractionCounter = 0;
    void AddFraction()
    {
        if (isActive)
        {
            if (!(string.Compare(instanciateOnThisBlock.name, currentWorkingBlock.name) == 0))
            {
                currentWorkingBlock.GetComponent<TexDrawUtils>().digitvalue[currentWorkingBlock.GetComponent<TexDrawUtils>().digitvalue.Count - 1] = value;
                currentWorkingBlock = instanciateOnThisBlock;
            }

            // if (canAddFraction)
            {

                StopblinkingCursor();
                if (OnprintfractionAction != null)
                    OnprintfractionAction();
                // adjust brackets if necessary
                adjustBracketsforFraction();
                textBoxTypeMode = modeFraction;
                getNewCurrentWorkingTex();
                StartBlinkingCursor();
                display += 2;
                step_attribute.NUM += 1;
                step_attribute.TOTATT++;
                //Debug.Log("Add frac TOTATT : " + step_attribute.TOTATT);
                step_attribute.F++;
                //operatedClicked = true;
                //valueentered = 0;
                canAddFraction = false;
                fractionCounter++;
                if (operSighnAdded)
                    operSighnAdded = false;
            }
        }

    }

    void AddTextValue(string _value)
    {
        
        GameObject prevTex = currentWorkingBlock;
        if (isActive)
        {
            if (!(string.Compare(instanciateOnThisBlock.name, currentWorkingBlock.name) == 0))
            {
                Debug.Log("Count: " + currentWorkingBlock.GetComponent<TexDrawUtils>().digitvalue.Count + " value: " + value);
                currentWorkingBlock.GetComponent<TexDrawUtils>().digitvalue[currentWorkingBlock.GetComponent<TexDrawUtils>().digitvalue.Count - 1] = value;
                currentWorkingBlock = instanciateOnThisBlock;
            }


            StopblinkingCursor();
            //if (OnprintfractionAction != null)
            //    OnprintfractionAction();
            // adjust brackets if necessary
            //adjustBracketsforFraction();
            Debug.Log("AddTextValue: " + _value);
            valueentered++;
            textBoxTypeMode = modeTextValue;
            getNewCurrentWorkingTex();
            //adjustValuesForText();
            StartBlinkingCursor();
            display += 1;
            //step_attribute.NUM += 1;
            //step_attribute.TEXT++;
            //step_attribute.TOTATT++;
            //step_attribute.F++;
            //operatedClicked = true;
            //valueentered = 0;
            //canAddFraction = false;
            //fractionCounter++;

            if (operSighnAdded)
                operSighnAdded = false;
            TexDrawUtils tex = currentWorkingBlock.GetComponent<TexDrawUtils>();
            tex.texString = _value;
            tex.notClickable = true;
            currentWorkingTexDraw.text = _value;
            currentWorkingBlock = prevTex;
            currentWorkingTexDraw = currentWorkingBlock.GetComponent<TEXDraw>();

        }
    }

    void adjustValuesForText()
    {
        if (currentWorkingBlock.GetComponent<TexDrawUtils>().multiDigitAllowed && currentWorkingBlock.GetComponent<TexDrawUtils>().numberOfDigit > 0)
        {
        }
        else
            currentWorkingTexDraw.text = currentWorkingTexDraw.text.Remove(currentWorkingTexDraw.text.Length - 1);
        currentWorkingBlock.GetComponent<TexDrawUtils>().numberOfDigit++;

        //currentWorkingTexDraw.text = currentWorkingTexDraw.text.Insert(currentWorkingTexDraw.text.Length, _value);

        display += 2;
        step_attribute.TEXT++;
        step_attribute.TOTATT++;
        Debug.Log("Text TOTATT : " + step_attribute.TOTATT);
        //if (step_attribute.F > 0)
        //    step_attribute.F = 0;
        if (digitentered)
        {
            /*
            values.Add(value);
            currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex.Add(values.Count - 1);*/
            // if a digit isentered add value to the digitvalue and the exp to ecplist
            //Debug.Log("count current " + currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex.Count);
            if (currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex.Count == 0)
            {
                values.Add(value);
                value = 0;
                currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex.Add(values.Count - 1);
            }
            else
            {
                int index = currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex[currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex.Count - 1];
                // Debug.Log("index current " + index);
                if (currentWorkingBlock.GetComponent<TexDrawUtils>().multiDigitAllowed)
                {
                    // as single digit has single value
                    currentWorkingBlock.GetComponent<TexDrawUtils>().digitvalue[currentWorkingBlock.GetComponent<TexDrawUtils>().digitvalue.Count - 1] = value;
                    //currentWorkingBlock.GetComponent<TexDrawUtils>().digitvalue.Add(0);
                }


                if (operSighnAdded && currentWorkingBlock.GetComponent<TexDrawUtils>().multiDigitAllowed)
                {
                    values.Insert(index + 1, value);
                    currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex.Add(index + 1);
                    chagebelowtextUtil(currentWorkingBlock.GetComponent<TexDrawUtils>().serialNumber);

                }
                else
                    values[index] = value;
                //values[index] = value;
                value = 0;
                valuefromList = false;
                //currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex.Add(index);
            }
            digitentered = false;

        }
        if (currentWorkingBlock.GetComponent<TexDrawUtils>().multiDigitAllowed)
        {
            // as single digit has single value
            //currentWorkingBlock.GetComponent<TexDrawUtils>().digitvalue[currentWorkingBlock.GetComponent<TexDrawUtils>().digitvalue.Count - 1] = value;
            currentWorkingBlock.GetComponent<TexDrawUtils>().digitvalue.Add(0);
        }
        value = 0;

        if (!singleStep)
        {
            operatedClicked = true;
            operSighnAdded = true;
        }
        else
        {
            // it is from single step delete last tes util from all

        }
        if (currentWorkingBlock.GetComponent<TexDrawUtils>().multiDigitAllowed)
        {
            // currentWorkingTexDraw.text = currentWorkingTexDraw.text.Insert(currentWorkingTexDraw.text.Length, " ");
        }
        else
        {
            // Debug.Log("instanciateOnThisBlock.name: " + instanciateOnThisBlock.name + " :currentWorkingBlock.name: " + currentWorkingBlock.name);
            if (string.Compare(instanciateOnThisBlock.name, currentWorkingBlock.name) == 0)
            {
                TexDrawUtils prevTex;
                if (currentWorkingBlock.GetComponent<TexDrawUtils>().serialNumber > 0)
                {
                    prevTex = allTexDrawInaRow[currentWorkingBlock.GetComponent<TexDrawUtils>().serialNumber - 1];
                    if (String.Compare(prevTex.texType, UtilityREST.General) == 0)
                    {
                        fromGeneral = true;
                    }
                    else
                    {
                        fromGeneral = false;
                        //nextValue = false;
                    }
                }
                else
                {
                    fromGeneral = false;
                }

                // check if the prev value is mixed fraction or fraction then no need to add value in list 

                nextValue = true;
                placeValue = 0;
                // getNewCurrentWorkingTex();
                // Debug.Log("ExpressionClicked");

            }

        }
    }

    bool fractionBracketAdjusted = false;
    int fractionCounterAfterBracketAdjust;
    void adjustBracketsforFraction()
    {
        //Debug.Log(" adjust brackets for fraction" + operators.Count);
        if (operators.Count > 0)
        {
            // need to adjust brackets
            //Debug.Log("nneed to adjust brackets for fraction");
            // check if there is a digit before f and operator
            if (allTexDrawInaRow.Count > 2)
            {
                TexDrawUtils lastTex = allTexDrawInaRow[allTexDrawInaRow.Count - 3];
                if (String.Equals(lastTex.texType, UtilityREST.General))
                {
                    //Debug.Log("putin brackets" + lastTex.faceValue + lastTex.texType + lastTex.texPosition);
                    // adding opening brackets at the begining and before the +
                    operators.Insert(0, UtilityREST.var_openingBrackets);
                    operators.Insert(operators.Count - 1, UtilityREST.var_closingBrackets);
                    operators.Add(UtilityREST.var_openingBrackets);
                    operators.Add(UtilityREST.var_closingBrackets);
                    fractionBracketAdjusted = true;
                    display += 4;
                }
                else
                {
                    //Debug.Log("we dnt need brackets");
                }
            }

        }
    }

   

    void Delete()
    {
        if (isActive)
        {
            Debug.Log("valueentered delete: " + valueentered);
            StopblinkingCursor();

            GameObject temp = currentRow.transform.GetChild(indexofWorkingArea).gameObject;
            GameObject nextObj;

            Debug.Log("before currentNoofColumns: " + currentNoofColumns + "value: " + value);
            valueEnteredinList = false;
            if (isanswer)
            {
                int pow = placeValue - 1;
                if (value != 0)
                    value = value % (int)(Math.Pow(10, pow));
                nextObj = temp.transform.GetChild(currentWorkingBlock.transform.GetSiblingIndex() - 1).gameObject;
                //valueentered--;
            }
            else
            {
                // Debug.Log("changeValueOnBackspace: " + changeValueOnBackspace);
                if (changeValueOnBackspace)
                {
                    value = value / 10;
                    Debug.Log("cannot change value");
                    //valueentered--;
                }

                if (!currentWorkingBlock.GetComponent<TexDrawUtils>().multiDigitAllowed && currentNoofColumns > 1)
                {
                    if (currentWorkingBlock.GetComponent<TexDrawUtils>().serialNumber > 0)
                        nextObj = allTexDrawInaRow[currentWorkingBlock.GetComponent<TexDrawUtils>().serialNumber - 1].gameObject;
                    else
                        nextObj = null;
                }
                else
                    nextObj = null;
            }

            //Debug.Log("value: " + value);
            if (string.Compare(instanciateOnThisBlock.name, currentWorkingBlock.name) == 0)
            {
                // when the cursor is in the instanciating block
                Debug.Log("currentNoofColumns: " + currentNoofColumns);

                if (currentNoofColumns > 1)
                {
                    if (String.Compare(nextObj.GetComponent<TexDrawUtils>().texType, UtilityREST.General) == 0)
                    {
                        //allTexDrawInaRow.RemoveAt( currentWorkingBlock.GetComponent<TexDrawUtils>().serialNumber);


                        if (isanswer)
                        {
                            //if(currentWorkingBlock.GetComponent<TexDrawUtils>().placeValue)
                            //Debug.Log("place value: " + currentWorkingBlock.GetComponent<TexDrawUtils>().placeValue);
                            Destroy(currentWorkingBlock);
                            allTexDrawInaRow.RemoveAt(currentWorkingBlock.GetComponent<TexDrawUtils>().serialNumber);
                            currentWorkingBlock = temp.transform.GetChild(currentWorkingBlock.transform.GetSiblingIndex() - 1).gameObject;
                            currentWorkingTexDraw = currentWorkingBlock.GetComponent<TEXDraw>();
                            valueentered--;
                        }
                        else
                        {
                            Destroy(currentWorkingBlock);
                            allTexDrawInaRow.RemoveAt(currentWorkingBlock.GetComponent<TexDrawUtils>().serialNumber);
                            currentWorkingBlock = temp.transform.GetChild(currentWorkingBlock.transform.GetSiblingIndex() + 1).gameObject;
                            currentWorkingTexDraw = currentWorkingBlock.GetComponent<TEXDraw>();
                            valueentered--;
                        }

                        instanciateOnThisBlock = currentWorkingBlock;
                        intTestNo--;
                        currentNoofColumns--;
                        placeValue--;

                        if (currentWorkingBlock.GetComponent<TexDrawUtils>().numberOfDigit > 0)
                        {
                            if (currentWorkingBlock.GetComponent<TexDrawUtils>().multiDigitAllowed)
                            {
                                currentWorkingTexDraw.text = currentWorkingTexDraw.text.Remove(currentWorkingTexDraw.text.Length - 2);
                                //currentWorkingTexDraw.text = currentWorkingTexDraw.text.Remove(0);
                            }

                            else
                            {
                                //Debug.Log("count: " + currentWorkingBlock.GetComponent<TexDrawUtils>().expIndex.Count);
                                int length = 1;
                                if (currentWorkingBlock.GetComponent<TexDrawUtils>().expIndex.Count > 0)
                                {
                                    string oper = "";
                                    foreach (int i in currentWorkingBlock.GetComponent<TexDrawUtils>().expIndex)
                                    {

                                        oper = operators[i];
                                    }
                                    length = getlengthofOper(oper);
                                }
                                //Debug.Log("changeValueOnBackspace: " + changeValueOnBackspace);
                                currentWorkingTexDraw.text = currentWorkingTexDraw.text.Remove(currentWorkingTexDraw.text.Length - length);
                            }


                            currentWorkingBlock.GetComponent<TexDrawUtils>().numberOfDigit--;
                            currentWorkingBlock.GetComponent<TexDrawUtils>().lastValueNoOfDigits--;
                            currentWorkingTexDraw.text = currentWorkingTexDraw.text.Insert(currentWorkingTexDraw.text.Length, " ");

                        }
                        if (currentWorkingBlock.GetComponent<TexDrawUtils>().expIndex.Count > 0)
                        {
                            Debug.Log("has Operator");
                            if (Functionmode == modeAddition)
                            {
                                // for addition need to remove the entire row
                                foreach (int i in currentWorkingBlock.GetComponent<TexDrawUtils>().expIndex)
                                {
                                    operators.RemoveAt(i);
                                }
                                currentWorkingBlock.GetComponent<TexDrawUtils>().expIndex.Clear();
                                display -= 2;
                                foreach (int i in currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex)
                                {
                                    // need to work
                                    Debug.Log("i: " + i);
                                    if (values.Count > i)
                                        values.RemoveAt(i + 1);
                                    int prevSerial = currentWorkingBlock.GetComponent<TexDrawUtils>().serialNumber - 1;
                                    allTexDrawInaRow.RemoveAt(currentWorkingBlock.GetComponent<TexDrawUtils>().serialNumber);
                                    //check if the current block is the last block in the parent if not delete all blocks below it.
                                    if ((temp.transform.childCount - 1) > currentWorkingBlock.transform.GetSiblingIndex())
                                    {
                                        //List<GameObject> obj = new List<GameObject>();
                                        GameObject obj;
                                        int extar = (temp.transform.childCount - 1) - currentWorkingBlock.transform.GetSiblingIndex();
                                        for (int j = 1; j <= extar; j++)
                                        {
                                            obj = temp.transform.GetChild(currentWorkingBlock.transform.GetSiblingIndex() + j).gameObject;
                                            Debug.Log("obj : " + obj.name + " temp.transform.childCount: " + temp.transform.childCount + " currentWorkingBlock.transform.GetSiblingIndex(): " + currentWorkingBlock.transform.GetSiblingIndex());
                                            Destroy(obj);
                                        }




                                    }
                                    Destroy(currentWorkingBlock);
                                    //allTexDrawInaRow.RemoveAt(currentWorkingBlock.GetComponent<TexDrawUtils>().serialNumber);
                                    TexDrawUtils prevutil = allTexDrawInaRow[prevSerial];

                                    currentWorkingBlock = prevutil.gameObject;
                                    currentWorkingBlock = temp.transform.GetChild(currentWorkingBlock.transform.GetSiblingIndex() - 1).gameObject;
                                    currentWorkingTexDraw = currentWorkingBlock.GetComponent<TEXDraw>();
                                    instanciateOnThisBlock = currentWorkingBlock;
                                    currentWorkingBlock.GetComponent<TexDrawUtils>().notClickable = false;
                                    allTexDrawInaRow.Add(currentWorkingBlock.GetComponent<TexDrawUtils>());
                                    currentNoofColumns = prevNoofColumns;
                                    valueIndex = prevutil.valueIndex[prevutil.valueIndex.Count - 1];
                                    value = values[valueIndex];
                                    noOfRowsinStep--;
                                    Debug.Log(value + " valueIndex: " + valueIndex);

                                }
                            }
                            else
                            {
                                foreach (int i in currentWorkingBlock.GetComponent<TexDrawUtils>().expIndex)
                                {
                                    //Debug.Log("has Operator");
                                    operators.RemoveAt(i);
                                    valueentered--;
                                }
                                currentWorkingBlock.GetComponent<TexDrawUtils>().expIndex.Clear();
                                display -= 2;
                                if (currentWorkingBlock.GetComponent<TexDrawUtils>().serialNumber > 0)
                                {
                                    TexDrawUtils nextUtil = allTexDrawInaRow[currentWorkingBlock.GetComponent<TexDrawUtils>().serialNumber - 1];
                                    // delete the current block


                                    if (String.Compare(nextUtil.texType, UtilityREST.General) == 0)
                                    {
                                        foreach (int i in currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex)
                                        {
                                            // need to work
                                            //Debug.Log("i: " + i);
                                            if (values.Count > i)
                                                values.RemoveAt(i + 1);
                                        }
                                    }

                                    valueIndex = nextUtil.valueIndex[nextUtil.valueIndex.Count - 1];
                                    value = values[valueIndex];


                                    placeValue = getNumberOfDigitsin(value);
                                }
                                else
                                {
                                    clearingrow();
                                }
                            }


                        }

                        if (currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex.Count > 0 && placeValue == 0)
                        {
                            foreach (int i in currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex)
                            {
                                // need to work
                                // Debug.Log("i: " + i);
                                if (values.Count > i)
                                    values[i] = 0;
                                //values.RemoveAt(i);
                            }
                        }
                    }
                    else if (String.Compare(nextObj.GetComponent<TexDrawUtils>().texType, UtilityREST.type_text) == 0)
                    {

                        Debug.Log("Deleting Text block");
                        //foreach (int i in currentWorkingBlock.GetComponent<TexDrawUtils>().expIndex)
                        //{
                        //    //Debug.Log("has Operator");
                        //    operators.RemoveAt(i);
                        //    valueentered--;
                        //}
                        currentWorkingBlock.GetComponent<TexDrawUtils>().expIndex.Clear();
                        currentWorkingBlock.GetComponent<TexDrawUtils>().serialNumber--;
                        allTexDrawInaRow.RemoveAt(currentWorkingBlock.GetComponent<TexDrawUtils>().serialNumber);
                        Destroy(nextObj);
                        //15-062020 updated while incroporating value intered for text
                        valueentered--;
                        //display -= 2;
                        //if (currentWorkingBlock.GetComponent<TexDrawUtils>().serialNumber > 0)
                        //{
                        //    TexDrawUtils nextUtil = allTexDrawInaRow[currentWorkingBlock.GetComponent<TexDrawUtils>().serialNumber - 1];
                        //    // delete the current block


                        //    //if (String.Compare(nextUtil.texType, UtilityREST.General) == 0)
                        //    //{
                        //    //    foreach (int i in currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex)
                        //    //    {
                        //    //        // need to work
                        //    //        //Debug.Log("i: " + i);
                        //    //        if (values.Count > i)
                        //    //            values.RemoveAt(i + 1);
                        //    //    }
                        //    //}

                        //    //valueIndex = nextUtil.valueIndex[nextUtil.valueIndex.Count - 1];
                        //    //value = values[valueIndex];


                        //    //placeValue = getNumberOfDigitsin(value);
                        //}
                        //else
                        //{
                        //    clearingrow();
                        //}
                    }
                    else
                    {
                        // selecting the denominator of fraction or mixed fraction

                        Debug.Log("Type: " + nextObj.GetComponent<TexDrawUtils>().texType + " pos: " + nextObj.GetComponent<TexDrawUtils>().texPosition);
                        currentWorkingBlock = nextObj;
                        currentWorkingTexDraw = currentWorkingBlock.GetComponent<TEXDraw>();
                        int index = currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex[currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex.Count - 1];
                        //Debug.Log("index " + index);
                        value = currentWorkingBlock.GetComponent<TexDrawUtils>().digitvalue[currentWorkingBlock.GetComponent<TexDrawUtils>().digitvalue.Count - 1];
                        Debug.Log("value " + value);
                        //value = values[index];
                        valuefromList = true;
                    }

                }
                else
                {
                    //Debug.Log("nothing to delete");
                    if (Functionmode == modeAddition)
                    {
                        clearCanvas();
                    }
                    else
                        clearingrow();
                    if (OnShowMessage != null)
                    {
                        OnShowMessage("nothing to delete");
                    }
                }

            }
            else
            {
                if (currentWorkingBlock.GetComponent<TexDrawUtils>().numberOfDigit > 0)
                {
                    if (currentWorkingBlock.GetComponent<TexDrawUtils>().multiDigitAllowed)
                    {
                        // if the current block allows multidigit. ie mf or f 
                        //currentWorkingTexDraw.text = currentWorkingTexDraw.text.Remove(currentWorkingTexDraw.text.Length - 2);
                        //Debug.Log("changeValueOnBackspace: " + changeValueOnBackspace + "has exp: "+ hasExpression);
                        currentWorkingTexDraw.text = currentWorkingTexDraw.text.Remove(currentWorkingTexDraw.text.Length - 1);
                        valueentered--;
                        currentWorkingBlock.GetComponent<TexDrawUtils>().numberOfDigit--;
                        if (currentWorkingBlock.GetComponent<TexDrawUtils>().numberOfDigit == 0)
                            currentWorkingTexDraw.text = currentWorkingTexDraw.text.Insert(currentWorkingTexDraw.text.Length, " ");

                        currentWorkingBlock.GetComponent<TexDrawUtils>().lastValueNoOfDigits--;

                        if (currentWorkingBlock.GetComponent<TexDrawUtils>().lastValueNoOfDigits == 0 && currentWorkingBlock.GetComponent<TexDrawUtils>().numberOfDigit > 0)
                        {
                            //Debug.Log("need to delete row from values: "+ currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex[currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex.Count - 1]+" oper: "+ currentWorkingBlock.GetComponent<TexDrawUtils>().expIndex[currentWorkingBlock.GetComponent<TexDrawUtils>().expIndex.Count - 1]);
                            //values.RemoveAt(currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex[currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex.Count - 1]);
                            // for multidigit allowable box delete the last value and the 2nd last operator in case of fractions
                            if (String.Equals(currentWorkingBlock.GetComponent<TexDrawUtils>().texType, UtilityREST.F))
                            {
                                // ned to rmove the operator.
                                currentWorkingBlock.GetComponent<TexDrawUtils>().digitvalue.RemoveAt(currentWorkingBlock.GetComponent<TexDrawUtils>().digitvalue.Count - 1);
                                currentWorkingBlock.GetComponent<TexDrawUtils>().expValue.RemoveAt(currentWorkingBlock.GetComponent<TexDrawUtils>().expValue.Count - 2);
                                valueentered--;
                            }
                            Debug.Log("operSighnAdded : " + operSighnAdded);
                            int curr_index = currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex[currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex.Count - 1];
                            if (!operSighnAdded)
                            {
                                // if the last valueis enterd in the values list  
                                values.RemoveAt(curr_index);
                                currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex.RemoveAt(currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex.Count - 1);
                                value = values[curr_index - 1];
                                decreaseAllTexutl(currentWorkingBlock.GetComponent<TexDrawUtils>().serialNumber);
                                Debug.Log("operSighnAdded value: " + value);
                            }
                            else
                            {
                                value = values[curr_index];
                            }

                            operSighnAdded = false;
                            /*     
                             values[] = 0;
                             (currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex.Count - 1);*/
                            if (currentWorkingBlock.GetComponent<TexDrawUtils>().expIndex.Count > 0)
                            {

                                string oper = operators[currentWorkingBlock.GetComponent<TexDrawUtils>().expIndex[currentWorkingBlock.GetComponent<TexDrawUtils>().expIndex.Count - 1]];
                                int length = getlengthofOper(oper);
                                if (length > 1)
                                    currentWorkingTexDraw.text = currentWorkingTexDraw.text.Remove(currentWorkingTexDraw.text.Length - (length - 1));
                                operators.RemoveAt(currentWorkingBlock.GetComponent<TexDrawUtils>().expIndex[currentWorkingBlock.GetComponent<TexDrawUtils>().expIndex.Count - 1]);
                                currentWorkingBlock.GetComponent<TexDrawUtils>().expIndex.RemoveAt(currentWorkingBlock.GetComponent<TexDrawUtils>().expIndex.Count - 1);
                                decreaseAllTexutlOper(currentWorkingBlock.GetComponent<TexDrawUtils>().serialNumber);
                                //currentWorkingBlock.GetComponent<TexDrawUtils>().expIndex[currentWorkingBlock.GetComponent<TexDrawUtils>().expIndex.Count - 1] = 0;
                                display -= 2;
                            }


                            Debug.Log("lastValueNoOfDigits: " + currentWorkingBlock.GetComponent<TexDrawUtils>().lastValueNoOfDigits + " numberOfDigit: " + currentWorkingBlock.GetComponent<TexDrawUtils>().numberOfDigit);
                            currentWorkingBlock.GetComponent<TexDrawUtils>().lastValueNoOfDigits = currentWorkingBlock.GetComponent<TexDrawUtils>().noOfDigitsLastValue[currentWorkingBlock.GetComponent<TexDrawUtils>().noOfDigitsLastValue.Count - 1];
                            currentWorkingBlock.GetComponent<TexDrawUtils>().noOfDigitsLastValue.RemoveAt(currentWorkingBlock.GetComponent<TexDrawUtils>().noOfDigitsLastValue.Count - 1);
                        }
                    }

                    else
                    {
                        if (canDelete)
                        {
                            Debug.Log(" canDelete count: " + currentWorkingBlock.GetComponent<TexDrawUtils>().expIndex.Count);
                            int length = 1;
                            if (currentWorkingBlock.GetComponent<TexDrawUtils>().expIndex.Count > 0)
                            {
                                string oper = "";
                                foreach (int i in currentWorkingBlock.GetComponent<TexDrawUtils>().expIndex)
                                {

                                    oper = operators[i];
                                }
                                length = getlengthofOper(oper);
                            }
                            currentWorkingTexDraw.text = currentWorkingTexDraw.text.Remove(currentWorkingTexDraw.text.Length - length);
                            valueentered--;
                            currentWorkingBlock.GetComponent<TexDrawUtils>().numberOfDigit--;
                            currentWorkingBlock.GetComponent<TexDrawUtils>().lastValueNoOfDigits--;
                            currentWorkingTexDraw.text = currentWorkingTexDraw.text.Insert(currentWorkingTexDraw.text.Length, " ");
                            // so that the next value doesnot change 
                            changeValueOnBackspace = false;
                            // need to check what type of value it is i.e. numeric or expression
                            if (currentWorkingBlock.GetComponent<TexDrawUtils>().expIndex.Count > 0)
                            {
                                hasExpression = true;
                                if (!selectedBypointer)
                                {
                                    values.RemoveAt(currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex[currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex.Count - 1] + 1);
                                    decreaseAllTexutl(currentWorkingBlock.GetComponent<TexDrawUtils>().serialNumber);
                                    display--;
                                }

                            }
                        }
                        else
                        {
                            if (OnShowMessage != null)
                            {
                                OnShowMessage("can not delete from here");
                            }
                        }

                    }
                }
                else
                {
                    //all the digits enterd in the box is deleted.
                    int serial_number = currentWorkingBlock.GetComponent<TexDrawUtils>().serialNumber;
                    if (serial_number > 0)
                    {

                        bool goToNext = true;
                        //Debug.Log(currentWorkingBlock.GetComponent<TexDrawUtils>().texType + currentWorkingBlock.GetComponent<TexDrawUtils>().texPosition);
                        if (String.Compare(currentWorkingBlock.GetComponent<TexDrawUtils>().texType, UtilityREST.F) == 0 && String.Compare(currentWorkingBlock.GetComponent<TexDrawUtils>().texPosition, UtilityREST.Numerator) == 0)
                        {
                            values[currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex[currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex.Count - 1]] = 0;
                            GameObject demObj = allTexDrawInaRow[serial_number + 1].gameObject;
                            Debug.Log("demObj.numberOfDigit" + demObj.GetComponent<TexDrawUtils>().numberOfDigit);
                            if (demObj.GetComponent<TexDrawUtils>().numberOfDigit == 0)
                            {
                                //Delete fraction and values 
                                // deleteing the fraction need to check for bracketed fraction 
                                int curr_index = demObj.GetComponent<TexDrawUtils>().valueIndex[demObj.GetComponent<TexDrawUtils>().valueIndex.Count - 1];
                                int tempserial = demObj.GetComponent<TexDrawUtils>().serialNumber;
                                values.RemoveAt(curr_index);
                                decreaseAllTexutl(tempserial);
                                allTexDrawInaRow.RemoveAt(tempserial);
                                decreaseAllSeialNoafter(tempserial);// check this for delete after submit option
                                curr_index = currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex[demObj.GetComponent<TexDrawUtils>().valueIndex.Count - 1];
                                tempserial = currentWorkingBlock.GetComponent<TexDrawUtils>().serialNumber;
                                values.RemoveAt(curr_index);
                                decreaseAllTexutl(tempserial);
                                allTexDrawInaRow.RemoveAt(tempserial);
                                decreaseAllSeialNoafter(tempserial);
                                goToNext = true;
                                currentNoofColumns--;
                                Debug.Log(" delete fraction lol " + currentNoofColumns);
                                GameObject parentObj = currentWorkingBlock.transform.parent.gameObject;
                                Destroy(parentObj);
                                display -= 2;
                                fractionCounter--;
                            }
                            else
                            {
                                goToNext = false;
                            }

                        }
                        else if (String.Compare(currentWorkingBlock.GetComponent<TexDrawUtils>().texType, UtilityREST.MF) == 0 && String.Compare(currentWorkingBlock.GetComponent<TexDrawUtils>().texPosition, UtilityREST.General) == 0)
                        {
                            Debug.Log("in MF General");
                            //when values are already present:
                            values[currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex[currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex.Count - 1]] = 0;
                            GameObject demObj = allTexDrawInaRow[serial_number + 2].gameObject;
                            GameObject nemObj = allTexDrawInaRow[serial_number + 1].gameObject;
                            //Debug.Log("demObj.numberOfDigit" + demObj.GetComponent<TexDrawUtils>().numberOfDigit + " Num: " + nemObj.GetComponent<TexDrawUtils>().numberOfDigit);
                            if (demObj.GetComponent<TexDrawUtils>().numberOfDigit == 0 && nemObj.GetComponent<TexDrawUtils>().numberOfDigit == 0)
                            {
                                //Delete fraction and values 
                                int curr_index = demObj.GetComponent<TexDrawUtils>().valueIndex[demObj.GetComponent<TexDrawUtils>().valueIndex.Count - 1];
                                int tempserial = demObj.GetComponent<TexDrawUtils>().serialNumber;
                                values.RemoveAt(curr_index);
                                decreaseAllTexutl(tempserial);
                                allTexDrawInaRow.RemoveAt(tempserial);
                                decreaseAllSeialNoafter(tempserial);
                                curr_index = nemObj.GetComponent<TexDrawUtils>().valueIndex[demObj.GetComponent<TexDrawUtils>().valueIndex.Count - 1];
                                tempserial = nemObj.GetComponent<TexDrawUtils>().serialNumber;
                                values.RemoveAt(curr_index);
                                decreaseAllTexutl(tempserial);
                                allTexDrawInaRow.RemoveAt(tempserial);
                                decreaseAllSeialNoafter(tempserial);
                                curr_index = currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex[demObj.GetComponent<TexDrawUtils>().valueIndex.Count - 1];
                                tempserial = currentWorkingBlock.GetComponent<TexDrawUtils>().serialNumber;
                                values.RemoveAt(curr_index);
                                decreaseAllTexutl(tempserial);
                                allTexDrawInaRow.RemoveAt(tempserial);
                                decreaseAllSeialNoafter(tempserial);
                                goToNext = true;
                                GameObject parentObj = nemObj.transform.parent.gameObject;
                                currentNoofColumns -= 2;
                                Destroy(parentObj);
                                Destroy(currentWorkingBlock);
                                fractionCounter--;
                                //display -= 1;
                            }
                            else
                            {
                                goToNext = false;
                            }
                        }
                        else if (String.Compare(currentWorkingBlock.GetComponent<TexDrawUtils>().texType, UtilityREST.General) == 0 && String.Compare(currentWorkingBlock.GetComponent<TexDrawUtils>().texPosition, UtilityREST.General) == 0)
                        {
                            // Debug.Log("Value is general need to do something");
                            // logic to change value of a number and 
                            //check if the texbox contained 
                            int curr_exp_index;//= currentWorkingBlock.GetComponent<TexDrawUtils>().expIndex[currentWorkingBlock.GetComponent<TexDrawUtils>().expIndex.Count - 1];
                            int tempserial = currentWorkingBlock.GetComponent<TexDrawUtils>().serialNumber;
                            Debug.Log("hasExpression : " + hasExpression);
                            if (hasExpression)
                            {
                                curr_exp_index = currentWorkingBlock.GetComponent<TexDrawUtils>().expIndex[currentWorkingBlock.GetComponent<TexDrawUtils>().expIndex.Count - 1];
                                //Debug.Log("currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex.Count : " + currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex.Count);
                                TexDrawUtils prevTex = allTexDrawInaRow[currentWorkingBlock.GetComponent<TexDrawUtils>().serialNumber - 1];
                                int tempcurr_index = prevTex.valueIndex[prevTex.valueIndex.Count - 1];
                                value = values[tempcurr_index];
                                //Debug.Log("value : " + value);
                                //Debug.Log("tempcurr_index : " + tempcurr_index);
                                //values.RemoveAt(tempcurr_index+1);
                                //decreaseAllTexutl(tempserial);
                                // if (!selectedBypointer)
                                {
                                    Debug.Log("not selected by pointer has expression");
                                    /*string oper = operators[curr_exp_index];
                                    int length = getlengthofOper(oper);
                                    if (length > 1)
                                        currentWorkingTexDraw.text = currentWorkingTexDraw.text.Remove(currentWorkingTexDraw.text.Length - (length - 1));*/
                                    operators.RemoveAt(curr_exp_index);
                                    decreaseAllTexutlOper(tempserial);
                                    display--;
                                }

                                hasExpression = false;
                            }
                            else
                            {
                                int curr_index = currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex[currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex.Count - 1];
                                Debug.Log("called");
                                values[curr_index] = value;
                                //decreaseAllTexutl(tempserial);
                                allTexDrawInaRow.RemoveAt(tempserial);
                                decreaseAllSeialNoafter(tempserial);

                                changeValueOnBackspace = true;
                            }

                            Destroy(currentWorkingBlock);
                            currentNoofColumns--;
                            Debug.Log(" delete fraction lol " + currentNoofColumns);

                        }


                        //{
                        if (goToNext)
                        {
                            // go to the next tex
                            TexDrawUtils tempTexUtil = allTexDrawInaRow[serial_number - 1];
                            if (!(String.Compare(currentWorkingBlock.GetComponent<TexDrawUtils>().texType, UtilityREST.General) == 0))
                            {
                                if (string.Compare(instanciateOnThisBlock.name, allTexDrawInaRow[serial_number].gameObject.name) == 0)
                                {
                                    tempTexUtil = allTexDrawInaRow[serial_number];
                                }
                            }

                            // the value is assige on changig from denominator to numerator
                            if (tempTexUtil.digitvalue.Count > 0)
                                value = tempTexUtil.digitvalue[tempTexUtil.digitvalue.Count - 1];
                            Debug.Log("fdhfh: " + tempTexUtil.texType + "tex: " + tempTexUtil.digitvalue.Count);//tempTexUtil.texPosition+tempTexUtil.digitvalue[tempTexUtil.digitvalue.Count-1]);    
                            currentWorkingBlock = tempTexUtil.gameObject;
                            currentWorkingTexDraw = currentWorkingBlock.GetComponent<TEXDraw>();
                        }
                        else
                        {
                            // stay in the next tex
                            if (OnShowMessage != null)
                            {
                                OnShowMessage("There are data in the block");
                            }

                        }
                        //}


                    }
                    else
                    {
                        Debug.Log("serial_number = 0" + serial_number);
                        //clearingrow();
                        if (allTexDrawInaRow.Count > 1)
                        {
                            if (String.Compare(currentWorkingBlock.GetComponent<TexDrawUtils>().texType, UtilityREST.F) == 0 && String.Compare(currentWorkingBlock.GetComponent<TexDrawUtils>().texPosition, UtilityREST.Numerator) == 0)
                            {

                                GameObject demObj = allTexDrawInaRow[serial_number + 1].gameObject;
                                Debug.Log("demObj.numberOfDigit" + demObj.GetComponent<TexDrawUtils>().numberOfDigit);
                                if (demObj.GetComponent<TexDrawUtils>().numberOfDigit == 0)
                                {
                                    //Delete fraction and values 
                                    int curr_index = demObj.GetComponent<TexDrawUtils>().valueIndex[demObj.GetComponent<TexDrawUtils>().valueIndex.Count - 1];
                                    int tempserial = demObj.GetComponent<TexDrawUtils>().serialNumber;
                                    values.RemoveAt(curr_index);
                                    decreaseAllTexutl(tempserial);
                                    allTexDrawInaRow.RemoveAt(tempserial);
                                    decreaseAllSeialNoafter(tempserial);
                                    curr_index = currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex[demObj.GetComponent<TexDrawUtils>().valueIndex.Count - 1];
                                    tempserial = currentWorkingBlock.GetComponent<TexDrawUtils>().serialNumber;
                                    values.RemoveAt(curr_index);
                                    decreaseAllTexutl(tempserial);
                                    allTexDrawInaRow.RemoveAt(tempserial);
                                    decreaseAllSeialNoafter(tempserial);
                                    currentNoofColumns--;
                                    Debug.Log(" delete fraction lol " + currentNoofColumns);
                                    GameObject parentObj = currentWorkingBlock.transform.parent.gameObject;
                                    Destroy(parentObj);
                                    display -= 2;
                                }

                            }
                            else if (String.Compare(currentWorkingBlock.GetComponent<TexDrawUtils>().texType, UtilityREST.MF) == 0 && String.Compare(currentWorkingBlock.GetComponent<TexDrawUtils>().texPosition, UtilityREST.General) == 0)
                            {
                                Debug.Log("in MF General allTexDrawInaRow.Count");
                                // when Mixed fraction is the first value
                                // delete Mixed fraction and make the canvas 
                                GameObject demObj = allTexDrawInaRow[serial_number + 2].gameObject;
                                GameObject nemObj = allTexDrawInaRow[serial_number + 1].gameObject;
                                Debug.Log("demObj.numberOfDigit" + demObj.GetComponent<TexDrawUtils>().numberOfDigit + " Num: " + nemObj.GetComponent<TexDrawUtils>().numberOfDigit);
                                if (demObj.GetComponent<TexDrawUtils>().numberOfDigit == 0 && nemObj.GetComponent<TexDrawUtils>().numberOfDigit == 0)
                                {
                                    //Delete fraction and values 
                                    int curr_index = demObj.GetComponent<TexDrawUtils>().valueIndex[demObj.GetComponent<TexDrawUtils>().valueIndex.Count - 1];
                                    int tempserial = demObj.GetComponent<TexDrawUtils>().serialNumber;
                                    values.RemoveAt(curr_index);
                                    decreaseAllTexutl(tempserial);
                                    allTexDrawInaRow.RemoveAt(tempserial);
                                    decreaseAllSeialNoafter(tempserial);
                                    curr_index = nemObj.GetComponent<TexDrawUtils>().valueIndex[nemObj.GetComponent<TexDrawUtils>().valueIndex.Count - 1];
                                    tempserial = nemObj.GetComponent<TexDrawUtils>().serialNumber;
                                    values.RemoveAt(curr_index);
                                    decreaseAllTexutl(tempserial);
                                    allTexDrawInaRow.RemoveAt(tempserial);
                                    decreaseAllSeialNoafter(tempserial);
                                    curr_index = currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex[currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex.Count - 1];
                                    tempserial = currentWorkingBlock.GetComponent<TexDrawUtils>().serialNumber;
                                    values.RemoveAt(curr_index);
                                    decreaseAllTexutl(tempserial);
                                    allTexDrawInaRow.RemoveAt(tempserial);
                                    decreaseAllSeialNoafter(tempserial);
                                    currentNoofColumns -= 2;
                                    Debug.Log(" delete fraction lol " + currentNoofColumns);
                                    GameObject parentObj = nemObj.transform.parent.gameObject;
                                    Destroy(parentObj);
                                    Destroy(currentWorkingBlock);
                                    //display -= 1;
                                }
                            }
                            else if (String.Compare(currentWorkingBlock.GetComponent<TexDrawUtils>().texType, UtilityREST.General) == 0 && String.Compare(currentWorkingBlock.GetComponent<TexDrawUtils>().texPosition, UtilityREST.General) == 0)
                            {
                                int curr_index = currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex[currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex.Count - 1];
                                int tempserial = currentWorkingBlock.GetComponent<TexDrawUtils>().serialNumber;
                                values.RemoveAt(curr_index);
                                decreaseAllTexutl(tempserial);
                                allTexDrawInaRow.RemoveAt(tempserial);
                                decreaseAllSeialNoafter(tempserial);
                                Destroy(currentWorkingBlock);
                                currentNoofColumns--;
                                Debug.Log(" delete fraction lol " + currentNoofColumns);
                                display -= 1;
                            }
                            //TexDrawUtils tempTexUtil = allTexDrawInaRow[serial_number];
                            currentWorkingBlock = instanciateOnThisBlock;//tempTexUtil.gameObject;
                            currentWorkingTexDraw = currentWorkingBlock.GetComponent<TEXDraw>();
                        }
                        else
                        {
                            if (Functionmode == modeAddition)
                            {
                                clearCanvas();
                            }
                            else
                                clearingrow();
                            if (OnShowMessage != null)
                            {
                                OnShowMessage("nothing to delete");
                            }
                        }

                    }
                }
                //Debug.Log("nothing to delete");


            }
            //Debug.Log("Display: " + display);
            selectedBypointer = false;
            StartBlinkingCursor();
        }

    }

    int getlengthofOper(string oper)
    {
        // getting the length of the operators
        int len = 1;
        string operatorValue = "";
        switch (oper)
        {
            case "op_addition":
                operatorValue = "+";
                break;
            case "op_subtraction":
                operatorValue = "-";
                break;
            case "op_multiply":
                operatorValue = "\\times";
                break;
            case "op_divide":
                operatorValue = "\\div";
                break;
            case "op_isequal":
                operatorValue = "=";
                break;
            case "op_isnotequal":
                operatorValue = "\\not[0-0]{=}";
                break;
            default:
                operatorValue = "+";
                break;

        }
        len = operatorValue.Length;
        return len;
    }

    int getNumberOfDigitsin(int numberValue)
    {
        return numberValue.ToString().Length;
    }

    void decreaseAllSeialNoafter(int serial)
    {
        for (int i = serial; i < allTexDrawInaRow.Count; i++)
        {
            TexDrawUtils temp = allTexDrawInaRow[i];
            temp.serialNumber--;
        }
    }

    GameObject getNextWorkingTex()
    {
        GameObject nextWorkingtext = null;
        int currentIndex = allTexDrawInaRow.IndexOf(currentWorkingBlock.GetComponent<TexDrawUtils>());
        Debug.Log("currentIndex: " + currentIndex);
        if(currentIndex< (allTexDrawInaRow.Count - 1))
        {
            nextWorkingtext = allTexDrawInaRow[currentIndex + 1].gameObject;
        }
        return nextWorkingtext;
    }

    void GoToNextWorkingBloack()
    {
        if (isActive)
        {
            GameObject obj = getNextWorkingTex();
            if(obj!=null)
                OnClickedOnATexDraw(obj);
        }
    }

    public void OnClickedOnATexDraw(GameObject obj)
    {
        if (isActive)
        {
            StopblinkingCursor();
            if (!obj.GetComponent<TexDrawUtils>().notClickable)
            {
                /*
                 * if (valueentered) test
                 {
                     values.Add(value);
                     value = 0;
                 }
                 */

                if (currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex.Count == 0)
                {
                    values.Add(value);
                    value = 0;
                    currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex.Add(values.Count - 1);
                    Debug.Log("added");
                }
                else
                {
                    int index = currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex[currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex.Count - 1];
                    if (currentWorkingBlock.GetComponent<TexDrawUtils>().multiDigitAllowed)
                    {
                        // as single digit has single value
                        //Debug.Log("value entered: " + currentWorkingBlock.GetComponent<TexDrawUtils>().digitvalue.Count);
                        currentWorkingBlock.GetComponent<TexDrawUtils>().digitvalue[currentWorkingBlock.GetComponent<TexDrawUtils>().digitvalue.Count - 1] = value;
                    }
                    if (operSighnAdded && currentWorkingBlock.GetComponent<TexDrawUtils>().multiDigitAllowed)
                    {
                        values.Insert(index + 1, value);
                        currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex.Add(index + 1);
                        chagebelowtextUtil(currentWorkingBlock.GetComponent<TexDrawUtils>().serialNumber);

                    }
                    else
                        values[index] = value;
                    //values[index] = value;
                    //Debug.Log(" preadded added index: "+ index);
                    value = 0;
                    //currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex.Add(index);
                }
                //valueentered = false;
                digitentered = false;
                //currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex.Add(values.Count - 1);
                operSighnAdded = false;
                currentWorkingBlock = obj;
                currentWorkingTexDraw = currentWorkingBlock.GetComponent<TEXDraw>();
                //Debug.Log("clicked on: " + currentWorkingBlock.name);
                if (currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex.Count == 0)
                {
                    value = 0;
                    valuefromList = false;
                }
                else
                {
                    //Debug.Log("value index count: " + currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex.Count);
                    int index = currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex[currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex.Count - 1];
                    //Debug.Log("index " + index);
                    value = values[index];
                    valuefromList = true;
                }
                if (currentWorkingBlock.GetComponent<TexDrawUtils>().placeValue == 0 && !currentWorkingBlock.GetComponent<TexDrawUtils>().multiDigitAllowed)
                {
                    // Debug.Log(" change value");
                    canDelete = true;
                    changeValueOnBackspace = true;
                }
                else
                {
                    // Debug.Log("cannot change value");
                    canDelete = false;
                    changeValueOnBackspace = false;
                    if (String.Compare(currentWorkingBlock.GetComponent<TexDrawUtils>().texType, UtilityREST.F) == 0 || String.Compare(currentWorkingBlock.GetComponent<TexDrawUtils>().texType, UtilityREST.MF) == 0)
                    {
                        canDelete = true;
                        changeValueOnBackspace = true;
                    }
                }
                //Debug.Log("value currentWorkingBlock.GetComponent<TexDrawUtils>().placeValue: " + currentWorkingBlock.GetComponent<TexDrawUtils>().placeValue);
                selectedBypointer = true;
                // changeValueOnBackspace = true;

                //noofvaluesafterselect = 0;
            }

            StartBlinkingCursor();
        }

    }

    void chagebelowtextUtil(int index)
    {
        //Debug.Log("change index" + index);
        for (int i = index + 1; i < allTexDrawInaRow.Count; i++)
        {
            TexDrawUtils temp = allTexDrawInaRow[i];
            for (int j = 0; j < temp.valueIndex.Count; j++)
            {
                //Debug.Log("temp value: " + temp.valueIndex[j]);
                temp.valueIndex[j] = temp.valueIndex[j] + 1;
                //Debug.Log("after temp value: " + temp.valueIndex[j]);
            }
            // temp.;
        }
    }

    void changeOperatorvaluebelow(int index)
    {
        for (int i = index + 1; i < allTexDrawInaRow.Count; i++)
        {
            TexDrawUtils temp = allTexDrawInaRow[i];
            for (int j = 0; j < temp.expIndex.Count; j++)
            {
                Debug.Log("temp value: " + temp.expIndex[j]);
                temp.expIndex[j] = temp.expIndex[j] + 1;
                Debug.Log("after temp value: " + temp.expIndex[j]);
            }
            // temp.;
        }
    }

    void decreaseAllTexutlOper(int index)
    {
        Debug.Log("change index");
        for (int i = index + 1; i < allTexDrawInaRow.Count; i++)
        {
            TexDrawUtils temp = allTexDrawInaRow[i];
            for (int j = 0; j < temp.expIndex.Count; j++)
            {
                //Debug.Log("temp value: " + temp.expIndex[j]);
                temp.expIndex[j] = temp.expIndex[j] - 1;
                //Debug.Log("after temp value: " + temp.expIndex[j]);
            }
            // temp.;
        }
    }

    void decreaseAllTexutl(int index)
    {
        Debug.Log("change index");
        for (int i = index + 1; i < allTexDrawInaRow.Count; i++)
        {
            TexDrawUtils temp = allTexDrawInaRow[i];
            for (int j = 0; j < temp.valueIndex.Count; j++)
            {
                //Debug.Log("temp value: " + temp.valueIndex[j]);
                temp.valueIndex[j] = temp.valueIndex[j] - 1;
                //Debug.Log("after temp value: " + temp.valueIndex[j]);
            }
            // temp.;
        }
    }

    void LCM()
    {
        if (isActive)
        {
            StopblinkingCursor();
            currentWorkingBlock.GetComponent<TexDrawUtils>().multiDigitAllowed = true;
            currentWorkingBlock.GetComponent<TexDrawUtils>().lcmFactor = true;
            getNewCurrentWorkingTex();
            Functionmode = modeLCM;
            texValue = downLine;
            currentWorkingTexDraw.text = currentWorkingTexDraw.text.Insert(0, "\\vborder[1100 black]");
            StartBlinkingCursor();
        }

    }

    void HCF()
    {
        if (isActive)
        {
            StopblinkingCursor();
            currentWorkingBlock.GetComponent<TexDrawUtils>().multiDigitAllowed = true;
            currentWorkingBlock.GetComponent<TexDrawUtils>().lcmFactor = true;
            getNewCurrentWorkingTex();
            Functionmode = modeHCF;
            texValue = downLine;
            currentWorkingTexDraw.text = currentWorkingTexDraw.text.Insert(0, "\\vborder[1100 black]");
            StartBlinkingCursor();
        }

    }

    void Divide()
    {
        if (isActive)
        {
            StopblinkingCursor();
            //Debug.Log("Divide");
            NextLine();
            getNewCurrentWorkingTex();
            currentWorkingBlock.GetComponent<TexDrawUtils>().multiDigitAllowed = true;
            currentWorkingBlock.GetComponent<TexDrawUtils>().lcmFactor = true;
            getNewCurrentWorkingTex();
            Functionmode = modeDivision;
            texValue = UpLine;
            currentWorkingTexDraw.text = currentWorkingTexDraw.text.Insert(0, "\\vborder[1001 black]");
            StartBlinkingCursor();
        }

    }

    void Addition()
    {
        if (isActive)
        {
            StopblinkingCursor();
            //Debug.Log("Add");
            if (Functionmode != modeAddition)
            {
                Functionmode = modeAddition;
                currentWorkingBlock.GetComponent<TexDrawUtils>().multiDigitAllowed = true;
                NextLine();
                currentWorkingBlock.GetComponent<TexDrawUtils>().multiDigitAllowed = true;
                NextLine();
                nextValue = true;
                //Debug.Log("addition");
                placeValue = 0;
                values.Clear();
                allTexDrawInaRow.Clear();
                getNewCurrentWorkingTex();
                //initilizeStep();

            }
            StartBlinkingCursor();
        }
    }

    void roughWorkOpened()
    {
        if (isActive)
        {
            isActive = false;
        }
    }

    void roughworkClicked(int index)
    {

        currentRow.GetComponent<RowController>().hasRoughwork = true;
        currentRow.GetComponent<RowController>().roughIndex = index;
        if (OnroughworkShowAction != null)
            OnroughworkShowAction(index);
        // currentStep.transform.GetChild(indexOfStep).gameObject.SetActive(true);
    }

    void backFromRoughWork()
    {
        //Debug.Log("back button on rough work pressed main");
        isActive = true;

    }

    void RoughWorkReOpened()
    {

        if (isActive)
        {
            isActive = false;
        }
    }

    void clearingrow()
    {
        GameObject temp = currentRow.transform.GetChild(indexofWorkingArea).gameObject;
        foreach (Transform child in temp.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        currentMaxSpaceInRow = 0;
        currentrowPrevLineSpace = 0;
        //rowNumber++;
        currentNoofColumns = 0;
        prevNoofColumns = 1;
        noOfRowsinStep = 0;
        answerPressedCounter = 0;
        nexttextback = false;
        texValue = defaultvalue;
        intTestNo = 0;
        initilizeStep();
        //Debug.Log("initilizeStep clearing row");
        currentRow.GetComponent<RowController>().hasRoughwork = false;
        currentRow.GetComponent<RowController>().noofrows = 0;
        //Debug.Log("PrevRowMaxSpace: " + PrevRowMaxSpace);
        getNewCurrentWorkingTex();
    }

    void CLearRow()
    {
        if (isActive)
        {
            StopblinkingCursor();
            if (valueentered > 0 || fractionCounter > 0)
            {
                if (Functionmode == modeAddition)
                {
                    clearCanvas();
                }
                else
                    clearingrow();
            }
            else
            {
                if (OnShowMessage != null)
                    OnShowMessage("Nothing to clear");

            }



            StartBlinkingCursor();
        }
    }

    void StartBlinkingCursor()
    {
        blinkCursor = true;
        StartCoroutine("blinkingCursor");
    }

    void StopblinkingCursor()
    {
        StopCoroutine("blinkingCursor");
        if (!blinkCursor)
            currentWorkingTexDraw.text = currentWorkingTexDraw.text.Remove(currentWorkingTexDraw.text.Length - cursorVarCount, cursorVarCount);
        blinkCursor = false;

    }

    IEnumerator blinkingCursor()
    {
        yield return new WaitForSeconds(1.0f);
        if (blinkCursor)
        {


            currentWorkingTexDraw.text = currentWorkingTexDraw.text.Insert(currentWorkingTexDraw.text.Length, cursorVar);

        }
        else
        {

            currentWorkingTexDraw.text = currentWorkingTexDraw.text.Remove(currentWorkingTexDraw.text.Length - cursorVarCount, cursorVarCount);


        }
        blinkCursor = !blinkCursor;
        StartCoroutine("blinkingCursor");
    }
}
