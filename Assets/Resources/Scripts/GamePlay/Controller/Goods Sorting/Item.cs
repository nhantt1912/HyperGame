using System;
using DG.Tweening;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemType _itemType;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Collider2D _collider;

    public ItemType ItemType => _itemType;

    [SerializeField] private bool _isEmpty;
    public bool IsEmpty => _isEmpty;

    public Action OnDropItem;
    
    public Action<Item> OnAcceptDroppedItem;

    private bool _isDrag;
    private bool _wasDropped;
    private Vector3 _startPos;

    private void Start()
    {
        /*if (_isEmpty)
        {
            _itemType = ItemType.None;
            _spriteRenderer.sprite = null;
            _spriteRenderer.color = new Color(1,1,1,0);
        }
        else
        {
            _startPos = transform.position;
        }*/
    }

    public void Init(ItemType itemType, Sprite sprite)
    {
        _itemType = itemType;

        if(itemType == ItemType.None)
        {
            _spriteRenderer.sprite = null;
            _spriteRenderer.color = new Color(1,1,1,0);
        }
        else
        {
            _spriteRenderer.sprite = sprite;
            _startPos = transform.position;

        }
    }

    public void OnCollect()
    {
        transform.DOScale(Vector3.zero,0.3f)
            .SetEase(Ease.InBack)
            .OnComplete(()=> Destroy(gameObject));
    }

    // ================= DRAG =================

    private void OnMouseDown()
    {
        if (_isEmpty) return;

        _isDrag = true;
        _startPos = transform.position;
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
    }

    // ================= DROP =================

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
                ItemType droppedType = _itemType;

                target.AcceptDroppedItem(this);
                FinalizeSourceAfterDrop(this);

                OnDropItem?.Invoke();

                EventManager.Invoke(new Goods_Sorting.EventDefine.OnDropItem
                {
                    itemType = droppedType
                });

                _wasDropped = true;
                return;
            }
        }
    }

    private void AcceptDroppedItem(Item droppedItem)
    {
        _itemType = droppedItem._itemType;
        _isEmpty = false;

        _spriteRenderer.sprite = droppedItem._spriteRenderer.sprite;
        _spriteRenderer.color = Color.white;
        
        OnAcceptDroppedItem?.Invoke(this);
    }

    private void FinalizeSourceAfterDrop(Item droppedItem)
    {
        droppedItem._itemType = ItemType.None;
        droppedItem._isEmpty = true;

        droppedItem._spriteRenderer.sprite = null;
        droppedItem._spriteRenderer.color = new Color(1,1,1,0);

        droppedItem.ReturnToStart();
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