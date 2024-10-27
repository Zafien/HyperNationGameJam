using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUnit : BaseUnit
{
    [TabGroup("Current Weapon")][SerializeField] protected WeaponBaseScriptable _Weapon;
    [TabGroup("Current Weapon")][SerializeField] protected float _damage;
    // Update is called once per frame
    void Update()
    {
        
    }

    public override void DoDamage()
    {
        base.DoDamage();


    }
}
