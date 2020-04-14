using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    
    public GameObject heldObject;
    public EquipedItemBehavior EquipedItem;

    public bool isShadow = false;

    public float movementSpeed = 5.0f;
    public Camera cam;
    float speed;
    float isRight = 1.0f;
    public List<List<GameObject>> tiles;

    public InventoryManager Inventory;

    Vector2 movement;
    int woodTotal = 0;
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

        if (movement.x != 0)
            isRight = movement.x;

        if (heldObject)
            heldObject.transform.localPosition = new Vector2(0.05f * -isRight, heldObject.transform.localPosition.y);

        if (Input.GetMouseButtonDown(0) && EquipedItem.canUse)
        {
            EquipedItem.canUse = false;
            EquipedItem.Use();
        }

        if (Input.mouseScrollDelta.y > 0 && EquipedItem.canUse)
        {
            GameObject tempObject = Inventory.MoveHotBarToRight();
            if (tempObject != null)
            {
                heldObject = tempObject;
                EquipedItem.currentObject = heldObject;
            }
            else
            {
                heldObject = null;
                EquipedItem.currentObject = null;
            }

        }

        if (Input.mouseScrollDelta.y < 0 && EquipedItem.canUse)
        {
            GameObject tempObject = Inventory.MoveHotBarToLeft();
            if (tempObject != null)
            {
                heldObject = tempObject;
                EquipedItem.currentObject = heldObject;
            }
            else
            {
                heldObject = null;
                EquipedItem.currentObject = null;
            }
        }

        cam.transform.position = new Vector3(transform.position.x, transform.position.y, -5);
    }

    private void FixedUpdate()
    {
        if (!isShadow)
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    public bool PickUp(ItemBehavior item)
    {
        if (!Inventory.Contains(item.ID))
        {
            Inventory.Add(item);
            Inventory.AddToHotBar(item);
            item.gameObject.SetActive(false);
            return true;
        }

        Destroy(item.gameObject);
        Inventory.Get(item.ID).amount++;

        return true;
    }
}
