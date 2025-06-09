using UnityEngine;
using System.Numerics;
using static Define;

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
    private string coinString = "0";

    public BigInteger Coin
    {
        get => BigInteger.TryParse(coinString, out var result) ? result : BigInteger.Zero;
        set => coinString = value.ToString();
    }

    public CharacterStats ToCharacterStats()
    {
        return new CharacterStats(Job, Level, MaxExp, NowExp, Attack, AttackSpeed, Defence, Health, Critical, Coin);
    }
}
