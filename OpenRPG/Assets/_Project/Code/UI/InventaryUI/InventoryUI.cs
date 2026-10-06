using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class InventoryUI : MonoBehaviour
{
    [SerializeField]
    private UIDocument uiDocument;
    [SerializeField]
    private VisualTreeAsset itemSlotTemplate;
    [SerializeField]
    private VisualTreeAsset quickSlotTemplate;
    
    private IActionInput _actionInput;
    private PlayerInventory _playerInventory;
    private VisualElement _inventoryRoot;
    private VisualElement _quickSlotsPanel;
    private QuickSlotsView _quickSlotsView;
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
        _quickSlotsPanel = root.Q<VisualElement>("QuickSlotsPanel");

        _inventoryView = new InventoryView(root, itemSlotTemplate);
        _quickSlotsView = new QuickSlotsView(_quickSlotsPanel, quickSlotTemplate, _playerInventory);
        _inventoryView.SetInventory(_playerInventory, _quickSlotsView);
        
        _quickSlotsView.Build();
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
        _quickSlotsView?.Dispose();
    }
}
