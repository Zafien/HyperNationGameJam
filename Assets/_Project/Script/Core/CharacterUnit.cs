using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using TMPro;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine.UIElements;

public class CharacterUnit : BaseUnit, IAttack
{
    [TabGroup("Arsenal Weapons")][SerializeField] protected WeaponBase _currWeapon;

    [TabGroup("TEST WEAPON")][SerializeField][ReadOnly] public WeaponData _WeaponData;
 
    [TabGroup("Current Weapon")][SerializeField] protected int _damage; //Inside scriptable
    [TabGroup("Current Weapon")][SerializeField] protected ParticleSystem MuzzleVfx;
    [TabGroup("Current Weapon")][SerializeField] protected Bullet Bullet;
    [TabGroup("Current Weapon")][SerializeField] protected Transform BulletSpawnPoint;
    [TabGroup("Current Weapon")] public float CoolDown;
    [TabGroup("Current Weapon"), Range(0f, 10f)] public float MainRange; //Closest more damage
    [TabGroup("Current Weapon"), Range(10f, 20f)] public float SecondRange; //second closes has slight damage
    [TabGroup("Current Weapon"), Range(20f, 30f)] public float ThirdRange; //third closes has minimal damage


    [TabGroup("UI")] [SerializeField] private CharacterHudManager _characterHud; //third closes has minimal damage

    public BodyRotator BodyRotator;
    public bool isDamaging;
    public bool IsMeleeMode;


    public bool isOnCooldown = false;

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

        StartMelee();
        _characterHud.SetPlayerCurrentHp(_healthData.HealthAmount);
      
    }

    protected override void SetScriptableData()
    {
        base.SetScriptableData();
        UpdateWeaponFromInspector(_currWeapon.WeaponData);


        _characterHud.SetImageMaxFloat(_characterHud.PlayerHudSkill, _WeaponData.CoolDown);
        _characterHud.SetSliderMaxHp(_healthData.HealthAmount);
    }
    void InitializeCurrWeapon()
    {

        if (_WeaponData.WeaponType == Weapon.Gun)
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
        if (BodyRotator.NearestEnemy != null && isDamaging == false && _WeaponData.WeaponType == Weapon.Gun)
        {
            StartCoroutine(Damaging(BodyRotator.NearestEnemy));
            Debug.LogError("IS SHOOTING");
        }
    }
    void StartMelee()
    {
        if (BodyRotator.NearestEnemy != null && isDamaging == false && _WeaponData.WeaponType == Weapon.Melee && isOnCooldown == false)
        {
            Melee();
            StartCoroutine(MeleeAttackCoolDown(_WeaponData.CoolDown));
        }
    }
 
    public void DoAttackDamage(BaseUnit receiver, float damageAmount)
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
            yield return new WaitForSeconds(_WeaponData.CoolDown);
        }
        Debug.Log("Coroutine Finshed!");
        isDamaging = false;
    }

    public void Shoot()
    {

        if (BodyRotator.NearestEnemy != null)
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
 
    private IEnumerator MeleeAttackCoolDown(float cooldownDuration)
    {
        Debug.Log("Ability used!");
        isOnCooldown = true;

        float elapsedTime = 0f;
  
        // Gradually update the fill amount of the radial image
        while (elapsedTime < cooldownDuration)
        {
            elapsedTime += Time.deltaTime;
            _characterHud.PlayerHudSkill.fillAmount = 1 - (elapsedTime / cooldownDuration); // Fill decreases over time
        
            yield return null; // Wait for the next frame
        }

      
        _characterHud.PlayerHudSkill.fillAmount = cooldownDuration; // Cooldown complete
        isOnCooldown = false;
        Debug.Log("Ability ready again!");
    }

    public void Melee()
    {
        //Add animator
        if (isOnCooldown == false)
        {
            _currWeapon.ActivateAttack();
            PerformAttack();
        }


    }


    void PerformAttack()
    {
        // Define the center of the sphere (e.g., the player's position)
        Vector3 attackPosition = transform.position + transform.forward * 5f;

        // Detect colliders within the attack radius
        Collider[] hitColliders = Physics.OverlapSphere(attackPosition, _WeaponData.Radius, BodyRotator.enemyLayerMask);

        foreach (Collider hitCollider in hitColliders)
        {
            BaseUnit test = hitCollider.GetComponent<BaseUnit>();
            // Try to get a component that can take damage (e.g., Health)
            DoAttackDamage(test, _WeaponData.Damage);
            Debug.LogError("GOT ENEMY");
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
            Debug.Log($"Distance from: {start} to: {end}");
        }
    }

    private void OnDrawGizmos()
    {
        if (_WeaponData.WeaponType == Weapon.Melee)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, MainRange);

            // Visualize the attack radius in the scene view
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position + transform.forward * 5f, _WeaponData.Radius);
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
