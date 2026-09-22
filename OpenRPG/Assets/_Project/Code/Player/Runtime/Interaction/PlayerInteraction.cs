using System;
using UnityEngine;

public class PlayerInteraction
{
    private InteractionController _interactionController;

    public PlayerInteraction(InteractionController interactionController)
    {
        _interactionController = interactionController;
    }

    public IInteractable PlayerInteract(IInteractor interactor)
    {
        IInteractable interactable = _interactionController.Interact(interactor);

        if (interactable == null) throw new NullReferenceException();
        
        return interactable;
    }
}
