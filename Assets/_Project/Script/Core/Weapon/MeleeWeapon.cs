using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Melee Weapon", menuName = "Weapons/Melee Weapon")]
public class MeleeWeapon : WeaponBase
{

    //public float MeleeRange;
    //public float MeleeDamage;   
    public override void ActivateAttack()
    {

       var InstantiatedVFX = Instantiate(WeaponData.ParticleFx, WeaponData.VFXSpawnLocation);
        InstantiatedVFX.transform.parent = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


