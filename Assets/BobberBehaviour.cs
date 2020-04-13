using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobberBehaviour : MonoBehaviour
{
    public float cosineAmount = 0.01f;
    public Vector2 orginalPos;

    private void Start()
    {
        orginalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(orginalPos.x,orginalPos.y + (Mathf.Cos(Time.time ) * cosineAmount), 1);
    }
}
