using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class EnemyDatas : ScriptableObject
{
    public string enemyName;
    public int maxHealth;
    public int attack;
    public int defend;
    public Sprite sprite;
    public List<Skill> skills = new List<Skill>();
}