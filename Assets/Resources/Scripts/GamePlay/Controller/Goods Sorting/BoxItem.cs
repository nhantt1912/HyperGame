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
    
    private void Start()
    {
        /*_rowIndex = _listRow.Count;
        
        foreach (var row in _listRow)
        {
            row.OnCollectRow += CollectRow;
        }*/
        
    }

    public void Init(BoxDataSO boxDataSo)
    {
        for (int i = 0; i < boxDataSo.BoxData.Length; i++)
        {
            _listRow[i].InitRow(boxDataSo);
            _listRow[i].OnCollectRow += CollectRow;
        }
    }
    
    public void AddItemIntoBox(BoxDataSO boxDataSo)
    {
        
    }

    public void AddItemIntoRow(Item item)
    {
       var isRight = _listRow[_rowIndex].AddItemIntoRow(item);
       
       if (isRight)
       {
           Debug.Log("Added Item");
       }
       else
       {
           Debug.Log("Row Is Full");
       }
    }

    private void CollectRow()
    {
        
    }
    
    
}
