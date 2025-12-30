using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SatisLevelData", menuName = "ScriptableObjects/SatisLevelData")]
public class SatisLevelData : ScriptableObject
{
    [SerializeField] private List<SatisLevel> listSatisLevel;
    public List<SatisLevel> ListSatisLevel => listSatisLevel;

    public LevelBase GetLevel(int levelId)
    {
        return listSatisLevel.Find(x => x.levelId == levelId).levelBase;
    }
    
}

[Serializable]
public struct SatisLevel
{
    public int levelId;
    public string levelName;
    public LevelBase levelBase;
}