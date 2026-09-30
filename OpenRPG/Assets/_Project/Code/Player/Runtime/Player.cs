using System;
using UnityEngine;
using Zenject;

public class Player : MonoBehaviour, IItemUser, IHealable, IManaUser, IInteractor
{
    public IMovement Movement { get; private set; }
    public PlayerStats PlayerStats { get; private set; }
    
    public ResourceStat Health => PlayerStats.Health;
    public ResourceStat Mana => PlayerStats.Mana;
    public Transform Transform => transform;
    
    private IActionInput _actionInput;
    private PlayerInteraction _playerInteraction;
    private PlayerInventory _playerInventory;
    private PlayerItemUsage _playerItemUsage;

    [Inject]
    private void Construct(IMovement movement,
        PlayerStats playerStats,
        IActionInput actionInput,
        PlayerInteraction playerInteraction,
        PlayerInventory playerInventory,
        PlayerItemUsage playerItemUsage)
    {
        Movement = movement;
        PlayerStats = playerStats;
        _actionInput = actionInput;
        _playerInteraction = playerInteraction;
        _playerInventory = playerInventory;
        _playerItemUsage = playerItemUsage;
        
    }
    
    public bool TryGet<T>(out T capability) where T : class
    {
        capability = this as T;
        return capability != null;
    }
    
    private void Update()
    {
        _playerInteraction.UpdateTarget(this);

        HandleItemUsage();
        
        if (!_actionInput.InteractPressed)
            return;
       
        IInteractable interactable = _playerInteraction.PlayerInteract(this);
        if  (interactable == null) return;
        
        if (interactable is IPickable pickable)
        {
            if(!_playerInventory.AddItemToInventory(pickable.Item))
                return;
        }
        interactable.Interact(this);
    }
    
    private void HandleItemUsage()
    {
        if (_actionInput.UseFirstQuickSlot)
        {
            _playerItemUsage.UseQuickItem(0, this);
        }

        if (_actionInput.UseSecondQuickSlot)
        {
            _playerItemUsage.UseQuickItem(1, this);
        }
    }
}
