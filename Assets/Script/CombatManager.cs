using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using UnityEngine.Scripting;

// 技能类
public class Skill
{
    public string skillName;//技能名称
    public string skillcode;//技能代号
    public int diceNeed;//需要几个骰子

    public Skill(string _name, string _code,int _num)
    {
        skillName = _name;
        skillcode = _code;
        diceNeed = _num;
    }
}

public class CombatManager:MonoBehaviour
{
    public static CombatManager instance;
    private void Awake() {
        if(instance!=null){
            Destroy(instance);
        }
        instance=this;
    }
    public void UseSkill_Player(List<object> _varObj){
        foreach(var _var in _varObj){
            Debug.Log(_var);
        }

        string _functionName = (string)_varObj[1];
        int _amountOfDice = (int)_varObj[2];
        Type type = GetType();

        // 获取函数信息
        MethodInfo method = type.GetMethod(_functionName);
        if (method != null)
        {

            object[] parameters=new object[_amountOfDice+1];
            // 函数参数
            switch(_functionName){
                case "BasicAttack" :
                parameters[0]=_varObj[_varObj.Count-1];
                parameters[1]=_varObj[_varObj.Count-2];
                break;
            }
                // 通过Invoke调用函数，并传递参数
            method.Invoke(this, parameters);
            var _attacker = (Character)_varObj[0];
            _attacker.HasAction();
        }
        else
        {
            Debug.LogError("找不到函数：" + _functionName);
        }
        GameManager.instance.ReinitalizeSkill();
    }
    public void BasicAttack(Enemy _target,int _damage){
        Debug.Log("攻击了"+_target.name);
        _target.TakeDamage(_damage);

    }
    public void BasicAttack_2(Enemy _target,int _damage){
        Debug.Log("攻击了"+_target.name);
        _target.TakeDamage(_damage);
    }

}

public static class SkillLibrary
{
    // public static Skill BasicAttack = new Skill("普通攻击", "BasicAttack",1);
    public static Skill Fireball = new Skill("Fireball", "20",1);
    // 添加更多技能...

    // // 你可以根据需要添加其他方法，例如获取技能信息、检查技能是否可用等
    
}