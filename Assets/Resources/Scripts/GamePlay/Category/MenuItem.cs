using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuItem : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private MenuType menuType;
    
    public Action<MenuType> onClick;
    
    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick?.AddListener(OnClickMenuItem);
    }

    private void OnClickMenuItem()
    {
        onClick?.Invoke(menuType);
    }
    
    
}

[Serializable]
public enum MenuType
{
    None = 0,
    HandeMade = 1,
    Puzzle = 2,
    GoodSorting = 3,
}
