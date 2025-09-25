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

    [Header("烟雾设置")]
    public GameObject smokePrefab;
    public float smokeDamageInterval = 2f;

    public void Smoggy()
    {
        // 生成烟雾（假设在房间中心位置，可根据需要调整）
        if (smokePrefab != null)
        {
            GameObject smoke = Instantiate(smokePrefab, room.transform.position, Quaternion.identity);
            // 设置烟雾父物体为房间，方便管理
            smoke.transform.parent = room.transform;
            // 添加碰撞器作为触发器
            SphereCollider collider = smoke.AddComponent<SphereCollider>();
            collider.isTrigger = true;
            collider.radius = 3f; // 烟雾影响范围
            
            // 添加烟雾伤害逻辑组件
            SmokeHarm smokeHarm = smoke.AddComponent<SmokeHarm>();
            smokeHarm.Initialize(disaterDamage, smokeDamageInterval);
        }
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
    // 假设房间有特定标签"Room"，且相邻房间在同一父物体下
    if (room != null && room.transform.parent != null)
    {
        // 获取所有同级房间（同一父物体下的其他房间）
        foreach (Transform sibling in room.transform.parent)
        {
            // 排除当前房间本身
            if (sibling.gameObject != room && sibling.CompareTag("Room"))
            {
                // 计算房间间距，只对相邻近的房间生效
                float distance = Vector3.Distance(room.transform.position, sibling.position);
                if (distance < 5f) // 假设距离小于5米视为相邻
                {
                    // 为隔壁房间添加持续掉血buff
                    RoomDebuff debuff = sibling.gameObject.GetComponent<RoomDebuff>();
                    if (debuff == null)
                    {
                        debuff = sibling.gameObject.AddComponent<RoomDebuff>();
                    }
                    // 应用掉血效果：每3秒造成disaterDamage一半的伤害
                    debuff.ApplyDamageOverTime(disaterDamage / 2, 3f, 10f); // 伤害值、间隔、持续时间
                    Debug.Log($"为{sibling.name}施加掉血buff");
                }
            }
        }
    }
}

// 房间掉血buff组件
public class RoomDebuff : MonoBehaviour
{
    private int damagePerTick;
    private float tickInterval;
    private float duration;
    private float lastTickTime;
    private float startTime;
    private bool isActive = false;

    // 应用持续伤害效果
    public void ApplyDamageOverTime(int damage, float interval, float duration)
    {
        damagePerTick = damage;
        tickInterval = interval;
        this.duration = duration;
        lastTickTime = Time.time;
        startTime = Time.time;
        isActive = true;
    }

    private void Update()
    {
        if (isActive)
        {
            // 检查buff是否过期
            if (Time.time - startTime >= duration)
            {
                isActive = false;
                return;
            }

            // 定期对房间内的内容物造成伤害
            if (Time.time - lastTickTime >= tickInterval)
            {
                DamageRoomContents();
                lastTickTime = Time.time;
            }
        }
    }

    // 对房间内的内容物造成伤害
    private void DamageRoomContents()
    {
        RoomDIsater roomDisaster = GetComponent<RoomDIsater>();
        if (roomDisaster != null && roomDisaster.contents != null)
        {
            // 对房间内的每个内容物造成伤害（假设内容物有健康组件）
            foreach (var content in roomDisaster.contents)
            {
                PlayerHealth health = content.GetComponent<PlayerHealth>();
                if (health != null)
                {
                    health.TakeDamage(damagePerTick);
                }
            }
        }
    }
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

// 烟雾伤害逻辑组件
public class SmokeHarm : MonoBehaviour
{
    private int damage;
    private float interval;
    private float lastDamageTime;
    private bool isPlayerIn = false;
    private GameObject player;

    public void Initialize(int dmg, float intvl)
    {
        damage = dmg;
        interval = intvl;
        lastDamageTime = Time.time;
    }

    // 检测玩家进入烟雾
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerIn = true;
            player = other.gameObject;
            // 立即造成一次伤害
            DealDamage();
        }
    }

    // 检测玩家离开烟雾
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerIn = false;
            player = null;
        }
    }

    private void Update()
    {
        // 定时造成伤害
        if (isPlayerIn && Time.time - lastDamageTime >= interval)
        {
            DealDamage();
            lastDamageTime = Time.time;
        }
    }

    // 对玩家造成伤害
    private void DealDamage()
    {
        if (player != null)
        {
            PlayerHealth health = player.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }
    }
}

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth;
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        // 处理玩家受伤逻辑
    }
}
