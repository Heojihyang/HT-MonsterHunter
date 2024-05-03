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
}