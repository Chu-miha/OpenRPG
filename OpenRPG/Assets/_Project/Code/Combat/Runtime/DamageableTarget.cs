using System;
using UnityEngine;
//ТЕСТ БОЕВОЙ СИСТЕМЫ
public class DamageableTarget : MonoBehaviour, IDamageable
{
    [SerializeField] private int _maxHealth = 30;
    
    private int _currentHealth;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(DamageInfo damageInfo)
    {
        if (_currentHealth <= 0)
            return;

        _currentHealth -= damageInfo.Amount;
        _currentHealth = Mathf.Max(0, _currentHealth);
        
        Debug.Log($"{gameObject.name} received {damageInfo.Amount} damage. HP: {_currentHealth}/{_maxHealth}");
        
        if (_currentHealth == 0) Destroy(gameObject);
    }
}
