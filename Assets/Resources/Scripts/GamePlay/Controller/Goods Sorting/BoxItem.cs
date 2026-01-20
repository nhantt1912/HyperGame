using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BoxItem : MonoBehaviour
{
   [SerializeField] private List<Row> _listRow;

    private int _rowIndex;
    
    private void Start()
    {
        _rowIndex = _listRow.Count;
        
        foreach (var row in _listRow)
        {
            row.OnCollectRow += CollectRow;
        }
        
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
        _listRow[_rowIndex - 1].MoveNextPosition(-65f);
    }
    
    
}
