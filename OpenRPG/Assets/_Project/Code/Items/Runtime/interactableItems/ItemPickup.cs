using UnityEngine;

public class ItemPickup : MonoBehaviour, IPickable, IInteractable
{
    [field: SerializeField]
    public Item Item { get; private set; }

    public bool CanInteract(IInteractor interactor)
    {
        return Item != null;
    }

    public void Interact(IInteractor interactor)
    {
        Destroy(gameObject);
    }
}
