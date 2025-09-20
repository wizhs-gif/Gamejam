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
        if(items.Count <= capacity)
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
    }
    
}

  

