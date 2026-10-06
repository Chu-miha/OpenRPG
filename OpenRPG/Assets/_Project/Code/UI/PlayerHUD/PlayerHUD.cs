using UniRx;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField]
    private UIDocument uiDocument;
    [SerializeField]
    private VisualTreeAsset quickSlotTemplate;

    private PlayerStats _playerStats;
    private PlayerInteraction _playerInteraction;
    private PlayerInventory _playerInventory;

    private PlayerHUDBarView _barView;
    private InteractionPromptView _promptView;
    private QuickSlotsView _quickSlotsView;
    
    
    [Inject]
    private void Construct(PlayerStats playerStats, PlayerInteraction playerInteraction,  PlayerInventory playerInventory)
    {
        _playerStats = playerStats;
        _playerInteraction = playerInteraction;
        _playerInventory = playerInventory;
    }

    private void Awake()
    {
        VisualElement root = uiDocument.rootVisualElement;

        _barView = new PlayerHUDBarView(root);
        
        VisualElement promptElement = root.Q<VisualElement>("InteractionPrompt");

        _promptView = new InteractionPromptView(promptElement);
        
        VisualElement quickSlotsElement = root.Q<VisualElement>("QuickSlotsHUD");

        _quickSlotsView = new QuickSlotsView(quickSlotsElement, quickSlotTemplate, _playerInventory);

        _quickSlotsView.Build();
    }
    
    private void Start()
    {
        SubscribeStats();
        SubscribeInteraction();
    }
    
    private void SubscribeInteraction()
    {
        _playerInteraction.CurrentTarget
            .Subscribe(_promptView.Update)
            .AddTo(this);
    }
    
    private void SubscribeStats()
    {
        _playerStats.Health.ReactiveValue
            .Subscribe(value =>
            {
                _barView.UpdateHealth(
                    value,
                    _playerStats.Health.MaxValue);
            });


        _playerStats.Mana.ReactiveValue
            .Subscribe(value =>
            {
                _barView.UpdateMana(
                    value,
                    _playerStats.Mana.MaxValue);
            });


        _playerStats.Stamina.ReactiveValue
            .Subscribe(value =>
            {
                _barView.UpdateStamina(
                    value,
                    _playerStats.Stamina.MaxValue);
            });
    }
    
    private void OnDestroy()
    {
        _quickSlotsView?.Dispose();
    }

}
