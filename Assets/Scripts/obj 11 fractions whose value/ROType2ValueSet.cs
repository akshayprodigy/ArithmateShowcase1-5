using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ROType2ValueSet : MonoBehaviour
{
    public Transform placeHolder1, placeHolder2, placeHolder3;
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }
   public void Initialize()
    {
        placeHolder1= this.transform.GetChild(0);
        placeHolder2=this.transform.GetChild(2);
        placeHolder3= this.transform.GetChild(3);
    }
    public void valueSet(string value1, string value2, string value3)
    {
        placeHolder1.GetComponent<TEXDraw>().text = value1;
        placeHolder2.GetComponent<TEXDraw>().text = value2;
        placeHolder3.GetComponent<TEXDraw>().text = value3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
