using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (Random.value > 0.1f)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Animator>().enabled = false;
            return;
        }

        GetComponent<Animator>().SetInteger("index", Random.Range(0,3));
    }
}
