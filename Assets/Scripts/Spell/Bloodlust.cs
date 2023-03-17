using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloodlust : Spell
{
    [SerializeField] private Attack _attackHero;
    [SerializeField] private Health _healthHero;
    [SerializeField] private int _timeAction=5;
    [SerializeField] private int _cooldown=5;
    private int _counter = 0;
    private bool _canActive = true;
  
    public override void Activation()
    {
        if (_canActive == true)
        {
            _canActive = false;
            StartCoroutine(TimeAction());
        }       
    }

    private void EffectBloodlust()
    {
        _attackHero.AddDamage((_attackHero.TargetHealth.MaxHealth / 100) * 5);
        _counter++;
        
        if (_counter == 2)
        {
            _healthHero.TakeHealing((_attackHero.TargetHealth.MaxHealth / 100) * 5);
            _counter = 0;
        }
    }
    private IEnumerator TimeAction()
    {
        _attackHero.MadeAttack.AddListener(EffectBloodlust);
        yield return new WaitForSeconds(_timeAction);
        _attackHero.MadeAttack.RemoveListener(EffectBloodlust);
        StartCoroutine(Cooldown());
    }
    private IEnumerator Cooldown()
    {      
        yield return new WaitForSeconds(_cooldown);
        _canActive = true;
    }   
}
