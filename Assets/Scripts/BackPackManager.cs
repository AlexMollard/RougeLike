using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackPackManager : MonoBehaviour
{
    RectTransform rectTransform;
    public InventoryManager invManager;
    public ItemBehavior testObject;
    Vector2 gridSize = new Vector2(5,5);
    float space = 80.0f;
    InventorySlot[] slots;

    int currentIndex = 0;
    int currentRow = 4;
    int currentCol = -4;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        transform.parent.gameObject.SetActive(false);
        slots = GetComponentsInChildren<InventorySlot>();
    }

    public void SnapToGrid(InventorySlot slot)
    {
        //BROKEN
        Vector3 currentPos = slot.transform.localPosition;
        slot.transform.localPosition = new Vector3(
            (currentPos.x / gridSize.x) * 5f,
            (currentPos.y / gridSize.y) * 5f
            );
    }


    public void RemoveItem(InventorySlot item)
    {
        Destroy(item.gameObject);
    }

    public void AddItem(ItemBehavior item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].setAttributes(item);
                break;
            }
        }
    }

    public void SortInv()
    {
        
    }
}
