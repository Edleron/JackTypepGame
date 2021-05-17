using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WheelCore : IComparable<WheelCore>
{
    public enum WinTypes { Won, Lose };

    public string AnName;
    public int AnGain;
    public WinTypes AnWinType;
    public Color AnColor;
    public string AnText;

    public WheelCore()
    {

    }

    public WheelCore(string newName, int newGain, WinTypes newWinType, Color newColor, string newText)
    {
        AnName = newName;
        AnGain = newGain;
        AnWinType = newWinType;
        AnColor = newColor;
        AnText = newText;
    }
    public int CompareTo(WheelCore other)
    {
        if (other == null)
        {
            return 1;
        }

        return AnGain - other.AnGain;
    }
}
