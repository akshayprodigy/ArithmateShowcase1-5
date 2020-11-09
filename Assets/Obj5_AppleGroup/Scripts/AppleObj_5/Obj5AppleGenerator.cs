using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obj5AppleGenerator : MonoBehaviour
{
    public AppleManager AppleManager;

    private void Awake()
    {
        Initialize();
    }
    public void Initialize()
    {
        //AppleManager = GameObject.FindObjectOfType<AppleManager>();
        setCollectedAppleForTest();
        AppleManager.DebugValue();
        GenerateApple();
    }
    void setCollectedAppleForTest()
    {
        AppleManager.CollectedFullRedApple = 2;
        AppleManager.CollectedFullGreenApple = 3;
        AppleManager.CollectedFullYellowApple = 3;
        AppleManager.totalWholeApple(); 
    }
   void GenerateApple()
    {
        GenerateFullAppleRed();
        GenerateFullAppleGreen();
        GenerateFullAppleYellow();
    }
    void GenerateFullAppleRed()
    {
        for(int i = 0;i< AppleManager.CollectedFullRedApple;i++)
        {
            GameObject InitiatedApplel = Instantiate(Resources.Load("Prefabs/AppleObj5/ApplesToBeInitiated/FullAppleRed")) as GameObject;
           InitiatedApplel.transform.parent = GameObject.Find("Obj3NumDenumTrey").transform.GetChild(i).transform;


            GameObject.Find("Obj3NumDenumTrey").transform.GetChild(i).GetChild(0).gameObject.SetActive(false); 

            InitiatedApplel.transform.localPosition = new Vector3(0, 0, 0);
            InitiatedApplel.transform.localScale = new Vector3(1, 1, 1);
            Debug.Log("Full Red");
        }
    }
    void GenerateFullAppleGreen()
    {
        for (int i = 0; i < AppleManager.CollectedFullGreenApple; i++)
        {
            GameObject InitiatedApplel = Instantiate(Resources.Load("Prefabs/AppleObj5/ApplesToBeInitiated/FullAppleGreen")) as GameObject;
            InitiatedApplel.transform.parent = GameObject.Find("Obj3NumDenumTrey").transform.GetChild(AppleManager.CollectedFullRedApple +i).transform;
            InitiatedApplel.transform.localPosition = new Vector3(0, 0, 0);
            InitiatedApplel.transform.localScale = new Vector3(1, 1, 1);
            GameObject.Find("Obj3NumDenumTrey").transform.GetChild(AppleManager.CollectedFullRedApple + i).GetChild(0).gameObject.SetActive(false);

            Debug.Log("Full Green");
        }
    }
    void GenerateFullAppleYellow()
    {
        for (int i = 0; i < AppleManager.CollectedFullYellowApple; i++)
        {
            GameObject InitiatedApplel = Instantiate(Resources.Load("Prefabs/AppleObj5/ApplesToBeInitiated/FullAppleYellow")) as GameObject;
            InitiatedApplel.transform.parent = GameObject.Find("Obj3NumDenumTrey").transform.GetChild(AppleManager.CollectedFullRedApple + AppleManager.CollectedFullGreenApple + i).transform;
            InitiatedApplel.transform.localPosition = new Vector3(0, 0, 0);
            InitiatedApplel.transform.localScale = new Vector3(1, 1, 1);
            GameObject.Find("Obj3NumDenumTrey").transform.GetChild(AppleManager.CollectedFullRedApple + AppleManager.CollectedFullGreenApple + i).GetChild(0).gameObject.SetActive(false);
            Debug.Log("Full Yellow");
        }
    }
   
}
