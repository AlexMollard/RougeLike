using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public GameObject playerObject;
    public float movementSpeed = 1;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerObject = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //if (Vector2.Distance(transform.position, playerObject.transform.position) < 1.0f)
        //{
        //    transform.position = Vector2.MoveTowards(transform.position, playerObject.transform.position, movementSpeed * Time.deltaTime);
        //}
    }
}
