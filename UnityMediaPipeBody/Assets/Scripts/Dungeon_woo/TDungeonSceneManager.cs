using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TDungeonSceneManager : MonoBehaviour
{
    // PlayerPrefs.SetInt("MonsterNumberToSend", sendMonsterNumber);
    public int receivedMonsterNumber;

    //canvas
    public GameObject canvas;


    void Start()
    {
        // 어떤 던전에 들어온건지 데이터 받아오기
        receivedMonsterNumber = PlayerPrefs.GetInt("MonsterNumberToSend", 0);
        // BGM Start
        SoundManager.instance.PlayBGM("BGM_Ingame");

        // canvas 초기화
        canvas.SetActive(true);
    }

    // 종료 씬으로 이동
    public void GoOverScene(int score)
    {
        PlayerPrefs.SetInt("ScoreToSend", score);   // 씬 로드하면서 몬스터 번호 같이 넘겨주기
        SceneManager.LoadScene("OverScene");
    }

    // 메인씬으로 이동
    public void GoMainScene()
    {
        // BGM Start
        SoundManager.instance.StopBGM("BGM_Ingame");
        SceneManager.LoadScene("MainScene");
    }
}
