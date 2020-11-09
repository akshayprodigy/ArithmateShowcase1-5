using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SimpleJSON;
using System;

public class CanvasManager : MonoBehaviour
{

    // Use this for initialization
    public GameObject sighn, exp, StartPosition, targetGameObject, lcmOption, current2ndpos = null, roughworkObject, maincanvas, loading, brackets, StringKeyBoard, showKeyboardpos, hidekeyboardpos;
    MainGridLayoutManager _mMainWorkManager;
    public List<GameObject> roughworkList;
    bool showSighn = false, showFun = false;
    public float speed = 200.0F;
    private float startTime;
    //Vector3 endPos;
    public TEXDraw answerPanel, stepPanel, questionType, questionValue;
    private int stepNo;
    private int cursorPosition, stepCursorPosition;
    private int currentCharacterCount, prevCharacterCount, stepCharCount = 10, stepAnswerCharcCount = 1, cursorIncrement = 1;
    private List<int> stepStartPoint;
    private string spaceVar = " ", cursorVar = "\\vborder[1000 black]";
    bool add, ans, odd = true, blink = true, sub, roughworkOn = false;
    int addCarryPosDifference = 0, addAnswerPosDifference = 0, addExtraChar = 6, cursorVarCount = 20;
    float NoOfDigitInAnswer = 0.0f, addAnswer = 0;
    private IEnumerator blinkCoroutine;
    char a = '\\';
    GameObject CurrentRoughwork;
    string scene3d = "screen setup";
    public GameObject dialog, dialogYesNo;
    const string enum_improper_fraction = "enum_improper_fraction", op_addition = "op_addition", op_isequal = "op_isequal", op_comma = "op_comma", enum_num = "enum_num", enum_mixed_fraction = "enum_mixed_fraction", enum_text = "enum_text", op_multiply = "op_multiply", op_div = "op_div", op_divide = "op_divide";
    bool animation_in_progress = false;
    public bool dialog_on, loading_on, reopenRoughWork = false;
    string dashboard_scene = "Home";//"SampleScene";//"Menu_Page 1";
    bool hasRoughwork = false;
    string alphaValue = "";
    public TEXDraw alphaNumTex;
    public GameObject fullScreenMsg;
    public string buttonToPress = "";

    public bool isTutorial = false;

    public delegate void NumberClickedAction(int value);
    public static event NumberClickedAction OnNumberClickedAction;
    public delegate void NextLineAction();
    public static event NextLineAction OnNextLineAction;
    public delegate void MixedFractionAction();
    public static event MixedFractionAction OnMixedFractionAction;
    public delegate void FractionAction();
    public static event FractionAction OnFractionAction;
    public delegate void TextAction(string value);
    public static event TextAction OnTextAction;
    public delegate void ExpClickedAction(string value);
    public static event ExpClickedAction OnExpClickedAction;
    public delegate void NextStepAction();
    public static event NextStepAction OnNextStepAction;
    public delegate void DeleteAction();
    public static event DeleteAction OnDeleteAction;
    public delegate void TabAction();
    public static event TabAction OnTabAction;
    public delegate void LCMAction();
    public static event LCMAction OnLCMAction;
    public delegate void HCFAction();
    public static event HCFAction OnHCFAction;
    public delegate void AnswerAction();
    public static event AnswerAction OnAnswerAction;
    public delegate void DivideAction();
    public static event DivideAction OnDivideAction;
    public delegate void RoughworkAction(int index);
    public static event RoughworkAction OnRoughworkAction;
    public delegate void RoughworkOpenAction();
    public static event RoughworkOpenAction OnRoughworkOpenAction;
    public delegate void ReOpenRoughworkAction();
    public static event ReOpenRoughworkAction OnReOpenRoughworkAction;
    public delegate void AdditionAction();
    public static event AdditionAction OnAdditionAction;
    public delegate void NextQuestion();
    public static event NextQuestion OnNextQuestion;
    public delegate void ClearCanvas();
    public static event ClearCanvas OnClearCanvas;
    public delegate void NextDontKnowQuestion();
    public static event NextDontKnowQuestion OnNextDontKnowQuestion;
    public delegate void ClearCurrentRow();
    public static event ClearCurrentRow OnClearCurrentRow;
    public delegate void LogMessage(string cases);
    public static event LogMessage onLogMessage;
    public delegate void testOnTutorial(string value);
    public static event testOnTutorial OntestOnTutorial;


    private void OnEnable()
    {
        NewRoughManager.OnRoughWorkBackAction += backFromRoughWork;
        RowController.OnOpenRoughWorkAction += reOpenRoughWorkArea;
        StepControllerScript.OnOpenRoughWorkAction += reOpenRoughWorkArea;
        MainGridLayoutManager.OnShowMessage += showMessage;
        CallRESTServices.OnQuestionDownloaded += showQuestion;
        CallRESTServices.OnShowMessage += showMessage;
        MainGridLayoutManager.OndownloadNextQuestion += showDownloading;
        DialogControllerScript.OnCloseDialog += closeMessage;
        NewRoughManager.OnHasRoughWork += addRoughWork;
        MainGridLayoutManager.OnShowFullScreenMsg += showfullscrenandHide;
    }

    private void OnDisable()
    {
        NewRoughManager.OnRoughWorkBackAction -= backFromRoughWork;
        RowController.OnOpenRoughWorkAction -= reOpenRoughWorkArea;
        StepControllerScript.OnOpenRoughWorkAction -= reOpenRoughWorkArea;
        MainGridLayoutManager.OnShowMessage -= showMessage;
        CallRESTServices.OnQuestionDownloaded -= showQuestion;
        CallRESTServices.OnShowMessage -= showMessage;
        MainGridLayoutManager.OndownloadNextQuestion -= showDownloading;
        DialogControllerScript.OnCloseDialog -= closeMessage;
        NewRoughManager.OnHasRoughWork -= addRoughWork;
        MainGridLayoutManager.OnShowFullScreenMsg -= showfullscrenandHide;
    }

    void Start()
    {
        _mMainWorkManager = maincanvas.GetComponent<MainGridLayoutManager>();
        dialog.SetActive(false);
        dialogYesNo.SetActive(false);
        add = true;
        //sub = true;
        stepStartPoint = new List<int>();
        stepNo = 0;
        currentCharacterCount = prevCharacterCount = -1;
        cursorPosition = stepCursorPosition = 2;
        animation_in_progress = false;
        dialog_on = false;
        loading_on = false;
        //Debug.Log("Start");
        showDownloading();
        PrintStepNo();
        skippOneLine();
        //blinkCoroutine =  blinkingCursor();
        StartCoroutine("blinkingCursor");
        //loading.SetActive(true);
        hideFullScreenMsg();
        //JSONNode responce = JSON.Parse(UtilityREST.QuestionData);

        //showQuestion(responce);
        Debug.Log("isTutorial: " + isTutorial);
        if(!isTutorial)
            ShowQuestionFromTutorial(UtilityREST.QuestionData);
    }

    public void ShowQuestionFromTutorial(string question)
    {
        Debug.LogError("ShowQuestionFromTutorial question: " + question);
        JSONNode responce = JSON.Parse(question);

        showQuestion(responce);
    }


    void showfullscrenandHide()
    {
        showFullScreenMsg();
        Invoke("hideFullScreenMsg", 10f);
    }

    void showFullScreenMsg()
    {
        fullScreenMsg.SetActive(true);

    }

    void hideFullScreenMsg()
    {
        fullScreenMsg.SetActive(false);
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
            //Debug.Log("count " + stepStartPoint.Count);
        }
        else if (sub)
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
            //Debug.Log("count " + stepStartPoint.Count);
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
                stepPanel.text = stepPanel.text.Insert(stepCursorPosition, "\n      " + "   ");
                //Debug.Log("PrintStepNo cursorPosition: " + cursorPosition + "value:" + answerPanel.text.Length);
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
                if (stepNo == 1)
                {
                    stepPanel.text = stepPanel.text.Insert(stepCursorPosition, "\n Step " + stepNo + " :");
                    stepCursorPosition += (stepCharCount);
                }
                //Debug.Log("PrintStepNo cursorPosition: " + cursorPosition + "value:" + answerPanel.text.Length);
                answerPanel.text = answerPanel.text.Insert(cursorPosition, "\n");
                stepNo++;

                cursorPosition += (stepAnswerCharcCount);
            }

            //Debug.Log("PrintStepNo cursorPosition: " + cursorPosition+ "value:"+ answerPanel.text.Length);
        }
        else if (sub)
        {
            if (stepNo == 0)
            {
                /*
                answerPanel.text = answerPanel.text.Insert(cursorPosition, "\n Step " + stepNo + " :         ");
                stepNo++;
                cursorPosition += (stepCharCount+4);
                stepStartPoint.Add(cursorPosition);*/
                //Debug.Log("PrintStepNo stepCursorPosition: " + stepCursorPosition + "value:" + stepPanel.text.Length);
                stepPanel.text = stepPanel.text.Insert(stepCursorPosition, "\n      " + "   ");
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

    public void dontKnow()
    {
        if (OnNextDontKnowQuestion != null)
            OnNextDontKnowQuestion();
    }

    void closeMessage()
    {
        dialog_on = false;
    }

    void showMessage(string msg)
    {

        if (!dialog.active)
            dialog.SetActive(true);
        if (onLogMessage != null)
        {
            onLogMessage(msg);
        }
        dialog.GetComponent<DialogControllerScript>().msg = msg;
        dialog.GetComponent<DialogControllerScript>().showMessage();
        dialog_on = true;
    }

    void showYesNoMsg(string msg)
    {
        if (onLogMessage != null)
        {
            onLogMessage(msg);
        }
        if (!dialogYesNo.active)
            dialogYesNo.SetActive(true);
        dialogYesNo.GetComponent<YesNoDialog>().showMessage(msg);
        dialog_on = true;
    }

    public void closeYesNoDialog()
    {
        dialogYesNo.SetActive(false);
        dialog_on = false;
    }

    public void OkYesNoDialog()
    {
        dialogYesNo.SetActive(false);
        dialog_on = false;
        clearCanvas();
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
        if (!dialog_on && !loading_on)
        {
            if (!animation_in_progress && current2ndpos != sighn)
            {
                if (isTutorial)
                {
                    if(!string.Equals(buttonToPress, CanvasTutorialManager.onFractionButton)){
                        showMessage("Please click on the shown buttons");
                        if (OntestOnTutorial != null)
                            OntestOnTutorial(CanvasTutorialManager.onError);
                        return;
                    }
                    else
                    {
                        if (OntestOnTutorial != null)
                            OntestOnTutorial(CanvasTutorialManager.onSuccess);
                    }
                }
                if (current2ndpos != null)
                {
                    StartCoroutine(MoveUiBack(current2ndpos));
                }
                showSighn = true;
                StartCoroutine(MoveUi(sighn));
                current2ndpos = sighn;
                //if (isTutorial)
                //{
                //    if (OntestOnTutorial != null)
                //        OntestOnTutorial(CanvasTutorialManager.onFractionButton);
                //}
            }
        }
    }

    void closePannel()
    {
        if (current2ndpos != null)
        {
            StartCoroutine(MoveUiBack(current2ndpos));
        }
        current2ndpos = null;
    }

    public void ShowFun()
    {
        if (!dialog_on && !loading_on)
        {
            if (!animation_in_progress && current2ndpos != exp)
            {
                if (isTutorial)
                {
                    if (!string.Equals(buttonToPress, CanvasTutorialManager.onNotRequired))
                    {
                        showMessage("Please click on the shown buttons");
                        if (OntestOnTutorial != null)
                            OntestOnTutorial(CanvasTutorialManager.onError);
                        return;
                    }
                    else
                    {
                        if (OntestOnTutorial != null)
                            OntestOnTutorial(CanvasTutorialManager.onSuccess);
                    }
                }
                if (current2ndpos != null)
                {
                    StartCoroutine(MoveUiBack(current2ndpos));
                }
                StartCoroutine(MoveUi(exp));
                current2ndpos = exp;
                showFun = true;
            }
        }
    }

    public void ShowLCMOption()
    {
        if (!dialog_on && !loading_on)
        {
            if (!animation_in_progress && current2ndpos != lcmOption)
            {

                if (!string.Equals(buttonToPress, CanvasTutorialManager.onNotRequired))
                {
                    showMessage("Please click on the shown buttons");
                    if (OntestOnTutorial != null)
                        OntestOnTutorial(CanvasTutorialManager.onError);
                    return;
                }
                else
                {
                    if (OntestOnTutorial != null)
                        OntestOnTutorial(CanvasTutorialManager.onSuccess);
                }

                if (current2ndpos != null)
                {
                    StartCoroutine(MoveUiBack(current2ndpos));
                }
                StartCoroutine(MoveUi(lcmOption));
                current2ndpos = lcmOption;
            }
        }
    }

    public void ShowBrackets()
    {
        if (!dialog_on && !loading_on)
        {
            if (!animation_in_progress && current2ndpos != brackets)
            {
                if (!string.Equals(buttonToPress, CanvasTutorialManager.onNotRequired))
                {
                    showMessage("Please click on the shown buttons");
                    if (OntestOnTutorial != null)
                        OntestOnTutorial(CanvasTutorialManager.onError);

                    return;
                }
                else
                {
                    if (OntestOnTutorial != null)
                        OntestOnTutorial(CanvasTutorialManager.onSuccess);
                }

                if (current2ndpos != null)
                {
                    StartCoroutine(MoveUiBack(current2ndpos));
                }
                StartCoroutine(MoveUi(brackets));
                current2ndpos = brackets;
            }
        }
    }

    private IEnumerator MoveUiBack(GameObject obj)
    {
        animation_in_progress = true;
        while (Vector3.Distance(obj.transform.position, StartPosition.transform.position) > 1f)
        {
            obj.transform.position = Vector3.MoveTowards(obj.transform.position, StartPosition.transform.position, Time.deltaTime * speed);
            yield return new WaitForSeconds(0.0002f);
        }
        animation_in_progress = false;
    }

    private IEnumerator MoveUi(GameObject obj)
    {
        while (Vector3.Distance(obj.transform.position, targetGameObject.transform.position) > 1f)
        {
            obj.transform.position = Vector3.MoveTowards(obj.transform.position, targetGameObject.transform.position, Time.deltaTime * speed);
            yield return new WaitForSeconds(0.002f);
        }

    }

    public void showKeyBoard()
    {
        alphaValue = "";
        ShowAlphaNumInTex();
        StartCoroutine(MoveKeyBoard(showKeyboardpos.transform.position));
    }

    public void HideKeyBoard()
    {
        StartCoroutine(MoveKeyBoard(hidekeyboardpos.transform.position));
    }

    public void AlphaNumericalClick(string val)
    {
        alphaValue += val;
        ShowAlphaNumInTex();
    }

    public void ClearAllAlphaNumeric()
    {
        alphaValue = "";
        ShowAlphaNumInTex();
    }

    public void AlphaNubericalBackSpace()
    {
        if (alphaValue.Length > 1)
            alphaValue = alphaValue.Remove(alphaValue.Length - 1, 1);
        ShowAlphaNumInTex();
    }

    void ShowAlphaNumInTex()
    {
        alphaNumTex.text = alphaValue;
    }

    public void CapsLockOn()
    {

    }

    private IEnumerator MoveKeyBoard(Vector3 _pos)
    {
        while (Vector3.Distance(StringKeyBoard.transform.position, _pos) > 1f)
        {
            StringKeyBoard.transform.position = Vector3.MoveTowards(StringKeyBoard.transform.position, _pos, Time.deltaTime * speed);
            yield return new WaitForSeconds(0.002f);
        }

    }

    void closeDiologIfOpen()
    {
        if (dialog.active)
            dialog.SetActive(false);
        closeMessage();
    }

    public void showQuestion(JSONNode question)
    {
        // stop loading sigh
        //Debug.Log("show Question");
        closeDiologIfOpen();
        
        string qName = question["Qtype_Name"].Value;
        questionType.text = questionType.text.Remove(0);
        questionType.text = questionType.text.Insert(questionType.text.Length, qName);
        Debug.Log("Solution " + UtilityREST.solution_No + " JSON " + UtilityREST.tlcd_diff_level_json);

        if (!isTutorial)
        {
            JSONNode diff_Json = JSON.Parse(UtilityREST.tlcd_diff_level_json);
            JSONNode SPECIFIC = diff_Json["NOQ"]["SPECIFIC"];
            JSONArray SPECIFIC_array = (JSONArray)SPECIFIC;
            JSONNode specific_step = SPECIFIC_array[UtilityREST.solution_No];
            //Debug.Log("S_NOTE :" + specific_step);
            string S_NOTE = specific_step["S_NOTE"].Value;

            if (String.Compare(S_NOTE, "") == 0)
            {

            }
            else
            {
                questionType.text = questionType.text.Insert(questionType.text.Length, "(" + S_NOTE + ")");
            }
           
        }

        questionValue.text = questionValue.text.Remove(0);
        foreach (JSONNode data in question["Question_Format"])
        {
            //Debug.Log("qName :" + data.ToString());
            string Name = data["Name"].Value;
            switch (Name)
            {
                case enum_improper_fraction:
                    questionValue.text = questionValue.text.Insert(questionValue.text.Length, "\\frac");
                    string n = data["N"].Value;
                    questionValue.text = questionValue.text.Insert(questionValue.text.Length, "{" + n + "}");
                    string d = data["D"].Value;
                    questionValue.text = questionValue.text.Insert(questionValue.text.Length, "{" + d + "}");
                    if (UtilityArtifacts.isobj16)
                    {
                        ImproperFraction fac = new ImproperFraction();
                        fac.num.Add(int.Parse(n));
                        fac.dem.Add(int.Parse(d));
                        UtilityArtifacts.ansfractionList.Add(fac);
                        //UtilityArtifacts.NumeratorObj16 = int.Parse(n);
                        //UtilityArtifacts.DemoninatorObj16 = int.Parse(d);
                    }
                    break;
                case enum_mixed_fraction:
                    string mw = data["W"].Value;
                    questionValue.text = questionValue.text.Insert(questionValue.text.Length, "{" + mw + "}");
                    questionValue.text = questionValue.text.Insert(questionValue.text.Length, "\\frac");
                    string mn = data["N"].Value;
                    questionValue.text = questionValue.text.Insert(questionValue.text.Length, "{" + mn + "}");
                    string md = data["D"].Value;
                    questionValue.text = questionValue.text.Insert(questionValue.text.Length, "{" + md + "}");
                    break;
                case op_addition:
                    questionValue.text = questionValue.text.Insert(questionValue.text.Length, "+");
                    break;
                case op_isequal:
                    questionValue.text = questionValue.text.Insert(questionValue.text.Length, "=");
                    break;
                case op_multiply:
                    questionValue.text = questionValue.text.Insert(questionValue.text.Length, "\\times");
                    break;
                case op_div:
                    questionValue.text = questionValue.text.Insert(questionValue.text.Length, "\\div");
                    break;
                case op_divide:
                    questionValue.text = questionValue.text.Insert(questionValue.text.Length, "\\div");
                    break;
                case op_comma:
                    questionValue.text = questionValue.text.Insert(questionValue.text.Length, ",");
                    break;
                case enum_num:
                    string nv = data["N"].Value;
                    questionValue.text = questionValue.text.Insert(questionValue.text.Length, "{" + nv + "}");
                    break;
                case enum_text:
                    string txt = data["Type"].Value;
                    questionValue.text = questionValue.text.Insert(questionValue.text.Length, "{" + txt + "}");
                    break;

            }
        }
        //Debug.Log("Show Question");
        closeLoading();
        //loading.SetActive(false);

    }

    public void clearCurrentRow()
    {
        if (!dialog_on && !loading_on)
        {
            if (!string.Equals(buttonToPress, CanvasTutorialManager.onNotRequired))
            {
                showMessage("Please click on the shown buttons");
                if (OntestOnTutorial != null)
                    OntestOnTutorial(CanvasTutorialManager.onError);

                return;
            }
            else
            {
                if (OntestOnTutorial != null)
                    OntestOnTutorial(CanvasTutorialManager.onSuccess);
            }
            closePannel();
            if (OnClearCurrentRow != null)
            {
                OnClearCurrentRow();
                // may need implementation of rough work
            }
        }
    }

    public void roughwork()
    {
        if (!dialog_on && !loading_on)
        {
            if (isTutorial)
            {
                if (!string.Equals(buttonToPress, CanvasTutorialManager.onNotRequired))
                {
                    showMessage("Please click on the shown buttons");
                    if (OntestOnTutorial != null)
                        OntestOnTutorial(CanvasTutorialManager.onError);
                    return;
                }
                else
                {
                    if (OntestOnTutorial != null)
                        OntestOnTutorial(CanvasTutorialManager.onSuccess);
                }
            }
            // Debug.Log("rough work area: ");
            closePannel();
            if (!_mMainWorkManager.currentRow.GetComponent<RowController>().hasRoughwork)
            {
                if (null == CurrentRoughwork)
                {
                    CurrentRoughwork = (GameObject)Instantiate(roughworkObject, transform);
                    //Debug.Log(CurrentRoughwork.transform.GetSiblingIndex());
                    CurrentRoughwork.transform.SetSiblingIndex(4);
                    //Debug.Log("roughh Index: " + CurrentRoughwork.transform.GetSiblingIndex());
                    if (OnRoughworkOpenAction != null)
                        OnRoughworkOpenAction();
                    CurrentRoughwork.GetComponentInChildren<NewRoughManager>().setRoughWorkActive();
                    reopenRoughWork = false;
                }

            }
            else
            {
                showMessage("Roughwork area is already there for this step.");

            }
        }

    }

    void addRoughWork()
    {
        if (!reopenRoughWork)
        {
            roughworkList.Add(CurrentRoughwork);
            if (OnRoughworkAction != null)
                OnRoughworkAction(roughworkList.Count - 1);
            hasRoughwork = true;
        }

    }

    public void backFromRoughWork()
    {
        if (!dialog_on && !loading_on)
        {
            // back button pessed on rough work area
            //Debug.Log("back button on rough work pressed");
            CurrentRoughwork.SetActive(false);
            CurrentRoughwork = null;
        }

    }

    public void reOpenRoughWorkArea(int index)
    {
        if (!dialog_on && !loading_on)
        {
            //Debug.Log("ReOpening Rough work Index: " + index);
            CurrentRoughwork = roughworkList[index];
            CurrentRoughwork.SetActive(true);
            if (index == roughworkList.Count - 1 && hasRoughwork)
                CurrentRoughwork.GetComponentInChildren<NewRoughManager>().setRoughWorkActive();
            reopenRoughWork = true;
            if (OnReOpenRoughworkAction != null)
                OnReOpenRoughworkAction();
        }
    }

    public void pressedNumber(int number)
    {
        if (!dialog_on && !loading_on)
        {
            // through delegate to pass value to acvite canvas area 
            if (isTutorial)
            {
                int pNumber;
                if(!int.TryParse(buttonToPress,out pNumber))
                {
                    showMessage("Please click on the shown buttons");
                    if (OntestOnTutorial != null)
                        OntestOnTutorial(CanvasTutorialManager.onError);
                    return;
                }
                if (pNumber != number)
                {
                    showMessage("Please click on the shown buttons");
                    if (OntestOnTutorial != null)
                        OntestOnTutorial(CanvasTutorialManager.onError);
                    return;
                }
                else
                {
                    if (OntestOnTutorial != null)
                        OntestOnTutorial(CanvasTutorialManager.onSuccess);
                }
            }
            closePannel();
            if (OnNumberClickedAction != null)
            {
                OnNumberClickedAction(number);
            }
        }

    }

    public void pressedExpression(string exp)
    {
        Debug.LogError("pressedExpression: " + exp);
        if (!dialog_on && !loading_on)
        {
            // through delegate to pass value to acvite canvas area 
            if (isTutorial)
            {
                if ((string.Equals(buttonToPress, CanvasTutorialManager.onEqualButton))|| (string.Equals(buttonToPress, CanvasTutorialManager.onMulButton)))
                {

                    if (OntestOnTutorial != null)
                        OntestOnTutorial(CanvasTutorialManager.onSuccess);

                }
                else
                {
                    showMessage("Please click on the shown buttons");
                    if (OntestOnTutorial != null)
                        OntestOnTutorial(CanvasTutorialManager.onError);
                    return;
                }
            }
            closePannel();

            if (OnExpClickedAction != null)
            {
                OnExpClickedAction(exp);
            }
        }

    }

    public void pressedNextLine()
    {
        if (!dialog_on && !loading_on)
        {
            closePannel();
            if (OnNextLineAction != null)
                OnNextLineAction();
        }
    }

    public void pressedMixedFraction()
    {
        if (!dialog_on && !loading_on)
        {
            if (isTutorial)
            {
                if (!string.Equals(buttonToPress, CanvasTutorialManager.onNotRequired))
                {
                    showMessage("Please click on the shown buttons");
                    if (OntestOnTutorial != null)
                        OntestOnTutorial(CanvasTutorialManager.onError);
                    return;
                }
                else
                {
                    if (OntestOnTutorial != null)
                        OntestOnTutorial(CanvasTutorialManager.onSuccess);
                }
            }
            closePannel();
            if (OnMixedFractionAction != null)
                OnMixedFractionAction();
        }
    }

    public void pressedNextstep()
    {
        if (!dialog_on && !loading_on)
        {
            if (isTutorial)
            {
                if (!string.Equals(buttonToPress, CanvasTutorialManager.onNextButton))
                {
                    showMessage("Please click on the shown buttons");
                    if (OntestOnTutorial != null)
                        OntestOnTutorial(CanvasTutorialManager.onError);
                    return;
                }
                else
                {
                    if (OntestOnTutorial != null)
                        OntestOnTutorial(CanvasTutorialManager.onSuccess);
                }
            }
            closePannel();
            if (OnNextStepAction != null)
                OnNextStepAction();
            hasRoughwork = false;
        }
    }

    public void pressedDelete()
    {
        if (!dialog_on && !loading_on)
        {
            if (isTutorial)
            {
                if (!string.Equals(buttonToPress, CanvasTutorialManager.onNotRequired))
                {
                    showMessage("Please click on the shown buttons");
                    if (OntestOnTutorial != null)
                        OntestOnTutorial(CanvasTutorialManager.onError);
                    return;
                }
                else
                {
                    if (OntestOnTutorial != null)
                        OntestOnTutorial(CanvasTutorialManager.onSuccess);
                }
            }
            closePannel();
            if (OnDeleteAction != null)
            {
                OnDeleteAction();

            }
        }
    }

    public void pressedTab()
    {
        if (!dialog_on && !loading_on)
        {
            if (isTutorial)
            {
                if (!string.Equals(buttonToPress, CanvasTutorialManager.onTabButton))
                {
                    showMessage("Please click on the shown buttons");
                    if (OntestOnTutorial != null)
                        OntestOnTutorial(CanvasTutorialManager.onError);
                    return;
                }
                else
                {
                    if (OntestOnTutorial != null)
                        OntestOnTutorial(CanvasTutorialManager.onSuccess);
                }
            }
            closePannel();
            if (OnTabAction != null)
            {
                OnTabAction();

            }
        }
    }

    public void pressedLCM()
    {
        if (!dialog_on && !loading_on)
        {
            closePannel();
            if (OnLCMAction != null)
                OnLCMAction();
        }
    }

    public void pressedHCF()
    {
        if (!dialog_on && !loading_on)
        {
            closePannel();
            if (OnHCFAction != null)
                OnHCFAction();
        }
    }

    public void pressedAddition()
    {
        if (!dialog_on && !loading_on)
        {
            if (isTutorial)
            {
                if (!string.Equals(buttonToPress, CanvasTutorialManager.onNotRequired))
                {
                    showMessage("Please click on the shown buttons");
                    if (OntestOnTutorial != null)
                        OntestOnTutorial(CanvasTutorialManager.onError);
                    return;
                }
                else
                {
                    if (OntestOnTutorial != null)
                        OntestOnTutorial(CanvasTutorialManager.onSuccess);
                }
            }
            closePannel();
            if (OnAdditionAction != null)
                OnAdditionAction();
        }
    }

    public void openLCMOption()
    {

    }

    public void pressedDivide()
    {
        if (!dialog_on && !loading_on)
        {
            if (isTutorial)
            {
                if (!string.Equals(buttonToPress, CanvasTutorialManager.onNotRequired))
                {
                    showMessage("Please click on the shown buttons");
                    if (OntestOnTutorial != null)
                        OntestOnTutorial(CanvasTutorialManager.onError);
                    return;
                }
                else
                {
                    if (OntestOnTutorial != null)
                        OntestOnTutorial(CanvasTutorialManager.onSuccess);
                }
            }
            closePannel();
            if (OnDivideAction != null)
                OnDivideAction();
        }
    }

    public void pressedAnswer()
    {
        if (!dialog_on && !loading_on)
        {
            if (isTutorial)
            {
                if (!string.Equals(buttonToPress, CanvasTutorialManager.onNotRequired))
                {
                    showMessage("Please click on the shown buttons");
                    if (OntestOnTutorial != null)
                        OntestOnTutorial(CanvasTutorialManager.onError);
                    return;
                }
                else
                {
                    if (OntestOnTutorial != null)
                        OntestOnTutorial(CanvasTutorialManager.onSuccess);
                }
            }
            closePannel();
            if (OnAnswerAction != null)
                OnAnswerAction();
        }
    }

    public void pressedDashboard()
    {
        StartCoroutine(LoadYourAsyncScene());
    }

    public void pressedFraction()
    {
        if (!dialog_on && !loading_on)
        {
            if (isTutorial)
            {
                if (!string.Equals(buttonToPress, CanvasTutorialManager.onImproperButton))
                {
                    showMessage("Please click on the shown buttons");
                    if (OntestOnTutorial != null)
                        OntestOnTutorial(CanvasTutorialManager.onError);
                    return;
                }
                else
                {
                    if (OntestOnTutorial != null)
                        OntestOnTutorial(CanvasTutorialManager.onSuccess);
                }
            }
            closePannel();

            if (OnFractionAction != null)
                OnFractionAction();
        }
    }

    public void pressedText()
    {
        // sends the typed string to main grid layout manager for addition
        if (!dialog_on && !loading_on)
        {
            closePannel();
            if (OnTextAction != null)
                OnTextAction(alphaValue);
        }
    }

    public void pressed1()
    {
    }
    public void pressed2()
    {
        //string a = answerPanel.text.ToString();
        //answerPanel.text = a + 2;
        // Debug.Log("cursorPosition: " + cursorPosition + " " + answerPanel.text.Length);
        StopBlinking();
        if (add)
        {
            if (ans)
            {
                if (odd)
                {
                    /*
                    if (answerPanel.text[cursorPosition] == a)
                    {
                        Debug.Log("inside true");
                        answerPanel.text = answerPanel.text.Remove(cursorPosition, cursorVarCount);
                    }*/
                    // Debug.Log(answerPanel.text[cursorPosition]);
                    answerPanel.text = answerPanel.text.Insert(cursorPosition, "2");
                    addAnswer += 2 * (Mathf.Pow(10.0f, NoOfDigitInAnswer));
                    NoOfDigitInAnswer++;
                    if (currentCharacterCount < (2 * prevCharacterCount) - 2)
                    {
                        cursorPosition = addCarryPosDifference;

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
                    addCarryPosDifference--;
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
        else if (sub)
        {
            if (ans)
            {
                if (odd)
                {
                    answerPanel.text = answerPanel.text.Insert(cursorPosition, "2");
                    addAnswer += 1 * (Mathf.Pow(10.0f, NoOfDigitInAnswer));
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
                // if(stepNo == 1)
                //{
                //Debug.Log("cursorPosition: " + cursorPosition + " " + answerPanel.text.Length);
                answerPanel.text = answerPanel.text.Insert(cursorPosition, "2");
                cursorPosition += cursorIncrement;
                //Debug.Log("cursorPosition: " + cursorPosition + " " + answerPanel.text.Length);
                /* }
                 else
                 {
                     answerPanel.text = answerPanel.text.Insert(cursorPosition, "1");
                     cursorPosition++;
                 }*/


            }
            currentCharacterCount++;
        }
        StartBlinking();
    }
    public void pressed3()
    {
        //string a = answerPanel.text.ToString();
        //answerPanel.text = a + 3;
        // Debug.Log("cursorPosition: " + cursorPosition + " " + answerPanel.text.Length);
        StopBlinking();
        if (add)
        {
            if (ans)
            {
                if (odd)
                {
                    /*
                    if (answerPanel.text[cursorPosition] == a)
                    {
                        Debug.Log("inside true");
                        answerPanel.text = answerPanel.text.Remove(cursorPosition, cursorVarCount);
                    }*/
                    // Debug.Log(answerPanel.text[cursorPosition]);
                    answerPanel.text = answerPanel.text.Insert(cursorPosition, "3");
                    addAnswer += 3 * (Mathf.Pow(10.0f, NoOfDigitInAnswer));
                    NoOfDigitInAnswer++;
                    if (currentCharacterCount < (2 * prevCharacterCount) - 2)
                    {
                        cursorPosition = addCarryPosDifference;

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
                    addCarryPosDifference--;
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
        else if (sub)
        {
            if (ans)
            {
                if (odd)
                {
                    answerPanel.text = answerPanel.text.Insert(cursorPosition, "3");
                    addAnswer += 1 * (Mathf.Pow(10.0f, NoOfDigitInAnswer));
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
                // if(stepNo == 1)
                //{
                //Debug.Log("cursorPosition: " + cursorPosition + " " + answerPanel.text.Length);
                answerPanel.text = answerPanel.text.Insert(cursorPosition, "3");
                cursorPosition += cursorIncrement;
                //Debug.Log("cursorPosition: " + cursorPosition + " " + answerPanel.text.Length);
                /* }
                 else
                 {
                     answerPanel.text = answerPanel.text.Insert(cursorPosition, "1");
                     cursorPosition++;
                 }*/


            }
            currentCharacterCount++;
        }
        StartBlinking();
    }
    public void pressed4()
    {
        //string a = answerPanel.text.ToString();
        //answerPanel.text = a + 4;
        StopBlinking();
        if (add)
        {
            if (ans)
            {
                if (odd)
                {
                    /*
                    if (answerPanel.text[cursorPosition] == a)
                    {
                        Debug.Log("inside true");
                        answerPanel.text = answerPanel.text.Remove(cursorPosition, cursorVarCount);
                    }*/
                    //Debug.Log(answerPanel.text[cursorPosition]);
                    answerPanel.text = answerPanel.text.Insert(cursorPosition, "4");
                    addAnswer += 4 * (Mathf.Pow(10.0f, NoOfDigitInAnswer));
                    NoOfDigitInAnswer++;
                    //Debug.Log("currentCharacterCount: " + currentCharacterCount + "  ((2 * prevCharacterCount) - 1): " + ((2 * prevCharacterCount) - 1));
                    if (currentCharacterCount < ((2 * prevCharacterCount) - 2))
                    {
                        cursorPosition = addCarryPosDifference;

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
                    addCarryPosDifference--;
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
        else if (sub)
        {
            if (ans)
            {
                if (odd)
                {
                    answerPanel.text = answerPanel.text.Insert(cursorPosition, "4");
                    addAnswer += 1 * (Mathf.Pow(10.0f, NoOfDigitInAnswer));
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
                    answerPanel.text = answerPanel.text.Insert(cursorPosition, "4");
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
                // if(stepNo == 1)
                //{
                //Debug.Log("cursorPosition: " + cursorPosition + " " + answerPanel.text.Length);
                answerPanel.text = answerPanel.text.Insert(cursorPosition, "4");
                cursorPosition += cursorIncrement;
                //Debug.Log("cursorPosition: " + cursorPosition + " " + answerPanel.text.Length);
                /* }
                 else
                 {
                     answerPanel.text = answerPanel.text.Insert(cursorPosition, "1");
                     cursorPosition++;
                 }*/


            }
            currentCharacterCount++;
        }
        StartBlinking();
    }
    public void pressed5()
    {
        //string a = answerPanel.text.ToString();
        //answerPanel.text = a + 5;
        StopBlinking();
        if (add)
        {
            if (ans)
            {
                if (odd)
                {
                    /*
                    if (answerPanel.text[cursorPosition] == a)
                    {
                        Debug.Log("inside true");
                        answerPanel.text = answerPanel.text.Remove(cursorPosition, cursorVarCount);
                    }*/
                    // Debug.Log(answerPanel.text[cursorPosition]);
                    answerPanel.text = answerPanel.text.Insert(cursorPosition, "5");
                    addAnswer += 5 * (Mathf.Pow(10.0f, NoOfDigitInAnswer));
                    NoOfDigitInAnswer++;
                    if (currentCharacterCount < (2 * prevCharacterCount) - 2)
                    {
                        cursorPosition = addCarryPosDifference;

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
                    addCarryPosDifference--;
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
        else if (sub)
        {
            if (ans)
            {
                if (odd)
                {
                    answerPanel.text = answerPanel.text.Insert(cursorPosition, "5");
                    addAnswer += 1 * (Mathf.Pow(10.0f, NoOfDigitInAnswer));
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
                // if(stepNo == 1)
                //{
                Debug.Log("cursorPosition: " + cursorPosition + " " + answerPanel.text.Length);
                answerPanel.text = answerPanel.text.Insert(cursorPosition, "5");
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
        StartBlinking();
    }
    public void pressed6()
    {
        // string a = answerPanel.text.ToString();
        //answerPanel.text = a + 6;
        StopBlinking();
        if (add)
        {
            if (ans)
            {
                if (odd)
                {
                    /*
                    if (answerPanel.text[cursorPosition] == a)
                    {
                        Debug.Log("inside true");
                        answerPanel.text = answerPanel.text.Remove(cursorPosition, cursorVarCount);
                    }*/
                    //Debug.Log(answerPanel.text[cursorPosition]);
                    answerPanel.text = answerPanel.text.Insert(cursorPosition, "6");
                    addAnswer += 6 * (Mathf.Pow(10.0f, NoOfDigitInAnswer));
                    NoOfDigitInAnswer++;
                    if (currentCharacterCount < (2 * prevCharacterCount) - 2)
                    {
                        cursorPosition = addCarryPosDifference;

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
                    addCarryPosDifference--;
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
        else if (sub)
        {
            if (ans)
            {
                if (odd)
                {
                    answerPanel.text = answerPanel.text.Insert(cursorPosition, "6");
                    addAnswer += 1 * (Mathf.Pow(10.0f, NoOfDigitInAnswer));
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
                // if(stepNo == 1)
                //{
                Debug.Log("cursorPosition: " + cursorPosition + " " + answerPanel.text.Length);
                answerPanel.text = answerPanel.text.Insert(cursorPosition, "6");
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
        StartBlinking();
    }
    public void pressed7()
    {
        StopBlinking();
        if (add)
        {
            if (ans)
            {
                if (odd)
                {
                    /*
                    if (answerPanel.text[cursorPosition] == a)
                    {
                        Debug.Log("inside true");
                        answerPanel.text = answerPanel.text.Remove(cursorPosition, cursorVarCount);
                    }*/
                    //Debug.Log(answerPanel.text[cursorPosition]);
                    answerPanel.text = answerPanel.text.Insert(cursorPosition, "7");
                    addAnswer += 7 * (Mathf.Pow(10.0f, NoOfDigitInAnswer));
                    NoOfDigitInAnswer++;
                    if (currentCharacterCount < (2 * prevCharacterCount) - 2)
                    {
                        cursorPosition = addCarryPosDifference;

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
                    addCarryPosDifference--;
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
        else if (sub)
        {
            if (ans)
            {
                if (odd)
                {
                    answerPanel.text = answerPanel.text.Insert(cursorPosition, "7");
                    addAnswer += 1 * (Mathf.Pow(10.0f, NoOfDigitInAnswer));
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
                // if(stepNo == 1)
                //{
                Debug.Log("cursorPosition: " + cursorPosition + " " + answerPanel.text.Length);
                answerPanel.text = answerPanel.text.Insert(cursorPosition, "7");
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
        StartBlinking();
    }
    public void pressed8()
    {
        //  string a = answerPanel.text.ToString();
        // answerPanel.text = a + 8;
        StopBlinking();
        if (add)
        {
            if (ans)
            {
                if (odd)
                {
                    /*
                    if (answerPanel.text[cursorPosition] == a)
                    {
                        Debug.Log("inside true");
                        answerPanel.text = answerPanel.text.Remove(cursorPosition, cursorVarCount);
                    }*/
                    //Debug.Log(answerPanel.text[cursorPosition]);
                    answerPanel.text = answerPanel.text.Insert(cursorPosition, "8");
                    addAnswer += 8 * (Mathf.Pow(10.0f, NoOfDigitInAnswer));
                    NoOfDigitInAnswer++;
                    if (currentCharacterCount < (2 * prevCharacterCount) - 2)
                    {
                        cursorPosition = addCarryPosDifference;

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
                    addCarryPosDifference--;
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
        else if (sub)
        {
            if (ans)
            {
                if (odd)
                {
                    answerPanel.text = answerPanel.text.Insert(cursorPosition, "8");
                    addAnswer += 1 * (Mathf.Pow(10.0f, NoOfDigitInAnswer));
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
                // if(stepNo == 1)
                //{
                Debug.Log("cursorPosition: " + cursorPosition + " " + answerPanel.text.Length);
                answerPanel.text = answerPanel.text.Insert(cursorPosition, "8");
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
        StartBlinking();
    }
    public void pressed9()
    {
        //string a = answerPanel.text.ToString();
        //answerPanel.text = a + 9;
        StopBlinking();
        if (add)
        {
            if (ans)
            {
                if (odd)
                {
                    /*
                    if (answerPanel.text[cursorPosition] == a)
                    {
                        Debug.Log("inside true");
                        answerPanel.text = answerPanel.text.Remove(cursorPosition, cursorVarCount);
                    }*/
                    //Debug.Log(answerPanel.text[cursorPosition]);
                    answerPanel.text = answerPanel.text.Insert(cursorPosition, "9");
                    addAnswer += 9 * (Mathf.Pow(10.0f, NoOfDigitInAnswer));
                    NoOfDigitInAnswer++;
                    if (currentCharacterCount < (2 * prevCharacterCount) - 2)
                    {
                        cursorPosition = addCarryPosDifference;

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
                    addCarryPosDifference--;
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
        else if (sub)
        {
            if (ans)
            {
                if (odd)
                {
                    answerPanel.text = answerPanel.text.Insert(cursorPosition, "9");
                    addAnswer += 1 * (Mathf.Pow(10.0f, NoOfDigitInAnswer));
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
                // if(stepNo == 1)
                //{
                Debug.Log("cursorPosition: " + cursorPosition + " " + answerPanel.text.Length);
                answerPanel.text = answerPanel.text.Insert(cursorPosition, "9");
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
        StartBlinking();
    }
    public void pressed0()
    {
        // string a = answerPanel.text.ToString();
        //answerPanel.text = a + 0;
        StopBlinking();
        if (add)
        {
            if (ans)
            {
                if (odd)
                {
                    /*
                    if (answerPanel.text[cursorPosition] == a)
                    {
                        Debug.Log("inside true");
                        answerPanel.text = answerPanel.text.Remove(cursorPosition, cursorVarCount);
                    }*/
                    //Debug.Log(answerPanel.text[cursorPosition]);
                    answerPanel.text = answerPanel.text.Insert(cursorPosition, "0");
                    addAnswer += 0 * (Mathf.Pow(10.0f, NoOfDigitInAnswer));
                    NoOfDigitInAnswer++;
                    if (currentCharacterCount < (2 * prevCharacterCount) - 2)
                    {
                        cursorPosition = addCarryPosDifference;

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
                    addCarryPosDifference--;
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
        else if (sub)
        {
            if (ans)
            {
                if (odd)
                {
                    answerPanel.text = answerPanel.text.Insert(cursorPosition, "0");
                    addAnswer += 1 * (Mathf.Pow(10.0f, NoOfDigitInAnswer));
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
                // if(stepNo == 1)
                //{
                Debug.Log("cursorPosition: " + cursorPosition + " " + answerPanel.text.Length);
                answerPanel.text = answerPanel.text.Insert(cursorPosition, "0");
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
        StartBlinking();
    }

    public void goTo3dScene()
    {
        //StartCoroutine(LoadYourAsyncScene());
    }

    public void clickOnClearCanvas()
    {
        if (!dialog_on && !loading_on)
        {
            closePannel();
            showYesNoMsg("are you sure you want to delete");
            hasRoughwork = false;
        }
    }

    public void clearCanvas()
    {
        // if (!dialog_on && !loading_on)
        {
            //closePannel();
            if (OnClearCanvas != null)
                OnClearCanvas();
        }
    }

    public void pressedRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
        else
        {
            Debug.Log("Add isnot selected");
        }

    }

    public void SubNumber()
    {
        if (sub)
        {
            adjustStepSpaces();
            /*
            string a = answerPanel.text.ToString();
            answerPanel.text = a + "\n "+"+  ";
            */
            PrintStepNo();
            // Debug.Log("cursorPosition AddNumber: " + cursorPosition +" "+ answerPanel.text.Length);
            answerPanel.text = answerPanel.text.Insert(cursorPosition, "-  ");
            cursorPosition += 3;
            // Debug.Log("step : " + stepStartPoint[stepStartPoint.Count - 1] + " value " + answerPanel.text[stepStartPoint[stepStartPoint.Count - 1]] + " value +1" + answerPanel.text[stepStartPoint[stepStartPoint.Count - 1]+3]);
            //Debug.Log("add cursorPosition: " + cursorPosition);
            stepStartPoint.Add(cursorPosition);
            //Debug.Log("count " + stepStartPoint.Count);
        }
        else
        {
            Debug.Log("Add isnot selected");
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
            //answerPanel.text = answerPanel.text.Remove(cursorPosition, 1);
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
                answerPanel.text = answerPanel.text.Insert(cursorPosition, "\\vborder[0001 black]=  ");
                // as 17 digits were placed; keep 1 for later use
                cursorPosition += 23;
                ans = true;
                odd = true;
                addAnswerPosDifference = cursorPosition;
                addCarryPosDifference = stepStartPoint[0] + (currentCharacterCount - 2);//(stepNo -1) * (currentCharacterCount + addExtraChar) + 3+23;
                //Debug.Log("addCarryPosDifference: " + addCarryPosDifference + " addAnswerPosDifference: " + addAnswerPosDifference);
                // Debug.Log("step : " + stepStartPoint[stepStartPoint.Count - 1] + " value " + answerPanel.text[stepStartPoint[stepStartPoint.Count - 1]] + " value +1" + answerPanel.text[stepStartPoint[stepStartPoint.Count - 1]+3]);
                //Debug.Log("add cursorPosition: " + cursorPosition);
                //stepStartPoint.Add(cursorPosition);
                //Debug.Log("count " + stepStartPoint.Count);
            }
            //StartCoroutine(blinkCoroutine);
            StartCoroutine("blinkingCursor");
        }
        else if (sub)
        {
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

    public void DeleteLast()
    {
        if (currentCharacterCount > 0)
        {
            StopBlinking();
            if (ans)
            {
                Debug.Log("Delete Odd: " + odd);
                if (odd)
                {
                    Debug.Log("currentCharacterCount: " + currentCharacterCount + " prevCharacterCount: " + prevCharacterCount);
                    if (currentCharacterCount < (2 * prevCharacterCount) - 1)
                    {
                        addCarryPosDifference++;
                        cursorPosition = addCarryPosDifference;
                        //Debug.Log("cursor: <" + cursorPosition + " value: " + answerPanel.text[cursorPosition] + " value1: " + answerPanel.text[cursorPosition + 1]);
                        answerPanel.text = answerPanel.text.Remove(cursorPosition, 1);
                        // Debug.Log("cursor: <" + cursorPosition + " value: " + answerPanel.text[cursorPosition] + " value1: " + answerPanel.text[cursorPosition + 1]);
                        answerPanel.text = answerPanel.text.Insert(cursorPosition, " ");
                    }
                    else
                    {
                        cursorPosition = addAnswerPosDifference;

                        NoOfDigitInAnswer--;
                        // Debug.Log("cursor: >" + cursorPosition + " value: " + answerPanel.text[cursorPosition] + " value1: " + answerPanel.text[cursorPosition + 1]);
                        answerPanel.text = answerPanel.text.Remove(cursorPosition, 1);
                        // Debug.Log("cursor: >" + cursorPosition + " value: " + answerPanel.text[cursorPosition] + " value1: " + answerPanel.text[cursorPosition + 1]);
                        // answerPanel.text = answerPanel.text.Insert(cursorPosition, " ");
                    }

                }
                else
                {
                    cursorPosition = addAnswerPosDifference;
                    //Debug.Log("cursor: " + cursorPosition + " value: " + answerPanel.text[cursorPosition] + " value1: " + answerPanel.text[cursorPosition + 1]);
                    answerPanel.text = answerPanel.text.Remove(cursorPosition, 1);
                    //Debug.Log("cursor: " + cursorPosition + " value: " + answerPanel.text[cursorPosition] + " value1: " + answerPanel.text[cursorPosition + 1] + "next addCarryPosDifference"+ (addCarryPosDifference+2)+" val" + answerPanel.text[addCarryPosDifference+2]);
                    NoOfDigitInAnswer--;
                    //addCarryPosDifference++;
                }
                if (currentCharacterCount < (2 * prevCharacterCount) - 1)
                {
                    odd = !odd;
                }
                else
                    odd = true;
            }
            else
            {
                Debug.Log("Delete cursorPosition: " + cursorPosition); //+ answerPanel.text[cursorPosition]
                Debug.Log("value at cursor: " + "prev value at cursor: " + answerPanel.text[cursorPosition - 1]);
                answerPanel.text = answerPanel.text.Remove(cursorPosition - 1, 1);
                cursorPosition -= cursorIncrement;
                Debug.Log("Delete after cursorPosition: " + cursorPosition);
                Debug.Log("value at cursor: " + answerPanel.text[cursorPosition - 1]);
            }
            currentCharacterCount--;
            StartBlinking();
        }
        else
        {
            Debug.Log("cannot delete ");
        }
    }

    void adjustStepSpaces()
    {
        /* foreach (int i in stepStartPoint)
         {
             Debug.Log("i: " + i + "val: " + answerPanel.text[i] + " i+1: " + answerPanel.text[i + 1]);
         }*/
        if (prevCharacterCount != -1)
        {
            if (prevCharacterCount > currentCharacterCount)
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
                stepStartPoint[stepStartPoint.Count - 1] = cursorPosition;
                cursorPosition += (currentCharacterCount);
            }
            else
            {
                if (prevCharacterCount < currentCharacterCount)
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
        StopBlinking();
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
                    addCarryPosDifference--;
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
        StartBlinking();
    }

    void StartBlinking()
    {
        blink = true;
        StartCoroutine("blinkingCursor");
    }

    void StopBlinking()
    {
        StopCoroutine("blinkingCursor");
        if (!blink)
            answerPanel.text = answerPanel.text.Remove(cursorPosition, cursorVarCount);
        blink = false;
    }

    public void GoToNextQuestion()
    {
        if (!dialog_on && !loading_on)
        {
            if (isTutorial)
            {
                if (!string.Equals(buttonToPress, CanvasTutorialManager.onSubmitButton))
                {
                    showMessage("Please click on the shown buttons");

                    if (OntestOnTutorial != null)
                        OntestOnTutorial(CanvasTutorialManager.onError);
                    return;
                }
                else
                {
                    if (OntestOnTutorial != null)
                        OntestOnTutorial(CanvasTutorialManager.onSuccess);
                    return;
                }
            }
            if (OnNextQuestion != null)
                OnNextQuestion();
            //loading.SetActive(true);
        }
    }

    public void OnTutorialOver()
    {
        if (OntestOnTutorial != null)
            OntestOnTutorial(CanvasTutorialManager.onSuccess);
    }

    void showWithPromptDownloading(string msg)
    {
        //Debug.Log("showDownloading");
        loading.SetActive(true);
        loading_on = true;
    }

    void showDownloading()
    {
        //Debug.Log("showDownloading");
        loading.SetActive(true);
        loading_on = true;
    }

    void closeLoading()
    {

        loading.SetActive(false);
        loading_on = false;
        //Debug.Log("close loading: " + loading_on + " loading: " + loading.active);
    }

    IEnumerator blinkingCursor()
    {
        yield return new WaitForSeconds(1.0f);
        if (add)
        {
            //Debug.Log("add cursorPosition: " + cursorPosition + "length " + answerPanel.text.Length+ " cursorVarCount"+ cursorVarCount);
            if (ans)
            {
                if (odd)
                {
                    //Debug.Log("NoOfDigitInAnswer: " + NoOfDigitInAnswer);
                    if (NoOfDigitInAnswer > 0)
                    {
                        if (blink)
                        {
                            //answerPanel.text = answerPanel.text.Remove(cursorPosition-1, 1);
                            //Debug.Log("next | cursorPosition: " + cursorPosition + "length " + answerPanel.text.Length);
                            answerPanel.text = answerPanel.text.Insert(cursorPosition, cursorVar);
                            //Debug.Log("next | after cursorPosition: " + cursorPosition + "length " + answerPanel.text.Length);
                            //Debug.Log("cursorPosition: " + cursorPosition + "length " + answerPanel.text.Length);
                            //answerPanel.text = answerPanel.text.Insert(cursorPosition, " ");
                        }
                        else
                        {
                            //Debug.Log("next blink cursorPosition: " + cursorPosition + "length " + answerPanel.text.Length);
                            answerPanel.text = answerPanel.text.Remove(cursorPosition, cursorVarCount);
                            //Debug.Log("next blink after cursorPosition: " + "length " + answerPanel.text.Length);
                            //Debug.Log("cursorPosition: " + cursorPosition + "length " + answerPanel.text.Length);
                            //answerPanel.text = answerPanel.text.Insert(cursorPosition, " ");
                            //answerPanel.text = answerPanel.text.Insert(cursorPosition-1, " ");
                        }
                    }
                    else
                    {
                        if (blink)
                        {
                            //answerPanel.text = answerPanel.text.Remove(cursorPosition-1, 1);
                            //Debug.Log("next | cursorPosition: " + cursorPosition + "length " + answerPanel.text.Length);
                            answerPanel.text = answerPanel.text.Insert(cursorPosition, cursorVar);
                            //Debug.Log("next | after cursorPosition: " + cursorPosition + "length " + answerPanel.text.Length);
                            //Debug.Log("cursorPosition: " + cursorPosition + "length " + answerPanel.text.Length);
                            //answerPanel.text = answerPanel.text.Insert(cursorPosition, " ");
                        }
                        else
                        {
                            //Debug.Log("next blink cursorPosition: " + "length " + answerPanel.text.Length);
                            answerPanel.text = answerPanel.text.Remove(cursorPosition, cursorVarCount);
                            //Debug.Log("next blink after cursorPosition: " + "length " + answerPanel.text.Length);
                            //Debug.Log("cursorPosition: " + cursorPosition + "length " + answerPanel.text.Length);
                            //answerPanel.text = answerPanel.text.Insert(cursorPosition, " ");
                            //answerPanel.text = answerPanel.text.Insert(cursorPosition-1, " ");
                        }
                    }

                }
                else
                {

                    if (blink)
                    {
                        //answerPanel.text = answerPanel.text.Remove(cursorPosition, 1);
                        answerPanel.text = answerPanel.text.Insert(cursorPosition, cursorVar);
                    }
                    else
                    {
                        answerPanel.text = answerPanel.text.Remove(cursorPosition, cursorVarCount);
                        //answerPanel.text = answerPanel.text.Insert(cursorPosition, " ");
                    }
                }
                //Debug.Log("1 cursorPosition: " + cursorPosition);

            }
            else
            {
                if (blink)
                {
                    //answerPanel.text = answerPanel.text.Remove(cursorPosition, 1);

                    answerPanel.text = answerPanel.text.Insert(cursorPosition, cursorVar);
                    // Debug.Log("cursorPosition: " + cursorPosition + "length " + answerPanel.text.Length);
                }
                else
                {
                    // Debug.Log("cursorPosition: " + cursorPosition + "length " + answerPanel.text.Length + " cursorVarCount: "+ cursorVarCount);
                    //Debug.Log("cursorPosition: " + cursorPosition + "length " + answerPanel.text.Length + " cursorVarCount: " + cursorVarCount);
                    answerPanel.text = answerPanel.text.Remove(cursorPosition);
                    //Debug.Log("after cursorPosition: " + cursorPosition + "length " + answerPanel.text.Length + " cursorVarCount: " + cursorVarCount);
                    //answerPanel.text = answerPanel.text.Insert(cursorPosition, " ");
                }
            }

        }
        else
        {
            if (blink)
            {
                //answerPanel.text = answerPanel.text.Remove(cursorPosition, 1);
                answerPanel.text = answerPanel.text.Insert(cursorPosition, cursorVar);
            }
            else
            {

                answerPanel.text = answerPanel.text.Remove(cursorPosition);

                //answerPanel.text = answerPanel.text.Insert(cursorPosition, " ");
            }
        }

        blink = !blink;
        StartCoroutine("blinkingCursor");
        // StartCoroutine(blinkCoroutine);
    }

    IEnumerator LoadYourAsyncScene()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        //AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene3d);
        PlayerPrefs.SetString("logout", "false");
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(dashboard_scene);
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
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
