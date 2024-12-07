using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillButtonUI : MonoBehaviour
{

    public WeapoonEffectBase WeaponEffect;
    // Start is called before the first frame update
    public CharacterUnit chacter;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PressedSkill()
    {
        chacter._WeaponData.WeaponEffect = WeaponEffect;

 
    }
}
