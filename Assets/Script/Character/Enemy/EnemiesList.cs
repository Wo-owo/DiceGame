using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New EnemiesList",menuName ="EnemiesList",order =0)]
public class EnemiesList : ScriptableObject
{
    public List<Enemy> enemies = new List<Enemy>();
}
