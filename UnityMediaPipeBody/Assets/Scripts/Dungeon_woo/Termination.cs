using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Termination : MonoBehaviour
{
    public GameObject overallScene; // �� ���� ������Ʈ
    private int score = 0;
    //SceneManager.LoadScene("MainScene");
    void start()
    {
        // ���ھ� �Ѱܹޱ�
        
        // �� UI ���
        overallAssessment(score);

        if(score >= 300)
        {
            // ���� ������ ����
            
        }
        else
        {
            // ���� ���� ����
        }

        // ����ȭ������ �̵�
        
    }

    // �� UI
    IEnumerator overallAssessment(float score)
    {
        UiManager.Instance.UpdateModeratorLabel(score.ToString() + "��!");

        if (score >= 540 && score <= 600)
        {
            
        }
        else if (score >= 420 && score <= 539)
        {

        }
        else if (score >= 300 && score <= 419)
        {

        }
        else if (score >= 180 && score <= 299)
        {

        }
        else if (score >= 60 && score <= 179)
        {

        }
        else
        {

        }

        yield return new WaitForSeconds(4);
    }
}


