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
    public event Action<int> OnCoinChanged;
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
            CharacterStats oldStats = _stats;
            _stats = value;

            if (oldStats.Coin != _stats.Coin)
            {
                OnCoinChanged?.Invoke(_stats.Coin);
            }
            OnStatsChanged?.Invoke(_stats);
        }
    }
    public Inventory Inventory { get; private set; }

    public void Init(string name, CharacterStats stats, Inventory inventory)
    {
        _name = name;
        _stats = stats;
        Inventory = inventory;
        Inventory.Character = this;
    }
}