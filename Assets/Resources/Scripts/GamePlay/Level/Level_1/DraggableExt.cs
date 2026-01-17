using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
        if (!(Vector3.Distance(transform.position, targetPosition.position) < distance)) return;
        
        OnCorrectPosition();
    }
        
    private void OnCorrectPosition()
    {
        SetEnable(false);
        transform.DOMove(targetPosition.position, 0.2f);   
        transform.DORotate(Vector3.zero, 0.1f).OnComplete(() =>
        {
            OnCompleteAction?.Invoke();
        });
    }
    
}
