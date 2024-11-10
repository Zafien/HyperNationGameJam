using System.Collections.Generic;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;


[CreateAssetMenu(fileName = "Weapons", menuName = "ScriptableObject/MeleeWeapon", order = 1)]
public class MeleeBaseScriptable : SerializedScriptableObject
{

    public float Damage;
    public float CoolDown;
    public float range;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
