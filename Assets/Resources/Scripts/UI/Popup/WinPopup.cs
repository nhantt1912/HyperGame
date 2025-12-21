using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinPopup : PopupBase
{
    [SerializeField] private Button homeBTN;
    [SerializeField] private Button nextBTN;
    private void Start()
    {
        Debug.Log("Win Popup");
        homeBTN.onClick?.AddListener(OnClickHomeBTN);
        nextBTN.onClick?.AddListener(OnNextLevel);
        EventManager.AddListener<EventDefine.OnLevelComplete>(OnLevelComplete);
    }

    private void OnClickHomeBTN()
    {
        Debug.Log("Home");
        EventManager.Invoke(new EventDefine.OnBackHome());
        OnHide();
    }
    
    private void OnNextLevel()
    {
        
    }

    private void OnLevelComplete(EventDefine.OnLevelComplete obj)
    {
        OnShow();
    }
    
}
