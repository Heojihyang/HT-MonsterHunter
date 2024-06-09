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
        // 당일 날짜 (오늘)
        // string targetDate = DateTime.Now.ToString("yyyy-MM-dd"); 

        // 특정 날짜 (지정)
        string targetDate = "2024-05-22"; // 대상 날짜 설정

        // 대상 날짜의 게임 플레이 데이터 확인
        if (!GameData.instance.recordData.dailyRecords.ContainsKey(targetDate))
        {
            // 대상 날짜의 게임 플레이 데이터가 없는 경우: 새로운 데이터 생성
            GamePlayData targetDateGamePlayData = new GamePlayData();
            targetDateGamePlayData.PlayCount = 1;
            targetDateGamePlayData.ClearedMaps = new List<int> { 6 }; // 임시 값 추가

            // 해당 운동 부위에 대한 몬스터 잠금 해제
            GameData.instance.monsterdata.MonsterUnLocked[6] = true;

            // 새로운 데이터를 대상 날짜의 게임 플레이 데이터로 설정
            GameData.instance.recordData.dailyRecords[targetDate] = targetDateGamePlayData;
        }
        else
        {
            // 대상 날짜의 게임 플레이 데이터가 이미 존재하는 경우: 데이터 업데이트
            GamePlayData targetDateGamePlayData = GameData.instance.recordData.dailyRecords[targetDate];
            targetDateGamePlayData.PlayCount++; // 플레이 횟수 증가
            targetDateGamePlayData.ClearedMaps.Add(6); // 임시 값으로 1 추가

            // 해당 운동 부위에 대한 몬스터 잠금 해제
            GameData.instance.monsterdata.MonsterUnLocked[6] = true;

            // 업데이트된 데이터를 대상 날짜의 게임 플레이 데이터로 설정
            GameData.instance.recordData.dailyRecords[targetDate] = targetDateGamePlayData;
        }


        // 데이터를 저장
        GameData.instance.SaveGamePlayData();
        GameData.instance.SaveMonsterData();

        // 데이터를 불러오기
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
