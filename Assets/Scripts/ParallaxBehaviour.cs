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
    Vector2 spawnPos;

    private void Start()
    {
        spawnPos = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        if (playerObject == null)
            playerObject = GameObject.FindWithTag("Player");

        movement.x = -playerObject.transform.position.x * speed;
        movement.y = -playerObject.transform.position.y * speed;
        transform.position = movement + spawnPos;

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
