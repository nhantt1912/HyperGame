using System.Collections;
using System.Collections.Generic;
using Resources.Scripts.GamePlay.Controller.Goods_Sorting;
using UnityEditor;
using UnityEngine;

public class GoodSortingLevelEditor : EditorWindow
{
    private SortingLevelLayout levelData;

    private const int width = 3;
    private const int height = 4;

    private Vector2 scroll;

    [UnityEditor.MenuItem("Tools/Good Sorting/Level Editor")]
    public static void Open()
    {
        GetWindow<GoodSortingLevelEditor>("Sorting Level Editor");
    }

    private void OnGUI()
    {
        EditorGUILayout.Space();

        levelData = (SortingLevelLayout)EditorGUILayout.ObjectField(
            "Level Layout",
            levelData,
            typeof(SortingLevelLayout),
            false);

        if (levelData == null)
        {
            EditorGUILayout.HelpBox("Assign Level Layout first", MessageType.Info);
            return;
        }

        DrawToolbar();

        EditorGUILayout.Space();

        scroll = EditorGUILayout.BeginScrollView(scroll);

        DrawGrid();

        EditorGUILayout.EndScrollView();

        ValidateLevel();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(levelData);
        }
    }

    void DrawToolbar()
    {
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Clear"))
        {
            for (int i = 0; i < levelData.boxes.Length; i++)
                levelData.boxes[i] = null;
        }

        if (GUILayout.Button("Random Fill"))
        {
            string[] guids = AssetDatabase.FindAssets("t:BoxDataSO");

            for (int i = 0; i < levelData.boxes.Length; i++)
            {
                if (guids.Length == 0) return;

                int rand = Random.Range(0, guids.Length);

                string path = AssetDatabase.GUIDToAssetPath(guids[rand]);

                levelData.boxes[i] =
                    AssetDatabase.LoadAssetAtPath<BoxDataSO>(path);
            }
        }

        EditorGUILayout.EndHorizontal();
    }

    void DrawGrid()
    {
        for (int y = height - 1; y >= 0; y--)
        {
            EditorGUILayout.BeginHorizontal();

            for (int x = 0; x < width; x++)
            {
                int index = y * width + x;

                DrawBox(index);
            }

            EditorGUILayout.EndHorizontal();
        }
    }

    void DrawBox(int index)
    {
        EditorGUILayout.BeginVertical(GUILayout.Width(120));

        levelData.boxes[index] =
            (BoxDataSO)EditorGUILayout.ObjectField(
                levelData.boxes[index],
                typeof(BoxDataSO),
                false,
                GUILayout.Width(110));

        DrawPreview(levelData.boxes[index]);

        EditorGUILayout.EndVertical();
    }

    void DrawPreview(BoxDataSO box)
    {
        if (box == null) return;

        EditorGUILayout.BeginVertical("box");

        foreach (var item in box.BoxData)
        {
            if (item == null) continue;

           // GUILayout.Label(item.name);
        }

        EditorGUILayout.EndVertical();
    }

    void ValidateLevel()
    {
        int empty = 0;

        foreach (var box in levelData.boxes)
        {
            if (box == null)
                empty++;
        }

        EditorGUILayout.Space();

        if (empty > 0)
        {
            EditorGUILayout.HelpBox(
                $"Level contains {empty} empty box",
                MessageType.Warning);
        }
        else
        {
            EditorGUILayout.HelpBox(
                "Level looks valid",
                MessageType.Info);
        }
    }
}
