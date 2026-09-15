using UnityEngine;

public class TestInteractable : MonoBehaviour, IInteractable
{
    public bool CanInteract(IInteractor interactor)
    {
        return true;
    }

    public void Interact(IInteractor interactor)
    {
        Debug.Log("Picked up test item!");
        Destroy(gameObject);
    }
}
