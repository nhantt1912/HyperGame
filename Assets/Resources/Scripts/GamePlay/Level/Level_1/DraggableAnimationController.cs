using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

public class DraggableAnimationController : MonoBehaviour
{
    [SerializeField] private bool isRotation;
    [SerializeField] private bool isScale;
    [SerializeField] private bool isRaiseSorting;
    
    [SerializeField] private SortingGroup sortingGroup;
    private Draggable draggable;
    
    private int sortiingDefault;
    private void Start()
    {
        draggable = GetComponent<Draggable>();
        draggable.OnMouseDownAction += OnPickItem;
        draggable.OnMouseUpAction += OnDropItem;
        
        sortiingDefault = sortingGroup.sortingOrder;    
    }

    private void OnPickItem()
    {
        if (isRaiseSorting) sortingGroup.sortingOrder += 50;
        
        if (isScale) transform.DOScale(Vector3.one * 1.1f, 0.1f);
        
        if(isRotation) transform.DORotate(Vector3.zero, 0.1f);

    }
    
    private void OnDropItem()
    {
        transform?.DOKill();
        if(isRaiseSorting) sortingGroup.sortingOrder = sortiingDefault;
        if(isScale) transform.DOScale(Vector3.one, 0.1f);
        if (isRotation)
        {
            float angleRandom = UnityEngine.Random.Range(-10, 10);
            transform.DORotate(new Vector3(0, 0, angleRandom), 0.1f);
        }
    }

}
