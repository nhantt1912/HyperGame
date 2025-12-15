using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MenuManager : UIBase
{
    [SerializeField] private List<MenuItem> menuItems;

    public Action<MenuType> onClickType;

    protected override void Awake()
    {
        base.Awake();
        menuItems.ForEach(menuItem => menuItem.onClick = OnClickMenuItem);

}

    private void OnClickMenuItem(MenuType type)
    {
        onClickType?.Invoke(type);
        OnHide();
    }

    
    
}
