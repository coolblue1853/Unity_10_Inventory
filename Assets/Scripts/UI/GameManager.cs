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
    private Character _character;
    // 
    [SerializeField] private InventoryManager _inventory;
    // ������Ƽ, get�� ����
    public Character Character => _character;
    public InventoryManager MainInventory => _inventory;

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
        _character = new Character("Chad",stats);

        // ����
        UIManager.Instance.UIMainMenu.SetDetail(_character);
    }
}
