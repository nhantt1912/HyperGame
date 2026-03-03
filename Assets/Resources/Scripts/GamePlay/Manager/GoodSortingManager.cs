using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEngine;

public class GoodSortingManager : MonoBehaviour
{
   [SerializeField,ReadOnly] private int _levelCurrent;
    public int LevelCurrent => _levelCurrent;
    
    private void Start()
    {
        _levelCurrent = 1;
    }
}
