using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



[CreateAssetMenu(fileName = "Effects", menuName = "Effects/SpawnMine")]
public class SpawnMine : WeapoonEffectBase
{
    public GameObject bomb;

    public override void ApplyEffect(Vector3 position, Vector3 direction)
    {
        GameObject bombGo = Instantiate(bomb, position, Quaternion.identity);
    }


}
