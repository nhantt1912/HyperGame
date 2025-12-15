using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseLevelDrag : LevelBase
{
    [SerializeField] private int countComplete;
    
    protected int _count;
    
    public override void Init(int levelId)
    {
        base.Init(levelId);
        _count = 0;
    }

    protected virtual void CheckCountComplete()
    {
        if (_count >= countComplete)
        {
            OnLevelComplete();
        }
    }

}
