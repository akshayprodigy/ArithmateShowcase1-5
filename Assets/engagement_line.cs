using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class engagement_line : MonoBehaviour
{
    public obj_12_improper_and_mixed obj_12_Improper_And_Mixed;

    // Start is called before the first frame update
    void Start()
    {
        obj_12_Improper_And_Mixed = FindObjectOfType<obj_12_improper_and_mixed>();
        Debug.Log("Sibling Index : " + transform.GetSiblingIndex());
    }

    void enable_lines()
    {
        obj_12_Improper_And_Mixed.line_1_engagement.transform.GetChild(transform.GetSiblingIndex()).GetChild(0).gameObject.SetActive(true);
        obj_12_Improper_And_Mixed.line_1_engagement.transform.GetChild(transform.GetSiblingIndex()).GetChild(1).gameObject.SetActive(true);
        obj_12_Improper_And_Mixed.line_2_engagement.transform.GetChild(transform.GetSiblingIndex()).GetChild(0).gameObject.SetActive(true);
        obj_12_Improper_And_Mixed.line_2_engagement.transform.GetChild(transform.GetSiblingIndex()).GetChild(1).gameObject.SetActive(true);


    }
    void disable_lines()
    {
        obj_12_Improper_And_Mixed.line_1_engagement.transform.GetChild(transform.GetSiblingIndex()).GetChild(0).gameObject.SetActive(false);
        obj_12_Improper_And_Mixed.line_1_engagement.transform.GetChild(transform.GetSiblingIndex()).GetChild(1).gameObject.SetActive(false);
        obj_12_Improper_And_Mixed.line_2_engagement.transform.GetChild(transform.GetSiblingIndex()).GetChild(0).gameObject.SetActive(false);
        obj_12_Improper_And_Mixed.line_2_engagement.transform.GetChild(transform.GetSiblingIndex()).GetChild(1).gameObject.SetActive(false);
    }

    public void OnMouseEnter()
    {
        enable_lines();
        obj_12_Improper_And_Mixed.pointer_count++;
        obj_12_Improper_And_Mixed.line_1_engagement.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        obj_12_Improper_And_Mixed.line_2_engagement.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        if (obj_12_Improper_And_Mixed.pointer_count > 2)
        {

            obj_12_Improper_And_Mixed.engagementsubmit.gameObject.SetActive(true);
        }
    }
    public void OnMouseExit()
    {
        disable_lines();
    }
}