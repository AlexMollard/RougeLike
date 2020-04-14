﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<ItemBehavior> items;
    public HotBarSlot[] HotBar;
    public HotBarSlot currentHotBar;
    public BackPackManager backPack;
    public PlayerController player;
    public int currentIndex;

    public void UpdateHotBar()
    {
        for (int i = 0; i < HotBar.Length; i++)
        {
            HotBar[i].updateSelection((i == currentIndex) ? true : false);
        }
    }

    public bool AddToHotBar(ItemBehavior item)
    {
        for (int i = 0; i < HotBar.Length; i++)
        {
            if (HotBar[i].item == null)
            {
                HotBar[i].SetItem(item);
                return true;
            }
        }

        return false;
    }

    public void MoveHotBarToRight()
    {
        currentIndex++;

        if (currentIndex == HotBar.Length)
            currentIndex = 0;

        currentHotBar = HotBar[currentIndex];
        UpdateHotBar();
        UpdateHeldItem();
    }

    public void MoveHotBarToLeft()
    {
        currentIndex--;

        if (currentIndex < 0)
            currentIndex = HotBar.Length - 1;

        currentHotBar = HotBar[currentIndex];
        UpdateHotBar();
        UpdateHeldItem();
    }

    public void UpdateHeldItem()
    {
        if (HotBar[currentIndex].item != null)
        {
            player.EquipedItem.currentObject = HotBar[currentIndex].item.gameObject;
            player.heldObject = HotBar[currentIndex].item.gameObject;
        }
        else
        {
            player.EquipedItem.currentObject = null;
            player.heldObject = null;
        }
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
        if (Contains(item.ID))
        {
            if (Get(item.ID).amount < 99)
            {
                Get(item.ID).amount += item.amount;
                Destroy(item.gameObject);
                return;
            }
            else
            {
                return;
            }
        }

        item.gameObject.SetActive(false);
        items.Add(item);

        if (!AddToHotBar(item))
        {
            backPack.AddItem(item);
        }
    }

    public void CreateNewStack(ItemBehavior item)
    {

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

    public ItemBehavior SetHotBarFromInv(int index, InventorySlot slot)
    {
        ItemBehavior returnItem = HotBar[index].item;
        HotBar[index].SetItem(slot.item);
        UpdateHeldItem();
        return returnItem;
    }
}
