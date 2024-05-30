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
        // 예시 데이터 추가: 현재 날짜, 플레이 횟수 1, 플레이 시간 1.5 시간, 클리어한 게임 "Game1", 수집한 몬스터 "MonsterA"
        // GameData.instance.AddGamePlayData(DateTime.Today, 1, 1.5f, "Game1", "MonsterA");

        // 게임 데이터 로드 해주기 !! 
        GameData.instance.LoadMonsterData();
        GameData.instance.LoadPlayerData();
        // GameData.instance.LoadGamePlayData();
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
