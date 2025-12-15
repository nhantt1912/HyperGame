using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Menu Manager")]
    [SerializeField] private MenuManager menuManager;
    
    [Header("Hande Made Category")]
    [SerializeField] private CategoryManager categoryManager;
    
    private void Start()
    {
        menuManager.onClickType += OnSelectTypeMode;
        menuManager.OnShow();
        
    }

    private void OnSelectTypeMode(MenuType typeMode)
    {
        categoryManager.OnShow();
        categoryManager.SetUp(typeMode);
    }
    
    
}
