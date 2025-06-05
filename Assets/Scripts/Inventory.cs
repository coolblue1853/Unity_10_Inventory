using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IInventoryObserver
{
    void OnInventoryChanged(ItemData[] items);
}
public class Inventory : MonoBehaviour
{
    public UIInventory UiInventory;
    private List<IInventoryObserver> _observers = new();
    public ItemData[] Items; // 아이템이 담기는 배열
    public Inventory(UIInventory uiInventory)
    {
        UiInventory = uiInventory;
        _observers = new();
        Items = new ItemData[Constant.InventoryCount];
    }

    // 옵저버 추가, 제거, 최신화 함수
    public void AddObserver(IInventoryObserver observer)
    {
        if (!_observers.Contains(observer))
            _observers.Add(observer);
    }
    public void RemoveObserver(IInventoryObserver observer)
    {
        _observers.Remove(observer);
    }
    private void NotifyObservers()
    {
        foreach (var observer in _observers)
            observer.OnInventoryChanged(Items);
    }
    // 인벤토리에 아이템 추가
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

    // 스탯치 반영
    public void ApplyItemStat(ItemData item)
    {
        if (item == null || item.equips == null) return;

        var stats = GameManager.Instance.Character.Stats;

        foreach (var equip in item.equips)
            equip.Apply(ref stats);

        GameManager.Instance.Character.Stats = stats; // 옵저버 트리거
    }
    public void RemoveItemStat(ItemData item)
    {
        if (item == null || item.equips == null) return;

        var stats = GameManager.Instance.Character.Stats;

        foreach (var equip in item.equips)
            equip.Remove(ref stats);

        GameManager.Instance.Character.Stats = stats; // 옵저버 트리거
    }
}
