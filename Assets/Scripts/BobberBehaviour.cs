using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobberBehaviour : MonoBehaviour
{
    public float cosineAmount = 0.01f;
    public Vector2 orginalPos;
    float count = 0.0f;
    Vector3[] point = new Vector3[3];
    public bool bite = false;
    public bool inWater = false;
    public bool inFishingSpot = false;
    float timer = 0.0f;
    ParticleSystem ps;
    public void SetPoints(Vector2 pointOne, Vector2 pointTwo, Vector2 pointThree)
    {
        point[0] = pointOne;
        point[1] = pointTwo;
        point[2] = pointThree;
        ps = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        // If water is not in water dont bob or have particles
        //Add more particle textures


        if (count < 1.0f)
        {
            count += 1.0f * Time.deltaTime;

            Vector3 m1 = Vector3.Lerp(point[0], point[1], count);
            Vector3 m2 = Vector3.Lerp(point[1], point[2], count);
            transform.position = Vector3.Lerp(m1, m2, count);
        }
        else if (inFishingSpot)
        {
            if (bite)
            {
                timer += Time.deltaTime * 30;
                transform.position = new Vector2(point[2].x, point[2].y + Mathf.Cos(timer) * (cosineAmount / 2));

                ps.emissionRate = 20;
            }
            else
            {
                timer += Time.deltaTime;

                transform.position = new Vector2(point[2].x, point[2].y + Mathf.Cos(timer) * cosineAmount);

                ps.emissionRate = 1.5f;
            }
        }
        else
        {
            ps.emissionRate = 0.0f;
        }
    }
}
