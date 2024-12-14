using JetBrains.Annotations;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class WeaponBase : SerializedScriptableObject
{
    public WeaponData WeaponData;
    public GunWeapon GunWeaponData;
    public MeleeWeapon MeleeWeaponData;
    //public Weapon WeaponType;
    //public string WeaponName;
    //public int Damage;
    //public float AttackRate;
    //public Transform AttackPoint;

  

    //[TabGroup("Range"), Range(0f, 10f)] public float GunMainDamageRange; //Closest more damage
    //[TabGroup("Range"), Range(10f, 20f)] public float GunSecondDamageRange; //second closes has slight damage
    //[TabGroup("Range"), Range(20f, 30f)] public float GunThirdDamageRange; //third closes has minimal damage

    //public WeaponEffect weaponEffect; //Scriptable for effects

    protected float NextAttackTime = 0f;

    public abstract void ActivateAttack();


    public void DamageCalculation()
    {  

    }
    //public void ActivateEffect(Transform attackPoint) 
}

[System.Serializable]
public class WeaponData
{
    public WeaponData(WeaponData data)
    {
        Damage = data.Damage; //Weapon damage is inside the BUllet
        CoolDown = data.CoolDown;
        WeaponType = data.WeaponType;
        VFXSpawnLocation = data.VFXSpawnLocation;
        Radius = data.Radius;
        Range1 = data.Range1;
        Range2 = data.Range2;   
        Range3 = data.Range3;
        WeaponEffect = data.WeaponEffect;
        ParticleFx = data.ParticleFx;
    }
    public float Damage;
    public float CoolDown;
    public Weapon WeaponType;
    public Transform VFXSpawnLocation;
    public ParticleSystem ParticleFx;
    public float Radius;
    public float Range1;
    public float Range2;
    public float Range3;
    public WeapoonEffectBase WeaponEffect;



    //public Bullet Bullet;
    //public int Damage;



}

public enum Weapon
{
    Gun,
    Melee
}