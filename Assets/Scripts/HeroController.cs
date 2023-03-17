using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private Attack _attack;
    private Health _target;

    void Start()
    {
        
    }

    
    void Update()
    {
        if(_target == null)
        {
            float minRange = _attack.AttackRange;
            Health closeEnemy = null;
            foreach (Health enemyHealth in _enemySpawner.ActiveEnemies)
            {
                float distance = (enemyHealth.gameObject.transform.position - transform.position).magnitude;
                if (distance < _attack.AttackRange)
                {
                    if (closeEnemy == null)
                    {
                        closeEnemy = enemyHealth;
                        minRange = distance;
                    }
                    else
                    {
                        if(minRange > distance)
                        {
                            closeEnemy = enemyHealth;
                            minRange = distance;
                        }
                    }
                    
                }
            }
            if (closeEnemy != null)
            {
                _target = closeEnemy;
                _attack.SetTargetHealth(_target);
                _target.CharacterDeath.AddListener(TargetDeath);
                _attack.enabled = true;
            }
        }
    }

    private void TargetDeath()
    {
        _target = null;
        _attack.SetTargetHealth(null);
        _attack.enabled = false;

    }
}
