using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TDungeonSceneManager : MonoBehaviour
{
    // PlayerPrefs.SetInt("MonsterNumberToSend", sendMonsterNumber);
    public int receivedMonsterNumber;

    void Start()
    {
        // 어떤 던전에 들어온건지 데이터 받아오기
        receivedMonsterNumber = PlayerPrefs.GetInt("MonsterNumberToSend", 0);
        // BGM Start
        SoundManager.instance.PlayBGM("BGM_Ingame");
    }

    // 메인씬으로 이동
    public void GoMainScene()
    {
        // BGM Start
        SoundManager.instance.StopBGM("BGM_Ingame");
        SceneManager.LoadScene("MainScene");
    }
}
