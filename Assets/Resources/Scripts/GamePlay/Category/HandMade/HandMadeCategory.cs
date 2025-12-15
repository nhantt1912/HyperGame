using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class HandMadeCategory : MonoBehaviour
{
    [SerializeField] private LevelItem handMadeLevelItem;
    [SerializeField] private Transform content;
    public void Init()
    {
       LevelItem handMadeItem = Instantiate(handMadeLevelItem, content);
     //  handMadeItem.Init();
    }
}
