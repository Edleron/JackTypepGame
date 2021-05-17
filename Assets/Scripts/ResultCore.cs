using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultCore : IComparable<ResultCore>
{
    public float AtaFinalAngle;
    public int AtaGain;
    public bool AtaStatus;

    public ResultCore()
    {

    }
   
    public int CompareTo(ResultCore other)
    {
        if (other == null)
        {
            return 1;
        }

        return AtaGain - other.AtaGain;
    }
}

