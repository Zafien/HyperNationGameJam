using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeapoonEffectBase : SerializedScriptableObject
{
    public float Cooldown;
    //public bool CanActivate;
    public abstract void ApplyEffect(Vector3 position, Vector3 direction);
    //Add cooldown
}
