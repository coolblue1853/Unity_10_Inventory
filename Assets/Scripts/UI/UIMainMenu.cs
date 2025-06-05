using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : UIBase
{
    private GameObject _statusObj;
    private GameObject _InventoryObj;
    [SerializeField] private Button _statusBtn;
    [SerializeField] private Button _InventoryBtn;

    protected override void Start()
    {
        base.Start();

        _statusObj = _uIManager.UIStatus.gameObject;
        _InventoryObj = _uIManager.UIInventory.gameObject;
        _statusBtn.onClick.AddListener(OpenStatus);
        _InventoryBtn.onClick.AddListener(OpenInventory);

    }

    void OpenStatus()
    {
        _uIManager.ResetUI();
        _statusObj.SetActive(true);
    }
    void OpenInventory()
    {
        _uIManager.ResetUI();
        _InventoryObj.SetActive(true);
    }

}
