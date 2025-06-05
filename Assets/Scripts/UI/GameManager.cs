using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static Define;

public class GameManager : MonoBehaviour
{
    // �̱���
    public static GameManager Instance { get; private set; }
    // �÷��̾� ����
    [SerializeField] private GameObject _characterPrefab;
    [SerializeField] private GameObject _inventoryPrefab;
    private Character _character;
    // UI�Ŵ���
    [SerializeField] private UIManager _uiManager;
    // ������Ƽ, get�� ����
    public Character Character => _character;
    public UIManager UIManager => _uiManager;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void SetData()
    {
        var jobInfo = new JobInfo(Job.Slave);
        // ���� baseTable ���� �ʿ伺 ����
        var stats = new CharacterStats(jobInfo, 10, 12, 9, 35, 40, 100, 25, 20000);
        Inventory inventory = Instantiate(_inventoryPrefab).GetComponent<Inventory>();
        inventory.UiInventory = _uiManager.UIInventory;
        inventory.Init();
        _character = Instantiate(_characterPrefab).GetComponent<Character>(); // �̸� ���������� ������ Character
        _character.Init("Chad", stats, inventory);


        // ������ ����
        _character.OnStatsChanged += (newStats) => {
            _uiManager.UIStatus.SetStatus(_character);
        };
        _character.OnCoinChanged += (newStats) => {
            _uiManager.UIMainMenu.SetDetail(_character);
        };
        // ����
        _uiManager.UIMainMenu.SetDetail(_character);

    }
}
