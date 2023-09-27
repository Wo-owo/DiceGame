using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{   
    //回合
    public enum Turn
    {
        start,
        Player,
        Enemy,
        end
    }

    public List<Dice> diceList = new List<Dice>(); // 存储骰子的 List
    //public GameObject dice1, dice2, dice3, dice4, dice5, dice6;//备用的骰子对象

    public TMP_Text turnText; // 用于显示当前回合的文本
    public Button rollButton; // 投骰子按钮
    public Button endTurnButton; // 结束回合按钮

    private void Awake() {

        // 初始化骰子列表，将每个骰子添加到列表中
        foreach (Transform child in transform)
        {
            Dice dice = child.GetComponent<Dice>();
            if (dice != null)
            {
                diceList.Add(dice);
            }
        }
    }
    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>投掷所有骰子</summary>
    private void RollAllDice()
    {
        // 循环遍历每个骰子并进行投掷
        foreach (Dice dice in diceList)
        {
            dice.Roll();
        }
    }
    private void RollOneDice(Dice _dice)
    {
        if(_dice==null){
            Debug.LogError("未指定骰子就调用了RollOneDice");
        }
        // 重新投掷指定骰子
        _dice.Roll();   
    }

    
}
