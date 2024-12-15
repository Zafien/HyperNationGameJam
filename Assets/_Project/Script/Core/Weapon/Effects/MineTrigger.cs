using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineTrigger : MonoBehaviour
{


    public GameObject explosionEffect; // Reference to the explosion VFX prefab
    public float explosionRadius = 5f; // Radius of the explosion
    public LayerMask enemy;
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to an enemy
        if (other.CompareTag("Enemy"))
        {
            Explode();
        }
    }

    void Explode()
    {
        // Instantiate the explosion VFX
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        // Find nearby objects within the explosion radius
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius, enemy);

        foreach (Collider hitCollider in hitColliders)
        {
            BaseUnit test = hitCollider.GetComponent<BaseUnit>();
            // Try to get a component that can take damage (e.g., Health)
            DoAttackDamage(test, 5);
            Debug.LogError("GOT ENEMY");
        }


        // Destroy the mine object after a short delay to allow sound/VFX to finish
        Destroy(gameObject, 0.5f);
    }
    private void OnDrawGizmosSelected()
    {
        // Draw the explosion radius in the editor for visualization
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    public void DoAttackDamage(BaseUnit receiver, float damageAmount)
    {
        receiver.ModifyHealthAmount(damageAmount);
    }
}
