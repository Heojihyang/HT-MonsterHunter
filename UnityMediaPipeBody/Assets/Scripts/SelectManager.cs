using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectManager : MonoBehaviour
{
    public GameObject previousMonster;
    public GameObject ExplainButton;
    public GameObject Explain_LeftLeg1;    // 허벅지 설명창
    public GameObject Explain_RightArm1;   // 삼두 설명창
    public GameObject Explain_etc;         // 그 외 부위 설명창 

    public GameObject LeftLeg1;
    public GameObject RightArm1;


    // 알맞은 몬스터 이미지 활성화 & 운동 설명 버튼 활성화 
    public void ActBlob(GameObject newMonster)
    {
         
        if (previousMonster != null)           
        {
            previousMonster.SetActive(false);  // 이전 몬스터 비활성화
        }
            
        newMonster.SetActive(true);            // 새로운 몬스터 활성화 

        ExplainButton.SetActive(true);

        previousMonster = newMonster;          // 이전 몬스터 업데이트
    }



    // 운동 버튼 클릭 시 : 몬스터 종류에 알맞게 설명창 띄우기
    public void ActExerciseExplain()
    {
        // ( 허벅지 / 삼두 / 그 외 )

        if(LeftLeg1.activeSelf)
        {
            Explain_LeftLeg1.SetActive(true);
        }
        else if(RightArm1.activeSelf)
        {
            Explain_RightArm1.SetActive(true);
        }
        else
        {
            Explain_etc.SetActive(true);
        }

    }



    // 운동 설명 창에서 뒤로가기 클릭 시 
    public void InActExerciseExplain()
    {
        // 운동설명 창 비활성화 ( 허벅지 / 삼두 / 그 외 )

        if (Explain_LeftLeg1.activeSelf)
        {
            Explain_LeftLeg1.SetActive(false);
        }
        else if (Explain_RightArm1.activeSelf)
        {
            Explain_RightArm1.SetActive(false);
        }
        else
        {
            Explain_etc.SetActive(false);
        }


        // 켜져있던 몬스터와 설명 버튼 비활성화 시키기
        if (previousMonster != null)
        {
            previousMonster.SetActive(false);    
        }
            
        ExplainButton.SetActive(false);
    }




    // (시작) 클릭 시 : 던전으로 이동
    public void ChangeTDungeonScene()
    {
        if (Explain_LeftLeg1.activeSelf)        // 허벅지 던전 : 8번
        {
            print("허벅지 던전으로 이동합니다.");

            PlayerPrefs.SetInt("MonsterNumberToSend", 8);   // 씬 로드하면서 몬스터 번호 같이 넘겨주기

        }
        else if (Explain_RightArm1.activeSelf)  // 삼두 던전 : 6번
        {
            print("삼두 던전으로 이동합니다.");

            PlayerPrefs.SetInt("MonsterNumberToSend", 6);   

        }

        SoundManager.instance.StopBGM("BGM_Main");      // BGM 끄기
        SceneManager.LoadScene("TDungeonScene");
    }


    // (뒤로가기) 클릭 시 : 메인 화면으로 이동
    public void ChangeMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
