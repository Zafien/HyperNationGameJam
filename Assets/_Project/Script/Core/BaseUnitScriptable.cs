using System.Collections.Generic;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "ScriptableObject/UnitData", order = 2)]
public class BaseUnitScriptable : SerializedScriptableObject
{
    public HealthData HealthData;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
public class HealthData
{
    public HealthData() { }
    public HealthData(HealthData data)
    {
        HealthAmount = data.HealthAmount;
    }

    public int HealthAmount = 100;
    public bool IsInvulnerable = false;
    public bool IsAlive => HealthAmount > 0;
}