using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRodBehaviour : MonoBehaviour
{
    bool isCast = false;
    public GameObject bobbie;
    GameObject playerObject;
    GameObject fishingSpotPrefab;

    public void Cast()
    {
        if (isCast)
        {
            return;
        }

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null)
        {
            if(hit.transform.GetComponent<FishingSpotBehaviour>())
            {
                Debug.Log("Water ahoy");
            }
        }

        playerObject = GameObject.FindWithTag("Player");
        
        GameObject boi = Instantiate(bobbie);

        Vector2 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector2 pointOne = playerObject.transform.position;

        Vector2 pointTwo = Vector2.Lerp(mousePos, pointOne, 0.5f);

        pointTwo.y += 0.5f;

        boi.GetComponent<BobberBehaviour>().SetPoints(pointOne, pointTwo, mousePos);
    
    }
}
