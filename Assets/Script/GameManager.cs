using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{   
    //回合枚举
    private enum TurnPhase
    {
        startBattle,
        playerTurn,
        enemyTurn,
        endBattle
    }

    private TurnPhase currentTurn;//回合
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

    public List<Button> skillButton = new List<Button>();//技能按钮
    public GameObject selectedChara;//临时选中的角色
    public GameObject selectedEnemy;//临时选中的敌人
    public bool isSkill;//是否选中了技能
    public string whichSkill;//使用哪个技能
    public Dice whichDice;//选择哪个骰子

    public float delayBeforePlayerTurn = 2f;
    public float delayBeforeEnemyTurn = 2f;
    public float delayBeforeEndBattle = 2f;
    bool isBattle;//是否进入战斗
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
        //currentTurn =TurnPhase.startBattle;//初始化回合为无
        turnPanel.gameObject.SetActive(false);
        selectedChara=null;
        selectedEnemy=null;
        isSkill=false;
        //levelnum = 1;

        endTurnButton.onClick.AddListener(OnEndTurnButtonClicked);

        StartBattle();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1)){
            whichDice = null;
            whichSkill = null;

        }
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
    // public void TurnChange(){

    //     switch(currentTurn){
    //         case Turn.none:
    //             currentTurn=Turn.start;
    //             break;
    //         case Turn.start:
    //             currentTurn=Turn.Player;

    //             break;
    //         case Turn.Player:
    //             currentTurn = Turn.Enemy;
    //             break;
    //         case Turn.Enemy:
    //             currentTurn = Turn.Player;
    //             break;
    //         default:
    //             Debug.LogError("进入了不存在的回合");
    //             break;        
    //     }
    //     Debug.Log("切换回合："+currentTurn);
    //     turnPanel.ShowPanel(currentTurn.ToString());
    // }

    void TurnStart(){

    }
    void playerTurn(){
        Debug.Log("进入玩家回合");
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
        //currentTurn = Turn.none;

        //结算奖励
    }

    /// <summary>敌方状态机</summary>
    private void EnemyAction(){
        
    }
    
    public void EnemyDeath(int _num){
        Debug.Log("敌人死亡函数EnemyDeath()");
        enemyList.RemoveAt(_num);
        if(enemyList.Count==0){
            Debug.Log("全部敌人死亡");
            levelnum++;
            LevelManager.instance.UpdateLevelNum(levelnum);
        }

    }
    /// <summary>
    /// 选择骰子
    /// </summary>
    public void ChooseDice(Dice _dice){
        
        if(currentTurn==TurnPhase.playerTurn){
            whichDice = _dice;
            Debug.Log("选定了"+_dice.bianhao+"号骰子");
        }
        
    }
    
    /// <summary>
    /// 切换选定的角色
    /// </summary>
    public void ChangeCharacterSelected(GameObject _obj,List<string> _skills){
        selectedChara = _obj;
        foreach(var _btn in skillButton){
            _btn.gameObject.SetActive(false);
        }
        for(int a = 0;a<_skills.Count;a++){
            skillButton[a].gameObject.SetActive(true);
            skillButton[a].GetComponentInChildren<TMP_Text>().text=_skills[a];

            skillButton[a].onClick.AddListener(delegate{
                whichSkill = _skills[a];
            });
            Debug.Log("");
        }
    }
    
    public void ChangeEnemySelected(GameObject _obj){
        if(isSkill){
            selectedEnemy = _obj;
        }
    }
    
    public void StartBattle(){
        LevelManager.instance.UpdateLevelNum(levelnum);
        LevelManager.instance.LoadLevel();
        //isBattle = true;
        StartCoroutine(BattleLoop());
    }
    /// <summary>
    /// 战斗循环
    /// </summary>
    /// <returns></returns>
    private IEnumerator BattleLoop()
    {
        while (true)
        {
            yield return StartCoroutine(StartBattlePhase());
            yield return StartCoroutine(playerTurnPhase());
            yield return StartCoroutine(EnemyTurnPhase());
        }
    }

    private IEnumerator StartBattlePhase()
    {
        Debug.Log("开始战斗 Start Battle");
        // 显示一些战斗开始的UI或效果

        // 等待一段时间
        yield return new WaitForSeconds(delayBeforePlayerTurn);

        // 切换到玩家回合
        currentTurn = TurnPhase.playerTurn;
    }

    private IEnumerator playerTurnPhase()
    {
        Debug.Log("玩家回合Player Turn");
        // 启用玩家的输入和操作

        // 等待玩家点击结束回合按钮
        while (currentTurn == TurnPhase.playerTurn)
        {
            yield return null;
        }

        // 玩家点击结束回合按钮后，切换到敌方回合
        currentTurn = TurnPhase.enemyTurn;
    }

    private IEnumerator EnemyTurnPhase()
    {
        Debug.Log("敌人回合Enemy Turn");
        // 模拟敌方的操作，例如敌人AI

        // 等待一段时间
        yield return new WaitForSeconds(delayBeforeEnemyTurn);

        // 切换到玩家回合
        currentTurn = TurnPhase.playerTurn;
    }
    private IEnumerator EndBattlePhase()
    {
        Debug.Log("结束战斗End Battle Phase");
        // 处理战斗结束的逻辑，例如计算奖励
        StopCoroutine(BattleLoop());
        // 等待一段时间
        yield return new WaitForSeconds(delayBeforeEndBattle);

        // 切换到结束战斗状态
        currentTurn = TurnPhase.endBattle;
    }

    public void OnEndTurnButtonClicked()
    {
        Debug.Log("点击了回合结束按钮");
        // 玩家点击结束回合按钮的处理
        if (currentTurn == TurnPhase.playerTurn)
        {
            currentTurn = TurnPhase.enemyTurn;
        }
    }
}
