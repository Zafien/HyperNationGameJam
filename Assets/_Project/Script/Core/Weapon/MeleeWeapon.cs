using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Melee Weapon", menuName = "Weapons/Melee Weapon")]
public class MeleeWeapon : WeaponBase
{

    //public float MeleeRange;
    //public float MeleeDamage;   
    public override void ApplyDamage()
    {

        Instantiate(WeaponData.ParticleFx, WeaponData.VFXSpawnLocation);
        //var fxInstance = Instantiate(WeaponData.ParticleFx, WeaponData.VFXSpawnLocation.position * 20f, WeaponData.VFXSpawnLocation.rotation);
        //// In Update:
        //fxInstance.transform.position = WeaponData.VFXSpawnLocation.position;
        //fxInstance.transform.rotation = WeaponData.VFXSpawnLocation.rotation;
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


