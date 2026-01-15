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
        
    private void Start()
    {
        exitButton.onClick?.AddListener(OnHide);
        musicButton.onClick?.AddListener(OnClickMusic);
        soundButton.onClick?.AddListener(OnClickSound);
        vibrationButton.onClick?.AddListener(OnClickVibration);
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
