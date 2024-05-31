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

        // 게임 데이터 로드 해주기 !! 
        GameData.instance.LoadMonsterData();
        GameData.instance.LoadPlayerData();
        GameData.instance.LoadGamePlayData();


        #region 예시로 임시 데이터 추가 
        /*
        string today = DateTime.Now.ToString("yyyy-MM-dd");
        GamePlayData todayGamePlayData = new GamePlayData();
        todayGamePlayData.PlayCount = 5;
        todayGamePlayData.TotalPlayTime = 120.5f;
        todayGamePlayData.ClearedMaps = new List<string> { "Map1", "Map2" };
        todayGamePlayData.CollectedMonsters = new MonsterData();
        todayGamePlayData.CollectedMonsters.MonsterUnLocked[0] = true;
        // todayGamePlayData.CollectedMonsters.MonsterName[0] = "한벙두";

        // 오늘의 게임 플레이 데이터를 기록
        GameData.instance.recordData.dailyRecords[today] = todayGamePlayData;

        // 데이터를 저장
        GameData.instance.SaveGamePlayData();

        // 데이터를 불러오기
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
