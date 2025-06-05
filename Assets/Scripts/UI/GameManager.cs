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
    private CharacterStats baseStats;
    private Character _character;

    // 프로퍼티, get만 지정
    public Character Character => _character;

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
        var stats = new CharacterStats(jobInfo, 10, 12, 9, 35, 40, 100, 25, 20000); // 추후 baseTable 만들 필요성 있음
        _character = new Character("Chad",stats);

        // 갱신
        UIManager.Instance.UIMainMenu.SetDetail(_character);
    }
}
