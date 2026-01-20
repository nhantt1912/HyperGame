 using System;
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsSortingController : MonoBehaviour
{
    [SerializeField] private List<BoxItem> _listBoxItem;

    private void Start()
    {
        EventManager.AddListener<Goods_Sorting.EventDefine.OnDropItem>(OnDropItem);
    }

    private void OnDropItem(Goods_Sorting.EventDefine.OnDropItem obj)
    {
        if (obj.itemType == ItemType.MI_30)
        {
            
        }
    }
}
