using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Character : MonoBehaviour,IPointerClickHandler
{
    public Sprite charaSprite;
    public int hp;
    public int maxHp;
    public string charaName;
    public TMP_Text hpText;
    public Image charaImage;
    public enum CharaType{
        enemy,player
    }
    public CharaType charaType;
    public List<Skill> skills = new List<Skill>();
    // int skillNum;//技能数
    // public TMP_Text hpText;
    // public List<string> skills = new List<string>();
    public bool isDeath;//是否死亡
    protected virtual void Start() {
        //skillNum = skills.Count;
        hp = maxHp;
        //UpdateHpText();
        Debug.Log(this.name+"技能数:"+skills.Count);
    }
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

    public virtual void TakeDamage(int damageAmount)
    {
        hp-= damageAmount;
        if(hp<0){
            hp=0;
            isDeath=true;
        }


        // 处理其他伤害相关的逻辑，例如检查是否死亡等
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
}