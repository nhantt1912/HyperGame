using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Row : MonoBehaviour
{
    [SerializeField] private List<Item> _listItem;

    public void InitItem(ItemType itemType,Sprite sprite )
    {
        foreach (var item in _listItem)
        {
            item.Init(itemType,sprite);
        }
    }
    
    
}
