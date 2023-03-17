using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private Attack _attack;
    [SerializeField] private MovingToTarget _movingToTarget;
    [SerializeField] private GameObject _target;
    Transform _targetTransform;

    public void SetTarget(GameObject target)
    {
        _target = target;
        _targetTransform = _target.transform;
        _attack.SetTargetHealth(_target.GetComponent<Health>());
        _movingToTarget.SetTargetTransform(_targetTransform);
        _attack.enabled = false;
        _movingToTarget.enabled = true;
    }

    void Start()
    {
        
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
