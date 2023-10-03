using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class Character : MonoBehaviour,IPointerClickHandler
{
    public int hp;
    public int maxHp;
    int skillNum;//技能数
    public TMP_Text hpText;
    public List<string> skills = new List<string>();
    public bool isDeath;//是否死亡
    private void Start() {
        skillNum = skills.Count;
        hp = maxHp;
        UpdateHpText();

    }
    public void UpdateHpText(){
        hpText.text= hp.ToString()+"/"+maxHp.ToString();
    }
    /// <summary>
    /// 角色受伤
    /// </summary>
    public void GetDamage(int _damage){
        if(_damage>hp){
            hp=0;

        }
        else{
            hp-=_damage;
        }
        UpdateHpText();
    }
    public void GetHeal(int _heal){
        if(_heal+hp>maxHp){
            hp=maxHp;
        }
        else{
            hp+=_heal;
        }
        UpdateHpText();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("点击了角色");
        GameManager.instance.ChangeCharacterSelected(this.gameObject,skills);
    }
}
