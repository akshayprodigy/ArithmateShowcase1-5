using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogControllerScript : MonoBehaviour {

    // Use this for initialization
    public delegate void CloseDialog();
    public static event CloseDialog OnCloseDialog;

    public TEXDraw tex;
    public string msg;
  
    private void Start()
    {
        showMessage();
    }

 

   public void showMessage()
    {
        if (msg == "")
            msg = "error in step";
        tex.text = msg;
        //this.gameObject.SetActive(true);

    }

    public void readMsg()
    {
        this.gameObject.SetActive(false);
        if (OnCloseDialog != null)
            OnCloseDialog();
    }
}
