using UnityEngine;

public interface IActionInput
{
    bool JumpPressed { get; }
    bool AttackPressed  { get; }
    bool InteractPressed { get; }
    bool UseFirstQuickSlot { get; }
    bool UseSecondQuickSlot { get; }
    bool InventoryPressed { get; }
}
