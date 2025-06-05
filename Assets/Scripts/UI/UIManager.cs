using UnityEngine;

public class UIManager : MonoBehaviour
{   
    //�̱���
    public static UIManager Instance { get; private set; }

    [SerializeField] private UIMainMenu _uiMainMenu;
    [SerializeField] private UIStatus _uiStatus;
    [SerializeField] private UIInventory _uiInventory;

    // ������Ƽ, get�� ����
    public UIMainMenu UIMainMenu => _uiMainMenu;
    public UIStatus UIStatus => _uiStatus;
    public UIInventory UIInventory => _uiInventory;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public void ResetUI()
    {
        _uiStatus.gameObject.SetActive(false);
        _uiInventory.gameObject.SetActive(false);
    }
}
