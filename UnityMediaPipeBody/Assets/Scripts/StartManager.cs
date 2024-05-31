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
        string today = DateTime.Now.ToString("yyyy-MM-dd");
        GamePlayData todayGamePlayData = new GamePlayData();
        todayGamePlayData.PlayCount = 5;
        todayGamePlayData.TotalPlayTime = 120.5f;
        todayGamePlayData.ClearedMaps = new List<string> { "Map1", "Map2" };
        todayGamePlayData.CollectedMonsters = new MonsterData();
        todayGamePlayData.CollectedMonsters.MonsterUnLocked[0] = true;
        // todayGamePlayData.CollectedMonsters.MonsterName[0] = "�Ѻ���";

        // ������ ���� �÷��� �����͸� ���
        GameData.instance.recordData.dailyRecords[today] = todayGamePlayData;

        // �����͸� ����
        GameData.instance.SaveGamePlayData();

        // �����͸� �ҷ�����
        GameData.instance.LoadGamePlayData();
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
