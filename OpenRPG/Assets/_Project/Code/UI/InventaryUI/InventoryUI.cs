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
    [SerializeField] 
    private VisualTreeAsset combatSlotTemplate;
    
    private IActionInput _actionInput;
    private PlayerInventory _playerInventory;
    private VisualElement _inventoryRoot;
    private VisualElement _quickSlotsPanel;
    private QuickSlotsView _quickSlotsView;
    private InventoryView _inventoryView;
    private VisualElement _equipmentPanel;
    private EquipmentView _equipmentView;
    private PlayerEquipment _playerEquipment;
    private PlayerEquipmentService _equipmentService;

    [Inject]
    private void Construct(IActionInput actionInput, PlayerInventory playerInventory, PlayerEquipment playerEquipment, PlayerEquipmentService equipmentService)
    {
        _actionInput = actionInput;
        _playerInventory = playerInventory;
        _playerEquipment = playerEquipment;
        _equipmentService = equipmentService;
    }

    private void Awake()
    {
        VisualElement root = uiDocument.rootVisualElement;

        _inventoryRoot = root.Q<VisualElement>("InventoryRoot");
        _quickSlotsPanel = root.Q<VisualElement>("QuickSlotsPanel");
        _equipmentPanel = root.Q<VisualElement>("EquipmentPanel");

        _quickSlotsView = new QuickSlotsView(_quickSlotsPanel, quickSlotTemplate, _playerInventory);

        _quickSlotsView.Build();

        _equipmentView = new EquipmentView(_equipmentPanel, combatSlotTemplate, _playerEquipment);

        _equipmentView.Build();

        _inventoryView = new InventoryView(root, itemSlotTemplate);

        _inventoryView.SetInventory(_playerInventory, _quickSlotsView, _equipmentService, _playerEquipment, _equipmentView.CombatSlotViews);
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
        _equipmentView?.Dispose();
    }
}
