using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingToTarget : MonoBehaviour
{
    [SerializeField] private float _speed = 0.5f;
    [SerializeField] private Transform _targetTransform;
    private Coroutine _coroutineMovementCycle;


    public void SetTargetTransform(Transform targetTransform)
    {
        _targetTransform = targetTransform;
    }

    private void OnEnable()
    {
        _coroutineMovementCycle = StartCoroutine(MovementCycle());
    }
    private void OnDisable()
    {
        StopCoroutine(_coroutineMovementCycle);
    }

    private IEnumerator MovementCycle()
    {
        while (true)
        {
           
            if (_targetTransform != null)
            {
                Vector3 direction = _targetTransform.position - transform.position;
                if ((direction).magnitude > 0.5f)
                {
                    transform.Translate(direction.normalized * Time.deltaTime * _speed);
                }             
            }
            else
            {
                Debug.Log("Нет целей для движения");                
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
