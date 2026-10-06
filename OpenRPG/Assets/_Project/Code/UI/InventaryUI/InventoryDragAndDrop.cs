using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryDragAndDrop : IDisposable
{
    private readonly VisualElement _dragLayer;
    private readonly PlayerInventory _playerInventory;
    private readonly List<InventorySlotView> _slotViews;
    private readonly IReadOnlyList<QuickSlotView> _quickSlotViews;

    private VisualElement _ghost;
    private InventorySlotView _sourceSlot;
    private InventorySlotView _targetSlot;
    private QuickSlotView _targetQuickSlot;

    private bool _isDragging;

    public InventoryDragAndDrop(VisualElement dragLayer, PlayerInventory playerInventory, List<InventorySlotView> slotViews, IReadOnlyList<QuickSlotView> quickSlotViews)
    {
        _dragLayer = dragLayer;
        _playerInventory = playerInventory;
        _slotViews = slotViews;
        _quickSlotViews = quickSlotViews;

        RegisterEvents();
    }

    private void RegisterEvents()
    {
        _dragLayer.RegisterCallback<PointerDownEvent>(OnPointerDown);

        _dragLayer.RegisterCallback<PointerMoveEvent>(OnPointerMove);

        _dragLayer.RegisterCallback<PointerUpEvent>(OnPointerUp);
    }

    private void OnPointerDown(PointerDownEvent evt)
    {
        if (evt.button != 0)
            return;

        InventorySlotView slot = GetSlotAt(evt.position);

        if (slot == null)
            return;

        InventorySlot inventorySlot = _playerInventory.GetSlot(slot.Index);

        if (inventorySlot == null || inventorySlot.IsEmpty)
        {
            return;
        }

        StartDrag(slot, evt.position);

        _dragLayer.CapturePointer(evt.pointerId);

        evt.StopPropagation();
    }

    private void OnPointerMove(PointerMoveEvent evt)
    {
        if (!_isDragging)
            return;

        MoveGhost(evt.position);

        UpdateTarget(evt.position);
        
        UpdateQuickSlotTarget(evt.position);

        evt.StopPropagation();
    }

    private void OnPointerUp(PointerUpEvent evt)
    {
        if (!_isDragging)
            return;

        EndDrag(evt.position);

        if (_dragLayer.HasPointerCapture(evt.pointerId))
        {
            _dragLayer.ReleasePointer(evt.pointerId);
        }

        evt.StopPropagation();
    }

    private void StartDrag(InventorySlotView sourceSlot, Vector2 pointerPosition)
    {
        _sourceSlot = sourceSlot;
        _targetSlot = null;

        _isDragging = true;

        CreateGhost(pointerPosition);

        SetSlotDragging(_sourceSlot, true);
    }

    private void EndDrag(Vector2 pointerPosition)
    {
        InventorySlotView targetSlot = GetSlotAt(pointerPosition);

        QuickSlotView targetQuickSlot = GetQuickSlotAt(pointerPosition);

        if (targetSlot != null &&
            targetSlot != _sourceSlot)
        {
            TryMove(_sourceSlot.Index, targetSlot.Index);
        }
        else if (targetQuickSlot != null)
        {
            TryAssignQuickSlot(_sourceSlot.Index, targetQuickSlot.Index);
        }

        ClearTarget();
        ClearQuickSlotTarget();

        DestroyGhost();

        SetSlotDragging(_sourceSlot, false);

        _sourceSlot = null;
        _targetSlot = null;
        _targetQuickSlot = null;

        _isDragging = false;
    }

    private void TryMove(int fromIndex, int toIndex)
    {
        bool moved = _playerInventory.MoveItem(fromIndex, toIndex);

        if (!moved)
            return;

        UpdateSlot(fromIndex);
        UpdateSlot(toIndex);
    }

    private void UpdateSlot(int index)
    {
        if (index < 0 || index >= _slotViews.Count)
        {
            return;
        }

        InventorySlot slot = _playerInventory.GetSlot(index);
        
        _slotViews[index].SetSlot(slot);
    }

    private void CreateGhost(Vector2 pointerPosition)
    {
        Item item = _playerInventory.GetSlot(_sourceSlot.Index).Stack.Item;

        _ghost = new VisualElement();

        _ghost.name = "InventoryDragGhost";

        _ghost.style.position = Position.Absolute;

        float width = _sourceSlot.Root.worldBound.width;

        float height = _sourceSlot.Root.worldBound.height;

        _ghost.style.width = width;

        _ghost.style.height = height;

        _ghost.style.backgroundImage = new StyleBackground(item.Icon);

        _ghost.style.backgroundSize = new BackgroundSize(BackgroundSizeType.Contain);

        _ghost.style.backgroundPositionX = new BackgroundPosition(BackgroundPositionKeyword.Center);

        _ghost.style.backgroundPositionY = new BackgroundPosition(BackgroundPositionKeyword.Center);

        _ghost.style.backgroundRepeat = new BackgroundRepeat(Repeat.NoRepeat, Repeat.NoRepeat);

        _ghost.style.opacity = 0.75f;

        _ghost.pickingMode = PickingMode.Ignore;

        _ghost.style.left = pointerPosition.x - width * 0.5f;

        _ghost.style.top = pointerPosition.y - height * 0.5f;

        _dragLayer.Add(_ghost);
    }

    private void MoveGhost(Vector2 pointerPosition)
    {
        if (_ghost == null)
            return;

        float width = _ghost.resolvedStyle.width;

        float height = _ghost.resolvedStyle.height;

        _ghost.style.left = pointerPosition.x - width * 0.5f;

        _ghost.style.top = pointerPosition.y - height * 0.5f;
    }

    private void DestroyGhost()
    {
        if (_ghost == null)
            return;

        _ghost.RemoveFromHierarchy();
        _ghost = null;
    }

    private void UpdateTarget(Vector2 pointerPosition)
    {
        InventorySlotView target = GetSlotAt(pointerPosition);

        if (target == _targetSlot)
            return;

        ClearTarget();

        if (target == null || target == _sourceSlot)
        {
            return;
        }

        _targetSlot = target;

        SetSlotTarget(_targetSlot, true);
    }

    private void ClearTarget()
    {
        if (_targetSlot == null)
            return;

        SetSlotTarget(_targetSlot, false);

        _targetSlot = null;
    }

    private InventorySlotView GetSlotAt(Vector2 pointerPosition)
    {
        foreach (InventorySlotView slotView in _slotViews)
        {
            if (slotView.Root.worldBound.Contains(pointerPosition))
            {
                return slotView;
            }
        }

        return null;
    }

    private void SetSlotDragging(InventorySlotView slotView, bool dragging)
    {
        if (slotView == null)
            return;

        if (dragging)
        {
            slotView.Root.AddToClassList(
                "inventory-slot--dragging");
        }
        else
        {
            slotView.Root.RemoveFromClassList(
                "inventory-slot--dragging");
        }
    }

    private void SetSlotTarget(InventorySlotView slotView, bool target)
    {
        if (slotView == null)
            return;

        if (target)
        {
            slotView.Root.AddToClassList(
                "inventory-slot--target");
        }
        else
        {
            slotView.Root.RemoveFromClassList(
                "inventory-slot--target");
        }
    }
    
    private QuickSlotView GetQuickSlotAt(Vector2 pointerPosition)
    {
        foreach (QuickSlotView slotView in _quickSlotViews)
        {
            if (slotView.Root.worldBound.Contains(pointerPosition))
            {
                return slotView;
            }
        }
        return null;
    }
    
    private void UpdateQuickSlotTarget(Vector2 pointerPosition)
    {
        QuickSlotView target = GetQuickSlotAt(pointerPosition);

        if (target == _targetQuickSlot) return;

        ClearQuickSlotTarget();

        if (target == null) return;

        _targetQuickSlot = target;

        _targetQuickSlot.SetTarget(true);
    }
    
    private void ClearQuickSlotTarget()
    {
        if (_targetQuickSlot == null)
            return;

        _targetQuickSlot.SetTarget(false);

        _targetQuickSlot = null;
    }
    
    private void TryAssignQuickSlot( int inventoryIndex, int quickSlotIndex)
    {
        InventorySlot inventorySlot = _playerInventory.GetSlot(inventoryIndex);

        if (inventorySlot == null || inventorySlot.IsEmpty)
        {
            return;
        }

        Item item = inventorySlot.Stack.Item;

        if (item.Effects == null || item.Effects.Length == 0)
        {
            return;
        }

        _playerInventory.AssignQuickItem(quickSlotIndex, item);
    }
    
    public void Dispose()
    {
        _dragLayer.UnregisterCallback<PointerDownEvent>(OnPointerDown);

        _dragLayer.UnregisterCallback<PointerMoveEvent>(OnPointerMove);

        _dragLayer.UnregisterCallback<PointerUpEvent>(OnPointerUp);
    }
}
