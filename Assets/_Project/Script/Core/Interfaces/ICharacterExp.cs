using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public interface ICharacterExp
{
    public float MaxExp();

    public void OnLevelUp();

    public void GainExp(float GainedExp);
 

}
[System.Serializable]
public class CharacterExp
{

    public float MaxExp = 100;
    public float CurrentExp;
    public CharacterExp(CharacterExp data)
    {
        MaxExp = data.MaxExp;   
        CurrentExp = data.CurrentExp;   
    }

}



