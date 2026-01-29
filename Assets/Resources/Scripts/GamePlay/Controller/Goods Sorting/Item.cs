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

public class Item : MonoBehaviour, IDropHandler,IBeginDragHandler,IEndDragHandler,IDragHandler
{
    [SerializeField] private ItemType _itemType;
    [SerializeField] private Image _imgItem;
    public ItemType ItemType => _itemType;

   [SerializeField] private bool _isEmpty;
    public bool IsEmpty => _isEmpty;
    
    private bool _isDrag;
    private Vector3 _startPos;
    
    private void Start()
    {
        if (_isEmpty)
        {
            _itemType = ItemType.None;
            // nếu ô rỗng thì làm trong suốt ảnh
            if (_imgItem != null)
            {
                Color c = _imgItem.color;
                c.a = 0f; // trong suốt
                _imgItem.color = c;
                // vẫn giữ enabled = true để có thể thay đổi alpha runtime
                _imgItem.enabled = true;
            }
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
        GameObject droppedObject = eventData?.pointerDrag;
        if (droppedObject == null)
        {
            Debug.Log("OnDrop: pointerDrag is null");
            return;
        }

        Item droppedItem = droppedObject.GetComponent<Item>();
        if (droppedItem == null)
        {
            Debug.Log("OnDrop: dropped object has no Item component");
            return;
        }

        // THIS (this) is the drop target. Check if target is empty.
        if (this.IsEmpty)
        {
            Debug.Log(gameObject.name);

            // Copy data from dropped item to this target
            this._itemType = droppedItem._itemType;
            if (this._imgItem != null && droppedItem._imgItem != null)
            {
                this._imgItem.sprite = droppedItem._imgItem.sprite;
                // đặt alpha về 1 (opaque)
                Color tc = this._imgItem.color;
                tc.a = 1f; // tương đương 255
                this._imgItem.color = tc;
                this._imgItem.enabled = true;
            }
            else
            {
                Debug.LogWarning($"OnDrop: Image missing on target ({gameObject.name}) or source ({droppedObject.name})");
            }

            this._isEmpty = false;

            // Mark source as empty and deactivate it
            droppedItem._isEmpty = true;
            if (droppedItem._imgItem != null)
            {
                Color sc = droppedItem._imgItem.color;
                sc.a = 0f;
                droppedItem._imgItem.color = sc;
            }
            droppedItem.gameObject.SetActive(false);

            // Invoke drop event for the target
            OnDropItem();
        }
        else
        {
            Debug.LogWarning($"OnDrop: Target ({gameObject.name}) is not empty");
            // Target already has an item => return the dragged item to its start position
            droppedItem.ReturnToStart();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _isDrag = true;
        // record start position in world space (OnDrag uses ScreenToWorldPoint)
        _startPos = transform.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _isDrag = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(!_isDrag) return;
        Vector3 pos = Camera.main.ScreenToWorldPoint(eventData.position);
        pos.z = 0;
        transform.position = pos;
    }

    // Return the item back to its recorded start position using world-space tween
    public void ReturnToStart()
    {
        transform.DOMove(_startPos, 0.3f);
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
