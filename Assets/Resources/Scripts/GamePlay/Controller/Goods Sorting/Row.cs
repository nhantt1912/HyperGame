using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Row : MonoBehaviour
{
    [SerializeField] private List<Item> _listItem;

    public Action OnCollectRow;
    
    /*public void InitItem(ItemType itemType,Sprite sprite )
    {
        foreach (var item in _listItem)
        {
            item.Init(itemType,sprite);
        }
    }*/

    public bool AddItemIntoRow(Item item)
    {
        for (int i = 0; i < _listItem.Count; i++)
        {
            if (_listItem[i] != null) continue;
            _listItem[i] = item;
            return true;
        }
        return false;
    }
    
    
    public void MoveNextPosition(float value)
    {
        transform.DOLocalMoveY(value,0.2f).SetEase(Ease.OutBack);
    }

    private void CorrectRow()
    {
        foreach (var item in _listItem)
        {
            item.OnCollect();
        }
        OnCollectRow?.Invoke();
    }

    public void CheckRow()
    {
        bool allSame = AreAllItemsSameType();
        
        if (allSame)
        {
            CorrectRow();
        }
        else
        {
            Debug.Log("Not all items are same");
        }
    }
    
    private bool AreAllItemsSameType()
    {
        if (_listItem == null || _listItem.Count == 0)
            return false;

        var firstItem = _listItem[0];
        if (firstItem == null)
            return false;

        var firstType = firstItem.ItemType;

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
