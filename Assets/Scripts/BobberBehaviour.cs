using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobberBehaviour : MonoBehaviour
{
    public float cosineAmount = 0.01f;
    public Vector2 orginalPos;
    float count = 0.0f;
    Vector3[] point = new Vector3[3];
    float throwTime;

    public void SetPoints(Vector2 pointOne, Vector2 pointTwo, Vector2 pointThree)
    {
        point[0] = pointOne;
        point[1] = pointTwo;
        point[2] = pointThree;
        throwTime = Time.realtimeSinceStartup;
    }

    void Update()
    {
        if (count < 1.0f)
        {
            count += 1.0f * Time.deltaTime;

            Vector3 m1 = Vector3.Lerp(point[0], point[1], count);
            Vector3 m2 = Vector3.Lerp(point[1], point[2], count);
            transform.position = Vector3.Lerp(m1, m2, count);
        }
        else
        {
            transform.position = new Vector2(point[2].x, point[2].y + Mathf.Cos(Time.time + throwTime) * cosineAmount);
        }
    }
}
