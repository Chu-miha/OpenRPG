using UnityEngine;

public interface IInteractable
{
    bool CanInteract(IInteractor interactor);
    
    void Interact(IInteractor interactor);
}
