using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class PictureCollageLevel : BaseLevelDrag
{
    
    [ReadOnly,SerializeField] private List<DraggableExt> listItemDraggables;

    public override void Init(int levelId)
    {
        base.Init(levelId);
        listItemDraggables.ForEach(x => x.OnCompleteAction += OnCompleteItem);
    }

    private void OnCompleteItem()
    {
        _count++;
        CheckCountComplete();
    }

}
