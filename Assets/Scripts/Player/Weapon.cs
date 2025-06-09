using UnityEngine;


public class Weapon : MonoBehaviour
{
    private AudioManager _audioManager;
    private Character _character;
    [SerializeField] private Enemy _enemy; // 원래라면은 충돌이나 감지를 시켜줘야함
    public void Init(Character character)
    {
        _character = character;
        _audioManager = GameManager.Instance.AudioManager;

    }
    public void Attack()
    {
        int critNum =  Random.Range(1, 101);

        // 크리티컬 성공
        if(_character.Stats.Critical >= critNum)
        {
            var stats = _character.Stats;
            stats.Coin += (int)(stats.Attack * Constant.CritCoinRate);
            _character.Stats = stats;
        }
        else // 크리티컬 실패
        {
            var stats = _character.Stats;
            stats.Coin += stats.Attack;
            _character.Stats = stats;
        }
        _audioManager.PlaySFX("Attack");
        _enemy.Hit();
    }
}
