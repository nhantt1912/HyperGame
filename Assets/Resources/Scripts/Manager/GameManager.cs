using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   // [SerializeField] private LevelBase levelBasePrefabs;
    [SerializeField] private SatisLevelData satisLevelData;
    [SerializeField] private Transform content;

    private int _indexLevelCurrent;
    
    private LevelBase _levelBaseCurrent;
    
    private void Start()
    {
        EventManager.AddListener<EventDefine.OnSelectLevel>(OnSelectLevel);
        EventManager.AddListener<EventDefine.OnLevelComplete>(OnLevelComplete);
        EventManager.AddListener<EventDefine.OnLevelFail>(OnLevelFail);
        EventManager.AddListener<EventDefine.OnBackHome>(OnBackHome);
        EventManager.AddListener<EventDefine.OnReplayLevel>(OnReplayLevel);
        EventManager.AddListener<EventDefine.OnNextLevel>(OnNextLevel);;
    }

    private void OnReplayLevel(EventDefine.OnReplayLevel obj)
    {
        Destroy(_levelBaseCurrent.gameObject);
        LevelBase levelBase = Instantiate(satisLevelData.GetLevel(_indexLevelCurrent) , content);
        _levelBaseCurrent = levelBase;
        levelBase.Init(_indexLevelCurrent);
    }    
    
    private void OnNextLevel(EventDefine.OnNextLevel obj)
    {
        _indexLevelCurrent++;
        if (satisLevelData.GetLevel(_indexLevelCurrent) == null)
        {
            Debug.Log("Level Not Found");
        }
        else
        {
            Destroy(_levelBaseCurrent.gameObject);
            LevelBase levelBase = Instantiate(satisLevelData.GetLevel(_indexLevelCurrent) , content);
            _levelBaseCurrent = levelBase;
            levelBase.Init(_indexLevelCurrent);
        }
    }
    
    private void OnBackHome(EventDefine.OnBackHome obj)
    {
        Destroy(_levelBaseCurrent.gameObject);
    }

    private void OnSelectLevel(EventDefine.OnSelectLevel levelItem)
    {
        Debug.Log(levelItem.levelId);
        if (satisLevelData.GetLevel(levelItem.levelId) == null)
        {
            Debug.Log("Level Not Found");
            return;
        }
        
        _indexLevelCurrent = levelItem.levelId;
        LevelBase levelBase = Instantiate(satisLevelData.GetLevel(levelItem.levelId) , content);
        _levelBaseCurrent = levelBase;
        levelBase.Init(levelItem.levelId);
    }
    
    private void OnLevelFail(EventDefine.OnLevelFail levelCurrent)
    {
        Debug.Log("Level Fail");
        if(_indexLevelCurrent != levelCurrent.levelId) return;
    }

    private void OnLevelComplete(EventDefine.OnLevelComplete levelCurrent)
    {
        Debug.Log($"Level {levelCurrent.levelId} Complete");
        
        if(_indexLevelCurrent != levelCurrent.levelId) return;
       
    }
}
