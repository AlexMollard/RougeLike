using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRodBehaviour : MonoBehaviour
{
    bool isCast = false;
    public GameObject bobbie;
    GameObject playerObject;
    GameObject boi;
    float timeInWater = 0.0f;
    public float minBiteTime;
    public float maxBiteTime;
    public float timeToPullIn = 1.0f;
    float biteTime = 1.0f;
    float pullInTimer = 0.0f;
    GameObject currentPool;
    bool inWater = false;
    private void Start()
    {
        playerObject = GameObject.FindWithTag("Player");
    }

    public void Cast()
    {
        if (isCast)
        {
            PullIn();
            return;
        }


        if (Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), playerObject.transform.position) > 1)
            return;

        boi = Instantiate(bobbie, playerObject.transform.position, Quaternion.identity);

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null)
        {
            if(hit.transform.GetComponent<FishingSpotBehaviour>())
            {
                currentPool = hit.transform.gameObject;
                HitWater();
            }
        }

        isCast = true;
        
        boi.GetComponent<Rope>().player = playerObject;
        boi.GetComponent<Rope>().rod = gameObject;


        Vector2 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector2 pointOne = playerObject.transform.position;

        Vector2 pointTwo = Vector2.Lerp(mousePos, pointOne, 0.5f);

        pointTwo.y += 0.5f;

        boi.GetComponent<BobberBehaviour>().SetPoints(pointOne, pointTwo, mousePos);
    
    }

    void HitWater()
    {
        timeInWater = 0.0f;
        biteTime = Random.Range(minBiteTime, maxBiteTime);
        inWater = true;
        boi.GetComponent<BobberBehaviour>().inFishingSpot = true;
    }

    void PullIn()
    {
        if (inWater == true)
        {
            if (timeInWater > biteTime && pullInTimer < timeToPullIn)
            {
                GameObject fish = currentPool.GetComponent<FishingSpotBehaviour>().GetFish();
                Instantiate(fish, playerObject.transform.position, Quaternion.identity);
            }
        }

        inWater = false;
        pullInTimer = 0.0f;
        isCast = false;
        Destroy(boi);
    }

    public void UpdateTimers()
    {
        timeInWater += Time.deltaTime;

        if (timeInWater > biteTime)
        {
            boi.GetComponent<BobberBehaviour>().bite = true;
            pullInTimer += Time.deltaTime;
            
            if (pullInTimer > timeToPullIn)
                boi.GetComponent<BobberBehaviour>().bite = false;
        }
    }

    public void TestDistance()
    {
        if (Vector2.Distance(boi.transform.position, playerObject.transform.position) > 1)
        {
            PullIn();
        }
    }
}
