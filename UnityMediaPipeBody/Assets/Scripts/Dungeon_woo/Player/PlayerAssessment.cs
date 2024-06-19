using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAssessment : MonoBehaviour
{
    // ����
    public GameObject monster;
    public Animator mosterAnimator;
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

    // ���̵� ��
    public GameObject guideModel;
    public Animator animator;

    // BulletGenerator(�ǰ�ȿ��)
    public BulletGenerator bullet;

    // ���� ���� ������Ʈ
    public GameObject TerminationGameManager;

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

    // ���� ���� �� -> ����
    public float MotionRating(int motionGrade)
    {
        if (motionGrade >= 10)
        {
            // ���⿡ �ǰ� ȣ��
            bullet.GetComponent<BulletGenerator>().ShootBullet(0);

            // ���� ������ �ִϸ��̼�
            mosterAnimator.SetBool("ani_Damage", true);

            Debug.Log("�� : 1���");
            UiManager.Instance.UpdateAdviceLabel("�Ϻ��ؿ�!");
            score += 5;
        }
        else if (motionGrade >= 6)
        {
            // ���⿡ �ǰ� ȣ��
            bullet.GetComponent<BulletGenerator>().ShootBullet(0);

            // ���� ������ �ִϸ��̼�
            mosterAnimator.SetBool("ani_Damage", true);

            Debug.Log("�� : 2���");
            UiManager.Instance.UpdateAdviceLabel("���� ���ƿ�!");
            score += 3;
        }
        else if (motionGrade >= 2)
        {
            // ���⿡ �ǰ� ȣ��
            bullet.GetComponent<BulletGenerator>().ShootBullet(0);

            // ���� ������ �ִϸ��̼�
            mosterAnimator.SetBool("ani_Damage", true);

            Debug.Log("�� : 3���");
            UiManager.Instance.UpdateAdviceLabel("���ƿ�");
            score += 1;
        }
        else
        {
            // �ǰ�ȿ�� �׽�Ʈ
            //bullet.GetComponent<BulletGenerator>().ShootBullet(0);

            // ���� ������ �ִϸ��̼� �׽�Ʈ
            //mosterAnimator.SetBool("ani_Damage", true);

            Debug.Log("�� : 4���");
            UiManager.Instance.UpdateAdviceLabel("���ݸ� �� ������ �غ����?");
        }

        Debug.Log("Grade : " + motionGrade);
        Debug.Log("��������� Score : " + score);
        return 0f;
    }

    /* ---------------------------------------------------------------- */
    // �� ����� � ��ƾ ��
    IEnumerator RunThighRoutine()
    {
        Debug.Log("����� �ڷ�ƾ�� ����Ǿ����ϴ�.");
        UiManager.Instance.UpdateModeratorLabel("�غ�!");
        yield return new WaitForSeconds(2);

    
        // 1. ���ĵ� ���̵� ���� ������
        // 1��Ʈ - ��
        UiManager.Instance.UpdateActionName("�� - ���ĵ� ���̵� ���� ������ (1set)");
        UiManager.Instance.UpdateActionCount(0, 15);
        UiManager.Instance.UpdateAdviceLabel("");

        Debug.Log("5�� ��, '���ĵ����̵巹�׷����� 1��Ʈ'�� �����մϴ�.");
        UiManager.Instance.UpdateModeratorLabel("5�� ��, '���ĵ� ���̵� ���׷�����' 1��Ʈ�� �����մϴ�");
        yield return new WaitForSeconds(2);
        for (int i = 5; i > 0; i--)
        {
            Debug.Log(i + "��");
            UiManager.Instance.UpdateModeratorLabel(i.ToString());
            yield return new WaitForSeconds(1);
        }
        
        count = 15;
        UiManager.Instance.UpdateModeratorLabel("");
        animator.SetBool("SideLegRaise", true);
        yield return StartCoroutine(R_StandingSideLegRaise());
 
        
        // 1��Ʈ - ��
        UiManager.Instance.UpdateActionName("�� - ���ĵ� ���̵� ���� ������ (1set)");
        UiManager.Instance.UpdateActionCount(0, 15);
        UiManager.Instance.UpdateAdviceLabel("");
        yield return StartCoroutine(L_StandingSideLegRaise());
        animator.SetBool("SideLegRaise", false);

        // 2��Ʈ - ��
        UiManager.Instance.UpdateActionName("�� - ���ĵ� ���̵� ���� ������ (2set)");
        UiManager.Instance.UpdateActionCount(0, 15);
        UiManager.Instance.UpdateAdviceLabel("");

        Debug.Log("5�� ��, '���ĵ����̵巹�׷����� 2��Ʈ'�� �����մϴ�");
        UiManager.Instance.UpdateModeratorLabel("5�� ��, '���ĵ����̵巹�׷����� 2��Ʈ'�� �����մϴ�");
        yield return new WaitForSeconds(2);
        for (int i = 5; i > 0; i--)
        {
            Debug.Log(i + "��");
            UiManager.Instance.UpdateModeratorLabel(i.ToString());
            yield return new WaitForSeconds(1);
        }
        count = 15;
        UiManager.Instance.UpdateModeratorLabel("");
        animator.SetBool("SideLegRaise", true);
        yield return StartCoroutine(R_StandingSideLegRaise());

        // 2��Ʈ - ��
        UiManager.Instance.UpdateActionName("�� - ���ĵ� ���̵� ���� ������ (2set)");
        UiManager.Instance.UpdateActionCount(0, 15);
        UiManager.Instance.UpdateAdviceLabel("");
        yield return StartCoroutine(L_StandingSideLegRaise());
        animator.SetBool("SideLegRaise", false);

        // 2. ����Ʈ
        UiManager.Instance.UpdateActionName("����Ʈ");
        UiManager.Instance.UpdateActionCount(0, 20);
        UiManager.Instance.UpdateAdviceLabel("");

        Debug.Log("5�� ��, '����Ʈ'�� �����մϴ�");
        UiManager.Instance.UpdateModeratorLabel("5�� ��, '����Ʈ'�� �����մϴ�");
        yield return new WaitForSeconds(2);
        for (int i = 5; i > 0; i--)
        {
            Debug.Log(i + "��");
            UiManager.Instance.UpdateModeratorLabel(i.ToString());
            yield return new WaitForSeconds(1);
        }
        count = 20;
        UiManager.Instance.UpdateModeratorLabel("");
        animator.SetBool("Squat", true);
        yield return StartCoroutine(Squat());
        animator.SetBool("Squat", false);

        // 3. ����
        // 1��Ʈ (�¿� 20��)
        UiManager.Instance.UpdateActionName("���� (1set)");
        UiManager.Instance.UpdateActionCount(0, 20);
        UiManager.Instance.UpdateAdviceLabel("");

        // �����ڿ� 2�� �� ���ֱ�
        UiManager.Instance.UpdateAngle2Label("");

        Debug.Log("5�� ��, '���� 1��Ʈ'�� �����մϴ�.");
        UiManager.Instance.UpdateModeratorLabel("5�� ��, '���� 1��Ʈ'�� �����մϴ�");
        yield return new WaitForSeconds(2);
        for (int i = 5; i > 0; i--)
        {
            Debug.Log(i + "��");
            UiManager.Instance.UpdateModeratorLabel(i.ToString());
            yield return new WaitForSeconds(1);
        }
        count = 20;
        UiManager.Instance.UpdateModeratorLabel("");
        animator.SetBool("Launge", true);
        yield return StartCoroutine(Lunge());
        animator.SetBool("Launge", false);

        // 2��Ʈ (�¿� 20��)
        UiManager.Instance.UpdateActionName("���� (2set)");
        UiManager.Instance.UpdateActionCount(0, 20);
        UiManager.Instance.UpdateAdviceLabel("");

        Debug.Log("5�� ��, '���� 2��Ʈ'�� �����մϴ�.");
        UiManager.Instance.UpdateModeratorLabel("5�� ��, '���� 2��Ʈ'�� �����մϴ�");
        yield return new WaitForSeconds(2);
        for (int i = 5; i > 0; i--)
        {
            Debug.Log(i + "��");
            UiManager.Instance.UpdateModeratorLabel(i.ToString());
            yield return new WaitForSeconds(1);
        }
        count = 20;
        UiManager.Instance.UpdateModeratorLabel("");
        animator.SetBool("Launge", true);
        yield return StartCoroutine(Lunge());
        animator.SetBool("Launge", false);
        
        // � ��
        UiManager.Instance.UpdateActionName("");
        UiManager.Instance.UpdateActionCount(0, 0);
        UiManager.Instance.UpdateAdviceLabel("");

        SoundManager.instance.StopBGM("BGM_Ingame");
        SoundManager.instance.PlaySFX("SFX_Count_2");
        UiManager.Instance.UpdateModeratorLabel("����!");
        yield return new WaitForSeconds(3);

        //���� �� �򰡾� �̵�
        dungeonScene.GetComponent<TDungeonSceneManager>().GoOverScene(score);
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
            SoundManager.instance.PlaySFX("SFX_Count_1");

            //playerLandmark�� Null�� -> �ڷ�ƾ ����� 5������ ��ٸ��� �ذ��
            // ���� ����
            yield return new WaitForSeconds(1.5f);
            float angle1 = GetComponent<AngleCalculator>().GetAngle(playerLandmark[12], playerLandmark[13], playerLandmark[17], playerLandmark[13]);
            float angle2 = GetComponent<AngleCalculator>().GetAngle(playerLandmark[13], playerLandmark[15], playerLandmark[17], playerLandmark[15]);
            grade = 0;

            Debug.Log("���ĵ� ���̵� ���׷�����(��) " + (i + 1) + "ȸ");
            
            // �� 1�� ����(��ȭ) �ٸ��� �󸶳� ���÷ȴ°�
            if (angle1 >= 120 ) { grade += 5;  }
            else if(angle1 >= 115) { grade += 3; }
            else if(angle1 >= 110) { grade += 1; }

            // �� 2�� ����(��ȭ) �ٸ��� ���θ��� �ʰ� �� ��°�
            if (angle2 >= 150) { grade += 5; }
            else if (angle2 >= 140) { grade += 3; }
            else if (angle2 >= 135) { grade += 1; }

            //���� ��
            MotionRating(grade);

            // �����ڿ� ��
            UiManager.Instance.UpdateAngle1Label("�ٸ��� ������ ���÷ȴ°� : " + angle1);
            UiManager.Instance.UpdateAngle2Label("�ٸ��� ���θ��� �ʾҴ°� : " + angle2);
            UiManager.Instance.UpdateOverallLabel("���� ���� ��(10�� ����) : " + grade + "��");
            UiManager.Instance.UpdateScorelLabel("���� ���ھ� : " + score);

            mosterAnimator.SetBool("ani_Damage", false); 
            yield return new WaitForSeconds(1.5f); //3�ʿ� �ѹ��� ��������
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
            SoundManager.instance.PlaySFX("SFX_Count_1");

            // ���� ����
            yield return new WaitForSeconds(1.5f);
            float angle1 = GetComponent<AngleCalculator>().GetAngle(playerLandmark[13], playerLandmark[12], playerLandmark[16], playerLandmark[12]);
            float angle2 = GetComponent<AngleCalculator>().GetAngle(playerLandmark[12], playerLandmark[14], playerLandmark[16], playerLandmark[14]);
            grade = 0;

            Debug.Log("���ĵ� ���̵� ���׷�����(��) " + (i + 1) + "ȸ");


            // �� 1��(��ȭ) �ٸ��� �󸶳� ���÷ȴ°�
            if (angle1 >= 120) { grade += 5; }
            else if (angle1 >= 115) { grade += 3; }
            else if (angle1 >= 110) { grade += 1; }

            // �� 2��(��ȭ) �ٸ��� ���θ��� �ʰ� �� ��°�
            if (angle2 >= 150) { grade += 5; }
            else if (angle2 >= 140) { grade += 3; }
            else if (angle2 >= 135) { grade += 1; }

            //���� ��
            MotionRating(grade);

            // �����ڿ� ��
            UiManager.Instance.UpdateAngle1Label("�ٸ��� ������ ���÷ȴ°� : " + angle1);
            UiManager.Instance.UpdateAngle2Label("�ٸ��� ���θ��� �ʾҴ°� : " + angle2);
            UiManager.Instance.UpdateOverallLabel("���� ���� ��(10�� ����) : " + grade + "��");
            UiManager.Instance.UpdateScorelLabel("���� ���ھ� : " + score);

            mosterAnimator.SetBool("ani_Damage", false);
            yield return new WaitForSeconds(1.5f);
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
            SoundManager.instance.PlaySFX("SFX_Count_1");

            // ���� ����
            yield return new WaitForSeconds(1.5f);
            float angle1 = GetComponent<AngleCalculator>().GetAngle(playerLandmark[13], playerLandmark[15], playerLandmark[17], playerLandmark[15]);
            float angle2 = GetComponent<AngleCalculator>().GetAngle(playerLandmark[1], playerLandmark[13], playerLandmark[15], playerLandmark[13]);
            grade = 0;

            Debug.Log("����Ʈ " + (i + 1) + "ȸ");

            
            // �� 1��(��ȭ) - ����� ǫ �ɾҴ°�
            if (angle1 <= 100) { grade += 5; }
            else if (angle1 <= 115) { grade += 3; }
            else if (angle1 <= 130) { grade += 1; }

            // �� 2��(��ȭ) - �㸮�� ������ �����°�
            if (35 <= angle2 && angle2 <= 55) { grade += 5; }
            else if (35 <= angle2 && angle2 <= 90) { grade += 3; }
            else if (10 <= angle2) { grade += 1; }

            //���� ��
            MotionRating(grade);

            // �����ڿ� ��
            UiManager.Instance.UpdateAngle1Label("������ ������ �ɾҴ°� : " + angle1);
            UiManager.Instance.UpdateAngle2Label("�㸮�� ������ �����°� : " + angle2);
            UiManager.Instance.UpdateOverallLabel("���� ���� ��(10�� ����) : " + grade + "��");
            UiManager.Instance.UpdateScorelLabel("���� ���ھ� : " + score);

            mosterAnimator.SetBool("ani_Damage", false);
            yield return new WaitForSeconds(1.5f);
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
            SoundManager.instance.PlaySFX("SFX_Count_1");

            // ����(��)
            Debug.Log("���� " + (k) + "ȸ");

            // ���� ����(��ȭ)
            yield return new WaitForSeconds(1.5f);
            float angle1 = GetComponent<AngleCalculator>().GetAngle(playerLandmark[13], playerLandmark[15], playerLandmark[17], playerLandmark[15]);
            grade = 0;

            if (80 <= angle1 && angle1 <= 100) { grade += 10; }
            else if (70 <= angle1 && angle1 <= 105) { grade += 6; }
            else if (60 <= angle1 && angle1 <= 110) { grade += 2; }

            //���� ��
            MotionRating(grade);

            // �����ڿ� ��
            UiManager.Instance.UpdateAngle1Label("�ٸ��� ������ �����°� : " + angle1);
            UiManager.Instance.UpdateOverallLabel("���� ���� ��(10�� ����) : " + grade + "��");
            UiManager.Instance.UpdateScorelLabel("���� ���ھ� : " + score);

            mosterAnimator.SetBool("ani_Damage", false);
            yield return new WaitForSeconds(1.5f);

            // ����(��)
            UiManager.Instance.UpdateActionCount(++k, count);
            SoundManager.instance.PlaySFX("SFX_Count_1");
            Debug.Log("���� " + (k) + "ȸ");

            // ���� ����
            yield return new WaitForSeconds(1.5f);
            float angle2 = GetComponent<AngleCalculator>().GetAngle(playerLandmark[12], playerLandmark[14], playerLandmark[16], playerLandmark[14]);
            grade = 0;

            
            if (80 <= angle2 && angle1 <= 100) { grade += 10; }
            else if (70 <= angle2 && angle1 <= 105) { grade += 6; }
            else if (60 <= angle2 && angle1 <= 110) { grade += 2; }

            //���� ��
            MotionRating(grade);

            // �����ڿ� ��
            UiManager.Instance.UpdateAngle1Label("�ٸ��� ������ �����°� : " + angle2);
            UiManager.Instance.UpdateOverallLabel("���� ���� ��(10�� ����) : " + grade + "��");
            UiManager.Instance.UpdateScorelLabel("���� ���ھ� : " + score);

            mosterAnimator.SetBool("ani_Damage", false);
            yield return new WaitForSeconds(1.5f);
        }
        yield return new WaitForSeconds(0);
    }
    /* ---------------------------------------------------------------- */

    private void Start()
    {
        score = 0;
        // �ڴ��� ��ȣ �Ѱܹް� �ش� ��ƾ �����ϱ��
        int dunNum = dungeonScene.GetComponent<TDungeonSceneManager>().receivedMonsterNumber;

        // ���̵� �� �ִϸ�����
        animator = guideModel.GetComponent<Animator>();

        // ���� �ִϸ�����
        mosterAnimator = monster.GetComponent<MonsterController>().animator;

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
    }
}