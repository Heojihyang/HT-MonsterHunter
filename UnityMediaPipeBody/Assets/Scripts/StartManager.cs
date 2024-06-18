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
        // 파일 생성 부분 코드 (처음 데이터 저장 시작할 때 필요한 과정)
        // GameData.instance.SavePlayerData();
        // GameData.instance.SaveGamePlayData();
        // GameData.instance.SaveMonsterData();
        // 위 3줄(Save함수들) 주석 해제해서 한번 플레이 시키고 다시 주석처리 필수ㅎㅎ

        // --------------------------------------------------------------------------
        

        // 게임 데이터 로드 해주기 !! 
        GameData.instance.LoadMonsterData();
        GameData.instance.LoadPlayerData();
        GameData.instance.LoadGamePlayData();  
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
