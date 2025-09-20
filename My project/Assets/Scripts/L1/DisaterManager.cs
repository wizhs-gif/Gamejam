using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DisaterManager : MonoBehaviour
{
    public int disaterCount;
    public GameObject[] rooms;
    
    
    public void DisaterHappen()
    {
        disaterCount = Random.Range(1, rooms.Length);
        for(int i = 1; i <= rooms.Length; i++)
        {
            if(i == disaterCount)
            {
                rooms[i].GetComponent<RoomDIsater>().isDisater = true;
            }
        }
   
    }

   private IEnumerator DisaterStart()
    {
        DisaterHappen();
        yield return new WaitForSeconds(5);
    }

    void Start()
    {
        StartCoroutine(DisaterLoop());
    }

    private IEnumerator DisaterLoop()
    {
        while(true)
        {
            StartCoroutine(DisaterStart());
            yield return new WaitForSeconds(120); // 等待120秒后再次触发
        }
    }


 
}
