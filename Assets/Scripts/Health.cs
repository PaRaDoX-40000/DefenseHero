using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int _health=10;
    public UnityEvent CharacterDeath;

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        CharacterDeath?.Invoke();
    } 
}
