using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;  // Speed of the bullet
    public float lifeTime = 1f;  // Time before bullet is destroyed
    public int Damage;


    //public float Range1;
    //public float Range2;
    //public float Range3;
    private void OnEnable()
    {
        // Automatically release the bullet after `lifetime` seconds
        Invoke(nameof(ReleaseToPool), lifeTime);
    }
    public void DoAttackDamage(BaseUnit receiver, int damageAmount)
    {
        receiver.ModifyHealthAmount(damageAmount);
    }

    private void OnTriggerEnter(Collider other)
    {
        var test = other.gameObject.GetComponent<BaseUnit>();
        if (other.gameObject.tag == "Enemy")
        {
            DoAttackDamage(test, Damage);
            ReleaseToPool();
        }
     
    }

    public void RangeChecker(float minRange,float MaxRange)
    {

    }

    private void ReleaseToPool()
    {
        BulletPuller.Instance.ReleaseBullet(this);
    }

}
