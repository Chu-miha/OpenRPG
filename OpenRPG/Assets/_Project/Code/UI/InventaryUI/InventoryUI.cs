using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class InventoryUI : MonoBehaviour
{
    [SerializeField]
    private UIDocument uiDocument;
    [SerializeField]
    private VisualTreeAsset itemSlotTemplate;
    
    private IActionInput _actionInput;
    private PlayerInventory _playerInventory;
    private VisualElement _inventoryRoot;
    private InventoryView _inventoryView;

    [Inject]
    private void Construct(IActionInput actionInput, PlayerInventory playerInventory)
    {
        _actionInput = actionInput;
        _playerInventory = playerInventory;
    }

    private void Awake()
    {
        VisualElement root = uiDocument.rootVisualElement;

        _inventoryRoot = root.Q<VisualElement>("InventoryRoot");

        _inventoryView = new InventoryView(root, itemSlotTemplate);
        _inventoryView.SetInventory(_playerInventory);
    }

    private void Update()
    {
        if (_actionInput.InventoryPressed)
        {
            ToggleInventory();
        }
    }

    private void ToggleInventory()
    {
        bool isOpen = _inventoryRoot.style.display == DisplayStyle.Flex;

        if (isOpen)
        {
            Close();
            return;
        }

        Open();
    }

    private void Open()
    {
        _inventoryView.Build(_playerInventory.SlotCount);

        _inventoryView.UpdateAll(_playerInventory);

        _inventoryRoot.style.display = DisplayStyle.Flex;
    }

    private void Close()
    {
        _inventoryRoot.style.display = DisplayStyle.None;
    }
    
    private void OnDestroy()
    {
        _inventoryView?.Dispose();
    }
}
