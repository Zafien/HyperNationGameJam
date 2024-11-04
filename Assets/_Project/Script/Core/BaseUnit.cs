using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Rendering;
using System;
using System.Collections;
using UniRx;



public class BaseUnit : MonoExt, IHealth, IStatus
{

    [TabGroup("Reference")][SerializeField] protected BaseUnitScriptable _unitDataScriptable;
    [TabGroup("Health")][SerializeField][ReadOnly] public HealthData _healthData;
    // Start is called before the first frame update

    public Subject<int> OnHealthAmountChanged = new Subject<int>();

    public Subject<Unit> Ondead = new Subject<Unit>();

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
        Ondead = new Subject<Unit>();


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
            OnDeath();
            OnHealthAmountChanged?.OnNext(healthAmount);


        }
    }

    public void OnDeath()
    {
        if (_healthData.HealthAmount <= 0)
        {
            // Die
            _healthData.HealthAmount = 0;
            this.gameObject.SetActive(false);   
            Ondead.OnNext(Unit.Default);
            Debug.Log($"{this.gameObject.name} died");
        }
    }

    public void AddStatus(Status status)
    {
       
    }

    public void RemoveStatus(Status status)
    {
       
    }
}
