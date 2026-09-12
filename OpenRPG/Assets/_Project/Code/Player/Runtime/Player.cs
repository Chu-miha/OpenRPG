using UnityEngine;
using Zenject;

public class Player : MonoBehaviour, IItemUser, IHealable, IManaUser
{
    public IPlayerMovement Movement { get; private set; }
    public PlayerStats PlayerStats { get; private set; }
    
    public ResourceStat Health => PlayerStats.Health;
    public ResourceStat Mana => PlayerStats.Mana;

    [Inject]
    private void Construct(IPlayerMovement movement, PlayerStats playerStats)
    {
        Movement = movement;
        PlayerStats = playerStats;
        
    }
    
    public bool TryGet<T>(out T capability) where T : class
    {
        capability = this as T;
        return capability != null;
    }
}
