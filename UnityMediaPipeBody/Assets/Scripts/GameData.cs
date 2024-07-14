using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//// Json ���
//   Json�� string ���·� ����
//   ��ųʸ��� �������� �ʾƼ�, �迭�� �ְ�޾ƾ���

//   �����ϴ� ���
//    1. ������ ������ ����
//    2. �����͸� Json���� ��ȯ
//    3. Json�� �ܺο� ����

//   �ҷ����� ���
//    1. �ܺο� ����� Json�� ������
//    2. Json�� ������ ���·� ��ȯ
//    3. �ҷ��� ������ ���


public class MonsterData
{
    public bool[] MonsterUnLocked = new bool[10];   // ����(20��) ���� ����
    public string[] MonsterName = new string[10];   // ���� �̸�
}

public class PlayerData
{
    public int PlayerExp;   // �÷��̾� ����ġ 
}

[Serializable]
public class GamePlayData
{
    public int PlayCount;            // ������ �÷����� Ƚ��
    public List<int> ClearedMaps;    // Ŭ������ ���Ӹ� ����Ʈ : � ���� 

}

[Serializable]
public class RecordData
{
    public Dictionary<string, GamePlayData> dailyRecords;   // ��¥�� �����÷��� ������ ����
}

// -----------------------------------------------------------------------------------------------------------

public class GameData : MonoBehaviour
{
    public static GameData instance;   // �̱���

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

        Path = Application.persistentDataPath + "/";  // ���
        print("��λ��� " + Path);

        recordData.dailyRecords = new Dictionary<string, GamePlayData>();   // �ʱ�ȭ
    }



    // MonsterData -------------------------------------------------------------------------------------------

    public void SaveMonsterData()
    {
        string data = JsonUtility.ToJson(monsterdata);     // 2. ������ -> Json 
        File.WriteAllText(Path + MonsterFileName, data);   // 3. Json �ܺο� ����
        print("���� ������ ���� �Ϸ�");
    }

    public void LoadMonsterData()
    {
        // ���� ���� ���� Ȯ�� 
        if (File.Exists(Path + MonsterFileName))
        {                                                             // 1. �ܺ� ����� ���̽� ������
            string data = File.ReadAllText(Path + MonsterFileName);   // 2. Json -> ������
            monsterdata = JsonUtility.FromJson<MonsterData>(data);    // 3. �ҷ��� ������ -> monsterdata�� ������
            print("���� ������ �ҷ����� �Ϸ�");
        }
        else
        {
            SaveMonsterData();                                        // ���� ������ �⺻ ������ ����
            print("�⺻ ���� ������ ���� �Ϸ�");
        }
    }



    // PlayerData -------------------------------------------------------------------------------------------

    public void SavePlayerData()
    {
        string data = JsonUtility.ToJson(playerdata);     // 2. ������ -> Json 
        File.WriteAllText(Path + PlayerFileName, data);   // 3. Json �ܺο� ����
        print("�÷��̾� ������ ���� �Ϸ�");
    }

    public void LoadPlayerData()
    {
        if (File.Exists(Path + PlayerFileName))
        {                                                             // 1. �ܺ� ����� ���̽� ������
            string data = File.ReadAllText(Path + PlayerFileName);    // 2. Json -> ������
            playerdata = JsonUtility.FromJson<PlayerData>(data);      // 3. �ҷ��� ������ -> playerdata�� ������
            print("�÷��̾� ������ �ҷ����� �Ϸ�");
        }
        else
        {
            SavePlayerData();                                         // ���� ������ �⺻ ������ ����
            print("�⺻ �÷��̾� ������ ���� �Ϸ�");
        }
    }



    // GamePlayData -------------------------------------------------------------------------------------------

    public void SaveGamePlayData()
    {
        string data = DictionaryJsonUtility.ToJson(recordData.dailyRecords, true);   // 2. ������ -> Json (���� ���� ��ȯ ���)
        File.WriteAllText(Path + GamePlayFileName, data);                            // 3. Json �ܺο� ����
        print("���� �÷��� ������ ���� �Ϸ�");

    }

    public void LoadGamePlayData()
    {
        if (File.Exists(Path + GamePlayFileName))
        {                                                                                             // 1. �ܺ� ����� ���̽� ������
            string data = File.ReadAllText(Path + GamePlayFileName);                                  // 2. Json -> ������
            recordData.dailyRecords = DictionaryJsonUtility.FromJson<string, GamePlayData>(data);     // 3. �ҷ��� ������ -> gameplaydata�� ������
            print("���� �÷��� ������ �ҷ����� �Ϸ�");
        }
        else
        {
            SaveGamePlayData();                                                                       // ���� ������ �⺻ ������ ����
            print("�⺻ ���� �÷��� ������ ���� �Ϸ�");
        }
    }

}
