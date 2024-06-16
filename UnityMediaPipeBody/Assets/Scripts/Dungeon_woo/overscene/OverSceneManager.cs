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
        // 종합 점수 넘겨받기
        score = PlayerPrefs.GetInt("ScoreToSend", score);
        Debug.Log("score : " + score);

        // 종료 루틴 호출
        StartCoroutine(ShutdownRoutine(score));
    }

    // 종료 루틴
    IEnumerator ShutdownRoutine(int score)
    {
        // 스모크 내려주기


        // 평가 알려주기
        yield return StartCoroutine(OverallAssessment(score));

        // 마무리
        if (score >= 300)
        {
            // 몬스터 수집 o
            StartCoroutine(Failure());  // 시간 부족으로 일단 그냥 메인화면으로 넘어가게 설정(몬스터 수집 구현 X)
        }
        else
        {
            // 몬스터 수집 X   
            StartCoroutine(Failure());
        }
    }


    // 평가 알려주기
    IEnumerator OverallAssessment(int score)
    {
        score = 450;    // test
        label.text = score.ToString() + "/ 600 점!";
        Debug.Log("최종 평가 알려주기 라벨 셋팅 완료");

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
        Debug.Log("최종 평가 알려주기 별 셋팅 완료");

        yield return new WaitForSeconds(4);

        // 별이랑 라벨 없애기
        label.text = score.ToString() + "";
        for (int i=0; i<stars.Length; i++)
        {
            Destroy(stars[i]);
        }
        Debug.Log("별과 라벨 없애기 완료");
    }

    // 몬스터 수집
    IEnumerator Success()
    {

        SceneManager.LoadScene("MainScene");
        yield return new WaitForSeconds(0);
    }

    IEnumerator Failure()
    {
        label.text = "메인 화면으로 이동합니다";
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("MainScene");
    }
}

