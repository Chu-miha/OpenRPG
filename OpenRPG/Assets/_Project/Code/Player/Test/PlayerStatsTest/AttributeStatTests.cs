using NUnit.Framework;
using UniRx;
using UnityEngine;


public class AttributeStatTests
{
     private AttributeStat _stat;

    [SetUp]
    public void SetUp()
    {
        _stat = new AttributeStat(50);
    }

    [Test]
    public void Constructor_SetsInitialValue()
    {
        Assert.That(_stat.Value, Is.EqualTo(50));
    }

    [Test]
    public void SetValue_WhenValueChanges_SetsNewValue()
    {
        _stat.SetValue(75);

        Assert.That(_stat.Value, Is.EqualTo(75));
    }

    [Test]
    public void SetValue_WhenValueDoesNotChange_DoesNothing()
    {
        _stat.SetValue(50);

        Assert.That(_stat.Value, Is.EqualTo(50));
    }

    [Test]
    public void Modify_WhenPositive_IncreasesValue()
    {
        _stat.Modify(10);

        Assert.That(_stat.Value, Is.EqualTo(60));
    }

    [Test]
    public void Modify_WhenNegative_DecreasesValue()
    {
        _stat.Modify(-10);

        Assert.That(_stat.Value, Is.EqualTo(40));
    }

    [Test]
    public void Modify_WhenZero_DoesNotChangeValue()
    {
        _stat.Modify(0);

        Assert.That(_stat.Value, Is.EqualTo(50));
    }

    [Test]
    public void ReactiveValue_WhenValueChanges_IsInvoked()
    {
        bool valueChanged  = false;

        _stat.ReactiveValue
            .Skip(1)
            .Subscribe(_ => valueChanged = true);

        _stat.SetValue(75);

        Assert.That(valueChanged, Is.True);
    }

    [Test]
    public void ReactiveValue_WhenValueDoesNotChange_IsNotInvoked()
    {
        bool valueChanged = false;

        _stat.ReactiveValue
            .Skip(1)
            .Subscribe(_ => valueChanged = true);

        _stat.SetValue(50);

        Assert.That(valueChanged, Is.False);
    }

    [Test]
    public void ReactiveValue_PassesNewValue()
    {
        int receivedValue = 0;

        _stat.ReactiveValue
            .Skip(1)
            .Subscribe(value => receivedValue = value);

        _stat.SetValue(75);

        Assert.That(receivedValue, Is.EqualTo(75));
    }

    [TestCase(0, 10, 10)]
    [TestCase(25, 10, 35)]
    [TestCase(50, -20, 30)]
    [TestCase(100, -50, 50)]
    public void Modify_WithDifferentValues_ProducesExpectedResult(
        int startValue,
        int amount,
        int expectedValue)
    {
        _stat = new AttributeStat(startValue);

        _stat.Modify(amount);

        Assert.That(_stat.Value, Is.EqualTo(expectedValue));
    }
}
