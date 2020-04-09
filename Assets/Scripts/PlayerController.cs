using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 1.0f;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        cam.transform.position = new Vector3(transform.position.x, transform.position.y, cam.transform.position.z);

        Vector2 updatedPos = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            updatedPos += new Vector2(0, movementSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            updatedPos += new Vector2(0, -movementSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            updatedPos += new Vector2(movementSpeed * Time.deltaTime, 0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            updatedPos += new Vector2(-movementSpeed * Time.deltaTime,0);
        }

        GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity + updatedPos;
    }
}
