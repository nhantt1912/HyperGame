using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CategoryManager : UIBase
{
    [SerializeField] private LevelItem levelItem;
    [SerializeField] private GoodsSortingController _goodSortingUI;
    [SerializeField] private Transform content;

    [SerializeField] private HandMadeLevelData _handMadeLevelData;
    [SerializeField] private SatisLevelData _satisLevelData;
    
    private bool _isActive;
    
    protected override void Awake()
    {
        base.Awake();
        EventManager.AddListener<EventDefine.OnSelectLevel>(OnSelectLevel);
    }

    private void OnSelectLevel(EventDefine.OnSelectLevel obj)
    {
        OnHide();
    }

    public bool ISActive()
    {
        _isActive = canvasGroup.alpha == 1 ? true : false;
        return _isActive;
    }

    public void SetUp(MenuType type)
    {
        ClearAllLevelItem();
        _goodSortingUI.Active(type == MenuType.GoodSorting);
        
        switch (type)
        {
            case MenuType.HandeMade:
                
                for (int i = 0; i < _handMadeLevelData.ListHandMadeLevel.Count; i++)
                {
                    LevelItem handMadeItem = Instantiate(levelItem, content);
                    handMadeItem.Init(i,"Level " + (i + 1),type );
                }
                
                // data
                break;
            
            case MenuType.Satis:
                
                for (int i = 0; i < _satisLevelData.ListSatisLevel.Count; i++)
                {
                    LevelItem puzzleItem = Instantiate(levelItem, content);
                    puzzleItem.Init(i,"Level " + (i + 1),type );
                }
                
                // data
                break;
        }
    }

    public override void OnHide()
    {
        base.OnHide();
       // ClearAllLevelItem();
    }

    private void ClearAllLevelItem()
    {
        while (content.childCount > 0)
        {
            DestroyImmediate(content.GetChild(0).gameObject);
        }
    }
}
