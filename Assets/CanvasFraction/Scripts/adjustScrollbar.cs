using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class adjustScrollbar : MonoBehaviour {

    // Use this for initialization
    public bool adjust;
    float scrollValue;
    float lastsize;
    public Scrollbar v_scrollbar;
    private void OnEnable()
    {
        CanvasManager.OnNextStepAction += NextStepClicked;
        CanvasManager.OnFractionAction += NextStepClicked;
        CanvasManager.OnMixedFractionAction += NextStepClicked;
    }

    private void OnDisable()
    {
        CanvasManager.OnNextStepAction -= NextStepClicked;
        CanvasManager.OnFractionAction -= NextStepClicked;
        CanvasManager.OnMixedFractionAction += NextStepClicked;
    }
    public adjustScrollbar other;

    void NextStepClicked()
    {
        //scrollValue = this.GetComponent<Scrollbar>().value;
        this.GetComponent<ScrollRect>().verticalNormalizedPosition = 0;
        if(this != null)
            StartCoroutine("scrollrectdown");
    }

    private void Start()
    {
        if (adjust)
        {
            lastsize = (float)Math.Round(v_scrollbar.size, 2);
            //Debug.Log("Start change Value" + lastsize + " lastsize" + v_scrollbar.size);
        }
           
    }
    private void Update()
    {
        if (adjust)
        {
            if (lastsize != (float)Math.Round(v_scrollbar.size,2))
            {
                Debug.Log("change Value"+ lastsize+ " lastsize"+ v_scrollbar.size);
                this.GetComponent<ScrollRect>().verticalNormalizedPosition = 0;
                lastsize = (float)Math.Round(v_scrollbar.size, 2);
            }
        }
       
    }
    public void OnScroll()
    {
       // Debug.Log("v_scrollbar: " + v_scrollbar.value + " moved: " + v_scrollbar.isActiveAndEnabled + " active:"+ v_scrollbar.size);
       //Debug.Log("On Value Change of scrollbar: " + this.GetComponent<ScrollRect>().verticalNormalizedPosition);
       if (other.GetComponent<ScrollRect>().verticalScrollbar.IsActive() && this.GetComponent<ScrollRect>().verticalScrollbar.IsActive())
            other.adjusttheotherScroll(this.GetComponent<ScrollRect>().verticalNormalizedPosition);
    }

    IEnumerator scrollrectdown()
    {
        
        yield return new WaitForSeconds(1.5f);
        //    this.GetComponent<ScrollRect>().verticalNormalizedPosition = 0;
        //Canvas.ForceUpdateCanvases();
        //this.GetComponent<ScrollRect>().horizontalNormalizedPosition = 0;
    }

    public void adjusttheotherScroll(float value)
    {
        this.GetComponent<ScrollRect>().verticalNormalizedPosition = value;
    }
}
