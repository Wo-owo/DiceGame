using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Enemy : Character
{
    public int attack;
    public int defend;
    public EnemyDatas enemyData;

    
    protected override void Start()
    {
        base.Start();
        hp=maxHp;
        //skills = 
        // 使用 EnemyData 初始化敌人属性
    }
}
