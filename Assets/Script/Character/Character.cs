using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Character : MonoBehaviour
{
    public Sprite charaSprite;
    public int hp;
    public int maxHp;
    public string charaName;
    public TMP_Text hpText;
    public Image charaImage;
    public bool isAction;//是否行动过
    public bool isDeath;//是否死亡
   
    public enum CharaType{
        enemy,player
    }
    public CharaType charaType;
    

    protected virtual void Start() {
        //skillNum = skills.Count;
        hp = maxHp;
        //UpdateHpText();
        
    }

    public virtual void TakeDamage(int damageAmount)
    {
        hp-= damageAmount;
        if(hp<0){
            hp=0;
            isDeath=true;

            if(charaType==CharaType.enemy){
                GameManager.instance.enemyList.Remove(this.gameObject);
            }
        }
        hpText.text=hp.ToString();

        // 处理其他伤害相关的逻辑，例如检查是否死亡等
    }
    public virtual void HasAction(bool _isaction){
        isAction = _isaction;
    }
    
}