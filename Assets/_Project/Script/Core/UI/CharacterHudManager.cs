using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UniRx;

public class CharacterHudManager : MonoExt
{
    // Start is called before the first frame update
    [SerializeField]private CharacterUnit _characterUnit;
    public Image PlayerHudSkill;
    public Slider PlayerMaxHpSlider;



    private void Awake()
    {
     
    }

    protected virtual void Start()
    {

    }


    public void SetImageMaxFloat(Image Image,float MaxValue)
    {

        Image.fillAmount = (100 / 100) * MaxValue;
    }

    public void SetSliderMaxHp(float MaxHp)
    {
        PlayerMaxHpSlider.maxValue = MaxHp;
        PlayerMaxHpSlider.value = MaxHp;    
    }

    public void SetPlayerCurrentHp(float Hp)
    {
        PlayerMaxHpSlider.value = Hp;
    }
}
