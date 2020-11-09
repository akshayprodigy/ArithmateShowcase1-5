    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManagerCopy : MonoBehaviour {

    // Use this for initialization
    public GameObject sighn, exp , StartPosition, targetGameObject;
    bool showSighn = false,showFun = false ;
    public float speed = 1.0F;
    private float startTime;
    //Vector3 endPos;
    public TEXDraw answerPanel, stepPanel;
    private int stepNo;
    private int cursorPosition,stepCursorPosition;
    private int currentCharacterCount, prevCharacterCount, stepCharCount = 10, stepAnswerCharcCount = 1,cursorIncrement = 1;
    private List<int> stepStartPoint;
    private string spaceVar = "0",cursorVar = "_";
    public bool add, ans,odd = true , blink = true,sub;
    public int addCarryPosDifference = 0, addAnswerPosDifference = 0,addExtraChar = 6;
    public float NoOfDigitInAnswer = 0.0f, addAnswer = 0;
    private IEnumerator blinkCoroutine;
    void Start () {
        add = true;
        stepStartPoint = new List<int>();
        stepNo = 0;
        currentCharacterCount = prevCharacterCount = -1;
        cursorPosition = stepCursorPosition = 2;
        PrintStepNo();
        skippOneLine();
        //blinkCoroutine =  blinkingCursor();
        StartCoroutine("blinkingCursor");
    }
    void skippOneLine()
    {
        if (add)
        {
            currentCharacterCount = 0;
            adjustStepSpaces();
            /*
            string a = answerPanel.text.ToString();
            answerPanel.text = a + "\n "+"+  ";
            */
            PrintStepNo();
            // Debug.Log("cursorPosition AddNumber: " + cursorPosition +" "+ answerPanel.text.Length);
            answerPanel.text = answerPanel.text.Insert(cursorPosition, "    ");
            cursorPosition += 4;
            // Debug.Log("step : " + stepStartPoint[stepStartPoint.Count - 1] + " value " + answerPanel.text[stepStartPoint[stepStartPoint.Count - 1]] + " value +1" + answerPanel.text[stepStartPoint[stepStartPoint.Count - 1]+3]);
            //Debug.Log("add cursorPosition: " + cursorPosition);
            stepStartPoint.Add(cursorPosition);
            Debug.Log("count " + stepStartPoint.Count);
        }
    }
    void PrintStepNo()
    {
        if (add)
        {
            if (stepNo == 0)
            {
                /*
                answerPanel.text = answerPanel.text.Insert(cursorPosition, "\n Step " + stepNo + " :         ");
                stepNo++;
                cursorPosition += (stepCharCount+4);
                stepStartPoint.Add(cursorPosition);*/
                //Debug.Log("PrintStepNo stepCursorPosition: " + stepCursorPosition + "value:" + stepPanel.text.Length);
                stepPanel.text = stepPanel.text.Insert(stepCursorPosition, "\n      "  + "   ");
                Debug.Log("PrintStepNo cursorPosition: " + cursorPosition + "value:" + answerPanel.text.Length);
                answerPanel.text = answerPanel.text.Insert(cursorPosition, "\n    ");
                stepNo++;
                stepCursorPosition += (stepCharCount);
                cursorPosition += (stepAnswerCharcCount + 4);
                stepStartPoint.Add(cursorPosition);
            }
            else
            {
                /*
               //Debug.Log("PrintStepNo cursorPosition: " + cursorPosition + " answerPanel.text.Length " + answerPanel.text.Length);
                answerPanel.text = answerPanel.text.Insert(cursorPosition, "\n Step " + stepNo + " :     ");
                stepNo++;
                cursorPosition += stepCharCount;
                //Debug.Log("Print StepNo"+ "stepCharCount: "+ stepCharCount+" cursorPosition: " + cursorPosition + " answerPanel.text.Length " + answerPanel.text.Length);
                */
                //Debug.Log("PrintStepNo stepCursorPosition: " + stepCursorPosition + "value:" + stepPanel.text.Length);
                stepPanel.text = stepPanel.text.Insert(stepCursorPosition, "\n Step " + stepNo + " :");
                //Debug.Log("PrintStepNo cursorPosition: " + cursorPosition + "value:" + answerPanel.text.Length);
                answerPanel.text = answerPanel.text.Insert(cursorPosition, "\n");
                stepNo++;
                stepCursorPosition += (stepCharCount);
                cursorPosition += (stepAnswerCharcCount);
            }

            //Debug.Log("PrintStepNo cursorPosition: " + cursorPosition+ "value:"+ answerPanel.text.Length);
        }
    }
	// Update is called once per frame
    /*
	void Update () {
        if (showSighn)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fracJourney = distCovered / 150;
            sighn.transform.position =  Vector3.Lerp(new Vector3(0,0,0), endPos, fracJourney);
            Debug.Log("ShowSighn: "+ sighn.transform.position);
        }
	}
    */
    public void ShowSighn()
    {
        // Show Shigh
       // Debug.Log("ShowSighn");
        showSighn = true;
       // startTime = Time.time;
        StartCoroutine(MoveUi(sighn));
    }

    public void ShowFun()
    {
    if(showSighn)
        {
            StopCoroutine("MoveUi");
            StartCoroutine(MoveUiBack(sighn));
        }

    }
    private IEnumerator MoveUiBack(GameObject obj)
    {
        
            while (Vector3.Distance(obj.transform.position, StartPosition.transform.position) > 1f)
            {
                obj.transform.position = Vector3.MoveTowards(obj.transform.position, StartPosition.transform.position, Time.deltaTime * speed);
                yield return new WaitForSeconds(0.02f);
            }
            
        
    }
    private IEnumerator MoveUi(GameObject obj)
    {
       // while (showSighn)
       // {
            while (Vector3.Distance(obj.transform.position, targetGameObject.transform.position) > 1f)
            {
                obj.transform.position = Vector3.MoveTowards(obj.transform.position, targetGameObject.transform.position, Time.deltaTime * speed);
                yield return new WaitForSeconds(0.02f);
            }
        //}
    }

    public void pressed1()
    { //string a = answerPanel.text.ToString();
      //answerPanel.text = a + 1;
      //Debug.Log("cursorPosition: " + cursorPosition + " " + answerPanel.text.Length);
        if (add)
        {
            if (ans)
            {
                if (odd)
                {
                    answerPanel.text = answerPanel.text.Insert(cursorPosition, "1");
                    addAnswer += 1 * (Mathf.Pow(10.0f, NoOfDigitInAnswer));
                    NoOfDigitInAnswer++;
                    if (currentCharacterCount < (2 * prevCharacterCount)-2)
                    {
                        cursorPosition = addCarryPosDifference;
                        addCarryPosDifference--;  
                    }
                    else
                    {
                        cursorPosition = addAnswerPosDifference;
                    }  
                }
                else
                {
                    answerPanel.text = answerPanel.text.Remove(cursorPosition, 1);
                    answerPanel.text = answerPanel.text.Insert(cursorPosition, "1");   
                    cursorPosition = addAnswerPosDifference;
                }
                //Debug.Log("1 cursorPosition: " + cursorPosition);
                if (currentCharacterCount < (2 * prevCharacterCount)-2)
                {
                    odd = !odd;
                }
                else
                    odd = true;
            }
            else
            {
                // if(stepNo == 1)
                //{
                Debug.Log("cursorPosition: " + cursorPosition + " " + answerPanel.text.Length);
                answerPanel.text = answerPanel.text.Insert(cursorPosition, "1");
                cursorPosition += cursorIncrement;
                Debug.Log("cursorPosition: " + cursorPosition + " " + answerPanel.text.Length);
                /* }
                 else
                 {
                     answerPanel.text = answerPanel.text.Insert(cursorPosition, "1");
                     cursorPosition++;
                 }*/

                
            }
            currentCharacterCount++;
        }
    }
    public void pressed2()
    {
        //string a = answerPanel.text.ToString();
        //answerPanel.text = a + 2;
        // Debug.Log("cursorPosition: " + cursorPosition + " " + answerPanel.text.Length);
        if (add)
        {
            if (ans)
            {
                if (odd)
                {
                    answerPanel.text = answerPanel.text.Insert(cursorPosition, "2");
                    addAnswer += 2 * (Mathf.Pow(10.0f, NoOfDigitInAnswer));
                    NoOfDigitInAnswer++;
                    if (currentCharacterCount < (2 * prevCharacterCount) - 2)
                    {
                        cursorPosition = addCarryPosDifference;
                        addCarryPosDifference--;
                    }
                    else
                    {
                        cursorPosition = addAnswerPosDifference;
                    }
                }
                else
                {
                    answerPanel.text = answerPanel.text.Remove(cursorPosition, 1);
                    answerPanel.text = answerPanel.text.Insert(cursorPosition, "2");
                    cursorPosition = addAnswerPosDifference;
                }
                //Debug.Log("1 cursorPosition: " + cursorPosition);
                if (currentCharacterCount < (2 * prevCharacterCount) - 2)
                {
                    odd = !odd;
                }
                else
                    odd = true;
            }
            else
            {
                // if (stepNo == 1)
                // {
                answerPanel.text = answerPanel.text.Insert(cursorPosition, "2");
                cursorPosition += cursorIncrement;
                /*
                }
                else
                {
                    answerPanel.text = answerPanel.text.Insert(cursorPosition, "2");
                    cursorPosition++;
                }*/
            }
            currentCharacterCount++;
        }
    }
    public void pressed3()
    {
        //string a = answerPanel.text.ToString();
        //answerPanel.text = a + 3;
        // Debug.Log("cursorPosition: " + cursorPosition + " " + answerPanel.text.Length);
        if (add)
        {
            if (ans)
            {
                if (odd)
                {
                    answerPanel.text = answerPanel.text.Insert(cursorPosition, "3");
                    addAnswer += 3 * (Mathf.Pow(10.0f, NoOfDigitInAnswer));
                    NoOfDigitInAnswer++;
                    if (currentCharacterCount < (2 * prevCharacterCount) - 2)
                    {
                        cursorPosition = addCarryPosDifference;
                        addCarryPosDifference--;
                    }
                    else
                    {
                        cursorPosition = addAnswerPosDifference;
                    }
                }
                else
                {
                    answerPanel.text = answerPanel.text.Remove(cursorPosition, 1);
                    answerPanel.text = answerPanel.text.Insert(cursorPosition, "3");
                    cursorPosition = addAnswerPosDifference;
                }
                //Debug.Log("1 cursorPosition: " + cursorPosition);
                if (currentCharacterCount < (2 * prevCharacterCount) - 2)
                {
                    odd = !odd;
                }
                else
                    odd = true;
            }
            else
            {
                //if (stepNo == 1)
                //{
                answerPanel.text = answerPanel.text.Insert(cursorPosition, "3");
                cursorPosition += cursorIncrement;
                /*
                }
                else
                {
                    answerPanel.text = answerPanel.text.Insert(cursorPosition, "3");
                    cursorPosition++;
                }*/
            }
            currentCharacterCount++;
        }
    }
    public void pressed4()
    {
        //string a = answerPanel.text.ToString();
        //answerPanel.text = a + 4;
        if (add)
        {
            if (ans)
            {
                if (odd)
                {
                    answerPanel.text = answerPanel.text.Insert(cursorPosition, "4");
                    addAnswer += 4 * (Mathf.Pow(10.0f, NoOfDigitInAnswer));
                    NoOfDigitInAnswer++;
                    //Debug.Log("currentCharacterCount: " + currentCharacterCount + "  ((2 * prevCharacterCount) - 1): " + ((2 * prevCharacterCount) - 1));
                    if (currentCharacterCount < ((2 * prevCharacterCount) - 2))
                    {
                        cursorPosition = addCarryPosDifference;
                        addCarryPosDifference--;
                    }
                    else
                    {
                        cursorPosition = addAnswerPosDifference;
                    }
                }
                else
                {
                    answerPanel.text = answerPanel.text.Remove(cursorPosition, 1);
                    answerPanel.text = answerPanel.text.Insert(cursorPosition, "4");
                    cursorPosition = addAnswerPosDifference;
                }
                //Debug.Log("1 cursorPosition: " + cursorPosition);
                if (currentCharacterCount < ((2 * prevCharacterCount) - 2))
                {
                    odd = !odd;
                }
                else
                    odd = true;
            }
            else
            {
                //  if (stepNo == 1)
                //{
                answerPanel.text = answerPanel.text.Insert(cursorPosition, "4");
                cursorPosition += cursorIncrement;
                /*  }
                  else
                  {
                      answerPanel.text = answerPanel.text.Insert(cursorPosition, "4");
                      cursorPosition++;
                  }*/
            }
            currentCharacterCount++;
        }
    }
    public void pressed5()
    {
        //string a = answerPanel.text.ToString();
        //answerPanel.text = a + 5;
        if (add)
        {
            if (ans)
            {
                if (odd)
                {
                    answerPanel.text = answerPanel.text.Insert(cursorPosition, "5");
                    addAnswer += 5 * (Mathf.Pow(10.0f, NoOfDigitInAnswer));
                    NoOfDigitInAnswer++;
                    if (currentCharacterCount < (2 * prevCharacterCount) - 2)
                    {
                        cursorPosition = addCarryPosDifference;
                        addCarryPosDifference--;
                    }
                    else
                    {
                        cursorPosition = addAnswerPosDifference;
                    }
                }
                else
                {
                    answerPanel.text = answerPanel.text.Remove(cursorPosition, 1);
                    answerPanel.text = answerPanel.text.Insert(cursorPosition, "5");
                    cursorPosition = addAnswerPosDifference;
                }
                //Debug.Log("1 cursorPosition: " + cursorPosition);
                if (currentCharacterCount < (2 * prevCharacterCount) - 2)
                {
                    odd = !odd;
                }
                else
                    odd = true;
            }
            else
            {
                // if (stepNo == 1)
                //{
                answerPanel.text = answerPanel.text.Insert(cursorPosition, "5");
                cursorPosition += cursorIncrement;
                /*  }
                  else
                  {
                      answerPanel.text = answerPanel.text.Insert(cursorPosition, "5");
                      cursorPosition++;
                  }*/
            }
            currentCharacterCount++;
        }
    }
    public void pressed6()
    {
        // string a = answerPanel.text.ToString();
        //answerPanel.text = a + 6;
        if (add)
        {
            if (ans)
            {
                if (odd)
                {
                    answerPanel.text = answerPanel.text.Insert(cursorPosition, "6");
                    addAnswer += 6 * (Mathf.Pow(10.0f, NoOfDigitInAnswer));
                    NoOfDigitInAnswer++;
                    if (currentCharacterCount < (2 * prevCharacterCount) - 2)
                    {
                        cursorPosition = addCarryPosDifference;
                        addCarryPosDifference--;
                    }
                    else
                    {
                        cursorPosition = addAnswerPosDifference;
                    }
                }
                else
                {
                    answerPanel.text = answerPanel.text.Remove(cursorPosition, 1);
                    answerPanel.text = answerPanel.text.Insert(cursorPosition, "6");
                    cursorPosition = addAnswerPosDifference;
                }
                //Debug.Log("1 cursorPosition: " + cursorPosition);
                if (currentCharacterCount < (2 * prevCharacterCount) - 2)
                {
                    odd = !odd;
                }
                else
                    odd = true;
            }
            else
            {
                //  if (stepNo == 1)
                //{
                answerPanel.text = answerPanel.text.Insert(cursorPosition, "6");
                cursorPosition += cursorIncrement;
                /*
                }
                else
                {
                    answerPanel.text = answerPanel.text.Insert(cursorPosition, "6");
                    cursorPosition++;
                }*/
            }
            currentCharacterCount++;
        }

    }
    public void pressed7()
    {
        //  string a = answerPanel.text.ToString();
        //answerPanel.text = a + 7;
        if (add)
        {
            if (ans)
            {
                if (odd)
                {
                    answerPanel.text = answerPanel.text.Insert(cursorPosition, "7");
                    addAnswer += 7 * (Mathf.Pow(10.0f, NoOfDigitInAnswer));
                    NoOfDigitInAnswer++;
                    if (currentCharacterCount < (2 * prevCharacterCount) - 2)
                    {
                        cursorPosition = addCarryPosDifference;
                        addCarryPosDifference--;
                    }
                    else
                    {
                        cursorPosition = addAnswerPosDifference;
                    }
                }
                else
                {
                    answerPanel.text = answerPanel.text.Remove(cursorPosition, 1);
                    answerPanel.text = answerPanel.text.Insert(cursorPosition, "7");
                    cursorPosition = addAnswerPosDifference;
                }
                //Debug.Log("1 cursorPosition: " + cursorPosition);
                if (currentCharacterCount < (2 * prevCharacterCount) - 2)
                {
                    odd = !odd;
                }
                else
                    odd = true;
            }
            else
            {
                // if (stepNo == 1)
                //{
                answerPanel.text = answerPanel.text.Insert(cursorPosition, "7");
                cursorPosition += cursorIncrement;
                /*
                }
                else
                {
                    answerPanel.text = answerPanel.text.Insert(cursorPosition, "7");
                    cursorPosition++;
                }*/
            }
            currentCharacterCount++;
        }
    }
    public void pressed8()
    {
        //  string a = answerPanel.text.ToString();
        // answerPanel.text = a + 8;
        if (add)
        {
            if (ans)
            {
                if (odd)
                {
                    answerPanel.text = answerPanel.text.Insert(cursorPosition, "8");
                    addAnswer+= 8 * (Mathf.Pow(10.0f, NoOfDigitInAnswer));
                    NoOfDigitInAnswer++;
                    if (currentCharacterCount < (2 * prevCharacterCount) - 2)
                    {
                        cursorPosition = addCarryPosDifference;
                        addCarryPosDifference--;
                    }
                    else
                    {
                        cursorPosition = addAnswerPosDifference;
                    }
                }
                else
                {
                    answerPanel.text = answerPanel.text.Remove(cursorPosition, 1);
                    answerPanel.text = answerPanel.text.Insert(cursorPosition, "8");
                    cursorPosition = addAnswerPosDifference;
                }
                //Debug.Log("1 cursorPosition: " + cursorPosition);
                if (currentCharacterCount < (2 * prevCharacterCount) - 2)
                {
                    odd = !odd;
                }
                else
                    odd = true;
            }
            else
            {
                // if (stepNo == 1)
                //{
                answerPanel.text = answerPanel.text.Insert(cursorPosition, "8");
                cursorPosition += cursorIncrement;
                /*
                }
                else
                {
                    answerPanel.text = answerPanel.text.Insert(cursorPosition, "8");
                    cursorPosition++;
                }*/
            }
            currentCharacterCount++;
        }
    }
    public void pressed9()
    {
        //string a = answerPanel.text.ToString();
        //answerPanel.text = a + 9;
        if (add)
        {
            if (ans)
            {
                if (odd)
                {
                    answerPanel.text = answerPanel.text.Insert(cursorPosition, "9");
                    addAnswer += 9 * (Mathf.Pow(10.0f, NoOfDigitInAnswer));
                    NoOfDigitInAnswer++;
                    if (currentCharacterCount < (2 * prevCharacterCount) - 2)
                    {
                        cursorPosition = addCarryPosDifference;
                        addCarryPosDifference--;
                    }
                    else
                    {
                        cursorPosition = addAnswerPosDifference;
                    }
                }
                else
                {
                    answerPanel.text = answerPanel.text.Remove(cursorPosition, 1);
                    answerPanel.text = answerPanel.text.Insert(cursorPosition, "9");
                    cursorPosition = addAnswerPosDifference;
                }
                //Debug.Log("1 cursorPosition: " + cursorPosition);
                if (currentCharacterCount < (2 * prevCharacterCount) - 2)
                {
                    odd = !odd;
                }
                else
                    odd = true;
            }
            else
            {
                // if (stepNo == 1)
                //{
                answerPanel.text = answerPanel.text.Insert(cursorPosition, "9");
                cursorPosition += cursorIncrement;
                /* }
                 else
                 {
                     answerPanel.text = answerPanel.text.Insert(cursorPosition, "9");
                     cursorPosition++;
                 }*/
            }
            currentCharacterCount++;
        }
    }
    public void pressed0()
    {
        // string a = answerPanel.text.ToString();
        //answerPanel.text = a + 0;
        if (add)
        {
            if (ans)
            {
                if (odd)
                {
                    answerPanel.text = answerPanel.text.Insert(cursorPosition, "0");
                    addAnswer += 0 * (Mathf.Pow(10.0f, NoOfDigitInAnswer));
                    NoOfDigitInAnswer++;
                    if (currentCharacterCount < (2 * prevCharacterCount) - 2)
                    {
                        cursorPosition = addCarryPosDifference;
                        addCarryPosDifference--;
                    }
                    else
                    {
                        cursorPosition = addAnswerPosDifference;
                    }
                }
                else
                {
                    answerPanel.text = answerPanel.text.Remove(cursorPosition, 1);
                    answerPanel.text = answerPanel.text.Insert(cursorPosition, "0");
                    cursorPosition = addAnswerPosDifference;
                }
                //Debug.Log("1 cursorPosition: " + cursorPosition);
                if (currentCharacterCount < (2 * prevCharacterCount) - 2)
                {
                    odd = !odd;
                }
                else
                    odd = true;
            }
            else
            {
                // if (stepNo == 1)
                // {
                answerPanel.text = answerPanel.text.Insert(cursorPosition, "0");
                cursorPosition += cursorIncrement;
                /*
                }
                else
                {
                    answerPanel.text = answerPanel.text.Insert(cursorPosition, "0");
                    cursorPosition++;
                }*/
            }
            currentCharacterCount++;
        }
    }

    public void AddNumber()
    {
        if (add)
        {
            adjustStepSpaces();
            /*
            string a = answerPanel.text.ToString();
            answerPanel.text = a + "\n "+"+  ";
            */
            PrintStepNo();
            // Debug.Log("cursorPosition AddNumber: " + cursorPosition +" "+ answerPanel.text.Length);
            answerPanel.text = answerPanel.text.Insert(cursorPosition, "+  ");
            cursorPosition += 3;
            // Debug.Log("step : " + stepStartPoint[stepStartPoint.Count - 1] + " value " + answerPanel.text[stepStartPoint[stepStartPoint.Count - 1]] + " value +1" + answerPanel.text[stepStartPoint[stepStartPoint.Count - 1]+3]);
            //Debug.Log("add cursorPosition: " + cursorPosition);
            stepStartPoint.Add(cursorPosition);
            //Debug.Log("count " + stepStartPoint.Count);
        }

    }

    public void NextStep()
    {
        /*
        //Debug.Log("Nest Step");
        string a = answerPanel.text.ToString();
        //answerPanel.text = a + "\n " + "\border[01]{ cchgm } ";
        answerPanel.text = answerPanel.text.Insert(0, "5");*/

        if (add)
        {
            //StopCoroutine(blinkCoroutine);
            StopCoroutine("blinkingCursor");
            answerPanel.text = answerPanel.text.Remove(cursorPosition, 1);
            if (ans)
            {
                Debug.Log("addAnswer: " + addAnswer);
            }
            else
            {
                adjustStepSpaces();
                /*
                string a = answerPanel.text.ToString();
                answerPanel.text = a + "\n "+"+  ";
                */
                PrintStepNo();
                // Debug.Log("cursorPosition AddNumber: " + cursorPosition +" "+ answerPanel.text.Length);
                answerPanel.text = answerPanel.text.Insert(cursorPosition, "\\vborder[0001 black]=  }");
                // as 17 digits were placed; keep 1 for later use
                cursorPosition += 23;
                ans = true;
                addAnswerPosDifference = cursorPosition;
                addCarryPosDifference = stepStartPoint[0] + (currentCharacterCount - 2);//(stepNo -1) * (currentCharacterCount + addExtraChar) + 3+23;
                Debug.Log("addCarryPosDifference: " + addCarryPosDifference + " addAnswerPosDifference: " + addAnswerPosDifference);
                // Debug.Log("step : " + stepStartPoint[stepStartPoint.Count - 1] + " value " + answerPanel.text[stepStartPoint[stepStartPoint.Count - 1]] + " value +1" + answerPanel.text[stepStartPoint[stepStartPoint.Count - 1]+3]);
                //Debug.Log("add cursorPosition: " + cursorPosition);
                //stepStartPoint.Add(cursorPosition);
                //Debug.Log("count " + stepStartPoint.Count);
            }
            //StartCoroutine(blinkCoroutine);
            StartCoroutine("blinkingCursor");
        }
    }
    
    void adjustStepSpaces()
    {
       /* foreach (int i in stepStartPoint)
        {
            Debug.Log("i: " + i + "val: " + answerPanel.text[i] + " i+1: " + answerPanel.text[i + 1]);
        }*/
        if(prevCharacterCount != -1)
        {
            if(prevCharacterCount  > currentCharacterCount)
            {
                int diff = prevCharacterCount - currentCharacterCount;
                cursorPosition = cursorPosition - (currentCharacterCount);
                while (diff > 0)
                {
                    if (diff == 1)
                    {
                        answerPanel.text = answerPanel.text.Insert(cursorPosition, spaceVar);
                        cursorPosition += cursorIncrement;
                    }
                    else
                    {
                        answerPanel.text = answerPanel.text.Insert(cursorPosition, spaceVar);
                        cursorPosition += cursorIncrement;
                    }
                    diff--;
                }
                stepStartPoint[stepStartPoint.Count - 1]= cursorPosition;
                cursorPosition += (currentCharacterCount);
            }
            else
            {
                if(prevCharacterCount < currentCharacterCount)
                {
                    int temp = cursorPosition;
                    for (int i = 0; i < stepStartPoint.Count - 1; i++)
                    {
                        cursorPosition = stepStartPoint[i];
                        int diff = currentCharacterCount - prevCharacterCount;
                        while (diff > 0)
                        {
                           // if (i == 0)
                            //{
                                answerPanel.text = answerPanel.text.Insert(cursorPosition, spaceVar);
                                cursorPosition += cursorIncrement;
                                stepStartPoint[i] = cursorPosition;
                                temp += 1;
                                diff--;
                                for (int j = i + 1; j < stepStartPoint.Count; j++)
                                {
                                    //Debug.Log("Adjust lower values i: " + i + "j " + j + "stepStartPoint[j]: " + stepStartPoint[j]);
                                    stepStartPoint[j] = stepStartPoint[j] + cursorIncrement;
                                   // Debug.Log("After Adjust lower values i: " + i + "stepStartPoint[j]: " + stepStartPoint[j]);
                                }
                                /*
                            }
                            else
                            {
                                Debug.Log("Space Adjustment i: " + i + "cursorPosition: " + cursorPosition);
                                answerPanel.text = answerPanel.text.Insert(cursorPosition, spaceVar);
                                cursorPosition += 1;
                                stepStartPoint[i] = cursorPosition;
                                temp += 1;
                                diff--;
                                for (int j = i + 1; j < stepStartPoint.Count; j++)
                                {
                                    Debug.Log("Adjust lower values i: " + i + "j " + j + "stepStartPoint[j]: " + stepStartPoint[j]);
                                    stepStartPoint[j] = stepStartPoint[j] + 1;
                                    Debug.Log("After Adjust lower values i: " + i + "stepStartPoint[j]: " + stepStartPoint[j]);
                                }
                            }*/

                        }
                    }
                    cursorPosition = temp;

                }
                prevCharacterCount = currentCharacterCount;
            }
        }
        else
        {
            prevCharacterCount = currentCharacterCount;
        }
        
        currentCharacterCount = 0;
        /*
        foreach (int i in stepStartPoint)
        {
            Debug.Log("after i: " + i + "val: " + answerPanel.text[i] +" i+1: " + answerPanel.text[i + 1]);
        }*/
    }

    public void pressedTAB()
    {
        if (add)
        {
            if (ans)
            {
                if (odd)
                {
                    Debug.Log("you need some Values");
                }
                else
                {
                    answerPanel.text = answerPanel.text.Remove(cursorPosition, 1);
                    answerPanel.text = answerPanel.text.Insert(cursorPosition, " ");
                    cursorPosition = addAnswerPosDifference;
                    currentCharacterCount++;
                    if (currentCharacterCount < (2 * prevCharacterCount) - 2)
                    {
                        odd = !odd;
                    }
                    else
                        odd = true;
                }
                //Debug.Log("1 cursorPosition: " + cursorPosition);
               
            }
            else
            {

            }
            
        }
    }

    IEnumerator blinkingCursor()
    {
        yield return new WaitForSeconds(1.0f);
        if (add)
        {
            if (ans)
            {
                if (odd)
                {
                    if (blink)
                    {
                        answerPanel.text = answerPanel.text.Remove(cursorPosition-1, 1);
                        answerPanel.text = answerPanel.text.Insert(cursorPosition-1, "0");
                    }
                    else
                    {
                        answerPanel.text = answerPanel.text.Remove(cursorPosition-1, 1);
                        answerPanel.text = answerPanel.text.Insert(cursorPosition-1, " ");
                    }
                }
                else
                {
                    if (blink)
                    {
                        answerPanel.text = answerPanel.text.Remove(cursorPosition, 1);
                        answerPanel.text = answerPanel.text.Insert(cursorPosition, "0");
                    }
                    else
                    {
                        answerPanel.text = answerPanel.text.Remove(cursorPosition, 1);
                        answerPanel.text = answerPanel.text.Insert(cursorPosition, " ");
                    }
                }
                //Debug.Log("1 cursorPosition: " + cursorPosition);

            }
            else
            {
                if (blink)
                {
                    answerPanel.text = answerPanel.text.Remove(cursorPosition, 1);
                    answerPanel.text = answerPanel.text.Insert(cursorPosition, "0");
                }
                else
                {
                    answerPanel.text = answerPanel.text.Remove(cursorPosition, 1);
                    answerPanel.text = answerPanel.text.Insert(cursorPosition, " ");
                }
            }

        }
        else
        {
            if (blink)
            {
                answerPanel.text = answerPanel.text.Remove(cursorPosition, 1);
                answerPanel.text = answerPanel.text.Insert(cursorPosition, "0");
            }
            else
            {
                answerPanel.text = answerPanel.text.Remove(cursorPosition, 1);
                answerPanel.text = answerPanel.text.Insert(cursorPosition, " ");
            }
        }
        
        blink = !blink;
        StartCoroutine("blinkingCursor");
       // StartCoroutine(blinkCoroutine);
    }
    
    /*
    void adjustStepSpaces()
    {
        //Debug.Log("adjustStepSpaces: prevCharacterCount" + prevCharacterCount + " currentCharacterCount: " + currentCharacterCount);
        if (prevCharacterCount != 0)
        {
            if (prevCharacterCount > currentCharacterCount)
            {
                int diff = prevCharacterCount - currentCharacterCount;
                // Debug.Log("before change diff cursorPosition: " + cursorPosition + " answerPanel.text.Length " + answerPanel.text.Length);
                cursorPosition = cursorPosition - (currentCharacterCount);
                //Debug.Log("before diff cursorPosition: "+ cursorPosition + " answerPanel.text.Length " + answerPanel.text.Length);
                while (diff > 0)
                {
                    
                        answerPanel.text = answerPanel.text.Insert(cursorPosition, "0");
                        cursorPosition += 1;
                   
                    diff--;
                }
                stepStartPoint.Insert(stepStartPoint.Count - 1, cursorPosition);
                cursorPosition += (currentCharacterCount);
                // Debug.Log("diff cursorPosition: " + cursorPosition + " answerPanel.text.Length "+ answerPanel.text.Length);
            }
            else
            {
                if (prevCharacterCount < currentCharacterCount)
                {
                    int temp = cursorPosition;
                    //Debug.Log("stepStartPoint.Count: " + stepStartPoint.Count);
                    for (int i = 0; i < stepStartPoint.Count - 2; i++)
                    {
                        cursorPosition = stepStartPoint[i];
                        //Debug.Log("cursor value: " + answerPanel.text[cursorPosition] + "next " + answerPanel.text[cursorPosition + 1] + " stepStartPoint.Count:" + stepStartPoint.Count);
                        int diff = currentCharacterCount - prevCharacterCount;
                        Debug.Log("diff: " + diff+" i: "+i + " cursorPosition: "+ cursorPosition);
                        while (diff > 0)
                        {
                            answerPanel.text = answerPanel.text.Insert(cursorPosition, "0");
                            cursorPosition += 1;
                            stepStartPoint[i] = cursorPosition;
                            temp += 1;
                            diff--;
                            for (int j = i + 1; j < stepStartPoint.Count - 1; j++)
                            {
                                stepStartPoint[j] = stepStartPoint[j] + 1;
                                Debug.Log("Change positio j: " + j + " stepStartPoint[j]: " + stepStartPoint[j]);
                               // Debug.Log("j " + j + " stepStartPoint[j]  " + stepStartPoint[j] + "value:" + answerPanel.text[stepStartPoint[j] + 2]);
                            }
                            // Debug.Log("diff cursorPosition: " + cursorPosition + " diff " + diff + "temp");


                        }
                    }
                    cursorPosition = temp;

                }
                prevCharacterCount = currentCharacterCount;
            }
        }
        else
        {
            prevCharacterCount = currentCharacterCount;
        }

        currentCharacterCount = 0;
    }*/

}
