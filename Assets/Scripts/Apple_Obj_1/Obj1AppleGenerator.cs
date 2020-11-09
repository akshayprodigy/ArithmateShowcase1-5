using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obj1AppleGenerator : MonoBehaviour
{
    //public AppleManager appleManager;
    public GameObject initiateObjectParent;
    private void Awake()
    {
        //Initialize();
    }
    public void Initialize()
    {
        initiateObjectParent = GameObject.Find("initiateObjectParent");
        AppleManager.total();
        //AppleManager.totalAppleCollected = 8;
        //appleManager = GameObject.FindObjectOfType<AppleManager>();

        AppleManager.DebugValue();
        GenerateApple();
    }
    void GenerateApple()
    {

        GenerateAllAppls();
        //GenerateFullAppleRed();
        // GenerateHalfAppleRed();
        // GenerateThirdAppleRed();
        // GenerateFourthAppleRed();
        // GenerateFullAppleGreen();
        // GenerateHalfAppleGreen();
        //GenerateThirdAppleGreen();
        // GenerateFourthAppleGreen();
        // GenerateFullAppleYellow();
        // GenerateHalfAppleYellow();
        //GenerateThirdAppleYellow();
        //GenerateFourthAppleYellow();
    }
    void GenerateAllAppls()
    {
        for (int i = 0; i < AppleManager.collectedAppleName.Count; i++)
        {
            Debug.Log("Name = " + AppleManager.collectedAppleName[i]);
            GameObject InitiatedApplel = Instantiate(Resources.Load("Prefabs/AppleObj1/ApplesToBeInitiated/" + AppleManager.collectedAppleName[i])) as GameObject;
            InitiatedApplel.transform.parent = initiateObjectParent.transform;
            InitiatedApplel.transform.localScale = new Vector3(1, 1, 1);
            Debug.Log("Full Red");
        }
    }
    //void GenerateFullAppleRed()
    //{
    //    for (int i = 0; i < 5; i++)
    //    {
    //        GameObject InitiatedApplel = Instantiate(Resources.Load("Prefabs/AppleObj1/ApplesToBeInitiated/FullAppleRed")) as GameObject;
    //        InitiatedApplel.transform.parent = initiateObjectParent.transform;
    //        Debug.Log("Full Red");
    //    }
    //}
    //void GenerateFullAppleGreen()
    //{
    //    for (int i = 0; i < AppleManager.CollectedFullGreenApple; i++)
    //    {
    //        GameObject InitiatedApplel = Instantiate(Resources.Load("Prefabs/AppleObj1/ApplesToBeInitiated/FullAppleGreen")) as GameObject;
    //        InitiatedApplel.transform.parent = initiateObjectParent.transform;
    //        Debug.Log("Full Green");
    //    }
    //}
    //void GenerateFullAppleYellow()
    //{
    //    for (int i = 0; i < AppleManager.CollectedFullYellowApple; i++)
    //    {
    //        GameObject InitiatedApplel = Instantiate(Resources.Load("Prefabs/AppleObj1/ApplesToBeInitiated/FullAppleYellow")) as GameObject;
    //        InitiatedApplel.transform.parent = initiateObjectParent.transform;
    //        Debug.Log("Full Yellow");
    //    }
    //}
    //void GenerateHalfAppleRed()
    //{
    //    for (int i = 0; i < AppleManager.CollectedHalfRedApple; i++)
    //    {
    //        GameObject InitiatedApplel = Instantiate(Resources.Load("Prefabs/AppleObj1/ApplesToBeInitiated/HalfAppleRed")) as GameObject;
    //        InitiatedApplel.transform.parent = initiateObjectParent.transform;
    //        Debug.Log("Half Red");
    //    }
    //}
    //void GenerateHalfAppleGreen()
    //{
    //    for (int i = 0; i < AppleManager.CollectedHalfGreenApple; i++)
    //    {
    //        GameObject InitiatedApplel = Instantiate(Resources.Load("Prefabs/AppleObj1/ApplesToBeInitiated/HalfAppleGreen")) as GameObject;
    //        InitiatedApplel.transform.parent = initiateObjectParent.transform;
    //        Debug.Log("Half Green");
    //    }
    //}
    //void GenerateHalfAppleYellow()
    //{
    //    for (int i = 0; i < AppleManager.CollectedHalfYellowApple; i++)
    //    {
    //        GameObject InitiatedApplel = Instantiate(Resources.Load("Prefabs/AppleObj1/ApplesToBeInitiated/HalfAppleYellow")) as GameObject;
    //        InitiatedApplel.transform.parent = initiateObjectParent.transform;
    //        Debug.Log("Half yellow");
    //    }
    //}
    //void GenerateThirdAppleRed()
    //{
    //    for (int i = 0; i < AppleManager.CollectedThirdRedApple; i++)
    //    {
    //        GameObject InitiatedApplel = Instantiate(Resources.Load("Prefabs/AppleObj1/ApplesToBeInitiated/ThirdAppleRed")) as GameObject;
    //        InitiatedApplel.transform.parent = initiateObjectParent.transform;
    //        Debug.Log("Third red");
    //    }
    //}
    //void GenerateThirdAppleGreen()
    //{
    //    for (int i = 0; i < 3; i++)
    //    {
    //        GameObject InitiatedApplel = Instantiate(Resources.Load("Prefabs/AppleObj1/ApplesToBeInitiated/ThirdAppleGreen")) as GameObject;
    //        InitiatedApplel.transform.parent = initiateObjectParent.transform;
    //        Debug.Log("Third Green");
    //    }
    //}
    //void GenerateThirdAppleYellow()
    //{
    //    for (int i = 0; i < AppleManager.CollectedThirdYellowApple; i++)
    //    {
    //        GameObject InitiatedApplel = Instantiate(Resources.Load("Prefabs/AppleObj1/ApplesToBeInitiated/ThirdAppleYellow")) as GameObject;
    //        InitiatedApplel.transform.parent = initiateObjectParent.transform;
    //        Debug.Log("Third Yellow");
    //    }
    //}
    //void GenerateFourthAppleRed()
    //{

    //    for (int i = 0; i < AppleManager.CollectedQuarterRedApple; i++)
    //    {
    //        GameObject InitiatedApplel = Instantiate(Resources.Load("Prefabs/AppleObj1/ApplesToBeInitiated/FourthAppleRed")) as GameObject;
    //        InitiatedApplel.transform.parent = initiateObjectParent.transform;
    //        Debug.Log("Fourth Red");
    //    }
    //}
    //void GenerateFourthAppleGreen()
    //{
    //    for (int i = 0; i < AppleManager.CollectedQuarterGreenApple; i++)
    //    {
    //        GameObject InitiatedApplel = Instantiate(Resources.Load("Prefabs/AppleObj1/ApplesToBeInitiated/FourthAppleGreen")) as GameObject;
    //        InitiatedApplel.transform.parent = initiateObjectParent.transform;
    //        Debug.Log("Fourth Green");
    //    }
    //}
    //void GenerateFourthAppleYellow()
    //{
    //    for (int i = 0; i < AppleManager.CollectedQuarterYellowApple; i++)
    //    {
    //        GameObject InitiatedApplel = Instantiate(Resources.Load("Prefabs/AppleObj1/ApplesToBeInitiated/FourthAppleYellow")) as GameObject;
    //        InitiatedApplel.transform.parent = initiateObjectParent.transform;
    //        Debug.Log("Fourth Yellow");
    //    }
    //}
}
