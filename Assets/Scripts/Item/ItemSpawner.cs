using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private List<ItemData> _dataList;
    private Character _character;
    private Inventory _inventory;

    private readonly Dictionary<ItemRarity, float> rarityChances = new()
    {
        { ItemRarity.Common, 0.5f },
        { ItemRarity.Uncommon, 0.25f },
        { ItemRarity.Rare, 0.15f },
        { ItemRarity.Unique, 0.07f },
        { ItemRarity.Legendary, 0.03f }
    };

    //  ������ �̱� �Լ�
    public void SpawnRandItem()
    {
        if (_character == null)
            _character = GameManager.Instance.Character;
        if (_inventory == null)
            _inventory = _character.Inventory;

        if (_character.Stats.Coin < Constant.ItemCost)
            return;

        // ���� ����
        var stats = _character.Stats;
        stats.Coin -= Constant.ItemCost;
        _character.Stats = stats;

        // Ȯ���� ���� ��� ����
        ItemRarity selectedRarity = RollRarity();

        // �ش� ����� ������ ���͸�
        List<ItemData> candidates = _dataList.FindAll(item => item.Rarity == selectedRarity);

        if (candidates.Count > 0)
        {
            int rand = Random.Range(0, candidates.Count);
            _inventory.AddItem(candidates[rand]);
        }

    }
    // ��� ����
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
