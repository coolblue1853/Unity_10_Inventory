using System;
using UnityEngine;
using static Define;

// 스탯 종류 구조체
[System.Serializable]
public struct CharacterStats
{
    public JobInfo Job;
    public int Level;
    public int MaxExp;
    public int NowExp;
    public int Attack;
    public int Defence;
    public int Health;
    public int Critical;
    public int Coin;


    public CharacterStats(JobInfo job,int level, int maxExp, int nowExp, int attack, int defence, int health, int critical, int coin)
    {
        Job = job;
        Level = level;
        MaxExp = maxExp;
        NowExp = nowExp;
        Attack = attack;
        Defence = defence;
        Health = health;
        Critical = critical;
        Coin = coin;
    }
}
public class Character : MonoBehaviour
{
    [SerializeField] private string _name;
    // 옵저버 패턴
    public event Action<CharacterStats> OnStatsChanged;
    public string Name
    {
        get => _name;
        set => _name = value;
    }

    [SerializeField]
    private CharacterStats _stats;
    public CharacterStats Stats
    {
        get => _stats;
        set
        {
            _stats = value;
            OnStatsChanged?.Invoke(_stats);
        }
    }
    public Inventory Inventory { get; private set; }

    // 생성자
    public Character(string name, CharacterStats stats, Inventory inventory)
    {
        _name = name;
        _stats = stats;
        Inventory = inventory;
    }
}