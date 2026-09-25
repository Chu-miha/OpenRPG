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
        return _interactionController.Interact(interactor);
    }
}
