using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private int _damage=2;
    [SerializeField] private int _attackSpeed=1;
    [SerializeField] private int _attackRange = 2;
    private Health _targetHealth;
    private Coroutine _coroutineAttackCycle;

    public int AttackRange  => _attackRange; 

    public void SetTargetHealth(Health targetHealth)
    {
        _targetHealth = targetHealth;
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
                _targetHealth.TakeDamage(_damage);
            }
            else
            {
                Debug.Log("Нет целей для атаки");
                break;
            }          
        }
    }

}
