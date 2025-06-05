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
                // 필요한 항목 계속 추가
        }
    }

    public void Remove(ref CharacterStats stats)
    {
        switch (Type)
        {
            case BuffType.Attack:
                stats.Attack -= (int)Value;
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
