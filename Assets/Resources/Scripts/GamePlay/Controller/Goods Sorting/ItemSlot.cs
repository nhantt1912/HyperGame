using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    public Item CurrentItem;

    public bool IsEmpty => CurrentItem == null;

    public void SetItem(Item item)
    {
        CurrentItem = item;

        item.transform.position = transform.position;
    }

    public void Clear()
    {
        CurrentItem = null;
    }
}