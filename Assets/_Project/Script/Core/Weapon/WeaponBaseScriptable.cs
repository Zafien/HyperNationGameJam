using System.Collections.Generic;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapons", menuName = "ScriptableObject/GunWeapon", order = 2)]
public class WeaponBaseScriptable : SerializedScriptableObject

{
    public float Damage;
    public float CoolDown;
    [TabGroup("Melee Range")] public float MeleeRange;
    [TabGroup("Gun Range"),Range(0f, 10f)] public float GunMainDamageRange; //Closest more damage
    [TabGroup("Gun Range"),Range(10f, 20f)] public float GunSecondDamageRange; //second closes has slight damage
    [TabGroup("Gun Range"),Range(20f, 30f)] public float GunThirdDamageRange; //third closes has minimal damage
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
public class GunStats
{
    
}
public class MeleeStas
{

}

public enum Weapon
{
    Melee,
    Gun
}



