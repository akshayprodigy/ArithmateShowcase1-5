using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scroll : MonoBehaviour
{
    public static scroll Instance;
    public GameObject ScrollViewDebug_Pnl;
    public GameObject ScrollViewDebug_Btn;

    //public Text debuggingContent_Text;
    public static string debuggingContent_String;
    public string temp_String;
    public static List<string> debuggingContent_TextList;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //debuggingContent_TextList = new List<string>();
        debuggingContent_String = "";
        OnClose_Btn();

    }

    private void OnEnable()
    {

        temp_String = debuggingContent_String;
        //debuggingContent_Text.text = temp_String;
    }


    public void OnScrollView_Btn()
    {
        ScrollViewDebug_Pnl.SetActive(true);
        ScrollViewDebug_Btn.SetActive(false);
        
    }

    public void OnClose_Btn()
    {
        ScrollViewDebug_Pnl.SetActive(false);
        ScrollViewDebug_Btn.SetActive(true);
        
    }

    public void AddToList(string btnName)
    {
        //debuggingContent_TextList.Add(btnName);

        //for (int i = 0; i < debuggingContent_TextList.Count; i++)
        //{
        //    debuggingContent_String = temp_String + debuggingContent_String + debuggingContent_TextList[i];
        //}

        //debuggingContent_Text.text = debuggingContent_String;

    }

}
