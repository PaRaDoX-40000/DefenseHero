using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] Attack _attack;
    [SerializeField] MovingToTarget _movingToTarget;
    [SerializeField] Health _targetHealth;
    Transform _targetTransform;

    void Start()
    {
        _targetTransform = _targetHealth.gameObject.transform;
        _attack.SetTargetHealth(_targetHealth);
        _movingToTarget.SetTargetTransform(_targetTransform);
        _attack.enabled = false;
        _movingToTarget.enabled = true;
    }

    
    void Update()
    {
        if((_targetTransform.position - transform.position).magnitude <= _attack.AttackRange)
        {
            _attack.enabled = true;
            _movingToTarget.enabled = false;
        }
        else
        {
            _attack.enabled = false;
            _movingToTarget.enabled = true;
        }
    }
}
