using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


public class BodyRotator : MonoBehaviour
{

    [TabGroup("BodyParts")][SerializeField] public Transform upperBody;
    [TabGroup("BodyParts")][SerializeField] public Transform feetTransform;
    [TabGroup("BodyParts")][SerializeField] public Transform leftArm;
    [TabGroup("BodyPart Stats")][SerializeField] public float UpperBodyResetRotationSpeed;

    [TabGroup("Enemy Detection")][SerializeField] private List<GameObject> _enemies = new List<GameObject>();
    [TabGroup("Enemy Detection")][SerializeField] public GameObject NearestEnemy;
    [TabGroup("Enemy Detection")][SerializeField] public LayerMask enemyLayerMask;
    [TabGroup("Enemy Detection")][SerializeField] public float Radius;
    

    private Quaternion originalUpperBodyRotation;

    private void Start()
    {
        originalUpperBodyRotation = transform.localRotation;
    
    }
    public void Update()
    {
        CheckEnemiesInRange();
        if (NearestEnemy != null)
        {
            TargetEnemy();
        }
        else
        {
            StartCoroutine(ResetUpperBodyRotation());
        }
   
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
    public void CheckEnemiesInRange()
    {
        _enemies.Clear();
        float shortestDistance = Mathf.Infinity;
        Vector3 origin = transform.position;
        Collider[] colliders = Physics.OverlapSphere(origin, Radius, enemyLayerMask);
        if (colliders.Length > 0)
        {
            // Add enemies to the list
            foreach (var collider in colliders)
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);

                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    NearestEnemy = collider.gameObject; // Store the nearest enemy
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

        //if (distance > minDistance)
        //{ /* Rotate */

        //}
        Vector3 targetPosition = new Vector3(NearestEnemy.transform.position.x, 0, NearestEnemy.transform.position.z);
        upperBody.transform.LookAt(targetPosition);
        leftArm.transform.LookAt(targetPosition);
        //RotateHandTowardsEnemy();
    }

    void AddObjectToList(GameObject obj, List<GameObject> myList)
    {
        if (!myList.Contains(obj))
            myList.Add(obj);
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