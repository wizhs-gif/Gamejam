using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;
    public List<Item> shopItems = new List<Item>();
    

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

    }

    

}

