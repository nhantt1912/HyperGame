using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Row : MonoBehaviour
{
    [SerializeField] private List<Item> _listItem;
    [SerializeField] private Transform _tfParent;

    public Action OnCollectRow;

    private void Start()
    {
       // InitRow();
    }

    public void InitRow(BoxData rowData)
    {
        for (int c = 0; c < rowData.RowData.Length; c++)
        {
            Item item = _tfParent.GetChild(c).GetComponent<Item>();

            item.Init(
                rowData.RowData[c].itemType,
                rowData.RowData[c].sprite
            );

            item.OnAcceptDroppedItem += OnAcceptDropItem;

            _listItem.Add(item);
        }
    }

    public void SetActiveRow(bool value)
    {
        foreach (var item in _listItem)
        {
            item.Active(value);
        }
    }

    public void MoveNextPosition()
    {
        transform.DOLocalMoveY(transform.localPosition.y - 0.1f, 0.1f);
    }

    private void OnAcceptDropItem(Item obj)
    {
        Debug.Log(" Item Hold : " + obj);
        CheckRow();
    }
    
    private void CheckRow()
    {
        var allSame = AreAllItemsSameType();
        
        if (allSame)
        {
            CorrectRow();
        }
        else
        {
            Debug.Log("Not all items are same");
        }
    }
    
    private void CorrectRow()
    {
        foreach (var item in _listItem)
        {
            item.OnCollect();
        }
        Debug.Log("Collect Row");

        _listItem.Clear();
        OnCollectRow?.Invoke();
    }
    
    private bool AreAllItemsSameType()
    {
        if (_listItem == null || _listItem.Count == 0)
            return false;

        var firstItem = _listItem[0];
        if (firstItem == null)
            return false;

        var firstType = firstItem.ItemType;
        
        if(firstType == ItemType.None) return false;
        
        foreach (var item in _listItem)
        {
            if (item == null)
                return false;

            if (item.ItemType != firstType)
                return false;
        }

        return true;
    }
    
}
