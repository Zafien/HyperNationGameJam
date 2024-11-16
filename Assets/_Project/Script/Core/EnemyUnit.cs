using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : BaseUnit
{
    BaseUnit basetest;
    public EnemyUI EnemyUI;
    // Update is called once per frame
    public override void Initialize(object data = null)
    {
        base.Initialize(data);

    }

    public override void OnSubscriptionSet()
    {
        base.OnSubscriptionSet();

   

    }
    private void Awake()
    {
        EnemyUI.SetMaxHp(200);
    }
    void Update()
    {
        EnemyUI.SetHealthBarUI(_healthData.HealthAmount);
    }
    

    public void DropSomething()
    {

    }



}
