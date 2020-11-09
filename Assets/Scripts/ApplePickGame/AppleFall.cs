using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleFall : MonoBehaviour
{
	float appleFallingSpeed;
	Transform selectedApple;
	Vector3 pos;
	void Start()
    {
		Initialization();

	}
	void Initialization()
    {
		 appleFallingSpeed = GameObject.FindObjectOfType<GameManager>().AppleFallingSpeed;
		selectedApple = this.gameObject.transform;
		pos = this.transform.position;
		//Debug.Log("falling speed = " + gameObject.name);

	}
	void Update()
	{
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - appleFallingSpeed , 0);

        if (transform.localPosition.y <= 0)
		{
			Debug.Log("touched = " + gameObject.name);
			//transform.localPosition = new Vector3(transform.localPosition.x, 0, 0);
			int NoOfAppleToBeInitiated = Random.Range(0, 2);
			GameObject InitiatedApplel;
			switch(gameObject.tag)
            {
				case "ThirdAppleRed":
					
					InitiatedApplel = Instantiate(Resources.Load("Prefabs/ApplePicking/ApplesToBeInitiated/2_3_AppleRed")) as GameObject;
					InitiatedApplel.transform.position = new Vector3(selectedApple.position.x, selectedApple.position.y - 0.4f, selectedApple.position.z);
					InitiatedApplel.transform.localScale = new Vector3(1, 1, 1);
					InitiatedApplel.transform.parent = transform.parent;
					break;

				case "ThirdAppleGreen":
					InitiatedApplel = Instantiate(Resources.Load("Prefabs/ApplePicking/ApplesToBeInitiated/2_3_AppleGreen")) as GameObject;
					InitiatedApplel.transform.position = new Vector3(selectedApple.position.x, selectedApple.position.y - 0.4f, selectedApple.position.z);
					InitiatedApplel.transform.localScale = new Vector3(1, 1, 1);
					InitiatedApplel.transform.parent = transform.parent;
					break;
				case "ThirdAppleYellow":
					InitiatedApplel = Instantiate(Resources.Load("Prefabs/ApplePicking/ApplesToBeInitiated/2_3_Yellow")) as GameObject;
					InitiatedApplel.transform.position = new Vector3(selectedApple.position.x, selectedApple.position.y - 0.4f, selectedApple.position.z);
					InitiatedApplel.transform.localScale = new Vector3(1, 1, 1);
					InitiatedApplel.transform.parent = transform.parent;
					break;
				case "HalfAppleRed":
					InitiatedApplel = Instantiate(Resources.Load("Prefabs/ApplePicking/ApplesToBeInitiated/HalfAppleRed")) as GameObject;
					InitiatedApplel.transform.position = new Vector3(selectedApple.position.x, selectedApple.position.y - 0.4f, selectedApple.position.z);
					InitiatedApplel.transform.localScale = new Vector3(1, 1, 1);
					InitiatedApplel.transform.parent = transform.parent;
					break;
				case "HalfAppleYellow":
					InitiatedApplel = Instantiate(Resources.Load("Prefabs/ApplePicking/ApplesToBeInitiated/HalfAppleYellow")) as GameObject;
					InitiatedApplel.transform.position = new Vector3(selectedApple.position.x, selectedApple.position.y - 0.4f, selectedApple.position.z);
					InitiatedApplel.transform.localScale = new Vector3(1, 1, 1);
					InitiatedApplel.transform.parent = transform.parent;
					break;
				case "HalfAppleGreen":
					InitiatedApplel = Instantiate(Resources.Load("Prefabs/ApplePicking/ApplesToBeInitiated/HalfAppleGreen")) as GameObject;
					InitiatedApplel.transform.position = new Vector3(selectedApple.position.x, selectedApple.position.y - 0.4f, selectedApple.position.z);
					InitiatedApplel.transform.localScale = new Vector3(1, 1, 1);
					InitiatedApplel.transform.parent = transform.parent;
					break;
				case "FullRedApple":
					InitiatedApplel = Instantiate(Resources.Load("Prefabs/ApplePicking/ApplesToBeInitiated/FullAppleRed")) as GameObject;
					InitiatedApplel.transform.position = new Vector3(selectedApple.position.x, selectedApple.position.y - 0.4f, selectedApple.position.z);
					InitiatedApplel.transform.localScale = new Vector3(1, 1, 1);
					InitiatedApplel.transform.parent = transform.parent;
					break;
				case "FullGreenApple":
					InitiatedApplel = Instantiate(Resources.Load("Prefabs/ApplePicking/ApplesToBeInitiated/FullAppleGreen")) as GameObject;
					InitiatedApplel.transform.position = new Vector3(selectedApple.position.x, selectedApple.position.y - 0.4f, selectedApple.position.z);
					InitiatedApplel.transform.localScale = new Vector3(1, 1, 1);
					break;
				case "FullYellowApple":
					InitiatedApplel = Instantiate(Resources.Load("Prefabs/ApplePicking/ApplesToBeInitiated/FullAppleYellow")) as GameObject;
					InitiatedApplel.transform.position = new Vector3(selectedApple.position.x, selectedApple.position.y - 0.4f, selectedApple.position.z);
					InitiatedApplel.transform.localScale = new Vector3(1, 1, 1);
					break;
			}

			this.transform.position = pos;
			gameObject.SetActive(false);
			
		}
	}
	void OnDisable()
    {
		this.gameObject.GetComponent<AppleFall>().enabled = false;
    }
}
