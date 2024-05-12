using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 몬스터 관련
    public GameObject monster;
    public float damage = 7.5f;

    // 플레이어 랜드마크 위치 (PipeServer의 정보를 담을 곳)
    private Vector3[] PlayerLandmarkPosition = new Vector3[33];
    private Vector3 HeadLandmarkPosition = new Vector3(0, 0, 0);


    private void Update()
    {
        // (임시)스페이스바를 눌렀을 때 공격하는 동작을 감지
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    // 몬스터 공격 함수
    private void Attack()
    {
        monster.GetComponent<MonsterController>().TakeDamage(damage);
    }

    // 현재 랜드마크 위치 가져오기(파이프서버에서 프레임 단위로 호출, 갱신)
    public void getLandmarkPosition(GameObject[] landmark, GameObject head)
    {
        //Debug.Log("landmark[11] 좌표 : " + landmark[11].transform.position);
        /*
        // 몸
        for (int i = 0; i < LANDMARK_COUNT; ++i)
        {
            PlayerLandmarkPosition[i] = landmark[i].transform.localPosition;
        }
        // 머리
        HeadLandmarkPosition = head.transform.position;
        */
    }

    // 정답 좌표 가져오기
    private void getCorrectLandmarkPosition()
    {

    }

    // 정답 판정
    private void motionEquality()
    {

    }

}