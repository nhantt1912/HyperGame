using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BoxItem : MonoBehaviour
{
   [SerializeField] private List<Row> _listRow;

    private int _rowIndex;

    public void Init(BoxDataSO boxDataSo)
    {
        for (int r = 0; r < boxDataSo.BoxData.Length; r++)
        {
            _listRow[r].InitRow(boxDataSo.BoxData[r]); 
            _listRow[r].OnCollectRow += CollectRow;
        }
    }

    private void CollectRow()
    {
        
    }
    
    
}
