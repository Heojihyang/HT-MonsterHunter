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

    // (허벅지) 클릭 시 : 운동설명 창 활성화
    public void ActExerciseExplain()
    {
        Explain.SetActive(true);
    }

    // (시작) 클릭 시 : 던전으로 이동
    public void ChangeTDungeonScene()
    {
        SceneManager.LoadScene("TDungeonScene");    
        PlayerPrefs.SetInt("MonsterNumberToSend", 0);   //씬 로드하면서 몬스터 번호 같이 넘겨주기
    }
}
