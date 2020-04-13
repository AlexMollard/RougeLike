using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotBarSlot : MonoBehaviour
{
    public Sprite icon;
    public ItemBehavior item;

    public Image image;

    public void SetItem(ItemBehavior newItem, Sprite newIcon)
    {
        icon = newIcon;
        item = newItem;

        image.sprite = icon;
    }
}
