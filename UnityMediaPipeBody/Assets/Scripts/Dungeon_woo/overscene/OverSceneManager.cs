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
        // ���� ���� �Ѱܹޱ�
        score = PlayerPrefs.GetInt("ScoreToSend", score);
        Debug.Log("score : " + score);

        // ���� ��ƾ ȣ��
        StartCoroutine(ShutdownRoutine(score));
    }

    // ���� ��ƾ
    IEnumerator ShutdownRoutine(int score)

    {   //  450���� ����!!!! �߿�!!!!
        score = 451;

        // ����ũ �����ֱ�


        // �� �˷��ֱ�(�� UI�� ����Text UI)
        yield return StartCoroutine(OverallAssessment(score));    

        // ������
        if (score >= 450)
        {
            // ���� ���� o
            StartCoroutine(Success());
        }
        else
        {
            // ���� ���� X   
            StartCoroutine(Failure());
        }
    }


    // �� �˷��ֱ�
    IEnumerator OverallAssessment(int score)
    {
        label.text = score.ToString() + "/ 600 ��!";
        Debug.Log("���� �� �˷��ֱ� �� ���� �Ϸ�");

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
        Debug.Log("���� �� �˷��ֱ� �� ���� �Ϸ�");

        yield return new WaitForSeconds(4);

        // ���̶� �� ���ֱ�
        label.text = "";
        for (int i=0; i<stars.Length; i++)
        {
            Destroy(stars[i]);
        }
        Debug.Log("���� �� ���ֱ� �Ϸ�");
    }

    // ���� ����
    IEnumerator Success()
    {
        SaveData();

        label.text = "���� ���� ����!";
        yield return new WaitForSeconds(2);
        label.text = "���� ȭ������ �̵��մϴ�";
        SceneManager.LoadScene("MainScene");
        yield return new WaitForSeconds(0);
    }

    IEnumerator Failure()
    {
        label.text = "���� ȭ������ �̵��մϴ�";
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("MainScene");
    }

    /// �÷��� ������ ����
    private void SaveData()
    {
        // ���� ��¥ (����)
        string targetDate = DateTime.Now.ToString("yyyy-MM-dd");

        // Ư�� ��¥ (����)
        // string targetDate = "2024-05-22"; // ��� ��¥ ����

        int ClearMonsterNum = 8; // 8�� ���ʹ� ����� (���� 0��~9������ ����)

        // ��� ��¥�� ���� �÷��� ������ Ȯ��
        if (!GameData.instance.recordData.dailyRecords.ContainsKey(targetDate))
        {
            // ��� ��¥�� ���� �÷��� �����Ͱ� ���� ���: ���ο� ������ ����

            GamePlayData targetDateGamePlayData = new GamePlayData();
            targetDateGamePlayData.PlayCount = 1;
            targetDateGamePlayData.ClearedMaps = new List<int> { ClearMonsterNum }; // �ӽ� �� �߰�

            // �ش� � ������ ���� ���� ��� ����
            GameData.instance.monsterdata.MonsterUnLocked[ClearMonsterNum] = true;

            // ���ο� �����͸� ��� ��¥�� ���� �÷��� �����ͷ� ����
            GameData.instance.recordData.dailyRecords[targetDate] = targetDateGamePlayData;

            print(ClearMonsterNum + "�� ���Ͱ� ���ο� ���� �÷��� �����Ϳ� ����Ǿ����ϴ�.");

            // �÷��̾� ����ġ 1 ���� 
            GameData.instance.playerdata.PlayerExp += 1;

        }
        else
        {
            // ��� ��¥�� ���� �÷��� �����Ͱ� �̹� �����ϴ� ���: ������ ������Ʈ

            GamePlayData targetDateGamePlayData = GameData.instance.recordData.dailyRecords[targetDate];
            targetDateGamePlayData.PlayCount++; // �÷��� Ƚ�� ����
            targetDateGamePlayData.ClearedMaps.Add(ClearMonsterNum); // Ŭ���� �� ����Ʈ�� �߰� 

            // �ش� � ������ ���� ���� ��� ����
            GameData.instance.monsterdata.MonsterUnLocked[ClearMonsterNum] = true; // �ٷ� �� �� �ڵ�� ���� �����ؾ��� 

            // ������Ʈ�� �����͸� ��� ��¥�� ���� �÷��� �����ͷ� ����
            GameData.instance.recordData.dailyRecords[targetDate] = targetDateGamePlayData;

            print(ClearMonsterNum + "�� ���Ͱ� ���� �÷��� �����Ϳ� �߰��Ǿ����ϴ�.");

            // �÷��̾� ����ġ 1 ���� 
            GameData.instance.playerdata.PlayerExp += 1;
        }


        // ���ο� ������ �߰� ���� ��, �ݵ�� Load�� ���� ���־����
        // : ���� ������Ʈ�� �����͸� �����;��ϱ� ���� 

        // �����͸� ����
        GameData.instance.SavePlayerData();
        GameData.instance.SaveGamePlayData();
        GameData.instance.SaveMonsterData();

        // �����͸� �ҷ����� : ���� ������Ʈ �� �����͸� �������� ��.
        GameData.instance.LoadPlayerData();
        GameData.instance.LoadGamePlayData();
        GameData.instance.LoadMonsterData();
    }
}

