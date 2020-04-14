using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<ItemBehavior> items;
    public HotBarSlot[] HotBar;
    public HotBarSlot currentHotBar;
    public int currentIndex;

    public void UpdateHotBar()
    {
        for (int i = 0; i < 4; i++)
        {
            HotBar[i].updateSelection((i == currentIndex) ? true : false);
        }
    }

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

    public GameObject MoveHotBarToRight()
    {
        currentIndex++;

        if (currentIndex == 4)
            currentIndex = 0;

        currentHotBar = HotBar[currentIndex];
        UpdateHotBar();

        if (HotBar[currentIndex].item)
            return HotBar[currentIndex].item.gameObject;
        else
            return null;
    }

    public GameObject MoveHotBarToLeft()
    {
        currentIndex--;

        if (currentIndex < 0)
            currentIndex = 3;

        currentHotBar = HotBar[currentIndex];
        UpdateHotBar();

        if (HotBar[currentIndex].item)
            return HotBar[currentIndex].item.gameObject;
        else
            return null;
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
