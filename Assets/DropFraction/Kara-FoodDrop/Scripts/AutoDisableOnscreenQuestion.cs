using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDisableOnscreenQuestion : MonoBehaviour
{
   
    void OnEnable()
    {
        StartCoroutine("off1");
    }

  
    IEnumerator off1()
    {
        Debug.Log("changes");
        yield return new WaitForSeconds(10.0f);
        Debug.Log("changes1");
        GameObject.FindObjectOfType<GameManager1>().disableQuestionOnScreen();


    }
}
