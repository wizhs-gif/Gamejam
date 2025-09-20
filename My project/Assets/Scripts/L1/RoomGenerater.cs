using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerater : MonoBehaviour
{
    public List<GameObject> rooms;
    public int maxRooms = 16;
    public List<Transform> roomPosition;
    void Start()
    {
        
        SpawnRooms();
    }

    void SpawnRooms()
    {
        if(rooms.Count != roomPosition.Count)
        {
            Debug.Log("必须输入16个房间");
            return;
        }

        List<GameObject> ShuffleRooms = new List<GameObject>(rooms);
        Shuffle(ShuffleRooms);

        for(int i = 0; i < maxRooms; i++)
        {
            Instantiate(ShuffleRooms[i], roomPosition[i].position, roomPosition[i].rotation,transform);
        }

    }
    void Shuffle(List<GameObject> list)
    {
        for(int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            GameObject temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }
        
}
