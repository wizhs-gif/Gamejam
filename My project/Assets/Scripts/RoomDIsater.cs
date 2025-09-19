using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDIsater : MonoBehaviour
{
    public bool isDisater = false;
    public GameObject room;
    public void Disater()
    {
        isDisater = true;
        Debug.Log(room.name + " is disatered");
    }

    void Update()
    {
        if (isDisater)
        {

            Disater();
        }
    }
}
