using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior :  Character 
{
        
    protected override void Start()
    {
        base.Start();
        //skills.Add(SkillLibrary.BasicAttack);
        Skill skill = new Skill("基础攻击","BasicAttack",1);
        skills.Add(skill);
        Debug.Log(this.name+"技能数:"+skills.Count);
    }
}
