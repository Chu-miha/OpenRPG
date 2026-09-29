using UnityEngine;
using UnityEngine.UIElements;

public class PlayerHUDBarView
{
    private readonly HealthBarView _health;
    private readonly ManaBarView _mana;
    private readonly StaminaBarView _stamina;


    public PlayerHUDBarView(VisualElement root)
    {
        VisualElement health =
            root.Q<VisualElement>("HealthBar");

        VisualElement mana =
            root.Q<VisualElement>("ManaBar");

        VisualElement stamina =
            root.Q<VisualElement>("StaminaBar");


        _health = new HealthBarView(health);
        _mana = new ManaBarView(mana);
        _stamina = new StaminaBarView(stamina);
    }


    public void UpdateHealth(int current, int max)
    {
        _health.UpdateValue(current, max);
    }


    public void UpdateMana(int current, int max)
    {
        _mana.UpdateValue(current, max);
    }


    public void UpdateStamina(int current, int max)
    {
        _stamina.UpdateValue(current, max);
    }
}
