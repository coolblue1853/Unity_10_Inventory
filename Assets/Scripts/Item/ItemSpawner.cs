using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private List<ItemData> dataList;
    private Inventory inventory;

    public void SpawnRandItem()
    {
        if (inventory == null)
            inventory = GameManager.Instance.Character.Inventory;

        int randNum = Random.Range(0, dataList.Count);
        inventory.AddItem(dataList[randNum]);
    }
}
