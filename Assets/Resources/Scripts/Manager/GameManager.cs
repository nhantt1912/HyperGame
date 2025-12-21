using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LevelBase levelBasePrefabs;
    [SerializeField] private Transform content;

    private int _indexLevelCurrent;
    
    private LevelBase _levelBaseCurrent;
    
    private void Start()
    {
        EventManager.AddListener<EventDefine.OnSelectLevel>(OnSelectLevel);
        EventManager.AddListener<EventDefine.OnLevelComplete>(OnLevelComplete);
        EventManager.AddListener<EventDefine.OnLevelFail>(OnLevelFail);
        EventManager.AddListener<EventDefine.OnBackHome>(OnBackHome);
    }

    private void OnBackHome(EventDefine.OnBackHome obj)
    {
        Destroy(_levelBaseCurrent.gameObject);
    }

    private void OnSelectLevel(EventDefine.OnSelectLevel levelItem)
    {
        Debug.Log(levelItem.levelId);
        
        _indexLevelCurrent = levelItem.levelId;
        LevelBase levelBase = Instantiate(levelBasePrefabs, content);
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
