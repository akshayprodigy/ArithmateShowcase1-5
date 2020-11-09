using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class NewRoughManager : MonoBehaviour {

    // Use this for initialization

    public delegate void RoughWorkBackAction();
    public static event RoughWorkBackAction OnRoughWorkBackAction;
    public delegate void HasRoughWork();
    public static event HasRoughWork OnHasRoughWork;
    public delegate void ShowMessage(string msg);
    public static event ShowMessage OnShowMessage;

    public GameObject Row, GeneralDigit, Fraction, FractiondigitUnclickable;

    public GameObject currentWorkingBlock, currentRow, instanciateOnThisBlock;//currentStep
    int currentNoofColumns, prevNoofColumns, rowNumber, currentMaxSpaceInRow, PrevRowMaxSpace, currentrowPrevLineSpace;
    int indexOfStep = 0, indexofWorkingArea = 1, cursorVarCount = 20, noOfRowsinStep, intTestNo;
    bool blinkCursor, selectedBypointer, nexttextback, nextValue, fromGeneral;
    int Functionmode = 0, textBoxTypeMode = 0, answerPressedCounter;
    const int modeBasic = 111, modeLCM = 112, modeHCF = 114, modeDivision = 113, modeAddition = 115, modeFraction = 121, modeMixedFraction = 122, defaultmode = 0;
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
    bool islastStep, valueEnteredinList, hasExpression;
    bool operatedClicked, operSighnAdded;
    bool singleStep, isanswer, valueSwapped;
    bool canAddFraction = true;
    bool previous_value_zero = false, digitentered = false, valuefromList = false, changeValueOnBackspace = true, canDelete = true;
    public List<TexDrawUtils> allTexDrawInaRow;
    int valueentered = 0;
    int valueIndex, placeValue;
    int prevExpvalueCounter = 0;
    int OpeningBracketcounter = 0;
    bool openingBracketAdded = false;
    bool hasBracked = false;
    int fractionCounter = 0;
    bool fractionBracketAdjusted = false;
    int fractionCounterAfterBracketAdjust;

    void OnEnable()
    {
        CanvasManager.OnNumberClickedAction += NumberClicked;
        CanvasManager.OnNextLineAction += GotoNextLine;
        CanvasManager.OnMixedFractionAction += AddMixedFraction;
        CanvasManager.OnFractionAction += AddFraction;
        CanvasManager.OnExpClickedAction += ExpressionClicked;
        CanvasManager.OnNextStepAction += NextStep;
        CanvasManager.OnDeleteAction += Delete;
        CanvasManager.OnLCMAction += LCM;
        CanvasManager.OnHCFAction += HCF;
        CanvasManager.OnAnswerAction += Answer;
        CanvasManager.OnDivideAction += Divide;
        //CanvasManager.OnRoughworkAction += roughworkClicked;
        //CanvasManager.OnReOpenRoughworkAction += RoughWorkReOpened;
        CanvasManager.OnAdditionAction += ClickedOnAdditin;
        //RoughWorkManager.OnRoughWorkBackAction += backFromRoughWork;
        TexDrawUtils.OnTexDrawClickedAction += OnClickedOnATexDraw;
        //CanvasManager.OnNextQuestion += CheckCurrentCanvas;
        CanvasManager.OnClearCurrentRow += CLearRow;
        //CallRESTServices.OnQuestionDownloaded += showQuestion;
        //CanvasManager.OnNextDontKnowQuestion += showforNextUnknownQuestion;
        CanvasManager.OnClearCanvas += onClearCanvasButton;
        //CanvasManager.OnRoughworkOpenAction += roughWorkOpened;
    }

    private void OnDisable()
    {
        CanvasManager.OnNumberClickedAction -= NumberClicked;
        CanvasManager.OnNextLineAction -= GotoNextLine;
        CanvasManager.OnMixedFractionAction -= AddMixedFraction;
        CanvasManager.OnFractionAction -= AddFraction;
        CanvasManager.OnExpClickedAction -= ExpressionClicked;
        //CanvasManager.OnNextStepAction -= NextStep;
        CanvasManager.OnDeleteAction -= Delete;
        CanvasManager.OnLCMAction -= LCM;
        CanvasManager.OnHCFAction += HCF;
        CanvasManager.OnAnswerAction -= Answer;
        CanvasManager.OnDivideAction -= Divide;
        CanvasManager.OnAdditionAction -= ClickedOnAdditin;
        //CanvasManager.OnRoughworkAction -= roughworkClicked;
        //CanvasManager.OnReOpenRoughworkAction -= RoughWorkReOpened;
        //RoughWorkManager.OnRoughWorkBackAction += backFromRoughWork;
        TexDrawUtils.OnTexDrawClickedAction -= OnClickedOnATexDraw;
        //CanvasManager.OnNextQuestion -= CheckCurrentCanvas;
        CanvasManager.OnClearCurrentRow -= CLearRow;
        //CallRESTServices.OnQuestionDownloaded -= showQuestion;
        //CanvasManager.OnNextDontKnowQuestion -= showforNextUnknownQuestion;
        CanvasManager.OnClearCanvas -= onClearCanvasButton;
        //CanvasManager.OnRoughworkOpenAction -= roughWorkOpened;
    }

    void Start () {
        initiate();
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
        
        getNewCurrentWorkingTex();
        StartBlinkingCursor();


    }

    public void backpressed()
    {
        // going back to main canvas
        if (valueentered > 0)
        {
            if (OnHasRoughWork != null)
                OnHasRoughWork();
        }
        if (OnRoughWorkBackAction != null)
            OnRoughWorkBackAction();
        isActive = false;

    }

    public void setRoughWorkActive()
    {
        isActive = true;

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
                Debug.Log("num pos: " + pos + " values: " + values.Count);
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
                        Debug.Log("allTexDrawInaRow[allTexDrawInaRow.Count - 2].expIndex[allTexDrawInaRow[allTexDrawInaRow.Count - 2].expIndex.Count - 1]: " + allTexDrawInaRow[allTexDrawInaRow.Count - 3].expIndex[allTexDrawInaRow[allTexDrawInaRow.Count - 3].expIndex.Count - 1]);
                        allTexDrawInaRow[allTexDrawInaRow.Count - 3].expIndex[allTexDrawInaRow[allTexDrawInaRow.Count - 3].expIndex.Count - 1] = insertpos;
                        Debug.Log("allTexDrawInaRow[allTexDrawInaRow.Count - 2].expIndex[allTexDrawInaRow[allTexDrawInaRow.Count - 2].expIndex.Count - 1]: " + allTexDrawInaRow[allTexDrawInaRow.Count - 3].expIndex[allTexDrawInaRow[allTexDrawInaRow.Count - 3].expIndex.Count - 1]);

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
            //valueEnteredinList = false;
            switch (Functionmode)
            {
                case modeBasic:
                    //Debug.Log("Funtion Mode:");
                    break;
                case modeAddition:
                    StartBlinkingCursor();
                    Debug.Log("in here");
                    if (string.Equals(exp, "+")){
                        allTexDrawInaRow.RemoveAt(allTexDrawInaRow.Count - 1);
                        GotoNextLine();
                    }
                    else if(string.Equals(exp, "=")){
                        removeAllvalueIndex();
                        Answer();
                        valueEnteredinList = false;
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
            default:

                break;
        }
        return value;
    }

    void NextStep()
    {
        if (singleStep)
        {
            //StartBlinkingCursor();
            //printStepNumber();
            removeAllvalueIndex();
            Answer();
            valueEnteredinList = false;
            // 

        }
    }

    void removeAllvalueIndex()
    {
        foreach (TexDrawUtils tex in allTexDrawInaRow)
        {
            tex.valueIndex.Clear();
        }
    }

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
           // if (OnprintNextLineAction != null)
            //    OnprintNextLineAction();
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

    public void OnClickedOnATexDraw(GameObject obj)
    {
        if (isActive)
        {
            StopblinkingCursor();
            if (!obj.GetComponent<TexDrawUtils>().notClickable)
            {
                /*if (valueentered) test
                 {
                     values.Add(value);
                     value = 0;
                 }*/
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
                        Debug.Log("value entered: " + currentWorkingBlock.GetComponent<TexDrawUtils>().digitvalue.Count);
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
                textBoxTypeMode = modeMixedFraction;
                getNewCurrentWorkingTex();
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
                //if (OnprintfractionAction != null)
                //    OnprintfractionAction();
                // adjust brackets if necessary
                adjustBracketsforFraction();
                textBoxTypeMode = modeFraction;
                getNewCurrentWorkingTex();
                StartBlinkingCursor();
                display += 2;
                //operatedClicked = true;
                //valueentered = 0;
                canAddFraction = false;
                fractionCounter++;
                if (operSighnAdded)
                    operSighnAdded = false;
            }
        }

    }

    void adjustBracketsforFraction()
    {
        Debug.Log(" adjust brackets for fraction" + operators.Count);
        if (operators.Count > 0)
        {
            // need to adjust brackets
            Debug.Log("nneed to adjust brackets for fraction");
            // check if there is a digit before f and operator
            if (allTexDrawInaRow.Count > 2)
            {
                TexDrawUtils lastTex = allTexDrawInaRow[allTexDrawInaRow.Count - 3];
                if (String.Equals(lastTex.texType, UtilityREST.General))
                {
                    Debug.Log("putin brackets" + lastTex.faceValue + lastTex.texType + lastTex.texPosition);
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
                    Debug.Log("we dnt need brackets");
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
                            //Debug.Log("has Operator");
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
                                    Destroy(currentWorkingBlock);
                                    //allTexDrawInaRow.RemoveAt(currentWorkingBlock.GetComponent<TexDrawUtils>().serialNumber);
                                    TexDrawUtils prevutil = allTexDrawInaRow[prevSerial];
                                    currentWorkingBlock = prevutil.gameObject;
                                    currentWorkingTexDraw = currentWorkingBlock.GetComponent<TEXDraw>();
                                    instanciateOnThisBlock = currentWorkingBlock;
                                    currentWorkingBlock.GetComponent<TexDrawUtils>().notClickable = false;
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
                                decreaseAllSeialNoafter(tempserial);
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

    void decreaseAllSeialNoafter(int serial)
    {
        for (int i = serial; i < allTexDrawInaRow.Count; i++)
        {
            TexDrawUtils temp = allTexDrawInaRow[i];
            temp.serialNumber--;
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


    void ClickedOnAdditin()
    {
        if (isActive)
        {
            Addition();
            singleStep = true;
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
            
            foreach (Transform child in transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            //Debug.Log("clearCanvas");
            initiate();
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
