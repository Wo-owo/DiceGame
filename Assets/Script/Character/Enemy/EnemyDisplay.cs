using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EnemyDisplay : MonoBehaviour
{
    public Enemy enemy;
    public int hp;
    public int maxHp;
    public int attack;
    
    public TMP_Text hpText;
    public TMP_Text attackText;
    public int num;//编号

    private void Start() {
        GetComponent<Image>().sprite=enemy.sprite;
        hp = enemy.maxHp;
        attack=enemy.attack;
        
    }
    public void UpdateText(){
        Debug.Log("更新"+enemy.name+"的文本");
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
}
