using UnityEngine;
using Zenject;

public class Player : MonoBehaviour
{
    public IPlayerMovement Movement { get; private set; }

    [Inject]
    private void Construct(IPlayerMovement movement)
    {
        Movement = movement;
    }
}
