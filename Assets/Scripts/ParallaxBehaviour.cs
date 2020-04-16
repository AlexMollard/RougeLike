using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBehaviour : MonoBehaviour
{
    public Camera cam;
    public Rigidbody2D rb;
    public float speed;
    Vector2 movement;
    GameObject playerObject;
    float timePassed = 0.0f;

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime / 4;

        if (playerObject == null)
            playerObject = GameObject.FindWithTag("Player");

        float time = Time.time;
        time = time / 10;

        movement.x = Mathf.PerlinNoise(time, time) + timePassed;
        movement.y = Mathf.PerlinNoise(time + 3, time + 3) + timePassed;
        transform.position = movement;

        if (Vector2.Distance(transform.position,playerObject.transform.position) > 5.0f)
        {
            // Input rebound here
        }
    }

    private void FixedUpdate()
    {
       // rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
