using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BoxItem : MonoBehaviour
{
   [SerializeField] private List<Row> _listRow;

    private int _rowIndex;

    public void Init(BoxDataSO boxDataSo)
    {
        _rowIndex = 0;
        for (int r = 0; r < boxDataSo.BoxData.Length; r++)
        {
            _listRow[r].InitRow(boxDataSo.BoxData[r]); 
            _listRow[r].OnCollectRow += CollectRow;
            _listRow[r].SetActiveRow(false);
        }
        _listRow[_rowIndex].SetActiveRow(true);
    }

    private void CollectRow()
    {
        if(_rowIndex >= _listRow.Count - 1) return;
        _rowIndex++;
        _listRow[_rowIndex].SetActiveRow(true);
        DOVirtual.DelayedCall(0.3f, () =>
        {
            _listRow[_rowIndex].MoveNextPosition();
        });
    }
    
    
}
