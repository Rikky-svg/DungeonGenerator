using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generation : MonoBehaviour
{
    [Header("Room prefabs")]
    public GameObject[] roomPrefs;
    public GameObject StartRoom;
    public GameObject BossRoom;

    [Header("Number of rooms")]
    public int NumderOfRooms;

    [Header("Start coordinates")]
    public int StartX;
    public int StartY;

    [Header("Offset")]
    public float offsetX;
    public float offsetY;

    [Header("Map size")]
    public int MapSizeX;
    public int MapSizeY;

    [Header("Room size")]
    public float RoomSizeX;
    public float RoomSizeY;

    private GameObject[,] SpawnedRooms;

    void Start()
    {

        // Create array with spawnes rooms
        SpawnedRooms = new GameObject[MapSizeX, MapSizeY];

        // Create the start room
        GameObject StartRoomObj = Instantiate(StartRoom, new Vector2((StartX - offsetX) * RoomSizeX, (StartY - offsetY) * RoomSizeY), Quaternion.identity);
        SpawnedRooms[StartX, StartY] = StartRoomObj;

        for (int i = 0; i < NumderOfRooms; i++)
        {
            if (i < NumderOfRooms - 1) SpawnRooms();
            else SpawnBossRoom();
        }

        Doors();
    }

    void SpawnRooms()
    {
        // Collection with coordinates
        HashSet<Vector2Int> MaybePlaces = new HashSet<Vector2Int>();

        //Check coordinates
        for (int x = 0; x < SpawnedRooms.GetLength(0); x++)
        {
            for (int y = 0; y < SpawnedRooms.GetLength(1); y++)
            {
                if (SpawnedRooms[x, y] == null) continue;

                if (x > 0 && SpawnedRooms[x - 1, y] == null) MaybePlaces.Add(new Vector2Int(x - 1, y));
                if (x < (SpawnedRooms.GetLength(0) - 1) && SpawnedRooms[x + 1, y] == null) MaybePlaces.Add(new Vector2Int(x + 1, y));
                if (y > 0 && SpawnedRooms[x, y - 1] == null) MaybePlaces.Add(new Vector2Int(x, y - 1));
                if (y < (SpawnedRooms.GetLength(1) - 1) && SpawnedRooms[x, y + 1] == null) MaybePlaces.Add(new Vector2Int(x, y + 1));
            }
        }

        Vector2Int[] arrMaybePlaces = new Vector2Int[MaybePlaces.Count];
        MaybePlaces.CopyTo(arrMaybePlaces);

        // Random number
        int randCountMP = Random.Range(0, arrMaybePlaces.Length);

        // Coordinates
        float posX = arrMaybePlaces[randCountMP].x;
        float posY = arrMaybePlaces[randCountMP].y;

        // Add to array and create the room
        GameObject RoomObj = Instantiate(roomPrefs[Random.Range(0, roomPrefs.Length)], new Vector2((posX - offsetX) * RoomSizeX, (posY - offsetY) * RoomSizeY), Quaternion.identity);
        SpawnedRooms[arrMaybePlaces[randCountMP].x, arrMaybePlaces[randCountMP].y] = RoomObj;
    }

    void SpawnBossRoom()
    {
        // Collection with coordinates
        HashSet<Vector2Int> MaybePlaces = new HashSet<Vector2Int>();

        //Check coordinates
        for (int x = 0; x < SpawnedRooms.GetLength(0); x++)
        {
            for (int y = 0; y < SpawnedRooms.GetLength(1); y++)
            {
                if (SpawnedRooms[x, y] == null) continue;

                if (x > 0 && SpawnedRooms[x - 1, y] == null) MaybePlaces.Add(new Vector2Int(x - 1, y));
                if (x < (SpawnedRooms.GetLength(0) - 1) && SpawnedRooms[x + 1, y] == null) MaybePlaces.Add(new Vector2Int(x + 1, y));
                if (y > 0 && SpawnedRooms[x, y - 1] == null) MaybePlaces.Add(new Vector2Int(x, y - 1));
                if (y < (SpawnedRooms.GetLength(1) - 1) && SpawnedRooms[x, y + 1] == null) MaybePlaces.Add(new Vector2Int(x, y + 1));
            }
        }

        Vector2Int[] arrMaybePlaces = new Vector2Int[MaybePlaces.Count];
        MaybePlaces.CopyTo(arrMaybePlaces);

        // Random number
        int randCountMP = Random.Range(0, arrMaybePlaces.Length);

        // Coordinates
        float posX = arrMaybePlaces[randCountMP].x;
        float posY = arrMaybePlaces[randCountMP].y;

        // Add to array and create the boss room
        GameObject RoomObj = Instantiate(BossRoom, new Vector2((posX - offsetX) * RoomSizeX, (posY - offsetY) * RoomSizeY), Quaternion.identity);
        SpawnedRooms[arrMaybePlaces[randCountMP].x, arrMaybePlaces[randCountMP].y] = RoomObj;
    }

    void Doors()
    {
        for (int x = 0; x < SpawnedRooms.GetLength(0); x++)
        {
            for (int y = 0; y < SpawnedRooms.GetLength(1); y++)
            {
                if (SpawnedRooms[x, y] != null)
                {
                    Room RoomScript = SpawnedRooms[x, y].GetComponent<Room>();

                    if (x > 0 && SpawnedRooms[x - 1, y] != null)                             RoomScript.OpenDoor(3);
                    if (y > 0 && SpawnedRooms[x, y - 1] != null)                             RoomScript.OpenDoor(2);
                    if (x < SpawnedRooms.GetLength(0) - 1 && SpawnedRooms[x + 1, y] != null) RoomScript.OpenDoor(1);
                    if (y < SpawnedRooms.GetLength(1) - 1 && SpawnedRooms[x, y + 1] != null) RoomScript.OpenDoor(0);

                    if ((x > 0 && SpawnedRooms[x - 1, y] == null) || x <= 0)                             RoomScript.CloseDoor(3);
                    if ((y > 0 && SpawnedRooms[x, y - 1] == null) || y <= 0)                             RoomScript.CloseDoor(2);
                    if ((x < SpawnedRooms.GetLength(0) - 1 && SpawnedRooms[x + 1, y] == null) || x >= SpawnedRooms.GetLength(0)) RoomScript.CloseDoor(1);
                    if ((y < SpawnedRooms.GetLength(1) - 1 && SpawnedRooms[x, y + 1] == null) || y >= SpawnedRooms.GetLength(1)) RoomScript.CloseDoor(0);
                }

            }
        }
    }
}
