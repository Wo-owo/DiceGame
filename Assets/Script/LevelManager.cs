using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyData
{
    public string type;
    public int count;
}

[System.Serializable]
public class LevelData
{
    public int level;
    public EnemyData[] enemies;
}

[System.Serializable]
public class EnemiesList
{
    public LevelData[] levels;
}

public class LevelManager : MonoBehaviour
{
    public GameObject enemyPrefab; // 预制体1
    public TextAsset levelJson; // 将 JSON 文件拖拽到该字段
    
    public EnemyPool enemyPool;
    public GameObject enemyPanel;//怪物池
    public int levelnum; //关卡数

    public static LevelManager instance;

    private void Awake() {
        if(instance!=null){
            Destroy(instance);
        }
        instance=this;
        levelnum = GameManager.instance.levelnum;
    }
    private void Start()
    {
        
        //LoadLevel();
        
    }

    public void UpdateLevelNum(int _num)
    {
        levelnum = _num;
    }

    public void LoadLevel()
    {
        // 将 JSON 数据转换为 EnemiesList 对象
        EnemiesList enemiesList = JsonUtility.FromJson<EnemiesList>(levelJson.ToString());

        // 查找指定关卡数据
        LevelData levelData = FindLevelData(levelnum, enemiesList);

        if (levelData != null)
        {
            // 输出关卡信息
            Debug.Log("Level: " + levelData.level);

            foreach (EnemyData enemyData in levelData.enemies)
            {
                // 根据 JSON 数据生成怪物
                GenerateEnemies(enemyData.type, enemyData.count);
            }
        }
        else
        {
            Debug.LogError("Level data not found for level " + levelnum);
        }
    }

    private LevelData FindLevelData(int targetLevel, EnemiesList enemiesList)
    {
        foreach (LevelData levelData in enemiesList.levels)
        {
            if (levelData.level == targetLevel)
            {
                return levelData;
            }
        }

        return null;
    }

    private void GenerateEnemies(string enemyType, int count)
    {
        // 在这里根据怪物类型和数量生成怪物
        for (int i = 0; i < count; i++)
        {
            Debug.Log("生成"+enemyType);
            string _temp = "";

            GameObject _newObj = Instantiate(enemyPrefab,enemyPanel.transform);

            switch(enemyType){
                case "假人":
                    _temp = "fakeTest";
                    break;
                case "哥布林":
                    _temp = "goblin";
                    break;
                
            }

            Debug.Log(enemyPool.enemies.Find(x=>x.name==_temp));

            _newObj.GetComponent<EnemyDisplay>().enemy = enemyPool.enemies.Find(x=>x.name==_temp);

            //_newObj.GetComponent<EnemyDisplay>().enemy = enemiesList.
            // GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            // // 在这里设置怪物的类型和其他属性
            // enemy.GetComponent<Enemy>().Initialize(enemyType);
        }
    }
}
