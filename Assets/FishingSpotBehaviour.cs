using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingSpotBehaviour : MonoBehaviour
{
    public GameObject[] fish;

    public GameObject GetFish()
    {
        return fish[Random.Range(0,fish.Length)];
    }
}
