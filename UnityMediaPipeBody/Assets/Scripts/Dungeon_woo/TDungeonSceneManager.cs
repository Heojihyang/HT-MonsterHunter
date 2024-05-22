using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TDungeonSceneManager : MonoBehaviour
{
    // 어떤 던전에 들어온건지 데이터 받아오기
    // PlayerPrefs.SetInt("MonsterNumberToSend", sendMonsterNumber);
    public int receivedMonsterNumber;

    void Start()
    {
        receivedMonsterNumber = PlayerPrefs.GetInt("MonsterNumberToSend", 0);
    }

    // 메인씬으로 이동
    public void GoMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
