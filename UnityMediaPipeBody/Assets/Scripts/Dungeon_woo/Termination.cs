using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Termination : MonoBehaviour
{
    public GameObject overallScene; // 씬 관리 오브젝트
    private int score = 0;
    //SceneManager.LoadScene("MainScene");
    void start()
    {
        // 스코어 넘겨받기
        
        // 평가 UI 출력
        overallAssessment(score);

        if(score >= 300)
        {
            // 몬스터 수집에 성공
            
        }
        else
        {
            // 몬스터 수집 실패
        }

        // 메인화면으로 이동
        
    }

    // 평가 UI
    IEnumerator overallAssessment(float score)
    {
        UiManager.Instance.UpdateModeratorLabel(score.ToString() + "점!");

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


