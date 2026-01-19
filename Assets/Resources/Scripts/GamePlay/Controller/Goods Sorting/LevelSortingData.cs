using System;
using System.Collections.Generic;
using UnityEngine;

namespace Resources.Scripts.GamePlay.Controller.Goods_Sorting
{
    [CreateAssetMenu (fileName = "Data Goods Sorting", menuName = "ScriptableObjects/GoodsSortingSO", order = 1)]
    public class LevelSortingData : ScriptableObject
    {
        [Header("Grid settings (rows x columns) - editable per asset")]
        [Min(1)] [SerializeField] private int rowsPerBox = 3;
        [Min(1)] [SerializeField] private int itemsPerRow = 3;

        [SerializeField] private List<BoxData> listBoxItem = new List<BoxData>();
        public List<BoxData> ListBoxData => listBoxItem;

        // Ensure the data shape matches configured rows/items per box when edited in the inspector
        private void OnValidate()
        {
            if (listBoxItem == null)
                listBoxItem = new List<BoxData>();

            if (listBoxItem.Count == 0)
                listBoxItem.Add(new BoxData());

            for (int i = 0; i < listBoxItem.Count; i++)
            {
                if (listBoxItem[i] == null)
                {
                    // replace null entry with a new initialized box
                    listBoxItem[i] = new BoxData();
                    continue;
                }

                listBoxItem[i].EnsureSize(rowsPerBox, itemsPerRow);
            }
        }

        // Public helpers to make runtime usage & maintenance easier
        public ItemData GetItem(int boxIndex, int row, int col)
        {
            if (!IsValidIndices(boxIndex, row, col))
                return null;

            return listBoxItem[boxIndex].rowData[row].itemData[col];
        }

        public bool SetItem(int boxIndex, int row, int col, ItemData data)
        {
            if (!IsValidIndices(boxIndex, row, col))
                return false;

            listBoxItem[boxIndex].rowData[row].itemData[col] = data ?? new ItemData();
            return true;
        }

        public void ResizeAllBoxes(int newRows, int newCols)
        {
            if (newRows < 1) newRows = 1;
            if (newCols < 1) newCols = 1;

            rowsPerBox = newRows;
            itemsPerRow = newCols;

            foreach (var box in listBoxItem)
                box.EnsureSize(rowsPerBox, itemsPerRow);
        }

        private bool IsValidIndices(int boxIndex, int row, int col)
        {
            if (listBoxItem == null) return false;
            if (boxIndex < 0 || boxIndex >= listBoxItem.Count) return false;
            var b = listBoxItem[boxIndex];
            if (b.rowData == null) return false;
            if (row < 0 || row >= b.rowData.Count) return false;
            var r = b.rowData[row];
            if (r.itemData == null) return false;
            if (col < 0 || col >= r.itemData.Count) return false;
            return true;
        }
    }

    [Serializable]
    public class BoxData
    {
        public List<RowData> rowData = new List<RowData>();

        public void EnsureSize(int rows, int itemsPerRow)
        {
            if (rowData == null)
                rowData = new List<RowData>();

            // add missing rows
            while (rowData.Count < rows)
                rowData.Add(new RowData());

            // remove extra rows
            while (rowData.Count > rows)
                rowData.RemoveAt(rowData.Count - 1);

            // ensure each row has the correct number of items
            for (int i = 0; i < rowData.Count; i++)
            {
                if (rowData[i] == null)
                {
                    rowData[i] = new RowData();
                    continue;
                }

                rowData[i].EnsureSize(itemsPerRow);
            }
        }
    }

    [Serializable]
    public class RowData
    {
        public List<ItemData> itemData = new List<ItemData>();

        public void EnsureSize(int itemsPerRow)
        {
            if (itemData == null)
                itemData = new List<ItemData>();

            // add missing items
            while (itemData.Count < itemsPerRow)
                itemData.Add(new ItemData());

            // remove extra items
            while (itemData.Count > itemsPerRow)
                itemData.RemoveAt(itemData.Count - 1);
        }
    }

    [Serializable]
    public class ItemData
    {
        public ItemType itemType;
        public Sprite sprite;
    }
}