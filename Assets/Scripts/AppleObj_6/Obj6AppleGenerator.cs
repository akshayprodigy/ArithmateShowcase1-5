using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obj6AppleGenerator : MonoBehaviour
{
    //public AppleManager AppleManager;

    private void Awake()
    {
        //Initialize();
    }
    public void Initialize()
    {

        setCollectedAppleForTest();
        AppleManager.DebugValue();
        GenerateApple();
    }
    void setCollectedAppleForTest()
    {
        AppleManager.CollectedFullRedApple = 3;
        AppleManager.CollectedFullGreenApple = 1;
        AppleManager.CollectedFullYellowApple = 1;
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
        for (int i = 0; i < AppleManager.CollectedFullRedApple; i++)
        {
            GameObject InitiatedApplel = Instantiate(Resources.Load("Prefabs/AppleObj5/ApplesToBeInitiated/FullAppleRed")) as GameObject;
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
            GameObject InitiatedApplel1 = Instantiate(Resources.Load("Prefabs/AppleObj5/ApplesToBeInitiated/FullAppleGreen")) as GameObject;
            InitiatedApplel1.transform.parent = GameObject.Find("Obj3NumDenumTrey").transform.GetChild(AppleManager.CollectedFullRedApple + i).transform;
           
            InitiatedApplel1.transform.localPosition = new Vector3(0, 0, 0);
            InitiatedApplel1.transform.localScale = new Vector3(1, 1, 1);
            Debug.Log("Full Green");
        }
    }
    void GenerateFullAppleYellow()
    {
        for (int i = 0; i < AppleManager.CollectedFullYellowApple; i++)
        {
            GameObject InitiatedApplel2 = Instantiate(Resources.Load("Prefabs/AppleObj5/ApplesToBeInitiated/FullAppleYellow")) as GameObject;
            InitiatedApplel2.transform.parent = GameObject.Find("Obj3NumDenumTrey").transform.GetChild(AppleManager.CollectedFullRedApple + AppleManager.CollectedFullGreenApple + i).transform;
           
            InitiatedApplel2.transform.localPosition = new Vector3(0, 0, 0);
            InitiatedApplel2.transform.localScale = new Vector3(1, 1, 1);
            Debug.Log("Full Yellow");
        }
    }

}
