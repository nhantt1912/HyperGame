using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureCollageLevel : BaseLevelDrag
{
    [SerializeField] private List<Draggable> listItemDraggables;

    public override void Init(int levelId)
    {
        base.Init(levelId);
        listItemDraggables.ForEach(x => x.OnMouseUpAction += OnDropItem);
    }

    private void OnDropItem()
    {
        _count++;
        CheckCountComplete();
    }
}
