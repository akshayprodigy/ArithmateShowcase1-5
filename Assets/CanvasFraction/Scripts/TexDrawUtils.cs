using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TexDrawUtils : MonoBehaviour {

    // Use this for initialization
    public bool multiDigitAllowed;
    public int numberOfDigit;
    public bool lcmFactor;
    public bool notClickable;
    public bool openingBracketAdded;
    public int numberOfExp = 0;
    public int lastValueNoOfDigits;
    public List<int> noOfDigitsLastValue = new List<int>();
    public List<int> valueIndex = new List<int>();
    public List<int> expIndex = new List<int>();
    public delegate void TexDrawClickedAction(GameObject obj);
    public static event TexDrawClickedAction OnTexDrawClickedAction;
    public int serialNumber;
    public int faceValue;
    public int placeValue;
    public string texType;
    public string texPosition;
    public string texString;
    public List<int> digitvalue = new List<int>();
    public List<string> expValue = new List<string>();

    public void OnTexDrawClicked()
    {
        //Debug.Log("TexDraw Clicked");
        if (OnTexDrawClickedAction != null)
        {
            OnTexDrawClickedAction(this.gameObject);
        }
    }
}
