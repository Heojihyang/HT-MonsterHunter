using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonsterController : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    // �������� ������ �Լ�
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("���Ķ�!");

        // ���� ü���� 0 �������� Ȯ���Ͽ� ��� ó��
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // ��� ó�� �Լ�
    private void Die()
    {
        // ���⿡ ��� ó�� ���� �߰�
        Debug.Log("��");
        Destroy(gameObject); // ���� ���� ������Ʈ �ı�
    }
}