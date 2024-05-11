using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Text PlayerTitle;
    public Slider ExpBar;
    public Text PlayerGoal;
    
    // Start is called before the first frame update
    void Start()
    {
        // --- �÷��̾� ������ ���� ---
        // 1. �÷��̾� ExpBar : playerdata�� ����ġ �����ͼ� �����̴� ����
        // 2. �÷��̾� ȣĪ(��Ż���/�︰��/��û��/��â/�ｺ�� ��) �־��ֱ� 

        //GameData.instance.playerdata.PlayerExp = 9;
        //GameData.instance.SavePlayerData();

        // 1. �÷��̾� ExpBar
        int exp = GameData.instance.playerdata.PlayerExp;
        float expbar;
        if (exp < 50)
        {
            expbar = exp % 10.0f; // 10���γ��� �������� Expbar�� �������� �ȴ�. 
            ExpBar.value = expbar/10.0f;
        }
        else // ������ ��� 
        {
            ExpBar.value = 1.0f; // ������ Ǯ�� ä��� 
            print("����� �����Դϴ�.");
        }


        // 2. �÷��̾� ȣĪ
        float title = exp / 10.0f; // 10���γ��� �� ���� ���� Īȣ ����
        // 3. ������ ���� ���� Ƚ��
        int remain = exp % 10; // ������ 
        int n = 10 - remain; // ���� Ƚ�� n

        if (title < 1)
        {
            PlayerTitle.text = "��Ż���";
            PlayerGoal.text = $"������ �︰�̱��� {n} ����"; // ���ڿ� ���� ==> $"����� ������ ���ϸ�: {apple + grape}"
        }
        else if (title < 2)
        {
            PlayerTitle.text = "�︰��";
            PlayerGoal.text = $"������ ��û����� {n} ����";
        }
        else if (title < 3)
        {
            PlayerTitle.text = "��û��";
            PlayerGoal.text = $"������ ��â���� {n} ����";
        }
        else if (title < 4)
        {
            PlayerTitle.text = "��â";
            PlayerGoal.text = $"������ �ｺ�� �ű��� {n} ����";
        }
        else if (title >= 4)
        {
            PlayerTitle.text = "�ｺ�� ��";
            PlayerGoal.text = "����� ���� �Դϴ� :)";
        }
        else
        {
            PlayerTitle.text = "��������~ ><";
            PlayerGoal.text = "ȭ���� ���� �� �ذ��� ����r^^*";
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

    // (���Ϻ���) Ŭ�� �� : TRecordScene���� �̵�
    public void ChangeTRecordScene()
    {
        SceneManager.LoadScene("TRecordScene");
    }

    // (����) Ŭ�� �� : TMonsterListScene���� �̵�
    public void ChangeTMonsterListScene()
    {
        SceneManager.LoadScene("TMonsterListScene");
    }
}
