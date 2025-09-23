using UnityEngine;

[System.Serializable]
public class PlayerResources
{
    public int gold;      // 金币
    public int material;  // 材料数量
    public int rescued;   // 搜救人数（可以用于结算奖励）

    public PlayerResources(int startGold = 0, int startMaterial = 0)
    {
        gold = startGold;
        material = startMaterial;
        rescued = 0;
    }

    public void AddGold(int amount)
    {
        gold += amount;
    }

    public bool SpendGold(int amount)
    {
        if (gold >= amount)
        {
            gold -= amount;
            return true;
        }
        return false;
    }
}
