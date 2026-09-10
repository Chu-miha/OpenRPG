using UnityEngine;
using Zenject;

public class Player : MonoBehaviour
{
    public IPlayerMovement Movement { get; private set; }
    public PlayerStats PlayerStats { get; private set; }

    [Inject]
    private void Construct(IPlayerMovement movement, PlayerStats playerStats)
    {
        Movement = movement;
        PlayerStats = playerStats;
        
    }
}
