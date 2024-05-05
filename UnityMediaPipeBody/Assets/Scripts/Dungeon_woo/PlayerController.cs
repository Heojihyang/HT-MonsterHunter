using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject monster;
    public float damage = 7.5f;

    private void Update()
    {
        // �����̽��ٸ� ������ �� �����ϴ� ������ ����
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

    // ���� ���帶ũ ��ġ ��������
    private void getLandmarkPosition()
    {
        /*
        GetComponent<PipeServer>().instances;
        for (int i = 0; i < instances.Length; i++)
        {
            Debug.Log("Array Element " + i + ": " + array[i]);
        }
        */
    }
}