using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{
    public GameObject itemImage;
    float timer = 0.0f;
    public float displayTime = 1.5f;


    public void Start()
    {
        gameObject.SetActive(false);
    }

    public void Update()
    {
        timer += Time.deltaTime;

        if (timer > displayTime)
            gameObject.SetActive(false);
        
    }

    public void DisplayItem(ItemBehavior item)
    {
        timer = 0.0f;
        itemImage.GetComponent<Image>().sprite = item.invIcon;
        gameObject.SetActive(true);
    }
}
