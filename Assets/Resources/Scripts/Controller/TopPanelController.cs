using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TopPanelController : MonoBehaviour
{
    [SerializeField] private Button _settingButton;
     [SerializeField] private Button _backButton;

     public Action OnBackAction;
     public Action OnSettingAction;
     
     private void Start()
     {
         _settingButton.onClick?.AddListener(OnSetting);
         _backButton.onClick?.AddListener(OnBack);
     }
     
     private void OnSetting()
     {
         OnSettingAction?.Invoke();
     }
     
     private void OnBack()
     {
         OnBackAction?.Invoke();
     }

    
}
