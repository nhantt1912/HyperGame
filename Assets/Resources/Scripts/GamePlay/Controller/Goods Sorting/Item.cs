using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Goods_Sorting;
using MyBox;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class Item : MonoBehaviour, IDropHandler,IBeginDragHandler,IEndDragHandler,IDragHandler
{
    [SerializeField] private ItemType _itemType;
    [SerializeField] private Image _imgItem;
    public ItemType ItemType => _itemType;

   [SerializeField] private bool _isEmpty;
    public bool IsEmpty => _isEmpty;
    
    public Action<Item> OnDropItem;
    private bool _isDrag;
    private Vector3 _startPos;
    private bool _wasDropped;

    private CanvasGroup _canvasGroup;
    
    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>() ?? gameObject.AddComponent<CanvasGroup>();

        if (_isEmpty)
        {
            _itemType = ItemType.None;
            if (_imgItem != null)
            {
                _imgItem.sprite = null;
                _imgItem.SetAlpha(0);   
            }
        }
        else
        {
            _startPos = transform.position;
        }
    }
    public void Init(ItemType itemType, Sprite sprite)
    {
        _itemType = itemType;
        if (_imgItem != null)
        {
            _imgItem.sprite = sprite;
            _imgItem.SetAlpha(1);
            _imgItem.enabled = sprite != null;
        }

        _isEmpty = itemType == ItemType.None || sprite == null;
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
        if (droppedObject == null) return;
        Item droppedItem = droppedObject.GetComponent<Item>(); // droppedItem là thằng cầm drag
        if (droppedItem == null) return;
        
        if (this.IsEmpty)
        {
            AcceptDroppedItem(droppedItem);
            FinalizeSourceAfterDrop(droppedItem);
            OnDropItem?.Invoke(droppedItem);
            
            EventManager.Invoke(new Goods_Sorting.EventDefine.OnDropItem{itemType = _itemType});
        }
        else
        {
            droppedItem.ReturnToStart();
        }
    }

    private void AcceptDroppedItem(Item droppedItem)
    {
        this._itemType = droppedItem._itemType;
        this._isEmpty = false;
        if (this._imgItem != null && droppedItem._imgItem != null)
        {
            _imgItem.sprite = droppedItem._imgItem.sprite;
            this._imgItem.SetAlpha(1);
            this._imgItem.enabled = true;
            _imgItem.raycastTarget = true;
        }
    }

    private void FinalizeSourceAfterDrop(Item droppedItem)
    {
        droppedItem._itemType = ItemType.None;
        droppedItem._isEmpty = true;
        droppedItem._wasDropped = true;

        if (droppedItem._imgItem != null)
        {
            droppedItem._imgItem.sprite = null;
            droppedItem._imgItem.SetAlpha(0);
        }

        droppedItem.ReturnToStart(() =>
        {
            droppedItem._isDrag = false;
        });
    }
        
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_isEmpty) return;

        _isDrag = true;
        _startPos = transform.position;

        transform.SetAsLastSibling();

        if (_canvasGroup != null)
            _canvasGroup.blocksRaycasts = false;

        if (_imgItem != null)
        {
            _imgItem.raycastTarget = false;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_canvasGroup != null)
            _canvasGroup.blocksRaycasts = true;

        if (!_wasDropped)
        {
            ReturnToStart(() =>
            {
                _isDrag = false;
            });
        }

        _wasDropped = false;
        _isDrag = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(!_isDrag) return;

        Vector3 pos = Camera.main.ScreenToWorldPoint(eventData.position);
        pos.z = 0;
        transform.position = pos;
    }

    private void ReturnToStart(Action onComplete = null)
    {
        transform.DOMove(_startPos, 0.3f).OnComplete(() =>
        {
            _imgItem.raycastTarget = true;
            onComplete?.Invoke();
        });
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
