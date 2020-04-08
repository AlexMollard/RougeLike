using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGen : MonoBehaviour
{
    public int mapSize = 5;
    public int roomTotal = 8;
    public GameObject roomPrefab;
    List<List<GameObject>> rooms;

    // Start is called before the first frame update
    void Start()
    {
        rooms = new List<List<GameObject>>();
        for (int x = 0; x < mapSize; x++)
        {
            rooms.Add(new List<GameObject>());
            for (int y = 0; y < mapSize; y++)
            {
                GameObject currentRoom = Instantiate(roomPrefab, new Vector3(x - (mapSize * 0.5f), y - (mapSize * 0.5f)), Quaternion.identity, this.transform);
                rooms[x].Add(currentRoom);
                currentRoom.GetComponent<RoomGen>().gridPos = new Vector2(x,y);
                currentRoom.transform.name = (x + ", " + y);

            }
        }

        List<GameObject> activeRooms = new List<GameObject>();
        List<GameObject> surroundingRooms = new List<GameObject>();
        GameObject currentActiveRoom = rooms[Random.Range(0, mapSize)][Random.Range(0, mapSize)];
        currentActiveRoom.GetComponent<RoomGen>().mapIndex = new Vector2(0, 4);
        for (int i = 0; i < roomTotal; i++)
        {
            activeRooms.Add(currentActiveRoom);
            RoomGen room = currentActiveRoom.GetComponent<RoomGen>();
            room.isActiveRoom = true;

            if (mapSize > (int)room.gridPos.x + 1)
                if(!rooms[(int)room.gridPos.x + 1][(int)room.gridPos.y].GetComponent<RoomGen>().isActiveRoom)
                    surroundingRooms.Add(rooms[(int)room.gridPos.x + 1][(int)room.gridPos.y]);

            if (0 <= (int)room.gridPos.x - 1)
                if (!rooms[(int)room.gridPos.x - 1][(int)room.gridPos.y].GetComponent<RoomGen>().isActiveRoom)
                    surroundingRooms.Add(rooms[(int)room.gridPos.x - 1][(int)room.gridPos.y]);

            if (mapSize > (int)room.gridPos.y + 1)
                if (!rooms[(int)room.gridPos.x][(int)room.gridPos.y + 1].GetComponent<RoomGen>().isActiveRoom)
                    surroundingRooms.Add(rooms[(int)room.gridPos.x][(int)room.gridPos.y + 1]);

            if (0 <= (int)room.gridPos.y - 1)
                if (!rooms[(int)room.gridPos.x][(int)room.gridPos.y - 1].GetComponent<RoomGen>().isActiveRoom)
                    surroundingRooms.Add(rooms[(int)room.gridPos.x][(int)room.gridPos.y - 1]);

            int newRoom = Random.Range(0, surroundingRooms.Count);
            currentActiveRoom = surroundingRooms[newRoom];
            surroundingRooms.RemoveAt(newRoom);
            currentActiveRoom.GetComponent<RoomGen>().mapIndex = new Vector2(Random.Range(0, 5), Random.Range(0, 4));
        }

        for (int i = 0; i < activeRooms.Count; i++)
        {
            RoomGen room = activeRooms[i].GetComponent<RoomGen>();
            if (mapSize > (int)room.gridPos.x + 1)
                if (rooms[(int)room.gridPos.x + 1][(int)room.gridPos.y].GetComponent<RoomGen>().isActiveRoom)
                    room.hasRoom[1] = true;

            if (0 <= (int)room.gridPos.x - 1)
                if (rooms[(int)room.gridPos.x - 1][(int)room.gridPos.y].GetComponent<RoomGen>().isActiveRoom)
                    room.hasRoom[3] = true;

            if (mapSize > (int)room.gridPos.y + 1)
                if (rooms[(int)room.gridPos.x][(int)room.gridPos.y + 1].GetComponent<RoomGen>().isActiveRoom)
                    room.hasRoom[0] = true;

            if (0 <= (int)room.gridPos.y - 1)
                if (rooms[(int)room.gridPos.x][(int)room.gridPos.y - 1].GetComponent<RoomGen>().isActiveRoom)
                    room.hasRoom[2] = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
