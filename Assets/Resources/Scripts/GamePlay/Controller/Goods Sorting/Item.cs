using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemType _itemType;
    [SerializeField] private SpriteRenderer _srItem;

    public void Init(ItemType itemType, Sprite sprite)
    {
        _itemType = itemType;
        _srItem.sprite = sprite;
    }
    
}

[Serializable]
public enum ItemType
{
    MI_30,
    MI_31,
    MI_32,
    MI_33,
    MI_34
}
