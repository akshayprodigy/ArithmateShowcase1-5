using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GridLayoutManager : MonoBehaviour {

    // Use this for initialization
    public GameObject prefabTex;
    GameObject nextTex,lastTex;
    int currColCount, prevColCount,noOfRows,intTestNo = 0, cursorVarCount = 20;
    VariableGridLayoutGroup mVariableGridLayout;
    bool add = false, ans = false, drawAnsLine = false,odd =  false,sub = false,selectedBypointer = false,carryPosition =false;
    bool blinkCursor = false;
    string ansLine = "-", cursorVar = "\\vborder[1000 black]";//"\\vborder[0001 black] ",;
    int ansStartPoint = 0,CarryPoint = 2,noofvaluesafterselect = 0; //20;
    int noofDidits, maxnoofdigits, value;
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

    void Start () {
        mVariableGridLayout = GetComponent<VariableGridLayoutGroup>();
        currColCount = 0;
        prevColCount = 0;
        noofDidits = 0;
        maxnoofdigits = 0;
        value = 0;
        noOfRows = 0;
        ans = false;
        sub = false;
        odd = false;
        noofvaluesafterselect = 0;
        carryPosition = false;
        selectedBypointer = false;
        drawAnsLine = false;
        InstanciateNextTexDraw();
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
        if(mVariableGridLayout.constraintCount < currColCount)
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
                    if(i<3)
                        child.transform.SetSiblingIndex((i * currColCount) - 1);
                    else
                        child.transform.SetSiblingIndex((i * currColCount) - 2);
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
        nextTex = (GameObject)Instantiate(prefabTex, transform);
        if(drawAnsLine)
            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, ansLine);
        else
            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, " ");
        intTestNo++;
        nextTex.name = "newTex"+intTestNo.ToString();
        lastInstanciatedValueName = nextTex.name;
        if (ans)
        {
            nextTex.transform.SetSiblingIndex(transform.childCount -3);
        }
        else
        {
            if (currColCount > 1)
            {
                //Debug.Log("noOfRows: " + noOfRows + " currColCount: " + currColCount);
                nextTex.transform.SetSiblingIndex((noOfRows) * mVariableGridLayout.constraintCount);//currColCount);
            }
        }
    }
    public void pressed1()
    {
        StopblinkingCursor();
        if (selectedBypointer)
        {
            if (!carryPosition)
            {
                if(noofvaluesafterselect < 1)
                {
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "1");
                    noofvaluesafterselect++;
                }
                else
                {
                    Debug.Log("can not add more than 1 digit");
                }

            }
            else
            {
                if(noofvaluesafterselect == 0)
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, "1");
                noofvaluesafterselect++;
            }
            if(string.Compare(nextTex.name, lastInstanciatedValueName) == 0)
            {
                noofvaluesafterselect = 0;
                InstanciateNextTexDraw();
            }
           
        }
        else
        {
            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "1");

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
                    odd = true;
                    nextTex = transform.GetChild(transform.childCount - 3).gameObject;
                }
            }
            else
            {

                value = (value * 10) + 1;
                noofDidits++;
                InstanciateNextTexDraw();

            }
        }
        StartBlinkingCursor();
    }
    public void pressed2()
    {
        StopblinkingCursor();
        if (selectedBypointer)
        {
            if (!carryPosition)
            {
                if (noofvaluesafterselect < 1)
                {
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "2");
                    noofvaluesafterselect++;
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
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, "2");
                noofvaluesafterselect++;
            }
            if (string.Compare(nextTex.name, lastInstanciatedValueName) == 0)
            {
                noofvaluesafterselect = 0;
                InstanciateNextTexDraw();
            }

        }
        else
        {

            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "2");
            if (ans)
            {
                if (odd)
                {
                    value += 2 + (int)Mathf.Pow(10, noofDidits);
                    noofDidits++;
                    InstanciateNextTexDraw();
                    //Debug.Log("prevColCount: " + prevColCount + " CarryPoint: " + CarryPoint);
                    /*if (CarryPoint < maxnoofdigits + 1)
                    {
                        odd = false;
                        nextTex = transform.GetChild(CarryPoint++).gameObject;
                    }
                    else
                    {*/
                        odd = true;
                     //   nextTex = transform.GetChild(transform.childCount - 3).gameObject;
                   // }

                }
                else
                {
                    odd = true;
                    nextTex = transform.GetChild(transform.childCount - 3).gameObject;
                }


            }
            else
            {
                value = (value * 10) + 2;
                noofDidits++;
                InstanciateNextTexDraw();
            }
        }
        StartBlinkingCursor();
    }
    public void pressed3()
    {
        StopblinkingCursor();
        if (selectedBypointer)
        {
            if (!carryPosition)
            {
                if (noofvaluesafterselect < 1)
                {
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "3");
                    noofvaluesafterselect++;
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
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, "3");
                noofvaluesafterselect++;
            }
            if (string.Compare(nextTex.name, lastInstanciatedValueName) == 0)
            {
                noofvaluesafterselect = 0;
                InstanciateNextTexDraw();
            }

        }
        else
        {

            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "3");
            if (ans)
            {
                if (odd)
                {
                    value += 3 + (int)Mathf.Pow(10, noofDidits);
                    noofDidits++;
                    InstanciateNextTexDraw();
                    //Debug.Log("prevColCount: " + prevColCount + " CarryPoint: " + CarryPoint);
                  /*  if (CarryPoint < maxnoofdigits + 1)
                    {
                        odd = false;
                        nextTex = transform.GetChild(CarryPoint++).gameObject;
                    }
                    else
                    {*/
                        odd = true;
                    //    nextTex = transform.GetChild(transform.childCount - 3).gameObject;
                    //}

                }
                else
                {
                    odd = true;
                    nextTex = transform.GetChild(transform.childCount - 3).gameObject;
                }


            }
            else
            {
                value = (value * 10) + 3;
                noofDidits++;
                InstanciateNextTexDraw();
            }
        }
        StartBlinkingCursor();
    }
    public void pressed4()
    {
        StopblinkingCursor();
        if (selectedBypointer)
        {
            if (!carryPosition)
            {
                if (noofvaluesafterselect < 1)
                {
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "4");
                    noofvaluesafterselect++;
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
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, "4");
                noofvaluesafterselect++;
            }
            if (string.Compare(nextTex.name, lastInstanciatedValueName) == 0)
            {
                noofvaluesafterselect = 0;
                InstanciateNextTexDraw();
            }

        }
        else
        {

            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "4");
            if (ans)
            {
                if (odd)
                {
                    value += 4 + (int)Mathf.Pow(10, noofDidits);
                    noofDidits++;
                    InstanciateNextTexDraw();
                    //Debug.Log("prevColCount: " + prevColCount + " CarryPoint: " + CarryPoint);
                    /*if (CarryPoint < maxnoofdigits + 1)
                    {
                        odd = false;
                        nextTex = transform.GetChild(CarryPoint++).gameObject;
                    }
                    else
                    {*/
                        odd = true;
                    //    nextTex = transform.GetChild(transform.childCount - 3).gameObject;
                    //}

                }
                else
                {
                    odd = true;
                    nextTex = transform.GetChild(transform.childCount - 3).gameObject;
                }


            }
            else
            {
                value = (value * 10) + 4;
                noofDidits++;
                InstanciateNextTexDraw();
            }
        }
        StartBlinkingCursor();
    }
    public void pressed5()
    {
        StopblinkingCursor();
        if (selectedBypointer)
        {
            if (!carryPosition)
            {
                if (noofvaluesafterselect < 1)
                {
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "5");
                    noofvaluesafterselect++;
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
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, "5");
                noofvaluesafterselect++;
            }
            if (string.Compare(nextTex.name, lastInstanciatedValueName) == 0)
            {
                noofvaluesafterselect = 0;
                InstanciateNextTexDraw();
            }

        }
        else
        {

            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "5");
            if (ans)
            {
                if (odd)
                {
                    value += 5 + (int)Mathf.Pow(10, noofDidits);
                    noofDidits++;
                    InstanciateNextTexDraw();
                    //Debug.Log("prevColCount: " + prevColCount + " CarryPoint: " + CarryPoint);
                   /* if (CarryPoint < maxnoofdigits + 1)
                    {
                        odd = false;
                        nextTex = transform.GetChild(CarryPoint++).gameObject;
                    }
                    else
                    {*/
                        odd = true;
                    //    nextTex = transform.GetChild(transform.childCount - 3).gameObject;
                   // }

                }
                else
                {
                    odd = true;
                    nextTex = transform.GetChild(transform.childCount - 3).gameObject;
                }


            }
            else
            {
                value = (value * 10) + 5;
                noofDidits++;
                InstanciateNextTexDraw();
            }
        }
        StartBlinkingCursor();
    }
    public void pressed6()
    {
        StopblinkingCursor();
        if (selectedBypointer)
        {
            if (!carryPosition)
            {
                if (noofvaluesafterselect < 1)
                {
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "6");
                    noofvaluesafterselect++;
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
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, "6");
                noofvaluesafterselect++;
            }
            if (string.Compare(nextTex.name, lastInstanciatedValueName) == 0)
            {
                noofvaluesafterselect = 0;
                InstanciateNextTexDraw();
            }

        }
        else
        {

            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "6");
            if (ans)
            {
                if (odd)
                {
                    value += 6 + (int)Mathf.Pow(10, noofDidits);
                    noofDidits++;
                    InstanciateNextTexDraw();
                    //Debug.Log("prevColCount: " + prevColCount + " CarryPoint: " + CarryPoint);
                  /*  if (CarryPoint < maxnoofdigits + 1)
                    {
                        odd = false;
                        nextTex = transform.GetChild(CarryPoint++).gameObject;
                    }
                    else
                    {*/
                        odd = true;
                  //      nextTex = transform.GetChild(transform.childCount - 3).gameObject;
                   // }

                }
                else
                {
                    odd = true;
                    nextTex = transform.GetChild(transform.childCount - 3).gameObject;
                }


            }
            else
            {
                value = (value * 10) + 6;
                noofDidits++;
                InstanciateNextTexDraw();
            }
        }
        StartBlinkingCursor();
    }
    public void pressed7()
    {
        StopblinkingCursor();
        if (selectedBypointer)
        {
            if (!carryPosition)
            {
                if (noofvaluesafterselect < 1)
                {
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "7");
                    noofvaluesafterselect++;
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
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, "7");
                noofvaluesafterselect++;
            }
            if (string.Compare(nextTex.name, lastInstanciatedValueName) == 0)
            {
                noofvaluesafterselect = 0;
                InstanciateNextTexDraw();
            }

        }
        else
        {

            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "7");
            if (ans)
            {
                if (odd)
                {
                    value += 7 + (int)Mathf.Pow(10, noofDidits);
                    noofDidits++;
                    InstanciateNextTexDraw();
                    //Debug.Log("prevColCount: " + prevColCount + " CarryPoint: " + CarryPoint);
                    /*if (CarryPoint < maxnoofdigits + 1)
                    {
                        odd = false;
                        nextTex = transform.GetChild(CarryPoint++).gameObject;
                    }
                    else
                    {*/
                        odd = true;
                   //     nextTex = transform.GetChild(transform.childCount - 3).gameObject;
                    //}

                }
                else
                {
                    odd = true;
                    nextTex = transform.GetChild(transform.childCount - 3).gameObject;
                }


            }
            else
            {
                value = (value * 10) + 7;
                noofDidits++;
                InstanciateNextTexDraw();
            }
        }
        StartBlinkingCursor();
    }
    public void pressed8()
    {
        StopblinkingCursor();
        if (selectedBypointer)
        {
            if (!carryPosition)
            {
                if (noofvaluesafterselect < 1)
                {
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "8");
                    noofvaluesafterselect++;
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
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, "8");
                noofvaluesafterselect++;
            }
            if (string.Compare(nextTex.name, lastInstanciatedValueName) == 0)
            {
                noofvaluesafterselect = 0;
                InstanciateNextTexDraw();
            }
        }
        else
        {

            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "8");
            if (ans)
            {
                if (odd)
                {
                    value += 8 + (int)Mathf.Pow(10, noofDidits);
                    noofDidits++;
                    InstanciateNextTexDraw();
                    //Debug.Log("prevColCount: " + prevColCount + " CarryPoint: " + CarryPoint);
                   /* if (CarryPoint < maxnoofdigits + 1)
                    {
                        odd = false;
                        nextTex = transform.GetChild(CarryPoint++).gameObject;
                    }
                    else
                    {*/
                        odd = true;
                    //    nextTex = transform.GetChild(transform.childCount - 3).gameObject;
                    //}

                }
                else
                {
                    odd = true;
                    nextTex = transform.GetChild(transform.childCount - 3).gameObject;
                }


            }
            else
            {
                value = (value * 10) + 8;
                noofDidits++;
                InstanciateNextTexDraw();
            }
        }
        StartBlinkingCursor();
    }
    public void pressed9()
    {
        StopblinkingCursor();
        if (selectedBypointer)
        {
            if (!carryPosition)
            {
                if (noofvaluesafterselect < 1)
                {
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "9");
                    noofvaluesafterselect++;
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
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, "9");
                noofvaluesafterselect++;
            }
            if (string.Compare(nextTex.name, lastInstanciatedValueName) == 0)
            {
                noofvaluesafterselect = 0;
                InstanciateNextTexDraw();
            }
        }
        else
        {

            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "9");
            if (ans)
            {
                if (odd)
                {
                    value += 9 + (int)Mathf.Pow(10, noofDidits);
                    noofDidits++;
                    InstanciateNextTexDraw();
                    //Debug.Log("prevColCount: " + prevColCount + " CarryPoint: " + CarryPoint);
                   /* if (CarryPoint < maxnoofdigits + 1)
                    {
                        odd = false;
                        nextTex = transform.GetChild(CarryPoint++).gameObject;
                    }
                    else
                    {*/
                        odd = true;
                   //     nextTex = transform.GetChild(transform.childCount - 3).gameObject;
                    //}

                }
                else
                {
                    odd = true;
                    nextTex = transform.GetChild(transform.childCount - 3).gameObject;
                }


            }
            else
            {
                value = (value * 10) + 9;
                noofDidits++;
                InstanciateNextTexDraw();
            }
        }
        StartBlinkingCursor();
    }
    public void pressed0()
    {
        StopblinkingCursor();
        if (selectedBypointer)
        {
            if (!carryPosition)
            {
                if (noofvaluesafterselect < 1)
                {
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                    nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "0");
                    noofvaluesafterselect++;
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
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(nextTex.GetComponent<TEXDraw>().text.Length, "0");
                noofvaluesafterselect++;
            }
            if (string.Compare(nextTex.name, lastInstanciatedValueName) == 0)
            {
                noofvaluesafterselect = 0;
                InstanciateNextTexDraw();
            }
        }
        else
        {

            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "0");
            if (ans)
            {
                if (odd)
                {
                    value += 0 + (int)Mathf.Pow(10, noofDidits);
                    noofDidits++;
                    InstanciateNextTexDraw();
                    //Debug.Log("prevColCount: " + prevColCount + " CarryPoint: " + CarryPoint);
                  /*  if (CarryPoint < maxnoofdigits + 1)
                    {
                        odd = false;
                        nextTex = transform.GetChild(CarryPoint++).gameObject;
                    }
                    else
                    {*/
                        odd = true;
                   //     nextTex = transform.GetChild(transform.childCount - 3).gameObject;
                    //}

                }
                else
                {
                    odd = true;
                    nextTex = transform.GetChild(transform.childCount - 3).gameObject;
                }


            }
            else
            {
                value = (value * 10) + 0;
                noofDidits++;
                InstanciateNextTexDraw();
            }
        }
        StartBlinkingCursor();
    }
    public void pressedNext()
    {
        //
        //Debug.Log("pressedNext " + transform.childCount);
        //StopblinkingCursor();
        if (maxnoofdigits < noofDidits)
            maxnoofdigits = noofDidits;
        noofDidits = 0;
       // Debug.Log("value: " + value);
        value = 0;
        //nextTex.transform.SetSiblingIndex(transform.childCount);
        if(prevColCount > currColCount)
        {
            for (int i = 0; i < (prevColCount - currColCount); i++)
            {
                Debug.Log("transform.childCount: " + transform.childCount);
                nextTex = (GameObject)Instantiate(prefabTex, transform);
                Debug.Log("transform.childCount: " + transform.childCount);
                nextTex.transform.SetSiblingIndex(transform.childCount -2);
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
        InstanciateNextTexDraw();
        //StartBlinkingCursor();
    }

    public void pressedAns()
    {
        StopblinkingCursor();
        pressedNext();
        drawAnsLine = true;
        for(int i = 0;i< prevColCount; i++)
        {
            InstanciateNextTexDraw();
        }
        drawAnsLine = false;
        
        pressedNext();
        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
        nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, "=");
        
        odd = true;
        InstanciateNextTexDraw();
        InstanciateNextTexDraw();
        ans = true;
        InstanciateNextTexDraw();
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

    public void pressedTab()
    {
        StopblinkingCursor();
        if(ans && !odd)
        {
            odd = true;
            nextTex = transform.GetChild(transform.childCount - 3).gameObject;
        }
        else
        {
            Debug.Log("No where to go");
        }
        StartBlinkingCursor();
    }

    public void pressedDelete()
    {
        StopblinkingCursor();
        Debug.Log("noofDidits: " + noofDidits);
        if (noofDidits > 0)
        {
            if (ans)
            {
                if (odd)
                {
                    if (noofDidits < maxnoofdigits )
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
                            /* if (noOfRows < 3)
                             {*/
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
                    /*
                    Debug.Log("odd curr nextTex" + nextTex.GetComponent<TEXDraw>().text.ToString() + "index: " + nextTex.transform.GetSiblingIndex());
                    GameObject lastTex = transform.GetChild(CarryPoint - 1).gameObject;
                    Debug.Log("last " + lastTex.GetComponent<TEXDraw>().text.ToString() + "index: " + lastTex.transform.GetSiblingIndex());
                    Debug.Log("last+1 " + (transform.GetChild(transform.childCount - 4).gameObject).GetComponent<TEXDraw>().text.ToString() + "index: " + (transform.GetChild(transform.childCount - 4).gameObject).transform.GetSiblingIndex());
                    */
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
                    if(CarryPoint >1)
                        CarryPoint--;
                    value /= 10;
                    odd = true;
                    if (currColCount >= prevColCount)
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
            }
            else
            {
                /*
            Debug.Log("curr nextTex" + nextTex.GetComponent<TEXDraw>().text.ToString() + "index: " + nextTex.transform.GetSiblingIndex());
            GameObject lastTex = transform.GetChild(nextTex.transform.GetSiblingIndex() + 1).gameObject;
            Debug.Log("last " + lastTex.GetComponent<TEXDraw>().text.ToString() + "index: " + lastTex.transform.GetSiblingIndex());
            Debug.Log("last+1 " + (transform.GetChild(nextTex.transform.GetSiblingIndex() + 2).gameObject).GetComponent<TEXDraw>().text.ToString() + "index: " + (transform.GetChild(nextTex.transform.GetSiblingIndex() + 2).gameObject).transform.GetSiblingIndex());*/

                Destroy(nextTex);
                intTestNo--;
                nextTex = transform.GetChild(nextTex.transform.GetSiblingIndex() + 1).gameObject;
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0);
                nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Insert(0, " ");
                noofDidits--;
                
                value /= 10;
                if (currColCount >= prevColCount)
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
        }
        else
        {
            Debug.Log("Nothing to delete");
        }
        Debug.Log("odd: " + odd);
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
        Debug.Log("current Selected GameObject is " + obj.name + " value: " + obj.GetComponent<TEXDraw>().text.ToString());
        lastTex = nextTex;
        nextTex = obj;
        selectedBypointer = true;
        noofvaluesafterselect = 0;
        if (nextTex.transform.GetSiblingIndex() < prevColCount+2)
            carryPosition = true;
        else
        {
            carryPosition = false;
        }
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

    void StartBlinkingCursor()
    {
        blinkCursor = true;
        StartCoroutine("blinkingCursor");
    }

    void StopblinkingCursor()
    {
        StopCoroutine("blinkingCursor");
        if (!blinkCursor)
            nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0, cursorVarCount);
        else
        {
            //nextTex.GetComponent<TEXDraw>().text = nextTex.GetComponent<TEXDraw>().text.Remove(0, 1);
        }
        blinkCursor = false;
    }

    IEnumerator blinkingCursor()
    {
        yield return new WaitForSeconds(1.0f);
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
        blinkCursor = !blinkCursor;
        StartCoroutine("blinkingCursor");
    }

}
