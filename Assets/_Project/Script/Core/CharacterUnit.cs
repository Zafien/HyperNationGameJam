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
    [TabGroup("Main Weapon Data")][SerializeField] protected WeaponBase _currWeapon;
    [TabGroup("Main Weapon Data")][SerializeField][ReadOnly] public WeaponData _WeaponData;
    [TabGroup("Main Weapon Data")][SerializeField] public Transform _GunWeaponTransform;


    [TabGroup("Weapon Gizmos"), Range(0f, 10f)] public float MainRange; //Closest more damage
    [TabGroup("Weapon Gizmos"), Range(10f, 20f)] public float SecondRange; //second closes has slight damage
    [TabGroup("Weapon Gizmos"), Range(20f, 30f)] public float ThirdRange; //third closes has minimal damage
    [TabGroup("UI")] [SerializeField] private CharacterHudManager _characterHud; //third closes has minimal damage
    [TabGroup("UI")][SerializeField] private CharacterSkillUiManager _weaponEffectUI;
    public BodyRotator BodyRotator;
    public bool isDamaging;
    public bool IsMeleeMode;


    public bool isOnCooldown = false;
    public bool SkillCooldown = false;
    public Subject<Unit> OnGainingExp { get; private set; }


    
    public override void Initialize(object data = null)
    {
        base.Initialize(data);

    }

    void Awake()
    {
        InitializeCurrWeapon();
        OnGainingExp = new Subject<Unit>();
    }

    public override void OnSubscriptionSet()
    {
        base.OnSubscriptionSet();

        AddEvent(BodyRotator.OnStartShooting, _ => StartShooting());
        _characterHud.SetExp(_unitStats.CurrExp, _unitStats.MaxExp);
    }

    private void UpdateWeaponFromInspector(WeaponData weaponData) => _WeaponData = new WeaponData(weaponData);
  
    void Update()
    {
        DebugShootLine();

        StartMelee();
        StartActivatingSkill();
        _characterHud.SetPlayerCurrentHp(_unitStats.HealthAmount);

        //_characterHud.SetExp(_unitStats.CurrExp, _unitStats.MaxExp);
    }

    protected override void SetScriptableData()
    {
        base.SetScriptableData();
        UpdateWeaponFromInspector(_currWeapon.WeaponData);

        _characterHud.SetImageMaxFloat(_characterHud.PlayerCDImage, _WeaponData.CoolDown);
        _characterHud.SetSliderMaxHp(_unitStats.HealthAmount);

        SetGunTransform();
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
    public void SetGunTransform()
    {
        if (!IsMeleeMode)
        {
            _currWeapon.GunWeaponData.BulletSpawnPoint = _GunWeaponTransform;
        }

    }
    void StartShooting()
    {
        if (BodyRotator.NearestEnemy != null && isDamaging == false && _WeaponData.WeaponType == Weapon.Gun && isOnCooldown == false)
        {
            ShootEnemy();      
            Debug.LogError("IS SHOOTING");
            StartCoroutine(WeaponCoolDown(_currWeapon.WeaponData.CoolDown));
        }
    }
    void StartMelee()
    {
        if (BodyRotator.NearestEnemy != null && isDamaging == false && _WeaponData.WeaponType == Weapon.Melee && isOnCooldown == false)
        {
            MeleeEnemy();
            StartCoroutine(WeaponCoolDown(_currWeapon.WeaponData.CoolDown));
        }
    }

    void StartActivatingSkill()
    {
        if (_WeaponData.WeaponEffect == null)
        {
            return;
        }
        if (SkillCooldown == false)
        {
     
            ActivateWeaponEffectEnemy();
            StartCoroutine(SkillCoolDown(_WeaponData.WeaponEffect.Cooldown));
            Debug.LogError(_WeaponData.WeaponEffect.Cooldown + "asdasdwawd");
        }

    }
 
    public void DoAttackDamage(BaseUnit receiver, float damageAmount)
    {
        receiver.ModifyHealthAmount(damageAmount);
    }

   
    public void Shoot()
    {

        if (BodyRotator.NearestEnemy != null)
        {
            Vector3 start = _currWeapon.GunWeaponData.BulletSpawnPoint.position;
            Vector3 end = BodyRotator.NearestEnemy.transform.position;

            // Calculate the direction from spawn point to enemy
            Vector3 direction = (end - start).normalized;

            // Instantiate the bullet at the spawn point
            //    GameObject bullet = Instantiate(_currWeapon.GunWeaponData.Bullet, start, Quaternion.LookRotation(direction));
            //Instantiate(bullet, start, Quaternion.LookRotation(direction));
            Bullet bullet = BulletPuller.Instance.GetBullet();


            if (bullet != null)
            {
                // Apply velocity to the spawned bullet
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                // Set up the bullet's position, rotation, and direction
                bullet.transform.position = start;
                bullet.transform.rotation = Quaternion.LookRotation(direction);
                //bullet.SetDirection(direction);
                if (rb != null)
                {
                    rb.velocity = direction * 80f; // Adjust speed as necessary
                    //Debug.Log($"Bullet velocity set to {rb.velocity}");
                }
            }
            else
            {
                Debug.LogError("Failed to spawn bullet: Bullet pool is empty!");
            }
        }
    }


  

    public IEnumerator WeaponCoolDown(float CoolDownDuration)
    {
        Debug.Log("Ability used!");
        isOnCooldown = true;

        float elapsedTime = 0f;

        // Gradually update the fill amount of the radial image
        while (elapsedTime < CoolDownDuration)
        {
            elapsedTime += Time.deltaTime;
            _characterHud.PlayerCDImage.fillAmount = 1 - (elapsedTime / CoolDownDuration); // Fill decreases over time

            yield return null; // Wait for the next frame
        }


        _characterHud.PlayerCDImage.fillAmount = 1; // Cooldown complete
        isOnCooldown = false;
        Debug.Log("Ability ready again!");
    }

    public IEnumerator SkillCoolDown(float CoolDownDuration)
    {
        SkillCooldown = true;

        float elapsedTime = 0f;
        while (elapsedTime < CoolDownDuration)
        {
            elapsedTime += Time.deltaTime;
            //_characterHud.PlayerCDImage.fillAmount = 1 - (elapsedTime / CoolDownDuration); // Fill decreases over time

            yield return null; // Wait for the next frame
        }
        SkillCooldown = false;
    }

    public void ShootEnemy()
    {
        if (isOnCooldown == false)
        {
            Shoot();
        }
    }
    public void MeleeEnemy()
    {
        //Add animator
        if (isOnCooldown == false)
        {
            _currWeapon.ActivateAttack();
            PerformAttack();
        }
    }

    public void ActivateWeaponEffectEnemy()
    {
        if (_WeaponData.WeaponEffect == null)
        {
            Debug.LogError("Weapon or WeaponEffect is null!");
            return;
        }
        if (SkillCooldown == false)
        {
            var currentWeaponEffect = _WeaponData.WeaponEffect;
            Debug.LogError("SPAWNED Skill");
            currentWeaponEffect.ApplyEffect(transform.position, transform.forward);
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

    [Button]
    public void OnGainExp(float Exp)
    {
        _unitStats.CurrExp += Exp;
       
        OnGainingExp?.OnNext(Unit.Default);
        if (_unitStats.CurrExp >= _unitStats.MaxExp)
        {
            LevelUp();
            if (_unitStats.CurrLevel >= 3)
            {
                return;
            }
            _weaponEffectUI.OpenSkillUI();
            Debug.LogError("LEVEL UP");
          
   
        }
    }
    public void LevelUp()
    {

        //Reset Value of UI slider 

        _unitStats.CurrExp -= _unitStats.MaxExp;
        _unitStats.CurrLevel++;
        _unitStats.MaxExp = CalculateNewMaxExp(_unitStats.CurrLevel);
        _characterHud.UpdateSliderExp(_unitStats.CurrExp, _unitStats.MaxExp);
    }

    private float CalculateNewMaxExp(int level)
    {
        // Example formula: MaxExp increases exponentially
        return 100 * Mathf.Pow(1.5f, level - 1);
    }

}
