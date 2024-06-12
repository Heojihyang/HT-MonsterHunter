using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectManager : MonoBehaviour
{
    public GameObject previousMonster;
    public GameObject ExplainButton;
    public GameObject Explain;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 알맞은 몬스터 이미지 활성화 & 운동 설명 버튼 활성화 
    public void ActBlob(GameObject newMonster)
    {
        // 이전 몬스터 비활성화
        if (previousMonster != null)
            previousMonster.SetActive(false);

        // 새로운 몬스터 활성화 
        newMonster.SetActive(true);

        ExplainButton.SetActive(true);

        // 이전 몬스터 업데이트
        previousMonster = newMonster;
    }

    // 운동 버튼 클릭 시 : 운동설명 창 활성화
    public void ActExerciseExplain()
    {
        Explain.SetActive(true);
    }

    // 운동 설명 창에서 뒤로가기 클릭 시 : 운동설명 창 비활성화
    public void InActExerciseExplain()
    {
        Explain.SetActive(false);

        // 켜져있던 몬스터와 설명 버튼 비활성화 시키기

        // 이전 몬스터와 설명 버튼 비활성화
        if (previousMonster != null)
            previousMonster.SetActive(false);
        ExplainButton.SetActive(false);
    }

    // (시작) 클릭 시 : 던전으로 이동
    public void ChangeTDungeonScene()
    {
        SoundManager.instance.StopBGM("BGM_Main");      // BGM 끄기
        PlayerPrefs.SetInt("MonsterNumberToSend", 0);   // 씬 로드하면서 몬스터 번호 같이 넘겨주기
        SceneManager.LoadScene("TDungeonScene");
    }

    // (시작) 클릭 시 : 메인 화면으로 이동
    public void ChangeMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
