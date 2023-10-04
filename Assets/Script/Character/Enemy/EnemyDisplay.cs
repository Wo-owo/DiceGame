using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class EnemyDisplay : MonoBehaviour,IPointerClickHandler
{
    //public Enemy enemy;
    public EnemyDatas enemyDatas;
    public int hp;
    public int maxHp;
    public int attack;
    public Image image;
    public TMP_Text hpText;
    public TMP_Text attackText;
    public int num;//编号
    public bool isAction;//是否行动过了

    private void Start() {
        if(enemyDatas.sprite==null){
            Debug.Log("怪物图像为空");
        }
        else{
            GetComponent<Image>().sprite=enemyDatas.sprite;
        }
        
        hp = enemyDatas.maxHealth;
        attack=enemyDatas.attack;
        hpText.text=hp.ToString();
        attackText.text=attack.ToString();
        isAction = false;
    }
    public void UpdateText(){
        Debug.Log("更新"+enemyDatas.name+"的文本");
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
        GameManager.instance.EnemySelected(this.gameObject);
        Debug.Log("点击到了敌人"+enemyDatas.name);
    }
}
