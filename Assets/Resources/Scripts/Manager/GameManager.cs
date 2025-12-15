using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LevelBase levelBasePrefabs;
    [SerializeField] private Transform content;

    private int _levelCurrent;
    
    private void Start()
    {
        EventManager.AddListener<EventDefine.OnSelectLevel>(OnSelectLevel);
        EventManager.AddListener<EventDefine.OnLevelComplete>(OnLevelComplete);
        EventManager.AddListener<EventDefine.OnLevelFail>(OnLevelFail);
    }

    private void OnSelectLevel(EventDefine.OnSelectLevel levelItem)
    {
        Debug.Log(levelItem.levelId);
        
        _levelCurrent = levelItem.levelId;
        LevelBase levelBase = Instantiate(levelBasePrefabs, content);
        levelBase.Init(levelItem.levelId);
    }
    
    private void OnLevelFail(EventDefine.OnLevelFail levelCurrent)
    {
        Debug.Log("Level Fail");
        if(_levelCurrent != levelCurrent.levelId) return;
    }

    private void OnLevelComplete(EventDefine.OnLevelComplete levelCurrent)
    {
        Debug.Log($"Level {levelCurrent.levelId} Complete");
        
        if(_levelCurrent != levelCurrent.levelId) return;
       
    }
}
