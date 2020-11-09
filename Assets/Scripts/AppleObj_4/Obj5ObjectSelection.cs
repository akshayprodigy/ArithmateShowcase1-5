using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Obj5ObjectSelection : MonoBehaviour
{
    // Start is called before the first frame update
    void OnMouseButtonDown()
    {
        Debug.Log(this.gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (GameObject.FindObjectOfType<GameManager>().isObj4On)
                if (!FindObjectOfType<Obj4Manager>().graden_activity)
                    selectObject();

        }

    }
    void selectObject()
    {



        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.gameObject.name);
            if (hit.collider.gameObject.tag == "Map Part")
            {
                GameObject.FindObjectOfType<conversationManager>().DisableConversation();

                if (hit.collider.gameObject.GetComponent<SpriteRenderer>().color == Color.white
               || hit.collider.gameObject.GetComponent<SpriteRenderer>().color == new Color(1f, 0.51f, 0f))
                {
                    if (hit.collider.gameObject.name == "bench")
                        Obj4Manager.SelectedPart++;
                    else
                        Obj4Manager.other_part++;

                    hit.collider.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                }
                else if (hit.collider.gameObject.GetComponent<SpriteRenderer>().color == Color.green && (hit.collider.gameObject.name == "bench"))
                {
                    if (hit.collider.gameObject.name == "bench")
                        Obj4Manager.SelectedPart--;
                    else
                        Obj4Manager.other_part--;
                    hit.collider.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                }
                else if (hit.collider.gameObject.GetComponent<SpriteRenderer>().color == Color.green && (hit.collider.gameObject.name != "bench"))
                {
                    
                    hit.collider.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                    Obj4Manager.other_part--;
                }

            }
            else
            {
                if (hit.collider.gameObject.name == "bench")
                    Obj4Manager.SelectedPart--;
                else
                    Obj4Manager.other_part--;


                hit.collider.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
        Debug.Log("selected = " + Obj4Manager.SelectedPart);
        Debug.Log("other parts = " +Obj4Manager.other_part);
    }
}

