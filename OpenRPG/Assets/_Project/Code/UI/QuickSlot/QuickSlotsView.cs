using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UIElements;

public class QuickSlotsView
{
    private readonly VisualElement _root;
    private readonly VisualTreeAsset _template;
    private readonly PlayerInventory _playerInventory;

    private readonly List<QuickSlotView> _slotViews = new();

    private readonly CompositeDisposable _disposables = new();

    public IReadOnlyList<QuickSlotView> SlotViews => _slotViews;

    public QuickSlotsView(VisualElement root, VisualTreeAsset template, PlayerInventory playerInventory)
    {
        _root = root;
        _template = template;
        _playerInventory = playerInventory;
        Subscribe();
    }

    private void Subscribe()
    {
        _playerInventory.QuickSlotChanged
            .Subscribe(UpdateSlot)
            .AddTo(_disposables);

        _playerInventory.InventoryChanged
            .Subscribe(_ => UpdateAll())
            .AddTo(_disposables);
    }

    public void Build()
    {
        _root.Clear();
        _slotViews.Clear();

        CreateSlot(0, "R");
        CreateSlot(1, "T");

        UpdateAll();
    }

    private void CreateSlot(int index, string key)
    {
        VisualElement slotElement = _template.Instantiate();

        QuickSlotView slotView = new QuickSlotView(slotElement, index, key);

        _slotViews.Add(slotView);

        _root.Add(slotView.Root);
    }

    private void UpdateSlot(int index)
    {
        if (index < 0 || index >= _slotViews.Count)
        {
            return;
        }

        Item item = _playerInventory.GetQuickItem(index);

        if (item == null)
        {
            _slotViews[index].Clear();
            return;
        }

        int quantity = _playerInventory.GetItemQuantity(item);

        _slotViews[index].SetItem(item, quantity);
    }

    private void UpdateAll()
    {
        for (int i = 0; i < _slotViews.Count; i++)
        {
            UpdateSlot(i);
        }
    }

    public void Dispose()
    {
        _disposables.Dispose();
    }
}
