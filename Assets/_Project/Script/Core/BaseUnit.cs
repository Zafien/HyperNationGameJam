using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoExt, IHealth, IStatus
{

    [TabGroup("Reference")][SerializeField] protected BaseUnitScriptable _unitDataScriptable;
    // Start is called before the first frame update


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
     



    }

    protected virtual void SetScriptableData()
    {   


    
    }

    public void ModifyHealthAmount(int value)
    {

    }

    public void OnDeath()
    {

    }

    public void AddStatus(Status status)
    {
       
    }

    public void RemoveStatus(Status status)
    {
       
    }

    public virtual void DoDamage()
    {

    }
}
