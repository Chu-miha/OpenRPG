using UnityEngine;

public class ItemPickup : MonoBehaviour, IPickable, IInteractable, IInteractionInfo
{
    [field: SerializeField]
    public Item Item { get; private set; }
    
    public string InteractionText =>
        Item == null ? "" : $"Взять \"{Item.Name}\"";

    public bool CanInteract(IInteractor interactor)
    {
        return Item != null;
    }

    public void Interact(IInteractor interactor)
    {
        Destroy(gameObject);
    }
}
