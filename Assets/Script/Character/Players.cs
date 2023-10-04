using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 角色类+角色显示
/// </summary>
public class PlayerCharacter : Character
{
    // 可以添加特定于玩家的属性和方法

    protected override void Start()
    {
        base.Start();
        // 玩家角色的初始化逻辑

        hpText.text = hp + "/"+maxHp;
        GetComponent<Image>().sprite=charaSprite;
    }
    // 使用技能
    // public void UseSkill(int skillIndex, Character target)
    // {
    //     if (skillIndex >= 0 && skillIndex < skills.Count)
    //     {
    //         Skill selectedSkill = skills[skillIndex];
    //         SkillManager.InflictDamage(this, target, selectedSkill.damage);
    //     }
    //     else
    //     {
    //         Debug.LogError("Invalid skill index");
    //     }
    // }

}
