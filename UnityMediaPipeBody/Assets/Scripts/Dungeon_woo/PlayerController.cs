using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // ���� ����
    public GameObject monster;
    public float damage = 7.5f;

    // �÷��̾� ���帶ũ ��ġ (PipeServer�� ������ ���� ��)
    public Vector3[] PlayerLandmarkPosition = new Vector3[33];
    public Vector3 HeadLandmarkPosition = new Vector3(0, 0, 0);


    private void Update()
    {
        // (�ӽ�)�����̽��ٸ� ������ �� �����ϴ� ������ ����
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    // ���� ���� �Լ�
    private void Attack()
    {
        monster.GetComponent<MonsterController>().TakeDamage(damage);
    }

    // ���� ���帶ũ ��ġ ��������(�������������� ������ ������ ȣ��, ����)
    private void getLandmarkPosition(GameObject[] landmark, GameObject head)
    {
        /*
        // ��
        for (int i = 0; i < LANDMARK_COUNT; ++i)
        {
            PlayerLandmarkPosition[i] = landmark[i].transform.localPosition;
        }
        // �Ӹ�
        HeadLandmarkPosition = head.transform.position;
        */
    }

    // ���� ��ǥ ��������
    private void getCorrectLandmarkPosition()
    {

    }

    // ���� ����
    private void motionEquality()
    {

    }

}