using UnityEngine;

public class PlayerStats
{
    public ResourceStat Health { get; }
    public ResourceStat Mana { get; }
    public ResourceStat Stamina { get; }

    public AttributeStat Strength { get; }
    public AttributeStat Agility { get; }
    public AttributeStat Intelligence { get; }
    

    public PlayerStats(PlayerDataStat data)
    {
        Health = new ResourceStat(data.health, data.maxHealth);
        Mana = new ResourceStat(data.mana, data.maxMana);
        Stamina = new ResourceStat(data.stamina, data.maxStamina);

        Strength = new AttributeStat(data.strength);
        Agility = new AttributeStat(data.agility);
        Intelligence = new AttributeStat(data.intelligence);
    }
}
