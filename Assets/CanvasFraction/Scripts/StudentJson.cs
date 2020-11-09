using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class StudentJson {

    // Use this for initialization
    public List<StudentSolution> studJson;

    public StudentJson()
    {
        studJson = new List<StudentSolution>();
    }
}
