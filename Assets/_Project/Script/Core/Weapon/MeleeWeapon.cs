using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Melee Weapon", menuName = "Weapons/Melee Weapon")]
public class MeleeWeapon : WeaponBase
{

    public float Range;
    public override void Attack()
    {
        throw new System.NotImplementedException();
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
