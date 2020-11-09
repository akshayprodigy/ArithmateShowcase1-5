using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class remain : MonoBehaviour
{ 
    void Start()
    {
        //PlayerPrefs.DeleteAll();
        DontDestroyOnLoad(this.gameObject);
    }


}
