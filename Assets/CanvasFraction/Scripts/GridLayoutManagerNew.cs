
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GridLayoutManagerNew : MonoBehaviour
{

    // Use this for initialization
    public GameObject prefabTex;
    GameObject nextTex, lastTex;
    int currColCount, prevColCount, noOfRows, intTestNo = 0, cursorVarCount = 20;
    VariableGridLayoutGroup mVariableGridLayout;
    bool add = false, ans = false, drawAnsLine = false, odd = false, sub = false, selectedBypointer = false, carryPosition = false, mul = false, leavextraLine = false, multiplyAnserFirst = false, multiAnsNextLine = false;
    bool lcm = false,lcmFactor = false,divide = false , cmm = false,pfm = false;
    bool blinkCursor = false, lcmAnswerFirst = false,divideFirstLine = false;
    string ansLine = "-", cursorVar = "\\vborder[1000 black]";//"\\vborder[0001 black] ",;
    int ansStartPoint = 0, CarryPoint = 2, noofvaluesafterselect = 0, noOfTimesAnsPressed = 0,divideLineNumber = 0; //20;
    int noofDidits, maxnoofdigits, value, lcmDigits = 0, prevlcmDigits = 0, lcmFactorDigits = 0 ;
    private string lastInstanciatedValueName;
    private IEnumerator blinkCoroutine;
    private void OnEnable()
    {
        TexDrawUtils.OnTexDrawClickedAction += OnClickedOnATexDraw;
    }

    private void OnDisable()
    {
        TexDrawUtils.OnTexDrawClickedAction -= OnClickedOnATexDraw;
    }

    void Start()
    {
        mVariableGridLayout = GetComponent<VariableGridLayoutGroup>();
        currColCount = 0;
        prevColCount = 0;
        noofDidits = 0;
        maxnoofdigits = 0;
        lcmDigits = 0;
        prevlcmDigits = 0;
        value = 0;
        noOfRows = 0;
        noOfTimesAnsPressed = 0;
        lcmFactorDigits = 0;
        divideLineNumber = 0;
        ans = false;
        mul = false;
        lcm = false;
        sub = false;
        odd = false;
        divide = false;
        cmm = false;
        pfm = false;
        divideFirstLine = false;
        lcmAnswerFirst = false;
        lcmFactor = false;
        multiAnsNextLine = false;
        leavextraLine = false;
        multiplyAnserFirst = false;
        noofvaluesafterselect = 0;
        carryPosition = false;
        selectedBypointer = false;
        drawAnsLine = false;
        InstanciateNextTexDraw();
        nextTex.GetComponent<TexDrawUtils>().multiDigitAllowed = true;
        pressedNext();
        nextTex.GetComponent<TexDrawUtils>().multiDigitAllowed = true;
        // for 2 lines:
        InstanciateNextTexDraw();
        nextTex.GetComponent<TexDrawUtils>().multiDigitAllowed = true;
        pressedNext();
        StartBlinkingCursor();
    }
    /*
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                // whatever tag you are looking for on your game object
                if (hit.collider.tag == "TexDraw")
                {
                    Debug.Log("---> Hit: " + hit.collider.gameObject.name);
                }
            }
        }
    }*/
    void InstanciateNextTexDraw()
    {

        currColCount++;
        if (mVariableGridLayout.constraintCount < currColCount)
        {
            mVariableGridLayout.constraintCount++;
            GameObject child;
            /* if (noOfRows < 3)
             {*/
            for (int i = 1; i <= noOfRows; i++)
            {
                child = (GameObject)Instantiate(prefabTex, transform);
                child.GetComponent<TEXDraw>().text = child.GetComponent<TEXDraw>().text.Insert(0, " ");
                intTestNo++;
                child.name = "Child" + intTestNo.ToString();
                if (i < 3)
                {
                    child.GetComponent<TexDrawUtils>().multiDigitAllowed = true;
                    child.GetComponent<TexDrawUtils>().numberOfDigit = 0;
                }
                if (lcm)
                {
                    child.transform.SetSiblingIndex((i-1) * currColCount);
                }
                else
                {
                    if (i < 4)
                        child.transform.SetSiblingIndex((i * currColCount) - 1);
                    else
                        child.transform.SetSiblingIndex((i * currColCount) - 2);
                }
                
                //if(ans && i==noOfRows)
                //child.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, ansLine);
                //child.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "l");
            }
            /*}
              else
              {
                  for (int i = 1; i <= noOfRows; i++)
                  {
                      child = (GameObject)Instantiate(prefabTex, transform);

                      intTestNo++;
                      child.name = "Child" + intTestNo.ToString();
                      child.transform.SetSiblingIndex((i * currColCount) - 2);
                      //child.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "l");
                  }
              }*/
        }
        if (lcmFactor)
        {
            lcmFactorDigits = nextTex.transform.GetSiblingIndex();
        }
        nextTex = (GameObject)Instantiate(prefabTex, transform);
        nextTex.GetComponent<TexDrawUtils>().multiDigitAllowed = false;
        nextTex.GetComponent<TexDrawUtils>().numberOfDigit = 0;
        if (drawAnsLine)
            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, ansLine);
        else
        {
            if (divide)
            {
                Debug.Log("Divide");
                if (divideFirstLine)
                {
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "\\vborder[0001 black] ");
                }else if(divideLineNumber%2 != 0)
                {
                    Debug.Log("here");
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "\\vborder[0100 black] ");
                }
            }
            if (lcm)
            {
                if (lcmFactor)
                {
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, " ");
                }
                else
                {
                    if (ans)
                    {
                        //Debug.Log(" NO LINE");
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, " ");
                    }
                    else
                    {
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "\\vborder[0100 black] ");
                        //Debug.Log("  LINE");
                    }
                    
                }
            }
            else
            {
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, " ");
            }
            
        }
            
        intTestNo++;
        nextTex.name = "newTex" + intTestNo.ToString();
        lastInstanciatedValueName = nextTex.name;
        if (lcmFactor)
        {
            nextTex.transform.SetSiblingIndex(lcmFactorDigits);
        }
        else
        {
            if (ans && !multiAnsNextLine)
            {
                //Debug.Log("OR here");
                if (lcm)
                {
                    if (currColCount > 1)
                    {
                        //Debug.Log("noOfRows: " + noOfRows + " currColCount: " + currColCount);
                        //Debug.Log("here");
                        nextTex.transform.SetSiblingIndex((noOfRows) * mVariableGridLayout.constraintCount);//currColCount);
                    }
                }
                else
                {
                    nextTex.transform.SetSiblingIndex(transform.childCount - 3);
                }
                
            }
            else
            {
                if (currColCount > 1)
                {
                    //Debug.Log("noOfRows: " + noOfRows + " currColCount: " + currColCount);
                    //Debug.Log("here");
                    nextTex.transform.SetSiblingIndex((noOfRows) * mVariableGridLayout.constraintCount);//currColCount);
                }
            }
        }
    }
    public void pressed1()
    {
        
        StopblinkingCursor();
        string charValue = "1";
        if (selectedBypointer)
        {
           
            if (cmm)
            {
                lcm = true;
                lcmFactor = true;
            }
            if (lcm)
            {
                if (!lcmFactor)
                {
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                    //nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                    selectedBypointer = false;
                    //lcmDigits++;
                    if (nextTex.transform.GetSiblingIndex() == (transform.childCount - currColCount))
                    {
                        InstanciateNextTexDraw();
                        noofDidits++;
                        Debug.Log("Instanciate new variable");
                    }
                    else
                    {
                        nextTex = transform.GetChild(nextTex.transform.GetSiblingIndex() - 1).gameObject;
                    }

                }
                else
                {
                    Debug.Log("LCM Factor");
                    if (noofvaluesafterselect == 0)
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                    nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                    noofvaluesafterselect++;
                }
            }
            else
            {
                if (!carryPosition)
                {
                    Debug.Log("carry lcmFactor: " + lcmFactor);
                    if (divide && lcmFactor)
                    {
                        if (noofvaluesafterselect == 0)
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                        nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                        noofvaluesafterselect++;
                    }
                    else if (noofvaluesafterselect < 1)
                    {
                        Debug.Log("not carry ");
                        if (divide)
                        {
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                            nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                            //lcmDigits++;
                            value += 1 + (int)Mathf.Pow(10, noofDidits);
                            noofDidits++;
                            if ((nextTex.transform.GetSiblingIndex() % prevColCount) > 1)
                                nextTex = transform.GetChild(nextTex.transform.GetSiblingIndex() - 1).gameObject;
                            selectedBypointer = false;
                        }
                        else
                        {
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, charValue);
                            noofvaluesafterselect++;
                            nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                            selectedBypointer = false;
                        }
                            
                    }
                    else
                    {
                        Debug.Log("can not add more than 1 digit");
                    }

                }
                else
                {


                    if (noofvaluesafterselect == 0)
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                    nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                    noofvaluesafterselect++;

                }
                if (string.Compare(nextTex.name, lastInstanciatedValueName) == 0)
                {
                    noofvaluesafterselect = 0;
                    InstanciateNextTexDraw();
                }
            }
        }
        else
        {
            if (divide)
            {
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                //lcmDigits++;
                value += 1 + (int)Mathf.Pow(10, noofDidits);
                noofDidits++;
            }
            if (cmm)
            {
                lcm = true;
                lcmFactor = true;
            }
            if (lcm)
            {
                if (!lcmFactor)
                {
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                    nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                    lcmDigits++;
                    value += 1 + (int)Mathf.Pow(10, noofDidits);
                    noofDidits++;
                }
            }
            else
            {
                if (!divide)
                {
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, charValue);
                    nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                }
                
            }
            if (ans)
            {
                if (odd)
                {
                    value += 1 + (int)Mathf.Pow(10, noofDidits);
                    noofDidits++;
                    InstanciateNextTexDraw();
                    //Debug.Log("prevColCount: " + prevColCount + " CarryPoint: " + CarryPoint);

                    /*
                    if (CarryPoint < maxnoofdigits + 1)
                    {
                        odd = false;
                        nextTex = transform.GetChild(CarryPoint++).gameObject;
                    }
                    else
                    {*/
                    odd = true;
                    //nextTex = transform.GetChild(transform.childCount - 3).gameObject;
                    //}

                }
                else
                {
                    if (!lcm)
                    {
                        odd = true;
                        nextTex = transform.GetChild(transform.childCount - 3).gameObject;
                    }
                    else
                    {
                        if (cmm)
                        {
                            if (nextTex.GetComponent<TexDrawUtils>().numberOfDigit < 1)
                                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                            nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                            lcmFactorDigits++;
                        }
                        else
                        {
                            Debug.Log("LCM Answer");
                            noofDidits++;
                            InstanciateNextTexDraw();
                        }

                    }

                }
            }
            else
            {

                if (lcm)
                {
                    if (lcmFactor)
                    {
                        // this block never gets executed
                        //Debug.Log("LCM Factor");
                        //InstanciateNextTexDraw();
                        Debug.Log("cmm LCM Factor nextTex: " + nextTex.name);
                        if (nextTex.GetComponent<TexDrawUtils>().numberOfDigit < 1)
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                        nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                        lcmFactorDigits++;
                    }
                    else
                    {
                        if (nextTex.transform.GetSiblingIndex() == (transform.childCount - currColCount))
                        {
                            InstanciateNextTexDraw();
                            noofDidits++;
                            //Debug.Log("Instanciate new variable");
                        }
                        else
                        {
                            nextTex = transform.GetChild(nextTex.transform.GetSiblingIndex() - 1).gameObject;
                        }

                    }


                }
                else
                {
                    if (divide)
                    {
                        if (divideFirstLine)
                        {
                            value = (value * 10) + 1;
                            noofDidits++;
                            InstanciateNextTexDraw();
                        }
                        else
                        {
                            value = (value * 10) + 1;
                            noofDidits++;
                            Debug.Log("index: " + (nextTex.transform.GetSiblingIndex() % prevColCount));
                            if((nextTex.transform.GetSiblingIndex() % prevColCount)>1)
                                nextTex = transform.GetChild(nextTex.transform.GetSiblingIndex()-1).gameObject;
                        }
                    }
                    else
                    {
                        value = (value * 10) + 1;
                        noofDidits++;
                        InstanciateNextTexDraw();
                    }
                    
                }

            }
        }
        StartBlinkingCursor();
    }
    public void pressed2()
    {
        StopblinkingCursor();
        string charValue = "2";
        if (selectedBypointer)
        {

            if (cmm)
            {
                lcm = true;
                lcmFactor = true;
            }
            if (lcm)
            {
                if (!lcmFactor)
                {
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                    //nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                    selectedBypointer = false;
                    //lcmDigits++;
                    if (nextTex.transform.GetSiblingIndex() == (transform.childCount - currColCount))
                    {
                        InstanciateNextTexDraw();
                        noofDidits++;
                        Debug.Log("Instanciate new variable");
                    }
                    else
                    {
                        nextTex = transform.GetChild(nextTex.transform.GetSiblingIndex() - 1).gameObject;
                    }

                }
                else
                {
                    Debug.Log("LCM Factor");
                    if (noofvaluesafterselect == 0)
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                    nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                    noofvaluesafterselect++;
                }
            }
            else
            {
                if (!carryPosition)
                {
                    Debug.Log("carry lcmFactor: " + lcmFactor);
                    if (divide && lcmFactor)
                    {
                        if (noofvaluesafterselect == 0)
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                        nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                        noofvaluesafterselect++;
                    }
                    else if (noofvaluesafterselect < 1)
                    {
                        Debug.Log("not carry ");
                        if (divide)
                        {
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                            nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                            //lcmDigits++;
                            value += 1 + (int)Mathf.Pow(10, noofDidits);
                            noofDidits++;
                            if ((nextTex.transform.GetSiblingIndex() % prevColCount) > 1)
                                nextTex = transform.GetChild(nextTex.transform.GetSiblingIndex() - 1).gameObject;
                            selectedBypointer = false;
                        }
                        else
                        {
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, charValue);
                            noofvaluesafterselect++;
                            nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                            selectedBypointer = false;
                        }

                    }
                    else
                    {
                        Debug.Log("can not add more than 1 digit");
                    }

                }
                else
                {


                    if (noofvaluesafterselect == 0)
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                    nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                    noofvaluesafterselect++;

                }
                if (string.Compare(nextTex.name, lastInstanciatedValueName) == 0)
                {
                    noofvaluesafterselect = 0;
                    InstanciateNextTexDraw();
                }
            }
        }
        else
        {
            if (divide)
            {
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                //lcmDigits++;
                value += 1 + (int)Mathf.Pow(10, noofDidits);
                noofDidits++;
            }
            if (cmm)
            {
                lcm = true;
                lcmFactor = true;
            }
            if (lcm)
            {
                if (!lcmFactor)
                {
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                    nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                    lcmDigits++;
                    value += 1 + (int)Mathf.Pow(10, noofDidits);
                    noofDidits++;
                }
            }
            else
            {
                if (!divide)
                {
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, charValue);
                    nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                }

            }
            if (ans)
            {
                if (odd)
                {
                    value += 1 + (int)Mathf.Pow(10, noofDidits);
                    noofDidits++;
                    InstanciateNextTexDraw();
                    //Debug.Log("prevColCount: " + prevColCount + " CarryPoint: " + CarryPoint);

                    /*
                    if (CarryPoint < maxnoofdigits + 1)
                    {
                        odd = false;
                        nextTex = transform.GetChild(CarryPoint++).gameObject;
                    }
                    else
                    {*/
                    odd = true;
                    //nextTex = transform.GetChild(transform.childCount - 3).gameObject;
                    //}

                }
                else
                {
                    if (!lcm)
                    {
                        odd = true;
                        nextTex = transform.GetChild(transform.childCount - 3).gameObject;
                    }
                    else
                    {
                        if (cmm)
                        {
                            if (nextTex.GetComponent<TexDrawUtils>().numberOfDigit < 1)
                                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                            nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                            lcmFactorDigits++;
                        }
                        else
                        {
                            Debug.Log("LCM Answer");
                            noofDidits++;
                            InstanciateNextTexDraw();
                        }

                    }

                }
            }
            else
            {

                if (lcm)
                {
                    if (lcmFactor)
                    {
                        // this block never gets executed
                        //Debug.Log("LCM Factor");
                        //InstanciateNextTexDraw();
                        Debug.Log("cmm LCM Factor nextTex: " + nextTex.name);
                        if (nextTex.GetComponent<TexDrawUtils>().numberOfDigit < 1)
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                        nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                        lcmFactorDigits++;
                    }
                    else
                    {
                        if (nextTex.transform.GetSiblingIndex() == (transform.childCount - currColCount))
                        {
                            InstanciateNextTexDraw();
                            noofDidits++;
                            //Debug.Log("Instanciate new variable");
                        }
                        else
                        {
                            nextTex = transform.GetChild(nextTex.transform.GetSiblingIndex() - 1).gameObject;
                        }

                    }


                }
                else
                {
                    if (divide)
                    {
                        if (divideFirstLine)
                        {
                            value = (value * 10) + 1;
                            noofDidits++;
                            InstanciateNextTexDraw();
                        }
                        else
                        {
                            value = (value * 10) + 1;
                            noofDidits++;
                            Debug.Log("index: " + (nextTex.transform.GetSiblingIndex() % prevColCount));
                            if ((nextTex.transform.GetSiblingIndex() % prevColCount) > 1)
                                nextTex = transform.GetChild(nextTex.transform.GetSiblingIndex() - 1).gameObject;
                        }
                    }
                    else
                    {
                        value = (value * 10) + 1;
                        noofDidits++;
                        InstanciateNextTexDraw();
                    }

                }

            }
        }
        StartBlinkingCursor();
    }
    public void pressed3()
    {
        StopblinkingCursor();
        string charValue = "3";
        if (selectedBypointer)
        {

            if (cmm)
            {
                lcm = true;
                lcmFactor = true;
            }
            if (lcm)
            {
                if (!lcmFactor)
                {
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                    //nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                    selectedBypointer = false;
                    //lcmDigits++;
                    if (nextTex.transform.GetSiblingIndex() == (transform.childCount - currColCount))
                    {
                        InstanciateNextTexDraw();
                        noofDidits++;
                        Debug.Log("Instanciate new variable");
                    }
                    else
                    {
                        nextTex = transform.GetChild(nextTex.transform.GetSiblingIndex() - 1).gameObject;
                    }

                }
                else
                {
                    Debug.Log("LCM Factor");
                    if (noofvaluesafterselect == 0)
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                    nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                    noofvaluesafterselect++;
                }
            }
            else
            {
                if (!carryPosition)
                {
                    Debug.Log("carry lcmFactor: " + lcmFactor);
                    if (divide && lcmFactor)
                    {
                        if (noofvaluesafterselect == 0)
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                        nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                        noofvaluesafterselect++;
                    }
                    else if (noofvaluesafterselect < 1)
                    {
                        Debug.Log("not carry ");
                        if (divide)
                        {
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                            nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                            //lcmDigits++;
                            value += 1 + (int)Mathf.Pow(10, noofDidits);
                            noofDidits++;
                            if ((nextTex.transform.GetSiblingIndex() % prevColCount) > 1)
                                nextTex = transform.GetChild(nextTex.transform.GetSiblingIndex() - 1).gameObject;
                            selectedBypointer = false;
                        }
                        else
                        {
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, charValue);
                            noofvaluesafterselect++;
                            nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                            selectedBypointer = false;
                        }

                    }
                    else
                    {
                        Debug.Log("can not add more than 1 digit");
                    }

                }
                else
                {


                    if (noofvaluesafterselect == 0)
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                    nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                    noofvaluesafterselect++;

                }
                if (string.Compare(nextTex.name, lastInstanciatedValueName) == 0)
                {
                    noofvaluesafterselect = 0;
                    InstanciateNextTexDraw();
                }
            }
        }
        else
        {
            if (divide)
            {
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                //lcmDigits++;
                value += 1 + (int)Mathf.Pow(10, noofDidits);
                noofDidits++;
            }
            if (cmm)
            {
                lcm = true;
                lcmFactor = true;
            }
            if (lcm)
            {
                if (!lcmFactor)
                {
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                    nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                    lcmDigits++;
                    value += 1 + (int)Mathf.Pow(10, noofDidits);
                    noofDidits++;
                }
            }
            else
            {
                if (!divide)
                {
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, charValue);
                    nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                }

            }
            if (ans)
            {
                if (odd)
                {
                    value += 1 + (int)Mathf.Pow(10, noofDidits);
                    noofDidits++;
                    InstanciateNextTexDraw();
                    //Debug.Log("prevColCount: " + prevColCount + " CarryPoint: " + CarryPoint);

                    /*
                    if (CarryPoint < maxnoofdigits + 1)
                    {
                        odd = false;
                        nextTex = transform.GetChild(CarryPoint++).gameObject;
                    }
                    else
                    {*/
                    odd = true;
                    //nextTex = transform.GetChild(transform.childCount - 3).gameObject;
                    //}

                }
                else
                {
                    if (!lcm)
                    {
                        odd = true;
                        nextTex = transform.GetChild(transform.childCount - 3).gameObject;
                    }
                    else
                    {
                        if (cmm)
                        {
                            if (nextTex.GetComponent<TexDrawUtils>().numberOfDigit < 1)
                                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                            nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                            lcmFactorDigits++;
                        }
                        else
                        {
                            Debug.Log("LCM Answer");
                            noofDidits++;
                            InstanciateNextTexDraw();
                        }

                    }

                }
            }
            else
            {

                if (lcm)
                {
                    if (lcmFactor)
                    {
                        // this block never gets executed
                        //Debug.Log("LCM Factor");
                        //InstanciateNextTexDraw();
                        Debug.Log("cmm LCM Factor nextTex: " + nextTex.name);
                        if (nextTex.GetComponent<TexDrawUtils>().numberOfDigit < 1)
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                        nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                        lcmFactorDigits++;
                    }
                    else
                    {
                        if (nextTex.transform.GetSiblingIndex() == (transform.childCount - currColCount))
                        {
                            InstanciateNextTexDraw();
                            noofDidits++;
                            //Debug.Log("Instanciate new variable");
                        }
                        else
                        {
                            nextTex = transform.GetChild(nextTex.transform.GetSiblingIndex() - 1).gameObject;
                        }

                    }


                }
                else
                {
                    if (divide)
                    {
                        if (divideFirstLine)
                        {
                            value = (value * 10) + 1;
                            noofDidits++;
                            InstanciateNextTexDraw();
                        }
                        else
                        {
                            value = (value * 10) + 1;
                            noofDidits++;
                            Debug.Log("index: " + (nextTex.transform.GetSiblingIndex() % prevColCount));
                            if ((nextTex.transform.GetSiblingIndex() % prevColCount) > 1)
                                nextTex = transform.GetChild(nextTex.transform.GetSiblingIndex() - 1).gameObject;
                        }
                    }
                    else
                    {
                        value = (value * 10) + 1;
                        noofDidits++;
                        InstanciateNextTexDraw();
                    }

                }

            }
        }
        StartBlinkingCursor();
    }
    public void pressed4()
    {
        StopblinkingCursor();
        string charValue = "4";
        if (selectedBypointer)
        {

            if (cmm)
            {
                lcm = true;
                lcmFactor = true;
            }
            if (lcm)
            {
                if (!lcmFactor)
                {
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                    //nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                    selectedBypointer = false;
                    //lcmDigits++;
                    if (nextTex.transform.GetSiblingIndex() == (transform.childCount - currColCount))
                    {
                        InstanciateNextTexDraw();
                        noofDidits++;
                        Debug.Log("Instanciate new variable");
                    }
                    else
                    {
                        nextTex = transform.GetChild(nextTex.transform.GetSiblingIndex() - 1).gameObject;
                    }

                }
                else
                {
                    Debug.Log("LCM Factor");
                    if (noofvaluesafterselect == 0)
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                    nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                    noofvaluesafterselect++;
                }
            }
            else
            {
                if (!carryPosition)
                {
                    Debug.Log("carry lcmFactor: " + lcmFactor);
                    if (divide && lcmFactor)
                    {
                        if (noofvaluesafterselect == 0)
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                        nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                        noofvaluesafterselect++;
                    }
                    else if (noofvaluesafterselect < 1)
                    {
                        Debug.Log("not carry ");
                        if (divide)
                        {
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                            nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                            //lcmDigits++;
                            value += 1 + (int)Mathf.Pow(10, noofDidits);
                            noofDidits++;
                            if ((nextTex.transform.GetSiblingIndex() % prevColCount) > 1)
                                nextTex = transform.GetChild(nextTex.transform.GetSiblingIndex() - 1).gameObject;
                            selectedBypointer = false;
                        }
                        else
                        {
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, charValue);
                            noofvaluesafterselect++;
                            nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                            selectedBypointer = false;
                        }

                    }
                    else
                    {
                        Debug.Log("can not add more than 1 digit");
                    }

                }
                else
                {


                    if (noofvaluesafterselect == 0)
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                    nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                    noofvaluesafterselect++;

                }
                if (string.Compare(nextTex.name, lastInstanciatedValueName) == 0)
                {
                    noofvaluesafterselect = 0;
                    InstanciateNextTexDraw();
                }
            }
        }
        else
        {
            if (divide)
            {
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                //lcmDigits++;
                value += 1 + (int)Mathf.Pow(10, noofDidits);
                noofDidits++;
            }
            if (cmm)
            {
                lcm = true;
                lcmFactor = true;
            }
            if (lcm)
            {
                if (!lcmFactor)
                {
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                    nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                    lcmDigits++;
                    value += 1 + (int)Mathf.Pow(10, noofDidits);
                    noofDidits++;
                }
            }
            else
            {
                if (!divide)
                {
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, charValue);
                    nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                }

            }
            if (ans)
            {
                if (odd)
                {
                    value += 1 + (int)Mathf.Pow(10, noofDidits);
                    noofDidits++;
                    InstanciateNextTexDraw();
                    //Debug.Log("prevColCount: " + prevColCount + " CarryPoint: " + CarryPoint);

                    /*
                    if (CarryPoint < maxnoofdigits + 1)
                    {
                        odd = false;
                        nextTex = transform.GetChild(CarryPoint++).gameObject;
                    }
                    else
                    {*/
                    odd = true;
                    //nextTex = transform.GetChild(transform.childCount - 3).gameObject;
                    //}

                }
                else
                {
                    if (!lcm)
                    {
                        odd = true;
                        nextTex = transform.GetChild(transform.childCount - 3).gameObject;
                    }
                    else
                    {
                        if (cmm)
                        {
                            if (nextTex.GetComponent<TexDrawUtils>().numberOfDigit < 1)
                                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                            nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                            lcmFactorDigits++;
                        }
                        else
                        {
                            Debug.Log("LCM Answer");
                            noofDidits++;
                            InstanciateNextTexDraw();
                        }

                    }

                }
            }
            else
            {

                if (lcm)
                {
                    if (lcmFactor)
                    {
                        // this block never gets executed
                        //Debug.Log("LCM Factor");
                        //InstanciateNextTexDraw();
                        Debug.Log("cmm LCM Factor nextTex: " + nextTex.name);
                        if (nextTex.GetComponent<TexDrawUtils>().numberOfDigit < 1)
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                        nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                        lcmFactorDigits++;
                    }
                    else
                    {
                        if (nextTex.transform.GetSiblingIndex() == (transform.childCount - currColCount))
                        {
                            InstanciateNextTexDraw();
                            noofDidits++;
                            //Debug.Log("Instanciate new variable");
                        }
                        else
                        {
                            nextTex = transform.GetChild(nextTex.transform.GetSiblingIndex() - 1).gameObject;
                        }

                    }


                }
                else
                {
                    if (divide)
                    {
                        if (divideFirstLine)
                        {
                            value = (value * 10) + 1;
                            noofDidits++;
                            InstanciateNextTexDraw();
                        }
                        else
                        {
                            value = (value * 10) + 1;
                            noofDidits++;
                            Debug.Log("index: " + (nextTex.transform.GetSiblingIndex() % prevColCount));
                            if ((nextTex.transform.GetSiblingIndex() % prevColCount) > 1)
                                nextTex = transform.GetChild(nextTex.transform.GetSiblingIndex() - 1).gameObject;
                        }
                    }
                    else
                    {
                        value = (value * 10) + 1;
                        noofDidits++;
                        InstanciateNextTexDraw();
                    }

                }

            }
        }
        StartBlinkingCursor();
    }
    public void pressed5()
    {
        StopblinkingCursor();
        string charValue = "5";
        if (selectedBypointer)
        {

            if (cmm)
            {
                lcm = true;
                lcmFactor = true;
            }
            if (lcm)
            {
                if (!lcmFactor)
                {
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                    //nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                    selectedBypointer = false;
                    //lcmDigits++;
                    if (nextTex.transform.GetSiblingIndex() == (transform.childCount - currColCount))
                    {
                        InstanciateNextTexDraw();
                        noofDidits++;
                        Debug.Log("Instanciate new variable");
                    }
                    else
                    {
                        nextTex = transform.GetChild(nextTex.transform.GetSiblingIndex() - 1).gameObject;
                    }

                }
                else
                {
                    Debug.Log("LCM Factor");
                    if (noofvaluesafterselect == 0)
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                    nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                    noofvaluesafterselect++;
                }
            }
            else
            {
                if (!carryPosition)
                {
                    Debug.Log("carry lcmFactor: " + lcmFactor);
                    if (divide && lcmFactor)
                    {
                        if (noofvaluesafterselect == 0)
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                        nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                        noofvaluesafterselect++;
                    }
                    else if (noofvaluesafterselect < 1)
                    {
                        Debug.Log("not carry ");
                        if (divide)
                        {
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                            nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                            //lcmDigits++;
                            value += 1 + (int)Mathf.Pow(10, noofDidits);
                            noofDidits++;
                            if ((nextTex.transform.GetSiblingIndex() % prevColCount) > 1)
                                nextTex = transform.GetChild(nextTex.transform.GetSiblingIndex() - 1).gameObject;
                            selectedBypointer = false;
                        }
                        else
                        {
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, charValue);
                            noofvaluesafterselect++;
                            nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                            selectedBypointer = false;
                        }

                    }
                    else
                    {
                        Debug.Log("can not add more than 1 digit");
                    }

                }
                else
                {


                    if (noofvaluesafterselect == 0)
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                    nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                    noofvaluesafterselect++;

                }
                if (string.Compare(nextTex.name, lastInstanciatedValueName) == 0)
                {
                    noofvaluesafterselect = 0;
                    InstanciateNextTexDraw();
                }
            }
        }
        else
        {
            if (divide)
            {
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                //lcmDigits++;
                value += 1 + (int)Mathf.Pow(10, noofDidits);
                noofDidits++;
            }
            if (cmm)
            {
                lcm = true;
                lcmFactor = true;
            }
            if (lcm)
            {
                if (!lcmFactor)
                {
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                    nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                    lcmDigits++;
                    value += 1 + (int)Mathf.Pow(10, noofDidits);
                    noofDidits++;
                }
            }
            else
            {
                if (!divide)
                {
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, charValue);
                    nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                }

            }
            if (ans)
            {
                if (odd)
                {
                    value += 1 + (int)Mathf.Pow(10, noofDidits);
                    noofDidits++;
                    InstanciateNextTexDraw();
                    //Debug.Log("prevColCount: " + prevColCount + " CarryPoint: " + CarryPoint);

                    /*
                    if (CarryPoint < maxnoofdigits + 1)
                    {
                        odd = false;
                        nextTex = transform.GetChild(CarryPoint++).gameObject;
                    }
                    else
                    {*/
                    odd = true;
                    //nextTex = transform.GetChild(transform.childCount - 3).gameObject;
                    //}

                }
                else
                {
                    if (!lcm)
                    {
                        odd = true;
                        nextTex = transform.GetChild(transform.childCount - 3).gameObject;
                    }
                    else
                    {
                        if (cmm)
                        {
                            if (nextTex.GetComponent<TexDrawUtils>().numberOfDigit < 1)
                                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                            nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                            lcmFactorDigits++;
                        }
                        else
                        {
                            Debug.Log("LCM Answer");
                            noofDidits++;
                            InstanciateNextTexDraw();
                        }

                    }

                }
            }
            else
            {

                if (lcm)
                {
                    if (lcmFactor)
                    {
                        // this block never gets executed
                        //Debug.Log("LCM Factor");
                        //InstanciateNextTexDraw();
                        Debug.Log("cmm LCM Factor nextTex: " + nextTex.name);
                        if (nextTex.GetComponent<TexDrawUtils>().numberOfDigit < 1)
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                        nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                        lcmFactorDigits++;
                    }
                    else
                    {
                        if (nextTex.transform.GetSiblingIndex() == (transform.childCount - currColCount))
                        {
                            InstanciateNextTexDraw();
                            noofDidits++;
                            //Debug.Log("Instanciate new variable");
                        }
                        else
                        {
                            nextTex = transform.GetChild(nextTex.transform.GetSiblingIndex() - 1).gameObject;
                        }

                    }


                }
                else
                {
                    if (divide)
                    {
                        if (divideFirstLine)
                        {
                            value = (value * 10) + 1;
                            noofDidits++;
                            InstanciateNextTexDraw();
                        }
                        else
                        {
                            value = (value * 10) + 1;
                            noofDidits++;
                            Debug.Log("index: " + (nextTex.transform.GetSiblingIndex() % prevColCount));
                            if ((nextTex.transform.GetSiblingIndex() % prevColCount) > 1)
                                nextTex = transform.GetChild(nextTex.transform.GetSiblingIndex() - 1).gameObject;
                        }
                    }
                    else
                    {
                        value = (value * 10) + 1;
                        noofDidits++;
                        InstanciateNextTexDraw();
                    }

                }

            }
        }
        StartBlinkingCursor();
    }
    public void pressed6()
    {
        StopblinkingCursor();
        string charValue = "6";
        if (selectedBypointer)
        {

            if (cmm)
            {
                lcm = true;
                lcmFactor = true;
            }
            if (lcm)
            {
                if (!lcmFactor)
                {
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                    //nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                    selectedBypointer = false;
                    //lcmDigits++;
                    if (nextTex.transform.GetSiblingIndex() == (transform.childCount - currColCount))
                    {
                        InstanciateNextTexDraw();
                        noofDidits++;
                        Debug.Log("Instanciate new variable");
                    }
                    else
                    {
                        nextTex = transform.GetChild(nextTex.transform.GetSiblingIndex() - 1).gameObject;
                    }

                }
                else
                {
                    Debug.Log("LCM Factor");
                    if (noofvaluesafterselect == 0)
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                    nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                    noofvaluesafterselect++;
                }
            }
            else
            {
                if (!carryPosition)
                {
                    Debug.Log("carry lcmFactor: " + lcmFactor);
                    if (divide && lcmFactor)
                    {
                        if (noofvaluesafterselect == 0)
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                        nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                        noofvaluesafterselect++;
                    }
                    else if (noofvaluesafterselect < 1)
                    {
                        Debug.Log("not carry ");
                        if (divide)
                        {
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                            nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                            //lcmDigits++;
                            value += 1 + (int)Mathf.Pow(10, noofDidits);
                            noofDidits++;
                            if ((nextTex.transform.GetSiblingIndex() % prevColCount) > 1)
                                nextTex = transform.GetChild(nextTex.transform.GetSiblingIndex() - 1).gameObject;
                            selectedBypointer = false;
                        }
                        else
                        {
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, charValue);
                            noofvaluesafterselect++;
                            nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                            selectedBypointer = false;
                        }

                    }
                    else
                    {
                        Debug.Log("can not add more than 1 digit");
                    }

                }
                else
                {


                    if (noofvaluesafterselect == 0)
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                    nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                    noofvaluesafterselect++;

                }
                if (string.Compare(nextTex.name, lastInstanciatedValueName) == 0)
                {
                    noofvaluesafterselect = 0;
                    InstanciateNextTexDraw();
                }
            }
        }
        else
        {
            if (divide)
            {
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                //lcmDigits++;
                value += 1 + (int)Mathf.Pow(10, noofDidits);
                noofDidits++;
            }
            if (cmm)
            {
                lcm = true;
                lcmFactor = true;
            }
            if (lcm)
            {
                if (!lcmFactor)
                {
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                    nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                    lcmDigits++;
                    value += 1 + (int)Mathf.Pow(10, noofDidits);
                    noofDidits++;
                }
            }
            else
            {
                if (!divide)
                {
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, charValue);
                    nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                }

            }
            if (ans)
            {
                if (odd)
                {
                    value += 1 + (int)Mathf.Pow(10, noofDidits);
                    noofDidits++;
                    InstanciateNextTexDraw();
                    //Debug.Log("prevColCount: " + prevColCount + " CarryPoint: " + CarryPoint);

                    /*
                    if (CarryPoint < maxnoofdigits + 1)
                    {
                        odd = false;
                        nextTex = transform.GetChild(CarryPoint++).gameObject;
                    }
                    else
                    {*/
                    odd = true;
                    //nextTex = transform.GetChild(transform.childCount - 3).gameObject;
                    //}

                }
                else
                {
                    if (!lcm)
                    {
                        odd = true;
                        nextTex = transform.GetChild(transform.childCount - 3).gameObject;
                    }
                    else
                    {
                        if (cmm)
                        {
                            if (nextTex.GetComponent<TexDrawUtils>().numberOfDigit < 1)
                                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                            nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                            lcmFactorDigits++;
                        }
                        else
                        {
                            Debug.Log("LCM Answer");
                            noofDidits++;
                            InstanciateNextTexDraw();
                        }

                    }

                }
            }
            else
            {

                if (lcm)
                {
                    if (lcmFactor)
                    {
                        // this block never gets executed
                        //Debug.Log("LCM Factor");
                        //InstanciateNextTexDraw();
                        Debug.Log("cmm LCM Factor nextTex: " + nextTex.name);
                        if (nextTex.GetComponent<TexDrawUtils>().numberOfDigit < 1)
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                        nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                        lcmFactorDigits++;
                    }
                    else
                    {
                        if (nextTex.transform.GetSiblingIndex() == (transform.childCount - currColCount))
                        {
                            InstanciateNextTexDraw();
                            noofDidits++;
                            //Debug.Log("Instanciate new variable");
                        }
                        else
                        {
                            nextTex = transform.GetChild(nextTex.transform.GetSiblingIndex() - 1).gameObject;
                        }

                    }


                }
                else
                {
                    if (divide)
                    {
                        if (divideFirstLine)
                        {
                            value = (value * 10) + 1;
                            noofDidits++;
                            InstanciateNextTexDraw();
                        }
                        else
                        {
                            value = (value * 10) + 1;
                            noofDidits++;
                            Debug.Log("index: " + (nextTex.transform.GetSiblingIndex() % prevColCount));
                            if ((nextTex.transform.GetSiblingIndex() % prevColCount) > 1)
                                nextTex = transform.GetChild(nextTex.transform.GetSiblingIndex() - 1).gameObject;
                        }
                    }
                    else
                    {
                        value = (value * 10) + 1;
                        noofDidits++;
                        InstanciateNextTexDraw();
                    }

                }

            }
        }
        StartBlinkingCursor();
    }
    public void pressed7()
    {
        StopblinkingCursor();
        string charValue = "7";
        if (selectedBypointer)
        {

            if (cmm)
            {
                lcm = true;
                lcmFactor = true;
            }
            if (lcm)
            {
                if (!lcmFactor)
                {
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                    //nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                    selectedBypointer = false;
                    //lcmDigits++;
                    if (nextTex.transform.GetSiblingIndex() == (transform.childCount - currColCount))
                    {
                        InstanciateNextTexDraw();
                        noofDidits++;
                        Debug.Log("Instanciate new variable");
                    }
                    else
                    {
                        nextTex = transform.GetChild(nextTex.transform.GetSiblingIndex() - 1).gameObject;
                    }

                }
                else
                {
                    Debug.Log("LCM Factor");
                    if (noofvaluesafterselect == 0)
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                    nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                    noofvaluesafterselect++;
                }
            }
            else
            {
                if (!carryPosition)
                {
                    Debug.Log("carry lcmFactor: " + lcmFactor);
                    if (divide && lcmFactor)
                    {
                        if (noofvaluesafterselect == 0)
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                        nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                        noofvaluesafterselect++;
                    }
                    else if (noofvaluesafterselect < 1)
                    {
                        Debug.Log("not carry ");
                        if (divide)
                        {
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                            nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                            //lcmDigits++;
                            value += 1 + (int)Mathf.Pow(10, noofDidits);
                            noofDidits++;
                            if ((nextTex.transform.GetSiblingIndex() % prevColCount) > 1)
                                nextTex = transform.GetChild(nextTex.transform.GetSiblingIndex() - 1).gameObject;
                            selectedBypointer = false;
                        }
                        else
                        {
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, charValue);
                            noofvaluesafterselect++;
                            nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                            selectedBypointer = false;
                        }

                    }
                    else
                    {
                        Debug.Log("can not add more than 1 digit");
                    }

                }
                else
                {


                    if (noofvaluesafterselect == 0)
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                    nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                    noofvaluesafterselect++;

                }
                if (string.Compare(nextTex.name, lastInstanciatedValueName) == 0)
                {
                    noofvaluesafterselect = 0;
                    InstanciateNextTexDraw();
                }
            }
        }
        else
        {
            if (divide)
            {
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                //lcmDigits++;
                value += 1 + (int)Mathf.Pow(10, noofDidits);
                noofDidits++;
            }
            if (cmm)
            {
                lcm = true;
                lcmFactor = true;
            }
            if (lcm)
            {
                if (!lcmFactor)
                {
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                    nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                    lcmDigits++;
                    value += 1 + (int)Mathf.Pow(10, noofDidits);
                    noofDidits++;
                }
            }
            else
            {
                if (!divide)
                {
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, charValue);
                    nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                }

            }
            if (ans)
            {
                if (odd)
                {
                    value += 1 + (int)Mathf.Pow(10, noofDidits);
                    noofDidits++;
                    InstanciateNextTexDraw();
                    //Debug.Log("prevColCount: " + prevColCount + " CarryPoint: " + CarryPoint);

                    /*
                    if (CarryPoint < maxnoofdigits + 1)
                    {
                        odd = false;
                        nextTex = transform.GetChild(CarryPoint++).gameObject;
                    }
                    else
                    {*/
                    odd = true;
                    //nextTex = transform.GetChild(transform.childCount - 3).gameObject;
                    //}

                }
                else
                {
                    if (!lcm)
                    {
                        odd = true;
                        nextTex = transform.GetChild(transform.childCount - 3).gameObject;
                    }
                    else
                    {
                        if (cmm)
                        {
                            if (nextTex.GetComponent<TexDrawUtils>().numberOfDigit < 1)
                                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                            nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                            lcmFactorDigits++;
                        }
                        else
                        {
                            Debug.Log("LCM Answer");
                            noofDidits++;
                            InstanciateNextTexDraw();
                        }

                    }

                }
            }
            else
            {

                if (lcm)
                {
                    if (lcmFactor)
                    {
                        // this block never gets executed
                        //Debug.Log("LCM Factor");
                        //InstanciateNextTexDraw();
                        Debug.Log("cmm LCM Factor nextTex: " + nextTex.name);
                        if (nextTex.GetComponent<TexDrawUtils>().numberOfDigit < 1)
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                        nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                        lcmFactorDigits++;
                    }
                    else
                    {
                        if (nextTex.transform.GetSiblingIndex() == (transform.childCount - currColCount))
                        {
                            InstanciateNextTexDraw();
                            noofDidits++;
                            //Debug.Log("Instanciate new variable");
                        }
                        else
                        {
                            nextTex = transform.GetChild(nextTex.transform.GetSiblingIndex() - 1).gameObject;
                        }

                    }


                }
                else
                {
                    if (divide)
                    {
                        if (divideFirstLine)
                        {
                            value = (value * 10) + 1;
                            noofDidits++;
                            InstanciateNextTexDraw();
                        }
                        else
                        {
                            value = (value * 10) + 1;
                            noofDidits++;
                            Debug.Log("index: " + (nextTex.transform.GetSiblingIndex() % prevColCount));
                            if ((nextTex.transform.GetSiblingIndex() % prevColCount) > 1)
                                nextTex = transform.GetChild(nextTex.transform.GetSiblingIndex() - 1).gameObject;
                        }
                    }
                    else
                    {
                        value = (value * 10) + 1;
                        noofDidits++;
                        InstanciateNextTexDraw();
                    }

                }

            }
        }
        StartBlinkingCursor();
    }
    public void pressed8()
    {
        StopblinkingCursor();
        string charValue = "8";
        if (selectedBypointer)
        {

            if (cmm)
            {
                lcm = true;
                lcmFactor = true;
            }
            if (lcm)
            {
                if (!lcmFactor)
                {
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                    //nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                    selectedBypointer = false;
                    //lcmDigits++;
                    if (nextTex.transform.GetSiblingIndex() == (transform.childCount - currColCount))
                    {
                        InstanciateNextTexDraw();
                        noofDidits++;
                        Debug.Log("Instanciate new variable");
                    }
                    else
                    {
                        nextTex = transform.GetChild(nextTex.transform.GetSiblingIndex() - 1).gameObject;
                    }

                }
                else
                {
                    Debug.Log("LCM Factor");
                    if (noofvaluesafterselect == 0)
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                    nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                    noofvaluesafterselect++;
                }
            }
            else
            {
                if (!carryPosition)
                {
                    Debug.Log("carry lcmFactor: " + lcmFactor);
                    if (divide && lcmFactor)
                    {
                        if (noofvaluesafterselect == 0)
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                        nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                        noofvaluesafterselect++;
                    }
                    else if (noofvaluesafterselect < 1)
                    {
                        Debug.Log("not carry ");
                        if (divide)
                        {
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                            nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                            //lcmDigits++;
                            value += 1 + (int)Mathf.Pow(10, noofDidits);
                            noofDidits++;
                            if ((nextTex.transform.GetSiblingIndex() % prevColCount) > 1)
                                nextTex = transform.GetChild(nextTex.transform.GetSiblingIndex() - 1).gameObject;
                            selectedBypointer = false;
                        }
                        else
                        {
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, charValue);
                            noofvaluesafterselect++;
                            nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                            selectedBypointer = false;
                        }

                    }
                    else
                    {
                        Debug.Log("can not add more than 1 digit");
                    }

                }
                else
                {


                    if (noofvaluesafterselect == 0)
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                    nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                    noofvaluesafterselect++;

                }
                if (string.Compare(nextTex.name, lastInstanciatedValueName) == 0)
                {
                    noofvaluesafterselect = 0;
                    InstanciateNextTexDraw();
                }
            }
        }
        else
        {
            if (divide)
            {
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                //lcmDigits++;
                value += 1 + (int)Mathf.Pow(10, noofDidits);
                noofDidits++;
            }
            if (cmm)
            {
                lcm = true;
                lcmFactor = true;
            }
            if (lcm)
            {
                if (!lcmFactor)
                {
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                    nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                    lcmDigits++;
                    value += 1 + (int)Mathf.Pow(10, noofDidits);
                    noofDidits++;
                }
            }
            else
            {
                if (!divide)
                {
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, charValue);
                    nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                }

            }
            if (ans)
            {
                if (odd)
                {
                    value += 1 + (int)Mathf.Pow(10, noofDidits);
                    noofDidits++;
                    InstanciateNextTexDraw();
                    //Debug.Log("prevColCount: " + prevColCount + " CarryPoint: " + CarryPoint);

                    /*
                    if (CarryPoint < maxnoofdigits + 1)
                    {
                        odd = false;
                        nextTex = transform.GetChild(CarryPoint++).gameObject;
                    }
                    else
                    {*/
                    odd = true;
                    //nextTex = transform.GetChild(transform.childCount - 3).gameObject;
                    //}

                }
                else
                {
                    if (!lcm)
                    {
                        odd = true;
                        nextTex = transform.GetChild(transform.childCount - 3).gameObject;
                    }
                    else
                    {
                        if (cmm)
                        {
                            if (nextTex.GetComponent<TexDrawUtils>().numberOfDigit < 1)
                                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                            nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                            lcmFactorDigits++;
                        }
                        else
                        {
                            Debug.Log("LCM Answer");
                            noofDidits++;
                            InstanciateNextTexDraw();
                        }

                    }

                }
            }
            else
            {

                if (lcm)
                {
                    if (lcmFactor)
                    {
                        // this block never gets executed
                        //Debug.Log("LCM Factor");
                        //InstanciateNextTexDraw();
                        Debug.Log("cmm LCM Factor nextTex: " + nextTex.name);
                        if (nextTex.GetComponent<TexDrawUtils>().numberOfDigit < 1)
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                        nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                        lcmFactorDigits++;
                    }
                    else
                    {
                        if (nextTex.transform.GetSiblingIndex() == (transform.childCount - currColCount))
                        {
                            InstanciateNextTexDraw();
                            noofDidits++;
                            //Debug.Log("Instanciate new variable");
                        }
                        else
                        {
                            nextTex = transform.GetChild(nextTex.transform.GetSiblingIndex() - 1).gameObject;
                        }

                    }


                }
                else
                {
                    if (divide)
                    {
                        if (divideFirstLine)
                        {
                            value = (value * 10) + 1;
                            noofDidits++;
                            InstanciateNextTexDraw();
                        }
                        else
                        {
                            value = (value * 10) + 1;
                            noofDidits++;
                            Debug.Log("index: " + (nextTex.transform.GetSiblingIndex() % prevColCount));
                            if ((nextTex.transform.GetSiblingIndex() % prevColCount) > 1)
                                nextTex = transform.GetChild(nextTex.transform.GetSiblingIndex() - 1).gameObject;
                        }
                    }
                    else
                    {
                        value = (value * 10) + 1;
                        noofDidits++;
                        InstanciateNextTexDraw();
                    }

                }

            }
        }
        StartBlinkingCursor();
    }
    public void pressed9()
    {
        StopblinkingCursor();
        string charValue = "9";
        if (selectedBypointer)
        {

            if (cmm)
            {
                lcm = true;
                lcmFactor = true;
            }
            if (lcm)
            {
                if (!lcmFactor)
                {
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                    //nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                    selectedBypointer = false;
                    //lcmDigits++;
                    if (nextTex.transform.GetSiblingIndex() == (transform.childCount - currColCount))
                    {
                        InstanciateNextTexDraw();
                        noofDidits++;
                        Debug.Log("Instanciate new variable");
                    }
                    else
                    {
                        nextTex = transform.GetChild(nextTex.transform.GetSiblingIndex() - 1).gameObject;
                    }

                }
                else
                {
                    Debug.Log("LCM Factor");
                    if (noofvaluesafterselect == 0)
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                    nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                    noofvaluesafterselect++;
                }
            }
            else
            {
                if (!carryPosition)
                {
                    Debug.Log("carry lcmFactor: " + lcmFactor);
                    if (divide && lcmFactor)
                    {
                        if (noofvaluesafterselect == 0)
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                        nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                        noofvaluesafterselect++;
                    }
                    else if (noofvaluesafterselect < 1)
                    {
                        Debug.Log("not carry ");
                        if (divide)
                        {
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                            nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                            //lcmDigits++;
                            value += 1 + (int)Mathf.Pow(10, noofDidits);
                            noofDidits++;
                            if ((nextTex.transform.GetSiblingIndex() % prevColCount) > 1)
                                nextTex = transform.GetChild(nextTex.transform.GetSiblingIndex() - 1).gameObject;
                            selectedBypointer = false;
                        }
                        else
                        {
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, charValue);
                            noofvaluesafterselect++;
                            nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                            selectedBypointer = false;
                        }

                    }
                    else
                    {
                        Debug.Log("can not add more than 1 digit");
                    }

                }
                else
                {


                    if (noofvaluesafterselect == 0)
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                    nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                    noofvaluesafterselect++;

                }
                if (string.Compare(nextTex.name, lastInstanciatedValueName) == 0)
                {
                    noofvaluesafterselect = 0;
                    InstanciateNextTexDraw();
                }
            }
        }
        else
        {
            if (divide)
            {
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                //lcmDigits++;
                value += 1 + (int)Mathf.Pow(10, noofDidits);
                noofDidits++;
            }
            if (cmm)
            {
                lcm = true;
                lcmFactor = true;
            }
            if (lcm)
            {
                if (!lcmFactor)
                {
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                    nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                    lcmDigits++;
                    value += 1 + (int)Mathf.Pow(10, noofDidits);
                    noofDidits++;
                }
            }
            else
            {
                if (!divide)
                {
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, charValue);
                    nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                }

            }
            if (ans)
            {
                if (odd)
                {
                    value += 1 + (int)Mathf.Pow(10, noofDidits);
                    noofDidits++;
                    InstanciateNextTexDraw();
                    //Debug.Log("prevColCount: " + prevColCount + " CarryPoint: " + CarryPoint);

                    /*
                    if (CarryPoint < maxnoofdigits + 1)
                    {
                        odd = false;
                        nextTex = transform.GetChild(CarryPoint++).gameObject;
                    }
                    else
                    {*/
                    odd = true;
                    //nextTex = transform.GetChild(transform.childCount - 3).gameObject;
                    //}

                }
                else
                {
                    if (!lcm)
                    {
                        odd = true;
                        nextTex = transform.GetChild(transform.childCount - 3).gameObject;
                    }
                    else
                    {
                        if (cmm)
                        {
                            if (nextTex.GetComponent<TexDrawUtils>().numberOfDigit < 1)
                                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                            nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                            lcmFactorDigits++;
                        }
                        else
                        {
                            Debug.Log("LCM Answer");
                            noofDidits++;
                            InstanciateNextTexDraw();
                        }

                    }

                }
            }
            else
            {

                if (lcm)
                {
                    if (lcmFactor)
                    {
                        // this block never gets executed
                        //Debug.Log("LCM Factor");
                        //InstanciateNextTexDraw();
                        Debug.Log("cmm LCM Factor nextTex: " + nextTex.name);
                        if (nextTex.GetComponent<TexDrawUtils>().numberOfDigit < 1)
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                        nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                        lcmFactorDigits++;
                    }
                    else
                    {
                        if (nextTex.transform.GetSiblingIndex() == (transform.childCount - currColCount))
                        {
                            InstanciateNextTexDraw();
                            noofDidits++;
                            //Debug.Log("Instanciate new variable");
                        }
                        else
                        {
                            nextTex = transform.GetChild(nextTex.transform.GetSiblingIndex() - 1).gameObject;
                        }

                    }


                }
                else
                {
                    if (divide)
                    {
                        if (divideFirstLine)
                        {
                            value = (value * 10) + 1;
                            noofDidits++;
                            InstanciateNextTexDraw();
                        }
                        else
                        {
                            value = (value * 10) + 1;
                            noofDidits++;
                            Debug.Log("index: " + (nextTex.transform.GetSiblingIndex() % prevColCount));
                            if ((nextTex.transform.GetSiblingIndex() % prevColCount) > 1)
                                nextTex = transform.GetChild(nextTex.transform.GetSiblingIndex() - 1).gameObject;
                        }
                    }
                    else
                    {
                        value = (value * 10) + 1;
                        noofDidits++;
                        InstanciateNextTexDraw();
                    }

                }

            }
        }
        StartBlinkingCursor();
    }
    public void pressed0()
    {
        StopblinkingCursor();
        string charValue = "0";
        if (selectedBypointer)
        {

            if (cmm)
            {
                lcm = true;
                lcmFactor = true;
            }
            if (lcm)
            {
                if (!lcmFactor)
                {
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                    //nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                    selectedBypointer = false;
                    //lcmDigits++;
                    if (nextTex.transform.GetSiblingIndex() == (transform.childCount - currColCount))
                    {
                        InstanciateNextTexDraw();
                        noofDidits++;
                        Debug.Log("Instanciate new variable");
                    }
                    else
                    {
                        nextTex = transform.GetChild(nextTex.transform.GetSiblingIndex() - 1).gameObject;
                    }

                }
                else
                {
                    Debug.Log("LCM Factor");
                    if (noofvaluesafterselect == 0)
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                    nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                    noofvaluesafterselect++;
                }
            }
            else
            {
                if (!carryPosition)
                {
                    Debug.Log("carry lcmFactor: " + lcmFactor);
                    if (divide && lcmFactor)
                    {
                        if (noofvaluesafterselect == 0)
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                        nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                        noofvaluesafterselect++;
                    }
                    else if (noofvaluesafterselect < 1)
                    {
                        Debug.Log("not carry ");
                        if (divide)
                        {
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                            nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                            //lcmDigits++;
                            value += 1 + (int)Mathf.Pow(10, noofDidits);
                            noofDidits++;
                            if ((nextTex.transform.GetSiblingIndex() % prevColCount) > 1)
                                nextTex = transform.GetChild(nextTex.transform.GetSiblingIndex() - 1).gameObject;
                            selectedBypointer = false;
                        }
                        else
                        {
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, charValue);
                            noofvaluesafterselect++;
                            nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                            selectedBypointer = false;
                        }

                    }
                    else
                    {
                        Debug.Log("can not add more than 1 digit");
                    }

                }
                else
                {


                    if (noofvaluesafterselect == 0)
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                    nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                    noofvaluesafterselect++;

                }
                if (string.Compare(nextTex.name, lastInstanciatedValueName) == 0)
                {
                    noofvaluesafterselect = 0;
                    InstanciateNextTexDraw();
                }
            }
        }
        else
        {
            if (divide)
            {
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                //lcmDigits++;
                value += 1 + (int)Mathf.Pow(10, noofDidits);
                noofDidits++;
            }
            if (cmm)
            {
                lcm = true;
                lcmFactor = true;
            }
            if (lcm)
            {
                if (!lcmFactor)
                {
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                    nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                    lcmDigits++;
                    value += 1 + (int)Mathf.Pow(10, noofDidits);
                    noofDidits++;
                }
            }
            else
            {
                if (!divide)
                {
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, charValue);
                    nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                }

            }
            if (ans)
            {
                if (odd)
                {
                    value += 1 + (int)Mathf.Pow(10, noofDidits);
                    noofDidits++;
                    InstanciateNextTexDraw();
                    //Debug.Log("prevColCount: " + prevColCount + " CarryPoint: " + CarryPoint);

                    /*
                    if (CarryPoint < maxnoofdigits + 1)
                    {
                        odd = false;
                        nextTex = transform.GetChild(CarryPoint++).gameObject;
                    }
                    else
                    {*/
                    odd = true;
                    //nextTex = transform.GetChild(transform.childCount - 3).gameObject;
                    //}

                }
                else
                {
                    if (!lcm)
                    {
                        odd = true;
                        nextTex = transform.GetChild(transform.childCount - 3).gameObject;
                    }
                    else
                    {
                        if (cmm)
                        {
                            if (nextTex.GetComponent<TexDrawUtils>().numberOfDigit < 1)
                                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                            nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                            lcmFactorDigits++;
                        }
                        else
                        {
                            Debug.Log("LCM Answer");
                            noofDidits++;
                            InstanciateNextTexDraw();
                        }

                    }

                }
            }
            else
            {

                if (lcm)
                {
                    if (lcmFactor)
                    {
                        // this block never gets executed
                        //Debug.Log("LCM Factor");
                        //InstanciateNextTexDraw();
                        Debug.Log("cmm LCM Factor nextTex: " + nextTex.name);
                        if (nextTex.GetComponent<TexDrawUtils>().numberOfDigit < 1)
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                        nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                        lcmFactorDigits++;
                    }
                    else
                    {
                        if (nextTex.transform.GetSiblingIndex() == (transform.childCount - currColCount))
                        {
                            InstanciateNextTexDraw();
                            noofDidits++;
                            //Debug.Log("Instanciate new variable");
                        }
                        else
                        {
                            nextTex = transform.GetChild(nextTex.transform.GetSiblingIndex() - 1).gameObject;
                        }

                    }


                }
                else
                {
                    if (divide)
                    {
                        if (divideFirstLine)
                        {
                            value = (value * 10) + 1;
                            noofDidits++;
                            InstanciateNextTexDraw();
                        }
                        else
                        {
                            value = (value * 10) + 1;
                            noofDidits++;
                            Debug.Log("index: " + (nextTex.transform.GetSiblingIndex() % prevColCount));
                            if ((nextTex.transform.GetSiblingIndex() % prevColCount) > 1)
                                nextTex = transform.GetChild(nextTex.transform.GetSiblingIndex() - 1).gameObject;
                        }
                    }
                    else
                    {
                        value = (value * 10) + 1;
                        noofDidits++;
                        InstanciateNextTexDraw();
                    }

                }

            }
        }
        StartBlinkingCursor();
    }
    public void pressedComma()
    {
        StopblinkingCursor();
        string charValue = ",";
        if (selectedBypointer)
        {
            if (cmm)
            {
                lcm = false;
                lcmFactor = false;

                InstanciateNextTexDraw();
            }
            if (lcm)
            {
                if (!pfm)
                {
                
                    if (!lcmFactor)
                    {
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                        //nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                        selectedBypointer = false;
                        //lcmDigits++;
                        if (nextTex.transform.GetSiblingIndex() == (transform.childCount - currColCount))
                        {
                            InstanciateNextTexDraw();
                            noofDidits++;
                            Debug.Log("Instanciate new variable");
                        }
                        else
                        {
                            nextTex = transform.GetChild(nextTex.transform.GetSiblingIndex() - 1).gameObject;
                        }

                    }
                    else
                    {
                        Debug.Log("LCM Factor");
                        if (noofvaluesafterselect == 0)
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                        nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                        noofvaluesafterselect++;
                    }
                }
            }
            else
            {
                if (!carryPosition)
                {
                    Debug.Log("lcmFactor: " + lcmFactor);
                    if (noofvaluesafterselect < 1)
                    {
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, charValue);
                        noofvaluesafterselect++;
                        nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                        selectedBypointer = false;
                    }
                    else
                    {
                        Debug.Log("can not add more than 1 digit");
                    }

                }
                else
                {


                    if (noofvaluesafterselect == 0)
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                    nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                    noofvaluesafterselect++;

                }
                if (string.Compare(nextTex.name, lastInstanciatedValueName) == 0)
                {
                    noofvaluesafterselect = 0;
                    InstanciateNextTexDraw();
                }
            }
        }
        else
        {
            if (cmm)
            {
                lcm = false;
                lcmFactor = false;
                
                InstanciateNextTexDraw();
                /*
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                InstanciateNextTexDraw();*/
            }
            if (lcm)
            {

                if (!pfm)
                {
                    if (!lcmFactor)
                    {
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                        nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                        lcmDigits++;
                    }
                }
                else
                {
                    if (ans)
                    {
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                        nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                    }
                }
            }
            else
            {
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, charValue);
                nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
            }
            if (ans)
            {
                if (odd)
                {
                    value += 1 + (int)Mathf.Pow(10, noofDidits);
                    noofDidits++;
                    InstanciateNextTexDraw();
                    //Debug.Log("prevColCount: " + prevColCount + " CarryPoint: " + CarryPoint);

                    /*
                    if (CarryPoint < maxnoofdigits + 1)
                    {
                        odd = false;
                        nextTex = transform.GetChild(CarryPoint++).gameObject;
                    }
                    else
                    {*/
                    odd = true;
                    //nextTex = transform.GetChild(transform.childCount - 3).gameObject;
                    //}

                }
                else
                {
                    if (!lcm)
                    {
                        odd = true;
                        nextTex = transform.GetChild(transform.childCount - 3).gameObject;
                    }
                    else
                    {
                        
                        Debug.Log("LCM Answer");
                        noofDidits++;
                        InstanciateNextTexDraw();
                    }

                }
            }
            else
            {

                if (lcm)
                {
                    if (!pfm)
                    {
                        if (lcmFactor)
                        {
                            // this block never gets executed
                            //Debug.Log("LCM Factor");
                            InstanciateNextTexDraw();
                            //Debug.Log("LCM Factor nextTex: " + nextTex.name);
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, charValue);
                            nextTex.GetComponent<TexDrawUtils>().numberOfDigit++;
                            lcmFactorDigits++;
                        }
                        else
                        {
                            if (nextTex.transform.GetSiblingIndex() == (transform.childCount - currColCount))
                            {
                                InstanciateNextTexDraw();
                                noofDidits++;
                                //Debug.Log("Instanciate new variable");
                            }
                            else
                            {
                                nextTex = transform.GetChild(nextTex.transform.GetSiblingIndex() - 1).gameObject;
                            }

                        }
                    }


                }
                else
                {
                    value = (value * 10) + 1;
                    noofDidits++;
                    InstanciateNextTexDraw();
                }

            }
        }
        StartBlinkingCursor();
    }
    public void pressedNext()
    {
        //
        //Debug.Log("pressedNext " + transform.childCount);
        //StopblinkingCursor();
        //Debug.Log("currColCount: " + currColCount + " prevColCount: " + prevColCount);
        if (maxnoofdigits < noofDidits)
            maxnoofdigits = noofDidits;
        noofDidits = 0;
        // Debug.Log("value: " + value);
        value = 0;
        //nextTex.transform.SetSiblingIndex(transform.childCount);
        if (prevColCount > currColCount)
        {
            for (int i = 0; i < (prevColCount - currColCount); i++)
            {
                //Debug.Log("transform.childCount: " + transform.childCount);
                nextTex = (GameObject)Instantiate(prefabTex, transform);
                //Debug.Log("transform.childCount: " + transform.childCount);
                nextTex.transform.SetSiblingIndex(transform.childCount - 2);
                intTestNo++;
                nextTex.name = "newTex" + intTestNo.ToString();
            }
        }
        if (prevColCount < currColCount)
        {
            prevColCount = currColCount;
            mVariableGridLayout.constraintCount = currColCount;
        }
        noOfRows++;
        currColCount = 0;
        noofvaluesafterselect = 0;
        selectedBypointer = false;
        InstanciateNextTexDraw();
        //StartBlinkingCursor();
    }

    public void pressedAns()
    {
        StopblinkingCursor();
        //Debug.Log("mul: " + mul + " multiplyAnserFirst: " + multiplyAnserFirst + " leavextraLine: " + leavextraLine);
        noOfTimesAnsPressed++;
        ans = false;
        if (cmm)
        {
            lcm = true;
        }
        if (lcm)
        {
            lcmFactor = false;
            selectedBypointer = false;
            if (prevColCount > currColCount)
            {
                Debug.Log("need to increase count");

                for (int i = 0; i <= (prevColCount - currColCount); i++)
                {
                    InstanciateNextTexDraw();
                }
            }
            pressedNext();
            if (!lcmAnswerFirst)
            {
                drawAnsLine = true;
                for (int i = 0; i < prevColCount; i++)
                {
                    InstanciateNextTexDraw();
                }
                drawAnsLine = false;
                pressedNext();
                lcmAnswerFirst = true;
            }
            if (pfm)
            {
                Debug.Log("Ans pressed: " + noOfTimesAnsPressed);
                
                if(noOfTimesAnsPressed == 1)
                {
                    InstanciateNextTexDraw();
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "P");
                    InstanciateNextTexDraw();
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "F");
                    InstanciateNextTexDraw();
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "Of");
                    InstanciateNextTexDraw();
                    pressedNext();
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "x");
                    InstanciateNextTexDraw();
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "=");
                }
                else if (noOfTimesAnsPressed >= 2)
                {
                    drawAnsLine = true;
                    for (int i = 0; i < prevColCount; i++)
                    {
                        InstanciateNextTexDraw();
                    }
                    drawAnsLine = false;
                    pressedNext();

                    InstanciateNextTexDraw();
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "L");
                    InstanciateNextTexDraw();
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "C");
                    InstanciateNextTexDraw();
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "M");
                    InstanciateNextTexDraw();
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, " ");
                    InstanciateNextTexDraw();
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "=");
                }

            }
            else
            {
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "L");
                InstanciateNextTexDraw();
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "C");
                InstanciateNextTexDraw();
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "M");
                InstanciateNextTexDraw();
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, " ");
                InstanciateNextTexDraw();
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "=");
            }
            
            ans = true;
            InstanciateNextTexDraw();
            
        }
        else
        {
            if (mul && !multiplyAnserFirst)
            {
                // multiAnsNextLine = true;
                nextTex = transform.GetChild(transform.childCount - 1).gameObject;
            }
            if (mul && noofDidits > 1 && multiplyAnserFirst)
            {
                leavextraLine = true;
                multiplyAnserFirst = false;
            }
            pressedNext();
            drawAnsLine = true;
            for (int i = 0; i < prevColCount; i++)
            {
                InstanciateNextTexDraw();
            }
            drawAnsLine = false;

            pressedNext();
            if (leavextraLine)
            {
                for (int i = 0; i < prevColCount; i++)
                {
                    InstanciateNextTexDraw();
                }
                pressedNext();
                leavextraLine = false;
                // mul = false;
            }

            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "=");

            odd = true;
            InstanciateNextTexDraw();
            InstanciateNextTexDraw();
            ans = true;
            InstanciateNextTexDraw();
        }
       
        StartBlinkingCursor();
    }

    public void pressedAdd()
    {
        StopblinkingCursor();
        add = true;
        pressedNext();
        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "+");
        InstanciateNextTexDraw();
        InstanciateNextTexDraw();
        StartBlinkingCursor();
    }

    public void pressedMultiply()
    {
        StopblinkingCursor();
        if (lcm)
        {
            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "*");
            InstanciateNextTexDraw();
        }
        else
        {
            mul = true;
            multiplyAnserFirst = true;
            pressedNext();
            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "*");
            InstanciateNextTexDraw();
            InstanciateNextTexDraw();
        }
       
        StartBlinkingCursor();
    }

    public void pressedDivide()
    {
        StopblinkingCursor();
        nextTex.GetComponent<TexDrawUtils>().lcmFactor = true;
        InstanciateNextTexDraw();
        divide = true;
        divideFirstLine = true;
        divideLineNumber = 0; 
        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "\\vborder[1001 black]");
        StartBlinkingCursor();
    }

    public void pressedNextValue()
    {
        StopblinkingCursor();
        if (cmm)
        {
            lcm = false;
            lcmFactor = false;
        }
        if (lcm)
        {
            if (pfm && ans)
            {

                pressedNext();
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "x");
                InstanciateNextTexDraw();
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "=");
                InstanciateNextTexDraw();
            }
            else
            {
                lcmFactor = false;
                pressedNext();
                nextTex.GetComponent<TexDrawUtils>().lcmFactor = true;
                InstanciateNextTexDraw();
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "\\vborder[1100 black] ");
            }
        }
        else
        {
            if (cmm)
            {
                pressedNext();
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "15");
                InstanciateNextTexDraw();
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "=");
                InstanciateNextTexDraw();
            }
            else if(divide){
                lcmFactor = false;
                divideFirstLine = false;
                divideLineNumber++;
                pressedNext();
                int steps = (prevColCount - currColCount);
                for (int i = 0; i <steps; i++)
                {
                    /*
                    //Debug.Log("transform.childCount: " + transform.childCount);
                    nextTex = (GameObject)Instantiate(prefabTex, transform);
                    //Debug.Log("transform.childCount: " + transform.childCount);
                    nextTex.transform.SetSiblingIndex(transform.childCount - 2);
                    intTestNo++;
                    nextTex.name = "newTex" + intTestNo.ToString();*/
                    InstanciateNextTexDraw();
                }
                nextTex = transform.GetChild(transform.childCount - 2).gameObject;
            }
            else
            {
                if (mul && ans)
                {
                    multiAnsNextLine = true;
                }
                add = true;
                pressedNext();

                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "+");
                InstanciateNextTexDraw();
                InstanciateNextTexDraw();
                if (mul && ans)
                {
                    multiAnsNextLine = false;
                    InstanciateNextTexDraw();
                }
            }
            
        }
        StartBlinkingCursor();
    }

    public void pressedTab()
    {
        StopblinkingCursor();
        if (pfm)
        {
            lcmFactor = false;
            pressedNext();
            pressedNext();
            nextTex.GetComponent<TexDrawUtils>().lcmFactor = true;
            InstanciateNextTexDraw();
            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "\\vborder[1100 black] ");
        }
        else
        {

      
        if (ans && !odd)
        {
            odd = true;
            nextTex = transform.GetChild(transform.childCount - 3).gameObject;
        }
        else
        {
            Debug.Log("No where to go");
        }
        }
        StartBlinkingCursor();
    }

    public void pressedDelete()
    {
        StopblinkingCursor();
        Debug.Log("noofDidits: " + noofDidits);
        if (selectedBypointer)
        {
            if (nextTex.GetComponent<TexDrawUtils>().multiDigitAllowed)
            {
                if(nextTex.GetComponent<TexDrawUtils>().numberOfDigit > 0)
                {
                    Debug.Log(nextTex.GetComponent<TEXDraw>().text[nextTex.GetComponent<TEXDraw>().text.Length - 1]);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length-1,1);
                    if(nextTex.GetComponent<TexDrawUtils>().numberOfDigit > 1)
                    {

                    }
                    else
                    {
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length - 1, " ");
                    }
                    
                    nextTex.GetComponent<TexDrawUtils>().numberOfDigit--;

                }
                else
                {
                    Debug.Log("nothing to delete");
                }
                
            }
            else
            {
                Debug.Log("delete");
                if (lcm)
                {
                    if (lcmFactor)
                    {
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, " ");
                        nextTex.GetComponent<TexDrawUtils>().numberOfDigit--;
                    }
                    else
                    {
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, " ");
                        noofDidits--;
                    }
                }
                else
                {
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, " ");
                    nextTex.GetComponent<TexDrawUtils>().numberOfDigit--;
                }
               
            }
        }
        else
        {
            if (noofDidits > 0)
            {
                if (ans)
                {
                    Destroy(nextTex);
                    intTestNo--;
                    nextTex = transform.GetChild(nextTex.transform.GetSiblingIndex() - 1).gameObject;
                    Debug.Log("text: " + nextTex.GetComponent<TEXDraw>().text + "name: " + nextTex.name);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, " ");
                    noofDidits--;

                    value /= 10;
                    if (currColCount >= prevColCount)
                    {
                        mVariableGridLayout.constraintCount--;
                        GameObject child;
                        Debug.Log("No of Rows: " + noOfRows);
                        for (int i = 1; i <= noOfRows; i++)
                        {

                            //child.GetComponent<TEXDraw>().text = child.GetComponent<TEXDraw>().text.Insert(0, " ");
                            intTestNo--;
                            //child.name = "Child" + intTestNo.ToString();
                            //if (i < 3)
                            if (i < 4)
                                child = transform.GetChild((i * currColCount) - 1).gameObject;
                            else
                                child = transform.GetChild((i * currColCount) - 3).gameObject;
                            //child.transform.SetSiblingIndex((i * currColCount) - 2);
                            //child.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "l");
                            Debug.Log("child Index: " + child.transform.GetSiblingIndex());
                            Destroy(child);
                        }
                    }
                    currColCount--;
                    /*
                    if (odd)
                    { 
                        if (noofDidits < maxnoofdigits)
                        {
                            nextTex = transform.GetChild(CarryPoint - 1).gameObject;
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, " ");
                            odd = false;
                        }
                        else
                        {
                            Debug.Log("odd");
                            Debug.Log("text: " + nextTex.GetComponent<TEXDraw>().text + "name: " + nextTex.name);
                            Destroy(nextTex);
                            intTestNo--;
                            nextTex = transform.GetChild(nextTex.transform.GetSiblingIndex() - 1).gameObject;
                            Debug.Log("text: " + nextTex.GetComponent<TEXDraw>().text + "name: " + nextTex.name);
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, " ");
                            noofDidits--;

                            value /= 10;
                            if (currColCount >= prevColCount)
                            {
                                mVariableGridLayout.constraintCount--;
                                GameObject child;
                                Debug.Log("No of Rows: " + noOfRows);
                                for (int i = 1; i <= noOfRows; i++)
                                {

                                    //child.GetComponent<TEXDraw>().text = child.GetComponent<TEXDraw>().text.Insert(0, " ");
                                    intTestNo--;
                                    //child.name = "Child" + intTestNo.ToString();
                                    if (i < 3)
                                        child = transform.GetChild((i * currColCount) - 1).gameObject;
                                    else
                                        child = transform.GetChild((i * currColCount) - 3).gameObject;
                                    //child.transform.SetSiblingIndex((i * currColCount) - 2);
                                    //child.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "l");
                                    Debug.Log("child Index: " + child.transform.GetSiblingIndex());
                                    Destroy(child);
                                }
                            }
                            currColCount--;
                        }

                        Debug.Log("odd curr nextTex" + nextTex.GetComponent<TEXDraw>().text.ToString() + "index: " + nextTex.transform.GetSiblingIndex());
                        GameObject lastTex = transform.GetChild(CarryPoint - 1).gameObject;
                        Debug.Log("last " + lastTex.GetComponent<TEXDraw>().text.ToString() + "index: " + lastTex.transform.GetSiblingIndex());
                        Debug.Log("last+1 " + (transform.GetChild(transform.childCount - 4).gameObject).GetComponent<TEXDraw>().text.ToString() + "index: " + (transform.GetChild(transform.childCount - 4).gameObject).transform.GetSiblingIndex());

                }
                    else
                    {
                        Debug.Log("even curr nextTex" + nextTex.GetComponent<TEXDraw>().text.ToString() + "index: " + nextTex.transform.GetSiblingIndex());
                        GameObject lastTex = transform.GetChild(transform.childCount - 4).gameObject;
                        Debug.Log("last " + lastTex.GetComponent<TEXDraw>().text.ToString() + " index: " + lastTex.transform.GetSiblingIndex());
                        Debug.Log("last+1 " + (transform.GetChild(CarryPoint - 2).gameObject).GetComponent<TEXDraw>().text.ToString() + " index: " + (transform.GetChild(CarryPoint - 1).gameObject).transform.GetSiblingIndex() + " CarryPoint: " + CarryPoint);

                        // if (noofDidits > 1)
                        {
                            Destroy(transform.GetChild(transform.childCount - 3).gameObject);
                            intTestNo--;
                        }

                        nextTex = transform.GetChild(transform.childCount - 4).gameObject;
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, " ");
                        noofDidits--;
                        if (CarryPoint > 1)
                            CarryPoint--;
                        value /= 10;
                        odd = true;
                        if (currColCount >= prevColCount)
                        {
                            mVariableGridLayout.constraintCount--;
                            GameObject child;

                            Debug.Log("No of Rows: " + noOfRows);
                            for (int i = 1; i <= noOfRows; i++)
                            {

                                //child.GetComponent<TEXDraw>().text = child.GetComponent<TEXDraw>().text.Insert(0, " ");
                                intTestNo--;
                                //child.name = "Child" + intTestNo.ToString();
                                if (i < 3)
                                    child = transform.GetChild((i * currColCount) - 1).gameObject;
                                else
                                    child = transform.GetChild((i * currColCount) - 3).gameObject;
                                //child.transform.SetSiblingIndex((i * currColCount) - 2);
                                //child.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "l");
                                Debug.Log("child Index: " + child.transform.GetSiblingIndex());
                                Destroy(child);
                            }
                        }
                        currColCount--;
                    }
                    */
                }
                else
                {
                    /*
                Debug.Log("curr nextTex" + nextTex.GetComponent<TEXDraw>().text.ToString() + "index: " + nextTex.transform.GetSiblingIndex());
                GameObject lastTex = transform.GetChild(nextTex.transform.GetSiblingIndex() + 1).gameObject;
                Debug.Log("last " + lastTex.GetComponent<TEXDraw>().text.ToString() + "index: " + lastTex.transform.GetSiblingIndex());
                Debug.Log("last+1 " + (transform.GetChild(nextTex.transform.GetSiblingIndex() + 2).gameObject).GetComponent<TEXDraw>().text.ToString() + "index: " + (transform.GetChild(nextTex.transform.GetSiblingIndex() + 2).gameObject).transform.GetSiblingIndex());*/

                    if(!divide)
                    {
                        Destroy(nextTex);
                        intTestNo--;
                    }
                    if(divide && divideFirstLine)
                    {
                        Debug.Log("Delete");
                        Destroy(nextTex);
                        intTestNo--;
                    }
                   
                    nextTex = transform.GetChild(nextTex.transform.GetSiblingIndex() + 1).gameObject;
                    if (lcm || divide)
                    {
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - 1);
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, " ");
                        noofDidits--;

                        value /= 10;
                        Debug.Log("currColCount: " + currColCount + " prevColCount: " + prevColCount + " noofDidits: " + noofDidits);
                    }
                    else
                    {
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, " ");
                        noofDidits--;

                        value /= 10;
                        Debug.Log("currColCount: " + currColCount + " prevColCount: " + prevColCount + " noofDidits: " + noofDidits);
                    }
                    if (!divide)
                    {
                        if (currColCount > prevColCount)
                        {
                            mVariableGridLayout.constraintCount--;
                            GameObject child;
                            /* if (noOfRows < 3)
                             {*/
                            Debug.Log("No of Rows: " + noOfRows);
                            for (int i = 1; i <= noOfRows; i++)
                            {

                                //child.GetComponent<TEXDraw>().text = child.GetComponent<TEXDraw>().text.Insert(0, " ");
                                intTestNo--;
                                //child.name = "Child" + intTestNo.ToString();
                                // if (i < 3)
                                if (i < 4)
                                    child = transform.GetChild((i * currColCount) - 1).gameObject;
                                else
                                    child = transform.GetChild((i * currColCount) - 3).gameObject;
                                //child.transform.SetSiblingIndex((i * currColCount) - 2);
                                //child.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "l");
                                //Debug.Log("child Index: " + child.transform.GetSiblingIndex());
                                Destroy(child);
                            }
                        }
                        currColCount--;
                    }
                }
            }
            else
            {
                Debug.Log("Nothing to delete");
            }
        }
        //Debug.Log("odd: " + odd);
        StartBlinkingCursor();
    }

    public void pressedSub()
    {
        StopblinkingCursor();
        add = true;
        pressedNext();
        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "-");
        InstanciateNextTexDraw();
        InstanciateNextTexDraw();
        StartBlinkingCursor();
    }

    public void OnClickedOnATexDraw(GameObject obj)
    {
        StopblinkingCursor();
        // Debug.Log("current Selected GameObject is " + obj.name + " value: " + obj.GetComponent<TEXDraw>().text.ToString());
        if (divide)
        {
            if (divideFirstLine)
                divideFirstLine = false;
        }
        if (lcm)
        {
            if (lcmFactor)
            {

            }
            else
            {
                if(prevColCount > currColCount)
                {
                    //Debug.Log("need to increase count prevColCount: "+ prevColCount + " currColCount: "+ currColCount+ " (prevColCount - currColCount):"+ (prevColCount - currColCount));
                    int nuberofturns = (prevColCount - currColCount);
                    for (int i = 0; i < nuberofturns; i++)
                    {
                        //Debug.Log("i = " + i);
                        InstanciateNextTexDraw();
                        //Debug.Log("i = " + i);
                    }
                }
                lastTex = nextTex;
            }
        }
        else
        {
            lastTex = nextTex;
        }
        
        nextTex = obj;
        selectedBypointer = true;
        noofvaluesafterselect = 0;
        if (cmm & ans)
        {
            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "\\vborder[1111 black]");
        }
        if (nextTex.transform.GetSiblingIndex() < prevColCount + 2)
            carryPosition = true;
        else
        {
            carryPosition = false;
        }
        if (nextTex.GetComponent<TexDrawUtils>().lcmFactor)
            lcmFactor = true;
        else
            lcmFactor = false;
        Debug.Log("lcm Factor: " + lcmFactor);
        StartBlinkingCursor();
    }

    public void cutNumber()
    {
        StopblinkingCursor();
        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "\\not[0-0]");
        nextTex = transform.transform.GetChild(nextTex.transform.GetSiblingIndex() - mVariableGridLayout.constraintCount).gameObject;
        if (nextTex.transform.GetSiblingIndex() < prevColCount + 2)
            carryPosition = true;
        else
        {
            carryPosition = false;
        }
        StartBlinkingCursor();
    }

    public void pressedLCM()
    {
        StopblinkingCursor();
        nextTex.GetComponent<TexDrawUtils>().lcmFactor = true;
        InstanciateNextTexDraw();
        lcm = true;
        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "\\vborder[1010 black]");
        StartBlinkingCursor();
    }

    public void pressedLCMPFM()
    {
        StopblinkingCursor();
        nextTex.GetComponent<TexDrawUtils>().lcmFactor = true;
        InstanciateNextTexDraw();
        lcm = true;
        pfm = true;
        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "\\vborder[1100 black]");
        StartBlinkingCursor();
    }

    public void pressedLCMCMM()
    {
        StopblinkingCursor();
        cmm = true;
        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "9");
        InstanciateNextTexDraw();
        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "=");
        InstanciateNextTexDraw();
        StartBlinkingCursor();
    }


    void StartBlinkingCursor()
    {
        blinkCursor = true;
        StartCoroutine("blinkingCursor");
    }

    void StopblinkingCursor()
    {
        StopCoroutine("blinkingCursor");
        if (lcmFactor)
        {
            if (!blinkCursor)
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length - cursorVarCount, cursorVarCount);
            else
            {
                //nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0, 1);
            }
        }
        else
        {
            if (!blinkCursor)
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0, cursorVarCount);
            else
            {
                //nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0, 1);
            }
        }
        blinkCursor = false;
    }

    IEnumerator blinkingCursor()
    {
        yield return new WaitForSeconds(1.0f);
        if (lcmFactor)
        {
            if (blinkCursor)
            {
                //nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0, 1);
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, cursorVar);

            }
            else
            {

                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(nextTex.GetComponent<TEXDraw>().text.Length- cursorVarCount, cursorVarCount);
                //nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, " ");
            }
        }
        else
        {
            if (blinkCursor)
            {
                //nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0, 1);
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, cursorVar);

            }
            else
            {

                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0, cursorVarCount);
                //nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, " ");
            }
        }
        blinkCursor = !blinkCursor;
        StartCoroutine("blinkingCursor");
    }



}
