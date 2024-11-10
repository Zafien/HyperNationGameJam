using System.Collections.Generic;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapons", menuName = "ScriptableObject/Weapon", order = 2)]
public class WeaponBaseScriptable : SerializedScriptableObject
{
    public float Damage;
    public float CoolDown;

   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
public enum Weapon
{
    Melee,
    Gun
}



