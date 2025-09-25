using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagManager : MonoBehaviour
{
    public static BagManager  Instance;
    public List<Item> items = new List<Item>();
    public int capacity;
    public PlayerResourses playerResourses = new PlayerResourses();
    [Header("UI")]
    public Image[] slots;
    public Text goldText;
   

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        // 确保游戏开始时背包为空
        items.Clear();
    }

    void Start()
    {
        UpdateUI();
     
    }

    public class PlayerResourses
    {
        public int gold;
        public Item supplies;
    }

    public bool AddItem(Item item)
    {
        if(items.Count < capacity)
        {
            items.Add(item);
            UpdateUI();
            return true;
        }
        else{
            Debug.Log("背包已满");
            return false;
        }
    }

    // 重载方法：通过物品名称添加物品
    public bool AddItem(string itemName)
    {
        Item item = Resources.Load<Item>($"Items/{itemName}");
        if (item != null)
        {
            return AddItem(item);
        }
        else
        {
            Debug.LogWarning($"找不到物品: {itemName}");
            return false;
        }
    }

    // 主动清空背包（如在关卡开始/重置时调用）
    public void ClearBag()
    {
        items.Clear();
        UpdateUI();
    }

    public void UpdateUI()
    {
        //ui刷新逻辑
        for(int i = 0;i<slots.Length;i++)
        {
            if (i < items.Count)
            {
                slots[i].sprite = items[i].icon;
                slots[i].enabled = true; // 显示图标
            }
            else
            {
                slots[i].sprite = null;
                slots[i].enabled = false; // 隐藏空格子
            }
        }

        // 刷新金币显示
        if(goldText != null)
        {
            goldText.text = playerResourses.gold.ToString();
        }
    }
    
}

  

