using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class CharacterUnit : BaseUnit, IAttack
{
    [TabGroup("Arsenal Weapons")][SerializeField] protected WeaponBase _currWeapon;

    [TabGroup("TEST WEAPON")][SerializeField][ReadOnly] public WeaponData _WeaponData;
    [TabGroup("Current Weapon")] public Weapon CurrentWeaponEnum; 
    [TabGroup("Current Weapon")][SerializeField] protected int _damage; //Inside scriptable
    [TabGroup("Current Weapon")][SerializeField] protected ParticleSystem MuzzleVfx;
    [TabGroup("Current Weapon")][SerializeField] protected Bullet Bullet;
    [TabGroup("Current Weapon")][SerializeField] protected Transform BulletSpawnPoint;
    [TabGroup("Current Weapon")] public float CoolDown;
    [TabGroup("Current Weapon"), Range(0f, 10f)] public float MainRange; //Closest more damage
    [TabGroup("Current Weapon"), Range(10f, 20f)] public float SecondRange; //second closes has slight damage
    [TabGroup("Current Weapon"), Range(20f, 30f)] public float ThirdRange; //third closes has minimal damage


    public BodyRotator BodyRotator;
    public bool isDamaging;
    public bool IsMeleeMode;


  

    public override void Initialize(object data = null)
    {
        base.Initialize(data);

    }

    public override void OnSubscriptionSet()
    {
        base.OnSubscriptionSet();

        AddEvent(BodyRotator.OnStartShooting, _ => StartShooting());

    }

    private void UpdateWeaponFromInspector(WeaponData weaponData) => _WeaponData = new WeaponData(weaponData);
    void Awake()
    {
        InitializeCurrWeapon();
    }
    void Update()
    {
        DebugShootLine();
  
        
    }

    protected override void SetScriptableData()
    {
        base.SetScriptableData();
        UpdateWeaponFromInspector(_currWeapon.WeaponData);

    }
    void InitializeCurrWeapon()
    {
        if (CurrentWeaponEnum == Weapon.Gun)
        {
            IsMeleeMode = false;
        }
        else
        {
            IsMeleeMode = true;

        }
    }
    void StartShooting()
    {
        if (BodyRotator.NearestEnemy != null && isDamaging == false)
        {
            StartCoroutine(Damaging(BodyRotator.NearestEnemy));
            //Vector3 directionToEnemy = BodyRotator.NearestEnemy.transform.position - transform.position;

            //// Ensure Y is zero for aiming only on the XZ plane
            //directionToEnemy.y = 0;

            //// If there is a valid direction, rotate towards the enemy
            //if (directionToEnemy != Vector3.zero)
            //{
            //    Quaternion targetRotation = Quaternion.LookRotation(directionToEnemy);
            //    BodyRotator.leftArm.transform.rotation = targetRotation; // Rotate the body towards the enemy
            //    StartCoroutine(Damageing(BodyRotator.NearestEnemy));
            //}
        }
        else
        {

        }
    }
 
    public void DoAttackDamage(BaseUnit receiver, int damageAmount)
    {
        receiver.ModifyHealthAmount(damageAmount);
    }

    public IEnumerator Damaging(BaseUnit target)
    {
        isDamaging = true;

      
        while (BodyRotator.NearestEnemy != null)
        {

            Debug.Log("Coroutine started!");
       
            //DoAttackDamage(target, _damage);

            Shoot();
            yield return new WaitForSeconds(CoolDown);
        }
        Debug.Log("Coroutine Finshed!");
        isDamaging = false;
    }

    public void Shoot()
    {

        if (BodyRotator.NearestEnemy != null == true)
        {
            Vector3 start = BulletSpawnPoint.position;
            Vector3 end = BodyRotator.NearestEnemy.transform.position;

            // Calculate the direction from spawn point to enemy
            Vector3 direction = (end - start).normalized;
        
            // Instantiate the bullet at the spawn point
            GameObject bullet = Instantiate(Bullet.gameObject, start, Quaternion.LookRotation(direction));
            Instantiate(MuzzleVfx, start, Quaternion.LookRotation(direction));
            // Apply velocity in the calculated direction

            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = direction * 80f;
            }
        }
    }

    private void DebugShootLine()
    {
        if (BodyRotator.NearestEnemy != null)
        {
            Vector3 start = BodyRotator.leftArm.position;
            Vector3 end = BodyRotator.NearestEnemy.transform.position;

            // Draw a red line to visualize the shot
            Debug.DrawLine(start, end, Color.red);

            // Log the start and end points for further debugging
            Debug.Log($"Shooting from: {start} to: {end}");
        }
    }

    private void OnDrawGizmos()
    {
        if (CurrentWeaponEnum == Weapon.Melee)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, MainRange);
        }
        else
        {

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, MainRange);

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, SecondRange);

            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(transform.position, ThirdRange);
        }
    }
}
