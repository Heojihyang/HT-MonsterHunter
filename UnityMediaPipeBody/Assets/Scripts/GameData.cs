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

[Serializable]
public class GamePlayData
{
    // ������ �÷����� ��¥�� �� ��¥�� ������ ����

    public int PlayCount; // ������ �÷����� Ƚ��

    // public float TotalPlayTime; // �÷��� �� �ð� = � ������ �ð� Ȱ���ؼ� ��� 

    public List<string> ClearedMaps; // Ŭ������ ���Ӹ� ����Ʈ : � ���� 

    // public MonsterData CollectedMonsters; // ������ ���� ������
}

// ��¥�� � ��� 
[Serializable]
public class RecordData
{
    public Dictionary<string, GamePlayData> dailyRecords; // ��¥�� ���� �÷��� �����͸� �����ϴ� Dictionary
}

public class GameData : MonoBehaviour
{
    public static GameData instance; // �̱���

    string Path;
    string MonsterFileName = "MonsterDataSave";
    string PlayerFileName = "PlayerDataSave";
    string GamePlayFileName = "GamePlayDataSave";

    public MonsterData monsterdata = new MonsterData();
    public PlayerData playerdata = new PlayerData();
    public RecordData recordData = new RecordData();

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

        // �ʱ�ȭ
        recordData.dailyRecords = new Dictionary<string, GamePlayData>();
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
        string data = DictionaryJsonUtility.ToJson(recordData.dailyRecords, true); // JSON ��ȯ

        // 3. Json�� �ܺο� ����
        File.WriteAllText(Path + GamePlayFileName, data);

        print("���� �÷��� ������ ���� �Ϸ�");

    }

    public void LoadGamePlayData()
    {
        if (File.Exists(Path + GamePlayFileName))
        {
            string data = File.ReadAllText(Path + GamePlayFileName);
            recordData.dailyRecords = DictionaryJsonUtility.FromJson<string, GamePlayData>(data);
            print("���� �÷��� ������ �ҷ����� �Ϸ�");
        }
        else
        {
            print("���� �÷��� ������ ������ �������� �ʽ��ϴ�.");
        }
    }

}
