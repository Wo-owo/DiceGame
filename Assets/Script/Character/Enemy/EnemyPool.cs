using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyPool", menuName = "DiceGame/EnemyPool", order = 0)]
public class EnemyPool : ScriptableObject {
    public List<EnemyDatas> enemies = new List<EnemyDatas>();
}

