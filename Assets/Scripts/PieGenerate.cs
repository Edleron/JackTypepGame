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
        MyList.Add(new WheelCore("Test", -100, WheelCore.WinTypes.Lose, Color.white, "-100"));
    }
    public void Remove(int index)
    {
        MyList.RemoveAt(index);
    }

    public void JusGenerate()
    {
        float filAmoundCalculated = (float)1 / MyList.Count;
        float angleCalculated = 360 / MyList.Count;
        for (int i = 1; i <= MyList.Count; i++)
        {
            GameObject clone = Instantiate(PieDef, targetParents) as GameObject;
            clone.GetComponent<Image>().fillAmount = filAmoundCalculated;
            clone.GetComponent<Image>().color = MyList[i - 1].AnColor;
            clone.GetComponent<RectTransform>().transform.Rotate(new Vector3(0, 0, (i * angleCalculated) - 30));
            clone.name = MyList[i - 1].AnName;
            clone.transform.GetChild(0).GetComponent<Text>().text = MyList[i - 1].AnText;
        }
    }

    public ResultCore GetResult()
    {
        //Objenin Ref'i heap'de hep kalıcak Duzelt.
        float listCount = (float)MyList.Count;
        float angleCalculated = 360 / listCount;

        List<float> angleList = new List<float>();
        //int unCertainty = UnityEngine.Random.Range(60, 90);
        for (int i = 1; i <= listCount; i++)
        {
            angleList.Add((i * angleCalculated));
            Debug.LogError(angleList[i - 1]);
        }
        int randomNumber = UnityEngine.Random.Range(-1, angleList.Count - 1);
        Debug.LogError(randomNumber);
        float FinalAngle = angleList[randomNumber];
        ResultCore result = new ResultCore();
        result.AtaFinalAngle = FinalAngle;
        result.AtaGain = MyList[randomNumber].AnGain;
        result.AtaStatus = (MyList[randomNumber].AnWinType == WheelCore.WinTypes.Won ? true : false);
        return result;
    }
    #endregion
}
