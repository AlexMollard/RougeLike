using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBehaviour : MonoBehaviour
{
    public Sprite[] topLeft;
    public Sprite[] topCenter;
    public Sprite[] topRight;

    public Sprite[] topInnerLeft;
    public Sprite[] topInnerRight;

    public Sprite[] middleLeft;
    public Sprite[] middleRight;

    public Sprite[] bottomLeft;
    public Sprite[] bottomCenter;
    public Sprite[] bottomRight;

    public Sprite[] bottomInnerLeft;
    public Sprite[] bottomInnerRight;


    // Currently broken and needs to be worked on
    // Need to research on square marching?
    public void SetWall(bool up, bool right, bool down, bool left, bool floorAbove, bool floorBelow, bool floorRight, bool floorLeft)
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();

        if (down && floorAbove  && floorRight)
        {
            renderer.sprite = bottomInnerLeft[Random.Range(0,bottomInnerLeft.Length)];
            return;
        }

        if (down  && floorAbove && floorLeft)
        {
            renderer.sprite = bottomInnerRight[Random.Range(0, bottomInnerRight.Length)];
            return;
        }

        if (up && floorBelow && floorRight)
        {
            renderer.sprite = topInnerLeft[Random.Range(0, topInnerLeft.Length)];
            return;
        }

        if (up && floorBelow && floorLeft)
        {
            renderer.sprite = topInnerRight[Random.Range(0, topInnerRight.Length)];
            return;
        }

        if (down && right && !floorAbove && !floorBelow && !floorLeft && !floorRight)
        {
            renderer.sprite = topLeft[Random.Range(0, topLeft.Length)];
            return;
        }

        if (down && left && !floorAbove && !floorBelow && !floorLeft && !floorRight)
        {
            renderer.sprite = topRight[Random.Range(0, topRight.Length)];
            return;
        }

        if (up && right && !down && !floorBelow && !floorRight)
        {
            renderer.sprite = bottomLeft[Random.Range(0, bottomLeft.Length)];
            return;
        }

        if (up && left && !down && !floorBelow && !floorLeft)
        {
            renderer.sprite = bottomRight[Random.Range(0, bottomRight.Length)];
            return;
        }


        if (floorBelow && !floorAbove)
        {
            renderer.sprite = topCenter[Random.Range(0, topCenter.Length)];
            return;
        }

        if (floorRight && !floorAbove)
        {
            renderer.sprite = middleLeft[Random.Range(0, middleLeft.Length)];
            return;
        }

        if (floorLeft && !floorAbove)
        {
            renderer.sprite = middleRight[Random.Range(0, middleRight.Length)];
            return;
        }

        renderer.sprite = bottomCenter[Random.Range(0, bottomCenter.Length)];

    }
}
