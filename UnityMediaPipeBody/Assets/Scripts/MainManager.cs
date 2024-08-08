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
    public Image LevelImage;
    public Sprite[] LevelImg = new Sprite[5];
    

    void Start()
    {
        // BGM strat
        SoundManager.instance.PlayBGM("BGM_Main");


        //// --- �÷��̾� ������ ���� ---
        // 1. �÷��̾� ExpBar : playerdata�� ����ġ �����ͼ� �����̴� ����
        // 2. �÷��̾� ȣĪ(��Ż���/�︰��/��û��/��â/�ｺ�� ��) �־��ֱ� 


        // 1. �÷��̾� ExpBar

        int exp = GameData.instance.playerdata.PlayerExp;
        float expbar;

        ExpBar.interactable = false;              // ����� ���� ��Ȱ��ȭ

        if (exp < 50)
        {
            expbar = exp % 10.0f;                // 10���γ��� �������� Expbar�� �������� �ȴ�. 
            ExpBar.value = expbar /10.0f;
        }
        else                                     // ������ ��� : ������ Ǯ�� ä��� 
        {
            ExpBar.value = 1.0f;
            print("����� �����Դϴ�.");
        }



        // 2. �÷��̾� ȣĪ

        float title = exp / 10.0f;       // 10���γ��� �� ���� ���� Īȣ ����

        int remain = exp % 10;           // ������ 
        int n = 10 - remain;             // ������ ���� ���� Ƚ�� n


        #region �÷��̾� ����ġ�� ���� Īȣ �ο� 

        if (title < 1)
        {
            PlayerTitle.text = "��Ż���";
            PlayerGoal.text = $"������ �︰�̱��� {n} ����";  // ���ڿ� ���� ==> $"����� ������ ���ϸ�: {apple + grape}"
            LevelImage.sprite = LevelImg[0];
        }
        else if (title < 2)
        {
            PlayerTitle.text = "�︰��";
            PlayerGoal.text = $"������ ��û����� {n} ����";
            LevelImage.sprite = LevelImg[1];
        }
        else if (title < 3)
        {
            PlayerTitle.text = "��û��";
            PlayerGoal.text = $"������ ��â���� {n} ����";
            LevelImage.sprite = LevelImg[2];

        }
        else if (title < 4)
        {
            PlayerTitle.text = "��â";
            PlayerGoal.text = $"������ �ｺ�� �ű��� {n} ����";
            LevelImage.sprite = LevelImg[3];
        }
        else if (title >= 4)
        {
            PlayerTitle.text = "�ｺ�� ��";
            PlayerGoal.text = "����� ���� �Դϴ� :)";
            LevelImage.sprite = LevelImg[4];
        }
        else
        {
            PlayerTitle.text = "����~ ><";
            PlayerGoal.text = "����������������";
        }

        #endregion


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
