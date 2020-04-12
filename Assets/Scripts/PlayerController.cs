using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public bool isShadow = false;

    public float movementSpeed = 5.0f;
    public Camera cam;
    float speed;

    public List<List<GameObject>> tiles;

    Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            speed = movementSpeed * 2;
        else
            speed = movementSpeed;

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");


        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (isShadow)
            return;
        
        cam.transform.position = new Vector3(transform.position.x, transform.position.y, -5);
    }

    private void FixedUpdate()
    {
        if (!isShadow)
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
