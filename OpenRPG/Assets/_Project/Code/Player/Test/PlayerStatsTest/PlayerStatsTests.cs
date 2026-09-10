using UnityEngine;
using NUnit.Framework;

public class PlayerStatsTests
{
    private PlayerDataStat _data;
    private PlayerStats _playerStats;
    
    [SetUp]
    public void SetUp()
    {
        _data = ScriptableObject.CreateInstance<PlayerDataStat>();

        _data.health = 15;
        _data.maxHealth = 100;

        _data.mana = 25;
        _data.maxMana = 50;

        _data.stamina = 30;
        _data.maxStamina = 100;

        _data.strength = 10;
        _data.agility = 12;
        _data.intelligence = 15;

        _playerStats = new PlayerStats(_data);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(_data);
    }
    
     [Test]
    public void Constructor_InitializesAllStatsFromData()
    {
        Assert.That(_playerStats.Health.Value, Is.EqualTo(_data.health));
        Assert.That(_playerStats.Health.MaxValue, Is.EqualTo(_data.maxHealth));

        Assert.That(_playerStats.Mana.Value, Is.EqualTo(_data.mana));
        Assert.That(_playerStats.Mana.MaxValue, Is.EqualTo(_data.maxMana));

        Assert.That(_playerStats.Stamina.Value, Is.EqualTo(_data.stamina));
        Assert.That(_playerStats.Stamina.MaxValue, Is.EqualTo(_data.maxStamina));

        Assert.That(_playerStats.Strength.Value, Is.EqualTo(_data.strength));
        Assert.That(_playerStats.Agility.Value, Is.EqualTo(_data.agility));
        Assert.That(_playerStats.Intelligence.Value, Is.EqualTo(_data.intelligence));
    }

    [TestCase(1, 100)]
    [TestCase(50, 100)]
    [TestCase(99, 100)]
    [TestCase(100, 100)]
    public void Health_InitializesCorrectly(int health, int maxHealth)
    {
        _data.health = health;
        _data.maxHealth = maxHealth;

        _playerStats = new PlayerStats(_data);

        Assert.That(_playerStats.Health.Value, Is.EqualTo(health));
        Assert.That(_playerStats.Health.MaxValue, Is.EqualTo(maxHealth));
    }

    [TestCase(1, 50)]
    [TestCase(25, 50)]
    [TestCase(49, 50)]
    [TestCase(50, 50)]
    public void Mana_InitializesCorrectly(int mana, int maxMana)
    {
        _data.mana = mana;
        _data.maxMana = maxMana;

        _playerStats = new PlayerStats(_data);

        Assert.That(_playerStats.Mana.Value, Is.EqualTo(mana));
        Assert.That(_playerStats.Mana.MaxValue, Is.EqualTo(maxMana));
    }

    [TestCase(1, 100)]
    [TestCase(30, 100)]
    [TestCase(75, 100)]
    [TestCase(100, 100)]
    public void Stamina_InitializesCorrectly(int stamina, int maxStamina)
    {
        _data.stamina = stamina;
        _data.maxStamina = maxStamina;

        _playerStats = new PlayerStats(_data);

        Assert.That(_playerStats.Stamina.Value, Is.EqualTo(stamina));
        Assert.That(_playerStats.Stamina.MaxValue, Is.EqualTo(maxStamina));
    }

    [TestCase(1)]
    [TestCase(10)]
    [TestCase(50)]
    public void Strength_InitializesCorrectly(int strength)
    {
        _data.strength = strength;

        _playerStats = new PlayerStats(_data);

        Assert.That(_playerStats.Strength.Value, Is.EqualTo(strength));
    }

    [TestCase(1)]
    [TestCase(12)]
    [TestCase(50)]
    public void Agility_InitializesCorrectly(int agility)
    {
        _data.agility = agility;

        _playerStats = new PlayerStats(_data);

        Assert.That(_playerStats.Agility.Value, Is.EqualTo(agility));
    }

    [TestCase(1)]
    [TestCase(15)]
    [TestCase(50)]
    public void Intelligence_InitializesCorrectly(int intelligence)
    {
        _data.intelligence = intelligence;

        _playerStats = new PlayerStats(_data);

        Assert.That(_playerStats.Intelligence.Value, Is.EqualTo(intelligence));
    }
}
