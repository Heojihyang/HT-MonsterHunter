using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{

    void Start()
    {
        // 게임 데이터 로드
        GameData.instance.LoadMonsterData();
        GameData.instance.LoadPlayerData();
        GameData.instance.LoadGamePlayData();


        #region 임시 데이터 추가 하는 코드 
        /*

        // 당일 날짜 (오늘)
        string targetDate = DateTime.Now.ToString("yyyy-MM-dd");

        // 특정 날짜 (지정)
        // string targetDate = "2024-05-22"; // 대상 날짜 설정

        int ClearMonsterNum = 8; // 8번 몬스터는 허벅지 (현재 0번~9번까지 있음)

        // 대상 날짜의 게임 플레이 데이터 확인
        if (!GameData.instance.recordData.dailyRecords.ContainsKey(targetDate))
        {
            // 대상 날짜의 게임 플레이 데이터가 없는 경우: 새로운 데이터 생성

            GamePlayData targetDateGamePlayData = new GamePlayData();
            targetDateGamePlayData.PlayCount = 1;
            targetDateGamePlayData.ClearedMaps = new List<int> { ClearMonsterNum }; // 임시 값 추가

            // 해당 운동 부위에 대한 몬스터 잠금 해제
            GameData.instance.monsterdata.MonsterUnLocked[ClearMonsterNum] = true;

            // 새로운 데이터를 대상 날짜의 게임 플레이 데이터로 설정
            GameData.instance.recordData.dailyRecords[targetDate] = targetDateGamePlayData;

            print(ClearMonsterNum + "번 몬스터가 새로운 게임 플레이 데이터에 저장되었습니다.");

            // 플레이어 경험치 1 증가 
            GameData.instance.playerdata.PlayerExp += 1;

        }
        else
        {
            // 대상 날짜의 게임 플레이 데이터가 이미 존재하는 경우: 데이터 업데이트

            GamePlayData targetDateGamePlayData = GameData.instance.recordData.dailyRecords[targetDate];
            targetDateGamePlayData.PlayCount++; // 플레이 횟수 증가
            targetDateGamePlayData.ClearedMaps.Add(ClearMonsterNum); // 클리어 맵 리스트에 추가 

            // 해당 운동 부위에 대한 몬스터 잠금 해제
            GameData.instance.monsterdata.MonsterUnLocked[ClearMonsterNum] = true; // 바로 윗 줄 코드와 숫자 동일해야함 

            // 업데이트된 데이터를 대상 날짜의 게임 플레이 데이터로 설정
            GameData.instance.recordData.dailyRecords[targetDate] = targetDateGamePlayData;

            print(ClearMonsterNum + "번 몬스터가 게임 플레이 데이터에 추가되었습니다.");

            // 플레이어 경험치 1 증가 
            GameData.instance.playerdata.PlayerExp += 1;
        }


        // 새로운 데이터 추가 저장 후, 반드시 Load도 진행 해주어야함
        // : 새로 업데이트된 데이터를 가져와야하기 때문 

        // 데이터를 저장
        GameData.instance.SavePlayerData();
        GameData.instance.SaveGamePlayData();
        GameData.instance.SaveMonsterData();

        // 데이터를 불러오기 : 새로 업데이트 된 데이터를 가져오는 것.
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
