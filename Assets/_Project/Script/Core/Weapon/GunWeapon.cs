using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Ranged Weapon", menuName = "Weapons/Ranged Weapon")]
public class GunWeapon : WeaponBase
{
    //public Bullet BulletPrefab;
    //public float RangeDamage;

    public GameObject Bullet;
    public Transform BulletSpawnPoint;
    public override void ActivateAttack()
    {

 

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

