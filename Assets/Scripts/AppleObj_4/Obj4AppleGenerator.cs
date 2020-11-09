using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obj4AppleGenerator : MonoBehaviour
{
   
    public GameObject initiateObjectParent;
    private void Awake()
    {
        Initialize();
    }
    public void Initialize()
    {
        initiateObjectParent = GameObject.Find("initiateObjectParent");
        setCollectedAppleForTest();
        AppleManager.DebugValue();
        GenerateApple();
    }
    void setCollectedAppleForTest()
    {
        AppleManager.CollectedFullRedApple = 3;
        AppleManager.CollectedFullGreenApple = 1;
        AppleManager.CollectedFullYellowApple = 2;
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
            GameObject InitiatedApplel = Instantiate(Resources.Load("Prefabs/AppleObj3/ApplesToBeInitiated/FullAppleRed")) as GameObject;
           InitiatedApplel.transform.parent = GameObject.Find("Obj3NumDenumTrey").transform.GetChild(i).transform;
            InitiatedApplel.transform.localPosition = new Vector3(0, 0, 0);
            InitiatedApplel.transform.localScale = new Vector3(1, 1, 1);
            Debug.Log("Full Red");
        }
    }
    void GenerateFullAppleGreen()
    {
        for (int i = 0; i < AppleManager.CollectedFullGreenApple; i++)
        {
            GameObject InitiatedApplel = Instantiate(Resources.Load("Prefabs/AppleObj3/ApplesToBeInitiated/FullAppleGreen")) as GameObject;
            InitiatedApplel.transform.parent = GameObject.Find("Obj3NumDenumTrey").transform.GetChild(AppleManager.CollectedFullRedApple +i).transform;
            InitiatedApplel.transform.localPosition = new Vector3(0, 0, 0);
            InitiatedApplel.transform.localScale = new Vector3(1, 1, 1);
            Debug.Log("Full Green");
        }
    }
    void GenerateFullAppleYellow()
    {
        for (int i = 0; i < AppleManager.CollectedFullYellowApple; i++)
        {
            GameObject InitiatedApplel = Instantiate(Resources.Load("Prefabs/AppleObj3/ApplesToBeInitiated/FullAppleYellow")) as GameObject;
            InitiatedApplel.transform.parent = GameObject.Find("Obj3NumDenumTrey").transform.GetChild(AppleManager.CollectedFullRedApple + AppleManager.CollectedFullGreenApple + i).transform;
            InitiatedApplel.transform.localPosition = new Vector3(0, 0, 0);
            InitiatedApplel.transform.localScale = new Vector3(1, 1, 1);
            Debug.Log("Full Yellow");
        }
    }
   
}
