using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISlot : MonoBehaviour
{
    public ItemData Item = null;
    private bool _isEquiped = false;

    [SerializeField] private Image _icon;
    [SerializeField] private GameObject _equiped;



    // ������ ������Ʈ
    public void UpdateIcon(Sprite icon)
    {
        _icon.sprite = icon;
        _icon.color = Constant.Alpha255;
    }

    public void SetItem(ItemData item)
    {
        Item = item;
        _equiped.SetActive(false);
    }
    // �������� ������Ʈ

    // UI �ʱ�ȭ �Լ�
    public void ResetSlot()
    {
        Item = null;
        _icon.sprite = null;
        _icon.color = Constant.Alpha0;
        _equiped.SetActive(false);
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
        }
        else
        {
            _isEquiped = false;
            _equiped.SetActive(false);
        }


    }
}
