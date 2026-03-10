using System.Collections;
using System.Collections.Generic;
using Resources.Scripts.GamePlay.Controller.Goods_Sorting;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(LevelSortingData))]
public class LevelSortingDataEditor : Editor
{
   private LevelSortingData level;

    private void OnEnable()
    {
        level = (LevelSortingData)target;
        ResizeArray();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUI.BeginChangeCheck();

        level.width = EditorGUILayout.IntField("Width", level.width);
        level.height = EditorGUILayout.IntField("Height", level.height);

        if (EditorGUI.EndChangeCheck())
        {
            ResizeArray();
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Box Grid", EditorStyles.boldLabel);

        DrawGrid();

        EditorGUILayout.Space();

        if (GUILayout.Button("Random Level"))
        {
            GenerateRandomLevel();
        }

        serializedObject.ApplyModifiedProperties();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(level);
        }
    }

    void ResizeArray()
    {
        int newSize = level.width * level.height;

        if (level.boxes == null)
        {
            level.boxes = new BoxDataSO[newSize];
            return;
        }

        if (level.boxes.Length != newSize)
        {
            BoxDataSO[] newArray = new BoxDataSO[newSize];

            for (int i = 0; i < Mathf.Min(level.boxes.Length, newSize); i++)
            {
                newArray[i] = level.boxes[i];
            }

            level.boxes = newArray;
        }
    }

    void DrawGrid()
    {
        for (int y = level.height - 1; y >= 0; y--)
        {
            EditorGUILayout.BeginHorizontal();

            for (int x = 0; x < level.width; x++)
            {
                int index = x + y * level.width;

                DrawBox(index);
            }

            EditorGUILayout.EndHorizontal();
        }
    }

    void DrawBox(int index)
    {
        BoxDataSO box = level.boxes[index];

        EditorGUILayout.BeginVertical(GUILayout.Width(90));

        level.boxes[index] = (BoxDataSO)EditorGUILayout.ObjectField(
            box,
            typeof(BoxDataSO),
            false,
            GUILayout.Width(80)
        );

        if (box != null)
        {
            DrawBoxPreview(box);
        }
        else
        {
            GUILayout.Space(40);
        }

        EditorGUILayout.EndVertical();
    }
    
    void DrawBoxPreview(BoxDataSO box)
    {
        for (int r = 2; r >= 0; r--)
        {
            EditorGUILayout.BeginHorizontal();

            for (int c = 0; c < 3; c++)
            {
                ItemData item = box.BoxData[r].RowData[c];

                if (item != null && item.sprite != null)
                {
                    GUILayout.Label(item.sprite.texture,
                        GUILayout.Width(20),
                        GUILayout.Height(20));
                }
                else
                {
                    GUILayout.Box("", GUILayout.Width(20), GUILayout.Height(20));
                }
            }

            EditorGUILayout.EndHorizontal();
        }
    }

    void GenerateRandomLevel()
    {
        string[] guids = AssetDatabase.FindAssets("t:BoxDataSO");

        if (guids.Length == 0)
        {
            Debug.LogWarning("No BoxDataSO found");
            return;
        }

        for (int i = 0; i < level.boxes.Length; i++)
        {
            int randomIndex = Random.Range(0, guids.Length);

            string path = AssetDatabase.GUIDToAssetPath(guids[randomIndex]);

            level.boxes[i] = AssetDatabase.LoadAssetAtPath<BoxDataSO>(path);
        }

        EditorUtility.SetDirty(level);
    }
}
