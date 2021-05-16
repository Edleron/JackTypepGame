using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR
[CustomEditor(typeof(PieGenerate))]
public class GenerateWhellEditor : Editor
{
    enum displayFieldType { DisplayAsAutomaticFields }
    displayFieldType DisplayFieldType;

    PieGenerate t;
    SerializedObject GetTarget;
    SerializedProperty ThisList;
    int ListSize;
    bool shows = false;

    void OnEnable()
    {
        t = (PieGenerate)target;
        GetTarget = new SerializedObject(t);
        ThisList = GetTarget.FindProperty("MyList"); // Find the List in our script and create a refrence of it
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        PieGenerate pieGenerate = (PieGenerate)target;

        //DrawWheelLevel(pieGenerate);

        //CreatedItemsSystem();

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        CreatedLevelSystem(pieGenerate);

        DrawWheelLevel(pieGenerate);
    }


    private void CreatedItemsSystem()
    {
        GetTarget.Update();

        //Choose how to display the list<> Example purposes only
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        DisplayFieldType = (displayFieldType)EditorGUILayout.EnumPopup("", DisplayFieldType);

        //Resize our list
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Define the list size with a number");
        //ListSize = ThisList.arraySize;
        //ListSize = EditorGUILayout.IntField("List Size", ListSize);

        if (ListSize != ThisList.arraySize)
        {
            while (ListSize > ThisList.arraySize)
            {
                ThisList.InsertArrayElementAtIndex(ThisList.arraySize);
            }
            while (ListSize < ThisList.arraySize)
            {
                ThisList.DeleteArrayElementAtIndex(ThisList.arraySize - 1);
            }
        }

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Or");
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        //Or add a new item to the List<> with a button
        EditorGUILayout.LabelField("Add a new item with a button");

        if (GUILayout.Button("Add New"))
        {
            t.MyList.Add(new PieGenerate.WheelCore("Wheel One", 100, PieGenerate.WheelCore.WinTypes.Won, Color.white, "100"));
        }

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        //Display our list to the inspector window

        for (int i = 0; i < ThisList.arraySize; i++)
        {
            SerializedProperty MyListRef = ThisList.GetArrayElementAtIndex(i);
            SerializedProperty MyName = MyListRef.FindPropertyRelative("AnName");
            SerializedProperty MyGain = MyListRef.FindPropertyRelative("AnGain");
            SerializedProperty MyWinType = MyListRef.FindPropertyRelative("AnWinType");
            SerializedProperty MyColor = MyListRef.FindPropertyRelative("AnColor");
            SerializedProperty MyText = MyListRef.FindPropertyRelative("AnText");


            // Display the property fields in two ways.

            if (DisplayFieldType == 0)
            {// Choose to display automatic or custom field types. This is only for example to help display automatic and custom fields.
             //1. Automatic, No customization <-- Choose me I'm automatic and easy to setup
                EditorGUILayout.LabelField("Automatic Field By Property Type");
                EditorGUILayout.PropertyField(MyName);
                EditorGUILayout.PropertyField(MyGain);
                EditorGUILayout.PropertyField(MyWinType);
                EditorGUILayout.PropertyField(MyColor);
                EditorGUILayout.PropertyField(MyText);

                // Array fields with remove at index
                EditorGUILayout.Space();
                EditorGUILayout.Space();
            }
            else
            {
                EditorGUILayout.LabelField("Customizable Field With GUI");
                MyName.stringValue = EditorGUILayout.TextField("Name == ", MyName.stringValue);
                MyGain.intValue = EditorGUILayout.IntField("Gain Int", MyGain.intValue);
                MyWinType.stringValue = EditorGUILayout.TextField("Win Types == ", MyWinType.stringValue);
                MyColor.colorValue = EditorGUILayout.ColorField("Text == ", MyColor.colorValue);
                MyText.stringValue = EditorGUILayout.TextField("Text == ", MyText.stringValue);
                EditorGUILayout.Space();
            }

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Remove an index from the List<> with a button");
            if (GUILayout.Button("Remove This Index (" + i.ToString() + ")"))
            {
                ThisList.DeleteArrayElementAtIndex(i);
            }
            EditorGUILayout.Space();
            EditorGUILayout.Space();
        }

        GetTarget.ApplyModifiedProperties();
    }

   

    private void DrawWheelLevel(PieGenerate pieGenerate)
    {
        if (pieGenerate.whellType == PieGenerate.WhellTypes.WheelOne)
        {
            EditorGUILayout.Space();
            shows = EditorGUILayout.Foldout(shows, "Backgrounds", true);

            if (shows)
            {
                EditorGUI.indentLevel++;
                Debug.LogError("Lutfen Six yada Twelve Seçiniz");
                EditorGUI.indentLevel--;
            }
        }
        else if (pieGenerate.whellType == PieGenerate.WhellTypes.WheelSix)
        {
            EditorGUILayout.Space();
            shows = EditorGUILayout.Foldout(shows, "Backgrounds", true);

            if (shows)
            {
                EditorGUI.indentLevel++;
                ListSize = 6;
                CreatedItemsSystem();
                EditorGUI.indentLevel--;
            }
        }
        else if (pieGenerate.whellType == PieGenerate.WhellTypes.WheelTwelve)
        {
            EditorGUILayout.Space();
            shows = EditorGUILayout.Foldout(shows, "Backgrounds", true);

            if (shows)
            {
                EditorGUI.indentLevel++;
                ListSize = 12;
                CreatedItemsSystem();
                EditorGUI.indentLevel--;
            }
        }

    }

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

