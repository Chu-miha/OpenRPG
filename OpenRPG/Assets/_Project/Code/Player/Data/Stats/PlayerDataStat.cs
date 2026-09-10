using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDataStat", menuName = "Scriptable Objects/PlayerStat")]
public class PlayerDataStat : ScriptableObject
{
    [Header("Health")]
    public int health;
    public int maxHealth;

    [Header("Mana")]
    public int mana;
    public int maxMana;

    [Header("Stamina")]
    public int stamina;
    public int maxStamina;

    [Header("Attributes")]
    public int strength;
    public int agility;
    public int intelligence;
}
