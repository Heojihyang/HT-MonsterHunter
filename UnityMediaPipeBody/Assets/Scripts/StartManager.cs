using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        // ���� ������ �ε� ���ֱ� !! 
        GameData.instance.LoadMonsterData();
        GameData.instance.LoadPlayerData();
        GameData.instance.LoadGamePlayData();


        #region ���÷� �ӽ� ������ �߰� 
        /*
        // ���� ��¥ (����)
        // string targetDate = DateTime.Now.ToString("yyyy-MM-dd"); 

        // Ư�� ��¥ (����)
        string targetDate = "2024-05-22"; // ��� ��¥ ����

        // ��� ��¥�� ���� �÷��� ������ Ȯ��
        if (!GameData.instance.recordData.dailyRecords.ContainsKey(targetDate))
        {
            // ��� ��¥�� ���� �÷��� �����Ͱ� ���� ���: ���ο� ������ ����
            GamePlayData targetDateGamePlayData = new GamePlayData();
            targetDateGamePlayData.PlayCount = 1;
            targetDateGamePlayData.ClearedMaps = new List<int> { 6 }; // �ӽ� �� �߰�

            // �ش� � ������ ���� ���� ��� ����
            GameData.instance.monsterdata.MonsterUnLocked[6] = true;

            // ���ο� �����͸� ��� ��¥�� ���� �÷��� �����ͷ� ����
            GameData.instance.recordData.dailyRecords[targetDate] = targetDateGamePlayData;
        }
        else
        {
            // ��� ��¥�� ���� �÷��� �����Ͱ� �̹� �����ϴ� ���: ������ ������Ʈ
            GamePlayData targetDateGamePlayData = GameData.instance.recordData.dailyRecords[targetDate];
            targetDateGamePlayData.PlayCount++; // �÷��� Ƚ�� ����
            targetDateGamePlayData.ClearedMaps.Add(6); // �ӽ� ������ 1 �߰�

            // �ش� � ������ ���� ���� ��� ����
            GameData.instance.monsterdata.MonsterUnLocked[6] = true;

            // ������Ʈ�� �����͸� ��� ��¥�� ���� �÷��� �����ͷ� ����
            GameData.instance.recordData.dailyRecords[targetDate] = targetDateGamePlayData;
        }


        // �����͸� ����
        GameData.instance.SaveGamePlayData();
        GameData.instance.SaveMonsterData();

        // �����͸� �ҷ�����
        GameData.instance.LoadGamePlayData();
        GameData.instance.LoadMonsterData();
        */
        #endregion

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
