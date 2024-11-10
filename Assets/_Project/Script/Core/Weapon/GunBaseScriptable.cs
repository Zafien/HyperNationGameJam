using System.Collections.Generic;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapons", menuName = "ScriptableObject/GunWeapon", order = 2)]
public class GunBaseScriptable : SerializedScriptableObject
{
    public float Damage;
    public float CoolDown;
    [Range(0f, 10f)] public float MainDamageRange; //Closest more damage
    [Range(10f, 20f)] public float SecondDamageRange; //second closes has slight damage
    [Range(20f, 30f)] public float ThirdDamageRange; //third closes has minimal damage
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



