using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<ItemBehavior> items;
    public HotBarSlot[] HotBar;

    public bool AddToHotBar(ItemBehavior item)
    {
        for (int i = 0; i < 4; i++)
        {
            if (HotBar[i].item == null)
            {
                HotBar[i].SetItem(item, item.invIcon);
                return true;
            }
        }

        return false;
    }

    public void MoveHotBarToRight()
    {
        HotBarSlot tempSlot = HotBar[3];
        for (int i = 1; i < 4; i++)
        {
            HotBar[i] = HotBar[i - 1];
        }
        HotBar[0] = tempSlot;
    }

    public void MoveHotBarToLeft()
    {
        HotBarSlot tempSlot = HotBar[0];
        for (int i = 0; i > 3; i++)
        {
            HotBar[i + 1] = HotBar[i];
        }
        HotBar[3] = tempSlot;
    }

    public bool Contains(int ID)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].ID == ID)
                return true;
        }
        return false;
    }

    public void Add(ItemBehavior item)
    {
        items.Add(item);
    }

    public ItemBehavior Get(int ID)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].ID == ID)
                return items[i];
        }
        return null;
    }
}
