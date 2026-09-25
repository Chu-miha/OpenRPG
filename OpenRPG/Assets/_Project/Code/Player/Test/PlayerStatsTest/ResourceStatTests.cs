using NUnit.Framework;
using UniRx;
using UnityEngine;

public class ResourceStatTests
{
     private ResourceStat _stat;

    [SetUp]
    public void SetUp()
    {
        _stat = new ResourceStat(
            currentValue: 50,
            maxValue: 100);
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
    public void Modify_WhenExceedsMaximum_ClampsToMaximum()
    {
        _stat.Modify(100);

        Assert.That(_stat.Value, Is.EqualTo(100));
    }

    [Test]
    public void Modify_WhenBelowMinimum_ClampsToMinimum()
    {
        _stat.Modify(-100);

        Assert.That(_stat.Value, Is.EqualTo(0));
    }

    [Test]
    public void SetValue_WhenAboveMaximum_ClampsToMaximum()
    {
        _stat.SetValue(150);

        Assert.That(_stat.Value, Is.EqualTo(100));
    }

    [Test]
    public void SetValue_WhenBelowMinimum_ClampsToMinimum()
    {
        _stat.SetValue(-50);

        Assert.That(_stat.Value, Is.EqualTo(0));
    }

    [Test]
    public void ValueChanged_WhenValueChanges_IsInvoked()
    {
        bool valueChanged = false;

        _stat.ReactiveValue
            .Skip(1)
            .Subscribe(_ => valueChanged = true);

        _stat.Modify(10);

        Assert.That(valueChanged, Is.True);
    }

    [Test]
    public void ValueChanged_WhenValueDoesNotChange_IsNotInvoked()
    {
        bool valueChanged = false;

        _stat.ReactiveValue
            .Skip(1)
            .Subscribe(_ => valueChanged = true);

        _stat.Modify(10);

        Assert.That(valueChanged, Is.True);
    }
    
    [Test]
    public void ReactiveValue_WhenModifyChangesValue_PassesNewValue()
    {
        int receivedValue = 0;

        _stat.ReactiveValue
            .Skip(1)
            .Subscribe(value => receivedValue = value);

        _stat.Modify(10);

        Assert.That(receivedValue, Is.EqualTo(60));
    }

    [TestCase(0, 10, 10)]
    [TestCase(25, 10, 35)]
    [TestCase(50, -20, 30)]
    [TestCase(90, 20, 100)]
    [TestCase(10, -50, 0)]
    public void Modify_WithDifferentValues_ProducesExpectedResult(
        int startValue,
        int amount,
        int expectedValue)
    {
        _stat = new ResourceStat(startValue, 100);

        _stat.Modify(amount);

        Assert.That(_stat.Value, Is.EqualTo(expectedValue));
    }
}
