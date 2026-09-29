using UniRx;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField]
    private UIDocument uiDocument;

    private PlayerStats _playerStats;
    private PlayerInteraction _playerInteraction;

    private PlayerHUDBarView _barView;
    private InteractionPromptView _promptView;
    
    
    [Inject]
    private void Construct(PlayerStats playerStats, PlayerInteraction playerInteraction)
    {
        _playerStats = playerStats;
        _playerInteraction = playerInteraction;
    }

    private void Awake()
    {
        VisualElement root = uiDocument.rootVisualElement;

        _barView = new PlayerHUDBarView(root);
        
        VisualElement promptElement = 
            root.Q<VisualElement>("InteractionPrompt");

        _promptView = new InteractionPromptView(promptElement);
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

}
