using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    public bool isTrue = false;
    float timer;

    public void Update()
    {
        if (timer < 0.1f)
        {
            timer += Time.deltaTime;
            isTrue = false;
        }
    }

    private void OnMouseOver()
    {
        isTrue = true;
        timer = 0.0f;
    }
}
