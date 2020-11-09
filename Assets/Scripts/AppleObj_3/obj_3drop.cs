using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class obj_3drop : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image containerImage;
    public Image receivingImage;
    public  Color normalColor;
    public Color highlightColor = Color.green;
    public Color highlightWrongColor = Color.red;
    public GameObject currentData;
    public AppleManager AppleManager;
    public Sprite dead;
    public bool dragged;


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
        if (dropSprite != null&&!dragged)
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

                if (originalObj.name.Equals(gameObject.name)&& originalObj.name.Equals("num"))
                {
                FindObjectOfType<Obj3Manager>().num_dragged = true;
                }
                else if (originalObj.name.Equals(gameObject.name) && originalObj.name.Equals("denum"))
                {
                FindObjectOfType<Obj3Manager>().denum_dragged = true;
                }
           
        dragged = true;
            //}

        }
        else
        {
           
            Debug.Log("nothing is coming");
        }
         

          
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

        containerImage.color = normalColor;
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