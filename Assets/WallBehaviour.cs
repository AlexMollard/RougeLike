using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBehaviour : MonoBehaviour
{
    public Sprite topLeft;
    public Sprite topCenter;
    public Sprite topRight;

    public Sprite middleLeft;
    public Sprite middleCenter;
    public Sprite middleRight;

    public Sprite bottomLeft;
    public Sprite bottomCenter;
    public Sprite bottomRight;


    // Currently broken and needs to be worked on
    // Need to research on square marching?

    public void SetWall(bool up, bool right, bool down, bool left)
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();

        if (up && right && down && left)
        {
            renderer.sprite = middleCenter;
            return;
        }

        if (up && right)
        {
            renderer.sprite = bottomLeft;
            return;
        }

        if (up && left)
        {
            renderer.sprite = bottomRight;
            return;
        }

        if (down && right)
        {
            renderer.sprite = topLeft;
            return;
        }

        if (down && left)
        {
            renderer.sprite = topRight;
            return;
        }

        if (down && up)
        {
            renderer.sprite = topCenter;
            return;
        }

        if (right && left)
        {
            renderer.sprite = topCenter;
            return;
        }

    }
}
