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

    private VisualElement _inventoryRoot;
    private VisualElement _itemGrid;

    [Inject]
    private void Construct(IActionInput actionInput)
    {
        _actionInput = actionInput;
    }

    private void Awake()
    {
        VisualElement root = uiDocument.rootVisualElement;

        _inventoryRoot = root.Q<VisualElement>("InventoryRoot");
        _itemGrid = root.Q<VisualElement>("ItemGrid");
        
        CreateTestSlots();
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

        _inventoryRoot.style.display =
            isOpen ? DisplayStyle.None : DisplayStyle.Flex;
    }
    
    private void CreateTestSlots()
    {
        for (int i = 0; i < 120; i++)
        {
            VisualElement slot = itemSlotTemplate.Instantiate();

            _itemGrid.Add(slot);
        }
    }
}
