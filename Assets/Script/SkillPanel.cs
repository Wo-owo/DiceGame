using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SkillPanel : MonoBehaviour
{
    //public Button[] skillButtons; // 技能按钮数组
    public List<Button> skillButtons = new List<Button>();
    private Players selectedCharacter; // 当前选中的角色

    // 设置技能面板的角色
    public void SetSelectedCharacter(Players _character)
    {
        selectedCharacter = _character;
        Debug.Log("更新"+_character.name+"技能面板");
        UpdateSkillButtons();
    }

    public void HideAllSkill(){
        foreach(var _btn in skillButtons){
            _btn.gameObject.SetActive(false);
        }
    }
    // 更新技能按钮显示
    private void UpdateSkillButtons()
    {
        
        if (selectedCharacter != null)
        {
            for (int i = 0; i < selectedCharacter.skills.Count; i++)
            {
                Debug.Log(i);
                if (i < selectedCharacter.skills.Count)
                {
                    // 如果技能存在，则显示按钮，并设置按钮文本为技能名称
                    skillButtons[i].gameObject.SetActive(true);
                    skillButtons[i].GetComponentInChildren<TMP_Text>().text = selectedCharacter.skills[i].skillName;
                    skillButtons[i].onClick.AddListener(delegate{
                        //GameManager.instance.whichSkill = selectedCharacter.skills[i].skillcode;
                        Debug.Log(selectedCharacter.name+"技能数:"+selectedCharacter.skills.Count+",按钮数"+skillButtons.Count+",i="+i);


                        GameManager.instance.SelectSkill(selectedCharacter.skills[i-1].skillcode,selectedCharacter.skills[i-1].diceNeed);
                    });
                }
                else
                {
                    // 如果技能不存在，则隐藏按钮
                    skillButtons[i].gameObject.SetActive(false);
                }
            }
        }
    }

    // 点击技能按钮
    public void OnSkillButtonClicked(int skillIndex)
    {
        if (selectedCharacter != null && skillIndex >= 0 && skillIndex < selectedCharacter.skills.Count)
        {

            //selectedCharacter.UseSkill(skillIndex, /* 目标角色 */);
        }
    }
}
