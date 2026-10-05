using UnityEngine;

public class InventorySlot
{
    public ItemStack Stack { get; private set; }

    public bool IsEmpty => Stack == null;

    public void SetStack(ItemStack stack)
    {
        Stack = stack;
    }

    public ItemStack TakeStack()
    {
        ItemStack stack = Stack;
        Stack = null;

        return stack;
    }

    public void Clear()
    {
        Stack = null;
    }
}
