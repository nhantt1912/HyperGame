using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BoxDataSO))]
public class BoxDataSOEditor : Editor
{
    private BoxDataSO box;

    private void OnEnable()
    {
        box = (BoxDataSO)target;

        if (box.BoxData == null || box.BoxData.Length != 3)
        {
            box.BoxData = new BoxData[3];
        }

        for (int r = 0; r < 3; r++)
        {
            if (box.BoxData[r] == null)
                box.BoxData[r] = new BoxData();

            if (box.BoxData[r].RowData == null || box.BoxData[r].RowData.Length != 3)
                box.BoxData[r].RowData = new ItemData[3];

            for (int c = 0; c < 3; c++)
            {
                if (box.BoxData[r].RowData[c] == null)
                    box.BoxData[r].RowData[c] = new ItemData();
            }
        }
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("BOX GRID", EditorStyles.boldLabel);

        for (int r = 2; r >= 0; r--)
        {
            EditorGUILayout.BeginHorizontal();

            for (int c = 0; c < 3; c++)
            {
                DrawItemCell(r, c);
            }

            EditorGUILayout.EndHorizontal();
        }

        serializedObject.ApplyModifiedProperties();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(box);
        }
    }

    void DrawItemCell(int r, int c)
    {
        ItemData item = box.BoxData[r].RowData[c];

        EditorGUILayout.BeginVertical(GUILayout.Width(70));

        item.sprite = (Sprite)EditorGUILayout.ObjectField(
            item.sprite,
            typeof(Sprite),
            false,
            GUILayout.Width(64),
            GUILayout.Height(64)
        );

        item.itemType = (ItemType)EditorGUILayout.EnumPopup(item.itemType);

        EditorGUILayout.EndVertical();
    }
}
