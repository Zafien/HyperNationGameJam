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
    public Image PlayerCDImage;
    public Slider PlayerMaxHpSlider;
    public Slider PlayerExpSlider;
    private void Start()
    {
        Initialize();
        OnSubscriptionSet();
    }
    public override void Initialize(object data = null)
    {
        base.Initialize(data);  
    }

    public override void OnSubscriptionSet()
    {
        AddEvent(_characterUnit.OnGainingExp, _ => UpdateExp());
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

    public void SetExp(float CurrExp, float MaxExp)
    {
       
        PlayerExpSlider.value = CurrExp;
        PlayerExpSlider.maxValue = MaxExp;
    }
    public void UpdateExp()
    {
        Debug.LogError("CHING GING");
        PlayerExpSlider.value = _characterUnit._unitStats.CurrExp;
    }

    //public void SetExpMaxFloat(Image Image, float MaxValue)
    //{

    //    Image.fillAmount = (100 / 100) * MaxValue;
    //}

    //public void SetSliderMaxExp(float MaxExp)
    //{
    //    PlayerMaxHpSlider.maxValue = MaxExp;
    //    PlayerMaxHpSlider.value = MaxExp;
    //}

    //public void SetPlayerCurrentExp(float Exp)
    //{
    //    PlayerMaxHpSlider.value = Hp;
    //}
}
