using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAssessment : MonoBehaviour
{
    // ����
    public GameObject monster;
    public float damage = 7.5f;

    // �÷��̾�
    public GameObject[] playerLandmark = new GameObject[PLAYER_LANDMARK_COUNT];         // �÷��̾� �� ���帶ũ
    public GameObject head;                                                             // �÷��̾� �Ӹ�
    private Vector3[] playerLandmarkPosition = new Vector3[PLAYER_LANDMARK_COUNT];      // �÷��̾� �� ���帶ũ ������
    private Vector3 headLandmarkPosition = new Vector3(0, 0, 0);                        // �÷��̾� �Ӹ� ������
    const int PLAYER_LANDMARK_COUNT = 22;                                               // �÷��̾� ���帶ũ ��
    
    public GameObject dungeonScene;       // �� ���� ������Ʈ
    private int count;                    // �ڷ�ƾ�� ���� count ����
    public int score;                     // �÷��̾� ���� �� ����

    /*
    //��(22��) ���帶ũ Index
    LEFT_SHOULDER = 0, RIGHT_SHOULDER = 1, LEFT_ELBOW = 2, RIGHT_ELBOW = 3,
    LEFT_WRIST = 4, RIGHT_WRIST = 5, LEFT_PINKY = 6, RIGHT_PINKY = 7,
    LEFT_INDEX = 8, RIGHT_INDEX = 9, LEFT_THUMB = 10, RIGHT_THUMB = 11,
    LEFT_HIP = 12, RIGHT_HIP = 13, LEFT_KNEE = 14, RIGHT_KNEE = 15,
    LEFT_ANKLE = 16, RIGHT_ANKLE = 17, LEFT_HEEL = 18, RIGHT_HEEL = 19,
    LEFT_FOOT_INDEX = 20, RIGHT_FOOT_INDEX = 21,
     */

    /*
    // ���� ���
    float n = angleCal.GetAngle(playerLandmark[12], playerLandmark[13], playerLandmark[17], playerLandmark[13]);
    if (n >= 120)
    {
        Attack();
        Debug.Log("����!!!!!");
    }
    */

    // ���� ���帶ũ ��������()
    public void getPlayerLandmark(GameObject[] landmark, GameObject head)
    {
        for (int i = 11; i < landmark.Length; ++i)
        {
            playerLandmark[i-11] = landmark[i];
        }
        this.head = head;
    }

    // �÷��̾� ���帶ũ�� ������ ���ϱ�()
    public void getPlayerLandmarkPosition(GameObject[] landmark, GameObject head)
    {
        for (int i = 0; i < PLAYER_LANDMARK_COUNT; ++i)
        {
            playerLandmarkPosition[i] = landmark[i].transform.localPosition;
        }
        headLandmarkPosition = head.transform.position;
    }

    // ���� ����()
    private void Attack()
    {
        monster.GetComponent<MonsterController>().TakeDamage(damage);
    }


    // ������� � ��ƾ��
    IEnumerator RunThighRoutine()
    {
        Debug.Log("����� �ڷ�ƾ�� ����Ǿ����ϴ�.");
        Debug.Log("5�� ��, ��� �����մϴ�.");
        for(int i = 5; i > 0 ; i--)
        {
            Debug.Log(i + "��");
            yield return new WaitForSeconds(1);
        }
        

        // ���ĵ� ���̵� ���� ������
        count = 12;
        yield return StartCoroutine(R_StandingSideLegRaise());
        yield return StartCoroutine(L_StandingSideLegRaise());
        yield return new WaitForSeconds(5);

        count = 15;
        yield return StartCoroutine(R_StandingSideLegRaise());
        yield return StartCoroutine(L_StandingSideLegRaise());
        yield return new WaitForSeconds(10);

        // ����Ʈ
        count = 20;
        yield return StartCoroutine(Squat());
        yield return new WaitForSeconds(10);

        // ����
        count = 20;
        yield return StartCoroutine(Lunge());
        yield return new WaitForSeconds(5);
        yield return StartCoroutine(Lunge());

        Debug.Log("����� ��� �����մϴ�.");
    }

    // �����-���ĵ����̵巹�׷�����(��)
    IEnumerator R_StandingSideLegRaise()
    {
        // 1. LEFT_HIP = 12  RIGHT_HIP = 13   RIGHT_ANKLE = 17
        // 2. RIGHT_HIP = 13   RIGHT_KNEE = 15   RIGHT_ANKLE = 17
        for (int i = 0; i < count; i++)
        {
            //playerLandmark�� Null�� -> �ڷ�ƾ ����� 5������ ��ٸ��� �ذ��
            float angle1 = GetComponent<AngleCalculator>().GetAngle(playerLandmark[13], playerLandmark[12], playerLandmark[16], playerLandmark[12]);
            float angle2 = GetComponent<AngleCalculator>().GetAngle(playerLandmark[13], playerLandmark[15], playerLandmark[17], playerLandmark[15]);
            int grade = 0;

            Debug.Log("���ĵ� ���̵� ���׷�����(��) " + (i + 1) + "ȸ");

            // �� 1�� ����
            if (angle1 >= 150 ) { grade += 5;  }
            else if(angle1 >= 135) { grade += 3; }
            else if(angle1 >= 120) { grade += 1; }

            // �� 2�� ����
            if (angle2 >= 180) { grade += 5; }
            else if (angle2 >= 170) { grade += 3; }
            else if (angle2 >= 165) { grade += 1; }

            // ���� ���� ��-> ���� ������� score ������Ʈ/ UI������Ʈ/ score������Ʈ/ �Ѿ� �߻�
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
                Debug.Log("�־�");
            }

            Debug.Log("Grade : " + grade);
            Debug.Log("��������� Score : " + score);
            yield return new WaitForSeconds(3); //3�ʿ� �ѹ��� ��������
        }
        yield return new WaitForSeconds(0);
    }

    // �����-���ĵ����̵巹�׷�����(��)
    IEnumerator L_StandingSideLegRaise()
    {
        // 1. RIGHT_HIP = 13   LEFT_HIP = 12   LEFT_ANKLE = 16
        // 2. LEFT_HIP = 12   LEFT_KNEE = 14   LEFT_ANKLE = 16
        for (int i = 0; i < count; i++)
        {
            float angle1 = GetComponent<AngleCalculator>().GetAngle(playerLandmark[13], playerLandmark[12], playerLandmark[16], playerLandmark[12]);
            float angle2 = GetComponent<AngleCalculator>().GetAngle(playerLandmark[12], playerLandmark[14], playerLandmark[16], playerLandmark[14]);
            int grade = 0;

            Debug.Log("���ĵ� ���̵� ���׷�����(��) " + (i + 1) + "ȸ");

            // �� 1��
            if (angle1 >= 150) { grade += 5; }
            else if (angle1 >= 135) { grade += 3; }
            else if (angle1 >= 120) { grade += 1; }

            // �� 2��
            if (angle2 >= 180) { grade += 5; }
            else if (angle2 >= 170) { grade += 3; }
            else if (angle2 >= 165) { grade += 1; }

            // ���� ��
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
                Debug.Log("�־�");
            }

            Debug.Log("Grade : " + grade);
            Debug.Log("��������� Score : " + score);
            yield return new WaitForSeconds(3);
        }
        yield return new WaitForSeconds(0);
    }

    // �����-����Ʈ
    IEnumerator Squat()
    {
        for (int i = 0; i < count; i++)
        {

        }
        yield return new WaitForSeconds(0);
    }

    // �����-����
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
        // �ڴ��� ��ȣ �Ѱܹް� �ش� ��ƾ �����ϱ��
        int dunNum = dungeonScene.GetComponent<TDungeonSceneManager>().receivedMonsterNumber;

        switch (dunNum)
        {
            case 0:
                Debug.Log("�Ѱܹ��� ���� ��ȣ�� ���� ����� ��ƾ�� ȣ���մϴ�. - PlayerAssessment");
                StartCoroutine(RunThighRoutine());          // ����� ��ƾ �ڷ�ƾ ȣ��

                break;
            default:
                // StartCoroutine(RunThighRoutine());
                break;
        }


        // �����̽��ٸ� ������ �� ���� - ���� ���� �׽�Ʈ
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