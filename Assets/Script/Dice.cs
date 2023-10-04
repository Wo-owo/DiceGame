using UnityEngine;
using System;
using TMPro;
using UnityEngine.EventSystems;
using Microsoft.Unity.VisualStudio.Editor;
public class Dice : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] public int sides = 6; // 骰子的面数，默认为6
    [SerializeField] public bool canBeModified = false; // 是否允许修改骰子属性

    
    public TMP_Text numText;//数字的文字
    public int currentValue = 1; // 当前骰子的点数

    //debug用
    public int bianhao;//骰子编号

    public bool isSelected;//被选中
    public bool isUsed;//被使用过
    public GameObject selectedSign;//标记
    public GameObject usedSign;//标记
    private void Start()
    {
        currentValue = 0;
        selectedSign.SetActive(false);
    }

    public void Roll()
    {
        // 生成随机点数
        currentValue = UnityEngine.Random.Range(1, sides + 1);
        
        // 在这里添加你想要执行的点数技能或效果
        numText.text = currentValue.ToString();        
        Debug.Log(bianhao+"号骰子点数: " + currentValue);
    }

    public int GetCurrentValue()
    {
        return currentValue;
    }

    /// <summary>调整骰子面数</summary>
    /// <param name="_newSides"></param>
    public void ModifiedSide(int _newSides){
        if(canBeModified){
            sides = _newSides;
        }
    }
    /// <summary>调整骰子面数变回初始</summary>
    public void ReModifiedSide(){
        sides = 6;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.instance.ChooseDice(this);
        
    }
    public void UsedDice(bool _use){
        isUsed = _use;
        usedSign.SetActive(isUsed);

    }
    public void SelectedDice(bool _use){
        isSelected = _use;
        selectedSign.SetActive(isUsed);
    }
}
 