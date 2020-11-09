using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifferetOperators {

    // Use this for initialization

    public int openingBracket;
    public int closingBracket;
    public int denominatorOperator;
    public int mf;
    public int plusSigh;
    public int subSigh;
    public int multiplySigh;
    public int divideSigh;
    public int equalSigh;

    public bool Equals(DifferetOperators opt)
    {
        if (this.openingBracket == opt.openingBracket && this.closingBracket == opt.closingBracket && this.denominatorOperator == opt.denominatorOperator && this.mf == opt.mf &&
            this.plusSigh == opt.plusSigh && this.subSigh == opt.subSigh && this.multiplySigh == opt.multiplySigh && this.divideSigh == opt.divideSigh && this.equalSigh == opt.equalSigh)
        {
            return true;
        }
        else
            return false;
    }
}
