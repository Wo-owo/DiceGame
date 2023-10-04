using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Warrior :  Character ,IPointerClickHandler
{
    public GameObject deathSign;
    public GameObject actionSign;
    
    protected override void Start()
    {
        base.Start();
        //skills.Add(SkillLibrary.BasicAttack);
        Skill skill = new Skill("基础攻击","BasicAttack",1);
        skills.Add(skill);
        Debug.Log(this.name+"技能数:"+skills.Count);
        hp = maxHp;
        hpText.text = hp.ToString()+"/"+maxHp.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(isDeath){
            Debug.Log(this.name+"已经死亡");
            return;
        } 

        Debug.Log("点击了"+this.name);
        if(charaType==CharaType.player){
            GameManager.instance.SelectCharacter(this);
            
        }
    }
    public override void TakeDamage(int damageAmount)
    {
        //base.TakeDamage(damageAmount);
        hp-= damageAmount;
        if(hp<0){
            hp=0;
            isDeath=true;
            deathSign.SetActive(true);
            
        }
        hpText.text=hp.ToString();
    }
    public override void HasAction(){
        isAction=!isAction;
        actionSign.SetActive(isAction);
    }
}
