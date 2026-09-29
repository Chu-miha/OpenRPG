using System;
using UniRx;
using UnityEngine;

public class PlayerInteraction
{
    private InteractionController _interactionController;
    
    private readonly ReactiveProperty<IInteractable> _currentTarget =
        new ReactiveProperty<IInteractable>();


    public IReadOnlyReactiveProperty<IInteractable> CurrentTarget =>
        _currentTarget;

    public PlayerInteraction(InteractionController interactionController)
    {
        _interactionController = interactionController;
    }
    
    public void UpdateTarget(IInteractor interactor)
    {
        IInteractable target =
            _interactionController.Interact(interactor);

        if (_currentTarget.Value == target)
            return;

        _currentTarget.Value = target;
    }
    

    public IInteractable PlayerInteract(IInteractor interactor)
    {
        return _currentTarget.Value;
    }
}
