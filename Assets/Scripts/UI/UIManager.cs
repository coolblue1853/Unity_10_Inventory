using UnityEngine;

public class UIManager : MonoBehaviour
{   
    //싱글턴
    public static UIManager Instance { get; private set; }

    [SerializeField] private UIMainMenu _uiMainMenu;
    [SerializeField] private UIStatus _uiStatus;
    [SerializeField] private UIInventory _uiInventory;

    // 프로퍼티, get만 지정
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
