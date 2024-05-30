using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // ���� ������ �߰�: ���� ��¥, �÷��� Ƚ�� 1, �÷��� �ð� 1.5 �ð�, Ŭ������ ���� "Game1", ������ ���� "MonsterA"
        // GameData.instance.AddGamePlayData(DateTime.Today, 1, 1.5f, "Game1", "MonsterA");

        // ���� ������ �ε� ���ֱ� !! 
        GameData.instance.LoadMonsterData();
        GameData.instance.LoadPlayerData();
        // GameData.instance.LoadGamePlayData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
