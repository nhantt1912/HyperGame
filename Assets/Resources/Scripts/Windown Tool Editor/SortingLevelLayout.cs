using UnityEngine;

[CreateAssetMenu(menuName = "GoodSorting/Sorting Level Layout")]
public class SortingLevelLayout : ScriptableObject
{
    public const int Width = 3;
    public const int Height = 4;

    public BoxDataSO[] boxes = new BoxDataSO[Width * Height];

    public BoxDataSO GetBox(int x, int y)
    {
        return boxes[y * Width + x];
    }

    public void SetBox(int x, int y, BoxDataSO box)
    {
        boxes[y * Width + x] = box;
    }
}