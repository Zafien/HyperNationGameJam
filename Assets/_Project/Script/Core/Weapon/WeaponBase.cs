using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class WeaponBase : SerializedScriptableObject
{
    public string weaponName;
    public int damage;
    public float attackRate;
    public Transform attackPoint;

    public WeapoonEffectBase WeaponEffect;

    //public WeaponEffect weaponEffect; //Scriptable for effects

    protected float nextAttackTime = 0f;

    public abstract void Attack();

    //public void ActivateEffect(Transform attackPoint) 
}
