using UnityEngine;

public interface IActionInput
{
    bool JumpPressed { get; }
    bool AttackPressed  { get; }
    bool InteractPressed { get; }
}
