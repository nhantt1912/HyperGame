using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPopup : UIBase
{
    [Header("Button Exit")]
    [SerializeField] private Button exitButton;

    [Header("Button Setting")]
    [SerializeField] private Button musicButton;
    [SerializeField] private Button soundButton;
    [SerializeField] private Button vibrationButton;

    [Header("Button On")]
    [SerializeField] private GameObject musicOn;
    [SerializeField] private GameObject soundOn;
    [SerializeField] private GameObject vibrationOn;
        
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _resumeButton;
    
    protected override void Awake()
    {
        base.Awake();
        // register this scene-placed popup with manager so manager can control it
        PopupManager.Instance.RegisterInstance(PopupType.Setting, this);
    }

    private void Start()
    {
        exitButton.onClick?.AddListener(OnExitClicked);
        musicButton.onClick?.AddListener(OnClickMusic);
        soundButton.onClick?.AddListener(OnClickSound);
        vibrationButton.onClick?.AddListener(OnClickVibration);
        _homeButton?.onClick?.AddListener(OnClickHome);
        _resumeButton?.onClick?.AddListener(OnResumeClicked);
    }

    private void OnDestroy()
    {
        // unregister to avoid stale references in manager
        PopupManager.Instance.UnregisterInstance(PopupType.Setting, this);
    }

    private void OnExitClicked()
    {
        // delegate closing to manager so it can handle modal stack / lifecycle
        PopupManager.Instance.Close(PopupType.Setting);
    }

    private void OnResumeClicked()
    {
        PopupManager.Instance.Close(PopupType.Setting);
    }

    private void OnClickHome()
    {
        EventManager.Invoke(new EventDefine.OnBackHome());
        PopupManager.Instance.Close(PopupType.Setting);
    }

    public override void OnShow()
    {
        base.OnShow();
        UpdateUI();
    }

    private void UpdateUI()
    {
        musicOn.SetActive(SettingData.settingData[SETTING.MUSIC]);
        soundOn.SetActive(SettingData.settingData[SETTING.SOUND]);
        vibrationOn.SetActive(SettingData.settingData[SETTING.VIBRATE]);
    }
    
    private void OnClickMusic()
    {
        SettingData.SetData(SETTING.MUSIC, !SettingData.settingData[SETTING.MUSIC]);
        musicOn.SetActive(SettingData.settingData[SETTING.MUSIC]);
        SoundManager.Instance.ApplySoundSetting();
    }
    
    private void OnClickSound()
    {
        SettingData.SetData(SETTING.SOUND, !SettingData.settingData[SETTING.SOUND]);
        soundOn.SetActive(SettingData.settingData[SETTING.SOUND]);
        SoundManager.Instance.ApplySoundSetting();
    }
    
    private void OnClickVibration()
    {
        SettingData.SetData(SETTING.VIBRATE, !SettingData.settingData[SETTING.VIBRATE]);
        vibrationOn.SetActive(SettingData.settingData[SETTING.VIBRATE]);
    }
    
    
}
