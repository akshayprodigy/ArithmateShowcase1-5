using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDisableInfo : MonoBehaviour
{
   
    void OnEnable()
    {

        StartCoroutine("off");
       
    }

    IEnumerator off()
    {
        yield return new WaitForSeconds(10.0f);
        GameObject.FindObjectOfType<GameManager1>().disableInfoPanelBegining();
        
        
    }
   
}
