using System.Collections.Generic;
using Resources.Scripts.GamePlay.Controller.Goods_Sorting;
using UnityEngine;

public class RoundController : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private BoxItem _boxPrefab;

    [Header("Level Data")]
    [SerializeField] private LevelSortingData _levelData;

    [Header("Grid Settings")]
    [SerializeField] private Grid grid;
    [SerializeField] private Vector2 padding = new Vector2(0.2f, 0.2f);

    [Header("Hierarchy")]
    [SerializeField] private Transform _parent;

    [Header("Skip Cells")]
    [SerializeField] private List<Vector2Int> _listSkipCells;

    private BoxItem[,] _gridObject;

    private int _width;
    private int _height;

    private void Start()
    {
        GenerateRound();
    }

    private void GenerateRound()
    {
        if (_levelData == null)
        {
            Debug.LogError("Level Data is missing!");
            return;
        }

        _width = _levelData.width;
        _height = _levelData.height;

        _gridObject = new BoxItem[_width, _height];

        Vector3 cellSize = grid.cellSize;

        float offsetX = (_width - 1) * (cellSize.x + padding.x) * 0.5f;
        float offsetY = (_height - 1) * (cellSize.y + padding.y) * 0.5f;

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                int editorY = _height - 1 - y;

                BoxDataSO data = _levelData.GetBox(x, editorY);

                if (data == null)
                    continue;

                Vector3 position = new Vector3(
                    x * (cellSize.x + padding.x) - offsetX,
                    y * (cellSize.y + padding.y) - offsetY,
                    0);

                BoxItem box = Instantiate(_boxPrefab, _parent);
                box.transform.localPosition = position;

                box.Init(data);

                _gridObject[x, y] = box;
            }
        }
    
    }
}