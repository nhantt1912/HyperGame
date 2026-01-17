using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace ScratchCardAsset.Editor
{
    public class ScratchCardMenu : UnityEditor.Editor
    {
        private static string ScratchCardDefaultPath = "Assets/ScratchCard/Prefabs/ScratchCard.prefab";

        private static void CreateScratchCard()
        {
            var asset = AssetDatabase.LoadAssetAtPath(ScratchCardDefaultPath, typeof(GameObject));
            var instantiatedPrefab = PrefabUtility.InstantiatePrefab(asset);
            Selection.activeObject = instantiatedPrefab;

            EditorUtility.SetDirty(instantiatedPrefab);
            EditorSceneManager.MarkSceneDirty(((GameObject) instantiatedPrefab).scene);
        }
    }
}