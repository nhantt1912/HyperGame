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
        InitRow();
    }

    private void InitRow()
    {
        for (int i = 0; i < 3; i++)
        {
           Item item = _tfParent.GetChild(i).GetComponent<Item>();
           item.OnDropItem += OnDropItem;
           _listItem.Add(item);
        }    
    }

    private void OnDropItem(Item obj)
    {
        Debug.Log(" Item Hold : " + obj);
        CheckRow();
    }

    public bool AddItemIntoRow(Item item)
    {
        if(item.IsEmpty) return false;
        
        for (int i = 0; i < _listItem.Count; i++)
        {
            if (_listItem[i] != null) continue;
            _listItem[i] = item;
            return true;
        }
        return false;
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
