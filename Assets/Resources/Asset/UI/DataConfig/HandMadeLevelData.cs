using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HandMadeLevelData", menuName = "ScriptableObjects/HandMadeLevelData")]
public class HandMadeLevelData : ScriptableObject
{
    [SerializeField] private List<HandMadeLevel> _listHandMadeLevel;
    public List<HandMadeLevel> ListHandMadeLevel => _listHandMadeLevel;

    public LevelBase GetLevel(int levelId)
    {
        return _listHandMadeLevel.Find(x => x.levelId == levelId).levelBase;
    }
    
}
[Serializable]
public class HandMadeLevel
{
    public int levelId;
    public string levelName;
    public LevelBase levelBase;   
}