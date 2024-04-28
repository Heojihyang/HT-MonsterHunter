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
    public bool[] MonsterUnLocked = new bool[20];

    // ���� ����ġ
    public int[] MonsterExp = new int[20];
}

public class GameData : MonoBehaviour
{
    string Path;
    string FileName = "MonsterDataSave";

    MonsterData monsterdata = new MonsterData();

    private void Awake()
    {
        // ��θ� �������
        Path = Application.persistentDataPath + "/";
    }

    void Start()
    {
        
    }

    public void SaveData()
    {
        // 2. �����͸� Json���� ��ȯ
        string data = JsonUtility.ToJson(monsterdata); // Json�� string ���·� �����

        // print(Path); // ���� ������ Ȯ���ϱ�

        // 3. Json�� �ܺο� ����
        File.WriteAllText(Path + FileName, data);

        print("������ ���� �Ϸ�");
    }

    public void LoadData()
    {
        // 1. �ܺο� ����� ���̽��� ������
        // 2. Json�� ������ ���·� ��ȯ
        string data = File.ReadAllText(Path + FileName); // Json�̱� ������ string ���·� �޾���

        // 3. �ҷ��� ������ ���
        monsterdata = JsonUtility.FromJson<MonsterData>(data); // �ҷ��� �����Ͱ� monsterdata�� �������

        print("������ �ҷ����� �Ϸ�");
    }
}
