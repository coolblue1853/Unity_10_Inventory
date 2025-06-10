using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static UnityEditor.Progress;
public interface IInventoryObserver
{
    void OnInventoryChanged(ItemData[] items);
}
public class Inventory : MonoBehaviour
{
    public UIInventory UiInventory;
    public Character Character;
    private List<IInventoryObserver> _observers = new();
    public ItemData[] Items; // �������� ���� �迭
    public bool IsSellMode = false;
    public void Init()
    {
        _observers = new();
        Items = new ItemData[Constant.InventoryCount];
    }

    public void TogleMode()
    {
        IsSellMode = !IsSellMode;
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
    // �κ��丮 ������ ����

    public void DeleteItem(int num)
    {
        Items[num] = null;
        NotifyObservers();
    }

    // ����ġ �ݿ�
    public void ApplyItemStat(ItemData item)
    {
        if (item == null || item.equips == null) return;

        var stats = Character.Stats;

        foreach (var equip in item.equips)
            equip.Apply(ref stats);

        Character.Stats = stats; // ������ Ʈ����
    }
    public void RemoveItemStat(ItemData item)
    {
        if (item == null || item.equips == null) return;

        var stats = Character.Stats;

        foreach (var equip in item.equips)
            equip.Remove(ref stats);

        Character.Stats = stats; // ������ Ʈ����
    }
}
