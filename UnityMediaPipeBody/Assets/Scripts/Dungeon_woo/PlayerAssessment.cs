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


    // �� ���Ͱ��� ���� ���() 0������ 180�� ������ ��
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
    // �� ���Ͱ��� ��ȣ�ִ� ���� ���() -180������ 180
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

    // ���� ����()
    private void Attack()
    {
        monster.GetComponent<MonsterController>().TakeDamage(damage);
    }


    private void Update()
    {
        // (�ӽ�)�����̽��ٸ� ������ �� ����
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
        */

        // ���ĵ����̵巹�׷����� ����
        float n = GetAngle(playerLandmark[12], playerLandmark[13], playerLandmark[17], playerLandmark[13]);
        if (n >= 120)
        {
            Attack();
            Debug.Log("����!!!!!");
        }
    }
}