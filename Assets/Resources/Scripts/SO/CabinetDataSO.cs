using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CabinetData", menuName = "CabinetData")]
public class CabinetDataSO : ScriptableObject
{
    public BoxDataSO[,] boxData = new BoxDataSO[3,4];
}
