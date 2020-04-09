using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGen : MonoBehaviour
{
    public int mapSize = 5;
    public int roomTotal = 8;
    public GameObject emptyTilePrefab;
    public GameObject[] tilePrefabs;
    public Sprite roomLayout;
    List<List<GameObject>> tiles;
    int roomSize = 10;
    List<GameObject> activeRooms = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        tiles = new List<List<GameObject>>();


        for (int x = 0; x < mapSize * roomSize; x++)
        {
            tiles.Add(new List<GameObject>());
            for (int y = 0; y < mapSize * roomSize; y++)
            {
                GameObject currentTile = Instantiate(emptyTilePrefab, new Vector3((x - (mapSize * roomSize) * 0.5f) * 0.1f, (y - (mapSize * roomSize) * 0.5f) * 0.1f), Quaternion.identity, this.transform);
                tiles[x].Add(currentTile);
                currentTile.GetComponent<TileBehaviour>().tilePos = new Vector2(x, y);
                currentTile.GetComponent<TileBehaviour>().type = TileType.Empty;
                currentTile.transform.name = (x + ", " + y);
            }
        }

        SetRooms();
        SetTiles();


    }

    void SetTiles()
    {
        for (int x = 0; x < tiles.Count; x++)
        {
            for (int y = 0; y < tiles[x].Count; y++)
            {
                if (tiles[x][y].GetComponent<TileBehaviour>().type == TileType.Empty)
                    continue;

                bool isWall = false;

                if (y % 10 == 0)
                {
                    if (y > 0)
                    {
                        if (tiles[x][y - 1].GetComponent<TileBehaviour>().type == TileType.Empty)
                            isWall = true;
                    }
                    else
                    {
                        if (y == 0)
                            isWall = true;
                    }
                }

                if (y % 10 == 9)
                {
                    if (y < mapSize * 10 - 1)
                    {
                        if (tiles[x][y + 1].GetComponent<TileBehaviour>().type == TileType.Empty)
                            isWall = true;
                    }
                    else
                    {
                        if (y == mapSize * 10 - 1)
                            isWall = true;
                    }
                }

                if (x % 10 == 0)
                {
                    if (x > 0)
                    {
                        if (tiles[x - 1][y].GetComponent<TileBehaviour>().type == TileType.Empty)
                            isWall = true;
                    }
                    else
                    {
                        if (x == 0)
                            isWall = true;
                    }
                }

                if (x % 10 == 9)
                {
                    if (x < mapSize * 10 - 1)
                    {
                        if (tiles[x + 1][y].GetComponent<TileBehaviour>().type == TileType.Empty)
                            isWall = true;
                    }
                    else
                    {
                        if (x == mapSize * 10 - 1)
                            isWall = true;
                    }
                }


                TileType currentType = tiles[x][y].GetComponent<TileBehaviour>().type;
                GameObject currentTile = Instantiate(tilePrefabs[(isWall) ? (int)TileType.Wall : (int)currentType], tiles[x][y].transform.position, Quaternion.identity,tiles[x][y].transform);

                if (currentType == TileType.Player)
                {
                    currentTile.GetComponent<PlayerController>().tiles = tiles;
                }

                if (currentType == TileType.Weapon || currentType == TileType.Player || currentType == TileType.Enemy || currentType == TileType.Health)
                {
                    currentTile = Instantiate(tilePrefabs[(int)TileType.Ground], tiles[x][y].transform.position, Quaternion.identity, tiles[x][y].transform);
                }


            }
        }
    }

    void SetRooms()
    {
        List<GameObject> surroundingRooms = new List<GameObject>();
        GameObject currentActiveRoom = tiles[Random.Range(0, mapSize) * roomSize][Random.Range(0, mapSize) * roomSize];
        Vector2 currentRoomPos = currentActiveRoom.GetComponent<TileBehaviour>().tilePos;
        activeRooms.Add(currentActiveRoom);
        Vector2 mapIndex = new Vector2(0, 4);

        for (int i = 0; i < roomTotal; i++)
        {

            if (mapSize * 10 > currentRoomPos.x + 10)
                if (!activeRooms.Contains(tiles[(int)currentRoomPos.x + 10][(int)currentRoomPos.y]))
                    surroundingRooms.Add(tiles[(int)currentRoomPos.x + 10][(int)currentRoomPos.y]);

            if (0 <= currentRoomPos.x - 10)
                if (!activeRooms.Contains(tiles[(int)currentRoomPos.x - 10][(int)currentRoomPos.y]))
                    surroundingRooms.Add(tiles[(int)currentRoomPos.x - 10][(int)currentRoomPos.y]);

            if (mapSize * 10 > currentRoomPos.y + 10)
                if (!activeRooms.Contains(tiles[(int)currentRoomPos.x][(int)currentRoomPos.y + 10]))
                    surroundingRooms.Add(tiles[(int)currentRoomPos.x][(int)currentRoomPos.y + 10]);

            if (0 <= currentRoomPos.y - 10)
                if (!activeRooms.Contains(tiles[(int)currentRoomPos.x][(int)currentRoomPos.y - 10]))
                    surroundingRooms.Add(tiles[(int)currentRoomPos.x][(int)currentRoomPos.y - 10]);
                    


            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    if (tiles[x + (int)currentRoomPos.x][y + (int)currentRoomPos.y].GetComponent<TileBehaviour>().type != TileType.Empty)
                        break;
                    
                    Color currentPixel = roomLayout.texture.GetPixel(x + (int)(mapIndex.x * 10), y + (int)(mapIndex.y * 10));
                    int tileIndex = AssignTile(currentPixel);

                    TileBehaviour currentTile = tiles[x + (int)currentRoomPos.x][y + (int)currentRoomPos.y].GetComponent<TileBehaviour>();
                    currentTile.type = (TileType)tileIndex;
                }
            }

            int roomIndex = Random.Range(0, surroundingRooms.Count);

            currentRoomPos = surroundingRooms[roomIndex].GetComponent<TileBehaviour>().tilePos;
            currentActiveRoom = surroundingRooms[roomIndex];
            surroundingRooms.RemoveAt(roomIndex);

            mapIndex = new Vector2(Random.Range(0, 5), Random.Range(0, 4));
            activeRooms.Add(currentActiveRoom);
        }
    }

    void WallOverRide(int x, int y)
    {
        //GameObject currentTile = Instantiate(tiles[(int)TileType.Wall]);
        //currentTile.transform.parent = this.transform;
        //currentTile.transform.position = new Vector3(RoomPos.x + (x * 0.1f), RoomPos.y + (y * 0.1f));
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
