using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectManager : MonoBehaviour
{
    public GameObject previousMonster;
    public GameObject ExplainButton;
    public GameObject Explain;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // �˸��� ���� �̹��� Ȱ��ȭ & � ���� ��ư Ȱ��ȭ 
    public void ActBlob(GameObject newMonster)
    {
        // ���� ���� ��Ȱ��ȭ
        if (previousMonster != null)
            previousMonster.SetActive(false);

        // ���ο� ���� Ȱ��ȭ 
        newMonster.SetActive(true);

        ExplainButton.SetActive(true);

        // ���� ���� ������Ʈ
        previousMonster = newMonster;
    }

    // � ��ư Ŭ�� �� : ����� â Ȱ��ȭ
    public void ActExerciseExplain()
    {
        Explain.SetActive(true);
    }

    // � ���� â���� �ڷΰ��� Ŭ�� �� : ����� â ��Ȱ��ȭ
    public void InActExerciseExplain()
    {
        Explain.SetActive(false);

        // �����ִ� ���Ϳ� ���� ��ư ��Ȱ��ȭ ��Ű��

        // ���� ���Ϳ� ���� ��ư ��Ȱ��ȭ
        if (previousMonster != null)
            previousMonster.SetActive(false);
        ExplainButton.SetActive(false);
    }

    // (����) Ŭ�� �� : �������� �̵�
    public void ChangeTDungeonScene()
    {
        SoundManager.instance.StopBGM("BGM_Main");      // BGM ����
        PlayerPrefs.SetInt("MonsterNumberToSend", 0);   // �� �ε��ϸ鼭 ���� ��ȣ ���� �Ѱ��ֱ�
        SceneManager.LoadScene("TDungeonScene");
    }

    // (����) Ŭ�� �� : ���� ȭ������ �̵�
    public void ChangeMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
