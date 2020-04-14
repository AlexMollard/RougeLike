using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackPackManager : MonoBehaviour
{
    List<InventorySlot> backPackItems = new List<InventorySlot>();
    public GameObject slotPrefab;
    RectTransform rectTransform;
    public InventoryManager invManager;
    public ItemBehavior testObject;
    Vector2 gridSize = new Vector2(5,5);
    float space = 80.0f;


    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        transform.parent.gameObject.SetActive(false);
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
        backPackItems.Remove(item);
        Destroy(item.gameObject);
    }

    public void AddItem(ItemBehavior item)
    {
        GameObject currentObject = Instantiate(slotPrefab,transform);
        InventorySlot currentSlot = currentObject.GetComponent<InventorySlot>();

        float xSize = rectTransform.sizeDelta.x * 0.9f;
        float ySize = rectTransform.sizeDelta.y * 0.9f;


        currentObject.transform.localPosition = new Vector3(Random.Range(0, xSize) - xSize * 0.5f, Random.Range(0, ySize) - ySize * 0.5f);
        backPackItems.Add(currentSlot);
        currentSlot.setAttributes(item);
        CheckAllOverLaps();
    }

    public void CheckAllOverLaps()
    {

    }

    public void SortInv()
    {
        int row = 5;
        int col = -4;
        int size = backPackItems.Count;
        List<int> tempList = new List<int>();
        List<InventorySlot> tempSlots = new List<InventorySlot>();

        for (int i = 0; i < backPackItems.Count; i++)
        {
            if (tempList.Contains(backPackItems[i].item.ID))
                tempSlots.Add(backPackItems[i]);
            else
                tempList.Add(backPackItems[i].item.ID);

            if (i % 9 == 0)
            {
                row--;
                col = -4;
            }


            backPackItems[i].transform.localPosition = new Vector3(col * 80.0f, row * 80.0f - 40.0f,0);
                col++;
        }

        if (tempSlots.Count > 0)
        {
            for (int i = 0; i < tempSlots.Count; i++)
            {
                invManager.Add(tempSlots[i].item);
                RemoveItem(tempSlots[i]);
            }
        }
    }
}
