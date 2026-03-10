using System;
using System.Collections.Generic;
using UnityEngine;

namespace Resources.Scripts.GamePlay.Controller.Goods_Sorting
{
    [CreateAssetMenu(fileName = "Data Goods Sorting", menuName = "ScriptableObjects/GoodsSortingSO", order = 1)]
    public class LevelSortingData : ScriptableObject
    {
        public int width = 3;
        public int height = 4;

        public BoxDataSO[] boxes;

        public BoxDataSO GetBox(int x, int y)
        {
            int flippedY = height - 1 - y;

            int index = x + flippedY * width;

            if (index < 0 || index >= boxes.Length)
                return null;

            return boxes[index];
        }
    }
}