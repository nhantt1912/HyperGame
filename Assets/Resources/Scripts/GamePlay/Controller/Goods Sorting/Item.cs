using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Goods_Sorting;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class Item : MonoBehaviour, IDropHandler,IBeginDragHandler,IEndDragHandler
{
    [SerializeField] private ItemType _itemType;
    [SerializeField] private Image _imgItem;
    public ItemType ItemType => _itemType;

   [SerializeField] private bool _isEmpty;
    public bool IsEmpty => _isEmpty;
    
    private void Start()
    {
        if (!_isEmpty)
        {
            _itemType = ItemType.None;
            _imgItem.enabled = false;
        }
    }

    private void OnDropItem()
    {
        EventManager.Invoke(new Goods_Sorting.EventDefine.OnDropItem{itemType = _itemType});
    }

    public void Init(ItemType itemType, Sprite sprite)
    {
        _itemType = itemType;
        _imgItem.sprite = sprite;
    }

    public void OnCollect()
    {
        transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }


    public void OnDrop(PointerEventData eventData)
    {
        // Get the GameObject being dragged
        GameObject droppedObject = eventData.pointerDrag;
    
        // Check if it has an Item component
        if (droppedObject != null)
        {
            Item droppedItem = droppedObject.GetComponent<Item>();
        
            if (droppedItem != null)
            {
                // Check if the current item slot is empty
                if (_isEmpty)
                {
                    Debug.Log("Drop Item");
                    // Handle the dropped item
                    // For example, you might want to:
                    // - Get the item type: droppedItem.ItemType
                    // - Move the item to this location
                    // - Update the UI
                
                    // Trigger your drop event
                   // EventManager.Invoke(new Goods_Sorting.EventDefine.OnDropItem{itemType = droppedItem.ItemType});
                
                    // You might also want to update the current item's state
                    /*_itemType = droppedItem.ItemType;
                    _imgItem.sprite = droppedItem.GetComponent<Image>().sprite;
                    _imgItem.enabled = true;
                    _isEmpty = false;*/
                }
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
    }
}

[Serializable]
public enum ItemType
{
    None = -1,
    MI_30,
    MI_31,
    MI_32,
    MI_33,
    MI_34
}
