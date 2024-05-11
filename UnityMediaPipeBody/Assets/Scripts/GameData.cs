using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// Json ���
// ��ųʸ��� �������� �ʾƼ�, �迭�� �ְ�޾ƾ���

// �����ϴ� ���
// 1. ������ ������ ����
// 2. �����͸� Json���� ��ȯ
// 3. Json�� �ܺο� ����

// �ҷ����� ���
// 1. �ܺο� ����� Json�� ������
// 2. Json�� ������ ���·� ��ȯ
// 3. �ҷ��� ������ ���

// 1. ������ ������ ����
public class MonsterData
{
    // ����(20��) ���� ����
    // ����O : true , ����X : False
    public bool[] MonsterUnLocked = new bool[10];

    // ���� ����ġ ��� ��..!
    // public int[] MonsterExp = new int[10];

    // ���� �̸�
    public string[] MonsterName = new string[10];
}

public class PlayerData
{
    //  �÷��̾� ����ġ 
    public int PlayerExp;
}

public class GameData : MonoBehaviour
{
    public static GameData instance; // �̱���

    string Path;
    string MonsterFileName = "MonsterDataSave";
    string PlayerFileName = "PlayerDataSave";

    public MonsterData monsterdata = new MonsterData();
    public PlayerData playerdata = new PlayerData();

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
        print("��λ���"+Path); // ��� Ȯ�� 
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
}
