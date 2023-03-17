using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ControlEnemy : Spell
{
    [SerializeField] private EnemySpawner _enemySpawner;
   
    [SerializeField] private int _timeAction = 5;
    [SerializeField] private int _cooldown = 5;
    [SerializeField] private GameObject _hero;
    private GameObject _target;
    private GameObject _enemyTarget;
    private EnemyStateMachine _targrtEnemyStateMachine;
    private bool _canActive = true;

    public override void Activation()
    {
        if (_canActive == true)
        {
            
            List<Health> enemyHealth = _enemySpawner.ActiveEnemies.Where(q => q.gameObject.GetComponent<EnemyStateMachine>().Boss == false).ToList<Health>();
            if (enemyHealth.Count == 0)
            {
                return;
            }
            int randonNumber = Random.Range(0, enemyHealth.Count);
            _target = enemyHealth[randonNumber].gameObject;
            
            FaindNewTarget();
            StartCoroutine(TimeAction());
            
        }
    }

    private void FaindNewTarget()
    {
        _target.GetComponent<Attack>().MadeAttack.AddListener(ProvokeEnemy);
        _enemyTarget = FaindTarget().gameObject;
        _target.GetComponent<EnemyStateMachine>().SetTarget(_enemyTarget);
        _enemyTarget.GetComponent<Health>().CharacterDeath.AddListener(FaindNewTarget);
    }

    private void ProvokeEnemy()
    {
        _enemyTarget.GetComponent<EnemyStateMachine>().SetTarget(_target);
        _target.GetComponent<Attack>().MadeAttack.RemoveListener(ProvokeEnemy);
    }

    private Health FaindTarget()
    {
        float minRange = (_enemySpawner.ActiveEnemies[0].gameObject.transform.position - transform.position).magnitude;
        Health closeEnemy = _enemySpawner.ActiveEnemies[0];
        foreach (Health enemyHealth in _enemySpawner.ActiveEnemies)
        {
            float distance = (enemyHealth.gameObject.transform.position - transform.position).magnitude;
            if (minRange > distance)
            {
                closeEnemy = enemyHealth;
                minRange = distance;
            }
        }
        return closeEnemy;     
    }
    private void Clear()
    {
        _target.GetComponent<Attack>().MadeAttack.RemoveListener(ProvokeEnemy);
        _enemyTarget.GetComponent<Health>().CharacterDeath.RemoveListener(FaindNewTarget);
        _target.GetComponent<EnemyStateMachine>().SetTarget(_hero);
        _enemyTarget.GetComponent<EnemyStateMachine>().SetTarget(_hero);

        _target = null;
        _enemyTarget = null;
    }

    private IEnumerator TimeAction()
    {       
        yield return new WaitForSeconds(_timeAction);
        Clear();
        StartCoroutine(Cooldown());
    }
    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(_cooldown);
        _canActive = true;
    }

}
