using UnityEngine;
using System.Collections;
using DG.Tweening;

public class ObjectMove : MonoBehaviour 
{
	public delegate void LogMessage(string cases);
	public static event LogMessage onLogMessage;
	void Update()
	{
		Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
		if (Input.GetMouseButtonDown(0))
		{
			if (GameObject.FindObjectOfType<GameManager1>().isGameActive == false)
			{
				if (hit != null && hit.collider != null)
				{
                    if (hit.collider.name == this.transform.gameObject.name)
                    {
                        if (hit.collider.tag == "Wrong" || hit.collider.tag == "Untagged")
						{
							Debug.Log("i have selected = " + hit.collider.name +"and my name is = "+ hit.transform.gameObject.name);

							selectInSequence();
							totalSelected();
							checkAnswer();


						}
						if (hit.collider.tag == "Wrong")
						{
							if (onLogMessage != null)
							{
								onLogMessage("User does not know equivalent fraction");
							}
							GameObject.FindObjectOfType<GameManager1>().wrongSound.Play();
							this.transform.DOShakePosition(1.5f, 0.5f, 5, 10, false, true);
							Handheld.Vibrate();
						}
						else if (hit.collider.tag == "Untagged")
						{
							if (onLogMessage != null)
							{
								onLogMessage("User knows equivalent fraction");
							}
							GameObject.FindObjectOfType<GameManager1>().correctSound.Play();
							Debug.Log("name = " + this.name);
							if (this.name == "2by2(Clone)")
							{
								GameObject.FindObjectOfType<GameManager1>().setFracText("\\frac{2}{2}");

							}
							else if (this.name == "3by3(Clone)")
							{
								GameObject.FindObjectOfType<GameManager1>().setFracText("\\frac{3}{3}");

							}
							else if (this.name == "4by4(Clone)")
							{
								GameObject.FindObjectOfType<GameManager1>().setFracText("\\frac{4}{4}");

							}
							Destroy(this.gameObject);
							//if (GameObject.FindObjectOfType<GameManager1>().isShape3 || GameObject.FindObjectOfType<GameManager1>().isShape4)
							//GameObject.FindObjectOfType<GameManager1>().remove();

						}

                    }

                }
			}
		}
	}
	void sendValues()
	{
		string Total_For_this_Shape = GameObject.FindObjectOfType<GameManager1>().userTotalCorrect.ToString();
		string Correct_in_sequence = GameObject.FindObjectOfType<GameManager1>().userSelectedSequence.ToString();
		string Missed = GameObject.FindObjectOfType<GameManager1>().LostFood.ToString();
		string Total_Selected = GameObject.FindObjectOfType<GameManager1>().allCorrectCount.ToString();
		string result = GameObject.FindObjectOfType<GameManager1>().answer;
		Debug.Log("values = " + result +Total_For_this_Shape + Correct_in_sequence + Missed + Total_Selected);
        if (onLogMessage != null)
        {
            onLogMessage("Result: " + result + "<br> <br>Total Correct: " + Total_For_this_Shape + "<br>Correct_in_sequence: " + Correct_in_sequence + "<br> Missed: " + Missed);
        }

    }
    void FixedUpdate () 
	{
		transform.position = new Vector3(transform.position.x , transform.position.y - GameObject.FindObjectOfType<GameManager1>().speed, 0);

		if (transform.position.y <= -4.5f)
		{
			if (gameObject.tag == "Untagged")
			{
				GameObject.FindObjectOfType<GameManager1>().LostFood++;
				GameObject.FindObjectOfType<GameManager1>().Debug_Text.text = "\nTotal = " + GameObject.FindObjectOfType<GameManager1>().userTotalCorrect
				+ "\nCorrect in sequence = " + GameObject.FindObjectOfType<GameManager1>().userSelectedSequence
				+ "\nMissed =" + GameObject.FindObjectOfType<GameManager1>().LostFood;
			}
			
			Destroy(gameObject);
		}
        if (GameObject.FindObjectOfType<GameManager1>().speed > 0.03f)
        {
            GameObject.FindObjectOfType<GameManager1>().speed = 0.03f;
        }
    }
	void Start()
    {
		//GameObject.FindObjectOfType<GameManager1>().fracToDisplay.GetComponent<TEXDraw>().text = "";
        GameObject.FindObjectOfType<GameManager1>().fracToDisplayPnl.SetActive(false);

    }
	void OnTriggerEnter2D(Collider2D col)
    {
		if(col.name=="Blade")
        {
			Debug.Log(this.name);
			selectInSequence();
			totalSelected();
			checkAnswer();
			
			Destroy(gameObject);
		}
    }
	void selectInSequence()
    {
		if (GameObject.FindObjectOfType<GameManager1>().userSelectedSequence < 3)
		{
			GameObject.FindObjectOfType<GameManager1>().userSelectedSequence++;
			if (this.tag == "Wrong")
			{
				GameObject.FindObjectOfType<GameManager1>().userSelectedSequence = 0;
				

			}
			
		}

		
	}
	void totalSelected()
	{
		if (this.tag!= "Wrong")
		{
			GameObject.FindObjectOfType<GameManager1>().userTotalCorrect++;
			GameObject.FindObjectOfType<GameManager1>().allCorrectCount++;
			GameObject.FindObjectOfType<GameManager1>().answer = "Correct";
			if(GameObject.FindObjectOfType<GameManager1>().userTotalCorrect>1)
            {
				GameObject.FindObjectOfType<GameManager1>().isShapes = true;

			}
			if (GameObject.FindObjectOfType<GameManager1>().scount == 0)
			{

				GameObject.FindObjectOfType<GameManager1>().speed = GameObject.FindObjectOfType<GameManager1>().speed + 0.005f;
                if (GameObject.FindObjectOfType<GameManager1>().speed > 0.03f)
                {
                    GameObject.FindObjectOfType<GameManager1>().speed = 0.03f;
                }
                GameObject.FindObjectOfType<GameManager1>().generationSpeed = GameObject.FindObjectOfType<GameManager1>().generationSpeed - 0.1f;
			}
			if (GameObject.FindObjectOfType<GameManager1>().s1count == 1 || GameObject.FindObjectOfType<GameManager1>().s1count == 2)
			{
				Debug.Log("Go remove");
				StartCoroutine("nextQuestActive");
				GameObject.FindObjectOfType<GameManager1>().remove();

			}
		}
		else
        {
			GameObject.FindObjectOfType<GameManager1>().answer = "Incorrect";
			
		}
		sendValues();
	}
	IEnumerator nextQuestActive()
    {
		Debug.Log("next method");
		yield return new WaitForSeconds(2.0f);
		if (GameObject.FindObjectOfType<GameManager1>().s1count == 1)
		{
			foreach (GameObject g in GameObject.FindObjectOfType<GameManager1>().s1)
				g.SetActive(false);
			GameObject.FindObjectOfType<GameManager1>().s1[GameObject.FindObjectOfType<GameManager1>().userTotalCorrect].SetActive(true);

		}
		else if (GameObject.FindObjectOfType<GameManager1>().s1count == 2)
		{
			foreach (GameObject g1 in GameObject.FindObjectOfType<GameManager1>().s2)
				g1.SetActive(false);
			GameObject.FindObjectOfType<GameManager1>().s2[GameObject.FindObjectOfType<GameManager1>().userTotalCorrect].SetActive(true);

		}
	}
	void checkAnswer()
    {
		if (GameObject.FindObjectOfType<GameManager1>().scount == 0)
        {
			if (GameObject.FindObjectOfType<GameManager1>().userSelectedSequence >= 3 && GameObject.FindObjectOfType<GameManager1>().userTotalCorrect >= 5)
			{
				Debug.Log("Done");
				if (GameObject.FindObjectOfType<GameManager1>().noOfShape < 5)
				{
					GameObject.FindObjectOfType<GameManager1>().noOfShape++;
					GameObject.FindObjectOfType<GameManager1>().Debug_Text.text = "\nTotal = " + GameObject.FindObjectOfType<GameManager1>().userTotalCorrect
					+ "\nCorrect in sequence = " + GameObject.FindObjectOfType<GameManager1>().userSelectedSequence
					+ "\nMissed =" + GameObject.FindObjectOfType<GameManager1>().LostFood
					+ "\nMove to next shape";
					//StartCoroutine(empty());
					resetValue();
				}
				else
				{
					GameObject.FindObjectOfType<GameManager1>().isGameActive = true;
					GameObject.FindObjectOfType<GameManager1>().remove();
					GameObject.FindObjectOfType<GameManager1>().gameOverPnl.SetActive(true);
					GameObject.FindObjectOfType<GameManager1>().belowPanel.SetActive(false);
					GameObject.FindObjectOfType<GameManager1>().pauseBtn.SetActive(false);
					GameObject.FindObjectOfType<GameManager1>().playBtn.SetActive(false);
					GameObject.FindObjectOfType<GameManager1>().Debug_Text.text = "\nTotal = " + GameObject.FindObjectOfType<GameManager1>().userTotalCorrect
					+ "\nCorrect in sequence = " + GameObject.FindObjectOfType<GameManager1>().userSelectedSequence
					+ "\nMissed =" + GameObject.FindObjectOfType<GameManager1>().LostFood
					+ "\nGame Over";
                    GameObject.FindObjectOfType<GameManager1>().showOnGameOver();
                }
			}
			else
			{
				GameObject.FindObjectOfType<GameManager1>().Debug_Text.text = "\nTotal = " + GameObject.FindObjectOfType<GameManager1>().userTotalCorrect
					+ "\nCorrect in sequence = " + GameObject.FindObjectOfType<GameManager1>().userSelectedSequence
					+ "\nMissed =" + GameObject.FindObjectOfType<GameManager1>().LostFood;

				StartCoroutine(empty());
			}
		}
		else
        {
			if (GameObject.FindObjectOfType<GameManager1>().userTotalCorrect >= 3)
			{
				Debug.Log("Done");
				if (GameObject.FindObjectOfType<GameManager1>().noOfShape < 5)
				{
					GameObject.FindObjectOfType<GameManager1>().noOfShape++;
					GameObject.FindObjectOfType<GameManager1>().Debug_Text.text = "\nTotal = " + GameObject.FindObjectOfType<GameManager1>().userTotalCorrect
					+ "\nCorrect in sequence = " + GameObject.FindObjectOfType<GameManager1>().userSelectedSequence
					+ "\nMissed =" + GameObject.FindObjectOfType<GameManager1>().LostFood
					+ "\nMove to next shape";
					//StartCoroutine(empty());
					resetValue();
				}
				else
				{
					GameObject.FindObjectOfType<GameManager1>().isGameActive = true;
					GameObject.FindObjectOfType<GameManager1>().remove();
					GameObject.FindObjectOfType<GameManager1>().gameOverPnl.SetActive(true);
					GameObject.FindObjectOfType<GameManager1>().belowPanel.SetActive(false);
					GameObject.FindObjectOfType<GameManager1>().pauseBtn.SetActive(false);
					GameObject.FindObjectOfType<GameManager1>().playBtn.SetActive(false);
					GameObject.FindObjectOfType<GameManager1>().Debug_Text.text = "\nTotal = " + GameObject.FindObjectOfType<GameManager1>().userTotalCorrect
					+ "\nCorrect in sequence = " + GameObject.FindObjectOfType<GameManager1>().userSelectedSequence
					+ "\nMissed =" + GameObject.FindObjectOfType<GameManager1>().LostFood
					+ "\nGame Over";
                    GameObject.FindObjectOfType<GameManager1>().showOnGameOver();
                }
			}
			else
			{
				GameObject.FindObjectOfType<GameManager1>().Debug_Text.text = "\nTotal = " + GameObject.FindObjectOfType<GameManager1>().userTotalCorrect
					+ "\nCorrect in sequence = " + GameObject.FindObjectOfType<GameManager1>().userSelectedSequence
					+ "\nMissed =" + GameObject.FindObjectOfType<GameManager1>().LostFood;

				StartCoroutine(empty());
			}
		}
    }
	void resetValue()
    {
		GameObject.FindObjectOfType<GameManager1>().userSelectedSequence = 0;
		GameObject.FindObjectOfType<GameManager1>().userTotalCorrect = 0;
		GameObject.FindObjectOfType<GameManager1>().speed = 0.02f;
		GameObject.FindObjectOfType<GameManager1>().LostFood = 0;
		GameObject.FindObjectOfType<GameManager1>().generationSpeed = 3.0f;
		GameObject.FindObjectOfType<GameManager1>().isShapes = false;
		GameObject.FindObjectOfType<GameManager1>().isPause = true;
		GameObject.FindObjectOfType<GameManager1>().remove();
        GameObject.FindObjectOfType<GameManager1>().enableQuestionOnScreen();
        GameObject.FindObjectOfType<GameManager1>().Debug_Text.text = "\nTotal = " + GameObject.FindObjectOfType<GameManager1>().userTotalCorrect
				+ "\nCorrect in sequence = " + GameObject.FindObjectOfType<GameManager1>().userSelectedSequence
				+ "\nMissed =" + GameObject.FindObjectOfType<GameManager1>().LostFood;
		


	}
	
	IEnumerator start_act()
	{
		yield return new WaitForSeconds(0.4f);
		GameObject.FindObjectOfType<GameManager1>().isPause = false;

	}
	IEnumerator empty()
    {
		yield return new WaitForSeconds(1.0f);
		Debug.Log("empty");
		GameObject.FindObjectOfType<GameManager1>().Debug_Text.text = "";

	}
}
