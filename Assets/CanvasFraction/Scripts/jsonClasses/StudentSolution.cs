using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StudentSolution {

    // Use this for initialization
    public string step_name;
    public List<StepDisplay> Display;
    public Attributes_step Attribute;


    public StudentSolution()
    {
        Display = new List<StepDisplay>();
        Attribute = new Attributes_step();
    }
}

