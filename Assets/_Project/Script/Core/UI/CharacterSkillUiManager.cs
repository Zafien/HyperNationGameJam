using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CharacterSkillUiManager : MonoBehaviour
{
    // Start is called before the first frame update

    public List<Button> SkillsButton = new List<Button>();
    public Button FireSkillButton;
   
 
    public SkillButtonUI SkillUiButton;
    private void OnEnable()
    {
        FireSkillButton.onClick.AddListener(ChosenSkill);
    }
    private void OnDisable()
    {
        FireSkillButton.onClick.RemoveAllListeners();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenSkillUI()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void ChosenSkill()
    {
        
        SkillUiButton.PressedSkill();
        Debug.LogError("PRESSED THUIS");
    }
}
