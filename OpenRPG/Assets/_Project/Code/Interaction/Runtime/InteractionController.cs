using UnityEngine;

public class InteractionController
{
    private readonly IInteractionDetector _detector;
    
    public InteractionController(IInteractionDetector detector)
    {
        _detector = detector;
    }
    
    public IInteractable Interact(IInteractor interactor)
    {
        IInteractable interactable = _detector.Detect(interactor);

        if (interactable == null)
            return null;

        if (!interactable.CanInteract(interactor))
            return null;

        return interactable;
    }
}
