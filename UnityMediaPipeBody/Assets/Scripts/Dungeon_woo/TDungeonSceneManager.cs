using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TDungeonSceneManager : MonoBehaviour
{
    // � ������ ���°��� ������ �޾ƿ���
    // PlayerPrefs.SetInt("MonsterNumberToSend", sendMonsterNumber);
    public int receivedMonsterNumber;

    void Start()
    {
        receivedMonsterNumber = PlayerPrefs.GetInt("MonsterNumberToSend", 0);
    }

    // ���ξ����� �̵�
    public void GoMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
