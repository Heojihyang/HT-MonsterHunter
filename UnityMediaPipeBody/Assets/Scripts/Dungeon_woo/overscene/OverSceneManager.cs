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
    {
        // ����ũ �����ֱ�


        // �� �˷��ֱ�
        yield return StartCoroutine(OverallAssessment(score));

        // ������
        if (score >= 300)
        {
            // ���� ���� o
            StartCoroutine(Failure());  // �ð� �������� �ϴ� �׳� ����ȭ������ �Ѿ�� ����(���� ���� ���� X)
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
        score = 450;    // test
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
        label.text = score.ToString() + "";
        for (int i=0; i<stars.Length; i++)
        {
            Destroy(stars[i]);
        }
        Debug.Log("���� �� ���ֱ� �Ϸ�");
    }

    // ���� ����
    IEnumerator Success()
    {

        SceneManager.LoadScene("MainScene");
        yield return new WaitForSeconds(0);
    }

    IEnumerator Failure()
    {
        label.text = "���� ȭ������ �̵��մϴ�";
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("MainScene");
    }
}

