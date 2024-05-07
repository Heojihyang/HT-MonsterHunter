using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Slider ExpBar;

    // Start is called before the first frame update
    void Start()
    {
        // �÷��̾� ������ �ε�
        // GameData.instance.LoadPlayerData();

        // --- �÷��̾� ������ ���� ---
        // 1. �÷��̾� ExpBar : playerdata�� ����ġ �����ͼ� �����̴� ����
        // 2. �÷��̾� ȣĪ(��Ż���/�︰��/��û��/��â/�ｺ�� ��) �־��ֱ� 

        // 1. �÷��̾� ExpBar
        int exp = GameData.instance.playerdata.PlayerExp;
        if(exp < 50)
        {
            float expbar = exp % 10.0f; // 10���γ��� �������� Expbar�� �������� �ȴ�. 
            ExpBar.value = expbar;
            print("������ ��: " + expbar);
        }
        else
        {

            print("����� �����Դϴ�.");
        }
        

        // 2. �÷��̾� ȣĪ
        float title = exp / 10.0f; // 10���γ��� �� ���� ���� Īȣ ����
        print("���� ��: " + title);

        if(title < 1) // ��Ż���
        {
            print("��Ż���");
        }
        else if(title < 2) // �︰��
        {
            print("�︰��");
        }
        else if (title < 3) // ��û��
        {
            print("��û��");
        }
        else if (title < 4) // ��â
        {
            print("��â");
        }
        else if (title >= 5) // �ｺ�� ��
        {
            print("�ｺ�� ��");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // (��ϱ�) Ŭ�� �� : SelectScene���� �̵� 
    public void ChangeSelectScene()
    {
        SceneManager.LoadScene("SelectScene");
    }

    // (���Ϻ���) Ŭ�� �� : 

    // (����) Ŭ�� �� : TMonsterListScene���� �̵�
    public void ChangeTMonsterListScene()
    {
        SceneManager.LoadScene("TMonsterListScene");
    }
}
