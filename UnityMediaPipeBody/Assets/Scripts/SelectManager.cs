using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectManager : MonoBehaviour
{
    public GameObject Explain;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // (�����) Ŭ�� �� : ����� â Ȱ��ȭ
    public void ActExerciseExplain()
    {
        Explain.SetActive(true);
    }

    // (�����) Ŭ�� �� : ����� â Ȱ��ȭ
    public void ChangeTDungeonScene()
    {
        SceneManager.LoadScene("TDungeonScene");
    }
}