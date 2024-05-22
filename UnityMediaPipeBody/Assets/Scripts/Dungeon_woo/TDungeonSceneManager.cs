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
        // � ������ ���°��� ������ �޾ƿ���
        receivedMonsterNumber = PlayerPrefs.GetInt("MonsterNumberToSend", 0);
        // BGM Start
        SoundManager.instance.PlayBGM("BGM_Ingame");
    }

    // ���ξ����� �̵�
    public void GoMainScene()
    {
        // BGM Start
        SoundManager.instance.StopBGM("BGM_Ingame");
        SceneManager.LoadScene("MainScene");
    }
}
