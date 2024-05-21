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
        UiManager.Instance.UpdateModeratorLabel("�غ�!");
        yield return new WaitForSeconds(2);

        // 1. ���ĵ� ���̵� ���� ������
        // 1��Ʈ(�� 12��, �� 12��)
        UiManager.Instance.UpdateActionName("���ĵ� ���̵� ���� ������(��)");
        UiManager.Instance.UpdateActionCount(0, 12);

        Debug.Log("5�� ��, '���ĵ����̵巹�׷����� 1��Ʈ'�� �����մϴ�.");
        UiManager.Instance.UpdateModeratorLabel("5�� ��, '���ĵ� ���̵� ���׷����� 1��Ʈ�� �����մϴ�");
        yield return new WaitForSeconds(2);
        for (int i = 5; i > 0; i--)
        {
            Debug.Log(i + "��");
            UiManager.Instance.UpdateModeratorLabel(i.ToString());
            yield return new WaitForSeconds(1);
        }

        count = 12;
        UiManager.Instance.UpdateModeratorLabel("");
        yield return StartCoroutine(R_StandingSideLegRaise());

        UiManager.Instance.UpdateActionName("���ĵ� ���̵� ���� ������(��)");
        UiManager.Instance.UpdateActionCount(0, 12);
        yield return StartCoroutine(L_StandingSideLegRaise());

          // 2��Ʈ(�� 15��, �� 15��)
        UiManager.Instance.UpdateActionName("���ĵ� ���̵� ���� ������(��)");
        UiManager.Instance.UpdateActionCount(0, 15);

        Debug.Log("5�� ��, '���ĵ����̵巹�׷����� 2��Ʈ'�� �����մϴ�");
        UiManager.Instance.UpdateModeratorLabel("5�� ��, '���ĵ����̵巹�׷����� 2��Ʈ'�� �����մϴ�");
        for (int i = 5; i > 0; i--)
        {
            Debug.Log(i + "��");
            UiManager.Instance.UpdateModeratorLabel(i.ToString());
            yield return new WaitForSeconds(1);
        }
        count = 15;
        UiManager.Instance.UpdateModeratorLabel("");
        yield return StartCoroutine(R_StandingSideLegRaise());

        UiManager.Instance.UpdateActionName("���ĵ� ���̵� ���� ������(��)");
        UiManager.Instance.UpdateActionCount(0, 15);
        yield return StartCoroutine(L_StandingSideLegRaise());

        // 2. ����Ʈ
        UiManager.Instance.UpdateActionName("����Ʈ");
        UiManager.Instance.UpdateActionCount(0, 20);

        Debug.Log("10�� ��, '����Ʈ'�� �����մϴ�");
        UiManager.Instance.UpdateModeratorLabel("10�� ��, '����Ʈ'�� �����մϴ�");
        for (int i = 10; i > 0; i--)
        {
            Debug.Log(i + "��");
            UiManager.Instance.UpdateModeratorLabel(i.ToString());
            yield return new WaitForSeconds(1);
        }
        count = 20;
        yield return StartCoroutine(Squat());

        // 3. ����
          // 1��Ʈ (�¿� 20��)
        UiManager.Instance.UpdateActionName("����");
        UiManager.Instance.UpdateActionCount(0, 20);

        Debug.Log("10�� ��, '���� 1��Ʈ'�� �����մϴ�.");
        UiManager.Instance.UpdateModeratorLabel("10�� ��, '���� 1��Ʈ'�� �����մϴ�");
        for (int i = 10; i > 0; i--)
        {
            Debug.Log(i + "��");
            UiManager.Instance.UpdateModeratorLabel(i.ToString());
            yield return new WaitForSeconds(1);
        }
        count = 20;
        yield return StartCoroutine(Lunge());

          // 2��Ʈ (�¿� 20��)
        UiManager.Instance.UpdateActionCount(0, 20);

        Debug.Log("5�� ��, '���� 2��Ʈ'�� �����մϴ�.");
        UiManager.Instance.UpdateModeratorLabel("5�� ��, '���� 2��Ʈ'�� �����մϴ�");
        for (int i = 5; i > 0; i--)
        {
            Debug.Log(i + "��");
            UiManager.Instance.UpdateModeratorLabel(i.ToString());
            yield return new WaitForSeconds(1);
        }
        count = 20;
        yield return StartCoroutine(Lunge());

        UiManager.Instance.UpdateModeratorLabel("����!");
        yield return new WaitForSeconds(3);
        UiManager.Instance.UpdateModeratorLabel("���� ~ ");

        Debug.Log("����� �ڷ�ƾ�� �����մϴ�.");
    }

    // �����-���ĵ����̵巹�׷�����(��)
    IEnumerator R_StandingSideLegRaise()
    {
        // 1. LEFT_HIP = 12  RIGHT_HIP = 13   RIGHT_ANKLE = 17
        // 2. RIGHT_HIP = 13   RIGHT_KNEE = 15   RIGHT_ANKLE = 17
        int grade = 0;
        for (int i = 0; i < count; i++)
        {
            UiManager.Instance.UpdateActionCount(i+1, count);

            //playerLandmark�� Null�� -> �ڷ�ƾ ����� 5������ ��ٸ��� �ذ��
            float angle1 = GetComponent<AngleCalculator>().GetAngle(playerLandmark[13], playerLandmark[12], playerLandmark[16], playerLandmark[12]);
            float angle2 = GetComponent<AngleCalculator>().GetAngle(playerLandmark[13], playerLandmark[15], playerLandmark[17], playerLandmark[15]);
            grade = 0;

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
                UiManager.Instance.UpdateAdviceLabel("�Ϻ��ؿ�!");
                score += 5;
            }
            else if (grade >= 6) 
            { 
                Debug.Log("Very Good!");
                UiManager.Instance.UpdateAdviceLabel("���� ���ƿ�!");
                score += 3;
            }
            else if(grade >= 2)
            {
                Debug.Log("Good");
                UiManager.Instance.UpdateAdviceLabel("���ƿ�");
                score += 1;
            }
            else
            {
                Debug.Log("�־�");
                UiManager.Instance.UpdateAdviceLabel("���ݸ� �� ������ �غ����?");
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
        int grade = 0;
        for (int i = 0; i < count; i++)
        {
            UiManager.Instance.UpdateActionCount(i + 1, count);

            float angle1 = GetComponent<AngleCalculator>().GetAngle(playerLandmark[13], playerLandmark[12], playerLandmark[16], playerLandmark[12]);
            float angle2 = GetComponent<AngleCalculator>().GetAngle(playerLandmark[12], playerLandmark[14], playerLandmark[16], playerLandmark[14]);
            grade = 0;

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
                UiManager.Instance.UpdateAdviceLabel("�Ϻ��ؿ�!");
                score += 5;
            }
            else if (grade >= 6)
            {
                Debug.Log("Very Good!");
                UiManager.Instance.UpdateAdviceLabel("���� ���ƿ�!");
                score += 3;
            }
            else if (grade >= 2)
            {
                Debug.Log("Good");
                UiManager.Instance.UpdateAdviceLabel("���ƿ�");
                score += 1;
            }
            else
            {
                Debug.Log("�־�");
                UiManager.Instance.UpdateAdviceLabel("���ݸ� �� ������ �غ����?");
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
        // 1. RIGHT_HIP = 13   RIGHT_KNEE = 15   RIGHT_ANKLE = 17
        // 2. RIGHT_SHOULDER = 1   RIGHT_HIP = 13   RIGHT_KNEE = 15
        int grade = 0;
        for (int i = 0; i < count; i++)
        {
            UiManager.Instance.UpdateActionCount(i + 1, count);

            float angle1 = GetComponent<AngleCalculator>().GetAngle(playerLandmark[13], playerLandmark[15], playerLandmark[17], playerLandmark[15]);
            float angle2 = GetComponent<AngleCalculator>().GetAngle(playerLandmark[1], playerLandmark[13], playerLandmark[15], playerLandmark[13]);
            grade = 0;

            Debug.Log("����Ʈ " + (i + 1) + "ȸ");

            // �� 1�� - ����� ǫ �ɾҴ°�
            if (angle1 <= 90) { grade += 5; }
            else if (angle1 <= 100) { grade += 3; }
            else if (angle1 <= 110) { grade += 1; }

            // �� 2�� - �㸮�� ������ �����°�
            if (35 <= angle2 && angle2 <= 55) { grade += 5; }
            else if (30 <= angle2 && angle2 <= 90) { grade += 3; }
            else if (20 <= angle2) { grade += 1; }

            // ���� ��
            if (grade >= 10)
            {
                Debug.Log("Excellent!");
                UiManager.Instance.UpdateAdviceLabel("�Ϻ��ؿ�!");
                score += 5;
            }
            else if (grade >= 6)
            {
                Debug.Log("Very Good!");
                UiManager.Instance.UpdateAdviceLabel("���� ���ƿ�!");
                score += 3;
            }
            else if (grade >= 2)
            {
                Debug.Log("Good");
                UiManager.Instance.UpdateAdviceLabel("���ƿ�");
                score += 1;
            }
            else
            {
                Debug.Log("�־�");
                UiManager.Instance.UpdateAdviceLabel("���ݸ� �� ������ �غ����?");
            }

            Debug.Log("Grade : " + grade);
            Debug.Log("��������� Score : " + score);
            yield return new WaitForSeconds(3);
        }
        yield return new WaitForSeconds(0);
    }

    // �����-����(�¿� ��Ʈ)
    IEnumerator Lunge()
    {
        // ���� ���׸� 2���� ���� �㸮�� ���ɾ������ �𸣰ھ ���׸� 1���� �ϰ� ������ 2��� ��
        // RIGHT_HIP = 13   RIGHT_KNEE = 15   RIGHT_ANKLE = 17
        // LEFT_HIP = 12   LEFT_KNEE = 14   LEFT_ANKLE = 16
        int grade = 0;
        int k = 0;
        for (int i = 0; i < count/2; i++)   //10��
        {
            UiManager.Instance.UpdateActionCount(++k, count);

            // ����(��)
            Debug.Log("���� " + (++k) + "ȸ");   

            float angle1 = GetComponent<AngleCalculator>().GetAngle(playerLandmark[13], playerLandmark[15], playerLandmark[17], playerLandmark[15]);
            grade = 0;

            if (87 <= angle1 && angle1 <= 93) { grade += 10; }
            else if (83 <= angle1 && angle1 <= 97) { grade += 6; }
            else if (70 <= angle1 && angle1 <= 110) { grade += 2; }

            if (grade >= 10)
            {
                Debug.Log("Excellent!");
                UiManager.Instance.UpdateAdviceLabel("�Ϻ��ؿ�!");
                score += 5;
            }
            else if (grade >= 6)
            {
                Debug.Log("Very Good!");
                UiManager.Instance.UpdateAdviceLabel("���� ���ƿ�!");
                score += 3;
            }
            else if (grade >= 2)
            {
                Debug.Log("Good");
                UiManager.Instance.UpdateAdviceLabel("���ƿ�");
                score += 1;
            }
            else
            {
                Debug.Log("�־�");
                UiManager.Instance.UpdateAdviceLabel("���ݸ� �� ������ �غ����?");
            }

            Debug.Log("Grade : " + grade);
            Debug.Log("��������� Score : " + score);
            yield return new WaitForSeconds(3);

            // ����(��)
            Debug.Log("���� " + (++k) + "ȸ");
            
            float angle2 = GetComponent<AngleCalculator>().GetAngle(playerLandmark[12], playerLandmark[14], playerLandmark[16], playerLandmark[14]);
            grade = 0;

            if (87 <= angle2 && angle1 <= 93) { grade += 10; }
            else if (83 <= angle2 && angle1 <= 97) { grade += 6; }
            else if (70 <= angle2 && angle1 <= 110) { grade += 2; }

            // ���� ��
            if (grade >= 10)
            {
                Debug.Log("Excellent!");
                UiManager.Instance.UpdateAdviceLabel("�Ϻ��ؿ�!");
                score += 5;
            }
            else if (grade >= 6)
            {
                Debug.Log("Very Good!");
                UiManager.Instance.UpdateAdviceLabel("���� ���ƿ�!");
                score += 3;
            }
            else if (grade >= 2)
            {
                Debug.Log("Good");
                UiManager.Instance.UpdateAdviceLabel("���ƿ�");
                score += 1;
            }
            else
            {
                Debug.Log("�־�");
                UiManager.Instance.UpdateAdviceLabel("���ݸ� �� ������ �غ����?");
            }

            Debug.Log("Grade : " + grade);
            Debug.Log("��������� Score : " + score);
            yield return new WaitForSeconds(3);
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