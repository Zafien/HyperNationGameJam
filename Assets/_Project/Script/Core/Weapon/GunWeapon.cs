using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Ranged Weapon", menuName = "Weapons/Ranged Weapon")]
public class GunWeapon : WeaponBase
{
    [TabGroup("Gun Range"), Range(0f, 10f)] public float GunMainDamageRange; //Closest more damage
    [TabGroup("Gun Range"), Range(10f, 20f)] public float GunSecondDamageRange; //second closes has slight damage
    [TabGroup("Gun Range"), Range(20f, 30f)] public float GunThirdDamageRange; //third closes has minimal damage

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
