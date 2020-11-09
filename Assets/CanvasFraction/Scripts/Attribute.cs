using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Attributes_step
{

    // Use this for initialization
    // counts all the numbers in the step
    public int NUM;
    // counts all the nmber with operator 
    public int NUMOP;
    // counts all the mixed fraction 
    public int MF;
    // counts all the Mixed fraction  with operator 
    public int MFOP;
    // counts all the fraction 
    public int F;
    // counts all the fraction with operator 
    public int FOP;
    // counts all the operator 
    public int OP;
    // counts all the operator 
    public int TOTOP;
    // counts all total attribute 
    public int TOTATT;

    public int TEXT;

    public Attributes_step()
    {
        NUM = 0;
        NUMOP = 0;
        MF = 0;
        MFOP = 0;
        F = 0;
        FOP = 0;
        OP = 0;
        TOTOP = 0;
        TOTATT = 0;
        TEXT = 0;
    }
    public Attributes_step(int _NUM, int _NUMOP,int _MF, int _MFOP,int _F,int _FOP,int _OP,int _TOTOP,int _TOTATT)
    {
        NUM = _NUM;
        NUMOP = _NUMOP;
        MF = _MF;
        MFOP = _MFOP;
        F = _F;
        FOP = _FOP;
        OP = _OP;
        TOTOP = _TOTOP;
        TOTATT = _TOTATT;
    }
}
