using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    Ground = 0,
    Wall,
    Enemy,
    Weapon,
    Health,
    Player
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

    // Start is called before the first frame update
    void Start()
    {
        if (!isActiveRoom)
        {
            Destroy(gameObject);
        }


        RoomPos = this.transform.position;
        objects = new List<List<GameObject>>();

        for (int x = 0; x < 10; x++)
        {
            objects.Add(new List<GameObject>());
            for (int y = 0; y < 10; y++)
            {
                Color currentPixel = roomLayout.texture.GetPixel(x + (int)(mapIndex.x * 10), y + (int)(mapIndex.y * 10));
                int tileIndex = AssignTile(currentPixel);
                GameObject currentTile = Instantiate(tiles[tileIndex]);
                currentTile.transform.parent = this.transform;
                currentTile.transform.position = new Vector3(RoomPos.x + (x * 0.1f), RoomPos.y + (y * 0.1f));

                objects[x].Add(currentTile);

                if (tileIndex == (int)TileType.Weapon || tileIndex == (int)TileType.Player || tileIndex == (int)TileType.Enemy || tileIndex == (int)TileType.Health)
                {
                    currentTile = Instantiate(tiles[(int)TileType.Ground]);
                    currentTile.transform.parent = this.transform;
                    currentTile.transform.position = new Vector3(RoomPos.x + (x * 0.1f), RoomPos.y + (y * 0.1f));

                    objects[x].Add(currentTile);
                }

                if (tileIndex == (int)TileType.Ground && x == 9 && !hasRoom[1])
                    WallOverRide(x, y);

                if (tileIndex == (int)TileType.Ground && x == 0 && !hasRoom[3])
                    WallOverRide(x, y);

                if (tileIndex == (int)TileType.Ground && y == 9 && !hasRoom[0])
                    WallOverRide(x, y);

                if (tileIndex == (int)TileType.Ground && y == 0 && !hasRoom[2])
                    WallOverRide(x, y);

            }
        }

    }

    void WallOverRide(int x, int y)
    {
        GameObject currentTile = Instantiate(tiles[(int)TileType.Wall]);
        currentTile.transform.parent = this.transform;
        currentTile.transform.position = new Vector3(RoomPos.x + (x * 0.1f), RoomPos.y + (y * 0.1f));

        objects[x].Add(currentTile);
    }

    int AssignTile(Color currentPixel)
    {
        if (currentPixel.r == 0 && currentPixel.g == 0 && currentPixel.b == 0)
        {
            return (int)TileType.Wall;
        }

        if (currentPixel.r == 1 && currentPixel.g == 0 && currentPixel.b == 0)
        {
            return (int)TileType.Enemy;
        }

        if (currentPixel.r == 0 && currentPixel.g == 0 && currentPixel.b == 1)
        {
            return (int)TileType.Weapon;
        }

        if (currentPixel.r == 0 && currentPixel.g == 1 && currentPixel.b == 0)
        {
            return (int)TileType.Health;
        }

        if (currentPixel.r == 1 && currentPixel.g == 0 && currentPixel.b == 1)
        {
            return (int)TileType.Player;
        }

        return (int)TileType.Ground;
    }

}
