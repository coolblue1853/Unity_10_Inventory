using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISlot : MonoBehaviour
{
    private Inventory inventory;
    public ItemData Item = null;
    private bool _isEquiped = false;

    [SerializeField] private Image _icon;
    [SerializeField] private GameObject _equiped;


    private void Start()
    {
        inventory = GameManager.Instance.Character.Inventory;
    }
    // 아이콘 업데이트
    public void UpdateIcon(Sprite icon)
    {
        _icon.sprite = icon;
        _icon.color = Constant.Alpha255;
    }

    public void SetItem(ItemData item)
    {
        Item = item;
        //_equiped.SetActive(false);
    }
    // 장착여부 업데이트

    // UI 초기화 함수
    public void ResetSlot()
    {
        Item = null;
        _icon.sprite = null;
        _icon.color = Constant.Alpha0;
    }

    // UI 등장 합수
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
