using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDIsater : MonoBehaviour
{
    public bool isDisater = false;
    public GameObject room;
    public List<GameObject> contents;//房间内的人和物品
    private int disaterLevel = 1;
    public int maxLevel = 4;
    public bool isDisaterVisible;
    public int disaterDamage;


    
    public void Disater()
    {
        isDisater = true;
        Debug.Log(room.name + " is disatered");
        
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
            StartCoroutine(DisaterLevel());
        }
        
    }

    void Update()
    {
        if (isDisater)
        {

            Disater();
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

    //小型火灾
    public void SmallDisater()
    {
        isDisaterVisible = false;
        

    }
    //中型火灾
    public void MediumDisater()
    {
        
        isDisaterVisible = false;
        Smoggy();
        DamageNearbyRooms();
        DamageContents(0.3f);
        
    }
    //大型灾难
    public void BigDisater()
    { 
        isDisaterVisible = true;
    
    }

    public void Ruin()
    {
        DestoryContents();
        SetRuin();
    }

    public void Smoggy()
    {
        //显示烟雾，玩家碰到烟雾受到伤害
    }

    public void DamageContents(float probability)
    {
        for(int i = contents.Count-1; i >= 0;i--)
        {
            if (Random.value < probability)
            {
                Destroy(contents[i]);
                contents.RemoveAt(i);
            }
        }
    }

    public void DamageNearbyRooms()
    {
        //隔壁房间施加掉血buff
    }

    public void DestoryContents()
    {
        foreach(var obj in contents)
        {
            Destroy(obj);
        }
        contents.Clear();
    }

    public void SetRuin()
    {
        //贴图更换为废墟
    }


}
