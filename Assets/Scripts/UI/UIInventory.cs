using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : UIBase, IInventoryObserver
{
    Inventory inventory;
    [SerializeField] private GameObject _slot;
    [SerializeField] private TextMeshProUGUI _slotTxt;
    private GridLayoutGroup _slotGroup;
    List<UISlot> _slots;
    enum Objs
    {
        Content,
    }

    protected override void Start()
    {
        base.Start();
        inventory = GameManager.Instance.Character.Inventory;
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
        SetInvenCount();
        inventory.AddObserver(this);
    }

    private void SetInvenCount()
    {
        int itemCount = 0;
        for (int i = 0; i < Constant.InventoryCount; i++)
        {
            if (inventory.Items[i] != null)
                itemCount++;
        }
        _slotTxt.text = $"Inventory {itemCount} / {Constant.InventoryCount}";
    }
    public void OnInventoryChanged(ItemData[] items)
    {
        RefreshUI();
        SetInvenCount();
    }
    public void RefreshUI()
    {
        var items = inventory.Items;

        for (int i = 0; i < _slots.Count; i++)
        {
            var slot = _slots[i];
            var item = items[i];

            if (item != null)
            {
                slot.SetItem(item);
                slot.UpdateIcon(item);
            }
            else
            {
                slot.ResetSlot();
            }
        }
    }
}
