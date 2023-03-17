using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : SpellByArea
{
    [SerializeField] private Collider2D _hitCollider;
    [SerializeField] private int _damage = 100;
    [SerializeField] private int _cooldown = 5;
    [SerializeField] private SpriteRenderer _spriteRenderer;    
    [SerializeField] Vector2 _fieldframesHeight;
    [SerializeField] Vector2 _fieldframesWidth;    
    List<GameObject> _hitObjects = new List<GameObject>();
    private bool _canActive = true;


    public override void Activation()
    {
        if (_canActive == true)
        {
            _canActive = false;
            _spriteRenderer.enabled = true;
            transform.position = new Vector3(Random.Range(_fieldframesHeight.x, _fieldframesHeight.y), Random.Range(_fieldframesWidth.x, _fieldframesWidth.y), 0);
            _hitCollider.enabled = true;
            StartCoroutine(Waiting());
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
        int damage = _damage + _hitObjects.Count * 10;
        foreach(GameObject _hitObject in _hitObjects)
        {
            _hitObject.GetComponent<Health>().TakeDamage(damage);
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
