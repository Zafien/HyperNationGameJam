using System.Collections.Generic;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "ScriptableObject/UnitData", order = 2)]
public class BaseUnitScriptable : SerializedScriptableObject
{
    public UnitStats UnitStats;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}

[System.Serializable]
public class UnitStats
{

    public UnitStats(UnitStats data)
    {
        HealthAmount = data.HealthAmount;
        Speed = data.Speed; 
        MaxExp = data.MaxExp;
        CurrLevel = data.CurrLevel; 
    }
    public float HealthAmount = 100;
    public float Speed;
    public float MaxExp;
    public float CurrExp;
    public int CurrLevel;
    public bool IsInvulnerable = false;
    public bool IsAlive => HealthAmount > 0;

}
