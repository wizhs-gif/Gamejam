using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisaterManager : MonoBehaviour
{
    public int disaterCount;
    public GameObject[] rooms;
    public int disaterLevel = 1;
    public bool isDisaterVisible;
    public int maxLevel = 3;
    public int disaterDamage;
    
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

        if(disaterLevel == 1)
        {
            StartCoroutine(DisaterLevel());
            SmallDisater();
        }

        if(disaterLevel == 2)
        {
            StartCoroutine(DisaterLevel());
            MediumDisater();
        }

        if(disaterLevel == 3)
        {
            BigDisater();
        }

    }

    private IEnumerator DisaterLevel()
    {       
        if(disaterLevel <= maxLevel)
        {
            yield return new WaitForSeconds(30);
            disaterLevel++;
        }
    }   

    private IEnumerator DisaterStart()
    {
        
        
        DisaterHappen();
        yield return new WaitForSeconds(120);
    }

    //小型火灾
    public void SmallDisater()
    {
        isDisaterVisible = false;
        disaterDamage = 1;

        
    }
    //·中型火灾
    public void MediumDisater()
    {
        isDisaterVisible = false;
        disaterDamage = 2;
        
    }
    //大型火灾
    public void BigDisater()
    { 
        isDisaterVisible = true;
        disaterDamage = 3;
    
    }

    void Update()
    {
        StartCoroutine(DisaterStart());
    }


 
}
