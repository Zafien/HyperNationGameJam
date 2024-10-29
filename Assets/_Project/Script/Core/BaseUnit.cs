using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Rendering;
using System;
using System.Collections;
using UniRx;
using TMPro;
using Unity.VisualScripting;

public class BaseUnit : MonoExt, IHealth, IStatus
{

    [TabGroup("Reference")][SerializeField] protected BaseUnitScriptable _unitDataScriptable;
    [TabGroup("Health")][SerializeField][ReadOnly] public HealthData _healthData;
    // Start is called before the first frame update

    public Subject<int> OnHealthAmountChanged = new Subject<int>();
    // Update is called once per frame
    void Update()
    {
        
    }
    protected virtual void Start()
    {
        Initialize();
        OnSubscriptionSet();

    }

    public override void Initialize(object data = null)
    {
        base.Initialize(data);
        SetScriptableData();
        OnHealthAmountChanged = new Subject<int>();



    }

    protected virtual void SetScriptableData()
    {
        UpdateHealthDataFromInspector(_unitDataScriptable.HealthData);



    }
    private void UpdateHealthDataFromInspector(HealthData healthData) => _healthData = new HealthData(healthData);
    public void ModifyHealthAmount(int value)
    {
        if (_healthData.IsAlive)
        {
            int healthAmount = _healthData.HealthAmount -= value;
            OnHealthAmountChanged?.OnNext(healthAmount);
            //OnDeath();

        }
    }

    public void OnDeath()
    {
        Destroy(this);
    }

    public void AddStatus(Status status)
    {
       
    }

    public void RemoveStatus(Status status)
    {
       
    }
}
