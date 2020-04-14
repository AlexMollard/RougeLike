using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipedItemBehavior : MonoBehaviour
{
    Vector3 mouse_pos;//Assign to the object you want to rotate
    Vector3 object_pos;
    float angle;
    Camera cam;
   
    Vector3 normalScale = new Vector3(0.75f,0.75f,1);
    Vector3 flippedScale = new Vector3(0.75f,-0.75f,1);
    
    public bool canUse = true;
    
    Vector2 defaultPos;
    Vector2 targetPos;

    public float coolDown = 1.0f;
    public float coolDownTimer = 0.0f;

    public GameObject currentObject;
    public Sprite currentSprite;

    public bool hit = false;

    private void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (currentObject == null)
        {
            currentSprite = null;
            GetComponent<SpriteRenderer>().sprite = null;
        }
        else if (currentObject.GetComponent<SpriteRenderer>().sprite != currentSprite)
        {
            currentSprite = currentObject.GetComponent<SpriteRenderer>().sprite;
            GetComponent<SpriteRenderer>().sprite = currentSprite;
        }


        if (coolDownTimer > 0.0f)
        {
            coolDownTimer -= Time.deltaTime * 2;
            transform.localPosition = Vector3.Lerp(defaultPos, targetPos, coolDownTimer);
            return;
        }
        else
        {
            canUse = true;
            hit = false;
        }

        mouse_pos = Input.mousePosition;
        mouse_pos.z = Vector2.Distance(cam.transform.position, transform.position); //The distance between the camera and object
        object_pos = cam.WorldToScreenPoint(transform.position);
        mouse_pos.x = mouse_pos.x - object_pos.x;
        mouse_pos.y = mouse_pos.y - object_pos.y;
        angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;

        if (angle > 90.0f || angle < -90.0f)
            transform.localScale = flippedScale;
        else
            transform.localScale = normalScale;


        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public void Use()
    {
        coolDownTimer = coolDown;
        defaultPos = transform.localPosition;
        targetPos = transform.right + transform.localPosition;
    }

}
