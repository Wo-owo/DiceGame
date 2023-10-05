using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering;
using Unity.VisualScripting;
using System;

public class GameManager : MonoBehaviour
{   
    //回合枚举
    public enum TurnPhase
    {
        startBattle,
        playerTurn,
        enemyTurn,
        endBattle
    }

    public TurnPhase currentTurn;//回合
    public static GameManager instance;//单例

    public List<Dice> diceList = new List<Dice>(); // 存储骰子的 List
    //public GameObject dice1, dice2, dice3, dice4, dice5, dice6;//备用的骰子对象

    public TMP_Text turnText; // 用于显示当前回合的文本
    public Button rollButton; // 投骰子按钮
    public Button endTurnButton; // 结束回合按钮

     public List<GameObject> enemyList = new List<GameObject>();//敌人池子
    public List<Character> playerList = new List<Character>();//玩家池子
    public List<Character> deathPlayerlist = new List<Character>();//死亡的玩家的池子


    int amountOfDice;//数量
    int amountLimit;//限定数量

    public TurnPanel turnPanel;

    public int levelnum;//关卡数

    public List<Button> skillButton = new List<Button>();//技能按钮
    public Players selectedChara;//临时选中的角色
    public GameObject selectedEnemy;//临时选中的敌人
    public bool isSkill;//是否选中了技能
    public string whichSkill;//使用哪个技能
    public Dice whichDice;//选择哪个骰子
    public List<Dice> skillDice;//技能所需要的骰子

    public float delayBeforePlayerTurn = 2f;
    public float delayBeforeEnemyTurn = 2f;
    public float delayBeforeEndBattle = 2f;
    bool isBattle;//是否进入战斗
    public SkillPanel skillPanel;
    public List<object> varObj = new List<object>();
    public List<Dice> useDice = new List<Dice>();
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
        isBattle = true;
        //levelnum = 1;

        endTurnButton.onClick.AddListener(OnEndTurnButtonClicked);

        StartBattle();

        skillDice.Clear();
    }

    // Update is called once per frame
    void Update()
    { 
        //清空重选
        if(Input.GetMouseButtonDown(1)){
            ResetSkill();
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
        yield return StartCoroutine(StartBattlePhase());
        while (isBattle)
        {
            
            yield return StartCoroutine(playerTurnPhase());
            yield return StartCoroutine(EnemyTurnPhase());

        }
    }

    private IEnumerator StartBattlePhase()
    {
        Debug.Log("开始战斗 Start Battle");
        isBattle=true;
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
        RollAllDice();
        // 等待玩家点击结束回合按钮
        while (currentTurn == TurnPhase.playerTurn)
        {
            GameOver();//游戏结束判定
            yield return null;
        }
        // if(enemyList.Count==0){
        //     currentTurn = TurnPhase.endBattle;
        // }
        // else {
        //     currentTurn = TurnPhase.enemyTurn;
        // }
        // 玩家点击结束回合按钮后，切换到敌方回合
        
    }

    private IEnumerator EnemyTurnPhase()
    {
        Debug.Log("敌人回合Enemy Turn");
        yield return new WaitForSeconds(delayBeforeEnemyTurn);
        
        //状态机
        foreach(var _enemy in enemyList){
            
            Character _target ;
            int i=0;
            do{
                i++;
                _target=playerList[UnityEngine.Random.Range(0,playerList.Count)];
            }while(_target.isDeath==true||i>20);
            if(i>20){
                Debug.Log("搜寻玩家时溢出！");
                int a =0;
                while(a<4){
                    if(!playerList[a].isDeath){
                        _target = playerList[a];
                    }
                }
            }
            _target.TakeDamage(_enemy.GetComponent<Enemy>().attack);
            yield return new WaitForSeconds(delayBeforeEnemyTurn);
        }


        // 等待一段时间
        yield return new WaitForSeconds(delayBeforeEnemyTurn);

        GameOver();//游戏结束判定

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
    
    /// <summary>
    /// 切换选定的角色
    /// </summary>
    public void SelectCharacter(Players _character){
        Debug.Log("点击了角色");
        //切换角色就清除临时变量
        UnableSkill();
        if(!_character.isAction&&!_character.isDeath){
            if(selectedChara!=null&&selectedChara!=_character){
                selectedChara.selectedSign.SetActive(false);
                
            }
            selectedChara = _character;
            _character.selectedSign.SetActive(true);

            Debug.Log("选择了"+_character.name);
            skillPanel.SetSelectedCharacter(_character);
            varObj.Add(_character);
        }
        
    }
    
    /// <summary>
    /// 选择技能
    /// </summary>
    /// <param name="_code"></param>
    /// <param name="_num"></param>
    public void SelectSkill(string _code,int _num){
        //初始化泛型列表
        amountOfDice = 0;
        amountLimit = 0;
        //StartCoroutine(WaitForDice(_num));
        isSkill=true;
        varObj.Add(_code);
        varObj.Add(_num);
        amountLimit = _num;
        //StartCoroutine(WaitForDice(_num));
    }
    /// <summary>
    /// 选择骰子
    /// </summary>
    bool fullDice;
    public void ChooseDice(Dice _dice){
        if(currentTurn==TurnPhase.playerTurn&&isSkill){
            if(amountOfDice>amountLimit){
                Debug.LogWarning("超过该技能指定骰子");
                return;
            }
            else if(_dice.isSelected){
                _dice.isSelected=false;
                amountOfDice--;
                Debug.Log("取消了"+_dice.bianhao+"号骰子");
            }
            else if(_dice.isSelected==false&&!fullDice&&!_dice.isUsed){
                _dice.isSelected=true;
                amountOfDice++;
                Debug.Log("选定了"+_dice.bianhao+"号骰子");
            }
            
            
            if(amountOfDice==amountLimit){
                fullDice=true;
            }
            else if(amountOfDice <amountLimit){
                fullDice=false;
            }
            _dice.selectedSign.SetActive(_dice.isSelected);
        }
        
    }
    public void EnemySelected(Enemy _obj){
        Debug.Log("选择了敌人");
        if(currentTurn!=TurnPhase.playerTurn){
            Debug.Log("不在玩家回合内");
            return;
        }
        if(isSkill&&fullDice){
            foreach(var _dice in diceList){
                if(!_dice.isUsed&&_dice.isSelected){
                    useDice.Add(_dice);
                    varObj.Add(_dice.currentValue);
                }
            }
            varObj.Add(_obj);
            
            //electedEnemy = _obj;
            CombatManager.instance.UseSkill_Player(varObj);

            varObj.Clear();
        }
    }
    public void AttackSuccess(){
        foreach(var _dice in useDice){
            _dice.UsedDice(true);
            _dice.SelectedDice(false);
        }
    }
    public void AttackFailed(){
        UnableSkill(); 
    }

    /// <summary>
    /// 重新初始化战斗
    /// </summary>
    public void ReinitalizeSkill(){
        whichDice = null;
        whichSkill = null;
        isSkill=false;
        selectedChara = null;
        selectedEnemy = null;
        amountLimit=0;
        amountOfDice=0;           
        varObj.Clear();
        useDice.Clear();
        foreach(var _dice in diceList){
                _dice.SelectedDice(false);
                _dice.UsedDice(false);
        }
        foreach(var _chara in playerList){
            _chara.HasAction(false);
        }
    }

    /// <summary>
    /// 重设技能
    /// </summary>
    public void ResetSkill(){
        whichDice = null;
        whichSkill = null;
        isSkill=false;
        selectedChara = null;
        selectedEnemy = null;
        amountLimit=0;
        amountOfDice=0;    
        foreach(var _btn in skillButton){
        _btn.gameObject.SetActive(false);
        }
        amountLimit=0;
        amountOfDice=0;           
        foreach(var _dice in diceList){
            if(!_dice.isUsed&&_dice.isSelected){
                _dice.SelectedDice(false);
                _dice.UsedDice(true);
            }
        }
        varObj.Clear();
        useDice.Clear();
        
    }
    /// <summary>
    /// 未成功使用数据
    /// </summary>
    public void UnableSkill(){
        amountLimit=0;
        amountOfDice=0;           
        varObj.Clear();
        useDice.Clear();
        whichDice = null;
        whichSkill = null;
        isSkill=false;
        //selectedChara = null;
        selectedEnemy = null;
        foreach(var _dice in diceList){
            if(!_dice.isUsed&&_dice.isSelected){
                _dice.SelectedDice(false);
                
            }
        }
    }

    public void GameOver(){
        int a = 0;
        foreach(var _chara in playerList){
            if(_chara.isDeath == true){
                a++;
                
            }
        }
        if(a>=4){
            StopCoroutine(BattleLoop());
            isBattle=false;
            Debug.Log("失败");
            return;
            //弹出游戏结束界面；
            
        }
        if(enemyList.Count==0){
            StopCoroutine(BattleLoop());
            isBattle=false;
            Debug.Log("胜利");
            return;
            
        }
    }
}
