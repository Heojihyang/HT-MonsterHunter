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
        // ���� ���� �κ� �ڵ� (ó�� ������ ���� ������ �� �ʿ��� ����)
        // GameData.instance.SavePlayerData();
        // GameData.instance.SaveGamePlayData();
        // GameData.instance.SaveMonsterData();
        // �� 3��(Save�Լ���) �ּ� �����ؼ� �ѹ� �÷��� ��Ű�� �ٽ� �ּ�ó�� �ʼ�����

        // --------------------------------------------------------------------------
        

        // ���� ������ �ε� ���ֱ� !! 
        GameData.instance.LoadMonsterData();
        GameData.instance.LoadPlayerData();
        GameData.instance.LoadGamePlayData();  
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
