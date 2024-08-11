using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectManager : MonoBehaviour
{
    public GameObject previousMonster;
    public GameObject ExplainButton;
    public GameObject Explain_LeftLeg1;    // ����� ����â
    public GameObject Explain_RightArm1;   // ��� ����â
    public GameObject Explain_etc;         // �� �� ���� ����â 

    public GameObject LeftLeg1;
    public GameObject RightArm1;


    // �˸��� ���� �̹��� Ȱ��ȭ & � ���� ��ư Ȱ��ȭ 
    public void ActBlob(GameObject newMonster)
    {
         
        if (previousMonster != null)           
        {
            previousMonster.SetActive(false);  // ���� ���� ��Ȱ��ȭ
        }
            
        newMonster.SetActive(true);            // ���ο� ���� Ȱ��ȭ 

        ExplainButton.SetActive(true);

        previousMonster = newMonster;          // ���� ���� ������Ʈ
    }



    // � ��ư Ŭ�� �� : ���� ������ �˸°� ����â ����
    public void ActExerciseExplain()
    {
        // ( ����� / ��� / �� �� )

        if(LeftLeg1.activeSelf)
        {
            Explain_LeftLeg1.SetActive(true);
        }
        else if(RightArm1.activeSelf)
        {
            Explain_RightArm1.SetActive(true);
        }
        else
        {
            Explain_etc.SetActive(true);
        }

    }



    // � ���� â���� �ڷΰ��� Ŭ�� �� 
    public void InActExerciseExplain()
    {
        // ����� â ��Ȱ��ȭ ( ����� / ��� / �� �� )

        if (Explain_LeftLeg1.activeSelf)
        {
            Explain_LeftLeg1.SetActive(false);
        }
        else if (Explain_RightArm1.activeSelf)
        {
            Explain_RightArm1.SetActive(false);
        }
        else
        {
            Explain_etc.SetActive(false);
        }


        // �����ִ� ���Ϳ� ���� ��ư ��Ȱ��ȭ ��Ű��
        if (previousMonster != null)
        {
            previousMonster.SetActive(false);    
        }
            
        ExplainButton.SetActive(false);
    }




    // (����) Ŭ�� �� : �������� �̵�
    public void ChangeTDungeonScene()
    {
        if (Explain_LeftLeg1.activeSelf)        // ����� ���� : 8��
        {
            print("����� �������� �̵��մϴ�.");

            PlayerPrefs.SetInt("MonsterNumberToSend", 8);   // �� �ε��ϸ鼭 ���� ��ȣ ���� �Ѱ��ֱ�

        }
        else if (Explain_RightArm1.activeSelf)  // ��� ���� : 6��
        {
            print("��� �������� �̵��մϴ�.");

            PlayerPrefs.SetInt("MonsterNumberToSend", 6);   

        }

        SoundManager.instance.StopBGM("BGM_Main");      // BGM ����
        SceneManager.LoadScene("TDungeonScene");
    }


    // (�ڷΰ���) Ŭ�� �� : ���� ȭ������ �̵�
    public void ChangeMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
