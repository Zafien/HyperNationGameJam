using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : BaseUnit
{
    public EnemyUI EnemyUI;
    // Update is called once per frame

    private void Awake()
    {
        EnemyUI.SetMaxHp(200);
    }
    void Update()
    {
        EnemyUI.SetHealthBarUI(_healthData.HealthAmount);
    }



}
