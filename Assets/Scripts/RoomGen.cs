using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    Empty = -1,
    Ground = 0,
    Wall,
    Enemy,
    Weapon,
    Health,
    Player,
    Planks
}

public class RoomGen : MonoBehaviour
{
    public Sprite roomLayout;
    public GameObject[] tiles;
    public List<List<GameObject>> objects;
    public Vector2 gridPos;
    public Vector2 mapIndex;
    public bool isActiveRoom = false;
    public bool[] hasRoom = { false, false, false, false }; // Up, Right, Down, Left
    Vector3 RoomPos;

}
