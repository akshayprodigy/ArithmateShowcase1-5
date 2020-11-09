using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RoughWorkManager : MonoBehaviour {

    // Use this for initialization
    public GameObject Row, GeneralDigit, Fraction;

    public GameObject currentWorkingBlock, currentStep, currentRow, instanciateOnThisBlock,CursorBlock;
    int currentNoofColumns, prevNoofColumns, rowNumber, currentMaxSpaceInRow, PrevRowMaxSpace, currentrowPrevLineSpace;
    int indexOfStep = 0, indexofWorkingArea = 1, cursorVarCount = 20, noOfRowsinStep, intTestNo;
    bool blinkCursor, selectedBypointer, nexttextback;
    int Functionmode = 0, textBoxTypeMode = 0, answerPressedCounter;
    const int modeBasic = 111, modeLCM = 112, modeHCF = 114, modeDivision = 113, modeAddition = 115, modeFraction = 121, modeMixedFraction = 122, defaultmode = 0;
    
    public bool isActive,valuePressed;
    string cursorVar = "\\vborder[1000 black]", texValue = " ", downLine = "\\vborder[0100 black] ", defaultvalue = " ", UpLine = "\\vborder[0001 black] ";
    TEXDraw currentWorkingTexDraw,cursorTexDraw;

    private void OnEnable()
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
        CanvasManager.OnAdditionAction += Addition;
        CanvasManager.OnClearCanvas += clearCanvas;
        TexDrawUtils.OnTexDrawClickedAction += OnClickedOnATexDraw;
    }

    private void OnDisable()
    {
        CanvasManager.OnNumberClickedAction -= NumberClicked;
        CanvasManager.OnNextLineAction -= GotoNextLine;
        CanvasManager.OnMixedFractionAction -= AddMixedFraction;
        CanvasManager.OnFractionAction -= AddFraction;
        CanvasManager.OnExpClickedAction -= ExpressionClicked;
        CanvasManager.OnNextStepAction -= NextStep;
        CanvasManager.OnDeleteAction -= Delete;
        CanvasManager.OnLCMAction -= LCM;
        CanvasManager.OnHCFAction += HCF;
        CanvasManager.OnAnswerAction -= Answer;
        CanvasManager.OnDivideAction -= Divide;
        CanvasManager.OnAdditionAction -= Addition;
        CanvasManager.OnClearCanvas -= clearCanvas;
        TexDrawUtils.OnTexDrawClickedAction -= OnClickedOnATexDraw;
    }

    public void backpressed()
    {
        // going back to main canvas
        if (valuePressed)
        {
            //if (OnHasRoughWork != null)
                //OnHasRoughWork();
        }
        //if (OnRoughWorkBackAction != null)
            //OnRoughWorkBackAction();
        isActive = false;

    }

    void Start () {
       
        initiate();
        Debug.Log("RoughWork opened");
    }

	
    public void setRoughWorkActive()
    {
        isActive = true;

    }

    void initiate()
    {
        /*
        currentNoofColumns = 0;
        prevNoofColumns = 1;
        noOfRowsinStep = 0;
        answerPressedCounter = 0;
        intTestNo = 0;
        blinkCursor = true;
        nexttextback = false;
        valuePressed = false;
        getNewCurrentWorkingTex();
        // for changing cursor position;
        CursorBlock = currentWorkingBlock;
        cursorTexDraw = currentWorkingTexDraw;
        // for changing cursor position;
        StartBlinkingCursor();*/
        rowNumber = 1;
        currentNoofColumns = 0;
        prevNoofColumns = 1;
        noOfRowsinStep = 0;
        answerPressedCounter = 0;
        currentMaxSpaceInRow = 0;
        currentrowPrevLineSpace = 0;
        PrevRowMaxSpace = 0;
        intTestNo = 0;
        
        blinkCursor = true;
        isActive = true;
        nexttextback = false;
        currentRow = (GameObject)Instantiate(Row, transform);
        Functionmode = defaultmode;
        getNewCurrentWorkingTex();
        //checkPrequisit();
        StartBlinkingCursor();
    }

    void clearCanvas()
    {
        if (isActive)
        {
            foreach (Transform child in transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            initiate();
        }
        
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
                //allTexDrawInaRow.Insert(prevSerial, currentWorkingBlock.GetComponent<TexDrawUtils>());
                currentWorkingBlock.GetComponent<TexDrawUtils>().serialNumber = prevSerial;
                currentWorkingBlock.GetComponent<TexDrawUtils>().digitvalue.Add(0);
                prevTex.GetComponent<TexDrawUtils>().serialNumber = ++prevSerial;
                // Numerator woking
                //Debug.Log("prev tex value: " + prevTex.GetComponent<TexDrawUtils>().valueIndex[prevTex.GetComponent<TexDrawUtils>().valueIndex.Count - 1]);
                pos = prevTex.GetComponent<TexDrawUtils>().valueIndex[prevTex.GetComponent<TexDrawUtils>().valueIndex.Count - 1];
                //value = 0;
                //Debug.Log("num pos: " + pos + " values: " + values.Count);
                //values.Insert(pos, value);
                //currentWorkingBlock.GetComponent<TexDrawUtils>().expValue.Add("op-oround");
                //currentWorkingBlock.GetComponent<TexDrawUtils>().expValue.Add("op_num_denom");
                //if (fractionBracketAdjusted)
                //{
                //    if (fractionCounterAfterBracketAdjust > 0)
                //    {
                //        string val = operators[operators.Count - 1];
                //        operators.RemoveAt(operators.Count - 1);
                //        int insertpos = operators.Count - 1;
                //        operators.Insert(insertpos, val);
                //        Debug.Log("allTexDrawInaRow[allTexDrawInaRow.Count - 2].expIndex[allTexDrawInaRow[allTexDrawInaRow.Count - 2].expIndex.Count - 1]: " + allTexDrawInaRow[allTexDrawInaRow.Count - 3].expIndex[allTexDrawInaRow[allTexDrawInaRow.Count - 3].expIndex.Count - 1]);
                //        allTexDrawInaRow[allTexDrawInaRow.Count - 3].expIndex[allTexDrawInaRow[allTexDrawInaRow.Count - 3].expIndex.Count - 1] = insertpos;
                //        Debug.Log("allTexDrawInaRow[allTexDrawInaRow.Count - 2].expIndex[allTexDrawInaRow[allTexDrawInaRow.Count - 2].expIndex.Count - 1]: " + allTexDrawInaRow[allTexDrawInaRow.Count - 3].expIndex[allTexDrawInaRow[allTexDrawInaRow.Count - 3].expIndex.Count - 1]);

                //    }
                //    operators.Insert(operators.Count - 1, UtilityREST.var_openingBrackets);
                //    fractionCounterAfterBracketAdjust++;
                //}

                //else
                //    operators.Add("op-oround");
                //int operPos = operators.Count - 1;
                ////operators.Add("op-cround");
                //display++;
                //values.Add(value);
                currentWorkingBlock.GetComponent<TexDrawUtils>().texType = UtilityREST.F;
                currentWorkingBlock.GetComponent<TexDrawUtils>().texPosition = UtilityREST.Numerator;
                currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex.Add(pos);
                //currentWorkingBlock.GetComponent<TexDrawUtils>().expIndex.Add(operPos);
                prevTex.GetComponent<TexDrawUtils>().valueIndex[prevTex.GetComponent<TexDrawUtils>().valueIndex.Count - 1] = ++pos;
                // denominator
                GameObject dem = ParentWorkingBlock.transform.GetChild(1).gameObject;
                pos = prevTex.GetComponent<TexDrawUtils>().valueIndex[prevTex.GetComponent<TexDrawUtils>().valueIndex.Count - 1];
                //Debug.Log("prev tex value dem: " + pos);
                prevSerial = prevTex.GetComponent<TexDrawUtils>().serialNumber;
                //allTexDrawInaRow.Insert(prevSerial, dem.GetComponent<TexDrawUtils>());
                dem.GetComponent<TexDrawUtils>().texType = UtilityREST.F;
                dem.GetComponent<TexDrawUtils>().texPosition = UtilityREST.Denominator;
                dem.GetComponent<TexDrawUtils>().serialNumber = prevSerial;
                prevTex.GetComponent<TexDrawUtils>().serialNumber = ++prevSerial;
                //allTexDrawInaRow.Add(dem.GetComponent<TexDrawUtils>());
                //dem.GetComponent<TexDrawUtils>().serialNumber = allTexDrawInaRow.Count - 1;
                dem.GetComponent<TexDrawUtils>().expValue.Add("op-cround");
                //if (fractionBracketAdjusted)
                //    operators.Insert(operators.Count - 1, UtilityREST.var_closingBrackets);
                //else
                //    operators.Add("op-cround");
                //int operPosDem = operators.Count - 1;
                //display++;
                ////Debug.Log("dem pos: " + pos);
                //values.Insert(pos, value);
                //dem.GetComponent<TexDrawUtils>().valueIndex.Add(pos);
                //dem.GetComponent<TexDrawUtils>().digitvalue.Add(0);
                //dem.GetComponent<TexDrawUtils>().expIndex.Add(operPosDem);
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
                //allTexDrawInaRow.Insert(prevSerial, currentWorkingBlock.GetComponent<TexDrawUtils>());
                currentWorkingBlock.GetComponent<TexDrawUtils>().serialNumber = prevSerial;
                prevTex.GetComponent<TexDrawUtils>().serialNumber = ++prevSerial;
                //currentWorkingBlock.GetComponent<TexDrawUtils>().serialNumber = allTexDrawInaRow.Count - 1;
                //value = 0;
                //values.Add(value);
                pos = prevTex.GetComponent<TexDrawUtils>().valueIndex[prevTex.GetComponent<TexDrawUtils>().valueIndex.Count - 1];
                ////value = 0;
                ////values.Insert(pos, value);
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
                //allTexDrawInaRow.Insert(prevSerial, frac.GetComponent<TexDrawUtils>());
                frac.GetComponent<TexDrawUtils>().serialNumber = prevSerial;
                prevTex.GetComponent<TexDrawUtils>().serialNumber = ++prevSerial;
                //values.Add(value);
                pos = prevTex.GetComponent<TexDrawUtils>().valueIndex[prevTex.GetComponent<TexDrawUtils>().valueIndex.Count - 1];
                //value = 0;
                //values.Insert(pos, value);
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
                //allTexDrawInaRow.Insert(prevSerial, frac.GetComponent<TexDrawUtils>());
                frac.GetComponent<TexDrawUtils>().serialNumber = prevSerial;
                prevTex.GetComponent<TexDrawUtils>().serialNumber = ++prevSerial;
                //frac.GetComponent<TexDrawUtils>().serialNumber = allTexDrawInaRow.Count - 1;
                //values.Add(value);
                //frac.GetComponent<TexDrawUtils>().digitvalue.Add(0);
                //values.Insert(pos, value);
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
                //allTexDrawInaRow.Add(currentWorkingBlock.GetComponent<TexDrawUtils>());
                currentWorkingTexDraw = currentWorkingBlock.GetComponent<TEXDraw>();
                currentWorkingTexDraw.text = currentWorkingTexDraw.text.Insert(0, texValue);
                // setup bloc

                //currentWorkingBlock.GetComponent<TexDrawUtils>().serialNumber = allTexDrawInaRow.Count - 1;
                currentWorkingBlock.GetComponent<TexDrawUtils>().texType = UtilityREST.General;
                currentWorkingBlock.GetComponent<TexDrawUtils>().texPosition = UtilityREST.General;
                //if (nextValue)
                //{
                //    //Debug.Log("next Value");

                //    value = 0;
                //    if (fromGeneral)
                //    {
                //        values.Add(value);
                //    }

                //    valueIndex = values.Count - 1;
                //    nextValue = false;

                //}
                //Debug.Log("value new tex valueIndex: " + valueIndex);
                //currentWorkingBlock.GetComponent<TexDrawUtils>().valueIndex.Add(valueIndex);
                currentWorkingBlock.GetComponent<TexDrawUtils>().faceValue = 0;
                instanciateOnThisBlock = currentWorkingBlock;
                //Debug.Log("instanciateOnThisBlock.name: " + instanciateOnThisBlock.name);
                break;
        }

        // instanciateOnThisBlock = currentWorkingBlock;
    }

    //void getNewCurrentWorkingTex()
    //{
    //    GameObject temp = this.gameObject;//currentRow.transform.GetChild(indexofWorkingArea).gameObject;
    //    currentNoofColumns++;
    //    VariableGridLayoutGroup mVariableGridLayout = temp.GetComponent<VariableGridLayoutGroup>();
    //    if (currentNoofColumns > prevNoofColumns)
    //    {
    //        mVariableGridLayout.constraintCount++;
    //        GameObject child;
    //        /* if (noOfRows < 3)
    //         {*/
    //        for (int i = 1; i <= noOfRowsinStep; i++)
    //        {
    //            child = (GameObject)Instantiate(GeneralDigit, transform);
    //            child.transform.parent = temp.transform;
    //            child.GetComponent<TEXDraw>().text = child.GetComponent<TEXDraw>().text.Insert(0, " ");
    //            intTestNo++;
    //            child.name = "Child" + rowNumber.ToString() + intTestNo.ToString();

    //            switch (Functionmode)
    //            {
    //                case modeAddition:
    //                    Debug.Log("Funtion Mode:");
    //                    if (i < 3)
    //                    {
    //                        child.GetComponent<TexDrawUtils>().multiDigitAllowed = true;
    //                        child.GetComponent<TexDrawUtils>().numberOfDigit = 0;
    //                    }
    //                    if (i < 4)
    //                        child.transform.SetSiblingIndex((i * currentNoofColumns) - 1);
    //                    else
    //                        child.transform.SetSiblingIndex((i * currentNoofColumns) - 2);
    //                    break;
    //                case modeLCM:
    //                    Debug.Log("LCM Mode:");
    //                    child.transform.SetSiblingIndex((i - 1) * currentNoofColumns);
    //                    break;
    //                case modeHCF:
    //                    Debug.Log("LCM Mode:");
    //                    child.transform.SetSiblingIndex((i - 1) * currentNoofColumns);
    //                    break;
    //                default:
    //                    Debug.Log("Default Mode:");
    //                    child.transform.SetSiblingIndex((i * currentNoofColumns - 1));
    //                    break;

    //            }
    //        }
    //    }
    //    GameObject prevTex;
    //    switch (textBoxTypeMode)
    //    {
    //        case modeFraction:
    //            prevTex = currentWorkingBlock;
    //            Debug.Log("fraction mode");
    //            currentWorkingBlock = (GameObject)Instantiate(Fraction, transform);
    //            currentWorkingBlock.transform.parent = temp.transform;
    //            currentWorkingBlock.transform.SetSiblingIndex(((noOfRowsinStep) * mVariableGridLayout.constraintCount));
    //            prevTex.transform.SetSiblingIndex(((noOfRowsinStep) * mVariableGridLayout.constraintCount));
    //            currentWorkingBlock.name = "newTexFrac" + rowNumber.ToString() + intTestNo.ToString();
    //            currentWorkingBlock = currentWorkingBlock.transform.GetChild(0).gameObject;
    //            currentWorkingBlock.GetComponent<TexDrawUtils>().multiDigitAllowed = true;
    //            currentWorkingTexDraw = currentWorkingBlock.GetComponent<TEXDraw>();
    //            intTestNo++;
    //            textBoxTypeMode = defaultmode;
    //            break;
    //        case modeMixedFraction:
    //            // Debug.Log("modeMixedFraction mode");
    //            //as two values are added
    //            currentNoofColumns++;
    //            if (currentNoofColumns > prevNoofColumns)
    //            {
    //                mVariableGridLayout.constraintCount++;
    //                GameObject child;
    //                /* if (noOfRows < 3)
    //                 {*/
    //                for (int i = 1; i <= noOfRowsinStep; i++)
    //                {
    //                    child = (GameObject)Instantiate(GeneralDigit, transform);
    //                    child.transform.parent = temp.transform;
    //                    child.GetComponent<TEXDraw>().text = child.GetComponent<TEXDraw>().text.Insert(0, " ");
    //                    intTestNo++;
    //                    child.name = "Child" + rowNumber.ToString() + intTestNo.ToString();

    //                    switch (Functionmode)
    //                    {
    //                        case modeBasic:
    //                            Debug.Log("Funtion Mode:");
    //                            if (i < 3)
    //                            {
    //                                child.GetComponent<TexDrawUtils>().multiDigitAllowed = true;
    //                                child.GetComponent<TexDrawUtils>().numberOfDigit = 0;
    //                            }
    //                            if (i < 4)
    //                                child.transform.SetSiblingIndex((i * currentNoofColumns) - 1);
    //                            else
    //                                child.transform.SetSiblingIndex((i * currentNoofColumns) - 2);
    //                            break;
    //                        case modeLCM:
    //                            Debug.Log("LCM Mode:");
    //                            child.transform.SetSiblingIndex((i - 1) * currentNoofColumns);
    //                            break;
    //                        case modeHCF:
    //                            Debug.Log("LCM Mode:");
    //                            child.transform.SetSiblingIndex((i - 1) * currentNoofColumns);
    //                            break;
    //                        default:
    //                            Debug.Log("Default Mode:");
    //                            child.transform.SetSiblingIndex((i * currentNoofColumns - 1));
    //                            break;

    //                    }
    //                }
    //            }
    //            prevTex = currentWorkingBlock;
    //            currentWorkingBlock = (GameObject)Instantiate(GeneralDigit, transform);
    //            currentWorkingBlock.transform.parent = temp.transform;
    //            currentWorkingBlock.transform.SetSiblingIndex((noOfRowsinStep) * mVariableGridLayout.constraintCount);
    //            currentWorkingBlock.name = "newTexmix" + rowNumber.ToString() + intTestNo.ToString();
    //            currentWorkingTexDraw = currentWorkingBlock.GetComponent<TEXDraw>();
    //            currentWorkingTexDraw.text = currentWorkingTexDraw.text.Insert(0, " ");
    //            currentWorkingBlock = (GameObject)Instantiate(Fraction, transform);
    //            currentWorkingBlock.transform.parent = temp.transform;
    //            currentWorkingBlock.transform.SetSiblingIndex(((noOfRowsinStep) * mVariableGridLayout.constraintCount));
    //            prevTex.transform.SetSiblingIndex(((noOfRowsinStep) * mVariableGridLayout.constraintCount));
    //            currentWorkingBlock.name = "newTexMixFrac" + rowNumber.ToString() + intTestNo.ToString();
    //            currentWorkingBlock = currentWorkingTexDraw.gameObject;
    //            currentWorkingBlock.GetComponent<TexDrawUtils>().multiDigitAllowed = true;
    //            //prevTex.name = "newTex" + intTestNo.ToString();

    //            intTestNo++;
    //            textBoxTypeMode = defaultmode;
    //            break;
    //        default:
    //            // for changing cursor position;
    //            if (currentWorkingBlock != null)
    //            {
    //                cursorTexDraw = currentWorkingTexDraw;
    //                CursorBlock = currentWorkingBlock;
    //            }
    //            // for changing cursor position;
    //            currentWorkingBlock = (GameObject)Instantiate(GeneralDigit, transform);
    //            currentWorkingBlock.transform.parent = temp.transform;
    //            if (nexttextback)
    //            {
    //                currentWorkingBlock.transform.SetSiblingIndex(temp.transform.childCount - 2);
    //            }
    //            else
    //                currentWorkingBlock.transform.SetSiblingIndex((noOfRowsinStep) * mVariableGridLayout.constraintCount);
    //            currentWorkingBlock.name = "newTex" + rowNumber.ToString() + intTestNo.ToString();
    //            intTestNo++;

    //            currentWorkingTexDraw = currentWorkingBlock.GetComponent<TEXDraw>();
    //            currentWorkingTexDraw.text = currentWorkingTexDraw.text.Insert(0, texValue);


    //            instanciateOnThisBlock = currentWorkingBlock;
    //            //Debug.Log("instanciateOnThisBlock.name: " + instanciateOnThisBlock.name);
    //            break;
    //    }

    //    // instanciateOnThisBlock = currentWorkingBlock;
    //}

    void NumberClicked(int number)
    {
        if (isActive)
        {
            StopblinkingCursor();
            valuePressed = true;
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

            StartBlinkingCursor();
        }

    }


    void ExpressionClicked(string exp)
    {
        if (isActive)
        {
            StopblinkingCursor();
            switch (Functionmode)
            {
                case modeBasic:
                    Debug.Log("Funtion Mode:");
                    break;
                case modeLCM:
                    Debug.Log("Lcm Mode");
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
                    Debug.Log("HCF Mode");
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
                    Debug.Log("Default Mode:");
                    if (currentWorkingBlock.GetComponent<TexDrawUtils>().multiDigitAllowed && currentWorkingBlock.GetComponent<TexDrawUtils>().numberOfDigit > 0)
                    {
                    }
                    else
                        currentWorkingTexDraw.text = currentWorkingTexDraw.text.Remove(currentWorkingTexDraw.text.Length - 1);
                    currentWorkingBlock.GetComponent<TexDrawUtils>().numberOfDigit++;
                    currentWorkingTexDraw.text = currentWorkingTexDraw.text.Insert(currentWorkingTexDraw.text.Length, exp);
                    if (currentWorkingBlock.GetComponent<TexDrawUtils>().multiDigitAllowed)
                    {
                        // currentWorkingTexDraw.text = currentWorkingTexDraw.text.Insert(currentWorkingTexDraw.text.Length, " ");
                    }
                    else
                    {
                        // Debug.Log("instanciateOnThisBlock.name: " + instanciateOnThisBlock.name + " :currentWorkingBlock.name: " + currentWorkingBlock.name);
                        if (string.Compare(instanciateOnThisBlock.name, currentWorkingBlock.name) == 0)
                            getNewCurrentWorkingTex();
                    }
                    break;
            }
            StartBlinkingCursor();
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

                    NextLine();
                    getNewCurrentWorkingTex();
                    currentWorkingTexDraw.text = currentWorkingTexDraw.text.Remove(currentWorkingTexDraw.text.Length - 1);
                    currentWorkingTexDraw.text = currentWorkingTexDraw.text.Insert(currentWorkingTexDraw.text.Length, "+");
                    getNewCurrentWorkingTex();
                    texValue = defaultvalue;
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
                    GameObject temp = this.gameObject;//currentRow.transform.GetChild(indexofWorkingArea).gameObject;
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
            CursorBlock = currentWorkingBlock;
            cursorTexDraw = currentWorkingTexDraw;
            StartBlinkingCursor();
        }

    }

    void NextLine()
    {
        if (isActive)
        {
            currentWorkingBlock.GetComponent<TexDrawUtils>().notClickable = true;
            GameObject temp = this.gameObject;//currentRow.transform.GetChild(indexofWorkingArea).gameObject;
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
                Debug.Log("prevNoofColumns: " + prevNoofColumns + " currentNoofColumns: " + currentNoofColumns);
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
    
    void NextStep()
    {
        if (isActive)
        {
            StopblinkingCursor();
            Debug.Log("move to main canvas");
            StartBlinkingCursor();
        }

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
                    Debug.Log("answer HCF");
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
                    answerPressedCounter++;
                    if (answerPressedCounter == 1)
                    {
                        NextLine();
                        for (int i = 0; i < prevNoofColumns; i++)
                        {
                            texValue = "-";
                            getNewCurrentWorkingTex();
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
            StopblinkingCursor();
            textBoxTypeMode = modeMixedFraction;
            getNewCurrentWorkingTex();
            StartBlinkingCursor();
        }

    }

    void AddFraction()
    {
        if (isActive)
        {
            StopblinkingCursor();
            textBoxTypeMode = modeFraction;
            getNewCurrentWorkingTex();
            StartBlinkingCursor();
        }

    }

    void Delete()
    {
        if (isActive)
        {
            StopblinkingCursor();

            GameObject temp = this.gameObject;//currentRow.transform.GetChild(indexofWorkingArea).gameObject;
            if (string.Compare(instanciateOnThisBlock.name, currentWorkingBlock.name) == 0)
            {

                if (currentNoofColumns > 1)
                {
                    Destroy(currentWorkingBlock);
                    currentWorkingBlock = temp.transform.GetChild(currentWorkingBlock.transform.GetSiblingIndex() + 1).gameObject;
                    currentWorkingTexDraw = currentWorkingBlock.GetComponent<TEXDraw>();
                    instanciateOnThisBlock = currentWorkingBlock;
                    intTestNo--;
                    currentNoofColumns--;
                    if (currentWorkingBlock.GetComponent<TexDrawUtils>().numberOfDigit > 0)
                    {
                        if (currentWorkingBlock.GetComponent<TexDrawUtils>().multiDigitAllowed)
                            currentWorkingTexDraw.text = currentWorkingTexDraw.text.Remove(currentWorkingTexDraw.text.Length - 2);
                        else
                            currentWorkingTexDraw.text = currentWorkingTexDraw.text.Remove(currentWorkingTexDraw.text.Length - 1);

                        currentWorkingBlock.GetComponent<TexDrawUtils>().numberOfDigit--;
                        currentWorkingTexDraw.text = currentWorkingTexDraw.text.Insert(currentWorkingTexDraw.text.Length, " ");

                    }
                }

            }
            else
            {
                if (currentWorkingBlock.GetComponent<TexDrawUtils>().numberOfDigit > 0)
                {
                    if (currentWorkingBlock.GetComponent<TexDrawUtils>().multiDigitAllowed)
                        currentWorkingTexDraw.text = currentWorkingTexDraw.text.Remove(currentWorkingTexDraw.text.Length - 2);
                    else
                        currentWorkingTexDraw.text = currentWorkingTexDraw.text.Remove(currentWorkingTexDraw.text.Length - 1);

                    currentWorkingBlock.GetComponent<TexDrawUtils>().numberOfDigit--;
                    currentWorkingTexDraw.text = currentWorkingTexDraw.text.Insert(currentWorkingTexDraw.text.Length, " ");

                }

            }
            StartBlinkingCursor();
        }

    }

    public void OnClickedOnATexDraw(GameObject obj)
    {
        if (isActive)
        {
            StopblinkingCursor();
            if (!obj.GetComponent<TexDrawUtils>().notClickable)
            {
                currentWorkingBlock = obj;
                currentWorkingTexDraw = currentWorkingBlock.GetComponent<TEXDraw>();
                //Debug.Log("clicked on: " + currentWorkingBlock.name);
                selectedBypointer = true;
                //noofvaluesafterselect = 0;
            }

            StartBlinkingCursor();
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
            CursorBlock = currentWorkingBlock;
            cursorTexDraw = currentWorkingTexDraw;
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
            CursorBlock = currentWorkingBlock;
            cursorTexDraw = currentWorkingTexDraw;
            StartBlinkingCursor();
        }

    }

    void Divide()
    {
        if (isActive)
        {
            StopblinkingCursor();
            Debug.Log("Divide");
            NextLine();
            getNewCurrentWorkingTex();
            currentWorkingBlock.GetComponent<TexDrawUtils>().multiDigitAllowed = true;
            currentWorkingBlock.GetComponent<TexDrawUtils>().lcmFactor = true;
            getNewCurrentWorkingTex();
            Functionmode = modeDivision;
            texValue = UpLine;
            currentWorkingTexDraw.text = currentWorkingTexDraw.text.Insert(0, "\\vborder[1001 black]");
            CursorBlock = currentWorkingBlock;
            cursorTexDraw = currentWorkingTexDraw;
            StartBlinkingCursor();
        }

    }

    void Addition()
    {
        if (isActive)
        {
            StopblinkingCursor();
            Debug.Log("Add");
            Functionmode = modeAddition;
            currentWorkingBlock.GetComponent<TexDrawUtils>().multiDigitAllowed = true;
            NextLine();
            //getNewCurrentWorkingTex();
            currentWorkingBlock.GetComponent<TexDrawUtils>().multiDigitAllowed = true;
            NextLine();
            //currentWorkingBlock.GetComponent<TexDrawUtils>().lcmFactor = true;
            //getNewCurrentWorkingTex();

            //texValue = UpLine;
            //currentWorkingTexDraw.text = currentWorkingTexDraw.text.Insert(0, "+");
            //getNewCurrentWorkingTex();
            getNewCurrentWorkingTex();
            CursorBlock = currentWorkingBlock;
            cursorTexDraw = currentWorkingTexDraw;
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
