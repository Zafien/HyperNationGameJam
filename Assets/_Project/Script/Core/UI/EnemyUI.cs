using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyUI : MonoBehaviour
{
    public Slider HpSlider;

    public void SetHealthBarUI(float Hp)
    {
        HpSlider.value = Hp;    
    }

    public void SetMaxHp(float MaxHp)
    {
        HpSlider.maxValue = MaxHp;
    }
}
