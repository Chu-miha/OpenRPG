using System.Reflection;
using NUnit.Framework;
using UnityEngine;

public class InventoryTests
{
    private Inventory _inventory;
    private Item _itemA;
    private Item _itemB;

    [SetUp]
    public void SetUp()
    {
        _inventory = new Inventory();

        _itemA = CreateItem("item_a", "Item A");
        _itemB = CreateItem("item_b", "Item B");
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(_itemA);
        Object.DestroyImmediate(_itemB);
    }

    private static Item CreateItem(string id, string name)
    {
        Item item = ScriptableObject.CreateInstance<Item>();

        SetProperty(item, nameof(Item.Id), id);
        SetProperty(item, nameof(Item.Name), name);

        return item;
    }

    private static void SetProperty<T>(Item item, string propertyName, T value)
    {
        FieldInfo field = typeof(Item).GetField(
            $"<{propertyName}>k__BackingField",
            BindingFlags.Instance | BindingFlags.NonPublic
        );

        Assert.IsNotNull(field);

        field.SetValue(item, value);
    }

    [Test]
    public void Add_NewItem_AddsItem()
    {
        bool result = _inventory.Add(_itemA);

        Assert.IsTrue(result);
        Assert.AreEqual(1, _inventory.GetQuantity(_itemA));
    }

    [Test]
    public void Add_ExistingItem_IncreasesQuantity()
    {
        _inventory.Add(_itemA);
        _inventory.Add(_itemA);

        Assert.AreEqual(2, _inventory.GetQuantity(_itemA));
    }

    [Test]
    public void Add_WithAmount_IncreasesQuantityByAmount()
    {
        _inventory.Add(_itemA, 5);

        Assert.AreEqual(5, _inventory.GetQuantity(_itemA));
    }

    [Test]
    public void Add_DifferentItems_StoresThemSeparately()
    {
        _inventory.Add(_itemA);
        _inventory.Add(_itemB);

        Assert.AreEqual(1, _inventory.GetQuantity(_itemA));
        Assert.AreEqual(1, _inventory.GetQuantity(_itemB));
    }

    [Test]
    public void Add_NullItem_ReturnsFalse()
    {
        bool result = _inventory.Add(null);

        Assert.IsFalse(result);
    }

    [Test]
    public void Add_InvalidAmount_ReturnsFalse()
    {
        bool result = _inventory.Add(_itemA, 0);

        Assert.IsFalse(result);
        Assert.AreEqual(0, _inventory.GetQuantity(_itemA));
    }

    [Test]
    public void Remove_Item_DecreasesQuantity()
    {
        _inventory.Add(_itemA, 3);

        bool result = _inventory.Remove(_itemA);

        Assert.IsTrue(result);
        Assert.AreEqual(2, _inventory.GetQuantity(_itemA));
    }

    [Test]
    public void Remove_Amount_DecreasesQuantityByAmount()
    {
        _inventory.Add(_itemA, 5);

        bool result = _inventory.Remove(_itemA, 3);

        Assert.IsTrue(result);
        Assert.AreEqual(2, _inventory.GetQuantity(_itemA));
    }

    [Test]
    public void Remove_LastItem_RemovesItem()
    {
        _inventory.Add(_itemA);

        bool result = _inventory.Remove(_itemA);

        Assert.IsTrue(result);
        Assert.AreEqual(0, _inventory.GetQuantity(_itemA));
    }

    [Test]
    public void Remove_MoreThanAvailable_ReturnsFalse()
    {
        _inventory.Add(_itemA, 2);

        bool result = _inventory.Remove(_itemA, 3);

        Assert.IsFalse(result);
        Assert.AreEqual(2, _inventory.GetQuantity(_itemA));
    }

    [Test]
    public void Remove_UnknownItem_ReturnsFalse()
    {
        bool result = _inventory.Remove(_itemA);

        Assert.IsFalse(result);
    }

    [Test]
    public void Remove_NullItem_ReturnsFalse()
    {
        bool result = _inventory.Remove(null);

        Assert.IsFalse(result);
    }

    [Test]
    public void GetQuantity_UnknownItem_ReturnsZero()
    {
        Assert.AreEqual(0, _inventory.GetQuantity(_itemA));
    }

    [Test]
    public void GetQuantity_NullItem_ReturnsZero()
    {
        Assert.AreEqual(0, _inventory.GetQuantity(null));
    }
}
