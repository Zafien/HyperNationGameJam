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



public class BodyRotator : MonoExt
{

    [TabGroup("BodyParts")][SerializeField] public Transform upperBody;
    [TabGroup("BodyParts")][SerializeField] public Transform feetTransform;
    [TabGroup("BodyParts")][SerializeField] public Transform leftArm;
    [TabGroup("BodyPart Stats")][SerializeField] public float UpperBodyResetRotationSpeed;

    [TabGroup("Enemy Detection")][SerializeField] private List<GameObject> _enemies = new List<GameObject>();
    [TabGroup("Enemy Detection")][SerializeField] public BaseUnit NearestEnemy;
    [TabGroup("Enemy Detection")][SerializeField] public LayerMask enemyLayerMask;

    [TabGroup("Melee Detection")][SerializeField] public float MeleeRadiusDetectionRange;
    [TabGroup("Range Detection")][SerializeField] public float RangeRadiusDetectionRange;
 

    public Subject<Unit> OnStartShooting { get; private set; }


    public Weapon CurrWeapon;
    private Quaternion originalUpperBodyRotation;

 

    private void Awake()
    {
        OnStartShooting = new Subject<Unit>();
    }


    private void Start()
    {
        Initialize();
        OnSubscriptionSet();

        originalUpperBodyRotation = transform.localRotation;
    }

    public void Update()
    {
        CheckEnemiesInRange(CurrWeapon);
        RemoveDeadObjects();
        if (NearestEnemy != null)
        {
            TargetEnemy();
            //RotateUpperBodyToTarget(upperBody,NearestEnemy.transform);
        }
        else
        {
            StartCoroutine(ResetUpperBodyRotation());
        }
    }

    private void OnDrawGizmos()
    {
        if (CurrWeapon == Weapon.Melee)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, MeleeRadiusDetectionRange);
        }
        else
        {    
           
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, RangeRadiusDetectionRange);
        }
    }

    public void CheckEnemiesInRange(Weapon CurrentWeapon)
    {
        _enemies.Clear();

        float detectionRange = 0;
        if (CurrentWeapon == Weapon.Melee)
        {
            detectionRange = MeleeRadiusDetectionRange;
        }
        else if (CurrentWeapon == Weapon.Gun)
        {
            detectionRange = RangeRadiusDetectionRange; 
        }
        float shortestDistance = Mathf.Infinity;
        Vector3 origin = transform.position;
        Collider[] colliders = Physics.OverlapSphere(origin, detectionRange, enemyLayerMask);
        if (colliders.Length > 0)
        {
            
            // Add enemies to the list
            foreach (var collider in colliders)
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    NearestEnemy = collider.gameObject.GetComponent<BaseUnit>(); // Store the nearest enemy
                }
            }
        }
        else
        {
            NearestEnemy = null;
    
        }
    }

    public void TargetEnemy()
    {
        if (NearestEnemy == null) return;

        // Rotation threshold for determining alignment
        float rotationThreshold = 5f; // Adjust for sensitivity

        // Calculate the target direction on the XZ plane
        Vector3 targetPosition = new Vector3(NearestEnemy.transform.position.x, upperBody.position.y, NearestEnemy.transform.position.z);
        Vector3 directionToTarget = targetPosition - upperBody.position;
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

        // Gradually rotate upper body towards target rotation, constrained to Y-axis
        float rotationSpeed = 1000f; // Adjust rotation speed as needed
        upperBody.rotation = Quaternion.RotateTowards(upperBody.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Lock rotation to Y-axis only
        upperBody.rotation = Quaternion.Euler(0, upperBody.rotation.eulerAngles.y, 0);

        // Check if the left arm is aligned within the threshold
        Quaternion leftArmTargetRotation = Quaternion.LookRotation(targetPosition - leftArm.position);
        float angleDifference = Quaternion.Angle(leftArm.rotation, leftArmTargetRotation);

        // If aligned, call OnStartShooting
        if (angleDifference <= rotationThreshold)
        {
            OnStartShooting?.OnNext(Unit.Default);
            Debug.Log("Shooting: Left arm aligned with the enemy.");
        }
    }

    public void RotateUpperBodyToTarget(Transform upperBody, Transform target)
    {
        // Ensure target is valid
        if (target == null)
            return;

        // Calculate the direction to the enemy on the XZ plane
        Vector3 directionToTarget = new Vector3(target.position.x, upperBody.position.y, target.position.z) - upperBody.position;

        // Calculate the target rotation using LookRotation
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

        // Smoothly rotate the upper body towards the target
        float rotationSpeed = 5f; // Adjust this speed as needed
        upperBody.rotation = Quaternion.RotateTowards(upperBody.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Zero out the x and z rotation to keep the upper body upright
        Quaternion adjustedRotation = upperBody.rotation;
        adjustedRotation.x = 0;
        adjustedRotation.z = 0;
        upperBody.rotation = adjustedRotation;
    }



    void AddObjectToList(GameObject obj, List<GameObject> myList)
    {
        if (!myList.Contains(obj))
            myList.Add(obj);
    }

    public void RemoveDeadObjects()
    {
        
        var test = GetComponent<BaseUnit>();
        //Remove Dead objects
        if (NearestEnemy != null && !test._healthData.IsAlive)
        {
            NearestEnemy = null;
            Debug.LogError("No Enemy");
        }
     
    }

    private IEnumerator ResetUpperBodyRotation()
    {
        NearestEnemy = null;
        float elapsedTime = 0f;
        float duration = UpperBodyResetRotationSpeed; // Time it takes to reset the rotation

        Quaternion currentRotation = upperBody.localRotation;

        // Smoothly interpolate the upper body back to its original rotation
        while (elapsedTime < duration)
        {
            upperBody.localRotation = Quaternion.Slerp(
                currentRotation,
                originalUpperBodyRotation,
                elapsedTime / duration
            );
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // Ensure the rotation is exactly the original at the end
        upperBody.localRotation = originalUpperBodyRotation;
    }
}
