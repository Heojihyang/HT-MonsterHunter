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
    
    public GameObject dungeonScene;       // 씬 관리 오브젝트
    private int count;                    // 코루틴을 위한 count 변수
    public int score;                     // 플레이어 동작 평가 점수

    /*
    //몸(22개) 랜드마크 Index
    LEFT_SHOULDER = 0, RIGHT_SHOULDER = 1, LEFT_ELBOW = 2, RIGHT_ELBOW = 3,
    LEFT_WRIST = 4, RIGHT_WRIST = 5, LEFT_PINKY = 6, RIGHT_PINKY = 7,
    LEFT_INDEX = 8, RIGHT_INDEX = 9, LEFT_THUMB = 10, RIGHT_THUMB = 11,
    LEFT_HIP = 12, RIGHT_HIP = 13, LEFT_KNEE = 14, RIGHT_KNEE = 15,
    LEFT_ANKLE = 16, RIGHT_ANKLE = 17, LEFT_HEEL = 18, RIGHT_HEEL = 19,
    LEFT_FOOT_INDEX = 20, RIGHT_FOOT_INDEX = 21,
     */

    /*
    // 공격 방법
    float n = angleCal.GetAngle(playerLandmark[12], playerLandmark[13], playerLandmark[17], playerLandmark[13]);
    if (n >= 120)
    {
        Attack();
        Debug.Log("공격!!!!!");
    }
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

    // 몬스터 공격()
    private void Attack()
    {
        monster.GetComponent<MonsterController>().TakeDamage(damage);
    }


    // ★허벅지 운동 루틴★
    IEnumerator RunThighRoutine()
    {
        Debug.Log("허벅지 코루틴이 실행되었습니다.");
        Debug.Log("5초 뒤, 운동을 시작합니다.");
        for(int i = 5; i > 0 ; i--)
        {
            Debug.Log(i + "초");
            yield return new WaitForSeconds(1);
        }
        

        // 스탠딩 사이드 레그 레이즈
        count = 12;
        yield return StartCoroutine(R_StandingSideLegRaise());
        yield return StartCoroutine(L_StandingSideLegRaise());
        yield return new WaitForSeconds(5);

        count = 15;
        yield return StartCoroutine(R_StandingSideLegRaise());
        yield return StartCoroutine(L_StandingSideLegRaise());
        yield return new WaitForSeconds(10);

        // 스쿼트
        count = 20;
        yield return StartCoroutine(Squat());
        yield return new WaitForSeconds(10);

        // 런지
        count = 20;
        yield return StartCoroutine(Lunge());
        yield return new WaitForSeconds(5);
        yield return StartCoroutine(Lunge());

        Debug.Log("허벅지 운동을 종료합니다.");
    }

    // 허벅지-스탠딩사이드레그레이즈(우)
    IEnumerator R_StandingSideLegRaise()
    {
        // 1. LEFT_HIP = 12  RIGHT_HIP = 13   RIGHT_ANKLE = 17
        // 2. RIGHT_HIP = 13   RIGHT_KNEE = 15   RIGHT_ANKLE = 17
        for (int i = 0; i < count; i++)
        {
            //playerLandmark가 Null임 -> 코루틴 실행시 5초정도 기다리니 해결됨
            float angle1 = GetComponent<AngleCalculator>().GetAngle(playerLandmark[13], playerLandmark[12], playerLandmark[16], playerLandmark[12]);
            float angle2 = GetComponent<AngleCalculator>().GetAngle(playerLandmark[13], playerLandmark[15], playerLandmark[17], playerLandmark[15]);
            int grade = 0;

            Debug.Log("스탠딩 사이드 레그레이즈(우) " + (i + 1) + "회");

            // 평가 1번 각도
            if (angle1 >= 150 ) { grade += 5;  }
            else if(angle1 >= 135) { grade += 3; }
            else if(angle1 >= 120) { grade += 1; }

            // 평가 2번 각도
            if (angle2 >= 180) { grade += 5; }
            else if (angle2 >= 170) { grade += 3; }
            else if (angle2 >= 165) { grade += 1; }

            // 동작 종합 평가-> 몬스터 수집요건 score 업데이트/ UI업데이트/ score업데이트/ 총알 발사
            if (grade >= 10) 
            { 
                Debug.Log("Excellent!");
                score += 5;
            }
            else if (grade >= 6) 
            { 
                Debug.Log("Very Good!");
                score += 3;
            }
            else if(grade >= 2)
            {
                Debug.Log("Good!");
                score += 1;
            }
            else
            {
                Debug.Log("최악");
            }

            Debug.Log("Grade : " + grade);
            Debug.Log("현재까지의 Score : " + score);
            yield return new WaitForSeconds(3); //3초에 한번씩 동작진행
        }
        yield return new WaitForSeconds(0);
    }

    // 허벅지-스탠딩사이드레그레이즈(좌)
    IEnumerator L_StandingSideLegRaise()
    {
        // 1. RIGHT_HIP = 13   LEFT_HIP = 12   LEFT_ANKLE = 16
        // 2. LEFT_HIP = 12   LEFT_KNEE = 14   LEFT_ANKLE = 16
        for (int i = 0; i < count; i++)
        {
            float angle1 = GetComponent<AngleCalculator>().GetAngle(playerLandmark[13], playerLandmark[12], playerLandmark[16], playerLandmark[12]);
            float angle2 = GetComponent<AngleCalculator>().GetAngle(playerLandmark[12], playerLandmark[14], playerLandmark[16], playerLandmark[14]);
            int grade = 0;

            Debug.Log("스탠딩 사이드 레그레이즈(좌) " + (i + 1) + "회");

            // 평가 1번
            if (angle1 >= 150) { grade += 5; }
            else if (angle1 >= 135) { grade += 3; }
            else if (angle1 >= 120) { grade += 1; }

            // 평가 2번
            if (angle2 >= 180) { grade += 5; }
            else if (angle2 >= 170) { grade += 3; }
            else if (angle2 >= 165) { grade += 1; }

            // 동작 평가
            if (grade >= 10)
            {
                Debug.Log("Excellent!");
                score += 5;
            }
            else if (grade >= 6)
            {
                Debug.Log("Very Good!");
                score += 3;
            }
            else if (grade >= 2)
            {
                Debug.Log("Good!");
                score += 1;
            }
            else
            {
                Debug.Log("최악");
            }

            Debug.Log("Grade : " + grade);
            Debug.Log("현재까지의 Score : " + score);
            yield return new WaitForSeconds(3);
        }
        yield return new WaitForSeconds(0);
    }

    // 허벅지-스쿼트
    IEnumerator Squat()
    {
        for (int i = 0; i < count; i++)
        {

        }
        yield return new WaitForSeconds(0);
    }

    // 허벅지-런지
    IEnumerator Lunge()
    {
        for (int i = 0; i < count; i++)
        {

        }
        yield return new WaitForSeconds(0);
    }

    private void Start()
    {
        score = 0;
        // ★던전 번호 넘겨받고 해당 루틴 실행하기★
        int dunNum = dungeonScene.GetComponent<TDungeonSceneManager>().receivedMonsterNumber;

        switch (dunNum)
        {
            case 0:
                Debug.Log("넘겨받은 던전 번호에 따라 허벅지 루틴을 호출합니다. - PlayerAssessment");
                StartCoroutine(RunThighRoutine());          // 허벅지 루틴 코루틴 호출

                break;
            default:
                // StartCoroutine(RunThighRoutine());
                break;
        }


        // 스페이스바를 눌렀을 때 공격 - 공격 로직 테스트
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
        */
    }

    private void Update()
    {

    }
}