using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Obj11Drop : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
	public Image containerImage;
	public Image receivingImage;
	private Color normalColor;
	public Color highlightColor = Color.green;
	public Color highlightWrongColor = Color.red;
	public GameObject currentData;
	public int trey1Slot, trey2Slot, trey3Slot;


	public void OnEnable()
	{
		Initilize();
		//AppleManager = GameObject.FindObjectOfType<AppleManager>();

	}
	public void Initilize()
    {
		receivingImage = this.GetComponent<Image>();
		containerImage = this.transform.parent.GetComponent<Image>();
		if (containerImage != null)
			normalColor = containerImage.color;
		trey1Slot = 0; trey2Slot = 0; trey3Slot = 0;
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
			Debug.Log("container name = " + containerImage);

			

			if ((originalObj.name == "6 Parts" && containerImage.name == "6 Parts"))
			{
				receivingImage = this.transform.parent.GetChild(trey1Slot).GetComponent<Image>();
				//receivingImage = GameObject.Find("6 Parts").GetComponent<Image>();
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

				if ((originalObj.name == "6 Parts" && containerImage.name == "6 Parts"))
					receivingImage.tag = "FracApple";
				trey1Slot = trey1Slot + 1;
				Invoke("rem", 0.1f);
			}
			else if ((originalObj.name == "3 Parts" && containerImage.name == "3 Parts"))
			{
				receivingImage = this.transform.parent.GetChild(trey2Slot).GetComponent<Image>();
				//receivingImage = GameObject.Find("6 Parts").GetComponent<Image>();
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
				if ((originalObj.name == "3 Parts" && containerImage.name == "3 Parts"))
					receivingImage.tag = "FullApple";
				trey2Slot = trey2Slot + 1;

				Invoke("rem", 0.1f);
			}
			else if ((originalObj.name == "8 Parts" && containerImage.name == "8 Parts"))
			{
				receivingImage = this.transform.parent.GetChild(trey3Slot).GetComponent<Image>();
				//receivingImage = GameObject.Find("6 Parts").GetComponent<Image>();
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
				if ((originalObj.name == "8 Parts" && containerImage.name == "8 Parts"))
					receivingImage.tag = "FullApple";
				trey3Slot = trey3Slot + 1;

				Invoke("rem", 0.1f);
			}
			

		}

	}
	void rem()
	{


		currentData.SetActive(false);
		GameObject.Find("SubmitButton").transform.GetChild(0).gameObject.SetActive(true);
	}

	void callNaext()
	{
		Debug.Log("hint invoked");
		GameObject.FindObjectOfType<Obj1Manager>().hintEnable();
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

		var dragMe = originalObj.GetComponent<Obj11Drag>();
		if (dragMe == null)
			return null;

		var srcImage = originalObj.GetComponent<Image>();
		if (srcImage == null)
			return null;

		return srcImage.sprite;

	}
}