﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour : MonoBehaviour
{
    public Vector2 tilePos;
    public TileType type = TileType.Ground;
    public GameObject tile;
    public bool hasItem = false;
}
