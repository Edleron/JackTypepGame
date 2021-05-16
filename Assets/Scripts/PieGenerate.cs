using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PieGenerate : MonoBehaviour
{
    public GameObject PieDef;
    public RectTransform targetParents;

    internal enum WhellTypes { WheelOne, WheelTwo, WheelThree, WheelFour, WheelFive, WheelSix, WheelEight, WheelNine, WheelTen, WheelTwelve };
    [Header("Presents")]
    [SerializeField] internal WhellTypes whellType;

    [HideInInspector]
    public List<WheelCore> MyList = new List<WheelCore>();

    #region Helper Metods
    public void AddNew()
    {
        //Add a new index position to the end of our list
        MyList.Add(new WheelCore("Harvey", -100, WheelCore.WinTypes.Lose, Color.white, "-100"));
    }
    public void Remove(int index)
    {
        //Remove an index position from our list at a point in our list array
        MyList.RemoveAt(index);
    }

    public void JusGenerate()
    {
        float filAmoundCalculated = (float)1 / MyList.Count;
        Debug.LogError(filAmoundCalculated);
        float angleCalculated = 360 / MyList.Count;
        for (int i = 0; i < MyList.Count; i++)
        {
            GameObject clone = Instantiate(PieDef, targetParents) as GameObject;
            clone.GetComponent<Image>().fillAmount = filAmoundCalculated;
            clone.GetComponent<Image>().color = MyList[i].AnColor;
            clone.GetComponent<RectTransform>().transform.Rotate(new Vector3(0, 0, i * angleCalculated));
            clone.transform.GetChild(0).GetComponent<Text>().text = MyList[i].AnText;
        }
    }
    #endregion

    #region Wheel
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

        //This method is required by the IComparable
        //interface. 
        public int CompareTo(WheelCore other)
        {
            if (other == null)
            {
                return 1;
            }

            //Return the difference in power.
            return AnGain - other.AnGain;
        }
    }
    #endregion
}
