using UnityEngine;
using System;
using TMPro;
public class Dice : MonoBehaviour
{
    [SerializeField] public int sides = 6; // 骰子的面数，默认为6
    [SerializeField] public bool canBeModified = false; // 是否允许修改骰子属性

    public TMP_Text numText;//数字的文字
    private int currentValue = 1; // 当前骰子的点数

    //debug用
    public int bianhao;//骰子编号

    private void Start()
    {
        currentValue = 0;
    }

    public void Roll()
    {
        // 生成随机点数
        currentValue = UnityEngine.Random.Range(1, sides + 1);
        
        // 在这里添加你想要执行的点数技能或效果

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
}
