using UnityEngine;
using Zenject;

public class Player : MonoBehaviour, IItemUser, IHealable, IManaUser, IInteractor
{
    public IMovement Movement { get; private set; }
    public PlayerStats PlayerStats { get; private set; }
    public IInventory Inventory { get; private set; }
    
    public ResourceStat Health => PlayerStats.Health;
    public ResourceStat Mana => PlayerStats.Mana;
    public Transform Transform => transform;
    
    private IActionInput _actionInput;
    private InteractionController _interactionController;

    [Inject]
    private void Construct(IMovement movement, PlayerStats playerStats, IActionInput actionInput, InteractionController interactionController, IInventoryFactory inventoryFactory)
    {
        Movement = movement;
        PlayerStats = playerStats;
        _actionInput = actionInput;
        _interactionController = interactionController;
        Inventory = inventoryFactory.Create();
        
    }
    
    public bool TryGet<T>(out T capability) where T : class
    {
        capability = this as T;
        return capability != null;
    }
    
    private void Update()
    {
        if (!_actionInput.InteractPressed)
            return;

        IInteractable interactable = _interactionController.Interact(this);

        if (interactable == null)
            return;

        interactable.Interact(this);
    }
}
