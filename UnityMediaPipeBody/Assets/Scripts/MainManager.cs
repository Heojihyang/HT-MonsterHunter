using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Slider ExpBar;

    // Start is called before the first frame update
    void Start()
    {
        // 플레이어 데이터 로드
        // GameData.instance.LoadPlayerData();

        // --- 플레이어 프로필 세팅 ---
        // 1. 플레이어 ExpBar : playerdata의 경험치 가져와서 슬라이더 연결
        // 2. 플레이어 호칭(헬신생아/헬린이/헬청년/헬창/헬스의 신) 넣어주기 

        // 1. 플레이어 ExpBar
        int exp = GameData.instance.playerdata.PlayerExp;
        if(exp < 50)
        {
            float expbar = exp % 10.0f; // 10으로나눈 나머지가 Expbar의 게이지가 된다. 
            ExpBar.value = expbar;
            print("나머지 값: " + expbar);
        }
        else
        {

            print("당신은 만렙입니다.");
        }
        

        // 2. 플레이어 호칭
        float title = exp / 10.0f; // 10으로나눈 몫 값에 따라 칭호 수여
        print("몫의 값: " + title);

        if(title < 1) // 헬신생아
        {
            print("헬신생아");
        }
        else if(title < 2) // 헬린이
        {
            print("헬린이");
        }
        else if (title < 3) // 헬청년
        {
            print("헬청년");
        }
        else if (title < 4) // 헬창
        {
            print("헬창");
        }
        else if (title >= 5) // 헬스의 신
        {
            print("헬스의 신");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // (운동하기) 클릭 시 : SelectScene으로 이동 
    public void ChangeSelectScene()
    {
        SceneManager.LoadScene("SelectScene");
    }

    // (운동기록보기) 클릭 시 : 

    // (도감) 클릭 시 : TMonsterListScene으로 이동
    public void ChangeTMonsterListScene()
    {
        SceneManager.LoadScene("TMonsterListScene");
    }
}
