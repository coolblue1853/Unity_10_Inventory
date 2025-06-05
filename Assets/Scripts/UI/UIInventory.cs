using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : UIBase, IInventoryObserver
{
    InventoryManager inventoryManager;
    [SerializeField] private GameObject _slot;
    private GridLayoutGroup _slotGroup;
    List<UISlot> _slots;
    enum Objs
    {
        Content,
    }

    protected override void Start()
    {
        base.Start();
        inventoryManager = GameManager.Instance.MainInventory;
        _slots = new List<UISlot>();
        Bind<GridLayoutGroup>(typeof(Objs));

        _slotGroup = Get<GridLayoutGroup>((int)Objs.Content);

        Init();
    }

    private void Init()
    {
        for (int i = 0; i < Constant.InventoryCount; i++)
        {
            var go = Instantiate(_slot);
            go.transform.SetParent(_slotGroup.transform);
            go.transform.localScale = Vector3.one;
            var slot = go.GetComponent<UISlot>();
            slot.ResetSlot();
            _slots.Add(slot);
        }

        inventoryManager.AddObserver(this);
    }

    public void OnInventoryChanged(ItemData[] items)
    {
        RefreshUI();
    }
    public void RefreshUI()
    {
        var items = inventoryManager.Items;

        for (int i = 0; i < _slots.Count; i++)
        {
            var slot = _slots[i];
            var item = items[i];

            if (item != null)
            {
                slot.SetItem(item);
                slot.UpdateIcon(item.Icon);
            }
            else
            {
                slot.ResetSlot();
            }
        }
    }
}
