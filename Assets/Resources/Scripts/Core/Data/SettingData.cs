using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class SettingData : MonoBehaviour
{
    public static Dictionary<SETTING, bool> settingData;
    private const string KEY_SETTING = "SETTING";

    private void Awake()
    {
        LoadSetting();
    }

    private void LoadSetting()
    {
        if (!PlayerPrefs.HasKey(KEY_SETTING))
        {
            settingData = new Dictionary<SETTING, bool>()
            {
                {SETTING.SOUND,true},
                {SETTING.MUSIC,true},
                {SETTING.VIBRATE,true}
            }; 
            SaveData();           
            return;
        }

        var saveData = PlayerPrefs.GetString(KEY_SETTING);
        settingData = JsonConvert.DeserializeObject<Dictionary<SETTING, bool>>(saveData);
    }

    static void SaveData()
    {
        string saveData = JsonConvert.SerializeObject(settingData);
        PlayerPrefs.SetString(KEY_SETTING,saveData);
    }

    public static void SetData(SETTING setting, bool value)
    {
        settingData[setting] = value;
        SaveData();
    }
    
    }

public enum SETTING
{
    SOUND,
    MUSIC,
    VIBRATE
}
