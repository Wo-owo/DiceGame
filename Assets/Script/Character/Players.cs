using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Players :  Character ,IPointerClickHandler
{
    public GameObject deathSign;
    public GameObject actionSign;
     public GameObject selectedSign;
    public List<Skill> skills = new List<Skill>();
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
    public override void HasAction(bool _isaction){
        isAction=!isAction;
        actionSign.SetActive(isAction);
    }
    
}