using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBehaviour : MonoBehaviour
{
    public Sprite topLeft;
    public Sprite topCenter;
    public Sprite topRight;

    public Sprite topInnerLeft;
    public Sprite topInnerRight;

    public Sprite middleLeft;
    public Sprite middleRight;

    public Sprite bottomLeft;
    public Sprite bottomCenter;
    public Sprite bottomRight;

    public Sprite bottomInnerLeft;
    public Sprite bottomInnerRight;


    // Currently broken and needs to be worked on
    // Need to research on square marching?
    public void SetWall(bool up, bool right, bool down, bool left, bool floorAbove, bool floorBelow, bool floorRight, bool floorLeft)
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();

        if (down && floorAbove  && floorRight)
        {
            renderer.sprite = bottomInnerLeft;
            return;
        }

        if (down  && floorAbove && floorLeft)
        {
            renderer.sprite = bottomInnerRight;
            return;
        }

        if (up && floorBelow && floorRight)
        {
            renderer.sprite = topInnerLeft;
            return;
        }

        if (up && floorBelow && floorLeft)
        {
            renderer.sprite = topInnerRight;
            return;
        }

        if (down && right && !floorAbove && !floorBelow && !floorLeft && !floorRight)
        {
            renderer.sprite = topLeft;
            return;
        }

        if (down && left && !floorAbove && !floorBelow && !floorLeft && !floorRight)
        {
            renderer.sprite = topRight;
            return;
        }

        if (up && right && !down && !floorBelow && !floorRight)
        {
            renderer.sprite = bottomLeft;
            return;
        }

        if (up && left && !down && !floorBelow && !floorLeft)
        {
            renderer.sprite = bottomRight;
            return;
        }


        if (floorBelow && !floorAbove)
        {
            renderer.sprite = topCenter;
            return;
        }

        if (floorRight && !floorAbove)
        {
            renderer.sprite = middleLeft;
            return;
        }

        if (floorLeft && !floorAbove)
        {
            renderer.sprite = middleRight;
            return;
        }

    }
}
