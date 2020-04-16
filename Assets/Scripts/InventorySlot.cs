using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventorySlot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public RayCastManager canvasRayCaster;
    
    public Sprite icon;
    public ItemBehavior item;
    public Image image;
    public bool _isDragging;
    public TextMeshProUGUI amoutText;
    RectTransform draggedItemRect;
    Vector3 spawnPoint;
    Vector3 pickUpPoint;
    BackPackManager backPack;

    private void Start()
    {
        backPack = transform.parent.GetComponent<BackPackManager>();
        draggedItemRect = GetComponent<RectTransform>();
        spawnPoint = draggedItemRect.localPosition;
        getCanvas(transform.parent.gameObject);
    }

    void getCanvas(GameObject g)
    {
        if (g.GetComponent<RayCastManager>() != null)
        {
            canvasRayCaster = g.GetComponent<RayCastManager>();
            return;
        }
        else
        {
            if (g.transform.parent != null)
            {
                getCanvas(g.transform.parent.gameObject);
            }
        }
    }
    public void setAttributes(ItemBehavior newItem)
    {
        if (newItem == null)
        {
            backPack.RemoveItem(this);
            return;
        }
        item = newItem;
        icon = item.invIcon;
        image.sprite = item.invIcon;
    }

    private void Update()
    {
        if (_isDragging)
        {
            Vector2 localPosition = Vector2.zero;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(draggedItemRect, Input.mousePosition, null, out localPosition);

            draggedItemRect.position = draggedItemRect.TransformPoint(localPosition);
        }

        if (Input.GetKey(KeyCode.Tab))
        {
            draggedItemRect.localPosition = spawnPoint;
        }

        if (item.amount > 1)
        {
            amoutText.gameObject.SetActive(true);
            amoutText.text = item.amount.ToString();
        }
        else
            amoutText.gameObject.SetActive(false);
    
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isDragging = true;
        pickUpPoint = transform.localPosition;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isDragging = false;
        if (canvasRayCaster.rayCast(backPack.gameObject))
        {
            //backPack.SnapToGrid(this);    // Needs to be fixed
            return;
        }
        else
        {
            for (int i = 0; i < backPack.invManager.HotBar.Length; i++)
            {
                if (canvasRayCaster.rayCast(backPack.invManager.HotBar[i].gameObject))
                {
                    setAttributes(backPack.invManager.SetHotBarFromInv(i, this));
                }
            }
        }

        transform.localPosition = pickUpPoint;
    }
}
