using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UISlot : MonoBehaviour
{
    private Inventory inventory;
    public ItemData Item = null;
    private bool _isEquiped = false;

    [SerializeField] private Image _icon;
    [SerializeField] private Image _base;
    [SerializeField] private GameObject _equiped;


    private void Start()
    {
        inventory = GameManager.Instance.Character.Inventory;
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
    public void ResetSlot()
    {
        Item = null;
        _icon.sprite = null;
        _icon.color = Constant.Alpha0;
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
            inventory.ApplyItemStat(Item);
        }
        else
        {
            _isEquiped = false;
            _equiped.SetActive(false);
            inventory.RemoveItemStat(Item);
        }


    }
}
