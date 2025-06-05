using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : UIBase
{
    private GameObject _statusObj;
    private GameObject _InventoryObj;
    [SerializeField] private Button _statusBtn;
    [SerializeField] private Button _InventoryBtn;
    [SerializeField] private Button _mainBtn;

    protected override void Start()
    {
        base.Start();

        _statusObj = _uIManager.UIStatus.gameObject;
        _InventoryObj = _uIManager.UIInventory.gameObject;
        _mainBtn.onClick.AddListener(OpenMainMenu);
        _statusBtn.onClick.AddListener(OpenStatus);
        _InventoryBtn.onClick.AddListener(OpenInventory);
        OpenMainMenu();
    }
    void OpenMainMenu()
    {
        _uIManager.ResetUI();
        ResetBtn(Define.Button.Main);
    }
    void OpenStatus()
    {
        _uIManager.ResetUI();
        ResetBtn(Define.Button.Status);
        _statusObj.SetActive(true);
    }
    void OpenInventory()
    {
        _uIManager.ResetUI();
        ResetBtn(Define.Button.Inventory);
        _InventoryObj.SetActive(true);
    }


    private void ResetBtn(Define.Button type)
    {
        _mainBtn.gameObject.SetActive(false);
        _statusBtn.gameObject.SetActive(false);
        _InventoryBtn.gameObject.SetActive(false);

        switch (type)
        {
            case Define.Button.Main:
                _statusBtn.gameObject.SetActive(true);
                _InventoryBtn.gameObject.SetActive(true);
                break;
            default:
                _mainBtn.gameObject.SetActive(true);
                break;
        }
    }
}
