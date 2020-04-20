using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickUpDisplay : MonoBehaviour
{
    public TextMeshProUGUI amount;
    public TextMeshProUGUI itemName;
    
    Vector2 startPos;
    Vector2 endPos;
    float timer = 0.0f;

    bool DeleteMe = false;
    public void UpdateMovement()
    {
        timer += Time.deltaTime;

        transform.position = Vector2.Lerp(startPos, endPos, timer);
        transform.localScale = Vector2.Lerp(Vector2.one, Vector2.zero, timer);

        if (timer > 0.99f)
            DeleteMe = true;
    }

    public void SetAttributes(ItemBehavior item, Vector2 startingPos)
    {
        startPos = startingPos;
        endPos = startPos + new Vector2(0,1);
        amount.text = item.GetAmount().ToString();
        itemName.text = item.GetName();
    }

    public bool GetDeleteMe()
    {
        return DeleteMe;
    }
}
