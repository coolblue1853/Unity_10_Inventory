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

        // ���ΰ���
        var stats = character.Stats;
        stats.Coin -= Constant.ItemCost;
        character.Stats = stats;

        // ������ ���� ����
        int randNum = Random.Range(0, dataList.Count);
        inventory.AddItem(dataList[randNum]);
    }
}
