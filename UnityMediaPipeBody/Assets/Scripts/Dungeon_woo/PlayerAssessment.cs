using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAssessment : MonoBehaviour
{
    // 몬스터
    public GameObject monster;
    public float damage = 7.5f;

    // 플레이어
    public GameObject[] playerLandmark = new GameObject[PLAYER_LANDMARK_COUNT];         // 플레이어 몸 랜드마크
    public GameObject head;                                                             // 플레이어 머리
    private Vector3[] playerLandmarkPosition = new Vector3[PLAYER_LANDMARK_COUNT];      // 플레이어 몸 랜드마크 포지션
    private Vector3 headLandmarkPosition = new Vector3(0, 0, 0);                        // 플레이어 머리 포지션
    const int PLAYER_LANDMARK_COUNT = 22;                                               // 플레이어 랜드마크 수

    /*
    //몸(22개) 랜드마크 Index
    LEFT_SHOULDER = 0, RIGHT_SHOULDER = 1, LEFT_ELBOW = 2, RIGHT_ELBOW = 3,
    LEFT_WRIST = 4, RIGHT_WRIST = 5, LEFT_PINKY = 6, RIGHT_PINKY = 7,
    LEFT_INDEX = 8, RIGHT_INDEX = 9, LEFT_THUMB = 10, RIGHT_THUMB = 11,
    LEFT_HIP = 12, RIGHT_HIP = 13, LEFT_KNEE = 14, RIGHT_KNEE = 15,
    LEFT_ANKLE = 16, RIGHT_ANKLE = 17, LEFT_HEEL = 18, RIGHT_HEEL = 19,
    LEFT_FOOT_INDEX = 20, RIGHT_FOOT_INDEX = 21,
     */

    // 현재 랜드마크 가져오기()
    public void getPlayerLandmark(GameObject[] landmark, GameObject head)
    {
        for (int i = 11; i < landmark.Length; ++i)
        {
            playerLandmark[i-11] = landmark[i];
        }
        this.head = head;
    }

    // 플레이어 랜드마크의 포지션 구하기()
    public void getPlayerLandmarkPosition(GameObject[] landmark, GameObject head)
    {
        for (int i = 0; i < PLAYER_LANDMARK_COUNT; ++i)
        {
            playerLandmarkPosition[i] = landmark[i].transform.localPosition;
        }
        headLandmarkPosition = head.transform.position;
    }


    // 두 벡터간의 각도 계산() 0도에서 180도 사이의 값
    public float GetAngle(GameObject referenceFrom, GameObject referenceTo, GameObject from, GameObject to)
    {
        Vector3 referenceVector = referenceTo.transform.position - referenceFrom.transform.position;
        Vector3 directionVector = to.transform.position - from.transform.position;
        Debug.Log("referenceVector : " + referenceVector);
        Debug.Log("directionVector : " + directionVector);

        referenceVector.Normalize();
        directionVector.Normalize();
        Debug.Log("referenceVector Normalize : " + referenceVector);
        Debug.Log("directionVector Normalize : " + referenceVector);

        float angle = Vector3.Angle(referenceVector, directionVector);
        Debug.Log("angle : " + angle);

        return angle;
    }

    /*
    // 두 벡터간의 부호있는 각도 계산() -180도에서 180
    public float GetSignedAngle(GameObject referenceFrom, GameObject referenceTo, GameObject from, GameObject to)
    {
        Vector3 referenceVector = referenceTo.transform.position - referenceFrom.transform.position;
        Vector3 directionVector = to.transform.position - from.transform.position;
        Debug.Log("referenceVector : " + referenceVector);
        Debug.Log("directionVector : " + directionVector);

        referenceVector.Normalize();
        referenceVector.Normalize();
        Debug.Log("referenceVector Normalize : " + referenceVector);
        Debug.Log("directionVector Normalize : " + referenceVector);

        Vector3 crossProduct = Vector3.Cross(referenceVector, directionVector);
        Debug.Log("crossProduct : " + crossProduct);

        float signedAngle = Vector3.SignedAngle(referenceVector, directionVector, crossProduct);
        Debug.Log("signedAngle : " + signedAngle);

        return signedAngle;
    }
    */

    // 몬스터 공격()
    private void Attack()
    {
        monster.GetComponent<MonsterController>().TakeDamage(damage);
    }


    private void Update()
    {
        // (임시)스페이스바를 눌렀을 때 공격
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
        */

        // 스탠딩사이드레그레이즈 공격
        float n = GetAngle(playerLandmark[12], playerLandmark[13], playerLandmark[17], playerLandmark[13]);
        if (n >= 120)
        {
            Attack();
            Debug.Log("공격!!!!!");
        }
    }
}