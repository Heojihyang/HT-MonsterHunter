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


    // 버튼 클릭 시 : 운동설명 창 활성화
    public void ActExerciseExplain()
    {
        Explain.SetActive(true);
        
    }

    // 운동 설명 창에서 뒤로가기 클릭 시 : 운동설명 창 비활성화
    public void InActExerciseExplain()
    {
        Explain.SetActive(false);

        // 켜져있던 몬스터 off 
        // Blob.SetActive(true);
    }

    // (시작) 클릭 시 : 던전으로 이동
    public void ChangeTDungeonScene()
    {
        SoundManager.instance.StopBGM("BGM_Main");      // BGM 끄기
        PlayerPrefs.SetInt("MonsterNumberToSend", 0);   // 씬 로드하면서 몬스터 번호 같이 넘겨주기
        SceneManager.LoadScene("TDungeonScene");
    }

    // (시작) 클릭 시 : 메인 화면으로 이동
    public void ChangeMainScene()
    {
        SceneManager.LoadScene("TMainScene");
    }
}
