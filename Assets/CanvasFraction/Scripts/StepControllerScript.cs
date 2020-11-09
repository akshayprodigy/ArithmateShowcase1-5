using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepControllerScript : MonoBehaviour {

    // Use this for initialization
    public int rowNumber;
    public bool hasRoughwork;
    public int roughIndex;
    public delegate void OpenRoughWorkAction(int index);
    public static event OpenRoughWorkAction OnOpenRoughWorkAction;

    public void clickedOnRoughWork()
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
