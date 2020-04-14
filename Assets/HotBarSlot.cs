using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotBarSlot : MonoBehaviour
{
    public Sprite icon;
    public ItemBehavior item;
    public Image image;
    public GameObject border;
    public bool selected;

    public void updateSelection(bool isSelected)
    {
        selected = isSelected;

        if (isSelected)
        {
            border.GetComponent<RectTransform>().sizeDelta = new Vector2(110,110);
        }
        else
        {
            border.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
        }
    }

    public void SetItem(ItemBehavior newItem, Sprite newIcon)
    {
        icon = newIcon;
        item = newItem;

        image.sprite = icon;
    }
}
