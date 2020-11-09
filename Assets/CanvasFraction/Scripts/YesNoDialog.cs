using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YesNoDialog : MonoBehaviour {

    // Use this for initialization
    public TEXDraw tex;

    public void showMessage(string msg)
    {
        tex.text = msg;

    }
    public void closeMsg()
    {
        this.gameObject.SetActive(false);
       
    }
}
