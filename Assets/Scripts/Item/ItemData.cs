using UnityEngine;

public enum ItemType
{
    Resource,
    Equipable,
    Consumable
}
public enum ConsumableType
{
    Health,
}
public enum BuffType
{
    Attack,
    AttackSpeed,
    Defence,
    Health,
    Critical,
}
[System.Serializable]
public class ItemDataConsumable
{
    public ConsumableType Type;
    public float Value;
}
[System.Serializable]
public class ItemDataBuff
{
    public BuffType Type;
    public float Time;
    public float Value;
}
[System.Serializable]
public class ItemDataEquip
{
    public BuffType Type;
    public float Value;
    public void Apply(ref CharacterStats stats)
    {
        switch (Type)
        {
            case BuffType.Attack:
                stats.Attack += (int)Value;
                break;
            case BuffType.AttackSpeed:
                stats.AttackSpeed += (int)Value;
                break;
            case BuffType.Defence:
                stats.Defence += (int)Value;
                break;
            case BuffType.Health:
                stats.Health += (int)Value;
                break;
            case BuffType.Critical:
                stats.Critical += (int)Value;
                break;
        }
    }

    public void Remove(ref CharacterStats stats)
    {
        switch (Type)
        {
            case BuffType.Attack:
                stats.Attack -= (int)Value;
                break;
            case BuffType.AttackSpeed:
                stats.AttackSpeed -= (int)Value;
                break;
            case BuffType.Defence:
                stats.Defence -= (int)Value;
                break;
            case BuffType.Health:
                stats.Health -= (int)Value;
                break;
            case BuffType.Critical:
                stats.Critical -= (int)Value;
                break;
        }
    }
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string DisplayName;
    public string Descrition;
    public ItemType Type;
    public Sprite Icon;
    public GameObject DropPrefab;

    [Header("Stacking")]
    public int MaxStackAmount;

    [Header("Consumable")]
    public ItemDataConsumable[] consumables;

    [Header("Buff")]
    public ItemDataBuff[] buffs;

    [Header("Equip")]
    public ItemDataEquip[] equips;
}
