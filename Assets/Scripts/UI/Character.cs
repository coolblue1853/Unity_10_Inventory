using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ���� ����ü
[System.Serializable]
public struct CharacterStats
{
    public int Level;
    public int Exp;
    public int Attack;
    public int Defence;
    public int Health;
    public int Critical;
}
public class Character : MonoBehaviour
{
    [SerializeField] private string _name;
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
        set => _stats = value;
    }

    // �׳� CharacterStats �� �Ѱ��༭ ����
    public Character(string name, CharacterStats stats)
    {
        _name = name;
        _stats = stats;
    }

    // ���� ��� �������� �������༭ ����
    public Character(string name, int level, int exp, int attack, int defence, int health, int critical)
    {
        _name = name;
        _stats = new CharacterStats
        {
            Level = level,
            Exp = exp,
            Attack = attack,
            Defence = defence,
            Health = health,
            Critical = critical
        };
    }
}