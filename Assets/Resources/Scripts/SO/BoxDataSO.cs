using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Data Box", menuName = "ScriptableObjects/BoxDataSO", order = 1)]
public class BoxDataSO : ScriptableObject
{
      public BoxData[] BoxData = new BoxData[3];
      
}
[Serializable]
public class BoxData
{
   public ItemData[] RowData = new ItemData[3];      
}
    
[Serializable]
public class ItemData
{
    public ItemType itemType;
    public Sprite sprite;
}