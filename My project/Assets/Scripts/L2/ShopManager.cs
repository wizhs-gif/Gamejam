using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [Header("商店物品")]
    public Item[] allItems;       // 可出售的所有物品
    public int[] prices;          // 对应物品的价格（长度要和 allItems 一样）

    [Header("UI 槽位")]
    public ShopItemSlot[] slots;  // 拖进所有格子

    void Start()
    {
        UpdateShopUI();
    }

    /// <summary>
    /// 刷新商店 UI，把物品和价格分配给每个格子
    /// </summary>
    public void UpdateShopUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < allItems.Length && allItems[i] != null)
            {
                int price = (i < prices.Length) ? prices[i] : 0; // 防止越界
                slots[i].Setup(allItems[i], price);
            }
            else
            {
                slots[i].Clear();
            }
        }
    }

    /// <summary>
    /// 外部调用：刷新商店
    /// </summary>
    public void RefreshShop()
    {
        UpdateShopUI();
    }
}
