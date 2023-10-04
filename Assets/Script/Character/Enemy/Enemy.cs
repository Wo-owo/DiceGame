using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class Enemy : Character,IPointerClickHandler
{
    public EnemyDatas enemyData;
    public int attack;

    public TMP_Text attackText;
    
    public int num;//编号
    //public bool isAction;//是否行动过了
    
    protected override void Start()
    {
        base.Start();
        hp=maxHp;
        hpText.text = hp.ToString();
        if(enemyData.sprite==null){
            Debug.Log("怪物图像为空");
        }
        else{
            GetComponent<Image>().sprite=enemyData.sprite;
        }
        
        hp = enemyData.maxHealth;
        attack=enemyData.attack;
        hpText.text=hp.ToString();
        attackText.text=attack.ToString();
        isAction = false;
        //skills = 
        // 使用 EnemyData 初始化敌人属性
    }
    public void UpdateText(){
        Debug.Log("更新"+enemyData.name+"的文本");
        hpText.text=hp.ToString();
        attackText.text=attack.ToString();
    }
    /// <summary>
    /// 修改数值
    /// </summary>
    public void ChangeHp(int _num){
        hp-=_num;
        hpText.text=hp.ToString();
        Death();
    }
    public void Death(){
        if(hp<=0){
            Debug.Log("敌人死亡");
            GameManager.instance.EnemyDeath(num);
            Destroy(this);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.instance.EnemySelected(this);
        Debug.Log("点击到了敌人"+enemyData.name);
    }
    public override void TakeDamage(int damageAmount)
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
}