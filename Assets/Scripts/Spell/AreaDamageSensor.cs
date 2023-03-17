using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDamageSensor : MonoBehaviour
{
    private bool _canTakeDamage=true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_canTakeDamage == true)
        {
            if (collision.gameObject.TryGetComponent<SpellByArea>(out SpellByArea spellByArea))
            {
                spellByArea.AddHitObject(gameObject);
            }
        }
        
    }
    private IEnumerator _canTakeDamageTimer()
    {
        _canTakeDamage = false;
        yield return new WaitForSeconds(0.06f);
        _canTakeDamage = true;
    }
}
