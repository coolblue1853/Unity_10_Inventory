using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static Define;

public class GameManager : MonoBehaviour
{
    // 싱글턴
    public static GameManager Instance { get; private set; }
    // 플레이어 정보
    private Character _character;
    // UI매니저
    [SerializeField] private UIManager _uiManager;
    // 프로퍼티, get만 지정
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
        // 추후 baseTable 만들 필요성 있음
        var stats = new CharacterStats(jobInfo, 10, 12, 9, 35, 40, 100, 25, 20000);
        Inventory inventory = new Inventory(_uiManager.UIInventory);
        _character = new Character("Chad",stats, inventory);

        // 옵저버 구독
        _character.OnStatsChanged += (newStats) => {
            _uiManager.UIStatus.SetStatus(_character);
        };
        // 갱신
        _uiManager.UIMainMenu.SetDetail(_character);

    }
}
