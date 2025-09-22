using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;
    public List<Item> shopItems = new List<Item>();
    
    [Header("UI")]
    public UnityEngine.UI.Text goldText; // 显示玩家金币
    public Transform itemContainer;
    public GameObject shopItemPrefab;
    private List<Item> currentShopItems = new List<Item>();
    public int itemsPerRefresh = 10;


    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private BagManager.PlayerResourses GetPlayerResourses()
    {
        return BagManager.Instance.playerResourses;
    }

    public bool BuyItem(Item item)
    {
        var resources = GetPlayerResourses();
        if(resources.gold >= item.price)
        {
            resources.gold -= item.price;
            Debug.Log("购买成功");
            // 添加到背包
            BagManager.Instance.AddItem(item);
            // 同步刷新
            BagManager.Instance.UpdateUI();
            UpdateShopUI();
            return true;
        }
        else
        {
            Debug.Log("购买失败");
            return false;

        }
        
    }

    public bool SellItem(Item item)
    {
        var resources = GetPlayerResourses();
        if(BagManager.Instance.items.Contains(item))
        {
            BagManager.Instance.items.Remove(item);
            resources.gold += item.price;
            Debug.Log("出售成功");
            BagManager.Instance.UpdateUI();
            UpdateShopUI();
            return true;
        }
        else
        {
            return false;
        }
    }

    private void UpdateShopUI()
    {
        var resources = GetPlayerResourses();
        //UI更新功能
        if(goldText != null)
        {
            goldText.text = resources.gold.ToString();
        }
        foreach (Transform child in itemContainer)
            Destroy(child.gameObject);

        // 清空当前商店物品
        currentShopItems.Clear();

        // 随机抽 itemsPerRefresh 个不重复物品
        List<Item> poolCopy = new List<Item>(shopItems);
        for (int i = 0; i < itemsPerRefresh && poolCopy.Count > 0; i++)
        {
            int index = Random.Range(0, poolCopy.Count);
            currentShopItems.Add(poolCopy[index]);
            poolCopy.RemoveAt(index); // 防止重复
        }

        // 显示到UI
        foreach (var item in currentShopItems)
        {
            GameObject obj = Instantiate(shopItemPrefab, itemContainer);
            obj.transform.Find("Icon").GetComponent<Image>().sprite = item.icon;
            obj.transform.Find("Name").GetComponent<Text>().text = item.itemName;
            obj.transform.Find("Price").GetComponent<Text>().text = item.price.ToString();

            Button buyButton = obj.transform.Find("BuyButton").GetComponent<Button>();
            buyButton.onClick.AddListener(() => BuyItem(item));
        }

    }
}

