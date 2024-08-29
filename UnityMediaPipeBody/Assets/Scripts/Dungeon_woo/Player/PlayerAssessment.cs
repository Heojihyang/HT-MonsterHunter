using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAssessment : MonoBehaviour
{
    [Header("����")]
    public GameObject dungeonScene;
    public BulletGenerator bullet;
    public Camera mainCamera;

    [Header ("����")]
    public GameObject monster;
    public Animator mosterAnimator;

    [Header("���̵��")]
    public GameObject guideModel;
    public List<GameObject> guideModels;
    public Animator animator;

    [Header("�÷��̾�")]
    public int score;                                                                   // �÷��̾� ���� �� ����
    public GameObject[] playerLandmark = new GameObject[PLAYER_LANDMARK_COUNT];         // �÷��̾� �� ���帶ũ
    public GameObject head;                                                             // �÷��̾� �Ӹ�
    private Vector3[] playerLandmarkPosition = new Vector3[PLAYER_LANDMARK_COUNT];      // �÷��̾� �� ���帶ũ ������
    private Vector3 headLandmarkPosition = new Vector3(0, 0, 0);                        // �÷��̾� �Ӹ� ������
    const int PLAYER_LANDMARK_COUNT = 22;                                               // �÷��̾� ���帶ũ ��
    
    private int count;  // �ڷ�ƾ�� ���� count ����
    private int dunNum; // ���� ����


    /*
    //��(22��) ���帶ũ Index
    LEFT_SHOULDER = 0, RIGHT_SHOULDER = 1, LEFT_ELBOW = 2, RIGHT_ELBOW = 3,
    LEFT_WRIST = 4, RIGHT_WRIST = 5, LEFT_PINKY = 6, RIGHT_PINKY = 7,
    LEFT_INDEX = 8, RIGHT_INDEX = 9, LEFT_THUMB = 10, RIGHT_THUMB = 11,
    LEFT_HIP = 12, RIGHT_HIP = 13, LEFT_KNEE = 14, RIGHT_KNEE = 15,
    LEFT_ANKLE = 16, RIGHT_ANKLE = 17, LEFT_HEEL = 18, RIGHT_HEEL = 19,
    LEFT_FOOT_INDEX = 20, RIGHT_FOOT_INDEX = 21,
    */

    /// ���� ���帶ũ ��������()
    public void getPlayerLandmark(GameObject[] landmark, GameObject head)
    {
        for (int i = 11; i < landmark.Length; ++i)
        {
            playerLandmark[i-11] = landmark[i];
        }
        this.head = head;
    }

    /// �÷��̾� ���帶ũ�� ������ ���ϱ�()
    public void getPlayerLandmarkPosition(GameObject[] landmark, GameObject head)
    {
        for (int i = 0; i < PLAYER_LANDMARK_COUNT; ++i)
        {
            playerLandmarkPosition[i] = landmark[i].transform.localPosition;
        }
        headLandmarkPosition = head.transform.position;
    }

    /// ���� ���� ��
    public float MotionRating(int motionGrade)
    {
        if (motionGrade >= 10)
        {
            //Debug.Log("�� : 1���");
            bullet.GetComponent<BulletGenerator>().ShootBullet(0);
            mosterAnimator.SetBool("ani_Damage", true); 
            UiManager.Instance.UpdateAdviceLabel("�Ϻ��ؿ�!");
            score += 5;
        }
        else if (motionGrade >= 6)
        {
            //Debug.Log("�� : 2���");
            bullet.GetComponent<BulletGenerator>().ShootBullet(0);
            mosterAnimator.SetBool("ani_Damage", true);
            UiManager.Instance.UpdateAdviceLabel("���� ���ƿ�!");
            score += 3;
        }
        else if (motionGrade >= 2)
        {
            //Debug.Log("�� : 3���");
            bullet.GetComponent<BulletGenerator>().ShootBullet(0);
            mosterAnimator.SetBool("ani_Damage", true);
            UiManager.Instance.UpdateAdviceLabel("���ƿ�");
            score += 1;
        }
        else
        {
            //Debug.Log("�� : 4���");
            //bullet.GetComponent<BulletGenerator>().ShootBullet(0);
            //mosterAnimator.SetBool("ani_Damage", true);
            UiManager.Instance.UpdateAdviceLabel("���ݸ� �� ������ �غ����?");
        }

        return 0f;
    }

    /* ---------------------------------------------------------------- */
    /// �� ����� � ��ƾ ��
    IEnumerator RunThighRoutine()
    {
        UiManager.Instance.UpdateModeratorLabel("�غ�!");
        yield return new WaitForSeconds(2);

    
        // 1. ���ĵ� ���̵� ���� ������
        // 1��Ʈ - ��
        UiManager.Instance.UpdateActionName("�� - ���ĵ� ���̵� ���� ������ (1set)");
        UiManager.Instance.UpdateActionCount(0, 15);
        UiManager.Instance.UpdateAdviceLabel("");

        UiManager.Instance.UpdateModeratorLabel("5�� ��, '���ĵ� ���̵� ���׷�����' 1��Ʈ�� �����մϴ�");
        yield return new WaitForSeconds(2);
        for (int i = 5; i > 0; i--)
        {
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

        UiManager.Instance.UpdateModeratorLabel("5�� ��, '���ĵ����̵巹�׷����� 2��Ʈ'�� �����մϴ�");
        yield return new WaitForSeconds(2);
        for (int i = 5; i > 0; i--)
        {
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

        UiManager.Instance.UpdateModeratorLabel("5�� ��, '����Ʈ'�� �����մϴ�");
        yield return new WaitForSeconds(2);
        for (int i = 5; i > 0; i--)
        {
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

        UiManager.Instance.UpdateModeratorLabel("5�� ��, '���� 1��Ʈ'�� �����մϴ�");
        yield return new WaitForSeconds(2);
        for (int i = 5; i > 0; i--)
        {
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

        UiManager.Instance.UpdateModeratorLabel("5�� ��, '���� 2��Ʈ'�� �����մϴ�");
        yield return new WaitForSeconds(2);
        for (int i = 5; i > 0; i--)
        {
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
        if (score >= 450)
        {
            mosterAnimator.SetBool("ani_Die", true);
        }
        yield return new WaitForSeconds(3);

        //���� �� �򰡾� �̵�
        dungeonScene.GetComponent<TDungeonSceneManager>().GoOverScene(score);
    }


    /// �����-���ĵ����̵巹�׷�����(��)
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

            yield return new WaitForSeconds(1.5f);
            mosterAnimator.SetBool("ani_Damage", false);
        }
        yield return new WaitForSeconds(0);
    }

    /// �����-���ĵ����̵巹�׷�����(��)
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

            yield return new WaitForSeconds(1.5f);
            mosterAnimator.SetBool("ani_Damage", false);
        }
        yield return new WaitForSeconds(0);
    }

    /// �����-����Ʈ
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

            yield return new WaitForSeconds(1.5f);
            mosterAnimator.SetBool("ani_Damage", false);
        }
        yield return new WaitForSeconds(0);
    }

    /// �����-����(�¿� ��Ʈ)
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

            yield return new WaitForSeconds(1.5f);
            mosterAnimator.SetBool("ani_Damage", false);

            // ����(��)
            UiManager.Instance.UpdateActionCount(++k, count);
            SoundManager.instance.PlaySFX("SFX_Count_1");

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

            yield return new WaitForSeconds(1.5f);
            mosterAnimator.SetBool("ani_Damage", false);
        }
        yield return new WaitForSeconds(0);
    }
    /* ---------------------------------------------------------------- */



    /* ---------------------------------------------------------------- */
    /// �� ��α� � ��ƾ ��
    IEnumerator RunTricepsRoutine()
    {
        UiManager.Instance.UpdateModeratorLabel("�غ�!");
        yield return new WaitForSeconds(2);

        // 1. ������
        UiManager.Instance.UpdateActionName("������ (1set)");
        UiManager.Instance.UpdateActionCount(0, 15);
        UiManager.Instance.UpdateAdviceLabel("");
        UiManager.Instance.UpdateModeratorLabel("5�� ��, '������' 1��Ʈ�� �����մϴ�");
        yield return new WaitForSeconds(2);

        for (int i = 5; i > 0; i--)
        {
            UiManager.Instance.UpdateModeratorLabel(i.ToString());
            yield return new WaitForSeconds(1);
        }
        UiManager.Instance.UpdateModeratorLabel("");

        count = 15;
        animator.SetBool("Reize", true);
        yield return StartCoroutine(Rais());
        animator.SetBool("Reize", false);


        // 2. ���������
        UiManager.Instance.UpdateActionName("��������� (1set)");
        UiManager.Instance.UpdateActionCount(0, 15);
        UiManager.Instance.UpdateAdviceLabel("");
        UiManager.Instance.UpdateModeratorLabel("5�� ��, '���������'1��Ʈ�� �����մϴ�");
        yield return new WaitForSeconds(2);

        for (int i = 5; i > 0; i--)
        {
            UiManager.Instance.UpdateModeratorLabel(i.ToString());
            yield return new WaitForSeconds(1);
        }
        UiManager.Instance.UpdateModeratorLabel("");

        count = 15;
        animator.SetBool("ShoulderPress", true);
        yield return StartCoroutine(ShoulderPress());
        animator.SetBool("ShoulderPress", false);


        // 3. ű�� - ��
        UiManager.Instance.UpdateActionName("ű�� - �� (1set)");
        UiManager.Instance.UpdateActionCount(0, 12);
        UiManager.Instance.UpdateAdviceLabel("");
        UiManager.Instance.UpdateModeratorLabel("5�� ��, 'ű�� - ��' 1��Ʈ�� �����մϴ�");
        yield return new WaitForSeconds(2);

        for (int i = 5; i > 0; i--)
        {
            UiManager.Instance.UpdateModeratorLabel(i.ToString());
            yield return new WaitForSeconds(1);
        }
        UiManager.Instance.UpdateModeratorLabel("");

        count = 12;
        animator.SetBool("KickBack", true);
        yield return StartCoroutine(R_KickBack());
        animator.SetBool("KickBack", false);


        // 4. ű�� - ��
        UiManager.Instance.UpdateActionName("ű�� - �� (2set)");
        UiManager.Instance.UpdateActionCount(0, 12);
        UiManager.Instance.UpdateAdviceLabel("");
        UiManager.Instance.UpdateModeratorLabel("5�� ��, 'ű�� - ��' 2��Ʈ�� �����մϴ�");
        yield return new WaitForSeconds(2);

        for (int i = 5; i > 0; i--)
        {
            UiManager.Instance.UpdateModeratorLabel(i.ToString());
            yield return new WaitForSeconds(1);
        }
        UiManager.Instance.UpdateModeratorLabel("");

        count = 12;
        animator.SetBool("KickBack", true);
        yield return StartCoroutine(L_KickBack());
        animator.SetBool("KickBack", false);


        // � ��
        UiManager.Instance.UpdateActionName("");
        UiManager.Instance.UpdateActionCount(0, 0);
        UiManager.Instance.UpdateAdviceLabel("");

        SoundManager.instance.StopBGM("BGM_Ingame");
        SoundManager.instance.PlaySFX("SFX_Count_2");
        UiManager.Instance.UpdateModeratorLabel("����!");
        if (score >= 450)
        {
            mosterAnimator.SetBool("ani_Die", true);
        }
        yield return new WaitForSeconds(3);

        //���� �� �򰡾� �̵�
        dungeonScene.GetComponent<TDungeonSceneManager>().GoOverScene(score);
    }



    /// ��α� - ������
    IEnumerator Rais()
    {
        // 1. RIGHT_SHOULDER = 1   RIGHT_ELBOW = 3   RIGHT_WRIST = 5
        // 2. RIGHT_WRIST = 5   LEFT_WRIST = 4
        int grade = 0;
        for (int i = 0; i < count; i++)
        {
            UiManager.Instance.UpdateActionCount(i + 1, count);
            SoundManager.instance.PlaySFX("SFX_Count_1");

            // ���� ����
            yield return new WaitForSeconds(1.5f);
            float angle1 = GetComponent<AngleCalculator>().GetAngle(playerLandmark[1], playerLandmark[3], playerLandmark[5], playerLandmark[3]);
            //float angle2 = Mathf.Abs(playerLandmark[5].transform.position.y - playerLandmark[4].transform.position.y);
            grade = 0;

            // �� 1�� - ���� ����� ����°�
            if (angle1 >= 85) { grade += 10; }
            else if (angle1 >= 80) { grade += 6; }
            else if (angle1 >= 70) { grade += 2; }

            // �� 2�� - �� ���� �����ΰ�  -> �׽�Ʈ �ʿ�
            //if (angle2 <= 1) { grade += 5; }
            //else if (angle1 <= 3) { grade += 3; }
            //else if (angle1 <= 5) { grade += 1; }

            // ���� ��
            MotionRating(grade);

            // �����ڿ� ��
            UiManager.Instance.UpdateAngle1Label("���� ����� ����°� : " + angle1);
            //UiManager.Instance.UpdateAngle2Label("�� ���� �����ΰ� : " + angle2);
            UiManager.Instance.UpdateOverallLabel("���� ���� ��(10�� ����) : " + grade + "��");
            UiManager.Instance.UpdateScorelLabel("���� ���ھ� : " + score);

            yield return new WaitForSeconds(1.5f);
            mosterAnimator.SetBool("ani_Damage", false);
        }
        yield return new WaitForSeconds(0);
    }

    /// ��α� - ���������
    IEnumerator ShoulderPress()
    {
        // 1. LEFT_WRIST = 4   LEFT_ELBOW = 2   LEFT_SHOULDER = 0
        // 2. RIGHT_WRIST = 5   RIGHT_ELBOW = 3   RIGHT_SHOULDER = 1
        int grade = 0;
        for (int i = 0; i < count; i++)
        {
            UiManager.Instance.UpdateActionCount(i + 1, count);
            SoundManager.instance.PlaySFX("SFX_Count_1");

            // ���� ����
            yield return new WaitForSeconds(1.5f);
            float angle1 = GetComponent<AngleCalculator>().GetAngle(playerLandmark[4], playerLandmark[2], playerLandmark[0], playerLandmark[2]);
            float angle2 = GetComponent<AngleCalculator>().GetAngle(playerLandmark[5], playerLandmark[3], playerLandmark[1], playerLandmark[3]);
            grade = 0;

            // �� 1�� - ���� ���� �����ϰ� ���ηȳ�
            if (angle1 >= 80 && angle1 <=100) { grade += 5; }
            else if (angle1 >= 70 && angle1 <= 110) { grade += 3; }
            else if (angle1 >= 60 && angle1 <= 120) { grade += 1; }

            // �� 2�� - ������ ���� �����ϰ� ���ηȳ�
            if (angle2 >= 80 && angle1 <= 100) { grade += 5; }
            else if (angle2 >= 70 && angle1 <= 110) { grade += 3; }
            else if (angle2 >= 60 && angle1 <= 120) { grade += 1; }

            //���� ��
            MotionRating(grade);

            // �����ڿ� ��
            UiManager.Instance.UpdateAngle1Label("���� ���� �����ϰ� ���ηȳ� : " + angle1);
            UiManager.Instance.UpdateAngle2Label("������ ���� �����ϰ� ���ηȳ� : " + angle2);
            UiManager.Instance.UpdateOverallLabel("���� ���� ��(10�� ����) : " + grade + "��");
            UiManager.Instance.UpdateScorelLabel("���� ���ھ� : " + score);

            yield return new WaitForSeconds(1.5f);
            mosterAnimator.SetBool("ani_Damage", false);
        }
        yield return new WaitForSeconds(0);
    }

    /// ��α� - ű��(��)
    IEnumerator R_KickBack()
    {
        // 1. RIGHT_SHOULDER = 1   RIGHT_HIP = 13   RIGHT_ANKLE = 17
        // 2. RIGHT_WRIST = 5   RIGHT_ELBOW = 3   RIGHT_HIP = 13
        int grade = 0;
        for (int i = 0; i < count; i++)
        {
            UiManager.Instance.UpdateActionCount(i + 1, count);
            SoundManager.instance.PlaySFX("SFX_Count_1");

            // ���� ����
            yield return new WaitForSeconds(1.5f);
            float angle1 = GetComponent<AngleCalculator>().GetAngle(playerLandmark[1], playerLandmark[13], playerLandmark[17], playerLandmark[13]);
            float angle2 = GetComponent<AngleCalculator>().GetAngle(playerLandmark[5], playerLandmark[3], playerLandmark[13], playerLandmark[3]);
            grade = 0;

            // �� 1�� - ��ü�� ������ �����°�?
            if (angle1 >= 160) { grade += 5; }
            else if (angle1 >= 150) { grade += 3; }
            else if (angle1 >= 140) { grade += 1; }

            // �� 2�� - ���� �����ϰ� ����°�?
            if (angle2 <= 25) { grade += 5; }
            else if (angle2 <= 30) { grade += 3; }
            else if (angle2 <= 40) { grade += 1; }

            //���� ��
            MotionRating(grade);

            // �����ڿ� ��
            UiManager.Instance.UpdateAngle1Label("��ü�� ������ �����°� : " + angle1);
            UiManager.Instance.UpdateAngle2Label("���� �����ϰ� ����°� : " + angle2);
            UiManager.Instance.UpdateOverallLabel("���� ���� ��(10�� ����) : " + grade + "��");
            UiManager.Instance.UpdateScorelLabel("���� ���ھ� : " + score);

            yield return new WaitForSeconds(1.5f);
            mosterAnimator.SetBool("ani_Damage", false);
        }
        yield return new WaitForSeconds(0);
    }


    /// ��α� - ű��(��)
    IEnumerator L_KickBack()
    {
        // 1. LEFT_SHOULDER = 0   LEFT_HIP = 12   LEFT_ANKLE = 16
        // 2. LEFT_WRIST = 4  LEFT_ELBOW = 2   LEFT_HIP = 12
        int grade = 0;
        for (int i = 0; i < count; i++)
        {
            UiManager.Instance.UpdateActionCount(i + 1, count);
            SoundManager.instance.PlaySFX("SFX_Count_1");

            // ���� ����
            yield return new WaitForSeconds(1.5f);
            float angle1 = GetComponent<AngleCalculator>().GetAngle(playerLandmark[0], playerLandmark[12], playerLandmark[16], playerLandmark[12]);
            float angle2 = GetComponent<AngleCalculator>().GetAngle(playerLandmark[4], playerLandmark[2], playerLandmark[12], playerLandmark[2]);
            grade = 0;

            // �� 1�� - ��ü�� ������ �����°�?
            if (angle1 >= 160) { grade += 5; }
            else if (angle1 >= 150) { grade += 3; }
            else if (angle1 >= 140) { grade += 1; }

            // �� 2�� - ���� �����ϰ� ����°�?
            if (angle2 <= 25) { grade += 5; }
            else if (angle2 <= 30) { grade += 3; }
            else if (angle2 <= 40) { grade += 1; }

            //���� ��
            MotionRating(grade);

            // �����ڿ� ��
            UiManager.Instance.UpdateAngle1Label("��ü�� ������ �����°� : " + angle1);
            UiManager.Instance.UpdateAngle2Label("���� �����ϰ� ����°� : " + angle2);
            UiManager.Instance.UpdateOverallLabel("���� ���� ��(10�� ����) : " + grade + "��");
            UiManager.Instance.UpdateScorelLabel("���� ���ھ� : " + score);

            yield return new WaitForSeconds(1.5f);
            mosterAnimator.SetBool("ani_Damage", false);
        }
        yield return new WaitForSeconds(0);
    }
    /* ---------------------------------------------------------------- */


    private void Start()
    {
        score = 0;

        // �ڴ��� ��ȣ �Ѱܹް� �ش� ��ƾ �����ϱ��
        dunNum = PlayerPrefs.GetInt("MonsterNumberToSend");

        // ���� �ִϸ�����
        mosterAnimator = monster.GetComponent<MonsterController>().animator;

        // ������ �´� � ��ƾ ����
        switch (dunNum)
        {
            case 0:
                Debug.Log("���� ��ƾ�� �����մϴ�.");
                // ���� ��ƾ�� ȣ���ϼ���
                break;
            case 1:
                Debug.Log("�� ��ƾ�� �����մϴ�.");
                // �� ��ƾ�� ȣ���ϼ���
                break;
            case 2:
                Debug.Log("���� ��ƾ�� �����մϴ�.");
                // ���� ��ƾ�� ȣ���ϼ���
                break;
            case 3:
                Debug.Log("�㸮 ��ƾ�� �����մϴ�.");
                // �㸮 ��ƾ�� ȣ���ϼ���
                break;
            case 4:
                Debug.Log("�̵� ��ƾ�� �����մϴ�.");
                // �̵� ��ƾ�� ȣ���ϼ���
                break;
            case 5:
                Debug.Log("���ϱ� ��ƾ�� �����մϴ�.");
                // ���ϱ� ��ƾ�� ȣ���ϼ���
                break;
            case 6:
                Debug.Log("��α� ��ƾ�� �����մϴ�.");
                guideModel = Instantiate(guideModels[1]);
                guideModel.transform.SetParent(mainCamera.transform, false);
                animator = guideModel.GetComponent<Animator>();
                StartCoroutine(RunTricepsRoutine());
                break;
            case 7:
                Debug.Log("�� ��ƾ�� �����մϴ�.");
                // �� ��ƾ�� ȣ���ϼ���
                break;
            case 8:
                Debug.Log("����� ��ƾ�� �����մϴ�.");
                guideModel = Instantiate(guideModels[0]);
                guideModel.transform.SetParent(mainCamera.transform, false);
                animator = guideModel.GetComponent<Animator>();
                StartCoroutine(RunThighRoutine());
                break;
            case 9:
                Debug.Log("���Ƹ� ��ƾ�� �����մϴ�.");
                // ���Ƹ� ��ƾ�� ȣ���ϼ���
                break;
            default:
                Debug.Log("���� ���� �ȵǴµ�?");
                break;
        }
    }
}