using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class UIManager : MonoBehaviour
{
    [Header("Menu Manager")]
    [SerializeField] private MenuManager _menuManager;
    
    [Header("Hande Made Category")]
    [SerializeField] private CategoryManager _categoryManager;
    
    [SerializeField] private TopPanelController _topPanelController;
    
    private void Start()
    {
        EventManager.AddListener<EventDefine.OnBackHome>(OnBackHome);
        _menuManager.onClickType += OnSelectTypeMode;
        _menuManager.OnShow();
        
        _topPanelController.OnBackAction += OnClickBackTopSetting;
        _topPanelController.OnSettingAction += OnClickSetting;
    }

    private void OnClickBackTopSetting()
    {
        if (!_categoryManager.ISActive()) return;
        
        _categoryManager.OnHide();
        _menuManager.OnShow();
    }
    
    private void OnClickSetting()
    {
        
    }

    private void OnBackHome(EventDefine.OnBackHome obj)
    {
        _categoryManager.OnShow();
        Debug.Log("Back Home");
    }

    private void OnSelectTypeMode(MenuType typeMode)
    {
        _categoryManager.OnShow();
        _categoryManager.SetUp(typeMode);
    }
    
    private void OnDestroy() => UnRegisterEvent();
    
    private void UnRegisterEvent()
    {
        EventManager.RemoveListener<EventDefine.OnBackHome>(OnBackHome);
        _topPanelController.OnBackAction -= OnClickBackTopSetting;
        _topPanelController.OnSettingAction -= OnClickSetting;
    }
    
}
