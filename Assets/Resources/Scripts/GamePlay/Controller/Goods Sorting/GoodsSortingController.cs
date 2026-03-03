 using System;
 using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GoodsSortingController : UIBase
{
    [Header("Manager")]
    [SerializeField] private GoodSortingManager _goodSortingManager;
    
    [Header("UI Element")]
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Button _playBtn;
    [SerializeField] private TextMeshProUGUI _playTMP;
    
    public Action OnPlayAction;
    private void Start()
    {
        _playBtn.onClick.AddListener(() =>
        {
            OnPlayAction?.Invoke();
            OnHide();
        });
    }
    public void Active(bool value)
    {
        if (value)
            base.OnShow();
        else
            base.OnHide();
    }

    public override void OnShow()
    {
        base.OnShow();
        var levelCurrent = _goodSortingManager.LevelCurrent;
        ShowTextLevelCurrent(levelCurrent);
    }

    private void ShowTextLevelCurrent(int levelCurrent)
    {
        _playTMP.text = $"Level {levelCurrent}";
    }
  
}
