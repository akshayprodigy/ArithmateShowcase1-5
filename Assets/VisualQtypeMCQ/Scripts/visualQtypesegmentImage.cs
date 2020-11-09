using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum moving { horizontal, vertical, nothing };
public class visualQtypesegmentImage : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public moving snapMoveing = moving.vertical;
    moving currentlyMoving = moving.nothing;
    List<GameObject> SnapeLine;
   public Vector2 previousPosition;
    int snapePos = 0;
    public int AnswerPosition = 5;
    public float movingNow;
    public Vector2 movingDirection;
    internal moving SnapMoveing { get => snapMoveing; set => snapMoveing = value; }

    public delegate void SnapPoint(bool answer);
    public static event SnapPoint OnSnapPoint;
    public void OnBeginDrag(PointerEventData eventData)
    {
        SnapeLine[snapePos].SetActive(true);
        previousPosition = eventData.position;
        //throw new System.NotImplementedException();
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag: " + eventData.position+""+(eventData.position-previousPosition));
        SnapeLine[snapePos].SetActive(false);
         movingDirection = eventData.position - previousPosition;
        if (Mathf.Abs(movingDirection.x)> Mathf.Abs(movingDirection.y))
        {
            currentlyMoving = moving.horizontal;
        }
        else
        {
            currentlyMoving = moving.vertical;
        }

        if (currentlyMoving == SnapMoveing)
        {
            if (SnapMoveing == moving.vertical)
            {
                if ((movingDirection.y) > 0)
                {
                    snapePos--;
                    if (snapePos < 0)
                        snapePos = 0;
                }
                else
                {
                    snapePos++;
                    if (snapePos > (SnapeLine.Count - 1))
                        snapePos = SnapeLine.Count - 1;
                }
            }
            else
            {
                if (movingDirection.x > 0)
                {
                    snapePos--;
                    if (snapePos < 0)
                        snapePos = 0;
                }
                else
                {
                    snapePos++;
                    if (snapePos > (SnapeLine.Count - 1))
                        snapePos = SnapeLine.Count - 1;
                }
            }
        }
        SnapeLine[snapePos].SetActive(true);
        previousPosition = eventData.position;
        //throw new System.NotImplementedException();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(snapePos == AnswerPosition)
        {
            if (OnSnapPoint != null)
                OnSnapPoint(true);
            GameObject.FindObjectOfType<Obj10manager>().rightcut = true;
            Debug.Log("Right answer");
        }
        else
        {
            if (OnSnapPoint != null)
                OnSnapPoint(false);
            GameObject.FindObjectOfType<Obj10manager>().rightcut = false;
            Debug.Log("wrong  answersnapePos "+ snapePos+ " AnswerPosition "+ AnswerPosition);
        }
        //throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        SnapeLine = new List<GameObject>();
        foreach (Transform child in transform)
        {
            SnapeLine.Add(child.gameObject);
        }
        foreach (GameObject obj in SnapeLine)
            obj.SetActive(false);
    }

}
