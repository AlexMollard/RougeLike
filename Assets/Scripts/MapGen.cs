using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGen : MonoBehaviour
{
    public int mapSize = 5;
    public int roomTotal = 8;
    public float doubleRoomChance = 0.5f;
    public float doubleHighRoomChance = 0.25f;
    public float doubleWideRoomChance = 0.25f;

    public Color[] typeColors;

    public GameObject emptyTilePrefab;
    public GameObject[] tilePrefabs;
    public Sprite roomLayout;
    public Sprite largeRoomLayout;
    public Sprite VertRoomLayout;
    List<List<GameObject>> tiles;
    int roomAmountOnSheet = 8;

    int roomSize = 40;
    float tileScale;

    // Start is called before the first frame update
    void Start()
    {
        tiles = new List<List<GameObject>>();
        tileScale = 0.16f;

        for (int x = 0; x < mapSize * roomSize; x++)
        {
            tiles.Add(new List<GameObject>());
            for (int y = 0; y < mapSize * roomSize; y++)
            {
                GameObject currentTile = Instantiate(emptyTilePrefab, new Vector3((x - (mapSize * roomSize) * 0.5f) * tileScale, (y - (mapSize * roomSize) * 0.5f) * tileScale), Quaternion.identity, this.transform);
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
                {
                    tiles[x][y].GetComponent<SpriteRenderer>().enabled = false;
                    continue;
                }

                TileBehaviour current = tiles[x][y].GetComponent<TileBehaviour>();

                SetWalls(x, y);

               // if (GetWallCount(x, y) >= 5)
               // {
               //     current.type = TileType.Empty;
               //     continue;
               // }

                TileType currentType = current.type;

                GameObject currentTile = Instantiate(tilePrefabs[(int)currentType], tiles[x][y].transform.position, Quaternion.identity,tiles[x][y].transform);
                tiles[x][y].GetComponent<SpriteRenderer>().enabled = false;

                tiles[x][y].GetComponent<TileBehaviour>().tile = currentTile;

                // Gives player class tiles for movement and other stuff
                if (currentType == TileType.Player)
                    currentTile.GetComponent<PlayerController>().tiles = tiles;

                // Gives enemy class tiles for movement and other stuff
                if (currentType == TileType.Enemy)
                    currentTile.GetComponent<EnemyBehaviour>().tiles = tiles;

                // Places floor under entity
                if (currentType == TileType.Weapon || currentType == TileType.Player || currentType == TileType.Enemy || currentType == TileType.Health)
                    Instantiate(tilePrefabs[(int)TileType.Ground], tiles[x][y].transform.position, Quaternion.identity, tiles[x][y].transform);


            }
        }

        for (int x = 0; x < tiles.Count; x++)
        {
            for (int y = 0; y < tiles[x].Count; y++)
            {
                if (tiles[x][y].GetComponent<TileBehaviour>().type != TileType.Wall)
                    continue;

                GameObject current = tiles[x][y].GetComponent<TileBehaviour>().tile;

                SetWallSprite(x, y, current);
            }
        }
    }

    void SetWallSprite(int x, int y, GameObject currentTile)
    {
        if (currentTile == null)
        {
        Debug.LogError("It was null " + x + ", "+ y);
            return;

        }
        WallBehaviour wall = currentTile.GetComponent<WallBehaviour>();

        bool top = false, right = false, bottom = false, left = false, floorAbove = false, floorBelow = false, floorRight = false, floorLeft = false;

        if (x < mapSize * roomSize - 1)
        {
            if (tiles[x + 1][y].GetComponent<TileBehaviour>().type == TileType.Wall)
                right = true;


            if (tiles[x + 1][y].GetComponent<TileBehaviour>().type != TileType.Wall && tiles[x + 1][y].GetComponent<TileBehaviour>().type != TileType.Empty)
                floorRight = true;
        }

        if (x > 0)
        {
            if (tiles[x - 1][y].GetComponent<TileBehaviour>().type == TileType.Wall)
                left = true;

            if (tiles[x - 1][y].GetComponent<TileBehaviour>().type != TileType.Wall && tiles[x - 1][y].GetComponent<TileBehaviour>().type != TileType.Empty)
                floorLeft = true;
        }

        if (y < mapSize * roomSize - 1)
        {
            if (tiles[x][y + 1].GetComponent<TileBehaviour>().type == TileType.Wall)
                top = true;

            if (tiles[x][y + 1].GetComponent<TileBehaviour>().type != TileType.Wall && tiles[x][y + 1].GetComponent<TileBehaviour>().type != TileType.Empty)
                floorAbove = true;
        }

        if (y > 0)
        {
            if (tiles[x][y - 1].GetComponent<TileBehaviour>().type == TileType.Wall)
                bottom = true;

            if (tiles[x][y - 1].GetComponent<TileBehaviour>().type != TileType.Wall && tiles[x][y - 1].GetComponent<TileBehaviour>().type != TileType.Empty)
                floorBelow = true;
        }
        wall.SetWall(top, right, bottom, left, floorAbove, floorBelow, floorRight, floorLeft);
    }

    void SetWalls(int x, int y)
    {
        TileBehaviour current = tiles[x][y].GetComponent<TileBehaviour>();

        // Down
        if (y % roomSize == 0)
        {
            if (y > 0)
            {
                if (tiles[x][y - 1].GetComponent<TileBehaviour>().type == TileType.Empty)
                    current.type = TileType.Wall;
            }
            else
            {
                if (y == 0)
                    current.type = TileType.Wall;
            }
        }

        // Up
        if (y % roomSize == roomSize - 1)
        {
            if (y < mapSize * roomSize - 1)
            {
                if (tiles[x][y + 1].GetComponent<TileBehaviour>().type == TileType.Empty)
                    current.type = TileType.Wall;
            }
            else
            {
                if (y == mapSize * roomSize - 1)
                    current.type = TileType.Wall;
            }
        }

        // Left
        if (x % roomSize == 0)
        {
            if (x > 0)
            {
                if (tiles[x - 1][y].GetComponent<TileBehaviour>().type == TileType.Empty)
                    current.type = TileType.Wall;
            }
            else
            {
                if (x == 0)
                    current.type = TileType.Wall;
            }
        }

        // Right
        if (x % roomSize == roomSize - 1)
        {
            if (x < mapSize * roomSize - 1)
            {
                if (tiles[x + 1][y].GetComponent<TileBehaviour>().type == TileType.Empty)
                    current.type = TileType.Wall;
            }
            else
            {
                if (x == mapSize * roomSize - 1)
                    current.type = TileType.Wall;
            }
        }
    }

    int GetWallCount(int x, int y)
    {
        int totalNonEmptyOrWalls = 0;

        if (x < mapSize * roomSize - 1)
            if (tiles[x + 1][y].GetComponent<TileBehaviour>().type == TileType.Empty || tiles[x + 1][y].GetComponent<TileBehaviour>().type == TileType.Wall)
                totalNonEmptyOrWalls++;

        if (x > 0)
            if (tiles[x - 1][y].GetComponent<TileBehaviour>().type == TileType.Empty || tiles[x - 1][y].GetComponent<TileBehaviour>().type == TileType.Wall)
                totalNonEmptyOrWalls++;

        if (y < mapSize * roomSize - 1)
        {
            if (tiles[x][y + 1].GetComponent<TileBehaviour>().type == TileType.Empty || tiles[x][y + 1].GetComponent<TileBehaviour>().type == TileType.Wall)
                totalNonEmptyOrWalls++;

            // Top - Left
            if (x > 0)
                if (tiles[x - 1][y + 1].GetComponent<TileBehaviour>().type == TileType.Empty || tiles[x - 1][y + 1].GetComponent<TileBehaviour>().type == TileType.Wall)
                    totalNonEmptyOrWalls++;

            // Top - Right
            if (x < mapSize * roomSize - 1)
                if (tiles[x + 1][y + 1].GetComponent<TileBehaviour>().type == TileType.Empty || tiles[x + 1][y + 1].GetComponent<TileBehaviour>().type == TileType.Wall)
                    totalNonEmptyOrWalls++;
        }

        if (y > 0)
        {
            if (tiles[x][y - 1].GetComponent<TileBehaviour>().type == TileType.Empty || tiles[x][y - 1].GetComponent<TileBehaviour>().type == TileType.Wall)
                totalNonEmptyOrWalls++;

            // Bottom - Left
            if (x > 0)
                if (tiles[x - 1][y - 1].GetComponent<TileBehaviour>().type == TileType.Empty || tiles[x - 1][y - 1].GetComponent<TileBehaviour>().type == TileType.Wall)
                    totalNonEmptyOrWalls++;

            // Bottom - Right
            if (x < mapSize * roomSize - 1)
                if (tiles[x + 1][y - 1].GetComponent<TileBehaviour>().type == TileType.Empty || tiles[x + 1][y - 1].GetComponent<TileBehaviour>().type == TileType.Wall)
                    totalNonEmptyOrWalls++;
        }


        if (y == 0 || y == mapSize * roomSize - 1)
        {
            totalNonEmptyOrWalls++;
        }
        if (x == 0 || x == mapSize * roomSize - 1)
        {
            totalNonEmptyOrWalls++;
        }

        return totalNonEmptyOrWalls;
    }


    void SetRooms()
    {
        List<Vector2> oldRoomIndexes = new List<Vector2>();
        List<GameObject> surroundingRooms = new List<GameObject>();
        
        GameObject currentActiveRoom = tiles[(mapSize / 2) * roomSize][(mapSize / 2) * roomSize];
        
        Vector2 currentRoomPos = currentActiveRoom.GetComponent<TileBehaviour>().tilePos;
        Vector2 mapIndex = new Vector2(0, roomAmountOnSheet - 1);
        Vector2 normalisedRoomPos = new Vector2((int)currentRoomPos.x / roomSize, (int)currentRoomPos.y / roomSize);

        oldRoomIndexes.Add(mapIndex);

        Room[,] rooms = new Room[mapSize, mapSize];
        for (int x = 0; x < mapSize; x++)
        {
            for (int y = 0; y < mapSize; y++)
            {
                rooms[x, y] = new Room();
            }
        }


        for (int i = 0; i < roomTotal; i++)
        {
            bool canRightRoom = false;
            bool canUpRoom = false;
            rooms[(int)normalisedRoomPos.x, (int)normalisedRoomPos.y].Occupied = true;

            if (mapSize * roomSize > currentRoomPos.x + roomSize)
                if (rooms[(int)normalisedRoomPos.x + 1, (int)normalisedRoomPos.y].Occupied == false)
                {
                    surroundingRooms.Add(tiles[(int)currentRoomPos.x + roomSize][(int)currentRoomPos.y]);

                    if (mapSize * roomSize > currentRoomPos.x + (roomSize * 2))
                        if (rooms[(int)normalisedRoomPos.x + 2, (int)normalisedRoomPos.y].Occupied == false)
                        {
                            rooms[(int)normalisedRoomPos.x + 1, (int)normalisedRoomPos.y].SetRight(rooms[(int)normalisedRoomPos.x + 2, (int)normalisedRoomPos.y]);
                            canRightRoom = true;
                        }
                }


            if (0 <= currentRoomPos.x - roomSize)
                if (rooms[(int)normalisedRoomPos.x - 1, (int)normalisedRoomPos.y].Occupied == false)
                    surroundingRooms.Add(tiles[(int)currentRoomPos.x - roomSize][(int)currentRoomPos.y]);

            if (mapSize * roomSize > currentRoomPos.y + roomSize)
                if (rooms[(int)normalisedRoomPos.x, (int)normalisedRoomPos.y + 1].Occupied == false)
                {
                    surroundingRooms.Add(tiles[(int)currentRoomPos.x][(int)currentRoomPos.y + roomSize]);
                    if (mapSize * roomSize > currentRoomPos.y + (roomSize * 2))
                        if (rooms[(int)normalisedRoomPos.x, (int)normalisedRoomPos.y + 2].Occupied == false)
                        {
                            rooms[(int)normalisedRoomPos.x, (int)normalisedRoomPos.y + 1].SetRight(rooms[(int)normalisedRoomPos.x, (int)normalisedRoomPos.y + 2]);
                            canUpRoom = true;
                        }
                }

            if (0 <= currentRoomPos.y - roomSize)
                if (rooms[(int)normalisedRoomPos.x, (int)normalisedRoomPos.y - 1].Occupied == false)
                    surroundingRooms.Add(tiles[(int)currentRoomPos.x][(int)currentRoomPos.y - roomSize]);


            bool buildingRightRoom = false;
            bool buildingUpRoom = false;

            if (canRightRoom && i > 0 && Random.value < doubleWideRoomChance)
            {
                List<GameObject> placeholderRooms = new List<GameObject>();

                if (mapSize * roomSize > currentRoomPos.x + (roomSize * 2))
                    if (!surroundingRooms.Contains(tiles[(int)currentRoomPos.x + (roomSize * 2)][(int)currentRoomPos.y]))
                        if (rooms[(int)normalisedRoomPos.x + 2, (int)normalisedRoomPos.y].Occupied == false)
                            placeholderRooms.Add(tiles[(int)currentRoomPos.x + (roomSize * 2)][(int)currentRoomPos.y]);


                if (mapSize * roomSize > currentRoomPos.x + roomSize)
                {
                    if (surroundingRooms.Contains(tiles[(int)currentRoomPos.x + roomSize][(int)currentRoomPos.y]))
                    {
                        surroundingRooms.Remove(tiles[(int)currentRoomPos.x + roomSize][(int)currentRoomPos.y]);
                    }
                    rooms[(int)normalisedRoomPos.x + 1, (int)normalisedRoomPos.y].Occupied = true;
                }

                if (surroundingRooms.Contains(tiles[(int)currentRoomPos.x][(int)currentRoomPos.y]))
                    surroundingRooms.Remove(tiles[(int)currentRoomPos.x][(int)currentRoomPos.y]);

                if (mapSize * roomSize > currentRoomPos.y + roomSize)
                    if (!surroundingRooms.Contains(tiles[(int)currentRoomPos.x + roomSize][(int)currentRoomPos.y + roomSize]))
                        if (rooms[(int)normalisedRoomPos.x + 1, (int)normalisedRoomPos.y + 1].Occupied == false)
                            placeholderRooms.Add(tiles[(int)currentRoomPos.x + roomSize][(int)currentRoomPos.y + roomSize]);

                if (0 <= currentRoomPos.y - roomSize)
                    if (!surroundingRooms.Contains(tiles[(int)currentRoomPos.x + roomSize][(int)currentRoomPos.y - roomSize]))
                        if (rooms[(int)normalisedRoomPos.x + 1, (int)normalisedRoomPos.y - 1].Occupied == false)
                            placeholderRooms.Add(tiles[(int)currentRoomPos.x + roomSize][(int)currentRoomPos.y - roomSize]);


                    if (tiles[(int)currentRoomPos.x + roomSize][(int)currentRoomPos.y].GetComponent<TileBehaviour>().type == TileType.Empty)
                    {
                        buildingRightRoom = true;
                        surroundingRooms.AddRange(placeholderRooms);
                        mapIndex = new Vector2(Random.Range(0, roomAmountOnSheet / 2), Random.Range(0, roomAmountOnSheet));
                    }
            }

            if (canUpRoom && i > 0 && buildingRightRoom == false && Random.value < doubleHighRoomChance)
            {
                List<GameObject> placeholderRooms = new List<GameObject>();

                // Up-Right
                if (mapSize * roomSize > currentRoomPos.x + roomSize)
                    if (!surroundingRooms.Contains(tiles[(int)currentRoomPos.x + roomSize][(int)currentRoomPos.y + roomSize]))
                        if (rooms[(int)normalisedRoomPos.x + 1, (int)normalisedRoomPos.y + 1].Occupied == false)
                            placeholderRooms.Add(tiles[(int)currentRoomPos.x + roomSize][(int)currentRoomPos.y + roomSize]);

                // Up-Left
                if (0 <= currentRoomPos.x - roomSize)
                    if (!surroundingRooms.Contains(tiles[(int)currentRoomPos.x - roomSize][(int)currentRoomPos.y + roomSize]))
                        if (rooms[(int)normalisedRoomPos.x - 1, (int)normalisedRoomPos.y + 1].Occupied == false)
                            placeholderRooms.Add(tiles[(int)currentRoomPos.x - roomSize][(int)currentRoomPos.y + roomSize]);

                // Up
                if (mapSize * roomSize > currentRoomPos.y + roomSize)
                {
                    if (surroundingRooms.Contains(tiles[(int)currentRoomPos.x ][(int)currentRoomPos.y + roomSize]))
                    {
                        surroundingRooms.Remove(tiles[(int)currentRoomPos.x][(int)currentRoomPos.y + roomSize]);
                    }
                    rooms[(int)normalisedRoomPos.x , (int)normalisedRoomPos.y + 1].Occupied = true;
                }

                // Current
                if (surroundingRooms.Contains(tiles[(int)currentRoomPos.x][(int)currentRoomPos.y]))
                    surroundingRooms.Remove(tiles[(int)currentRoomPos.x][(int)currentRoomPos.y]);
                
                // Up-Up
                if (mapSize * roomSize > currentRoomPos.y + (roomSize * 2))
                    if (!surroundingRooms.Contains(tiles[(int)currentRoomPos.x][(int)currentRoomPos.y + (roomSize * 2)]))
                        if (rooms[(int)normalisedRoomPos.x, (int)normalisedRoomPos.y + 2].Occupied == false)
                            placeholderRooms.Add(tiles[(int)currentRoomPos.x][(int)currentRoomPos.y + (roomSize * 2)]);



                    if (tiles[(int)currentRoomPos.x][(int)currentRoomPos.y + roomSize].GetComponent<TileBehaviour>().type == TileType.Empty)
                    {
                        buildingUpRoom = true;
                        surroundingRooms.AddRange(placeholderRooms);
                        mapIndex = new Vector2(Random.Range(0, roomAmountOnSheet), Random.Range(0, roomAmountOnSheet / 2));
                    }
            }


            int xSize = (buildingRightRoom) ? roomSize * 2 : roomSize;
            int ySize = (buildingUpRoom) ? roomSize * 2 : roomSize;


            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    Color currentPixel;
                    if (buildingRightRoom)
                    {
                        currentPixel = largeRoomLayout.texture.GetPixel(x + (int)((mapIndex.x * (roomSize * 2))), y + (int)(mapIndex.y * roomSize));
                    }
                    else if (buildingUpRoom)
                    {
                        currentPixel = VertRoomLayout.texture.GetPixel(x + (int)((mapIndex.x * roomSize)), y + (int)((mapIndex.y * (roomSize * 2))));
                    }
                    else
                        currentPixel = roomLayout.texture.GetPixel(x + (int)(mapIndex.x * roomSize), y + (int)(mapIndex.y * roomSize));



                    int tileIndex = AssignTile(currentPixel);

                    TileBehaviour currentTile = tiles[x + (int)currentRoomPos.x][y + (int)currentRoomPos.y].GetComponent<TileBehaviour>();
                    currentTile.type = (TileType)tileIndex;
                }
            }

            int roomIndex = 0;

            for (int s = 0; s < surroundingRooms.Count; s++)
            {
                Vector2 tilePos = surroundingRooms[s].GetComponent<TileBehaviour>().tilePos;
                if (rooms[(int)tilePos.x / roomSize,(int)tilePos.y / roomSize].Occupied == true)
                {
                    surroundingRooms.RemoveAt(s);
                }
            }

            roomIndex = Random.Range(0, surroundingRooms.Count);

            
            currentRoomPos = surroundingRooms[roomIndex].GetComponent<TileBehaviour>().tilePos;


            surroundingRooms.RemoveAt(roomIndex);

            mapIndex = new Vector2(Random.Range(0, roomAmountOnSheet), Random.Range(0, roomAmountOnSheet));
            while (oldRoomIndexes.Contains(mapIndex))
            {
                mapIndex = new Vector2(Random.Range(0, roomAmountOnSheet), Random.Range(0, roomAmountOnSheet));
            }

            normalisedRoomPos = new Vector2((int)currentRoomPos.x / roomSize, (int)currentRoomPos.y / roomSize);
            rooms[(int)normalisedRoomPos.x, (int)normalisedRoomPos.y].Occupied = true;
            oldRoomIndexes.Add(mapIndex); 

        }
    }

    int AssignTile(Color currentPixel)
    {

        for (int i = 0; i < typeColors.Length; i++)
        {
            if (TestColor(currentPixel, typeColors[i]))
            {
                return i;
            }
        }

        return (int)TileType.Empty;
    }

    bool TestColor(Color pixel, Color type)
    {
        float threshhold = 0.05f;
        if (pixel.r < type.r + threshhold &&
            pixel.r > type.r - threshhold &&
            pixel.g < type.g + threshhold &&
            pixel.g > type.g - threshhold &&
            pixel.b < type.b + threshhold &&
            pixel.b > type.b - threshhold)
        {
            return true;
        }


        return false;
    }

}

