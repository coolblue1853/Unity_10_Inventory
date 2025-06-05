using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private List<ItemData> dataList;
    private InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = GameManager.Instance.MainInventory;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            inventoryManager.AddItem(dataList[0]);
        }
    }
}
