using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class StepGridLayout : MonoBehaviour {

    // Use this for initialization
    public GameObject Step, GeneralDigit, Fraction;
    GameObject currentWorkingBlock,currentStep;
    TEXDraw currentStepTexDraw;
    int stepNo;
    bool nextline,instep;
    CanvasManager canvasManager;
    private void OnEnable()
    {
        MainGridLayoutManager.OnprintNextStepAction += printNextStep;
        MainGridLayoutManager.OnprintNextLineAction += nextLine;
        MainGridLayoutManager.OnprintfractionAction += replacewithfraction;
        MainGridLayoutManager.OnroughworkShowAction += hasRoughWork;
        
        CallRESTServices.OnQuestionDownloaded += showQuestion;
        MainGridLayoutManager.OnClearCanvas += clearCanvas;
    }

    private void OnDisable()
    {
        MainGridLayoutManager.OnprintNextStepAction -= printNextStep;
        MainGridLayoutManager.OnprintNextLineAction -= nextLine;
        MainGridLayoutManager.OnprintfractionAction -= replacewithfraction;
        MainGridLayoutManager.OnroughworkShowAction -= hasRoughWork;
        CallRESTServices.OnQuestionDownloaded -= showQuestion;
        MainGridLayoutManager.OnClearCanvas -= clearCanvas;
    }

    void Start () {
        stepNo = 0;
        nextline = false;
        canvasManager = GameObject.FindObjectOfType<CanvasManager>();

    }
	
	void nextLine()
    {
        currentWorkingBlock = (GameObject)Instantiate(GeneralDigit, transform);
        currentStepTexDraw = currentWorkingBlock.GetComponent<TEXDraw>();
        instep = false;
        nextline = true;
    }
    void replacewithfraction()
    {
        if (instep)
        {
            currentWorkingBlock = (GameObject)Instantiate(GeneralDigit, transform);
            instep = false;
        }
        if (nextline)
        {
            //Destroy(currentWorkingBlock);
            currentWorkingBlock = (GameObject)Instantiate(GeneralDigit, transform);
        }
        nextline = false;

    }

    void hasRoughWork(int index)
    {
        Debug.Log("currentStep: " + currentStep.name);
        currentStep.transform.GetChild(0).gameObject.SetActive(true);
        currentStep.GetComponent<StepControllerScript>().hasRoughwork = true;
        currentStep.GetComponent<StepControllerScript>().roughIndex = index;
    }
    void printNextStep()
    {
        //Debug.Log("called from step Maingrid");
        stepNo++;
        nextline = false;
        instep = true;
        currentWorkingBlock = (GameObject)Instantiate(Step, transform);
        currentWorkingBlock.GetComponent<StepControllerScript>().rowNumber = stepNo;
        currentStep = currentWorkingBlock;
        currentStepTexDraw = currentWorkingBlock.GetComponent<TEXDraw>();
        currentStepTexDraw.text = currentStepTexDraw.text.Insert(currentStepTexDraw.text.Length, "Step " + stepNo.ToString());
        if (canvasManager.isTutorial)
        {
            currentWorkingBlock = (GameObject)Instantiate(GeneralDigit, transform);
            //Debug.Log("called from step Maingrid print step name");
            //currentStep = currentWorkingBlock;
            currentStepTexDraw = currentWorkingBlock.GetComponent<TEXDraw>();
            currentStepTexDraw.text = currentStepTexDraw.text.Insert(currentStepTexDraw.text.Length, "Sample step");
        }
        else
        {
            // if json gives to print step name
            if (UtilityREST.print_step_name)
            {
                //Debug.Log("called from step Maingrid print step name");
                string solJson = UtilityREST.solJson;
                int solution_No = UtilityREST.solution_No;
                JSONNode responce = JSON.Parse(solJson);
                //Debug.Log("responce: " + responce.ToString());
                JSONArray resArray = (JSONArray)responce;
                JSONNode soln = resArray[solution_No];
                JSONArray steps = (JSONArray)soln["Steps"];
                JSONNode stepsData = steps[stepNo - 1];
                string step_name = stepsData["step_name"].Value;
                currentWorkingBlock = (GameObject)Instantiate(GeneralDigit, transform);
                //Debug.Log("called from step Maingrid print step name");
                //currentStep = currentWorkingBlock;
                currentStepTexDraw = currentWorkingBlock.GetComponent<TEXDraw>();
                currentStepTexDraw.text = currentStepTexDraw.text.Insert(currentStepTexDraw.text.Length, step_name);
            }
        }
       

    }
    void showQuestion(JSONNode question)
    {
        Debug.Log("called from rest");
        if(stepNo > 0)
            clearCanvas();
        
    }

    void clearCanvas()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        stepNo = 0;
        nextline = false;
    }
}
