using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OverSceneManager : MonoBehaviour
{
    public int score;
    public GameObject smoke;

    public GameObject[] stars;
    public SpriteRenderer[] spriteRenderer;
    public Text label;

    void Start()
    {
        // 종합 점수 넘겨받기
        score = PlayerPrefs.GetInt("ScoreToSend", score);
        Debug.Log("score : " + score);

        // 종료 루틴 호출
        StartCoroutine(ShutdownRoutine(score));
    }

    // 종료 루틴
    IEnumerator ShutdownRoutine(int score)

    {   //  450으로 고정!!!! 중요!!!!
        score = 451;

        // 스모크 내려주기


        // 평가 알려주기(별 UI와 점수Text UI)
        yield return StartCoroutine(OverallAssessment(score));    

        // 마무리
        if (score >= 450)
        {
            // 몬스터 수집 o
            StartCoroutine(Success());
        }
        else
        {
            // 몬스터 수집 X   
            StartCoroutine(Failure());
        }
    }


    // 평가 알려주기
    IEnumerator OverallAssessment(int score)
    {
        label.text = score.ToString() + "/ 600 점!";
        Debug.Log("최종 평가 알려주기 라벨 셋팅 완료");

        if (score >= 540 && score <= 600)
        {
            stars[0].GetComponent<SpriteRenderer>().color = Color.white;
            stars[1].GetComponent<SpriteRenderer>().color = Color.white;
            stars[2].GetComponent<SpriteRenderer>().color = Color.white;
            stars[3].GetComponent<SpriteRenderer>().color = Color.white;
            stars[4].GetComponent<SpriteRenderer>().color = Color.white;
        }
        else if (score >= 420 && score <= 539)
        {
            stars[0].GetComponent<SpriteRenderer>().color = Color.black;
            stars[1].GetComponent<SpriteRenderer>().color = Color.white;
            stars[2].GetComponent<SpriteRenderer>().color = Color.white;
            stars[3].GetComponent<SpriteRenderer>().color = Color.white;
            stars[4].GetComponent<SpriteRenderer>().color = Color.white;
        }
        else if (score >= 300 && score <= 419)
        {
            stars[0].GetComponent<SpriteRenderer>().color = Color.black;
            stars[1].GetComponent<SpriteRenderer>().color = Color.black;
            stars[2].GetComponent<SpriteRenderer>().color = Color.white;
            stars[3].GetComponent<SpriteRenderer>().color = Color.white;
            stars[4].GetComponent<SpriteRenderer>().color = Color.white;
        }
        else if (score >= 180 && score <= 299)
        {
            stars[0].GetComponent<SpriteRenderer>().color = Color.black;
            stars[1].GetComponent<SpriteRenderer>().color = Color.black;
            stars[2].GetComponent<SpriteRenderer>().color = Color.black;
            stars[3].GetComponent<SpriteRenderer>().color = Color.white;
            stars[4].GetComponent<SpriteRenderer>().color = Color.white;
        }
        else if (score >= 60 && score <= 179)
        {
            stars[0].GetComponent<SpriteRenderer>().color = Color.black;
            stars[1].GetComponent<SpriteRenderer>().color = Color.black;
            stars[2].GetComponent<SpriteRenderer>().color = Color.black;
            stars[3].GetComponent<SpriteRenderer>().color = Color.black;
            stars[4].GetComponent<SpriteRenderer>().color = Color.white;
        }
        else
        {
            stars[0].GetComponent<SpriteRenderer>().color = Color.black;
            stars[1].GetComponent<SpriteRenderer>().color = Color.black;
            stars[2].GetComponent<SpriteRenderer>().color = Color.black;
            stars[3].GetComponent<SpriteRenderer>().color = Color.black;
            stars[4].GetComponent<SpriteRenderer>().color = Color.black;
        }
        Debug.Log("최종 평가 알려주기 별 셋팅 완료");

        yield return new WaitForSeconds(4);

        // 별이랑 라벨 없애기
        label.text = "";
        for (int i=0; i<stars.Length; i++)
        {
            Destroy(stars[i]);
        }
        Debug.Log("별과 라벨 없애기 완료");
    }

    // 몬스터 수집
    IEnumerator Success()
    {
        SaveData();

        label.text = "몬스터 수집 성공!";
        yield return new WaitForSeconds(2);
        label.text = "메인 화면으로 이동합니다";
        SceneManager.LoadScene("MainScene");
        yield return new WaitForSeconds(0);
    }

    IEnumerator Failure()
    {
        label.text = "메인 화면으로 이동합니다";
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("MainScene");
    }

    /// 플레이 데이터 저장
    private void SaveData()
    {
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
    }
}

