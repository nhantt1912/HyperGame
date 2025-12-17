using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    public Action OnMouseDownAction;
    public Action OnMouseUpAction;
    
    protected bool _isDrag;

    private BoxCollider2D _boxCollider;
    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void OnMouseDown()
    {
        OnMouseDownAction?.Invoke();
        _isDrag = true;
    }
    
    protected virtual void OnMouseUp()
    {
        OnMouseUpAction?.Invoke();
        _isDrag = false;
    }

    protected virtual void Update()
    {
        if(!_isDrag)return;
        
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        transform.position = pos;
    }
    
    public void SetEnable(bool enable)
    {
        _boxCollider.enabled = enable;
    }
}
