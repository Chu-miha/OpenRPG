using UnityEngine;

public interface IInteractionDetector
{
    IInteractable Detect(IInteractor interactor);
}
