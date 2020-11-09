using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class obj13_drop_me : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image containerImage;
    public Image receivingImage;
    public Sprite actual_image;
    public Color normalColor;
    public Color highlightColor = Color.green;
    public Color highlightWrongColor = Color.red;
    public GameObject currentData;
    public AppleManager AppleManager;
    public Sprite dead;


    public void OnEnable()
    {
        receivingImage = this.GetComponent<Image>();
        containerImage = this.GetComponent<Image>();



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

            //if (originalObj.name.Equals(gameObject.name))
            //{


            receivingImage.overrideSprite = dropSprite;
            Color currentDataColor = currentData.GetComponent<Image>().color;
            currentDataColor.a = 0;
            currentData.GetComponent<Image>().color = currentDataColor;

            if (containerImage == null)
                return;


            // containerImage.color = highlightColor;
            if (this.name.Equals("Quotient"))
            {
                FindObjectOfType<Obj13Manager>().enable_three_labels_submit();
                if (originalObj.name.Equals(gameObject.name) && originalObj.name.Equals("Quotient"))
                {
                    FindObjectOfType<Obj13Manager>().quotient = true;
                }
                else
                {
                   // containerImage.color = highlightWrongColor;
                }
            }
            else if (this.name.Equals("Remainder"))
            {
                FindObjectOfType<Obj13Manager>().enable_three_labels_submit();
                if (originalObj.name.Equals(gameObject.name) && originalObj.name.Equals("Remainder"))
                {
                    FindObjectOfType<Obj13Manager>().remainder = true;
                }
                else
                {
                  // containerImage.color = highlightWrongColor;
                }
            }
            else if (this.name.Equals("Divisor"))
            {
                FindObjectOfType<Obj13Manager>().enable_three_labels_submit();
                if (originalObj.name.Equals(gameObject.name) && originalObj.name.Equals("Divisor"))
                {
                    FindObjectOfType<Obj13Manager>().divisor = true;
                }
                else
                {
                  //  containerImage.color = highlightWrongColor;
                }
            }


            //}

        }
        else
        {
            Debug.Log("nothing is coming");
        }



    }

    public   void set_image_to_actual()
    {
        GetComponent<Image>().color = Color.white;
        receivingImage.overrideSprite = actual_image;
        GetComponent<AnimateColors>().enabled = true;
        Invoke("disable_highlight", 3);



    }

    void disable_highlight()
    {
        GetComponent<AnimateColors>().enabled = false;
        GetComponent<Image>().color = Color.white;
    }

    public void OnPointerEnter(PointerEventData data)
    {

        Debug.Log("my name = ");

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

       // containerImage.color = normalColor;
    }

    private Sprite GetDropSprite(PointerEventData data)
    {

        var originalObj = data.pointerDrag;
        if (originalObj == null)
            return null;

        var srcImage = originalObj.GetComponent<Image>();
        if (srcImage == null)
            return null;

        return srcImage.sprite;

    }
}
