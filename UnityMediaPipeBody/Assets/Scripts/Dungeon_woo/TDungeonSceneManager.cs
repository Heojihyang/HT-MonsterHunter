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
        // � ������ ���°��� ������ �޾ƿ���
        receivedMonsterNumber = PlayerPrefs.GetInt("MonsterNumberToSend", 0);
        // BGM Start
        SoundManager.instance.PlayBGM("BGM_Ingame");

        // canvas �ʱ�ȭ
        canvas.SetActive(true);
    }

    // ���� ������ �̵�
    public void GoOverScene(int score)
    {
        PlayerPrefs.SetInt("ScoreToSend", score);   // �� �ε��ϸ鼭 ���� ��ȣ ���� �Ѱ��ֱ�
        SceneManager.LoadScene("OverScene");
    }

    // ���ξ����� �̵�
    public void GoMainScene()
    {
        // BGM Start
        SoundManager.instance.StopBGM("BGM_Ingame");
        SceneManager.LoadScene("MainScene");
    }
}
