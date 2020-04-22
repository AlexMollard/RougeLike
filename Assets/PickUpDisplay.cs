using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickUpDisplay : MonoBehaviour
{
    public TextMeshProUGUI itemName;

    int amountINT;
    string itemNameSTRING;

    public int itemID;

    Vector3 startPos;
    Vector3 endPos;
    float timer = 0.0f;

    bool DeleteMe = false;
    public void UpdateMovement()
    {
        timer += Time.deltaTime * 1.5f;

        transform.position = Vector3.Lerp(startPos, endPos, timer);
        transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, timer - 1);

        if (timer > 1.99f)
            DeleteMe = true;
    }

    public void AddAmount(int newAmount)
    {
        amountINT += newAmount;
        UpdateText();
        timer = 0.0f;
    }

    public void SetAttributes(ItemBehavior item, Vector3 startingPos)
    {
        startPos = startingPos;
        endPos = startPos + new Vector3(0,40);
        itemID = item.GetID();
        amountINT = item.GetAmount();
        itemNameSTRING = item.GetName();
        UpdateText();
    }

    void UpdateText()
    {
        itemName.text = amountINT.ToString() + "x " + itemNameSTRING;
    }

    public bool GetDeleteMe()
    {
        return DeleteMe;
    }
}
