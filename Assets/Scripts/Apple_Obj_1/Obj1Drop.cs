using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Obj1Drop : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
	public Image containerImage;
	public Image receivingImage;
	private Color normalColor;
	public Color highlightColor = Color.green;
	public Color highlightWrongColor = Color.red;
	public GameObject currentData;
	public AppleManager AppleManager;


	public void OnEnable()
	{
		receivingImage = this.GetComponent<Image>();
		containerImage = this.transform.parent.GetComponent<Image>();
		if (containerImage != null)
			normalColor = containerImage.color;
		//AppleManager = GameObject.FindObjectOfType<AppleManager>();

	}

	public void OnDrop(PointerEventData data)
	{
		containerImage.color = normalColor;

		if (receivingImage == null)
			return;

		Sprite dropSprite = GetDropSprite(data);
		if (dropSprite != null)
		{
			var originalObj = data.pointerDrag;
			currentData = originalObj;
			Debug.Log("name = " + originalObj);
			

			if ((originalObj.name == "Frac" && containerImage.name == "Frac"))
			{
				AppleManager.filledFracSlots = AppleManager.filledFracSlots + 1;
				receivingImage = GameObject.Find("Frac").transform.GetChild(AppleManager.filledFracSlots).GetComponent<Image>();
				Color c = receivingImage.color;
				c.a = 1;
				receivingImage.color = c;
				Debug.Log("parent name = " + originalObj.transform.parent.name);
				receivingImage.overrideSprite = dropSprite;
				Color currentDataColor = currentData.GetComponent<Image>().color;
				currentDataColor.a = 0;
				currentData.GetComponent<Image>().color = currentDataColor;

				if (containerImage == null)
					return;

				Debug.Log("my name = " + originalObj.name + "\n Box name = " + containerImage.name);

				GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
				containerImage.color = highlightColor;

				Invoke("resetColor", 0.3f);


				if ((originalObj.name == "Frac" && containerImage.name == "Frac"))
					receivingImage.tag = "FracApple";

				Invoke("rem", 0.1f);
			}
			else if ((originalObj.name == "Full" && containerImage.name == "Full"))
			{
				AppleManager.filledWholeSlot = AppleManager.filledWholeSlot + 1;
				receivingImage = GameObject.Find("Full").transform.GetChild(AppleManager.filledWholeSlot).GetComponent<Image>();
				Color c = receivingImage.color;
				c.a = 1;
				receivingImage.color = c;
				Debug.Log("parent name = " + originalObj.transform.parent.name);
				receivingImage.overrideSprite = dropSprite;
				Color currentDataColor = currentData.GetComponent<Image>().color;
				currentDataColor.a = 0;
				currentData.GetComponent<Image>().color = currentDataColor;

				if (containerImage == null)
					return;

				Debug.Log("my name = " + originalObj.name + "\n Box name = " + containerImage.name);

				GameObject.Find("CorrectSound").GetComponent<AudioSource>().Play();
				containerImage.color = highlightColor;

				Invoke("resetColor", 0.6f);


				if ((originalObj.name == "Full" && containerImage.name == "Full"))
					receivingImage.tag = "FullApple";

				Invoke("rem", 0.1f);
			}

			else
			{
				GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
				containerImage.color = highlightWrongColor;

				Invoke("resetColor", 0.6f);
			}
		}

	}
	void rem()
	{


		currentData.transform.parent.gameObject.SetActive(false);
        //AppleManager.total();
        Debug.Log("Toatl remain = " + AppleManager.totalAppleCollected);
		if (AppleManager.totalAppleCollected == 1)
		{
			Debug.Log("All finished");
			//GameObject.Find("AppleSlot").transform.GetChild(1).gameObject.SetActive(false);
			GameObject.Find("AppleSlots").SetActive(false);
			GameObject.FindObjectOfType<Obj1Manager>().hintEnable();
			Invoke("callNaext", 1.0f);
		}

		else
			AppleManager.totalAppleCollected = AppleManager.totalAppleCollected - 1;
		//this.GetComponent<Obj1Drop>().enabled = false;
	}

	void callNaext()
	{
		Debug.Log("hint invoked");
		//GameObject.FindObjectOfType<Obj1Manager>().hintEnable();
	}
	public void OnPointerEnter(PointerEventData data)
	{
		//if (containerImage == null)
		//    return;

		//var dropSprite = GetDropSprite(data);

		//if (dropSprite != null)
		//{

		//    var originalObj = data.pointerDrag;
		//    currentData = originalObj;

		Debug.Log("my name = ");
		//if ((originalObj.name == "Full" && containerImage.name == "Full") || (originalObj.name == "Frac" && containerImage.name == "Frac"))
		//{
		//    containerImage.color = highlightColor;
		//}
		//else
		//{
		//    containerImage.color = highlightWrongColor;
		//}
		//}
	}

	void resetColor()
	{
		if (containerImage == null)
			return;

		containerImage.color = normalColor;
	}
	public void OnPointerExit(PointerEventData data)
	{
		if (containerImage == null)
			return;

		containerImage.color = normalColor;
	}

	private Sprite GetDropSprite(PointerEventData data)
	{

		var originalObj = data.pointerDrag;
		if (originalObj == null)
			return null;

		var dragMe = originalObj.GetComponent<Obj1Drag>();
		if (dragMe == null)
			return null;

		var srcImage = originalObj.GetComponent<Image>();
		if (srcImage == null)
			return null;

		return srcImage.sprite;

	}
}