using System;
using DG.Tweening;
using MyBox;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemType _itemType;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Collider2D _collider;

    private Transform _tfParent;
    public ItemType ItemType => _itemType;

    [SerializeField] private bool _isEmpty;
    public bool IsEmpty => _isEmpty;

    public Action OnDropItem;
    
    public Action<Item> OnAcceptDroppedItem;

    private int _sortingOderDefault;
    private bool _isDrag;
    private bool _wasDropped;
    private Vector3 _startPos;

    public void Init(ItemType itemType, Sprite sprite)
    {
        _itemType = itemType;

        if(itemType == ItemType.None)
        {
            _isEmpty = true;
            _spriteRenderer.enabled = false;
            _spriteRenderer.sprite = null;
            _spriteRenderer.color = new Color(1,1,1,0);
        }
        else
        {
            _isEmpty = false;
            _spriteRenderer.enabled = true;
            _spriteRenderer.sprite = sprite;
            _startPos = transform.position;
            _sortingOderDefault = _spriteRenderer.sortingOrder;
        }
        _tfParent = transform.parent;
    }

    public void OnCollect()
    {
        transform.DOScale(Vector3.zero,0.3f)
            .SetEase(Ease.InBack)
            .OnComplete(()=> Destroy(gameObject));
    }

    public void Active(bool value)
    {
        _collider.enabled = value;
        float alpha = value ? 1 : 0.5f;
        _spriteRenderer.SetAlpha(alpha);
    }

    #region DragAndDrop

    private void OnMouseDown()
    {
        if (_isEmpty) return;

        _isDrag = true;
        _startPos = transform.position;
        transform.SetParent(null);
        _spriteRenderer.sortingOrder += 100;
    }

    private void OnMouseDrag()
    {
        if (!_isDrag) return;

        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        transform.position = pos;
    }

    private void OnMouseUp()
    {
        if (!_isDrag) return;

        CheckDrop();

        if (!_wasDropped)
        {
            ReturnToStart();
        }

        _isDrag = false;
        _wasDropped = false;
        transform.SetParent(_tfParent);
        _spriteRenderer.sortingOrder = _sortingOderDefault;
    }


    private void CheckDrop()
    {
        Collider2D[] hits = Physics2D.OverlapPointAll(transform.position);

        foreach (var hit in hits)
        {
            Item target = hit.GetComponent<Item>();

            if (target == null || target == this)
                continue;

            if (target.IsEmpty)
            {
                target.AcceptDroppedItem(this);
                FinalizeSourceAfterDrop(this);

                OnDropItem?.Invoke();

                _wasDropped = true;
                return;
            }
        }
    }
    
    #endregion DragAndDrop

    private void AcceptDroppedItem(Item droppedItem)
    {
        _itemType = droppedItem._itemType;
        _isEmpty = false;

        _spriteRenderer.enabled = true;
        _spriteRenderer.sprite = droppedItem._spriteRenderer.sprite;
        _spriteRenderer.color = Color.white;
        
        OnAcceptDroppedItem?.Invoke(this);
    }

    private void FinalizeSourceAfterDrop(Item droppedItem)
    {
        droppedItem._itemType = ItemType.None;
        droppedItem._isEmpty = true;

        droppedItem._spriteRenderer.enabled = false;
        droppedItem._spriteRenderer.sprite = null;
        droppedItem._spriteRenderer.color = new Color(1,1,1,0);

        droppedItem.ReturnToStart();
        
        OnAcceptDroppedItem?.Invoke(droppedItem);
    }

    private void ReturnToStart(Action onComplete = null)
    {
        transform.DOMove(_startPos,0.3f)
            .OnComplete(()=> onComplete?.Invoke());
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