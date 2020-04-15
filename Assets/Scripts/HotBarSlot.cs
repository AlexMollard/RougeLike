using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HotBarSlot : MonoBehaviour
{
    public Sprite icon;
    public ItemBehavior item;
    public Image image;
    public GameObject border;
    public bool selected;
    public TextMeshProUGUI amoutText;

    private void Update()
    {

        if (item)
        {
            if (item.amount > 1)
            {
                amoutText.text = item.amount.ToString();
                amoutText.gameObject.SetActive(true);
            }
            else
            {
                amoutText.gameObject.SetActive(false);
            }
        }
        else
        {
            amoutText.gameObject.SetActive(false);
        }
    }

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

    public void SetItem(ItemBehavior newItem)
    {
        item = newItem;
        icon = item.invIcon;

        image.sprite = icon;
    }
}
