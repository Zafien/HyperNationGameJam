using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class CharacterUnit : BaseUnit, IAttack
{
    [TabGroup("Current Weapon")][SerializeField] protected WeaponBaseScriptable _Weapon;
    [TabGroup("Current Weapon")][SerializeField] protected int _damage;
    public BodyRotator BodyRotator;
    public bool isDamaging;
    public float CoolDown;
    // Update is called once per frame




    void Update()
    {
        if (BodyRotator.NearestEnemy != null && isDamaging == false)
        {
            StartCoroutine(Damageing());
        }
        else
        {

        }
        
    }

    [Button]
    public void DoAttackDamage(BaseUnit receiver, int damageAmount)
    {
        receiver.ModifyHealthAmount(damageAmount);
    }


    public IEnumerator Damageing()
    {
        isDamaging = true;

        var enemy = BodyRotator.NearestEnemy.GetComponent<BaseUnit>();
        while (enemy._healthData.IsAlive && BodyRotator.NearestEnemy != null)
        {
            Debug.Log("Coroutine started!");
            DoAttackDamage(enemy, _damage);
              yield return new WaitForSeconds(CoolDown);
        }
        Debug.Log("Coroutine Finshed!");
        isDamaging = false;
    }
}
