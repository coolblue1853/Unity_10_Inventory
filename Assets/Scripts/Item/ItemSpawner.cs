using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private List<ItemData> dataList;
    private Character character;
    private Inventory inventory;

    // ��޺� Ȯ�� (���� 1.0 �Ǵ� 100%)
    private readonly Dictionary<ItemRarity, float> rarityChances = new()
    {
        { ItemRarity.Common, 0.5f },     // 50%
        { ItemRarity.Uncommon, 0.25f },  // 25%
        { ItemRarity.Rare, 0.15f },      // 15%
        { ItemRarity.Unique, 0.07f },    // 7%
        { ItemRarity.Legendary, 0.03f }  // 3%
    };

    public void SpawnRandItem()
    {
        if (character == null)
            character = GameManager.Instance.Character;
        if (inventory == null)
            inventory = character.Inventory;

        if (character.Stats.Coin < Constant.ItemCost)
            return;

        // ���� ����
        var stats = character.Stats;
        stats.Coin -= Constant.ItemCost;
        character.Stats = stats;

        // Ȯ���� ���� ��� ����
        ItemRarity selectedRarity = RollRarity();

        // �ش� ����� ������ ���͸�
        List<ItemData> candidates = dataList.FindAll(item => item.Rarity == selectedRarity);

        if (candidates.Count > 0)
        {
            int rand = Random.Range(0, candidates.Count);
            inventory.AddItem(candidates[rand]);
        }

    }

    private ItemRarity RollRarity()
    {
        float roll = Random.value; // 0.0 ~ 1.0
        float cumulative = 0f;

        foreach (var pair in rarityChances)
        {
            cumulative += pair.Value;
            if (roll <= cumulative)
                return pair.Key;
        }

        return ItemRarity.Common; 
    }
}
