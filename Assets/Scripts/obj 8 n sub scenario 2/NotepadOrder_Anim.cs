using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NotepadOrder_Anim : MonoBehaviour
{

    GameObject notePad;

    private void Awake()
    {
        notePad = GameObject.Find("Notepad").gameObject;

    }

    // Start is called before the first frame update
    void Start()
    {
        notePad.SetActive(false);
    }

    private void OnMouseDown()
    {
       if(this.tag == "Notepad_Order")
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = null;
            this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
            notePad.gameObject.SetActive(true);
            notePad.GetComponent<Animator>().enabled = true;

        }

        else if (this.tag == "Notepad_Anim")
        {
            notePad.gameObject.SetActive(false);
            foreach (DragSprite_Pizza pizzaSlice in FindObjectOfType<obj_8_subscenario2>().draggable_object)
            {
                pizzaSlice.enabled = true;
            }
        }
    }
}
