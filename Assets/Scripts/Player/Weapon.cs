using UnityEngine;


public class Weapon : MonoBehaviour
{
    private AudioManager _audioManager;
    private Character _character;
    [SerializeField] private Enemy _enemy; // ��������� �浹�̳� ������ ���������
    public void Init(Character character)
    {
        _character = character;
        _audioManager = GameManager.Instance.AudioManager;

    }
    public void Attack()
    {
        int critNum =  Random.Range(1, 101);

        // ũ��Ƽ�� ����
        if(_character.Stats.Critical >= critNum)
        {
            var stats = _character.Stats;
            var attackPower = (int)(stats.Attack * Constant.CritCoinRate);
            stats.Coin += attackPower;
            _character.Stats = stats;
            _enemy.Hit(attackPower);
        }
        else // ũ��Ƽ�� ����
        {
            var stats = _character.Stats;
            stats.Coin += stats.Attack;
            _character.Stats = stats;
            _enemy.Hit(stats.Attack);
        }
        _audioManager.PlaySFX("Attack");
  
    }
}
