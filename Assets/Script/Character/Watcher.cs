using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Watcher : Players
{
    //public GameObject selectedSign;
    protected override void Start()
    {
        Skill skill = new Skill("基础攻击","BasicAttack",1);
        skills.Add(skill);
        Debug.Log(this.name+"技能数:"+skills.Count);
        hp = maxHp;
        hpText.text = hp.ToString()+"/"+maxHp.ToString();
    }

}
