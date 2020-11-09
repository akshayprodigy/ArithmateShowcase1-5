    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManagercopy : MonoBehaviour {

    // Use this for initialization
    public GameObject sighn, exp , StartPosition, targetGameObject;
    bool showSighn = false;
    public float speed = 1.0F;
    private float startTime;
    //Vector3 endPos;
    public TEXDraw answerPanel,stepPanel;
    private int stepNo;
    private int cursorPosition;
    private int currentCharacterCount,prevCharacterCount,stepCharCount = 15;
    private List<int> stepStartPoint;
    void Start () {
        stepStartPoint = new List<int>();
        stepNo = 1;
        currentCharacterCount = prevCharacterCount = 0;
        cursorPosition = 0;
        PrintStepNo();
	}
	
    void PrintStepNo()
    {
        if (stepNo == 1)
        {
            answerPanel.text = answerPanel.text.Insert(cursorPosition, "\n Step " + stepNo + " :         ");
            stepNo++;
            cursorPosition += (stepCharCount+4);
            stepStartPoint.Add(cursorPosition);
        }
        else
        {
           //Debug.Log("PrintStepNo cursorPosition: " + cursorPosition + " answerPanel.text.Length " + answerPanel.text.Length);
            answerPanel.text = answerPanel.text.Insert(cursorPosition, "\n Step " + stepNo + " :     ");
            stepNo++;
            cursorPosition += stepCharCount;
            //Debug.Log("Print StepNo"+ "stepCharCount: "+ stepCharCount+" cursorPosition: " + cursorPosition + " answerPanel.text.Length " + answerPanel.text.Length);
        }
       
        //Debug.Log("PrintStepNo cursorPosition: " + cursorPosition+ "value:"+ answerPanel.text[cursorPosition]);
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
	}*/
    public void ShowSighn()
    {
        // Show Shigh
       // Debug.Log("ShowSighn");
        showSighn = true;
        startTime = Time.time;
        StartCoroutine(MoveUi());
    }

    private IEnumerator MoveUi()
    {
        while (showSighn)
        {
            while (Vector3.Distance(StartPosition.transform.position, targetGameObject.transform.position) > 1f)
            {
                sighn.transform.position = Vector3.MoveTowards(sighn.transform.position, targetGameObject.transform.position, Time.deltaTime * speed);
                yield return new WaitForSeconds(0.02f);
            }
        }
    }

    public void pressed1()
    { //string a = answerPanel.text.ToString();
        //answerPanel.text = a + 1;
       // Debug.Log("cursorPosition: " + cursorPosition + " " + answerPanel.text.Length);
        answerPanel.text = answerPanel.text.Insert(cursorPosition, "1" );
        cursorPosition++;
        currentCharacterCount++;
    }
    public void pressed2()
    {
        //string a = answerPanel.text.ToString();
        //answerPanel.text = a + 2;
       // Debug.Log("cursorPosition: " + cursorPosition + " " + answerPanel.text.Length);
        answerPanel.text = answerPanel.text.Insert(cursorPosition, "2");
        cursorPosition++;
        currentCharacterCount++;
    }
    public void pressed3()
    {
        //string a = answerPanel.text.ToString();
        //answerPanel.text = a + 3;
       // Debug.Log("cursorPosition: " + cursorPosition + " " + answerPanel.text.Length);
        answerPanel.text = answerPanel.text.Insert(cursorPosition, "3");
        cursorPosition++;
        currentCharacterCount++;
    }
    public void pressed4()
    {
        //string a = answerPanel.text.ToString();
        //answerPanel.text = a + 4;
        answerPanel.text = answerPanel.text.Insert(cursorPosition, "4");
        cursorPosition++;
        currentCharacterCount++;
    }
    public void pressed5()
    {
        //string a = answerPanel.text.ToString();
        //answerPanel.text = a + 5;
        answerPanel.text = answerPanel.text.Insert(cursorPosition, "5");
        cursorPosition++;
        currentCharacterCount++;
    }
    public void pressed6()
    {
       // string a = answerPanel.text.ToString();
        //answerPanel.text = a + 6;
        answerPanel.text = answerPanel.text.Insert(cursorPosition, "6");
        cursorPosition++;
        currentCharacterCount++;

    }
    public void pressed7()
    {
      //  string a = answerPanel.text.ToString();
        //answerPanel.text = a + 7;
        answerPanel.text = answerPanel.text.Insert(cursorPosition, "7");
        cursorPosition++;
        currentCharacterCount++;
    }
    public void pressed8()
    {
      //  string a = answerPanel.text.ToString();
       // answerPanel.text = a + 8;
        answerPanel.text = answerPanel.text.Insert(cursorPosition, "8");
        cursorPosition++;
        currentCharacterCount++;
    }
    public void pressed9()
    {
        //string a = answerPanel.text.ToString();
        //answerPanel.text = a + 9;
        answerPanel.text = answerPanel.text.Insert(cursorPosition, "9");
        cursorPosition++;
        currentCharacterCount++;
    }
    public void pressed0()
    {
       // string a = answerPanel.text.ToString();
        //answerPanel.text = a + 0;
        answerPanel.text = answerPanel.text.Insert(cursorPosition, "0");
        cursorPosition++;
        currentCharacterCount++;
    }

    public void AddNumber()
    {
        adjustStepSpaces();
        /*
        string a = answerPanel.text.ToString();
        answerPanel.text = a + "\n "+"+  ";
        */
        PrintStepNo();
       // Debug.Log("cursorPosition AddNumber: " + cursorPosition +" "+ answerPanel.text.Length);
        answerPanel.text = answerPanel.text.Insert(cursorPosition, "+  ");
        cursorPosition +=3;
        stepStartPoint.Add(cursorPosition);

    }

    public void NextStep()
    {
        Debug.Log("Nest Step");
        string a = answerPanel.text.ToString();
        //answerPanel.text = a + "\n " + "\border[01]{ cchgm } ";
        answerPanel.text = answerPanel.text.Insert(0, "5");
    }
    
    void adjustStepSpaces()
    {
        //Debug.Log("adjustStepSpaces: prevCharacterCount" + prevCharacterCount + " currentCharacterCount: " + currentCharacterCount);
        if(prevCharacterCount != 0)
        {
            if(prevCharacterCount  > currentCharacterCount)
            {
                int diff = prevCharacterCount - currentCharacterCount;
               // Debug.Log("before change diff cursorPosition: " + cursorPosition + " answerPanel.text.Length " + answerPanel.text.Length);
                cursorPosition = cursorPosition - (currentCharacterCount);
                //Debug.Log("before diff cursorPosition: "+ cursorPosition + " answerPanel.text.Length " + answerPanel.text.Length);
                while (diff > 0)
                {
                    if (diff == 1)
                    {
                        answerPanel.text = answerPanel.text.Insert(cursorPosition, " ");
                        cursorPosition += 1;
                    }
                    else
                    {
                        answerPanel.text = answerPanel.text.Insert(cursorPosition, "  ");
                        cursorPosition += 2;
                    }
                    diff--;
                }
                stepStartPoint.Insert(stepStartPoint.Count - 1, cursorPosition);
                cursorPosition += (currentCharacterCount);
               // Debug.Log("diff cursorPosition: " + cursorPosition + " answerPanel.text.Length "+ answerPanel.text.Length);
            }
            else
            {
                if(prevCharacterCount < currentCharacterCount)
                {
                    int temp = cursorPosition;
                    for (int i = 0; i < stepStartPoint.Count - 1; i++)
                    {
                        cursorPosition = stepStartPoint[i];
                        Debug.Log("cursor value: " + answerPanel.text[cursorPosition] +"next "+ answerPanel.text[cursorPosition+1] +" stepStartPoint.Count:" + stepStartPoint.Count);
                        int diff = currentCharacterCount - prevCharacterCount;
                        while (diff > 0)
                        {
                            answerPanel.text = answerPanel.text.Insert(cursorPosition, " ");
                            cursorPosition += 1;
                            stepStartPoint[i] = cursorPosition;
                            temp += 1;
                            diff--;
                            for (int j = i+1; j < stepStartPoint.Count - 1; j++)
                            {
                                stepStartPoint[j] = stepStartPoint[j] + 1;
                                Debug.Log( "j " + j + " stepStartPoint[j]  " + stepStartPoint[j] + "value:" + answerPanel.text[stepStartPoint[j]+2]);
                            }
                           // Debug.Log("diff cursorPosition: " + cursorPosition + " diff " + diff + "temp");


                        }
                    }
                    cursorPosition = temp;
                    /*
                     for (int i = 0; i < stepStartPoint.Count-1; i++)
                     {
                         Debug.Log("curr Big " + cursorPosition);
                         int diff = currentCharacterCount - prevCharacterCount;
                         cursorPosition = stepStartPoint[i];
                         Debug.Log("curr Big " + cursorPosition+ "i "+i+ " diff "+ diff);
                         while (diff > 0)
                         {
                             answerPanel.text = answerPanel.text.Insert(cursorPosition, "  ");
                             cursorPosition += 2;
                             diff--;
                             for (int j = 0; j < stepStartPoint.Count - 1; j++)
                             {
                                 stepStartPoint[j] = stepStartPoint[j] + 2;
                                 Debug.Log("curr Big " + cursorPosition + "j " + j + " stepStartPoint[j]  " + stepStartPoint[j]);
                             }
                         }

                     } //*/ 

                }
                prevCharacterCount = currentCharacterCount;
            }
        }
        else
        {
            prevCharacterCount = currentCharacterCount;
        }
        
        currentCharacterCount = 0;
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
