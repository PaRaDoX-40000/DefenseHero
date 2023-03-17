using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth=10;
    private int _health = 10;
    public UnityEvent CharacterDeath;

    public int MaxHealth => _maxHealth; 

    private void Start()
    {
        _health = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Death();
        }
    }
    public void TakeHealing(int heal)
    {
        _health += heal;
        if(_health> _maxHealth)
        {
            _health = _maxHealth;
        }
    }

    private void Death()
    {
        CharacterDeath?.Invoke();
        CharacterDeath.RemoveAllListeners();
    } 
    public void RestoreAllHealth()
    {
        _health = _maxHealth;
    }
}
