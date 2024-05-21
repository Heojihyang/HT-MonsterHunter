using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//// Json ���
// ��ųʸ��� �������� �ʾƼ�, �迭�� �ְ�޾ƾ���

// �����ϴ� ���
// 1. ������ ������ ����
// 2. �����͸� Json���� ��ȯ
// 3. Json�� �ܺο� ����

// �ҷ����� ���
// 1. �ܺο� ����� Json�� ������
// 2. Json�� ������ ���·� ��ȯ
// 3. �ҷ��� ������ ���



//// 1. ������ ������ ����
public class MonsterData
{
    // ����(20��) ���� ����
    // ����O : true , ����X : False
    public bool[] MonsterUnLocked = new bool[10];

    // ���� �̸�
    public string[] MonsterName = new string[10];
}

public class PlayerData
{
    //  �÷��̾� ����ġ 
    public int PlayerExp;
}

public class GamePlayData
{
    // ������ �÷����� ��¥�� �� ��¥��
    // (������ �÷����� Ƚ��, �÷��� �� �ð�, Ŭ������ ����, ������ ����) �� �������� ����
    
    public int playCount;
    public float totalPlayTime;
    public List<string> clearedGames = new List<string>();
    public List<string> collectedMonsters = new List<string>();

    /*
    public GamePlayData()
    {
        clearedGames = new List<string>();
        collectedMonsters = new List<string>();
    }
    */
}


public class GameData : MonoBehaviour
{
    public static GameData instance; // �̱���

    string Path;
    string MonsterFileName = "MonsterDataSave";
    string PlayerFileName = "PlayerDataSave";
    string gamePlayFileName = "GamePlayDataSave";

    public MonsterData monsterdata = new MonsterData();
    public PlayerData playerdata = new PlayerData();
    public Dictionary<DateTime, GamePlayData> gamePlayData = new Dictionary<DateTime, GamePlayData>();

    private void Awake()
    {
        #region �̱���
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        #endregion

        // ��θ� �������
        Path = Application.persistentDataPath + "/";
        print("��λ��� "+Path); // ��� Ȯ�� 
    }

    void Start()
    {

    }

    public void SaveMonsterData()
    {
        // 2. �����͸� Json���� ��ȯ
        string data = JsonUtility.ToJson(monsterdata); // Json�� string ���·� �����

        // print(Path); // ���� ������ Ȯ���ϱ�

        // 3. Json�� �ܺο� ����
        File.WriteAllText(Path + MonsterFileName, data);

        print("���� ������ ���� �Ϸ�");
    }

    public void LoadMonsterData()
    {
        // 1. �ܺο� ����� ���̽��� ������
        // 2. Json�� ������ ���·� ��ȯ
        string data = File.ReadAllText(Path + MonsterFileName); // Json�̱� ������ string ���·� �޾���

        // 3. �ҷ��� ������ ���
        monsterdata = JsonUtility.FromJson<MonsterData>(data); // �ҷ��� �����Ͱ� monsterdata�� �������

        print("���� ������ �ҷ����� �Ϸ�");
    }

    public void SavePlayerData()
    {
        // 2. �����͸� Json���� ��ȯ
        string data = JsonUtility.ToJson(playerdata); // Json�� string ���·� �����

        // print(Path); // ���� ������ Ȯ���ϱ�

        // 3. Json�� �ܺο� ����
        File.WriteAllText(Path + PlayerFileName, data);

        print("�÷��̾� ������ ���� �Ϸ�");
    }

    public void LoadPlayerData()
    {
        // 1. �ܺο� ����� ���̽��� ������
        // 2. Json�� ������ ���·� ��ȯ
        string data = File.ReadAllText(Path + PlayerFileName); // Json�̱� ������ string ���·� �޾���

        // 3. �ҷ��� ������ ���
        playerdata = JsonUtility.FromJson<PlayerData>(data); // �ҷ��� �����Ͱ� monsterdata�� �������

        print("�÷��̾� ������ �ҷ����� �Ϸ�");
    }

    public void SaveGamePlayData()
    {
        string data = JsonUtility.ToJson(new Serialization<DateTime, GamePlayData>(gamePlayData)); // �����͸� Json���� ��ȯ

        // print(data); // ������� {} 

        /*
        print(gamePlayData[DateTime.Today].playCount);
        print(gamePlayData[DateTime.Today].totalPlayTime);
        print(gamePlayData[DateTime.Today].clearedGames[0]);
        print(gamePlayData[DateTime.Today].collectedMonsters[0]);
        */
        File.WriteAllText(Path + gamePlayFileName, data); // Json�� �ܺο� ����
        print("���� �÷��� ������ ���� �Ϸ�");
    }

    public void LoadGamePlayData()
    {
        if (File.Exists(Path + gamePlayFileName))
        {
            string data = File.ReadAllText(Path + gamePlayFileName); // �ܺο� ����� Json�� ������ 
            gamePlayData = JsonUtility.FromJson<Serialization<DateTime, GamePlayData>>(data).ToDictionary(); // Json�� ������ ���·� ��ȯ
            print("���� �÷��� ������ �ҷ����� �Ϸ�");
        }
    }

    // ���� �÷��� ������ �߰� 
    public void AddGamePlayData(DateTime date, int playCount, float playTime, string clearedGame, string collectedMonster)
    {
        // �־��� ��¥�� �����Ͱ� ���� ��쿡�� ���ο� ������ �߰�
        if (!gamePlayData.ContainsKey(date))
        {
            gamePlayData[date] = new GamePlayData();
           
        }


        gamePlayData[date].playCount += playCount;
        gamePlayData[date].totalPlayTime += playTime;
        gamePlayData[date].clearedGames.Add(clearedGame);
        gamePlayData[date].collectedMonsters.Add(collectedMonster);

        /*
        print(gamePlayData[date].playCount );
        print(gamePlayData[date].totalPlayTime );
        print(gamePlayData[date].clearedGames[0]);
        print(gamePlayData[date].collectedMonsters[0]);
        */
        // SaveGamePlayData(); // ������ �߰� �� ��� ����
    }

    // ���� �÷��� ������ ��ȸ 
    public void PrintGamePlayData()
    {
        foreach (var record in gamePlayData)
        {
            Debug.Log("Date: " + record.Key.ToString("yyyy-MM-dd"));
            Debug.Log("Play Count: " + record.Value.playCount);
            Debug.Log("Total Play Time: " + record.Value.totalPlayTime + " hours");

            Debug.Log("Cleared Games:");
            foreach (var game in record.Value.clearedGames)
            {
                Debug.Log("- " + game);
            }

            Debug.Log("Collected Monsters:");
            foreach (var monster in record.Value.collectedMonsters)
            {
                Debug.Log("- " + monster);
            }
        }
    }
}

//// Serializable Ŭ������ Dictionary�� ����ȭ
// JsonUtility =>  Dictionary�� ����ȭ�� �� ���� ������,
// �� Ŭ���� ���ؼ�, Dictionary�� List�� ��ȯ�Ͽ� ����ȭ�� �� �ְ� ���� 

[Serializable]
public class Serialization<TKey, TValue> : ISerializationCallbackReceiver
{
    // ����ȭ�� �� ����� ����Ʈ
    [SerializeField]
    private List<TKey> keys;
    [SerializeField]
    private List<TValue> values;

    // ������ Dictionary�� ������ ����
    private Dictionary<TKey, TValue> target;

    // ������
    public Serialization(Dictionary<TKey, TValue> _target)
    {
        target = _target;
    }
    
    // ����ȭ ������ ȣ��
    public void OnBeforeSerialize()
    {
        // Dictionary�� Ű�� ���� ����Ʈ�� ��ȯ.
        keys = new List<TKey>(target.Keys);
        values = new List<TValue>(target.Values);
    }

    // ����ȭ ���Ŀ� ȣ��
    public void OnAfterDeserialize()
    {
        var count = Math.Min(keys.Count, values.Count);
        target = new Dictionary<TKey, TValue>(count);
        for (var i = 0; i < count; i++)
        {
            // ����Ʈ�� Dictionary�� ��ȯ
            target.Add(keys[i], values[i]);
        }
    }

    // Dictionary ��ȯ 
    public Dictionary<TKey, TValue> ToDictionary()
    {
        return target;
    }
}
