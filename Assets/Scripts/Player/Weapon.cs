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
            stats.Coin += (int)(stats.Attack * Constant.CritCoinRate);
            _character.Stats = stats;
        }
        else // ũ��Ƽ�� ����
        {
            var stats = _character.Stats;
            stats.Coin += stats.Attack;
            _character.Stats = stats;
        }
        _audioManager.PlaySFX("Attack");
        _enemy.Hit();
    }
}
