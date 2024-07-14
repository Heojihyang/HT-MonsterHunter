using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{

    void Start()
    {
        // ���� ������ �ε�
        GameData.instance.LoadMonsterData();
        GameData.instance.LoadPlayerData();
        GameData.instance.LoadGamePlayData();


        #region �ӽ� ������ �߰� �ϴ� �ڵ� 
        /*

        // ���� ��¥ (����)
        string targetDate = DateTime.Now.ToString("yyyy-MM-dd");

        // Ư�� ��¥ (����)
        // string targetDate = "2024-05-22"; // ��� ��¥ ����

        int ClearMonsterNum = 8; // 8�� ���ʹ� ����� (���� 0��~9������ ����)

        // ��� ��¥�� ���� �÷��� ������ Ȯ��
        if (!GameData.instance.recordData.dailyRecords.ContainsKey(targetDate))
        {
            // ��� ��¥�� ���� �÷��� �����Ͱ� ���� ���: ���ο� ������ ����

            GamePlayData targetDateGamePlayData = new GamePlayData();
            targetDateGamePlayData.PlayCount = 1;
            targetDateGamePlayData.ClearedMaps = new List<int> { ClearMonsterNum }; // �ӽ� �� �߰�

            // �ش� � ������ ���� ���� ��� ����
            GameData.instance.monsterdata.MonsterUnLocked[ClearMonsterNum] = true;

            // ���ο� �����͸� ��� ��¥�� ���� �÷��� �����ͷ� ����
            GameData.instance.recordData.dailyRecords[targetDate] = targetDateGamePlayData;

            print(ClearMonsterNum + "�� ���Ͱ� ���ο� ���� �÷��� �����Ϳ� ����Ǿ����ϴ�.");

            // �÷��̾� ����ġ 1 ���� 
            GameData.instance.playerdata.PlayerExp += 1;

        }
        else
        {
            // ��� ��¥�� ���� �÷��� �����Ͱ� �̹� �����ϴ� ���: ������ ������Ʈ

            GamePlayData targetDateGamePlayData = GameData.instance.recordData.dailyRecords[targetDate];
            targetDateGamePlayData.PlayCount++; // �÷��� Ƚ�� ����
            targetDateGamePlayData.ClearedMaps.Add(ClearMonsterNum); // Ŭ���� �� ����Ʈ�� �߰� 

            // �ش� � ������ ���� ���� ��� ����
            GameData.instance.monsterdata.MonsterUnLocked[ClearMonsterNum] = true; // �ٷ� �� �� �ڵ�� ���� �����ؾ��� 

            // ������Ʈ�� �����͸� ��� ��¥�� ���� �÷��� �����ͷ� ����
            GameData.instance.recordData.dailyRecords[targetDate] = targetDateGamePlayData;

            print(ClearMonsterNum + "�� ���Ͱ� ���� �÷��� �����Ϳ� �߰��Ǿ����ϴ�.");

            // �÷��̾� ����ġ 1 ���� 
            GameData.instance.playerdata.PlayerExp += 1;
        }


        // ���ο� ������ �߰� ���� ��, �ݵ�� Load�� ���� ���־����
        // : ���� ������Ʈ�� �����͸� �����;��ϱ� ���� 

        // �����͸� ����
        GameData.instance.SavePlayerData();
        GameData.instance.SaveGamePlayData();
        GameData.instance.SaveMonsterData();

        // �����͸� �ҷ����� : ���� ������Ʈ �� �����͸� �������� ��.
        GameData.instance.LoadPlayerData();
        GameData.instance.LoadGamePlayData();
        GameData.instance.LoadMonsterData();
        */

        #endregion

    }


    public void ChangeMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
