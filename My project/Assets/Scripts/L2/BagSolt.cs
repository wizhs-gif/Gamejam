using UnityEngine;
using UnityEngine.UI;

public class BagSolt : MonoBehaviour
{
    public Image icon;
    private Item currentItem;
    public void SetItem(Item item)
    {
        currentItem = item;
        icon.sprite = item.icon;
        icon.enabled = true;

    }

    public void ClearItem(Item item)
    {
        currentItem = null;
        if(icon != null)
        {
            icon.sprite = null;
            icon.enabled = false;
        }
    }

}
