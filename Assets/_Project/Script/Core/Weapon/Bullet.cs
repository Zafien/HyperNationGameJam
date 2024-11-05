using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;  // Speed of the bullet
    public float lifeTime = 5f;  // Time before bullet is destroyed



    public void DoAttackDamage(BaseUnit receiver, int damageAmount)
    {
        receiver.ModifyHealthAmount(damageAmount);
    }

    private void OnTriggerEnter(Collider other)
    {
        var test = other.gameObject.GetComponent<BaseUnit>();
        if (other.gameObject.tag == "Enemy")
        {
            DoAttackDamage(test, 20);
            Destroy(this.gameObject);
        }
     
    }
}
