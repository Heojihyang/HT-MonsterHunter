using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    PlayerData playerdata = new PlayerData();

    // Start is called before the first frame update
    void Start()
    {
        // playerdata의 경험치 가져와서
        // 프로필의 경험치 바 & 플레이어 호칭(헬린이) 넣어주기

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // (운동하기) 클릭 시 : SelectScene으로 이동 
    public void ChangeSelectScene()
    {
        SceneManager.LoadScene("SelectScene");
    }

    // (운동기록보기) 클릭 시 : 

    // (도감) 클릭 시 : TMonsterListScene으로 이동
    public void ChangeTMonsterListScene()
    {
        SceneManager.LoadScene("TMonsterListScene");
    }
}
