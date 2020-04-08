using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 updatedPos = transform.position;
        if (Input.GetKey(KeyCode.W))
        {
            updatedPos = new Vector3(updatedPos.x, updatedPos.y + (movementSpeed * Time.deltaTime));
        }

        if (Input.GetKey(KeyCode.S))
        {
            updatedPos = new Vector3(updatedPos.x, updatedPos.y - (movementSpeed * Time.deltaTime));
        }

        if (Input.GetKey(KeyCode.D))
        {
            updatedPos = new Vector3(updatedPos.x + (movementSpeed * Time.deltaTime), updatedPos.y);
        }

        if (Input.GetKey(KeyCode.A))
        {
            updatedPos = new Vector3(updatedPos.x - (movementSpeed * Time.deltaTime), updatedPos.y);
        }

        transform.position = updatedPos;
    }
}
