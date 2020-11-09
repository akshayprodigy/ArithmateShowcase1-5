using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowController : MonoBehaviour {

    // Use this for initialization
    public int noofrows;
    public int roughIndex;
    public bool hasRoughwork;
    public delegate void OpenRoughWorkAction(int index);
    public static event OpenRoughWorkAction OnOpenRoughWorkAction;


    public void openRoughWorkArea()
    {
        if (hasRoughwork)
        {
            if (OnOpenRoughWorkAction != null)
                OnOpenRoughWorkAction(roughIndex);
            Debug.Log("Opening Rough work Setion");
        }
        else
        {
            Debug.Log("No rough work");
        }
    }
}
