using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]

public class Item : ScriptableObject
{
        public string itemName;
        public Sprite icon;
        public int price; // 购买或出售价格
        public bool isStackable; // 是否可堆叠
    
}
