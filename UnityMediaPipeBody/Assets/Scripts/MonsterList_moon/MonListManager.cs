using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MonListManager : MonoBehaviour
{
    public List<MonsterListData> monsterListData;

    MonsterData Monsterdata = new MonsterData(); // GameData.cs

    GameData gamedata = new GameData();
    // public GameObject gamedata;

    public GameObject MonsterInfo;
    public GameObject MonsterImage;
    public Text MonsterName;


    // ���� ���� ���� �ð�ȭ -> ���ӿ�����Ʈ�� �̹��� ���� ��Ű�� 
    void Start()
    {
        // �׽�Ʈ �غ��� 
        Monsterdata.MonsterUnLocked[0] = true;
        Monsterdata.MonsterUnLocked[1] = true;

        // �� ���� �� �ӽ� 3
        for (int id = 0; id < 3; id++)
        {
            // ���� ���� ���� Ȯ�� 
            bool haveMonster = Monsterdata.MonsterUnLocked[id];

            if (haveMonster) 
            {
                // monsterListData ����Ʈ���� ������ IDã��
                int monsterId = monsterListData.FindIndex(data => data.ID == id);

                // �ش� ID�� GameObject�� Sprite �����ͼ�
                GameObject FalseImage = monsterListData[monsterId].MonFalseImage;
                Sprite TrueImage = monsterListData[monsterId].MonTrueImage;

                // GameObject�� FalseImage�� TrueImage�� �������ֱ�
                FalseImage.GetComponent<Image>().sprite = TrueImage;

                print(id + "�� ������ ���� �Դϴ�.");
            }
            else
            {
                print(id + "�� ���� �������� ���� ���� �Դϴ�.");
            }
          
        }
        
    }

    // ������ ���� Ŭ�� �� ����â Ȱ��ȭ
    // �ش� ��ư�� ���� ID �Ѱ��ְ� ���� ��������ֱ� 
    public void ActMonsterInfo(int ID)
    {
        MonsterInfo.SetActive(true);

        // --- ������ �����ͼ� �־��ֱ� ---
        // 1. ���� �̹��� : MonsterListData�� True �̹��� �������� 
        // 2. ���� �̸� : MonsterData�� �̸� �����ͼ� Text�� �־��ֱ�
        // 3. ���� �߻� ���� -> ID�� ���� ������ ������ �����س��� �ű⼭ �������� 
        // 4. ���� �߻� ���� �̹��� 

        // 1. ���� �̹��� 
        Sprite MonsterImg = monsterListData[ID].MonTrueImage; // ��������
        MonsterImage.GetComponent<Image>().sprite = MonsterImg; // �־��ֱ� (UI���� �ʿ�: ���Ȯ��)

        // ���� ������ �ε�
        GameData.instance.LoadMonsterData(); 

        // 2. ���� �̸� : ���� ������ ��� 
        string monstername = GameData.instance.monsterdata.MonsterName[ID];
        MonsterName.text = monstername;

        // 3. ���� �߻� ����


    }

    // �ڷΰ��� ��ư Ŭ��
    public void InActMonsterInfo()
    {
        MonsterInfo.SetActive(false);
    }

}

// ���� ���� �ð�ȭ ���� ������ 
[Serializable]
public class MonsterListData
{
    [field: SerializeField]
    public int ID { get; private set; } // MonsterData�� �迭 ID�� ����

    [field: SerializeField]
    public GameObject MonFalseImage { get; private set; } // ����X ǥ��

    [field: SerializeField]
    public Sprite MonTrueImage { get; private set; } // ����O ǥ�� 

    [field: SerializeField]
    public string MonsterPart { get; private set; } // ���� �߻� ����

    [field: SerializeField]
    public Sprite MonsterPartImage { get; private set; } // ���� �߻� ���� �̹���

}
