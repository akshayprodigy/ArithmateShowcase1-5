using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slider_number_line : MonoBehaviour
{
    public Slider slider1, slider2;
   
    public obj_12_improper_and_mixed Obj_12_Improper_And_Mixed;
    int slider_Count;
    private void Start()
    {
        slider_Count = 0;
        Obj_12_Improper_And_Mixed = FindObjectOfType<obj_12_improper_and_mixed>();
    }
    public void changed_value_slider_1()
    {
        slider_Count++;
        if (slider_Count > 3)
        {
            Obj_12_Improper_And_Mixed.engagementsubmit.gameObject.SetActive(true);
        }
        reset_lines();
        Obj_12_Improper_And_Mixed.line_1_engagement.transform.GetChild((int)slider1.value).GetChild(0).gameObject.SetActive(true);
        Obj_12_Improper_And_Mixed.line_1_engagement.transform.GetChild((int)slider1.value).GetChild(1).gameObject.SetActive(true);

        Obj_12_Improper_And_Mixed.line_2_engagement.transform.GetChild((int)slider1.value).GetChild(0).gameObject.SetActive(true);
        Obj_12_Improper_And_Mixed.line_2_engagement.transform.GetChild((int)slider1.value).GetChild(1).gameObject.SetActive(true);
        slider2.value = slider1.value;

    }
    public void changed_value_slider_2()
    {
        slider_Count++;
        if (slider_Count > 3)
        {
            Obj_12_Improper_And_Mixed.engagementsubmit.gameObject.SetActive(true);
        }
        reset_lines();
        Obj_12_Improper_And_Mixed.line_1_engagement.transform.GetChild((int)slider2.value).GetChild(0).gameObject.SetActive(true);
        Obj_12_Improper_And_Mixed.line_1_engagement.transform.GetChild((int)slider2.value).GetChild(1).gameObject.SetActive(true);

        Obj_12_Improper_And_Mixed.line_2_engagement.transform.GetChild((int)slider2.value).GetChild(0).gameObject.SetActive(true);
        Obj_12_Improper_And_Mixed.line_2_engagement.transform.GetChild((int)slider2.value).GetChild(1).gameObject.SetActive(true);
        slider1.value = slider2.value;
    }

    void reset_lines()
    {
        for (int i = 0; i < 16; i++)
        {
            Obj_12_Improper_And_Mixed.line_1_engagement.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
            Obj_12_Improper_And_Mixed.line_1_engagement.transform.GetChild(i).GetChild(1).gameObject.SetActive(false);

            Obj_12_Improper_And_Mixed.line_2_engagement.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
            Obj_12_Improper_And_Mixed.line_2_engagement.transform.GetChild(i).GetChild(1).gameObject.SetActive(false);

        }
    }
}
