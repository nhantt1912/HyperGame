using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundController : MonoBehaviour
{
    [SerializeField] private BoxItem _boxPrefab;
    [SerializeField] private BoxDataSO _boxData;
    
    [SerializeField] private Grid grid;

    [SerializeField] private int _width;
    [SerializeField] private int _height;

    [SerializeField] private Vector2 padding = new Vector2(0.2f, 0.2f);
    [SerializeField] private Transform _parent;

    [SerializeField] private List<Vector2> _listSkipCells;

    [SerializeField] private BoxItem[,] _gridObject;

    private void Start()
    {
        _gridObject = new BoxItem[_width, _height];
        GenerateRound();
    }

    private void GenerateRound()
    {
        Vector3 cellSize = grid.cellSize;

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                Vector2 cell = new Vector2(x, y);

                if (_listSkipCells.Contains(cell)) continue;

                Vector3 position = new Vector3(
                    x * (cellSize.x + padding.x),
                    y * (cellSize.y + padding.y),
                    0);

                _gridObject[x, y] = Instantiate(_boxPrefab);
                _gridObject[x,y].Init(_boxData);
                _gridObject[x, y].transform.SetParent(_parent);
                _gridObject[x, y].transform.localPosition = position;
            }
        }
    }
}