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
    [TabGroup("Unit Stats")][SerializeField][ReadOnly] public UnitStats _unitStats;

    // Start is called before the first frame update

    public Subject<float> OnHealthAmountChanged = new Subject<float>();

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
    public override void OnSubscriptionSet()
    {
        base.OnSubscriptionSet();
        //AddEvent(_healthData.teste, _ => OnDeath());


        //AddEvent(_dialogueManager.OnDialogueEnd, _ => OnDialogueEnd());
    }    

    public override void Initialize(object data = null)
    {
        base.Initialize(data);
        SetScriptableData();
        OnHealthAmountChanged = new Subject<float>();
        Ondead = new Subject<Unit>();


    }

    protected virtual void SetScriptableData()
    {
        UpdateHealthDataFromInspector(_unitDataScriptable.UnitStats);


    }
    private void UpdateHealthDataFromInspector(UnitStats UnitStats) => _unitStats = new UnitStats(UnitStats);

    public void ModifyHealthAmount(float value)
    {
        if (_unitStats.IsAlive)
        {
            float healthAmount = _unitStats.HealthAmount -= value;
            OnDeath();
            OnHealthAmountChanged?.OnNext(healthAmount);


        }
    }

    public void OnDeath()
    {
        //Use object pooling here
        if (_unitStats.HealthAmount <= 0)
        {
            // Die
            _unitStats.HealthAmount = 0;
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
