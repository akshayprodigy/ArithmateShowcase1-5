using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager1 : MonoBehaviour
{
	public GameObject[] objects,object1,object2,object3, object4;
	public GameObject[] numbers, numbers1, numbers2,numbers3, numbers4;
	public GameObject[] shapes,subShapes,questShapes;
	private float lastCreated;
	public GameObject[] s1, s2;
	public int scount,s1count;
	public TextMesh lostFoodLabel,sequenceLabel,totalLabel;
	public int LostFood;
	public int selectSequence, totalCorrect,allCorrectCount;
	public int userSelectedSequence, userTotalCorrect,noOfShape;
	public GameObject fracToDisplay, fracToDisplayOnScreen,fracToDisplayPnl,belowPanel,QuestText;
	public Text score_Text,Debug_Text;
	public TextMesh scoreLabel;
	public static int score;
	public AudioSource correctSound, wrongSound;
    public delegate void LogMessage(string cases);
    public static event LogMessage onLogMessage;
    public int Score
	{
		set
		{
			score = value;

			scoreLabel.text = Score.ToString();
		}
		get
		{
			return score;
		}
	}
	public string answer,missedObject, finishedObjectiveText, startedObjectiveText;

	public float speed,generationSpeed;
	public bool isShape1, isShape2, isShape3, isShape4,isShape5, isGameActive,isShapes,isPause;
	public GameObject gameOverPnl,initiatedObject,playBtn,pauseBtn,infoPanel;

	void Update()
    {
		if (isGameActive == false)
		{
			Debug.Log("1");
           
            //score_Text.text = "Total = "+userSelectedSequence.ToString();
            //score_Text.text = "Total = " + userTotalCorrect.ToString();
            switch (noOfShape)
			{

				case 1:
                    generationSpeed = 3;
                    scount = 0;
					isShape1 = true;
					isShape2 = false;
					isShape3 = false;
					isShape4 = false;
					isShape5 = false;
					Debug.Log("3");
					foreach (GameObject g in shapes)
						g.GetComponent<Button>().interactable = false;
					shapes[0].GetComponent<Button>().interactable = true;
					Debug.Log("3");
					foreach (GameObject g1 in subShapes)
						g1.SetActive(false);
					subShapes[0].SetActive(true);
					QuestText.GetComponent<TEXDraw>().text = "Find the next 'Equivalent Fractions' for";
                   
					break;
				case 2:
                    generationSpeed = 2.9f;
                    scount = 0;
                  //  speed = 0.3f;
					isShape1 = false;
					isShape2 = true;
					isShape3 = false;
					isShape4 = false;
					isShape5 = false;
					foreach (GameObject g in shapes)
						g.GetComponent<Button>().interactable = false;
					shapes[1].GetComponent<Button>().interactable = true;
					foreach (GameObject g1 in subShapes)
						g1.SetActive(false);
					subShapes[1].SetActive(true);
					QuestText.GetComponent<TEXDraw>().text = "Find the next 'Equivalent Fractions' for  \\frac{1}{3}";

					break;
				case 3:
                    generationSpeed = 2.8f;
                    isShape1 = false;
					isShape2 = false;
					isShape3 = false;
					isShape4 = false;
					isShape5 = true;
					
					foreach (GameObject g in shapes)
						g.GetComponent<Button>().interactable = false;
					shapes[4].GetComponent<Button>().interactable = true;
					foreach (GameObject g1 in subShapes)
						g1.SetActive(false);
					subShapes[4].SetActive(true);
					
                    QuestText.GetComponent<TEXDraw>().text = "Find the next 'Equivalent Fractions' for";

                    break;
				case 4:
                    generationSpeed = 2.7f;
                    speed = 0.03f;
					//generationSpeed = 1.0f;
					isShape1 = false;
					isShape2 = false;
					isShape3 = true;
					isShape4 = false;
					isShape5 = false;
					scount = 1;
					s1count = 1;
					foreach (GameObject g in shapes)
						g.GetComponent<Button>().interactable = false;
					shapes[2].GetComponent<Button>().interactable = true;
					foreach (GameObject g1 in subShapes)
						g1.SetActive(false);
					subShapes[2].SetActive(true);
                    //fracToDisplay.GetComponent<TEXDraw>().text = "";
					QuestText.GetComponent<TEXDraw>().text = "Identify the correct multiplier for the given shapes";

					break;
				case 5:
                    generationSpeed = 2.7f;
                    isShape1 = false;
					isShape2 = false;
					isShape3 = false;
					isShape4 = true;
					isShape5 = false;
					s1count = 2;
					foreach (GameObject g in shapes)
						g.GetComponent<Button>().interactable = false;
					shapes[3].GetComponent<Button>().interactable = true;
					foreach (GameObject g1 in subShapes)
						g1.SetActive(false);
					subShapes[3].SetActive(true);
                    //fracToDisplay.GetComponent<TEXDraw>().text = "";
                    QuestText.GetComponent<TEXDraw>().text = "Identify the correct multiplier for the given shapes";
					//QuestText.GetComponent<TEXDraw>().text = "Find pairs \nof\nEquivalent fractions.";

					break;
			}
		}

	}
	public void remove()
    {
		foreach (Transform child in initiatedObject.transform)
		{
			GameObject.Destroy(child.gameObject);
		}
		fracToDisplay.SetActive(false);

	}
	void Start () 
	{
		//StartCoroutine("disableInfoPanelBegining");
		//Invoke("disableInfoPanelBegining", 10.0f);
		score = 0;
		LostFood = 0;
		lastCreated = 0;
		noOfShape = 1;
		allCorrectCount = 0;
		lastCreated = Time.time;
		score_Text.text = "Total = ";
		missedObject = "";
		Debug_Text.text = "";
		//fracToDisplay = GameObject.Find("fracText");
		fracToDisplay.GetComponent<TEXDraw>().text = "";
		fracToDisplay.SetActive(false);
		//fracToDisplayOnScreen = GameObject.Find("QuestionOnScreenText");
		fracToDisplayOnScreen.GetComponent<TEXDraw>().text = "";
		//fracToDisplayPnl = GameObject.Find("QuestionOnScreen");
        fracToDisplayPnl.SetActive(false);
		belowPanel.SetActive(false);

		speed = 0.02f;
		generationSpeed = 3.0f;
		//isGameActive = false;
		isShape1 = true;
		isShape2 = false;
		isShape3 = false;
		isShape4 = false;
		isShape5 = false;
		isShapes = false;
		isPause = false;
		gameOverPnl.SetActive(false);
		enableInfoPanel();
		//QuestText.GetComponent<TEXDraw>().text = "Find Next Equivalent fractions for \\frac{2}{3}";
		QuestText.GetComponent<TEXDraw>().text = "";
		correctSound = GameObject.Find("correct").GetComponent<AudioSource>();
		wrongSound = GameObject.Find("wrong").GetComponent<AudioSource>();
		Invoke("CreateObjects", 0.1f);



	}
	public void setFracText(string fracText)
    {
		fracToDisplay.GetComponent<TEXDraw>().text = fracText;
		fracToDisplay.SetActive(true);

	}
	public void enableInfoPanel()
    {
		isGameActive = true;
		infoPanel.SetActive(true);
        //Time.timeScale = 0f;
    }
	public void disableInfoPanel()
	{
        Time.timeScale = 1f;
        //isGameActive = false;
		
		infoPanel.SetActive(false);
        //enableQuestionOnScreen();

    }
    public void enableQuestionOnScreen()
    {
        isGameActive = true;
        QuestText.GetComponent<TEXDraw>().text = "";
		fracToDisplay.GetComponent<TEXDraw>().text = "";
		belowPanel.SetActive(false);
		fracToDisplayPnl.SetActive(true);
        foreach (GameObject g in shapes)
            g.GetComponent<Button>().interactable = false;

        foreach (GameObject g1 in subShapes)
            g1.SetActive(false);

        if (noOfShape == 1)
        {
            foreach (GameObject g in questShapes)
                g.SetActive(false);
            questShapes[0].SetActive(true);
            fracToDisplayOnScreen.GetComponent<TEXDraw>().text = "Find the next 'Equivalent Fractions' for";
            finishedObjectiveText = "";
            startedObjectiveText = "Identifying 'Equivalent Fraction' by comparing between two shapes";
        }
        else if (noOfShape == 2)
        {
            foreach (GameObject g in questShapes)
                g.SetActive(false);
            questShapes[1].SetActive(true);
            fracToDisplayOnScreen.GetComponent<TEXDraw>().text = "Find the next 'Equivalent Fractions' for \\frac{1}{3}";
            finishedObjectiveText = "User has identified 'Equivalent fraction' successfully";
            startedObjectiveText = "Identifying 'Equivalent Fraction' by comparing between shape and fractions";
        }
        else if (noOfShape == 3)
        {
            foreach (GameObject g in questShapes)
                g.SetActive(false);
            questShapes[4].SetActive(true);
            fracToDisplayOnScreen.GetComponent<TEXDraw>().text = "Find the next 'Equivalent Fractions' for";
            finishedObjectiveText = "User has identified 'Equivalent fraction' successfully";
            startedObjectiveText = "Identifying 'Equivalent Fraction' by comparing between fractions and shape";
        }
        else if (noOfShape == 4)
        {
         
            foreach (GameObject g in questShapes)
                g.SetActive(false);
            questShapes[2].SetActive(true);
            fracToDisplayOnScreen.GetComponent<TEXDraw>().text = "Identify the correct multiplier for the given shapes";
            finishedObjectiveText = "User has identified 'Equivalent fraction' successfully";
            startedObjectiveText = "Started Level 4 Identify Numerator and Denominator";
        }
        else if (noOfShape == 5)
        {
            
            foreach (GameObject g in questShapes)
                g.SetActive(false);
            questShapes[3].SetActive(true);
			//finishedObjectiveText = "Finished Level2";
			//startedObjectiveText = "Started Level 3 Match fraction to Shape";
			//fracToDisplayOnScreen.GetComponent<TEXDraw>().text = "Identify the correct multiplier for the given shapes";
			fracToDisplayOnScreen.GetComponent<TEXDraw>().text = "Identify the correct multiplier for the given shapes";
			
            if (onLogMessage != null)
            {
                onLogMessage(finishedObjectiveText);
            }
        }
       
        //Time.timeScale = 0f;
        ////Invoke("disableQuestionOnScreen", 10.0f);
        //if (onLogMessage != null)
        //{
        //    onLogMessage(finishedObjectiveText);
        //}
        Invoke("showStartObjectiveState", 0.1f);
        // Time.timeScale = 0f;
        if (onLogMessage != null)
        {
            onLogMessage(finishedObjectiveText);
        }
    }
    void showStartObjectiveState()
    {
        if (onLogMessage != null)
        {
            onLogMessage(startedObjectiveText);
        }
    }

    public void showOnGameOver()
    {
        if (onLogMessage != null)
        {
            onLogMessage("'Active Experimentation’ Session ends");
        }
        Invoke("reset", 3);
    }
    public void disableQuestionOnScreen()
	{
		//Time.timeScale = 1f;
		isGameActive = false;
		fracToDisplay.GetComponent<TEXDraw>().text = "";
		fracToDisplayPnl.SetActive(false);
		belowPanel.SetActive(true);
		Invoke("CreateObjects", generationSpeed);
		Debug.Log("changes2");

	}
	public void disableInfoPanelBegining()
	{
		
		//yield return new WaitForSecondsRealtime(10.0f);
		//isGameActive = false;
		
		Time.timeScale = 1f;
		Debug_Text.text = "Start the game";
        infoPanel.SetActive(false);
        enableQuestionOnScreen();
		StopAllCoroutines();

    }

	public void play()
    {
		Time.timeScale = 1f;
		isGameActive = false;
		pauseBtn.SetActive(true);
		playBtn.SetActive(false);
	}
	public void pause()
	{
		isGameActive = true;
		pauseBtn.SetActive(false);
		playBtn.SetActive(true);
		Time.timeScale = 0f;
	}
	public void reset()
    {
		SceneManager.LoadScene(10);

	}
	void CreateObjects()
	{
		Debug.Log("3");
		if (isGameActive == false)
			{
				if (isShapes == true)
				{
					initShape();
				}
				else
				{
					initNumber();
				}
			}
		
		
	}

	void initShape()
    {
		if (isGameActive == false)
		{
			if (isShape1 == true)
			{

				GameObject g = Instantiate(objects[Random.Range(0, objects.Length)], new Vector3(Random.Range(-4.5f, 3.0f), 5.5f, 0), Quaternion.identity) as GameObject;

				if (g.name == "2by4(Clone)")

					g.tag = "Untagged";
				else if (g.name == "3by6(Clone)")
					g.tag = "Untagged";
				else if (g.name == "4by8(Clone)")
					g.tag = "Untagged";
				else
					g.tag = "Wrong";
				g.transform.parent = initiatedObject.transform;

				//if (Score % 5 == 0)
				//	speed -= 0.1f;

				Invoke("CreateObjects", generationSpeed);
			}
			if (isShape2 == true)
			{

				GameObject g = Instantiate(object1[Random.Range(0, object1.Length)], new Vector3(Random.Range(-4.5f, 3.3f), 5.5f, 0), Quaternion.identity) as GameObject;
				 if (g.name == "2by6(Clone)")
					g.tag = "Untagged";
				else if (g.name == "3by9(Clone)")
					g.tag = "Untagged";
				else if (g.name == "4by12(Clone)")
					g.tag = "Untagged";

				else
					g.tag = "Wrong";


				g.transform.parent = initiatedObject.transform;

				//if (Score % 5 == 0)
				//	speed -= 0.1f;

				Invoke("CreateObjects", generationSpeed);
			}
			if (isShape3 == true)
			{

				GameObject g = Instantiate(numbers2[Random.Range(0, numbers2.Length)], new Vector3(Random.Range(-4.5f, 2.8f), 5.5f, 0), Quaternion.identity) as GameObject;
				if (userTotalCorrect == 0 && g.name == "2by2(Clone)")

					g.tag = "Untagged";
				else if (userTotalCorrect == 1 && g.name == "4by4(Clone)")
					g.tag = "Untagged";
				else if (userTotalCorrect == 2 && g.name == "3by3(Clone)")
					g.tag = "Untagged";
				else
					g.tag = "Wrong";
				//GameObject g = Instantiate(object2[Random.Range(0, object2.Length)], new Vector3(Random.Range(-4.5f, 2.8f), 5.5f, 0), Quaternion.identity) as GameObject;
				g.transform.parent = initiatedObject.transform;

				//if (Score % 5 == 0)
				//	speed -= 0.1f;

				Invoke("CreateObjects", generationSpeed);
			}
			if (isShape4 == true)
			{

				GameObject g = Instantiate(object3[Random.Range(0, object3.Length)], new Vector3(Random.Range(-4.5f, 2.8f), 5.5f, 0), Quaternion.identity) as GameObject;
				if (userTotalCorrect == 0 && g.name == "4by4(Clone)")
					g.tag = "Untagged";
				else if (userTotalCorrect == 1 && g.name == "2by2(Clone)")
					g.tag = "Untagged";
				else if (userTotalCorrect == 2 && g.name == "3by3(Clone)")
					g.tag = "Untagged";
				else
					g.tag = "Wrong";
				g.transform.parent = initiatedObject.transform;

				//if (Score % 5 == 0)
				//	speed -= 0.1f;

				Invoke("CreateObjects", generationSpeed);
			}
			if (isShape5 == true)
			{

				GameObject g = Instantiate(object4[Random.Range(0, objects.Length)], new Vector3(Random.Range(-4.5f, 3.0f), 5.5f, 0), Quaternion.identity) as GameObject;

				if (g.name == "4by6(Clone)")

					g.tag = "Untagged";
				else if (g.name == "6by9(Clone)")
					g.tag = "Untagged";
				else if (g.name == "8by12(Clone)")
					g.tag = "Untagged";
				else
					g.tag = "Wrong";
				g.transform.parent = initiatedObject.transform;

				//if (Score % 5 == 0)
				//	speed -= 0.1f;

				Invoke("CreateObjects", generationSpeed);
			}
		}
	}

	void initNumber()
    {
		Debug.Log("8");
		if (isShape1 == true)
		{
			Debug.Log("9");
			if (isGameActive == false)
		{
				Debug.Log("10");
				GameObject g = Instantiate(objects[Random.Range(0, numbers.Length)], new Vector3(Random.Range(-4.5f, 2.8f), 5.5f, 0), Quaternion.identity) as GameObject;
				if (g.name == "2by4(Clone)")

					g.tag = "Untagged";
				else if (g.name == "3by6(Clone)")
					g.tag = "Untagged";
				else if (g.name == "4by8(Clone)")
					g.tag = "Untagged";
				else
					g.tag = "Wrong";
				

				g.transform.parent = initiatedObject.transform;
			//if (Score % 5 == 0)
			//	speed -= 0.1f;
		}
			Invoke("CreateObjects", generationSpeed);
		}
		if (isShape2 == true)
		{
		if (isGameActive == false)
		{
			GameObject g = Instantiate(numbers1[Random.Range(0, numbers1.Length)], new Vector3(Random.Range(-4.5f, 2.8f), 5.5f, 0), Quaternion.identity) as GameObject;
				 if (g.name == "2by6(Clone)")
					g.tag = "Untagged";
				else if (g.name == "3by9(Clone)")
					g.tag = "Untagged";
				else if (g.name == "4by12(Clone)")
					g.tag = "Untagged";
				
				else
					g.tag = "Wrong";

				g.transform.parent = initiatedObject.transform;
			//if (Score % 5 == 0)
			//	speed -= 0.1f;
		}
			Invoke("CreateObjects", generationSpeed);
		}
		if (isShape3 == true)
		{
		if (isGameActive == false)
		{
			GameObject g = Instantiate(numbers2[Random.Range(0, numbers2.Length)], new Vector3(Random.Range(-4.5f, 2.8f), 5.5f, 0), Quaternion.identity) as GameObject;
				if (userTotalCorrect == 0 && g.name == "2by2(Clone)")
					g.tag = "Untagged";
				else if (userTotalCorrect == 1 && g.name == "4by4(Clone)")
					g.tag = "Untagged";
				else if (userTotalCorrect == 2 && g.name == "3by3(Clone)")
					g.tag = "Untagged";
				else
					g.tag = "Wrong";
				g.transform.parent = initiatedObject.transform;
			//if (Score % 5 == 0)
			//	speed -= 0.1f;
		}
			Invoke("CreateObjects", generationSpeed);
		}
		if (isShape4 == true)
		{
			if (isGameActive == false)
			{
				GameObject g = Instantiate(numbers3[Random.Range(0, numbers3.Length)], new Vector3(Random.Range(-4.5f, 3.0f), 5.5f, 0), Quaternion.identity) as GameObject;
				g.transform.parent = initiatedObject.transform;
				if (userTotalCorrect == 0 && g.name == "4by4(Clone)")
					g.tag = "Untagged";
				else if (userTotalCorrect == 1 && g.name == "2by2(Clone)")
					g.tag = "Untagged";
				else if (userTotalCorrect == 2 && g.name == "3by3(Clone)")
					g.tag = "Untagged";
				else
					g.tag = "Wrong";
				//if (Score % 5 == 0)
				//	speed -= 0.1f;
			}
			Invoke("CreateObjects", generationSpeed);
		}
		if (isShape5 == true)
		{
			if (isGameActive == false)
			{
				GameObject g = Instantiate(object4[Random.Range(0, objects.Length)], new Vector3(Random.Range(-4.5f, 3.0f), 5.5f, 0), Quaternion.identity) as GameObject;

				if (g.name == "4by6(Clone)")

					g.tag = "Untagged";
				else if (g.name == "6by9(Clone)")
					g.tag = "Untagged";
				else if (g.name == "8by12(Clone)")
					g.tag = "Untagged";
				else
					g.tag = "Wrong";
				g.transform.parent = initiatedObject.transform;

				//if (Score % 5 == 0)
				//	speed -= 0.1f;

				Invoke("CreateObjects", generationSpeed);
			}
		}
	}
}
