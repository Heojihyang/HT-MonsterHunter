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
        UiManager.Instance.UpdateModeratorLabel("준비!");
        yield return new WaitForSeconds(2);

        // 1. 스탠딩 사이드 레그 레이즈
        // 1세트(우 12번, 좌 12번)
        UiManager.Instance.UpdateActionName("스탠딩 사이드 레그 레이즈(우)");
        UiManager.Instance.UpdateActionCount(0, 12);

        Debug.Log("5초 뒤, '스탠딩사이드레그레이즈 1세트'를 시작합니다.");
        UiManager.Instance.UpdateModeratorLabel("5초 뒤, '스탠딩 사이드 레그레이즈 1세트를 시작합니다");
        yield return new WaitForSeconds(2);
        for (int i = 5; i > 0; i--)
        {
            Debug.Log(i + "초");
            UiManager.Instance.UpdateModeratorLabel(i.ToString());
            yield return new WaitForSeconds(1);
        }

        count = 12;
        UiManager.Instance.UpdateModeratorLabel("");
        yield return StartCoroutine(R_StandingSideLegRaise());

        UiManager.Instance.UpdateActionName("스탠딩 사이드 레그 레이즈(좌)");
        UiManager.Instance.UpdateActionCount(0, 12);
        yield return StartCoroutine(L_StandingSideLegRaise());

          // 2세트(우 15번, 좌 15번)
        UiManager.Instance.UpdateActionName("스탠딩 사이드 레그 레이즈(우)");
        UiManager.Instance.UpdateActionCount(0, 15);

        Debug.Log("5초 뒤, '스탠딩사이드레그레이즈 2세트'를 시작합니다");
        UiManager.Instance.UpdateModeratorLabel("5초 뒤, '스탠딩사이드레그레이즈 2세트'를 시작합니다");
        for (int i = 5; i > 0; i--)
        {
            Debug.Log(i + "초");
            UiManager.Instance.UpdateModeratorLabel(i.ToString());
            yield return new WaitForSeconds(1);
        }
        count = 15;
        UiManager.Instance.UpdateModeratorLabel("");
        yield return StartCoroutine(R_StandingSideLegRaise());

        UiManager.Instance.UpdateActionName("스탠딩 사이드 레그 레이즈(좌)");
        UiManager.Instance.UpdateActionCount(0, 15);
        yield return StartCoroutine(L_StandingSideLegRaise());

        // 2. 스쿼트
        UiManager.Instance.UpdateActionName("스쿼트");
        UiManager.Instance.UpdateActionCount(0, 20);

        Debug.Log("10초 뒤, '스쿼트'를 시작합니다");
        UiManager.Instance.UpdateModeratorLabel("10초 뒤, '스쿼트'를 시작합니다");
        for (int i = 10; i > 0; i--)
        {
            Debug.Log(i + "초");
            UiManager.Instance.UpdateModeratorLabel(i.ToString());
            yield return new WaitForSeconds(1);
        }
        count = 20;
        yield return StartCoroutine(Squat());

        // 3. 런지
          // 1세트 (좌우 20번)
        UiManager.Instance.UpdateActionName("런지");
        UiManager.Instance.UpdateActionCount(0, 20);

        Debug.Log("10초 뒤, '런지 1세트'를 시작합니다.");
        UiManager.Instance.UpdateModeratorLabel("10초 뒤, '런지 1세트'를 시작합니다");
        for (int i = 10; i > 0; i--)
        {
            Debug.Log(i + "초");
            UiManager.Instance.UpdateModeratorLabel(i.ToString());
            yield return new WaitForSeconds(1);
        }
        count = 20;
        yield return StartCoroutine(Lunge());

          // 2세트 (좌우 20번)
        UiManager.Instance.UpdateActionCount(0, 20);

        Debug.Log("5초 뒤, '런지 2세트'를 시작합니다.");
        UiManager.Instance.UpdateModeratorLabel("5초 뒤, '런지 2세트'를 시작합니다");
        for (int i = 5; i > 0; i--)
        {
            Debug.Log(i + "초");
            UiManager.Instance.UpdateModeratorLabel(i.ToString());
            yield return new WaitForSeconds(1);
        }
        count = 20;
        yield return StartCoroutine(Lunge());

        UiManager.Instance.UpdateModeratorLabel("종료!");
        yield return new WaitForSeconds(3);
        UiManager.Instance.UpdateModeratorLabel("평가중 ~ ");

        Debug.Log("허벅지 코루틴을 종료합니다.");
    }

    // 허벅지-스탠딩사이드레그레이즈(우)
    IEnumerator R_StandingSideLegRaise()
    {
        // 1. LEFT_HIP = 12  RIGHT_HIP = 13   RIGHT_ANKLE = 17
        // 2. RIGHT_HIP = 13   RIGHT_KNEE = 15   RIGHT_ANKLE = 17
        int grade = 0;
        for (int i = 0; i < count; i++)
        {
            UiManager.Instance.UpdateActionCount(i+1, count);

            //playerLandmark가 Null임 -> 코루틴 실행시 5초정도 기다리니 해결됨
            float angle1 = GetComponent<AngleCalculator>().GetAngle(playerLandmark[13], playerLandmark[12], playerLandmark[16], playerLandmark[12]);
            float angle2 = GetComponent<AngleCalculator>().GetAngle(playerLandmark[13], playerLandmark[15], playerLandmark[17], playerLandmark[15]);
            grade = 0;

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
                UiManager.Instance.UpdateAdviceLabel("완벽해요!");
                score += 5;
            }
            else if (grade >= 6) 
            { 
                Debug.Log("Very Good!");
                UiManager.Instance.UpdateAdviceLabel("아주 좋아요!");
                score += 3;
            }
            else if(grade >= 2)
            {
                Debug.Log("Good");
                UiManager.Instance.UpdateAdviceLabel("좋아요");
                score += 1;
            }
            else
            {
                Debug.Log("최악");
                UiManager.Instance.UpdateAdviceLabel("조금만 더 열심히 해볼까요?");
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
        int grade = 0;
        for (int i = 0; i < count; i++)
        {
            UiManager.Instance.UpdateActionCount(i + 1, count);

            float angle1 = GetComponent<AngleCalculator>().GetAngle(playerLandmark[13], playerLandmark[12], playerLandmark[16], playerLandmark[12]);
            float angle2 = GetComponent<AngleCalculator>().GetAngle(playerLandmark[12], playerLandmark[14], playerLandmark[16], playerLandmark[14]);
            grade = 0;

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
                UiManager.Instance.UpdateAdviceLabel("완벽해요!");
                score += 5;
            }
            else if (grade >= 6)
            {
                Debug.Log("Very Good!");
                UiManager.Instance.UpdateAdviceLabel("아주 좋아요!");
                score += 3;
            }
            else if (grade >= 2)
            {
                Debug.Log("Good");
                UiManager.Instance.UpdateAdviceLabel("좋아요");
                score += 1;
            }
            else
            {
                Debug.Log("최악");
                UiManager.Instance.UpdateAdviceLabel("조금만 더 열심히 해볼까요?");
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
        // 1. RIGHT_HIP = 13   RIGHT_KNEE = 15   RIGHT_ANKLE = 17
        // 2. RIGHT_SHOULDER = 1   RIGHT_HIP = 13   RIGHT_KNEE = 15
        int grade = 0;
        for (int i = 0; i < count; i++)
        {
            UiManager.Instance.UpdateActionCount(i + 1, count);

            float angle1 = GetComponent<AngleCalculator>().GetAngle(playerLandmark[13], playerLandmark[15], playerLandmark[17], playerLandmark[15]);
            float angle2 = GetComponent<AngleCalculator>().GetAngle(playerLandmark[1], playerLandmark[13], playerLandmark[15], playerLandmark[13]);
            grade = 0;

            Debug.Log("스쿼트 " + (i + 1) + "회");

            // 평가 1번 - 제대로 푹 앉았는가
            if (angle1 <= 90) { grade += 5; }
            else if (angle1 <= 100) { grade += 3; }
            else if (angle1 <= 110) { grade += 1; }

            // 평가 2번 - 허리를 적절히 굽혔는가
            if (35 <= angle2 && angle2 <= 55) { grade += 5; }
            else if (30 <= angle2 && angle2 <= 90) { grade += 3; }
            else if (20 <= angle2) { grade += 1; }

            // 동작 평가
            if (grade >= 10)
            {
                Debug.Log("Excellent!");
                UiManager.Instance.UpdateAdviceLabel("완벽해요!");
                score += 5;
            }
            else if (grade >= 6)
            {
                Debug.Log("Very Good!");
                UiManager.Instance.UpdateAdviceLabel("아주 좋아요!");
                score += 3;
            }
            else if (grade >= 2)
            {
                Debug.Log("Good");
                UiManager.Instance.UpdateAdviceLabel("좋아요");
                score += 1;
            }
            else
            {
                Debug.Log("최악");
                UiManager.Instance.UpdateAdviceLabel("조금만 더 열심히 해볼까요?");
            }

            Debug.Log("Grade : " + grade);
            Debug.Log("현재까지의 Score : " + score);
            yield return new WaitForSeconds(3);
        }
        yield return new WaitForSeconds(0);
    }

    // 허벅지-런지(좌우 세트)
    IEnumerator Lunge()
    {
        // 원래 평가항목 2갠데 여기 허리를 어케애햐될지 모르겠어서 평가항목 1개만 하고 점수를 2배로 줌
        // RIGHT_HIP = 13   RIGHT_KNEE = 15   RIGHT_ANKLE = 17
        // LEFT_HIP = 12   LEFT_KNEE = 14   LEFT_ANKLE = 16
        int grade = 0;
        int k = 0;
        for (int i = 0; i < count/2; i++)   //10번
        {
            UiManager.Instance.UpdateActionCount(++k, count);

            // 런지(우)
            Debug.Log("런지 " + (++k) + "회");   

            float angle1 = GetComponent<AngleCalculator>().GetAngle(playerLandmark[13], playerLandmark[15], playerLandmark[17], playerLandmark[15]);
            grade = 0;

            if (87 <= angle1 && angle1 <= 93) { grade += 10; }
            else if (83 <= angle1 && angle1 <= 97) { grade += 6; }
            else if (70 <= angle1 && angle1 <= 110) { grade += 2; }

            if (grade >= 10)
            {
                Debug.Log("Excellent!");
                UiManager.Instance.UpdateAdviceLabel("완벽해요!");
                score += 5;
            }
            else if (grade >= 6)
            {
                Debug.Log("Very Good!");
                UiManager.Instance.UpdateAdviceLabel("아주 좋아요!");
                score += 3;
            }
            else if (grade >= 2)
            {
                Debug.Log("Good");
                UiManager.Instance.UpdateAdviceLabel("좋아요");
                score += 1;
            }
            else
            {
                Debug.Log("최악");
                UiManager.Instance.UpdateAdviceLabel("조금만 더 열심히 해볼까요?");
            }

            Debug.Log("Grade : " + grade);
            Debug.Log("현재까지의 Score : " + score);
            yield return new WaitForSeconds(3);

            // 런지(좌)
            Debug.Log("런지 " + (++k) + "회");
            
            float angle2 = GetComponent<AngleCalculator>().GetAngle(playerLandmark[12], playerLandmark[14], playerLandmark[16], playerLandmark[14]);
            grade = 0;

            if (87 <= angle2 && angle1 <= 93) { grade += 10; }
            else if (83 <= angle2 && angle1 <= 97) { grade += 6; }
            else if (70 <= angle2 && angle1 <= 110) { grade += 2; }

            // 동작 평가
            if (grade >= 10)
            {
                Debug.Log("Excellent!");
                UiManager.Instance.UpdateAdviceLabel("완벽해요!");
                score += 5;
            }
            else if (grade >= 6)
            {
                Debug.Log("Very Good!");
                UiManager.Instance.UpdateAdviceLabel("아주 좋아요!");
                score += 3;
            }
            else if (grade >= 2)
            {
                Debug.Log("Good");
                UiManager.Instance.UpdateAdviceLabel("좋아요");
                score += 1;
            }
            else
            {
                Debug.Log("최악");
                UiManager.Instance.UpdateAdviceLabel("조금만 더 열심히 해볼까요?");
            }

            Debug.Log("Grade : " + grade);
            Debug.Log("현재까지의 Score : " + score);
            yield return new WaitForSeconds(3);
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