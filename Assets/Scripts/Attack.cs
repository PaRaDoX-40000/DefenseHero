using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Attack : MonoBehaviour
{
    [SerializeField] private int _damage=2;
    [SerializeField] private float _attackSpeed=1;
    [SerializeField] private int _attackRange = 2;
    private int _additionalDamage=0;
    private Health _targetHealth;
    private Coroutine _coroutineAttackCycle;

    public UnityEvent MadeAttack;

    public int AttackRange  => _attackRange;
    public Health TargetHealth => _targetHealth; 

    public void SetTargetHealth(Health targetHealth)
    {
        _targetHealth = targetHealth;
    }

    public void AddDamage(int damage)
    {
        _additionalDamage += damage;
    }

    private void OnEnable()
    {
        _coroutineAttackCycle = StartCoroutine(attackCycle());
    }
    private void OnDisable()
    {
        StopCoroutine(_coroutineAttackCycle);
    }

    private IEnumerator attackCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(_attackSpeed);
            if (_targetHealth != null)
            {
                MadeAttack?.Invoke();
                _targetHealth.TakeDamage(_damage + _additionalDamage);
                _additionalDamage = 0;



            }                   
        }
    }

}
