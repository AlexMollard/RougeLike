using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingSpotBehaviour : MonoBehaviour
{
    public GameObject[] fish;
    public int lastCommonIndex;
    public int lastUnCommonIndex;


    public GameObject GetFish()
    {

        float chance = Random.value;

        if (chance < 0.75f)
        {
            return fish[Random.Range(0, lastCommonIndex)];
        }
        else if (chance < 0.9f)
        {
            return fish[Random.Range(lastCommonIndex, lastUnCommonIndex)];
        }
        else
        {
            return fish[Random.Range(lastUnCommonIndex, fish.Length)];
        }


    }
}
