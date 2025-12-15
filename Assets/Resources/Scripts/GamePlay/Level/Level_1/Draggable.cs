using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    public Action OnMouseDownAction;
    public Action OnMouseUpAction;
    
    private Vector3 _startPos;
    
    private bool _isDrag;
    
    private void Start()
    {
        
    }
    
    private void OnMouseDown()
    {
        OnMouseDownAction?.Invoke();
        
        _startPos = transform.position;
        _isDrag = true;
    }

    private void OnMouseUp()
    {
        OnMouseUpAction?.Invoke();
        _isDrag = false;
    }

    private void Update()
    {
        if(!_isDrag)return;
        
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        transform.position = pos;
        
    }
    
    
}
