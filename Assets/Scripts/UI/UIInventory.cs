using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : UIBase, IInventoryObserver
{
    private Inventory _inventory;
    List<UISlot> _slots;
    [SerializeField] private GameObject _slot;
    [SerializeField] private TextMeshProUGUI _slotTxt;
    [SerializeField] private TextMeshProUGUI _togleBtnTxt;
    [SerializeField] private Button _modeTogleBtn;
    private GridLayoutGroup _slotGroup;

    private string sellStr = "현재모드 : 판매\n\n장착모드로 변경";
    private string eqiopStr = "현재모드 : 장착\n\n판매모드로 변경";


    enum Objs
    {
        Content,
    }

    protected override void Start()
    {
        base.Start();
        _inventory = GameManager.Instance.Character.Inventory;
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
            slot.ResetSlot(i);
            _slots.Add(slot);
        }
        SetInvenCount();
        _inventory.AddObserver(this);
    }

    private void SetInvenCount()
    {
        int itemCount = 0;
        for (int i = 0; i < Constant.InventoryCount; i++)
        {
            if (_inventory.Items[i] != null)
                itemCount++;
        }
        _slotTxt.text = $"Inventory {itemCount} / {Constant.InventoryCount}";
    }
    public void OnInventoryChanged(ItemData[] items)
    {
        RefreshUI();
        SetInvenCount();
    }

    public void TogleEquipSell()
    {
        _inventory.TogleMode();
        if (_inventory.IsSellMode)
            _togleBtnTxt.text = sellStr;
        else
            _togleBtnTxt.text = eqiopStr;
    }

    public void RefreshUI()
    {
        var items = _inventory.Items;

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
