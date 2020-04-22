using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{
    public GameObject popUpText;
    List<PickUpDisplay> pickUps = new List<PickUpDisplay>();
    List<PickUpDisplay> toBeDeleted = new List<PickUpDisplay>();
    public GameObject player;

    private void Update()
    {
        if (pickUps.Count > 0)
        {
            for (int i = 0; i < pickUps.Count; i++)
            {
                pickUps[i].UpdateMovement();
                if (pickUps[i].GetDeleteMe())
                {
                    toBeDeleted.Add(pickUps[i]);
                }
            }

            for (int i = 0; i < toBeDeleted.Count; i++)
            {
                pickUps.Remove(toBeDeleted[i]);
                Destroy(toBeDeleted[i].gameObject);
            }

            toBeDeleted.Clear();
        }
    }

    public void DisplayItem(ItemBehavior item)
    {
        for (int i = 0; i < pickUps.Count; i++)
        {
            if (pickUps[i].itemID == item.GetID())
            {
                pickUps[i].AddAmount(item.GetAmount());
                return;
            }
        }

        GameObject popUp = Instantiate(popUpText, transform);
        pickUps.Add(popUp.GetComponent<PickUpDisplay>());
        pickUps[pickUps.Count - 1].SetAttributes(item, Camera.main.WorldToScreenPoint(player.transform.position) + new Vector3(0,50,0));
    }
}
