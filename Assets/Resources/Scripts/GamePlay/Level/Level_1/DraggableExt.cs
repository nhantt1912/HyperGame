using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DraggableExt : Draggable
{
    [SerializeField] private Transform targetPosition;
    [SerializeField] private float distance = 0.1f;
    
    public Action OnCompleteAction;
    protected override void OnMouseDown()
    {
        SoundManager.Instance.PlayClickSound();
        base.OnMouseDown();
    }

    protected override void OnMouseUp()
    {
        base.OnMouseUp();
        if (Vector3.Distance(transform.position, targetPosition.position) < distance)
        {
            SetEnable(false);
            OnCompleteAction?.Invoke();
        }
    }
}
