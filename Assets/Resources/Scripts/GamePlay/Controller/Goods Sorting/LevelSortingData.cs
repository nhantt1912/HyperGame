using System;
using System.Collections.Generic;
using UnityEngine;

namespace Resources.Scripts.GamePlay.Controller.Goods_Sorting
{
    [CreateAssetMenu(fileName = "Data Goods Sorting", menuName = "ScriptableObjects/GoodsSortingSO", order = 1)]
    public class LevelSortingData : ScriptableObject
    {
        [SerializeField] private int rowsPerBox = 3;
        [SerializeField] private int itemsPerRow = 3;

        public List<BoxData> boxes = new List<BoxData>();
    }

    [Serializable]
    public class BoxData
    {
        public SlotData[] slots = new SlotData[3];
    }
    
    [Serializable]
    public class SlotData
    {
        public ItemType itemType;
    }
}