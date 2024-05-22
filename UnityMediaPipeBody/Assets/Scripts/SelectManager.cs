using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectManager : MonoBehaviour
{
    public GameObject Explain;
    // public GameObject Blob;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActBlob()
    {
        // Blob.SetActive(false);
    }


    // ��ư Ŭ�� �� : ����� â Ȱ��ȭ
    public void ActExerciseExplain()
    {
        Explain.SetActive(true);
        
    }

    // � ���� â���� �ڷΰ��� Ŭ�� �� : ����� â ��Ȱ��ȭ
    public void InActExerciseExplain()
    {
        Explain.SetActive(false);

        // �����ִ� ���� off 
        // Blob.SetActive(true);
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
        SceneManager.LoadScene("TMainScene");
    }
}
