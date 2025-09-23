using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Image icon; // 显示物品图标
    public Text priceText; // 显示价格
    public GameObject hoverPanel; // 悬停时显示的面板（简略信息）
    public Button buyButton; // 购买按钮

    private Item currentItem;
    private int currentPrice;

    public void Setup(Item item, int price)
    {
        currentItem = item;
        currentPrice = price;
        icon.sprite = item.icon;
        icon.enabled = true;
        priceText.text = price.ToString();
        hoverPanel.SetActive(false);
        buyButton.gameObject.SetActive(false);
    }

    public void Clear()
    {
        currentItem = null;
        icon.sprite = null;
        icon.enabled = false;
        priceText.text = "";
        hoverPanel.SetActive(false);
        buyButton.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (currentItem != null)
        {
            hoverPanel.SetActive(true);
            hoverPanel.GetComponentInChildren<Text>().text = currentItem.itemName; // 简略信息
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoverPanel.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (currentItem != null)
        {
            buyButton.gameObject.SetActive(true);
            buyButton.onClick.RemoveAllListeners();
            buyButton.onClick.AddListener(() =>
            {
                if (BagManager.Instance.playerResourses.gold >= currentPrice)
                {
                    BagManager.Instance.playerResourses.gold -= currentPrice;
                    BagManager.Instance.AddItem(currentItem);
                    BagManager.Instance.UpdateUI();
                    Debug.Log("购买成功：" + currentItem.itemName);
                    buyButton.gameObject.SetActive(false);
                }
                else
                {
                    Debug.Log("金币不足！");
                }
            });
        }
    }
}
