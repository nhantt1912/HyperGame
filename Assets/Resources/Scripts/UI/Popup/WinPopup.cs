using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class WinPopup : PopupBase
{
    [SerializeField] private RectTransform rectHand;
    [SerializeField] private RectTransform rectText;
    
    [SerializeField] private Button homeBTN;
    [SerializeField] private Button nextBTN;
    
    private Tween _scaleTween;
    private Tween _rotateTween;
    private Tween _scaleTweenText;
    
    private void Start()
    {
        Debug.Log("Win Popup");
        // register scene-placed win popup with PopupManager so it can be shown via manager
        PopupManager.Instance.RegisterInstance(PopupType.Win, this);
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
        OnHide();
        EventManager.Invoke(new EventDefine.OnNextLevel());
    }

    private void OnLevelComplete(EventDefine.OnLevelComplete obj)
    {
        OnShow();
    }

    public override void OnShow()
    {
        base.OnShow();
        AnimationOnWin();
    }

    private void AnimationOnWin()
    {
        KillAllTween();
        _scaleTweenText = rectText.DOScale(Vector3.one * 1.2f, 0.8f).SetEase(Ease.OutBack);
        _scaleTween = rectHand.DOScale(Vector3.zero, 0.8f).From().SetEase(Ease.OutBack);
        _rotateTween = rectHand.DORotate(new Vector3(0, 0, 10), 0.2f)
            .From(new Vector3(0, 0, -10))
            .SetEase(Ease.InOutSine)
            .SetLoops(10, LoopType.Yoyo);
    }
    
    private void KillAllTween()
    {
        _scaleTween?.Kill();
        _rotateTween?.Kill();
        _scaleTweenText?.Kill();
    }
    
}
