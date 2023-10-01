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

public class LevelManager : MonoBehaviour
{
    public GameObject enemyPrefab; // 预制体1
    public TextAsset levelJson; // 将 JSON 文件拖拽到该字段
    public EnemiesList enemiesList;//怪物列表

    private void Start()
    {
        LoadLevel();
    }

    private void LoadLevel()
    {
        // 将 JSON 数据转换为 LevelData 对象
        LevelData levelData = JsonUtility.FromJson<LevelData>(levelJson.ToString());

        // 输出关卡信息
        Debug.Log("Level: " + levelData.level);

        foreach (EnemyData enemyData in levelData.enemies)
        {
            // 根据 JSON 数据生成怪物
            GenerateEnemies(enemyData.type, enemyData.count);
        }
    }

    private void GenerateEnemies(string enemyType, int count)
    {
        Debug.Log("执行GenerateEnemies，准备生成"+enemyType);
        GameObject _newPrefab = Instantiate(enemyPrefab);
        string _temp="";
        // 根据怪物类型选择对应的预制体
        switch (enemyType)
        {
            case "史莱姆":
                _temp = "slime";
                break;
            case "假人":
                _temp = "fakeTest";
                break;
            // 可以添加更多类型的预制体
        }
        _newPrefab.GetComponent<EnemyDisplay>().enemy = enemiesList.enemies.Find(x=>x.name ==_temp);
        
    }

    // private Vector3 GetRandomSpawnPosition()
    // {
    //     // 在实际游戏中，你可能需要编写逻辑来获取有效的随机生成位置
    //     return new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f));
    // }
}
