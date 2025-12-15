using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class LevelBase : MonoBehaviour
{
     [SerializeField] private int _levelId;
    
    public virtual void Init(int levelId)
    {
        _levelId = levelId;
    }

    public virtual void OnLevelComplete()
    {
        EventManager.Invoke(new EventDefine.OnLevelComplete{levelId = _levelId});
    }

    public virtual void OnLevelFail()
    {
        EventManager.Invoke(new EventDefine.OnLevelFail{levelId = _levelId});
    }    
}
