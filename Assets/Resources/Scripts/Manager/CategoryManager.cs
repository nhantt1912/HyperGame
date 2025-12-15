using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CategoryManager : UIBase
{
    [SerializeField] private LevelItem levelItem;
    [SerializeField] private Transform content;

    protected override void Awake()
    {
        base.Awake();
        EventManager.AddListener<EventDefine.OnSelectLevel>(OnSelectLevel);
    }

    private void OnSelectLevel(EventDefine.OnSelectLevel obj)
    {
        OnHide();
    }

    public void SetUp(MenuType type)
    {
        switch (type)
        {
            case MenuType.HandeMade:
                
                for (int i = 1; i < 11; i++)
                {
                    LevelItem handMadeItem = Instantiate(levelItem, content);
                    handMadeItem.Init(i,"Level " + i );
                }
                
                // data
                break;
            
            case MenuType.Puzzle:
                
                for (int i = 1; i < 16; i++)
                {
                    LevelItem puzzleItem = Instantiate(levelItem, content);
                    puzzleItem.Init(i,"Level " + i );
                }
                
                // data
                break;
        }
    }

    public override void OnHide()
    {
        base.OnHide();
        
        while (content.childCount > 0)
        {
            DestroyImmediate(content.GetChild(0).gameObject);
        }


    }
}
