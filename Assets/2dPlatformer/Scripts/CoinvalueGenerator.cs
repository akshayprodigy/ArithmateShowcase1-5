using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinvalueGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public string value;
    int Num, Dem;
    public TextMeshPro textNum, textDem;
    void Start()
    {
       
    }

    public void setUPCoin(string _value)
    {
        value = _value;
        string[] values = value.Split('/');
        textDem.text = values[1];
        textNum.text = values[2];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
