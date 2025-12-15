using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelName;
    [SerializeField] private Image levelImage;

    [SerializeField] private Button button;
    
    [ReadOnly(true)]
    [SerializeField] private int levelId;

    private void Start()
    {
        button.onClick.AddListener(OnClickLevelItem);
    }

    private void OnClickLevelItem()
    {
        EventManager.Invoke(new EventDefine.OnSelectLevel{levelId = levelId});
        
    }

    public void Init(int levelId,string levelName)
    {
        this.levelId = levelId;
        this.levelName.text = levelName;
    }
    
    
    
    
}
