using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{   
    //回合枚举
    public enum Turn
    {
        none,
        start,
        Player,
        Enemy,
        end
    }
    private Turn currentTurn;//回合
    public static GameManager instance;//单例

    public List<Dice> diceList = new List<Dice>(); // 存储骰子的 List
    //public GameObject dice1, dice2, dice3, dice4, dice5, dice6;//备用的骰子对象

    public TMP_Text turnText; // 用于显示当前回合的文本
    public Button rollButton; // 投骰子按钮
    public Button endTurnButton; // 结束回合按钮

     public List<GameObject> enemyList = new List<GameObject>();//敌人池子
    public List<Character> playerList = new List<Character>();//玩家池子
    public TurnPanel turnPanel;

    public int levelnum;//关卡数


    private void Awake() {
        //初始化单例
        if(instance!=null){
            Destroy(instance);
        }
        instance = this;

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
        currentTurn = Turn.none;//初始化回合为无
        turnPanel.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>投掷所有骰子</summary>
    public void RollAllDice()
    {
        Debug.Log("投全部骰子");
        // 循环遍历每个骰子并进行投掷
        foreach (Dice _dice in diceList)
        {
            _dice.Roll();
            
        }
        rollButton.interactable = false;
    }
    public void RollOneDice(Dice _dice)
    {
        Debug.Log("投"+_dice.bianhao+"号骰子");
        if(_dice==null){
            Debug.LogError("未指定骰子就调用了RollOneDice");
        }
        // 重新投掷指定骰子
        _dice.Roll();   
    }

    //回合切换
    public void TurnChange(){

        switch(currentTurn){
            case Turn.none:
                currentTurn=Turn.start;
                break;
            case Turn.start:
                currentTurn=Turn.Player;

                break;
            case Turn.Player:
                currentTurn = Turn.Enemy;
                break;
            case Turn.Enemy:
                currentTurn = Turn.Player;
                break;
            default:
                Debug.LogError("进入了不存在的回合");
                break;        
        }
        Debug.Log("切换回合："+currentTurn);
        turnPanel.ShowPanel(currentTurn.ToString());
    }

    void TurnStart(){

    }
    void PlayerTurn(){
        rollButton.interactable = true;
    }
    void EnemyTurn(){
        if(rollButton.interactable){
            rollButton.interactable= false;
        }

        EnemyAction();
    }

    /// <summary>
    /// 效果消失
    /// </summary>
    void EffectEnd(GameObject _obj,int _id){

    }

    /// <summary> 
    /// 不存在敌人时结束战斗
    /// </summary>
    public void TurnEndBattle(){
        currentTurn = Turn.none;

        //结算奖励

    }

    /// <summary>敌方状态机</summary>
    private void EnemyAction(){
        
    }

    /// <summary>房间加载器</summary>
    private void RoomLoader(){

    }
    
    public void EnemyDeath(int _num){
        enemyList.RemoveAt(_num);

    }
}
