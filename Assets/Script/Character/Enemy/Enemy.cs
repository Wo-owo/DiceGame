using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{
    public Sprite sprite;
    public string enemyName;
    public int hp;
    public int maxHp;
    public int attack;
    public int defend;
}
