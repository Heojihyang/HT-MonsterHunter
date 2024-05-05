using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MonListManager : MonoBehaviour
{
    public GameObject MonsterInfo;
    public GameObject MonsterImage;
    public GameObject MonsterPartImg;
    public Text MonsterName;
    public Text MonsterPart;

    public List<MonsterListData> monsterListData;

    MonsterData Monsterdata = new MonsterData(); // GameData.cs
    

    // ���� ���� ���� �ð�ȭ -> ���ӿ�����Ʈ�� �̹��� ���� ��Ű�� 
    void Start()
    {
        // --- ������ ���� ���� (��� Ȯ��) ---
        // GameData.instance.LoadMonsterData();
        // GameData.instance.monsterdata.MonsterName[2] = "";
        // GameData.instance.monsterdata.MonsterUnLocked[2] = false;
        // GameData.instance.SaveMonsterData();
        // ---
        
        // �� ���� �� �ӽ� 
        for (int id = 0; id < 3; id++)
        {
            // ���� ���� ���� Ȯ�� 
            bool haveMonster = GameData.instance.monsterdata.MonsterUnLocked[id];

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
    public void ActMonsterInfo(int id)
    {
        bool monsterUnLooked = GameData.instance.monsterdata.MonsterUnLocked[id];

        // �����Ϸ� �����ΰ�?
        if (monsterUnLooked)
        {
            MonsterInfo.SetActive(true);

            // --- ������ �����ͼ� �־��ֱ� 1~4 ---

            // 1. ���� �̹��� 
            Sprite MonsterImg = monsterListData[id].MonTrueImage; // ��������
            MonsterImage.GetComponent<Image>().sprite = MonsterImg; // �־��ֱ� (UI���� �ʿ�: ���Ȯ��)

            // ���� ������ �ε�
            GameData.instance.LoadMonsterData();

            // 2. ���� �̸� : ���� ������ ��� 
            string monstername = GameData.instance.monsterdata.MonsterName[id];
            MonsterName.text = monstername;

            // 3. ���� �߻� ����
            string monsterpart = monsterListData[id].MonsterPart;
            MonsterPart.text = monsterpart;

            // 4. ���� �߻� ���� �̹��� 
            Sprite monsterpartImg = monsterListData[id].MonsterPartImage;
            MonsterPartImg.GetComponent<Image>().sprite = monsterpartImg;
        }
        
    }

    // �������� �ڷΰ��� ��ư Ŭ��
    public void ChangeMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    // �˾����� �ڷΰ��� ��ư Ŭ��
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