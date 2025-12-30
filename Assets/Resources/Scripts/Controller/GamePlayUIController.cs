using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GamePlayUIController : MonoBehaviour
{
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private Button _replayButton;

    [SerializeField] private RectTransform _rectTransformNextLevelButton;
    [SerializeField] private RectTransform _rectTransformReplayButton;
    
    private Tween _tweenMoveNextLevelButton;
    private Tween _tweenMoveReplayButton;
    
    private void Start()
    {
        EventManager.AddListener<EventDefine.OnSelectLevel>(OnSelectLevel);
        EventManager.AddListener<EventDefine.OnBackHome>(OnBackHome);
        
        _nextLevelButton.onClick?.AddListener(OnClickNextButton);
        _replayButton.onClick?.AddListener(OnClickReplayButton);
    }

    private void OnSelectLevel(EventDefine.OnSelectLevel obj)
    {
        _nextLevelButton.interactable = false;
        _replayButton.interactable = false;
        _tweenMoveNextLevelButton?.Kill();
        _tweenMoveReplayButton?.Kill();
        _tweenMoveNextLevelButton = _rectTransformNextLevelButton.DOAnchorPosX(-165, 0.3f).SetEase(Ease.OutBack);
        _tweenMoveReplayButton = _rectTransformReplayButton.DOAnchorPosX(165, 0.3f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            _nextLevelButton.interactable = true;
            _replayButton.interactable = true;
        });
    }
     
    private void OnBackHome(EventDefine.OnBackHome obj)
    {
        _nextLevelButton.interactable = false;
        _replayButton.interactable = false;
        _tweenMoveNextLevelButton?.Kill();
        _tweenMoveReplayButton?.Kill();
        _tweenMoveNextLevelButton = _rectTransformNextLevelButton.DOAnchorPosX(0, 0.3f).SetEase(Ease.InBack);
        _tweenMoveReplayButton = _rectTransformReplayButton.DOAnchorPosX(0, 0.3f).SetEase(Ease.InBack);
        
    }
    
    private void OnClickNextButton()
    {
        EventManager.Invoke(new EventDefine.OnNextLevel());
    }   
    
    private void OnClickReplayButton()
    {
        EventManager.Invoke(new EventDefine.OnReplayLevel());
    }
}
