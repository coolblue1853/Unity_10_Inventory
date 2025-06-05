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
    public ItemData[] Items; // �������� ���� �迭
    public Inventory(UIInventory uiInventory)
    {
        UiInventory = uiInventory;
        _observers = new();
        Items = new ItemData[Constant.InventoryCount];
    }

    // ������ �߰�, ����, �ֽ�ȭ �Լ�
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
    // �κ��丮�� ������ �߰�
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

    // ����ġ �ݿ�
    public void ApplyItemStat(ItemData item)
    {
        if (item == null || item.equips == null) return;

        var stats = GameManager.Instance.Character.Stats;

        foreach (var equip in item.equips)
            equip.Apply(ref stats);

        GameManager.Instance.Character.Stats = stats; // ������ Ʈ����
    }
    public void RemoveItemStat(ItemData item)
    {
        if (item == null || item.equips == null) return;

        var stats = GameManager.Instance.Character.Stats;

        foreach (var equip in item.equips)
            equip.Remove(ref stats);

        GameManager.Instance.Character.Stats = stats; // ������ Ʈ����
    }
}
