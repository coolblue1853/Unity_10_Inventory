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

    //  아이템 뽑기 함수
    public void SpawnRandItem()
    {
        if (_character == null)
            _character = GameManager.Instance.Character;
        if (_inventory == null)
            _inventory = _character.Inventory;

        if (_character.Stats.Coin < Constant.ItemCost)
            return;

        // 코인 차감
        var stats = _character.Stats;
        stats.Coin -= Constant.ItemCost;
        _character.Stats = stats;

        // 확률에 따라 등급 선택
        ItemRarity selectedRarity = RollRarity();

        // 해당 등급의 아이템 필터링
        List<ItemData> candidates = _dataList.FindAll(item => item.Rarity == selectedRarity);

        if (candidates.Count > 0)
        {
            int rand = Random.Range(0, candidates.Count);
            _inventory.AddItem(candidates[rand]);
        }

    }
    // 등급 지정
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
