using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Slient :  Character ,IPointerClickHandler
{
    protected override void Start()
    {
        base.Start();
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
}
