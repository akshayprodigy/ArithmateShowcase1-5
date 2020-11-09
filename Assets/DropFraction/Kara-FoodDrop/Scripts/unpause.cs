using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unpause : MonoBehaviour
{
    void OnEnable()
    {
        Debug.Log("unpause 1");
        //GameObject.FindObjectOfType<GameManager>().remove();
        Invoke("start_act", 2.5f);
    }
    void Start()
    {
        if(this.gameObject.name== "Combo")
        {
            GameObject.FindObjectOfType<GameManager1>().speed = 0.03f;
            GameObject.FindObjectOfType<GameManager1>().generationSpeed = 6.0f;
        }
    }
    void start_act()
    {
        
        Debug.Log("unpause");
        GameObject.FindObjectOfType<GameManager1>().isPause = false;

    }
}
