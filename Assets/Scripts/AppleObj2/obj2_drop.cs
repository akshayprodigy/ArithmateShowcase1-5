using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class obj2_drop : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
	public Image containerImage;
	public Image receivingImage;
	private Color normalColor;
	public Color highlightColor = Color.green;
	public Color highlightWrongColor = Color.red;
	public GameObject currentData, childrenGO;
	public AppleManager AppleManager;
	public int t1, t2, t3;


	public void OnEnable()
	{
		receivingImage = this.GetComponent<Image>();
		containerImage = this.transform.parent.parent.GetComponent<Image>();
		if (containerImage != null)
			normalColor = containerImage.color;
		//AppleManager = GameObject.FindObjectOfType<AppleManager>();
		childrenGO = GameObject.Find("Tray1/GameObject/AppleInTray");

	}

	public void OnDrop(PointerEventData data)
	{
		if (GameObject.Find("GameManager").GetComponent<GameManager>().isObj1On)
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
				else if ((originalObj.name == "AppleInTray" && containerImage.name == "Tray1"))
				{
					//AppleManager.filledWholeSlot = AppleManager.filledWholeSlot + 1;
					t1 = t1 + 1;
					// childrenGO = GameObject.Find("Tray1/GameObject/AppleInTray"); 
					//receivingImage = childrenGO.GetComponent<Image>();
					receivingImage = GameObject.FindGameObjectWithTag("Obj2t1a1").transform.GetComponent<Image>();
					receivingImage.tag = "Red";


					//receivingImage = GameObject.Find("Tray1").transform.GetChild(1).GetChild(t1).GetComponent<Image>();
					Debug.Log("count " + AppleManager.filledWholeSlot);
					Color c = receivingImage.color;
					c.a = 1;
					receivingImage.color = c;
					foreach (GameObject g in GameObject.FindGameObjectsWithTag("Obj2t1a1"))
					{
						Color d = g.GetComponent<Image>().color;
						d.a = 0;
						g.GetComponent<Image>().color = d;
					}
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


					if ((originalObj.name == "AppleInTray" && containerImage.name == "Full"))
						receivingImage.tag = "FullApple";
					//rem();
					Invoke("rem", 0.01f);
				}
				else if ((originalObj.name == "AppleInTray" && containerImage.name == "Tray2"))
				{
					t2 = t2 + 1;
					receivingImage = GameObject.FindGameObjectWithTag("Obj2t2a2").transform.GetComponent<Image>();
					receivingImage.tag = "Green";
					Color c = receivingImage.color;
					c.a = 1;
					receivingImage.color = c;

					foreach (GameObject g in GameObject.FindGameObjectsWithTag("Obj2t2a2"))
					{
						Color d = g.GetComponent<Image>().color;
						d.a = 0;
						g.GetComponent<Image>().color = d;
					}
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


					if ((originalObj.name == "AppleInTray" && containerImage.name == "Full"))
						receivingImage.tag = "FullApple";
					//rem();
					Invoke("rem", 0.01f);
				}
				else if ((originalObj.name == "AppleInTray" && containerImage.name == "Tray3"))
				{
					t3 = t3 + 1;
					receivingImage = GameObject.FindGameObjectWithTag("Obj2t3a3").transform.GetComponent<Image>();
					receivingImage.tag = "Yellow";
					Color c = receivingImage.color;
					c.a = 1;
					receivingImage.color = c;

					foreach (GameObject g in GameObject.FindGameObjectsWithTag("Obj2t3a3"))
					{
						Color d = g.GetComponent<Image>().color;
						d.a = 0;
						g.GetComponent<Image>().color = d;
					}
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


					if ((originalObj.name == "AppleInTray" && containerImage.name == "Full"))
						receivingImage.tag = "FullApple";
					//rem();
					Invoke("rem", 0.01f);
				}

				else
				{
					GameObject.Find("ErrorSound").GetComponent<AudioSource>().Play();
					containerImage.color = highlightWrongColor;

					Invoke("resetColor", 0.6f);
				}
			}
		}
	}
	void changeTag()
    {
		if(currentData.transform.parent.parent.gameObject.name == "Tray1")
        {
			currentData.transform.gameObject.tag = "Obj2t1a1";
			Color c = currentData.transform.gameObject.GetComponent<Image>().color;
			c.a = 0;
			currentData.transform.gameObject.GetComponent<Image>().color = c;
			
		}
		else if (currentData.transform.parent.parent.gameObject.name == "Tray2")
		{
			currentData.transform.gameObject.tag = "Obj2t2a2";
			Color c = currentData.transform.gameObject.GetComponent<Image>().color;
			c.a = 0;
			currentData.transform.gameObject.GetComponent<Image>().color = c;
			
		}
		else if (currentData.transform.parent.parent.gameObject.name == "Tray3")
		{
			currentData.transform.gameObject.tag = "Obj2t3a3";
			Color c = currentData.transform.gameObject.GetComponent<Image>().color;
			c.a = 0;
			currentData.transform.gameObject.GetComponent<Image>().color = c;
			
		}
		else if (currentData.transform.parent.parent.gameObject.name == "Full")
		{
			Color c = currentData.transform.gameObject.GetComponent<Image>().color;
			c.a = 0;
			currentData.transform.gameObject.GetComponent<Image>().color = c;
			
		}

		
		currentData.gameObject.GetComponent<obj2_drag>().enabled = false;
		foreach (GameObject t1a1 in GameObject.FindGameObjectsWithTag("Red"))
			t1a1.GetComponent<obj2_drag>().enabled = true;
		foreach (GameObject t2a2 in GameObject.FindGameObjectsWithTag("Green"))
			t2a2.GetComponent<obj2_drag>().enabled = true;
		foreach (GameObject t3a3 in GameObject.FindGameObjectsWithTag("Yellow"))
			t3a3.GetComponent<obj2_drag>().enabled = true;
	}
	void rem()
	{
		changeTag();

		
	}
	void callNaext()
	{

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

		var dragMe = originalObj.GetComponent<obj2_drag>();
		if (dragMe == null)
			return null;

		var srcImage = originalObj.GetComponent<Image>();
		if (srcImage == null)
			return null;

		return srcImage.sprite;

	}
}