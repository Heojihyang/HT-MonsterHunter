using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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

    // (���Ϻ���) Ŭ�� �� : 

    // (����) Ŭ�� �� : TMonsterListScene���� �̵�
    public void ChangeTMonsterListScene()
    {
        SceneManager.LoadScene("TMonsterListScene");
    }
}