using UnityEngine;
using UnityEngine.UI;

public class UISlot : MonoBehaviour
{
    public int Idx = -1;
    private Inventory _inventory;
    private Character _character;
    public ItemData Item = null;
    private bool _isEquiped = false;

    [SerializeField] private Image _icon;
    [SerializeField] private Image _base;
    [SerializeField] private GameObject _equiped;


    private void Start()
    {
        _inventory = GameManager.Instance.Character.Inventory;
        _character = GameManager.Instance.Character;
    }
    // ������ ������Ʈ
    public void UpdateIcon(ItemData item)
    {
        _icon.sprite = item.Icon;
        _icon.color = Constant.Alpha255;
        switch (item.Rarity)
        {
            case ItemRarity.Common:
                _base.color = Constant.White;
                break;
            case ItemRarity.Uncommon:
                _base.color = Constant.Green;
                break;
            case ItemRarity.Rare:
                _base.color = Constant.Blue;
                break;
            case ItemRarity.Unique:
                _base.color = Constant.Pink;
                break;
            case ItemRarity.Legendary:
                _base.color = Constant.Orange;
                break;

        }
    }

    public void SetItem(ItemData item)
    {
        Item = item;
    }
    // �������� ������Ʈ

    // UI �ʱ�ȭ �Լ�
    public void ResetSlot(int idx = -1)
    {
        if(idx != -1)
            Idx = idx;

        Item = null;
        _icon.sprite = null;
        _icon.color = Constant.Alpha0;
    }

    public void OnClickItemSlot()
    {
        if (_inventory.IsSellMode)
            SellItem();
        else
            ToggleEquip();
    }
    // ������ �Ǹ�
    public void SellItem()
    {
        if (Item == null)
            return;

        //���� �߰�
        var stats = _character.Stats;
        stats.Coin += Constant.sellCost / 2;
        _character.Stats = stats;

        // �κ��丮 ����
        _base.color = Constant.White;
        if (_isEquiped)
        {
            _isEquiped = false;
            _equiped.SetActive(false);
            _inventory.RemoveItemStat(Item);
        }
        _inventory.DeleteItem(Idx);


        ResetSlot();
    }

    // UI ���� �ռ�
    public void ToggleEquip()
    {
        if (Item == null)
            return;

        if (!_isEquiped)
        {
            _isEquiped = true;
            _equiped.SetActive(true);
            _inventory.ApplyItemStat(Item);
        }
        else
        {
            _isEquiped = false;
            _equiped.SetActive(false);
            _inventory.RemoveItemStat(Item);
        }
    }
}
