using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerater : MonoBehaviour
{
    public GameObject[] rooms;
    public int maxRooms = 6;
    private List<Vector3> roomPosition = new List<Vector3>();
    void Start()
    {
        GenerateRooms();
    }

    void GenerateRooms()
    {

    }
        
}
