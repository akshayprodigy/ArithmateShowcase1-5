using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleManager 
{
    public static int CollectedFullRedApple, CollectedFullGreenApple, CollectedFullYellowApple, CollectedHalfRedApple, CollectedHalfGreenApple, CollectedHalfYellowApple, CollectedQuarterRedApple, CollectedQuarterGreenApple, CollectedQuarterYellowApple, CollectedThirdRedApple, CollectedThirdGreenApple, CollectedThirdYellowApple;
    public static int totalAppleCollected, totalWholeAppleCollected, totalFracAppleCollected;
    public static int filledWholeSlot, filledFracSlots,totalslots;
    public static int falledApple;
    public static List<string> collectedAppleName = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        
        //Initialization();
    }
   public static void Initialization()
    {
        totalslots = collectedAppleName.Count;
        //testingData();
        total();
        


    }
    public static void DebugValue()
    {
        Debug.Log("total = " + collectedAppleName.Count);
        Debug.Log("Full Red = " + CollectedFullRedApple +
              "\nHalf Red = " + CollectedHalfRedApple +
               "\nTwo Third Red = " + CollectedThirdRedApple +
                "\nOne Fourth Red = " + CollectedQuarterRedApple +
                 "\nFull Green = " + CollectedFullGreenApple +
                  "\nHalf Green = " + CollectedHalfGreenApple +
                   "\nTwo Third Green = " + CollectedThirdGreenApple +
                    "\nOne Fourth Green = " + CollectedQuarterGreenApple +
                 "\nFull Yellow = " + CollectedFullYellowApple +
                  "\nHalf Yellow = " + CollectedHalfYellowApple +
                   "\nTwo Third Yellow = " + CollectedThirdYellowApple +
                    "\nOne Fourth Yellow = " + CollectedQuarterYellowApple);
    }
    public static void total()
    {
        totalAppleCollected = CollectedFullRedApple + CollectedFullGreenApple + CollectedFullYellowApple + CollectedHalfRedApple + CollectedHalfGreenApple + CollectedHalfYellowApple + CollectedQuarterRedApple + CollectedQuarterGreenApple + CollectedQuarterYellowApple + CollectedThirdRedApple + CollectedThirdGreenApple + CollectedThirdYellowApple;
        Debug.Log("Total = " + totalAppleCollected);
    }
    public static void totalWholeApple()
    {

        totalWholeAppleCollected = CollectedFullRedApple + CollectedFullGreenApple + CollectedFullYellowApple;

        Debug.Log("Total = " + totalWholeAppleCollected);
    }
    //void testingData()
    //{
    //    CollectedFullRedApple = 3;
    //    CollectedFullGreenApple = 2;
    //    CollectedFullYellowApple = 3;
    //    CollectedHalfRedApple = 2;
    //    CollectedHalfGreenApple = 0;
    //    CollectedHalfYellowApple = 0;
    //    CollectedQuarterRedApple = 1;
    //    CollectedQuarterGreenApple = 1;
    //    CollectedQuarterYellowApple = 0;
    //    CollectedThirdRedApple = 1;
    //    CollectedThirdGreenApple = 1;
    //    CollectedThirdYellowApple = 0;
    //}
    public static void setGameData()
    {
        resetData();
        CollectedFullRedApple = 3;
        CollectedFullGreenApple = 2;
        CollectedFullYellowApple = 3;
        CollectedHalfRedApple = 2;
        CollectedHalfGreenApple = 0;
        CollectedHalfYellowApple = 0;
        CollectedQuarterRedApple = 1;
        CollectedQuarterGreenApple = 1;
        CollectedQuarterYellowApple = 0;
        CollectedThirdRedApple = 1;
        CollectedThirdGreenApple = 1;
        CollectedThirdYellowApple = 0;
        total();
    }
    public static void resetData()
    {
        CollectedFullRedApple = 0;
        CollectedFullGreenApple = 0;
        CollectedFullYellowApple = 0;
        CollectedHalfRedApple = 0;
        CollectedHalfGreenApple = 0;
        CollectedHalfYellowApple = 0;
        CollectedQuarterRedApple = 0;
        CollectedQuarterGreenApple = 0;
        CollectedQuarterYellowApple = 0;
        CollectedThirdRedApple = 0;
        CollectedThirdGreenApple = 0;
        CollectedThirdYellowApple = 0;
        totalAppleCollected = 0;

        totalWholeAppleCollected = 0;
        totalFracAppleCollected = 0;
        filledWholeSlot = 0;
        filledFracSlots = 0;
        totalslots = 0;
    }
}
