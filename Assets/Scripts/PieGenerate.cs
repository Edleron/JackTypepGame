using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PieGenerate : MonoBehaviour
{
    public GameObject PieDef;
    public RectTransform targetParents;

    enum WhellTypes { LvlOne, LvlTwo, LvlThree, LvlFour, LvlFive, LvlSix, LvlEight, LvlNine, LvlTen, LvlTwelfe };
    [Header("Presents")]
    [SerializeField] WhellTypes whellType;

    List<GameObject> backgrounds = new List<GameObject>();
    bool showBackgrounds = false;

    #region Deletings
    string itemName;
    int cost;
    float happies;
    #endregion

    #region Helper Metods
    public void JusGenerate()
    {
        Instantiate(PieDef, targetParents);
    }
    #endregion

    #region Unity Editor

#if UNITY_EDITOR
    [CustomEditor(typeof(PieGenerate))]
    public class GenerateWhellEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            PieGenerate pieGenerate = (PieGenerate)target;
            //DrawDetails(pieGenerate);
            DrawWheelLevel(pieGenerate);
            CreatedLevelSystem(pieGenerate);
        }

        private static void DrawWheelLevel(PieGenerate pieGenerate)
        {
            if (pieGenerate.whellType == WhellTypes.LvlOne)
            {
                EditorGUILayout.Space();
                pieGenerate.showBackgrounds = EditorGUILayout.Foldout(pieGenerate.showBackgrounds, "Backgrounds", true);

                if (pieGenerate.showBackgrounds)
                {
                    EditorGUI.indentLevel++;

                    List<GameObject> list = pieGenerate.backgrounds;
                    int size = Mathf.Max(0, EditorGUILayout.IntField("Size", list.Count));

                    while (size > list.Count)
                    {
                        list.Add(null);
                    }

                    while (size < list.Count)
                    {
                        list.RemoveAt(list.Count - 1);
                    }

                    for (int i = 0; i < list.Count; i++)
                    {
                        list[i] = EditorGUILayout.ObjectField("Elements " + i, list[i], typeof(GameObject), true) as GameObject;
                    }

                    EditorGUI.indentLevel--;
                }
            }
        }

        /*
        private static void DrawDetails(PieGenerate pieGenerate)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Details");
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("Name", GUILayout.MaxWidth(50));
            pieGenerate.itemName = EditorGUILayout.TextField(pieGenerate.itemName);

            EditorGUILayout.LabelField("Cost", GUILayout.MaxWidth(50));
            pieGenerate.cost = EditorGUILayout.IntField(pieGenerate.cost);

            EditorGUILayout.LabelField("Happies", GUILayout.MaxWidth(75));
            pieGenerate.happies = EditorGUILayout.FloatField(pieGenerate.happies);

            EditorGUILayout.EndHorizontal();           
        }
        */

        private static void CreatedLevelSystem(PieGenerate pieGenerate)
        {
            EditorGUILayout.Space();
            if (GUILayout.Button("Generate Circle Draw"))
            {
                pieGenerate.JusGenerate();
            }
        }
    }
#endif
    #endregion
}
