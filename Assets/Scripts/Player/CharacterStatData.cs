using UnityEngine;
using System.Numerics;

[CreateAssetMenu(fileName = "CharacterStatData", menuName = "Game Data/CharacterStatData")]
public class CharacterStatData : ScriptableObject
{
    public JobInfo Job;
    public int Level;
    public int MaxExp;
    public int NowExp;
    public int Attack;
    public int AttackSpeed;
    public int Defence;
    public int Health;
    public int Critical;

    [SerializeField]
    private string _coinString = "0";

    public BigInteger Coin
    {
        get => BigInteger.TryParse(_coinString, out var result) ? result : BigInteger.Zero;
        set => _coinString = value.ToString();
    }
    // 스크립터블 오브젝트에서 CharacterStats로 추출
    public CharacterStats ToCharacterStats()
    {
        return new CharacterStats(Job, Level, MaxExp, NowExp, Attack, AttackSpeed, Defence, Health, Critical, Coin);
    }
}
