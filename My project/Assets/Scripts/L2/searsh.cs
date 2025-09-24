using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoxInteraction : MonoBehaviour
{
    // 存储工具名称的列表，可根据实际情况添加工具
    public List<string> tools = new List<string> { "锤子", "螺丝刀", "扳手", "钳子" };
    // 标记箱子是否已被点击
    private bool isClicked = false;
    // 标记是否已经获得工具
    private bool hasReceivedTool = false;
    // 记录点击的时间
    private float clickTime;
    // 点击后等待获得工具的时间（10 秒）
    public float waitTime = 10f;

    void Start()
    {
        // 初始化工具列表，确保至少有一个工具
        if (tools.Count == 0)
        {
            tools.Add("默认工具");
        }
    }

    void OnMouseDown()
    {
        // 当箱子未被点击且未获得工具时，记录点击时间并标记为已点击
        if (!isClicked && !hasReceivedTool)
        {
            isClicked = true;
            clickTime = Time.time;
            Debug.Log("已点击箱子，10 秒后将获得随机工具");
        }
    }

    void Update()
    {
        // 如果箱子已被点击且未获得工具，检查是否达到等待时间
        if (isClicked && !hasReceivedTool)
        {
            if (Time.time - clickTime >= waitTime)
            {
                // 从工具列表中随机选择一个工具
                string randomTool = tools[Random.Range(0, tools.Count)];
                hasReceivedTool = true;
                Debug.Log("获得随机工具：" + randomTool);
                // 这里可以添加获得工具后的其他逻辑，比如给玩家添加工具等
            }
        }
    }
}