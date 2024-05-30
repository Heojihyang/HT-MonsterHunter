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

    // ���� �÷��� Ƚ��
    public int playCount; 

    // �� �÷��� �ð�
    public float totalPlayTime;

    // Ŭ������ ����
    public bool[] ClearedGames = new bool[10];

    // ������ ����
    public bool[] CollectedMonsters = new bool[10];
    

    /*
    public int playCount;
    public float totalPlayTime;
    public List<string> clearedGames = new List<string>();
    public List<string> collectedMonsters = new List<string>();

    
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
    public GamePlayData gmaeplaydata = new GamePlayData();
    // public Dictionary<string, GamePlayData> gamePlayData = new Dictionary<string, GamePlayData>();

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
        // 2. �����͸� Json���� ��ȯ
        string data = JsonUtility.ToJson(gmaeplaydata); // Json�� string ���·� �����

        // print(Path); // ���� ������ Ȯ���ϱ�

        // 3. Json�� �ܺο� ����
        File.WriteAllText(Path + PlayerFileName, data);

        print("�÷��̾� ������ ���� �Ϸ�");

        // var serializedData = new SerializableDictionary(gamePlayData);
        // string data = JsonUtility.ToJson(serializedData);

        /*
        print(gamePlayData[DateTime.Today].playCount);
        print(gamePlayData[DateTime.Today].totalPlayTime);
        print(gamePlayData[DateTime.Today].clearedGames[0]);
        print(gamePlayData[DateTime.Today].collectedMonsters[0]);
        */
        // File.WriteAllText(Path + gamePlayFileName, data); // Json�� �ܺο� ����
        // print("���� �÷��� ������ ���� �Ϸ�");
    }

    public void LoadGamePlayData()
    {
        /*
        if (File.Exists(Path + gamePlayFileName))
        {
            string data = File.ReadAllText(Path + gamePlayFileName);
            var serializedData = JsonUtility.FromJson<SerializableDictionary>(data);
            gamePlayData = serializedData.ToDictionary();
            print("���� �÷��� ������ �ҷ����� �Ϸ�");
        }
        */
        
    }


    // ���� �÷��� ������ �߰� 
    /*
    public void AddGamePlayData(DateTime date, int playCount, float playTime, string clearedGame, string collectedMonster)
    {
        string dateKey = date.ToString("yyyy-MM-dd");

        // �־��� ��¥�� �����Ͱ� ���� ��쿡�� ���ο� ������ �߰�
        if (!gamePlayData.ContainsKey(dateKey))
        {
            gamePlayData[dateKey] = new GamePlayData();
        }

        gamePlayData[dateKey].playCount += playCount;
        gamePlayData[dateKey].totalPlayTime += playTime;
        gamePlayData[dateKey].clearedGames.Add(clearedGame);
        gamePlayData[dateKey].collectedMonsters.Add(collectedMonster);

        SaveGamePlayData(); // ������ �߰� �� ��� ����
    }
    */

    // ���� �÷��� ������ ��ȸ 
    /*
    public void PrintGamePlayData()
    {

        foreach (var record in gamePlayData)
        {
            Debug.Log("Date: " + record.Key);
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
    */
}

/*
[Serializable]
public class SerializableDictionary
{
    [SerializeField]
    private List<string> keys = new List<string>();
    [SerializeField]
    private List<GamePlayData> values = new List<GamePlayData>();

    private Dictionary<string, GamePlayData> target;

    public SerializableDictionary(Dictionary<string, GamePlayData> target)
    {
        this.target = target;
    }

    public void OnBeforeSerialize()
    {
        keys.Clear();
        values.Clear();
        foreach (var kvp in target)
        {
            keys.Add(kvp.Key);
            values.Add(kvp.Value);
        }
    }

    public void OnAfterDeserialize()
    {
        target = new Dictionary<string, GamePlayData>();
        for (int i = 0; i < keys.Count; i++)
        {
            target.Add(keys[i], values[i]);
        }
    }

    public Dictionary<string, GamePlayData> ToDictionary()
    {
        return target;
    }
}
*/