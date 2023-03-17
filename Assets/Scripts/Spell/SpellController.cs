using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour
{
    [SerializeField] private Spell[] _spells;

    public void ActivationSpells(int numder)
    {
        if (numder+1 > _spells.Length)
            return;
        _spells[numder].Activation();        
    }
}
