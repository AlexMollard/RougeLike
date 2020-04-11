using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public bool Occupied = false;
    Room Up, Right;


    public void SetUp(Room up)
    {
        Up = up;
    }

    public void SetRight(Room right)
    {
        Right = right;
    }

    public bool CanHorizontal()
    {
        if (Right == null)
            return false;

        if (Right.Occupied == false)
            return true;

        return false;
    }

    public bool CanVerticle()
    {
        if (Up == null)
            return false;

        if (Up.Occupied == false)
            return true;

        return false;
    }

}
