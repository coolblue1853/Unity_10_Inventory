using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private List<ItemData> dataList;
    private Character character;
    private Inventory inventory;

    public void SpawnRandItem()
    {
        if (character == null)
            character = GameManager.Instance.Character;
        if (inventory == null)
            inventory = character.Inventory;

        if (character.Stats.Coin < Constant.ItemCost)
            return;

        // 코인감소
        var stats = character.Stats;
        stats.Coin -= Constant.ItemCost;
        character.Stats = stats;

        // 아이템 랜덤 생성
        int randNum = Random.Range(0, dataList.Count);
        inventory.AddItem(dataList[randNum]);
    }
}
