using UnityEngine;
using UnityEngine.UI;

public class UISub : UIBase
{
    private GameObject _mainObj;
    [SerializeField] private Button _mainBtn;

    protected override void Start()
    {
        base.Start();
        _mainObj = _uIManager.UIMainMenu.gameObject;
        _mainBtn.onClick.AddListener(OpenMain);
    }

    void OpenMain()
    {
        _uIManager.ResetUI();
        _mainObj.SetActive(true);
    }
}
