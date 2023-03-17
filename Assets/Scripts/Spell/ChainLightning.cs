using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainLightning : SpellByArea
{
    [SerializeField] private Collider2D _hitCollider;
    [SerializeField] private int _damage = 7;
    [SerializeField] private int _cooldown = 5;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private bool _active = false;
    List<GameObject> _hitObjects = new List<GameObject>();
    private bool _canActive = true;


    public override void Activation()
    {
        if(_canActive == true)
        {
            _canActive = false;
            _spriteRenderer.enabled = true;
            _active = true;
        }
       
    }
    private void Update()
    {
        if(_active != false)
        {
            Vector3 position = Camera.main.ViewportToWorldPoint(new Vector3(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height, 0));
            position.z = 0;
            transform.position = position;
            if (Input.GetMouseButton(0))
            {
                _active = false;
                _hitCollider.enabled = true;
                StartCoroutine(Waiting());               
            }
        }
    }

    private IEnumerator Waiting()
    {
        yield return new WaitForSeconds(0.05f);
        DealingDamage();
    }

    private void DealingDamage()
    {
        _hitCollider.enabled = false;        
        for(int i = 0; i < 6; i++)
        {
            if (_hitObjects.Count == 0)
            {
                break;
            }
            int number = Random.Range(0, _hitObjects.Count);

            if(_hitObjects[number].TryGetComponent<Health>(out Health health))
            {
                health.TakeDamage(_damage);
            }
            _hitObjects.Remove(_hitObjects[number]);

        }
       
        _hitObjects.Clear();
        _spriteRenderer.enabled = false;
        StartCoroutine(Cooldown());
        
    }

    public override void AddHitObject(GameObject hitObject)
    {
        _hitObjects.Add(hitObject);
    }
    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(_cooldown);
        _canActive = true;
    }
}
