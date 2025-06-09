using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject _characterPrefab;
    [SerializeField] private GameObject _inventoryPrefab;
    [SerializeField] private CharacterStatData _defaultStatData; 
   
    private Character _character;
    [SerializeField] private UIManager _uiManager;
    private AudioManager _audioManager;
    public Character Character => _character;
    public UIManager UIManager => _uiManager;
    public AudioManager AudioManager => _audioManager;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        _audioManager = GetComponentInChildren<AudioManager>();
        _audioManager.PlayBGM(0);
    }

    public void SetData()
    {
        CharacterStats stats = _defaultStatData.ToCharacterStats();

        Inventory inventory = Instantiate(_inventoryPrefab).GetComponent<Inventory>();
        inventory.UiInventory = _uiManager.UIInventory;
        inventory.Init();

        _character = _characterPrefab.GetComponent<Character>();
        _character.Init("Chad", stats, inventory);

        // 옵저버 구독
        _character.OnStatsChanged += (newStats) =>
        {
            _uiManager.UIStatus.SetStatus(_character);
        };
        _character.OnCoinChanged += (coin) =>
        {
            _uiManager.UIMainMenu.SetDetail(_character);
        };

        _uiManager.UIMainMenu.SetDetail(_character);
    }
}
