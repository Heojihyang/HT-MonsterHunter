using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Text PlayerTitle;
    public Slider ExpBar;
    public Text PlayerGoal;
    
    // Start is called before the first frame update
    void Start()
    {
        // --- 플레이어 프로필 세팅 ---
        // 1. 플레이어 ExpBar : playerdata의 경험치 가져와서 슬라이더 연결
        // 2. 플레이어 호칭(헬신생아/헬린이/헬청년/헬창/헬스의 신) 넣어주기 

        //GameData.instance.playerdata.PlayerExp = 9;
        //GameData.instance.SavePlayerData();

        // 1. 플레이어 ExpBar
        int exp = GameData.instance.playerdata.PlayerExp;
        float expbar;
        if (exp < 50)
        {
            expbar = exp % 10.0f; // 10으로나눈 나머지가 Expbar의 게이지가 된다. 
            ExpBar.value = expbar/10.0f;
        }
        else // 만렙의 경우 
        {
            ExpBar.value = 1.0f; // 게이지 풀로 채우기 
            print("당신은 만렙입니다.");
        }


        // 2. 플레이어 호칭
        float title = exp / 10.0f; // 10으로나눈 몫 값에 따라 칭호 수여
        // 3. 레벨업 까지 남은 횟수
        int remain = exp % 10; // 나머지 
        int n = 10 - remain; // 남은 횟수 n

        if (title < 1)
        {
            PlayerTitle.text = "헬신생아";
            PlayerGoal.text = $"앞으로 헬린이까지 {n} 남음"; // 문자열 보간 ==> $"사과랑 포도를 더하면: {apple + grape}"
        }
        else if (title < 2)
        {
            PlayerTitle.text = "헬린이";
            PlayerGoal.text = $"앞으로 헬청년까지 {n} 남음";
        }
        else if (title < 3)
        {
            PlayerTitle.text = "헬청년";
            PlayerGoal.text = $"앞으로 헬창까지 {n} 남음";
        }
        else if (title < 4)
        {
            PlayerTitle.text = "헬창";
            PlayerGoal.text = $"앞으로 헬스의 신까지 {n} 남음";
        }
        else if (title >= 4)
        {
            PlayerTitle.text = "헬스의 신";
            PlayerGoal.text = "당신은 만렙 입니다 :)";
        }
        else
        {
            PlayerTitle.text = "오류났당~ ><";
            PlayerGoal.text = "화내지 말고 잘 해결해 보쟈큥^^*";
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

    // (운동기록보기) 클릭 시 : TRecordScene으로 이동
    public void ChangeTRecordScene()
    {
        SceneManager.LoadScene("TRecordScene");
    }

    // (도감) 클릭 시 : TMonsterListScene으로 이동
    public void ChangeTMonsterListScene()
    {
        SceneManager.LoadScene("TMonsterListScene");
    }
}
