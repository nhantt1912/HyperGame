using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PuzzleCategory : MonoBehaviour
{
    [SerializeField] private LevelItem puzzleLevelItem;
    [SerializeField] private Transform content;
    public void Init()
    {
        LevelItem puzzleItem = Instantiate(puzzleLevelItem, content);
        //  handMadeItem.Init();
    }
}
