using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IInventoryObserver
{
    void OnInventoryChanged(ItemData[] items);
}
public class InventoryManager : MonoBehaviour
{
    [SerializeField] UIInventory uiInventory;
    private List<IInventoryObserver> observers = new();
    public ItemData[] Items;

    private void Awake()
    {
        Items = new ItemData[Constant.InventoryCount];
    }

    public void AddObserver(IInventoryObserver observer)
    {
        if (!observers.Contains(observer))
            observers.Add(observer);
    }
    public void RemoveObserver(IInventoryObserver observer)
    {
        observers.Remove(observer);
    }
    private void NotifyObservers()
    {
        foreach (var observer in observers)
            observer.OnInventoryChanged(Items);
    }
    public bool AddItem(ItemData item)
    {
        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i] == null)
            {
                Items[i] = item;
                NotifyObservers();
                return true;
            }
        }
        return false;
    }

    public void EquipItem(ItemData data)
    {
        var equips = data.equips;


    }
    public void UnEquipItem(ItemData data)
    {

    }
}
